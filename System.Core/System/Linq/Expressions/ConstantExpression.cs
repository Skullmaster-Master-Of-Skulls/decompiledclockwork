using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x02000226 RID: 550
	[DebuggerTypeProxy(typeof(Expression.ConstantExpressionProxy))]
	[__DynamicallyInvokable]
	public class ConstantExpression : Expression
	{
		// Token: 0x0600140A RID: 5130 RVA: 0x00043F77 File Offset: 0x00042177
		internal ConstantExpression(object value)
		{
			this._value = value;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x00043F86 File Offset: 0x00042186
		internal static ConstantExpression Make(object value, Type type)
		{
			if ((value == null && type == typeof(object)) || (value != null && value.GetType() == type))
			{
				return new ConstantExpression(value);
			}
			return new TypedConstantExpression(value, type);
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00043FBC File Offset: 0x000421BC
		[__DynamicallyInvokable]
		public override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._value == null)
				{
					return typeof(object);
				}
				return this._value.GetType();
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00043FDC File Offset: 0x000421DC
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Constant;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00043FE0 File Offset: 0x000421E0
		[__DynamicallyInvokable]
		public object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00043FE8 File Offset: 0x000421E8
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitConstant(this);
		}

		// Token: 0x0400097F RID: 2431
		private readonly object _value;
	}
}
