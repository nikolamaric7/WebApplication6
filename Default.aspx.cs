﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication6
{
    public partial class _Default : System.Web.UI.Page
    {
        public string conStr = "Data Source=.\\SQLEXPRESS;Initial Catalog=rezervacije;Integrated Security=true";
        public const int MIN_SEDISTA = 2;
        public const int MAX_SEDISTA = 53;
        public List<int> rezervisana = new List<int>();
        private List<Button> mesta = new List<Button>();
        protected void Page_Load(object sender, EventArgs e)
        {
            isticaj();
            kreirajMesta();
            kreirajTabelu();
        }
        public bool rezervisano(int sediste)
        {
            foreach (int broj in rezervisana)
            {
                if(broj==sediste){
                    return true;
                }
            }
            return false;
        }
        public void isticaj()
        {
            string select = "select brojSedista from sedista";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = conStr;

            SqlCommand cmd = new SqlCommand(select, con);

            SqlDataReader reader;

            using(con)
            {
                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        rezervisana.Add(Int32.Parse(reader["brojSedista"].ToString()));
                    }
                    reader.Close();
                    con.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        private void kreirajMesta()
        {
                for(int i=MIN_SEDISTA;i<MAX_SEDISTA;i++)
            {
                Button mesto = new Button();
                mesto.Text = i + "";
                mesta.Add(mesto);
            }
        }
        private void izaberiMesto(object o, EventArgs e)
        {
            Button b = (Button)o;
            txtSediste.Text = b.Text;
        }

        public void kreirajTabelu()
        {
            int mesto = 0;
            for(int i=0;i<13;i++)
            {
                TableRow red = new TableRow();
                red.Height = 20;
                for(int j=0;j<5;j++)
                {
                    TableCell celija = new TableCell();
                    celija.HorizontalAlign = HorizontalAlign.Center;
                    if (i == 0)
                    {
                        if(j==2)
                        {
                            celija.RowSpan = 13;
                            celija.Width = 30;
                        }
                        else
                        {
                            mesta.ElementAt(mesto).Click += new EventHandler(izaberiMesto);
                            if (rezervisano(Int32.Parse(mesta.ElementAt(mesto).Text))) 
                            {
                                mesta.ElementAt(mesto).BackColor = System.Drawing.Color.Red;
                                mesta.ElementAt(mesto).Enabled = false;
                            }
                            else
                            {
                                mesta.ElementAt(mesto).BackColor = System.Drawing.Color.Green;
                            }
                            celija.Controls.Add(mesta.ElementAt(mesto));
                            celija.ForeColor = System.Drawing.Color.Black;
                            celija.BackColor = System.Drawing.Color.LightBlue;
                            mesto++;
                        }
                    }
                    else
                    {
                        if (j == 2)
                        {
                            continue;
                        }
                        else
                        {
                            if (rezervisano(Int32.Parse(mesta.ElementAt(mesto).Text)))
                            {
                                mesta.ElementAt(mesto).BackColor = System.Drawing.Color.Red;
                                mesta.ElementAt(mesto).Enabled = false;
                            }
                            else
                            {
                                mesta.ElementAt(mesto).BackColor = System.Drawing.Color.Green;
                            }
                            mesta.ElementAt(mesto).Click += new EventHandler(izaberiMesto);
                            celija.Controls.Add(mesta.ElementAt(mesto));
                            celija.ForeColor = System.Drawing.Color.Black;
                            celija.BackColor = System.Drawing.Color.LightBlue;
                            if (mesto < 51)
                                mesto++;
                        }
                    }
                    red.Cells.Add(celija);
                }
                red.BorderWidth = 1;
                red.BorderStyle = BorderStyle.Solid;
                red.BorderColor = System.Drawing.Color.Black;
                tabela.Rows.Add(red);
            }
        }
        protected void btn_Click(object sender,EventArgs e)
        {
            string insert;
            insert = "INSERT INTO sedista(id, brojSedista, ime_prezime, email)";
            insert += "VALUES ('";
            insert += txtSediste.Text + "','";
            insert += txtSediste.Text + "','";
            insert += txtImePrezime.Text + "','";
            insert += txtEmail.Text + "')";

            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(insert, con);
            int dodat = 0;
            using(con)
            {
                try
                {
                    con.Open();
                    dodat = cmd.ExecuteNonQuery();
                    rezervisana.Add(Int32.Parse(txtSediste.Text));
                    foreach(Button b in mesta)
                    {
                        if(b.Text==txtSediste.Text)
                        {
                            b.BackColor = System.Drawing.Color.Red;
                            b.Enabled = false;
                        }
                    }
                    con.Close();
                    txtSediste.Text = "";
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
    