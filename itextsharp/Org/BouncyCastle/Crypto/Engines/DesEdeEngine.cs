using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020001F6 RID: 502
	public class DesEdeEngine : DesEngine
	{
		// Token: 0x0600137D RID: 4989 RVA: 0x0006F3A8 File Offset: 0x0006E3A8
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to DESede init - " + parameters.GetType().ToString());
			}
			byte[] key = ((KeyParameter)parameters).GetKey();
			this.forEncryption = forEncryption;
			byte[] array = new byte[8];
			Array.Copy(key, 0, array, 0, array.Length);
			this.workingKey1 = DesEngine.GenerateWorkingKey(forEncryption, array);
			byte[] array2 = new byte[8];
			Array.Copy(key, 8, array2, 0, array2.Length);
			this.workingKey2 = DesEngine.GenerateWorkingKey(!forEncryption, array2);
			if (key.Length == 24)
			{
				byte[] array3 = new byte[8];
				Array.Copy(key, 16, array3, 0, array3.Length);
				this.workingKey3 = DesEngine.GenerateWorkingKey(forEncryption, array3);
				return;
			}
			this.workingKey3 = this.workingKey1;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0006F463 File Offset: 0x0006E463
		public override string AlgorithmName
		{
			get
			{
				return "DESede";
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0006F46A File Offset: 0x0006E46A
		public override int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0006F470 File Offset: 0x0006E470
		public override int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.workingKey1 == null)
			{
				throw new InvalidOperationException("DESede engine not initialised");
			}
			if (inOff + 8 > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + 8 > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			byte[] array = new byte[8];
			if (this.forEncryption)
			{
				DesEngine.DesFunc(this.workingKey1, input, inOff, array, 0);
				DesEngine.DesFunc(this.workingKey2, array, 0, array, 0);
				DesEngine.DesFunc(this.workingKey3, array, 0, output, outOff);
			}
			else
			{
				DesEngine.DesFunc(this.workingKey3, input, inOff, array, 0);
				DesEngine.DesFunc(this.workingKey2, array, 0, array, 0);
				DesEngine.DesFunc(this.workingKey1, array, 0, output, outOff);
			}
			return 8;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0006F525 File Offset: 0x0006E525
		public override void Reset()
		{
		}

		// Token: 0x04000D97 RID: 3479
		private int[] workingKey1;

		// Token: 0x04000D98 RID: 3480
		private int[] workingKey2;

		// Token: 0x04000D99 RID: 3481
		private int[] workingKey3;

		// Token: 0x04000D9A RID: 3482
		private bool forEncryption;
	}
}
