using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000705 RID: 1797
	public class AlterColumnOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x060048E8 RID: 18664 RVA: 0x0015E6FB File Offset: 0x0015C8FB
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public AlterColumnOperation(string table, ColumnModel column, bool isDestructiveChange, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<ColumnModel>(column, "column");
			this._table = table;
			this._column = column;
			this._destructiveChange = isDestructiveChange;
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x0015E732 File Offset: 0x0015C932
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public AlterColumnOperation(string table, ColumnModel column, bool isDestructiveChange, AlterColumnOperation inverse, object anonymousArguments = null) : this(table, column, isDestructiveChange, anonymousArguments)
		{
			Check.NotNull<AlterColumnOperation>(inverse, "inverse");
			this._inverse = inverse;
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060048EA RID: 18666 RVA: 0x0015E754 File Offset: 0x0015C954
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x0015E75C File Offset: 0x0015C95C
		public ColumnModel Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x0015E764 File Offset: 0x0015C964
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x0015E76C File Offset: 0x0015C96C
		public override bool IsDestructiveChange
		{
			get
			{
				return this._destructiveChange;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060048EE RID: 18670 RVA: 0x0015E774 File Offset: 0x0015C974
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				AlterColumnOperation alterColumnOperation = this.Inverse as AlterColumnOperation;
				return this.Column.Annotations.Any<KeyValuePair<string, AnnotationValues>>() || (alterColumnOperation != null && alterColumnOperation.Column.Annotations.Any<KeyValuePair<string, AnnotationValues>>());
			}
		}

		// Token: 0x04001B14 RID: 6932
		private readonly string _table;

		// Token: 0x04001B15 RID: 6933
		private readonly ColumnModel _column;

		// Token: 0x04001B16 RID: 6934
		private readonly AlterColumnOperation _inverse;

		// Token: 0x04001B17 RID: 6935
		private readonly bool _destructiveChange;
	}
}
