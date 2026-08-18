using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000074 RID: 116
	public class ConditionalCompilationSet : ConditionalCompilationStatement
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0002275B File Offset: 0x0002095B
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x000227A3 File Offset: 0x000209A3
		public AstNode Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_value = value;
				this.m_value.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000227DC File Offset: 0x000209DC
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x000227E4 File Offset: 0x000209E4
		public string VariableName { get; set; }

		// Token: 0x06000739 RID: 1849 RVA: 0x000227ED File Offset: 0x000209ED
		public ConditionalCompilationSet(Context context) : base(context)
		{
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x000227F6 File Offset: 0x000209F6
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Value, null, null, null);
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00022806 File Offset: 0x00020A06
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00022812 File Offset: 0x00020A12
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Value == oldNode)
			{
				this.Value = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x0400027C RID: 636
		private AstNode m_value;
	}
}
