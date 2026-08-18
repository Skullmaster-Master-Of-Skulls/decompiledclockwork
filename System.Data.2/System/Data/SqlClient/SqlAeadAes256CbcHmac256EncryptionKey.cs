using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000194 RID: 404
	internal class SqlAeadAes256CbcHmac256EncryptionKey : SqlClientSymmetricKey
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x000AB1C8 File Offset: 0x000AA5C8
		internal SqlAeadAes256CbcHmac256EncryptionKey(byte[] rootKey, string algorithmName) : base(rootKey)
		{
			this._algorithmName = algorithmName;
			int num = 32;
			if (rootKey.Length != num)
			{
				throw SQL.InvalidKeySize(this._algorithmName, rootKey.Length, num);
			}
			string s = string.Format("Microsoft SQL Server cell encryption key with encryption algorithm:{0} and key length:{1}", this._algorithmName, 256);
			byte[] array = new byte[num];
			SqlSecurityUtility.GetHMACWithSHA256(Encoding.Unicode.GetBytes(s), this.RootKey, array);
			this._encryptionKey = new SqlClientSymmetricKey(array);
			string s2 = string.Format("Microsoft SQL Server cell MAC key with encryption algorithm:{0} and key length:{1}", this._algorithmName, 256);
			byte[] array2 = new byte[num];
			SqlSecurityUtility.GetHMACWithSHA256(Encoding.Unicode.GetBytes(s2), this.RootKey, array2);
			this._macKey = new SqlClientSymmetricKey(array2);
			string s3 = string.Format("Microsoft SQL Server cell IV key with encryption algorithm:{0} and key length:{1}", this._algorithmName, 256);
			byte[] array3 = new byte[num];
			SqlSecurityUtility.GetHMACWithSHA256(Encoding.Unicode.GetBytes(s3), this.RootKey, array3);
			this._ivKey = new SqlClientSymmetricKey(array3);
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x000AB2D4 File Offset: 0x000AA6D4
		internal byte[] EncryptionKey
		{
			get
			{
				return this._encryptionKey.RootKey;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000AB2EC File Offset: 0x000AA6EC
		internal byte[] MACKey
		{
			get
			{
				return this._macKey.RootKey;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x000AB304 File Offset: 0x000AA704
		internal byte[] IVKey
		{
			get
			{
				return this._ivKey.RootKey;
			}
		}

		// Token: 0x04000E88 RID: 3720
		internal const int KeySize = 256;

		// Token: 0x04000E89 RID: 3721
		private const string _encryptionKeySaltFormat = "Microsoft SQL Server cell encryption key with encryption algorithm:{0} and key length:{1}";

		// Token: 0x04000E8A RID: 3722
		private const string _macKeySaltFormat = "Microsoft SQL Server cell MAC key with encryption algorithm:{0} and key length:{1}";

		// Token: 0x04000E8B RID: 3723
		private const string _ivKeySaltFormat = "Microsoft SQL Server cell IV key with encryption algorithm:{0} and key length:{1}";

		// Token: 0x04000E8C RID: 3724
		private readonly SqlClientSymmetricKey _encryptionKey;

		// Token: 0x04000E8D RID: 3725
		private readonly SqlClientSymmetricKey _macKey;

		// Token: 0x04000E8E RID: 3726
		private readonly SqlClientSymmetricKey _ivKey;

		// Token: 0x04000E8F RID: 3727
		private readonly string _algorithmName;
	}
}
