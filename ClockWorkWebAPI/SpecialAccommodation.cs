using System;
using System.Collections;

namespace ClockWorkWebAPI
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class SpecialAccommodation
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000F27C File Offset: 0x0000D47C
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000F294 File Offset: 0x0000D494
		public int MaxExamsPerDay
		{
			get
			{
				return this.maxExamsPerDay;
			}
			set
			{
				this.maxExamsPerDay = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000F2B8 File Offset: 0x0000D4B8
		public int NumDaysRestBetweenExams
		{
			get
			{
				return this.numDaysRestBetweenExams;
			}
			set
			{
				this.numDaysRestBetweenExams = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public int NumHoursBetweenExams
		{
			get
			{
				return this.numHoursBetweenExams;
			}
			set
			{
				this.numHoursBetweenExams = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000F300 File Offset: 0x0000D500
		public bool CantBookOnline
		{
			get
			{
				return this.cantBookOnline;
			}
			set
			{
				this.cantBookOnline = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000F30C File Offset: 0x0000D50C
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000F324 File Offset: 0x0000D524
		public string RulesAdded
		{
			get
			{
				return this.rulesadded;
			}
			set
			{
				this.rulesadded = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000F330 File Offset: 0x0000D530
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000F348 File Offset: 0x0000D548
		public ArrayList OnlyAbleToWriteTimeRanges
		{
			get
			{
				return this.onlyAbleToWriteTimeRanges;
			}
			set
			{
				this.onlyAbleToWriteTimeRanges = value;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000F352 File Offset: 0x0000D552
		public SpecialAccommodation()
		{
			this.onlyAbleToWriteTimeRanges = new ArrayList();
		}

		// Token: 0x04000130 RID: 304
		private int maxExamsPerDay = 0;

		// Token: 0x04000131 RID: 305
		private int numDaysRestBetweenExams = 0;

		// Token: 0x04000132 RID: 306
		private int numHoursBetweenExams = 0;

		// Token: 0x04000133 RID: 307
		private bool cantBookOnline = false;

		// Token: 0x04000134 RID: 308
		private string rulesadded = "";

		// Token: 0x04000135 RID: 309
		private ArrayList onlyAbleToWriteTimeRanges;
	}
}
