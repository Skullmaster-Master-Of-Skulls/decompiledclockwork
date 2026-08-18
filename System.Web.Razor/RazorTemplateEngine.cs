using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;
using System.Web.Razor.Text;

namespace System.Web.Razor
{
	// Token: 0x02000059 RID: 89
	public class RazorTemplateEngine
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x00011874 File Offset: 0x0000FA74
		public RazorTemplateEngine(RazorEngineHost host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			this.Host = host;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00011891 File Offset: 0x0000FA91
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00011899 File Offset: 0x0000FA99
		public RazorEngineHost Host { get; private set; }

		// Token: 0x06000439 RID: 1081 RVA: 0x000118A4 File Offset: 0x0000FAA4
		public ParserResults ParseTemplate(ITextBuffer input)
		{
			return this.ParseTemplate(input, null);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000118C1 File Offset: 0x0000FAC1
		public ParserResults ParseTemplate(ITextBuffer input, CancellationToken? cancelToken)
		{
			return this.ParseTemplateCore(input.ToDocument(), cancelToken);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000118D0 File Offset: 0x0000FAD0
		public ParserResults ParseTemplate(TextReader input)
		{
			return this.ParseTemplate(input, null);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000118ED File Offset: 0x0000FAED
		public ParserResults ParseTemplate(TextReader input, CancellationToken? cancelToken)
		{
			return this.ParseTemplateCore(new SeekableTextReader(input), cancelToken);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x000118FC File Offset: 0x0000FAFC
		protected internal virtual ParserResults ParseTemplateCore(ITextDocument input, CancellationToken? cancelToken)
		{
			RazorParser razorParser = this.CreateParser();
			return razorParser.Parse(input);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00011918 File Offset: 0x0000FB18
		public GeneratorResults GenerateCode(ITextBuffer input)
		{
			return this.GenerateCode(input, null, null, null, null);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00011938 File Offset: 0x0000FB38
		public GeneratorResults GenerateCode(ITextBuffer input, CancellationToken? cancelToken)
		{
			return this.GenerateCode(input, null, null, null, cancelToken);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00011948 File Offset: 0x0000FB48
		public GeneratorResults GenerateCode(ITextBuffer input, string className, string rootNamespace, string sourceFileName)
		{
			return this.GenerateCode(input, className, rootNamespace, sourceFileName, null);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00011969 File Offset: 0x0000FB69
		public GeneratorResults GenerateCode(ITextBuffer input, string className, string rootNamespace, string sourceFileName, CancellationToken? cancelToken)
		{
			return this.GenerateCodeCore(input.ToDocument(), className, rootNamespace, sourceFileName, cancelToken);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00011980 File Offset: 0x0000FB80
		public GeneratorResults GenerateCode(TextReader input)
		{
			return this.GenerateCode(input, null, null, null, null);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000119A0 File Offset: 0x0000FBA0
		public GeneratorResults GenerateCode(TextReader input, CancellationToken? cancelToken)
		{
			return this.GenerateCode(input, null, null, null, cancelToken);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000119B0 File Offset: 0x0000FBB0
		public GeneratorResults GenerateCode(TextReader input, string className, string rootNamespace, string sourceFileName)
		{
			return this.GenerateCode(input, className, rootNamespace, sourceFileName, null);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000119D1 File Offset: 0x0000FBD1
		public GeneratorResults GenerateCode(TextReader input, string className, string rootNamespace, string sourceFileName, CancellationToken? cancelToken)
		{
			return this.GenerateCodeCore(new SeekableTextReader(input), className, rootNamespace, sourceFileName, cancelToken);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000119E8 File Offset: 0x0000FBE8
		protected internal virtual GeneratorResults GenerateCodeCore(ITextDocument input, string className, string rootNamespace, string sourceFileName, CancellationToken? cancelToken)
		{
			className = ((className ?? this.Host.DefaultClassName) ?? RazorTemplateEngine.DefaultClassName);
			rootNamespace = ((rootNamespace ?? this.Host.DefaultNamespace) ?? RazorTemplateEngine.DefaultNamespace);
			RazorParser razorParser = this.CreateParser();
			ParserResults parserResults = razorParser.Parse(input);
			RazorCodeGenerator razorCodeGenerator = this.CreateCodeGenerator(className, rootNamespace, sourceFileName);
			razorCodeGenerator.DesignTimeMode = this.Host.DesignTimeMode;
			razorCodeGenerator.Visit(parserResults);
			this.Host.PostProcessGeneratedCode(razorCodeGenerator.Context);
			IDictionary<int, GeneratedCodeMapping> designTimeLineMappings = null;
			if (this.Host.DesignTimeMode)
			{
				designTimeLineMappings = razorCodeGenerator.Context.CodeMappings;
			}
			return new GeneratorResults(parserResults, razorCodeGenerator.Context.CompileUnit, designTimeLineMappings);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00011A9B File Offset: 0x0000FC9B
		protected internal virtual RazorCodeGenerator CreateCodeGenerator(string className, string rootNamespace, string sourceFileName)
		{
			return this.Host.DecorateCodeGenerator(this.Host.CodeLanguage.CreateCodeGenerator(className, rootNamespace, sourceFileName, this.Host));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		protected internal virtual RazorParser CreateParser()
		{
			ParserBase incomingCodeParser = this.Host.CodeLanguage.CreateCodeParser();
			ParserBase incomingMarkupParser = this.Host.CreateMarkupParser();
			return new RazorParser(this.Host.DecorateCodeParser(incomingCodeParser), this.Host.DecorateMarkupParser(incomingMarkupParser))
			{
				DesignTimeMode = this.Host.DesignTimeMode
			};
		}

		// Token: 0x04000133 RID: 307
		public static readonly string DefaultClassName = "Template";

		// Token: 0x04000134 RID: 308
		public static readonly string DefaultNamespace = string.Empty;
	}
}
