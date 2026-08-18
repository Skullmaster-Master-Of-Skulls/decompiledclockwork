using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001E1 RID: 481
	public sealed class ExpressionContext
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x00067C78 File Offset: 0x00065E78
		public ExpressionContext(CodeExpression expression, Type expressionType, object owner, object presetValue)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (expressionType == null)
			{
				throw new ArgumentNullException("expressionType");
			}
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._expression = expression;
			this._expressionType = expressionType;
			this._owner = owner;
			this._presetValue = presetValue;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00067CD8 File Offset: 0x00065ED8
		public ExpressionContext(CodeExpression expression, Type expressionType, object owner) : this(expression, expressionType, owner, null)
		{
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00067CE4 File Offset: 0x00065EE4
		public CodeExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00067CEC File Offset: 0x00065EEC
		public Type ExpressionType
		{
			get
			{
				return this._expressionType;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00067CF4 File Offset: 0x00065EF4
		public object Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00067CFC File Offset: 0x00065EFC
		public object PresetValue
		{
			get
			{
				return this._presetValue;
			}
		}

		// Token: 0x040009F4 RID: 2548
		private CodeExpression _expression;

		// Token: 0x040009F5 RID: 2549
		private Type _expressionType;

		// Token: 0x040009F6 RID: 2550
		private object _owner;

		// Token: 0x040009F7 RID: 2551
		private object _presetValue;
	}
}
