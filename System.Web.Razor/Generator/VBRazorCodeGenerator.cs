using System;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000099 RID: 153
	public class VBRazorCodeGenerator : RazorCodeGenerator
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x00018D2F File Offset: 0x00016F2F
		public VBRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host) : base(className, rootNamespaceName, sourceFileName, host)
		{
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00018D43 File Offset: 0x00016F43
		internal override Func<CodeWriter> CodeWriterFactory
		{
			get
			{
				return () => new VBCodeWriter();
			}
		}
	}
}
