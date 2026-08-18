using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A0 RID: 160
	public static class BasicPersonMapper
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000EB10 File Offset: 0x0000CD10
		static BasicPersonMapper()
		{
			Mapper.CreateMap<BasicPerson, BasicPersonDTO>();
			Mapper.CreateMap<BasicPersonDTO, BasicPerson>().ForMember((BasicPerson pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<BasicPersonDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		public static BasicPerson ToDomainObject(this BasicPersonDTO groupDTO)
		{
			return Mapper.Map<BasicPersonDTO, BasicPerson>(groupDTO);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		public static BasicPersonDTO ToDTO(this BasicPerson group)
		{
			return Mapper.Map<BasicPerson, BasicPersonDTO>(group);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		public static IList<BasicPerson> ToDomainObject(this IList<BasicPersonDTO> dtos)
		{
			IList<BasicPerson> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<BasicPerson>();
			}
			return result;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000EC00 File Offset: 0x0000CE00
		public static IList<BasicPersonDTO> ToDTO(this IList<BasicPerson> items)
		{
			IList<BasicPersonDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<BasicPersonDTO>();
			}
			return result;
		}
	}
}
