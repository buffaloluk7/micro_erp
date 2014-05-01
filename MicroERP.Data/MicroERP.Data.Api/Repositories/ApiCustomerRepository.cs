﻿using Luvi.Http;
using Luvi.Http.Extension;
using Luvi.Json.Converter;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using MicroERP.Data.Api.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MicroERP.Data.Api.Repositories
{
    public class ApiCustomerRepository : ICustomerRepository
    {
        #region Properties

        private const string baseURL = "http://10.201.94.236:8000/microerp/customer";
        private RESTRequest request;

        #endregion

        #region Constructors
        
        public ApiCustomerRepository()
        {
            this.request = new RESTRequest()
            {
                Timeout = new TimeSpan(0, 0, 15),
                UseTransferEncodingChunked = true
            };
        }

        #endregion

        #region ICustomerRepository

        public async Task<CustomerModel> Create(CustomerModel customer)
        {
            var response = await this.request.Post(ApiCustomerRepository.baseURL, customer);

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<CustomerModel>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                case HttpStatusCode.Conflict:
                    throw new CustomerAlreadyExistsException(customer);

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        public async Task<IEnumerable<CustomerModel>> Read(string searchQuery)
        {
            string url = string.Format("{0}?q={1}", ApiCustomerRepository.baseURL, searchQuery);
            var response = await this.request.Get(url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Dictionary<string, Type> knownTypes = new Dictionary<string, Type>();
                knownTypes.Add("Person", typeof(PersonModel));
                knownTypes.Add("Company", typeof(CompanyModel));

                var jsonKnownTypeConverter = new JsonKnownTypeConverter<CustomerModel>(knownTypes);
                var jsonSerializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                jsonSerializerSettings.Converters.Add(jsonKnownTypeConverter);

                return await response.Content.ReadAsObjectAsync<IEnumerable<CustomerModel>>(jsonSerializerSettings);
            }

            throw new BadResponseException(response.StatusCode);
        }

        public async Task<CustomerModel> Read(int customerID)
        {
            string url = string.Format("{0}/{1}", baseURL, customerID);
            var response = await this.request.Get(url);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<CustomerModel>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                case HttpStatusCode.NotFound:
                    throw new CustomerNotFoundException();

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        public async Task<CustomerModel> Update(CustomerModel customer)
        {
            var response = await this.request.Put(baseURL + customer.ID, customer);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return customer;

                case HttpStatusCode.OK:
                    try
                    {
                        return await response.Content.ReadAsObjectAsync<CustomerModel>();
                    }
                    catch (JsonReaderException e)
                    {
                        throw new FaultyMessageException(inner: e);
                    }

                case HttpStatusCode.NotFound:
                    throw new CustomerNotFoundException();

                default:
                    throw new BadResponseException(response.StatusCode);
            }            
        }

        public async Task Delete(int customerID)
        {
            var response = await this.request.Delete(baseURL + customerID);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;

                case HttpStatusCode.NotFound:
                    throw new CustomerNotFoundException();

                default:
                    throw new BadResponseException(response.StatusCode);
            }
        }

        #endregion
    }
}