using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x020006A9 RID: 1705
	internal sealed class CompiledRegexRunner : RegexRunner
	{
		// Token: 0x06003FD4 RID: 16340 RVA: 0x0010CC49 File Offset: 0x0010AE49
		internal CompiledRegexRunner()
		{
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x0010CC51 File Offset: 0x0010AE51
		internal void SetDelegates(NoParamDelegate go, FindFirstCharDelegate firstChar, NoParamDelegate trackCount)
		{
			this.goMethod = go;
			this.findFirstCharMethod = firstChar;
			this.initTrackCountMethod = trackCount;
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x0010CC68 File Offset: 0x0010AE68
		protected override void Go()
		{
			this.goMethod(this);
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x0010CC76 File Offset: 0x0010AE76
		protected override bool FindFirstChar()
		{
			return this.findFirstCharMethod(this);
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x0010CC84 File Offset: 0x0010AE84
		protected override void InitTrackCount()
		{
			this.initTrackCountMethod(this);
		}

		// Token: 0x04002E8E RID: 11918
		private NoParamDelegate goMethod;

		// Token: 0x04002E8F RID: 11919
		private FindFirstCharDelegate findFirstCharMethod;

		// Token: 0x04002E90 RID: 11920
		private NoParamDelegate initTrackCountMethod;
	}
}
