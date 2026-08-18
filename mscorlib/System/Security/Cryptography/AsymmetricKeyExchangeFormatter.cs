using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200086A RID: 2154
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeFormatter
	{
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06004E9A RID: 20122
		public abstract string Parameters { get; }

		// Token: 0x06004E9B RID: 20123
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x06004E9C RID: 20124
		public abstract byte[] CreateKeyExchange(byte[] data);

		// Token: 0x06004E9D RID: 20125
		public abstract byte[] CreateKeyExchange(byte[] data, Type symAlgType);
	}
}
