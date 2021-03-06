﻿using System;
using System.Runtime.Serialization;

namespace MicroERP.Business.Domain.Models
{
    [DataContract]
    public class PersonModel : CustomerModel, IEquatable<PersonModel>
    {
        #region Fields

        private string title;
        private string firstName;
        private string lastName;
        private string suffix;
        private DateTime? birthDate;
        private CompanyModel company;
        private int? companyID;

        #endregion

        #region Properties

        [DataMember(Name = "title")]
        public string Title
        {
            get { return this.title; }
            set { base.Set(ref this.title, value); }
        }

        [DataMember(Name = "firstName")]
        public string FirstName
        {
            get { return this.firstName; }
            set { base.Set(ref this.firstName, value); }
        }

        [DataMember(Name = "lastName")]
        public string LastName
        {
            get { return this.lastName; }
            set { base.Set(ref this.lastName, value); }
        }

        [DataMember(Name = "suffix")]
        public string Suffix
        {
            get { return this.suffix; }
            set { base.Set(ref this.suffix, value); }
        }

        [DataMember(Name = "birthDate")]
        public DateTime? BirthDate
        {
            get { return this.birthDate; }
            set { base.Set(ref this.birthDate, value); }
        }

        [DataMember(Name = "companyID")]
        public int? CompanyID
        {
            get { return this.companyID; }
            set { base.Set(ref this.companyID, value); }
        }

        [IgnoreDataMember]
        public CompanyModel Company
        {
            get { return this.company; }
            set
            {
                base.Set(ref this.company, value);
                this.CompanyID = (value == null) ? default(int?) : value.ID;
            }
        }

        #endregion

        #region Constructors

        public PersonModel()
        {
        }

        public PersonModel(int id, string address, string billingAddress, string shippingAddress, string title,
            string firstName, string lastName, string suffix, DateTime? birthDate, CompanyModel company = null)
            : base(id, address, billingAddress, shippingAddress)
        {
            this.title = title;
            this.firstName = firstName;
            this.lastName = lastName;
            this.suffix = suffix;
            this.birthDate = birthDate;
            this.Company = company;
        }

        #endregion

        #region IEquatable

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PersonModel);
        }

        public bool Equals(PersonModel other)
        {
            return other != null &&
                   base.Equals(other) &&
                   other.title == this.title &&
                   other.firstName == this.firstName &&
                   other.lastName == this.lastName &&
                   other.suffix == this.suffix &&
                   other.birthDate == this.birthDate &&
                   Equals(this.company, other.company);
        }

        #endregion
    }
}