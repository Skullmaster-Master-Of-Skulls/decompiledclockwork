using System;
using System.ComponentModel;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A5 RID: 1701
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class RegexRunner
	{
		// Token: 0x06003FA6 RID: 16294 RVA: 0x0010B9D8 File Offset: 0x00109BD8
		protected internal RegexRunner()
		{
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x0010B9E0 File Offset: 0x00109BE0
		protected internal Match Scan(Regex regex, string text, int textbeg, int textend, int textstart, int prevlen, bool quick)
		{
			return this.Scan(regex, text, textbeg, textend, textstart, prevlen, quick, regex.MatchTimeout);
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x0010BA04 File Offset: 0x00109C04
		protected internal Match Scan(Regex regex, string text, int textbeg, int textend, int textstart, int prevlen, bool quick, TimeSpan timeout)
		{
			bool flag = false;
			Regex.ValidateMatchTimeout(timeout);
			this.ignoreTimeout = (Regex.InfiniteMatchTimeout == timeout);
			this.timeout = (this.ignoreTimeout ? ((int)Regex.InfiniteMatchTimeout.TotalMilliseconds) : ((int)(timeout.TotalMilliseconds + 0.5)));
			this.runregex = regex;
			this.runtext = text;
			this.runtextbeg = textbeg;
			this.runtextend = textend;
			this.runtextstart = textstart;
			int num = this.runregex.RightToLeft ? -1 : 1;
			int num2 = this.runregex.RightToLeft ? this.runtextbeg : this.runtextend;
			this.runtextpos = textstart;
			if (prevlen == 0)
			{
				if (this.runtextpos == num2)
				{
					return Match.Empty;
				}
				this.runtextpos += num;
			}
			this.StartTimeoutWatch();
			for (;;)
			{
				if (this.FindFirstChar())
				{
					this.CheckTimeout();
					if (!flag)
					{
						this.InitMatch();
						flag = true;
					}
					this.Go();
					if (this.runmatch._matchcount[0] > 0)
					{
						break;
					}
					this.runtrackpos = this.runtrack.Length;
					this.runstackpos = this.runstack.Length;
					this.runcrawlpos = this.runcrawl.Length;
				}
				if (this.runtextpos == num2)
				{
					goto Block_9;
				}
				this.runtextpos += num;
			}
			return this.TidyMatch(quick);
			Block_9:
			this.TidyMatch(true);
			return Match.Empty;
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x0010BB69 File Offset: 0x00109D69
		private void StartTimeoutWatch()
		{
			if (this.ignoreTimeout)
			{
				return;
			}
			this.timeoutChecksToSkip = 1000;
			this.timeoutOccursAt = Environment.TickCount + this.timeout;
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x0010BB94 File Offset: 0x00109D94
		protected void CheckTimeout()
		{
			if (this.ignoreTimeout)
			{
				return;
			}
			int num = this.timeoutChecksToSkip - 1;
			this.timeoutChecksToSkip = num;
			if (num != 0)
			{
				return;
			}
			this.timeoutChecksToSkip = 1000;
			this.DoCheckTimeout();
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x0010BBD0 File Offset: 0x00109DD0
		private void DoCheckTimeout()
		{
			int tickCount = Environment.TickCount;
			if (tickCount < this.timeoutOccursAt)
			{
				return;
			}
			if (0 > this.timeoutOccursAt && 0 < tickCount)
			{
				return;
			}
			throw new RegexMatchTimeoutException(this.runtext, this.runregex.pattern, TimeSpan.FromMilliseconds((double)this.timeout));
		}

		// Token: 0x06003FAC RID: 16300
		protected abstract void Go();

		// Token: 0x06003FAD RID: 16301
		protected abstract bool FindFirstChar();

		// Token: 0x06003FAE RID: 16302
		protected abstract void InitTrackCount();

		// Token: 0x06003FAF RID: 16303 RVA: 0x0010BC20 File Offset: 0x00109E20
		private void InitMatch()
		{
			if (this.runmatch == null)
			{
				if (this.runregex.caps != null)
				{
					this.runmatch = new MatchSparse(this.runregex, this.runregex.caps, this.runregex.capsize, this.runtext, this.runtextbeg, this.runtextend - this.runtextbeg, this.runtextstart);
				}
				else
				{
					this.runmatch = new Match(this.runregex, this.runregex.capsize, this.runtext, this.runtextbeg, this.runtextend - this.runtextbeg, this.runtextstart);
				}
			}
			else
			{
				this.runmatch.Reset(this.runregex, this.runtext, this.runtextbeg, this.runtextend, this.runtextstart);
			}
			if (this.runcrawl != null)
			{
				this.runtrackpos = this.runtrack.Length;
				this.runstackpos = this.runstack.Length;
				this.runcrawlpos = this.runcrawl.Length;
				return;
			}
			this.InitTrackCount();
			int num = this.runtrackcount * 8;
			int num2 = this.runtrackcount * 8;
			if (num < 32)
			{
				num = 32;
			}
			if (num2 < 16)
			{
				num2 = 16;
			}
			this.runtrack = new int[num];
			this.runtrackpos = num;
			this.runstack = new int[num2];
			this.runstackpos = num2;
			this.runcrawl = new int[32];
			this.runcrawlpos = 32;
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x0010BD8C File Offset: 0x00109F8C
		private Match TidyMatch(bool quick)
		{
			if (!quick)
			{
				Match match = this.runmatch;
				this.runmatch = null;
				match.Tidy(this.runtextpos);
				return match;
			}
			return null;
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x0010BDB9 File Offset: 0x00109FB9
		protected void EnsureStorage()
		{
			if (this.runstackpos < this.runtrackcount * 4)
			{
				this.DoubleStack();
			}
			if (this.runtrackpos < this.runtrackcount * 4)
			{
				this.DoubleTrack();
			}
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x0010BDE7 File Offset: 0x00109FE7
		protected bool IsBoundary(int index, int startpos, int endpos)
		{
			return (index > startpos && RegexCharClass.IsWordChar(this.runtext[index - 1])) != (index < endpos && RegexCharClass.IsWordChar(this.runtext[index]));
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x0010BE20 File Offset: 0x0010A020
		protected bool IsECMABoundary(int index, int startpos, int endpos)
		{
			return (index > startpos && RegexCharClass.IsECMAWordChar(this.runtext[index - 1])) != (index < endpos && RegexCharClass.IsECMAWordChar(this.runtext[index]));
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x0010BE5C File Offset: 0x0010A05C
		protected static bool CharInSet(char ch, string set, string category)
		{
			string set2 = RegexCharClass.ConvertOldStringsToClass(set, category);
			return RegexCharClass.CharInClass(ch, set2);
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x0010BE78 File Offset: 0x0010A078
		protected static bool CharInClass(char ch, string charClass)
		{
			return RegexCharClass.CharInClass(ch, charClass);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x0010BE84 File Offset: 0x0010A084
		protected void DoubleTrack()
		{
			int[] destinationArray = new int[this.runtrack.Length * 2];
			Array.Copy(this.runtrack, 0, destinationArray, this.runtrack.Length, this.runtrack.Length);
			this.runtrackpos += this.runtrack.Length;
			this.runtrack = destinationArray;
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x0010BEDC File Offset: 0x0010A0DC
		protected void DoubleStack()
		{
			int[] destinationArray = new int[this.runstack.Length * 2];
			Array.Copy(this.runstack, 0, destinationArray, this.runstack.Length, this.runstack.Length);
			this.runstackpos += this.runstack.Length;
			this.runstack = destinationArray;
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0010BF34 File Offset: 0x0010A134
		protected void DoubleCrawl()
		{
			int[] destinationArray = new int[this.runcrawl.Length * 2];
			Array.Copy(this.runcrawl, 0, destinationArray, this.runcrawl.Length, this.runcrawl.Length);
			this.runcrawlpos += this.runcrawl.Length;
			this.runcrawl = destinationArray;
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x0010BF8C File Offset: 0x0010A18C
		protected void Crawl(int i)
		{
			if (this.runcrawlpos == 0)
			{
				this.DoubleCrawl();
			}
			int[] array = this.runcrawl;
			int num = this.runcrawlpos - 1;
			this.runcrawlpos = num;
			array[num] = i;
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x0010BFC0 File Offset: 0x0010A1C0
		protected int Popcrawl()
		{
			int[] array = this.runcrawl;
			int num = this.runcrawlpos;
			this.runcrawlpos = num + 1;
			return array[num];
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x0010BFE5 File Offset: 0x0010A1E5
		protected int Crawlpos()
		{
			return this.runcrawl.Length - this.runcrawlpos;
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x0010BFF8 File Offset: 0x0010A1F8
		protected void Capture(int capnum, int start, int end)
		{
			if (end < start)
			{
				int num = end;
				end = start;
				start = num;
			}
			this.Crawl(capnum);
			this.runmatch.AddMatch(capnum, start, end - start);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x0010C028 File Offset: 0x0010A228
		protected void TransferCapture(int capnum, int uncapnum, int start, int end)
		{
			if (end < start)
			{
				int num = end;
				end = start;
				start = num;
			}
			int num2 = this.MatchIndex(uncapnum);
			int num3 = num2 + this.MatchLength(uncapnum);
			if (start >= num3)
			{
				end = start;
				start = num3;
			}
			else if (end <= num2)
			{
				start = num2;
			}
			else
			{
				if (end > num3)
				{
					end = num3;
				}
				if (num2 > start)
				{
					start = num2;
				}
			}
			this.Crawl(uncapnum);
			this.runmatch.BalanceMatch(uncapnum);
			if (capnum != -1)
			{
				this.Crawl(capnum);
				this.runmatch.AddMatch(capnum, start, end - start);
			}
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x0010C0AC File Offset: 0x0010A2AC
		protected void Uncapture()
		{
			int cap = this.Popcrawl();
			this.runmatch.RemoveMatch(cap);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x0010C0CC File Offset: 0x0010A2CC
		protected bool IsMatched(int cap)
		{
			return this.runmatch.IsMatched(cap);
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x0010C0DA File Offset: 0x0010A2DA
		protected int MatchIndex(int cap)
		{
			return this.runmatch.MatchIndex(cap);
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x0010C0E8 File Offset: 0x0010A2E8
		protected int MatchLength(int cap)
		{
			return this.runmatch.MatchLength(cap);
		}

		// Token: 0x04002E68 RID: 11880
		protected internal int runtextbeg;

		// Token: 0x04002E69 RID: 11881
		protected internal int runtextend;

		// Token: 0x04002E6A RID: 11882
		protected internal int runtextstart;

		// Token: 0x04002E6B RID: 11883
		protected internal string runtext;

		// Token: 0x04002E6C RID: 11884
		protected internal int runtextpos;

		// Token: 0x04002E6D RID: 11885
		protected internal int[] runtrack;

		// Token: 0x04002E6E RID: 11886
		protected internal int runtrackpos;

		// Token: 0x04002E6F RID: 11887
		protected internal int[] runstack;

		// Token: 0x04002E70 RID: 11888
		protected internal int runstackpos;

		// Token: 0x04002E71 RID: 11889
		protected internal int[] runcrawl;

		// Token: 0x04002E72 RID: 11890
		protected internal int runcrawlpos;

		// Token: 0x04002E73 RID: 11891
		protected internal int runtrackcount;

		// Token: 0x04002E74 RID: 11892
		protected internal Match runmatch;

		// Token: 0x04002E75 RID: 11893
		protected internal Regex runregex;

		// Token: 0x04002E76 RID: 11894
		private int timeout;

		// Token: 0x04002E77 RID: 11895
		private bool ignoreTimeout;

		// Token: 0x04002E78 RID: 11896
		private int timeoutOccursAt;

		// Token: 0x04002E79 RID: 11897
		private const int TimeoutCheckFrequency = 1000;

		// Token: 0x04002E7A RID: 11898
		private int timeoutChecksToSkip;
	}
}
