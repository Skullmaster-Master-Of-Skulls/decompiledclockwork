using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000063 RID: 99
	public sealed class ArrayLiteral : Expression
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00020F1A File Offset: 0x0001F11A
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00020F63 File Offset: 0x0001F163
		public AstNodeList Elements
		{
			get
			{
				return this.m_elements;
			}
			set
			{
				this.m_elements.IfNotNull((AstNodeList n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_elements = value;
				this.m_elements.IfNotNull(delegate(AstNodeList n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00020F9C File Offset: 0x0001F19C
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		public bool MayHaveIssues { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00020FB8 File Offset: 0x0001F1B8
		public int Length
		{
			get
			{
				int num = 0;
				foreach (AstNode astNode in this.m_elements)
				{
					if (!astNode.IsConstant)
					{
						return -1;
					}
					UnaryOperator unaryOperator = astNode as UnaryOperator;
					if (unaryOperator != null && unaryOperator.OperatorToken == JSToken.RestSpread)
					{
						int num2 = (unaryOperator.Operand as ArrayLiteral).IfNotNull((ArrayLiteral a) => a.Length, -1);
						if (num2 < 0)
						{
							return -1;
						}
						num += num2;
					}
					else
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0002106C File Offset: 0x0001F26C
		public ArrayLiteral(Context context) : base(context)
		{
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00021075 File Offset: 0x0001F275
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Elements, null, null, null);
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00021085 File Offset: 0x0001F285
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00021094 File Offset: 0x0001F294
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (oldNode == this.Elements)
			{
				if (newNode == null)
				{
					this.Elements = null;
					return true;
				}
				AstNodeList astNodeList = newNode as AstNodeList;
				if (astNodeList != null)
				{
					this.Elements = astNodeList;
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x000210CA File Offset: 0x0001F2CA
		public override bool IsConstant
		{
			get
			{
				return this.Elements == null || this.Elements.IsConstant;
			}
		}

		// Token: 0x0400025C RID: 604
		private AstNodeList m_elements;
	}
}
