using CarRental.API.Services.Interfaces;
using CarRental.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace CarRental.API.Helpers
{
    public class ObjectFromStringFactory<T>
        where T : IPriceCounter
    {
        public T GetInstance(string strFullyQualifiedName, string nameSpace)
        {
            Type t = Type.GetType($"{nameSpace}.{strFullyQualifiedName}");
            var instance = Activator.CreateInstance(t);
            return (T)instance;
        }
    }
}
