using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manage
{
    public interface Irepository<t> where t : class
    {
        void add(t addedclass);
        void delete(t updatedclass);
        void update(t updatedclass);
        IQueryable<t> select();
        t Get_Item(int Id);
    }
}
