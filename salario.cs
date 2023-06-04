using System;
class Salario{

static void Main(){

double salario, novoSalario = 0;

Console.WriteLine("Digite o seu salário");
salario = Convert.ToInt32(Console.ReadLine());
novoSalario = salario*1.15;
Console.WriteLine("Seu novo salário será {0}", novoSalario);
    }
}