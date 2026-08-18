using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.DAO.Communications;
using TechnoPro.Common.DAO.Impl.Communications;
using TechnoPro.Common.ICore.Communications;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Communications;

namespace TechnoPro.Common.Core.Communications
{
	// Token: 0x0200011B RID: 283
	public class CommunicationsSentManager : ICommunicationsSentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000C01 RID: 3073 RVA: 0x00054139 File Offset: 0x00052339
		public CommunicationsSentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0005414B File Offset: 0x0005234B
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00054153 File Offset: 0x00052353
		public OperationContext OpContext { get; set; }

		// Token: 0x06000C04 RID: 3076 RVA: 0x0005415C File Offset: 0x0005235C
		public StudentCommunicationHistory LoadStudentCommunicationHistory(int studentPersonId)
		{
			ICommunicationsSentDAO communicationsSentDAO = new CommunicationsSentDAO(this.OpContext);
			IList<Communication> communications = communicationsSentDAO.LoadCommunicationsForUser(studentPersonId);
			return new StudentCommunicationHistory
			{
				StudentPersonId = studentPersonId,
				Communications = communications
			};
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00054198 File Offset: 0x00052398
		[DebuggerStepThrough]
		public Task<StudentCommunicationHistory> LoadStudentCommunicationHistoryAsync(int studentPersonId)
		{
			CommunicationsSentManager.<LoadStudentCommunicationHistoryAsync>d__6 <LoadStudentCommunicationHistoryAsync>d__ = new CommunicationsSentManager.<LoadStudentCommunicationHistoryAsync>d__6();
			<LoadStudentCommunicationHistoryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StudentCommunicationHistory>.Create();
			<LoadStudentCommunicationHistoryAsync>d__.<>4__this = this;
			<LoadStudentCommunicationHistoryAsync>d__.studentPersonId = studentPersonId;
			<LoadStudentCommunicationHistoryAsync>d__.<>1__state = -1;
			<LoadStudentCommunicationHistoryAsync>d__.<>t__builder.Start<CommunicationsSentManager.<LoadStudentCommunicationHistoryAsync>d__6>(ref <LoadStudentCommunicationHistoryAsync>d__);
			return <LoadStudentCommunicationHistoryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000541E4 File Offset: 0x000523E4
		public int AddCommicationSendAttempt(CommunicationBase sendAttempt)
		{
			ICommunicationsSentDAO communicationsSentDAO = new CommunicationsSentDAO(this.OpContext);
			return communicationsSentDAO.AddCommicationSendAttempt(sendAttempt);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0005420C File Offset: 0x0005240C
		[DebuggerStepThrough]
		public Task<int> AddCommicationSendAttemptAsync(CommunicationBase sendAttempt)
		{
			CommunicationsSentManager.<AddCommicationSendAttemptAsync>d__8 <AddCommicationSendAttemptAsync>d__ = new CommunicationsSentManager.<AddCommicationSendAttemptAsync>d__8();
			<AddCommicationSendAttemptAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<AddCommicationSendAttemptAsync>d__.<>4__this = this;
			<AddCommicationSendAttemptAsync>d__.sendAttempt = sendAttempt;
			<AddCommicationSendAttemptAsync>d__.<>1__state = -1;
			<AddCommicationSendAttemptAsync>d__.<>t__builder.Start<CommunicationsSentManager.<AddCommicationSendAttemptAsync>d__8>(ref <AddCommicationSendAttemptAsync>d__);
			return <AddCommicationSendAttemptAsync>d__.<>t__builder.Task;
		}
	}
}
