using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x0200003B RID: 59
	internal class WebResourceRegexRunner20 : RegexRunner
	{
		// Token: 0x06000086 RID: 134 RVA: 0x0000D4F8 File Offset: 0x0000C4F8
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
			if (2 <= runtextend - num && runtext[num] == '<' && runtext[num + 1] == '%')
			{
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
					runtrack[--num2] = 2;
					goto IL_122;
				}
				goto IL_122;
			}
			for (;;)
			{
				IL_483:
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
					goto IL_4E4;
				case 1:
					num3++;
					break;
				case 2:
					goto IL_501;
				case 3:
					goto IL_551;
				case 4:
					goto IL_5A1;
				case 5:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					break;
				case 6:
					goto IL_610;
				}
			}
			IL_4E4:
			num = runtrack[num2++];
			goto IL_47A;
			IL_501:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 2;
				goto IL_122;
			}
			goto IL_122;
			IL_551:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 3;
				goto IL_1B5;
			}
			goto IL_1B5;
			IL_5A1:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 4;
				goto IL_33A;
			}
			goto IL_33A;
			IL_610:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 6;
				goto IL_414;
			}
			goto IL_414;
			IL_122:
			if (num >= runtextend || runtext[num++] != '=')
			{
				goto IL_483;
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
				runtrack[--num2] = 3;
			}
			IL_1B5:
			if (13 > runtextend - num || runtext[num] != 'W' || runtext[num + 1] != 'e' || runtext[num + 2] != 'b' || runtext[num + 3] != 'R' || runtext[num + 4] != 'e' || runtext[num + 5] != 's' || runtext[num + 6] != 'o' || runtext[num + 7] != 'u' || runtext[num + 8] != 'r' || runtext[num + 9] != 'c' || runtext[num + 10] != 'e' || runtext[num + 11] != '(' || runtext[num + 12] != '"')
			{
				goto IL_483;
			}
			num += 13;
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (runtext[num++] == '"')
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
			IL_33A:
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 5;
			if (2 > runtextend - num || runtext[num] != '"' || runtext[num + 1] != ')')
			{
				goto IL_483;
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
				runtrack[--num2] = 6;
			}
			IL_414:
			if (2 > runtextend - num || runtext[num] != '%' || runtext[num + 1] != '>')
			{
				goto IL_483;
			}
			num += 2;
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 5;
			IL_47A:
			this.runtextpos = num;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000DB64 File Offset: 0x0000CB64
		public override bool FindFirstChar()
		{
			string runtext = this.runtext;
			int runtextend = this.runtextend;
			int num2;
			for (int i = this.runtextpos + 1; i < runtextend; i = num2 + i)
			{
				int num;
				if ((num = (int)runtext[i]) != 37)
				{
					if ((num -= 37) <= 23)
					{
						switch (num)
						{
						default:
							num2 = 0;
							break;
						case 1:
							num2 = 2;
							break;
						case 2:
							num2 = 2;
							break;
						case 3:
							num2 = 2;
							break;
						case 4:
							num2 = 2;
							break;
						case 5:
							num2 = 2;
							break;
						case 6:
							num2 = 2;
							break;
						case 7:
							num2 = 2;
							break;
						case 8:
							num2 = 2;
							break;
						case 9:
							num2 = 2;
							break;
						case 10:
							num2 = 2;
							break;
						case 11:
							num2 = 2;
							break;
						case 12:
							num2 = 2;
							break;
						case 13:
							num2 = 2;
							break;
						case 14:
							num2 = 2;
							break;
						case 15:
							num2 = 2;
							break;
						case 16:
							num2 = 2;
							break;
						case 17:
							num2 = 2;
							break;
						case 18:
							num2 = 2;
							break;
						case 19:
							num2 = 2;
							break;
						case 20:
							num2 = 2;
							break;
						case 21:
							num2 = 2;
							break;
						case 22:
							num2 = 2;
							break;
						case 23:
							num2 = 1;
							break;
						}
					}
					else
					{
						num2 = 2;
					}
				}
				else
				{
					num = i;
					if (runtext[--num] == '<')
					{
						this.runtextpos = num;
						return true;
					}
					num2 = 1;
				}
			}
			this.runtextpos = this.runtextend;
			return false;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000DD04 File Offset: 0x0000CD04
		public override void InitTrackCount()
		{
			this.runtrackcount = 9;
		}
	}
}
