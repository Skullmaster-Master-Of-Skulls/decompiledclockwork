using System;
using System.Globalization;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Prng;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000074 RID: 116
	public class SecureRandom : Random
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00013E50 File Offset: 0x00012E50
		private static SecureRandom Master
		{
			get
			{
				if (SecureRandom.master[0] == null)
				{
					IRandomGenerator randomGenerator = SecureRandom.sha256Generator;
					randomGenerator = new ReversedWindowGenerator(randomGenerator, 32);
					SecureRandom secureRandom = SecureRandom.master[0] = new SecureRandom(randomGenerator);
					secureRandom.SetSeed(DateTime.Now.Ticks);
					secureRandom.SetSeed(new ThreadedSeedGenerator().GenerateSeed(24, true));
					secureRandom.GenerateSeed(1 + secureRandom.Next(32));
				}
				return SecureRandom.master[0];
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00013EC4 File Offset: 0x00012EC4
		public static SecureRandom GetInstance(string algorithm)
		{
			IRandomGenerator randomGenerator = null;
			string a;
			if ((a = algorithm.ToUpper(CultureInfo.InvariantCulture)) != null)
			{
				if (!(a == "SHA1PRNG"))
				{
					if (a == "SHA256PRNG")
					{
						randomGenerator = SecureRandom.sha256Generator;
					}
				}
				else
				{
					randomGenerator = SecureRandom.sha1Generator;
				}
			}
			if (randomGenerator != null)
			{
				return new SecureRandom(randomGenerator);
			}
			throw new ArgumentException("Unrecognised PRNG algorithm: " + algorithm, "algorithm");
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00013F2B File Offset: 0x00012F2B
		public static byte[] GetSeed(int length)
		{
			return SecureRandom.Master.GenerateSeed(length);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00013F38 File Offset: 0x00012F38
		public SecureRandom() : this(SecureRandom.sha1Generator)
		{
			this.SetSeed(SecureRandom.GetSeed(8));
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00013F51 File Offset: 0x00012F51
		public SecureRandom(byte[] inSeed) : this(SecureRandom.sha1Generator)
		{
			this.SetSeed(inSeed);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00013F65 File Offset: 0x00012F65
		public SecureRandom(IRandomGenerator generator) : base(0)
		{
			this.generator = generator;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00013F78 File Offset: 0x00012F78
		public virtual byte[] GenerateSeed(int length)
		{
			this.SetSeed(DateTime.Now.Ticks);
			byte[] array = new byte[length];
			this.NextBytes(array);
			return array;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013FA7 File Offset: 0x00012FA7
		public virtual void SetSeed(byte[] inSeed)
		{
			this.generator.AddSeedMaterial(inSeed);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00013FB5 File Offset: 0x00012FB5
		public virtual void SetSeed(long seed)
		{
			this.generator.AddSeedMaterial(seed);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00013FC4 File Offset: 0x00012FC4
		public override int Next()
		{
			int num;
			do
			{
				num = (this.NextInt() & int.MaxValue);
			}
			while (num == 2147483647);
			return num;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00013FE8 File Offset: 0x00012FE8
		public override int Next(int maxValue)
		{
			if (maxValue < 2)
			{
				if (maxValue < 0)
				{
					throw new ArgumentOutOfRangeException("maxValue < 0");
				}
				return 0;
			}
			else
			{
				if ((maxValue & -maxValue) == maxValue)
				{
					int num = this.NextInt() & int.MaxValue;
					long num2 = (long)maxValue * (long)num >> 31;
					return (int)num2;
				}
				int num3;
				int num4;
				do
				{
					num3 = (this.NextInt() & int.MaxValue);
					num4 = num3 % maxValue;
				}
				while (num3 - num4 + (maxValue - 1) < 0);
				return num4;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00014048 File Offset: 0x00013048
		public override int Next(int minValue, int maxValue)
		{
			if (maxValue <= minValue)
			{
				if (maxValue == minValue)
				{
					return minValue;
				}
				throw new ArgumentException("maxValue cannot be less than minValue");
			}
			else
			{
				int num = maxValue - minValue;
				if (num > 0)
				{
					return minValue + this.Next(num);
				}
				int num2;
				do
				{
					num2 = this.NextInt();
				}
				while (num2 < minValue || num2 >= maxValue);
				return num2;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001408C File Offset: 0x0001308C
		public override void NextBytes(byte[] buffer)
		{
			this.generator.NextBytes(buffer);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001409A File Offset: 0x0001309A
		public virtual void NextBytes(byte[] buffer, int start, int length)
		{
			this.generator.NextBytes(buffer, start, length);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000140AA File Offset: 0x000130AA
		public override double NextDouble()
		{
			return Convert.ToDouble((ulong)this.NextLong()) / SecureRandom.DoubleScale;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000140C0 File Offset: 0x000130C0
		public virtual int NextInt()
		{
			byte[] array = new byte[4];
			this.NextBytes(array);
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				num = (num << 8) + (int)(array[i] & byte.MaxValue);
			}
			return num;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000140F8 File Offset: 0x000130F8
		public virtual long NextLong()
		{
			return (long)((ulong)this.NextInt() << 32 | (ulong)this.NextInt());
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001410C File Offset: 0x0001310C
		// Note: this type is marked as 'beforefieldinit'.
		static SecureRandom()
		{
			SecureRandom[] array = new SecureRandom[1];
			SecureRandom.master = array;
			SecureRandom.DoubleScale = Math.Pow(2.0, 64.0);
		}

		// Token: 0x040001FE RID: 510
		private static readonly IRandomGenerator sha1Generator = new DigestRandomGenerator(new Sha1Digest());

		// Token: 0x040001FF RID: 511
		private static readonly IRandomGenerator sha256Generator = new DigestRandomGenerator(new Sha256Digest());

		// Token: 0x04000200 RID: 512
		private static readonly SecureRandom[] master;

		// Token: 0x04000201 RID: 513
		protected IRandomGenerator generator;

		// Token: 0x04000202 RID: 514
		private static readonly double DoubleScale;
	}
}
