using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x0200004A RID: 74
	internal class DirectiveRegex40Runner25 : RegexRunner
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x0001088C File Offset: 0x0000F88C
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
			if (num == this.runtextstart && 2 <= runtextend - num && runtext[num] == '<' && runtext[num + 1] == '%')
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
					goto IL_12F;
				}
				goto IL_12F;
			}
			for (;;)
			{
				IL_CCC:
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
					goto IL_D79;
				case 1:
					num3++;
					continue;
				case 2:
					goto IL_D96;
				case 3:
					goto IL_DE6;
				case 4:
					goto IL_E36;
				case 5:
					num3 += 2;
					continue;
				case 6:
					runstack[--num3] = runtrack[num2++];
					continue;
				case 7:
				{
					int num6;
					if ((num6 = runtrack[num2++]) != this.Crawlpos())
					{
						do
						{
							this.Uncapture();
						}
						while ((num6 = num6) != this.Crawlpos());
					}
					continue;
				}
				case 8:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					continue;
				case 9:
					num = runtrack[num2++];
					runtrack[--num2] = num;
					runtrack[--num2] = 13;
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
						runtrack[--num2] = 14;
					}
					break;
				case 10:
					goto IL_F03;
				case 11:
					goto IL_F53;
				case 12:
					goto IL_FA3;
				case 13:
					num = runtrack[num2++];
					runtrack[--num2] = num;
					runtrack[--num2] = 17;
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
						runtrack[--num2] = 18;
						goto IL_8F8;
					}
					goto IL_8F8;
				case 14:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 14;
					}
					break;
				case 15:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 15;
						goto IL_77B;
					}
					goto IL_77B;
				case 16:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 16;
						goto IL_818;
					}
					goto IL_818;
				case 17:
					goto IL_10F4;
				case 18:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 18;
						goto IL_8F8;
					}
					goto IL_8F8;
				case 19:
					goto IL_1155;
				case 20:
					goto IL_11A5;
				case 21:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (RegexRunner.CharInClass(runtext[num++], "\0\0\u0001d"))
					{
						goto Block_80;
					}
					continue;
				case 22:
					goto IL_1260;
				case 23:
					goto IL_16B;
				case 24:
					runstack[--num3] = runtrack[num2++];
					continue;
				case 25:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (RegexRunner.CharInClass(runtext[num++], "\0\0\u0001d"))
					{
						goto Block_82;
					}
					continue;
				}
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				if (num >= runtextend || runtext[num++] != '=')
				{
					continue;
				}
				num4 = runstack[num3++];
				this.Capture(4, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 8;
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
					runtrack[--num2] = 15;
				}
				IL_77B:
				if (num >= runtextend || runtext[num++] != '\'')
				{
					continue;
				}
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				num4 = (num5 = runtextend - num) + 1;
				while (--num4 > 0)
				{
					if (runtext[num++] == '\'')
					{
						num--;
						break;
					}
				}
				if (num5 > num4)
				{
					runtrack[--num2] = num5 - num4 - 1;
					runtrack[--num2] = num - 1;
					runtrack[--num2] = 16;
				}
				IL_818:
				num4 = runstack[num3++];
				this.Capture(5, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 8;
				if (num < runtextend && runtext[num++] == '\'')
				{
					break;
				}
				continue;
				IL_8F8:
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				if (num < runtextend && runtext[num++] == '=')
				{
					goto Block_54;
				}
			}
			goto IL_B57;
			Block_54:
			num4 = runstack[num3++];
			this.Capture(4, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
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
				runtrack[--num2] = 19;
			}
			IL_9D3:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(runtext[num++], "\u0001\b\u0001\"#%&'(>?d"))
				{
					num--;
					break;
				}
			}
			if (num5 > num4)
			{
				runtrack[--num2] = num5 - num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 20;
			}
			IL_A5F:
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			goto IL_B57;
			IL_B27:
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			goto IL_B57;
			IL_D79:
			num = runtrack[num2++];
			goto IL_CC3;
			IL_D96:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 2;
				goto IL_12F;
			}
			goto IL_12F;
			IL_DE6:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 3;
				goto IL_1F7;
			}
			goto IL_1F7;
			IL_E36:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 4;
				goto IL_2AA;
			}
			goto IL_2AA;
			IL_F03:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 10;
				goto IL_448;
			}
			goto IL_448;
			IL_F53:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 11;
				goto IL_523;
			}
			goto IL_523;
			IL_FA3:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 12;
				goto IL_5C0;
			}
			goto IL_5C0;
			IL_10F4:
			num = runtrack[num2++];
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			num4 = runstack[num3++];
			this.Capture(4, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if ((num4 = runtextend - num) > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 21;
				goto IL_B27;
			}
			goto IL_B27;
			IL_1155:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 19;
				goto IL_9D3;
			}
			goto IL_9D3;
			IL_11A5:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 20;
				goto IL_A5F;
			}
			goto IL_A5F;
			Block_80:
			if (num5 > 0)
			{
				runtrack[--num2] = num5 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 21;
				goto IL_B27;
			}
			goto IL_B27;
			IL_1260:
			num = runtrack[num2++];
			int num7 = runstack[num3++];
			runtrack[--num2] = 24;
			goto IL_C2A;
			Block_82:
			if (num5 > 0)
			{
				runtrack[--num2] = num5 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 25;
				goto IL_C5D;
			}
			goto IL_C5D;
			IL_12F:
			if (num < runtextend && runtext[num++] == '@')
			{
				runstack[--num3] = -1;
				runtrack[--num2] = 1;
				goto IL_BB7;
			}
			goto IL_CCC;
			IL_16B:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
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
			IL_1F7:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (num >= runtextend || !RegexRunner.CharInClass(runtext[num++], "\0\0\t\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
			{
				goto IL_CCC;
			}
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(runtext[num++], "\0\u0002\t:;\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
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
			IL_2AA:
			runstack[--num3] = this.runtrack.Length - num2;
			runstack[--num3] = this.Crawlpos();
			runtrack[--num2] = 5;
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (num >= runtextend || !RegexRunner.CharInClass(runtext[num++], "\u0001\0\t\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
			{
				goto IL_CCC;
			}
			num = (runtrack[--num2] = runstack[num3++]);
			runtrack[num2 - 1] = 6;
			num4 = runstack[num3++];
			num2 = this.runtrack.Length - runstack[num3++];
			runtrack[--num2] = num4;
			runtrack[--num2] = 7;
			num4 = runstack[num3++];
			this.Capture(3, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			runtrack[--num2] = num;
			runtrack[--num2] = 9;
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
			IL_448:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (num >= runtextend || runtext[num++] != '=')
			{
				goto IL_CCC;
			}
			num4 = runstack[num3++];
			this.Capture(4, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
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
			IL_523:
			if (num >= runtextend || runtext[num++] != '"')
			{
				goto IL_CCC;
			}
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
				runtrack[--num2] = 12;
			}
			IL_5C0:
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			if (num >= runtextend || runtext[num++] != '"')
			{
				goto IL_CCC;
			}
			IL_B57:
			num4 = runstack[num3++];
			this.Capture(2, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			IL_BB7:
			int num8 = num4 = runstack[num3++];
			runtrack[--num2] = num4;
			if (num8 != num)
			{
				runtrack[--num2] = num;
				runstack[--num3] = num;
				runtrack[--num2] = 22;
				if (num2 <= 204 || num3 <= 153)
				{
					runtrack[--num2] = 23;
					goto IL_CCC;
				}
				goto IL_16B;
			}
			else
			{
				runtrack[--num2] = 24;
			}
			IL_C2A:
			if ((num4 = runtextend - num) > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 25;
			}
			IL_C5D:
			if (2 > runtextend - num || runtext[num] != '%' || runtext[num + 1] != '>')
			{
				goto IL_CCC;
			}
			num += 2;
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 8;
			IL_CC3:
			this.runtextpos = num;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00011BA4 File Offset: 0x00010BA4
		public override bool FindFirstChar()
		{
			if (this.runtextpos > this.runtextstart)
			{
				this.runtextpos = this.runtextend;
				return false;
			}
			return true;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00011BD0 File Offset: 0x00010BD0
		public override void InitTrackCount()
		{
			this.runtrackcount = 51;
		}
	}
}
