from alg import *
from time import *

#A = [[1, 0, 5], [2, 1, 4], [-1, 1, 3]]
#A = [[1, 0], [2, 1], [-1, 1]]
A = [[1, 2, 3], [4, 5, 0], [1, 0, 1]]
#B = [[1, 2, 0], [0, -1, 1], [0, 3, 2]]
#B = [[3, 4, 5], [6, 7, 8]]
B = [[1, 0, 1, 1], [1, 0, 0, 0], [1, 0, 1, 0]]
m = base(A, B)
print(m)
m = vinograd(A, B)
print(m)
m = vinograd_mod(A, B)
print(m)
