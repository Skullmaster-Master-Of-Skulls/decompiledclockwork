using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000037 RID: 55
	internal interface ISyntaxTreeRewriter
	{
		// Token: 0x06000212 RID: 530
		Block Rewrite(Block input);
	}
}
