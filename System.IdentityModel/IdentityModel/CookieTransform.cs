using System;

namespace System.IdentityModel
{
	// Token: 0x0200002C RID: 44
	public abstract class CookieTransform
	{
		// Token: 0x0600014E RID: 334
		public abstract byte[] Decode(byte[] encoded);

		// Token: 0x0600014F RID: 335
		public abstract byte[] Encode(byte[] value);
	}
}
