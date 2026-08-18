using System;
using System.Data.Common;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x02000344 RID: 836
	internal class EntityAdapter : IEntityAdapter
	{
		// Token: 0x06001DCE RID: 7630 RVA: 0x0008F7E2 File Offset: 0x0008D9E2
		public EntityAdapter(ObjectContext context) : this(context, (EntityAdapter a) => new UpdateTranslator(a))
		{
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0008F808 File Offset: 0x0008DA08
		protected EntityAdapter(ObjectContext context, Func<EntityAdapter, UpdateTranslator> updateTranslatorFactory)
		{
			this._context = context;
			this._updateTranslatorFactory = updateTranslatorFactory;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0008F825 File Offset: 0x0008DA25
		public ObjectContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0008F82D File Offset: 0x0008DA2D
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x0008F835 File Offset: 0x0008DA35
		DbConnection IEntityAdapter.Connection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (EntityConnection)value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x0008F843 File Offset: 0x0008DA43
		// (set) Token: 0x06001DD4 RID: 7636 RVA: 0x0008F84B File Offset: 0x0008DA4B
		public EntityConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				this._connection = value;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x0008F854 File Offset: 0x0008DA54
		// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x0008F85C File Offset: 0x0008DA5C
		public bool AcceptChangesDuringUpdate
		{
			get
			{
				return this._acceptChangesDuringUpdate;
			}
			set
			{
				this._acceptChangesDuringUpdate = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0008F865 File Offset: 0x0008DA65
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x0008F86D File Offset: 0x0008DA6D
		public int? CommandTimeout { get; set; }

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0008F87E File Offset: 0x0008DA7E
		public int Update()
		{
			return this.Update<int>(0, (UpdateTranslator ut) => ut.Update());
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0008F8BC File Offset: 0x0008DABC
		public Task<int> UpdateAsync(CancellationToken cancellationToken)
		{
			return this.Update<Task<int>>(Task.FromResult<int>(0), (UpdateTranslator ut) => ut.UpdateAsync(cancellationToken));
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x0008F8F0 File Offset: 0x0008DAF0
		private T Update<T>(T noChangesResult, Func<UpdateTranslator, T> updateFunction)
		{
			if (!EntityAdapter.IsStateManagerDirty(this._context.ObjectStateManager))
			{
				return noChangesResult;
			}
			if (this._connection == null)
			{
				throw Error.EntityClient_NoConnectionForAdapter();
			}
			if (this._connection.StoreProviderFactory == null || this._connection.StoreConnection == null)
			{
				throw Error.EntityClient_NoStoreConnectionForUpdate();
			}
			if (ConnectionState.Open != this._connection.State)
			{
				throw Error.EntityClient_ClosedConnectionForUpdate();
			}
			UpdateTranslator arg = this._updateTranslatorFactory(this);
			return updateFunction(arg);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0008F967 File Offset: 0x0008DB67
		private static bool IsStateManagerDirty(ObjectStateManager entityCache)
		{
			return entityCache.HasChanges();
		}

		// Token: 0x04000A2E RID: 2606
		private bool _acceptChangesDuringUpdate = true;

		// Token: 0x04000A2F RID: 2607
		private EntityConnection _connection;

		// Token: 0x04000A30 RID: 2608
		private readonly ObjectContext _context;

		// Token: 0x04000A31 RID: 2609
		private readonly Func<EntityAdapter, UpdateTranslator> _updateTranslatorFactory;
	}
}
