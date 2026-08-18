using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Mappers.TPMailMan
{
	// Token: 0x0200002F RID: 47
	public static class TPMailAddressMapper
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x0000616C File Offset: 0x0000436C
		static TPMailAddressMapper()
		{
			Mapper.CreateMap<TPMailAddressDTO, TPMailAddress>().ForMember((TPMailAddress pb) => pb.Id, delegate(IMemberConfigurationExpression<TPMailAddressDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TPMailAddress, TPMailAddressDTO>();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000061DC File Offset: 0x000043DC
		public static TPMailAddress ToDomainObject(this TPMailAddressDTO tPMailAddressDTO)
		{
			return Mapper.Map<TPMailAddressDTO, TPMailAddress>(tPMailAddressDTO);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000061F4 File Offset: 0x000043F4
		public static TPMailAddressDTO ToDTO(this TPMailAddress tPMailAddress)
		{
			return Mapper.Map<TPMailAddress, TPMailAddressDTO>(tPMailAddress);
		}
	}
}
