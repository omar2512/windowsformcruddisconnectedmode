using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace itilabdisconnected
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PTQ4F3S\SQL;Initial Catalog=cSharpLab;Integrated Security=True");
        private void btndisplay_Click(object sender, EventArgs e)
        {

            SqlDataReader sdr;
            try
            {
                if (listBox1.Items.Count > 0)
                {
                    listBox1.Items.Clear();
                    comboBox1.Items.Clear();
                }
                sqlCommand1.CommandText = "Select id ,name ,depatment from person";
                sqlCommand1.Connection = con;
                con.Open();
                sdr = sqlCommand1.ExecuteReader();
                while (sdr.Read())
                {
                    string srg = (((int)sdr[0]).ToString()) + "\t" + sdr[1].ToString() + "\t" + sdr[2].ToString();
                    listBox1.Items.Add(srg);
                    comboBox1.Items.Add((int)sdr[0]);
                }
                sdr.Close();
                con.Close();
            }
            catch (Exception ex){ MessageBox.Show(ex.ToString()); }
        }

        private void tbnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "insert into person values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                Excutequery(str, "data has been added");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Excutequery(string st, string msg)
        {
            sqlCommand1.CommandText = st;
            sqlCommand1.Connection = con;
            con.Open();
            int affectedrows= sqlCommand1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(affectedrows.ToString() + "rows changed" +msg);

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "update person set id='" + textBox1.Text + "',name='" + textBox2.Text + "',depatment ='" + textBox3.Text + "' where id='"+textBox1.Text+"'";
                Excutequery(str,"data has been updated");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sqlCommand1.CommandText = "select id ,name ,depatment from person where id='" + comboBox1.Text + "'";
                sqlCommand1.Connection = con;
                con.Open();
                SqlDataReader sdr = sqlCommand1.ExecuteReader();
                if (sdr.Read())
                {
                    textBox1.Text = ((int)sdr[0]).ToString();
                    textBox2.Text = sdr[1].ToString();
                    textBox3.Text = sdr[2].ToString();
                }
                sdr.Close();
                con.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btndelet_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "delete from person where id='"+textBox1.Text+"'";
                Excutequery(str, "data has been deleted");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
