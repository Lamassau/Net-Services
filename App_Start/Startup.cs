using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(tekno.Services.Startup), "Init")]


namespace tekno.Services
{


    public class Startup
    {
        public static void Init()
        {
            AutoMapperConfig.RegisterMappings();
            
            
        }

    }

}
