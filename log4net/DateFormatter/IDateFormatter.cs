using System;
using System.IO;

namespace log4net.DateFormatter
{
	// Token: 0x0200007A RID: 122
	public interface IDateFormatter
	{
		// Token: 0x06000452 RID: 1106
		void FormatDate(DateTime dateToFormat, TextWriter writer);
	}
}
