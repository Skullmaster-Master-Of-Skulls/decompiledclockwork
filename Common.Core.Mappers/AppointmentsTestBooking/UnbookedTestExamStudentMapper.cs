using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001CE RID: 462
	public static class UnbookedTestExamStudentMapper
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x000220CC File Offset: 0x000202CC
		static UnbookedTestExamStudentMapper()
		{
			PersonBaseMapper.CreateMap();
			ClassTestBaseMapper.CreateMap();
			Mapper.CreateMap<UnbookedTestExamStudentDTO, UnbookedTestExamStudent>().ForMember((UnbookedTestExamStudent pb) => pb.Id, delegate(IMemberConfigurationExpression<UnbookedTestExamStudentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<UnbookedTestExamStudent, UnbookedTestExamStudentDTO>();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00022148 File Offset: 0x00020348
		public static UnbookedTestExamStudent ToDomainObject(this UnbookedTestExamStudentDTO dto)
		{
			return Mapper.Map<UnbookedTestExamStudentDTO, UnbookedTestExamStudent>(dto);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00022160 File Offset: 0x00020360
		public static UnbookedTestExamStudentDTO ToDTO(this UnbookedTestExamStudent item)
		{
			return Mapper.Map<UnbookedTestExamStudent, UnbookedTestExamStudentDTO>(item);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00022178 File Offset: 0x00020378
		public static IList<UnbookedTestExamStudent> ToDomainObject(this IList<UnbookedTestExamStudentDTO> dtos)
		{
			IList<UnbookedTestExamStudent> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from dto in dtos
				select Mapper.Map<UnbookedTestExamStudentDTO, UnbookedTestExamStudent>(dto)).ToList<UnbookedTestExamStudent>();
			}
			return result;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000221BC File Offset: 0x000203BC
		public static IList<UnbookedTestExamStudentDTO> ToDTO(this IList<UnbookedTestExamStudent> items)
		{
			IList<UnbookedTestExamStudentDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from item in items
				select Mapper.Map<UnbookedTestExamStudent, UnbookedTestExamStudentDTO>(item)).ToList<UnbookedTestExamStudentDTO>();
			}
			return result;
		}
	}
}
