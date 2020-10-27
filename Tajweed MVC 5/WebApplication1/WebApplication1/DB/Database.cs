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

        //Start Menu
        public List<AP_Menu> user_rights(int user_id)
        {
            List<AP_Menu> DBase = new List<AP_Menu>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select m.Menu_id ,m.Menu_name , m.Menu_parent_id ,m.Menu_URL from Login l , role r , user_privilege u , Menu m where l.Role_id = r.Role_id and r.Role_id = u.role_id and u.menu_id = m.Menu_id and l.User_id = @user_id", conn))
                {
                    conn.Open();
                    SqlParameter user = cmd.Parameters.AddWithValue("@user_id", user_id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AP_Menu emp = new AP_Menu();
                        emp.Menu_id = Convert.ToInt16(reader["Menu_id"]);
                        emp.Menu_name = reader["Menu_name"].ToString();
                        emp.Menu_URL = reader["Menu_URL"].ToString();
                        emp.Menu_parent_id = reader["Menu_parent_id"] != DBNull.Value ? Convert.ToInt16(reader["Menu_Parent_id"]) : (int?)null;
                        DBase.Add(emp);
                    }
                }
            }
            return DBase;
        }

        //end Menu

        //Courses Functions Start
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
                using (SqlCommand cmd = new SqlCommand("insert into batch_header (BH_ID,BATCH_NAME,TEACHER_ID,VOLUNTEER_ID,ZOOM_ID,CREATED_BY,bh_end_date) values (@BH_ID,@BATCH_NAME,@TEACHER_ID,@VOLUNTEER_ID,@ZOOM_ID,@CREATED_BY,@bh_end_date)", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@BH_ID", bh.Bh_id);
                    cmd.Parameters.AddWithValue("@BATCH_NAME", bh.Batch_Name);
                    cmd.Parameters.AddWithValue("@TEACHER_ID", bh.Teacher);
                    cmd.Parameters.AddWithValue("@VOLUNTEER_ID", bh.Volunteer);
                    cmd.Parameters.AddWithValue("@ZOOM_ID", bh.Zoom);
                    cmd.Parameters.AddWithValue("@CREATED_BY", "hellow");
                    cmd.Parameters.AddWithValue("@bh_end_date", bh.course_end_date);


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
                using (SqlCommand cmd = new SqlCommand("select l.User_id,l.User_name from Login l where ISNULL(l.User_status,'W') = 'A'", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student std = new Student();

                        std.Stud_id = Convert.ToInt32(reader["User_id"]);
                        std.Stud_name = reader["User_name"].ToString();


                        DBase.Add(std);

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
                using (SqlCommand cmd = new SqlCommand("select * from Batch_header bh where bh.BH_ID = @p_id and ISNULL(Delete_flag,'N') <> 'Y'", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@p_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    employee.Bh_id = Convert.ToInt16(reader["Bh_id"]);
                    employee.Batch_Name = reader["Batch_Name"].ToString();
                    employee.Teacher = Convert.ToInt32(reader["Teacher_id"]);
                    employee.Volunteer = Convert.ToInt32(reader["Volunteer_id"]);
                    employee.Zoom = reader["Zoom_id"].ToString();
                    if (reader["bh_end_date"] != DBNull.Value)
                    {
                        employee.course_end_date = Convert.ToDateTime(reader["bh_end_date"]);
                    }

                }
            }
            return employee;
        }

        public List<Batch_details> Get_Batch_detail_data(int id)
        {
            List<Batch_details> DBase = new List<Batch_details>();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select bd.BH_ID, bd.STU_ID,  s.User_name from Batch_details bd , login s  where bd.BH_ID = @p_id   and bd.STU_ID = s.User_id", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@p_id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Batch_details emp = new Batch_details();

                        emp.Bh_id = Convert.ToInt32(reader["Bh_id"]);
                        emp.stu_id = Convert.ToInt32(reader["STU_ID"]);
                        emp.stu_name = reader["User_name"].ToString();


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
                using (SqlCommand cmd = new SqlCommand("update Batch_header set batch_name = @batch_name, teacher_id =@teach_id, volunteer_id = @volunt_id, zoom_id = @zoom_id,bh_end_date = @bh_end_date where bh_id = @bh_id", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@bh_id", bh.Bh_id);
                    cmd.Parameters.AddWithValue("@batch_name", bh.Batch_Name);
                    cmd.Parameters.AddWithValue("@teach_id", bh.Teacher);
                    cmd.Parameters.AddWithValue("@volunt_id", bh.Volunteer);
                    cmd.Parameters.AddWithValue("@zoom_id", bh.Zoom);
                    cmd.Parameters.AddWithValue("@bh_end_date", bh.course_end_date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void dtlDelete(int id, int bh_id)
        {

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("delete from Batch_details where BH_ID = @bh_id and STU_ID = @stu_id", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@bh_id", bh_id);
                    cmd.Parameters.AddWithValue("@stu_id", id);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        //Courses Function End

        //Helpers Function start
        public Helper_mst AutoGenerate_Helper_id()
        {
            Helper_mst hm = new Helper_mst();
            SqlConnection con = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select  ISNULL(max(hh.hlp_id),0)+1 hh_id from hlp_header hh";
            cmd.Connection = con;
            con.Open();


            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            hm.Hpl_id = Convert.ToInt16(reader["hh_id"]);

            reader.Close();
            return hm;
        }

        public List<Batch_header> Course_DropDown()
        {
            List<Batch_header> DBase = new List<Batch_header>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select bh.BH_ID,bh.BATCH_NAME from Batch_header bh where ISNULL(bh.Delete_flag,'N') <> 'Y'", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Batch_header std = new Batch_header();

                        std.Bh_id = Convert.ToInt32(reader["BH_ID"]);
                        std.Batch_Name = reader["BATCH_NAME"].ToString();


                        DBase.Add(std);

                    }
                }
            }
            return DBase;
        }

        public void InsertHelperHeader(Helper_mst hm)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into hlp_header(hlp_id,bh_id,stu_id,created_by) values (@hlp_id,@bh_id,@stu_id,@created_by)", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@hlp_id", hm.Hpl_id);
                    cmd.Parameters.AddWithValue("@bh_id", hm.bh_id);
                    cmd.Parameters.AddWithValue("@stu_id", hm.stu_id);
                    cmd.Parameters.AddWithValue("@created_by", "hellow");

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void InsertHelperDetails(Helper_dtl hd)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into hlp_details values (@hpl_id,@stu_id)", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@hpl_id", hd.Hpl_id);
                    cmd.Parameters.AddWithValue("@stu_id", hd.stud_id);


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Helper_list> Helperfetchdetail()
        {
            List<Helper_list> DBase = new List<Helper_list>();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select hh.hlp_id, hh.bh_id, bh.BATCH_NAME,count(*) stud_enroll , s.User_name helper , bh.bh_end_date  from Hlp_header hh, Hlp_details hd , Batch_header bh , Login s    where hh.hlp_id = hd.Hpl_id   and bh.bh_id = hh.bh_id   and s.User_id = hh.stu_id  and ISNULL(hh.Delete_flag,'N') <> 'Y' group by hh.hlp_id, hh.bh_id, bh.BATCH_NAME,s.User_name, bh.bh_end_date", conn))
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Helper_list emp = new Helper_list();

                        if (reader["hlp_id"] != DBNull.Value)
                        {
                            emp.hlp_id = Convert.ToInt32(reader["hlp_id"]);
                        }

                        if (reader["bh_id"] != DBNull.Value)
                        {
                            emp.bh_id = Convert.ToInt32(reader["bh_id"]);
                        }

                        if (reader["BATCH_NAME"] != DBNull.Value)
                        {
                            emp.batch_name = reader["BATCH_NAME"].ToString();
                        }

                        if (reader["helper"] != DBNull.Value)
                        {
                            emp.Helper = reader["helper"].ToString();
                        }

                        if (reader["stud_enroll"] != DBNull.Value)
                        {
                            emp.stud_enroll = Convert.ToInt32(reader["stud_enroll"]);
                        }
                        if (reader["bh_end_date"] != DBNull.Value)
                        {
                            emp.bh_end_date = Convert.ToDateTime(reader["bh_end_date"]);
                        }
                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }

        public void Delete_Helper(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("update hlp_header set Delete_flag= 'Y' where hlp_id = @hp_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@hp_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Helper_mst get_helper_Master_data(int id)
        {
            Helper_mst employee = new Helper_mst();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Hlp_header where hlp_id = @h_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@h_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    employee.Hpl_id = Convert.ToInt16(reader["hlp_id"]);
                    employee.bh_id = Convert.ToInt16(reader["bh_id"]);
                    employee.stu_id = Convert.ToInt16(reader["stu_id"]);
                }
            }
            return employee;
        }

        public List<Helper_dtl> Get_helper_detail_data(int id)
        {
            List<Helper_dtl> DBase = new List<Helper_dtl>();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select hd.Hpl_id, hd.stud_id, s.User_name Stud_name from Hlp_details hd, login s  where hd.stud_id = s.User_id and Hpl_id = @hlp_id", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@hlp_id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Helper_dtl emp = new Helper_dtl();

                        emp.Hpl_id = Convert.ToInt32(reader["Hpl_id"]);
                        emp.stud_id = Convert.ToInt32(reader["stud_id"]);
                        emp.stud_name = reader["Stud_name"].ToString();
                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }

        public void Update_Helper_master(Helper_mst m)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("update Hlp_header set bh_id = @bh_id, stu_id = @stu_id where hlp_id = @hlp_id", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@hlp_id", m.Hpl_id);
                    cmd.Parameters.AddWithValue("@bh_id", m.bh_id);
                    cmd.Parameters.AddWithValue("@stu_id", m.stu_id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void hlpDelete(int id, int hm_id)
        {

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("delete from Hlp_details where hpl_id = @hpl_id and stud_id = @stud_id", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@hpl_id", hm_id);
                    cmd.Parameters.AddWithValue("@stud_id", id);

                    cmd.ExecuteNonQuery();

                }
            }
        }
        //Helpers Function end

        //Attendance Functions start
        public Attendance_mst AutoGenerate_attendance()
        {
            Attendance_mst at = new Attendance_mst();
            SqlConnection con = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select  ISNULL(max(a.Att_id),0)+1 at_id from Attendance a";
            cmd.Connection = con;
            con.Open();


            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            at.att_id = Convert.ToInt16(reader["at_id"]);

            reader.Close();
            return at;
        }

        public void InsertAttHeader(Attendance_mst at)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into Attendance(Att_id, bh_id, att_pass,created_by)  values(@Att_id, @bh_id, @att_pass,@created_by)", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@Att_id", at.att_id);
                    cmd.Parameters.AddWithValue("@bh_id", at.bh_id);
                    cmd.Parameters.AddWithValue("@att_pass", at.att_pass);
                    cmd.Parameters.AddWithValue("@created_by", "hellow");

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public List<Batch_header> Attfetchdetail()
        {
            List<Batch_header> DBase = new List<Batch_header>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select a.Att_id, a.bh_id,bh.BATCH_NAME,a.created_date,bh.bh_end_date from Attendance a  , Batch_header bh where a.bh_id = bh.BH_ID and ISNULL(a.Delete_flag,'N') <> 'Y'", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Batch_header emp = new Batch_header();
                        if (reader["Att_id"] != DBNull.Value)
                        {
                            emp.att_id = Convert.ToInt32(reader["Att_id"]);
                        }
                        if (reader["bh_id"] != DBNull.Value)
                        {
                            emp.Bh_id = Convert.ToInt32(reader["bh_id"]);
                        }
                        if (reader["BATCH_NAME"] != DBNull.Value)
                        {
                            emp.Batch_Name = Convert.ToString(reader["BATCH_NAME"]);
                        }
                        if (reader["created_date"] != DBNull.Value)
                        {
                            emp.Att_date = Convert.ToDateTime(reader["created_date"]);
                        }
                        if (reader["bh_end_date"] != DBNull.Value)
                        {
                            emp.course_end_date = Convert.ToDateTime(reader["bh_end_date"]);
                        }
                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }

        public void Deleteatt(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("update Attendance set Delete_flag= 'Y' where Att_id = @att_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@att_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Attendance_mst get_Duplicate_data(int id, DateTime date)
        {
            Attendance_mst employee = new Attendance_mst();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) att from Attendance a where a.bh_id = @bh_id and a.created_date = @created_date and ISNULL(a.Delete_flag,'N') <> 'Y'", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    cmd.Parameters.AddWithValue("@created_date", date);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    employee.att_id = Convert.ToInt16(reader["att"]);
                }
            }
            return employee;
        }

        public Helper_mst get_helper_Course_end_date(int id)
        {
            Helper_mst employee = new Helper_mst();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select h.bh_id,bh.bh_end_date from Hlp_header h, Batch_header bh where h.bh_id = bh.BH_ID and h.hlp_id = @h_id and ISNULL(h.Delete_flag,'N') <> 'Y'", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@h_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    employee.bh_id = Convert.ToInt32(reader["bh_id"]);
                    employee.created_date = Convert.ToDateTime(reader["bh_end_date"]);
                }
            }
            return employee;
        }
        public Helper_mst get_course_end_date(int id)
        {
            Helper_mst employee = new Helper_mst();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select bh.BH_ID, bh.bh_end_date from Batch_header bh where bh.BH_ID = @bh_id and ISNULL(bh.Delete_flag,'N') <> 'Y'", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    employee.bh_id = Convert.ToInt32(reader["BH_ID"]);
                    employee.created_date = Convert.ToDateTime(reader["bh_end_date"]);
                }
            }
            return employee;
        }
        //Attendance Functions end


        public void Registration(Registor r)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into login (User_name,	User_email,	User_contact,	DOB,	Martial_status,	F_H_name,	ID_Card,	Address,	Country,	Qualification,	Profession,	Q_A,	Future_Plan,	recommended, bh_id,User_flag) values (@User_name,	@User_email,	@User_contact,	@DOB,	@Martial_status,	@F_H_name,	@ID_Card,	@Address,	@Country,	@Qualification,	@Profession,	@Q_A,	@Future_Plan,	@recommended,@bh_id,@User_flag)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@User_name", r.Full_Name);
                    cmd.Parameters.AddWithValue("@User_email", r.email);
                    cmd.Parameters.AddWithValue("@User_contact", r.M_W_no);
                    cmd.Parameters.AddWithValue("@DOB", r.DOB);
                    cmd.Parameters.AddWithValue("@Martial_status", r.Marital_Status);
                    cmd.Parameters.AddWithValue("@F_H_name", r.FH_name);
                    cmd.Parameters.AddWithValue("@ID_Card", r.IDCardNo);
                    cmd.Parameters.AddWithValue("@Address", r.Address);
                    cmd.Parameters.AddWithValue("@Country", r.Country);
                    cmd.Parameters.AddWithValue("@Qualification", r.Qualification);
                    cmd.Parameters.AddWithValue("@Profession", r.Profession);
                    cmd.Parameters.AddWithValue("@Q_A", r.Q_A);
                    cmd.Parameters.AddWithValue("@Future_Plan", r.Future_plan);
                    cmd.Parameters.AddWithValue("@recommended", r.recommended);
                    cmd.Parameters.AddWithValue("@bh_id", r.bh_id);
                    cmd.Parameters.AddWithValue("@User_flag", "S");


                    cmd.ExecuteNonQuery();
                }
            }

        }

        public Registor get_email(string email)
        {
            Registor employee = new Registor();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) email from login l where lower(l.User_email) = @email", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader["email"] != DBNull.Value)
                    {
                        employee.email = Convert.ToString(reader["email"]);
                    }

                }
            }
            return employee;
        }

        public List<Registor> Userfetchdetail()
        {
            List<Registor> DBase = new List<Registor>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select l.user_id,l.user_name, l.User_email,l.User_contact,l.ID_Card, l.recommended, case when ISNULL(l.User_status,'W') = 'W' then 'Waiting' when ISNULL(l.User_status,'W') = 'A' then 'Approved'  end as user_status from login l", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Registor emp = new Registor();
                        if (reader["user_id"] != DBNull.Value)
                        {
                            emp.User_id = Convert.ToInt32(reader["user_id"]);
                        }
                        if (reader["user_name"] != DBNull.Value)
                        {
                            emp.Full_Name = Convert.ToString(reader["user_name"]);
                        }
                        if (reader["User_email"] != DBNull.Value)
                        {
                            emp.email = Convert.ToString(reader["User_email"]);
                        }
                        if (reader["User_contact"] != DBNull.Value)
                        {
                            emp.User_contact = Convert.ToInt64(reader["User_contact"]);
                        }
                        if (reader["ID_Card"] != DBNull.Value)
                        {
                            emp.IDCardNo = Convert.ToString(reader["ID_Card"]);
                        }

                        if (reader["User_status"] != DBNull.Value)
                        {
                            emp.User_status = Convert.ToString(reader["User_status"]);
                        }


                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }

        public void Approveduser(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("update login  set User_status = 'A' where User_id = @bh_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void DeleteUser(int id, int bh_id)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("delete from login where Bh_id = @bh_id and User_id = @user_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", bh_id);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Registor user_login(string email, string password)
        {
            Registor employee = new Registor();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select l.User_id,l.User_name,l.User_email,l.Password,l.role_id from Login l where l.User_email = @user_email and l.Password = @user_password and l.user_flag = 'U'", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@user_email", email);
                    cmd.Parameters.AddWithValue("@user_password", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader["User_id"] != DBNull.Value)
                        {
                            employee.User_id = Convert.ToInt32(reader["User_id"]);
                        }
                        if (reader["User_name"] != DBNull.Value)
                        {
                            employee.Full_Name = Convert.ToString(reader["User_name"]);
                        }
                        if (reader["User_email"] != DBNull.Value)
                        {
                            employee.email = Convert.ToString(reader["User_email"]);
                        }

                        if (reader["Password"] != DBNull.Value)
                        {
                            employee.pass = Convert.ToString(reader["Password"]);
                        }
                        if (reader["role_id"] != DBNull.Value)
                        {
                            employee.Role_id = Convert.ToInt32(reader["role_id"]);
                        }
                    }


                }
            }
            return employee;
        }

        public void Logoutupdate(int id, string user_active)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("update login set User_Active = @User_active where User_id = @user_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@user_id", id);
                    cmd.Parameters.AddWithValue("@User_active", user_active);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Attendance_data> Attendancefetchdetail(int id)
        {
            List<Attendance_data> DBase = new List<Attendance_data>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select a.Att_id,a.att_pass ,bh.BH_ID, bh.BATCH_NAME, t.Teach_name, bh.bh_end_date, a.created_date att_date from Attendance a , Batch_header bh , Batch_details bd, Teacher t  where a.bh_id = bh.BH_ID and  bh.BH_ID = bd.BH_ID  and bd.STU_ID = @STU_ID  and bh.TEACHER_ID = t.Teach_id  and ISNULL(bh.Delete_flag,'N') <> 'Y' and ISNULL(a.Delete_flag,'N') <> 'Y'", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@STU_ID", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Attendance_data emp = new Attendance_data();

                        if (reader["Att_id"] != DBNull.Value)
                        {
                            emp.att_id = Convert.ToInt32(reader["Att_id"]);
                        }
                        if (reader["Att_pass"] != DBNull.Value)
                        {
                            emp.Att_pass = Convert.ToString(reader["Att_pass"]);
                        }

                        if (reader["BH_ID"] != DBNull.Value)
                        {
                            emp.BH_id = Convert.ToInt32(reader["BH_ID"]);
                        }
                        if (reader["BATCH_NAME"] != DBNull.Value)
                        {
                            emp.Batch_name = Convert.ToString(reader["BATCH_NAME"]);
                        }
                        if (reader["Teach_name"] != DBNull.Value)
                        {
                            emp.Teach_name = Convert.ToString(reader["Teach_name"]);
                        }
                        if (reader["bh_end_date"] != DBNull.Value)
                        {
                            emp.Bh_end_date = Convert.ToDateTime(reader["bh_end_date"]);
                        }
                        if (reader["att_date"] != DBNull.Value)
                        {
                            emp.att_created = Convert.ToDateTime(reader["att_date"]);
                        }

                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }

        public Attendance_data get_att_pass(int att_id, string pass)
        {
            Attendance_data employee = new Attendance_data();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) att from Attendance a where a.Att_id = @att_id and upper(a.att_pass) = upper(@pass) and ISNULL(a.Delete_flag,'N') <> 'Y'", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@att_id", att_id);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    employee.att_id = Convert.ToInt16(reader["att"]);
                }
            }
            return employee;
        }

        public void Insertattdetails(int att_id, int stud_id)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into Attendance_details values(@att_id,@stud_id,@att_status)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@att_id", att_id);
                    cmd.Parameters.AddWithValue("@stud_id", stud_id);
                    cmd.Parameters.AddWithValue("@att_status", "P");
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public Attendance_data stop_duplicate_att(int id, int stud_id)
        {
            Attendance_data employee = new Attendance_data();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) att_taken from Attendance ah, Attendance_details ad   where ah.Att_id = ad.att_id and ah.Att_id =@att_id  and ad.stud_id = @stud_id and ah.created_date = CONVERT(VARCHAR(10), GETDATE(), 120)  and ISNULL(ah.Delete_flag,'N') <> 'Y'", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@att_id", id);
                    cmd.Parameters.AddWithValue("@stud_id", stud_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    employee.att_id = Convert.ToInt16(reader["att_taken"]);
                }
            }
            return employee;
        }



        public List<Att_Report> report_att_abcent(int id)
        {
            List<Att_Report> DBase = new List<Att_Report>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select bd.STU_ID, l.User_name,'A' att_status  from Batch_details bd , Batch_header bh, login l  where bh.BH_ID = bd.BH_ID  and l.User_id = bd.STU_ID  and bh.BH_ID = @bh_id and bd.STU_ID not in (select ad.stud_id from Attendance a ,Attendance_details ad where a.Att_id = ad.att_id and a.bh_id = @bh_id and ISNULL(a.Delete_flag,'N') <> 'Y')", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Att_Report emp = new Att_Report();

                        if (reader["STU_ID"] != DBNull.Value)
                        {
                            emp.stud_id = Convert.ToInt32(reader["STU_ID"]);
                        }
                        if (reader["User_name"] != DBNull.Value)
                        {
                            emp.Stud_name = Convert.ToString(reader["User_name"]);
                        }

                        if (reader["att_status"] != DBNull.Value)
                        {
                            emp.att_status = Convert.ToString(reader["att_status"]);
                        }
                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }

        public Attendance_data Get_report_header(int id)
        {
            Attendance_data employee = new Attendance_data();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select bh.BATCH_NAME,bh.bh_end_date from Batch_header bh where bh.BH_ID =@bh_id and ISNULL(bh.Delete_flag,'N') <> 'Y'", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader["BATCH_NAME"] != DBNull.Value)
                    {
                        employee.Batch_name = Convert.ToString(reader["BATCH_NAME"]);
                    }
                    if (reader["bh_end_date"] != DBNull.Value)
                    {
                        employee.Bh_end_date = Convert.ToDateTime(reader["bh_end_date"]);
                    }
                }
            }
            return employee;
        }
        public Attendance_data Get_all_users()
        {
            Attendance_data employee = new Attendance_data();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) Students from Login l", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader["Students"] != DBNull.Value)
                    {
                        employee.att_id = Convert.ToInt32(reader["Students"]);
                    }
                }
            }
            return employee;
        }
        public Attendance_data Get_Valid_Course()
        {
            Attendance_data employee = new Attendance_data();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) valid_course  from Batch_header b where ISNULL(b.course_complete,'N') ='Y' ", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader["valid_course"] != DBNull.Value)
                    {
                        employee.att_id = Convert.ToInt32(reader["valid_course"]);
                    }
                }
            }
            return employee;
        }
        public Attendance_data Get_Expired_Course()
        {
            Attendance_data employee = new Attendance_data();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) Expired_course from Batch_header b where ISNULL(b.course_complete, 'N') = 'N'", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader["Expired_course"] != DBNull.Value)
                    {
                        employee.att_id = Convert.ToInt32(reader["Expired_course"]);
                    }
                }
            }
            return employee;
        }
        public Attendance_data Get_all_attendance()
        {
            Attendance_data employee = new Attendance_data();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) Attendance from Batch_header", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader["Attendance"] != DBNull.Value)
                    {
                        employee.att_id = Convert.ToInt32(reader["Attendance"]);
                    }
                }
            }
            return employee;
        }

        //Requirement New
        public void InsertCourse(New_Course nc)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into Batch_header(BATCH_NAME,	Teacher_1,	CREATED_BY,	CREATED_DATE,	Delete_flag,	bh_end_date,	bh_start_date,	Teacher_2,	Teacher_3,	Teacher_4,	Teacher_5,	Course_desc) values (@BATCH_NAME,	@Teacher_1,	@CREATED_BY,	@CREATED_DATE,	@Delete_flag,	@bh_end_date,	@bh_start_date,	@Teacher_2,	@Teacher_3,	@Teacher_4,	@Teacher_5,	@Course_desc)", conn))
                {

                    conn.Open();

                    cmd.Parameters.AddWithValue("@BATCH_NAME", nc.BATCH_NAME);
                    cmd.Parameters.AddWithValue("@Teacher_1", nc.Teacher_1);
                    cmd.Parameters.AddWithValue("@CREATED_BY", "hellow");
                    cmd.Parameters.AddWithValue("@CREATED_DATE", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@Delete_flag", "N");
                    cmd.Parameters.AddWithValue("@bh_end_date", nc.bh_end_date.Date);
                    cmd.Parameters.AddWithValue("@bh_start_date", nc.bh_start_date.Date);
                    cmd.Parameters.AddWithValue("@Teacher_2", nc.Teacher_2);
                    cmd.Parameters.AddWithValue("@Teacher_3", nc.Teacher_3);
                    cmd.Parameters.AddWithValue("@Teacher_4", nc.Teacher_4);
                    cmd.Parameters.AddWithValue("@Teacher_5", nc.Teacher_5);
                    cmd.Parameters.AddWithValue("@Course_desc", nc.Course_desc);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public List<New_Course> Courses_Cart()
        {
            List<New_Course> DBase = new List<New_Course>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select bh.BH_ID,Upper( bh.BATCH_NAME)BATCH_NAME,bh.course_desc,t.Teach_name, bh.bh_start_date,bh.bh_end_date from batch_header bh, Teacher t where bh.TEACHER_1 = t.Teach_id and bh.Course_visible <> 'Y' and ISNULL(bh.Delete_flag,'N') <> 'Y' order by bh.bh_id desc", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        New_Course teach = new New_Course();

                        teach.bh_id = Convert.ToInt32(reader["BH_ID"]);
                        teach.BATCH_NAME = reader["BATCH_NAME"].ToString();
                        teach.Course_desc = reader["course_desc"].ToString();
                        teach.Teach_name = reader["Teach_name"].ToString();
                        teach.bh_start_date = Convert.ToDateTime(reader["bh_start_date"]);
                        teach.bh_end_date = Convert.ToDateTime(reader["bh_end_date"]);


                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }
        public List<Student> Get_student_list()
        {
            List<Student> DBase = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select l.Bh_id, l.User_id, CONCAT(l.User_name,' ',bh.BATCH_NAME)  Full_name ,  bh.bh_end_date from login l , Batch_header bh    where l.Bh_id = bh.BH_ID   and ISNULL(bh.Course_complete,'N') = 'Y' and l.User_status = 'A'   and l.User_flag = 'S'", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student teach = new Student();
                        if (reader["User_id"] != DBNull.Value)
                        {
                            teach.Stud_id = Convert.ToInt32(reader["User_id"]);
                        }
                        if (reader["Full_name"] != DBNull.Value)
                        {
                            teach.Stud_name = reader["Full_name"].ToString();
                        }
                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }

        public List_Header AutoGenerate_List_Header()
        {
            List_Header bh = new List_Header();
            SqlConnection con = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select ISNULL(MAX(l.list_id),0)+1 list_id from list_header l";
            cmd.Connection = con;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            bh.List_id = Convert.ToInt16(reader["list_id"]);

            reader.Close();
            return bh;
        }

        public void InsertList_header(List_Header lh)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into List_header (List_id,List_name) values (@List_id,@List_name)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@List_id", lh.List_id);
                    cmd.Parameters.AddWithValue("@List_name", lh.List_name);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void InsertList_Detail(List_Details ld)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into List_details(List_id,Stud_id) values (@List_id,@Stud_id)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@List_id", ld.List_id);
                    cmd.Parameters.AddWithValue("@Stud_id", ld.Stud_id);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public List<Batch_header> get_Course_dropdown()
        {
            List<Batch_header> DBase = new List<Batch_header>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select bh.BH_ID,bh.BATCH_NAME from Batch_header bh  where ISNULL(bh.Course_complete,'N') = 'N'", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Batch_header teach = new Batch_header();
                        if (reader["BH_ID"] != DBNull.Value)
                        {
                            teach.Bh_id = Convert.ToInt32(reader["BH_ID"]);
                        }
                        if (reader["BATCH_NAME"] != DBNull.Value)
                        {
                            teach.Batch_Name = reader["BATCH_NAME"].ToString();
                        }
                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }

        public List<List_Header> get_list_dropdown()
        {
            List<List_Header> DBase = new List<List_Header>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select lh.List_id, lh.List_name from List_header lh order by lh.id desc", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        List_Header teach = new List_Header();
                        if (reader["List_id"] != DBNull.Value)
                        {
                            teach.List_id = Convert.ToInt32(reader["List_id"]);
                        }
                        if (reader["List_name"] != DBNull.Value)
                        {
                            teach.List_name = reader["List_name"].ToString();
                        }
                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }

        public List<Student> get_Cascade_student(int id)
        {
            List<Student> DBase = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select l.User_id, CONCAT(l.User_name,' ',l.F_H_name) Full_name from Login l, Batch_header bh where l.Bh_id = bh.BH_ID and bh.BH_ID =@bh_id and l.User_status = 'A' and l.User_flag = 'S'", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student teach = new Student();
                        if (reader["User_id"] != DBNull.Value)
                        {
                            teach.Stud_id = Convert.ToInt32(reader["User_id"]);
                        }
                        if (reader["Full_name"] != DBNull.Value)
                        {
                            teach.Stud_name = reader["Full_name"].ToString();
                        }
                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }

        public List<Student> get_Cascade_list_student(int id)
        {
            List<Student> DBase = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select ld.Stud_id, CONCAT( l.User_name, ' ',l.F_H_name) stud_name from List_header lh,List_details ld, login l where lh.List_id = ld.List_id and ld.Stud_id = l.User_id and lh.List_id =@bh_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student teach = new Student();
                        if (reader["Stud_id"] != DBNull.Value)
                        {
                            teach.Stud_id = Convert.ToInt32(reader["Stud_id"]);
                        }
                        if (reader["stud_name"] != DBNull.Value)
                        {
                            teach.Stud_name = reader["stud_name"].ToString();
                        }
                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }


        public Helper_mst AutoGenerate_Helperid()
        {
            Helper_mst bh = new Helper_mst();
            SqlConnection con = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select ISNULL(max(hh.hlp_id),0)+1 help_id from Hlp_header hh";
            cmd.Connection = con;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            bh.Hpl_id = Convert.ToInt16(reader["help_id"]);

            reader.Close();
            return bh;
        }

        public void InsertHelpermst(Helper_mst ms)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into Hlp_header (hlp_id , bh_id,List_id,Helper_name) values (@hlp_id , @bh_id,@List_id,@Helper_name)", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@hlp_id", ms.Hpl_id);
                    cmd.Parameters.AddWithValue("@bh_id", ms.bh_id);
                    cmd.Parameters.AddWithValue("@List_id", ms.list_id);
                    cmd.Parameters.AddWithValue("@Helper_name", ms.Helper_name);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertHelperdtl(Helper_dtl md)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into Hlp_details (hpl_id , stud_id,hlp_stud_id) values (@hpl_id , @stud_id,@hlp_stud_id)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@hpl_id", md.Hpl_id);
                    cmd.Parameters.AddWithValue("@stud_id", md.stud_id);
                    cmd.Parameters.AddWithValue("@hlp_stud_id", md.hlp_stud_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Student> get_att_students(int id)
        {
            List<Student> DBase = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select l.User_id, CONCAT(l.User_name, ' ', l.recommended) User_name from Login l , Batch_header bh where l.Bh_id = bh.BH_ID  and ISNULL(bh.course_complete,'N') = 'N' and l.User_status = 'A' and bh.BH_ID = @bh_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student teach = new Student();
                        if (reader["User_id"] != DBNull.Value)
                        {
                            teach.Stud_id = Convert.ToInt32(reader["User_id"]);
                        }
                        if (reader["User_name"] != DBNull.Value)
                        {
                            teach.Stud_name = reader["User_name"].ToString();
                        }
                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }

        public Attendance_mst AutoGenerate_attendance_id()
        {
            Attendance_mst bh = new Attendance_mst();
            SqlConnection con = new SqlConnection(connectString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select ISNULL(max(a.Att_id),0)+1 att_id from Attendance a";
            cmd.Connection = con;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            bh.att_id = Convert.ToInt16(reader["att_id"]);

            reader.Close();
            return bh;
        }

        public void Insert_attendance(Attendance_data nc)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into Attendance(Att_id, bh_id, created_date) values(@Att_id, @bh_id, @created_date)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Att_id", nc.att_id);
                    cmd.Parameters.AddWithValue("@bh_id", nc.BH_id);
                    cmd.Parameters.AddWithValue("@created_date", DateTime.Now.Date);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Insert_attendance_details(Attendance_dtl nc)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into Attendance_details (Att_id,Stud_id,Att_status) values(@Att_id,@Stud_id,@Att_status)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Att_id", nc.att_id);
                    cmd.Parameters.AddWithValue("@Stud_id", nc.stud_id);
                    cmd.Parameters.AddWithValue("@Att_status", nc.Att_status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Attendance_data attendance_valid(int id)
        {
            Attendance_data employee = new Attendance_data();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) att_valid from Attendance a where a.bh_id = @bh_id and a.created_date = Convert(varchar(10), GETDATE(),120)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    if (reader["att_valid"] != DBNull.Value)
                    {
                        employee.att_id = Convert.ToInt32(reader["att_valid"]);
                    }
                }
            }
            return employee;
        }

        public List<Att_Report> report_att_present(int id, DateTime fromdate, DateTime todate)
        {
            List<Att_Report> DBase = new List<Att_Report>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select ad.Stud_id,a.created_date,l.User_name, ad.Att_status  from Attendance a , Attendance_details ad, login l where a.created_date between @Fromdate and @todate and a.Att_id = ad.Att_id and a.bh_id = @bh_id and l.User_id = ad.Stud_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Fromdate", fromdate.Date);
                    cmd.Parameters.AddWithValue("@todate", todate.Date);
                    cmd.Parameters.AddWithValue("@bh_id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Att_Report emp = new Att_Report();

                        if (reader["created_date"] != DBNull.Value)
                        {
                            emp.created_date = Convert.ToDateTime(reader["created_date"]);
                        }
                        if (reader["Stud_id"] != DBNull.Value)
                        {
                            emp.stud_id = Convert.ToInt32(reader["Stud_id"]);
                        }
                        if (reader["User_name"] != DBNull.Value)
                        {
                            emp.Stud_name = Convert.ToString(reader["User_name"]);
                        }

                        if (reader["Att_status"] != DBNull.Value)
                        {
                            emp.att_status = Convert.ToString(reader["Att_status"]);
                        }
                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }

        public void Insert_User_login(User_Login ul)
        {
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("insert into Login (User_name,Password,User_email,Role_id,User_flag) values(@User_name,@Password,@User_email,@Role_id,@User_flag)", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@User_name", ul.User_name);
                    cmd.Parameters.AddWithValue("@Password", ul.Password);
                    cmd.Parameters.AddWithValue("@User_email", ul.User_email);
                    cmd.Parameters.AddWithValue("@Role_id", ul.Role_id);
                    cmd.Parameters.AddWithValue("@User_flag", "U");


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Role> Role_Dropdown()
        {
            List<Role> DBase = new List<Role>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select r.Role_id, r.Role_name from Role r where r.Role_Active <> 'N'", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Role emp = new Role();

                        if (reader["Role_id"] != DBNull.Value)
                        {
                            emp.Role_id = Convert.ToInt32(reader["Role_id"]);
                        }
                        if (reader["Role_name"] != DBNull.Value)
                        {
                            emp.Role_name = Convert.ToString(reader["Role_name"]);
                        }
                        DBase.Add(emp);

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
                using (SqlCommand cmd = new SqlCommand("select bh.BH_ID,bh.BATCH_NAME, t.Teach_name, bh.bh_end_date , bh.Course_visible, ISNULL(bh.Course_complete,'N')Course_complete from Batch_header bh , Teacher t where bh.TEACHER_1 = t.Teach_id order by bh.BH_ID desc", conn))
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Batch_list emp = new Batch_list();


                        if (reader["BH_ID"] != DBNull.Value)
                        {
                            emp.Bh_id = Convert.ToInt32(reader["BH_ID"]);
                        }

                        if (reader["BATCH_NAME"] != DBNull.Value)
                        {
                            emp.Batch_name = reader["BATCH_NAME"].ToString();
                        }

                        if (reader["Teach_name"] != DBNull.Value)
                        {
                            emp.Teach_name = reader["Teach_name"].ToString();
                        }

                        if (reader["bh_end_date"] != DBNull.Value)
                        {
                            emp.bh_end_date = Convert.ToDateTime(reader["bh_end_date"]);
                        }
                        if (reader["Course_visible"] != DBNull.Value)
                        {
                            emp.Course_visible = reader["Course_visible"].ToString();
                        }

                        if (reader["Course_complete"] != DBNull.Value)
                        {
                            emp.Course_complete = reader["Course_complete"].ToString();
                        }



                        DBase.Add(emp);

                    }
                }
            }
            return DBase;
        }

        public void HideCourse(int id, string hide, string coursecomplete)
        {

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("update batch_header  set Course_visible =@hide, Course_complete = @Course_complete  where BH_ID =@bh_id", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@Course_complete", coursecomplete);
                    cmd.Parameters.AddWithValue("@hide", hide);
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Registor> fatch_students_list(int id)
        {
            List<Registor> DBase = new List<Registor>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select l.Bh_id, l.User_id,CONCAT(l.User_name, ' ',l.F_H_name) Full_name, l.recommended, l.User_contact,l.ID_Card, l.User_status  from Login l where l.Bh_id = @bh_id and l.User_flag = 'S' order by l.user_status desc", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Registor teach = new Registor();

                        if (reader["Bh_id"] != DBNull.Value)
                        {
                            teach.bh_id = Convert.ToInt32(reader["Bh_id"]);
                        }
                        if (reader["User_id"] != DBNull.Value)
                        {
                            teach.User_id = Convert.ToInt32(reader["User_id"]);
                        }
                        if (reader["Full_name"] != DBNull.Value)
                        {
                            teach.Full_Name = reader["Full_name"].ToString();
                        }

                        if (reader["recommended"] != DBNull.Value)
                        {
                            teach.email = reader["recommended"].ToString();
                        }
                        if (reader["User_contact"] != DBNull.Value)
                        {
                            teach.M_W_no = reader["User_contact"].ToString();
                        }

                        if (reader["ID_Card"] != DBNull.Value)
                        {
                            teach.IDCardNo = reader["ID_Card"].ToString();
                        }
                        if (reader["User_status"] != DBNull.Value)
                        {
                            teach.User_status = reader["User_status"].ToString();
                        }

                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }

        public Registor user_check_status(int bh_id, string IDcardno)
        {
            Registor employee = new Registor();

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select l.ID_Card, l.User_status from Login l where l.Bh_id = @bh_id and l.ID_Card = @IDcardno", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", bh_id);
                    cmd.Parameters.AddWithValue("@IDcardno", IDcardno);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader["User_status"] != DBNull.Value)
                        {
                            employee.User_status = Convert.ToString(reader["User_status"]);
                        }
                    }

                }
            }
            return employee;
        }

        public List<List_Header> List_Header_list()
        {
            List<List_Header> DBase = new List<List_Header>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select lh.list_id, lh.list_name, count(*) std from list_header lh, list_details ld where lh.list_id = ld.list_id group by lh.list_name,lh.list_id order by lh.list_id desc", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        List_Header teach = new List_Header();

                        if (reader["list_id"] != DBNull.Value)
                        {
                            teach.List_id = Convert.ToInt32(reader["list_id"]);
                        }
                        if (reader["list_name"] != DBNull.Value)
                        {
                            teach.List_name = Convert.ToString(reader["list_name"]);
                        }
                        if (reader["std"] != DBNull.Value)
                        {
                            teach.stud_count = Convert.ToString(reader["std"]);
                        }


                        DBase.Add(teach);

                    }
                }
            }
            return DBase;
        }


        public List<List_Details> List_Details_data(int id)
        {
            List<List_Details> DBase = new List<List_Details>();
            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select ld.id, ld.list_id, l.User_name STUDENTS from list_details ld, login l where ld.stud_id = l.user_id and ld.list_id =@bh_id", conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@bh_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        List_Details teach = new List_Details();

                        if (reader["id"] != DBNull.Value)
                        {
                            teach.id = Convert.ToInt32(reader["id"]);
                        }

                        if (reader["list_id"] != DBNull.Value)
                        {
                            teach.List_id = Convert.ToInt32(reader["list_id"]);
                        }
                        if (reader["STUDENTS"] != DBNull.Value)
                        {
                            teach.Stud_name = Convert.ToString(reader["STUDENTS"]);



                            DBase.Add(teach);

                        }
                    }
                }
                return DBase;
            }
        }

        public void listdtldelete(int id, int list_id)
        {

            using (SqlConnection conn = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("delete from list_details where list_id = @list_id and id = @id", conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@list_id", list_id);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
