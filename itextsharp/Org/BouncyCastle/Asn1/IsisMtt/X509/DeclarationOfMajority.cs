using System;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000149 RID: 329
	public class DeclarationOfMajority : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000BDD RID: 3037 RVA: 0x00041FC8 File Offset: 0x00040FC8
		public DeclarationOfMajority(int notYoungerThan)
		{
			this.declaration = new DerTaggedObject(false, 0, new DerInteger(notYoungerThan));
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00041FE4 File Offset: 0x00040FE4
		public DeclarationOfMajority(bool fullAge, string country)
		{
			if (country.Length > 2)
			{
				throw new ArgumentException("country can only be 2 characters");
			}
			DerPrintableString derPrintableString = new DerPrintableString(country, true);
			DerSequence obj;
			if (fullAge)
			{
				obj = new DerSequence(derPrintableString);
			}
			else
			{
				obj = new DerSequence(new Asn1Encodable[]
				{
					DerBoolean.False,
					derPrintableString
				});
			}
			this.declaration = new DerTaggedObject(false, 1, obj);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00042047 File Offset: 0x00041047
		public DeclarationOfMajority(DerGeneralizedTime dateOfBirth)
		{
			this.declaration = new DerTaggedObject(false, 2, dateOfBirth);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00042060 File Offset: 0x00041060
		public static DeclarationOfMajority GetInstance(object obj)
		{
			if (obj == null || obj is DeclarationOfMajority)
			{
				return (DeclarationOfMajority)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new DeclarationOfMajority((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000420B2 File Offset: 0x000410B2
		private DeclarationOfMajority(Asn1TaggedObject o)
		{
			if (o.TagNo > 2)
			{
				throw new ArgumentException("Bad tag number: " + o.TagNo);
			}
			this.declaration = o;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x000420E5 File Offset: 0x000410E5
		public override Asn1Object ToAsn1Object()
		{
			return this.declaration;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000420ED File Offset: 0x000410ED
		public DeclarationOfMajority.Choice Type
		{
			get
			{
				return (DeclarationOfMajority.Choice)this.declaration.TagNo;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x000420FC File Offset: 0x000410FC
		public virtual int NotYoungerThan
		{
			get
			{
				DeclarationOfMajority.Choice tagNo = (DeclarationOfMajority.Choice)this.declaration.TagNo;
				if (tagNo == DeclarationOfMajority.Choice.NotYoungerThan)
				{
					return DerInteger.GetInstance(this.declaration, false).Value.IntValue;
				}
				return -1;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00042134 File Offset: 0x00041134
		public virtual Asn1Sequence FullAgeAtCountry
		{
			get
			{
				DeclarationOfMajority.Choice tagNo = (DeclarationOfMajority.Choice)this.declaration.TagNo;
				if (tagNo == DeclarationOfMajority.Choice.FullAgeAtCountry)
				{
					return Asn1Sequence.GetInstance(this.declaration, false);
				}
				return null;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00042160 File Offset: 0x00041160
		public virtual DerGeneralizedTime DateOfBirth
		{
			get
			{
				DeclarationOfMajority.Choice tagNo = (DeclarationOfMajority.Choice)this.declaration.TagNo;
				if (tagNo == DeclarationOfMajority.Choice.DateOfBirth)
				{
					return DerGeneralizedTime.GetInstance(this.declaration, false);
				}
				return null;
			}
		}

		// Token: 0x04000973 RID: 2419
		private readonly Asn1TaggedObject declaration;

		// Token: 0x0200014A RID: 330
		public enum Choice
		{
			// Token: 0x04000975 RID: 2421
			NotYoungerThan,
			// Token: 0x04000976 RID: 2422
			FullAgeAtCountry,
			// Token: 0x04000977 RID: 2423
			DateOfBirth
		}
	}
}
