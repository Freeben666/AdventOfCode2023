char[,] grid;

var input = File.ReadAllLines("input.txt");

int columns = input[0].Length; // L'axe des X fait la longueur d'une ligne du fichier d'entrée
int rows = input.Length; // L'axe des Y fait la hauteur du fichier d'entrée

grid = new char[rows,columns];
