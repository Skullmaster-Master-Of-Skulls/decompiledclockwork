using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml.Linq
{
	// Token: 0x02000017 RID: 23
	[__DynamicallyInvokable]
	public abstract class XNode : XObject
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x0000477B File Offset: 0x0000297B
		internal XNode()
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004783 File Offset: 0x00002983
		[__DynamicallyInvokable]
		public XNode NextNode
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.parent != null && this != this.parent.content)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000047A4 File Offset: 0x000029A4
		[__DynamicallyInvokable]
		public XNode PreviousNode
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.parent == null)
				{
					return null;
				}
				XNode xnode = ((XNode)this.parent.content).next;
				XNode result = null;
				while (xnode != this)
				{
					result = xnode;
					xnode = xnode.next;
				}
				return result;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000047E3 File Offset: 0x000029E3
		[__DynamicallyInvokable]
		public static XNodeDocumentOrderComparer DocumentOrderComparer
		{
			[__DynamicallyInvokable]
			get
			{
				if (XNode.documentOrderComparer == null)
				{
					XNode.documentOrderComparer = new XNodeDocumentOrderComparer();
				}
				return XNode.documentOrderComparer;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000047FB File Offset: 0x000029FB
		[__DynamicallyInvokable]
		public static XNodeEqualityComparer EqualityComparer
		{
			[__DynamicallyInvokable]
			get
			{
				if (XNode.equalityComparer == null)
				{
					XNode.equalityComparer = new XNodeEqualityComparer();
				}
				return XNode.equalityComparer;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004814 File Offset: 0x00002A14
		[__DynamicallyInvokable]
		public void AddAfterSelf(object content)
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_MissingParent"));
			}
			new Inserter(this.parent, this).Add(content);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000484E File Offset: 0x00002A4E
		[__DynamicallyInvokable]
		public void AddAfterSelf(params object[] content)
		{
			this.AddAfterSelf(content);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004858 File Offset: 0x00002A58
		[__DynamicallyInvokable]
		public void AddBeforeSelf(object content)
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_MissingParent"));
			}
			XNode xnode = (XNode)this.parent.content;
			while (xnode.next != this)
			{
				xnode = xnode.next;
			}
			if (xnode == this.parent.content)
			{
				xnode = null;
			}
			new Inserter(this.parent, xnode).Add(content);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000048C5 File Offset: 0x00002AC5
		[__DynamicallyInvokable]
		public void AddBeforeSelf(params object[] content)
		{
			this.AddBeforeSelf(content);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000048CE File Offset: 0x00002ACE
		[__DynamicallyInvokable]
		public IEnumerable<XElement> Ancestors()
		{
			return this.GetAncestors(null, false);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000048D8 File Offset: 0x00002AD8
		[__DynamicallyInvokable]
		public IEnumerable<XElement> Ancestors(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return this.GetAncestors(name, false);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000048F4 File Offset: 0x00002AF4
		[__DynamicallyInvokable]
		public static int CompareDocumentOrder(XNode n1, XNode n2)
		{
			if (n1 == n2)
			{
				return 0;
			}
			if (n1 == null)
			{
				return -1;
			}
			if (n2 == null)
			{
				return 1;
			}
			if (n1.parent != n2.parent)
			{
				int num = 0;
				XNode xnode = n1;
				while (xnode.parent != null)
				{
					xnode = xnode.parent;
					num++;
				}
				XNode xnode2 = n2;
				while (xnode2.parent != null)
				{
					xnode2 = xnode2.parent;
					num--;
				}
				if (xnode != xnode2)
				{
					throw new InvalidOperationException(Res.GetString("InvalidOperation_MissingAncestor"));
				}
				if (num < 0)
				{
					do
					{
						n2 = n2.parent;
						num++;
					}
					while (num != 0);
					if (n1 == n2)
					{
						return -1;
					}
				}
				else if (num > 0)
				{
					do
					{
						n1 = n1.parent;
						num--;
					}
					while (num != 0);
					if (n1 == n2)
					{
						return 1;
					}
				}
				while (n1.parent != n2.parent)
				{
					n1 = n1.parent;
					n2 = n2.parent;
				}
			}
			else if (n1.parent == null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_MissingAncestor"));
			}
			XNode xnode3 = (XNode)n1.parent.content;
			for (;;)
			{
				xnode3 = xnode3.next;
				if (xnode3 == n1)
				{
					break;
				}
				if (xnode3 == n2)
				{
					return 1;
				}
			}
			return -1;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000049F3 File Offset: 0x00002BF3
		[__DynamicallyInvokable]
		public XmlReader CreateReader()
		{
			return new XNodeReader(this, null);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000049FC File Offset: 0x00002BFC
		[__DynamicallyInvokable]
		public XmlReader CreateReader(ReaderOptions readerOptions)
		{
			return new XNodeReader(this, null, readerOptions);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004A06 File Offset: 0x00002C06
		[__DynamicallyInvokable]
		public IEnumerable<XNode> NodesAfterSelf()
		{
			XNode i = this;
			while (i.parent != null && i != i.parent.content)
			{
				i = i.next;
				yield return i;
			}
			yield break;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004A16 File Offset: 0x00002C16
		[__DynamicallyInvokable]
		public IEnumerable<XNode> NodesBeforeSelf()
		{
			if (this.parent != null)
			{
				XNode i = (XNode)this.parent.content;
				do
				{
					i = i.next;
					if (i == this)
					{
						break;
					}
					yield return i;
				}
				while (this.parent != null && this.parent == i.parent);
				i = null;
			}
			yield break;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004A26 File Offset: 0x00002C26
		[__DynamicallyInvokable]
		public IEnumerable<XElement> ElementsAfterSelf()
		{
			return this.GetElementsAfterSelf(null);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004A2F File Offset: 0x00002C2F
		[__DynamicallyInvokable]
		public IEnumerable<XElement> ElementsAfterSelf(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return this.GetElementsAfterSelf(name);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004A47 File Offset: 0x00002C47
		[__DynamicallyInvokable]
		public IEnumerable<XElement> ElementsBeforeSelf()
		{
			return this.GetElementsBeforeSelf(null);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004A50 File Offset: 0x00002C50
		[__DynamicallyInvokable]
		public IEnumerable<XElement> ElementsBeforeSelf(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return this.GetElementsBeforeSelf(name);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004A68 File Offset: 0x00002C68
		[__DynamicallyInvokable]
		public bool IsAfter(XNode node)
		{
			return XNode.CompareDocumentOrder(this, node) > 0;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004A74 File Offset: 0x00002C74
		[__DynamicallyInvokable]
		public bool IsBefore(XNode node)
		{
			return XNode.CompareDocumentOrder(this, node) < 0;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004A80 File Offset: 0x00002C80
		[__DynamicallyInvokable]
		public static XNode ReadFrom(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExpectedInteractive"));
			}
			switch (reader.NodeType)
			{
			case XmlNodeType.Element:
				return new XElement(reader);
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return new XText(reader);
			case XmlNodeType.CDATA:
				return new XCData(reader);
			case XmlNodeType.ProcessingInstruction:
				return new XProcessingInstruction(reader);
			case XmlNodeType.Comment:
				return new XComment(reader);
			case XmlNodeType.DocumentType:
				return new XDocumentType(reader);
			}
			throw new InvalidOperationException(Res.GetString("InvalidOperation_UnexpectedNodeType", new object[]
			{
				reader.NodeType
			}));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004B4A File Offset: 0x00002D4A
		[__DynamicallyInvokable]
		public void Remove()
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_MissingParent"));
			}
			this.parent.RemoveNode(this);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004B70 File Offset: 0x00002D70
		[__DynamicallyInvokable]
		public void ReplaceWith(object content)
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_MissingParent"));
			}
			XContainer parent = this.parent;
			XNode xnode = (XNode)this.parent.content;
			while (xnode.next != this)
			{
				xnode = xnode.next;
			}
			if (xnode == this.parent.content)
			{
				xnode = null;
			}
			this.parent.RemoveNode(this);
			if (xnode != null && xnode.parent != parent)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
			}
			new Inserter(parent, xnode).Add(content);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004C07 File Offset: 0x00002E07
		[__DynamicallyInvokable]
		public void ReplaceWith(params object[] content)
		{
			this.ReplaceWith(content);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004C10 File Offset: 0x00002E10
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.GetXmlString(base.GetSaveOptionsFromAnnotations());
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004C1E File Offset: 0x00002E1E
		[__DynamicallyInvokable]
		public string ToString(SaveOptions options)
		{
			return this.GetXmlString(options);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004C27 File Offset: 0x00002E27
		[__DynamicallyInvokable]
		public static bool DeepEquals(XNode n1, XNode n2)
		{
			return n1 == n2 || (n1 != null && n2 != null && n1.DeepEquals(n2));
		}

		// Token: 0x060000C2 RID: 194
		[__DynamicallyInvokable]
		public abstract void WriteTo(XmlWriter writer);

		// Token: 0x060000C3 RID: 195 RVA: 0x00004C3E File Offset: 0x00002E3E
		internal virtual void AppendText(StringBuilder sb)
		{
		}

		// Token: 0x060000C4 RID: 196
		internal abstract XNode CloneNode();

		// Token: 0x060000C5 RID: 197
		internal abstract bool DeepEquals(XNode node);

		// Token: 0x060000C6 RID: 198 RVA: 0x00004C40 File Offset: 0x00002E40
		internal IEnumerable<XElement> GetAncestors(XName name, bool self)
		{
			for (XElement e = (self ? this : this.parent) as XElement; e != null; e = (e.parent as XElement))
			{
				if (name == null || e.name == name)
				{
					yield return e;
				}
			}
			yield break;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004C5E File Offset: 0x00002E5E
		private IEnumerable<XElement> GetElementsAfterSelf(XName name)
		{
			XNode i = this;
			while (i.parent != null && i != i.parent.content)
			{
				i = i.next;
				XElement xelement = i as XElement;
				if (xelement != null && (name == null || xelement.name == name))
				{
					yield return xelement;
				}
			}
			yield break;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004C75 File Offset: 0x00002E75
		private IEnumerable<XElement> GetElementsBeforeSelf(XName name)
		{
			if (this.parent != null)
			{
				XNode i = (XNode)this.parent.content;
				do
				{
					i = i.next;
					if (i == this)
					{
						break;
					}
					XElement xelement = i as XElement;
					if (xelement != null && (name == null || xelement.name == name))
					{
						yield return xelement;
					}
				}
				while (this.parent != null && this.parent == i.parent);
				i = null;
			}
			yield break;
		}

		// Token: 0x060000C9 RID: 201
		internal abstract int GetDeepHashCode();

		// Token: 0x060000CA RID: 202 RVA: 0x00004C8C File Offset: 0x00002E8C
		internal static XmlReaderSettings GetXmlReaderSettings(LoadOptions o)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			if ((o & LoadOptions.PreserveWhitespace) == LoadOptions.None)
			{
				xmlReaderSettings.IgnoreWhitespace = true;
			}
			xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
			xmlReaderSettings.MaxCharactersFromEntities = 10000000L;
			xmlReaderSettings.XmlResolver = null;
			return xmlReaderSettings;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004CC8 File Offset: 0x00002EC8
		internal static XmlWriterSettings GetXmlWriterSettings(SaveOptions o)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			if ((o & SaveOptions.DisableFormatting) == SaveOptions.None)
			{
				xmlWriterSettings.Indent = true;
			}
			if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
			{
				xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
			}
			return xmlWriterSettings;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004CFC File Offset: 0x00002EFC
		private string GetXmlString(SaveOptions o)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.OmitXmlDeclaration = true;
				if ((o & SaveOptions.DisableFormatting) == SaveOptions.None)
				{
					xmlWriterSettings.Indent = true;
				}
				if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
				{
					xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
				}
				if (this is XText)
				{
					xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
				}
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					XDocument xdocument = this as XDocument;
					if (xdocument != null)
					{
						xdocument.WriteContentTo(xmlWriter);
					}
					else
					{
						this.WriteTo(xmlWriter);
					}
				}
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x04000083 RID: 131
		private static XNodeDocumentOrderComparer documentOrderComparer;

		// Token: 0x04000084 RID: 132
		private static XNodeEqualityComparer equalityComparer;

		// Token: 0x04000085 RID: 133
		internal XNode next;
	}
}
