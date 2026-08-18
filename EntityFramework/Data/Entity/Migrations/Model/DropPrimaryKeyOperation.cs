using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x0200070D RID: 1805
	public class DropPrimaryKeyOperation : PrimaryKeyOperation
	{
		// Token: 0x06004935 RID: 18741 RVA: 0x0015F130 File Offset: 0x0015D330
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropPrimaryKeyOperation(object anonymousArguments = null) : base(anonymousArguments)
		{
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06004936 RID: 18742 RVA: 0x0015F154 File Offset: 0x0015D354
		public override MigrationOperation Inverse
		{
			get
			{
				AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(null)
				{
					Name = base.Name,
					Table = base.Table
				};
				base.Columns.Each(delegate(string c)
				{
					addPrimaryKeyOperation.Columns.Add(c);
				});
				return addPrimaryKeyOperation;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x0015F1AA File Offset: 0x0015D3AA
		// (set) Token: 0x06004938 RID: 18744 RVA: 0x0015F1B2 File Offset: 0x0015D3B2
		public CreateTableOperation CreateTableOperation { get; internal set; }
	}
}
