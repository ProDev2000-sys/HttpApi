using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpApiPractice.Models;
using System.Data;
using System.Text.Json;
using Newtonsoft.Json;

namespace HttpApiPractice.Controllers
{
    public class PracticeController : Controller
    {
        

        [HttpGet("BookData")]
        public ConcurrentDictionary<int,book> BookData()
        {
            var cs = "Host=localhost;Username=postgres;Password=sps@7890;Database=Books";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT * from book";

            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            ConcurrentDictionary<int, book> bookDict = new ConcurrentDictionary<int, book>();

            while (rdr.Read())
            {
                book bk = new book();
                bk.Id = rdr.GetInt32("Id");
                bk.Name = rdr.GetString("Name");
                bk.Author = rdr.GetString("Author");
                bk.Isbn = rdr.GetString("Isbn");
                bk.price = rdr.GetInt32("price");

                bookDict.TryAdd(bk.Id, bk);


            }
        
            return bookDict;
        }

        [HttpGet("StudentData")]
        public string StudentData()
        {
            var cs = "Host=localhost;Username=postgres;Password=sps@7890;Database=Practice_Thurs_15_07";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT * from student";

            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            ConcurrentDictionary<int, student> studentDict = new ConcurrentDictionary<int, student>();

            while (rdr.Read())
            {
                student studentObj = new student();
                studentObj.stu_id = rdr.GetInt32("stu_id");
                studentObj.fname = rdr.GetString("fname");
                studentObj.lname = rdr.GetString("lname");
                studentObj.email_id = rdr.GetString("email_id");
                studentObj.m_no = rdr.GetDecimal("m_no");
                studentObj.age = rdr.GetDecimal("age");
                studentDict.TryAdd(studentObj.stu_id, studentObj);
            }



            foreach (var i in studentDict)
            {
                Console.WriteLine(i.Key);
                Console.WriteLine("ID:{0}" + " " + "First Name : {1} " + " " + "Last Name : {2}" + " " + "Email ID : {3}" + " " + "Mobile Number : {4}" + " " + "Age : {5}", i.Value.stu_id, i.Value.fname, i.Value.lname, i.Value.email_id, i.Value.m_no, i.Value.age);
            }

            rdr.Close();
            var sql2 = "SELECT * from teacher";

            using var cmd2 = new NpgsqlCommand(sql2, con);

            using NpgsqlDataReader rdr2 = cmd2.ExecuteReader();

            ConcurrentDictionary<int, teacher> teacherDict = new ConcurrentDictionary<int, teacher>();

            while (rdr2.Read())
            {
                teacher teach = new teacher();

                teach.t_id = rdr2.GetInt32("t_id");
                teach.f_name = rdr2.GetString("f_name");
                teach.l_name = rdr2.GetString("l_name");
                teach.department = rdr2.GetString("department");

                teacherDict.TryAdd(teach.t_id, teach);

            }

            Console.WriteLine("____________________________________________________________________");

            foreach (var item in teacherDict)
            {
                Console.WriteLine(item.Value.l_name);

            }
            rdr2.Close();

            var sql3 = "SELECT * from studententry";

            using var cmd3 = new NpgsqlCommand(sql3, con);

            using NpgsqlDataReader rdr3 = cmd3.ExecuteReader();

            ConcurrentDictionary<int, studententry> studentEntryDict = new ConcurrentDictionary<int, studententry>();

            while (rdr3.Read())
            {
                studententry ster = new studententry();

                ster.id = rdr3.GetInt16("id");

                ster.student_name = rdr3.IsDBNull("student_name") ? null : rdr3.GetString("student_name");


                studentEntryDict.TryAdd(ster.id, ster);

            }

            var query1 = (from st in studentDict
                          join ste in studentEntryDict
                          on st.Value.stu_id equals ste.Value.id
                          orderby ste.Value.student_name
                          select new
                          {
                              ste.Value.student_name,
                              ste.Value.id,
                              st.Value.email_id,
                              st.Value.fname,
                              st.Value.lname,
                              st.Value.m_no,
                              st.Value.age

                          }).ToList();


            foreach (var i in query1)
            {
                Console.WriteLine(i);
               

            }
            string json = JsonConvert.SerializeObject(query1, Formatting.Indented);

            return json;    





        }

      
    }
}
