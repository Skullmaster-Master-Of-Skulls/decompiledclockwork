using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000338 RID: 824
	internal sealed class EntityContainerExpression : ExpressionResolution
	{
		// Token: 0x060030FF RID: 12543 RVA: 0x000C17E2 File Offset: 0x000BF9E2
		internal EntityContainerExpression(EntityContainer entityContainer) : base(ExpressionResolutionClass.EntityContainer)
		{
			this.EntityContainer = entityContainer;
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000C17F2 File Offset: 0x000BF9F2
		internal override string ExpressionClassName
		{
			get
			{
				return EntityContainerExpression.EntityContainerClassName;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x000C17F9 File Offset: 0x000BF9F9
		internal static string EntityContainerClassName
		{
			get
			{
				return Strings.LocalizedEntityContainerExpression;
			}
		}

		// Token: 0x0400154B RID: 5451
		internal readonly EntityContainer EntityContainer;
	}
}
