using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using libras_connect_domain.Repository.Interfaces;
using libras_connect_domain.Models;
using System.Data.SQLite;
using libras_connect_domain.Exceptions;
using libras_connect_domain.Enums;

namespace libras_connect_domain.Repository.Implements.SQLite
{
    /// <summary>
    /// Implementation of ISettingRepository<see cref="ISettingRepository"/>
    /// </summary>
    public class SettingRepository : BaseRepository, ISettingRepository
    {
        /// <summary>
        ///     <para><see cref="ISettingRepository.Create(Setting)"/></para>
        /// </summary>
        public void Create(Setting setting)
        {
            using (SQLiteConnection conn = new SQLiteConnection(this.strConnection))
            {
                try
                {
                    conn.Open();

                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        string query = @"INSERT INTO Setting (IP, Port, CameraId) VALUES(@ip, @port, @cameraId);";

                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("ip", setting.IP);
                        cmd.Parameters.AddWithValue("cameraId", setting.Camera);
                        cmd.Parameters.AddWithValue("port", setting.Port);

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

        /// <summary>
        ///     <para><see cref="ISettingRepository.Get"/></para>
        /// </summary>
        public ICollection<Setting> Get()
        {
            ICollection<Setting> list = new List<Setting>();

            using (SQLiteConnection conn = new SQLiteConnection(this.strConnection))
            {
                try
                {
                    conn.Open();

                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        string query = @"SELECT Id, IP, Port, CameraId FROM Setting";

                        cmd.CommandText = query;

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Setting setting = new Setting();
                                setting.Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"].ToString());
                                setting.IP = reader["IP"] == DBNull.Value ? null : reader["IP"].ToString();
                                setting.Port = reader["Port"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Port"].ToString());
                                setting.Camera = (CameraEnum) (reader["CameraId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CameraId"].ToString()));

                                list.Add(setting);
                            }
                        }

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

                return list;
            }
        }

        /// <summary>
        ///     <para><see cref="ISettingRepository.Update(Setting)"/></para>
        /// </summary>
        public void Update(Setting setting)
        {
            using (SQLiteConnection conn = new SQLiteConnection(this.strConnection))
            {
                try
                {
                    conn.Open();

                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        string query = @"UPDATE Setting SET IP = @ip, Port = @port, CameraId = @cameraId WHERE Id = @id";

                        cmd.CommandText = query;

                        cmd.Parameters.AddWithValue("ip", setting.IP);
                        cmd.Parameters.AddWithValue("port", setting.Port);
                        cmd.Parameters.AddWithValue("id", setting.Id);
                        cmd.Parameters.AddWithValue("cameraId", setting.Camera);

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

        /// <summary>
        ///     <para><see cref="ISettingRepository.Delete"/></para>
        /// </summary>
        public void Delete()
        {
            using (SQLiteConnection conn = new SQLiteConnection(this.strConnection))
            {
                try
                {
                    conn.Open();

                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        string query = "DELETE FROM Setting";

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
