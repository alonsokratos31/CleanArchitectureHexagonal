// Variables

int number = 10;
number = 10;
double dec = 12.3;
// float 7 digitos
float dec2 = 12.3f;
bool thereIsBeer = false;
var num = 15;

// Arrays
int[] numbers = new int[5];
numbers[0] = 2;
int[] numbers2 = { 1, 2, 3, 4, 5 };
Console.WriteLine(numbers[0]);

// Sentencias condicionales
int age = 12;
if(age > 60)
{
    Console.WriteLine("Es de la tercera edad");
}
else if (age > 18)
{
    Console.WriteLine("Es mayor de edad");
}
else
{
    Console.WriteLine("No es de la tercera edad");
}

switch (age)
{
    case < 18:
        Console.WriteLine("Es mayor de edad");
        break;

    case < 60:
        Console.WriteLine("Es de la tercera edad");
        break;

    default:
        Console.WriteLine("No es de la tercera edad");
        break;

}

// Sentencias de Iteracion

var names = new string[3]
{
    "Alonso", "Jessica", "Leonardo"
};

int i = 0;
do
{
    Console.WriteLine(names[i]);
    i++;

} while (i < names.Length);

while (i < names.Length)
{
    Console.WriteLine(names[i]);
    i++;
}

for(int pi = 0; pi < names.Length; pi++)
{
    Console.WriteLine(names[pi]);
}


// Funciones

int resultado = Area(5);
Console.WriteLine(resultado);

int Area(int s)
{
    int res = s * s;
    return res;
}