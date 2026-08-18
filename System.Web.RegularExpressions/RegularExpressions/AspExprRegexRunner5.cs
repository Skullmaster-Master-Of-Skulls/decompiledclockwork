using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x0200000E RID: 14
	internal class AspExprRegexRunner5 : RegexRunner
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00005800 File Offset: 0x00004800
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
			if (num == this.runtextstart && 2 <= runtextend - num && runtext[num] == '<' && runtext[num + 1] == '%')
			{
				num += 2;
				if ((num4 = runtextend - num) > 0)
				{
					runtrack[--num2] = num4 - 1;
					runtrack[--num2] = num;
					runtrack[--num2] = 2;
					goto IL_EE;
				}
				goto IL_EE;
			}
			int num5;
			for (;;)
			{
				IL_2B4:
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
					goto IL_31D;
				case 1:
					num3++;
					break;
				case 2:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (RegexRunner.CharInClass(runtext[num++], "\0\0\u0001d"))
					{
						goto Block_15;
					}
					break;
				case 3:
					num3 += 2;
					break;
				case 4:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (RegexRunner.CharInClass(runtext[num++], "\0\u0001\0\0"))
					{
						goto Block_17;
					}
					break;
				case 5:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					break;
				case 6:
					if ((num4 = runstack[num3++] - 1) >= 0)
					{
						goto Block_19;
					}
					runstack[num3] = runtrack[num2++];
					runstack[--num3] = num4;
					break;
				case 7:
					goto IL_136;
				case 8:
					num4 = runtrack[num2++];
					runstack[--num3] = runtrack[num2++];
					runstack[--num3] = num4;
					break;
				}
			}
			IL_31D:
			num = runtrack[num2++];
			goto IL_2AB;
			Block_15:
			if (num5 > 0)
			{
				runtrack[--num2] = num5 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 2;
				goto IL_EE;
			}
			goto IL_EE;
			Block_17:
			if (num5 > 0)
			{
				runtrack[--num2] = num5 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 4;
				goto IL_181;
			}
			goto IL_181;
			Block_19:
			num = runstack[num3++];
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			goto IL_245;
			IL_EE:
			if (num < runtextend && runtext[num++] == '=')
			{
				runstack[--num3] = -1;
				runstack[--num3] = 0;
				runtrack[--num2] = 3;
				goto IL_1B1;
			}
			goto IL_2B4;
			IL_136:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if ((num4 = runtextend - num) > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 4;
			}
			IL_181:
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 5;
			IL_1B1:
			num4 = runstack[num3++];
			int num6 = num5 = runstack[num3++];
			runtrack[--num2] = num5;
			if ((num6 != num || num4 < 0) && num4 < 1)
			{
				runstack[--num3] = num;
				runstack[--num3] = num4 + 1;
				runtrack[--num2] = 6;
				if (num2 <= 40 || num3 <= 30)
				{
					runtrack[--num2] = 7;
					goto IL_2B4;
				}
				goto IL_136;
			}
			else
			{
				runtrack[--num2] = num4;
				runtrack[--num2] = 8;
			}
			IL_245:
			if (2 > runtextend - num || runtext[num] != '%' || runtext[num + 1] != '>')
			{
				goto IL_2B4;
			}
			num += 2;
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 5;
			IL_2AB:
			this.runtextpos = num;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00005CD8 File Offset: 0x00004CD8
		public override bool FindFirstChar()
		{
			if (this.runtextpos > this.runtextstart)
			{
				this.runtextpos = this.runtextend;
				return false;
			}
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00005D04 File Offset: 0x00004D04
		public override void InitTrackCount()
		{
			this.runtrackcount = 10;
		}
	}
}
