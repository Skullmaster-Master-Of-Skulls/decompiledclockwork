using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x02000038 RID: 56
	internal class FormatStringRegexRunner19 : RegexRunner
	{
		// Token: 0x0600007F RID: 127 RVA: 0x0000CED4 File Offset: 0x0000BED4
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
			if (num <= runtextbeg || runtext[num - 1] == '\n')
			{
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				runstack[--num3] = -1;
				runtrack[--num2] = 1;
				goto IL_2AF;
			}
			int num4;
			for (;;)
			{
				IL_39B:
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
					goto IL_40C;
				case 1:
					num3++;
					break;
				case 2:
					goto IL_429;
				case 3:
					num3 += 2;
					break;
				case 4:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					break;
				case 5:
					if ((num4 = runstack[num3++] - 1) >= 0)
					{
						goto Block_15;
					}
					runstack[num3] = runtrack[num2++];
					runstack[--num3] = num4;
					break;
				case 6:
					goto IL_16D;
				case 7:
					num4 = runtrack[num2++];
					runstack[--num3] = runtrack[num2++];
					runstack[--num3] = num4;
					break;
				case 8:
					goto IL_532;
				case 9:
					goto IL_C6;
				case 10:
					runstack[--num3] = runtrack[num2++];
					break;
				}
			}
			IL_40C:
			num = runtrack[num2++];
			goto IL_392;
			IL_429:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 2;
				goto IL_144;
			}
			goto IL_144;
			Block_15:
			num = runstack[num3++];
			runtrack[--num2] = num4;
			runtrack[--num2] = 7;
			goto IL_27F;
			IL_532:
			num = runtrack[num2++];
			int num5 = runstack[num3++];
			runtrack[--num2] = 10;
			goto IL_31C;
			IL_C6:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			int num6;
			num4 = (num6 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (runtext[num++] == '"')
				{
					num--;
					break;
				}
			}
			if (num6 > num4)
			{
				runtrack[--num2] = num6 - num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 2;
			}
			IL_144:
			runstack[--num3] = -1;
			runstack[--num3] = 0;
			runtrack[--num2] = 3;
			goto IL_1EB;
			IL_16D:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (2 > runtextend - num || runtext[num] != '"' || runtext[num + 1] != '"')
			{
				goto IL_39B;
			}
			num += 2;
			num4 = runstack[num3++];
			this.Capture(3, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 4;
			IL_1EB:
			num4 = runstack[num3++];
			int num7 = num6 = runstack[num3++];
			runtrack[--num2] = num6;
			if ((num7 != num || num4 < 0) && num4 < 1)
			{
				runstack[--num3] = num;
				runstack[--num3] = num4 + 1;
				runtrack[--num2] = 5;
				if (num2 <= 60 || num3 <= 45)
				{
					runtrack[--num2] = 6;
					goto IL_39B;
				}
				goto IL_16D;
			}
			else
			{
				runtrack[--num2] = num4;
				runtrack[--num2] = 7;
			}
			IL_27F:
			num4 = runstack[num3++];
			this.Capture(2, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 4;
			IL_2AF:
			int num8 = num4 = runstack[num3++];
			runtrack[--num2] = num4;
			if (num8 != num)
			{
				runtrack[--num2] = num;
				runstack[--num3] = num;
				runtrack[--num2] = 8;
				if (num2 <= 60 || num3 <= 45)
				{
					runtrack[--num2] = 9;
					goto IL_39B;
				}
				goto IL_C6;
			}
			else
			{
				runtrack[--num2] = 10;
			}
			IL_31C:
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 4;
			if (num < runtextend && runtext[num] != '\n')
			{
				goto IL_39B;
			}
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 4;
			IL_392:
			this.runtextpos = num;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000D454 File Offset: 0x0000C454
		public override bool FindFirstChar()
		{
			return true;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000D464 File Offset: 0x0000C464
		public override void InitTrackCount()
		{
			this.runtrackcount = 15;
		}
	}
}
