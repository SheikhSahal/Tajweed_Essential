using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Data.SqlClient;


namespace WebApplication1.DB
{
    public class Database
    {
        string connectString = Database_connecting.connectString;
        public Batch_header AutoGenerate_batch_id()
        {
            Batch_header bh = new Batch_header();
            SqlConnection con = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select  ISNULL(max(bh.bh_id),0)+1 bh_id from batch_header bh";
            cmd.Connection = con;
            con.Open();


            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            bh.Bh_id = Convert.ToInt16(reader["bh_id"]);

            reader.Close();
            return bh;
        }


        public void InsertBatchHeader(Batch_header bh)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into batch_header (BH_ID,BATCH_NAME,TEACHER_ID,VOLUNTEER_ID,ZOOM_ID,CREATED_BY) values (@BH_ID,@BATCH_NAME,@TEACHER_ID,@VOLUNTEER_ID,@ZOOM_ID,@CREATED_BY)", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@BH_ID", bh.Bh_id);
                    cmd.Parameters.AddWithValue("@BATCH_NAME", bh.Batch_Name);
                    cmd.Parameters.AddWithValue("@TEACHER_ID", bh.Teacher);
                    cmd.Parameters.AddWithValue("@VOLUNTEER_ID", bh.Volunteer);
                    cmd.Parameters.AddWithValue("@ZOOM_ID", bh.Zoom);
                    cmd.Parameters.AddWithValue("@CREATED_BY", "hellow");

                    cmd.ExecuteNonQuery();
                }
            }

        }


        public void InsertBatchDetails(Batch_details bd)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into batch_details(BH_id,stu_id) values (@BH_id,@stu_id)", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@BH_ID", bd.Bh_id);
                    cmd.Parameters.AddWithValue("@stu_id", bd.stu_id);

                    cmd.ExecuteNonQuery();
                }
            }

        }


        public List<Teacher> Teacher_DropDown()
        {
            List<Teacher> DBase = new List<Teacher>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from teacher", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Teacher teach = new Teacher();

                        teach.Teach_id = Convert.ToInt32(reader["Teach_id"]);
                        teach.Teach_name = reader["Teach_name"].ToString();


                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }


        public List<Student> Student_DropDown()
        {
            List<Student> DBase = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from student", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student std = new Student();

                        std.Stud_id = Convert.ToInt32(reader["Stud_id"]);
                        std.Stud_name= reader["Stud_name"].ToString();


                        DBase.Add(std);

                    }
                }
            }
            return DBase;
        }


        public List<Batch_list> Batchfetchdetail()
        {
            List<Batch_list> DBase = new List<Batch_list>();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select bh.BH_ID,bh.BATCH_NAME, Count(*)as std_count, t.Teach_name , s.Stud_name from Batch_header bh , Batch_details bd , Teacher t , Student s  where bh.BH_ID = bd.BH_ID  and t.Teach_id = bh.TEACHER_ID and ISNull(bh.delete_flag,'N') <> 'Y' and bh.VOLUNTEER_ID = s.Stud_id group by bh.BATCH_NAME , t.Teach_name,s.Stud_name,bh.BH_ID", conn))
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Batch_list emp = new Batch_list();


                        if (reader["BH_ID"] != DBNull.Value)
                        {
                            emp.Bh_id = Convert.ToInt32( reader["BH_ID"]);
                        }

                        if (reader["BATCH_NAME"] != DBNull.Value)
                        {
                            emp.Batch_name = reader["BATCH_NAME"].ToString();
                        }

                        if (reader["std_count"] != DBNull.Value)
                        {
                            emp.Std_count = Convert.ToInt32(reader["std_count"]);
                        }

                        if (reader["Teach_name"] != DBNull.Value)
                        {
                            emp.Teach_name = reader["Teach_name"].ToString();
                        }

                        if (reader["Stud_name"] != DBNull.Value)
                        {
                            emp.Stud_name = reader["Stud_name"].ToString();
                        }


                        
                  

                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }



        public void DeleteBatch(int id)
        {

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("update Batch_header set Delete_flag= 'Y' where BH_ID = @bh_id", conn))
                {


                    conn.Open();

                    cmd.Parameters.AddWithValue("@bh_id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Batch_header get_batch_Master_data(int id)
        {
            Batch_header employee = new Batch_header();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Batch_header bh where bh.BH_ID = @p_id", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@p_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    employee.Bh_id = Convert.ToInt16(reader["Bh_id"]);
                    employee.Batch_Name = reader["Batch_Name"].ToString();
                    employee.Teacher = Convert.ToInt32(reader["Teacher_id"]);
                    employee.Volunteer = Convert.ToInt32( reader["Volunteer_id"]);
                    employee.Zoom = reader["Zoom_id"].ToString();
                }
            }
            return employee;
        }



        public List<Batch_details> Get_Batch_detail_data(int id)
        {
            List<Batch_details> DBase = new List<Batch_details>();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select bd.BH_ID, s.Stud_name from Batch_details bd , Student s where bd.BH_ID = 1  and bd.STU_ID = s.Stud_id", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@p_id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Batch_details emp = new Batch_details();

                        emp.Bh_id = Convert.ToInt32(reader["Bh_id"]);
                        //emp.stu_id = Convert.ToInt32( reader["STU_ID"]);
                        emp.stu_name = reader["Stud_name"].ToString();


                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }


        public void Update_batch_Master(Batch_header bh)
        {

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("update Batch_header set batch_name = @batch_name, teacher_id =@teach_id, volunteer_id = @volunt_id, zoom_id = @zoom_id where bh_id = @bh_id", conn))
                {


                    conn.Open();

                    cmd.Parameters.AddWithValue("@bh_id", bh.Bh_id);
                    cmd.Parameters.AddWithValue("@batch_name", bh.Batch_Name);
                    cmd.Parameters.AddWithValue("@teach_id", bh.Teacher);
                    cmd.Parameters.AddWithValue("@volunt_id", bh.Volunteer);
                    cmd.Parameters.AddWithValue("@zoom_id", bh.Zoom);




                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
