using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000704 RID: 1796
	public class AddPrimaryKeyOperation : PrimaryKeyOperation
	{
		// Token: 0x060048E4 RID: 18660 RVA: 0x0015E666 File Offset: 0x0015C866
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public AddPrimaryKeyOperation(object anonymousArguments = null) : base(anonymousArguments)
		{
			this.IsClustered = true;
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x0015E694 File Offset: 0x0015C894
		public override MigrationOperation Inverse
		{
			get
			{
				DropPrimaryKeyOperation dropPrimaryKeyOperation = new DropPrimaryKeyOperation(null)
				{
					Name = base.Name,
					Table = base.Table
				};
				base.Columns.Each(delegate(string c)
				{
					dropPrimaryKeyOperation.Columns.Add(c);
				});
				return dropPrimaryKeyOperation;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060048E6 RID: 18662 RVA: 0x0015E6EA File Offset: 0x0015C8EA
		// (set) Token: 0x060048E7 RID: 18663 RVA: 0x0015E6F2 File Offset: 0x0015C8F2
		public bool IsClustered { get; set; }
	}
}
