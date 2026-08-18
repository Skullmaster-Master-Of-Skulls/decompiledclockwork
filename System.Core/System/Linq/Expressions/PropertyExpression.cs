using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000250 RID: 592
	internal class PropertyExpression : MemberExpression
	{
		// Token: 0x0600159C RID: 5532 RVA: 0x000487F9 File Offset: 0x000469F9
		public PropertyExpression(Expression expression, PropertyInfo member) : base(expression)
		{
			this._property = member;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00048809 File Offset: 0x00046A09
		internal override MemberInfo GetMember()
		{
			return this._property;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00048811 File Offset: 0x00046A11
		public sealed override Type Type
		{
			get
			{
				return this._property.PropertyType;
			}
		}

		// Token: 0x04000A26 RID: 2598
		private readonly PropertyInfo _property;
	}
}
