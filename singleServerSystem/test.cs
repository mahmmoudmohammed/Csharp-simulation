using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace singleServerSystem
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }
        int i = 0;static int size=5;
        int[] interarrivalls = new int[size];
        float[] propapility = new float[size];
        float[] cumulative = new float[size];
        int[]from = new int[size];
        int[] to = new int[size];
         //cumulative[0] = propapility[0];
        private void button1_Click(object sender, EventArgs e)
        {

            interarrivalls[i] = int.Parse(textBox1.Text);
            propapility[i] = float.Parse(textBox2.Text);
            if (i == 0)
            {
                cumulative[i] = propapility[i];
                from[i] = 1; to[i] = (int)(cumulative[i] * 100);
                i++;
            }
            else
            {
                cumulative[i] = propapility[i] + cumulative[i - 1];
                from[i] = (int)(to[i-1]+1);   to[i] = (int)(cumulative[i] * 100);
                i++;
            }
            textBox1.Text = "";
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string interarr = "", prop = "", comu = "", froom = "", too = "";
            for (int i = 0; i < size; i++)
            {
              interarr += interarrivalls[i].ToString() + " ";
              prop += propapility[i].ToString() + " ";
              comu += cumulative[i].ToString() + " ";
              froom += from[i].ToString() + " ";
              too += to[i].ToString() + " ";
                
            }
            textBox3.Text = interarr;
            textBox4.Text = prop;
            textBox5.Text = comu;
            textBox6.Text = from[0].ToString();
            textBox7.Text = to[size-1].ToString();
        }
    }
}
