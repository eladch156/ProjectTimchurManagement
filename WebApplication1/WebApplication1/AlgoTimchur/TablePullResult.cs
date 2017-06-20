using System.Collections.Generic;


/***
 * This class is returned as a result of a data-extraction algorithm,
 * with each item on the list pertaining to the ID/Key value of the element selected.
 */
namespace WebApplication1.AlgoTimchur
{
    public class TablePullResult
    {
        public List<string> table = new List<string>(); 
    }
}