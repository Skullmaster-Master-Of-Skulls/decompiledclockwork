using System;

namespace TechnoPro.Common.Core.Updates.Adapters
{
	// Token: 0x02000012 RID: 18
	public static class UpdateExecuterAdapter
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00006278 File Offset: 0x00004478
		public static string ExecutingFileType(this IUpdateExecuter updateExecuter)
		{
			ExecuterFileTypeAttribute[] array = (ExecuterFileTypeAttribute[])updateExecuter.GetType().GetCustomAttributes(typeof(ExecuterFileTypeAttribute), true);
			return (array.Length != 0) ? array[0].FileType : string.Empty;
		}
	}
}
