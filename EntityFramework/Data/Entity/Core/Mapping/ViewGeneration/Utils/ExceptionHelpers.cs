using System;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Utils
{
	// Token: 0x0200048B RID: 1163
	internal static class ExceptionHelpers
	{
		// Token: 0x06002B14 RID: 11028 RVA: 0x000D07A0 File Offset: 0x000CE9A0
		internal static void ThrowMappingException(ErrorLog.Record errorRecord, ConfigViewGenerator config)
		{
			InternalMappingException ex = new InternalMappingException(errorRecord.ToUserString(), errorRecord);
			if (config.IsNormalTracing)
			{
				ex.ErrorLog.PrintTrace();
			}
			throw ex;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000D07D0 File Offset: 0x000CE9D0
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
