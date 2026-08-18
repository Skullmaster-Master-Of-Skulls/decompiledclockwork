using System;
using System.Data.Common;
using System.Security;

namespace System.Data.SqlClient
{
	// Token: 0x0200019E RID: 414
	public sealed class SqlCredential
	{
		// Token: 0x0600183A RID: 6202 RVA: 0x000AC038 File Offset: 0x000AB438
		public SqlCredential(string userId, SecureString password)
		{
			if (userId == null)
			{
				throw ADP.ArgumentNull("userId");
			}
			if (userId.Length > 128)
			{
				throw ADP.InvalidArgumentLength("userId", 128);
			}
			if (password == null)
			{
				throw ADP.ArgumentNull("password");
			}
			if (password.Length > 128)
			{
				throw ADP.InvalidArgumentLength("password", 128);
			}
			if (!password.IsReadOnly())
			{
				throw ADP.MustBeReadOnly("password");
			}
			this._userId = userId;
			this._password = password;
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x000AC0C4 File Offset: 0x000AB4C4
		public string UserId
		{
			get
			{
				return this._userId;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x000AC0D8 File Offset: 0x000AB4D8
		public SecureString Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x04000EA2 RID: 3746
		private string _userId;

		// Token: 0x04000EA3 RID: 3747
		private SecureString _password;
	}
}
