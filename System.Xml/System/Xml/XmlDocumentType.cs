using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000D7 RID: 215
	public class XmlDocumentType : XmlLinkedNode
	{
		// Token: 0x06000D18 RID: 3352 RVA: 0x0003A254 File Offset: 0x00039254
		protected internal XmlDocumentType(string name, string publicId, string systemId, string internalSubset, XmlDocument doc) : base(doc)
		{
			this.name = name;
			this.publicId = publicId;
			this.systemId = systemId;
			this.namespaces = true;
			this.internalSubset = internalSubset;
			if (!doc.IsLoading)
			{
				doc.IsLoading = true;
				XmlLoader xmlLoader = new XmlLoader();
				xmlLoader.ParseDocumentType(this);
				doc.IsLoading = false;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0003A2B3 File Offset: 0x000392B3
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0003A2BB File Offset: 0x000392BB
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0003A2C3 File Offset: 0x000392C3
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003A2C7 File Offset: 0x000392C7
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateDocumentType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0003A2EC File Offset: 0x000392EC
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0003A2EF File Offset: 0x000392EF
		public XmlNamedNodeMap Entities
		{
			get
			{
				if (this.entities == null)
				{
					this.entities = new XmlNamedNodeMap(this);
				}
				return this.entities;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0003A30B File Offset: 0x0003930B
		public XmlNamedNodeMap Notations
		{
			get
			{
				if (this.notations == null)
				{
					this.notations = new XmlNamedNodeMap(this);
				}
				return this.notations;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0003A327 File Offset: 0x00039327
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0003A32F File Offset: 0x0003932F
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0003A337 File Offset: 0x00039337
		public string InternalSubset
		{
			get
			{
				return this.internalSubset;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0003A33F File Offset: 0x0003933F
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x0003A347 File Offset: 0x00039347
		internal bool ParseWithNamespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003A350 File Offset: 0x00039350
		public override void WriteTo(XmlWriter w)
		{
			w.WriteDocType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0003A370 File Offset: 0x00039370
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0003A372 File Offset: 0x00039372
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x0003A37A File Offset: 0x0003937A
		internal SchemaInfo DtdSchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x0400092A RID: 2346
		private string name;

		// Token: 0x0400092B RID: 2347
		private string publicId;

		// Token: 0x0400092C RID: 2348
		private string systemId;

		// Token: 0x0400092D RID: 2349
		private string internalSubset;

		// Token: 0x0400092E RID: 2350
		private bool namespaces;

		// Token: 0x0400092F RID: 2351
		private XmlNamedNodeMap entities;

		// Token: 0x04000930 RID: 2352
		private XmlNamedNodeMap notations;

		// Token: 0x04000931 RID: 2353
		private SchemaInfo schemaInfo;
	}
}
