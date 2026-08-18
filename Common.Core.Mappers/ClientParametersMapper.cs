using System;
using System.Collections.Generic;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x02000008 RID: 8
	public static class ClientParametersMapper
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002A3A File Offset: 0x00000C3A
		static ClientParametersMapper()
		{
			Mapper.CreateMap<ClientParametersDTO, ClientParameters>();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A44 File Offset: 0x00000C44
		public static ClientParameters ToDomainObject(this ClientParametersDTO clientParametersDTO)
		{
			bool flag = clientParametersDTO == null;
			ClientParameters result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ClientParameters clientParameters = new ClientParameters();
				foreach (KeyValuePair<string, string> keyValuePair in clientParametersDTO)
				{
					clientParameters.Add(keyValuePair.Key, keyValuePair.Value);
				}
				result = clientParameters;
			}
			return result;
		}
	}
}
