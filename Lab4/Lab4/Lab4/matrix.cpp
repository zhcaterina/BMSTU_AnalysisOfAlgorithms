#include "stdafx.h"
#include "matrix.h"
#include <exception>
#include <random>
#include <iostream>
#include <thread>
#include <vector>

//using std::thread;
Matrix::Matrix(int randMax, unsigned rows, unsigned columns): rows(rows), columns(columns) {
	if (rows == 0 || columns == 0) {
		throw std::logic_error("Wrong parameters passed to constuctor!");
	}

	data = new unsigned*[rows];
	for (unsigned i = 0; i < rows; ++i) {
		data[i] = new unsigned[columns];
	}
	srand(time(nullptr));

	for (unsigned i = 0; i < rows; ++i) {
		for (unsigned j = 0; j < columns; ++j) {
			data[i][j] = rand() % randMax;
		}
	}
}

Matrix::Matrix(unsigned rows, unsigned columns): rows(rows), columns(columns) {
	if (rows == 0 || columns == 0) {
		throw std::logic_error("Wrong parameters passed to constuctor!");
	}

	data = new unsigned*[rows];
	for (unsigned i = 0; i < rows; ++i) {
		data[i] = new unsigned[columns];
	}
}

Matrix::Matrix(const Matrix& other): rows(other.rows), columns(other.columns) {

	data = new unsigned*[rows];
	for (unsigned i = 0; i < rows; ++i) {
		data[i] = new unsigned[columns];
	}

	for (unsigned i = 0; i < rows; ++i) {
		for (unsigned j = 0; j < rows; ++j) {
			data[i][j] = other.data[i][j];
		}
	}
}

Matrix::~Matrix() {

	for (unsigned i = 0; i < rows; ++i) {
		delete[] data[i];
	}
	delete[] data;
}

Matrix Matrix::classicMult(const Matrix &other, std::clock_t& time) const {
	if (columns != other.rows) {
		throw std::logic_error("Sizes don't match!");
	}
	Matrix result(rows, other.columns);

	std::clock_t start = std::clock();

	for (unsigned i = 0; i < rows; ++i) {
		for (unsigned j = 0; j < columns; ++j) {
			data[i][j] = 0;
			for (unsigned index = 0; index < columns; ++index) {
				data[i][j] = data[i][j] + data[i][index] * other.data[index][j];
			}
		}
	}
	time = std::clock() - start;

	return result;
}

Matrix Matrix::classicBufferMult(const Matrix &other, std::clock_t& time) const {
	if (columns != other.rows) {
		throw std::logic_error("Sizes don't match!");
	}
	Matrix result(rows, other.columns);

	std::clock_t start = std::clock();

	for (unsigned i = 0; i < rows; ++i) {
		for (unsigned j = 0; j < columns; ++j) {
			unsigned buffer = 0;
			for (unsigned index = 0; index < columns; ++index) {
				buffer += data[i][index] * other.data[index][j];
			}
			data[i][j] = buffer;
		}
	}

	time = std::clock() - start;

	return result;
}

Matrix Matrix::vinogradMult(const Matrix &other, std::clock_t& time) const {
	if (columns != other.rows) {
		throw std::logic_error("Sizes don't match!");
	}
	Matrix result(rows, other.columns);

	unsigned* rowFactor = new unsigned[rows];
	unsigned* columnFactor = new unsigned[other.columns];

	std::clock_t start = std::clock();

	for (unsigned i = 0; i < rows; ++i) {
		rowFactor[i] = data[i][0] * data[i][1];
		for (unsigned j = 2; j < columns - 1; j += 2) {
			rowFactor[i] = rowFactor[i] + data[i][j] * data[i][j + 1];
		}
	}

	for (unsigned i = 0; i < other.columns; ++i) {
		columnFactor[i] = other.data[0][i] * other.data[1][i];
		for (unsigned j = 2; j < other.rows - 1; j += 2) {
			columnFactor[i] = columnFactor[i] + other.data[j][i] * other.data[j + 1][i];
		}
	}

	for (unsigned i = 0; i < rows; ++i) {
		for (unsigned j = 0; j < other.columns; ++j) {
			result.data[i][j] = 0;
			for (unsigned k = 0; k < columns - 1; k += 2) {
				result.data[i][j] = result.data[i][j] + (data[i][k] + other.data[k + 1][j]) *
						(data[i][k + 1] + other.data[k][j]);
			}
			result.data[i][j] = result.data[i][j] - (rowFactor[i] + columnFactor[j]);
		}
	}

	if (columns % 2 != 0) {
		unsigned columnsDiv2 = columns / 2;
		for (unsigned i = 0; i < rows; ++i) {
			for (unsigned j = 0; j < other.columns; ++j) {
				result.data[i][j] = result.data[i][j] + data[i][columnsDiv2] * other.data[columnsDiv2][j];
			}
		}
	}

	time = std::clock() - start;

	delete[] columnFactor;
	delete[] rowFactor;

	return result;
}

