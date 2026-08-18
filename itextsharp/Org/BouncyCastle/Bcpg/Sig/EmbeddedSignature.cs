using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000252 RID: 594
	public class EmbeddedSignature : SignatureSubpacket
	{
		// Token: 0x060016AA RID: 5802 RVA: 0x0008354F File Offset: 0x0008254F
		public EmbeddedSignature(bool critical, byte[] data) : base(SignatureSubpacketTag.EmbeddedSignature, critical, data)
		{
		}
	}
}
