using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000285 RID: 645
	public abstract class XmlSchemaExternal : XmlSchemaObject
	{
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000CF0C1 File Offset: 0x000CD2C1
		// (set) Token: 0x060026BE RID: 9918 RVA: 0x000CF0C9 File Offset: 0x000CD2C9
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

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000CF0D2 File Offset: 0x000CD2D2
		// (set) Token: 0x060026C0 RID: 9920 RVA: 0x000CF0DA File Offset: 0x000CD2DA
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

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x000CF0E3 File Offset: 0x000CD2E3
		// (set) Token: 0x060026C2 RID: 9922 RVA: 0x000CF0EB File Offset: 0x000CD2EB
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

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x000CF0F4 File Offset: 0x000CD2F4
		// (set) Token: 0x060026C4 RID: 9924 RVA: 0x000CF0FC File Offset: 0x000CD2FC
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

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000CF105 File Offset: 0x000CD305
		// (set) Token: 0x060026C6 RID: 9926 RVA: 0x000CF10D File Offset: 0x000CD30D
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

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x000CF116 File Offset: 0x000CD316
		// (set) Token: 0x060026C8 RID: 9928 RVA: 0x000CF11E File Offset: 0x000CD31E
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

		// Token: 0x060026C9 RID: 9929 RVA: 0x000CF127 File Offset: 0x000CD327
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x000CF130 File Offset: 0x000CD330
		// (set) Token: 0x060026CB RID: 9931 RVA: 0x000CF138 File Offset: 0x000CD338
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

		// Token: 0x040010E4 RID: 4324
		private string location;

		// Token: 0x040010E5 RID: 4325
		private Uri baseUri;

		// Token: 0x040010E6 RID: 4326
		private XmlSchema schema;

		// Token: 0x040010E7 RID: 4327
		private string id;

		// Token: 0x040010E8 RID: 4328
		private XmlAttribute[] moreAttributes;

		// Token: 0x040010E9 RID: 4329
		private Compositor compositor;
	}
}
