using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace System.Xml.Linq
{
	// Token: 0x0200001C RID: 28
	[__DynamicallyInvokable]
	public abstract class XContainer : XNode
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x0000509A File Offset: 0x0000329A
		internal XContainer()
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000050A4 File Offset: 0x000032A4
		internal XContainer(XContainer other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.content is string)
			{
				this.content = other.content;
				return;
			}
			XNode xnode = (XNode)other.content;
			if (xnode != null)
			{
				do
				{
					xnode = xnode.next;
					this.AppendNodeSkipNotify(xnode.CloneNode());
				}
				while (xnode != other.content);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000510C File Offset: 0x0000330C
		[__DynamicallyInvokable]
		public XNode FirstNode
		{
			[__DynamicallyInvokable]
			get
			{
				XNode lastNode = this.LastNode;
				if (lastNode == null)
				{
					return null;
				}
				return lastNode.next;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000512C File Offset: 0x0000332C
		[__DynamicallyInvokable]
		public XNode LastNode
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.content == null)
				{
					return null;
				}
				XNode xnode = this.content as XNode;
				if (xnode != null)
				{
					return xnode;
				}
				string text = this.content as string;
				if (text != null)
				{
					if (text.Length == 0)
					{
						return null;
					}
					XText xtext = new XText(text);
					xtext.parent = this;
					xtext.next = xtext;
					Interlocked.CompareExchange<object>(ref this.content, xtext, text);
				}
				return (XNode)this.content;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000519C File Offset: 0x0000339C
		[__DynamicallyInvokable]
		public void Add(object content)
		{
			if (base.SkipNotify())
			{
				this.AddContentSkipNotify(content);
				return;
			}
			if (content == null)
			{
				return;
			}
			XNode xnode = content as XNode;
			if (xnode != null)
			{
				this.AddNode(xnode);
				return;
			}
			string text = content as string;
			if (text != null)
			{
				this.AddString(text);
				return;
			}
			XAttribute xattribute = content as XAttribute;
			if (xattribute != null)
			{
				this.AddAttribute(xattribute);
				return;
			}
			XStreamingElement xstreamingElement = content as XStreamingElement;
			if (xstreamingElement != null)
			{
				this.AddNode(new XElement(xstreamingElement));
				return;
			}
			object[] array = content as object[];
			if (array != null)
			{
				foreach (object obj in array)
				{
					this.Add(obj);
				}
				return;
			}
			IEnumerable enumerable = content as IEnumerable;
			if (enumerable != null)
			{
				foreach (object obj2 in enumerable)
				{
					this.Add(obj2);
				}
				return;
			}
			this.AddString(XContainer.GetStringValue(content));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000052A4 File Offset: 0x000034A4
		[__DynamicallyInvokable]
		public void Add(params object[] content)
		{
			this.Add(content);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000052B0 File Offset: 0x000034B0
		[__DynamicallyInvokable]
		public void AddFirst(object content)
		{
			new Inserter(this, null).Add(content);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000052CD File Offset: 0x000034CD
		[__DynamicallyInvokable]
		public void AddFirst(params object[] content)
		{
			this.AddFirst(content);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000052D8 File Offset: 0x000034D8
		[__DynamicallyInvokable]
		public XmlWriter CreateWriter()
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.ConformanceLevel = ((this is XDocument) ? ConformanceLevel.Document : ConformanceLevel.Fragment);
			return XmlWriter.Create(new XNodeBuilder(this), xmlWriterSettings);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005309 File Offset: 0x00003509
		[__DynamicallyInvokable]
		public IEnumerable<XNode> DescendantNodes()
		{
			return this.GetDescendantNodes(false);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005312 File Offset: 0x00003512
		[__DynamicallyInvokable]
		public IEnumerable<XElement> Descendants()
		{
			return this.GetDescendants(null, false);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000531C File Offset: 0x0000351C
		[__DynamicallyInvokable]
		public IEnumerable<XElement> Descendants(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return this.GetDescendants(name, false);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005338 File Offset: 0x00003538
		[__DynamicallyInvokable]
		public XElement Element(XName name)
		{
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				XElement xelement;
				for (;;)
				{
					xnode = xnode.next;
					xelement = (xnode as XElement);
					if (xelement != null && xelement.name == name)
					{
						break;
					}
					if (xnode == this.content)
					{
						goto IL_39;
					}
				}
				return xelement;
			}
			IL_39:
			return null;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000537F File Offset: 0x0000357F
		[__DynamicallyInvokable]
		public IEnumerable<XElement> Elements()
		{
			return this.GetElements(null);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005388 File Offset: 0x00003588
		[__DynamicallyInvokable]
		public IEnumerable<XElement> Elements(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return this.GetElements(name);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000053A0 File Offset: 0x000035A0
		[__DynamicallyInvokable]
		public IEnumerable<XNode> Nodes()
		{
			XNode i = this.LastNode;
			if (i != null)
			{
				do
				{
					i = i.next;
					yield return i;
				}
				while (i.parent == this && i != this.content);
			}
			yield break;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000053B0 File Offset: 0x000035B0
		[__DynamicallyInvokable]
		public void RemoveNodes()
		{
			if (base.SkipNotify())
			{
				this.RemoveNodesSkipNotify();
				return;
			}
			while (this.content != null)
			{
				string text = this.content as string;
				if (text != null)
				{
					if (text.Length > 0)
					{
						this.ConvertTextToNode();
					}
					else if (this is XElement)
					{
						base.NotifyChanging(this, XObjectChangeEventArgs.Value);
						if (text != this.content)
						{
							throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
						}
						this.content = null;
						base.NotifyChanged(this, XObjectChangeEventArgs.Value);
					}
					else
					{
						this.content = null;
					}
				}
				XNode xnode = this.content as XNode;
				if (xnode != null)
				{
					XNode next = xnode.next;
					base.NotifyChanging(next, XObjectChangeEventArgs.Remove);
					if (xnode != this.content || next != xnode.next)
					{
						throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
					}
					if (next != xnode)
					{
						xnode.next = next.next;
					}
					else
					{
						this.content = null;
					}
					next.parent = null;
					next.next = null;
					base.NotifyChanged(next, XObjectChangeEventArgs.Remove);
				}
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000054BE File Offset: 0x000036BE
		[__DynamicallyInvokable]
		public void ReplaceNodes(object content)
		{
			content = XContainer.GetContentSnapshot(content);
			this.RemoveNodes();
			this.Add(content);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000054D5 File Offset: 0x000036D5
		[__DynamicallyInvokable]
		public void ReplaceNodes(params object[] content)
		{
			this.ReplaceNodes(content);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000054DE File Offset: 0x000036DE
		internal virtual void AddAttribute(XAttribute a)
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000054E0 File Offset: 0x000036E0
		internal virtual void AddAttributeSkipNotify(XAttribute a)
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000054E4 File Offset: 0x000036E4
		internal void AddContentSkipNotify(object content)
		{
			if (content == null)
			{
				return;
			}
			XNode xnode = content as XNode;
			if (xnode != null)
			{
				this.AddNodeSkipNotify(xnode);
				return;
			}
			string text = content as string;
			if (text != null)
			{
				this.AddStringSkipNotify(text);
				return;
			}
			XAttribute xattribute = content as XAttribute;
			if (xattribute != null)
			{
				this.AddAttributeSkipNotify(xattribute);
				return;
			}
			XStreamingElement xstreamingElement = content as XStreamingElement;
			if (xstreamingElement != null)
			{
				this.AddNodeSkipNotify(new XElement(xstreamingElement));
				return;
			}
			object[] array = content as object[];
			if (array != null)
			{
				foreach (object obj in array)
				{
					this.AddContentSkipNotify(obj);
				}
				return;
			}
			IEnumerable enumerable = content as IEnumerable;
			if (enumerable != null)
			{
				foreach (object obj2 in enumerable)
				{
					this.AddContentSkipNotify(obj2);
				}
				return;
			}
			this.AddStringSkipNotify(XContainer.GetStringValue(content));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000055DC File Offset: 0x000037DC
		internal void AddNode(XNode n)
		{
			this.ValidateNode(n, this);
			if (n.parent != null)
			{
				n = n.CloneNode();
			}
			else
			{
				XNode xnode = this;
				while (xnode.parent != null)
				{
					xnode = xnode.parent;
				}
				if (n == xnode)
				{
					n = n.CloneNode();
				}
			}
			this.ConvertTextToNode();
			this.AppendNode(n);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005630 File Offset: 0x00003830
		internal void AddNodeSkipNotify(XNode n)
		{
			this.ValidateNode(n, this);
			if (n.parent != null)
			{
				n = n.CloneNode();
			}
			else
			{
				XNode xnode = this;
				while (xnode.parent != null)
				{
					xnode = xnode.parent;
				}
				if (n == xnode)
				{
					n = n.CloneNode();
				}
			}
			this.ConvertTextToNode();
			this.AppendNodeSkipNotify(n);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005684 File Offset: 0x00003884
		internal void AddString(string s)
		{
			this.ValidateString(s);
			if (this.content != null)
			{
				if (s.Length > 0)
				{
					this.ConvertTextToNode();
					XText xtext = this.content as XText;
					if (xtext != null && !(xtext is XCData))
					{
						XText xtext2 = xtext;
						xtext2.Value += s;
						return;
					}
					this.AppendNode(new XText(s));
				}
				return;
			}
			if (s.Length > 0)
			{
				this.AppendNode(new XText(s));
				return;
			}
			if (!(this is XElement))
			{
				this.content = s;
				return;
			}
			base.NotifyChanging(this, XObjectChangeEventArgs.Value);
			if (this.content != null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
			}
			this.content = s;
			base.NotifyChanged(this, XObjectChangeEventArgs.Value);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005748 File Offset: 0x00003948
		internal void AddStringSkipNotify(string s)
		{
			this.ValidateString(s);
			if (this.content == null)
			{
				this.content = s;
				return;
			}
			if (s.Length > 0)
			{
				if (this.content is string)
				{
					this.content = (string)this.content + s;
					return;
				}
				XText xtext = this.content as XText;
				if (xtext != null && !(xtext is XCData))
				{
					XText xtext2 = xtext;
					xtext2.text += s;
					return;
				}
				this.AppendNodeSkipNotify(new XText(s));
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000057D0 File Offset: 0x000039D0
		internal void AppendNode(XNode n)
		{
			bool flag = base.NotifyChanging(n, XObjectChangeEventArgs.Add);
			if (n.parent != null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
			}
			this.AppendNodeSkipNotify(n);
			if (flag)
			{
				base.NotifyChanged(n, XObjectChangeEventArgs.Add);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000581C File Offset: 0x00003A1C
		internal void AppendNodeSkipNotify(XNode n)
		{
			n.parent = this;
			if (this.content == null || this.content is string)
			{
				n.next = n;
			}
			else
			{
				XNode xnode = (XNode)this.content;
				n.next = xnode.next;
				xnode.next = n;
			}
			this.content = n;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005874 File Offset: 0x00003A74
		internal override void AppendText(StringBuilder sb)
		{
			string text = this.content as string;
			if (text != null)
			{
				sb.Append(text);
				return;
			}
			XNode xnode = (XNode)this.content;
			if (xnode != null)
			{
				do
				{
					xnode = xnode.next;
					xnode.AppendText(sb);
				}
				while (xnode != this.content);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000058C0 File Offset: 0x00003AC0
		private string GetTextOnly()
		{
			if (this.content == null)
			{
				return null;
			}
			string text = this.content as string;
			if (text == null)
			{
				XNode xnode = (XNode)this.content;
				for (;;)
				{
					xnode = xnode.next;
					if (xnode.NodeType != XmlNodeType.Text)
					{
						break;
					}
					text += ((XText)xnode).Value;
					if (xnode == this.content)
					{
						return text;
					}
				}
				return null;
			}
			return text;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005920 File Offset: 0x00003B20
		private string CollectText(ref XNode n)
		{
			string text = "";
			while (n != null && n.NodeType == XmlNodeType.Text)
			{
				text += ((XText)n).Value;
				n = ((n != this.content) ? n.next : null);
			}
			return text;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005970 File Offset: 0x00003B70
		internal bool ContentsEqual(XContainer e)
		{
			if (this.content == e.content)
			{
				return true;
			}
			string textOnly = this.GetTextOnly();
			if (textOnly != null)
			{
				return textOnly == e.GetTextOnly();
			}
			XNode xnode = this.content as XNode;
			XNode xnode2 = e.content as XNode;
			if (xnode != null && xnode2 != null)
			{
				xnode = xnode.next;
				xnode2 = xnode2.next;
				while (!(this.CollectText(ref xnode) != e.CollectText(ref xnode2)))
				{
					if (xnode == null && xnode2 == null)
					{
						return true;
					}
					if (xnode == null || xnode2 == null || !xnode.DeepEquals(xnode2))
					{
						break;
					}
					xnode = ((xnode != this.content) ? xnode.next : null);
					xnode2 = ((xnode2 != e.content) ? xnode2.next : null);
				}
			}
			return false;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005A28 File Offset: 0x00003C28
		internal int ContentsHashCode()
		{
			string textOnly = this.GetTextOnly();
			if (textOnly != null)
			{
				return textOnly.GetHashCode();
			}
			int num = 0;
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				do
				{
					xnode = xnode.next;
					string text = this.CollectText(ref xnode);
					if (text.Length > 0)
					{
						num ^= text.GetHashCode();
					}
					if (xnode == null)
					{
						break;
					}
					num ^= xnode.GetDeepHashCode();
				}
				while (xnode != this.content);
			}
			return num;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005A90 File Offset: 0x00003C90
		internal void ConvertTextToNode()
		{
			string text = this.content as string;
			if (text != null && text.Length > 0)
			{
				XText xtext = new XText(text);
				xtext.parent = this;
				xtext.next = xtext;
				this.content = xtext;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005AD1 File Offset: 0x00003CD1
		internal static string GetDateTimeString(DateTime value)
		{
			return XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005ADA File Offset: 0x00003CDA
		internal IEnumerable<XNode> GetDescendantNodes(bool self)
		{
			if (self)
			{
				yield return this;
			}
			XNode i = this;
			for (;;)
			{
				XContainer xcontainer = i as XContainer;
				XNode firstNode;
				if (xcontainer != null && (firstNode = xcontainer.FirstNode) != null)
				{
					i = firstNode;
				}
				else
				{
					while (i != null && i != this && i == i.parent.content)
					{
						i = i.parent;
					}
					if (i == null || i == this)
					{
						break;
					}
					i = i.next;
				}
				yield return i;
			}
			yield break;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005AF1 File Offset: 0x00003CF1
		internal IEnumerable<XElement> GetDescendants(XName name, bool self)
		{
			if (self)
			{
				XElement xelement = (XElement)this;
				if (name == null || xelement.name == name)
				{
					yield return xelement;
				}
			}
			XNode i = this;
			XContainer xcontainer = this;
			for (;;)
			{
				if (xcontainer != null && xcontainer.content is XNode)
				{
					i = ((XNode)xcontainer.content).next;
				}
				else
				{
					while (i != this && i == i.parent.content)
					{
						i = i.parent;
					}
					if (i == this)
					{
						break;
					}
					i = i.next;
				}
				XElement e = i as XElement;
				if (e != null && (name == null || e.name == name))
				{
					yield return e;
				}
				xcontainer = e;
				e = null;
			}
			yield break;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005B0F File Offset: 0x00003D0F
		private IEnumerable<XElement> GetElements(XName name)
		{
			XNode i = this.content as XNode;
			if (i != null)
			{
				do
				{
					i = i.next;
					XElement xelement = i as XElement;
					if (xelement != null && (name == null || xelement.name == name))
					{
						yield return xelement;
					}
				}
				while (i.parent == this && i != this.content);
			}
			yield break;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005B28 File Offset: 0x00003D28
		internal static string GetStringValue(object value)
		{
			string text;
			if (value is string)
			{
				text = (string)value;
			}
			else if (value is double)
			{
				text = XmlConvert.ToString((double)value);
			}
			else if (value is float)
			{
				text = XmlConvert.ToString((float)value);
			}
			else if (value is decimal)
			{
				text = XmlConvert.ToString((decimal)value);
			}
			else if (value is bool)
			{
				text = XmlConvert.ToString((bool)value);
			}
			else if (value is DateTime)
			{
				text = XContainer.GetDateTimeString((DateTime)value);
			}
			else if (value is DateTimeOffset)
			{
				text = XmlConvert.ToString((DateTimeOffset)value);
			}
			else if (value is TimeSpan)
			{
				text = XmlConvert.ToString((TimeSpan)value);
			}
			else
			{
				if (value is XObject)
				{
					throw new ArgumentException(Res.GetString("Argument_XObjectValue"));
				}
				text = value.ToString();
			}
			if (text == null)
			{
				throw new ArgumentException(Res.GetString("Argument_ConvertToString"));
			}
			return text;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005C1C File Offset: 0x00003E1C
		internal void ReadContentFrom(XmlReader r)
		{
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExpectedInteractive"));
			}
			XContainer xcontainer = this;
			NamespaceCache namespaceCache = default(NamespaceCache);
			NamespaceCache namespaceCache2 = default(NamespaceCache);
			for (;;)
			{
				switch (r.NodeType)
				{
				case XmlNodeType.Element:
				{
					XElement xelement = new XElement(namespaceCache.Get(r.NamespaceURI).GetName(r.LocalName));
					if (r.MoveToFirstAttribute())
					{
						do
						{
							xelement.AppendAttributeSkipNotify(new XAttribute(namespaceCache2.Get((r.Prefix.Length == 0) ? string.Empty : r.NamespaceURI).GetName(r.LocalName), r.Value));
						}
						while (r.MoveToNextAttribute());
						r.MoveToElement();
					}
					xcontainer.AddNodeSkipNotify(xelement);
					if (!r.IsEmptyElement)
					{
						xcontainer = xelement;
						goto IL_1FF;
					}
					goto IL_1FF;
				}
				case XmlNodeType.Text:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					xcontainer.AddStringSkipNotify(r.Value);
					goto IL_1FF;
				case XmlNodeType.CDATA:
					xcontainer.AddNodeSkipNotify(new XCData(r.Value));
					goto IL_1FF;
				case XmlNodeType.EntityReference:
					if (!r.CanResolveEntity)
					{
						goto Block_8;
					}
					r.ResolveEntity();
					goto IL_1FF;
				case XmlNodeType.ProcessingInstruction:
					xcontainer.AddNodeSkipNotify(new XProcessingInstruction(r.Name, r.Value));
					goto IL_1FF;
				case XmlNodeType.Comment:
					xcontainer.AddNodeSkipNotify(new XComment(r.Value));
					goto IL_1FF;
				case XmlNodeType.DocumentType:
					xcontainer.AddNodeSkipNotify(new XDocumentType(r.LocalName, r.GetAttribute("PUBLIC"), r.GetAttribute("SYSTEM"), r.Value, r.DtdInfo));
					goto IL_1FF;
				case XmlNodeType.EndElement:
					if (xcontainer.content == null)
					{
						xcontainer.content = string.Empty;
					}
					if (xcontainer == this)
					{
						return;
					}
					xcontainer = xcontainer.parent;
					goto IL_1FF;
				case XmlNodeType.EndEntity:
					goto IL_1FF;
				}
				break;
				IL_1FF:
				if (!r.Read())
				{
					return;
				}
			}
			goto IL_1DB;
			Block_8:
			throw new InvalidOperationException(Res.GetString("InvalidOperation_UnresolvedEntityReference"));
			IL_1DB:
			throw new InvalidOperationException(Res.GetString("InvalidOperation_UnexpectedNodeType", new object[]
			{
				r.NodeType
			}));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005E34 File Offset: 0x00004034
		internal void ReadContentFrom(XmlReader r, LoadOptions o)
		{
			if ((o & (LoadOptions.SetBaseUri | LoadOptions.SetLineInfo)) == LoadOptions.None)
			{
				this.ReadContentFrom(r);
				return;
			}
			if (r.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExpectedInteractive"));
			}
			XContainer xcontainer = this;
			XNode xnode = null;
			NamespaceCache namespaceCache = default(NamespaceCache);
			NamespaceCache namespaceCache2 = default(NamespaceCache);
			string text = ((o & LoadOptions.SetBaseUri) != LoadOptions.None) ? r.BaseURI : null;
			IXmlLineInfo xmlLineInfo = ((o & LoadOptions.SetLineInfo) != LoadOptions.None) ? (r as IXmlLineInfo) : null;
			for (;;)
			{
				string baseURI = r.BaseURI;
				switch (r.NodeType)
				{
				case XmlNodeType.Element:
				{
					XElement xelement = new XElement(namespaceCache.Get(r.NamespaceURI).GetName(r.LocalName));
					if (text != null && text != baseURI)
					{
						xelement.SetBaseUri(baseURI);
					}
					if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
					{
						xelement.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
					}
					if (r.MoveToFirstAttribute())
					{
						do
						{
							XAttribute xattribute = new XAttribute(namespaceCache2.Get((r.Prefix.Length == 0) ? string.Empty : r.NamespaceURI).GetName(r.LocalName), r.Value);
							if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
							{
								xattribute.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
							}
							xelement.AppendAttributeSkipNotify(xattribute);
						}
						while (r.MoveToNextAttribute());
						r.MoveToElement();
					}
					xcontainer.AddNodeSkipNotify(xelement);
					if (r.IsEmptyElement)
					{
						goto IL_305;
					}
					xcontainer = xelement;
					if (text != null)
					{
						text = baseURI;
						goto IL_305;
					}
					goto IL_305;
				}
				case XmlNodeType.Text:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if ((text != null && text != baseURI) || (xmlLineInfo != null && xmlLineInfo.HasLineInfo()))
					{
						xnode = new XText(r.Value);
						goto IL_305;
					}
					xcontainer.AddStringSkipNotify(r.Value);
					goto IL_305;
				case XmlNodeType.CDATA:
					xnode = new XCData(r.Value);
					goto IL_305;
				case XmlNodeType.EntityReference:
					if (!r.CanResolveEntity)
					{
						goto Block_25;
					}
					r.ResolveEntity();
					goto IL_305;
				case XmlNodeType.ProcessingInstruction:
					xnode = new XProcessingInstruction(r.Name, r.Value);
					goto IL_305;
				case XmlNodeType.Comment:
					xnode = new XComment(r.Value);
					goto IL_305;
				case XmlNodeType.DocumentType:
					xnode = new XDocumentType(r.LocalName, r.GetAttribute("PUBLIC"), r.GetAttribute("SYSTEM"), r.Value, r.DtdInfo);
					goto IL_305;
				case XmlNodeType.EndElement:
				{
					if (xcontainer.content == null)
					{
						xcontainer.content = string.Empty;
					}
					XElement xelement2 = xcontainer as XElement;
					if (xelement2 != null && xmlLineInfo != null && xmlLineInfo.HasLineInfo())
					{
						xelement2.SetEndElementLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
					}
					if (xcontainer == this)
					{
						return;
					}
					if (text != null && xcontainer.HasBaseUri)
					{
						text = xcontainer.parent.BaseUri;
					}
					xcontainer = xcontainer.parent;
					goto IL_305;
				}
				case XmlNodeType.EndEntity:
					goto IL_305;
				}
				break;
				IL_305:
				if (xnode != null)
				{
					if (text != null && text != baseURI)
					{
						xnode.SetBaseUri(baseURI);
					}
					if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
					{
						xnode.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
					}
					xcontainer.AddNodeSkipNotify(xnode);
					xnode = null;
				}
				if (!r.Read())
				{
					return;
				}
			}
			goto IL_2E1;
			Block_25:
			throw new InvalidOperationException(Res.GetString("InvalidOperation_UnresolvedEntityReference"));
			IL_2E1:
			throw new InvalidOperationException(Res.GetString("InvalidOperation_UnexpectedNodeType", new object[]
			{
				r.NodeType
			}));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006198 File Offset: 0x00004398
		internal void RemoveNode(XNode n)
		{
			bool flag = base.NotifyChanging(n, XObjectChangeEventArgs.Remove);
			if (n.parent != this)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_ExternalCode"));
			}
			XNode xnode = (XNode)this.content;
			while (xnode.next != n)
			{
				xnode = xnode.next;
			}
			if (xnode == n)
			{
				this.content = null;
			}
			else
			{
				if (this.content == n)
				{
					this.content = xnode;
				}
				xnode.next = n.next;
			}
			n.parent = null;
			n.next = null;
			if (flag)
			{
				base.NotifyChanged(n, XObjectChangeEventArgs.Remove);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006230 File Offset: 0x00004430
		private void RemoveNodesSkipNotify()
		{
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				do
				{
					XNode next = xnode.next;
					xnode.parent = null;
					xnode.next = null;
					xnode = next;
				}
				while (xnode != this.content);
			}
			this.content = null;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006273 File Offset: 0x00004473
		internal virtual void ValidateNode(XNode node, XNode previous)
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006275 File Offset: 0x00004475
		internal virtual void ValidateString(string s)
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006278 File Offset: 0x00004478
		internal void WriteContentTo(XmlWriter writer)
		{
			if (this.content != null)
			{
				if (this.content is string)
				{
					if (this is XDocument)
					{
						writer.WriteWhitespace((string)this.content);
						return;
					}
					writer.WriteString((string)this.content);
					return;
				}
				else
				{
					XNode xnode = (XNode)this.content;
					do
					{
						xnode = xnode.next;
						xnode.WriteTo(writer);
					}
					while (xnode != this.content);
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000062EC File Offset: 0x000044EC
		private static void AddContentToList(List<object> list, object content)
		{
			IEnumerable enumerable = (content is string) ? null : (content as IEnumerable);
			if (enumerable == null)
			{
				list.Add(content);
				return;
			}
			foreach (object obj in enumerable)
			{
				if (obj != null)
				{
					XContainer.AddContentToList(list, obj);
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000635C File Offset: 0x0000455C
		internal static object GetContentSnapshot(object content)
		{
			if (content is string || !(content is IEnumerable))
			{
				return content;
			}
			List<object> list = new List<object>();
			XContainer.AddContentToList(list, content);
			return list;
		}

		// Token: 0x04000087 RID: 135
		internal object content;
	}
}
