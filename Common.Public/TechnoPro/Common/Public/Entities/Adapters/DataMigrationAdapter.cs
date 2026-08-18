using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.DataMigration.Results;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005BD RID: 1469
	public static class DataMigrationAdapter
	{
		// Token: 0x06002F6B RID: 12139 RVA: 0x00035674 File Offset: 0x00033874
		public static string GenerateDataMigrationReportHtml<R, T>(this Dictionary<R, List<T>> items, Func<R, List<T>, string> outputItem)
		{
			return "<ul>" + string.Join("", items.Select(delegate(KeyValuePair<R, List<T>> kvp)
			{
				string[] array = new string[5];
				array[0] = "<li><b>";
				int num = 1;
				R key = kvp.Key;
				array[num] = key.ToString();
				array[2] = ": </b>";
				array[3] = outputItem(kvp.Key, kvp.Value);
				array[4] = "</li>";
				return string.Concat(array);
			}).ToArray<string>()) + "<ul>";
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000356C4 File Offset: 0x000338C4
		public static string GenerateDataMigrationReportHtml(this IList<MigrationCreateStudentResult> CreateStudentResults)
		{
			List<MigrationCreateStudentResult> list = (CreateStudentResults ?? new List<MigrationCreateStudentResult>()).ToList<MigrationCreateStudentResult>();
			list.Sort((MigrationCreateStudentResult g1, MigrationCreateStudentResult g2) => g1.Status.CompareTo(g2.Status));
			Dictionary<eMigrationCreateStudentStatus, List<MigrationCreateStudentResult>> items = (from res in list
			group res by res.Status into groupedItemClass
			select groupedItemClass).ToDictionary((IGrouping<eMigrationCreateStudentStatus, MigrationCreateStudentResult> gdc) => gdc.Key, (IGrouping<eMigrationCreateStudentStatus, MigrationCreateStudentResult> gdc) => gdc.ToList<MigrationCreateStudentResult>());
			string text = items.GenerateDataMigrationReportHtml((eMigrationCreateStudentStatus g, List<MigrationCreateStudentResult> r) => (r == null) ? "0" : r.Count.ToString());
			List<string> list2 = (from g in list
			where (g.Status & eMigrationCreateStudentStatus.Successful) > eMigrationCreateStudentStatus.Unknown
			select g into h
			select h.StudentNumber ?? "").Distinct<string>().ToList<string>();
			list2.Sort((string g1, string g2) => string.Compare(g1, g2, StringComparison.OrdinalIgnoreCase));
			List<string> list3 = (from g in list
			where (g.Status & eMigrationCreateStudentStatus.Successful) == eMigrationCreateStudentStatus.Unknown
			select g into h
			select h.StudentNumber ?? "").Distinct<string>().ToList<string>();
			list3.Sort((string g1, string g2) => string.Compare(g1, g2, StringComparison.OrdinalIgnoreCase));
			return string.Format("<h2>Create students</h2>\r\n<p>{0}</p>\r\n<p>{1} unique student account(s) created<br />\r\n{2} students skipped</p>\r\n<p>Successfully created: {3}</p>{4}", new object[]
			{
				text,
				list2.Count,
				list3.Count,
				string.Join(", ", list2.ToArray()),
				(list3.Count < 1) ? "" : ("<p>Failed to create: " + string.Join(", ", list3.ToArray()) + "</p>")
			});
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x00035930 File Offset: 0x00033B30
		public static string GenerateDataMigrationReportHtml(this IList<MigrationDataItemResult> MigrateDataResults)
		{
			List<MigrationDataItemResult> list = (MigrateDataResults ?? new List<MigrationDataItemResult>()).ToList<MigrationDataItemResult>();
			list.Sort((MigrationDataItemResult g1, MigrationDataItemResult g2) => g1.Status.CompareTo(g2.Status));
			Dictionary<eMigrationDataItemStatus, List<MigrationDataItemResult>> items = (from res in list
			group res by res.Status into groupedItemClass
			select groupedItemClass).ToDictionary((IGrouping<eMigrationDataItemStatus, MigrationDataItemResult> gdc) => gdc.Key, (IGrouping<eMigrationDataItemStatus, MigrationDataItemResult> gdc) => gdc.ToList<MigrationDataItemResult>());
			string text = items.GenerateDataMigrationReportHtml(delegate(eMigrationDataItemStatus g, List<MigrationDataItemResult> r)
			{
				Dictionary<string, List<MigrationDataItemResult>> items2 = (from item in r
				group item by item.DataItemName ?? "" into groupedNameClass
				select groupedNameClass).ToDictionary((IGrouping<string, MigrationDataItemResult> gdc) => gdc.Key, (IGrouping<string, MigrationDataItemResult> gdc) => gdc.ToList<MigrationDataItemResult>());
				string[] array2 = new string[5];
				array2[0] = "<li><b>";
				array2[1] = g.ToString();
				array2[2] = ": </b>";
				array2[3] = items2.GenerateDataMigrationReportHtml((string m, List<MigrationDataItemResult> n) => "<i>" + (m ?? "") + ": </i>" + n.Count.ToString());
				array2[4] = "</li>";
				return string.Concat(array2);
			});
			List<MigrationDataItemResult> list2 = (from g in list
			where (g.Status & eMigrationDataItemStatus.Successful) > eMigrationDataItemStatus.Unknown
			select g).ToList<MigrationDataItemResult>();
			List<MigrationDataItemResult> list3 = (from g in list
			where (g.Status & eMigrationDataItemStatus.Failed) > eMigrationDataItemStatus.Unknown
			select g).ToList<MigrationDataItemResult>();
			string format = "<h2>Migrate data</h2>\r\n<h3>Mappings</h3>\r\n<p>{0}</p>\r\n<p>{1} <br />\r\n{2} items skipped</p>\r\n<p>Successfully created: {3}</p>{4}";
			object[] array = new object[5];
			array[0] = text;
			array[1] = list2.Count;
			array[2] = list3.Count;
			array[3] = string.Join(", ", (from m in list2
			select m.DataItemName ?? "").Distinct<string>().ToArray<string>());
			int num = 4;
			object obj;
			if (list3.Count >= 1)
			{
				obj = "<p>Failed to migrate: " + string.Join(", ", (from m in list3
				select m.DataItemName ?? "").Distinct<string>().ToArray<string>()) + "</p>";
			}
			else
			{
				obj = "";
			}
			array[num] = obj;
			return string.Format(format, array);
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x00035B50 File Offset: 0x00033D50
		public static string GenerateDataMigrationReportHtml(this IList<MigrationAppointmentItemResult> MigrateAppResults)
		{
			return "<h2>Not implemented</h2>";
		}
	}
}
