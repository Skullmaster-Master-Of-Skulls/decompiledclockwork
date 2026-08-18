using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000B6 RID: 182
	public sealed class Member : Expression
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00037840 File Offset: 0x00035A40
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x00037887 File Offset: 0x00035A87
		public AstNode Root
		{
			get
			{
				return this.m_root;
			}
			set
			{
				this.m_root.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_root = value;
				this.m_root.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x000378C0 File Offset: 0x00035AC0
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x000378C8 File Offset: 0x00035AC8
		public string Name { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x000378D1 File Offset: 0x00035AD1
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x000378D9 File Offset: 0x00035AD9
		public Context NameContext { get; set; }

		// Token: 0x06000BDD RID: 3037 RVA: 0x000378E2 File Offset: 0x00035AE2
		public Member(Context context) : base(context)
		{
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x000378EB File Offset: 0x00035AEB
		public override OperatorPrecedence Precedence
		{
			get
			{
				return OperatorPrecedence.FieldAccess;
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000378EF File Offset: 0x00035AEF
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x000378FC File Offset: 0x00035AFC
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			Member member = otherNode as Member;
			return member != null && string.CompareOrdinal(this.Name, member.Name) == 0 && this.Root.IsEquivalentTo(member.Root);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00037939 File Offset: 0x00035B39
		internal override string GetFunctionGuess(AstNode target)
		{
			return this.Root.GetFunctionGuess(this) + '.' + this.Name;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00037959 File Offset: 0x00035B59
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Root, null, null, null);
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00037969 File Offset: 0x00035B69
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Root == oldNode)
			{
				this.Root = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0003797E File Offset: 0x00035B7E
		public override AstNode LeftHandSide
		{
			get
			{
				return this.Root.LeftHandSide;
			}
		}

		// Token: 0x040004D2 RID: 1234
		private AstNode m_root;
	}
}
