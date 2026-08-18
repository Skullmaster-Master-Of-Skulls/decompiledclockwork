using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x0200019F RID: 415
	internal class SqlConnectionPoolKey : DbConnectionPoolKey, ICloneable
	{
		// Token: 0x0600183D RID: 6205 RVA: 0x000AC0EC File Offset: 0x000AB4EC
		internal SqlConnectionPoolKey(string connectionString, SqlCredential credential, string accessToken) : base(connectionString)
		{
			this._credential = credential;
			this._accessToken = accessToken;
			this.CalculateHashCode();
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000AC114 File Offset: 0x000AB514
		private SqlConnectionPoolKey(SqlConnectionPoolKey key) : base(key)
		{
			this._credential = key.Credential;
			this._accessToken = key.AccessToken;
			this.CalculateHashCode();
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000AC148 File Offset: 0x000AB548
		object ICloneable.Clone()
		{
			return new SqlConnectionPoolKey(this);
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x000AC15C File Offset: 0x000AB55C
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x000AC170 File Offset: 0x000AB570
		internal override string ConnectionString
		{
			get
			{
				return base.ConnectionString;
			}
			set
			{
				base.ConnectionString = value;
				this.CalculateHashCode();
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x000AC18C File Offset: 0x000AB58C
		internal SqlCredential Credential
		{
			get
			{
				return this._credential;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x000AC1A0 File Offset: 0x000AB5A0
		internal string AccessToken
		{
			get
			{
				return this._accessToken;
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000AC1B4 File Offset: 0x000AB5B4
		public override bool Equals(object obj)
		{
			SqlConnectionPoolKey sqlConnectionPoolKey = obj as SqlConnectionPoolKey;
			return sqlConnectionPoolKey != null && this._credential == sqlConnectionPoolKey._credential && this.ConnectionString == sqlConnectionPoolKey.ConnectionString && this._accessToken == sqlConnectionPoolKey._accessToken;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000AC1FC File Offset: 0x000AB5FC
		public override int GetHashCode()
		{
			return this._hashValue;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000AC210 File Offset: 0x000AB610
		private void CalculateHashCode()
		{
			this._hashValue = base.GetHashCode();
			if (this._credential != null)
			{
				this._hashValue = this._hashValue * 17 + this._credential.GetHashCode();
				return;
			}
			if (this._accessToken != null)
			{
				this._hashValue = this._hashValue * 17 + this._accessToken.GetHashCode();
			}
		}

		// Token: 0x04000EA4 RID: 3748
		private SqlCredential _credential;

		// Token: 0x04000EA5 RID: 3749
		private int _hashValue;

		// Token: 0x04000EA6 RID: 3750
		private readonly string _accessToken;
	}
}
