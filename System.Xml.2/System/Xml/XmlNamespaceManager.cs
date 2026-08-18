using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x0200008F RID: 143
	[__DynamicallyInvokable]
	public class XmlNamespaceManager : IXmlNamespaceResolver, IEnumerable
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001341D File Offset: 0x0001161D
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

		// Token: 0x06000525 RID: 1317 RVA: 0x00013440 File Offset: 0x00011640
		internal XmlNamespaceManager()
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00013448 File Offset: 0x00011648
		[__DynamicallyInvokable]
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

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00013507 File Offset: 0x00011707
		[__DynamicallyInvokable]
		public virtual XmlNameTable NameTable
		{
			[__DynamicallyInvokable]
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00013510 File Offset: 0x00011710
		[__DynamicallyInvokable]
		public virtual string DefaultNamespace
		{
			[__DynamicallyInvokable]
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

		// Token: 0x06000529 RID: 1321 RVA: 0x00013533 File Offset: 0x00011733
		[__DynamicallyInvokable]
		public virtual void PushScope()
		{
			this.scopeId++;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00013544 File Offset: 0x00011744
		[__DynamicallyInvokable]
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

		// Token: 0x0600052B RID: 1323 RVA: 0x000135CC File Offset: 0x000117CC
		[__DynamicallyInvokable]
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
			XmlNamespaceManager.NamespaceDeclaration[] array = this.nsdecls;
			int num2 = this.lastDecl + 1;
			this.lastDecl = num2;
			array[num2].Set(prefix, uri, this.scopeId, previousNsIndex);
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

		// Token: 0x0600052C RID: 1324 RVA: 0x00013770 File Offset: 0x00011970
		[__DynamicallyInvokable]
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

		// Token: 0x0600052D RID: 1325 RVA: 0x00013800 File Offset: 0x00011A00
		[__DynamicallyInvokable]
		public virtual IEnumerator GetEnumerator()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(this.lastDecl + 1);
			for (int i = 0; i <= this.lastDecl; i++)
			{
				if (this.nsdecls[i].uri != null)
				{
					dictionary[this.nsdecls[i].prefix] = this.nsdecls[i].prefix;
				}
			}
			return dictionary.Keys.GetEnumerator();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00013878 File Offset: 0x00011A78
		[__DynamicallyInvokable]
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

		// Token: 0x0600052F RID: 1327 RVA: 0x0001393C File Offset: 0x00011B3C
		[__DynamicallyInvokable]
		public virtual string LookupNamespace(string prefix)
		{
			int num = this.LookupNamespaceDecl(prefix);
			if (num != -1)
			{
				return this.nsdecls[num].uri;
			}
			return null;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00013968 File Offset: 0x00011B68
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

		// Token: 0x06000531 RID: 1329 RVA: 0x00013A38 File Offset: 0x00011C38
		[__DynamicallyInvokable]
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

		// Token: 0x06000532 RID: 1330 RVA: 0x00013A94 File Offset: 0x00011C94
		[__DynamicallyInvokable]
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

		// Token: 0x06000533 RID: 1331 RVA: 0x00013B1C File Offset: 0x00011D1C
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

		// Token: 0x04000200 RID: 512
		private static volatile IXmlNamespaceResolver s_EmptyResolver;

		// Token: 0x04000201 RID: 513
		private XmlNamespaceManager.NamespaceDeclaration[] nsdecls;

		// Token: 0x04000202 RID: 514
		private int lastDecl;

		// Token: 0x04000203 RID: 515
		private XmlNameTable nameTable;

		// Token: 0x04000204 RID: 516
		private int scopeId;

		// Token: 0x04000205 RID: 517
		private Dictionary<string, int> hashTable;

		// Token: 0x04000206 RID: 518
		private bool useHashtable;

		// Token: 0x04000207 RID: 519
		private string xml;

		// Token: 0x04000208 RID: 520
		private string xmlNs;

		// Token: 0x04000209 RID: 521
		private const int MinDeclsCountForHashtable = 16;

		// Token: 0x02000314 RID: 788
		private struct NamespaceDeclaration
		{
			// Token: 0x06002DBB RID: 11707 RVA: 0x000EDD6A File Offset: 0x000EBF6A
			public void Set(string prefix, string uri, int scopeId, int previousNsIndex)
			{
				this.prefix = prefix;
				this.uri = uri;
				this.scopeId = scopeId;
				this.previousNsIndex = previousNsIndex;
			}

			// Token: 0x0400148A RID: 5258
			public string prefix;

			// Token: 0x0400148B RID: 5259
			public string uri;

			// Token: 0x0400148C RID: 5260
			public int scopeId;

			// Token: 0x0400148D RID: 5261
			public int previousNsIndex;
		}
	}
}
