using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001A7 RID: 423
	public class AlterProcedureOperation : ProcedureOperation
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x0003EF3A File Offset: 0x0003D13A
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public AlterProcedureOperation(string name, string bodySql, object anonymousArguments = null) : base(name, bodySql, anonymousArguments)
		{
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0003EF45 File Offset: 0x0003D145
		public override MigrationOperation Inverse
		{
			get
			{
				return NotSupportedOperation.Instance;
			}
		}
	}
}
