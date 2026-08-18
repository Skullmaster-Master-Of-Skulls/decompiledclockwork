using System;
using System.Collections.Generic;

namespace iTextSharp.text
{
	// Token: 0x02000336 RID: 822
	public class Chapter : Section
	{
		// Token: 0x06001DA0 RID: 7584 RVA: 0x000B1EE1 File Offset: 0x000B0EE1
		public Chapter(int number) : base(null, 1)
		{
			this.numbers = new List<int>();
			this.numbers.Add(number);
			this.triggerNewPage = true;
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000B1F09 File Offset: 0x000B0F09
		public Chapter(Paragraph title, int number) : base(title, 1)
		{
			this.numbers = new List<int>();
			this.numbers.Add(number);
			this.triggerNewPage = true;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x000B1F31 File Offset: 0x000B0F31
		public Chapter(string title, int number) : this(new Paragraph(title), number)
		{
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x000B1F40 File Offset: 0x000B0F40
		public override int Type
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x000B1F44 File Offset: 0x000B0F44
		public override bool IsNestable()
		{
			return false;
		}
	}
}
