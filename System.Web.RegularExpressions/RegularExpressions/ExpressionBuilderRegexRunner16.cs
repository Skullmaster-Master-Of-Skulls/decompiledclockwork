using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x0200002F RID: 47
	internal class ExpressionBuilderRegexRunner16 : RegexRunner
	{
		// Token: 0x0600006A RID: 106 RVA: 0x0000A7E4 File Offset: 0x000097E4
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
				IL_49B:
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
					goto IL_510;
				case 1:
					num3++;
					break;
				case 2:
					goto IL_52D;
				case 3:
					goto IL_57D;
				case 4:
					goto IL_5CD;
				case 5:
					num3 += 2;
					break;
				case 6:
					goto IL_629;
				case 7:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					break;
				case 8:
					if ((num4 = runstack[num3++] - 1) >= 0)
					{
						goto Block_33;
					}
					runstack[num3] = runtrack[num2++];
					runstack[--num3] = num4;
					break;
				case 9:
					goto IL_25F;
				case 10:
					num4 = runtrack[num2++];
					runstack[--num3] = runtrack[num2++];
					runstack[--num3] = num4;
					break;
				case 11:
					goto IL_726;
				}
			}
			IL_510:
			num = runtrack[num2++];
			goto IL_492;
			IL_52D:
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
			IL_57D:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 3;
				goto IL_1A3;
			}
			goto IL_1A3;
			IL_5CD:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 4;
				goto IL_236;
			}
			goto IL_236;
			IL_629:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 6;
				goto IL_2EB;
			}
			goto IL_2EB;
			Block_33:
			num = runstack[num3++];
			runtrack[--num2] = num4;
			runtrack[--num2] = 10;
			goto IL_3AF;
			IL_726:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 11;
				goto IL_459;
			}
			goto IL_459;
			IL_F9:
			if (2 > runtextend - num || runtext[num] != '<' || runtext[num + 1] != '%')
			{
				goto IL_49B;
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
				runtrack[--num2] = 3;
			}
			IL_1A3:
			if (num >= runtextend || runtext[num++] != '$')
			{
				goto IL_49B;
			}
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
				runtrack[--num2] = 4;
			}
			IL_236:
			runstack[--num3] = -1;
			runstack[--num3] = 0;
			runtrack[--num2] = 5;
			goto IL_31B;
			IL_25F:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(runtext[num++], "\0\u0001\0\0"))
				{
					num--;
					break;
				}
			}
			if (num5 > num4)
			{
				runtrack[--num2] = num5 - num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 6;
			}
			IL_2EB:
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 7;
			IL_31B:
			num4 = runstack[num3++];
			int num6 = num5 = runstack[num3++];
			runtrack[--num2] = num5;
			if ((num6 != num || num4 < 0) && num4 < 1)
			{
				runstack[--num3] = num;
				runstack[--num3] = num4 + 1;
				runtrack[--num2] = 8;
				if (num2 <= 52 || num3 <= 39)
				{
					runtrack[--num2] = 9;
					goto IL_49B;
				}
				goto IL_25F;
			}
			else
			{
				runtrack[--num2] = num4;
				runtrack[--num2] = 10;
			}
			IL_3AF:
			if (2 > runtextend - num || runtext[num] != '%' || runtext[num + 1] != '>')
			{
				goto IL_49B;
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
				runtrack[--num2] = 11;
			}
			IL_459:
			if (num < runtextend)
			{
				goto IL_49B;
			}
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 7;
			IL_492:
			this.runtextpos = num;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000AF68 File Offset: 0x00009F68
		public override bool FindFirstChar()
		{
			if (this.runtextpos > this.runtextstart)
			{
				this.runtextpos = this.runtextend;
				return false;
			}
			return true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000AF94 File Offset: 0x00009F94
		public override void InitTrackCount()
		{
			this.runtrackcount = 13;
		}
	}
}
