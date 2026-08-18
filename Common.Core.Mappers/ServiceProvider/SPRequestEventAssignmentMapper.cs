using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000073 RID: 115
	public static class SPRequestEventAssignmentMapper
	{
		// Token: 0x060001EA RID: 490 RVA: 0x0000BC6C File Offset: 0x00009E6C
		static SPRequestEventAssignmentMapper()
		{
			SPProviderMapper.CreateMap();
			Mapper.CreateMap<SPRequestEventAssignment, SPRequestEventAssignmentDTO>();
			Mapper.CreateMap<SPRequestEventAssignmentDTO, SPRequestEventAssignment>().ForMember((SPRequestEventAssignment pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPRequestEventAssignmentDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		public static SPRequestEventAssignment ToDomainObject(this SPRequestEventAssignmentDTO dto)
		{
			return Mapper.Map<SPRequestEventAssignmentDTO, SPRequestEventAssignment>(dto);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000BD08 File Offset: 0x00009F08
		public static SPRequestEventAssignmentDTO ToDTO(this SPRequestEventAssignment item)
		{
			return Mapper.Map<SPRequestEventAssignment, SPRequestEventAssignmentDTO>(item);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000BD20 File Offset: 0x00009F20
		public static IList<SPRequestEventAssignment> ToDomainObject(this IList<SPRequestEventAssignmentDTO> list)
		{
			IList<SPRequestEventAssignment> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPRequestEventAssignment>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000BD64 File Offset: 0x00009F64
		public static IList<SPRequestEventAssignmentDTO> ToDTO(this IList<SPRequestEventAssignment> list)
		{
			IList<SPRequestEventAssignmentDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPRequestEventAssignmentDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
