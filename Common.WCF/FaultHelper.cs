using System;
using System.ServiceModel;

namespace TechnoPro.Common.WCF
{
	// Token: 0x02000008 RID: 8
	public static class FaultHelper
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002EE4 File Offset: 0x000010E4
		public static FaultException<T> CreateFault<T>(T ex) where T : GenericFault
		{
			return new FaultException<T>(ex, ex.Message);
		}
	}
}
