using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000222 RID: 546
	public static class MediaVendorMapper
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x00029A30 File Offset: 0x00027C30
		static MediaVendorMapper()
		{
			Mapper.CreateMap<MediaVendor, MediaVendorDTO>();
			Mapper.CreateMap<MediaVendorDTO, MediaVendor>().ForMember((MediaVendor bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaVendorDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00029AAC File Offset: 0x00027CAC
		public static MediaVendor ToDomainObject(this MediaVendorDTO mediaVendorDTO)
		{
			return Mapper.Map<MediaVendorDTO, MediaVendor>(mediaVendorDTO);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00029AC4 File Offset: 0x00027CC4
		public static IList<MediaVendor> ToDomainObject(this IList<MediaVendorDTO> list)
		{
			IList<MediaVendor> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<MediaVendor>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00029B08 File Offset: 0x00027D08
		public static MediaVendorDTO ToDTO(this MediaVendor mediaJobRunningNote)
		{
			return Mapper.Map<MediaVendor, MediaVendorDTO>(mediaJobRunningNote);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00029B20 File Offset: 0x00027D20
		public static IList<MediaVendorDTO> ToDTO(this IList<MediaVendor> list)
		{
			IList<MediaVendorDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<MediaVendorDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
