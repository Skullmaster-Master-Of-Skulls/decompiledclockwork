using System;
using System.CodeDom;
using System.Web.Compilation;

namespace System.Web.WebPages.Razor
{
	// Token: 0x02000003 RID: 3
	internal interface IAssemblyBuilder
	{
		// Token: 0x0600000E RID: 14
		void AddCodeCompileUnit(BuildProvider buildProvider, CodeCompileUnit compileUnit);

		// Token: 0x0600000F RID: 15
		void GenerateTypeFactory(string typeName);
	}
}
