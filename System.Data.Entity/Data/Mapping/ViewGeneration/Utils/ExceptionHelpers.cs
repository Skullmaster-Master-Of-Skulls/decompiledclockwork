using System;
using System.Data.Mapping.ViewGeneration.Structures;

namespace System.Data.Mapping.ViewGeneration.Utils
{
	// Token: 0x0200026F RID: 623
	internal static class ExceptionHelpers
	{
		// Token: 0x06002625 RID: 9765 RVA: 0x00091910 File Offset: 0x0008FB10
		internal static void ThrowMappingException(ErrorLog.Record errorRecord, ConfigViewGenerator config)
		{
			InternalMappingException ex = new InternalMappingException(errorRecord.ToUserString(), errorRecord);
			if (config.IsNormalTracing)
			{
				ex.ErrorLog.PrintTrace();
			}
			throw ex;
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x00091940 File Offset: 0x0008FB40
		internal static void ThrowMappingException(ErrorLog errorLog, ConfigViewGenerator config)
		{
			InternalMappingException ex = new InternalMappingException(errorLog.ToUserString(), errorLog);
			if (config.IsNormalTracing)
			{
				ex.ErrorLog.PrintTrace();
			}
			throw ex;
		}
	}
}
