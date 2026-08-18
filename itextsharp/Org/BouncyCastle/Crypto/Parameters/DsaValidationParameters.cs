using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000242 RID: 578
	public class DsaValidationParameters
	{
		// Token: 0x06001658 RID: 5720 RVA: 0x00082419 File Offset: 0x00081419
		public DsaValidationParameters(byte[] seed, int counter)
		{
			if (seed == null)
			{
				throw new ArgumentNullException("seed");
			}
			this.seed = (byte[])seed.Clone();
			this.counter = counter;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00082447 File Offset: 0x00081447
		public byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00082459 File Offset: 0x00081459
		public int Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00082464 File Offset: 0x00081464
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaValidationParameters dsaValidationParameters = obj as DsaValidationParameters;
			return dsaValidationParameters != null && this.Equals(dsaValidationParameters);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0008248A File Offset: 0x0008148A
		protected bool Equals(DsaValidationParameters other)
		{
			return this.counter == other.counter && Arrays.AreEqual(this.seed, other.seed);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000824B0 File Offset: 0x000814B0
		public override int GetHashCode()
		{
			return this.counter.GetHashCode() ^ Arrays.GetHashCode(this.seed);
		}

		// Token: 0x04000F53 RID: 3923
		private readonly byte[] seed;

		// Token: 0x04000F54 RID: 3924
		private readonly int counter;
	}
}
