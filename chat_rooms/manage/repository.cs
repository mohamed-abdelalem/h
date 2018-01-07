using chat_rooms;
using chat_rooms.Models;
using manage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manage
{
    public class repository<t> : Irepository<t> where t : class
    {
        Chat_RoomEntities context;
        DbSet<t> set;
        public repository()
            : this(new Chat_RoomEntities())
        { }
        public repository(Chat_RoomEntities _context)
        {
            context = _context;
            set = context.Set<t>();

        }





        public void add(t addedclass)
        {
            set.Add(addedclass);

            context.SaveChanges();


        }
        //database db = new database();
        public void delete(t updatedclass)
        {
            context.Entry<t>(updatedclass).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
            //   db.insertinto("stu", "name,bd,age", "'khalid',15/5/2012','5'");
        }
        public void update(t updatedclass)
        {

            context.Entry<t>(updatedclass).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public IQueryable<t> select()
        {
            return (set);
        }
        public t Get_Item(int Id)
        {
            return (set.Find(Id));
        }

    }
}
