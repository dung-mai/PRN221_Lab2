
namespace Bussiness
{
    public class Pagination
    {
        public int Pageindex { get; set; } = 1;
        public int Pagesize { get; set; } = 8;
        public int Totalpage { get; set; }
        public int Totalrecords { get; set; } = 0;
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public Pagination()
        {
            Calc();
        }

        public Pagination(int totalrecords)
        {
            Totalrecords = totalrecords;
            Calc();
        }

        public Pagination(int totalrecords, int? pageindex, int pagesize)
        {
            Totalrecords = totalrecords;
            Pageindex = pageindex != null ? (int) pageindex : 1;
            Pagesize = pagesize;
            Calc();
        }

        public void Calc()
        {
            Pageindex = Math.Max(Pageindex, 1);
            Totalpage = (Totalrecords % Pagesize == 0) ? (Totalrecords / Pagesize) : (Totalrecords / Pagesize) + 1;
            Pageindex = Math.Min(Pageindex, Totalpage);
            StartIndex = Math.Max((Pageindex - 1) * Pagesize, 0);
            EndIndex = Math.Min(Pageindex * Pagesize - 1, Totalrecords - 1);
        }
    }
}