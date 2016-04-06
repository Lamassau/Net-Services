
namespace tekno.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class teknoData
    {
        public teknoData()
        {
            Initialize();
        }

        internal virtual void Initialize()
        {

        }

        public virtual void InitAdd()
        {

            if (typeof(IData).IsAssignableFrom(this.GetType()))
            {
                IData ie = (IData)this;
                ie.WhoCreated = "teknoApp";
                ie.WhenCreated = DateTime.Now;
                ie.WhoUpdated = "teknoApp";
                ie.WhenUpdated = DateTime.Now;
                ie.IsActive = true;
                ie.IsDeleted = false;
            }


        }

        public void InitUpdate()
        {
            if (typeof(IData).IsAssignableFrom(this.GetType()))
            {
                IData ie = (IData)this;
                ie.WhoUpdated = "teknoApp";
                ie.WhenUpdated = DateTime.Now;
            }
        }



    }


    public interface IData
    {
        string WhoUpdated { get; set; }
        DateTime WhenUpdated { get; set; }
        string WhoCreated { get; set; }
        DateTime WhenCreated { get; set; }
        bool IsActive { get; set; }
        bool IsDeleted { get; set; }
        byte[] rowVersion { get; set; }

        void InitAdd();
        void InitUpdate();

    }

}