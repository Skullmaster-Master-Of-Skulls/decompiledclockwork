using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Mappers.TPMailMan
{
	// Token: 0x02000035 RID: 53
	public static class TPSmtpClientMapper
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000067B4 File Offset: 0x000049B4
		static TPSmtpClientMapper()
		{
			Mapper.CreateMap<TPSmtpClientDTO, TPSmtpClient>().ForMember((TPSmtpClient pb) => pb.Id, delegate(IMemberConfigurationExpression<TPSmtpClientDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TPSmtpClient, TPSmtpClientDTO>();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006824 File Offset: 0x00004A24
		public static TPSmtpClient ToDomainObject(this TPSmtpClientDTO tPSmtpClientDTO)
		{
			return Mapper.Map<TPSmtpClientDTO, TPSmtpClient>(tPSmtpClientDTO);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000683C File Offset: 0x00004A3C
		public static TPSmtpClientDTO ToDTO(this TPSmtpClient tPSmtpClient)
		{
			return Mapper.Map<TPSmtpClient, TPSmtpClientDTO>(tPSmtpClient);
		}
	}
}
