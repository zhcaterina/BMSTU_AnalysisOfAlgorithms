#ifndef MATRIX_H
#define MATRIX_H

#include <ctime>

class Matrix {
	public:
		Matrix(unsigned rows = 1, unsigned columns = 1);
		Matrix(int randMax, unsigned rows = 1, unsigned columns = 1);
		Matrix(const Matrix& other);
		~Matrix();

		Matrix classicMult(const Matrix& other, std::clock_t& time) const;
		Matrix classicBufferMult(const Matrix& other, std::clock_t& time) const;

		Matrix vinogradMult(const Matrix& other, std::clock_t& time) const;
		Matrix vinogradImprMult(const Matrix& other, std::clock_t& time) const;

		Matrix vinogradParallel(const Matrix& other, const unsigned numberOfThreads, std::clock_t& time) const;

		void countRowFactor(const unsigned minRow, const unsigned size, unsigned* rowFactor) const;

		void countColumnFactor(const unsigned minColumn, const unsigned size, unsigned* columnFactor) const;
		void countPartialVinograd(const Matrix& other, const unsigned minRow, const unsigned size,
								  const unsigned* rowFactor, const unsigned* columnFactor, Matrix& result) const;
		void unevenVinograd(const Matrix& other, const unsigned minRow, const unsigned size, Matrix& result) const;

	private:
		unsigned rows;
		unsigned columns;
		unsigned** data;
};

#endif // MATRIX_H