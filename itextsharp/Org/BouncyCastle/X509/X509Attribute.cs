using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000294 RID: 660
	public class X509Attribute : Asn1Encodable
	{
		// Token: 0x060018EE RID: 6382 RVA: 0x00092CF5 File Offset: 0x00091CF5
		internal X509Attribute(Asn1Encodable at)
		{
			this.attr = AttributeX509.GetInstance(at);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00092D09 File Offset: 0x00091D09
		public X509Attribute(string oid, Asn1Encodable value)
		{
			this.attr = new AttributeX509(new DerObjectIdentifier(oid), new DerSet(value));
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00092D28 File Offset: 0x00091D28
		public X509Attribute(string oid, Asn1EncodableVector value)
		{
			this.attr = new AttributeX509(new DerObjectIdentifier(oid), new DerSet(value));
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x00092D47 File Offset: 0x00091D47
		public string Oid
		{
			get
			{
				return this.attr.AttrType.Id;
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00092D5C File Offset: 0x00091D5C
		public Asn1Encodable[] GetValues()
		{
			Asn1Set attrValues = this.attr.AttrValues;
			Asn1Encodable[] array = new Asn1Encodable[attrValues.Count];
			for (int num = 0; num != attrValues.Count; num++)
			{
				array[num] = attrValues[num];
			}
			return array;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00092D9D File Offset: 0x00091D9D
		public override Asn1Object ToAsn1Object()
		{
			return this.attr.ToAsn1Object();
		}

		// Token: 0x040010CD RID: 4301
		private readonly AttributeX509 attr;
	}
}
