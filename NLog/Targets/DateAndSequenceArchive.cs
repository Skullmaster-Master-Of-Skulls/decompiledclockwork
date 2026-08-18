using System;

namespace NLog.Targets
{
	// Token: 0x02000155 RID: 341
	internal class DateAndSequenceArchive
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0001CD72 File Offset: 0x0001AF72
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0001CD7A File Offset: 0x0001AF7A
		public string FileName { get; private set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0001CD83 File Offset: 0x0001AF83
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x0001CD8B File Offset: 0x0001AF8B
		public DateTime Date { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0001CD94 File Offset: 0x0001AF94
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0001CD9C File Offset: 0x0001AF9C
		public int Sequence { get; private set; }

		// Token: 0x06000C6D RID: 3181 RVA: 0x0001CDA5 File Offset: 0x0001AFA5
		public bool HasSameFormattedDate(DateTime date)
		{
			return date.ToString(this._dateFormat) == this._formattedDate;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
		public DateAndSequenceArchive(string fileName, DateTime date, string dateFormat, int sequence)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (dateFormat == null)
			{
				throw new ArgumentNullException("dateFormat");
			}
			this.Date = date;
			this._dateFormat = dateFormat;
			this.Sequence = sequence;
			this.FileName = fileName;
			this._formattedDate = date.ToString(dateFormat);
		}

		// Token: 0x04000327 RID: 807
		private readonly string _dateFormat;

		// Token: 0x04000328 RID: 808
		private readonly string _formattedDate;
	}
}
