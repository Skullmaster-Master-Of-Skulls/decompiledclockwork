using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.DAO.Impl.Updates;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x02000006 RID: 6
	public class ExecuteUpdateManager : IExecuteUpdateManager
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024B2 File Offset: 0x000006B2
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024BA File Offset: 0x000006BA
		public ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024C3 File Offset: 0x000006C3
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000024CB File Offset: 0x000006CB
		public IExternalLogManager ExternalLogManager { get; set; }

		// Token: 0x0600001D RID: 29 RVA: 0x000024D4 File Offset: 0x000006D4
		public ExecuteUpdateManager(ServerInstanceInfo serverInstance)
		{
			this.ServerInstance = serverInstance;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024E8 File Offset: 0x000006E8
		public void ExecuteUpdates()
		{
			IList<ExecuteUpdatesResp> list = this.ExecUpdates();
			CWLogger.Logger.Info("ExecuteUpdateManager::ExecuteUpdates:: {0} updates were executed, success={1}, errors={2}", list.Count, list.Count((ExecuteUpdatesResp r) => r.ExecuteUpdatesStatus != eExecuteUpdateStatus.Error), list.Count((ExecuteUpdatesResp r) => r.ExecuteUpdatesStatus == eExecuteUpdateStatus.Error));
			bool flag = list != null && list.Count > 0;
			if (flag)
			{
				this.SendNotificationEmailsAsync(list);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002588 File Offset: 0x00000788
		private static string GetUpdatingSystemPath()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025AC File Offset: 0x000007AC
		[DebuggerStepThrough]
		private Task SendNotificationEmailsAsync(IEnumerable<ExecuteUpdatesResp> execUpdatesResps)
		{
			ExecuteUpdateManager.<SendNotificationEmailsAsync>d__11 <SendNotificationEmailsAsync>d__ = new ExecuteUpdateManager.<SendNotificationEmailsAsync>d__11();
			<SendNotificationEmailsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendNotificationEmailsAsync>d__.<>4__this = this;
			<SendNotificationEmailsAsync>d__.execUpdatesResps = execUpdatesResps;
			<SendNotificationEmailsAsync>d__.<>1__state = -1;
			<SendNotificationEmailsAsync>d__.<>t__builder.Start<ExecuteUpdateManager.<SendNotificationEmailsAsync>d__11>(ref <SendNotificationEmailsAsync>d__);
			return <SendNotificationEmailsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025F8 File Offset: 0x000007F8
		private IDictionary<string, Type> GetUpdateExecuters()
		{
			Type iType = typeof(IUpdateExecuter);
			IEnumerable<Type> enumerable = from p in Assembly.GetExecutingAssembly().GetTypes()
			where iType.IsAssignableFrom(p) && p.IsClass
			select p;
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
			foreach (Type type in enumerable)
			{
				List<ExecuterFileTypeAttribute> list = (from att in (ExecuterFileTypeAttribute[])type.GetCustomAttributes(typeof(ExecuterFileTypeAttribute), false)
				select att).ToList<ExecuterFileTypeAttribute>();
				foreach (ExecuterFileTypeAttribute executerFileTypeAttribute in list)
				{
					dictionary.Add(executerFileTypeAttribute.FileType, type);
				}
			}
			return dictionary;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002714 File Offset: 0x00000914
		private IList<ExecuteUpdatesResp> ExecUpdates()
		{
			List<ExecuteUpdatesResp> list = new List<ExecuteUpdatesResp>();
			UpdateDAO updateDAO = new UpdateDAO();
			IList<UpdateStatus> executionStatus = updateDAO.GetExecutionStatus();
			IDictionary<string, Type> updateExecuters = this.GetUpdateExecuters();
			SortedList<int, IUpdateExecuter> sortedList = new SortedList<int, IUpdateExecuter>();
			foreach (UpdateStatus updateStatus in from u in executionStatus
			where u.Status == eUpdateStatus.OnSchedule.ToString()
			select u)
			{
				Type type = updateExecuters[updateStatus.FileType];
				bool flag = type != null;
				if (flag)
				{
					IUpdateExecuter updateExecuter = (IUpdateExecuter)Activator.CreateInstance(type);
					updateExecuter.ServerInstance = this.ServerInstance;
					updateExecuter.ExternalLogManager = this.ExternalLogManager;
					bool flag2 = !sortedList.ContainsKey(updateExecuter.ExecutionOrder);
					if (flag2)
					{
						sortedList.Add(updateExecuter.ExecutionOrder, updateExecuter);
					}
				}
			}
			foreach (IUpdateExecuter updateExecuter2 in sortedList.Values)
			{
				bool flag3 = updateExecuter2 != null;
				if (flag3)
				{
					ExecuteUpdatesResp executeUpdatesResp = updateExecuter2.ExecuteUpdate();
					list.Add(executeUpdatesResp);
					bool flag4 = executeUpdatesResp.ExecuteUpdatesStatus == eExecuteUpdateStatus.Error;
					if (flag4)
					{
						CWLogger.Logger.Info("ExecuteUpdateManager::ExecUpdates: File(s) '{0}' failed to update: {1}", executeUpdatesResp.Filenames.CommaSeparatedValues<string>(), executeUpdatesResp.LastError ?? "");
						return list;
					}
					CWLogger.Logger.Info("ExecuteUpdateManager::ExecUpdates: File(s) '{0}' were successfully updated", executeUpdatesResp.Filenames.CommaSeparatedValues<string>());
				}
			}
			return list;
		}
	}
}
