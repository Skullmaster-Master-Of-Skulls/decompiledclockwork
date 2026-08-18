using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000254 RID: 596
	public class LiteralDataPacket : InputStreamPacket
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x0008355C File Offset: 0x0008255C
		internal LiteralDataPacket(BcpgInputStream bcpgIn) : base(bcpgIn)
		{
			this.format = bcpgIn.ReadByte();
			int num = bcpgIn.ReadByte();
			this.fileName = new byte[num];
			for (int num2 = 0; num2 != num; num2++)
			{
				this.fileName[num2] = (byte)bcpgIn.ReadByte();
			}
			this.modDate = (long)((ulong)(bcpgIn.ReadByte() << 24 | bcpgIn.ReadByte() << 16 | bcpgIn.ReadByte() << 8 | bcpgIn.ReadByte()) * 1000UL);
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x000835DB File Offset: 0x000825DB
		public int Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x000835E3 File Offset: 0x000825E3
		public long ModificationTime
		{
			get
			{
				return this.modDate;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000835EB File Offset: 0x000825EB
		public string FileName
		{
			get
			{
				return Strings.FromUtf8ByteArray(this.fileName);
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000835F8 File Offset: 0x000825F8
		public byte[] GetRawFileName()
		{
			return Arrays.Clone(this.fileName);
		}

		// Token: 0x04000F9B RID: 3995
		private int format;

		// Token: 0x04000F9C RID: 3996
		private byte[] fileName;

		// Token: 0x04000F9D RID: 3997
		private long modDate;
	}
}
