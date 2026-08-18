using System;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace System.Data.Mapping
{
	// Token: 0x02000247 RID: 583
	internal sealed class StorageModificationFunctionResultBinding
	{
		// Token: 0x0600247D RID: 9341 RVA: 0x00084290 File Offset: 0x00082490
		internal StorageModificationFunctionResultBinding(string columnName, EdmProperty property)
		{
			this.ColumnName = EntityUtil.CheckArgumentNull<string>(columnName, "columnName");
			this.Property = EntityUtil.CheckArgumentNull<EdmProperty>(property, "property");
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000842BA File Offset: 0x000824BA
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}->{1}", new object[]
			{
				this.ColumnName,
				this.Property
			});
		}

		// Token: 0x04001030 RID: 4144
		internal readonly string ColumnName;

		// Token: 0x04001031 RID: 4145
		internal readonly EdmProperty Property;
	}
}
