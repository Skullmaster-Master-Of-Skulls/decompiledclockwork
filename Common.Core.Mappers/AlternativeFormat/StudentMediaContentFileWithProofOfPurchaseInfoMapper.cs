using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000211 RID: 529
	public static class StudentMediaContentFileWithProofOfPurchaseInfoMapper
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x00026A38 File Offset: 0x00024C38
		static StudentMediaContentFileWithProofOfPurchaseInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			MediaContentMapper.CreateMap();
			MediaContentFileWithoutDataMapper.CreateMap();
			Mapper.CreateMap<StudentMediaContentFileWithProofOfPurchaseInfo, StudentMediaContentFileWithProofOfPurchaseInfoDTO>();
			Mapper.CreateMap<StudentMediaContentFileWithProofOfPurchaseInfoDTO, StudentMediaContentFileWithProofOfPurchaseInfo>().ForMember((StudentMediaContentFileWithProofOfPurchaseInfo mc) => (object)mc.Id, delegate(IMemberConfigurationExpression<StudentMediaContentFileWithProofOfPurchaseInfoDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00026AC8 File Offset: 0x00024CC8
		public static StudentMediaContentFileWithProofOfPurchaseInfo ToDomainObject(this StudentMediaContentFileWithProofOfPurchaseInfoDTO mediaContentFileDTO)
		{
			return Mapper.Map<StudentMediaContentFileWithProofOfPurchaseInfoDTO, StudentMediaContentFileWithProofOfPurchaseInfo>(mediaContentFileDTO);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00026AE0 File Offset: 0x00024CE0
		public static IList<StudentMediaContentFileWithProofOfPurchaseInfo> ToDomainObject(this IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> list)
		{
			IList<StudentMediaContentFileWithProofOfPurchaseInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<StudentMediaContentFileWithProofOfPurchaseInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00026B24 File Offset: 0x00024D24
		public static StudentMediaContentFileWithProofOfPurchaseInfoDTO ToDTO(this StudentMediaContentFileWithProofOfPurchaseInfo mediaContentFile)
		{
			return Mapper.Map<StudentMediaContentFileWithProofOfPurchaseInfo, StudentMediaContentFileWithProofOfPurchaseInfoDTO>(mediaContentFile);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00026B3C File Offset: 0x00024D3C
		public static IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> ToDTO(this IList<StudentMediaContentFileWithProofOfPurchaseInfo> list)
		{
			IList<StudentMediaContentFileWithProofOfPurchaseInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<StudentMediaContentFileWithProofOfPurchaseInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
