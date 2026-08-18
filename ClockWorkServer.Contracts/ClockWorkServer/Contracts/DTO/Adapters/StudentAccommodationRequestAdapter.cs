using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C8E RID: 3214
	public static class StudentAccommodationRequestAdapter
	{
		// Token: 0x060042F5 RID: 17141 RVA: 0x00023900 File Offset: 0x00021B00
		public static IList<StudentCourseAccommodationRequestHistoryItemDateApprovedDTO> GetApprovedDates(this StudentCourseAccommodationRequestHistoryDTO history)
		{
			bool flag = history == null || history.HistoryItems == null;
			IList<StudentCourseAccommodationRequestHistoryItemDateApprovedDTO> result;
			if (flag)
			{
				result = new List<StudentCourseAccommodationRequestHistoryItemDateApprovedDTO>();
			}
			else
			{
				List<StudentCourseAccommodationRequestHistoryItemDateApprovedDTO> list = new List<StudentCourseAccommodationRequestHistoryItemDateApprovedDTO>();
				bool flag2 = false;
				foreach (StudentCourseAccommodationRequestHistoryItemDTO studentCourseAccommodationRequestHistoryItemDTO in history.HistoryItems)
				{
					bool flag3 = studentCourseAccommodationRequestHistoryItemDTO.Status == eStudentCourseAccommodationRequestStatus.Approved;
					bool flag4 = flag3 && !flag2;
					if (flag4)
					{
						list.Add(new StudentCourseAccommodationRequestHistoryItemDateApprovedDTO
						{
							StudentCourseAccommodationRequestId = studentCourseAccommodationRequestHistoryItemDTO.StudentCourseAccommodationRequestId,
							DateApproved = studentCourseAccommodationRequestHistoryItemDTO.DateModified,
							WhoApproved = studentCourseAccommodationRequestHistoryItemDTO.WhoModified
						});
					}
					flag2 = flag3;
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x000239D4 File Offset: 0x00021BD4
		public static StudentCourseAccommodationRequestHistoryItemDateApprovedDTO GetLastApprovedDate(this StudentCourseAccommodationRequestHistoryDTO history)
		{
			IList<StudentCourseAccommodationRequestHistoryItemDateApprovedDTO> approvedDates = history.GetApprovedDates();
			return (approvedDates.Count > 0) ? approvedDates[0] : null;
		}
	}
}
