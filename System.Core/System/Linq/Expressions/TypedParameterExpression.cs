using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000265 RID: 613
	internal class TypedParameterExpression : ParameterExpression
	{
		// Token: 0x0600160E RID: 5646 RVA: 0x00049356 File Offset: 0x00047556
		internal TypedParameterExpression(Type type, string name) : base(name)
		{
			this._paramType = type;
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00049366 File Offset: 0x00047566
		public sealed override Type Type
		{
			get
			{
				return this._paramType;
			}
		}

		// Token: 0x04000A4C RID: 2636
		private readonly Type _paramType;
	}
}
