using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text
{
	// Token: 0x020003BF RID: 959
	public class ChapterAutoNumber : Chapter
	{
		// Token: 0x06002160 RID: 8544 RVA: 0x000C9B0F File Offset: 0x000C8B0F
		public ChapterAutoNumber(Paragraph para) : base(para, 0)
		{
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000C9B19 File Offset: 0x000C8B19
		public ChapterAutoNumber(string title) : base(title, 0)
		{
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000C9B23 File Offset: 0x000C8B23
		public override Section AddSection(string title)
		{
			if (base.AddedCompletely)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("this.largeelement.has.already.been.added.to.the.document"));
			}
			return this.AddSection(title, 2);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000C9B45 File Offset: 0x000C8B45
		public override Section AddSection(Paragraph title)
		{
			if (base.AddedCompletely)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("this.largeelement.has.already.been.added.to.the.document"));
			}
			return this.AddSection(title, 2);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000C9B67 File Offset: 0x000C8B67
		public int SetAutomaticNumber(int number)
		{
			if (!this.numberSet)
			{
				number++;
				base.SetChapterNumber(number);
				this.numberSet = true;
			}
			return number;
		}

		// Token: 0x040016F4 RID: 5876
		protected bool numberSet;
	}
}
