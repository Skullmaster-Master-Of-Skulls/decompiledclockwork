using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeData;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeData;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeData
{
	// Token: 0x020000D2 RID: 210
	public static class MailMergeTestBookingMapper
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00011588 File Offset: 0x0000F788
		static MailMergeTestBookingMapper()
		{
			Mapper.CreateMap<MailMergeTestBooking, MailMergeTestBookingDTO>();
			Mapper.CreateMap<MailMergeTestBookingDTO, MailMergeTestBooking>().ForMember((MailMergeTestBooking pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<MailMergeTestBookingDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00011604 File Offset: 0x0000F804
		public static MailMergeTestBooking ToDomainObject(this MailMergeTestBookingDTO dto)
		{
			return Mapper.Map<MailMergeTestBookingDTO, MailMergeTestBooking>(dto);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001161C File Offset: 0x0000F81C
		public static MailMergeTestBookingDTO ToDTO(this MailMergeTestBooking item)
		{
			return Mapper.Map<MailMergeTestBooking, MailMergeTestBookingDTO>(item);
		}
	}
}
