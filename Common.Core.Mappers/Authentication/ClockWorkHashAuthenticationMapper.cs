using System;
using System.Linq.Expressions;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.Core.Mappers.Authentication
{
	// Token: 0x0200018F RID: 399
	public static class ClockWorkHashAuthenticationMapper
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x0001E8FC File Offset: 0x0001CAFC
		static ClockWorkHashAuthenticationMapper()
		{
			Mapper.CreateMap<ClockWorkHashAuthenticationDTO, ClockWorkHashAuthentication>().ForMember((ClockWorkHashAuthentication bo) => bo.Username, delegate(IMemberConfigurationExpression<ClockWorkHashAuthenticationDTO> m)
			{
				m.MapFrom<string>((ClockWorkHashAuthenticationDTO dto) => dto.Username);
			}).ForMember((ClockWorkHashAuthentication bo) => bo.HashValue, delegate(IMemberConfigurationExpression<ClockWorkHashAuthenticationDTO> m)
			{
				m.MapFrom<string>((ClockWorkHashAuthenticationDTO dto) => dto.HashValue);
			}).ForMember((ClockWorkHashAuthentication bo) => bo.Seed, delegate(IMemberConfigurationExpression<ClockWorkHashAuthenticationDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<string>(Expression.Lambda<Func<ClockWorkHashAuthenticationDTO, string>>(Expression.Call(Expression.Property(parameterExpression2, methodof(ClockWorkHashAuthenticationDTO.get_Seed())), methodof(int.ToString()), Array.Empty<Expression>()), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((ClockWorkHashAuthentication bo) => bo.StampTime, delegate(IMemberConfigurationExpression<ClockWorkHashAuthenticationDTO> m)
			{
				m.MapFrom<string>((ClockWorkHashAuthenticationDTO dto) => dto.StampTime.ToString("yyyy-MM-dd hh:mm:ss.fff"));
			});
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001EA48 File Offset: 0x0001CC48
		public static ClockWorkHashAuthentication ToDomainObject(this ClockWorkHashAuthenticationDTO dto)
		{
			return Mapper.Map<ClockWorkHashAuthenticationDTO, ClockWorkHashAuthentication>(dto);
		}
	}
}
