﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public abstract class CustomerModel : ObservableObject, IEquatable<CustomerModel>
    {
        #region Fields

        private int id;
        private string address;
        private string billingAddress;
        private string shippingAddress;
        private ObservableCollection<InvoiceModel> invoices;

        #endregion

        #region Properties

        [DataMember(Name = "id")]
        public int ID
        {
            get { return this.id; }
            set { base.Set<int>(ref this.id, value); }
        }

        [DataMember(Name = "address")]
        public string Address
        {
            get { return this.address; }
            set { base.Set<string>(ref this.address, value); }
        }

        [DataMember(Name = "billingAddress")]
        public string BillingAddress
        {
            get { return this.billingAddress; }
            set { base.Set<string>(ref this.billingAddress, value); }
        }

        [DataMember(Name = "shippingAddress")]
        public string ShippingAddress
        {
            get { return this.shippingAddress; }
            set { base.Set<string>(ref this.shippingAddress, value); }
        }

        [IgnoreDataMember]
        public ObservableCollection<InvoiceModel> Invoices
        {
            get { return this.invoices; }
            set { base.Set<ObservableCollection<InvoiceModel>>(ref this.invoices, value); }
        }

        #endregion

        #region Constructors

        public CustomerModel()
        {
            this.invoices = new ObservableCollection<InvoiceModel>();
        }

        public CustomerModel(int id, string address, string billingAddress, string shippingAddress) : this()
        {
            this.id = id;
            this.address = address;
            this.billingAddress = billingAddress;
            this.shippingAddress = shippingAddress;
        }

        #endregion

        #region IEquatable

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CustomerModel);
        }

        public virtual bool Equals(CustomerModel other)
        {
            return other.id == this.id
                && other.address == this.address
                && other.billingAddress == this.billingAddress
                && other.shippingAddress == this.shippingAddress;
        }

        #endregion
    }
}
