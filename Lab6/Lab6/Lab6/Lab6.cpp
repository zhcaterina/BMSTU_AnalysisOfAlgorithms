// Lab6.cpp: определ€ет точку входа дл€ консольного приложени€.
//

#include "stdafx.h"
#include <cmath>

const double accuracy =  0.000001;

double gauss(unsigned m, double** matrix)
{ 
	double result = 1; //(1)
	for (unsigned i = 0; i < m; ++i)
	{
		// обмен строк, если €чейка matrix[i][i] нулева€ на ту, где €чейка на той же позиции ненулева€ 
		if (fabs(matrix[i][i]) < accuracy) //(2)
		{
			for (unsigned j = i + 1; j < m; ++j)
			{ 
				if (fabs(matrix[j][i]) > accuracy) //(3)
				{
				double* buffer = matrix[i]; //(4)
				matrix[i] = matrix[j]; //(5)
				matrix[j] = buffer; //(6)
				result *= (-1);
				break;
				}
			}
		}
		// если обмен не произошел Ц определитель равен нулю 
		if (fabs(matrix[i][i]) < accuracy) //(7)
		{
		return 0; //(8)
		}
		// вычисл€ем новую итерацию определител€ 
		result *= matrix[i][i];//; (9)
		// приводим к треугольному виду
		for (unsigned j = i + 1; j < m; ++j)
		{ 
			if (fabs(matrix[j][i]) >= accuracy) //(10)
			{
				double q = matrix[j][i] / matrix[i][i]; //(11)
				for (unsigned k = i; k < m; ++k)
				{ 
					matrix[j][k] -= matrix[i][k] * q; //(12)
				}
			}
		}
	} 
	return result; //(13)
}

int _tmain(int argc, _TCHAR* argv[])
{
	return 0;
}

