using System;
using System.Collections.Generic;

class Medicine
{
    public string Name { get; }
    public decimal Price { get; }
    public int Count { get; private set; }

    public Medicine(string name, decimal price, int count)
    {
        if (string.IsNullOrWhiteSpace(name) || price <= 0 || count < 0)
            throw new ArgumentException("Duzgun deyil melumatlar.");

        Name = name;
        Price = price;
        Count = count;
    }

    public void Sell(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Satış miqdari 0'dan nboyuk olmalıdır.");
        if (quantity > Count)
            throw new InvalidOperationException("Stokta yeterli derman yox.");

        Count -= quantity;
    }
}

class Pharmacy
{
    private List<Medicine> Medicines { get; }
    public decimal TotalIncome { get; private set; }

    public Pharmacy()
    {
        Medicines = new List<Medicine>();
    }

    public Medicine FindMedicineByName(string name)
    {
        return Medicines.Find(medicine => medicine.Name == name);
    }

    public void AddMedicine(Medicine medicine)
    {
        if (FindMedicineByName(medicine.Name) != null)
            throw new InvalidOperationException("Eyni adda derman var.");

        Medicines.Add(medicine);
    }

    public void Sell(string name, int quantity)
    {
        Medicine medicine = FindMedicineByName(name);

        if (medicine == null)
            throw new ArgumentException("bu adda derman tapilmadi");

        medicine.Sell(quantity);
        TotalIncome += medicine.Price * quantity;
    }
}

class Program
{
    static void Main()
    {
        Pharmacy pharmacy = new Pharmacy();

        Medicine medicine1 = new Medicine("Parol", 10.0m, 100);
        Medicine medicine2 = new Medicine("Aspirin", 5.0m, 50);

        pharmacy.AddMedicine(medicine1);
        pharmacy.AddMedicine(medicine2);

        pharmacy.Sell("Parol", 20);
        pharmacy.Sell("Aspirin", 10);

        Console.WriteLine("Toplam Gelir: " + pharmacy.TotalIncome);
    }
}

