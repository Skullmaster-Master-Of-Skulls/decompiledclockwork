using System;
using System.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000631 RID: 1585
	public class StringComparer : IComparer
	{
		// Token: 0x0600610C RID: 24844 RVA: 0x003D56B0 File Offset: 0x003D46B0
		public int Compare(object x, object y)
		{
			string text;
			string text2;
			for (;;)
			{
				text = (x as string);
				text2 = (y as string);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (text == null)
						{
							return 0;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5F;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_87;
					case 3:
						if (text2 != null)
						{
							num = 2;
							continue;
						}
						return 0;
					}
					break;
				}
			}
			IL_5F:
			return string.CompareOrdinal(text, text2);
			IL_87:
			goto IL_5F;
		}
	}
}