Matrix Matrix::vinogradImprMult(const Matrix &other, std::clock_t& time) const {
	if (columns != other.rows) {
		throw std::logic_error("Sizes don't match!");
	}
	Matrix result(rows, other.columns);

	unsigned* rowFactor = new unsigned[rows];
	unsigned* columnFactor = new unsigned[other.columns];

	std::clock_t start = std::clock();

	for (unsigned i = 0; i < rows; ++i) {
		rowFactor[i] = data[i][0] * data[i][1];
		unsigned buffer = rowFactor[i];
		for (unsigned j = 2; j < columns - 1; j += 2) {
			buffer += data[i][j] * data[i][j + 1];
		}
		rowFactor[i] += buffer;
	}

	for (unsigned i = 0; i < other.columns; ++i) {
		columnFactor[i] = other.data[0][i] * other.data[1][i];
		unsigned buffer = columnFactor[i];
		for (unsigned j = 2; j < other.rows - 1; j += 2) {
			buffer += other.data[j][i] * other.data[j + 1][i];
		}
		columnFactor[i] = buffer;
	}

	for (unsigned i = 0; i < rows; ++i) {
		for (unsigned j = 0; j < other.columns; ++j) {
			unsigned buffer = 0;
			for (unsigned k = 0; k < columns - 1; k += 2) {
				buffer += (data[i][k] + other.data[k + 1][j]) *
						(data[i][k + 1] + other.data[k][j]);
			}
			result.data[i][j] = buffer;
			result.data[i][j] -= (rowFactor[i] + columnFactor[j]);
		}
	}

	if (columns % 2 != 0) {
		unsigned columnsDiv2 = columns / 2;
		for (unsigned i = 0; i < rows; ++i) {
			for (unsigned j = 0; j < other.columns; ++j) {
				result.data[i][j] += data[i][columnsDiv2] * other.data[columnsDiv2][j];
			}
		}
	}

	time = std::clock() - start;

	delete[] columnFactor;
	delete[] rowFactor;

	return result;
}

