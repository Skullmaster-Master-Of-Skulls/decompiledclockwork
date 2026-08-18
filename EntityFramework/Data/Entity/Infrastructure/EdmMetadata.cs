using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000757 RID: 1879
	[Obsolete("EdmMetadata is no longer used. The Code First Migrations <see cref=\"EdmModelDiffer\" /> is used instead.")]
	public class EdmMetadata
	{
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06005523 RID: 21795 RVA: 0x00172964 File Offset: 0x00170B64
		// (set) Token: 0x06005524 RID: 21796 RVA: 0x0017296C File Offset: 0x00170B6C
		public int Id { get; set; }

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06005525 RID: 21797 RVA: 0x00172975 File Offset: 0x00170B75
		// (set) Token: 0x06005526 RID: 21798 RVA: 0x0017297D File Offset: 0x00170B7D
		public string ModelHash { get; set; }

		// Token: 0x06005527 RID: 21799 RVA: 0x00172988 File Offset: 0x00170B88
		public static string TryGetModelHash(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			DbCompiledModel codeFirstModel = context.InternalContext.CodeFirstModel;
			if (codeFirstModel != null)
			{
				return new ModelHashCalculator().Calculate(codeFirstModel);
			}
			return null;
		}
	}
}
