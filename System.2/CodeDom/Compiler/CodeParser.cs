using System;
using System.IO;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000673 RID: 1651
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class CodeParser : ICodeParser
	{
		// Token: 0x06003C73 RID: 15475
		public abstract CodeCompileUnit Parse(TextReader codeStream);
	}
}
