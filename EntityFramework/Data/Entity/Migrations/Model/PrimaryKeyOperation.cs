using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000703 RID: 1795
	public abstract class PrimaryKeyOperation : MigrationOperation
	{
		// Token: 0x060048DA RID: 18650 RVA: 0x0015E5B0 File Offset: 0x0015C7B0
		public static string BuildDefaultName(string table)
		{
			Check.NotEmpty(table, "table");
			return string.Format(CultureInfo.InvariantCulture, "PK_{0}", new object[]
			{
				table
			}).RestrictTo(128);
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x0015E5EE File Offset: 0x0015C7EE
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected PrimaryKeyOperation(object anonymousArguments = null) : base(anonymousArguments)
		{
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060048DC RID: 18652 RVA: 0x0015E602 File Offset: 0x0015C802
		// (set) Token: 0x060048DD RID: 18653 RVA: 0x0015E60A File Offset: 0x0015C80A
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

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060048DE RID: 18654 RVA: 0x0015E61F File Offset: 0x0015C81F
		public IList<string> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060048DF RID: 18655 RVA: 0x0015E627 File Offset: 0x0015C827
		public bool HasDefaultName
		{
			get
			{
				return string.Equals(this.Name, this.DefaultName, StringComparison.Ordinal);
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060048E0 RID: 18656 RVA: 0x0015E63B File Offset: 0x0015C83B
		// (set) Token: 0x060048E1 RID: 18657 RVA: 0x0015E64D File Offset: 0x0015C84D
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

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060048E2 RID: 18658 RVA: 0x0015E656 File Offset: 0x0015C856
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x0015E659 File Offset: 0x0015C859
		internal string DefaultName
		{
			get
			{
				return PrimaryKeyOperation.BuildDefaultName(this.Table);
			}
		}

		// Token: 0x04001B10 RID: 6928
		private readonly List<string> _columns = new List<string>();

		// Token: 0x04001B11 RID: 6929
		private string _table;

		// Token: 0x04001B12 RID: 6930
		private string _name;
	}
}
