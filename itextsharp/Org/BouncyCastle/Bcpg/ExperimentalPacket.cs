using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x020005B5 RID: 1461
	public class ExperimentalPacket : ContainedPacket
	{
		// Token: 0x0600324F RID: 12879 RVA: 0x00138935 File Offset: 0x00137935
		internal ExperimentalPacket(PacketTag tag, BcpgInputStream bcpgIn)
		{
			this.tag = tag;
			this.contents = bcpgIn.ReadAll();
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x00138950 File Offset: 0x00137950
		public PacketTag Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x00138958 File Offset: 0x00137958
		public byte[] GetContents()
		{
			return (byte[])this.contents.Clone();
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x0013896A File Offset: 0x0013796A
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WritePacket(this.tag, this.contents, true);
		}

		// Token: 0x04002279 RID: 8825
		private readonly PacketTag tag;

		// Token: 0x0400227A RID: 8826
		private readonly byte[] contents;
	}
}
