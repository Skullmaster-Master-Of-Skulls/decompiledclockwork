using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200024A RID: 586
	internal sealed class EntityContainerExpression : ExpressionResolution
	{
		// Token: 0x060014A6 RID: 5286 RVA: 0x000625C1 File Offset: 0x000607C1
		internal EntityContainerExpression(EntityContainer entityContainer) : base(ExpressionResolutionClass.EntityContainer)
		{
			this.EntityContainer = entityContainer;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x000625D1 File Offset: 0x000607D1
		internal override string ExpressionClassName
		{
			get
			{
				return EntityContainerExpression.EntityContainerClassName;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x000625D8 File Offset: 0x000607D8
		internal static string EntityContainerClassName
		{
			get
			{
				return Strings.LocalizedEntityContainerExpression;
			}
		}

		// Token: 0x04000706 RID: 1798
		internal readonly EntityContainer EntityContainer;
	}
}
