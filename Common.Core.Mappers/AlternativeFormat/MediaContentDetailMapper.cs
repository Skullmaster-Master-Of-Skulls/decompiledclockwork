using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x0200020F RID: 527
	public static class MediaContentDetailMapper
	{
		// Token: 0x060008E2 RID: 2274 RVA: 0x00026378 File Offset: 0x00024578
		static MediaContentDetailMapper()
		{
			BasicMediaContentMapper.CreateMap();
			Mapper.CreateMap<MediaContentDetail, MediaContentDetailDTO>().ForMember((MediaContentDetailDTO dto) => dto.Id, delegate(IMemberConfigurationExpression<MediaContentDetail> m)
			{
				m.MapFrom<MediaContentIdentifier>((MediaContentDetail bo) => bo.Id);
			}).ForMember((MediaContentDetailDTO dto) => dto.MediaContent, delegate(IMemberConfigurationExpression<MediaContentDetail> m)
			{
				m.MapFrom<BasicMediaContentDTO>((MediaContentDetail bo) => bo.MediaContent.ToDTO());
			}).ForMember((MediaContentDetailDTO dto) => (object)dto.MediaContentFormat, delegate(IMemberConfigurationExpression<MediaContentDetail> m)
			{
				m.MapFrom<MediaContentFormat>((MediaContentDetail bo) => bo.MediaContentFormat);
			}).ForMember((MediaContentDetailDTO dto) => (object)dto.StudentPreferredFormat, delegate(IMemberConfigurationExpression<MediaContentDetail> m)
			{
				m.MapFrom<MediaContentFormat?>((MediaContentDetail bo) => bo.StudentPreferredFormat);
			}).ForMember((MediaContentDetailDTO dto) => (object)dto.MediaContentPerFormatId, delegate(IMemberConfigurationExpression<MediaContentDetail> m)
			{
				m.MapFrom<int>((MediaContentDetail bo) => bo.MediaContentPerFormatId);
			});
			Mapper.CreateMap<MediaContentDetailDTO, MediaContentDetail>().ForMember((MediaContentDetail bo) => bo.Id, delegate(IMemberConfigurationExpression<MediaContentDetailDTO> m)
			{
				m.MapFrom<MediaContentIdentifierDTO>((MediaContentDetailDTO dto) => dto.Id);
			}).ForMember((MediaContentDetail bo) => bo.MediaContent, delegate(IMemberConfigurationExpression<MediaContentDetailDTO> m)
			{
				m.MapFrom<BasicMediaContent>((MediaContentDetailDTO dto) => dto.MediaContent.ToDomainObject());
			}).ForMember((MediaContentDetail bo) => (object)bo.MediaContentFormat, delegate(IMemberConfigurationExpression<MediaContentDetailDTO> m)
			{
				m.MapFrom<MediaContentFormat>((MediaContentDetailDTO dto) => dto.MediaContentFormat);
			}).ForMember((MediaContentDetail bo) => (object)bo.StudentPreferredFormat, delegate(IMemberConfigurationExpression<MediaContentDetailDTO> m)
			{
				m.MapFrom<MediaContentFormat?>((MediaContentDetailDTO dto) => dto.StudentPreferredFormat);
			}).ForMember((MediaContentDetail bo) => (object)bo.MediaContentPerFormatId, delegate(IMemberConfigurationExpression<MediaContentDetailDTO> m)
			{
				m.MapFrom<int>((MediaContentDetailDTO dto) => dto.MediaContentPerFormatId);
			});
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00026708 File Offset: 0x00024908
		public static MediaContentDetail ToDomainObject(this MediaContentDetailDTO mediaContentDetailDTO)
		{
			return Mapper.Map<MediaContentDetailDTO, MediaContentDetail>(mediaContentDetailDTO);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00026720 File Offset: 0x00024920
		public static IList<MediaContentDetail> ToDomainObject(this IList<MediaContentDetailDTO> list)
		{
			IList<MediaContentDetail> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaContentDetail>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00026764 File Offset: 0x00024964
		public static MediaContentDetailDTO ToDTO(this MediaContentDetail mediaContentDetail)
		{
			return Mapper.Map<MediaContentDetail, MediaContentDetailDTO>(mediaContentDetail);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002677C File Offset: 0x0002497C
		public static IList<MediaContentDetailDTO> ToDTO(this IList<MediaContentDetail> list)
		{
			IList<MediaContentDetailDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaContentDetailDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
