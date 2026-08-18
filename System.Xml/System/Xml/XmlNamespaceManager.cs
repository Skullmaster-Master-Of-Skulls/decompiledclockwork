using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000040 RID: 64
	public class XmlNamespaceManager : IXmlNamespaceResolver, IEnumerable
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008514 File Offset: 0x00007514
		internal static IXmlNamespaceResolver EmptyResolver
		{
			get
			{
				if (XmlNamespaceManager.s_EmptyResolver == null)
				{
					XmlNamespaceManager.s_EmptyResolver = new XmlNamespaceManager(new NameTable());
				}
				return XmlNamespaceManager.s_EmptyResolver;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008531 File Offset: 0x00007531
		internal XmlNamespaceManager()
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000853C File Offset: 0x0000753C
		public XmlNamespaceManager(XmlNameTable nameTable)
		{
			this.nameTable = nameTable;
			this.xml = nameTable.Add("xml");
			this.xmlNs = nameTable.Add("xmlns");
			this.nsdecls = new XmlNamespaceManager.NamespaceDeclaration[8];
			string text = nameTable.Add(string.Empty);
			this.nsdecls[0].Set(text, text, -1, -1);
			this.nsdecls[1].Set(this.xmlNs, nameTable.Add("http://www.w3.org/2000/xmlns/"), -1, -1);
			this.nsdecls[2].Set(this.xml, nameTable.Add("http://www.w3.org/XML/1998/namespace"), 0, -1);
			this.lastDecl = 2;
			this.scopeId = 1;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000085FB File Offset: 0x000075FB
		public virtual XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008604 File Offset: 0x00007604
		public virtual string DefaultNamespace
		{
			get
			{
				string text = this.LookupNamespace(string.Empty);
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008627 File Offset: 0x00007627
		public virtual void PushScope()
		{
			this.scopeId++;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008638 File Offset: 0x00007638
		public virtual bool PopScope()
		{
			int num = this.lastDecl;
			if (this.scopeId == 1)
			{
				return false;
			}
			while (this.nsdecls[num].scopeId == this.scopeId)
			{
				if (this.useHashtable)
				{
					this.hashTable[this.nsdecls[num].prefix] = this.nsdecls[num].previousNsIndex;
				}
				num--;
			}
			this.lastDecl = num;
			this.scopeId--;
			return true;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000086C0 File Offset: 0x000076C0
		public virtual void AddNamespace(string prefix, string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			prefix = this.nameTable.Add(prefix);
			uri = this.nameTable.Add(uri);
			if (Ref.Equal(this.xml, prefix) && !uri.Equals("http://www.w3.org/XML/1998/namespace"))
			{
				throw new ArgumentException(Res.GetString("Xml_XmlPrefix"));
			}
			if (Ref.Equal(this.xmlNs, prefix))
			{
				throw new ArgumentException(Res.GetString("Xml_XmlnsPrefix"));
			}
			int num = this.LookupNamespaceDecl(prefix);
			int previousNsIndex = -1;
			if (num != -1)
			{
				if (this.nsdecls[num].scopeId == this.scopeId)
				{
					this.nsdecls[num].uri = uri;
					return;
				}
				previousNsIndex = num;
			}
			if (this.lastDecl == this.nsdecls.Length - 1)
			{
				XmlNamespaceManager.NamespaceDeclaration[] destinationArray = new XmlNamespaceManager.NamespaceDeclaration[this.nsdecls.Length * 2];
				Array.Copy(this.nsdecls, 0, destinationArray, 0, this.nsdecls.Length);
				this.nsdecls = destinationArray;
			}
			this.nsdecls[++this.lastDecl].Set(prefix, uri, this.scopeId, previousNsIndex);
			if (this.useHashtable)
			{
				this.hashTable[prefix] = this.lastDecl;
				return;
			}
			if (this.lastDecl >= 16)
			{
				this.hashTable = new Dictionary<string, int>(this.lastDecl);
				for (int i = 0; i <= this.lastDecl; i++)
				{
					this.hashTable[this.nsdecls[i].prefix] = i;
				}
				this.useHashtable = true;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008860 File Offset: 0x00007860
		public virtual void RemoveNamespace(string prefix, string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			for (int num = this.LookupNamespaceDecl(prefix); num != -1; num = this.nsdecls[num].previousNsIndex)
			{
				if (string.Equals(this.nsdecls[num].uri, uri) && this.nsdecls[num].scopeId == this.scopeId)
				{
					this.nsdecls[num].uri = null;
				}
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000088F0 File Offset: 0x000078F0
		public virtual IEnumerator GetEnumerator()
		{
			Hashtable hashtable = new Hashtable(this.lastDecl + 1);
			for (int i = 0; i <= this.lastDecl; i++)
			{
				if (this.nsdecls[i].uri != null)
				{
					hashtable[this.nsdecls[i].prefix] = this.nsdecls[i].prefix;
				}
			}
			return hashtable.Keys.GetEnumerator();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008964 File Offset: 0x00007964
		public virtual IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			int i = 0;
			switch (scope)
			{
			case XmlNamespaceScope.All:
				i = 2;
				break;
			case XmlNamespaceScope.ExcludeXml:
				i = 3;
				break;
			case XmlNamespaceScope.Local:
				i = this.lastDecl;
				while (this.nsdecls[i].scopeId == this.scopeId)
				{
					i--;
				}
				i++;
				break;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(this.lastDecl - i + 1);
			while (i <= this.lastDecl)
			{
				string prefix = this.nsdecls[i].prefix;
				string uri = this.nsdecls[i].uri;
				if (uri != null)
				{
					if (uri.Length > 0 || prefix.Length > 0 || scope == XmlNamespaceScope.Local)
					{
						dictionary[prefix] = uri;
					}
					else
					{
						dictionary.Remove(prefix);
					}
				}
				i++;
			}
			return dictionary;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008A2C File Offset: 0x00007A2C
		public virtual string LookupNamespace(string prefix)
		{
			int num = this.LookupNamespaceDecl(prefix);
			if (num != -1)
			{
				return this.nsdecls[num].uri;
			}
			return null;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008A58 File Offset: 0x00007A58
		private int LookupNamespaceDecl(string prefix)
		{
			if (!this.useHashtable)
			{
				for (int i = this.lastDecl; i >= 0; i--)
				{
					if (this.nsdecls[i].prefix == prefix && this.nsdecls[i].uri != null)
					{
						return i;
					}
				}
				for (int j = this.lastDecl; j >= 0; j--)
				{
					if (string.Equals(this.nsdecls[j].prefix, prefix) && this.nsdecls[j].uri != null)
					{
						return j;
					}
				}
				return -1;
			}
			int previousNsIndex;
			if (this.hashTable.TryGetValue(prefix, out previousNsIndex))
			{
				while (previousNsIndex != -1 && this.nsdecls[previousNsIndex].uri == null)
				{
					previousNsIndex = this.nsdecls[previousNsIndex].previousNsIndex;
				}
				return previousNsIndex;
			}
			return -1;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008B28 File Offset: 0x00007B28
		public virtual string LookupPrefix(string uri)
		{
			for (int i = this.lastDecl; i >= 0; i--)
			{
				if (string.Equals(this.nsdecls[i].uri, uri))
				{
					string prefix = this.nsdecls[i].prefix;
					if (string.Equals(this.LookupNamespace(prefix), uri))
					{
						return prefix;
					}
				}
			}
			return null;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008B84 File Offset: 0x00007B84
		public virtual bool HasNamespace(string prefix)
		{
			int num = this.lastDecl;
			while (this.nsdecls[num].scopeId == this.scopeId)
			{
				if (string.Equals(this.nsdecls[num].prefix, prefix) && this.nsdecls[num].uri != null)
				{
					return prefix.Length > 0 || this.nsdecls[num].uri.Length > 0;
				}
				num--;
			}
			return false;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008C0C File Offset: 0x00007C0C
		internal bool GetNamespaceDeclaration(int idx, out string prefix, out string uri)
		{
			idx = this.lastDecl - idx;
			if (idx < 0)
			{
				string text;
				uri = (text = null);
				prefix = text;
				return false;
			}
			prefix = this.nsdecls[idx].prefix;
			uri = this.nsdecls[idx].uri;
			return true;
		}

		// Token: 0x040004C7 RID: 1223
		private const int MinDeclsCountForHashtable = 16;

		// Token: 0x040004C8 RID: 1224
		private static IXmlNamespaceResolver s_EmptyResolver;

		// Token: 0x040004C9 RID: 1225
		private XmlNamespaceManager.NamespaceDeclaration[] nsdecls;

		// Token: 0x040004CA RID: 1226
		private int lastDecl;

		// Token: 0x040004CB RID: 1227
		private XmlNameTable nameTable;

		// Token: 0x040004CC RID: 1228
		private int scopeId;

		// Token: 0x040004CD RID: 1229
		private Dictionary<string, int> hashTable;

		// Token: 0x040004CE RID: 1230
		private bool useHashtable;

		// Token: 0x040004CF RID: 1231
		private string xml;

		// Token: 0x040004D0 RID: 1232
		private string xmlNs;

		// Token: 0x02000041 RID: 65
		private struct NamespaceDeclaration
		{
			// Token: 0x060001DF RID: 479 RVA: 0x00008C58 File Offset: 0x00007C58
			public void Set(string prefix, string uri, int scopeId, int previousNsIndex)
			{
				this.prefix = prefix;
				this.uri = uri;
				this.scopeId = scopeId;
				this.previousNsIndex = previousNsIndex;
			}

			// Token: 0x040004D1 RID: 1233
			public string prefix;

			// Token: 0x040004D2 RID: 1234
			public string uri;

			// Token: 0x040004D3 RID: 1235
			public int scopeId;

			// Token: 0x040004D4 RID: 1236
			public int previousNsIndex;
		}
	}
}
