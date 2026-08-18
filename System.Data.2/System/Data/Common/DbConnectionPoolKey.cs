using System;

namespace System.Data.Common
{
	// Token: 0x020002E6 RID: 742
	internal class DbConnectionPoolKey : ICloneable
	{
		// Token: 0x06002EE5 RID: 12005 RVA: 0x00129730 File Offset: 0x00128B30
		internal DbConnectionPoolKey(string connectionString)
		{
			this._connectionString = connectionString;
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x0012974C File Offset: 0x00128B4C
		protected DbConnectionPoolKey(DbConnectionPoolKey key)
		{
			this._connectionString = key.ConnectionString;
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x0012976C File Offset: 0x00128B6C
		object ICloneable.Clone()
		{
			return new DbConnectionPoolKey(this);
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x00129780 File Offset: 0x00128B80
		// (set) Token: 0x06002EE9 RID: 12009 RVA: 0x00129794 File Offset: 0x00128B94
		internal virtual string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x001297A8 File Offset: 0x00128BA8
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(DbConnectionPoolKey))
			{
				return false;
			}
			DbConnectionPoolKey dbConnectionPoolKey = obj as DbConnectionPoolKey;
			return dbConnectionPoolKey != null && this._connectionString == dbConnectionPoolKey._connectionString;
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x001297F0 File Offset: 0x00128BF0
		public override int GetHashCode()
		{
			if (this._connectionString != null)
			{
				return this._connectionString.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04001CD8 RID: 7384
		private string _connectionString;
	}
}
