using System;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003AE RID: 942
	public abstract class FunctionImportStructuralTypeMapping : MappingItem
	{
		// Token: 0x0600225A RID: 8794 RVA: 0x000A0AD1 File Offset: 0x0009ECD1
		internal FunctionImportStructuralTypeMapping(Collection<FunctionImportReturnTypePropertyMapping> columnsRenameList, LineInfo lineInfo)
		{
			this.ColumnsRenameList = columnsRenameList;
			this.LineInfo = lineInfo;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000A0AE7 File Offset: 0x0009ECE7
		public ReadOnlyCollection<FunctionImportReturnTypePropertyMapping> PropertyMappings
		{
			get
			{
				return new ReadOnlyCollection<FunctionImportReturnTypePropertyMapping>(this.ColumnsRenameList);
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000A0AF4 File Offset: 0x0009ECF4
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this.ColumnsRenameList);
			base.SetReadOnly();
		}

		// Token: 0x04000C1C RID: 3100
		internal readonly LineInfo LineInfo;

		// Token: 0x04000C1D RID: 3101
		internal readonly Collection<FunctionImportReturnTypePropertyMapping> ColumnsRenameList;
	}
}
