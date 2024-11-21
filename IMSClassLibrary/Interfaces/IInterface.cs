using IMSClassLibrary.Repos;
using IMSClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSClassLibrary.Interfaces
{
    internal interface IInterface<T>
    {
        ResultObject<T> Add(T item);

        ResultObject<T> AddRange(List<T> items);

        ResultObject<T> Update(T item);
        ResultObject<T> Get(int Id);
        ResultObject<List<T>> GetAll();
        ResultObject<T> DeleteById(int Id);
        ResultObject<T> Delete(T item);

    }
}
