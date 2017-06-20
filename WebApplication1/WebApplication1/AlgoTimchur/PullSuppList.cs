// Interface pertaining to extracting a subset of suppliers
// "polled" into a given tichur.
namespace WebApplication1.AlgoTimchur
{
    public interface PullSuppList
    {
        TablePullResult TichurExtract(TichurInfo input);
    }
}