﻿namespace Khadamati.DAL
{
    public class BookMarkRepo : GenericRepo<BookMark>,IBookMarkRepo
    {
        KhadamatiContext Context { get; set; }
        public BookMarkRepo(KhadamatiContext context) : base(context)
        {
            Context = context;
        }
        public BookMark returnBookmark(string uid,int sid)
        {
            return Context.Set<BookMark>().Find(sid, uid);
        }
    }
}
