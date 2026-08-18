using System;
using System.Threading;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000612 RID: 1554
	public class ThreadedSeedGenerator
	{
		// Token: 0x060034F7 RID: 13559 RVA: 0x00148AED File Offset: 0x00147AED
		public byte[] GenerateSeed(int numBytes, bool fast)
		{
			return new ThreadedSeedGenerator.SeedGenerator().GenerateSeed(numBytes, fast);
		}

		// Token: 0x02000613 RID: 1555
		private class SeedGenerator
		{
			// Token: 0x060034F9 RID: 13561 RVA: 0x00148B03 File Offset: 0x00147B03
			private void Run(object ignored)
			{
				while (!this.stop)
				{
					this.counter++;
				}
			}

			// Token: 0x060034FA RID: 13562 RVA: 0x00148B24 File Offset: 0x00147B24
			public byte[] GenerateSeed(int numBytes, bool fast)
			{
				this.counter = 0;
				this.stop = false;
				byte[] array = new byte[numBytes];
				int num = 0;
				int num2 = fast ? numBytes : (numBytes * 8);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.Run));
				for (int i = 0; i < num2; i++)
				{
					while (this.counter == num)
					{
						try
						{
							Thread.Sleep(1);
						}
						catch (Exception)
						{
						}
					}
					num = this.counter;
					if (fast)
					{
						array[i] = (byte)num;
					}
					else
					{
						int num3 = i / 8;
						array[num3] = (byte)((int)array[num3] << 1 | (num & 1));
					}
				}
				this.stop = true;
				return array;
			}

			// Token: 0x04002375 RID: 9077
			private volatile int counter;

			// Token: 0x04002376 RID: 9078
			private volatile bool stop;
		}
	}
}
