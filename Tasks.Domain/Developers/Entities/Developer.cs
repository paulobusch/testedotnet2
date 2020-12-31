﻿using System;
using System.Collections.Generic;
using Tasks.Domain._Common.Crypto;
using Tasks.Domain._Common.Entities;

namespace Tasks.Domain.Developers.Entities
{
    public class Developer : EntityBase
    {
        public string Name { get; private set; }
        public string Login { get; private set; }
        public string CPF { get; private set; }
        public string PasswordHash { get; private set; }

        public virtual IEnumerable<DeveloperProject> DeveloperProjects { get; private set; }

        protected Developer() : base() { }

        public Developer(
            Guid id,
            string name,
            string login,
            string cpf,
            string password
        ) : base(id)
        {
            this.PasswordHash = MD5Crypto.Encode(password);
            SetData(
                name: name,
                login: login,
                cpf: cpf
            );
        }

        public void SetData(
            string name,
            string login,
            string cpf
        )
        {
            this.Name = name;
            this.Login = login;
            this.CPF = cpf;
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            var hash = MD5Crypto.Encode(password);
            return hash.Equals(PasswordHash);
        }
    }
}