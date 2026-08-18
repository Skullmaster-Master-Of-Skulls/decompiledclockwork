using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005B9 RID: 1465
	public static class ClockWorkAuditAdapter
	{
		// Token: 0x06002F58 RID: 12120 RVA: 0x00034B80 File Offset: 0x00032D80
		public static string ConvertAuditResultsToHtmlReport(IList<AuditResult> Results)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<h1>Audit Results completed {0}</h1>", DateTime.Now.ToString("yyyy-MM-dd h:mm tt"));
			stringBuilder.AppendFormat("<div>{0}</div>", Results.GetStatusSummary());
			foreach (AuditResult auditResult in Results)
			{
				stringBuilder.AppendFormat("<h2>{0}: overall status: {1}</h2>", auditResult.AuditType.GetAttribute<ClockWorkAuditTypeAttribute>().Title, auditResult.Status.ToString());
				stringBuilder.AppendLine("<ul>");
				stringBuilder.AppendLine(string.Join("\r\n", (from g in auditResult.Checks
				select string.Concat(new string[]
				{
					"<li",
					(g.Status == eAuditStatus.Failed) ? " style='background-color: #FF5555; color: black'" : ((g.Status == eAuditStatus.CompletedSuccessfulWithWarnings) ? " style='background-color:yellow; color: black;'" : ""),
					">",
					"<b>",
					g.Title ?? "",
					":</b> ",
					g.Status.ToString(),
					"<div style='padding: 8px;'>",
					g.Note ?? "",
					"</div>",
					"</li>"
				})).ToArray<string>()));
				stringBuilder.AppendLine("</ul><hr /><br />");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x00034C9C File Offset: 0x00032E9C
		private static string GetStatusSummary(this IList<AuditResult> Results)
		{
			bool flag = Results == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				IEnumerable<AuditCheck> source = Results.SelectMany((AuditResult g) => g.Checks);
				Dictionary<eAuditStatus, List<AuditCheck>> source2 = (from g in source
				group g by g.Status).ToDictionary((IGrouping<eAuditStatus, AuditCheck> g) => g.Key, (IGrouping<eAuditStatus, AuditCheck> g) => g.Distinct<AuditCheck>().ToList<AuditCheck>());
				result = "<ul>" + string.Join("\r\n", (from g in source2
				select string.Concat(new string[]
				{
					"<li><b>",
					g.Key.ToString(),
					": </b>",
					g.Value.Count.ToString(),
					"</li>"
				})).ToArray<string>()) + "</ul>";
			}
			return result;
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x00034D94 File Offset: 0x00032F94
		public static eAuditStatus GetStatus(this AuditResult auditResult)
		{
			return (auditResult == null) ? eAuditStatus.Failed : auditResult.Checks.GetStatus();
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x00034DB8 File Offset: 0x00032FB8
		public static eAuditStatus GetStatus(this IList<AuditCheck> auditChecks)
		{
			eAuditStatus result;
			if (auditChecks != null)
			{
				result = (from g in auditChecks
				select g.Status).ToList<eAuditStatus>().GetStatus();
			}
			else
			{
				result = eAuditStatus.Failed;
			}
			return result;
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x00034E00 File Offset: 0x00033000
		public static eAuditStatus GetStatus(this IList<eAuditStatus> checkStatuses)
		{
			bool flag = checkStatuses == null;
			eAuditStatus result;
			if (flag)
			{
				result = eAuditStatus.Failed;
			}
			else
			{
				bool flag2 = checkStatuses.All((eAuditStatus g) => g == eAuditStatus.Pending);
				if (flag2)
				{
					result = eAuditStatus.Pending;
				}
				else
				{
					bool flag3 = checkStatuses.All((eAuditStatus g) => g == eAuditStatus.CompletedSuccessful);
					if (flag3)
					{
						result = eAuditStatus.CompletedSuccessful;
					}
					else
					{
						result = (checkStatuses.Any((eAuditStatus g) => g == eAuditStatus.Failed) ? eAuditStatus.Failed : eAuditStatus.CompletedSuccessfulWithWarnings);
					}
				}
			}
			return result;
		}
	}
}
