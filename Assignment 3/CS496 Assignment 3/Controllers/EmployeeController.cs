using CS496_Assignment_3.Models;
using CS496_Assignment_3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CS496_Assignment_3.Controllers
{
    public class EmployeeController : ApiController
    {
        private EmployeeRepository contactRepository;

        public EmployeeController()
        {
            this.contactRepository = new EmployeeRepository();
        }

        public Employee[] Get()
        {
            return this.contactRepository.GetAllContacts();
        }

        public HttpResponseMessage Post(Employee contact)
        {
            this.contactRepository.SaveContact(contact);

            var response = Request.CreateResponse<Employee>(System.Net.HttpStatusCode.Created, contact);

            return response;
        }
    }
}
