﻿using MicroERP.Business.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroERP.Business.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerModel> Create(CustomerModel customer);

        Task<IEnumerable<CustomerModel>> Read(string searchQuery, bool ordered = true);

        Task<CustomerModel> Read(int customerID);

        Task<CustomerModel> Update(CustomerModel customer);

        Task Delete(int customerID);
    }
}
