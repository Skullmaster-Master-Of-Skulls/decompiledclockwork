using System;
using System.Globalization;
using System.IO;

namespace log4net.DateFormatter
{
	// Token: 0x0200007E RID: 126
	public class SimpleDateFormatter : IDateFormatter
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x0000E5AE File Offset: 0x0000C7AE
		public SimpleDateFormatter(string format)
		{
			this.m_formatString = format;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000E5BD File Offset: 0x0000C7BD
		public virtual void FormatDate(DateTime dateToFormat, TextWriter writer)
		{
			writer.Write(dateToFormat.ToString(this.m_formatString, DateTimeFormatInfo.InvariantInfo));
		}

		// Token: 0x040001E1 RID: 481
		private readonly string m_formatString;
	}
}
