using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020001BC RID: 444
	[__DynamicallyInvokable]
	public class XmlSerializerNamespaces
	{
		// Token: 0x06001ECC RID: 7884 RVA: 0x000A8C8E File Offset: 0x000A6E8E
		[__DynamicallyInvokable]
		public XmlSerializerNamespaces()
		{
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000A8C96 File Offset: 0x000A6E96
		[__DynamicallyInvokable]
		public XmlSerializerNamespaces(XmlSerializerNamespaces namespaces)
		{
			this.namespaces = (Hashtable)namespaces.Namespaces.Clone();
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x000A8CB4 File Offset: 0x000A6EB4
		[__DynamicallyInvokable]
		public XmlSerializerNamespaces(XmlQualifiedName[] namespaces)
		{
			foreach (XmlQualifiedName xmlQualifiedName in namespaces)
			{
				this.Add(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			}
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x000A8CEB File Offset: 0x000A6EEB
		[__DynamicallyInvokable]
		public void Add(string prefix, string ns)
		{
			if (prefix != null && prefix.Length > 0)
			{
				XmlConvert.VerifyNCName(prefix);
			}
			if (ns != null && ns.Length > 0)
			{
				XmlConvert.ToUri(ns);
			}
			this.AddInternal(prefix, ns);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x000A8D1B File Offset: 0x000A6F1B
		internal void AddInternal(string prefix, string ns)
		{
			this.Namespaces[prefix] = ns;
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x000A8D2A File Offset: 0x000A6F2A
		[__DynamicallyInvokable]
		public XmlQualifiedName[] ToArray()
		{
			if (this.NamespaceList == null)
			{
				return new XmlQualifiedName[0];
			}
			return (XmlQualifiedName[])this.NamespaceList.ToArray(typeof(XmlQualifiedName));
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x000A8D55 File Offset: 0x000A6F55
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Namespaces.Count;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000A8D64 File Offset: 0x000A6F64
		internal ArrayList NamespaceList
		{
			get
			{
				if (this.namespaces == null || this.namespaces.Count == 0)
				{
					return null;
				}
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.Namespaces.Keys)
				{
					string text = (string)obj;
					arrayList.Add(new XmlQualifiedName(text, (string)this.Namespaces[text]));
				}
				return arrayList;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x000A8DF8 File Offset: 0x000A6FF8
		// (set) Token: 0x06001ED5 RID: 7893 RVA: 0x000A8E13 File Offset: 0x000A7013
		internal Hashtable Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new Hashtable();
				}
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000A8E1C File Offset: 0x000A701C
		internal string LookupPrefix(string ns)
		{
			if (string.IsNullOrEmpty(ns))
			{
				return null;
			}
			if (this.namespaces == null || this.namespaces.Count == 0)
			{
				return null;
			}
			foreach (object obj in this.namespaces.Keys)
			{
				string text = (string)obj;
				if (!string.IsNullOrEmpty(text) && (string)this.namespaces[text] == ns)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x04000CE9 RID: 3305
		private Hashtable namespaces;
	}
}
