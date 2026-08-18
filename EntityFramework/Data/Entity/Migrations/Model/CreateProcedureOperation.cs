using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020002AB RID: 683
	public class CreateProcedureOperation : ProcedureOperation
	{
		// Token: 0x06001815 RID: 6165 RVA: 0x00079787 File Offset: 0x00077987
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public CreateProcedureOperation(string name, string bodySql, object anonymousArguments = null) : base(name, bodySql, anonymousArguments)
		{
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00079792 File Offset: 0x00077992
		public override MigrationOperation Inverse
		{
			get
			{
				return new DropProcedureOperation(this.Name, null);
			}
		}
	}
}
