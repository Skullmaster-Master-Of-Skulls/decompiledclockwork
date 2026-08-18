using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Editor
{
	// Token: 0x0200001B RID: 27
	public class EditResult
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00003CB7 File Offset: 0x00001EB7
		public EditResult(PartialParseResult result, SpanBuilder editedSpan)
		{
			this.Result = result;
			this.EditedSpan = editedSpan;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003CCD File Offset: 0x00001ECD
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003CD5 File Offset: 0x00001ED5
		public PartialParseResult Result { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003CDE File Offset: 0x00001EDE
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003CE6 File Offset: 0x00001EE6
		public SpanBuilder EditedSpan { get; set; }
	}
}
