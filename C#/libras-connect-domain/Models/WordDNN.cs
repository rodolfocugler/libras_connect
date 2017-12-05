using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Models
{
    public class WordDNN : BaseModel
    {
        public int Index { get; set; }
        public string Word { get; set; }

        public override string ToString()
        {
            return string.Format("Index=[{0}]; Word=[{1}];", this.Index, this.Word);
        }
    }
}
