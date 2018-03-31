// Lab4.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <iostream>
#include <cstdint>
#include <fstream>
#include "matrix.h"

int _tmain(int argc, _TCHAR* argv[])
{
	unsigned size = 100;
	unsigned topRandom = 100;

	const unsigned maxNumberOfThreads = 4;

	std::ofstream out;
	out.open("out.txt");


	unsigned numberOfTests = 100;
	std::clock_t buffer = 0;

	for (unsigned i = 1; i <= 2; ++i) {
		size *= i;

		Matrix original(topRandom, size, size);
		Matrix mult(topRandom, size, size);

		out << "SIZE: " << size << std::endl;

		for (unsigned numberOfThreads = 1; numberOfThreads <= maxNumberOfThreads; ++numberOfThreads) {

			std::clock_t result = 0;
			for (unsigned i = 0; i < numberOfTests; ++i) {
				original.vinogradParallel(mult, numberOfThreads, buffer);
				result += buffer;
			}
			std::cout << "NUMBER OF THREADS: " << numberOfThreads << std::endl << "TIME: " << result << std::endl;
//			std::cout << "END " << i << std::endl;
		}

		size /= i;
}
	return 0;
}

