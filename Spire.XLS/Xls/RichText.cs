using System;
using Spire.Xls.Core;

namespace Spire.Xls
{
	// Token: 0x02000109 RID: 265
	public class RichText : RichTextObject
	{
		// Token: 0x06000BFC RID: 3068 RVA: 0x00075C98 File Offset: 0x00074C98
		public RichText(IRichTextString richTextString) : base(richTextString)
		{
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00075CAC File Offset: 0x00074CAC
		public new ExcelFont GetFont(int position)
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
			return new ExcelFont(base.GetFont(position));
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00075CF4 File Offset: 0x00074CF4
		public void SetFont(int startPos, int endPos, ExcelFont font)
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
			base.SetFont(startPos, endPos, font.Font);
		}
	}
}
