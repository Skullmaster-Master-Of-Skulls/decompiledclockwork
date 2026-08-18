using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Linq
{
	// Token: 0x0200000C RID: 12
	[KnownType(typeof(NameSerializer))]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class XName : IEquatable<XName>, ISerializable
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003C27 File Offset: 0x00001E27
		internal XName(XNamespace ns, string localName)
		{
			this.ns = ns;
			this.localName = XmlConvert.VerifyNCName(localName);
			this.hashCode = (ns.GetHashCode() ^ localName.GetHashCode());
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003C55 File Offset: 0x00001E55
		[__DynamicallyInvokable]
		public string LocalName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.localName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003C5D File Offset: 0x00001E5D
		[__DynamicallyInvokable]
		public XNamespace Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003C65 File Offset: 0x00001E65
		[__DynamicallyInvokable]
		public string NamespaceName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns.NamespaceName;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003C72 File Offset: 0x00001E72
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.ns.NamespaceName.Length == 0)
			{
				return this.localName;
			}
			return "{" + this.ns.NamespaceName + "}" + this.localName;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003CB0 File Offset: 0x00001EB0
		[__DynamicallyInvokable]
		public static XName Get(string expandedName)
		{
			if (expandedName == null)
			{
				throw new ArgumentNullException("expandedName");
			}
			if (expandedName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Argument_InvalidExpandedName", new object[]
				{
					expandedName
				}));
			}
			if (expandedName[0] != '{')
			{
				return XNamespace.None.GetName(expandedName);
			}
			int num = expandedName.LastIndexOf('}');
			if (num <= 1 || num == expandedName.Length - 1)
			{
				throw new ArgumentException(Res.GetString("Argument_InvalidExpandedName", new object[]
				{
					expandedName
				}));
			}
			return XNamespace.Get(expandedName, 1, num - 1).GetName(expandedName, num + 1, expandedName.Length - num - 1);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003D53 File Offset: 0x00001F53
		[__DynamicallyInvokable]
		public static XName Get(string localName, string namespaceName)
		{
			return XNamespace.Get(namespaceName).GetName(localName);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003D61 File Offset: 0x00001F61
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static implicit operator XName(string expandedName)
		{
			if (expandedName == null)
			{
				return null;
			}
			return XName.Get(expandedName);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003D6E File Offset: 0x00001F6E
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003D74 File Offset: 0x00001F74
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003D7C File Offset: 0x00001F7C
		[__DynamicallyInvokable]
		public static bool operator ==(XName left, XName right)
		{
			return left == right;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003D82 File Offset: 0x00001F82
		[__DynamicallyInvokable]
		public static bool operator !=(XName left, XName right)
		{
			return left != right;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003D8B File Offset: 0x00001F8B
		[__DynamicallyInvokable]
		bool IEquatable<XName>.Equals(XName other)
		{
			return this == other;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003D91 File Offset: 0x00001F91
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("name", this.ToString());
			info.SetType(typeof(NameSerializer));
		}

		// Token: 0x04000061 RID: 97
		private XNamespace ns;

		// Token: 0x04000062 RID: 98
		private string localName;

		// Token: 0x04000063 RID: 99
		private int hashCode;
	}
}
