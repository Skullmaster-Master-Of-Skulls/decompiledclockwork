using System;

namespace TechnoPro.Common.Public.Exceptions
{
	// Token: 0x020000C5 RID: 197
	public class DataAccessLayerException : Exception
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x0000D70E File Offset: 0x0000B90E
		public DataAccessLayerException()
		{
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000D718 File Offset: 0x0000B918
		public DataAccessLayerException(string message) : base(message)
		{
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000D723 File Offset: 0x0000B923
		public DataAccessLayerException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
