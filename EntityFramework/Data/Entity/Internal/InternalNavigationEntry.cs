using System;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000776 RID: 1910
	internal abstract class InternalNavigationEntry : InternalMemberEntry
	{
		// Token: 0x0600567F RID: 22143 RVA: 0x001765F9 File Offset: 0x001747F9
		protected InternalNavigationEntry(InternalEntityEntry internalEntityEntry, NavigationEntryMetadata navigationMetadata) : base(internalEntityEntry, navigationMetadata)
		{
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x00176603 File Offset: 0x00174803
		public virtual void Load()
		{
			this.ValidateNotDetached("Load");
			this._relatedEnd.Load();
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x0017661B File Offset: 0x0017481B
		public virtual Task LoadAsync(CancellationToken cancellationToken)
		{
			this.ValidateNotDetached("LoadAsync");
			return this._relatedEnd.LoadAsync(cancellationToken);
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06005682 RID: 22146 RVA: 0x00176634 File Offset: 0x00174834
		// (set) Token: 0x06005683 RID: 22147 RVA: 0x0017664C File Offset: 0x0017484C
		public virtual bool IsLoaded
		{
			get
			{
				this.ValidateNotDetached("IsLoaded");
				return this._relatedEnd.IsLoaded;
			}
			set
			{
				this.ValidateNotDetached("IsLoaded");
				this._relatedEnd.IsLoaded = value;
			}
		}

		// Token: 0x06005684 RID: 22148 RVA: 0x00176665 File Offset: 0x00174865
		public virtual IQueryable Query()
		{
			this.ValidateNotDetached("Query");
			return (IQueryable)this._relatedEnd.CreateSourceQuery();
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06005685 RID: 22149 RVA: 0x00176682 File Offset: 0x00174882
		protected IRelatedEnd RelatedEnd
		{
			get
			{
				if (this._relatedEnd == null && !this.InternalEntityEntry.IsDetached)
				{
					this._relatedEnd = this.InternalEntityEntry.GetRelatedEnd(this.Name);
				}
				return this._relatedEnd;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06005686 RID: 22150 RVA: 0x001766B6 File Offset: 0x001748B6
		public override object CurrentValue
		{
			get
			{
				if (this.Getter == null)
				{
					this.ValidateNotDetached("CurrentValue");
					return this.GetNavigationPropertyFromRelatedEnd(this.InternalEntityEntry.Entity);
				}
				return this.Getter(this.InternalEntityEntry.Entity);
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06005687 RID: 22151 RVA: 0x001766F3 File Offset: 0x001748F3
		protected Func<object, object> Getter
		{
			get
			{
				if (!this._triedToGetGetter)
				{
					DbHelpers.GetPropertyGetters(this.InternalEntityEntry.EntityType).TryGetValue(this.Name, out this._getter);
					this._triedToGetGetter = true;
				}
				return this._getter;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06005688 RID: 22152 RVA: 0x0017672C File Offset: 0x0017492C
		protected Action<object, object> Setter
		{
			get
			{
				if (!this._triedToGetSetter)
				{
					DbHelpers.GetPropertySetters(this.InternalEntityEntry.EntityType).TryGetValue(this.Name, out this._setter);
					this._triedToGetSetter = true;
				}
				return this._setter;
			}
		}

		// Token: 0x06005689 RID: 22153
		protected abstract object GetNavigationPropertyFromRelatedEnd(object entity);

		// Token: 0x0600568A RID: 22154 RVA: 0x00176768 File Offset: 0x00174968
		private void ValidateNotDetached(string method)
		{
			if (this._relatedEnd == null)
			{
				if (this.InternalEntityEntry.IsDetached)
				{
					throw Error.DbPropertyEntry_NotSupportedForDetached(method, this.Name, this.InternalEntityEntry.EntityType.Name);
				}
				this._relatedEnd = this.InternalEntityEntry.GetRelatedEnd(this.Name);
			}
		}

		// Token: 0x04002302 RID: 8962
		private IRelatedEnd _relatedEnd;

		// Token: 0x04002303 RID: 8963
		private Func<object, object> _getter;

		// Token: 0x04002304 RID: 8964
		private bool _triedToGetGetter;

		// Token: 0x04002305 RID: 8965
		private Action<object, object> _setter;

		// Token: 0x04002306 RID: 8966
		private bool _triedToGetSetter;
	}
}
