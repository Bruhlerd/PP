using Avalonia.Controls;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
namespace PP;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    string connectionString = "server=localhost; database=pp-gg; port=3306; User Id=root;password=Pass&_word1";

    public void Vxod(object? sender, RoutedEventArgs e)
    {
        if (Login.Text == "user" && Password.Text == "pass")
        {
            Table tbl = new Table();
            this.Hide();
            tbl.Show();
        }
    }
    
    
    public void Exit_PR(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}