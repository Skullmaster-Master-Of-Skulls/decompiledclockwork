using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200009C RID: 156
	public class CompressedDataPacket : InputStreamPacket
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0001AABB File Offset: 0x00019ABB
		internal CompressedDataPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
			this.algorithm = (CompressionAlgorithmTag)bcpgIn.ReadByte();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001AAD0 File Offset: 0x00019AD0
		public CompressionAlgorithmTag Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x04000282 RID: 642
		private readonly CompressionAlgorithmTag algorithm;
	}
}
