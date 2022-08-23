using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Testing.Data;
using Testing.Models;

namespace Testing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContextRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = addContextRequest.FullName,
                Email = addContextRequest.Email,
                Address = addContextRequest.Address,
                Phone = addContextRequest.Phone
            };
            await dbContext.Contacts.AddAsync(contact);

            dbContext.SaveChangesAsync();

            return Ok(contact);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContextRequest)
        {
            var  contact =await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.FullName = updateContextRequest.FullName;
                contact.Email = updateContextRequest.Email;
                contact.Address = updateContextRequest.Address;
                contact.Phone = updateContextRequest.Phone;
                await dbContext.SaveChangesAsync();

                return Ok(contact);

            }
            return NotFound();
        }
      //  [HttpDelete]
      //  [Route("{id:guid}")]



    }
}
