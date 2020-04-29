﻿using NHibernate.Event;
using SnackMachineApp.Logic.Core.Interfaces;
using SnackMachineApp.Logic.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace SnackMachineApp.Logic.Core
{
    internal class NHibernateDbEventListener :
        IPostInsertEventListener,
        IPostDeleteEventListener,
        IPostUpdateEventListener,
        IPostCollectionUpdateEventListener
    {
        public void OnPostUpdate(PostUpdateEvent ev)
        {
            DispatchEvents(ev.Entity as AggregateRoot);
        }

        public void OnPostDelete(PostDeleteEvent ev)
        {
            DispatchEvents(ev.Entity as AggregateRoot);
        }

        public void OnPostInsert(PostInsertEvent ev)
        {
            DispatchEvents(ev.Entity as AggregateRoot);
        }

        public void OnPostUpdateCollection(PostCollectionUpdateEvent ev)
        {
            DispatchEvents(ev.AffectedOwnerOrNull as AggregateRoot);
        }

        private void DispatchEvents(AggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                return;

            var eventDispatcher = ObjectFactory.Instance.Resolve<IDomainEventDispatcher>();

            foreach (var domainEvent in aggregateRoot.DomainEvents)
            {
                eventDispatcher.Dispatch(domainEvent);
            }

            aggregateRoot.ClearEvents();
        }
        
        #region Async
        public Task OnPostInsertAsync(PostInsertEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task OnPostUpdateAsync(PostUpdateEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task OnPostUpdateCollectionAsync(PostCollectionUpdateEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task OnPostDeleteAsync(PostDeleteEvent ev, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        #endregion        
    }
}