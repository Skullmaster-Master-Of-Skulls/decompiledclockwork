using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000708 RID: 1800
	public class CreateIndexOperation : IndexOperation
	{
		// Token: 0x0600490B RID: 18699 RVA: 0x0015ED0E File Offset: 0x0015CF0E
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public CreateIndexOperation(object anonymousArguments = null) : base(anonymousArguments)
		{
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600490C RID: 18700 RVA: 0x0015ED17 File Offset: 0x0015CF17
		// (set) Token: 0x0600490D RID: 18701 RVA: 0x0015ED1F File Offset: 0x0015CF1F
		public bool IsUnique { get; set; }

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600490E RID: 18702 RVA: 0x0015ED44 File Offset: 0x0015CF44
		public override MigrationOperation Inverse
		{
			get
			{
				DropIndexOperation dropIndexOperation = new DropIndexOperation(this, null)
				{
					Name = base.Name,
					Table = base.Table
				};
				base.Columns.Each(delegate(string c)
				{
					dropIndexOperation.Columns.Add(c);
				});
				return dropIndexOperation;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600490F RID: 18703 RVA: 0x0015ED9B File Offset: 0x0015CF9B
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06004910 RID: 18704 RVA: 0x0015ED9E File Offset: 0x0015CF9E
		// (set) Token: 0x06004911 RID: 18705 RVA: 0x0015EDA6 File Offset: 0x0015CFA6
		public bool IsClustered { get; set; }
	}
}
