using libras_connect_domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libras_connect_domain.Repository.Implements.SQLite
{
    public class BaseRepository
    {
        protected string strConnection = "Data Source=libras_connect.sqlite;Version=3;foreign keys=true;";

        public BaseRepository()
        {
            if (!File.Exists("libras_connect.sqlite"))
            {
                SQLiteConnection.CreateFile("libras_connect.sqlite");
                this.RunDDL();
            }
        }

        /// <summary>
        /// Close connection resource
        /// </summary>
        /// <param name="conn">SQLiteConnection object</param>
        public void CloseResources(SQLiteConnection conn)
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            conn = null;
        }

        /// <summary>
        /// Run Create tables
        /// </summary>
        private void RunDDL()
        {
            using (SQLiteConnection conn = new SQLiteConnection(this.strConnection))
            {
                try
                {
                    conn.Open();

                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        string query = @"
                                CREATE TABLE Setting 
                                (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    IP TEXT,
                                    Port INTEGER,
                                    CameraId INTEGER
                                );

                                CREATE TABLE HandDataDefault
                                (
                                    CameraId PRIMARY KEY,
                                    Text TEXT
                                );";

                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (DbException ex)
                {
                    throw new RepositoryException(ex.Message, ex);
                }
                finally
                {
                    this.CloseResources(conn);
                }
            }
        }
    }
}
