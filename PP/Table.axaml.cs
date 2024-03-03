using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;

namespace PP;

public partial class Table : Window
{
    public Table()
    {
        InitializeComponent();
        ShowTable(fullTable);
        FillStatus();
    }

    string fullTable =
        "SELECT workshop.ID, workshop.equipment, workshop.tool, employee.FIO FROM workshop JOIN employee on workshop.employee = employee.Id ";

    private List<workshop> orders;
    private List<tool> stats;
    private List<employee> clin;
    string connStr = "server=localhost;database=pp-gg;port=3306;User Id=root;password=Pass&_word1";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        orders = new List<workshop>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var ord = new workshop()
            {
                ID = reader.GetInt32("ID"),
                equipment = reader.GetInt32("equipment"),
                tool = reader.GetInt32("tool"),
                FIO = reader.GetString("FIO"),

            };
            orders.Add(ord);
        }

        conn.Close();
        DataGrid.ItemsSource = orders;
    }

    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = orders;
        gds = gds.Where(x => x.FIO.Contains((Search_Goods.Text))).ToList();
        DataGrid.ItemsSource = gds;
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable(fullTable);
        Search_Goods.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            workshop usr = DataGrid.SelectedItem as workshop;
            if (usr == null)
            {
                return;
            }

            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM workshop WHERE ID = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            orders.Remove(usr);
            ShowTable(fullTable);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        workshop ort = new workshop();
        AddEd add = new AddEd(ort, orders);
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        workshop currenOrder = DataGrid.SelectedItem as workshop;
        if (currenOrder == null)
            return;
        AddEd edit = new AddEd(currenOrder, orders);
        edit.Show();
        this.Close();
    }

    private void CmbStatus(object? sender, SelectionChangedEventArgs e)
    {
        var genderComboBox = (ComboBox)sender;
        var currentGender = genderComboBox.SelectedItem as employee;
        var filteredUsers = orders
            .Where(x => x.FIO == currentGender.FIO)
            .ToList();
        DataGrid.ItemsSource = filteredUsers;
    }

    public void FillStatus()
    {
        clin = new List<employee>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from employee", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGender = new employee()
            {
                Id = reader.GetInt32("Id"),
                FIO = reader.GetString("FIO"),
                post = reader.GetInt32("post")

            };
            clin.Add(currentGender);
        }

        conn.Close();
        var genderComboBox = this.Find<ComboBox>("CmbGender");
        genderComboBox.ItemsSource = clin;
    }
}