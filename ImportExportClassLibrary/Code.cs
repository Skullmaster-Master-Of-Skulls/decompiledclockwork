using System;
using System.Collections;

namespace ImportExportClassLibrary
{
	// Token: 0x02000029 RID: 41
	public class Code
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000095AC File Offset: 0x000085AC
		public string OriginalCodeText
		{
			get
			{
				if (this.originalCodeText.Length > 0)
				{
					return this.originalCodeText;
				}
				return this.codeText;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000095C9 File Offset: 0x000085C9
		public Code(string CodeText, long StartIndex, long EndIndex)
		{
			this.codeText = CodeText;
			this.originalCodeText = "";
			this.codeValue = "";
			this.startIndex = StartIndex;
			this.endIndex = EndIndex;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000095FC File Offset: 0x000085FC
		public Code(string CodeText, long StartIndex, long EndIndex, string OriginalCodeText)
		{
			this.codeText = CodeText;
			this.codeValue = "";
			this.startIndex = StartIndex;
			this.endIndex = EndIndex;
			this.originalCodeText = OriginalCodeText;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000962C File Offset: 0x0000862C
		public static Code FindCode(ArrayList codes, string codeText)
		{
			foreach (object obj in codes)
			{
				Code code = (Code)obj;
				if (code.codeText.CompareTo(codeText) == 0)
				{
					return code;
				}
			}
			return null;
		}

		// Token: 0x0400008A RID: 138
		public string codeText;

		// Token: 0x0400008B RID: 139
		public string codeValue;

		// Token: 0x0400008C RID: 140
		public long startIndex;

		// Token: 0x0400008D RID: 141
		public long endIndex;

		// Token: 0x0400008E RID: 142
		private string originalCodeText;
	}
}
