using System;
using System.Windows.Forms;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x02000182 RID: 386
	public static class MemoAdapters
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x0007920C File Offset: 0x0007740C
		public static string[] SplitMemoTextAndAttendees(this string memo)
		{
			string[] array = new string[2];
			array.Initialize();
			string[] array2 = memo.Split(new string[]
			{
				"\n*-*-*-*-*-*-* Do not edit below this line *-*-*-*-*-*-*\n".Substring(1, "\n*-*-*-*-*-*-* Do not edit below this line *-*-*-*-*-*-*\n".Length - 2),
				"\n*-*-*-*-*-*-* Do not edit above this line *-*-*-*-*-*-*\n".Substring(1, "\n*-*-*-*-*-*-* Do not edit above this line *-*-*-*-*-*-*\n".Length - 2)
			}, StringSplitOptions.RemoveEmptyEntries);
			bool flag = array2.Length == 1;
			if (flag)
			{
				array[0] = array2[0];
				array[1] = string.Empty;
			}
			else
			{
				bool flag2 = array2.Length == 2;
				if (flag2)
				{
					array[0] = array2[0];
					array[1] = array2[1];
				}
				else
				{
					bool flag3 = array2.Length > 2;
					if (flag3)
					{
						array[0] = array2[0] + array2[2];
						array[1] = array2[1];
					}
				}
			}
			return array;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000792CC File Offset: 0x000774CC
		public static string GetMemoRtf(this string memo)
		{
			using (RichTextBox richTextBox = new RichTextBox())
			{
				try
				{
					richTextBox.Text = memo;
					return richTextBox.Rtf;
				}
				catch
				{
				}
			}
			return memo;
		}

		// Token: 0x04000720 RID: 1824
		internal const string CUSTOM_MEMO_HEADER = "\n*-*-*-*-*-*-* Do not edit below this line *-*-*-*-*-*-*\n";

		// Token: 0x04000721 RID: 1825
		internal const string CUSTOM_MEMO_FOOTER = "\n*-*-*-*-*-*-* Do not edit above this line *-*-*-*-*-*-*\n";
	}
}
