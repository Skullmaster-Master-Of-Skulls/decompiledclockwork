using System;
using System.Threading;

namespace System.Xml.Linq
{
	// Token: 0x0200000E RID: 14
	[__DynamicallyInvokable]
	public sealed class XNamespace
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003DFD File Offset: 0x00001FFD
		internal XNamespace(string namespaceName)
		{
			this.namespaceName = namespaceName;
			this.hashCode = namespaceName.GetHashCode();
			this.names = new XHashtable<XName>(new XHashtable<XName>.ExtractKeyDelegate(XNamespace.ExtractLocalName), 8);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003E30 File Offset: 0x00002030
		[__DynamicallyInvokable]
		public string NamespaceName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003E38 File Offset: 0x00002038
		[__DynamicallyInvokable]
		public XName GetName(string localName)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			return this.GetName(localName, 0, localName.Length);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003E56 File Offset: 0x00002056
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.namespaceName;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003E5E File Offset: 0x0000205E
		[__DynamicallyInvokable]
		public static XNamespace None
		{
			[__DynamicallyInvokable]
			get
			{
				return XNamespace.EnsureNamespace(ref XNamespace.refNone, string.Empty);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003E6F File Offset: 0x0000206F
		[__DynamicallyInvokable]
		public static XNamespace Xml
		{
			[__DynamicallyInvokable]
			get
			{
				return XNamespace.EnsureNamespace(ref XNamespace.refXml, "http://www.w3.org/XML/1998/namespace");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003E80 File Offset: 0x00002080
		[__DynamicallyInvokable]
		public static XNamespace Xmlns
		{
			[__DynamicallyInvokable]
			get
			{
				return XNamespace.EnsureNamespace(ref XNamespace.refXmlns, "http://www.w3.org/2000/xmlns/");
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003E91 File Offset: 0x00002091
		[__DynamicallyInvokable]
		public static XNamespace Get(string namespaceName)
		{
			if (namespaceName == null)
			{
				throw new ArgumentNullException("namespaceName");
			}
			return XNamespace.Get(namespaceName, 0, namespaceName.Length);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003EAE File Offset: 0x000020AE
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static implicit operator XNamespace(string namespaceName)
		{
			if (namespaceName == null)
			{
				return null;
			}
			return XNamespace.Get(namespaceName);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003EBB File Offset: 0x000020BB
		[__DynamicallyInvokable]
		public static XName operator +(XNamespace ns, string localName)
		{
			if (ns == null)
			{
				throw new ArgumentNullException("ns");
			}
			return ns.GetName(localName);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003ED8 File Offset: 0x000020D8
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003EDE File Offset: 0x000020DE
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003EE6 File Offset: 0x000020E6
		[__DynamicallyInvokable]
		public static bool operator ==(XNamespace left, XNamespace right)
		{
			return left == right;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003EEC File Offset: 0x000020EC
		[__DynamicallyInvokable]
		public static bool operator !=(XNamespace left, XNamespace right)
		{
			return left != right;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003EF8 File Offset: 0x000020F8
		internal XName GetName(string localName, int index, int count)
		{
			XName result;
			if (this.names.TryGetValue(localName, index, count, out result))
			{
				return result;
			}
			return this.names.Add(new XName(this, localName.Substring(index, count)));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003F34 File Offset: 0x00002134
		internal static XNamespace Get(string namespaceName, int index, int count)
		{
			if (count == 0)
			{
				return XNamespace.None;
			}
			if (XNamespace.namespaces == null)
			{
				Interlocked.CompareExchange<XHashtable<WeakReference>>(ref XNamespace.namespaces, new XHashtable<WeakReference>(new XHashtable<WeakReference>.ExtractKeyDelegate(XNamespace.ExtractNamespace), 32), null);
			}
			for (;;)
			{
				WeakReference weakReference;
				if (!XNamespace.namespaces.TryGetValue(namespaceName, index, count, out weakReference))
				{
					if (count == "http://www.w3.org/XML/1998/namespace".Length && string.CompareOrdinal(namespaceName, index, "http://www.w3.org/XML/1998/namespace", 0, count) == 0)
					{
						break;
					}
					if (count == "http://www.w3.org/2000/xmlns/".Length && string.CompareOrdinal(namespaceName, index, "http://www.w3.org/2000/xmlns/", 0, count) == 0)
					{
						goto Block_7;
					}
					weakReference = XNamespace.namespaces.Add(new WeakReference(new XNamespace(namespaceName.Substring(index, count))));
				}
				XNamespace xnamespace = (weakReference != null) ? ((XNamespace)weakReference.Target) : null;
				if (!(xnamespace == null))
				{
					return xnamespace;
				}
			}
			return XNamespace.Xml;
			Block_7:
			return XNamespace.Xmlns;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004003 File Offset: 0x00002203
		private static string ExtractLocalName(XName n)
		{
			return n.LocalName;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000400C File Offset: 0x0000220C
		private static string ExtractNamespace(WeakReference r)
		{
			XNamespace xnamespace;
			if (r == null || (xnamespace = (XNamespace)r.Target) == null)
			{
				return null;
			}
			return xnamespace.NamespaceName;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000403C File Offset: 0x0000223C
		private static XNamespace EnsureNamespace(ref WeakReference refNmsp, string namespaceName)
		{
			XNamespace xnamespace;
			for (;;)
			{
				WeakReference weakReference = refNmsp;
				if (weakReference != null)
				{
					xnamespace = (XNamespace)weakReference.Target;
					if (xnamespace != null)
					{
						break;
					}
				}
				Interlocked.CompareExchange<WeakReference>(ref refNmsp, new WeakReference(new XNamespace(namespaceName)), weakReference);
			}
			return xnamespace;
		}

		// Token: 0x04000065 RID: 101
		internal const string xmlPrefixNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000066 RID: 102
		internal const string xmlnsPrefixNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000067 RID: 103
		private static XHashtable<WeakReference> namespaces;

		// Token: 0x04000068 RID: 104
		private static WeakReference refNone;

		// Token: 0x04000069 RID: 105
		private static WeakReference refXml;

		// Token: 0x0400006A RID: 106
		private static WeakReference refXmlns;

		// Token: 0x0400006B RID: 107
		private string namespaceName;

		// Token: 0x0400006C RID: 108
		private int hashCode;

		// Token: 0x0400006D RID: 109
		private XHashtable<XName> names;

		// Token: 0x0400006E RID: 110
		private const int NamesCapacity = 8;

		// Token: 0x0400006F RID: 111
		private const int NamespacesCapacity = 32;
	}
}
