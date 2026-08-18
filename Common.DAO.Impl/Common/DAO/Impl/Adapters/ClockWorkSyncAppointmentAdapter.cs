using System;
using System.Windows.Forms;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x0200017B RID: 379
	public static class ClockWorkSyncAppointmentAdapter
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x00078DD4 File Offset: 0x00076FD4
		public static string GetMemoPlainText(this ClockWorkSyncAppointment Appointment)
		{
			bool flag = !string.IsNullOrEmpty(Appointment.Memo);
			if (flag)
			{
				using (RichTextBox richTextBox = new RichTextBox())
				{
					try
					{
						richTextBox.Rtf = Appointment.Memo;
						return richTextBox.Text;
					}
					catch
					{
					}
				}
			}
			return Appointment.Memo;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00078E50 File Offset: 0x00077050
		public static string GetMemoRtf(this ClockWorkSyncAppointment Appointment)
		{
			string text = Appointment.Memo ?? "";
			using (RichTextBox richTextBox = new RichTextBox())
			{
				try
				{
					richTextBox.Text = text;
					return richTextBox.Rtf;
				}
				catch
				{
				}
			}
			return text;
		}
	}
}
