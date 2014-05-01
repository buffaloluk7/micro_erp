﻿using MicroERP.Business.Domain.Exceptions;
using MicroERP.Business.Domain.Models;
using MicroERP.Business.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MicroERP.Data.Fake.Repositories
{
    public class FakeInvoiceRepository : IInvoiceRepository
    {
        #region Fields

        private readonly List<InvoiceModel> invoices;

        #endregion

        #region Constructors

        public FakeInvoiceRepository()
        {
            var ii1 = new ObservableCollection<InvoiceItemModel>(new InvoiceItemModel[]
            {
                new InvoiceItemModel(1, 100, 10.2, 2.3),
                new InvoiceItemModel(2, 120, 5.4, 2.7)
            });

            var ii2 = new ObservableCollection<InvoiceItemModel>(new InvoiceItemModel[]
            {
                new InvoiceItemModel(3, 70, 14.2, 6.3),
                new InvoiceItemModel(4, 132, 1.4, 8.7)
            });

            this.invoices = new List<InvoiceModel>(new InvoiceModel[] {
                new InvoiceModel(1, DateTime.Now, DateTime.Now, 1234, "Test-Kommentar", "Test-Message"),
                new InvoiceModel(2, DateTime.Now, DateTime.Now, 1235, "Test-Kommentar", "Test-Message")
            });

            this.invoices[0].CustomerID = 1;
            this.invoices[1].CustomerID = 2;
            this.invoices[0].InvoiceItems = ii1;
            this.invoices[1].InvoiceItems = ii2;
        }

        #endregion

        #region IInvoiceRepository

        public async Task<InvoiceModel> Create(InvoiceModel invoice)
        {
            return await Task.Run(() =>
            {
                if (this.invoices.Any(i => i.ID == invoice.ID))
                {
                    throw new InvoiceAlreadyExistsException(invoice);
                }

                invoice.ID = this.invoices.Max(i => i.ID) + 1;
                this.invoices.Add(invoice);
                return invoice;
            });
        }

        public async Task<InvoiceModel> Read(int invoiceID)
        {
            return await Task.Run(() =>
            {
                var invoice = this.invoices.FirstOrDefault<InvoiceModel>(i => i.ID == invoiceID);

                if (invoice != null)
                {
                    return invoice;
                }

                throw new InvoiceNotFoundException();
            });
        }

        #endregion
    }
}