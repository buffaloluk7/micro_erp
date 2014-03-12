﻿using System;
using System.Threading.Tasks;
using MicroERP.Business.DataAccessLayer.Interfaces;
using System.Collections.Generic;
using MicroERP.Business.Models;
using MicroERP.Business.DataAccessLayer.ESC.Extensions;
using Newtonsoft.Json;
using MicroERP.Business.DataAccessLayer.ESC.Exceptions;
using System.Net;
using MicroERP.Business.DataAccessLayer.Exceptions;

namespace MicroERP.Business.DataAccessLayer.ESC
{
    public class ESCDataAccessLayer : IDataAccessLayer
    {
        private const string baseURL = "http://localhost:8000/api/customers/";

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            var response = await RESTRequest.Post(baseURL, customer);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                try
                {
                    return await response.Content.ReadAsObjectAsync<Customer>();
                }
                catch (JsonReaderException e)
                {
                    throw new FaultyMessageException(inner: e);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new CustomerAlreadyExistsException(customer);
            }

            throw new BadResponseException(response.StatusCode);
        }

        public async Task<IEnumerable<Customer>> ReadCustomers(string searchQuery)
        {
            string url = string.Format("{0}?q={1}", baseURL, searchQuery);
            var response = await RESTRequest.Get(url);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadAsObjectAsync<IEnumerable<Customer>>();
                }
                catch (JsonReaderException e)
                {
                    throw new FaultyMessageException(inner: e);
                }
            }

            throw new BadResponseException(response.StatusCode);
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var response = await RESTRequest.Put(baseURL + customer.ID);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return customer;
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    return await response.Content.ReadAsObjectAsync<Customer>();
                }
                catch (JsonReaderException e)
                {
                    throw new FaultyMessageException(inner: e);
                }
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomerNotFoundException();
            }

            throw new BadResponseException(response.StatusCode);
        }

        public async Task DeleteCustomer(int customerID)
        {
            var response = await RESTRequest.Delete(baseURL + customerID);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomerNotFoundException();
            }
            else if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new BadResponseException(response.StatusCode);
            }
        }
    }
}
