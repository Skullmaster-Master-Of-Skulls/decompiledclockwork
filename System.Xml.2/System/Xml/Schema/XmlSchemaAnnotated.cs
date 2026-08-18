using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200026C RID: 620
	public class XmlSchemaAnnotated : XmlSchemaObject
	{
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x000CCBCA File Offset: 0x000CADCA
		// (set) Token: 0x0600256A RID: 9578 RVA: 0x000CCBD2 File Offset: 0x000CADD2
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x000CCBDB File Offset: 0x000CADDB
		// (set) Token: 0x0600256C RID: 9580 RVA: 0x000CCBE3 File Offset: 0x000CADE3
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		public XmlSchemaAnnotation Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x000CCBEC File Offset: 0x000CADEC
		// (set) Token: 0x0600256E RID: 9582 RVA: 0x000CCBF4 File Offset: 0x000CADF4
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x000CCBFD File Offset: 0x000CADFD
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x000CCC05 File Offset: 0x000CAE05
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000CCC0E File Offset: 0x000CAE0E
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000CCC17 File Offset: 0x000CAE17
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x04001066 RID: 4198
		private string id;

		// Token: 0x04001067 RID: 4199
		private XmlSchemaAnnotation annotation;

		// Token: 0x04001068 RID: 4200
		private XmlAttribute[] moreAttributes;
	}
}
