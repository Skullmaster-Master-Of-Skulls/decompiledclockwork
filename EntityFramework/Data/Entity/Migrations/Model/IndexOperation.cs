using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000707 RID: 1799
	public abstract class IndexOperation : MigrationOperation
	{
		// Token: 0x06004902 RID: 18690 RVA: 0x0015EC50 File Offset: 0x0015CE50
		public static string BuildDefaultName(IEnumerable<string> columns)
		{
			Check.NotNull<IEnumerable<string>>(columns, "columns");
			return string.Format(CultureInfo.InvariantCulture, "IX_{0}", new object[]
			{
				columns.Join(null, "_")
			}).RestrictTo(128);
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x0015EC99 File Offset: 0x0015CE99
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected IndexOperation(object anonymousArguments = null) : base(anonymousArguments)
		{
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06004904 RID: 18692 RVA: 0x0015ECAD File Offset: 0x0015CEAD
		// (set) Token: 0x06004905 RID: 18693 RVA: 0x0015ECB5 File Offset: 0x0015CEB5
		public string Table
		{
			get
			{
				return this._table;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._table = value;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06004906 RID: 18694 RVA: 0x0015ECCA File Offset: 0x0015CECA
		public IList<string> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x0015ECD2 File Offset: 0x0015CED2
		public bool HasDefaultName
		{
			get
			{
				return string.Equals(this.Name, this.DefaultName, StringComparison.Ordinal);
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06004908 RID: 18696 RVA: 0x0015ECE6 File Offset: 0x0015CEE6
		// (set) Token: 0x06004909 RID: 18697 RVA: 0x0015ECF8 File Offset: 0x0015CEF8
		public string Name
		{
			get
			{
				return this._name ?? this.DefaultName;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600490A RID: 18698 RVA: 0x0015ED01 File Offset: 0x0015CF01
		internal string DefaultName
		{
			get
			{
				return IndexOperation.BuildDefaultName(this.Columns);
			}
		}

		// Token: 0x04001B20 RID: 6944
		private string _table;

		// Token: 0x04001B21 RID: 6945
		private readonly List<string> _columns = new List<string>();

		// Token: 0x04001B22 RID: 6946
		private string _name;
	}
}
