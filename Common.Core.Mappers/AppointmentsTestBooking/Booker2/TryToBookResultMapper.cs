using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.Booker2
{
	// Token: 0x020001DE RID: 478
	public static class TryToBookResultMapper
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x00022ACC File Offset: 0x00020CCC
		static TryToBookResultMapper()
		{
			Mapper.CreateMap<TryToBookResultDTO, TryToBookResult>().ForMember((TryToBookResult pb) => pb.Warnings, delegate(IMemberConfigurationExpression<TryToBookResultDTO> m)
			{
				m.MapFrom<List<TryToBookWarning>>((TryToBookResultDTO pbdto) => (pbdto.Warnings == null) ? null : (from g in pbdto.Warnings
				select g.ToDomainObject()).ToList<TryToBookWarning>());
			}).ForMember((TryToBookResult pb) => pb.Failures, delegate(IMemberConfigurationExpression<TryToBookResultDTO> m)
			{
				m.MapFrom<List<TryToBookFailure>>((TryToBookResultDTO pbdto) => (pbdto.Failures == null) ? null : (from g in pbdto.Failures
				select g.ToDomainObject()).ToList<TryToBookFailure>());
			}).ForMember((TryToBookResult pb) => pb.DebuggingLogItems, delegate(IMemberConfigurationExpression<TryToBookResultDTO> m)
			{
				m.MapFrom<List<string>>((TryToBookResultDTO pbdto) => (pbdto.DebuggingLogItems == null) ? null : pbdto.DebuggingLogItems.ToList<string>());
			}).ForMember((TryToBookResult pb) => pb.PotentialBookings, delegate(IMemberConfigurationExpression<TryToBookResultDTO> m)
			{
				m.MapFrom<List<TryToBookPotentialBooking>>((TryToBookResultDTO pbdto) => (pbdto.PotentialBookings == null) ? null : (from g in pbdto.PotentialBookings
				select g.ToDomainObject()).ToList<TryToBookPotentialBooking>());
			});
			Mapper.CreateMap<TryToBookResult, TryToBookResultDTO>().ForMember((TryToBookResultDTO pb) => pb.Warnings, delegate(IMemberConfigurationExpression<TryToBookResult> m)
			{
				m.MapFrom<List<TryToBookWarningDTO>>((TryToBookResult pbdto) => (pbdto.Warnings == null) ? null : (from g in pbdto.Warnings
				select g.ToDTO()).ToList<TryToBookWarningDTO>());
			}).ForMember((TryToBookResultDTO pb) => pb.Failures, delegate(IMemberConfigurationExpression<TryToBookResult> m)
			{
				m.MapFrom<List<TryToBookFailureDTO>>((TryToBookResult pbdto) => (pbdto.Failures == null) ? null : (from g in pbdto.Failures
				select g.ToDTO()).ToList<TryToBookFailureDTO>());
			}).ForMember((TryToBookResultDTO pb) => pb.DebuggingLogItems, delegate(IMemberConfigurationExpression<TryToBookResult> m)
			{
				m.MapFrom<List<string>>((TryToBookResult pbdto) => (pbdto.DebuggingLogItems == null) ? null : pbdto.DebuggingLogItems.ToList<string>());
			}).ForMember((TryToBookResultDTO pb) => pb.PotentialBookings, delegate(IMemberConfigurationExpression<TryToBookResult> m)
			{
				m.MapFrom<List<TryToBookPotentialBookingDTO>>((TryToBookResult pbdto) => (pbdto.PotentialBookings == null) ? null : (from g in pbdto.PotentialBookings
				select g.ToDTO()).ToList<TryToBookPotentialBookingDTO>());
			});
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00022D58 File Offset: 0x00020F58
		public static TryToBookResult ToDomainObject(this TryToBookResultDTO accommodationForTestDTO)
		{
			return Mapper.Map<TryToBookResultDTO, TryToBookResult>(accommodationForTestDTO);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00022D70 File Offset: 0x00020F70
		public static TryToBookResultDTO ToDTO(this TryToBookResult accommodationForTest)
		{
			return Mapper.Map<TryToBookResult, TryToBookResultDTO>(accommodationForTest);
		}
	}
}
