using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000278 RID: 632
	public class XmlSchemaComplexContent : XmlSchemaContentModel
	{
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x000CD973 File Offset: 0x000CBB73
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x000CD97B File Offset: 0x000CBB7B
		[XmlAttribute("mixed")]
		public bool IsMixed
		{
			get
			{
				return this.isMixed;
			}
			set
			{
				this.isMixed = value;
				this.hasMixedAttribute = true;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x000CD98B File Offset: 0x000CBB8B
		// (set) Token: 0x06002603 RID: 9731 RVA: 0x000CD993 File Offset: 0x000CBB93
		[XmlElement("restriction", typeof(XmlSchemaComplexContentRestriction))]
		[XmlElement("extension", typeof(XmlSchemaComplexContentExtension))]
		public override XmlSchemaContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x000CD99C File Offset: 0x000CBB9C
		[XmlIgnore]
		internal bool HasMixedAttribute
		{
			get
			{
				return this.hasMixedAttribute;
			}
		}

		// Token: 0x04001095 RID: 4245
		private XmlSchemaContent content;

		// Token: 0x04001096 RID: 4246
		private bool isMixed;

		// Token: 0x04001097 RID: 4247
		private bool hasMixedAttribute;
	}
}
