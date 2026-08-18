using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000044 RID: 68
	public static class ParserVisitorExtensions
	{
		// Token: 0x06000346 RID: 838 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		public static void Visit(this ParserVisitor self, ParserResults result)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			result.Document.Accept(self);
			foreach (RazorError err in result.ParserErrors)
			{
				self.VisitError(err);
			}
			self.OnComplete();
		}
	}
}
