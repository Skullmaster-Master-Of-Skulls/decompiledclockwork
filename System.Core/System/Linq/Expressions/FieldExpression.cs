using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200024F RID: 591
	internal class FieldExpression : MemberExpression
	{
		// Token: 0x06001599 RID: 5529 RVA: 0x000487D4 File Offset: 0x000469D4
		public FieldExpression(Expression expression, FieldInfo member) : base(expression)
		{
			this._field = member;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x000487E4 File Offset: 0x000469E4
		internal override MemberInfo GetMember()
		{
			return this._field;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x000487EC File Offset: 0x000469EC
		public sealed override Type Type
		{
			get
			{
				return this._field.FieldType;
			}
		}

		// Token: 0x04000A25 RID: 2597
		private readonly FieldInfo _field;
	}
}
