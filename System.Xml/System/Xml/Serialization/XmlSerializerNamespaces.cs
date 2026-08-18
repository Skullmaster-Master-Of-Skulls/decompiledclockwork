using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200033B RID: 827
	public class XmlSerializerNamespaces
	{
		// Token: 0x06002881 RID: 10369 RVA: 0x000D1A40 File Offset: 0x000D0A40
		public XmlSerializerNamespaces()
		{
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000D1A48 File Offset: 0x000D0A48
		public XmlSerializerNamespaces(XmlSerializerNamespaces namespaces)
		{
			this.namespaces = (Hashtable)namespaces.Namespaces.Clone();
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000D1A68 File Offset: 0x000D0A68
		public XmlSerializerNamespaces(XmlQualifiedName[] namespaces)
		{
			foreach (XmlQualifiedName xmlQualifiedName in namespaces)
			{
				this.Add(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			}
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000D1A9F File Offset: 0x000D0A9F
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

		// Token: 0x06002885 RID: 10373 RVA: 0x000D1ACF File Offset: 0x000D0ACF
		internal void AddInternal(string prefix, string ns)
		{
			this.Namespaces[prefix] = ns;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000D1ADE File Offset: 0x000D0ADE
		public XmlQualifiedName[] ToArray()
		{
			if (this.NamespaceList == null)
			{
				return new XmlQualifiedName[0];
			}
			return (XmlQualifiedName[])this.NamespaceList.ToArray(typeof(XmlQualifiedName));
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x000D1B09 File Offset: 0x000D0B09
		public int Count
		{
			get
			{
				return this.Namespaces.Count;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x000D1B18 File Offset: 0x000D0B18
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

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x000D1BAC File Offset: 0x000D0BAC
		// (set) Token: 0x0600288A RID: 10378 RVA: 0x000D1BC7 File Offset: 0x000D0BC7
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

		// Token: 0x0600288B RID: 10379 RVA: 0x000D1BD0 File Offset: 0x000D0BD0
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

		// Token: 0x04001682 RID: 5762
		private Hashtable namespaces;
	}
}
