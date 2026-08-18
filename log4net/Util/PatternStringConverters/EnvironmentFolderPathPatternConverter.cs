using System;
using System.IO;
using System.Security;

namespace log4net.Util.PatternStringConverters
{
	// Token: 0x020000DB RID: 219
	internal sealed class EnvironmentFolderPathPatternConverter : PatternConverter
	{
		// Token: 0x0600066B RID: 1643 RVA: 0x00014968 File Offset: 0x00012B68
		protected override void Convert(TextWriter writer, object state)
		{
			try
			{
				if (this.Option != null && this.Option.Length > 0)
				{
					Environment.SpecialFolder folder = (Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), this.Option, true);
					string folderPath = Environment.GetFolderPath(folder);
					if (folderPath != null && folderPath.Length > 0)
					{
						writer.Write(folderPath);
					}
				}
			}
			catch (SecurityException exception)
			{
				LogLog.Debug(EnvironmentFolderPathPatternConverter.declaringType, "Security exception while trying to expand environment variables. Error Ignored. No Expansion.", exception);
			}
			catch (Exception exception2)
			{
				LogLog.Error(EnvironmentFolderPathPatternConverter.declaringType, "Error occurred while converting environment variable.", exception2);
			}
		}

		// Token: 0x0400028D RID: 653
		private static readonly Type declaringType = typeof(EnvironmentFolderPathPatternConverter);
	}
}
