using System;
using System.Text;

namespace log4net.DateFormatter
{
	// Token: 0x0200007D RID: 125
	public class Iso8601DateFormatter : AbsoluteTimeDateFormatter
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x0000E534 File Offset: 0x0000C734
		protected override void FormatDateWithoutMillis(DateTime dateToFormat, StringBuilder buffer)
		{
			buffer.Append(dateToFormat.Year);
			buffer.Append('-');
			int month = dateToFormat.Month;
			if (month < 10)
			{
				buffer.Append('0');
			}
			buffer.Append(month);
			buffer.Append('-');
			int day = dateToFormat.Day;
			if (day < 10)
			{
				buffer.Append('0');
			}
			buffer.Append(day);
			buffer.Append(' ');
			base.FormatDateWithoutMillis(dateToFormat, buffer);
		}
	}
}
