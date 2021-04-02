using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Lab10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //baudrate
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("14400");
            comboBox2.Items.Add("19200");
            comboBox2.Items.Add("38400");
            comboBox2.Items.Add("56000");
            comboBox2.Items.Add("57600");
            comboBox2.Items.Add("76800");
            comboBox2.Items.Add("115200");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String[] portList = System.IO.Ports.SerialPort.GetPortNames();
            foreach (String portName in portList)
                comboBox1.Items.Add(portName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Connect";
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                toolStripLabel1.Text = serialPort1.PortName + " is closed.";
                button1.Text = "Connect";
            }
            else
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Int32.Parse(comboBox2.Text);
                serialPort1.NewLine = "\r\n";
                serialPort1.Open();
                toolStripLabel1.Text = serialPort1.PortName + " is connected.";
                button1.Text = "Disconnect";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            toolStripLabel1.Text = serialPort1.PortName + " is closed.";
        }

        private string pilihFile = "";

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pilihFile = openFileDialog1.FileName;
                label1.Text = pilihFile;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string baris;
            int counter = 0;
            listBox1.Items.Clear();
            TextReader txt = new StreamReader(pilihFile);
            while ((baris = txt.ReadLine()) != null)
            {
            listBox1.Items.Add(baris);
            counter++;
            serialPort1.WriteLine(baris);
            }
            toolStripLabel1.Text = "Sending " + counter.ToString() + "-line(s)";
            txt.Close();
        }
    }
}
