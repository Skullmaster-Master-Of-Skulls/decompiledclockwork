using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001E9 RID: 489
	public sealed class RootContext
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x0006A438 File Offset: 0x00068638
		public RootContext(CodeExpression expression, object value)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.expression = expression;
			this.value = value;
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0006A46A File Offset: 0x0006866A
		public CodeExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0006A472 File Offset: 0x00068672
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000A03 RID: 2563
		private CodeExpression expression;

		// Token: 0x04000A04 RID: 2564
		private object value;
	}
}
