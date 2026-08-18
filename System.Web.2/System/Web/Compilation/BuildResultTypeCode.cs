using System;

namespace System.Web.Compilation
{
	// Token: 0x02000810 RID: 2064
	internal enum BuildResultTypeCode
	{
		// Token: 0x0400335B RID: 13147
		Invalid = -1,
		// Token: 0x0400335C RID: 13148
		BuildResultCompiledAssembly = 1,
		// Token: 0x0400335D RID: 13149
		BuildResultCompiledType,
		// Token: 0x0400335E RID: 13150
		BuildResultCompiledTemplateType,
		// Token: 0x0400335F RID: 13151
		BuildResultCustomString = 5,
		// Token: 0x04003360 RID: 13152
		BuildResultMainCodeAssembly,
		// Token: 0x04003361 RID: 13153
		BuildResultCodeCompileUnit,
		// Token: 0x04003362 RID: 13154
		BuildResultCompiledGlobalAsaxType,
		// Token: 0x04003363 RID: 13155
		BuildResultResourceAssembly
	}
}
