using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x02000724 RID: 1828
	internal class EmptyContext : DbContext
	{
		// Token: 0x06004B25 RID: 19237 RVA: 0x001610C5 File Offset: 0x0015F2C5
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EmptyContext(DbConnection existingConnection) : base(existingConnection, false)
		{
			this.InternalContext.InitializerDisabled = true;
		}
	}
}
