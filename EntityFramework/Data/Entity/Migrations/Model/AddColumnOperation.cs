using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000700 RID: 1792
	public class AddColumnOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x060048C1 RID: 18625 RVA: 0x0015E2E5 File Offset: 0x0015C4E5
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public AddColumnOperation(string table, ColumnModel column, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<ColumnModel>(column, "column");
			this._table = table;
			this._column = column;
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060048C2 RID: 18626 RVA: 0x0015E314 File Offset: 0x0015C514
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060048C3 RID: 18627 RVA: 0x0015E31C File Offset: 0x0015C51C
		public ColumnModel Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060048C4 RID: 18628 RVA: 0x0015E33C File Offset: 0x0015C53C
		public override MigrationOperation Inverse
		{
			get
			{
				return new DropColumnOperation(this.Table, this.Column.Name, this.Column.Annotations.ToDictionary((KeyValuePair<string, AnnotationValues> a) => a.Key, (KeyValuePair<string, AnnotationValues> a) => a.Value.NewValue), null);
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060048C5 RID: 18629 RVA: 0x0015E3AA File Offset: 0x0015C5AA
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060048C6 RID: 18630 RVA: 0x0015E3AD File Offset: 0x0015C5AD
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				return this.Column.Annotations.Any<KeyValuePair<string, AnnotationValues>>();
			}
		}

		// Token: 0x04001B06 RID: 6918
		private readonly string _table;

		// Token: 0x04001B07 RID: 6919
		private readonly ColumnModel _column;
	}
}
