using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004C3 RID: 1219
	internal struct NodeQName
	{
		// Token: 0x06002E1D RID: 11805 RVA: 0x000B3D7E File Offset: 0x000B1F7E
		internal NodeQName(string name)
		{
			this = new NodeQName(name, string.Empty);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000B3D8C File Offset: 0x000B1F8C
		internal NodeQName(string name, string ns)
		{
			this.name = ((name == null) ? string.Empty : name);
			this.ns = ((ns == null) ? string.Empty : ns);
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x000B3DB0 File Offset: 0x000B1FB0
		internal bool IsEmpty
		{
			get
			{
				return this.name.Length == 0 && this.ns.Length == 0;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000B3DCF File Offset: 0x000B1FCF
		internal bool IsNameDefined
		{
			get
			{
				return this.name.Length > 0;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x000B3DDF File Offset: 0x000B1FDF
		internal bool IsNameWildcard
		{
			get
			{
				return this.name == QueryDataModel.Wildcard;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x000B3DEE File Offset: 0x000B1FEE
		internal bool IsNamespaceDefined
		{
			get
			{
				return this.ns.Length > 0;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002E23 RID: 11811 RVA: 0x000B3DFE File Offset: 0x000B1FFE
		internal bool IsNamespaceWildcard
		{
			get
			{
				return this.ns == QueryDataModel.Wildcard;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000B3E0D File Offset: 0x000B200D
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x000B3E15 File Offset: 0x000B2015
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000B3E1D File Offset: 0x000B201D
		internal bool EqualsName(string name)
		{
			return name == this.name;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000B3E2C File Offset: 0x000B202C
		internal bool Equals(NodeQName qname)
		{
			return qname.name.Length == this.name.Length && qname.name == this.name && qname.ns.Length == this.ns.Length && qname.ns == this.ns;
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000B3E91 File Offset: 0x000B2091
		internal bool EqualsNamespace(string ns)
		{
			return ns == this.ns;
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000B3EA0 File Offset: 0x000B20A0
		internal NodeQNameType GetQNameType()
		{
			NodeQNameType nodeQNameType = NodeQNameType.Empty;
			if (this.IsNameDefined)
			{
				if (this.IsNameWildcard)
				{
					nodeQNameType |= NodeQNameType.NameWildcard;
				}
				else
				{
					nodeQNameType |= NodeQNameType.Name;
				}
			}
			if (this.IsNamespaceDefined)
			{
				if (this.IsNamespaceWildcard)
				{
					nodeQNameType |= NodeQNameType.NamespaceWildcard;
				}
				else
				{
					nodeQNameType |= NodeQNameType.Namespace;
				}
			}
			return nodeQNameType;
		}

		// Token: 0x04002534 RID: 9524
		internal static NodeQName Empty = new NodeQName(string.Empty, string.Empty);

		// Token: 0x04002535 RID: 9525
		internal string name;

		// Token: 0x04002536 RID: 9526
		internal string ns;
	}
}
