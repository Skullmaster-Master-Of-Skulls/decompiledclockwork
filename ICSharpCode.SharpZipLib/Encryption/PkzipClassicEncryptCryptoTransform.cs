using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200006D RID: 109
	internal class PkzipClassicEncryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00016E67 File Offset: 0x00015E67
		internal PkzipClassicEncryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00016E78 File Offset: 0x00015E78
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00016E9C File Offset: 0x00015E9C
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = inputOffset; i < inputOffset + inputCount; i++)
			{
				byte ch = inputBuffer[i];
				outputBuffer[outputOffset++] = (inputBuffer[i] ^ base.TransformByte());
				base.UpdateKeys(ch);
			}
			return inputCount;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00016ED8 File Offset: 0x00015ED8
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00016EDB File Offset: 0x00015EDB
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00016EDE File Offset: 0x00015EDE
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00016EE1 File Offset: 0x00015EE1
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00016EE4 File Offset: 0x00015EE4
		public void Dispose()
		{
			base.Reset();
		}
	}
}
