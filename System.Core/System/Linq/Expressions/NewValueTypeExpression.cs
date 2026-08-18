using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000262 RID: 610
	internal class NewValueTypeExpression : NewExpression
	{
		// Token: 0x06001602 RID: 5634 RVA: 0x000491B3 File Offset: 0x000473B3
		internal NewValueTypeExpression(Type type, ReadOnlyCollection<Expression> arguments, ReadOnlyCollection<MemberInfo> members) : base(null, arguments, members)
		{
			this._valueType = type;
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x000491C5 File Offset: 0x000473C5
		public sealed override Type Type
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x04000A4A RID: 2634
		private readonly Type _valueType;
	}
}
