using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x02000029 RID: 41
	internal class SimpleDirectiveRegexRunner14 : RegexRunner
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00008A18 File Offset: 0x00007A18
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
				IL_CBF:
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
					goto IL_D6C;
				case 1:
					num3++;
					continue;
				case 2:
					goto IL_D89;
				case 3:
					goto IL_DD9;
				case 4:
					goto IL_E29;
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
					goto IL_EF6;
				case 11:
					goto IL_F46;
				case 12:
					goto IL_F96;
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
						goto IL_8EB;
					}
					goto IL_8EB;
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
						goto IL_76E;
					}
					goto IL_76E;
				case 16:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 16;
						goto IL_80B;
					}
					goto IL_80B;
				case 17:
					goto IL_10E7;
				case 18:
					num = runtrack[num2++];
					num4 = runtrack[num2++];
					if (num4 > 0)
					{
						runtrack[--num2] = num4 - 1;
						runtrack[--num2] = num - 1;
						runtrack[--num2] = 18;
						goto IL_8EB;
					}
					goto IL_8EB;
				case 19:
					goto IL_1148;
				case 20:
					goto IL_1198;
				case 21:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (RegexRunner.CharInClass(runtext[num++], "\0\0\u0001d"))
					{
						goto Block_79;
					}
					continue;
				case 22:
					goto IL_1253;
				case 23:
					goto IL_15E;
				case 24:
					runstack[--num3] = runtrack[num2++];
					continue;
				case 25:
					num = runtrack[num2++];
					num5 = runtrack[num2++];
					if (RegexRunner.CharInClass(runtext[num++], "\0\0\u0001d"))
					{
						goto Block_81;
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
				IL_76E:
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
				IL_80B:
				num4 = runstack[num3++];
				this.Capture(5, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 8;
				if (num < runtextend && runtext[num++] == '\'')
				{
					break;
				}
				continue;
				IL_8EB:
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				if (num < runtextend && runtext[num++] == '=')
				{
					goto Block_53;
				}
			}
			goto IL_B4A;
			Block_53:
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
			IL_9C6:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			num4 = (num5 = runtextend - num) + 1;
			while (--num4 > 0)
			{
				if (!RegexRunner.CharInClass(runtext[num++], "\u0001\u0004\u0001%&>?d"))
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
			IL_A52:
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			goto IL_B4A;
			IL_B1A:
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			goto IL_B4A;
			IL_D6C:
			num = runtrack[num2++];
			goto IL_CB6;
			IL_D89:
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
			IL_DD9:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 3;
				goto IL_1EA;
			}
			goto IL_1EA;
			IL_E29:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 4;
				goto IL_29D;
			}
			goto IL_29D;
			IL_EF6:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 10;
				goto IL_43B;
			}
			goto IL_43B;
			IL_F46:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 11;
				goto IL_516;
			}
			goto IL_516;
			IL_F96:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 12;
				goto IL_5B3;
			}
			goto IL_5B3;
			IL_10E7:
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
				goto IL_B1A;
			}
			goto IL_B1A;
			IL_1148:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 19;
				goto IL_9C6;
			}
			goto IL_9C6;
			IL_1198:
			num = runtrack[num2++];
			num4 = runtrack[num2++];
			if (num4 > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num - 1;
				runtrack[--num2] = 20;
				goto IL_A52;
			}
			goto IL_A52;
			Block_79:
			if (num5 > 0)
			{
				runtrack[--num2] = num5 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 21;
				goto IL_B1A;
			}
			goto IL_B1A;
			IL_1253:
			num = runtrack[num2++];
			int num7 = runstack[num3++];
			runtrack[--num2] = 24;
			goto IL_C1D;
			Block_81:
			if (num5 > 0)
			{
				runtrack[--num2] = num5 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 25;
				goto IL_C50;
			}
			goto IL_C50;
			IL_122:
			if (num < runtextend && runtext[num++] == '@')
			{
				runstack[--num3] = -1;
				runtrack[--num2] = 1;
				goto IL_BAA;
			}
			goto IL_CBF;
			IL_15E:
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
			IL_1EA:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (num >= runtextend || !RegexRunner.CharInClass(runtext[num++], "\0\0\t\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
			{
				goto IL_CBF;
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
			IL_29D:
			runstack[--num3] = this.runtrack.Length - num2;
			runstack[--num3] = this.Crawlpos();
			runtrack[--num2] = 5;
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (num >= runtextend || !RegexRunner.CharInClass(runtext[num++], "\u0001\0\t\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
			{
				goto IL_CBF;
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
			IL_43B:
			runstack[--num3] = num;
			runtrack[--num2] = 1;
			if (num >= runtextend || runtext[num++] != '=')
			{
				goto IL_CBF;
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
			IL_516:
			if (num >= runtextend || runtext[num++] != '"')
			{
				goto IL_CBF;
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
			IL_5B3:
			num4 = runstack[num3++];
			this.Capture(5, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			if (num >= runtextend || runtext[num++] != '"')
			{
				goto IL_CBF;
			}
			IL_B4A:
			num4 = runstack[num3++];
			this.Capture(2, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			num4 = runstack[num3++];
			this.Capture(1, num4, num);
			runtrack[--num2] = num4;
			runtrack[--num2] = 8;
			IL_BAA:
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
					goto IL_CBF;
				}
				goto IL_15E;
			}
			else
			{
				runtrack[--num2] = 24;
			}
			IL_C1D:
			if ((num4 = runtextend - num) > 0)
			{
				runtrack[--num2] = num4 - 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 25;
			}
			IL_C50:
			if (2 > runtextend - num || runtext[num] != '%' || runtext[num + 1] != '>')
			{
				goto IL_CBF;
			}
			num += 2;
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 8;
			IL_CB6:
			this.runtextpos = num;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00009D24 File Offset: 0x00008D24
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

		// Token: 0x0600005E RID: 94 RVA: 0x00009EC4 File Offset: 0x00008EC4
		public override void InitTrackCount()
		{
			this.runtrackcount = 51;
		}
	}
}
