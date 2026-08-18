using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000277 RID: 631
	internal sealed class ValueExpression : ExpressionResolution
	{
		// Token: 0x0600162D RID: 5677 RVA: 0x0006B844 File Offset: 0x00069A44
		internal ValueExpression(DbExpression value) : base(ExpressionResolutionClass.Value)
		{
			this.Value = value;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0006B854 File Offset: 0x00069A54
		internal override string ExpressionClassName
		{
			get
			{
				return ValueExpression.ValueClassName;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0006B85B File Offset: 0x00069A5B
		internal static string ValueClassName
		{
			get
			{
				return Strings.LocalizedValueExpression;
			}
		}

		// Token: 0x040007C6 RID: 1990
		internal readonly DbExpression Value;
	}
}
