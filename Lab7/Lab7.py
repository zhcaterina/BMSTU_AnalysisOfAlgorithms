from random import randint
import numpy
from time import *

#shortest_route - длина этого маршрута

#values = {'alpha': 0,  # вес феромона
#          'beta': 0,  # вес феромона
#          'e': 3,  # элитные муравьи
#          'Q': 10,  # параметр, имеющий значение порядка цены оптимального решения
#          'p': 0.5}  # интенсивность испарения



def create_distance_matrix(cities_number):
    matrix = numpy.zeros((cities_number, cities_number))
    max_len = int(input('Введите максимальное расстояние '))
    for i in range(cities_number):
        for j in range(i + 1, cities_number):
            distance = randint(1, max_len)  # от min до max расстояния
            matrix[i][j], matrix[j][i] = distance, distance # для того, чтобы матрица расстояний была симметричной

    return matrix


def ant_colony_optimization(distance_matrix, alpha, beta, e, Q, p, cities_number,time_max):
    shortest_route, length_route = None, None  # кратчайший путь и его длина
    pheromons = numpy.random.sample((cities_number, cities_number))# размещение
    visibility = 1 /( distance_matrix+0.0001)# присвоение видимости 

    time = 0

    while time < time_max:#6
        for_all_ants = numpy.zeros((cities_number, cities_number)) #генерация

        # для каждого муравья:
        for k in range(cities_number):
            ant_town, length_to_town = [k], 0
            current_town = k
            while len(ant_town) != cities_number:
                desired_cities = [r for r in range(cities_number)]
                for visited_town in ant_town:  
                    desired_cities.remove(visited_town) 
                probability = [0 for t in desired_cities] 
                for j in desired_cities: 
                    if distance_matrix[current_town][j] != 0: 
                        temp = sum(
                            (pheromons[current_town][l] ** alpha) * (visibility[current_town][l] ** beta) for l in
                            desired_cities)
                        probability[desired_cities.index(j)] = \
                            (pheromons[current_town][j] ** alpha) * (visibility[current_town][j] ** beta) / temp
                    else:
                        probability[desired_cities.index(j)] = 0

                max_probability = max(probability)
                if max_probability == 0: # проверка на лучшее решение (10)
                    return "Муравьишка в беде, он изолирован!"

                selected_city = probability.index(max_probability) 
                ant_town.append(desired_cities[selected_city]) 
                length_to_town += distance_matrix[current_town][desired_cities[selected_city]] 
                current_town = desired_cities.pop(selected_city)
            if length_route is None or (length_to_town + distance_matrix[ant_town[0]][ant_town[-1]]) < length_route:
                length_route = length_to_town + distance_matrix[ant_town[0]][ant_town[-1]]
                shortest_route = ant_town

            for pheromone in range(len(ant_town) - 1): # -1 из-за особенностей python (12)
                a = ant_town[pheromone]
                b = ant_town[pheromone + 1]
                for_all_ants[a][b] += Q / length_to_town

        for_elite = (e * Q / length_route) if length_route else 0 #  «элитные» муравьи усиливают рёбра наилучшего маршрута
        pheromons = (1 - p) * pheromons + for_all_ants + for_elite # найденного с начала работы алгоритма
        time += 1
    length_route=0
    for i in range(len(distance_matrix)-1):
        length_route+=distance_matrix[shortest_route[i]][shortest_route[i+1]]
    return length_route #shortest_route, length_route
def main():
    cities_number=int(input('Введите количество городов '))
    matrix=create_distance_matrix(cities_number)
    print(matrix)
    #alpha = float(input('Введите вес ферамона alpha '))  # вес феромона
    #beta = float(input('Введите вес ферамона beta '))  # вес феромона
    #e = int(input('Введите количеств ферамона, выделяемое элитным муравьем ')) # элитные муравьи
    #Q = int(input('Порядок цены оптимального решения '))  # параметр, имеющий значение порядка цены оптимального решения
    #p = float(input('Ваедите интенсивность испарения ')) # интенсивность испарения
   
    m = 0
    k = 0
    while(1):
        #print('\n')
        #time_max=(int(input('Введите количество поколений муравьев  ')))
        time_max = 10
        #alpha = float(input('Введите вес ферамона alpha '))  # вес феромона
        #beta = float(input('Введите вес ферамона beta '))  # вес феромона
        #e = int(input('Введите количество ферамона, выделяемое элитным муравьем ')) # элитные муравьи
        #Q = int(input('Порядок цены оптимального решения '))  # параметр, имеющий значение порядка цены оптимального решения
        #p = float(input('Ваедите интенсивность испарения ')) # интенсивность испарения

        p = m
        beta = k
        #alpha = 1
        #beta = 1
        alpha = 1 - beta
        e = 3
        Q = 10
        #p = 0.5
       
        print(alpha, beta, p, sep = '    ', end = '')
        print('    ',ant_colony_optimization(matrix,alpha,beta,e,Q,p,cities_number,time_max), sep = '    ')
        m += 0.1
        if (m > 1):
            m = 0
            k += 0.1
        if (k > 1):
            break
        

def main_time():
    while(1):
        cities_number=int(input('Введите количество городов '))
        matrix=create_distance_matrix(cities_number)
        print(matrix)
        #alpha = float(input('Введите вес ферамона alpha '))  # вес феромона
        #beta = float(input('Введите вес ферамона beta '))  # вес феромона
        #e = int(input('Введите количеств ферамона, выделяемое элитным муравьем ')) # элитные муравьи
        #Q = int(input('Порядок цены оптимального решения '))  # параметр, имеющий значение порядка цены оптимального решения
        #p = float(input('Ваедите интенсивность испарения ')) # интенсивность испарения
    
        print('\n')
        time_max=(int(input('Введите количество поколений муравьев  ')))
        #alpha = float(input('Введите вес ферамона alpha '))  # вес феромона
        #beta = float(input('Введите вес ферамона beta '))  # вес феромона
        #e = int(input('Введите количеств ферамона, выделяемое элитным муравьем ')) # элитные муравьи
        #Q = int(input('Порядок цены оптимального решения '))  # параметр, имеющий значение порядка цены оптимального решения
        #p = float(input('Ваедите интенсивность испарения ')) # интенсивность испарения
        alpha = 1
        beta = 1
        e = 3
        Q = 10
        p = 0.5
        t1 =time()
        print('\n')
        print(ant_colony_optimization(matrix,alpha,beta,e,Q,p,cities_number,time_max))
        t2 = time()
        print(t2-t1)
#main_time()
main()
