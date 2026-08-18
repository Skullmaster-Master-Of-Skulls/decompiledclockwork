using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000210 RID: 528
	public static class MediaContentFileWithoutDataMapper
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x000267C0 File Offset: 0x000249C0
		static MediaContentFileWithoutDataMapper()
		{
			PersonBaseMapper.CreateMap();
			MediaContentMapper.CreateMap();
			Mapper.CreateMap<MediaContentFileWithoutData, MediaContentFileWithoutDataDTO>();
			Mapper.CreateMap<MediaContentFileWithoutDataDTO, MediaContentFileWithoutData>().ForMember((MediaContentFileWithoutData mc) => (object)mc.Id, delegate(IMemberConfigurationExpression<MediaContentFileWithoutDataDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<MediaContentFileWithoutData, StudentMediaContentFileTrackingInfo>().ForMember((StudentMediaContentFileTrackingInfo o) => (object)o.StudentMediaContentFileId, delegate(IMemberConfigurationExpression<MediaContentFileWithoutData> m)
			{
				m.Ignore();
			}).ForMember((StudentMediaContentFileTrackingInfo o) => (object)o.StudentPersonId, delegate(IMemberConfigurationExpression<MediaContentFileWithoutData> m)
			{
				m.Ignore();
			}).ForMember((StudentMediaContentFileTrackingInfo o) => (object)o.FileDownloadTime, delegate(IMemberConfigurationExpression<MediaContentFileWithoutData> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00026968 File Offset: 0x00024B68
		public static MediaContentFileWithoutData ToDomainObject(this MediaContentFileWithoutDataDTO mediaContentFileDTO)
		{
			return Mapper.Map<MediaContentFileWithoutDataDTO, MediaContentFileWithoutData>(mediaContentFileDTO);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00026980 File Offset: 0x00024B80
		public static IList<MediaContentFileWithoutData> ToDomainObject(this IList<MediaContentFileWithoutDataDTO> list)
		{
			IList<MediaContentFileWithoutData> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaContentFileWithoutData>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000269C4 File Offset: 0x00024BC4
		public static MediaContentFileWithoutDataDTO ToDTO(this MediaContentFileWithoutData mediaContentFile)
		{
			return Mapper.Map<MediaContentFileWithoutData, MediaContentFileWithoutDataDTO>(mediaContentFile);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000269DC File Offset: 0x00024BDC
		public static IList<MediaContentFileWithoutDataDTO> ToDTO(this IList<MediaContentFileWithoutData> list)
		{
			IList<MediaContentFileWithoutDataDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaContentFileWithoutDataDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00026A20 File Offset: 0x00024C20
		public static StudentMediaContentFileTrackingInfo ToStudentFileTrackingInfo(this MediaContentFileWithoutData file)
		{
			return Mapper.Map<MediaContentFileWithoutData, StudentMediaContentFileTrackingInfo>(file);
		}
	}
}
