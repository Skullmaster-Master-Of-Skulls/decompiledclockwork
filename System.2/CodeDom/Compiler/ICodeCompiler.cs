using System;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200067E RID: 1662
	public interface ICodeCompiler
	{
		// Token: 0x06003D3C RID: 15676
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		CompilerResults CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit compilationUnit);

		// Token: 0x06003D3D RID: 15677
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		CompilerResults CompileAssemblyFromFile(CompilerParameters options, string fileName);

		// Token: 0x06003D3E RID: 15678
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		CompilerResults CompileAssemblyFromSource(CompilerParameters options, string source);

		// Token: 0x06003D3F RID: 15679
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		CompilerResults CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] compilationUnits);

		// Token: 0x06003D40 RID: 15680
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		CompilerResults CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames);

		// Token: 0x06003D41 RID: 15681
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		CompilerResults CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources);
	}
}
