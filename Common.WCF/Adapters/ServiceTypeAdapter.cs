using System;

namespace TechnoPro.Common.WCF.Adapters
{
	// Token: 0x02000020 RID: 32
	public static class ServiceTypeAdapter
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003ED8 File Offset: 0x000020D8
		public static Type GetContractType(this string serviceName)
		{
			return Type.GetType(string.Format("TechnoPro.ClockWorkServer.Contracts.{0}, ClockWorkServer.Contracts", serviceName.GetContractName()));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003F00 File Offset: 0x00002100
		public static string GetContractName(this string serviceName)
		{
			return "I" + (serviceName.ToLower().EndsWith("service") ? serviceName.Substring(0, serviceName.Length - 7) : serviceName);
		}
	}
}
