using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Mappers.Inventory
{
	// Token: 0x020000F4 RID: 244
	public static class InventoryLoanDTOMapper
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x000135B0 File Offset: 0x000117B0
		static InventoryLoanDTOMapper()
		{
			InventoryProductDTOMapper.CreateMap();
			InventoryLoanGroupDTOMapper.CreateMap();
			Mapper.CreateMap<InventoryLoan, InventoryLoanDTO>().ForMember((InventoryLoanDTO dto) => (object)dto.LoanId, delegate(IMemberConfigurationExpression<InventoryLoan> m)
			{
				m.MapFrom<int>((InventoryLoan bo) => bo.LoanId);
			}).ForMember((InventoryLoanDTO dto) => dto.LoanedProduct, delegate(IMemberConfigurationExpression<InventoryLoan> m)
			{
				m.MapFrom<InventoryProductDTO>((InventoryLoan bo) => bo.LoanedProduct.ToDTO());
			}).ForMember((InventoryLoanDTO dto) => dto.Group, delegate(IMemberConfigurationExpression<InventoryLoan> m)
			{
				m.MapFrom<InventoryLoanGroupDTO>((InventoryLoan bo) => bo.Group.ToDTO());
			});
			Mapper.CreateMap<InventoryLoanDTO, InventoryLoan>().ForMember((InventoryLoan bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<InventoryLoanDTO> m)
			{
				m.Ignore();
			}).ForMember((InventoryLoan bo) => (object)bo.LoanId, delegate(IMemberConfigurationExpression<InventoryLoanDTO> m)
			{
				m.MapFrom<int>((InventoryLoanDTO dto) => dto.LoanId);
			}).ForMember((InventoryLoan bo) => bo.LoanedProduct, delegate(IMemberConfigurationExpression<InventoryLoanDTO> m)
			{
				m.MapFrom<InventoryProduct>((InventoryLoanDTO dto) => dto.LoanedProduct.ToDomainObject());
			}).ForMember((InventoryLoan bo) => bo.Group, delegate(IMemberConfigurationExpression<InventoryLoanDTO> m)
			{
				m.MapFrom<InventoryLoanGroup>((InventoryLoanDTO dto) => dto.Group.ToDomainObject());
			});
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001382C File Offset: 0x00011A2C
		public static InventoryLoan ToDomainObject(this InventoryLoanDTO loanDTO)
		{
			return Mapper.Map<InventoryLoanDTO, InventoryLoan>(loanDTO);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00013844 File Offset: 0x00011A44
		public static InventoryLoanDTO ToDTO(this InventoryLoan loan)
		{
			return Mapper.Map<InventoryLoan, InventoryLoanDTO>(loan);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001385C File Offset: 0x00011A5C
		public static IList<InventoryLoan> ToDomainObject(this IList<InventoryLoanDTO> list)
		{
			IList<InventoryLoan> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<InventoryLoan>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000138A0 File Offset: 0x00011AA0
		public static IList<InventoryLoanDTO> ToDTO(this IList<InventoryLoan> list)
		{
			IList<InventoryLoanDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InventoryLoanDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
