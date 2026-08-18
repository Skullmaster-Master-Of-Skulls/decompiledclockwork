using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000869 RID: 2153
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06004E95 RID: 20117
		// (set) Token: 0x06004E96 RID: 20118
		public abstract string Parameters { get; set; }

		// Token: 0x06004E97 RID: 20119
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x06004E98 RID: 20120
		public abstract byte[] DecryptKeyExchange(byte[] rgb);
	}
}
