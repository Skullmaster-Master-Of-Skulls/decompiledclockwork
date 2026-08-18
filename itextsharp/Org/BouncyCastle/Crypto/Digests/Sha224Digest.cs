using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020003D7 RID: 983
	public class Sha224Digest : GeneralDigest
	{
		// Token: 0x0600224B RID: 8779 RVA: 0x000D5485 File Offset: 0x000D4485
		public Sha224Digest()
		{
			this.Reset();
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000D54A0 File Offset: 0x000D44A0
		public Sha224Digest(Sha224Digest t) : base(t)
		{
			this.H1 = t.H1;
			this.H2 = t.H2;
			this.H3 = t.H3;
			this.H4 = t.H4;
			this.H5 = t.H5;
			this.H6 = t.H6;
			this.H7 = t.H7;
			this.H8 = t.H8;
			Array.Copy(t.X, 0, this.X, 0, t.X.Length);
			this.xOff = t.xOff;
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000D5548 File Offset: 0x000D4548
		public override string AlgorithmName
		{
			get
			{
				return "SHA-224";
			}
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000D554F File Offset: 0x000D454F
		public override int GetDigestSize()
		{
			return 28;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000D5554 File Offset: 0x000D4554
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff] = Pack.BE_To_UInt32(input, inOff);
			if (++this.xOff == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000D5590 File Offset: 0x000D4590
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (uint)((ulong)bitLength >> 32);
			this.X[15] = (uint)bitLength;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000D55BC File Offset: 0x000D45BC
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt32_To_BE(this.H1, output, outOff);
			Pack.UInt32_To_BE(this.H2, output, outOff + 4);
			Pack.UInt32_To_BE(this.H3, output, outOff + 8);
			Pack.UInt32_To_BE(this.H4, output, outOff + 12);
			Pack.UInt32_To_BE(this.H5, output, outOff + 16);
			Pack.UInt32_To_BE(this.H6, output, outOff + 20);
			Pack.UInt32_To_BE(this.H7, output, outOff + 24);
			this.Reset();
			return 28;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000D5644 File Offset: 0x000D4644
		public override void Reset()
		{
			base.Reset();
			this.H1 = 3238371032U;
			this.H2 = 914150663U;
			this.H3 = 812702999U;
			this.H4 = 4144912697U;
			this.H5 = 4290775857U;
			this.H6 = 1750603025U;
			this.H7 = 1694076839U;
			this.H8 = 3204075428U;
			this.xOff = 0;
			Array.Clear(this.X, 0, this.X.Length);
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000D56CC File Offset: 0x000D46CC
		internal override void ProcessBlock()
		{
			for (int i = 16; i <= 63; i++)
			{
				this.X[i] = Sha224Digest.Theta1(this.X[i - 2]) + this.X[i - 7] + Sha224Digest.Theta0(this.X[i - 15]) + this.X[i - 16];
			}
			uint num = this.H1;
			uint num2 = this.H2;
			uint num3 = this.H3;
			uint num4 = this.H4;
			uint num5 = this.H5;
			uint num6 = this.H6;
			uint num7 = this.H7;
			uint num8 = this.H8;
			int num9 = 0;
			for (int j = 0; j < 8; j++)
			{
				num8 += Sha224Digest.Sum1(num5) + Sha224Digest.Ch(num5, num6, num7) + Sha224Digest.K[num9] + this.X[num9];
				num4 += num8;
				num8 += Sha224Digest.Sum0(num) + Sha224Digest.Maj(num, num2, num3);
				num9++;
				num7 += Sha224Digest.Sum1(num4) + Sha224Digest.Ch(num4, num5, num6) + Sha224Digest.K[num9] + this.X[num9];
				num3 += num7;
				num7 += Sha224Digest.Sum0(num8) + Sha224Digest.Maj(num8, num, num2);
				num9++;
				num6 += Sha224Digest.Sum1(num3) + Sha224Digest.Ch(num3, num4, num5) + Sha224Digest.K[num9] + this.X[num9];
				num2 += num6;
				num6 += Sha224Digest.Sum0(num7) + Sha224Digest.Maj(num7, num8, num);
				num9++;
				num5 += Sha224Digest.Sum1(num2) + Sha224Digest.Ch(num2, num3, num4) + Sha224Digest.K[num9] + this.X[num9];
				num += num5;
				num5 += Sha224Digest.Sum0(num6) + Sha224Digest.Maj(num6, num7, num8);
				num9++;
				num4 += Sha224Digest.Sum1(num) + Sha224Digest.Ch(num, num2, num3) + Sha224Digest.K[num9] + this.X[num9];
				num8 += num4;
				num4 += Sha224Digest.Sum0(num5) + Sha224Digest.Maj(num5, num6, num7);
				num9++;
				num3 += Sha224Digest.Sum1(num8) + Sha224Digest.Ch(num8, num, num2) + Sha224Digest.K[num9] + this.X[num9];
				num7 += num3;
				num3 += Sha224Digest.Sum0(num4) + Sha224Digest.Maj(num4, num5, num6);
				num9++;
				num2 += Sha224Digest.Sum1(num7) + Sha224Digest.Ch(num7, num8, num) + Sha224Digest.K[num9] + this.X[num9];
				num6 += num2;
				num2 += Sha224Digest.Sum0(num3) + Sha224Digest.Maj(num3, num4, num5);
				num9++;
				num += Sha224Digest.Sum1(num6) + Sha224Digest.Ch(num6, num7, num8) + Sha224Digest.K[num9] + this.X[num9];
				num5 += num;
				num += Sha224Digest.Sum0(num2) + Sha224Digest.Maj(num2, num3, num4);
				num9++;
			}
			this.H1 += num;
			this.H2 += num2;
			this.H3 += num3;
			this.H4 += num4;
			this.H5 += num5;
			this.H6 += num6;
			this.H7 += num7;
			this.H8 += num8;
			this.xOff = 0;
			Array.Clear(this.X, 0, 16);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000D5A59 File Offset: 0x000D4A59
		private static uint Ch(uint x, uint y, uint z)
		{
			return (x & y) ^ (~x & z);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000D5A63 File Offset: 0x000D4A63
		private static uint Maj(uint x, uint y, uint z)
		{
			return (x & y) ^ (x & z) ^ (y & z);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000D5A70 File Offset: 0x000D4A70
		private static uint Sum0(uint x)
		{
			return (x >> 2 | x << 30) ^ (x >> 13 | x << 19) ^ (x >> 22 | x << 10);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000D5A8E File Offset: 0x000D4A8E
		private static uint Sum1(uint x)
		{
			return (x >> 6 | x << 26) ^ (x >> 11 | x << 21) ^ (x >> 25 | x << 7);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000D5AAB File Offset: 0x000D4AAB
		private static uint Theta0(uint x)
		{
			return (x >> 7 | x << 25) ^ (x >> 18 | x << 14) ^ x >> 3;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000D5AC3 File Offset: 0x000D4AC3
		private static uint Theta1(uint x)
		{
			return (x >> 17 | x << 15) ^ (x >> 19 | x << 13) ^ x >> 10;
		}

		// Token: 0x0400178A RID: 6026
		private const int DigestLength = 28;

		// Token: 0x0400178B RID: 6027
		private uint H1;

		// Token: 0x0400178C RID: 6028
		private uint H2;

		// Token: 0x0400178D RID: 6029
		private uint H3;

		// Token: 0x0400178E RID: 6030
		private uint H4;

		// Token: 0x0400178F RID: 6031
		private uint H5;

		// Token: 0x04001790 RID: 6032
		private uint H6;

		// Token: 0x04001791 RID: 6033
		private uint H7;

		// Token: 0x04001792 RID: 6034
		private uint H8;

		// Token: 0x04001793 RID: 6035
		private uint[] X = new uint[64];

		// Token: 0x04001794 RID: 6036
		private int xOff;

		// Token: 0x04001795 RID: 6037
		internal static readonly uint[] K = new uint[]
		{
			1116352408U,
			1899447441U,
			3049323471U,
			3921009573U,
			961987163U,
			1508970993U,
			2453635748U,
			2870763221U,
			3624381080U,
			310598401U,
			607225278U,
			1426881987U,
			1925078388U,
			2162078206U,
			2614888103U,
			3248222580U,
			3835390401U,
			4022224774U,
			264347078U,
			604807628U,
			770255983U,
			1249150122U,
			1555081692U,
			1996064986U,
			2554220882U,
			2821834349U,
			2952996808U,
			3210313671U,
			3336571891U,
			3584528711U,
			113926993U,
			338241895U,
			666307205U,
			773529912U,
			1294757372U,
			1396182291U,
			1695183700U,
			1986661051U,
			2177026350U,
			2456956037U,
			2730485921U,
			2820302411U,
			3259730800U,
			3345764771U,
			3516065817U,
			3600352804U,
			4094571909U,
			275423344U,
			430227734U,
			506948616U,
			659060556U,
			883997877U,
			958139571U,
			1322822218U,
			1537002063U,
			1747873779U,
			1955562222U,
			2024104815U,
			2227730452U,
			2361852424U,
			2428436474U,
			2756734187U,
			3204031479U,
			3329325298U
		};
	}
}
