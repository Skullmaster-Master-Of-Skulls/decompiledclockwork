using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000241 RID: 577
	public class XmlSchemaComplexContent : XmlSchemaContentModel
	{
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x00081AA7 File Offset: 0x00080AA7
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x00081AAF File Offset: 0x00080AAF
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

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x00081ABF File Offset: 0x00080ABF
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x00081AC7 File Offset: 0x00080AC7
		[XmlElement("extension", typeof(XmlSchemaComplexContentExtension))]
		[XmlElement("restriction", typeof(XmlSchemaComplexContentRestriction))]
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

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00081AD0 File Offset: 0x00080AD0
		[XmlIgnore]
		internal bool HasMixedAttribute
		{
			get
			{
				return this.hasMixedAttribute;
			}
		}

		// Token: 0x0400110E RID: 4366
		private XmlSchemaContent content;

		// Token: 0x0400110F RID: 4367
		private bool isMixed;

		// Token: 0x04001110 RID: 4368
		private bool hasMixedAttribute;
	}
}
