using System;

namespace MailBee
{
	// Token: 0x02000071 RID: 113
	public class LogEntry
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x00009138 File Offset: 0x00008138
		internal LogEntry(DateTime A_0, LogMessageType A_1, string A_2, string A_3, string A_4, string A_5)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = A_4;
			this.f = A_5;
			this.g = true;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00009174 File Offset: 0x00008174
		public DateTime Time
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000917C File Offset: 0x0000817C
		public LogMessageType MessageType
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00009184 File Offset: 0x00008184
		public string ContextInfo
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000918C File Offset: 0x0000818C
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00009194 File Offset: 0x00008194
		public string MessageText
		{
			get
			{
				return this.e;
			}
			set
			{
				this.e = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000919D File Offset: 0x0000819D
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000091A5 File Offset: 0x000081A5
		public string MessageComment
		{
			get
			{
				return this.f;
			}
			set
			{
				this.f = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000091AE File Offset: 0x000081AE
		// (set) Token: 0x060003BF RID: 959 RVA: 0x000091B6 File Offset: 0x000081B6
		public bool AddThisEntry
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000091BF File Offset: 0x000081BF
		public override string ToString()
		{
			return this.d + this.e.Replace("\r\n", "\\r\\n") + this.f;
		}

		// Token: 0x0400017C RID: 380
		private DateTime a;

		// Token: 0x0400017D RID: 381
		private LogMessageType b;

		// Token: 0x0400017E RID: 382
		private string c;

		// Token: 0x0400017F RID: 383
		private string d;

		// Token: 0x04000180 RID: 384
		private string e;

		// Token: 0x04000181 RID: 385
		private string f;

		// Token: 0x04000182 RID: 386
		private bool g;
	}
}
