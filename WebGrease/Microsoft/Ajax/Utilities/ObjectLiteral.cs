using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000B9 RID: 185
	public sealed class ObjectLiteral : Expression
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00037B43 File Offset: 0x00035D43
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00037B8B File Offset: 0x00035D8B
		public AstNodeList Properties
		{
			get
			{
				return this.m_properties;
			}
			set
			{
				this.m_properties.IfNotNull((AstNodeList n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_properties = value;
				this.m_properties.IfNotNull(delegate(AstNodeList n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00037BC4 File Offset: 0x00035DC4
		public override bool IsConstant
		{
			get
			{
				if (this.Properties != null)
				{
					foreach (AstNode astNode in this.Properties)
					{
						if (!astNode.IsConstant)
						{
							return false;
						}
					}
					return true;
				}
				return true;
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00037C24 File Offset: 0x00035E24
		public ObjectLiteral(Context context) : base(context)
		{
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00037C2D File Offset: 0x00035E2D
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00037C39 File Offset: 0x00035E39
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.m_properties, null, null, null);
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00037C4C File Offset: 0x00035E4C
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (oldNode == this.m_properties)
			{
				AstNodeList astNodeList = newNode as AstNodeList;
				if (newNode == null || astNodeList != null)
				{
					this.Properties = astNodeList;
				}
			}
			return false;
		}

		// Token: 0x040004D8 RID: 1240
		private AstNodeList m_properties;
	}
}
