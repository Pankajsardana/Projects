namespace Lab_PagingSortingSearching.Models
{
    public class Pager
    {
        public Pager()
        {

        }

        public string SearchText { get; set; } = "";
        public string Controller { get; set; } = "";
        public string Action { get; set; } = "Index";

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set;}
        public int PageSize { get; private set;}
        public int TotalPages { get; private set;}

        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public int StartRecord { get; private set;}
        public int EndRecord { get; private set; }

        public Pager(int totalItems, int page,int pageSize=10) 
        {
            totalItems = (int)Math.Ceiling((Decimal)totalItems/(Decimal)pageSize);
            int currentpage = page;
            int startPage = currentpage - 5;
            int endPage = currentpage + 5;
            if(startPage<=0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if(endPage>startPage)
            {
                endPage = totalItems;
                if (endPage > 10) 
                { 
                    startPage = endPage - 9;
                }
            }
            TotalItems = totalItems; 
            CurrentPage = currentpage; 
            PageSize = pageSize; 
            TotalPages = totalItems; 
            StartPage = startPage; 
            EndPage = endPage; 
            StartRecord = (CurrentPage - 1) * PageSize + 1; 
            EndRecord = StartRecord - 1 + PageSize;

        }



    }
}
