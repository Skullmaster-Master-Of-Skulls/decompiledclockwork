using System;
using System.Collections;
using System.IO;
using System.Text;

namespace log4net.DateFormatter
{
	// Token: 0x0200007B RID: 123
	public class AbsoluteTimeDateFormatter : IDateFormatter
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		protected virtual void FormatDateWithoutMillis(DateTime dateToFormat, StringBuilder buffer)
		{
			int hour = dateToFormat.Hour;
			if (hour < 10)
			{
				buffer.Append('0');
			}
			buffer.Append(hour);
			buffer.Append(':');
			int minute = dateToFormat.Minute;
			if (minute < 10)
			{
				buffer.Append('0');
			}
			buffer.Append(minute);
			buffer.Append(':');
			int second = dateToFormat.Second;
			if (second < 10)
			{
				buffer.Append('0');
			}
			buffer.Append(second);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000E344 File Offset: 0x0000C544
		public virtual void FormatDate(DateTime dateToFormat, TextWriter writer)
		{
			lock (AbsoluteTimeDateFormatter.s_lastTimeStrings)
			{
				long num = dateToFormat.Ticks - dateToFormat.Ticks % 10000000L;
				string text = null;
				if (AbsoluteTimeDateFormatter.s_lastTimeToTheSecond != num)
				{
					AbsoluteTimeDateFormatter.s_lastTimeStrings.Clear();
				}
				else
				{
					text = (string)AbsoluteTimeDateFormatter.s_lastTimeStrings[base.GetType()];
				}
				if (text == null)
				{
					lock (AbsoluteTimeDateFormatter.s_lastTimeBuf)
					{
						text = (string)AbsoluteTimeDateFormatter.s_lastTimeStrings[base.GetType()];
						if (text == null)
						{
							AbsoluteTimeDateFormatter.s_lastTimeBuf.Length = 0;
							this.FormatDateWithoutMillis(dateToFormat, AbsoluteTimeDateFormatter.s_lastTimeBuf);
							text = AbsoluteTimeDateFormatter.s_lastTimeBuf.ToString();
							AbsoluteTimeDateFormatter.s_lastTimeStrings[base.GetType()] = text;
							AbsoluteTimeDateFormatter.s_lastTimeToTheSecond = num;
						}
					}
				}
				writer.Write(text);
				writer.Write(',');
				int millisecond = dateToFormat.Millisecond;
				if (millisecond < 100)
				{
					writer.Write('0');
				}
				if (millisecond < 10)
				{
					writer.Write('0');
				}
				writer.Write(millisecond);
			}
		}

		// Token: 0x040001DA RID: 474
		public const string AbsoluteTimeDateFormat = "ABSOLUTE";

		// Token: 0x040001DB RID: 475
		public const string DateAndTimeDateFormat = "DATE";

		// Token: 0x040001DC RID: 476
		public const string Iso8601TimeDateFormat = "ISO8601";

		// Token: 0x040001DD RID: 477
		private static long s_lastTimeToTheSecond = 0L;

		// Token: 0x040001DE RID: 478
		private static StringBuilder s_lastTimeBuf = new StringBuilder();

		// Token: 0x040001DF RID: 479
		private static Hashtable s_lastTimeStrings = new Hashtable();
	}
}
