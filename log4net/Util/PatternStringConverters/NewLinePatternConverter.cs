using System;
using log4net.Core;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000DF RID: 223
	internal sealed class NewLinePatternConverter : LiteralPatternConverter, IOptionHandler
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x00014BD8 File Offset: 0x00012DD8
		public void ActivateOptions()
		{
			if (SystemInfo.EqualsIgnoringCase(this.Option, "DOS"))
			{
				this.Option = "\r\n";
				return;
			}
			if (SystemInfo.EqualsIgnoringCase(this.Option, "UNIX"))
			{
				this.Option = "\n";
				return;
			}
			this.Option = SystemInfo.NewLine;
		}
	}
}
