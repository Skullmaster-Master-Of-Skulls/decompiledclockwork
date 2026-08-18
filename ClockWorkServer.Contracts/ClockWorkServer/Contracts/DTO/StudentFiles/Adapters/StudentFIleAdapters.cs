using System;
using System.Linq;
using TechnoPro.Common.Public.Entities.StudentFiles;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles.Adapters
{
	// Token: 0x0200023B RID: 571
	public static class StudentFIleAdapters
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x00005E98 File Offset: 0x00004098
		public static string GetCssClassSuffixForStatus(this StudentFilesStatusDTO status)
		{
			string statusText = ((status != null) ? status.Title : null) ?? "";
			eStudentFileStatusType eStudentFileStatusType = (status != null) ? status.StatusType : eStudentFileStatusType.Unknown;
			bool flag = eStudentFileStatusType == eStudentFileStatusType.Closed;
			string result;
			if (flag)
			{
				bool flag2 = new string[]
				{
					"approve",
					"complete",
					"done"
				}.Any((string g) => statusText.IndexOf(g, StringComparison.OrdinalIgnoreCase) >= 0);
				if (flag2)
				{
					result = "success";
				}
				else
				{
					result = "danger";
				}
			}
			else
			{
				bool flag3 = statusText.Length > 0;
				if (flag3)
				{
					result = "warning";
				}
				else
				{
					result = "info";
				}
			}
			return result;
		}
	}
}
