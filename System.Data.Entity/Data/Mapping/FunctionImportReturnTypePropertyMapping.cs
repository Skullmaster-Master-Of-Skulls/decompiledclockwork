using System;

namespace System.Data.Mapping
{
	// Token: 0x0200025A RID: 602
	internal abstract class FunctionImportReturnTypePropertyMapping
	{
		// Token: 0x06002585 RID: 9605 RVA: 0x0008BE65 File Offset: 0x0008A065
		internal FunctionImportReturnTypePropertyMapping(string cMember, string sColumn, LineInfo lineInfo)
		{
			this.CMember = cMember;
			this.SColumn = sColumn;
			this.LineInfo = lineInfo;
		}

		// Token: 0x0400112D RID: 4397
		internal readonly string CMember;

		// Token: 0x0400112E RID: 4398
		internal readonly string SColumn;

		// Token: 0x0400112F RID: 4399
		internal readonly LineInfo LineInfo;
	}
}
