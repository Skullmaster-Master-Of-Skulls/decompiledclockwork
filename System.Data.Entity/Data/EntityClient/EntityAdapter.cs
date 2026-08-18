using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Mapping.Update.Internal;
using System.Data.Objects;

namespace System.Data.EntityClient
{
	// Token: 0x02000121 RID: 289
	internal sealed class EntityAdapter : IEntityAdapter
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00041622 File Offset: 0x0003F822
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x0004162A File Offset: 0x0003F82A
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

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00041638 File Offset: 0x0003F838
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00041640 File Offset: 0x0003F840
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

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00041649 File Offset: 0x0003F849
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x00041651 File Offset: 0x0003F851
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

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0004165A File Offset: 0x0003F85A
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x00041662 File Offset: 0x0003F862
		int? IEntityAdapter.CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				this._commandTimeout = value;
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0004166C File Offset: 0x0003F86C
		public int Update(IEntityStateManager entityCache)
		{
			EntityUtil.CheckArgumentNull<IEntityStateManager>(entityCache, "entityCache");
			if (!EntityAdapter.IsStateManagerDirty(entityCache))
			{
				return 0;
			}
			if (this._connection == null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_NoConnectionForAdapter);
			}
			if (this._connection.StoreProviderFactory == null || this._connection.StoreConnection == null)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_NoStoreConnectionForUpdate);
			}
			if (ConnectionState.Open != this._connection.State)
			{
				throw EntityUtil.InvalidOperation(Strings.EntityClient_ClosedConnectionForUpdate);
			}
			return UpdateTranslator.Update(entityCache, this);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x000416E8 File Offset: 0x0003F8E8
		private static bool IsStateManagerDirty(IEntityStateManager entityCache)
		{
			bool result = false;
			using (IEnumerator<IEntityStateEntry> enumerator = entityCache.GetEntityStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ObjectStateEntry objectStateEntry = (ObjectStateEntry)enumerator.Current;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000A28 RID: 2600
		private bool _acceptChangesDuringUpdate = true;

		// Token: 0x04000A29 RID: 2601
		private EntityConnection _connection;

		// Token: 0x04000A2A RID: 2602
		private int? _commandTimeout;
	}
}
