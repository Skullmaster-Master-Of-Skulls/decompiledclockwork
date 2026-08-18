using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000560 RID: 1376
	public class CrlNumber : DerInteger
	{
		// Token: 0x06002F57 RID: 12119 RVA: 0x00125FD4 File Offset: 0x00124FD4
		public CrlNumber(BigInteger number) : base(number)
		{
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x00125FDD File Offset: 0x00124FDD
		public BigInteger Number
		{
			get
			{
				return base.PositiveValue;
			}
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x00125FE5 File Offset: 0x00124FE5
		public override string ToString()
		{
			return "CRLNumber: " + this.Number;
		}
	}
}
