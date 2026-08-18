using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls
{
	// Token: 0x02000059 RID: 89
	public class ExcelCommentWrapper : XlsComment
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x00058050 File Offset: 0x00057050
		internal ExcelCommentWrapper(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00058068 File Offset: 0x00057068
		internal ExcelCommentWrapper(spr\u2158 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00058080 File Offset: 0x00057080
		internal ExcelCommentWrapper(spr\u2158 A_0, object A_1, string A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00058098 File Offset: 0x00057098
		public void CopyFrom(ExcelCommentWrapper comment, Dictionary<int, int> fontIndexes)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.CopyFrom(comment, fontIndexes);
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000580DC File Offset: 0x000570DC
		public new RichText RichText
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return new RichText(base.RichText);
			}
		}
	}
}
