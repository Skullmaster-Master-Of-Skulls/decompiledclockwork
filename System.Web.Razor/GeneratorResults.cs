using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor
{
	// Token: 0x02000057 RID: 87
	public class GeneratorResults : ParserResults
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x000115D7 File Offset: 0x0000F7D7
		public GeneratorResults(ParserResults parserResults, CodeCompileUnit generatedCode, IDictionary<int, GeneratedCodeMapping> designTimeLineMappings) : this(parserResults.Document, parserResults.ParserErrors, generatedCode, designTimeLineMappings)
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000115ED File Offset: 0x0000F7ED
		public GeneratorResults(Block document, IList<RazorError> parserErrors, CodeCompileUnit generatedCode, IDictionary<int, GeneratedCodeMapping> designTimeLineMappings) : this(parserErrors.Count == 0, document, parserErrors, generatedCode, designTimeLineMappings)
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00011603 File Offset: 0x0000F803
		protected GeneratorResults(bool success, Block document, IList<RazorError> parserErrors, CodeCompileUnit generatedCode, IDictionary<int, GeneratedCodeMapping> designTimeLineMappings) : base(success, document, parserErrors)
		{
			this.GeneratedCode = generatedCode;
			this.DesignTimeLineMappings = designTimeLineMappings;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001161E File Offset: 0x0000F81E
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00011626 File Offset: 0x0000F826
		public CodeCompileUnit GeneratedCode { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0001162F File Offset: 0x0000F82F
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x00011637 File Offset: 0x0000F837
		public IDictionary<int, GeneratedCodeMapping> DesignTimeLineMappings { get; private set; }
	}
}
