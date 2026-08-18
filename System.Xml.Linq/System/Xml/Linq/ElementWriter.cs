using System;

namespace System.Xml.Linq
{
	// Token: 0x02000020 RID: 32
	internal struct ElementWriter
	{
		// Token: 0x06000181 RID: 385 RVA: 0x000079ED File Offset: 0x00005BED
		public ElementWriter(XmlWriter writer)
		{
			this.writer = writer;
			this.resolver = default(NamespaceResolver);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007A04 File Offset: 0x00005C04
		public void WriteElement(XElement e)
		{
			this.PushAncestors(e);
			XElement xelement = e;
			XNode xnode = e;
			for (;;)
			{
				e = (xnode as XElement);
				if (e != null)
				{
					this.WriteStartElement(e);
					if (e.content == null)
					{
						this.WriteEndElement();
					}
					else
					{
						string text = e.content as string;
						if (text == null)
						{
							xnode = ((XNode)e.content).next;
							continue;
						}
						this.writer.WriteString(text);
						this.WriteFullEndElement();
					}
				}
				else
				{
					xnode.WriteTo(this.writer);
				}
				while (xnode != xelement && xnode == xnode.parent.content)
				{
					xnode = xnode.parent;
					this.WriteFullEndElement();
				}
				if (xnode == xelement)
				{
					break;
				}
				xnode = xnode.next;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007AB4 File Offset: 0x00005CB4
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

		// Token: 0x06000184 RID: 388 RVA: 0x00007B08 File Offset: 0x00005D08
		private void PushAncestors(XElement e)
		{
			for (;;)
			{
				e = (e.parent as XElement);
				if (e == null)
				{
					break;
				}
				XAttribute xattribute = e.lastAttr;
				if (xattribute != null)
				{
					do
					{
						xattribute = xattribute.next;
						if (xattribute.IsNamespaceDeclaration)
						{
							this.resolver.AddFirst((xattribute.Name.NamespaceName.Length == 0) ? string.Empty : xattribute.Name.LocalName, XNamespace.Get(xattribute.Value));
						}
					}
					while (xattribute != e.lastAttr);
				}
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007B84 File Offset: 0x00005D84
		private void PushElement(XElement e)
		{
			this.resolver.PushScope();
			XAttribute xattribute = e.lastAttr;
			if (xattribute != null)
			{
				do
				{
					xattribute = xattribute.next;
					if (xattribute.IsNamespaceDeclaration)
					{
						this.resolver.Add((xattribute.Name.NamespaceName.Length == 0) ? string.Empty : xattribute.Name.LocalName, XNamespace.Get(xattribute.Value));
					}
				}
				while (xattribute != e.lastAttr);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007BF8 File Offset: 0x00005DF8
		private void WriteEndElement()
		{
			this.writer.WriteEndElement();
			this.resolver.PopScope();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007C10 File Offset: 0x00005E10
		private void WriteFullEndElement()
		{
			this.writer.WriteFullEndElement();
			this.resolver.PopScope();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007C28 File Offset: 0x00005E28
		private void WriteStartElement(XElement e)
		{
			this.PushElement(e);
			XNamespace @namespace = e.Name.Namespace;
			this.writer.WriteStartElement(this.GetPrefixOfNamespace(@namespace, true), e.Name.LocalName, @namespace.NamespaceName);
			XAttribute xattribute = e.lastAttr;
			if (xattribute != null)
			{
				do
				{
					xattribute = xattribute.next;
					@namespace = xattribute.Name.Namespace;
					string localName = xattribute.Name.LocalName;
					string namespaceName = @namespace.NamespaceName;
					this.writer.WriteAttributeString(this.GetPrefixOfNamespace(@namespace, false), localName, (namespaceName.Length == 0 && localName == "xmlns") ? "http://www.w3.org/2000/xmlns/" : namespaceName, xattribute.Value);
				}
				while (xattribute != e.lastAttr);
			}
		}

		// Token: 0x04000090 RID: 144
		private XmlWriter writer;

		// Token: 0x04000091 RID: 145
		private NamespaceResolver resolver;
	}
}
