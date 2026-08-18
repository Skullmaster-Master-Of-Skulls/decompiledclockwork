using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000B0 RID: 176
	public sealed class LabeledStatement : AstNode
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00036C5E File Offset: 0x00034E5E
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00036CA7 File Offset: 0x00034EA7
		public AstNode Statement
		{
			get
			{
				return this.m_statement;
			}
			set
			{
				this.m_statement.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_statement = value;
				this.m_statement.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00036CE0 File Offset: 0x00034EE0
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00036CE8 File Offset: 0x00034EE8
		public string Label { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00036CF1 File Offset: 0x00034EF1
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00036CF9 File Offset: 0x00034EF9
		public Context LabelContext { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00036D02 File Offset: 0x00034F02
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00036D0A File Offset: 0x00034F0A
		public LabelInfo LabelInfo { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00036D13 File Offset: 0x00034F13
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00036D1B File Offset: 0x00034F1B
		public Context ColonContext { get; set; }

		// Token: 0x06000B60 RID: 2912 RVA: 0x00036D24 File Offset: 0x00034F24
		public LabeledStatement(Context context) : base(context)
		{
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00036D2D File Offset: 0x00034F2D
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00036D39 File Offset: 0x00034F39
		public override AstNode LeftHandSide
		{
			get
			{
				if (this.Statement == null)
				{
					return null;
				}
				return this.Statement.LeftHandSide;
			}
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00036D50 File Offset: 0x00034F50
		internal override bool EncloseBlock(EncloseBlockType type)
		{
			return this.Statement != null && this.Statement.EncloseBlock(type);
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00036D68 File Offset: 0x00034F68
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Statement, null, null, null);
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00036D78 File Offset: 0x00034F78
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Statement == oldNode)
			{
				this.Statement = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x040004BC RID: 1212
		private AstNode m_statement;
	}
}
