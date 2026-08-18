using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	// Token: 0x0200002C RID: 44
	internal struct StreamingElementWriter
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000973C File Offset: 0x0000793C
		public StreamingElementWriter(XmlWriter w)
		{
			this.writer = w;
			this.element = null;
			this.attributes = new List<XAttribute>();
			this.resolver = default(NamespaceResolver);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009764 File Offset: 0x00007964
		private void FlushElement()
		{
			if (this.element != null)
			{
				this.PushElement();
				XNamespace @namespace = this.element.Name.Namespace;
				this.writer.WriteStartElement(this.GetPrefixOfNamespace(@namespace, true), this.element.Name.LocalName, @namespace.NamespaceName);
				foreach (XAttribute xattribute in this.attributes)
				{
					@namespace = xattribute.Name.Namespace;
					string localName = xattribute.Name.LocalName;
					string namespaceName = @namespace.NamespaceName;
					this.writer.WriteAttributeString(this.GetPrefixOfNamespace(@namespace, false), localName, (namespaceName.Length == 0 && localName == "xmlns") ? "http://www.w3.org/2000/xmlns/" : namespaceName, xattribute.Value);
				}
				this.element = null;
				this.attributes.Clear();
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009868 File Offset: 0x00007A68
		private string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			string namespaceName = ns.NamespaceName;
			if (namespaceName.Length == 0)
			{
				return string.Empty;
			}
			string prefixOfNamespace = this.resolver.GetPrefixOfNamespace(ns, allowDefaultNamespace);
			if (prefixOfNamespace != null)
			{
				return prefixOfNamespace;
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			return null;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000098BC File Offset: 0x00007ABC
		private void PushElement()
		{
			this.resolver.PushScope();
			foreach (XAttribute xattribute in this.attributes)
			{
				if (xattribute.IsNamespaceDeclaration)
				{
					this.resolver.Add((xattribute.Name.NamespaceName.Length == 0) ? string.Empty : xattribute.Name.LocalName, XNamespace.Get(xattribute.Value));
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00009958 File Offset: 0x00007B58
		private void Write(object content)
		{
			if (content == null)
			{
				return;
			}
			XNode xnode = content as XNode;
			if (xnode != null)
			{
				this.WriteNode(xnode);
				return;
			}
			string text = content as string;
			if (text != null)
			{
				this.WriteString(text);
				return;
			}
			XAttribute xattribute = content as XAttribute;
			if (xattribute != null)
			{
				this.WriteAttribute(xattribute);
				return;
			}
			XStreamingElement xstreamingElement = content as XStreamingElement;
			if (xstreamingElement != null)
			{
				this.WriteStreamingElement(xstreamingElement);
				return;
			}
			object[] array = content as object[];
			if (array != null)
			{
				foreach (object content2 in array)
				{
					this.Write(content2);
				}
				return;
			}
			IEnumerable enumerable = content as IEnumerable;
			if (enumerable != null)
			{
				foreach (object content3 in enumerable)
				{
					this.Write(content3);
				}
				return;
			}
			this.WriteString(XContainer.GetStringValue(content));
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009A4C File Offset: 0x00007C4C
		private void WriteAttribute(XAttribute a)
		{
			if (this.element == null)
			{
				throw new InvalidOperationException(Res.GetString("InvalidOperation_WriteAttribute"));
			}
			this.attributes.Add(a);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009A72 File Offset: 0x00007C72
		private void WriteNode(XNode n)
		{
			this.FlushElement();
			n.WriteTo(this.writer);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009A88 File Offset: 0x00007C88
		internal void WriteStreamingElement(XStreamingElement e)
		{
			this.FlushElement();
			this.element = e;
			this.Write(e.content);
			bool flag = this.element == null;
			this.FlushElement();
			if (flag)
			{
				this.writer.WriteFullEndElement();
			}
			else
			{
				this.writer.WriteEndElement();
			}
			this.resolver.PopScope();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009AE4 File Offset: 0x00007CE4
		private void WriteString(string s)
		{
			this.FlushElement();
			this.writer.WriteString(s);
		}

		// Token: 0x040000B3 RID: 179
		private XmlWriter writer;

		// Token: 0x040000B4 RID: 180
		private XStreamingElement element;

		// Token: 0x040000B5 RID: 181
		private List<XAttribute> attributes;

		// Token: 0x040000B6 RID: 182
		private NamespaceResolver resolver;
	}
}
