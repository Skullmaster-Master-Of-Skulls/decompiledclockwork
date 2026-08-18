using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.Common.Core.Mappers.ClockWorkServerJob
{
	// Token: 0x02000169 RID: 361
	public static class ClockWorkServerJobInfoMapper
	{
		// Token: 0x06000635 RID: 1589 RVA: 0x0001C600 File Offset: 0x0001A800
		static ClockWorkServerJobInfoMapper()
		{
			ClockWorkServerJobInfoCredentialsMapper.CreateMap();
			ClockWorkServerJobScheduleMapper.CreateMap();
			Mapper.CreateMap<ClockWorkServerJobInfo, ClockWorkServerJobInfoDTO>().ForMember((ClockWorkServerJobInfoDTO dto) => dto.JobSchedule, delegate(IMemberConfigurationExpression<ClockWorkServerJobInfo> m)
			{
				m.MapFrom<ClockWorkServerJobScheduleDTO>((ClockWorkServerJobInfo bo) => (bo.JobSchedule != null) ? bo.JobSchedule.ToDTO() : null);
			}).ForMember((ClockWorkServerJobInfoDTO dto) => dto.Impersonate, delegate(IMemberConfigurationExpression<ClockWorkServerJobInfo> m)
			{
				m.MapFrom<ClockWorkServerJobInfoDTO.CredentialsDTO>((ClockWorkServerJobInfo bo) => (bo.Impersonate != null) ? bo.Impersonate.ToDTO() : null);
			}).ForMember((ClockWorkServerJobInfoDTO dto) => dto.JobSteps, delegate(IMemberConfigurationExpression<ClockWorkServerJobInfo> m)
			{
				m.MapFrom<List<ClockWorkServerJobStepDTO>>((ClockWorkServerJobInfo bo) => (bo.JobSteps != null) ? bo.JobSteps.ToList<ClockWorkServerJobStep>().ConvertAll<ClockWorkServerJobStepDTO>((ClockWorkServerJobStep s) => s.ToDTO()) : null);
			});
			Mapper.CreateMap<ClockWorkServerJobInfoDTO, ClockWorkServerJobInfo>().ForMember((ClockWorkServerJobInfo bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<ClockWorkServerJobInfoDTO> m)
			{
				m.Ignore();
			}).ForMember((ClockWorkServerJobInfo bo) => bo.JobSchedule, delegate(IMemberConfigurationExpression<ClockWorkServerJobInfoDTO> m)
			{
				m.MapFrom<ClockWorkServerJobSchedule>((ClockWorkServerJobInfoDTO dto) => (dto.JobSchedule != null) ? dto.JobSchedule.ToDomainObject() : null);
			}).ForMember((ClockWorkServerJobInfo dto) => dto.Impersonate, delegate(IMemberConfigurationExpression<ClockWorkServerJobInfoDTO> m)
			{
				m.MapFrom<ClockWorkServerJobInfo.Credentials>((ClockWorkServerJobInfoDTO bo) => (bo.Impersonate != null) ? bo.Impersonate.ToDomainObject() : null);
			}).ForMember((ClockWorkServerJobInfo bo) => bo.JobSteps, delegate(IMemberConfigurationExpression<ClockWorkServerJobInfoDTO> m)
			{
				m.MapFrom<List<ClockWorkServerJobStep>>((ClockWorkServerJobInfoDTO dto) => (dto.JobSteps != null) ? dto.JobSteps.ToList<ClockWorkServerJobStepDTO>().ConvertAll<ClockWorkServerJobStep>((ClockWorkServerJobStepDTO s) => s.ToDomainObject()) : null);
			});
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001C85C File Offset: 0x0001AA5C
		public static ClockWorkServerJobInfo ToDomainObject(this ClockWorkServerJobInfoDTO dto)
		{
			return Mapper.Map<ClockWorkServerJobInfoDTO, ClockWorkServerJobInfo>(dto);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001C874 File Offset: 0x0001AA74
		public static ClockWorkServerJobInfoDTO ToDTO(this ClockWorkServerJobInfo bo)
		{
			return Mapper.Map<ClockWorkServerJobInfo, ClockWorkServerJobInfoDTO>(bo);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001C88C File Offset: 0x0001AA8C
		public static IList<ClockWorkServerJobInfo> ToDomainObject(this IList<ClockWorkServerJobInfoDTO> list)
		{
			IList<ClockWorkServerJobInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<ClockWorkServerJobInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001C8D0 File Offset: 0x0001AAD0
		public static IList<ClockWorkServerJobInfoDTO> ToDTO(this IList<ClockWorkServerJobInfo> list)
		{
			IList<ClockWorkServerJobInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<ClockWorkServerJobInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
