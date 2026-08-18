using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023E RID: 574
	internal sealed class XmlSchemaCollectionNode
	{
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x000819D3 File Offset: 0x000809D3
		// (set) Token: 0x06001B6B RID: 7019 RVA: 0x000819DB File Offset: 0x000809DB
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

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x000819E4 File Offset: 0x000809E4
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x000819EC File Offset: 0x000809EC
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

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x000819F5 File Offset: 0x000809F5
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x000819FD File Offset: 0x000809FD
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

		// Token: 0x0400110A RID: 4362
		private string namespaceUri;

		// Token: 0x0400110B RID: 4363
		private SchemaInfo schemaInfo;

		// Token: 0x0400110C RID: 4364
		private XmlSchema schema;
	}
}
