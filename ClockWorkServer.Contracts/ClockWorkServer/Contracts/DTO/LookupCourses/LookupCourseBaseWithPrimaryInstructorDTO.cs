using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A1 RID: 1953
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupCourseBaseWithPrimaryInstructorDTO : LookupCourseBaseDTO, ICloneable<LookupCourseBaseWithPrimaryInstructorDTO>, ICloneable
	{
		// Token: 0x0600282B RID: 10283 RVA: 0x000130AC File Offset: 0x000112AC
		public LookupCourseBaseWithPrimaryInstructorDTO()
		{
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000130B8 File Offset: 0x000112B8
		public LookupCourseBaseWithPrimaryInstructorDTO(LookupCourseBaseWithPrimaryInstructorDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				LookupCourseBaseDTO.CloneLookupCourseBaseItem<LookupCourseBaseWithPrimaryInstructorDTO>(this, item);
				this.PrimaryInstructor = ((item.PrimaryInstructor == null) ? null : item.PrimaryInstructor.Clone());
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x000130FC File Offset: 0x000112FC
		// (set) Token: 0x0600282E RID: 10286 RVA: 0x00013104 File Offset: 0x00011304
		[DataMember]
		public LookupInstructorDTO PrimaryInstructor { get; set; }

		// Token: 0x0600282F RID: 10287 RVA: 0x00013110 File Offset: 0x00011310
		public new LookupCourseBaseWithPrimaryInstructorDTO Clone()
		{
			return new LookupCourseBaseWithPrimaryInstructorDTO(this);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x00013128 File Offset: 0x00011328
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
