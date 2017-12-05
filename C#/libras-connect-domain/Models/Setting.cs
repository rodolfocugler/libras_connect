using libras_connect_domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public CameraEnum Camera { get; set; }
    }
}
