using System;

namespace OracleInternal.Common
{
	// Token: 0x020000B6 RID: 182
	internal class ColumnLocalParsePrimaryKeyInfo
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x00042568 File Offset: 0x00040768
		internal void CopyPrimaryKeyInfoFrom(ColumnLocalParsePrimaryKeyInfo destInfo)
		{
			this.bIsUnique = destInfo.bIsUnique;
			this.bIsKeyColumn = destInfo.bIsKeyColumn;
		}

		// Token: 0x04000978 RID: 2424
		internal string m_schemaName;

		// Token: 0x04000979 RID: 2425
		internal string m_columnName;

		// Token: 0x0400097A RID: 2426
		internal string pTabAlias;

		// Token: 0x0400097B RID: 2427
		internal string pTabName;

		// Token: 0x0400097C RID: 2428
		internal bool bIsExpression;

		// Token: 0x0400097D RID: 2429
		internal bool Updatable;

		// Token: 0x0400097E RID: 2430
		internal bool bIsUnique;

		// Token: 0x0400097F RID: 2431
		internal bool bIsKeyColumn;

		// Token: 0x04000980 RID: 2432
		internal bool bIsHidden;

		// Token: 0x04000981 RID: 2433
		internal static readonly ColumnLocalParsePrimaryKeyInfo Null = new ColumnLocalParsePrimaryKeyInfo();
	}
}
