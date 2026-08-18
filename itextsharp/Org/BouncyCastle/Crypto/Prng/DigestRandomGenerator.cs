using System;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x020004BB RID: 1211
	public class DigestRandomGenerator : IRandomGenerator
	{
		// Token: 0x06002949 RID: 10569 RVA: 0x000FC448 File Offset: 0x000FB448
		public DigestRandomGenerator(IDigest digest)
		{
			this.digest = digest;
			this.seed = new byte[digest.GetDigestSize()];
			this.seedCounter = 1L;
			this.state = new byte[digest.GetDigestSize()];
			this.stateCounter = 1L;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000FC494 File Offset: 0x000FB494
		public void AddSeedMaterial(byte[] inSeed)
		{
			lock (this)
			{
				this.DigestUpdate(inSeed);
				this.DigestUpdate(this.seed);
				this.DigestDoFinal(this.seed);
			}
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000FC4E4 File Offset: 0x000FB4E4
		public void AddSeedMaterial(long rSeed)
		{
			lock (this)
			{
				this.DigestAddCounter(rSeed);
				this.DigestUpdate(this.seed);
				this.DigestDoFinal(this.seed);
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000FC534 File Offset: 0x000FB534
		public void NextBytes(byte[] bytes)
		{
			this.NextBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000FC544 File Offset: 0x000FB544
		public void NextBytes(byte[] bytes, int start, int len)
		{
			lock (this)
			{
				int num = 0;
				this.GenerateState();
				int num2 = start + len;
				for (int i = start; i < num2; i++)
				{
					if (num == this.state.Length)
					{
						this.GenerateState();
						num = 0;
					}
					bytes[i] = this.state[num++];
				}
			}
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000FC5AC File Offset: 0x000FB5AC
		private void CycleSeed()
		{
			this.DigestUpdate(this.seed);
			long seedVal;
			this.seedCounter = (seedVal = this.seedCounter) + 1L;
			this.DigestAddCounter(seedVal);
			this.DigestDoFinal(this.seed);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000FC5EC File Offset: 0x000FB5EC
		private void GenerateState()
		{
			long seedVal;
			this.stateCounter = (seedVal = this.stateCounter) + 1L;
			this.DigestAddCounter(seedVal);
			this.DigestUpdate(this.state);
			this.DigestUpdate(this.seed);
			this.DigestDoFinal(this.state);
			if (this.stateCounter % 10L == 0L)
			{
				this.CycleSeed();
			}
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000FC64C File Offset: 0x000FB64C
		private void DigestAddCounter(long seedVal)
		{
			ulong num = (ulong)seedVal;
			for (int num2 = 0; num2 != 8; num2++)
			{
				this.digest.Update((byte)num);
				num >>= 8;
			}
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000FC678 File Offset: 0x000FB678
		private void DigestUpdate(byte[] inSeed)
		{
			this.digest.BlockUpdate(inSeed, 0, inSeed.Length);
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000FC68A File Offset: 0x000FB68A
		private void DigestDoFinal(byte[] result)
		{
			this.digest.DoFinal(result, 0);
		}

		// Token: 0x04001CE7 RID: 7399
		private const long CYCLE_COUNT = 10L;

		// Token: 0x04001CE8 RID: 7400
		private long stateCounter;

		// Token: 0x04001CE9 RID: 7401
		private long seedCounter;

		// Token: 0x04001CEA RID: 7402
		private IDigest digest;

		// Token: 0x04001CEB RID: 7403
		private byte[] state;

		// Token: 0x04001CEC RID: 7404
		private byte[] seed;
	}
}
