using Microsoft.Data.SqlClient;
using OneUmbrella.DAL.Interfaces;
using OneUmbrella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OneUmbrella.DAL.Repositories
{
    public class StructuralElementRepository: RepositoryBase<int, StructuralElement>, IStructuralElementRepository
    {
        public StructuralElementRepository(SqlConnection dbConnection) : base(dbConnection) { }

        public IEnumerable<StructuralElement> getAllForOneGrid(int gridId)
        {
            List<StructuralElement> elements = new List<StructuralElement>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM StructuralElement WHERE GRID_ID = @GridId"
                , connection))
            {
                command.Parameters.AddWithValue("@GridId", gridId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StructuralElement element = new StructuralElement
                        {
                            ElementId = reader.GetInt32(reader.GetOrdinal("ELEMENT_ID")),
                            GridId = reader.GetInt32(reader.GetOrdinal("GRID_ID")),
                            RowIndex = reader.GetInt32(reader.GetOrdinal("ROW_INDEX")),
                            ColumnIndex = reader.GetInt32(reader.GetOrdinal("COLUMN_INDEX")),
                            ElementType = reader.GetByte(reader.GetOrdinal("ELEMENT_TYPE"))

                        };
                        elements.Add(element);
                    }
                }
            }
            connection.Close();
            return elements;
        }
        public override IEnumerable<StructuralElement> getAll()
        {
            List<StructuralElement> elements = new List<StructuralElement>();
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM StructuralElement"
                , connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StructuralElement element = new StructuralElement
                        {
                            ElementId = reader.GetInt32(reader.GetOrdinal("ELEMENT_ID")),
                            GridId = reader.GetInt32(reader.GetOrdinal("GRID_ID")),
                            RowIndex = reader.GetInt32(reader.GetOrdinal("ROW_INDEX")),
                            ColumnIndex = reader.GetInt32(reader.GetOrdinal("COLUMN_INDEX")),
                            ElementType = reader.GetByte(reader.GetOrdinal("ELEMENT_TYPE"))

                        };
                        elements.Add(element);
                    }
                }
            }
            connection.Close();
            return elements;
        }
        public override StructuralElement? getById(int ElementId)
        {
            StructuralElement element = null;
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM StructuralElement WHERE ELEMENT_ID = @ElementId"
                , connection))
            {
                command.Parameters.AddWithValue("@ElementId", ElementId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        element = new StructuralElement
                        {
                            ElementId = reader.GetInt32(reader.GetOrdinal("ELEMENT_ID")),
                            GridId = reader.GetInt32(reader.GetOrdinal("GRID_ID")),
                            RowIndex = reader.GetInt32(reader.GetOrdinal("ROW_INDEX")),
                            ColumnIndex = reader.GetInt32(reader.GetOrdinal("COLUMN_INDEX")),
                            ElementType = reader.GetByte(reader.GetOrdinal("ELEMENT_TYPE"))

                        };
                    }
                }
            }
            connection.Close();
            return element;
        }
        public override bool create(StructuralElement element)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "INSERT INTO StructuralElement(" +
                "[GRID_ID]," +
                "[ROW_INDEX]," +
                "[COLUMN_INDEX]," +
                "[ELEMENT_TYPE])" +
                " VALUES(" +
                "@GridId," +
                "@RowIndex," +
                "@ColumnIndex," +
                "@ElementType)"
                , connection))
            {
                command.Parameters.AddWithValue("@GridId", element.GridId);
                command.Parameters.AddWithValue("@RowIndex", element.RowIndex);
                command.Parameters.AddWithValue("@ColumnIndex", element.ColumnIndex);
                command.Parameters.AddWithValue("@ElementType", element.ElementType);
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
        public override bool update(int elementId, StructuralElement element)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "UPDATE StructuralElement SET " +
                "[GRID_ID] = @GridId," +
                "[ROW_INDEX] = @RowIndex," +
                "[COLUMN_INDEX] = @ColumnIndex," +
                "[ELEMENT_TYPE] = @ElementType " +
                "WHERE ELEMENT_ID = @ElementId"
                , connection))
            {
                command.Parameters.AddWithValue("@GridId", element.GridId);
                command.Parameters.AddWithValue("@RowIndex", element.RowIndex);
                command.Parameters.AddWithValue("@ColumnIndex", element.ColumnIndex);
                command.Parameters.AddWithValue("@ElementType", element.ElementType);
                command.Parameters.AddWithValue("@ElementId", elementId);
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
        public override bool delete(int elementId)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(
                "DELETE" +
                " FROM StructuralElement" +
                " WHERE [ELEMENT_ID] = @ElementId"
                , connection))
            {
                command.Parameters.AddWithValue("@ElementId", elementId);
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
