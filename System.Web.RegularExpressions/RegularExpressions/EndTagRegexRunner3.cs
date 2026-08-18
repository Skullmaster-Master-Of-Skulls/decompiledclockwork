using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x02000008 RID: 8
	internal class EndTagRegexRunner3 : RegexRunner
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00004E14 File Offset: 0x00003E14
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
			int num5;
			if (num == this.runtextstart && 2 <= runtextend - num && runtext[num] == '<' && runtext[num + 1] == '/')
			{
				num += 2;
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				if (1 <= runtextend - num)
				{
					num++;
					num4 = 1;
					while (RegexRunner.CharInClass(runtext[num - num4--], "\0\u0004\t./:;\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
					{
						if (num4 <= 0)
						{
							num4 = (num5 = runtextend - num) + 1;
							while (--num4 > 0)
							{
								if (!RegexRunner.CharInClass(runtext[num++], "\0\u0004\t./:;\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
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
								goto IL_188;
							}
							goto IL_188;
						}
					}
				}
			}
			for (;;)
			{
				IL_284:
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
					goto IL_2DD;
				case 1:
					num3++;
					break;
				case 2:
					goto IL_2FA;
				case 3:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					break;
				case 4:
					goto IL_369;
				}
			}
			IL_2DD:
			num = runtrack[num2++];
			goto IL_27B;
			IL_2FA:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 2;
				goto IL_188;
			}
			goto IL_188;
			IL_369:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 4;
				goto IL_22C;
			}
			goto IL_22C;
			IL_188:
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
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
			IL_22C:
			if (num >= runtextend || runtext[num++] != '>')
			{
				goto IL_284;
			}
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 3;
			IL_27B:
			this.runtextpos = num;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000051DC File Offset: 0x000041DC
		public override bool FindFirstChar()
		{
			if (this.runtextpos > this.runtextstart)
			{
				this.runtextpos = this.runtextend;
				return false;
			}
			return true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00005208 File Offset: 0x00004208
		public override void InitTrackCount()
		{
			this.runtrackcount = 7;
		}
	}
}
