using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace naloga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListBoxData();
         
            
        }
        /// <summary>
        /// make list of people from database
        /// </summary>
        public void ListBoxData()
        {
            listBoxPeople.Items.Clear();
            List<Person> people = new List<Person>();
            people = Service.FindAllPeople();
            foreach (Person person in people)
            {
                listBoxPeople.Items.Add(person.IdAndNameAndSurname);
            }
        }
        /// <summary>
        /// clear data in textBoxes
        /// </summary>
        public void ClearData()
        {
            txtBoxName.Text = "";
            txtBoxSurname.Text = "";
            txtBoxAdress.Text = "";
            txtBoxVatNumber.Text = "";
            txtBoxCity.Text = "";
            txtBoxPostCode.Text = "";
        }
       /// <summary>
       /// enable od disable textboxes, buttons
       /// </summary>
       /// <param name="value"></param>
       public void Enable(bool value)
        {
            if(value == true)
            {
                txtBoxName.IsEnabled = true;
                txtBoxSurname.IsEnabled = true;
                txtBoxVatNumber.IsEnabled = true;
                txtBoxAdress.IsEnabled = true;
                txtBoxPostCode.IsEnabled = true;
                txtBoxCity.IsEnabled = true;
                btnOptionButton.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnEdit.IsEnabled = true;
            }
            else
            {
                txtBoxName.IsEnabled = false;
                txtBoxSurname.IsEnabled = false;
                txtBoxVatNumber.IsEnabled = false;
                txtBoxAdress.IsEnabled = false;
                txtBoxPostCode.IsEnabled = false;
                txtBoxCity.IsEnabled = false;
                btnOptionButton.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnEdit.IsEnabled = false;
            }
        }

        /// <summary>
        /// find data of person from input text
        /// </summary>
        /// <returns></returns>
        public Person GetData()
        {
            Person newPerson = new Person();
            newPerson.FirstName = txtBoxName.Text;
            newPerson.LastName = txtBoxSurname.Text;
            newPerson.Adress = txtBoxAdress.Text;
            newPerson.VatNumber = int.Parse(txtBoxVatNumber.Text.ToString());
            newPerson.City = txtBoxCity.Text;
            newPerson.PostCode = int.Parse(txtBoxPostCode.Text.ToString());
            return newPerson;
        }
       /// <summary>
       /// function show person data
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            if(listBoxPeople.SelectedItem == null)
            {
                MessageBox.Show("Izbrali niste nobene osebe!");
            }

            else
            {
                Enable(false);
                btnDelete.IsEnabled = true;
                btnEdit.IsEnabled = true;
                int id = int.Parse(listBoxPeople.SelectedItem.ToString().Split(':')[0]);
                foreach (Person person in Service.FindAllPeople())
                {
                    if (person.Id == id)
                    {
                        txtBoxName.Text = person.FirstName;
                        txtBoxSurname.Text = person.LastName;
                        txtBoxAdress.Text = person.Adress;
                        txtBoxVatNumber.Text = person.VatNumber.ToString();
                        txtBoxCity.Text = person.City;
                        txtBoxPostCode.Text = person.PostCode.ToString();
                    }
                }
            }
            
        }

       

        /// <summary>
        /// option button add or edit person data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOptionButton_Click(object sender, RoutedEventArgs e)
        {
            
           if(btnOptionButton.Content == "Dodaj novo osebo!")
            {
                Person newPerson = new Person();
                
                if(txtBoxName.Text == "" ||
                    txtBoxSurname.Text == ""  ||
                    txtBoxAdress.Text == "" ||
                    txtBoxVatNumber.Text == "" ||
                    txtBoxPostCode.Text == ""  ||
                    txtBoxCity.Text == "")
                {
                    MessageBox.Show("Prosim, izpolnite vsa polja!");
                    Enable(true);
                }
               
               
                else if (Service.CheckVatNumber(int.Parse(txtBoxVatNumber.Text)) == false)
                {
                    MessageBox.Show("Neveljavna davčna številka!");
                    txtBoxVatNumber.Text = "";
                    txtBoxVatNumber.Focus();
                    
                }
                else if (Service.CheckAdress(txtBoxAdress.Text) == false)
                {
                    MessageBox.Show("Neveljavni naslov, vpišite hišno števliko!");
                    txtBoxAdress.Text = "";
                    txtBoxAdress.Focus();
                }
                else if (Service.CheckPostCode(int.Parse(txtBoxPostCode.Text)) == false)
                {
                    MessageBox.Show("Neveljavna poštna številka!");
                    txtBoxPostCode.Text = "";
                    txtBoxPostCode.Focus();
                }
                else
                {
                newPerson = GetData();
                Service.AddPerson(newPerson);
                MessageBox.Show($"Uspešno ste dodali osebo {newPerson.AllData}");
                ClearData();
                ListBoxData();
                btnOptionButton.Content = "";
                btnOptionButton.IsEnabled = false;
                txtBoxVatNumber.IsEnabled = true;
                Enable(false);
                }              
                
            }

           else if(btnOptionButton.Content == "Posodobi podatke!")
            {
                
                Person newPerson = new Person();
                if (txtBoxName.Text == "" ||
                    txtBoxSurname.Text == "" ||
                    txtBoxAdress.Text == "" ||
                    txtBoxVatNumber.Text == "" ||
                    txtBoxPostCode.Text == "" ||
                    txtBoxCity.Text == "")
                {
                    MessageBox.Show("Prosim, izpolnite vsa polja!");
                    Enable(true);
                }
                else if (Service.CheckAdress(txtBoxAdress.Text) == false)
                {
                    MessageBox.Show("Neveljavni naslov, vpišite hišno števliko!");
                    txtBoxAdress.Text = "";
                    txtBoxAdress.Focus();
                }
                else if (Service.CheckPostCode(int.Parse(txtBoxPostCode.Text)) == false)
                {
                    MessageBox.Show("Neveljavna poštna številka!");
                    txtBoxPostCode.Text = "";
                    txtBoxPostCode.Focus();
                }
                else
                {
                    newPerson = GetData();
                    Service.UpdatePerson(newPerson);
                    MessageBox.Show($"Uspešno ste posodobili podatke osebe {newPerson.AllData}");
                    ClearData();
                    ListBoxData();
                    btnOptionButton.Content = "";
                    btnOptionButton.IsEnabled = false;
                    txtBoxVatNumber.IsEnabled = true;
                    Enable(false);
                }  
            }
           
            
            
            
        }
        /// <summary>
        /// function add new person to data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Enable(true);
            btnOptionButton.Content = "Dodaj novo osebo!";

            ClearData();
        }

        /// <summary>
        /// function edit person data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxPeople.SelectedItem == null)
            {
                MessageBox.Show("Izbrali niste nobene osebe!");
            }
            else
            {
                Enable(true);
                txtBoxVatNumber.IsEnabled = false;
                btnOptionButton.Content = "Posodobi podatke!";
            }
            
        }

        /// <summary>
        /// function delete person from data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxPeople.SelectedItem == null)
            {
                MessageBox.Show("Izbrali niste nobene osebe!");
            }
            else
            {
                Person person = new Person();
                person = GetData();
                Service.DeletePerson(person.VatNumber);
                MessageBox.Show($"Uspešno ste odstranili osebo {person.AllData}!");
                ListBoxData();
                ClearData();
                Enable(false);
            }
            
        }


    }
}
