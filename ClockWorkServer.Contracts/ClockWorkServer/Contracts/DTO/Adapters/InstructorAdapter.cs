using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C84 RID: 3204
	public static class InstructorAdapter
	{
		// Token: 0x060042C7 RID: 17095 RVA: 0x00021DE0 File Offset: 0x0001FFE0
		public static bool IsAllowed(this LookupInstructorDTO Instructor, ePermissionForCourseDTO permissionLevel)
		{
			bool flag = Instructor == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = permissionLevel == ePermissionForCourseDTO.NoPermission;
				result = (!flag2 && (Instructor.PermissionLevel & permissionLevel) > ePermissionForCourseDTO.PassiveAcceptAll);
			}
			return result;
		}
	}
}
