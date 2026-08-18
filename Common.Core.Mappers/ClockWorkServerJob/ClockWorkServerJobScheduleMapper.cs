using System;
using System.Collections.Generic;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.Common.Core.Mappers.ClockWorkServerJob
{
	// Token: 0x0200016D RID: 365
	public static class ClockWorkServerJobScheduleMapper
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x0001CBBC File Offset: 0x0001ADBC
		static ClockWorkServerJobScheduleMapper()
		{
			Mapper.CreateMap<ClockWorkServerJobSchedule, ClockWorkServerJobScheduleDTO>().Include<ClockWorkServerJobMonthlySchedule, ClockWorkServerJobMonthlyScheduleDTO>().Include<ClockWorkServerJobWeeklySchedule, ClockWorkServerJobWeeklyScheduleDTO>().Include<ClockWorkServerJobDailySchedule, ClockWorkServerJobDailyScheduleDTO>();
			Mapper.CreateMap<ClockWorkServerJobMonthlySchedule, ClockWorkServerJobMonthlyScheduleDTO>().ForMember((ClockWorkServerJobMonthlyScheduleDTO dto) => dto.DaysOfMonth, delegate(IMemberConfigurationExpression<ClockWorkServerJobMonthlySchedule> m)
			{
				m.MapFrom<IList<int>>((ClockWorkServerJobMonthlySchedule bo) => bo.DaysOfMonth);
			}).ForMember((ClockWorkServerJobMonthlyScheduleDTO dto) => dto.MonthsOfYear, delegate(IMemberConfigurationExpression<ClockWorkServerJobMonthlySchedule> m)
			{
				m.MapFrom<IList<int>>((ClockWorkServerJobMonthlySchedule bo) => bo.MonthsOfYear);
			});
			Mapper.CreateMap<ClockWorkServerJobWeeklySchedule, ClockWorkServerJobWeeklyScheduleDTO>().ForMember((ClockWorkServerJobWeeklyScheduleDTO dto) => (object)dto.AvoidWeekends, delegate(IMemberConfigurationExpression<ClockWorkServerJobWeeklySchedule> m)
			{
				m.MapFrom<bool>((ClockWorkServerJobWeeklySchedule bo) => bo.AvoidWeekends);
			}).ForMember((ClockWorkServerJobWeeklyScheduleDTO dto) => dto.DaysOfWeek, delegate(IMemberConfigurationExpression<ClockWorkServerJobWeeklySchedule> m)
			{
				m.MapFrom<IList<DayOfWeek>>((ClockWorkServerJobWeeklySchedule bo) => bo.DaysOfWeek);
			});
			Mapper.CreateMap<ClockWorkServerJobDailySchedule, ClockWorkServerJobDailyScheduleDTO>().ForMember((ClockWorkServerJobDailyScheduleDTO dto) => (object)dto.AvoidWeekends, delegate(IMemberConfigurationExpression<ClockWorkServerJobDailySchedule> m)
			{
				m.MapFrom<bool>((ClockWorkServerJobDailySchedule bo) => bo.AvoidWeekends);
			});
			Mapper.CreateMap<ClockWorkServerJobScheduleDTO, ClockWorkServerJobSchedule>().Include<ClockWorkServerJobMonthlyScheduleDTO, ClockWorkServerJobMonthlySchedule>().Include<ClockWorkServerJobWeeklyScheduleDTO, ClockWorkServerJobWeeklySchedule>().Include<ClockWorkServerJobDailyScheduleDTO, ClockWorkServerJobDailySchedule>();
			Mapper.CreateMap<ClockWorkServerJobMonthlyScheduleDTO, ClockWorkServerJobMonthlySchedule>().ForMember((ClockWorkServerJobMonthlySchedule bo) => bo.DaysOfMonth, delegate(IMemberConfigurationExpression<ClockWorkServerJobMonthlyScheduleDTO> m)
			{
				m.MapFrom<IList<int>>((ClockWorkServerJobMonthlyScheduleDTO dto) => dto.DaysOfMonth);
			}).ForMember((ClockWorkServerJobMonthlySchedule bo) => bo.MonthsOfYear, delegate(IMemberConfigurationExpression<ClockWorkServerJobMonthlyScheduleDTO> m)
			{
				m.MapFrom<IList<int>>((ClockWorkServerJobMonthlyScheduleDTO dto) => dto.MonthsOfYear);
			});
			Mapper.CreateMap<ClockWorkServerJobWeeklyScheduleDTO, ClockWorkServerJobWeeklySchedule>().ForMember((ClockWorkServerJobWeeklySchedule bo) => (object)bo.AvoidWeekends, delegate(IMemberConfigurationExpression<ClockWorkServerJobWeeklyScheduleDTO> m)
			{
				m.MapFrom<bool>((ClockWorkServerJobWeeklyScheduleDTO dto) => dto.AvoidWeekends);
			}).ForMember((ClockWorkServerJobWeeklySchedule bo) => bo.DaysOfWeek, delegate(IMemberConfigurationExpression<ClockWorkServerJobWeeklyScheduleDTO> m)
			{
				m.MapFrom<IList<DayOfWeek>>((ClockWorkServerJobWeeklyScheduleDTO dto) => dto.DaysOfWeek);
			});
			Mapper.CreateMap<ClockWorkServerJobDailyScheduleDTO, ClockWorkServerJobDailySchedule>().ForMember((ClockWorkServerJobDailySchedule bo) => (object)bo.AvoidWeekends, delegate(IMemberConfigurationExpression<ClockWorkServerJobDailyScheduleDTO> m)
			{
				m.MapFrom<bool>((ClockWorkServerJobDailyScheduleDTO dto) => dto.AvoidWeekends);
			});
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001CF60 File Offset: 0x0001B160
		public static ClockWorkServerJobSchedule ToDomainObject(this ClockWorkServerJobScheduleDTO dto)
		{
			Type type = dto.GetType();
			bool flag = type == typeof(ClockWorkServerJobMonthlyScheduleDTO);
			ClockWorkServerJobSchedule result;
			if (flag)
			{
				result = (ClockWorkServerJobMonthlySchedule)Mapper.Map(dto, type, typeof(ClockWorkServerJobMonthlySchedule));
			}
			else
			{
				bool flag2 = type == typeof(ClockWorkServerJobWeeklyScheduleDTO);
				if (flag2)
				{
					result = (ClockWorkServerJobWeeklySchedule)Mapper.Map(dto, type, typeof(ClockWorkServerJobWeeklySchedule));
				}
				else
				{
					bool flag3 = type == typeof(ClockWorkServerJobDailyScheduleDTO);
					if (flag3)
					{
						result = (ClockWorkServerJobDailySchedule)Mapper.Map(dto, type, typeof(ClockWorkServerJobDailySchedule));
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001D004 File Offset: 0x0001B204
		public static ClockWorkServerJobScheduleDTO ToDTO(this ClockWorkServerJobSchedule bo)
		{
			Type type = bo.GetType();
			bool flag = type == typeof(ClockWorkServerJobMonthlySchedule);
			ClockWorkServerJobScheduleDTO result;
			if (flag)
			{
				result = (ClockWorkServerJobMonthlyScheduleDTO)Mapper.Map(bo, type, typeof(ClockWorkServerJobMonthlyScheduleDTO));
			}
			else
			{
				bool flag2 = type == typeof(ClockWorkServerJobWeeklySchedule);
				if (flag2)
				{
					result = (ClockWorkServerJobWeeklyScheduleDTO)Mapper.Map(bo, type, typeof(ClockWorkServerJobWeeklyScheduleDTO));
				}
				else
				{
					bool flag3 = type == typeof(ClockWorkServerJobDailySchedule);
					if (flag3)
					{
						result = (ClockWorkServerJobDailyScheduleDTO)Mapper.Map(bo, type, typeof(ClockWorkServerJobDailyScheduleDTO));
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}
	}
}
