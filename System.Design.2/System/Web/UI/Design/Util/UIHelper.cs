using System;
using System.Drawing;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000167 RID: 359
	internal static class UIHelper
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00051E24 File Offset: 0x00050024
		internal static void UpdateFieldsCheckedListBoxColumnWidth(CheckedListBox checkedListBox)
		{
			int num = 0;
			using (Graphics graphics = checkedListBox.CreateGraphics())
			{
				foreach (object obj in checkedListBox.Items)
				{
					string text = obj.ToString();
					num = Math.Max(num, (int)graphics.MeasureString(text, checkedListBox.Font).Width);
				}
			}
			num += 50;
			checkedListBox.ColumnWidth = num;
		}
	}
}
