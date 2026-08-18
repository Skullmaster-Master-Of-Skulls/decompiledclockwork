using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000268 RID: 616
	public class XmlSchemaNotation : XmlSchemaAnnotated
	{
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x00083509 File Offset: 0x00082509
		// (set) Token: 0x06001CA6 RID: 7334 RVA: 0x00083511 File Offset: 0x00082511
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x0008351A File Offset: 0x0008251A
		// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x00083522 File Offset: 0x00082522
		[XmlAttribute("public")]
		public string Public
		{
			get
			{
				return this.publicId;
			}
			set
			{
				this.publicId = value;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x0008352B File Offset: 0x0008252B
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x00083533 File Offset: 0x00082533
		[XmlAttribute("system")]
		public string System
		{
			get
			{
				return this.systemId;
			}
			set
			{
				this.systemId = value;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x0008353C File Offset: 0x0008253C
		// (set) Token: 0x06001CAC RID: 7340 RVA: 0x00083544 File Offset: 0x00082544
		[XmlIgnore]
		internal XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
			set
			{
				this.qname = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x0008354D File Offset: 0x0008254D
		// (set) Token: 0x06001CAE RID: 7342 RVA: 0x00083555 File Offset: 0x00082555
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x0400119E RID: 4510
		private string name;

		// Token: 0x0400119F RID: 4511
		private string publicId;

		// Token: 0x040011A0 RID: 4512
		private string systemId;

		// Token: 0x040011A1 RID: 4513
		private XmlQualifiedName qname = XmlQualifiedName.Empty;
	}
}
