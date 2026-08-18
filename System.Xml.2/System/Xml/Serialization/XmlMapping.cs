using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000198 RID: 408
	[__DynamicallyInvokable]
	public abstract class XmlMapping
	{
		// Token: 0x06001AE0 RID: 6880 RVA: 0x00076E57 File Offset: 0x00075057
		internal XmlMapping(TypeScope scope, ElementAccessor accessor) : this(scope, accessor, XmlMappingAccess.Read | XmlMappingAccess.Write)
		{
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00076E62 File Offset: 0x00075062
		internal XmlMapping(TypeScope scope, ElementAccessor accessor, XmlMappingAccess access)
		{
			this.scope = scope;
			this.accessor = accessor;
			this.access = access;
			this.shallow = (scope == null);
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x00076E89 File Offset: 0x00075089
		internal ElementAccessor Accessor
		{
			get
			{
				return this.accessor;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00076E91 File Offset: 0x00075091
		internal TypeScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x00076E99 File Offset: 0x00075099
		[__DynamicallyInvokable]
		public string ElementName
		{
			[__DynamicallyInvokable]
			get
			{
				return System.Xml.Serialization.Accessor.UnescapeName(this.Accessor.Name);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00076EAB File Offset: 0x000750AB
		[__DynamicallyInvokable]
		public string XsdElementName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Accessor.Name;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x00076EB8 File Offset: 0x000750B8
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.accessor.Namespace;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00076EC5 File Offset: 0x000750C5
		// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x00076ECD File Offset: 0x000750CD
		internal bool GenerateSerializer
		{
			get
			{
				return this.generateSerializer;
			}
			set
			{
				this.generateSerializer = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00076ED6 File Offset: 0x000750D6
		internal bool IsReadable
		{
			get
			{
				return (this.access & XmlMappingAccess.Read) > XmlMappingAccess.None;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x00076EE3 File Offset: 0x000750E3
		internal bool IsWriteable
		{
			get
			{
				return (this.access & XmlMappingAccess.Write) > XmlMappingAccess.None;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00076EF0 File Offset: 0x000750F0
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x00076EF8 File Offset: 0x000750F8
		internal bool IsSoap
		{
			get
			{
				return this.isSoap;
			}
			set
			{
				this.isSoap = value;
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00076F01 File Offset: 0x00075101
		[__DynamicallyInvokable]
		public void SetKey(string key)
		{
			this.SetKeyInternal(key);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00076F0A File Offset: 0x0007510A
		internal void SetKeyInternal(string key)
		{
			this.key = key;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00076F14 File Offset: 0x00075114
		internal static string GenerateKey(Type type, XmlRootAttribute root, string ns)
		{
			if (root == null)
			{
				root = (XmlRootAttribute)XmlAttributes.GetAttr(type, typeof(XmlRootAttribute));
			}
			return string.Concat(new string[]
			{
				type.FullName,
				":",
				(root == null) ? string.Empty : root.Key,
				":",
				(ns == null) ? string.Empty : ns
			});
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00076F80 File Offset: 0x00075180
		internal string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00076F88 File Offset: 0x00075188
		internal void CheckShallow()
		{
			if (this.shallow)
			{
				throw new InvalidOperationException(Res.GetString("XmlMelformMapping"));
			}
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00076FA4 File Offset: 0x000751A4
		internal static bool IsShallow(XmlMapping[] mappings)
		{
			for (int i = 0; i < mappings.Length; i++)
			{
				if (mappings[i] == null || mappings[i].shallow)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000BFA RID: 3066
		private TypeScope scope;

		// Token: 0x04000BFB RID: 3067
		private bool generateSerializer;

		// Token: 0x04000BFC RID: 3068
		private bool isSoap;

		// Token: 0x04000BFD RID: 3069
		private ElementAccessor accessor;

		// Token: 0x04000BFE RID: 3070
		private string key;

		// Token: 0x04000BFF RID: 3071
		private bool shallow;

		// Token: 0x04000C00 RID: 3072
		private XmlMappingAccess access;
	}
}
