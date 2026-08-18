using System;
using System.Data.Common.CommandTrees;
using System.Data.Entity;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000337 RID: 823
	internal sealed class ValueExpression : ExpressionResolution
	{
		// Token: 0x060030FC RID: 12540 RVA: 0x000C17C4 File Offset: 0x000BF9C4
		internal ValueExpression(DbExpression value) : base(ExpressionResolutionClass.Value)
		{
			this.Value = value;
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x000C17D4 File Offset: 0x000BF9D4
		internal override string ExpressionClassName
		{
			get
			{
				return ValueExpression.ValueClassName;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000C17DB File Offset: 0x000BF9DB
		internal static string ValueClassName
		{
			get
			{
				return Strings.LocalizedValueExpression;
			}
		}

		// Token: 0x0400154A RID: 5450
		internal readonly DbExpression Value;
	}
}
