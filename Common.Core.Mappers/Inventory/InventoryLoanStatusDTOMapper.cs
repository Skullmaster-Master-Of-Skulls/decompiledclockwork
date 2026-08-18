using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000F8 RID: 248
	public static class InventoryLoanStatusDTOMapper
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x000143E0 File Offset: 0x000125E0
		static InventoryLoanStatusDTOMapper()
		{
			Mapper.CreateMap<InventoryLoanStatus, InventoryLoanStatusDTO>().ForMember((InventoryLoanStatusDTO dto) => (object)dto.LoanStatusId, delegate(IMemberConfigurationExpression<InventoryLoanStatus> m)
			{
				m.MapFrom<int>((InventoryLoanStatus bo) => bo.LoanStatusId);
			}).ForMember((InventoryLoanStatusDTO dto) => dto.Name, delegate(IMemberConfigurationExpression<InventoryLoanStatus> m)
			{
				m.MapFrom<string>((InventoryLoanStatus bo) => bo.Name);
			}).ForMember((InventoryLoanStatusDTO dto) => dto.Description, delegate(IMemberConfigurationExpression<InventoryLoanStatus> m)
			{
				m.MapFrom<string>((InventoryLoanStatus bo) => bo.Description);
			});
			Mapper.CreateMap<InventoryLoanStatusDTO, InventoryLoanStatus>().ForMember((InventoryLoanStatus bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryLoanStatusDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryLoanStatus bo) => (object)bo.LoanStatusId, delegate(IMemberConfigurationExpression<InventoryLoanStatusDTO> m)
			{
				m.MapFrom<int>((InventoryLoanStatusDTO dto) => dto.LoanStatusId);
			}).ForMember((InventoryLoanStatus bo) => bo.Name, delegate(IMemberConfigurationExpression<InventoryLoanStatusDTO> m)
			{
				m.MapFrom<string>((InventoryLoanStatusDTO dto) => dto.Name);
			}).ForMember((InventoryLoanStatus bo) => bo.Description, delegate(IMemberConfigurationExpression<InventoryLoanStatusDTO> m)
			{
				m.MapFrom<string>((InventoryLoanStatusDTO dto) => dto.Description);
			});
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00014650 File Offset: 0x00012850
		public static InventoryLoanStatusDTO ToDTO(this InventoryLoanStatus loanStatus)
		{
			return Mapper.Map<InventoryLoanStatus, InventoryLoanStatusDTO>(loanStatus);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00014668 File Offset: 0x00012868
		public static InventoryLoanStatus ToDomainObject(this InventoryLoanStatusDTO loanStatusDTO)
		{
			return Mapper.Map<InventoryLoanStatusDTO, InventoryLoanStatus>(loanStatusDTO);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00014680 File Offset: 0x00012880
		public static IList<InventoryLoanStatusDTO> ToDTO(this IList<InventoryLoanStatus> list)
		{
			IList<InventoryLoanStatusDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryLoanStatusDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000146C4 File Offset: 0x000128C4
		public static IList<InventoryLoanStatus> ToDomainObject(this IList<InventoryLoanStatusDTO> list)
		{
			IList<InventoryLoanStatus> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryLoanStatus>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
