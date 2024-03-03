using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;

namespace PP;

public partial class AddEd : Window
{
    private List<workshop> Orders;
    private workshop CurrenOrder;
    public AddEd(workshop currentOrder, List<workshop> orders)
    {
        InitializeComponent();
        CurrenOrder = currentOrder;
        this.DataContext = currentOrder;
        Orders = orders;
    }
    
    private MySqlConnection conn;
    string connStr = "server=localhost;database=pp-gg;port=3306;User Id=root;password=Pass&_word1";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Orders.FirstOrDefault(x => x.ID == CurrenOrder.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO workshop VALUES (" + Convert.ToInt32(id.Text) + ", '" + eq_id.Text + "', '" + to_id.Text + "', '" + em_id.Text + "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE workshop SET equipment = '" + eq_id.Text + "', tool = '" +  to_id.Text + "',  employee = '" + em_id.Text +"' WHERE ID = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Table back = new Table();
        this.Close();
        back.Show(); 
    }
}