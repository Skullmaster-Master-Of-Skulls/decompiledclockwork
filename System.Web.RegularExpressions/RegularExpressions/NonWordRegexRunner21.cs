using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x0200003E RID: 62
	internal class NonWordRegexRunner21 : RegexRunner
	{
		// Token: 0x0600008D RID: 141 RVA: 0x0000DDFC File Offset: 0x0000CDFC
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
			if (num < runtextend && RegexRunner.CharInClass(runtext[num++], "\u0001\0\t\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
			{
				int num4 = runstack[num3++];
				this.Capture(0, num4, num);
				runtrack[--num2] = num4;
				runtrack[num2 - 1] = 2;
			}
			else
			{
				for (;;)
				{
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
						goto IL_129;
					case 1:
						num3++;
						break;
					case 2:
						runstack[--num3] = runtrack[num2++];
						this.Uncapture();
						break;
					}
				}
				IL_129:
				num = runtrack[num2++];
			}
			this.runtextpos = num;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000DF70 File Offset: 0x0000CF70
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
					if (RegexRunner.CharInClass(runtext[num++], "\u0001\0\t\0\u0002\u0004\u0005\u0003\u0001\t\u0013\0"))
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

		// Token: 0x0600008F RID: 143 RVA: 0x0000DFEC File Offset: 0x0000CFEC
		public override void InitTrackCount()
		{
			this.runtrackcount = 3;
		}
	}
}
