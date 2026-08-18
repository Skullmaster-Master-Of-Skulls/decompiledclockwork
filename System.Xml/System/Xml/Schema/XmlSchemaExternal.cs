using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200024D RID: 589
	public abstract class XmlSchemaExternal : XmlSchemaObject
	{
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x00082F99 File Offset: 0x00081F99
		// (set) Token: 0x06001C37 RID: 7223 RVA: 0x00082FA1 File Offset: 0x00081FA1
		[XmlAttribute("schemaLocation", DataType = "anyURI")]
		public string SchemaLocation
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x00082FAA File Offset: 0x00081FAA
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x00082FB2 File Offset: 0x00081FB2
		[XmlIgnore]
		public XmlSchema Schema
		{
			get
			{
				return this.schema;
			}
			set
			{
				this.schema = value;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x00082FBB File Offset: 0x00081FBB
		// (set) Token: 0x06001C3B RID: 7227 RVA: 0x00082FC3 File Offset: 0x00081FC3
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

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x00082FCC File Offset: 0x00081FCC
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x00082FD4 File Offset: 0x00081FD4
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

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x00082FDD File Offset: 0x00081FDD
		// (set) Token: 0x06001C3F RID: 7231 RVA: 0x00082FE5 File Offset: 0x00081FE5
		[XmlIgnore]
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00082FEE File Offset: 0x00081FEE
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x00082FF6 File Offset: 0x00081FF6
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

		// Token: 0x06001C42 RID: 7234 RVA: 0x00082FFF File Offset: 0x00081FFF
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x00083008 File Offset: 0x00082008
		// (set) Token: 0x06001C44 RID: 7236 RVA: 0x00083010 File Offset: 0x00082010
		internal Compositor Compositor
		{
			get
			{
				return this.compositor;
			}
			set
			{
				this.compositor = value;
			}
		}

		// Token: 0x04001168 RID: 4456
		private string location;

		// Token: 0x04001169 RID: 4457
		private Uri baseUri;

		// Token: 0x0400116A RID: 4458
		private XmlSchema schema;

		// Token: 0x0400116B RID: 4459
		private string id;

		// Token: 0x0400116C RID: 4460
		private XmlAttribute[] moreAttributes;

		// Token: 0x0400116D RID: 4461
		private Compositor compositor;
	}
}
