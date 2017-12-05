using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_infrastructure.Config
{
    public static class ConfigValues
    {
        public const string Solution_Path = @"C:\Users\rodol\Documents\Projetos\tcc";
        public const string Dnn_Path = Solution_Path + @"\libras_connect.dnn";
        public const string Train_Txt = Solution_Path + @"\python\data\train.txt";
        public const string Test_Txt = Solution_Path + @"\python\data\test.txt";
        public const string Mongo_IP = "127.0.0.1";
        public const string Mongo_Database = "libras_connect";
        public const double Cntk_Threshold = 0.7;
    }
}
