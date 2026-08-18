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
	// Token: 0x020000F7 RID: 247
	public static class InventoryLoanGroupDTOMapper
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x00013E00 File Offset: 0x00012000
		static InventoryLoanGroupDTOMapper()
		{
			PersonBaseMapper.CreateMap();
			InventoryLocationDTOMapper.CreateMap();
			Mapper.CreateMap<InventoryLoanGroup, InventoryLoanGroupDTO>().ForMember((InventoryLoanGroupDTO dto) => (object)dto.LoanGroupId, delegate(IMemberConfigurationExpression<InventoryLoanGroup> m)
			{
				m.MapFrom<int>((InventoryLoanGroup bo) => bo.LoanGroupId);
			}).ForMember((InventoryLoanGroupDTO dto) => (object)dto.LoanedDate, delegate(IMemberConfigurationExpression<InventoryLoanGroup> m)
			{
				m.MapFrom<DateTime>((InventoryLoanGroup bo) => bo.LoanedDate);
			}).ForMember((InventoryLoanGroupDTO dto) => (object)dto.DueDate, delegate(IMemberConfigurationExpression<InventoryLoanGroup> m)
			{
				m.MapFrom<DateTime>((InventoryLoanGroup bo) => bo.DueDate);
			}).ForMember((InventoryLoanGroupDTO dto) => dto.LoanNotes, delegate(IMemberConfigurationExpression<InventoryLoanGroup> m)
			{
				m.MapFrom<string>((InventoryLoanGroup bo) => bo.LoanNotes);
			}).ForMember((InventoryLoanGroupDTO dto) => dto.LoanedTo, delegate(IMemberConfigurationExpression<InventoryLoanGroup> m)
			{
				m.MapFrom<PersonBaseDTO>((InventoryLoanGroup bo) => bo.LoanedTo.ToDTO());
			}).ForMember((InventoryLoanGroupDTO dto) => dto.WhoLoaned, delegate(IMemberConfigurationExpression<InventoryLoanGroup> m)
			{
				m.MapFrom<PersonBaseDTO>((InventoryLoanGroup bo) => bo.WhoLoaned.ToDTO());
			}).ForMember((InventoryLoanGroupDTO dto) => dto.Location, delegate(IMemberConfigurationExpression<InventoryLoanGroup> m)
			{
				m.MapFrom<InventoryLocationDTO>((InventoryLoanGroup bo) => bo.Location.ToDTO());
			});
			Mapper.CreateMap<InventoryLoanGroupDTO, InventoryLoanGroup>().ForMember((InventoryLoanGroup bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryLoanGroupDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryLoanGroup bo) => (object)bo.LoanGroupId, delegate(IMemberConfigurationExpression<InventoryLoanGroupDTO> m)
			{
				m.MapFrom<int>((InventoryLoanGroupDTO dto) => dto.LoanGroupId);
			}).ForMember((InventoryLoanGroup bo) => (object)bo.LoanedDate, delegate(IMemberConfigurationExpression<InventoryLoanGroupDTO> m)
			{
				m.MapFrom<DateTime>((InventoryLoanGroupDTO dto) => dto.LoanedDate);
			}).ForMember((InventoryLoanGroup bo) => (object)bo.DueDate, delegate(IMemberConfigurationExpression<InventoryLoanGroupDTO> m)
			{
				m.MapFrom<DateTime>((InventoryLoanGroupDTO dto) => dto.DueDate);
			}).ForMember((InventoryLoanGroup bo) => bo.LoanNotes, delegate(IMemberConfigurationExpression<InventoryLoanGroupDTO> m)
			{
				m.MapFrom<string>((InventoryLoanGroupDTO dto) => dto.LoanNotes);
			}).ForMember((InventoryLoanGroup bo) => bo.LoanedTo, delegate(IMemberConfigurationExpression<InventoryLoanGroupDTO> m)
			{
				m.MapFrom<PersonBase>((InventoryLoanGroupDTO dto) => dto.LoanedTo.ToDomainObject());
			}).ForMember((InventoryLoanGroup bo) => bo.WhoLoaned, delegate(IMemberConfigurationExpression<InventoryLoanGroupDTO> m)
			{
				m.MapFrom<PersonBase>((InventoryLoanGroupDTO dto) => dto.WhoLoaned.ToDomainObject());
			}).ForMember((InventoryLoanGroup bo) => bo.Location, delegate(IMemberConfigurationExpression<InventoryLoanGroupDTO> m)
			{
				m.MapFrom<InventoryLocation>((InventoryLoanGroupDTO dto) => dto.Location.ToDomainObject());
			});
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00014328 File Offset: 0x00012528
		public static InventoryLoanGroup ToDomainObject(this InventoryLoanGroupDTO loanGroupDTO)
		{
			return Mapper.Map<InventoryLoanGroupDTO, InventoryLoanGroup>(loanGroupDTO);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00014340 File Offset: 0x00012540
		public static InventoryLoanGroupDTO ToDTO(this InventoryLoanGroup loanGroup)
		{
			return Mapper.Map<InventoryLoanGroup, InventoryLoanGroupDTO>(loanGroup);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00014358 File Offset: 0x00012558
		public static IList<InventoryLoanGroup> ToDomainObject(this IList<InventoryLoanGroupDTO> list)
		{
			IList<InventoryLoanGroup> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryLoanGroup>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001439C File Offset: 0x0001259C
		public static IList<InventoryLoanGroupDTO> ToDTO(this IList<InventoryLoanGroup> list)
		{
			IList<InventoryLoanGroupDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryLoanGroupDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
