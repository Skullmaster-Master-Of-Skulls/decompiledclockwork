using System;
using System.Collections.Generic;
using System.IO;
using MS.Internal.Xml.Cache;

namespace System.Xml.XPath
{
	// Token: 0x020002E1 RID: 737
	public class XPathDocument : IXPathNavigable
	{
		// Token: 0x06002C1C RID: 11292 RVA: 0x000E89D5 File Offset: 0x000E6BD5
		internal XPathDocument()
		{
			this.nameTable = new NameTable();
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000E89E8 File Offset: 0x000E6BE8
		internal XPathDocument(XmlNameTable nameTable)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			this.nameTable = nameTable;
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000E8A05 File Offset: 0x000E6C05
		public XPathDocument(XmlReader reader) : this(reader, XmlSpace.Default)
		{
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000E8A0F File Offset: 0x000E6C0F
		public XPathDocument(XmlReader reader, XmlSpace space)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.LoadFromReader(reader, space);
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000E8A30 File Offset: 0x000E6C30
		public XPathDocument(TextReader textReader)
		{
			XmlTextReaderImpl xmlTextReaderImpl = this.SetupReader(new XmlTextReaderImpl(string.Empty, textReader));
			try
			{
				this.LoadFromReader(xmlTextReaderImpl, XmlSpace.Default);
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000E8A78 File Offset: 0x000E6C78
		public XPathDocument(Stream stream)
		{
			XmlTextReaderImpl xmlTextReaderImpl = this.SetupReader(new XmlTextReaderImpl(string.Empty, stream));
			try
			{
				this.LoadFromReader(xmlTextReaderImpl, XmlSpace.Default);
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000E8AC0 File Offset: 0x000E6CC0
		public XPathDocument(string uri) : this(uri, XmlSpace.Default)
		{
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000E8ACC File Offset: 0x000E6CCC
		public XPathDocument(string uri, XmlSpace space)
		{
			XmlTextReaderImpl xmlTextReaderImpl = this.SetupReader(new XmlTextReaderImpl(uri));
			try
			{
				this.LoadFromReader(xmlTextReaderImpl, space);
			}
			finally
			{
				xmlTextReaderImpl.Close();
			}
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000E8B10 File Offset: 0x000E6D10
		internal XmlRawWriter LoadFromWriter(XPathDocument.LoadFlags flags, string baseUri)
		{
			return new XPathDocumentBuilder(this, null, baseUri, flags);
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000E8B1C File Offset: 0x000E6D1C
		internal void LoadFromReader(XmlReader reader, XmlSpace space)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo == null || !xmlLineInfo.HasLineInfo())
			{
				xmlLineInfo = null;
			}
			this.hasLineInfo = (xmlLineInfo != null);
			this.nameTable = reader.NameTable;
			XPathDocumentBuilder xpathDocumentBuilder = new XPathDocumentBuilder(this, xmlLineInfo, reader.BaseURI, XPathDocument.LoadFlags.None);
			try
			{
				bool flag = reader.ReadState == ReadState.Initial;
				int depth = reader.Depth;
				string text = this.nameTable.Get("http://www.w3.org/2000/xmlns/");
				if (!flag || reader.Read())
				{
					while (flag || reader.Depth >= depth)
					{
						switch (reader.NodeType)
						{
						case XmlNodeType.Element:
						{
							bool isEmptyElement = reader.IsEmptyElement;
							xpathDocumentBuilder.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI, reader.BaseURI);
							while (reader.MoveToNextAttribute())
							{
								string namespaceURI = reader.NamespaceURI;
								if (namespaceURI == text)
								{
									if (reader.Prefix.Length == 0)
									{
										xpathDocumentBuilder.WriteNamespaceDeclaration(string.Empty, reader.Value);
									}
									else
									{
										xpathDocumentBuilder.WriteNamespaceDeclaration(reader.LocalName, reader.Value);
									}
								}
								else
								{
									xpathDocumentBuilder.WriteStartAttribute(reader.Prefix, reader.LocalName, namespaceURI);
									xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.Text);
									xpathDocumentBuilder.WriteEndAttribute();
								}
							}
							if (isEmptyElement)
							{
								xpathDocumentBuilder.WriteEndElement(true);
							}
							break;
						}
						case XmlNodeType.Text:
						case XmlNodeType.CDATA:
							xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.Text);
							break;
						case XmlNodeType.EntityReference:
							reader.ResolveEntity();
							break;
						case XmlNodeType.ProcessingInstruction:
							xpathDocumentBuilder.WriteProcessingInstruction(reader.LocalName, reader.Value, reader.BaseURI);
							break;
						case XmlNodeType.Comment:
							xpathDocumentBuilder.WriteComment(reader.Value);
							break;
						case XmlNodeType.DocumentType:
						{
							IDtdInfo dtdInfo = reader.DtdInfo;
							if (dtdInfo != null)
							{
								xpathDocumentBuilder.CreateIdTables(dtdInfo);
							}
							break;
						}
						case XmlNodeType.Whitespace:
							goto IL_1C6;
						case XmlNodeType.SignificantWhitespace:
							if (reader.XmlSpace != XmlSpace.Preserve)
							{
								goto IL_1C6;
							}
							xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.SignificantWhitespace);
							break;
						case XmlNodeType.EndElement:
							xpathDocumentBuilder.WriteEndElement(false);
							break;
						}
						IL_228:
						if (!reader.Read())
						{
							break;
						}
						continue;
						IL_1C6:
						if (space == XmlSpace.Preserve && (!flag || reader.Depth != 0))
						{
							xpathDocumentBuilder.WriteString(reader.Value, TextBlockType.Whitespace);
							goto IL_228;
						}
						goto IL_228;
					}
				}
			}
			finally
			{
				xpathDocumentBuilder.Close();
			}
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000E8D84 File Offset: 0x000E6F84
		public XPathNavigator CreateNavigator()
		{
			return new XPathDocumentNavigator(this.pageRoot, this.idxRoot, null, 0);
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x000E8D99 File Offset: 0x000E6F99
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000E8DA1 File Offset: 0x000E6FA1
		internal bool HasLineInfo
		{
			get
			{
				return this.hasLineInfo;
			}
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000E8DA9 File Offset: 0x000E6FA9
		internal int GetCollapsedTextNode(out XPathNode[] pageText)
		{
			pageText = this.pageText;
			return this.idxText;
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000E8DB9 File Offset: 0x000E6FB9
		internal void SetCollapsedTextNode(XPathNode[] pageText, int idxText)
		{
			this.pageText = pageText;
			this.idxText = idxText;
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000E8DC9 File Offset: 0x000E6FC9
		internal int GetRootNode(out XPathNode[] pageRoot)
		{
			pageRoot = this.pageRoot;
			return this.idxRoot;
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000E8DD9 File Offset: 0x000E6FD9
		internal void SetRootNode(XPathNode[] pageRoot, int idxRoot)
		{
			this.pageRoot = pageRoot;
			this.idxRoot = idxRoot;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000E8DE9 File Offset: 0x000E6FE9
		internal int GetXmlNamespaceNode(out XPathNode[] pageXmlNmsp)
		{
			pageXmlNmsp = this.pageXmlNmsp;
			return this.idxXmlNmsp;
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000E8DF9 File Offset: 0x000E6FF9
		internal void SetXmlNamespaceNode(XPathNode[] pageXmlNmsp, int idxXmlNmsp)
		{
			this.pageXmlNmsp = pageXmlNmsp;
			this.idxXmlNmsp = idxXmlNmsp;
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000E8E09 File Offset: 0x000E7009
		internal void AddNamespace(XPathNode[] pageElem, int idxElem, XPathNode[] pageNmsp, int idxNmsp)
		{
			if (this.mapNmsp == null)
			{
				this.mapNmsp = new Dictionary<XPathNodeRef, XPathNodeRef>();
			}
			this.mapNmsp.Add(new XPathNodeRef(pageElem, idxElem), new XPathNodeRef(pageNmsp, idxNmsp));
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000E8E38 File Offset: 0x000E7038
		internal int LookupNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			XPathNodeRef key = new XPathNodeRef(pageElem, idxElem);
			if (this.mapNmsp == null || !this.mapNmsp.ContainsKey(key))
			{
				pageNmsp = null;
				return 0;
			}
			key = this.mapNmsp[key];
			pageNmsp = key.Page;
			return key.Index;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000E8E86 File Offset: 0x000E7086
		internal void AddIdElement(string id, XPathNode[] pageElem, int idxElem)
		{
			if (this.idValueMap == null)
			{
				this.idValueMap = new Dictionary<string, XPathNodeRef>();
			}
			if (!this.idValueMap.ContainsKey(id))
			{
				this.idValueMap.Add(id, new XPathNodeRef(pageElem, idxElem));
			}
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000E8EBC File Offset: 0x000E70BC
		internal int LookupIdElement(string id, out XPathNode[] pageElem)
		{
			if (this.idValueMap == null || !this.idValueMap.ContainsKey(id))
			{
				pageElem = null;
				return 0;
			}
			XPathNodeRef xpathNodeRef = this.idValueMap[id];
			pageElem = xpathNodeRef.Page;
			return xpathNodeRef.Index;
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000E8F01 File Offset: 0x000E7101
		private XmlTextReaderImpl SetupReader(XmlTextReaderImpl reader)
		{
			reader.EntityHandling = EntityHandling.ExpandEntities;
			reader.XmlValidatingReaderCompatibilityMode = true;
			return reader;
		}

		// Token: 0x04001342 RID: 4930
		private XPathNode[] pageText;

		// Token: 0x04001343 RID: 4931
		private XPathNode[] pageRoot;

		// Token: 0x04001344 RID: 4932
		private XPathNode[] pageXmlNmsp;

		// Token: 0x04001345 RID: 4933
		private int idxText;

		// Token: 0x04001346 RID: 4934
		private int idxRoot;

		// Token: 0x04001347 RID: 4935
		private int idxXmlNmsp;

		// Token: 0x04001348 RID: 4936
		private XmlNameTable nameTable;

		// Token: 0x04001349 RID: 4937
		private bool hasLineInfo;

		// Token: 0x0400134A RID: 4938
		private Dictionary<XPathNodeRef, XPathNodeRef> mapNmsp;

		// Token: 0x0400134B RID: 4939
		private Dictionary<string, XPathNodeRef> idValueMap;

		// Token: 0x020004BB RID: 1211
		internal enum LoadFlags
		{
			// Token: 0x04001F88 RID: 8072
			None,
			// Token: 0x04001F89 RID: 8073
			AtomizeNames,
			// Token: 0x04001F8A RID: 8074
			Fragment
		}
	}
}
