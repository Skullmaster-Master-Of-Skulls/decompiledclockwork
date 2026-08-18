using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000206 RID: 518
	public class DistributionPointName : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x060013E7 RID: 5095 RVA: 0x000728FA File Offset: 0x000718FA
		public static DistributionPointName GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DistributionPointName.GetInstance(Asn1TaggedObject.GetInstance(obj, true));
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00072908 File Offset: 0x00071908
		public static DistributionPointName GetInstance(object obj)
		{
			if (obj == null || obj is DistributionPointName)
			{
				return (DistributionPointName)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new DistributionPointName((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0007295A File Offset: 0x0007195A
		public DistributionPointName(int type, Asn1Encodable name)
		{
			this.type = type;
			this.name = name;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00072970 File Offset: 0x00071970
		public DistributionPointName(GeneralNames name) : this(0, name)
		{
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0007297A File Offset: 0x0007197A
		public int PointType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00072982 File Offset: 0x00071982
		public Asn1Encodable Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0007298A File Offset: 0x0007198A
		public DistributionPointName(Asn1TaggedObject obj)
		{
			this.type = obj.TagNo;
			if (this.type == 0)
			{
				this.name = GeneralNames.GetInstance(obj, false);
				return;
			}
			this.name = Asn1Set.GetInstance(obj, false);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000729C1 File Offset: 0x000719C1
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.type, this.name);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x000729D8 File Offset: 0x000719D8
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistributionPointName: [");
			stringBuilder.Append(newLine);
			if (this.type == 0)
			{
				this.appendObject(stringBuilder, newLine, "fullName", this.name.ToString());
			}
			else
			{
				this.appendObject(stringBuilder, newLine, "nameRelativeToCRLIssuer", this.name.ToString());
			}
			stringBuilder.Append("]");
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00072A5C File Offset: 0x00071A5C
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

		// Token: 0x04000DC5 RID: 3525
		public const int FullName = 0;

		// Token: 0x04000DC6 RID: 3526
		public const int NameRelativeToCrlIssuer = 1;

		// Token: 0x04000DC7 RID: 3527
		internal readonly Asn1Encodable name;

		// Token: 0x04000DC8 RID: 3528
		internal readonly int type;
	}
}
