using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000767 RID: 1895
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Name is intentional")]
	internal class DbLocalView<TEntity> : ObservableCollection<TEntity>, ICollection<!0>, IEnumerable<!0>, IList, ICollection, IEnumerable where TEntity : class
	{
		// Token: 0x06005572 RID: 21874 RVA: 0x00173C76 File Offset: 0x00171E76
		public DbLocalView()
		{
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x00173C7E File Offset: 0x00171E7E
		public DbLocalView(IEnumerable<TEntity> collection)
		{
			Check.NotNull<IEnumerable<TEntity>>(collection, "collection");
			collection.Each(new Action<TEntity>(base.Add));
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x00173CA4 File Offset: 0x00171EA4
		internal DbLocalView(InternalContext internalContext)
		{
			this._internalContext = internalContext;
			try
			{
				this._inStateManagerChanged = true;
				foreach (TEntity item in this._internalContext.GetLocalEntities<TEntity>())
				{
					base.Add(item);
				}
			}
			finally
			{
				this._inStateManagerChanged = false;
			}
			this._internalContext.RegisterObjectStateManagerChangedEvent(new CollectionChangeEventHandler(this.StateManagerChangedHandler));
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06005575 RID: 21877 RVA: 0x00173D38 File Offset: 0x00171F38
		internal ObservableBackedBindingList<TEntity> BindingList
		{
			get
			{
				ObservableBackedBindingList<TEntity> result;
				if ((result = this._bindingList) == null)
				{
					result = (this._bindingList = new ObservableBackedBindingList<TEntity>(this));
				}
				return result;
			}
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x00173D60 File Offset: 0x00171F60
		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (!this._inStateManagerChanged && this._internalContext != null)
			{
				if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
				{
					foreach (object obj in e.OldItems)
					{
						TEntity entity = (TEntity)((object)obj);
						this._internalContext.Set<TEntity>().Remove(entity);
					}
				}
				if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
				{
					foreach (object obj2 in e.NewItems)
					{
						TEntity tentity = (TEntity)((object)obj2);
						if (!this._internalContext.EntityInContextAndNotDeleted(tentity))
						{
							this._internalContext.Set<TEntity>().Add(tentity);
						}
					}
				}
			}
			base.OnCollectionChanged(e);
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x00173E74 File Offset: 0x00172074
		private void StateManagerChangedHandler(object sender, CollectionChangeEventArgs e)
		{
			try
			{
				this._inStateManagerChanged = true;
				TEntity tentity = e.Element as TEntity;
				if (tentity != null)
				{
					if (e.Action == CollectionChangeAction.Remove && this.Contains(tentity))
					{
						this.Remove(tentity);
					}
					else if (e.Action == CollectionChangeAction.Add && !this.Contains(tentity))
					{
						base.Add(tentity);
					}
				}
			}
			finally
			{
				this._inStateManagerChanged = false;
			}
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x00173EF9 File Offset: 0x001720F9
		protected override void ClearItems()
		{
			new List<TEntity>(this).Each((TEntity t) => this.Remove(t));
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x00173F12 File Offset: 0x00172112
		protected override void InsertItem(int index, TEntity item)
		{
			if (!this.Contains(item))
			{
				base.InsertItem(index, item);
			}
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x00173F28 File Offset: 0x00172128
		public new virtual bool Contains(TEntity item)
		{
			IEqualityComparer<TEntity> @default = ObjectReferenceEqualityComparer.Default;
			foreach (TEntity x in base.Items)
			{
				if (@default.Equals(x, item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x00173F88 File Offset: 0x00172188
		public new virtual bool Remove(TEntity item)
		{
			IEqualityComparer<TEntity> @default = ObjectReferenceEqualityComparer.Default;
			int num = 0;
			while (num < base.Count && !@default.Equals(base.Items[num], item))
			{
				num++;
			}
			if (num == base.Count)
			{
				return false;
			}
			this.RemoveItem(num);
			return true;
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x00173FD4 File Offset: 0x001721D4
		bool ICollection<!0>.Contains(TEntity item)
		{
			return this.Contains(item);
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x00173FDD File Offset: 0x001721DD
		bool ICollection<!0>.Remove(TEntity item)
		{
			return this.Remove(item);
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x00173FE6 File Offset: 0x001721E6
		bool IList.Contains(object value)
		{
			return DbLocalView<TEntity>.IsCompatibleObject(value) && this.Contains((TEntity)((object)value));
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x00173FFE File Offset: 0x001721FE
		void IList.Remove(object value)
		{
			if (DbLocalView<TEntity>.IsCompatibleObject(value))
			{
				this.Remove((TEntity)((object)value));
			}
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x00174015 File Offset: 0x00172215
		private static bool IsCompatibleObject(object value)
		{
			return value is TEntity || value == null;
		}

		// Token: 0x040022BB RID: 8891
		private readonly InternalContext _internalContext;

		// Token: 0x040022BC RID: 8892
		private bool _inStateManagerChanged;

		// Token: 0x040022BD RID: 8893
		private ObservableBackedBindingList<TEntity> _bindingList;
	}
}
