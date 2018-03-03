
using Microsoft.EntityFrameworkCore;

public class Person
{
    public string Id { get; set; }
    public string Name {get; set;}
}
public class SimpleContext : DbContext
{
    public DbSet<Person> People {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite("Data Source=test.db");
    }
}