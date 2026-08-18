using System;
using ICSharpCode.SharpZipLib.Checksums;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200006C RID: 108
	internal class PkzipClassicCryptoBase
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x00016D08 File Offset: 0x00015D08
		protected byte TransformByte()
		{
			uint num = (this.keys[2] & 65535U) | 2U;
			return (byte)(num * (num ^ 1U) >> 8);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00016D30 File Offset: 0x00015D30
		protected void SetKeys(byte[] keyData)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (keyData.Length != 12)
			{
				throw new InvalidOperationException("Key length is not valid");
			}
			this.keys = new uint[3];
			this.keys[0] = (uint)((int)keyData[3] << 24 | (int)keyData[2] << 16 | (int)keyData[1] << 8 | (int)keyData[0]);
			this.keys[1] = (uint)((int)keyData[7] << 24 | (int)keyData[6] << 16 | (int)keyData[5] << 8 | (int)keyData[4]);
			this.keys[2] = (uint)((int)keyData[11] << 24 | (int)keyData[10] << 16 | (int)keyData[9] << 8 | (int)keyData[8]);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00016DCC File Offset: 0x00015DCC
		protected void UpdateKeys(byte ch)
		{
			this.keys[0] = Crc32.ComputeCrc32(this.keys[0], ch);
			this.keys[1] = this.keys[1] + (uint)((byte)this.keys[0]);
			this.keys[1] = this.keys[1] * 134775813U + 1U;
			this.keys[2] = Crc32.ComputeCrc32(this.keys[2], (byte)(this.keys[1] >> 24));
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00016E42 File Offset: 0x00015E42
		protected void Reset()
		{
			this.keys[0] = 0U;
			this.keys[1] = 0U;
			this.keys[2] = 0U;
		}

		// Token: 0x040002E2 RID: 738
		private uint[] keys;
	}
}
