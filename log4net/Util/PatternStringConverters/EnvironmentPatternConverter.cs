using System;
using System.IO;
using System.Security;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000DC RID: 220
	internal sealed class EnvironmentPatternConverter : PatternConverter
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00014A24 File Offset: 0x00012C24
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				if (this.Option != null && this.Option.Length > 0)
				{
					string environmentVariable = Environment.GetEnvironmentVariable(this.Option);
					if (environmentVariable == null)
					{
						environmentVariable = Environment.GetEnvironmentVariable(this.Option, EnvironmentVariableTarget.User);
					}
					if (environmentVariable == null)
					{
						environmentVariable = Environment.GetEnvironmentVariable(this.Option, EnvironmentVariableTarget.Machine);
					}
					if (environmentVariable != null && environmentVariable.Length > 0)
					{
						writer.Write(environmentVariable);
					}
				}
			}
			catch (SecurityException exception)
			{
				LogLog.Debug(EnvironmentPatternConverter.declaringType, "Security exception while trying to expand environment variables. Error Ignored. No Expansion.", exception);
			}
			catch (Exception exception2)
			{
				LogLog.Error(EnvironmentPatternConverter.declaringType, "Error occurred while converting environment variable.", exception2);
			}
		}

		// Token: 0x0400028E RID: 654
		private static readonly Type declaringType = typeof(EnvironmentPatternConverter);
	}
}
