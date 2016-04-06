using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekno.Services.Data
{
    public class pocoFactory
    {
        public static T New<T>() where T : teknoData, new()
        {
            T i = new T();
            i.InitAdd();
            return i;
        }

        public static T Update<T>(T item) where T : teknoData
        {
            item.InitUpdate();
            return item;
        }

    }
}
