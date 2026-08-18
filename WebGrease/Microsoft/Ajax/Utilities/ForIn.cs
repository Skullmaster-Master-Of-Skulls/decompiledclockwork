using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000091 RID: 145
	public sealed class ForIn : IterationStatement
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00028CE3 File Offset: 0x00026EE3
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x00028D2B File Offset: 0x00026F2B
		public AstNode Variable
		{
			get
			{
				return this.m_variable;
			}
			set
			{
				this.m_variable.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_variable = value;
				this.m_variable.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00028D64 File Offset: 0x00026F64
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x00028DAB File Offset: 0x00026FAB
		public AstNode Collection
		{
			get
			{
				return this.m_collection;
			}
			set
			{
				this.m_collection.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_collection = value;
				this.m_collection.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00028DE4 File Offset: 0x00026FE4
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00028DEC File Offset: 0x00026FEC
		public Context OperatorContext { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00028DF5 File Offset: 0x00026FF5
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x00028DFD File Offset: 0x00026FFD
		public BlockScope BlockScope { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00028E0E File Offset: 0x0002700E
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

		// Token: 0x060008D7 RID: 2263 RVA: 0x00028E42 File Offset: 0x00027042
		public ForIn(Context context) : base(context)
		{
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00028E4B File Offset: 0x0002704B
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00028E57 File Offset: 0x00027057
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Variable, this.Collection, base.Body, null);
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00028E71 File Offset: 0x00027071
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Variable == oldNode)
			{
				this.Variable = newNode;
				return true;
			}
			if (this.Collection == oldNode)
			{
				this.Collection = newNode;
				return true;
			}
			if (base.Body == oldNode)
			{
				base.Body = AstNode.ForceToBlock(newNode);
				return true;
			}
			return false;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00028EAF File Offset: 0x000270AF
		internal override bool EncloseBlock(EncloseBlockType type)
		{
			return base.Body != null && base.Body.EncloseBlock(type);
		}

		// Token: 0x04000327 RID: 807
		private AstNode m_variable;

		// Token: 0x04000328 RID: 808
		private AstNode m_collection;
	}
}
