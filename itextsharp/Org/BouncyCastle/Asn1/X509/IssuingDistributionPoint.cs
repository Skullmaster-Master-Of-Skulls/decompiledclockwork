using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000308 RID: 776
	public class IssuingDistributionPoint : Asn1Encodable
	{
		// Token: 0x06001C6A RID: 7274 RVA: 0x000AA721 File Offset: 0x000A9721
		public static IssuingDistributionPoint GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return IssuingDistributionPoint.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000AA730 File Offset: 0x000A9730
		public static IssuingDistributionPoint GetInstance(object obj)
		{
			if (obj == null || obj is IssuingDistributionPoint)
			{
				return (IssuingDistributionPoint)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new IssuingDistributionPoint((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x000AA784 File Offset: 0x000A9784
		public IssuingDistributionPoint(DistributionPointName distributionPoint, bool onlyContainsUserCerts, bool onlyContainsCACerts, ReasonFlags onlySomeReasons, bool indirectCRL, bool onlyContainsAttributeCerts)
		{
			this._distributionPoint = distributionPoint;
			this._indirectCRL = indirectCRL;
			this._onlyContainsAttributeCerts = onlyContainsAttributeCerts;
			this._onlyContainsCACerts = onlyContainsCACerts;
			this._onlyContainsUserCerts = onlyContainsUserCerts;
			this._onlySomeReasons = onlySomeReasons;
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (distributionPoint != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, distributionPoint)
				});
			}
			if (onlyContainsUserCerts)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, DerBoolean.True)
				});
			}
			if (onlyContainsCACerts)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, DerBoolean.True)
				});
			}
			if (onlySomeReasons != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 3, onlySomeReasons)
				});
			}
			if (indirectCRL)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 4, DerBoolean.True)
				});
			}
			if (onlyContainsAttributeCerts)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 5, DerBoolean.True)
				});
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000AA8A4 File Offset: 0x000A98A4
		private IssuingDistributionPoint(Asn1Sequence seq)
		{
			this.seq = seq;
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this._distributionPoint = DistributionPointName.GetInstance(instance, true);
					break;
				case 1:
					this._onlyContainsUserCerts = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				case 2:
					this._onlyContainsCACerts = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				case 3:
					this._onlySomeReasons = new ReasonFlags(DerBitString.GetInstance(instance, false));
					break;
				case 4:
					this._indirectCRL = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				case 5:
					this._onlyContainsAttributeCerts = DerBoolean.GetInstance(instance, false).IsTrue;
					break;
				default:
					throw new ArgumentException("unknown tag in IssuingDistributionPoint");
				}
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x000AA987 File Offset: 0x000A9987
		public bool OnlyContainsUserCerts
		{
			get
			{
				return this._onlyContainsUserCerts;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x000AA98F File Offset: 0x000A998F
		public bool OnlyContainsCACerts
		{
			get
			{
				return this._onlyContainsCACerts;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x000AA997 File Offset: 0x000A9997
		public bool IsIndirectCrl
		{
			get
			{
				return this._indirectCRL;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x000AA99F File Offset: 0x000A999F
		public bool OnlyContainsAttributeCerts
		{
			get
			{
				return this._onlyContainsAttributeCerts;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x000AA9A7 File Offset: 0x000A99A7
		public DistributionPointName DistributionPoint
		{
			get
			{
				return this._distributionPoint;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x000AA9AF File Offset: 0x000A99AF
		public ReasonFlags OnlySomeReasons
		{
			get
			{
				return this._onlySomeReasons;
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000AA9B7 File Offset: 0x000A99B7
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000AA9C0 File Offset: 0x000A99C0
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IssuingDistributionPoint: [");
			stringBuilder.Append(newLine);
			if (this._distributionPoint != null)
			{
				this.appendObject(stringBuilder, newLine, "distributionPoint", this._distributionPoint.ToString());
			}
			if (this._onlyContainsUserCerts)
			{
				this.appendObject(stringBuilder, newLine, "onlyContainsUserCerts", this._onlyContainsUserCerts.ToString());
			}
			if (this._onlyContainsCACerts)
			{
				this.appendObject(stringBuilder, newLine, "onlyContainsCACerts", this._onlyContainsCACerts.ToString());
			}
			if (this._onlySomeReasons != null)
			{
				this.appendObject(stringBuilder, newLine, "onlySomeReasons", this._onlySomeReasons.ToString());
			}
			if (this._onlyContainsAttributeCerts)
			{
				this.appendObject(stringBuilder, newLine, "onlyContainsAttributeCerts", this._onlyContainsAttributeCerts.ToString());
			}
			if (this._indirectCRL)
			{
				this.appendObject(stringBuilder, newLine, "indirectCRL", this._indirectCRL.ToString());
			}
			stringBuilder.Append("]");
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000AAAD8 File Offset: 0x000A9AD8
		private void appendObject(StringBuilder buf, string sep, string name, string val)
		{
			string value = "    ";
			buf.Append(value);
			buf.Append(name);
			buf.Append(":");
			buf.Append(sep);
			buf.Append(value);
			buf.Append(value);
			buf.Append(val);
			buf.Append(sep);
		}

		// Token: 0x0400139D RID: 5021
		private readonly DistributionPointName _distributionPoint;

		// Token: 0x0400139E RID: 5022
		private readonly bool _onlyContainsUserCerts;

		// Token: 0x0400139F RID: 5023
		private readonly bool _onlyContainsCACerts;

		// Token: 0x040013A0 RID: 5024
		private readonly ReasonFlags _onlySomeReasons;

		// Token: 0x040013A1 RID: 5025
		private readonly bool _indirectCRL;

		// Token: 0x040013A2 RID: 5026
		private readonly bool _onlyContainsAttributeCerts;

		// Token: 0x040013A3 RID: 5027
		private readonly Asn1Sequence seq;
	}
}
