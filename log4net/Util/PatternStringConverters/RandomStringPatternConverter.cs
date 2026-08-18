using System;
using System.IO;
using log4net.Core;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000E2 RID: 226
	internal sealed class RandomStringPatternConverter : PatternConverter, IOptionHandler
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x00014D1C File Offset: 0x00012F1C
		public void ActivateOptions()
		{
			string option = this.Option;
			if (option != null && option.Length > 0)
			{
				int length;
				if (SystemInfo.TryParse(option, out length))
				{
					this.m_length = length;
					return;
				}
				LogLog.Error(RandomStringPatternConverter.declaringType, "RandomStringPatternConverter: Could not convert Option [" + option + "] to Length Int32");
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00014D68 File Offset: 0x00012F68
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				lock (RandomStringPatternConverter.s_random)
				{
					for (int i = 0; i < this.m_length; i++)
					{
						int num = RandomStringPatternConverter.s_random.Next(36);
						if (num < 26)
						{
							char value = (char)(65 + num);
							writer.Write(value);
						}
						else if (num < 36)
						{
							char value2 = (char)(48 + (num - 26));
							writer.Write(value2);
						}
						else
						{
							writer.Write('X');
						}
					}
				}
			}
			catch (Exception exception)
			{
				LogLog.Error(RandomStringPatternConverter.declaringType, "Error occurred while converting.", exception);
			}
		}

		// Token: 0x04000291 RID: 657
		private static readonly Random s_random = new Random();

		// Token: 0x04000292 RID: 658
		private int m_length = 4;

		// Token: 0x04000293 RID: 659
		private static readonly Type declaringType = typeof(RandomStringPatternConverter);
	}
}
