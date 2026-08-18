using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003BD RID: 957
	public sealed class FunctionImportReturnTypeScalarPropertyMapping : FunctionImportReturnTypePropertyMapping
	{
		// Token: 0x060022FB RID: 8955 RVA: 0x000A3477 File Offset: 0x000A1677
		public FunctionImportReturnTypeScalarPropertyMapping(string propertyName, string columnName) : this(Check.NotNull<string>(propertyName, "propertyName"), Check.NotNull<string>(columnName, "columnName"), LineInfo.Empty)
		{
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x000A349A File Offset: 0x000A169A
		internal FunctionImportReturnTypeScalarPropertyMapping(string propertyName, string columnName, LineInfo lineInfo) : base(lineInfo)
		{
			this._propertyName = propertyName;
			this._columnName = columnName;
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x000A34B1 File Offset: 0x000A16B1
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x000A34B9 File Offset: 0x000A16B9
		internal override string CMember
		{
			get
			{
				return this.PropertyName;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x000A34C1 File Offset: 0x000A16C1
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x000A34C9 File Offset: 0x000A16C9
		internal override string SColumn
		{
			get
			{
				return this.ColumnName;
			}
		}

		// Token: 0x04000C45 RID: 3141
		private readonly string _propertyName;

		// Token: 0x04000C46 RID: 3142
		private readonly string _columnName;
	}
}
