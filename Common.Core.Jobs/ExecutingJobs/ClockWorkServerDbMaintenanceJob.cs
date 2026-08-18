using System;
using System.IO;
using TechnoPro.Common.DAO.Database;
using TechnoPro.Common.DAO.Impl.Database;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.InstanceInfo;

namespace TechnoPro.Common.Core.Jobs.ExecutingJobs
{
	// Token: 0x02000009 RID: 9
	[ClockWorkServerJobExecuting("Db maintenance")]
	public class ClockWorkServerDbMaintenanceJob : IClockWorkServerExecutingJob, IDisposable
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003DCC File Offset: 0x00001FCC
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003DD4 File Offset: 0x00001FD4
		protected ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002C4D File Offset: 0x00000E4D
		public string JobName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003DDD File Offset: 0x00001FDD
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003DE5 File Offset: 0x00001FE5
		protected string DbScripts { get; set; }

		// Token: 0x06000062 RID: 98 RVA: 0x00002C5A File Offset: 0x00000E5A
		public void Dispose()
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003DEE File Offset: 0x00001FEE
		public void Init(ServerInstanceInfo serverInstance, string parameters)
		{
			this.ServerInstance = serverInstance;
			this.DbScripts = parameters;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003E00 File Offset: 0x00002000
		public ClockWorkServerJobRunningResult Run()
		{
			string executingPath = Path.Combine(this.ServerInstance.InstallationPath, "bin");
			IDatabaseDAO databaseDAO = new DatabaseDAO(new OperationContext
			{
				WhoAmI = 0,
				AppContext = new ApplicationContext
				{
					ExecutingPath = executingPath
				}
			});
			string[] array = this.DbScripts.Split(new string[]
			{
				"go\r\n",
				"GO\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 0 && array[array.Length - 1].ToLower().EndsWith("go"))
			{
				array[array.Length - 1] = array[array.Length - 1].Substring(0, array[array.Length - 1].Length - 2);
			}
			databaseDAO.ExecuteCommands(array, true);
			return new ClockWorkServerJobRunningResult
			{
				JobName = this.JobName,
				Status = eClockWorkServerJobResult.Success,
				Message = string.Empty
			};
		}
	}
}
