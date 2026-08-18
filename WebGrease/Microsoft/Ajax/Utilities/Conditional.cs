using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200007C RID: 124
	public sealed class Conditional : Expression
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000234E3 File Offset: 0x000216E3
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x0002352B File Offset: 0x0002172B
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

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00023564 File Offset: 0x00021764
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x000235AB File Offset: 0x000217AB
		public AstNode TrueExpression
		{
			get
			{
				return this.m_trueExpression;
			}
			set
			{
				this.m_trueExpression.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_trueExpression = value;
				this.m_trueExpression.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x000235E4 File Offset: 0x000217E4
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x0002362B File Offset: 0x0002182B
		public AstNode FalseExpression
		{
			get
			{
				return this.m_falseExpression;
			}
			set
			{
				this.m_falseExpression.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_falseExpression = value;
				this.m_falseExpression.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00023664 File Offset: 0x00021864
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x0002366C File Offset: 0x0002186C
		public Context QuestionContext { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00023675 File Offset: 0x00021875
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x0002367D File Offset: 0x0002187D
		public Context ColonContext { get; set; }

		// Token: 0x06000797 RID: 1943 RVA: 0x00023686 File Offset: 0x00021886
		public Conditional(Context context) : base(context)
		{
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0002368F File Offset: 0x0002188F
		public override OperatorPrecedence Precedence
		{
			get
			{
				return OperatorPrecedence.Conditional;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00023694 File Offset: 0x00021894
		public void SwapBranches()
		{
			AstNode trueExpression = this.m_trueExpression;
			this.m_trueExpression = this.m_falseExpression;
			this.m_falseExpression = trueExpression;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000236BC File Offset: 0x000218BC
		public override PrimitiveType FindPrimitiveType()
		{
			if (this.TrueExpression != null && this.FalseExpression != null)
			{
				PrimitiveType primitiveType = this.TrueExpression.FindPrimitiveType();
				if (primitiveType == this.FalseExpression.FindPrimitiveType())
				{
					return primitiveType;
				}
			}
			return PrimitiveType.Other;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000236F8 File Offset: 0x000218F8
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			Conditional conditional = otherNode as Conditional;
			return conditional != null && this.Condition.IsEquivalentTo(conditional.Condition) && this.TrueExpression.IsEquivalentTo(conditional.TrueExpression) && this.FalseExpression.IsEquivalentTo(conditional.FalseExpression);
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00023748 File Offset: 0x00021948
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Condition, this.TrueExpression, this.FalseExpression, null);
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00023762 File Offset: 0x00021962
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0002376E File Offset: 0x0002196E
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Condition == oldNode)
			{
				this.Condition = newNode;
				return true;
			}
			if (this.TrueExpression == oldNode)
			{
				this.TrueExpression = newNode;
				return true;
			}
			if (this.FalseExpression == oldNode)
			{
				this.FalseExpression = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x000237A7 File Offset: 0x000219A7
		public override AstNode LeftHandSide
		{
			get
			{
				return this.Condition.LeftHandSide;
			}
		}

		// Token: 0x040002E2 RID: 738
		private AstNode m_condition;

		// Token: 0x040002E3 RID: 739
		private AstNode m_trueExpression;

		// Token: 0x040002E4 RID: 740
		private AstNode m_falseExpression;
	}
}
