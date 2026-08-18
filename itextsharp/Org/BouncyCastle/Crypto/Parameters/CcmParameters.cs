using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020002F4 RID: 756
	public class CcmParameters : AeadParameters
	{
		// Token: 0x06001BCC RID: 7116 RVA: 0x000A62B8 File Offset: 0x000A52B8
		public CcmParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText) : base(key, macSize, nonce, associatedText)
		{
		}
	}
}
