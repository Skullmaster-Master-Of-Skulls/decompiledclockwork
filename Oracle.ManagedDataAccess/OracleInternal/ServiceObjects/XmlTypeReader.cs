using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001BD RID: 445
	public class XmlTypeReader : XmlTextReader
	{
		// Token: 0x06001149 RID: 4425 RVA: 0x000BF38C File Offset: 0x000BD58C
		public XmlTypeReader(TextReader textReader) : base(textReader)
		{
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000BF3A0 File Offset: 0x000BD5A0
		public XmlTypeReader(Stream clob) : base(clob)
		{
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x000BF3B4 File Offset: 0x000BD5B4
		public Hashtable CollectedNamespaces
		{
			get
			{
				return this.m_namespaces;
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000BF3BC File Offset: 0x000BD5BC
		public override bool Read()
		{
			bool result = base.Read();
			if (base.NodeType == XmlNodeType.Element)
			{
				if (!string.IsNullOrEmpty(base.NamespaceURI) && !this.m_namespaces.ContainsKey(base.Prefix))
				{
					this.m_namespaces.Add(base.Prefix, base.NamespaceURI);
				}
				if (!this.m_bGetSchema)
				{
					for (int i = 0; i < base.AttributeCount; i++)
					{
						this.MoveToAttribute(i);
						if (string.Compare(base.Prefix, "xmlns", true) == 0)
						{
							if (string.Compare(base.LocalName, "xsi", true) == 0)
							{
								this.m_schemaXmlns = base.Value;
							}
						}
						else if (string.Compare(base.Prefix, "xsi", true) == 0)
						{
							if (string.Compare(base.LocalName, "SchemaLocation", true) == 0)
							{
								this.m_bHasTargetNamespace = true;
								this.m_bGetSchema = true;
								this.m_schemaURL = base.Value;
							}
							else if (string.Compare(base.LocalName, "noNamespaceSchemaLocation", true) == 0)
							{
								this.m_bHasTargetNamespace = false;
								this.m_bGetSchema = true;
								this.m_schemaURL = base.Value;
							}
						}
					}
					base.MoveToElement();
				}
			}
			return result;
		}

		// Token: 0x04001380 RID: 4992
		internal const string XMLNSNotation = "xmlns";

		// Token: 0x04001381 RID: 4993
		internal const string XSINotation = "xsi";

		// Token: 0x04001382 RID: 4994
		internal const string SchemaLocationNotatation = "noNamespaceSchemaLocation";

		// Token: 0x04001383 RID: 4995
		internal const string NSSchemaLocationNotatation = "SchemaLocation";

		// Token: 0x04001384 RID: 4996
		internal Hashtable m_namespaces = new Hashtable();

		// Token: 0x04001385 RID: 4997
		private bool m_bGetSchema;

		// Token: 0x04001386 RID: 4998
		internal bool m_bHasTargetNamespace;

		// Token: 0x04001387 RID: 4999
		internal string m_schemaURL;

		// Token: 0x04001388 RID: 5000
		internal string m_schemaXmlns;
	}
}
