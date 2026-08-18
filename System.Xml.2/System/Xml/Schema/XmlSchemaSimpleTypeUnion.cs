using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002B2 RID: 690
	public class XmlSchemaSimpleTypeUnion : XmlSchemaSimpleTypeContent
	{
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x000D2019 File Offset: 0x000D0219
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaObjectCollection BaseTypes
		{
			get
			{
				return this.baseTypes;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x000D2021 File Offset: 0x000D0221
		// (set) Token: 0x060027F7 RID: 10231 RVA: 0x000D2029 File Offset: 0x000D0229
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

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x000D2032 File Offset: 0x000D0232
		[XmlIgnore]
		public XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.baseMemberTypes;
			}
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000D203A File Offset: 0x000D023A
		internal void SetBaseMemberTypes(XmlSchemaSimpleType[] baseMemberTypes)
		{
			this.baseMemberTypes = baseMemberTypes;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000D2044 File Offset: 0x000D0244
		internal override XmlSchemaObject Clone()
		{
			if (this.memberTypes != null && this.memberTypes.Length != 0)
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

		// Token: 0x04001156 RID: 4438
		private XmlSchemaObjectCollection baseTypes = new XmlSchemaObjectCollection();

		// Token: 0x04001157 RID: 4439
		private XmlQualifiedName[] memberTypes;

		// Token: 0x04001158 RID: 4440
		private XmlSchemaSimpleType[] baseMemberTypes;
	}
}
