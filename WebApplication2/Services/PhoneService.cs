using WebApplication2.Models;

public class PhoneService
{
    public IEnumerable<Phone> GetAllPhones()
    {
        return StoreWarehouse.Phones;
    }
    public Phone GetPhoneById(int id)
    {
        return StoreWarehouse.Phones.Find(x => x.Id == id);
    }

    public void DeletePhoneById(int id)
    {
        Phone deletePhone = StoreWarehouse.Phones.Find(x => x.Id == id);
        StoreWarehouse.Phones.Remove(deletePhone);
    }

    public void AddPhone(Phone p)
    {
        StoreWarehouse.Phones.Add(p);
    }

    public List<Phone> GetPhoneByprice(int price)
    {
        return StoreWarehouse.Phones.Where(x => x.Price < price).ToList();
    }

    public Phone GetPhoneByName(string name)
    {
        return StoreWarehouse.Phones.FirstOrDefault(x => x.Name == name);
    }
}