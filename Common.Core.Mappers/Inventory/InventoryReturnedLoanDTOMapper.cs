using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000F5 RID: 245
	public static class InventoryReturnedLoanDTOMapper
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x000138E4 File Offset: 0x00011AE4
		static InventoryReturnedLoanDTOMapper()
		{
			PersonBaseMapper.CreateMap();
			InventoryLoanDTOMapper.CreateMap();
			InventoryLoanStatusDTOMapper.CreateMap();
			Mapper.CreateMap<InventoryReturnedLoan, InventoryReturnedLoanDTO>().ForMember((InventoryReturnedLoanDTO dto) => dto.WhoReturned, delegate(IMemberConfigurationExpression<InventoryReturnedLoan> m)
			{
				m.MapFrom<PersonBaseDTO>((InventoryReturnedLoan bo) => bo.WhoReturned.ToDTO());
			}).ForMember((InventoryReturnedLoanDTO dto) => dto.ReturnedNotes, delegate(IMemberConfigurationExpression<InventoryReturnedLoan> m)
			{
				m.MapFrom<string>((InventoryReturnedLoan bo) => bo.ReturnedNotes);
			}).ForMember((InventoryReturnedLoanDTO dto) => (object)dto.ReturnedDate, delegate(IMemberConfigurationExpression<InventoryReturnedLoan> m)
			{
				m.MapFrom<DateTime>((InventoryReturnedLoan bo) => bo.ReturnedDate);
			}).ForMember((InventoryReturnedLoanDTO dto) => dto.ReturnedStatus, delegate(IMemberConfigurationExpression<InventoryReturnedLoan> m)
			{
				m.MapFrom<InventoryLoanStatusDTO>((InventoryReturnedLoan bo) => bo.ReturnedStatus.ToDTO());
			});
			Mapper.CreateMap<InventoryReturnedLoanDTO, InventoryReturnedLoan>().ForMember((InventoryReturnedLoan bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryReturnedLoanDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryReturnedLoan bo) => bo.WhoReturned, delegate(IMemberConfigurationExpression<InventoryReturnedLoanDTO> m)
			{
				m.MapFrom<PersonBase>((InventoryReturnedLoanDTO dto) => dto.WhoReturned.ToDomainObject());
			}).ForMember((InventoryReturnedLoan bo) => bo.ReturnedNotes, delegate(IMemberConfigurationExpression<InventoryReturnedLoanDTO> m)
			{
				m.MapFrom<string>((InventoryReturnedLoanDTO dto) => dto.ReturnedNotes);
			}).ForMember((InventoryReturnedLoan bo) => (object)bo.ReturnedDate, delegate(IMemberConfigurationExpression<InventoryReturnedLoanDTO> m)
			{
				m.MapFrom<DateTime>((InventoryReturnedLoanDTO dto) => dto.ReturnedDate);
			}).ForMember((InventoryReturnedLoan bo) => bo.ReturnedStatus, delegate(IMemberConfigurationExpression<InventoryReturnedLoanDTO> m)
			{
				m.MapFrom<InventoryLoanStatus>((InventoryReturnedLoanDTO dto) => dto.ReturnedStatus.ToDomainObject());
			});
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00013C00 File Offset: 0x00011E00
		public static InventoryReturnedLoan ToDomainObject(this InventoryReturnedLoanDTO returnedLoanDTO)
		{
			return Mapper.Map<InventoryReturnedLoanDTO, InventoryReturnedLoan>(returnedLoanDTO);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00013C18 File Offset: 0x00011E18
		public static InventoryReturnedLoanDTO ToDTO(this InventoryReturnedLoan returnedLoan)
		{
			return Mapper.Map<InventoryReturnedLoan, InventoryReturnedLoanDTO>(returnedLoan);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00013C30 File Offset: 0x00011E30
		public static IList<InventoryReturnedLoan> ToDomainObject(this IList<InventoryReturnedLoanDTO> list)
		{
			IList<InventoryReturnedLoan> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryReturnedLoan>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00013C74 File Offset: 0x00011E74
		public static IList<InventoryReturnedLoanDTO> ToDTO(this IList<InventoryReturnedLoan> list)
		{
			IList<InventoryReturnedLoanDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryReturnedLoanDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
