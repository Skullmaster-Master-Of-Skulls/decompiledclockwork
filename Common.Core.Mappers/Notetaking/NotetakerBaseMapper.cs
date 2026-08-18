using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Public.Entities.Notetaking;

namespace TechnoPro.Common.Core.Mappers.Notetaking
{
	// Token: 0x020000B8 RID: 184
	public static class NotetakerBaseMapper
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000FF48 File Offset: 0x0000E148
		static NotetakerBaseMapper()
		{
			Mapper.CreateMap<NotetakerBaseDTO, NotetakerBase>().ForMember((NotetakerBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<NotetakerBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<NotetakerBase, NotetakerBaseDTO>();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		public static NotetakerBase ToDomainObject(this NotetakerBaseDTO dto)
		{
			return Mapper.Map<NotetakerBaseDTO, NotetakerBase>(dto);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		public static NotetakerBaseDTO ToDTO(this NotetakerBase item)
		{
			return Mapper.Map<NotetakerBase, NotetakerBaseDTO>(item);
		}
	}
}
