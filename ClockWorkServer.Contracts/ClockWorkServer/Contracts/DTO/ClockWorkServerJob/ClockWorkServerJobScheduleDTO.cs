using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200087B RID: 2171
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(ClockWorkServerJobMonthlyScheduleDTO))]
	[KnownType(typeof(ClockWorkServerJobWeeklyScheduleDTO))]
	[KnownType(typeof(ClockWorkServerJobDailyScheduleDTO))]
	public class ClockWorkServerJobScheduleDTO
	{
	}
}
