using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000211 RID: 529
	internal sealed class AssignBinaryExpression : BinaryExpression
	{
		// Token: 0x0600120D RID: 4621 RVA: 0x0003C7C4 File Offset: 0x0003A9C4
		internal AssignBinaryExpression(Expression left, Expression right) : base(left, right)
		{
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0003C7CE File Offset: 0x0003A9CE
		public sealed override Type Type
		{
			get
			{
				return base.Left.Type;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0003C7DB File Offset: 0x0003A9DB
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Assign;
			}
		}
	}
}
