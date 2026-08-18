using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200019A RID: 410
	internal class SqlClientSymmetricKey
	{
		// Token: 0x0600181B RID: 6171 RVA: 0x000AB46C File Offset: 0x000AA86C
		internal SqlClientSymmetricKey(byte[] rootKey)
		{
			if (rootKey == null || rootKey.Length == 0)
			{
				throw SQL.NullColumnEncryptionKeySysErr();
			}
			this._rootKey = rootKey;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000AB494 File Offset: 0x000AA894
		~SqlClientSymmetricKey()
		{
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x000AB4C8 File Offset: 0x000AA8C8
		internal virtual byte[] RootKey
		{
			get
			{
				return this._rootKey;
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x000AB4DC File Offset: 0x000AA8DC
		internal virtual string GetKeyHash()
		{
			return SqlSecurityUtility.GetSHA256Hash(this.RootKey);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000AB4F4 File Offset: 0x000AA8F4
		internal virtual int Length()
		{
			return this._rootKey.Length;
		}

		// Token: 0x04000E96 RID: 3734
		protected readonly byte[] _rootKey;
	}
}
