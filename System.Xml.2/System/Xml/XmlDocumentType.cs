using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000104 RID: 260
	public class XmlDocumentType : XmlLinkedNode
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x0004C088 File Offset: 0x0004A288
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

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0004C0E7 File Offset: 0x0004A2E7
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0004C0EF File Offset: 0x0004A2EF
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0004C0F7 File Offset: 0x0004A2F7
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004C0FB File Offset: 0x0004A2FB
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateDocumentType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0004C120 File Offset: 0x0004A320
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0004C123 File Offset: 0x0004A323
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

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0004C13F File Offset: 0x0004A33F
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

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0004C15B File Offset: 0x0004A35B
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0004C163 File Offset: 0x0004A363
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0004C16B File Offset: 0x0004A36B
		public string InternalSubset
		{
			get
			{
				return this.internalSubset;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0004C173 File Offset: 0x0004A373
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x0004C17B File Offset: 0x0004A37B
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

		// Token: 0x06001257 RID: 4695 RVA: 0x0004C184 File Offset: 0x0004A384
		public override void WriteTo(XmlWriter w)
		{
			w.WriteDocType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004C1A4 File Offset: 0x0004A3A4
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0004C1A6 File Offset: 0x0004A3A6
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x0004C1AE File Offset: 0x0004A3AE
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

		// Token: 0x04000509 RID: 1289
		private string name;

		// Token: 0x0400050A RID: 1290
		private string publicId;

		// Token: 0x0400050B RID: 1291
		private string systemId;

		// Token: 0x0400050C RID: 1292
		private string internalSubset;

		// Token: 0x0400050D RID: 1293
		private bool namespaces;

		// Token: 0x0400050E RID: 1294
		private XmlNamedNodeMap entities;

		// Token: 0x0400050F RID: 1295
		private XmlNamedNodeMap notations;

		// Token: 0x04000510 RID: 1296
		private SchemaInfo schemaInfo;
	}
}
