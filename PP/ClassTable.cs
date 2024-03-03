namespace PP;

public class tool   
{
    public int Id { get; set; }
    public string name_tool { get; set; }
    public int type_tool { get; set; }
    public int count { get; set; }

}

public class employee 
{
    public int Id { get; set; }
    public string FIO { get; set; }
    public int post { get; set; }
}

public class workshop 
{
    public int ID { get; set;}
    public int equipment { get; set; }
    public int tool { get; set; }
    public string FIO { get; set; }
    
}