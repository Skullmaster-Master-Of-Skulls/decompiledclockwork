using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x0200001A RID: 26
	public class HyperLinksCollection : XlsHyperLinksCollection
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00012998 File Offset: 0x00011998
		internal HyperLinksCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000129B0 File Offset: 0x000119B0
		internal HyperLinksCollection(spr\u2158 A_0, object A_1, bool A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x170000E4 RID: 228
		public new HyperLink this[int index]
		{
			get
			{
				int a_ = 9;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (index > base.Count - 1)
						{
							num = 3;
							continue;
						}
						goto IL_A4;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5B;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						break;
					case 2:
						num = 0;
						continue;
					case 3:
						goto IL_A2;
					}
					goto IL_57;
					IL_5B:
					num = 2;
					continue;
					IL_57:
					if (index >= 0)
					{
						goto IL_5B;
					}
					break;
				}
				IL_65:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾⽀❂⁄㽆", a_), RecordTableEnumerator.b("椾⁀⽂い≆楈⡊ⱌⅎ㽐㱒⅔睖㭘㹚絜㍞Ѡၢᙤ䝦ᵨͪ౬Ů兰䍲啴ᙶ᝸ὺ嵼᡾力권ﮎ戀ﮔ랖\uda98햠莢袤螦風", a_));
				IL_A2:
				goto IL_65;
				IL_A4:
				return base.List[index];
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00012A88 File Offset: 0x00011A88
		public HyperLink Add(CellRange range)
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
			return (HyperLink)base.Add(range);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00012AD0 File Offset: 0x00011AD0
		public new int Add(HyperLink link)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return base.Add(link);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00012B14 File Offset: 0x00011B14
		public HyperLinksCollection GetRangeHyperlinks(CellRange range)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return (HyperLinksCollection)base.GetRangeHyperlinks(range);
		}
	}
}
