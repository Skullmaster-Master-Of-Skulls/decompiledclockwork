using System;
using System.Collections.Generic;

namespace ClockWorkController
{
	// Token: 0x02000010 RID: 16
	public class Utility
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000600C File Offset: 0x0000420C
		public static int[] CommaSeparatedStringToIntArray(string commaSeparatedString)
		{
			string[] array = commaSeparatedString.Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			foreach (string text in array)
			{
				string text2 = text.Trim();
				bool flag = !string.IsNullOrEmpty(text2);
				if (flag)
				{
					int item;
					bool flag2 = int.TryParse(text2, out item);
					if (flag2)
					{
						list.Add(item);
					}
				}
			}
			int[] array3 = new int[list.Count];
			for (int j = 0; j < array3.Length; j++)
			{
				array3[j] = list[j];
			}
			return array3;
		}
	}
}
