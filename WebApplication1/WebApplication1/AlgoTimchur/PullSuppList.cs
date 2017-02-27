using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Database;

// Interface pertaining to extracting a subset of suppliers
// "polled" into a given tichur.
namespace WebApplication1.AlgoTimchur
{
    public interface PullSuppList
    {
        TablePullResult TichurAlgorithem(TichurInfo input);
    }
}