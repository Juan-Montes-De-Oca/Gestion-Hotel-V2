using System;

public class Room
{
    public int Number { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; }
    public string Details { get; set; }

    public Room(int number, string type, decimal price, string status, string details)
    {
        Number = number;
        Type = type;
        Price = price;
        Status = status;
        Details = details;
    }

    public void ShowDetails()
    {
        Console.WriteLine($"N.º: {Number} | Tipo: {Type} | Precio: ${Price:F2} | Estado: {Status} | Detalle: {Details}");
    }
}