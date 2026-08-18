using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020001EF RID: 495
	public class DHValidationParameters
	{
		// Token: 0x06001348 RID: 4936 RVA: 0x0006E92D File Offset: 0x0006D92D
		public DHValidationParameters(byte[] seed, int counter)
		{
			if (seed == null)
			{
				throw new ArgumentNullException("seed");
			}
			this.seed = (byte[])seed.Clone();
			this.counter = counter;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0006E95B File Offset: 0x0006D95B
		public byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0006E96D File Offset: 0x0006D96D
		public int Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0006E978 File Offset: 0x0006D978
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHValidationParameters dhvalidationParameters = obj as DHValidationParameters;
			return dhvalidationParameters != null && this.Equals(dhvalidationParameters);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0006E99E File Offset: 0x0006D99E
		protected bool Equals(DHValidationParameters other)
		{
			return this.counter == other.counter && Arrays.AreEqual(this.seed, other.seed);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0006E9C4 File Offset: 0x0006D9C4
		public override int GetHashCode()
		{
			return this.counter.GetHashCode() ^ Arrays.GetHashCode(this.seed);
		}

		// Token: 0x04000D7F RID: 3455
		private readonly byte[] seed;

		// Token: 0x04000D80 RID: 3456
		private readonly int counter;
	}
}
