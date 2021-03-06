﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MicroERP.Business.Domain.Enums;
using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;

namespace MicroERP.Business.Domain.Repositories
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Create a new customer - either a person or a company.
        /// </summary>
        /// <param name="customer">CustomerModel unequal to null.</param>
        /// <returns>The newly created customer ID.</returns>
        Task<int> Create(CustomerModel customer);

        /// <summary>
        /// Retrieve a single customer by its ID.
        /// </summary>
        /// <param name="customerID">customerID must be unequal to zero.</param>
        /// <returns>The customer.</returns>
        /// <exception cref="CustomerNotFoundException"></exception>
        Task<CustomerModel> Find(int customerID);

        /// <summary>
        /// Update a single customer.
        /// </summary>
        /// <param name="customer">An altered CustomerModel with a valid ID.</param>
        /// <returns>The updated customer.</returns>
        /// <exception cref="CustomerNotFoundException"></exception>
        Task<CustomerModel> Update(CustomerModel customer);

        /// <summary>
        /// Delete a single customer by its ID.
        /// </summary>
        /// <param name="customerID">customerID must be unequal to zero.</param>
        /// <returns></returns>
        /// <exception cref="CustomerNotFoundException"></exception>
        Task Delete(int customerID);

        /// <summary>
        /// Search for a customer by firstname, lastname or company name.
        /// </summary>
        /// <param name="searchQuery">Search string.</param>
        /// <param name="customerType">Specify the customerType</param>
        /// <returns>List of customers.</returns>
        Task<IEnumerable<CustomerModel>> Search(string searchQuery, CustomerType customerType = CustomerType.None);
    }
}