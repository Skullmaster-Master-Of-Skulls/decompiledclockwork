using System;

namespace System.Xml.Schema
{
	// Token: 0x02000276 RID: 630
	internal sealed class XmlSchemaCollectionNode
	{
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x000CD8A7 File Offset: 0x000CBAA7
		// (set) Token: 0x060025F3 RID: 9715 RVA: 0x000CD8AF File Offset: 0x000CBAAF
		internal string NamespaceURI
		{
			get
			{
				return this.namespaceUri;
			}
			set
			{
				this.namespaceUri = value;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x000CD8B8 File Offset: 0x000CBAB8
		// (set) Token: 0x060025F5 RID: 9717 RVA: 0x000CD8C0 File Offset: 0x000CBAC0
		internal SchemaInfo SchemaInfo
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

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x000CD8C9 File Offset: 0x000CBAC9
		// (set) Token: 0x060025F7 RID: 9719 RVA: 0x000CD8D1 File Offset: 0x000CBAD1
		internal XmlSchema Schema
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

		// Token: 0x04001091 RID: 4241
		private string namespaceUri;

		// Token: 0x04001092 RID: 4242
		private SchemaInfo schemaInfo;

		// Token: 0x04001093 RID: 4243
		private XmlSchema schema;
	}
}
