using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000090 RID: 144
	public sealed class ForNode : IterationStatement
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00028A3D File Offset: 0x00026C3D
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00028A87 File Offset: 0x00026C87
		public AstNode Initializer
		{
			get
			{
				return this.m_initializer;
			}
			set
			{
				this.m_initializer.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_initializer = value;
				this.m_initializer.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00028AC0 File Offset: 0x00026CC0
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00028B07 File Offset: 0x00026D07
		public AstNode Condition
		{
			get
			{
				return this.m_condition;
			}
			set
			{
				this.m_condition.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_condition = value;
				this.m_condition.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00028B40 File Offset: 0x00026D40
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00028B87 File Offset: 0x00026D87
		public AstNode Incrementer
		{
			get
			{
				return this.m_incrementer;
			}
			set
			{
				this.m_incrementer.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_incrementer = value;
				this.m_incrementer.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00028BC0 File Offset: 0x00026DC0
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x00028BC8 File Offset: 0x00026DC8
		public Context Separator1Context { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00028BD1 File Offset: 0x00026DD1
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00028BD9 File Offset: 0x00026DD9
		public Context Separator2Context { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00028BE2 File Offset: 0x00026DE2
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00028BEA File Offset: 0x00026DEA
		public BlockScope BlockScope { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00028BFB File Offset: 0x00026DFB
		public override Context TerminatingContext
		{
			get
			{
				Context result;
				if ((result = base.TerminatingContext) == null)
				{
					result = base.Body.IfNotNull((Block b) => b.TerminatingContext);
				}
				return result;
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00028C2F File Offset: 0x00026E2F
		public ForNode(Context context) : base(context)
		{
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00028C38 File Offset: 0x00026E38
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00028C44 File Offset: 0x00026E44
		internal override bool EncloseBlock(EncloseBlockType type)
		{
			return base.Body != null && base.Body.Count != 0 && base.Body.EncloseBlock(type);
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00028C69 File Offset: 0x00026E69
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Initializer, this.Condition, this.Incrementer, base.Body);
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00028C88 File Offset: 0x00026E88
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Initializer == oldNode)
			{
				this.Initializer = newNode;
				return true;
			}
			if (this.Condition == oldNode)
			{
				this.Condition = newNode;
				return true;
			}
			if (this.Incrementer == oldNode)
			{
				this.Incrementer = newNode;
				return true;
			}
			if (base.Body == oldNode)
			{
				base.Body = AstNode.ForceToBlock(newNode);
				return true;
			}
			return false;
		}

		// Token: 0x04000320 RID: 800
		private AstNode m_initializer;

		// Token: 0x04000321 RID: 801
		private AstNode m_condition;

		// Token: 0x04000322 RID: 802
		private AstNode m_incrementer;
	}
}
