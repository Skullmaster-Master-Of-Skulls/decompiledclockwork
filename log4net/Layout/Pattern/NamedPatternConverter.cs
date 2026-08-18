using System;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x02000099 RID: 153
	public abstract class NamedPatternConverter : PatternLayoutConverter, IOptionHandler
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0000F13C File Offset: 0x0000D33C
		public void ActivateOptions()
		{
			this.m_precision = 0;
			if (this.Option != null)
			{
				string text = this.Option.Trim();
				if (text.Length > 0)
				{
					int num;
					if (SystemInfo.TryParse(text, out num))
					{
						if (num <= 0)
						{
							LogLog.Error(NamedPatternConverter.declaringType, "NamedPatternConverter: Precision option (" + text + ") isn't a positive integer.");
							return;
						}
						this.m_precision = num;
						return;
					}
					else
					{
						LogLog.Error(NamedPatternConverter.declaringType, "NamedPatternConverter: Precision option \"" + text + "\" not a decimal integer.");
					}
				}
			}
		}

		// Token: 0x060004BD RID: 1213
		protected abstract string GetFullyQualifiedName(LoggingEvent loggingEvent);

		// Token: 0x060004BE RID: 1214 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
		protected sealed override void Convert(TextWriter writer, LoggingEvent loggingEvent)
		{
			string text = this.GetFullyQualifiedName(loggingEvent);
			if (this.m_precision <= 0 || text == null || text.Length < 2)
			{
				writer.Write(text);
				return;
			}
			int num = text.Length;
			string str = string.Empty;
			if (text.EndsWith("."))
			{
				str = ".";
				text = text.Substring(0, num - 1);
				num--;
			}
			int num2 = text.LastIndexOf(".");
			int num3 = 1;
			while (num2 > 0 && num3 < this.m_precision)
			{
				num2 = text.LastIndexOf('.', num2 - 1);
				num3++;
			}
			if (num2 == -1)
			{
				writer.Write(text + str);
				return;
			}
			writer.Write(text.Substring(num2 + 1, num - num2 - 1) + str);
		}

		// Token: 0x04000200 RID: 512
		private const string DOT = ".";

		// Token: 0x04000201 RID: 513
		private int m_precision;

		// Token: 0x04000202 RID: 514
		private static readonly Type declaringType = typeof(NamedPatternConverter);
	}
}
