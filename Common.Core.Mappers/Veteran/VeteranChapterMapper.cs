using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Veteran;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.Common.Core.Mappers.Veteran
{
	// Token: 0x02000015 RID: 21
	public static class VeteranChapterMapper
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003E70 File Offset: 0x00002070
		static VeteranChapterMapper()
		{
			Mapper.CreateMap<VeteranChapterDTO, VeteranChapter>();
			Mapper.CreateMap<VeteranChapter, VeteranChapterDTO>();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003E80 File Offset: 0x00002080
		public static VeteranChapter ToDomainObject(this VeteranChapterDTO dto)
		{
			return Mapper.Map<VeteranChapterDTO, VeteranChapter>(dto);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003E98 File Offset: 0x00002098
		public static VeteranChapterDTO ToDTO(this VeteranChapter item)
		{
			return Mapper.Map<VeteranChapter, VeteranChapterDTO>(item);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003EB0 File Offset: 0x000020B0
		public static IList<VeteranChapter> ToDomainObject(this IList<VeteranChapterDTO> dtos)
		{
			IList<VeteranChapter> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from dto in dtos
				select Mapper.Map<VeteranChapterDTO, VeteranChapter>(dto)).ToList<VeteranChapter>();
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003EF4 File Offset: 0x000020F4
		public static IList<VeteranChapterDTO> ToDTO(this IList<VeteranChapter> items)
		{
			IList<VeteranChapterDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from item in items
				select Mapper.Map<VeteranChapter, VeteranChapterDTO>(item)).ToList<VeteranChapterDTO>();
			}
			return result;
		}
	}
}
