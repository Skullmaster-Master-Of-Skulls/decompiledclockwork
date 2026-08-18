using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000F6 RID: 246
	public static class InventoryArchivedLoanDTOMapper
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00013CB8 File Offset: 0x00011EB8
		static InventoryArchivedLoanDTOMapper()
		{
			InventoryLoanStatusDTOMapper.CreateMap();
			InventoryLoanGroupDTOMapper.CreateMap();
			InventoryProductSnapshotDTOMapper.CreateMap();
			Mapper.CreateMap<InventoryArchivedLoan, InventoryArchivedLoanDTO>();
			Mapper.CreateMap<InventoryArchivedLoanDTO, InventoryArchivedLoan>().ForMember((InventoryArchivedLoan bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryArchivedLoanDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00013D48 File Offset: 0x00011F48
		public static InventoryArchivedLoan ToDomainObject(this InventoryArchivedLoanDTO returnedLoanDTO)
		{
			return Mapper.Map<InventoryArchivedLoanDTO, InventoryArchivedLoan>(returnedLoanDTO);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00013D60 File Offset: 0x00011F60
		public static InventoryArchivedLoanDTO ToDTO(this InventoryArchivedLoan returnedLoan)
		{
			return Mapper.Map<InventoryArchivedLoan, InventoryArchivedLoanDTO>(returnedLoan);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00013D78 File Offset: 0x00011F78
		public static IList<InventoryArchivedLoan> ToDomainObject(this IList<InventoryArchivedLoanDTO> list)
		{
			IList<InventoryArchivedLoan> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryArchivedLoan>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00013DBC File Offset: 0x00011FBC
		public static IList<InventoryArchivedLoanDTO> ToDTO(this IList<InventoryArchivedLoan> list)
		{
			IList<InventoryArchivedLoanDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryArchivedLoanDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
