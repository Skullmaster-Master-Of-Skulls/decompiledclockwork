using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200027D RID: 637
	public class XmlSchemaSimpleTypeUnion : XmlSchemaSimpleTypeContent
	{
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00085F0D File Offset: 0x00084F0D
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaObjectCollection BaseTypes
		{
			get
			{
				return this.baseTypes;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00085F15 File Offset: 0x00084F15
		// (set) Token: 0x06001D5E RID: 7518 RVA: 0x00085F1D File Offset: 0x00084F1D
		[XmlAttribute("memberTypes")]
		public XmlQualifiedName[] MemberTypes
		{
			get
			{
				return this.memberTypes;
			}
			set
			{
				this.memberTypes = value;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00085F26 File Offset: 0x00084F26
		[XmlIgnore]
		public XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.baseMemberTypes;
			}
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x00085F2E File Offset: 0x00084F2E
		internal void SetBaseMemberTypes(XmlSchemaSimpleType[] baseMemberTypes)
		{
			this.baseMemberTypes = baseMemberTypes;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00085F38 File Offset: 0x00084F38
		internal override XmlSchemaObject Clone()
		{
			if (this.memberTypes != null && this.memberTypes.Length > 0)
			{
				XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = (XmlSchemaSimpleTypeUnion)base.MemberwiseClone();
				XmlQualifiedName[] array = new XmlQualifiedName[this.memberTypes.Length];
				for (int i = 0; i < this.memberTypes.Length; i++)
				{
					array[i] = this.memberTypes[i].Clone();
				}
				xmlSchemaSimpleTypeUnion.MemberTypes = array;
				return xmlSchemaSimpleTypeUnion;
			}
			return this;
		}

		// Token: 0x040011E0 RID: 4576
		private XmlSchemaObjectCollection baseTypes = new XmlSchemaObjectCollection();

		// Token: 0x040011E1 RID: 4577
		private XmlQualifiedName[] memberTypes;

		// Token: 0x040011E2 RID: 4578
		private XmlSchemaSimpleType[] baseMemberTypes;
	}
}
