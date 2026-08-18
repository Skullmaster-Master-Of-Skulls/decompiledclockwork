using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200006E RID: 110
	internal class PkzipClassicDecryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x00016EEC File Offset: 0x00015EEC
		internal PkzipClassicDecryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00016EFC File Offset: 0x00015EFC
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00016F20 File Offset: 0x00015F20
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = inputOffset; i < inputOffset + inputCount; i++)
			{
				byte b = inputBuffer[i] ^ base.TransformByte();
				outputBuffer[outputOffset++] = b;
				base.UpdateKeys(b);
			}
			return inputCount;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00016F5A File Offset: 0x00015F5A
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00016F5D File Offset: 0x00015F5D
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00016F60 File Offset: 0x00015F60
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00016F63 File Offset: 0x00015F63
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00016F66 File Offset: 0x00015F66
		public void Dispose()
		{
			base.Reset();
		}
	}
}
