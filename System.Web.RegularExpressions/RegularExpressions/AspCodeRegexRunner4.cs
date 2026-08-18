using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x0200000B RID: 11
	internal class AspCodeRegexRunner4 : RegexRunner
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00005300 File Offset: 0x00004300
		public override void Go()
		{
			string runtext = this.runtext;
			int runtextstart = this.runtextstart;
			int runtextbeg = this.runtextbeg;
			int runtextend = this.runtextend;
			int num = this.runtextpos;
			int[] runtrack = this.runtrack;
			int num2 = this.runtrackpos;
			int[] runstack = this.runstack;
			int num3 = this.runstackpos;
			runtrack[--num2] = num;
			runtrack[--num2] = 0;
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (num == this.runtextstart && 2 <= runtextend - num && runtext[num] == '<' && runtext[num + 1] == '%')
			{
				num += 2;
				runstack[--num3] = this.runtrack.Length - num2;
				runstack[--num3] = this.Crawlpos();
				runtrack[--num2] = 2;
				runtrack[--num2] = num;
				runtrack[--num2] = 3;
				if (num < runtextend && runtext[num++] == '@')
				{
					int num4 = runstack[num3++];
					num2 = this.runtrack.Length - runstack[num3++];
					int num5 = num4;
					if (num4 != this.Crawlpos())
					{
						do
						{
							this.Uncapture();
						}
						while ((num5 = num5) != this.Crawlpos());
					}
				}
			}
			int num6;
			do
			{
				this.runtrackpos = num2;
				this.runstackpos = num3;
				this.EnsureStorage();
				num2 = this.runtrackpos;
				num3 = this.runstackpos;
				runtrack = this.runtrack;
				runstack = this.runstack;
				switch (runtrack[num2++])
				{
				default:
					goto IL_2E1;
				case 1:
					num3++;
					continue;
				case 2:
					num3 += 2;
					continue;
				case 3:
					num = runtrack[num2++];
					num6 = runstack[num3++];
					num2 = this.runtrack.Length - runstack[num3++];
					runtrack[--num2] = num6;
					runtrack[--num2] = 4;
					runstack[--num3] = num;
					runtrack[--num2] = 1;
					if ((num6 = runtextend - num) > 0)
					{
						runtrack[--num2] = num6 - 1;
						runtrack[--num2] = num;
						runtrack[--num2] = 5;
					}
					break;
				case 4:
				{
					int num7;
					if ((num7 = runtrack[num2++]) != this.Crawlpos())
					{
						do
						{
							this.Uncapture();
						}
						while ((num7 = num7) != this.Crawlpos());
					}
					continue;
				}
				case 5:
				{
					num = runtrack[num2++];
					int num8 = runtrack[num2++];
					if (!RegexRunner.CharInClass(runtext[num++], "\0\u0001\0\0"))
					{
						continue;
					}
					if (num8 > 0)
					{
						runtrack[--num2] = num8 - 1;
						runtrack[--num2] = num;
						runtrack[--num2] = 5;
					}
					break;
				}
				case 6:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					continue;
				}
				num6 = runstack[num3++];
				this.Capture(1, num6, num);
				runtrack[--num2] = num6;
				runtrack[--num2] = 6;
			}
			while (2 > runtextend - num || runtext[num] != '%' || runtext[num + 1] != '>');
			num += 2;
			num6 = runstack[num3++];
			this.Capture(0, num6, num);
			runtrack[--num2] = num6;
			runtrack[num2 - 1] = 6;
			IL_277:
			this.runtextpos = num;
			return;
			IL_2E1:
			num = runtrack[num2++];
			goto IL_277;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000056DC File Offset: 0x000046DC
		public override bool FindFirstChar()
		{
			if (this.runtextpos > this.runtextstart)
			{
				this.runtextpos = this.runtextend;
				return false;
			}
			return true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00005708 File Offset: 0x00004708
		public override void InitTrackCount()
		{
			this.runtrackcount = 10;
		}
	}
}
