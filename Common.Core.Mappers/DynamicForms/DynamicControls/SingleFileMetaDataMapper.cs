using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicControls;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicControls;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.DynamicControls
{
	// Token: 0x02000129 RID: 297
	public static class SingleFileMetaDataMapper
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x00018B58 File Offset: 0x00016D58
		static SingleFileMetaDataMapper()
		{
			DynamicFieldMapper.CreateMap();
			Mapper.CreateMap<SingleFileMetaDataDTO, SingleFileMetaData>().ForMember((SingleFileMetaData pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SingleFileMetaDataDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<SingleFileMetaData, SingleFileMetaDataDTO>();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00018BDC File Offset: 0x00016DDC
		public static SingleFileMetaData ToDomainObject(this SingleFileMetaDataDTO dynamicDataDTO)
		{
			return Mapper.Map<SingleFileMetaDataDTO, SingleFileMetaData>(dynamicDataDTO);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00018BF4 File Offset: 0x00016DF4
		public static SingleFileMetaDataDTO ToDTO(this SingleFileMetaData dynamicData)
		{
			return Mapper.Map<SingleFileMetaData, SingleFileMetaDataDTO>(dynamicData);
		}
	}
}
