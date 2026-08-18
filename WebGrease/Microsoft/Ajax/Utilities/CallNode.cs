using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200006B RID: 107
	public sealed class CallNode : Expression
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0002223E File Offset: 0x0002043E
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00022287 File Offset: 0x00020487
		public AstNode Function
		{
			get
			{
				return this.m_function;
			}
			set
			{
				this.m_function.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_function = value;
				this.m_function.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000222C0 File Offset: 0x000204C0
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00022307 File Offset: 0x00020507
		public AstNodeList Arguments
		{
			get
			{
				return this.m_arguments;
			}
			set
			{
				this.m_arguments.IfNotNull((AstNodeList n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_arguments = value;
				this.m_arguments.IfNotNull(delegate(AstNodeList n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00022340 File Offset: 0x00020540
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x00022348 File Offset: 0x00020548
		public bool IsConstructor { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00022351 File Offset: 0x00020551
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00022359 File Offset: 0x00020559
		public bool InBrackets { get; set; }

		// Token: 0x06000705 RID: 1797 RVA: 0x00022362 File Offset: 0x00020562
		public CallNode(Context context) : base(context)
		{
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0002236B File Offset: 0x0002056B
		public override OperatorPrecedence Precedence
		{
			get
			{
				return OperatorPrecedence.FieldAccess;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00022370 File Offset: 0x00020570
		public override bool IsExpression
		{
			get
			{
				Member member = this.Function as Member;
				return member == null || !member.Name.StartsWith("on", StringComparison.Ordinal) || this.Arguments.Count <= 0;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000223B0 File Offset: 0x000205B0
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x000223BC File Offset: 0x000205BC
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Function, this.Arguments, null, null);
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000223D4 File Offset: 0x000205D4
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Function == oldNode)
			{
				this.Function = newNode;
				return true;
			}
			if (this.Arguments == oldNode)
			{
				if (newNode == null)
				{
					this.Arguments = null;
					return true;
				}
				AstNodeList astNodeList = newNode as AstNodeList;
				if (astNodeList != null)
				{
					this.Arguments = astNodeList;
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0002241C File Offset: 0x0002061C
		public override AstNode LeftHandSide
		{
			get
			{
				return this.Function.LeftHandSide;
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0002242C File Offset: 0x0002062C
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			CallNode callNode = otherNode as CallNode;
			return callNode != null && this.InBrackets == callNode.InBrackets && this.IsConstructor == callNode.IsConstructor && this.Function.IsEquivalentTo(callNode.Function) && this.Arguments.IsEquivalentTo(callNode.Arguments);
		}

		// Token: 0x04000274 RID: 628
		private AstNode m_function;

		// Token: 0x04000275 RID: 629
		private AstNodeList m_arguments;
	}
}
