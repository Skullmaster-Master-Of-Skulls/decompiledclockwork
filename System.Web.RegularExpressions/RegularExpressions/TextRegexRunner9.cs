using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x0200001A RID: 26
	internal class TextRegexRunner9 : RegexRunner
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00007470 File Offset: 0x00006470
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
			int num4;
			if (num == this.runtextstart && 1 <= runtextend - num)
			{
				num++;
				num4 = 1;
				while (runtext[num - num4--] != '<')
				{
					if (num4 <= 0)
					{
						int num5;
						num4 = (num5 = runtextend - num) + 1;
						while (--num4 > 0)
						{
							if (runtext[num++] == '<')
							{
								num--;
								break;
							}
						}
						if (num5 > num4)
						{
							runtrack[--num2] = num5 - num4 - 1;
							runtrack[--num2] = num - 1;
							runtrack[--num2] = 2;
							goto IL_121;
						}
						goto IL_121;
					}
				}
			}
			for (;;)
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
					goto IL_1AF;
				case 1:
					num3++;
					break;
				case 2:
					goto IL_1CC;
				case 3:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					break;
				}
			}
			IL_1AF:
			num = runtrack[num2++];
			goto IL_151;
			IL_1CC:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 2;
			}
			IL_121:
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 3;
			IL_151:
			this.runtextpos = num;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000076B8 File Offset: 0x000066B8
		public override bool FindFirstChar()
		{
			if (this.runtextpos > this.runtextstart)
			{
				this.runtextpos = this.runtextend;
				return false;
			}
			return true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000076E4 File Offset: 0x000066E4
		public override void InitTrackCount()
		{
			this.runtrackcount = 4;
		}
	}
}
