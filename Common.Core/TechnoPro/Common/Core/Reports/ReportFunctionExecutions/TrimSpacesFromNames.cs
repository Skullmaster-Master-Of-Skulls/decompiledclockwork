using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x020000A0 RID: 160
	public class TrimSpacesFromNames : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x0000672B File Offset: 0x0000492B
		public TrimSpacesFromNames()
		{
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000206E2 File Offset: 0x0001E8E2
		public TrimSpacesFromNames(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000206F4 File Offset: 0x0001E8F4
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x000206FC File Offset: 0x0001E8FC
		public OperationContext OpContext { get; set; }

		// Token: 0x06000588 RID: 1416 RVA: 0x00020708 File Offset: 0x0001E908
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			List<int> list = (from g in peopleManager.LoadAllUserObjects(true)
			select g.PersonId).Distinct<int>().ToList<int>();
			DataTable dataTable = new DataTable("q");
			dataTable.Columns.Add("pid", typeof(int));
			dataTable.Columns.Add("OldFirstName");
			dataTable.Columns.Add("NewFirstName");
			dataTable.Columns.Add("OldMiddleName");
			dataTable.Columns.Add("NewMiddleName");
			dataTable.Columns.Add("OldLastName");
			dataTable.Columns.Add("NewLastName");
			for (int i = 0; i < list.Count; i += 500)
			{
				List<int> list2 = new List<int>();
				for (int j = 0; j < 500; j++)
				{
					int num = i + j;
					bool flag = num >= list.Count;
					if (flag)
					{
						break;
					}
					list2.Add(list[num]);
				}
				IList<PersonBase> source = peopleManager.LoadPersonsByIds(list2);
				List<TrimSpacesFromNames.UserToPotentiallyUpdate> list3 = (from g in source
				select new TrimSpacesFromNames.UserToPotentiallyUpdate(g) into h
				where h.TrimmedPerson != null
				select h).ToList<TrimSpacesFromNames.UserToPotentiallyUpdate>();
				foreach (TrimSpacesFromNames.UserToPotentiallyUpdate userToPotentiallyUpdate in list3)
				{
					peopleManager.UpdateUser(userToPotentiallyUpdate.TrimmedPerson, false);
					dataTable.Rows.Add(new object[]
					{
						userToPotentiallyUpdate.OriginalPerson.PersonId,
						userToPotentiallyUpdate.OriginalPerson.FirstName ?? "",
						userToPotentiallyUpdate.TrimmedPerson.FirstName,
						userToPotentiallyUpdate.OriginalPerson.MiddleName ?? "",
						userToPotentiallyUpdate.TrimmedPerson.MiddleName,
						userToPotentiallyUpdate.OriginalPerson.LastName ?? "",
						userToPotentiallyUpdate.TrimmedPerson.LastName
					});
				}
			}
			result.Data.Table = dataTable;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000209B0 File Offset: 0x0001EBB0
		private static PersonBase GetUserWithTrimmedNameOrNullIfNoTrimNecessary(PersonBase pb)
		{
			string text = pb.FirstName ?? "";
			string text2 = pb.MiddleName ?? "";
			string text3 = pb.LastName ?? "";
			string text4 = text.Trim();
			string text5 = text2.Trim();
			string text6 = text3.Trim();
			bool flag = text4 == text && text5 == text2 && text6 == text3;
			PersonBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				PersonBase personBase = pb.Clone();
				personBase.FirstName = text4;
				personBase.MiddleName = text5;
				personBase.LastName = text6;
				result = personBase;
			}
			return result;
		}

		// Token: 0x0200023F RID: 575
		internal class UserToPotentiallyUpdate
		{
			// Token: 0x0600136C RID: 4972 RVA: 0x000806A9 File Offset: 0x0007E8A9
			public UserToPotentiallyUpdate(PersonBase pb)
			{
				this.TrimmedPerson = TrimSpacesFromNames.GetUserWithTrimmedNameOrNullIfNoTrimNecessary(pb);
			}

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x0600136D RID: 4973 RVA: 0x000806C0 File Offset: 0x0007E8C0
			// (set) Token: 0x0600136E RID: 4974 RVA: 0x000806C8 File Offset: 0x0007E8C8
			public PersonBase OriginalPerson { get; set; }

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x0600136F RID: 4975 RVA: 0x000806D1 File Offset: 0x0007E8D1
			// (set) Token: 0x06001370 RID: 4976 RVA: 0x000806D9 File Offset: 0x0007E8D9
			public PersonBase TrimmedPerson { get; set; }
		}
	}
}
