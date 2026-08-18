using System;
using System.Collections;
using System.Text;
using a;
using a.f;

namespace MailBee.ImapMail
{
	// Token: 0x02000177 RID: 375
	public class FolderQuota
	{
		// Token: 0x06000CD8 RID: 3288 RVA: 0x00032FB3 File Offset: 0x00031FB3
		internal FolderQuota(string A_0, long A_1, long A_2, int A_3, int A_4)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = A_4;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00032FE0 File Offset: 0x00031FE0
		public string QuotaName
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00032FE8 File Offset: 0x00031FE8
		public long CurrentStorageSize
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00032FF0 File Offset: 0x00031FF0
		public long MaxStorageSize
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00032FF8 File Offset: 0x00031FF8
		public int CurrentMessageCount
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00033000 File Offset: 0x00032000
		public int MaxMessageCount
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00033008 File Offset: 0x00032008
		internal static FolderQuota b(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null || A_0.Count < 2)
			{
				return null;
			}
			Hashtable hashtable = global::a.f.s.b(A_0[1] as ArrayList, A_1);
			FolderQuota result;
			try
			{
				string a_ = ((ao)A_0[0]).a(A_1);
				ArrayList arrayList = hashtable["STORAGE"] as ArrayList;
				ArrayList arrayList2 = hashtable["MESSAGES"] as ArrayList;
				long a_2 = -1L;
				long a_3 = -1L;
				int a_4 = -1;
				int a_5 = -1;
				if (arrayList != null)
				{
					a_2 = long.Parse(((ao)arrayList[0]).a(Encoding.ASCII)) * 1024L;
					a_3 = long.Parse(((ao)arrayList[1]).a(Encoding.ASCII)) * 1024L;
				}
				if (arrayList2 != null)
				{
					a_4 = int.Parse(((ao)arrayList2[0]).a(Encoding.ASCII));
					a_5 = int.Parse(((ao)arrayList2[1]).a(Encoding.ASCII));
				}
				result = new FolderQuota(a_, a_2, a_3, a_4, a_5);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00033128 File Offset: 0x00032128
		internal static bool a(ArrayList A_0)
		{
			return A_0 != null && A_0.Count > 1;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00033138 File Offset: 0x00032138
		internal static string a(ArrayList A_0, Encoding A_1)
		{
			string result;
			try
			{
				result = ((ao)A_0[1]).a(A_1);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040008C8 RID: 2248
		private string a;

		// Token: 0x040008C9 RID: 2249
		private long b;

		// Token: 0x040008CA RID: 2250
		private long c;

		// Token: 0x040008CB RID: 2251
		private int d;

		// Token: 0x040008CC RID: 2252
		private int e;
	}
}
