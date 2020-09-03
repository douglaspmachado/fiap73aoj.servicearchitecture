using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App.Application.Interfaces
{
    public interface IDataBaseProvider
    {
        IDbConnection Connection { get; }

    }
}
