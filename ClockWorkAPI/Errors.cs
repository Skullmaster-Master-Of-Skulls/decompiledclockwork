using System;
using System.Windows.Forms;

namespace ClockWorkAPI
{
	// Token: 0x02000076 RID: 118
	public class Errors
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00020368 File Offset: 0x0001F368
		public static void ShowErrMsg(string errmsg)
		{
			if (errmsg != null && errmsg.Length > 0)
			{
				MessageBox.Show(errmsg);
			}
		}
	}
}
