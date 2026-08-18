using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Institution;
using TechnoPro.Common.Core.Updates.Adapters;
using TechnoPro.Common.DAO.Impl.Updates;
using TechnoPro.Common.DAO.Updates;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.Updates.Adapters;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x02000009 RID: 9
	[ExecuterFileType("ClockWork update")]
	internal class ClockWorkUpdateExecuter : IUpdateExecuter
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000290B File Offset: 0x00000B0B
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002913 File Offset: 0x00000B13
		private IUpdateDAO UpdateDAO { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000291C File Offset: 0x00000B1C
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002924 File Offset: 0x00000B24
		public string Name { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002930 File Offset: 0x00000B30
		public int ExecutionOrder
		{
			get
			{
				return UpdateFileTypes.UpdateFileTypesList.IndexOf(this.ExecutingFileType());
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002952 File Offset: 0x00000B52
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000295A File Offset: 0x00000B5A
		public ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002963 File Offset: 0x00000B63
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000296B File Offset: 0x00000B6B
		public IExternalLogManager ExternalLogManager { get; set; }

		// Token: 0x06000037 RID: 55 RVA: 0x00002974 File Offset: 0x00000B74
		public ClockWorkUpdateExecuter()
		{
			this.UpdateDAO = new UpdateDAO();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000298C File Offset: 0x00000B8C
		public ExecuteUpdatesResp ExecuteUpdate()
		{
			ExecuteUpdatesResp result;
			try
			{
				bool flag = !string.IsNullOrEmpty(this.ServerInstance.InstallationPath);
				if (flag)
				{
					string text = Path.Combine(this.ServerInstance.InstallationPath, "FileSystem Storage");
					string updates_PATH = ClockWorkUpdateSystemPathVariables.UPDATES_PATH;
					IList<UpdateStatus> executionStatus = this.UpdateDAO.GetExecutionStatus();
					InstitutionManager institutionManager = new InstitutionManager();
					string institutionUniqueName = institutionManager.GetInstitutionUniqueName();
					List<string> list = new List<string>();
					string text2 = (this.ServerInstance.Version != null) ? this.ServerInstance.Version.FormatVersion() : null;
					Version v = string.IsNullOrEmpty(text2) ? null : new Version(text2);
					foreach (UpdateStatus updateStatus in from u in executionStatus
					where u.Status == eUpdateStatus.OnSchedule.ToString() && u.FileType == "ClockWork update"
					select u)
					{
						bool flag2 = updateStatus.Filename.IsHotFix();
						if (flag2)
						{
							string version = updateStatus.Filename.GetVersion().FormatVersion();
							Version v2 = new Version(version);
							bool flag3 = v != null && v >= v2;
							if (flag3)
							{
								updateStatus.Status = eUpdateStatus.Dismiss.ToString();
								this.UpdateDAO.SaveExecutionStatus(updateStatus);
								continue;
							}
						}
						list.Add(updateStatus.Filename);
						UpdateManager.DeletePreviousInstallers(updateStatus.Filename, text);
						string path = Path.Combine(updates_PATH, updateStatus.IsPublic ? "Public" : institutionUniqueName);
						string fileName = Path.Combine(path, updateStatus.Filename);
						FileInfo fileInfo = new FileInfo(fileName);
						fileInfo.CopyTo(Path.Combine(text, updateStatus.Filename), true);
						updateStatus.Status = eUpdateStatus.Done.ToString();
						this.UpdateDAO.SaveExecutionStatus(updateStatus);
						this.ExternalLogManager.Log(string.Format("ClockWork '{0}' was successfully installed on {1}", updateStatus.Filename, DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt")));
					}
					ExecuteUpdatesResp executeUpdatesResp;
					if (list.Count <= 0)
					{
						(executeUpdatesResp = new ExecuteUpdatesResp()).ExecuteUpdatesStatus = eExecuteUpdateStatus.UpToDate;
					}
					else
					{
						ExecuteUpdatesResp executeUpdatesResp2 = new ExecuteUpdatesResp();
						executeUpdatesResp2.ExecuteUpdatesStatus = eExecuteUpdateStatus.Updated;
						executeUpdatesResp = executeUpdatesResp2;
						executeUpdatesResp2.Filenames = list;
					}
					result = executeUpdatesResp;
				}
				else
				{
					result = new ExecuteUpdatesResp
					{
						ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
						LastError = "Unable to find ClockWork Server intallation path"
					};
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ClockWorkUpdateExecuter::ExecuteUpdate:: {0}", ex.ToString()), ex);
				result = new ExecuteUpdatesResp
				{
					ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
					LastError = ex.Message
				};
			}
			return result;
		}
	}
}
