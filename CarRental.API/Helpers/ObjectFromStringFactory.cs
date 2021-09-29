using CarRental.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Helpers
{
    public class ObjectFromStringFactory<T>
        where T : IPriceCounter
    {
        public T GetInstance(string strFullyQualifiedName)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return (T)Activator.CreateInstance(t);
        }
    }
}
