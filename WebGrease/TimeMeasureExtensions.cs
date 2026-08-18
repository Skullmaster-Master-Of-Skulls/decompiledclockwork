using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x0200010F RID: 271
	public static class TimeMeasureExtensions
	{
		// Token: 0x06001105 RID: 4357 RVA: 0x0004BBF8 File Offset: 0x00049DF8
		public static string GetCsv(this IEnumerable<TimeMeasureResult> results)
		{
			TimeMeasureResult[] array = (from r in results
			orderby r.Duration descending
			select r).ToArray<TimeMeasureResult>();
			double totalTime = array.Sum((TimeMeasureResult r) => r.Duration);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(TimeMeasureExtensions.GetCsvRow(TimeMeasureExtensions.HeaderValues));
			foreach (TimeMeasureResult measureResult in array)
			{
				stringBuilder.AppendLine(TimeMeasureExtensions.GetCsvRow(TimeMeasureExtensions.GetValues(measureResult, totalTime)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0004BCB0 File Offset: 0x00049EB0
		public static string GetTextTable(this IEnumerable<TimeMeasureResult> results, string title)
		{
			TimeMeasureResult[] array = (from r in results
			orderby r.Duration descending
			select r).ToArray<TimeMeasureResult>();
			StringBuilder stringBuilder = new StringBuilder();
			double num = array.Sum((TimeMeasureResult r) => r.Duration);
			stringBuilder.AppendLine("/=======================================================================================");
			stringBuilder.AppendLine("| " + title);
			stringBuilder.AppendLine("|--------------------------------------------------------------------------------------");
			stringBuilder.AppendLine("| {1,14} | {2,7} | {3,6} | {4,7} | {0}".InvariantFormat(TimeMeasureExtensions.HeaderValues));
			stringBuilder.AppendLine("|--------------------------------------------------------------------------------------");
			foreach (TimeMeasureResult measureResult in array)
			{
				stringBuilder.AppendLine("| {1,14:N0} | {2,7:P1} | {3,6} | {4,7:N0} | {0}".InvariantFormat(TimeMeasureExtensions.GetValues(measureResult, num)));
			}
			stringBuilder.AppendLine("|--------------------------------------------------------------------------------------");
			stringBuilder.AppendLine("| {1,14:N0} | {2,7:P1} | {3,6} | {4,7} | {0}".InvariantFormat(new object[]
			{
				"Total",
				num,
				1,
				string.Empty,
				string.Empty
			}));
			stringBuilder.AppendLine("\\______________________________________________________________________________________");
			return stringBuilder.ToString();
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0004BE88 File Offset: 0x0004A088
		public static IEnumerable<TimeMeasureResult> Group(this IEnumerable<TimeMeasureResult> resultsToAdd, Func<TimeMeasureResult, string> groupSelector)
		{
			return (from r in resultsToAdd.GroupBy(groupSelector).Select(delegate(IGrouping<string, TimeMeasureResult> s)
			{
				TimeMeasureResult timeMeasureResult = new TimeMeasureResult();
				timeMeasureResult.IdParts = WebGreaseContext.ToIdParts(s.Key);
				timeMeasureResult.Count = s.Min((TimeMeasureResult m) => m.Count);
				timeMeasureResult.Duration = s.Sum((TimeMeasureResult m) => m.Duration);
				return timeMeasureResult;
			})
			orderby r.Duration descending
			select r).ToArray<TimeMeasureResult>();
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0004BEE5 File Offset: 0x0004A0E5
		private static string GetCsvRow(object[] values)
		{
			return "\"" + string.Join("\",\"", values) + "\"";
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0004BF04 File Offset: 0x0004A104
		private static object[] GetValues(TimeMeasureResult measureResult, double totalTime)
		{
			return new object[]
			{
				measureResult.Name,
				Math.Round(measureResult.Duration),
				measureResult.Duration / totalTime,
				measureResult.Count,
				measureResult.Duration / (double)measureResult.Count
			};
		}

		// Token: 0x040006B0 RID: 1712
		private static readonly object[] HeaderValues = new object[]
		{
			"Type",
			"Duration (ms)",
			"%",
			"#",
			"ms/#"
		};
	}
}
