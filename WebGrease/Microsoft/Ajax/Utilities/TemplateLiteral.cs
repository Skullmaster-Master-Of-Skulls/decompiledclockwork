using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000020 RID: 32
	public class TemplateLiteral : Expression
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00006A71 File Offset: 0x00004C71
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00006ABB File Offset: 0x00004CBB
		public Lookup Function
		{
			get
			{
				return this.m_function;
			}
			set
			{
				this.m_function.IfNotNull((Lookup n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_function = value;
				this.m_function.IfNotNull(delegate(Lookup n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00006AF4 File Offset: 0x00004CF4
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00006AFC File Offset: 0x00004CFC
		public string Text { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00006B05 File Offset: 0x00004D05
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00006B0D File Offset: 0x00004D0D
		public Context TextContext { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00006B16 File Offset: 0x00004D16
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00006B5F File Offset: 0x00004D5F
		public AstNodeList Expressions
		{
			get
			{
				return this.m_expressions;
			}
			set
			{
				this.m_expressions.IfNotNull((AstNodeList n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_expressions = value;
				this.m_expressions.IfNotNull(delegate(AstNodeList n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00006B98 File Offset: 0x00004D98
		public TemplateLiteral(Context context) : base(context)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00006BA1 File Offset: 0x00004DA1
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00006BAD File Offset: 0x00004DAD
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.m_function, this.m_expressions, null, null);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00006BD8 File Offset: 0x00004DD8
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Function == oldNode)
			{
				return (newNode as Lookup).IfNotNull(delegate(Lookup lookup)
				{
					this.Function = lookup;
					return true;
				});
			}
			if (this.Expressions == oldNode)
			{
				return (newNode as AstNodeList).IfNotNull(delegate(AstNodeList list)
				{
					this.Expressions = list;
					return true;
				});
			}
			return false;
		}

		// Token: 0x04000078 RID: 120
		private Lookup m_function;

		// Token: 0x04000079 RID: 121
		private AstNodeList m_expressions;
	}
}
