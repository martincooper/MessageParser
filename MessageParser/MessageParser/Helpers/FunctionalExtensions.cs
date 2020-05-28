using System;

namespace MessageParser.Helpers
{
    /// <summary>
    /// Forward Pipe operator.
    /// Converts funcA(funcB(funcC(obj))) into funcC(obj) |> funcB() |> funcA() 
    /// </summary>
    public static class FunctionalExtensions
    {
        public static C Pipe<A,B,C>(this A obj, Func<A,B> func1, Func<B,C> func2) => 
            func2(func1(obj));

        public static D Pipe<A,B,C,D>(this A obj, Func<A,B> func1, Func<B,C> func2, Func<C,D> func3) => 
            func3(func2(func1(obj)));

        public static E Pipe<A,B,C,D,E>(this A obj, Func<A,B> func1, Func<B,C> func2, Func<C,D> func3, Func<D,E> func4) => 
            func4(func3(func2(func1(obj))));
    }
}