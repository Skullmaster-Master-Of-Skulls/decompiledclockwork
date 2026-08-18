using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x02000035 RID: 53
	internal class BindParametersRegexRunner18 : RegexRunner
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000B7DC File Offset: 0x0000A7DC
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
			int num4 = (num5 = runtextend - num) + 1;
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
			}
			for (;;)
			{
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 3;
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				if (num < runtextend && runtext[num++] == '"')
				{
					runstack[--num3] = num;
					runtrack[--num2] = 1;
					runstack[--num3] = num;
					runtrack[--num2] = 1;
					runtrack[--num2] = num;
					runtrack[--num2] = 4;
					runstack[--num3] = num;
					runtrack[--num2] = 1;
					if (1 <= runtextend - num)
					{
						num++;
						num4 = 1;
						while (RegexRunner.CharInClass(runtext[num - num4--], "\0\u0002\t./\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
						{
							if (num4 <= 0)
							{
								num4 = (num5 = runtextend - num) + 1;
								while (--num4 > 0)
								{
									if (!RegexRunner.CharInClass(runtext[num++], "\0\u0002\t./\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
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
									goto IL_268;
								}
								goto IL_268;
							}
						}
					}
				}
				for (;;)
				{
					IL_E4C:
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
						goto IL_EE9;
					case 1:
						num3++;
						continue;
					case 2:
						goto IL_F06;
					case 3:
						num = runtrack[num2++];
						runstack[--num3] = num;
						runtrack[--num2] = 1;
						if (num >= runtextend || runtext[num++] != '\'')
						{
							continue;
						}
						runstack[--num3] = num;
						runtrack[--num2] = 1;
						runstack[--num3] = num;
						runtrack[--num2] = 1;
						runtrack[--num2] = num;
						runtrack[--num2] = 8;
						runstack[--num3] = num;
						runtrack[--num2] = 1;
						if (1 <= runtextend - num)
						{
							num++;
							num4 = 1;
							while (RegexRunner.CharInClass(runtext[num - num4--], "\0\u0002\t./\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
							{
								if (num4 <= 0)
								{
									num4 = (num5 = runtextend - num) + 1;
									while (--num4 > 0)
									{
										if (!RegexRunner.CharInClass(runtext[num++], "\0\u0002\t./\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
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
										goto IL_5D8;
									}
									goto IL_5D8;
								}
							}
							continue;
						}
						continue;
					case 4:
						num = runtrack[num2++];
						runstack[--num3] = num;
						runtrack[--num2] = 1;
						if (num < runtextend && runtext[num++] == '[' && 1 <= runtextend - num)
						{
							num++;
							num4 = 1;
							while (RegexRunner.CharInClass(runtext[num - num4--], "\0\u0001\0\0"))
							{
								if (num4 <= 0)
								{
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
										runtrack[--num2] = 7;
										goto IL_389;
									}
									goto IL_389;
								}
							}
							continue;
						}
						continue;
					case 5:
						goto IL_F78;
					case 6:
						runstack[--num3] = runtrack[num2++];
						this.Uncapture();
						continue;
					case 7:
						num = runtrack[num2++];
						num4 = runtrack[num2++];
						if (num4 > 0)
						{
							runtrack[--num2] = num4 - 1;
							runtrack[--num2] = num - 1;
							runtrack[--num2] = 7;
						}
						break;
					case 8:
						num = runtrack[num2++];
						runstack[--num3] = num;
						runtrack[--num2] = 1;
						if (num < runtextend && runtext[num++] == '[' && 1 <= runtextend - num)
						{
							num++;
							num4 = 1;
							while (RegexRunner.CharInClass(runtext[num - num4--], "\0\u0001\0\0"))
							{
								if (num4 <= 0)
								{
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
										runtrack[--num2] = 10;
										goto IL_6F9;
									}
									goto IL_6F9;
								}
							}
							continue;
						}
						continue;
					case 9:
						num = runtrack[num2++];
						num4 = runtrack[num2++];
						if (num4 > 0)
						{
							runtrack[--num2] = num4 - 1;
							runtrack[--num2] = num - 1;
							runtrack[--num2] = 9;
							goto IL_5D8;
						}
						goto IL_5D8;
					case 10:
						num = runtrack[num2++];
						num4 = runtrack[num2++];
						if (num4 > 0)
						{
							runtrack[--num2] = num4 - 1;
							runtrack[--num2] = num - 1;
							runtrack[--num2] = 10;
							goto IL_6F9;
						}
						goto IL_6F9;
					case 11:
						goto IL_10E8;
					case 12:
						num3 += 2;
						continue;
					case 13:
						goto IL_1144;
					case 14:
						num = runtrack[num2++];
						runstack[--num3] = num;
						runtrack[--num2] = 1;
						if (num >= runtextend || runtext[num++] != '\'')
						{
							continue;
						}
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
							runtrack[--num2] = 16;
							goto IL_BA9;
						}
						goto IL_BA9;
					case 15:
						goto IL_11A5;
					case 16:
						num = runtrack[num2++];
						num4 = runtrack[num2++];
						if (num4 > 0)
						{
							runtrack[--num2] = num4 - 1;
							runtrack[--num2] = num - 1;
							runtrack[--num2] = 16;
							goto IL_BA9;
						}
						goto IL_BA9;
					case 17:
						goto IL_1245;
					case 18:
						if ((num4 = runstack[num3++] - 1) >= 0)
						{
							goto Block_86;
						}
						runstack[num3] = runtrack[num2++];
						runstack[--num3] = num4;
						continue;
					case 19:
						goto IL_8C4;
					case 20:
						num4 = runtrack[num2++];
						runstack[--num3] = runtrack[num2++];
						runstack[--num3] = num4;
						continue;
					case 21:
						goto IL_1323;
					}
					IL_389:
					if (num < runtextend && runtext[num++] == ']')
					{
						goto Block_21;
					}
					continue;
					IL_6F9:
					if (num >= runtextend || runtext[num++] != ']')
					{
						continue;
					}
					num4 = runstack[num3++];
					this.Capture(9, num4, num);
					runtrack[--num2] = num4;
					runtrack[--num2] = 6;
					IL_748:
					num4 = runstack[num3++];
					this.Capture(7, num4, num);
					runtrack[--num2] = num4;
					runtrack[--num2] = 6;
					num4 = runstack[num3++];
					this.Capture(14, num4, num);
					runtrack[--num2] = num4;
					runtrack[--num2] = 6;
					if (num < runtextend && runtext[num++] == '\'')
					{
						goto Block_43;
					}
					continue;
					IL_5D8:
					num4 = runstack[num3++];
					this.Capture(8, num4, num);
					runtrack[--num2] = num4;
					runtrack[--num2] = 6;
					goto IL_748;
					IL_BA9:
					num4 = runstack[num3++];
					this.Capture(15, num4, num);
					runtrack[--num2] = num4;
					runtrack[--num2] = 6;
					if (num < runtextend && runtext[num++] == '\'')
					{
						goto Block_65;
					}
				}
				IL_F06:
				num = runtrack[num2++];
				num4 = runtrack[num2++];
				if (num4 > 0)
				{
					runtrack[--num2] = num4 - 1;
					runtrack[--num2] = num - 1;
					runtrack[--num2] = 2;
					continue;
				}
				continue;
				Block_21:
				num4 = runstack[num3++];
				this.Capture(5, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 6;
				goto IL_3D8;
				IL_F78:
				num = runtrack[num2++];
				num4 = runtrack[num2++];
				if (num4 > 0)
				{
					runtrack[--num2] = num4 - 1;
					runtrack[--num2] = num - 1;
					runtrack[--num2] = 5;
					goto IL_268;
				}
				goto IL_268;
				IL_1245:
				num = runtrack[num2++];
				num4 = runtrack[num2++];
				if (num4 > 0)
				{
					runtrack[--num2] = num4 - 1;
					runtrack[--num2] = num - 1;
					runtrack[--num2] = 17;
					goto IL_CCC;
				}
				goto IL_CCC;
				Block_65:
				num4 = runstack[num3++];
				this.Capture(13, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 6;
				goto IL_C28;
				IL_10E8:
				num = runtrack[num2++];
				num4 = runtrack[num2++];
				if (num4 > 0)
				{
					runtrack[--num2] = num4 - 1;
					runtrack[--num2] = num - 1;
					runtrack[--num2] = 11;
					goto IL_89B;
				}
				goto IL_89B;
				Block_43:
				num4 = runstack[num3++];
				this.Capture(6, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 6;
				goto IL_7F7;
				IL_1144:
				num = runtrack[num2++];
				num4 = runtrack[num2++];
				if (num4 > 0)
				{
					runtrack[--num2] = num4 - 1;
					runtrack[--num2] = num - 1;
					runtrack[--num2] = 13;
					goto IL_96F;
				}
				goto IL_96F;
				IL_11A5:
				num = runtrack[num2++];
				num4 = runtrack[num2++];
				if (num4 > 0)
				{
					runtrack[--num2] = num4 - 1;
					runtrack[--num2] = num - 1;
					runtrack[--num2] = 15;
					goto IL_A62;
				}
				goto IL_A62;
				IL_1323:
				num = runtrack[num2++];
				num4 = runtrack[num2++];
				if (num4 > 0)
				{
					runtrack[--num2] = num4 - 1;
					runtrack[--num2] = num - 1;
					runtrack[--num2] = 21;
					goto IL_E0A;
				}
				goto IL_E0A;
				Block_86:
				num = runstack[num3++];
				runtrack[--num2] = num4;
				runtrack[--num2] = 20;
				goto IL_D96;
				IL_3D8:
				num4 = runstack[num3++];
				this.Capture(3, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 6;
				num4 = runstack[num3++];
				this.Capture(14, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 6;
				if (num < runtextend && runtext[num++] == '"')
				{
					num4 = runstack[num3++];
					this.Capture(2, num4, num);
					runtrack[--num2] = num4;
					runtrack[--num2] = 6;
					goto IL_7F7;
				}
				goto IL_E4C;
				IL_268:
				num4 = runstack[num3++];
				this.Capture(4, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 6;
				goto IL_3D8;
				IL_8C4:
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				if (num >= runtextend || runtext[num++] != ',')
				{
					goto IL_E4C;
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
					runtrack[--num2] = 13;
					goto IL_96F;
				}
				goto IL_96F;
				IL_CFC:
				num4 = runstack[num3++];
				int num6 = num5 = runstack[num3++];
				runtrack[--num2] = num5;
				if ((num6 == num && num4 >= 0) || num4 >= 1)
				{
					runtrack[--num2] = num4;
					runtrack[--num2] = 20;
					goto IL_D96;
				}
				runstack[--num3] = num;
				runstack[--num3] = num4 + 1;
				runtrack[--num2] = 18;
				if (num2 <= 236 || num3 <= 177)
				{
					runtrack[--num2] = 19;
					goto IL_E4C;
				}
				goto IL_8C4;
				IL_CCC:
				num4 = runstack[num3++];
				this.Capture(10, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 6;
				goto IL_CFC;
				IL_C28:
				num4 = runstack[num3++];
				this.Capture(11, num4, num);
				runtrack[--num2] = num4;
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
					runtrack[--num2] = 17;
					goto IL_CCC;
				}
				goto IL_CCC;
				IL_89B:
				runstack[--num3] = -1;
				runstack[--num3] = 0;
				runtrack[--num2] = 12;
				goto IL_CFC;
				IL_7F7:
				num4 = runstack[num3++];
				this.Capture(1, num4, num);
				runtrack[--num2] = num4;
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
					runtrack[--num2] = 11;
					goto IL_89B;
				}
				goto IL_89B;
				IL_96F:
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				runtrack[--num2] = num;
				runtrack[--num2] = 14;
				runstack[--num3] = num;
				runtrack[--num2] = 1;
				if (num >= runtextend || runtext[num++] != '"')
				{
					goto IL_E4C;
				}
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
					runtrack[--num2] = 15;
				}
				IL_A62:
				num4 = runstack[num3++];
				this.Capture(15, num4, num);
				runtrack[--num2] = num4;
				runtrack[--num2] = 6;
				if (num < runtextend && runtext[num++] == '"')
				{
					num4 = runstack[num3++];
					this.Capture(12, num4, num);
					runtrack[--num2] = num4;
					runtrack[--num2] = 6;
					goto IL_C28;
				}
				goto IL_E4C;
				IL_E0A:
				if (num >= runtextend)
				{
					break;
				}
				goto IL_E4C;
				IL_D96:
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
					runtrack[--num2] = 21;
					goto IL_E0A;
				}
				goto IL_E0A;
			}
			num4 = runstack[num3++];
			this.Capture(0, num4, num);
			runtrack[--num2] = num4;
			runtrack[num2 - 1] = 6;
			IL_E43:
			this.runtextpos = num;
			return;
			IL_EE9:
			num = runtrack[num2++];
			goto IL_E43;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000CB5C File Offset: 0x0000BB5C
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
					if (RegexRunner.CharInClass(runtext[num++], "\0\u0004\u0001\"#'(d"))
					{
						goto IL_59;
					}
				}
				while (num2 > 0);
				bool result = false;
				goto IL_62;
				IL_59:
				num--;
				result = true;
				IL_62:
				this.runtextpos = num;
				return result;
			}
			return false;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000CBD8 File Offset: 0x0000BBD8
		public override void InitTrackCount()
		{
			this.runtrackcount = 59;
		}
	}
}
