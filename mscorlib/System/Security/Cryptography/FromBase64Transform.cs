using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200086F RID: 2159
	[ComVisible(true)]
	public class FromBase64Transform : ICryptoTransform, IDisposable
	{
		// Token: 0x06004EB3 RID: 20147 RVA: 0x001103FF File Offset: 0x0010F3FF
		public FromBase64Transform() : this(FromBase64TransformMode.IgnoreWhiteSpaces)
		{
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x00110408 File Offset: 0x0010F408
		public FromBase64Transform(FromBase64TransformMode whitespaces)
		{
			this._whitespaces = whitespaces;
			this._inputIndex = 0;
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06004EB5 RID: 20149 RVA: 0x0011042A File Offset: 0x0010F42A
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06004EB6 RID: 20150 RVA: 0x0011042D File Offset: 0x0010F42D
		public int OutputBlockSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06004EB7 RID: 20151 RVA: 0x00110430 File Offset: 0x0010F430
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06004EB8 RID: 20152 RVA: 0x00110433 File Offset: 0x0010F433
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00110438 File Offset: 0x0010F438
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			byte[] array = new byte[inputCount];
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
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				Buffer.InternalBlockCopy(array, 0, this._inputBuffer, this._inputIndex, num);
				this._inputIndex += num;
				return 0;
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			char[] chars = Encoding.ASCII.GetChars(array2, 0, 4 * num2);
			byte[] array3 = Convert.FromBase64CharArray(chars, 0, 4 * num2);
			Buffer.BlockCopy(array3, 0, outputBuffer, outputOffset, array3.Length);
			return array3.Length;
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x001105AC File Offset: 0x0010F5AC
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
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
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				this.Reset();
				return new byte[0];
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			char[] chars = Encoding.ASCII.GetChars(array2, 0, 4 * num2);
			byte[] result = Convert.FromBase64CharArray(chars, 0, 4 * num2);
			this.Reset();
			return result;
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x00110700 File Offset: 0x0010F700
		private byte[] DiscardWhiteSpaces(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			byte[] array = new byte[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return array;
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x0011075B File Offset: 0x0010F75B
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x0011076A File Offset: 0x0010F76A
		private void Reset()
		{
			this._inputIndex = 0;
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x00110773 File Offset: 0x0010F773
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x0011077B File Offset: 0x0010F77B
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._inputBuffer != null)
				{
					Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
				}
				this._inputBuffer = null;
				this._inputIndex = 0;
			}
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x001107AC File Offset: 0x0010F7AC
		~FromBase64Transform()
		{
			this.Dispose(false);
		}

		// Token: 0x0400289D RID: 10397
		private byte[] _inputBuffer = new byte[4];

		// Token: 0x0400289E RID: 10398
		private int _inputIndex;

		// Token: 0x0400289F RID: 10399
		private FromBase64TransformMode _whitespaces;
	}
}
