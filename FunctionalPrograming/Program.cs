
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine(Tomorrow());
Console.WriteLine(TomorrowPure(new DateTime(2025,05,15,00,00,00)));


var beer = new Beer()
{
    Name = "Cristal"
};

Console.WriteLine(ToUpperPure(beer).Name);
Console.WriteLine(beer.Name);

// una funcion no pura 
DateTime Tomorrow()
{
    return DateTime.Now.AddDays(1);
}

Beer ToUpper(Beer beer)
{
    beer.Name =  beer.Name.ToUpper();
    return beer;
}

// Función pura se quita la responsabilidad de saber en este ejemplo que día es hoy
DateTime TomorrowPure(DateTime date)
{
    return date.AddDays(1);
}
Beer ToUpperPure(Beer beer)
{
    var beer2 = new Beer()
    {
        Name = beer.Name.ToUpper(),
    };

    return beer2;
}

// Un delegado es un tipo que tiene especificado a una funcion
// Un action nunca retorna nada, son void
Action<string> show = Console.WriteLine;
show("Alonso");

// Expresiones Lambda
// Permite crear funciones sin nombre
Action<string> hi = name => Console.WriteLine($"Hola {name}");
hi("Alonso");

Action<int, int> add = (a, b) => show((a + b).ToString());
add(5, 6);


// Tipo Func
// define una funcion que siempre retorna algo
// El utimo elemento es el tipo de dato que retorna
Func<int,int, int> mul = (a, b) => a + b;
show(mul(3, 4).ToString());

Func<int, int, string> mulString = (a, b) =>
{
    var res = a * b;
    return res.ToString();
};

show(mulString(10, 50));

// Funcion de orden superior
// puede retornar una funcion como resultado
List<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

// Definen funciones y siempre retornan un true o un false
Predicate<int> condition1 = x => x % 2 == 0;
Predicate<int> condition2 = x => x > 5;

var numbers2 = Filter(numbers, condition1);
var numbers3 = Filter(numbers, condition2);

foreach (int number in numbers2)
{
    Console.WriteLine(number);
}

List<int> Filter(List<int> list, Predicate<int> condition)
{
    var resultList = new List<int>();

    Console.WriteLine("Filter");
    foreach (var item in list)
    {
        if (condition(item))
        {
            resultList.Add(item);
        }
    }

    return resultList;
}

public class Beer
{
    public string Name { get; set; }
}