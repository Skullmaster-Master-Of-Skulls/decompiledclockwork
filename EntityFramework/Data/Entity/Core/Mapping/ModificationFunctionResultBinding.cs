using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003C9 RID: 969
	public sealed class ModificationFunctionResultBinding : MappingItem
	{
		// Token: 0x06002367 RID: 9063 RVA: 0x000A5188 File Offset: 0x000A3388
		public ModificationFunctionResultBinding(string columnName, EdmProperty property)
		{
			Check.NotNull<string>(columnName, "columnName");
			Check.NotNull<EdmProperty>(property, "property");
			this._columnName = columnName;
			this._property = property;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x000A51B6 File Offset: 0x000A33B6
		// (set) Token: 0x06002369 RID: 9065 RVA: 0x000A51BE File Offset: 0x000A33BE
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
			internal set
			{
				this._columnName = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000A51C7 File Offset: 0x000A33C7
		public EdmProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000A51D0 File Offset: 0x000A33D0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}->{1}", new object[]
			{
				this.ColumnName,
				this.Property
			});
		}

		// Token: 0x04000C73 RID: 3187
		private string _columnName;

		// Token: 0x04000C74 RID: 3188
		private readonly EdmProperty _property;
	}
}
