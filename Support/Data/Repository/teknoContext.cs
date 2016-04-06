using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

public partial class teknoContext : DbContext
{
    public teknoContext()
                : base("name=teknoContext")
    {
    }
}