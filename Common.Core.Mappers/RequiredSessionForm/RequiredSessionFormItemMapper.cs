using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Mappers.RequiredSessionForm
{
	// Token: 0x02000086 RID: 134
	public static class RequiredSessionFormItemMapper
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		static RequiredSessionFormItemMapper()
		{
			TPMailMessageMapper.CreateMap();
			Mapper.CreateMap<RequiredSessionFormItem, RequiredSessionFormItemDTO>().ForMember((RequiredSessionFormItemDTO pb) => pb.EmailTemplate, delegate(IMemberConfigurationExpression<RequiredSessionFormItem> m)
			{
				m.MapFrom<TPMailMessageDTO>((RequiredSessionFormItem pbdto) => pbdto.EmailTemplate.ToDTO());
			});
			Mapper.CreateMap<RequiredSessionFormItemDTO, RequiredSessionFormItem>().ForMember((RequiredSessionFormItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<RequiredSessionFormItemDTO> m)
			{
				m.Ignore();
			}).ForMember((RequiredSessionFormItem pb) => pb.EmailTemplate, delegate(IMemberConfigurationExpression<RequiredSessionFormItemDTO> m)
			{
				m.MapFrom<TPMailMessage>((RequiredSessionFormItemDTO pbdto) => pbdto.EmailTemplate.ToDomainObject());
			});
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D214 File Offset: 0x0000B414
		public static RequiredSessionFormItemDTO ToDTO(this RequiredSessionFormItem attachedFileInfo)
		{
			return Mapper.Map<RequiredSessionFormItem, RequiredSessionFormItemDTO>(attachedFileInfo);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D22C File Offset: 0x0000B42C
		public static RequiredSessionFormItem ToDomainObject(this RequiredSessionFormItemDTO attachedFileInfoDTO)
		{
			return Mapper.Map<RequiredSessionFormItemDTO, RequiredSessionFormItem>(attachedFileInfoDTO);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000D244 File Offset: 0x0000B444
		public static IList<RequiredSessionFormItemDTO> ToDTO(this IList<RequiredSessionFormItem> list)
		{
			IList<RequiredSessionFormItemDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<RequiredSessionFormItemDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D288 File Offset: 0x0000B488
		public static IList<RequiredSessionFormItem> ToDomainObject(this IList<RequiredSessionFormItemDTO> list)
		{
			IList<RequiredSessionFormItem> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<RequiredSessionFormItem>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
