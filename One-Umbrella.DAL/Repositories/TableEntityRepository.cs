using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.DAL.Repositories
{
    public class TableEntityRepository : RepositoryBase<int, TableEntity>, ITableEntityRepository
    {
        public TableEntityRepository(SqlConnection dbConnection) : base(dbConnection) { }

        public IEnumerable<TableEntity> getAllForOneGrid(int gridId)
        {
            List<TableEntity> tables = new List<TableEntity>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM TableEntity WHERE GRID_ID = @GridId"
                , connection))
            {
                command.Parameters.AddWithValue("@GridId", gridId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableEntity table = new TableEntity
                        {
                            TableId = reader.GetInt32(reader.GetOrdinal("TABLE_ID")),
                            GridId = reader.GetInt32(reader.GetOrdinal("GRID_ID")),
                            RowIndex = reader.GetInt32(reader.GetOrdinal("ROW_INDEX")),
                            ColumnIndex = reader.GetInt32(reader.GetOrdinal("COLUMN_INDEX")),
                            EndRowIndex = reader.GetInt32(reader.GetOrdinal("END_ROW_INDEX")),
                            EndColumnIndex = reader.GetInt32(reader.GetOrdinal("END_COLUMN_INDEX")),
                            SeatCapability = reader.GetByte(reader.GetOrdinal("SEAT_CAPABILITY")),
                            TableType = reader.GetByte(reader.GetOrdinal("TABLE_TYPE"))

                        };
                        tables.Add(table);
                    }
                }
            }
            connection.Close();
            return tables;
        }
        public override IEnumerable<TableEntity> getAll()
        {
            List<TableEntity> tables = new List<TableEntity>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM TableEntity"
                , connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TableEntity table = new TableEntity
                        {
                            TableId = reader.GetInt32(reader.GetOrdinal("TABLE_ID")),
                            GridId = reader.GetInt32(reader.GetOrdinal("GRID_ID")),
                            RowIndex = reader.GetInt32(reader.GetOrdinal("ROW_INDEX")),
                            ColumnIndex = reader.GetInt32(reader.GetOrdinal("COLUMN_INDEX")),
                            EndRowIndex = reader.GetInt32(reader.GetOrdinal("END_ROW_INDEX")),
                            EndColumnIndex = reader.GetInt32(reader.GetOrdinal("END_COLUMN_INDEX")),
                            SeatCapability = reader.GetByte(reader.GetOrdinal("SEAT_CAPABILITY")),
                            TableType = reader.GetByte(reader.GetOrdinal("TABLE_TYPE"))

                        };
                        tables.Add(table);
                    }
                }
            }
            connection.Close();
            return tables;
        }
        public override TableEntity? getById(int tableEntityId)
        {
            TableEntity table = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM TableEntity WHERE TABLE_ID = @TableEntityId"
                , connection))
            {
                command.Parameters.AddWithValue("@TableEntityId", tableEntityId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        table = new TableEntity
                        {
                            TableId = reader.GetInt32(reader.GetOrdinal("TABLE_ID")),
                            GridId = reader.GetInt32(reader.GetOrdinal("GRID_ID")),
                            RowIndex = reader.GetInt32(reader.GetOrdinal("ROW_INDEX")),
                            ColumnIndex = reader.GetInt32(reader.GetOrdinal("COLUMN_INDEX")),
                            EndRowIndex = reader.GetInt32(reader.GetOrdinal("END_ROW_INDEX")),
                            EndColumnIndex = reader.GetInt32(reader.GetOrdinal("END_COLUMN_INDEX")),
                            SeatCapability = reader.GetByte(reader.GetOrdinal("SEAT_CAPABILITY")),
                            TableType = reader.GetByte(reader.GetOrdinal("TABLE_TYPE"))
                        };
                    }
                }
            }
            connection.Close();
            return table;
        }
        public override int create(TableEntity table)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO TableEntity(" +
                "[GRID_ID]," +
                "[ROW_INDEX]," +
                "[COLUMN_INDEX]," +
                "[END_ROW_INDEX]," +
                "[END_COLUMN_INDEX]," +
                "[SEAT_CAPABILITY]," +
                "[TABLE_TYPE])" +
                " OUTPUT INSERTED.TABLE_ID" +
                " VALUES(" +
                "@GridId," +
                "@RowIndex," +
                "@ColumnIndex," +
                "@EndRowIndex," +
                "@EndColumnIndex," +
                "@SeatCapability," +
                "@TableType)"
                , connection))
            {
                command.Parameters.AddWithValue("@GridId", table.GridId);
                command.Parameters.AddWithValue("@RowIndex", table.RowIndex);
                command.Parameters.AddWithValue("@ColumnIndex", table.ColumnIndex);
                command.Parameters.AddWithValue("@EndRowIndex", table.EndRowIndex);
                command.Parameters.AddWithValue("@EndColumnIndex", table.EndColumnIndex);
                command.Parameters.AddWithValue("@SeatCapability", table.SeatCapability);
                command.Parameters.AddWithValue("@TableType", table.TableType);
                int insertedId = (int)command.ExecuteScalar();
                connection.Close();
                return insertedId;
            }
        }
        public override bool update(int tableId,TableEntity table)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "UPDATE TableEntity SET " +
                "[GRID_ID] = @GridId," +
                "[ROW_INDEX] = @RowIndex," +
                "[COLUMN_INDEX] = @ColumnIndex," +
                "[END_ROW_INDEX] = @EndRowIndex," +
                "[END_COLUMN_INDEX] = @EndColumnIndex," +
                "[SEAT_CAPABILITY] = @SeatCapability," +
                "[TABLE_TYPE] = @TableType " +
                "WHERE TABLE_ID = @TableId"
                , connection))
            {
                command.Parameters.AddWithValue("@GridId", table.GridId);
                command.Parameters.AddWithValue("@RowIndex", table.RowIndex);
                command.Parameters.AddWithValue("@ColumnIndex", table.ColumnIndex);
                command.Parameters.AddWithValue("@EndRowIndex", table.EndRowIndex);
                command.Parameters.AddWithValue("@EndColumnIndex", table.EndColumnIndex);
                command.Parameters.AddWithValue("@SeatCapability", table.SeatCapability);
                command.Parameters.AddWithValue("@TableType", table.TableType);
                command.Parameters.AddWithValue("@TableId", tableId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    connection.Close();
                    return true;
                }
            }
            connection.Close();
            return false;
        }
        public override bool delete(int tableId)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "DELETE" +
                " FROM TableEntity" +
                " WHERE [TABLE_ID] = @TableId"
                , connection))
            {
                command.Parameters.AddWithValue("@TableId", tableId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    connection.Close();
                    return true;
                }
            }
            connection.Close();
            return false;
        }
    }
}
