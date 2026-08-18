using System;

namespace MailBee.Mime
{
	// Token: 0x02000561 RID: 1377
	public class MessageBuilderConfig
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x000DAD15 File Offset: 0x000D9D15
		// (set) Token: 0x06002D48 RID: 11592 RVA: 0x000DAD1D File Offset: 0x000D9D1D
		internal bool NeedToRebuild
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x000DAD26 File Offset: 0x000D9D26
		// (set) Token: 0x06002D4A RID: 11594 RVA: 0x000DAD2E File Offset: 0x000D9D2E
		public ReplaceUriWithCidHandler OnReplaceUriWithCid
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x000DAD37 File Offset: 0x000D9D37
		// (set) Token: 0x06002D4C RID: 11596 RVA: 0x000DAD3F File Offset: 0x000D9D3F
		public AddressDelimeterChar AddressDelimeter
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
				this.b = true;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x000DAD4F File Offset: 0x000D9D4F
		// (set) Token: 0x06002D4E RID: 11598 RVA: 0x000DAD57 File Offset: 0x000D9D57
		public HtmlToPlainAutoConvert HtmlToPlainMode
		{
			get
			{
				return this.e;
			}
			set
			{
				this.e = value;
				this.b = true;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x000DAD67 File Offset: 0x000D9D67
		// (set) Token: 0x06002D50 RID: 11600 RVA: 0x000DAD6F File Offset: 0x000D9D6F
		public HtmlToPlainConvertOptions HtmlToPlainOptions
		{
			get
			{
				return this.f;
			}
			set
			{
				this.f = value;
				this.b = true;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x000DAD7F File Offset: 0x000D9D7F
		// (set) Token: 0x06002D52 RID: 11602 RVA: 0x000DAD87 File Offset: 0x000D9D87
		public string RelatedFilesFolder
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

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x000DAD90 File Offset: 0x000D9D90
		// (set) Token: 0x06002D54 RID: 11604 RVA: 0x000DAD98 File Offset: 0x000D9D98
		public bool RemoveBccOnSend
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x000DADA1 File Offset: 0x000D9DA1
		// (set) Token: 0x06002D56 RID: 11606 RVA: 0x000DADA9 File Offset: 0x000D9DA9
		public bool SetDateOnSend
		{
			get
			{
				return this.i;
			}
			set
			{
				this.i = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000DADB2 File Offset: 0x000D9DB2
		// (set) Token: 0x06002D58 RID: 11608 RVA: 0x000DADBA File Offset: 0x000D9DBA
		public bool SetMessageIDOnSend
		{
			get
			{
				return this.j;
			}
			set
			{
				this.j = value;
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000DADC3 File Offset: 0x000D9DC3
		internal MessageBuilderConfig(MailMessage A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000DADF2 File Offset: 0x000D9DF2
		public void Apply()
		{
			if (this.a != null)
			{
				this.a.GetMessageRawData();
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000DAE08 File Offset: 0x000D9E08
		internal MessageBuilderConfig a(MailMessage A_0)
		{
			return new MessageBuilderConfig(A_0)
			{
				d = this.d,
				e = this.e,
				f = this.f,
				g = this.g,
				h = this.h,
				i = this.i,
				j = this.j
			};
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000DAE6F File Offset: 0x000D9E6F
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x000DAE77 File Offset: 0x000D9E77
		public bool BuildHeaderOnly
		{
			get
			{
				return this.k;
			}
			set
			{
				this.k = value;
			}
		}

		// Token: 0x04001F68 RID: 8040
		private MailMessage a;

		// Token: 0x04001F69 RID: 8041
		private bool b;

		// Token: 0x04001F6A RID: 8042
		private ReplaceUriWithCidHandler c;

		// Token: 0x04001F6B RID: 8043
		private AddressDelimeterChar d;

		// Token: 0x04001F6C RID: 8044
		private HtmlToPlainAutoConvert e;

		// Token: 0x04001F6D RID: 8045
		private HtmlToPlainConvertOptions f;

		// Token: 0x04001F6E RID: 8046
		private string g = string.Empty;

		// Token: 0x04001F6F RID: 8047
		private bool h = true;

		// Token: 0x04001F70 RID: 8048
		private bool i = true;

		// Token: 0x04001F71 RID: 8049
		private bool j = true;

		// Token: 0x04001F72 RID: 8050
		private bool k;
	}
}
