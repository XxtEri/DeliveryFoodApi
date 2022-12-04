namespace webNET_Hits_backend_aspnet_project_2.Models;

public class PageInfoModel
{
    public int Size { get; set; }
    public int Count { get; set; }
    public int Current { get; set; }

    public PageInfoModel(int size, int count, int current)
    {
        Size = size;
        Count = count;
        Current = current;
    }
}