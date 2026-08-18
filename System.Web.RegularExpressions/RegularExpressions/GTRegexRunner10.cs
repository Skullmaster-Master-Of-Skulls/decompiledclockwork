using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x0200001D RID: 29
	internal class GTRegexRunner10 : RegexRunner
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00007778 File Offset: 0x00006778
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
			if (num < runtextend && runtext[num++] != '%' && num < runtextend && runtext[num++] == '>')
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
						goto IL_140;
					case 1:
						num3++;
						break;
					case 2:
						runstack[--num3] = runtrack[num2++];
						this.Uncapture();
						break;
					}
				}
				IL_140:
				num = runtrack[num2++];
			}
			this.runtextpos = num;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00007900 File Offset: 0x00006900
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
					if (RegexRunner.CharInClass(runtext[num++], "\0\u0003\0\0%&"))
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

		// Token: 0x06000042 RID: 66 RVA: 0x0000797C File Offset: 0x0000697C
		public override void InitTrackCount()
		{
			this.runtrackcount = 3;
		}
	}
}
