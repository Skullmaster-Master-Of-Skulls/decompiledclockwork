using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001E3 RID: 483
	public static class ApplySpecialAccommodationsRespMapper
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00022E88 File Offset: 0x00021088
		static ApplySpecialAccommodationsRespMapper()
		{
			TestMapper.CreateMap();
			PrivateNoteMapper.CreateMap();
			Mapper.CreateMap<ApplySpecialAccommodationsRespDTO, ApplySpecialAccommodationsResp>().ForMember((ApplySpecialAccommodationsResp pb) => pb.NewTestScheduledTimeAndRoom, delegate(IMemberConfigurationExpression<ApplySpecialAccommodationsRespDTO> m)
			{
				m.MapFrom<Test>((ApplySpecialAccommodationsRespDTO pbdto) => (pbdto.NewTestScheduledTimeAndRoom == null) ? null : pbdto.NewTestScheduledTimeAndRoom.ToDomainObject());
			}).ForMember((ApplySpecialAccommodationsResp pb) => pb.PrivateNotes, delegate(IMemberConfigurationExpression<ApplySpecialAccommodationsRespDTO> m)
			{
				m.MapFrom<List<PrivateNote>>((ApplySpecialAccommodationsRespDTO pbdto) => (pbdto.PrivateNotes == null) ? null : pbdto.PrivateNotes.ToList<PrivateNoteDTO>().ConvertAll<PrivateNote>((PrivateNoteDTO g) => g.ToDomainObject()));
			}).ForMember((ApplySpecialAccommodationsResp pb) => pb.IconsToBookWith, delegate(IMemberConfigurationExpression<ApplySpecialAccommodationsRespDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<IList<int>>(Expression.Lambda<Func<ApplySpecialAccommodationsRespDTO, IList<int>>>(Expression.Coalesce(Expression.Property(parameterExpression2, methodof(ApplySpecialAccommodationsRespDTO.get_IconsToBookWith())), Expression.New(typeof(List<int>))), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((ApplySpecialAccommodationsResp pb) => pb.EmailBodySb, delegate(IMemberConfigurationExpression<ApplySpecialAccommodationsRespDTO> m)
			{
				m.MapFrom<StringBuilder>((ApplySpecialAccommodationsRespDTO pbdto) => (pbdto.EmailBodySb == null) ? null : new StringBuilder(pbdto.EmailBodySb));
			});
			Mapper.CreateMap<ApplySpecialAccommodationsResp, ApplySpecialAccommodationsRespDTO>().ForMember((ApplySpecialAccommodationsRespDTO pb) => pb.NewTestScheduledTimeAndRoom, delegate(IMemberConfigurationExpression<ApplySpecialAccommodationsResp> m)
			{
				m.MapFrom<TestDTO>((ApplySpecialAccommodationsResp pbdto) => (pbdto.NewTestScheduledTimeAndRoom == null) ? null : pbdto.NewTestScheduledTimeAndRoom.ToDTO());
			}).ForMember((ApplySpecialAccommodationsRespDTO pb) => pb.PrivateNotes, delegate(IMemberConfigurationExpression<ApplySpecialAccommodationsResp> m)
			{
				m.MapFrom<List<PrivateNoteDTO>>((ApplySpecialAccommodationsResp pbdto) => (pbdto.PrivateNotes == null) ? null : pbdto.PrivateNotes.ToList<PrivateNote>().ConvertAll<PrivateNoteDTO>((PrivateNote g) => g.ToDTO()));
			}).ForMember((ApplySpecialAccommodationsRespDTO pb) => pb.IconsToBookWith, delegate(IMemberConfigurationExpression<ApplySpecialAccommodationsResp> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<IList<int>>(Expression.Lambda<Func<ApplySpecialAccommodationsResp, IList<int>>>(Expression.Coalesce(Expression.Property(parameterExpression2, methodof(ApplySpecialAccommodationsResp.get_IconsToBookWith())), Expression.New(typeof(List<int>))), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((ApplySpecialAccommodationsRespDTO pb) => pb.EmailBodySb, delegate(IMemberConfigurationExpression<ApplySpecialAccommodationsResp> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<string>(Expression.Lambda<Func<ApplySpecialAccommodationsResp, string>>(Expression.Condition(Expression.Equal(Expression.Property(parameterExpression2, methodof(ApplySpecialAccommodationsResp.get_EmailBodySb())), Expression.Constant(null, typeof(object))), Expression.Constant(null, typeof(string)), Expression.Call(Expression.Property(parameterExpression2, methodof(ApplySpecialAccommodationsResp.get_EmailBodySb())), methodof(object.ToString()), Array.Empty<Expression>())), new ParameterExpression[]
				{
					parameterExpression2
				}));
			});
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00023120 File Offset: 0x00021320
		public static ApplySpecialAccommodationsResp ToDomainObject(this ApplySpecialAccommodationsRespDTO accommodationForTestDTO)
		{
			return Mapper.Map<ApplySpecialAccommodationsRespDTO, ApplySpecialAccommodationsResp>(accommodationForTestDTO);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00023138 File Offset: 0x00021338
		public static ApplySpecialAccommodationsRespDTO ToDTO(this ApplySpecialAccommodationsResp accommodationForTest)
		{
			return Mapper.Map<ApplySpecialAccommodationsResp, ApplySpecialAccommodationsRespDTO>(accommodationForTest);
		}
	}
}
