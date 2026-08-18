using System;
using System.Collections;
using System.Text;
using a;
using a.f;

namespace MailBee.ImapMail
{
	// Token: 0x02000178 RID: 376
	public class FolderStatus
	{
		// Token: 0x06000CE1 RID: 3297 RVA: 0x00033170 File Offset: 0x00032170
		private FolderStatus()
		{
			this.a = null;
			this.b = null;
			this.c = -1;
			this.d = -1;
			this.e = -1;
			this.f = -1L;
			this.g = -1L;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000331AB File Offset: 0x000321AB
		private FolderStatus(string A_0, string A_1, int A_2, int A_3, int A_4, long A_5, long A_6)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = A_4;
			this.f = A_5;
			this.g = A_6;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x000331E8 File Offset: 0x000321E8
		public string FolderName
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x000331F0 File Offset: 0x000321F0
		public string RawFolderName
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x000331F8 File Offset: 0x000321F8
		public int MessageCount
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00033200 File Offset: 0x00032200
		public int RecentCount
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00033208 File Offset: 0x00032208
		public int UnseenCount
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00033210 File Offset: 0x00032210
		public long UidNext
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00033218 File Offset: 0x00032218
		public long UidValidity
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00033220 File Offset: 0x00032220
		public bool IsValid
		{
			get
			{
				return this.a != null && this.c > -1 && this.d > -1 && this.e > -1 && this.f > -1L && this.g > -1L;
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0003325C File Offset: 0x0003225C
		internal static FolderStatus a(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null || A_0.Count < 2)
			{
				return null;
			}
			string text = null;
			try
			{
				text = ((ao)A_0[0]).a(A_1);
			}
			catch
			{
				return null;
			}
			Hashtable hashtable = global::a.f.s.c(A_0[1] as ArrayList, A_1);
			int a_ = hashtable.ContainsKey("MESSAGES") ? ((int)hashtable["MESSAGES"]) : -1;
			int a_2 = hashtable.ContainsKey("RECENT") ? ((int)hashtable["RECENT"]) : -1;
			int a_3 = hashtable.ContainsKey("UNSEEN") ? ((int)hashtable["UNSEEN"]) : -1;
			long a_4 = hashtable.ContainsKey("UIDNEXT") ? ((long)hashtable["UIDNEXT"]) : -1L;
			long a_5 = hashtable.ContainsKey("UIDVALIDITY") ? ((long)hashtable["UIDVALIDITY"]) : -1L;
			return new FolderStatus(global::a.f.f.a(text), text, a_, a_2, a_3, a_4, a_5);
		}

		// Token: 0x040008CD RID: 2253
		private string a;

		// Token: 0x040008CE RID: 2254
		private string b;

		// Token: 0x040008CF RID: 2255
		private int c;

		// Token: 0x040008D0 RID: 2256
		private int d;

		// Token: 0x040008D1 RID: 2257
		private int e;

		// Token: 0x040008D2 RID: 2258
		private long f;

		// Token: 0x040008D3 RID: 2259
		private long g;
	}
}
