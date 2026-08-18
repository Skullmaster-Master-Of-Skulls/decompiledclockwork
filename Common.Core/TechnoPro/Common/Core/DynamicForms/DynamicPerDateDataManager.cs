using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x020000FF RID: 255
	public class DynamicPerDateDataManager : IDynamicPerDateDataManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x00042FB5 File Offset: 0x000411B5
		public DynamicPerDateDataManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00042FC7 File Offset: 0x000411C7
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00042FCF File Offset: 0x000411CF
		public OperationContext OpContext { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00042FD8 File Offset: 0x000411D8
		private IDynamicPerDateDataDAO dao
		{
			get
			{
				bool flag = this._dao != null;
				IDynamicPerDateDataDAO dao;
				if (flag)
				{
					dao = this._dao;
				}
				else
				{
					this._dao = new DynamicPerDateDataDAO(this.OpContext);
					dao = this._dao;
				}
				return dao;
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00043018 File Offset: 0x00041218
		public IList<PerDateEntry> LoadPerDateEntries(int StudentPersonId, int ScreenNum)
		{
			return this.dao.LoadPerDateEntries(StudentPersonId, ScreenNum);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00043038 File Offset: 0x00041238
		public int CreatePerDateEntry(PerDateEntry perDateEntry)
		{
			return this.dao.CreatePerDateEntry(perDateEntry);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00043058 File Offset: 0x00041258
		public IList<PersonBase> LoadUniqueStudentsWithPerDateDataEnteredByForm(int ScreenNum)
		{
			return this.dao.LoadUniqueStudentsWithPerDateDataEnteredByForm(ScreenNum);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00043078 File Offset: 0x00041278
		public PerDateEntry GetExistingPerDateEntry(int StudentPersonId, int ScreenNum, Session Session)
		{
			return this.dao.GetExistingPerDateEntry(StudentPersonId, ScreenNum, Session.StartDate, Session.EndDate);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000430A4 File Offset: 0x000412A4
		public IList<PerDateEntryWithChildEntries> LoadPerDateEntriesWithChildEntries(int StudentPersonId, DynamicForm Form)
		{
			bool flag = Form.SubForm == null || Form.SubForm.ScreenNum < 1;
			IList<PerDateEntryWithChildEntries> result;
			if (flag)
			{
				result = (from g in this.LoadPerDateEntries(StudentPersonId, Form.ScreenNum)
				select g.ToPerDateEntryWithChildEntries()).ToList<PerDateEntryWithChildEntries>();
			}
			else
			{
				result = this.dao.LoadPerDateEntriesWithChildEntries(StudentPersonId, Form.ScreenNum, Form.SubForm.ScreenNum);
			}
			return result;
		}

		// Token: 0x040001C5 RID: 453
		private IDynamicPerDateDataDAO _dao;
	}
}
