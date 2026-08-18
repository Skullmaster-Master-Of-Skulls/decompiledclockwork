using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000309 RID: 777
	public class DistributionPoint : Asn1Encodable
	{
		// Token: 0x06001C77 RID: 7287 RVA: 0x000AAB30 File Offset: 0x000A9B30
		public static DistributionPoint GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DistributionPoint.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000AAB40 File Offset: 0x000A9B40
		public static DistributionPoint GetInstance(object obj)
		{
			if (obj == null || obj is DistributionPoint)
			{
				return (DistributionPoint)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DistributionPoint((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DistributionPoint: " + obj.GetType().Name);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000AAB90 File Offset: 0x000A9B90
		private DistributionPoint(Asn1Sequence seq)
		{
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num]);
				switch (instance.TagNo)
				{
				case 0:
					this.distributionPoint = DistributionPointName.GetInstance(instance, true);
					break;
				case 1:
					this.reasons = new ReasonFlags(DerBitString.GetInstance(instance, false));
					break;
				case 2:
					this.cRLIssuer = GeneralNames.GetInstance(instance, false);
					break;
				}
			}
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000AAC0C File Offset: 0x000A9C0C
		public DistributionPoint(DistributionPointName distributionPointName, ReasonFlags reasons, GeneralNames crlIssuer)
		{
			this.distributionPoint = distributionPointName;
			this.reasons = reasons;
			this.cRLIssuer = crlIssuer;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x000AAC29 File Offset: 0x000A9C29
		public DistributionPointName DistributionPointName
		{
			get
			{
				return this.distributionPoint;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x000AAC31 File Offset: 0x000A9C31
		public ReasonFlags Reasons
		{
			get
			{
				return this.reasons;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x000AAC39 File Offset: 0x000A9C39
		public GeneralNames CrlIssuer
		{
			get
			{
				return this.cRLIssuer;
			}
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x000AAC44 File Offset: 0x000A9C44
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (this.distributionPoint != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, this.distributionPoint)
				});
			}
			if (this.reasons != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.reasons)
				});
			}
			if (this.cRLIssuer != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.cRLIssuer)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000AACD4 File Offset: 0x000A9CD4
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistributionPoint: [");
			stringBuilder.Append(newLine);
			if (this.distributionPoint != null)
			{
				this.appendObject(stringBuilder, newLine, "distributionPoint", this.distributionPoint.ToString());
			}
			if (this.reasons != null)
			{
				this.appendObject(stringBuilder, newLine, "reasons", this.reasons.ToString());
			}
			if (this.cRLIssuer != null)
			{
				this.appendObject(stringBuilder, newLine, "cRLIssuer", this.cRLIssuer.ToString());
			}
			stringBuilder.Append("]");
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000AAD7C File Offset: 0x000A9D7C
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

		// Token: 0x040013A4 RID: 5028
		internal readonly DistributionPointName distributionPoint;

		// Token: 0x040013A5 RID: 5029
		internal readonly ReasonFlags reasons;

		// Token: 0x040013A6 RID: 5030
		internal readonly GeneralNames cRLIssuer;
	}
}
