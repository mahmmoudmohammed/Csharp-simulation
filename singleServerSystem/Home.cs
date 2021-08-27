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
    public partial class Home : Form
    {
        static int size=4,Size=4;
        int i = 0,j=0;
        int[] interarrivalls = new int[size];
        float[] propapility = new float[size];
        float[] cumulative = new float[size];
        int[] from = new int[size];
        int[] to = new int[size];

        int[] services = new int[Size];
        float[] service_propapility = new float[Size];
        float[] service_cumulative = new float[Size];
        int[] service_from = new int[Size];
        int[] service_to = new int[Size];
       
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            simulate();

        }
        private int digit(int diget)
        {
            int arrival =-1;
            for (int i = 0; i < size; i++)
			{
                if (diget >= from[i] && diget <= to[i])
                { arrival = interarrivalls[i]; break; }
			}
            return arrival;
        }
        private int diget(int digit)
        {
            int  service = -1;
            for (int i = 0; i < Size; i++)
            {
                if (digit >= service_from[i] && digit <= service_to[i])
                { service = services[i]; break; }
            }
            return service;
        }

        private void simulate()
        {
            //initialization of variable for first row

            int customerNomber = int.Parse(textBox1.Text), customer = 2,rand,rand2;
            int interarrivalStart = from[0];
            int interarrivalEnd =to[size-1];
            int serviceTimeStart = service_from[0];
            int serviceTimeEnd = service_to[Size - 1];
            int interarrivalTime = 0, wait = 0, idle = 0;
            int serviceTime, arrivalTime = 0, begin = arrivalTime;

            Random r = new Random();
            rand2 = r.Next(serviceTimeStart, serviceTimeEnd + 1);
            serviceTime = diget(rand2);
           
            int end = serviceTime, spend = serviceTime;
            listView1.Items.Add(new ListViewItem(new[] { "1", (serviceTime).ToString(), (interarrivalTime).ToString(), (arrivalTime).ToString(), (begin).ToString(), (end).ToString(), wait.ToString(), spend.ToString(), idle.ToString() }));
            //for other rows
            while (customer <= customerNomber)
            {
                 rand = r.Next(interarrivalStart, interarrivalEnd + 1);
                interarrivalTime = digit(rand);
               // Console.Write(interarrivalTime + " ");
               // Console.WriteLine(rand);
                arrivalTime = arrivalTime + interarrivalTime;
                rand2 = r.Next(serviceTimeStart, serviceTimeEnd + 1);
                serviceTime = diget(rand2);
               // Console.Write(serviceTime + " ");
              //  Console.WriteLine(rand2);
                begin = Math.Max(end, arrivalTime);
                if (end > arrivalTime)
                    wait = end - arrivalTime;
                idle = arrivalTime - end; if (idle < 0) idle = 0;
                end = begin + serviceTime;
                spend = wait + serviceTime;
                listView1.Items.Add(new ListViewItem(new[] { customer.ToString(), (serviceTime).ToString(), (interarrivalTime).ToString(), (arrivalTime).ToString(), begin.ToString(), end.ToString(), wait.ToString(), spend.ToString(), idle.ToString() }));
                customer++;
            }
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            //add  values of cumulative table arrays

            interarrivalls[i] = int.Parse(textBox2.Text);
            propapility[i] = float.Parse(textBox3.Text);
            services[i] = int.Parse(textBox4.Text);
            service_propapility[i] = float.Parse(textBox5.Text);
            if (i == 0)
            {
                //
                cumulative[i] = propapility[i];
                from[i] = 1; to[i] = (int)(cumulative[i] * 100);
                //
                service_cumulative[i] = service_propapility[i];
                service_from[i] = 1; service_to[i] = (int)(service_cumulative[i] * 100);
                i++;
            }
            else
            {
                cumulative[i] = propapility[i] + cumulative[i - 1];
                from[i] = (int)(to[i - 1] + 1); to[i] = (int)(cumulative[i] * 100);
                service_cumulative[i] = service_propapility[i] + service_cumulative[i - 1];
                service_from[i] = (int)(service_to[i - 1] + 1); service_to[i] = (int)(service_cumulative[i] * 100);
                i++;
            }
            //create fill cumulative table
            listView2.Items.Add(new ListViewItem(new[] { interarrivalls[j].ToString(), (propapility[j]).ToString(), (cumulative[j]).ToString(), (from[j]).ToString(), (to[j]).ToString() }));
            listView3.Items.Add(new ListViewItem(new[] { services[j].ToString(), (service_propapility[j]).ToString(), (service_cumulative[j]).ToString(), (service_from[j]).ToString(), (service_to[j]).ToString() }));

            textBox2.Text = ""; textBox3.Text = "";
            textBox4.Text = ""; textBox5.Text = "";
            j++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            doubleServier pop = new doubleServier();
            pop.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            doubleServier pop = new doubleServier();
            pop.Show();
        }
    }
}
