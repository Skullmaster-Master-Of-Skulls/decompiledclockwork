using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000031 RID: 49
	public class TrustSignature : SignatureSubpacket
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00008C64 File Offset: 0x00007C64
		private static byte[] IntToByteArray(int v1, int v2)
		{
			return new byte[]
			{
				(byte)v1,
				(byte)v2
			};
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008C83 File Offset: 0x00007C83
		public TrustSignature(bool critical, byte[] data) : base(SignatureSubpacketTag.TrustSig, critical, data)
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008C8E File Offset: 0x00007C8E
		public TrustSignature(bool critical, int depth, int trustAmount) : base(SignatureSubpacketTag.TrustSig, critical, TrustSignature.IntToByteArray(depth, trustAmount))
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00008C9F File Offset: 0x00007C9F
		public int Depth
		{
			get
			{
				return (int)(this.data[0] & byte.MaxValue);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00008CAF File Offset: 0x00007CAF
		public int TrustAmount
		{
			get
			{
				return (int)(this.data[1] & byte.MaxValue);
			}
		}
	}
}
