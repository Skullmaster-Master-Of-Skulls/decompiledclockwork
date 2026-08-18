using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Department;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers.Departments
{
	// Token: 0x02000135 RID: 309
	public static class DepartmentDTOMapper
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x000197B4 File Offset: 0x000179B4
		static DepartmentDTOMapper()
		{
			Mapper.CreateMap<Department, DepartmentDTO>();
			Mapper.CreateMap<DepartmentDTO, Department>().ForMember((Department pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DepartmentDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00019830 File Offset: 0x00017A30
		public static Department ToDomainObject(this DepartmentDTO departmentDTO)
		{
			return Mapper.Map<DepartmentDTO, Department>(departmentDTO);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00019848 File Offset: 0x00017A48
		public static DepartmentDTO ToDTO(this Department department)
		{
			return Mapper.Map<Department, DepartmentDTO>(department);
		}
	}
}
