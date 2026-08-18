using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;

namespace TechnoPro.Common.Core.Mappers.MergeDuplicatesStudents
{
	// Token: 0x020000BE RID: 190
	public static class PotentialDuplicateStudentSetMapper
	{
		// Token: 0x06000328 RID: 808 RVA: 0x000107D4 File Offset: 0x0000E9D4
		static PotentialDuplicateStudentSetMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<PotentialDuplicateStudentSetDTO, PotentialDuplicateStudentSet>();
			Mapper.CreateMap<PotentialDuplicateStudentSet, PotentialDuplicateStudentSetDTO>();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000107EC File Offset: 0x0000E9EC
		public static PotentialDuplicateStudentSet ToDomainObject(this PotentialDuplicateStudentSetDTO dto)
		{
			return Mapper.Map<PotentialDuplicateStudentSetDTO, PotentialDuplicateStudentSet>(dto);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00010804 File Offset: 0x0000EA04
		public static PotentialDuplicateStudentSetDTO ToDTO(this PotentialDuplicateStudentSet item)
		{
			return Mapper.Map<PotentialDuplicateStudentSet, PotentialDuplicateStudentSetDTO>(item);
		}
	}
}
