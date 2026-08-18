using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context;
using TechnoPro.Common.Public.Entities.CustomForms.Data.Context;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Data.Context
{
	// Token: 0x02000159 RID: 345
	public static class CustomDataContextMapper
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x0001B51C File Offset: 0x0001971C
		static CustomDataContextMapper()
		{
			CustomDataPerDateContextMapper.CreateMap();
			CustomDataPerSemesterContextMapper.CreateMap();
			CustomDataPerStudentContextMapper.CreateMap();
			Mapper.CreateMap<CustomDataContext, CustomDataContextDTO>().Include<CustomDataPerDateContext, CustomDataPerDateContextDTO>().Include<CustomDataPerSemesterContext, CustomDataPerSemesterContextDTO>().Include<CustomDataPerStudentContext, CustomDataPerStudentContextDTO>();
			Mapper.CreateMap<CustomDataContextDTO, CustomDataContext>().Include<CustomDataPerDateContextDTO, CustomDataPerDateContext>().Include<CustomDataPerSemesterContextDTO, CustomDataPerSemesterContext>().Include<CustomDataPerStudentContextDTO, CustomDataPerStudentContext>();
			CustomDataContext[] array = new CustomDataContext[]
			{
				new CustomDataPerDateContext(),
				new CustomDataPerSemesterContext(),
				new CustomDataPerStudentContext()
			};
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001B588 File Offset: 0x00019788
		public static CustomDataContext ToDomainObject(this CustomDataContextDTO dto)
		{
			return Mapper.Map<CustomDataContextDTO, CustomDataContext>(dto);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001B5A0 File Offset: 0x000197A0
		public static CustomDataContextDTO ToDTO(this CustomDataContext item)
		{
			return Mapper.Map<CustomDataContext, CustomDataContextDTO>(item);
		}
	}
}
