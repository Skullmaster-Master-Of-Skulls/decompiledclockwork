using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000832 RID: 2098
	[DataContract(Namespace = "http://tpro.ca")]
	public class CourseRegistrationWithStudentSpecificInfoDTO : CourseRegistrationDTO, ICloneable<CourseRegistrationWithStudentSpecificInfoDTO>, ICloneable
	{
		// Token: 0x06002AC3 RID: 10947 RVA: 0x000144A8 File Offset: 0x000126A8
		public CourseRegistrationWithStudentSpecificInfoDTO()
		{
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000144B4 File Offset: 0x000126B4
		public CourseRegistrationWithStudentSpecificInfoDTO(CourseRegistrationDTO item)
		{
			this.CloneItem<CourseRegistrationDTO>(item);
			CourseRegistrationWithStudentSpecificInfoDTO courseRegistrationWithStudentSpecificInfoDTO = item as CourseRegistrationWithStudentSpecificInfoDTO;
			bool flag = courseRegistrationWithStudentSpecificInfoDTO != null;
			if (flag)
			{
				this.StudentSpecificInfo = courseRegistrationWithStudentSpecificInfoDTO.StudentSpecificInfo;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000144EF File Offset: 0x000126EF
		// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x000144F7 File Offset: 0x000126F7
		[DataMember]
		public CourseStudentSpecificDTO StudentSpecificInfo { get; set; }

		// Token: 0x06002AC7 RID: 10951 RVA: 0x00014500 File Offset: 0x00012700
		public new CourseRegistrationWithStudentSpecificInfoDTO Clone()
		{
			return new CourseRegistrationWithStudentSpecificInfoDTO(this);
		}
	}
}
