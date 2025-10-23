using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhoneController : ControllerBase
{
    private readonly PhoneService _phoneService;

    public PhoneController(PhoneService phoneService)
    {
        _phoneService = phoneService;
    }

    [HttpGet("getValue")]
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    [HttpGet("getPhone/{id}")]
    public Phone GetPhone([FromRoute] int id)
    {
        Phone phone = StoreWarehouse.Phones.Find(x => x.Id == id);
        return phone;
    }

    [HttpGet("getPhone")]
    public ActionResult<Phone> GetPhone2([FromQuery] int id)
    {
        Phone phone = StoreWarehouse.Phones.Find(x => x.Id == id);

        if (phone is null)
            return NotFound();
        
        return Ok(phone);
    }

    [HttpGet("getPhoneSearch")]
    public IEnumerable<Phone> getPhoneSearch([FromQuery] string? name = null, [FromQuery] int top = 10, [FromQuery] int? skip = null)
    {
        IEnumerable<Phone> phones = StoreWarehouse.Phones;
        if (name is not null)
        {
            phones = phones.Where(p => p.Name.Contains(name));
        }

        return phones.ToList().Take(top).Skip(skip.Value);
    }

    /// <summary>
    /// Ottieni un elenco di telefoni
    /// </summary>
    /// <response code="200">Elenco degli telefoni restituito con successo.</response>
    /// <response code="401">No Authentication.</response>
    [HttpGet("getAllPhones")]
    [ProducesResponseType(200, StatusCode = 200, Type = typeof(IEnumerable<Phone>))]
    [ProducesResponseType(401)]
    public List<Phone> GetAllPhones()
    {
        return StoreWarehouse.Phones;
    }

    [HttpGet("getPhones/service/{id}")]
    public Phone GetPhoneWithService(int id, [FromServices] PhoneService service)
    {
        //errore nel codice... ATTENZIONE
        id = 1;
        return service.GetPhoneById(id);
    }

    [HttpGet("getPhonesWithError/{id}")]
    public Phone GetPhoneWithErrorService(int? id, [FromServices] PhoneService service)
    {
        id = null;
        return service.GetPhoneById(id.Value);
    }

    [HttpGet("getPhoneByPrice")]
    public ActionResult<IEnumerable<Phone>> GetPhoneByPrice([FromQuery] int price)
    {
        try
        {
            return Ok(StoreWarehouse.Phones.Where(x => x.Price < price).ToList());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("deletePhone/{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        //ATTENZIONE: torna sempre lo stesso status code !!
        Phone deletePhone = StoreWarehouse.Phones.Find(x => x.Id == id);

        if(deletePhone is null)
            return StatusCode(204);

        return Ok(StoreWarehouse.Phones.Remove(deletePhone));
    }

    [HttpPost("addPhone")]
    [ProducesResponseType(200, StatusCode = 200, Type = typeof(IEnumerable<Phone>))]
    [ProducesResponseType(400, StatusCode = 400)]
    [ProducesResponseType(500, StatusCode = 500)]
    public ActionResult AddPhone([FromBody] Phone p)
    {
        try
        {
            StoreWarehouse.Phones.Add(p);

            return Ok(p);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }

    [HttpPut("updatePhone/{id}")]
    public ActionResult UpdatePhone([FromRoute] int id, [FromBody] PhoneWithoutId p)
    {
        try
        {
            Phone ph = StoreWarehouse.Phones.FirstOrDefault(x => x.Id == id);

            if (ph != null)
            {
                ph.Name = p.Name;
                ph.Description = p.Description;
                ph.Colour= p.Colour;
                ph.Price = p.Price;

                return Ok();
            }

            return StatusCode(204);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }
}
