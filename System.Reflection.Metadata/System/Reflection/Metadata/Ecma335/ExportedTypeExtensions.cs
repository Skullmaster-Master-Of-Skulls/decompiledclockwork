using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000BD RID: 189
	public static class ExportedTypeExtensions
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x000157F0 File Offset: 0x000139F0
		public static int GetTypeDefinitionId(this ExportedType exportedType)
		{
			return exportedType.reader.ExportedTypeTable.GetTypeDefId(exportedType.rowId);
		}
	}
}
