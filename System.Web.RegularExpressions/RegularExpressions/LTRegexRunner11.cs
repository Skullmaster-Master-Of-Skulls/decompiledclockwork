using System;
using System.Text.RegularExpressions;

namespace System.Web.RegularExpressions
{
	// Token: 0x02000020 RID: 32
	internal class LTRegexRunner11 : RegexRunner
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00007A10 File Offset: 0x00006A10
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
			if (num < runtextend && runtext[num++] == '<' && num < runtextend && runtext[num++] != '%')
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

		// Token: 0x06000048 RID: 72 RVA: 0x00007B98 File Offset: 0x00006B98
		public override bool FindFirstChar()
		{
			string runtext = this.runtext;
			int runtextend = this.runtextend;
			int num2;
			for (int i = this.runtextpos + 0; i < runtextend; i = num2 + i)
			{
				int num;
				if ((num = (int)runtext[i]) == 60)
				{
					num = i;
					this.runtextpos = num;
					return true;
				}
				if ((num -= 60) == 0)
				{
					switch (num)
					{
					default:
						num2 = 0;
						break;
					}
				}
				else
				{
					num2 = 1;
				}
			}
			this.runtextpos = this.runtextend;
			return false;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00007C20 File Offset: 0x00006C20
		public override void InitTrackCount()
		{
			this.runtrackcount = 3;
		}
	}
}
