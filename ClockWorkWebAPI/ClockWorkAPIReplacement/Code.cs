using System;
using System.Collections;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200004F RID: 79
	public class Code
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001B848 File Offset: 0x00019A48
		public string OriginalCodeText
		{
			get
			{
				bool flag = this.originalCodeText.Length > 0;
				string result;
				if (flag)
				{
					result = this.originalCodeText;
				}
				else
				{
					result = this.codeText;
				}
				return result;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001B87B File Offset: 0x00019A7B
		public Code(string CodeText, long StartIndex, long EndIndex)
		{
			this.codeText = CodeText;
			this.originalCodeText = "";
			this.codeValue = "";
			this.startIndex = StartIndex;
			this.endIndex = EndIndex;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		public Code(string CodeText, long StartIndex, long EndIndex, string OriginalCodeText)
		{
			this.codeText = CodeText;
			this.codeValue = "";
			this.startIndex = StartIndex;
			this.endIndex = EndIndex;
			this.originalCodeText = OriginalCodeText;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001B8E4 File Offset: 0x00019AE4
		public static Code FindCode(ArrayList codes, string codeText)
		{
			foreach (object obj in codes)
			{
				Code code = (Code)obj;
				bool flag = code.codeText.CompareTo(codeText) == 0;
				if (flag)
				{
					return code;
				}
			}
			return null;
		}

		// Token: 0x040001EE RID: 494
		public string codeText;

		// Token: 0x040001EF RID: 495
		public string codeValue;

		// Token: 0x040001F0 RID: 496
		public long startIndex;

		// Token: 0x040001F1 RID: 497
		public long endIndex;

		// Token: 0x040001F2 RID: 498
		private string originalCodeText;
	}
}
