using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000402 RID: 1026
	public class GeneralSubtree : Asn1Encodable
	{
		// Token: 0x06002309 RID: 8969 RVA: 0x000D7F8C File Offset: 0x000D6F8C
		private GeneralSubtree(Asn1Sequence seq)
		{
			this.baseName = GeneralName.GetInstance(seq[0]);
			switch (seq.Count)
			{
			case 1:
				return;
			case 2:
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[1]);
				switch (instance.TagNo)
				{
				case 0:
					this.minimum = DerInteger.GetInstance(instance, false);
					return;
				case 1:
					this.maximum = DerInteger.GetInstance(instance, false);
					return;
				default:
					throw new ArgumentException("Bad tag number: " + instance.TagNo);
				}
				break;
			}
			case 3:
				this.minimum = DerInteger.GetInstance(Asn1TaggedObject.GetInstance(seq[1]));
				this.maximum = DerInteger.GetInstance(Asn1TaggedObject.GetInstance(seq[2]));
				return;
			default:
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000D8076 File Offset: 0x000D7076
		public GeneralSubtree(GeneralName baseName, BigInteger minimum, BigInteger maximum)
		{
			this.baseName = baseName;
			if (minimum != null)
			{
				this.minimum = new DerInteger(minimum);
			}
			if (maximum != null)
			{
				this.maximum = new DerInteger(maximum);
			}
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000D80A3 File Offset: 0x000D70A3
		public GeneralSubtree(GeneralName baseName) : this(baseName, null, null)
		{
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000D80AE File Offset: 0x000D70AE
		public static GeneralSubtree GetInstance(Asn1TaggedObject o, bool isExplicit)
		{
			return new GeneralSubtree(Asn1Sequence.GetInstance(o, isExplicit));
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000D80BC File Offset: 0x000D70BC
		public static GeneralSubtree GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is GeneralSubtree)
			{
				return (GeneralSubtree)obj;
			}
			return new GeneralSubtree(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000D80DD File Offset: 0x000D70DD
		public GeneralName Base
		{
			get
			{
				return this.baseName;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x000D80E5 File Offset: 0x000D70E5
		public BigInteger Minimum
		{
			get
			{
				if (this.minimum != null)
				{
					return this.minimum.Value;
				}
				return BigInteger.Zero;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x000D8100 File Offset: 0x000D7100
		public BigInteger Maximum
		{
			get
			{
				if (this.maximum != null)
				{
					return this.maximum.Value;
				}
				return null;
			}
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000D8118 File Offset: 0x000D7118
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.baseName
			});
			if (this.minimum != null && this.minimum.Value.SignValue != 0)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.minimum)
				});
			}
			if (this.maximum != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.maximum)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040017DF RID: 6111
		private readonly GeneralName baseName;

		// Token: 0x040017E0 RID: 6112
		private readonly DerInteger minimum;

		// Token: 0x040017E1 RID: 6113
		private readonly DerInteger maximum;
	}
}
