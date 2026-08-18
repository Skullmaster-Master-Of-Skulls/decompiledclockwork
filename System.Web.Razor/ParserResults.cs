using System;
using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor
{
	// Token: 0x02000056 RID: 86
	public class ParserResults
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x00011565 File Offset: 0x0000F765
		public ParserResults(Block document, IList<RazorError> parserErrors) : this(parserErrors == null || parserErrors.Count == 0, document, parserErrors)
		{
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001157E File Offset: 0x0000F77E
		protected ParserResults(bool success, Block document, IList<RazorError> errors)
		{
			this.Success = success;
			this.Document = document;
			this.ParserErrors = (errors ?? new List<RazorError>());
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000115A4 File Offset: 0x0000F7A4
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x000115AC File Offset: 0x0000F7AC
		public bool Success { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000115B5 File Offset: 0x0000F7B5
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x000115BD File Offset: 0x0000F7BD
		public Block Document { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000115C6 File Offset: 0x0000F7C6
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x000115CE File Offset: 0x0000F7CE
		public IList<RazorError> ParserErrors { get; private set; }
	}
}
