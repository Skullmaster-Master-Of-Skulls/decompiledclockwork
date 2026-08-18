using System;
using System.Globalization;
using System.Text;

namespace log4net.DateFormatter
{
	// Token: 0x0200007C RID: 124
	public class DateTimeDateFormatter : AbsoluteTimeDateFormatter
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0000E4A1 File Offset: 0x0000C6A1
		public DateTimeDateFormatter()
		{
			this.m_dateTimeFormatInfo = DateTimeFormatInfo.InvariantInfo;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		protected override void FormatDateWithoutMillis(DateTime dateToFormat, StringBuilder buffer)
		{
			int day = dateToFormat.Day;
			if (day < 10)
			{
				buffer.Append('0');
			}
			buffer.Append(day);
			buffer.Append(' ');
			buffer.Append(this.m_dateTimeFormatInfo.GetAbbreviatedMonthName(dateToFormat.Month));
			buffer.Append(' ');
			buffer.Append(dateToFormat.Year);
			buffer.Append(' ');
			base.FormatDateWithoutMillis(dateToFormat, buffer);
		}

		// Token: 0x040001E0 RID: 480
		private readonly DateTimeFormatInfo m_dateTimeFormatInfo;
	}
}
