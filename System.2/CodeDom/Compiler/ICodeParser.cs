using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000680 RID: 1664
	public interface ICodeParser
	{
		// Token: 0x06003D4D RID: 15693
		CodeCompileUnit Parse(TextReader codeStream);
	}
}
