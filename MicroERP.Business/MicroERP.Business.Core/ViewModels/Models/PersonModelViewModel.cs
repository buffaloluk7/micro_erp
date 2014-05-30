﻿using GalaSoft.MvvmLight;
using MicroERP.Business.Domain.Models;
using System;

namespace MicroERP.Business.Core.ViewModels.Models
{
    public class PersonModelViewModel : ObservableObject
    {
        #region Fields

        private readonly PersonModel person;

        #endregion

        #region Properties

        public string Title
        {
            get { return this.person.Title; }
            set { this.person.Title = value; }
        }

        public string FirstName
        {
            get { return this.person.FirstName; }
            set { this.person.FirstName = value; }
        }

        public string LastName
        {
            get { return this.person.LastName; }
            set { this.person.LastName = value; }
        }

        public string Suffix
        {
            get { return this.person.Suffix; }
            set { this.person.Suffix = value; }
        }

        public DateTime? BirthDate
        {
            get { return this.person.BirthDate; }
            set { this.person.BirthDate = value; }
        }

        // Currently not in use, but maybe one day. :-)
        public CompanyModelViewModel Company
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public PersonModelViewModel(PersonModel person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            this.person = person;
            this.person.PropertyChanged += person_PropertyChanged;

            this.Company = new CompanyModelViewModel(this.person.Company ?? new CompanyModel());
            this.RaisePropertyChanged(() => this.Company);
        }

        #endregion

        #region PropertyChanged

        private void person_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Title":
                case "FirstName":
                case "LastName":
                case "Suffix":
                case "BirthDate":
                    base.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }

        #endregion
    }
}