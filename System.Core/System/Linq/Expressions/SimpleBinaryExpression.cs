using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000214 RID: 532
	internal class SimpleBinaryExpression : BinaryExpression
	{
		// Token: 0x06001216 RID: 4630 RVA: 0x0003C827 File Offset: 0x0003AA27
		internal SimpleBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type) : base(left, right)
		{
			this._nodeType = nodeType;
			this._type = type;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0003C840 File Offset: 0x0003AA40
		public sealed override ExpressionType NodeType
		{
			get
			{
				return this._nodeType;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0003C848 File Offset: 0x0003AA48
		public sealed override Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0400095C RID: 2396
		private readonly ExpressionType _nodeType;

		// Token: 0x0400095D RID: 2397
		private readonly Type _type;
	}
}
