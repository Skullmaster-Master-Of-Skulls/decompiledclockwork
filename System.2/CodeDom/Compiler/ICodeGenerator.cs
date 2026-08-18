using System;
using System.IO;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200067F RID: 1663
	public interface ICodeGenerator
	{
		// Token: 0x06003D42 RID: 15682
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		bool IsValidIdentifier(string value);

		// Token: 0x06003D43 RID: 15683
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void ValidateIdentifier(string value);

		// Token: 0x06003D44 RID: 15684
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		string CreateEscapedIdentifier(string value);

		// Token: 0x06003D45 RID: 15685
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		string CreateValidIdentifier(string value);

		// Token: 0x06003D46 RID: 15686
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		string GetTypeOutput(CodeTypeReference type);

		// Token: 0x06003D47 RID: 15687
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		bool Supports(GeneratorSupport supports);

		// Token: 0x06003D48 RID: 15688
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromExpression(CodeExpression e, TextWriter w, CodeGeneratorOptions o);

		// Token: 0x06003D49 RID: 15689
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromStatement(CodeStatement e, TextWriter w, CodeGeneratorOptions o);

		// Token: 0x06003D4A RID: 15690
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromNamespace(CodeNamespace e, TextWriter w, CodeGeneratorOptions o);

		// Token: 0x06003D4B RID: 15691
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromCompileUnit(CodeCompileUnit e, TextWriter w, CodeGeneratorOptions o);

		// Token: 0x06003D4C RID: 15692
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		void GenerateCodeFromType(CodeTypeDeclaration e, TextWriter w, CodeGeneratorOptions o);
	}
}
