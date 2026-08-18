using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200087A RID: 2170
	[ComVisible(true)]
	public abstract class DeriveBytes
	{
		// Token: 0x06004F27 RID: 20263
		public abstract byte[] GetBytes(int cb);

		// Token: 0x06004F28 RID: 20264
		public abstract void Reset();
	}
}
