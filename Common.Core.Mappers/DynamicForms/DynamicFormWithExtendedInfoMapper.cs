using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000121 RID: 289
	public static class DynamicFormWithExtendedInfoMapper
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x0001819C File Offset: 0x0001639C
		static DynamicFormWithExtendedInfoMapper()
		{
			DynamicFormMapper.CreateMap();
			Mapper.CreateMap<DynamicFormWithExtendedInfoDTO, DynamicFormWithExtendedInfo>().ForMember((DynamicFormWithExtendedInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicFormWithExtendedInfoDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DynamicFormWithExtendedInfo, DynamicFormWithExtendedInfoDTO>();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00018220 File Offset: 0x00016420
		public static DynamicFormWithExtendedInfo ToDomainObject(this DynamicFormWithExtendedInfoDTO dto)
		{
			return Mapper.Map<DynamicFormWithExtendedInfoDTO, DynamicFormWithExtendedInfo>(dto);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00018238 File Offset: 0x00016438
		public static DynamicFormWithExtendedInfoDTO ToDTO(this DynamicFormWithExtendedInfo item)
		{
			return Mapper.Map<DynamicFormWithExtendedInfo, DynamicFormWithExtendedInfoDTO>(item);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00018250 File Offset: 0x00016450
		public static IList<DynamicFormWithExtendedInfo> ToDomainObject(this IList<DynamicFormWithExtendedInfoDTO> list)
		{
			IList<DynamicFormWithExtendedInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<DynamicFormWithExtendedInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00018294 File Offset: 0x00016494
		public static IList<DynamicFormWithExtendedInfoDTO> ToDTO(this IList<DynamicFormWithExtendedInfo> list)
		{
			IList<DynamicFormWithExtendedInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<DynamicFormWithExtendedInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
