using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000009 RID: 9
	internal class Axis : AstNode
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000225F File Offset: 0x0000045F
		public Axis(Axis.AxisType axisType, AstNode input, string prefix, string name, XPathNodeType nodetype)
		{
			this.axisType = axisType;
			this.input = input;
			this.prefix = prefix;
			this.name = name;
			this.nodeType = nodetype;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002297 File Offset: 0x00000497
		public Axis(Axis.AxisType axisType, AstNode input) : this(axisType, input, string.Empty, string.Empty, XPathNodeType.All)
		{
			this.abbrAxis = true;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022B4 File Offset: 0x000004B4
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Axis;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022B7 File Offset: 0x000004B7
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022BA File Offset: 0x000004BA
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000022C2 File Offset: 0x000004C2
		public AstNode Input
		{
			get
			{
				return this.input;
			}
			set
			{
				this.input = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022CB File Offset: 0x000004CB
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022D3 File Offset: 0x000004D3
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022DB File Offset: 0x000004DB
		public XPathNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022E3 File Offset: 0x000004E3
		public Axis.AxisType TypeOfAxis
		{
			get
			{
				return this.axisType;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022EB File Offset: 0x000004EB
		public bool AbbrAxis
		{
			get
			{
				return this.abbrAxis;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000022F3 File Offset: 0x000004F3
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000022FB File Offset: 0x000004FB
		public string Urn
		{
			get
			{
				return this.urn;
			}
			set
			{
				this.urn = value;
			}
		}

		// Token: 0x04000055 RID: 85
		private Axis.AxisType axisType;

		// Token: 0x04000056 RID: 86
		private AstNode input;

		// Token: 0x04000057 RID: 87
		private string prefix;

		// Token: 0x04000058 RID: 88
		private string name;

		// Token: 0x04000059 RID: 89
		private XPathNodeType nodeType;

		// Token: 0x0400005A RID: 90
		protected bool abbrAxis;

		// Token: 0x0400005B RID: 91
		private string urn = string.Empty;

		// Token: 0x020002FA RID: 762
		public enum AxisType
		{
			// Token: 0x040013CF RID: 5071
			Ancestor,
			// Token: 0x040013D0 RID: 5072
			AncestorOrSelf,
			// Token: 0x040013D1 RID: 5073
			Attribute,
			// Token: 0x040013D2 RID: 5074
			Child,
			// Token: 0x040013D3 RID: 5075
			Descendant,
			// Token: 0x040013D4 RID: 5076
			DescendantOrSelf,
			// Token: 0x040013D5 RID: 5077
			Following,
			// Token: 0x040013D6 RID: 5078
			FollowingSibling,
			// Token: 0x040013D7 RID: 5079
			Namespace,
			// Token: 0x040013D8 RID: 5080
			Parent,
			// Token: 0x040013D9 RID: 5081
			Preceding,
			// Token: 0x040013DA RID: 5082
			PrecedingSibling,
			// Token: 0x040013DB RID: 5083
			Self,
			// Token: 0x040013DC RID: 5084
			None
		}
	}
}
