using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Attributes;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repositories.AdminModule;

namespace TechShopCFAPI.Controllers.Admin
{
    public class CustomerListController : ApiController
    {
        CustomerRepository custRepo = new CustomerRepository();
        CredentialRepository credentialRepository = new CredentialRepository();

        [Route("api/customers/active"), BasicAuthentication]
        public IHttpActionResult Get()
        {
            return Ok(custRepo.GetActiveCustomers());
        }

        [Route("api/customers/active/name"), BasicAuthentication]
        public IHttpActionResult GetActiveByName(string name)
        {
            return Ok(custRepo.GetActiveByName(name));
        }

        [Route("api/customers/restricted/name"), BasicAuthentication]
        public IHttpActionResult GetRestrictedByName(string name)
        {
            return Ok(custRepo.GetRestrictedByName(name));
        }

        [Route("api/customers/{id}"), BasicAuthentication]
        public IHttpActionResult Get(int id)
        {
            Models.Customer cust = custRepo.Get(id);
            if (cust == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(cust);
        }

        [Route("api/customers/Block/{id}"), BasicAuthentication]
        public IHttpActionResult PutBlockCustomer(int customerId)
        {
            var cust = custRepo.Get(customerId);
            custRepo.BlockCustomer(cust.Email);

            credentialRepository.Restrict(cust.Email);
            return Ok();

        }

        [Route("api/customers/restricted"), BasicAuthentication]
        public IHttpActionResult GetRestricted()
        {
            return Ok(custRepo.GetRestrictedCustomers());
        }

        [Route("api/customers/history/{id}"), BasicAuthentication]
        public IHttpActionResult GetHistory(int id)
        {
            return Ok(custRepo.History(id));
        }


        [Route("api/customers/ReactiveCustomer/{id}"), BasicAuthentication]
        public IHttpActionResult PutReactivateCustomer(int customerId)
        {
            var cust = custRepo.GetCust(customerId);
            custRepo.ReactivateCustomer(cust.Email);
            credentialRepository.Reactive(cust.Email);
            return Ok();
        }

        [Route("api/customers/Review/{id}"), BasicAuthentication]
        public IHttpActionResult GetReview(int id)
        {
            return Ok(custRepo.CustomerReview(id));
        }
    }
}