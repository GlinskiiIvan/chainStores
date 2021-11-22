using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chainStores
{
    public partial class Form1 : Form
    {

        List<Store_Basic> all = new List<Store_Basic>();

        public abstract class Store_Basic {
            protected string name, adress, workSchedule;
            protected double size;

            public virtual string Name
            {
                get { return name; }
                set { name = value; }
            }

            public virtual double Size
            {
                get { return size; }
                set { size = value; }
            }

            public virtual string Adress
            {
                get { return adress; }
                set { adress = value; }
            }

            public virtual string WorkSchedule
            {
                get { return workSchedule; }
                set { workSchedule = value; }
            }

        }

        public class Stall: Store_Basic
        {
            public Stall() { }
            public Stall(string name, string adress, double size, string workSchedule)
            {
                this.name = name;
                this.adress = adress;
                this.size = size;
                this.workSchedule = workSchedule;
            }
        }

        public class Stall_Pavilion : Stall
        {
            public Stall_Pavilion() { }
            public Stall_Pavilion(string name, string adress, double size, string workSchedule, int numberEmployees) : base(name, adress, size, workSchedule)
            {
                this.numberEmployees = numberEmployees;
            }

            protected int numberEmployees;

            public int NumberEmployees
            {
                get { return numberEmployees; }
                set { numberEmployees = value; }
            }

        }

        public class Pavilion : Stall_Pavilion
        {
            public Pavilion(string name, string adress, double size, string workSchedule, int numberEmployees, int numberParkingSpaces, string additionalServices) :
                base(name, adress, size, workSchedule, numberEmployees)
            {                
                this.numberParkingSpaces = numberParkingSpaces;
                this.additionalServices = additionalServices;
            }

            private string additionalServices;
            private int numberParkingSpaces;

            public string AdditionalServices
            {
                get { return additionalServices; }
                set { additionalServices = value; }
            }

            public int NumberParkingSpaces
            {
                get { return numberParkingSpaces; }
                set {  numberParkingSpaces = value; }
            }

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (!IsEmpty(textBox_Name.Text) && !IsEmpty(textBox_Adress.Text) && !IsEmpty(textBox_Size.Text) && !IsEmpty(textBox_WorkSchedule.Text)
               && !IsEmpty(textBox_NumberEmployees.Text) && !IsEmpty(textBox_NumberParkingSpaces.Text) && !IsEmpty(textBox_AdditionalServices.Text))
            {
                Pavilion pavilion = new Pavilion(
                    textBox_Name.Text, textBox_Adress.Text, Convert.ToDouble(textBox_Size.Text), textBox_WorkSchedule.Text,
                    Int32.Parse(textBox_NumberEmployees.Text), Int32.Parse(textBox_NumberParkingSpaces.Text),
                    textBox_AdditionalServices.Text
                );
                if (!CountStore(all, textBox_Name.Text)) all.Add(pavilion);
                else MessageBox.Show("Магазин с таким именем уже существует.");
            }
            else if (!IsEmpty(textBox_Name.Text) && !IsEmpty(textBox_Adress.Text) && !IsEmpty(textBox_Size.Text) && !IsEmpty(textBox_WorkSchedule.Text)
               && !IsEmpty(textBox_NumberEmployees.Text) && IsEmpty(textBox_NumberParkingSpaces.Text) && IsEmpty(textBox_AdditionalServices.Text))
            {
                Stall_Pavilion stall_Pavilion = new Stall_Pavilion(textBox_Name.Text, textBox_Adress.Text, Convert.ToDouble(textBox_Size.Text), textBox_WorkSchedule.Text,
                    Int32.Parse(textBox_NumberEmployees.Text));
                if (!CountStore(all, textBox_Name.Text)) all.Add(stall_Pavilion);
                else MessageBox.Show("Магазин с таким именем уже существует.");                
            }
            else if (!IsEmpty(textBox_Name.Text) && !IsEmpty(textBox_Adress.Text) && !IsEmpty(textBox_Size.Text) && !IsEmpty(textBox_WorkSchedule.Text)
               && IsEmpty(textBox_NumberEmployees.Text) && IsEmpty(textBox_NumberParkingSpaces.Text) && IsEmpty(textBox_AdditionalServices.Text))
            {
                Stall stall = new Stall(textBox_Name.Text, textBox_Adress.Text, Convert.ToDouble(textBox_Size.Text), textBox_WorkSchedule.Text);
                if (!CountStore(all, textBox_Name.Text)) all.Add(stall);
                else MessageBox.Show("Магазин с таким именем уже существует.");                
            }
            else MessageBox.Show("Форма заполнена некорректно.");
        }

        private void button_Find_Click(object sender, EventArgs e)
        {
            foreach(var item in all)
            {
                if(item.Adress == textBoxFindAdress.Text)
                {
                    textBox_Answer.Text = "Название: " + item.Name + Environment.NewLine;
                    textBox_Answer.Text += "Адрес: " + item.Adress + Environment.NewLine;
                    textBox_Answer.Text += "Размер: " + item.Size + "кв/м" + Environment.NewLine;
                    textBox_Answer.Text += "График работы: " + item.WorkSchedule + Environment.NewLine;
                    if(item.GetType() == typeof(Stall_Pavilion))
                    {
                        textBox_Answer.Text += "Количество работников: " + ((Stall_Pavilion)item).NumberEmployees + Environment.NewLine;
                    }
                    if (item.GetType() == typeof(Pavilion))
                    {
                        textBox_Answer.Text += "Количество работников: " + ((Pavilion)item).NumberEmployees + Environment.NewLine;
                        textBox_Answer.Text += "Количество парковочных мест: " + ((Pavilion)item).NumberParkingSpaces + Environment.NewLine;
                        textBox_Answer.Text += "Дополнительные услуги: " + ((Pavilion)item).AdditionalServices + Environment.NewLine;

                    }
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_Name.Text = "";
            textBox_Adress.Text = "";
            textBox_Size.Text = "";
            textBox_WorkSchedule.Text = "";
            textBox_NumberEmployees.Text = "";
            textBox_NumberParkingSpaces.Text = "";
            textBox_AdditionalServices.Text = "";
        }

        bool IsEmpty(string str)
        {
            if(str == "") return true;
            else return false;
        }

        bool CountStore(List<Store_Basic> all, string name)
        {            
            foreach(var store in all)
            {
                if (store.Name == name) return true;                
            }
            return false;
        }
    }
}
