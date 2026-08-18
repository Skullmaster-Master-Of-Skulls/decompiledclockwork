using System;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x0200006E RID: 110
	public struct LoggingEventData
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000CA3F File Offset: 0x0000AC3F
		public DateTime TimeStampUtc
		{
			get
			{
				if (this.TimeStamp != default(DateTime) && this._timeStampUtc == default(DateTime))
				{
					return this.TimeStamp.ToUniversalTime();
				}
				return this._timeStampUtc;
			}
			set
			{
				this._timeStampUtc = value;
				this.TimeStamp = this._timeStampUtc.ToLocalTime();
			}
		}

		// Token: 0x0400019B RID: 411
		public string LoggerName;

		// Token: 0x0400019C RID: 412
		public Level Level;

		// Token: 0x0400019D RID: 413
		public string Message;

		// Token: 0x0400019E RID: 414
		public string ThreadName;

		// Token: 0x0400019F RID: 415
		[Obsolete("Prefer using TimeStampUtc, since local time can be ambiguous in time zones with daylight savings time.")]
		public DateTime TimeStamp;

		// Token: 0x040001A0 RID: 416
		private DateTime _timeStampUtc;

		// Token: 0x040001A1 RID: 417
		public LocationInfo LocationInfo;

		// Token: 0x040001A2 RID: 418
		public string UserName;

		// Token: 0x040001A3 RID: 419
		public string Identity;

		// Token: 0x040001A4 RID: 420
		public string ExceptionString;

		// Token: 0x040001A5 RID: 421
		public string Domain;

		// Token: 0x040001A6 RID: 422
		public PropertiesDictionary Properties;
	}
}
