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
    public partial class doubleServier : Form
    {
        int i = 0, j = 0;
//interarrival table arrays 
        static int size = 4, Size = 4;
        int[] interarrivalls = new int[size];
        float[] propapility = new float[size];
        float[] cumulative = new float[size];
        int[] from = new int[size];
        int[] to = new int[size];
// service time server 1
        int[]s1_services = new int[Size];
        float[] s1_propapility = new float[Size];
        float[] s1_cumulative = new float[Size];
        int[] s1_from = new int[Size];
        int[] s1_to = new int[Size];

// service time server 2
        int[] s2_services = new int[Size];
        float[] s2_propapility = new float[Size];
        float[] s2_cumulative = new float[Size];
        int[] s2_from = new int[Size];
        int[] s2_to = new int[Size];

        public doubleServier()
        {
            InitializeComponent();
        }



        private int digit(int diget)
        {
            int arrival = -1;
            for (int i = 0; i < size; i++)
            {
                if (diget >= from[i] && diget <= to[i])
                { arrival = interarrivalls[i]; break; }
            }
            return arrival;
        }
        private int s1_diget(int digit)
        {
            int service = -1;
            for (int i = 0; i < Size; i++)
            {
                if (digit >= s1_from[i] && digit <= s1_to[i])
                { service = s1_services[i]; break; }
            }
            return service;
        }
        private int s2_diget(int digit)
        {
            int service = -1;
            for (int i = 0; i < Size; i++)
            {
                if (digit >= s2_from[i] && digit <= s2_to[i])
                { service = s2_services[i]; break; }
            }
            return service;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //add  values of cumulative table arrays

            interarrivalls[i] = int.Parse(textBox2.Text);
            propapility[i] = float.Parse(textBox3.Text);
            s1_services[i] = int.Parse(textBox4.Text);
            s1_propapility[i] = float.Parse(textBox5.Text);
            s2_services[i] = int.Parse(textBox6.Text);
            s2_propapility[i] = float.Parse(textBox7.Text);
            if (i == 0)
            {
                //
                cumulative[i] = propapility[i];
                from[i] = 1; to[i] = (int)(cumulative[i] * 100);
                //
                s1_cumulative[i] = s1_propapility[i];
                s1_from[i] = 1; s1_to[i] = (int)(s1_cumulative[i] * 100);
                //
                s2_cumulative[i] = s2_propapility[i];
                s2_from[i] = 1; s2_to[i] = (int)(s2_cumulative[i] * 100);
                
                i++;
            }
            else
            {
                //
                cumulative[i] = propapility[i] + cumulative[i - 1];
                from[i] = (int)(to[i - 1] + 1); to[i] = (int)(cumulative[i] * 100);
                //
                s1_cumulative[i] = s1_propapility[i] + s1_cumulative[i - 1];
                s1_from[i] = (int)(s1_to[i - 1] + 1); s1_to[i] = (int)(s1_cumulative[i] * 100);
                //
                s2_cumulative[i] = s2_propapility[i] + s2_cumulative[i - 1];
                s2_from[i] = (int)(s2_to[i - 1] + 1); s2_to[i] = (int)(s2_cumulative[i] * 100);
                i++;
            }
            //create fill cumulative table
            listView2.Items.Add(new ListViewItem(new[] { interarrivalls[j].ToString(), (propapility[j]).ToString(), (cumulative[j]).ToString(), (from[j]).ToString(), (to[j]).ToString() }));
            listView3.Items.Add(new ListViewItem(new[] { s1_services[j].ToString(), (s1_propapility[j]).ToString(), (s1_cumulative[j]).ToString(), (s1_from[j]).ToString(), (s1_to[j]).ToString() }));
            listView4.Items.Add(new ListViewItem(new[] { s2_services[j].ToString(), (s2_propapility[j]).ToString(), (s2_cumulative[j]).ToString(), (s2_from[j]).ToString(), (s2_to[j]).ToString() }));

            textBox2.Text = ""; textBox3.Text = "";
            textBox4.Text = ""; textBox5.Text = "";
            textBox6.Text = ""; textBox7.Text = "";
            j++;
        }

        private void simulate()
        {
            //initialization of variable for first row

            int customerNomber = int.Parse(textBox1.Text), customer = 2, rand, rand2,j=1;
            int interarrivalStart = from[0];
            int interarrivalEnd = to[size - 1];
            int serviceTimeStart = s1_from[0];
            int serviceTimeEnd = s1_to[Size - 1];
            int interarrivalTime = 0, wait = 0, idle = 0;
            int arrivalTime = 0;
            // server1
            int s1_service, s1_begin = arrivalTime;
            //servser1
            int s2_service = 0 ,s2_begin = 0;
            int[] s2_end = new int[customerNomber];
            s2_end[0]=0;
            string empty = " "; 
            Random r = new Random();
            rand2 = r.Next(serviceTimeStart, serviceTimeEnd + 1);
            s1_service = s1_diget(rand2);
            int[] s1_end = new int[customerNomber];
             s1_end[0] = s1_service;
            listView1.Items.Add(new ListViewItem(new[] { "1", (interarrivalTime).ToString(), (arrivalTime).ToString(), (rand2).ToString(),
                (s1_begin).ToString(), s1_service.ToString(), (s1_end[0]).ToString(),s2_begin.ToString(), "0" ,s2_end[0].ToString(), wait.ToString() }));
            //for other rows
            while (customer <= customerNomber)
            {
                rand = r.Next(interarrivalStart, interarrivalEnd + 1);
                interarrivalTime = digit(rand);
                // Console.Write(interarrivalTime + " ");
                // Console.WriteLine(rand);
                arrivalTime = arrivalTime + interarrivalTime;
                rand2 = r.Next(serviceTimeStart, serviceTimeEnd + 1);
                if (s1_begin==0) { s2_service = s2_diget(rand2); s1_service = 0; } 
                    else { s1_service = s1_diget(rand2); s2_service = 0; }
                // Console.Write(serviceTime + " ");
                //  Console.WriteLine(rand2);
                //
                if (s1_end.Max() > s2_end.Max()) { s1_begin = 0; s1_end[j] = 0; } 
                    else { s1_begin = Math.Max(s1_end[j-1], arrivalTime); s1_end[j] = s1_begin + s1_service; }
                
                if (s1_begin!=0) { s2_begin = 0; s2_end[j] = 0; }
                    else { s2_begin = Math.Max(s2_end.Max(), arrivalTime); s2_end[j] = s2_begin + s2_service; }
                
                if (s1_begin !=0) wait = s1_end[j] - arrivalTime;
                     else wait = s2_end[j] - arrivalTime;
               if (wait<0) wait=0;
                //idle = arrivalTime - end; if (idle < 0) idle = 0;
                //s1_end = s1_begin + serviceTime;
                //spend = wait + serviceTime;
                listView1.Items.Add(new ListViewItem(new[] { customer.ToString(), (interarrivalTime).ToString(), (arrivalTime).ToString(), (rand2).ToString(),
                (s1_begin).ToString(), s1_service.ToString(), (s1_end[j]).ToString(), (s2_begin).ToString(), s2_service.ToString(), (s2_end[j]).ToString(), wait.ToString() }));
           
                customer++;
            }
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            simulate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home pop = new Home();
            pop.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home pop = new Home();
            pop.Show();
        }



    }
}
