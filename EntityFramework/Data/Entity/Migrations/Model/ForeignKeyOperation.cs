using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000701 RID: 1793
	public abstract class ForeignKeyOperation : MigrationOperation
	{
		// Token: 0x060048C9 RID: 18633 RVA: 0x0015E3BF File Offset: 0x0015C5BF
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected ForeignKeyOperation(object anonymousArguments = null) : base(anonymousArguments)
		{
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060048CA RID: 18634 RVA: 0x0015E3D3 File Offset: 0x0015C5D3
		// (set) Token: 0x060048CB RID: 18635 RVA: 0x0015E3DB File Offset: 0x0015C5DB
		public string PrincipalTable
		{
			get
			{
				return this._principalTable;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._principalTable = value;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060048CC RID: 18636 RVA: 0x0015E3F0 File Offset: 0x0015C5F0
		// (set) Token: 0x060048CD RID: 18637 RVA: 0x0015E3F8 File Offset: 0x0015C5F8
		public string DependentTable
		{
			get
			{
				return this._dependentTable;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this._dependentTable = value;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060048CE RID: 18638 RVA: 0x0015E40D File Offset: 0x0015C60D
		public IList<string> DependentColumns
		{
			get
			{
				return this._dependentColumns;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x0015E415 File Offset: 0x0015C615
		public bool HasDefaultName
		{
			get
			{
				return string.Equals(this.Name, this.DefaultName, StringComparison.Ordinal);
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060048D0 RID: 18640 RVA: 0x0015E429 File Offset: 0x0015C629
		// (set) Token: 0x060048D1 RID: 18641 RVA: 0x0015E43B File Offset: 0x0015C63B
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

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060048D2 RID: 18642 RVA: 0x0015E444 File Offset: 0x0015C644
		internal string DefaultName
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "FK_{0}_{1}_{2}", new object[]
				{
					this.DependentTable,
					this.PrincipalTable,
					this.DependentColumns.Join(null, "_")
				}).RestrictTo(128);
			}
		}

		// Token: 0x04001B0A RID: 6922
		private string _principalTable;

		// Token: 0x04001B0B RID: 6923
		private string _dependentTable;

		// Token: 0x04001B0C RID: 6924
		private readonly List<string> _dependentColumns = new List<string>();

		// Token: 0x04001B0D RID: 6925
		private string _name;
	}
}
