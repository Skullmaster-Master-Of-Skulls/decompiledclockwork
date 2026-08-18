using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001BE RID: 446
	public static class ClassTestBaseMapper
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x00020F34 File Offset: 0x0001F134
		static ClassTestBaseMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<ClassTestBaseDTO, ClassTestBase>().Include<ClassTestDTO, ClassTest>().ForMember((ClassTestBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ClassTestBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ClassTestBase, ClassTestBaseDTO>().Include<ClassTest, ClassTestDTO>();
			Mapper.CreateMap<ClassTestDTO, ClassTest>().ForMember((ClassTest pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ClassTestDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ClassTest, ClassTestDTO>();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00021030 File Offset: 0x0001F230
		public static ClassTestBase ToDomainObject(this ClassTestBaseDTO dto)
		{
			return Mapper.Map<ClassTestBaseDTO, ClassTestBase>(dto);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00021048 File Offset: 0x0001F248
		public static ClassTestBaseDTO ToDTO(this ClassTestBase item)
		{
			return Mapper.Map<ClassTestBase, ClassTestBaseDTO>(item);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00021060 File Offset: 0x0001F260
		public static ClassTest ToDomainObject(this ClassTestDTO dto)
		{
			Type type = dto.GetType();
			return (ClassTest)Mapper.Map(dto, type, typeof(ClassTest));
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00021090 File Offset: 0x0001F290
		public static ClassTestDTO ToDTO(this ClassTest item)
		{
			Type type = item.GetType();
			return (ClassTestDTO)Mapper.Map(item, type, typeof(ClassTestDTO));
		}
	}
}
