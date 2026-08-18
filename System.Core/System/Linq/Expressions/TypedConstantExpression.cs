using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000227 RID: 551
	internal class TypedConstantExpression : ConstantExpression
	{
		// Token: 0x06001410 RID: 5136 RVA: 0x00043FF1 File Offset: 0x000421F1
		internal TypedConstantExpression(object value, Type type) : base(value)
		{
			this._type = type;
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00044001 File Offset: 0x00042201
		public sealed override Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000980 RID: 2432
		private readonly Type _type;
	}
}
