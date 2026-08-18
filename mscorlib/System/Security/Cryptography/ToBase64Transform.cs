using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200086E RID: 2158
	[ComVisible(true)]
	public class ToBase64Transform : ICryptoTransform, IDisposable
	{
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x0011021A File Offset: 0x0010F21A
		public int InputBlockSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06004EA9 RID: 20137 RVA: 0x0011021D File Offset: 0x0010F21D
		public int OutputBlockSize
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06004EAA RID: 20138 RVA: 0x00110220 File Offset: 0x0010F220
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06004EAB RID: 20139 RVA: 0x00110223 File Offset: 0x0010F223
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x00110228 File Offset: 0x0010F228
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (this.asciiEncoding == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, 3, array, 0);
			byte[] bytes = this.asciiEncoding.GetBytes(array);
			if (bytes.Length != 4)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_SSE_InvalidDataSize"));
			}
			Buffer.BlockCopy(bytes, 0, outputBuffer, outputOffset, bytes.Length);
			return bytes.Length;
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x001102F0 File Offset: 0x0010F2F0
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (this.asciiEncoding == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (inputCount == 0)
			{
				return new byte[0];
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, inputCount, array, 0);
			return this.asciiEncoding.GetBytes(array);
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x00110399 File Offset: 0x0010F399
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x001103A8 File Offset: 0x0010F3A8
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x001103B0 File Offset: 0x0010F3B0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.asciiEncoding = null;
			}
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x001103BC File Offset: 0x0010F3BC
		~ToBase64Transform()
		{
			this.Dispose(false);
		}

		// Token: 0x0400289C RID: 10396
		private ASCIIEncoding asciiEncoding = new ASCIIEncoding();
	}
}
