using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000CA RID: 202
	public class UnaryOperator : Expression
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0004101F File Offset: 0x0003F21F
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x00041067 File Offset: 0x0003F267
		public AstNode Operand
		{
			get
			{
				return this.m_operand;
			}
			set
			{
				this.m_operand.IfNotNull((AstNode n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_operand = value;
				this.m_operand.IfNotNull(delegate(AstNode n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x000410A0 File Offset: 0x0003F2A0
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x000410A8 File Offset: 0x0003F2A8
		public Context OperatorContext { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x000410B1 File Offset: 0x0003F2B1
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x000410B9 File Offset: 0x0003F2B9
		public JSToken OperatorToken { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000410C2 File Offset: 0x0003F2C2
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x000410CA File Offset: 0x0003F2CA
		public bool IsPostfix { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x000410D3 File Offset: 0x0003F2D3
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x000410DB File Offset: 0x0003F2DB
		public bool OperatorInConditionalCompilationComment { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000410E4 File Offset: 0x0003F2E4
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x000410EC File Offset: 0x0003F2EC
		public bool ConditionalCommentContainsOn { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x000410F5 File Offset: 0x0003F2F5
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x000410FD File Offset: 0x0003F2FD
		public bool IsDelegator { get; set; }

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00041106 File Offset: 0x0003F306
		public UnaryOperator(Context context) : base(context)
		{
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0004110F File Offset: 0x0003F30F
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0004111C File Offset: 0x0003F31C
		public override PrimitiveType FindPrimitiveType()
		{
			switch (this.OperatorToken)
			{
			case JSToken.RestSpread:
			case JSToken.FirstOperator:
			case JSToken.Void:
				return PrimitiveType.Other;
			case JSToken.TypeOf:
				return PrimitiveType.String;
			case JSToken.LogicalNot:
				return PrimitiveType.Boolean;
			}
			return PrimitiveType.Number;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0004115E File Offset: 0x0003F35E
		public override OperatorPrecedence Precedence
		{
			get
			{
				return OperatorPrecedence.Unary;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00041162 File Offset: 0x0003F362
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Operand, null, null, null);
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00041172 File Offset: 0x0003F372
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Operand == oldNode)
			{
				this.Operand = newNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00041188 File Offset: 0x0003F388
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			UnaryOperator unaryOperator = otherNode as UnaryOperator;
			return unaryOperator != null && this.OperatorToken == unaryOperator.OperatorToken && this.Operand.IsEquivalentTo(unaryOperator.Operand);
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000411C8 File Offset: 0x0003F3C8
		public override bool IsConstant
		{
			get
			{
				return this.Operand.IfNotNull((AstNode o) => o.IsConstant);
			}
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000411F2 File Offset: 0x0003F3F2
		public override string ToString()
		{
			return OutputVisitor.OperatorString(this.OperatorToken) + ((this.Operand == null) ? "<null>" : this.Operand.ToString());
		}

		// Token: 0x0400054C RID: 1356
		private AstNode m_operand;
	}
}
