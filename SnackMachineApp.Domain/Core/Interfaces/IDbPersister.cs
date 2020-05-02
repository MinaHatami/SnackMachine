﻿using SnackMachineApp.Domain.SeedWork;
using System.Collections.Generic;

namespace SnackMachineApp.Domain.Core.Interfaces
{
    public interface IDbPersister<T> where T : Entity
    {
        IList<T> List();

        T GetById(long id);

        void Save(T entity);

        void Delete(T entity);
    }
}