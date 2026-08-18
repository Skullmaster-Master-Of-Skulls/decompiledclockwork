using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002A1 RID: 673
	public class XmlSchemaNotation : XmlSchemaAnnotated
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x000CF640 File Offset: 0x000CD840
		// (set) Token: 0x06002731 RID: 10033 RVA: 0x000CF648 File Offset: 0x000CD848
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

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x000CF651 File Offset: 0x000CD851
		// (set) Token: 0x06002733 RID: 10035 RVA: 0x000CF659 File Offset: 0x000CD859
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

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000CF662 File Offset: 0x000CD862
		// (set) Token: 0x06002735 RID: 10037 RVA: 0x000CF66A File Offset: 0x000CD86A
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

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x000CF673 File Offset: 0x000CD873
		// (set) Token: 0x06002737 RID: 10039 RVA: 0x000CF67B File Offset: 0x000CD87B
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

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x000CF684 File Offset: 0x000CD884
		// (set) Token: 0x06002739 RID: 10041 RVA: 0x000CF68C File Offset: 0x000CD88C
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

		// Token: 0x0400111A RID: 4378
		private string name;

		// Token: 0x0400111B RID: 4379
		private string publicId;

		// Token: 0x0400111C RID: 4380
		private string systemId;

		// Token: 0x0400111D RID: 4381
		private XmlQualifiedName qname = XmlQualifiedName.Empty;
	}
}
