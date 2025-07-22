using System;

namespace Dummy.Services
{
    public interface ICalculatorService
    {
        int Add(int a, int b);
        string Concat(string a, int b);
        double MultiplyAndAdd(double a, int b);
        string RepeatString(string s, int times);
    }
} 