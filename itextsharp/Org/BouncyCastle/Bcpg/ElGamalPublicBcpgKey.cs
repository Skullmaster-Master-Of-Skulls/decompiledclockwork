using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000255 RID: 597
	public class ElGamalPublicBcpgKey : BcpgObject, IBcpgKey
	{
		// Token: 0x060016B0 RID: 5808 RVA: 0x00083605 File Offset: 0x00082605
		public ElGamalPublicBcpgKey(BcpgInputStream bcpgIn)
		{
			this.p = new MPInteger(bcpgIn);
			this.g = new MPInteger(bcpgIn);
			this.y = new MPInteger(bcpgIn);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00083631 File Offset: 0x00082631
		public ElGamalPublicBcpgKey(BigInteger p, BigInteger g, BigInteger y)
		{
			this.p = new MPInteger(p);
			this.g = new MPInteger(g);
			this.y = new MPInteger(y);
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0008365D File Offset: 0x0008265D
		public string Format
		{
			get
			{
				return "PGP";
			}
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00083664 File Offset: 0x00082664
		public override byte[] GetEncoded()
		{
			byte[] result;
			try
			{
				result = base.GetEncoded();
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00083690 File Offset: 0x00082690
		public BigInteger P
		{
			get
			{
				return this.p.Value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0008369D File Offset: 0x0008269D
		public BigInteger G
		{
			get
			{
				return this.g.Value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000836AA File Offset: 0x000826AA
		public BigInteger Y
		{
			get
			{
				return this.y.Value;
			}
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x000836B8 File Offset: 0x000826B8
		public override void Encode(BcpgOutputStream bcpgOut)
		{
			bcpgOut.WriteObjects(new BcpgObject[]
			{
				this.p,
				this.g,
				this.y
			});
		}

		// Token: 0x04000F9E RID: 3998
		internal MPInteger p;

		// Token: 0x04000F9F RID: 3999
		internal MPInteger g;

		// Token: 0x04000FA0 RID: 4000
		internal MPInteger y;
	}
}
