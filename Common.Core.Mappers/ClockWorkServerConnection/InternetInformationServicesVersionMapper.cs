using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.Mappers.ClockWorkServerConnection
{
	// Token: 0x0200016F RID: 367
	internal static class InternetInformationServicesVersionMapper
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x0001D0E8 File Offset: 0x0001B2E8
		static InternetInformationServicesVersionMapper()
		{
			Mapper.CreateMap<InternetInformationServicesVersion, InternetInformationServicesVersionDTO>();
			Mapper.CreateMap<InternetInformationServicesVersionDTO, InternetInformationServicesVersion>();
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}
	}
}
