using System;

namespace MailBee.ImapMail
{
	// Token: 0x0200019A RID: 410
	public class UidPlusResult
	{
		// Token: 0x06000EB2 RID: 3762 RVA: 0x000367CB File Offset: 0x000357CB
		public UidPlusResult()
		{
			this.a(false);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000367DA File Offset: 0x000357DA
		internal void a(bool A_0)
		{
			this.a = A_0;
			this.b = false;
			this.c = null;
			this.d = null;
			this.e = null;
			this.f = null;
			this.g = -1L;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003680E File Offset: 0x0003580E
		internal void a(bool A_0, string A_1, string A_2, long A_3)
		{
			this.a = A_0;
			this.b = true;
			this.c = A_1;
			this.d = A_2;
			this.e = null;
			this.f = null;
			this.g = A_3;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00036842 File Offset: 0x00035842
		public bool IsSupported
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0003684A File Offset: 0x0003584A
		public bool IsValid
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00036852 File Offset: 0x00035852
		public string SrcUidString
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0003685A File Offset: 0x0003585A
		public string DestUidString
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00036862 File Offset: 0x00035862
		public long DestUidValidity
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0003686A File Offset: 0x0003586A
		public UidCollection SrcUids
		{
			get
			{
				if (this.e == null && this.c != null)
				{
					this.e = UidCollection.Parse(this.c);
				}
				return this.e;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00036893 File Offset: 0x00035893
		public UidCollection DestUids
		{
			get
			{
				if (this.f == null && this.d != null)
				{
					this.f = UidCollection.Parse(this.d);
				}
				return this.f;
			}
		}

		// Token: 0x0400094F RID: 2383
		private bool a;

		// Token: 0x04000950 RID: 2384
		private bool b;

		// Token: 0x04000951 RID: 2385
		private string c;

		// Token: 0x04000952 RID: 2386
		private string d;

		// Token: 0x04000953 RID: 2387
		private UidCollection e;

		// Token: 0x04000954 RID: 2388
		private UidCollection f;

		// Token: 0x04000955 RID: 2389
		private long g;
	}
}
