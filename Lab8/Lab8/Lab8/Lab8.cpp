// Lab8.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <iostream>
class AverageCounter 
{
	public:
		AverageCounter();
		double getAverage();
		friend std::istream& operator>>(std::istream& input, AverageCounter& counter);
		friend std::ostream& operator<<(std::ostream& output, AverageCounter& counter);
	private:
		void update(double value);
		unsigned count;
		double combined;
};
AverageCounter::AverageCounter(): count(0), combined(0.0) {}
void AverageCounter::update(double value) 
{
	combined += value;
	++count;
}
std::istream& operator>>(std::istream& input, AverageCounter& counter)
{
	double value = 0;
	if (input >> value) 
	{
		counter.update(value);
		return input;
	}
	return input >> value;
}

std::ostream& operator<<(std::ostream& output, AverageCounter& counter) 
{
	return output << "Average value: " << counter.getAverage();
}

double AverageCounter::getAverage() 
{
	return combined / count;
}

int _tmain(int argc, _TCHAR* argv[])
{
	AverageCounter counter;
	while (std::cin >> counter) 
	{
		std::cout << counter << std::endl;
	}
	return 0;
}

