using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x02000002 RID: 2
	internal class TagRegexRunner1 : RegexRunner
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000010D0
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
			if (num == this.runtextstart && num < runtextend && runtext[num++] == '<')
			{
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
								goto IL_171;
							}
							goto IL_171;
						}
					}
				}
			}
			for (;;)
			{
				IL_EE8:
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
					goto IL_FA9;
				case 1:
					num3++;
					continue;
				case 2:
					goto IL_FC6;
				case 3:
					runstack[--num3] = runtrack[num2++];
					this.Uncapture();
					continue;
				case 4:
					goto IL_1035;
				case 5:
					goto IL_1085;
				case 6:
					num = runtrack[num2++];
					runtrack[--num2] = num;
					runtrack[--num2] = 10;
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
					break;
				case 7:
					goto IL_10E6;
				case 8:
					goto IL_1136;
				case 9:
					goto IL_1186;
				case 10:
					num = runtrack[num2++];
					runtrack[--num2] = num;
					runtrack[--num2] = 14;
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
						goto IL_832;
					}
					goto IL_832;
				case 11:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 11;
					}
					break;
				case 12:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 12;
						goto IL_6B5;
					}
					goto IL_6B5;
				case 13:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 13;
						goto IL_752;
					}
					goto IL_752;
				case 14:
					num = runtrack[num2++];
					runtrack[--num2] = num;
					runtrack[--num2] = 18;
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
						goto IL_A50;
					}
					goto IL_A50;
				case 15:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 15;
						goto IL_832;
					}
					goto IL_832;
				case 16:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 16;
						goto IL_8C5;
					}
					goto IL_8C5;
				case 17:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (!RegexRunner.CharInClass(runtext[num++], "\0\u0001\0\0"))
					{
						continue;
					}
					if (num5 > 0)
					{
						runtrack[--num2] = num5 - 1;
						runtrack[--num2] = num;
						runtrack[--num2] = 17;
						goto IL_959;
					}
					goto IL_959;
				case 18:
					goto IL_13F3;
				case 19:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 19;
						goto IL_A50;
					}
					goto IL_A50;
				case 20:
					goto IL_1454;
				case 21:
					goto IL_14A4;
				case 22:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (RegexRunner.CharInClass(runtext[num++], "\0\0\u0001d"))
					{
						goto Block_107;
					}
					continue;
				case 23:
					goto IL_155F;
				case 24:
					goto IL_1BE;
				case 25:
					runstack[--num3] = runtrack[num2++];
					continue;
				case 26:
					goto IL_15A0;
				case 27:
					num3 += 2;
					continue;
				case 28:
					if ((num4 = runstack[num3++] - 1) >= 0)
					{
						goto Block_110;
					}
					runstack[num3] = runtrack[num2++];
					runstack[--num3] = num4;
					continue;
				case 29:
					goto IL_D8F;
				case 30:
					num4 = runtrack[num2++];
					runstack[--num3] = runtrack[num2++];
					runstack[--num3] = num4;
					continue;
				}
				if (num >= runtextend || runtext[num++] != '=')
				{
					continue;
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
					runtrack[--num2] = 12;
				}
				IL_6B5:
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
					runtrack[--num2] = 13;
				}
				IL_752:
				num4 = runstack[num3++];
				this.Capture(5, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 3;
				if (num < runtextend && runtext[num++] == '\'')
				{
					break;
				}
				continue;
				IL_832:
				if (num >= runtextend || runtext[num++] != '=')
				{
					continue;
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
					runtrack[--num2] = 16;
				}
				IL_8C5:
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				if (3 > runtextend - num || runtext[num] != '<' || runtext[num + 1] != '%' || runtext[num + 2] != '#')
				{
					continue;
				}
				num += 3;
				if ((num4 = runtextend - num) > 0)
				{
					runtrack[--num2] = num4 - 1;
					runtrack[--num2] = num;
					runtrack[--num2] = 17;
				}
				IL_959:
				if (2 <= runtextend - num && runtext[num] == '%' && runtext[num + 1] == '>')
				{
					goto Block_66;
				}
				continue;
				IL_A50:
				if (num < runtextend && runtext[num++] == '=')
				{
					goto Block_71;
				}
			}
			goto IL_C1F;
			Block_66:
			num += 2;
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
			goto IL_C1F;
			Block_71:
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
				runtrack[--num2] = 20;
			}
			IL_AE3:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(runtext[num++], "\u0001\u0004\u0001/0=?d"))
				{
					num--;
					break;
				}
			}
			if (num5 > num4)
			{
				runtrack[--num2] = num5 - num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 21;
			}
			IL_B6F:
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
			goto IL_C1F;
			IL_BEF:
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
			goto IL_C1F;
			IL_FA9:
			num = runtrack[num2++];
			goto IL_EDF;
			IL_FC6:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 2;
				goto IL_171;
			}
			goto IL_171;
			IL_1035:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 4;
				goto IL_28B;
			}
			goto IL_28B;
			IL_1085:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 5;
				goto IL_33E;
			}
			goto IL_33E;
			IL_10E6:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 7;
				goto IL_412;
			}
			goto IL_412;
			IL_1136:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 8;
				goto IL_4A5;
			}
			goto IL_4A5;
			IL_1186:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 9;
				goto IL_542;
			}
			goto IL_542;
			IL_13F3:
			num = runtrack[num2++];
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if ((num4 = runtextend - num) > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 22;
				goto IL_BEF;
			}
			goto IL_BEF;
			IL_1454:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 20;
				goto IL_AE3;
			}
			goto IL_AE3;
			IL_14A4:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 21;
				goto IL_B6F;
			}
			goto IL_B6F;
			Block_107:
			if (num5 > 0)
			{
				runtrack[--num2] = num5 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 22;
				goto IL_BEF;
			}
			goto IL_BEF;
			IL_155F:
			num = runtrack[num2++];
			int num6 = runstack[num3++];
			runtrack[--num2] = 25;
			goto IL_CF2;
			IL_15A0:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 26;
				goto IL_D66;
			}
			goto IL_D66;
			Block_110:
			num = runstack[num3++];
			runtrack[--num2] = num4;
			runtrack[--num2] = 30;
			goto IL_E90;
			IL_171:
			num4 = runstack[num3++];
			this.Capture(3, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
			runstack[--num3] = -1;
			runtrack[--num2] = 1;
			goto IL_C7F;
			IL_1BE:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (1 <= runtextend - num)
			{
				num++;
				num4 = 1;
				while (RegexRunner.CharInClass(runtext[num - num4--], "\0\0\u0001d"))
				{
					if (num4 <= 0)
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
							runtrack[--num2] = 4;
							goto IL_28B;
						}
						goto IL_28B;
					}
				}
				goto IL_EE8;
			}
			goto IL_EE8;
			IL_28B:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (num >= runtextend || !RegexRunner.CharInClass(runtext[num++], "\0\0\t\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
			{
				goto IL_EE8;
			}
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(runtext[num++], "\0\u0004\t-.:;\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
				{
					num--;
					break;
				}
			}
			if (num5 > num4)
			{
				runtrack[--num2] = num5 - num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 5;
			}
			IL_33E:
			num4 = runstack[num3++];
			this.Capture(4, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			runtrack[--num2] = num;
			runtrack[--num2] = 6;
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
				runtrack[--num2] = 7;
			}
			IL_412:
			if (num >= runtextend || runtext[num++] != '=')
			{
				goto IL_EE8;
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
				runtrack[--num2] = 8;
			}
			IL_4A5:
			if (num >= runtextend || runtext[num++] != '"')
			{
				goto IL_EE8;
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
				runtrack[--num2] = 9;
			}
			IL_542:
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
			if (num >= runtextend || runtext[num++] != '"')
			{
				goto IL_EE8;
			}
			IL_C1F:
			num4 = runstack[num3++];
			this.Capture(2, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
			IL_C7F:
			int num7 = num4 = runstack[num3++];
			runtrack[--num2] = num4;
			if (num7 != num)
			{
				runtrack[--num2] = num;
				runstack[--num3] = num;
				runtrack[--num2] = 23;
				if (num2 <= 212 || num3 <= 159)
				{
					runtrack[--num2] = 24;
					goto IL_EE8;
				}
				goto IL_1BE;
			}
			else
			{
				runtrack[--num2] = 25;
			}
			IL_CF2:
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
				runtrack[--num2] = 26;
			}
			IL_D66:
			runstack[--num3] = -1;
			runstack[--num3] = 0;
			runtrack[--num2] = 27;
			goto IL_DF6;
			IL_D8F:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (num >= runtextend || runtext[num++] != '/')
			{
				goto IL_EE8;
			}
			num4 = runstack[num3++];
			this.Capture(6, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 3;
			IL_DF6:
			num4 = runstack[num3++];
			int num8 = num5 = runstack[num3++];
			runtrack[--num2] = num5;
			if ((num8 != num || num4 < 0) && num4 < 1)
			{
				runstack[--num3] = num;
				runstack[--num3] = num4 + 1;
				runtrack[--num2] = 28;
				if (num2 <= 212 || num3 <= 159)
				{
					runtrack[--num2] = 29;
					goto IL_EE8;
				}
				goto IL_D8F;
			}
			else
			{
				runtrack[--num2] = num4;
				runtrack[--num2] = 30;
			}
			IL_E90:
			if (num >= runtextend || runtext[num++] != '>')
			{
				goto IL_EE8;
			}
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 3;
			IL_EDF:
			this.runtextpos = num;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00003768 File Offset: 0x00002768
		public override bool FindFirstChar()
		{
			if (this.runtextpos > this.runtextstart)
			{
				this.runtextpos = this.runtextend;
				return false;
			}
			return true;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00003794 File Offset: 0x00002794
		public override void InitTrackCount()
		{
			this.runtrackcount = 53;
		}
	}
}
