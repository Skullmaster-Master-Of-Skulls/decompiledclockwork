using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using a.i;

namespace MailBee.Mime
{
	// Token: 0x0200054D RID: 1357
	public class MailMerge
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x000D284E File Offset: 0x000D184E
		public MailMessage MergedMessage
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000D2856 File Offset: 0x000D1856
		internal MailMerge(MailMessage A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000D2870 File Offset: 0x000D1870
		public void AddAttachmentPattern(string filenamePattern)
		{
			this.a.Add(filenamePattern);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000D287F File Offset: 0x000D187F
		public void ClearAttachmentPatterns()
		{
			this.a.Clear();
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000D288C File Offset: 0x000D188C
		public void Reset()
		{
			this.c = null;
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000D2895 File Offset: 0x000D1895
		public int Replace(string pattern, string actualValue)
		{
			return this.Replace(pattern, actualValue, MailMergeTargets.All);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000D28A1 File Offset: 0x000D18A1
		public int Replace(string pattern, string actualValue, MailMergeTargets targets)
		{
			if (this.c == null)
			{
				this.c = this.b.w();
			}
			this.a(pattern, actualValue, this.c);
			return k.a(pattern, actualValue, targets, this.c);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000D28D8 File Offset: 0x000D18D8
		public Task<int> ReplaceAsync(string pattern, string actualValue)
		{
			return this.ReplaceAsync(pattern, actualValue, MailMergeTargets.All);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000D28E4 File Offset: 0x000D18E4
		public Task<int> ReplaceAsync(string pattern, string actualValue, MailMergeTargets targets)
		{
			MailMerge.b b;
			b.c = this;
			b.d = pattern;
			b.e = actualValue;
			b.f = targets;
			b.b = AsyncTaskMethodBuilder<int>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<int> asyncTaskMethodBuilder = b.b;
			asyncTaskMethodBuilder.Start<MailMerge.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000D2944 File Offset: 0x000D1944
		private Task b(string A_0, string A_1, MailMessage A_2)
		{
			MailMerge.a a;
			a.d = this;
			a.e = A_0;
			a.c = A_1;
			a.f = A_2;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<MailMerge.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000D29A4 File Offset: 0x000D19A4
		private void a(string A_0, string A_1, MailMessage A_2)
		{
			if (A_1 == null || A_1 == string.Empty)
			{
				return;
			}
			foreach (string text in this.a)
			{
				if (text != null && text.Length > 0 && text.IndexOf(A_0) >= 0)
				{
					foreach (string filename in text.Replace(A_0, A_1).Split(new char[]
					{
						';'
					}))
					{
						A_2.Attachments.Add(filename);
					}
				}
			}
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000D2A58 File Offset: 0x000D1A58
		internal MailMerge a(MailMessage A_0)
		{
			MailMerge mailMerge = new MailMerge(A_0);
			foreach (string value in this.a)
			{
				mailMerge.a.Add(value);
			}
			return mailMerge;
		}

		// Token: 0x04001EC8 RID: 7880
		private StringCollection a = new StringCollection();

		// Token: 0x04001EC9 RID: 7881
		private MailMessage b;

		// Token: 0x04001ECA RID: 7882
		private MailMessage c;
	}
}
