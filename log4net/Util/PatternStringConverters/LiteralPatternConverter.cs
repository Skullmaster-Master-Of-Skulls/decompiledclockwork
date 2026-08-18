using System;
using System.IO;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000DE RID: 222
	internal class LiteralPatternConverter : PatternConverter
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x00014B7C File Offset: 0x00012D7C
		public override PatternConverter SetNext(PatternConverter pc)
		{
			LiteralPatternConverter literalPatternConverter = pc as LiteralPatternConverter;
			if (literalPatternConverter != null)
			{
				this.Option += literalPatternConverter.Option;
				return this;
			}
			return base.SetNext(pc);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00014BB3 File Offset: 0x00012DB3
		public override void Format(TextWriter writer, object state)
		{
			writer.Write(this.Option);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00014BC1 File Offset: 0x00012DC1
		protected override void Convert(TextWriter writer, object state)
		{
			throw new InvalidOperationException("Should never get here because of the overridden Format method");
		}
	}
}
