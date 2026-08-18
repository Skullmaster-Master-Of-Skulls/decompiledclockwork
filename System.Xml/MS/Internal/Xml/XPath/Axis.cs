using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000127 RID: 295
	internal class Axis : AstNode
	{
		// Token: 0x06001170 RID: 4464 RVA: 0x0004DCAF File Offset: 0x0004CCAF
		public Axis(Axis.AxisType axisType, AstNode input, string prefix, string name, XPathNodeType nodetype)
		{
			this.axisType = axisType;
			this.input = input;
			this.prefix = prefix;
			this.name = name;
			this.nodeType = nodetype;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0004DCE7 File Offset: 0x0004CCE7
		public Axis(Axis.AxisType axisType, AstNode input) : this(axisType, input, string.Empty, string.Empty, XPathNodeType.All)
		{
			this.abbrAxis = true;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0004DD04 File Offset: 0x0004CD04
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Axis;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0004DD07 File Offset: 0x0004CD07
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0004DD0A File Offset: 0x0004CD0A
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x0004DD12 File Offset: 0x0004CD12
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

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0004DD1B File Offset: 0x0004CD1B
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0004DD23 File Offset: 0x0004CD23
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004DD2B File Offset: 0x0004CD2B
		public XPathNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0004DD33 File Offset: 0x0004CD33
		public Axis.AxisType TypeOfAxis
		{
			get
			{
				return this.axisType;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0004DD3B File Offset: 0x0004CD3B
		public bool AbbrAxis
		{
			get
			{
				return this.abbrAxis;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0004DD43 File Offset: 0x0004CD43
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x0004DD4B File Offset: 0x0004CD4B
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

		// Token: 0x04000B28 RID: 2856
		private Axis.AxisType axisType;

		// Token: 0x04000B29 RID: 2857
		private AstNode input;

		// Token: 0x04000B2A RID: 2858
		private string prefix;

		// Token: 0x04000B2B RID: 2859
		private string name;

		// Token: 0x04000B2C RID: 2860
		private XPathNodeType nodeType;

		// Token: 0x04000B2D RID: 2861
		protected bool abbrAxis;

		// Token: 0x04000B2E RID: 2862
		private string urn = string.Empty;

		// Token: 0x02000128 RID: 296
		public enum AxisType
		{
			// Token: 0x04000B30 RID: 2864
			Ancestor,
			// Token: 0x04000B31 RID: 2865
			AncestorOrSelf,
			// Token: 0x04000B32 RID: 2866
			Attribute,
			// Token: 0x04000B33 RID: 2867
			Child,
			// Token: 0x04000B34 RID: 2868
			Descendant,
			// Token: 0x04000B35 RID: 2869
			DescendantOrSelf,
			// Token: 0x04000B36 RID: 2870
			Following,
			// Token: 0x04000B37 RID: 2871
			FollowingSibling,
			// Token: 0x04000B38 RID: 2872
			Namespace,
			// Token: 0x04000B39 RID: 2873
			Parent,
			// Token: 0x04000B3A RID: 2874
			Preceding,
			// Token: 0x04000B3B RID: 2875
			PrecedingSibling,
			// Token: 0x04000B3C RID: 2876
			Self,
			// Token: 0x04000B3D RID: 2877
			None
		}
	}
}
