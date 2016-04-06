
using StructureMap;
using StructureMap.Configuration.DSL;

namespace tekno.Services
{

    public static class IOCConfig
    {


        public static IContainer GetContainer()
        {



            return  new Container(x =>
            {
                x.AddRegistry<AppServiceRegistry>();
                
            });


        }
    }

    public class AppServiceRegistry : Registry
    {
        public AppServiceRegistry()
        {




        }
    }
}