Matrix Matrix::vinogradParallel(const Matrix& other, const unsigned numberOfThreads, std::clock_t& time) const {

	if (columns != other.rows) {
		throw std::logic_error("Sizes don't match!");
	}
	Matrix result(rows, other.columns);

	unsigned* rowFactor = new unsigned[rows];
	unsigned* columnFactor = new unsigned[other.columns];

	std::clock_t start = std::clock();

	std::vector<std::thread> threadVector;

	const double rowFactorThreadSize = rows / (double) numberOfThreads;
	unsigned minRow = 0;
	for (unsigned i = 0; i < numberOfThreads; ++i, minRow += rowFactorThreadSize) {
		threadVector.push_back(std::thread(&Matrix::countRowFactor, this, minRow, (unsigned) rowFactorThreadSize, rowFactor));
	}
	for (std::thread& thread : threadVector) {
		if (thread.joinable()) {
			thread.join();
		}
	}
	threadVector.clear();

	const double columnFactorThreadSize = other.columns / (double) numberOfThreads;
	unsigned minColumn = 0;
	for (unsigned i = 0; i < numberOfThreads; ++i, minColumn += columnFactorThreadSize) {
		threadVector.push_back(std::thread(&Matrix::countColumnFactor, &other, minColumn,
										  (unsigned) columnFactorThreadSize, columnFactor));
	}
	for (std::thread& thread : threadVector) {
		if (thread.joinable()) {
			thread.join();
		}
	}
	threadVector.clear();

	const double rowByThread = rows / (double) numberOfThreads;
	minRow = 0;
	for (unsigned i = 0; i < numberOfThreads; ++i, minRow += rowByThread) {
		threadVector.push_back(std::thread(&Matrix::countPartialVinograd, this, std::ref(other), minRow, rowByThread,
										   rowFactor, columnFactor, std::ref(result)));
	}
	


	for (std::thread& thread : threadVector) {
		if (thread.joinable()) {
			thread.join();
		}
	}

	if (columns % 2 != 0) {
		threadVector.clear();
		minRow = 0;
		for (unsigned i = 0; i < numberOfThreads; ++i, minRow += rowByThread) {
			threadVector.push_back(std::thread(&Matrix::unevenVinograd, this, std::ref(other), minRow, rowByThread,
											   std::ref(result)));
		}

		for (std::thread& thread : threadVector) {
			if (thread.joinable()) {
				thread.join();
			}
		}
	}

	time = std::clock() - start;

	delete[] columnFactor;
	delete[] rowFactor;

	return result;
}

void Matrix::countPartialVinograd(const Matrix& other, const unsigned minRow, const unsigned size,
								  const unsigned* rowFactor, const unsigned* columnFactor, Matrix& result) const {
	unsigned maxRow = minRow + size;
	if (maxRow > rows) {
		maxRow = rows;
	}

	for (unsigned i = minRow; i < maxRow; ++i) {
		for (unsigned j = 0; j < other.columns; ++j) {
			unsigned buffer = 0;
			for (unsigned k = 0; k < columns - 1; k += 2) {
				buffer += (data[i][k] + other.data[k + 1][j]) *
						(data[i][k + 1] + other.data[k][j]);
			}
			result.data[i][j] = buffer;
			result.data[i][j] -= (rowFactor[i] + columnFactor[j]);
		}
	}
}

void Matrix::unevenVinograd(const Matrix& other, const unsigned minRow, const unsigned size, Matrix& result) const {
	const unsigned columnsDiv2 = other.columns / 2;
	unsigned maxRow = minRow + size;
	if (maxRow > rows) {
		maxRow = rows;
	}

	for (unsigned i = minRow; i < maxRow; ++i) {
		for (unsigned j = 0; j < other.columns; ++j) {
			result.data[i][j] += data[i][columnsDiv2] * other.data[columnsDiv2][j];
		}
	}
}


void Matrix::countRowFactor(const unsigned minRow, const unsigned size, unsigned* rowFactor) const {
	unsigned maxRow = minRow + size;
	if (maxRow > rows) {
		maxRow = rows;
	}

	const unsigned maxColumn = columns - 1;

	for (unsigned i = minRow; i < maxRow; ++i) {
		rowFactor[i] = data[i][0] * data[i][1];
		unsigned buffer = rowFactor[i];

		for (unsigned j = 2; j < maxColumn; j += 2) {
			buffer += data[i][j] * data[i][j + 1];
		}

		rowFactor[i] += buffer;
	}
}

void Matrix::countColumnFactor(const unsigned minColumn, const unsigned size, unsigned* columnFactor) const {
	unsigned maxColumn = minColumn + size;
	if (maxColumn > columns) {
		maxColumn = columns;
	}

	const unsigned maxRows = rows - 1;

	for (unsigned i = minColumn; i < maxColumn; ++i) {
		columnFactor[i] = data[0][i] * data[1][i];
		unsigned buffer = columnFactor[i];

		for (unsigned j = 2; j < maxRows; j += 2) {
			buffer += data[j][i] * data[j + 1][i];
		}

		columnFactor[i] = buffer;
	}
}