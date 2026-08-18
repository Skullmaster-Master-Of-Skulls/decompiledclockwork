using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000702 RID: 1794
	public class AddForeignKeyOperation : ForeignKeyOperation
	{
		// Token: 0x060048D3 RID: 18643 RVA: 0x0015E498 File Offset: 0x0015C698
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public AddForeignKeyOperation(object anonymousArguments = null) : base(anonymousArguments)
		{
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060048D4 RID: 18644 RVA: 0x0015E4AC File Offset: 0x0015C6AC
		public IList<string> PrincipalColumns
		{
			get
			{
				return this._principalColumns;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x0015E4B4 File Offset: 0x0015C6B4
		// (set) Token: 0x060048D6 RID: 18646 RVA: 0x0015E4BC File Offset: 0x0015C6BC
		public bool CascadeDelete { get; set; }

		// Token: 0x060048D7 RID: 18647 RVA: 0x0015E4E0 File Offset: 0x0015C6E0
		public virtual CreateIndexOperation CreateCreateIndexOperation()
		{
			CreateIndexOperation createIndexOperation = new CreateIndexOperation(null)
			{
				Table = base.DependentTable
			};
			base.DependentColumns.Each(delegate(string c)
			{
				createIndexOperation.Columns.Add(c);
			});
			return createIndexOperation;
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060048D8 RID: 18648 RVA: 0x0015E548 File Offset: 0x0015C748
		public override MigrationOperation Inverse
		{
			get
			{
				DropForeignKeyOperation dropForeignKeyOperation = new DropForeignKeyOperation(null)
				{
					Name = base.Name,
					PrincipalTable = base.PrincipalTable,
					DependentTable = base.DependentTable
				};
				base.DependentColumns.Each(delegate(string c)
				{
					dropForeignKeyOperation.DependentColumns.Add(c);
				});
				return dropForeignKeyOperation;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x0015E5AA File Offset: 0x0015C7AA
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B0E RID: 6926
		private readonly List<string> _principalColumns = new List<string>();
	}
}
