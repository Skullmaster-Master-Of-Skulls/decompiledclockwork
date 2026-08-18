using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.LookupCourses
{
	// Token: 0x020000D5 RID: 213
	public class LookupTimetableItemManager : ILookupTimetableItemManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00037C9B File Offset: 0x00035E9B
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00037CA3 File Offset: 0x00035EA3
		public ILookupTimetableItemDAO dao { get; set; }

		// Token: 0x06000829 RID: 2089 RVA: 0x00037CAC File Offset: 0x00035EAC
		public LookupTimetableItemManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new LookupTimetableItemDAO(opContext);
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00037CCB File Offset: 0x00035ECB
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x00037CD3 File Offset: 0x00035ED3
		public OperationContext OpContext { get; set; }

		// Token: 0x0600082C RID: 2092 RVA: 0x00037CDC File Offset: 0x00035EDC
		public LookupTimetableItem LoadLookupTimetableItem(int TimetableId)
		{
			return this.dao.LoadLookupTimetableItem(TimetableId);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00037CFA File Offset: 0x00035EFA
		public void SaveLookupTimetableItems(int LuCourseId, List<LookupTimetableItem> items)
		{
			this.dao.SaveLookupTimetableItems(LuCourseId, items);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00037D0C File Offset: 0x00035F0C
		public IList<LookupCourse> LoadLookupTimetableItemsByStudent(int StudentPid, DateTime StartDateTime, DateTime EndDateTime)
		{
			return this.dao.LoadLookupTimetableItemsByStudent(StudentPid, StartDateTime, EndDateTime);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00037D2C File Offset: 0x00035F2C
		[DebuggerStepThrough]
		public Task<IList<LookupCourse>> LoadLookupTimetableItemsByStudentAsync(int StudentPid, DateTime StartDateTime, DateTime EndDateTime)
		{
			LookupTimetableItemManager.<LoadLookupTimetableItemsByStudentAsync>d__12 <LoadLookupTimetableItemsByStudentAsync>d__ = new LookupTimetableItemManager.<LoadLookupTimetableItemsByStudentAsync>d__12();
			<LoadLookupTimetableItemsByStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<LookupCourse>>.Create();
			<LoadLookupTimetableItemsByStudentAsync>d__.<>4__this = this;
			<LoadLookupTimetableItemsByStudentAsync>d__.StudentPid = StudentPid;
			<LoadLookupTimetableItemsByStudentAsync>d__.StartDateTime = StartDateTime;
			<LoadLookupTimetableItemsByStudentAsync>d__.EndDateTime = EndDateTime;
			<LoadLookupTimetableItemsByStudentAsync>d__.<>1__state = -1;
			<LoadLookupTimetableItemsByStudentAsync>d__.<>t__builder.Start<LookupTimetableItemManager.<LoadLookupTimetableItemsByStudentAsync>d__12>(ref <LoadLookupTimetableItemsByStudentAsync>d__);
			return <LoadLookupTimetableItemsByStudentAsync>d__.<>t__builder.Task;
		}
	}
}
