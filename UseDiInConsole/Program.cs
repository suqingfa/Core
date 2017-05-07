using Microsoft.Extensions.DependencyInjection;
using System;

namespace UseDiInConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var provider = new ServiceCollection()
                .AddTransient<IService, Service>()
                .AddTransient<Program>()
                .BuildServiceProvider();

            var service = provider.GetService<Program>();
        }

        public Program(IService s)
        {
            s.Test();
            fu();

            int fu() => 10;
        }
    }

    public interface IService
    {
        void Test();
    }

    public class Service : IService
    {
        public void Test() => Console.WriteLine("Test Method");
    }
}