using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x0200070B RID: 1803
	public class DropForeignKeyOperation : ForeignKeyOperation
	{
		// Token: 0x0600492C RID: 18732 RVA: 0x0015F05F File Offset: 0x0015D25F
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropForeignKeyOperation(object anonymousArguments = null) : base(anonymousArguments)
		{
		}

		// Token: 0x0600492D RID: 18733 RVA: 0x0015F068 File Offset: 0x0015D268
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropForeignKeyOperation(AddForeignKeyOperation inverse, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotNull<AddForeignKeyOperation>(inverse, "inverse");
			this._inverse = inverse;
		}

		// Token: 0x0600492E RID: 18734 RVA: 0x0015F0A0 File Offset: 0x0015D2A0
		public virtual DropIndexOperation CreateDropIndexOperation()
		{
			DropIndexOperation dropIndexOperation = new DropIndexOperation(this._inverse.CreateCreateIndexOperation(), null)
			{
				Table = base.DependentTable
			};
			base.DependentColumns.Each(delegate(string c)
			{
				dropIndexOperation.Columns.Add(c);
			});
			return dropIndexOperation;
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x0015F0F5 File Offset: 0x0015D2F5
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06004930 RID: 18736 RVA: 0x0015F0FD File Offset: 0x0015D2FD
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B33 RID: 6963
		private readonly AddForeignKeyOperation _inverse;
	}
}
