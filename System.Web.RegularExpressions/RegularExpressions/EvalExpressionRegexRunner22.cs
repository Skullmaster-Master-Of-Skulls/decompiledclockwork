using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x02000041 RID: 65
	internal class EvalExpressionRegexRunner22 : RegexRunner
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000E080 File Offset: 0x0000D080
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
			if (num <= runtextbeg || runtext[num - 1] == '\n')
			{
				num4 = (num5 = runtextend - num) + 1;
				while (--num4 > 0)
				{
					if (!RegexRunner.CharInClass(char.ToLower(runtext[num++], CultureInfo.InvariantCulture), "\0\0\u0001d"))
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
					goto IL_10F;
				}
				goto IL_10F;
			}
			for (;;)
			{
				IL_3E9:
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
					goto IL_44A;
				case 1:
					num3++;
					break;
				case 2:
					goto IL_467;
				case 3:
					goto IL_4B7;
				case 4:
					goto IL_507;
				case 5:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					break;
				case 6:
					goto IL_576;
				}
			}
			IL_44A:
			num = runtrack[num2++];
			goto IL_3E0;
			IL_467:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 2;
				goto IL_10F;
			}
			goto IL_10F;
			IL_4B7:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 3;
				goto IL_211;
			}
			goto IL_211;
			IL_507:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 4;
				goto IL_2D0;
			}
			goto IL_2D0;
			IL_576:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 6;
				goto IL_3A7;
			}
			goto IL_3A7;
			IL_10F:
			if (4 > runtextend - num || char.ToLower(runtext[num], CultureInfo.InvariantCulture) != 'e' || char.ToLower(runtext[num + 1], CultureInfo.InvariantCulture) != 'v' || char.ToLower(runtext[num + 2], CultureInfo.InvariantCulture) != 'a' || char.ToLower(runtext[num + 3], CultureInfo.InvariantCulture) != 'l')
			{
				goto IL_3E9;
			}
			num += 4;
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(char.ToLower(runtext[num++], CultureInfo.InvariantCulture), "\0\0\u0001d"))
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
			IL_211:
			if (num >= runtextend || char.ToLower(runtext[num++], CultureInfo.InvariantCulture) != '(')
			{
				goto IL_3E9;
			}
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(char.ToLower(runtext[num++], CultureInfo.InvariantCulture), "\0\u0001\0\0"))
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
			IL_2D0:
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 5;
			if (num >= runtextend || char.ToLower(runtext[num++], CultureInfo.InvariantCulture) != ')')
			{
				goto IL_3E9;
			}
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(char.ToLower(runtext[num++], CultureInfo.InvariantCulture), "\0\0\u0001d"))
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
			IL_3A7:
			if (num < runtextend)
			{
				goto IL_3E9;
			}
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 5;
			IL_3E0:
			this.runtextpos = num;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000E654 File Offset: 0x0000D654
		public override bool FindFirstChar()
		{
			int num = this.runtextpos;
			string runtext = this.runtext;
			int num2 = this.runtextend - num;
			if (num2 > 0)
			{
				do
				{
					num2--;
					if (RegexRunner.CharInClass(char.ToLower(runtext[num++], CultureInfo.InvariantCulture), "\0\u0002\u0001efd"))
					{
						goto IL_63;
					}
				}
				while (num2 > 0);
				bool result = false;
				goto IL_6C;
				IL_63:
				num--;
				result = true;
				IL_6C:
				this.runtextpos = num;
				return result;
			}
			return false;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000E6D8 File Offset: 0x0000D6D8
		public override void InitTrackCount()
		{
			this.runtrackcount = 9;
		}
	}
}
