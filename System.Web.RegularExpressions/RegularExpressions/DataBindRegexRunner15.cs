using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x0200002C RID: 44
	internal class DataBindRegexRunner15 : RegexRunner
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000A050 File Offset: 0x00009050
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
			int num5;
			int num4;
			if (num == this.runtextstart)
			{
				num4 = (num5 = runtextend - num) + 1;
				while (--num4 > 0)
				{
					if (!RegexRunner.CharInClass(runtext[num++], "\0\0\u0001d"))
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
					goto IL_F9;
				}
				goto IL_F9;
			}
			for (;;)
			{
				IL_3A5:
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
					goto IL_416;
				case 1:
					num3++;
					break;
				case 2:
					goto IL_433;
				case 3:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (RegexRunner.CharInClass(runtext[num++], "\0\0\u0001d"))
					{
						goto Block_23;
					}
					break;
				case 4:
					num3 += 2;
					break;
				case 5:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (RegexRunner.CharInClass(runtext[num++], "\0\u0001\0\0"))
					{
						goto Block_25;
					}
					break;
				case 6:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					break;
				case 7:
					if ((num4 = runstack[num3++] - 1) >= 0)
					{
						goto Block_27;
					}
					runstack[num3] = runtrack[num2++];
					runstack[--num3] = num4;
					break;
				case 8:
					goto IL_1AA;
				case 9:
					num4 = runtrack[num2++];
					runstack[--num3] = runtrack[num2++];
					runstack[--num3] = num4;
					break;
				case 10:
					goto IL_612;
				}
			}
			IL_416:
			num = runtrack[num2++];
			goto IL_39C;
			IL_433:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 2;
				goto IL_F9;
			}
			goto IL_F9;
			Block_23:
			if (num5 > 0)
			{
				runtrack[--num2] = num5 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 3;
				goto IL_162;
			}
			goto IL_162;
			Block_25:
			if (num5 > 0)
			{
				runtrack[--num2] = num5 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 5;
				goto IL_1F5;
			}
			goto IL_1F5;
			Block_27:
			num = runstack[num3++];
			runtrack[--num2] = num4;
			runtrack[--num2] = 9;
			goto IL_2B9;
			IL_612:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 10;
				goto IL_363;
			}
			goto IL_363;
			IL_F9:
			if (2 > runtextend - num || runtext[num] != '<' || runtext[num + 1] != '%')
			{
				goto IL_3A5;
			}
			num += 2;
			if ((num4 = runtextend - num) > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 3;
			}
			IL_162:
			if (num < runtextend && runtext[num++] == '#')
			{
				runstack[--num3] = -1;
				runstack[--num3] = 0;
				runtrack[--num2] = 4;
				goto IL_225;
			}
			goto IL_3A5;
			IL_1AA:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if ((num4 = runtextend - num) > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 5;
			}
			IL_1F5:
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 6;
			IL_225:
			num4 = runstack[num3++];
			int num6 = num5 = runstack[num3++];
			runtrack[--num2] = num5;
			if ((num6 != num || num4 < 0) && num4 < 1)
			{
				runstack[--num3] = num;
				runstack[--num3] = num4 + 1;
				runtrack[--num2] = 7;
				if (num2 <= 48 || num3 <= 36)
				{
					runtrack[--num2] = 8;
					goto IL_3A5;
				}
				goto IL_1AA;
			}
			else
			{
				runtrack[--num2] = num4;
				runtrack[--num2] = 9;
			}
			IL_2B9:
			if (2 > runtextend - num || runtext[num] != '%' || runtext[num + 1] != '>')
			{
				goto IL_3A5;
			}
			num += 2;
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(runtext[num++], "\0\0\u0001d"))
				{
					num--;
					break;
				}
			}
			if (num5 > num4)
			{
				runtrack[--num2] = num5 - num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 10;
			}
			IL_363:
			if (num < runtextend)
			{
				goto IL_3A5;
			}
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 6;
			IL_39C:
			this.runtextpos = num;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000A6C0 File Offset: 0x000096C0
		public override bool FindFirstChar()
		{
			if (this.runtextpos > this.runtextstart)
			{
				this.runtextpos = this.runtextend;
				return false;
			}
			return true;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000A6EC File Offset: 0x000096EC
		public override void InitTrackCount()
		{
			this.runtrackcount = 12;
		}
	}
}
