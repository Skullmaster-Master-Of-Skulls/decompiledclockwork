using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Jobs.ExecutingJobs
{
	// Token: 0x0200000A RID: 10
	[ClockWorkServerJobExecuting("Run a report", ControlParametersType = "CtrlClockWorkServerJobReportParameters")]
	public class ClockWorkServerRunReportJob : IClockWorkServerExecutingJob, IDisposable
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003ED3 File Offset: 0x000020D3
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003EDB File Offset: 0x000020DB
		protected ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002C4D File Offset: 0x00000E4D
		public string JobName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003EE4 File Offset: 0x000020E4
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003EEC File Offset: 0x000020EC
		private int ReportId { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003EF5 File Offset: 0x000020F5
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003EFD File Offset: 0x000020FD
		private IList<ReportParameter> ReportParameters { get; set; }

		// Token: 0x0600006D RID: 109 RVA: 0x00002C5A File Offset: 0x00000E5A
		public void Dispose()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F06 File Offset: 0x00002106
		public void Init(ServerInstanceInfo serverInstance, string parameters)
		{
			this.ServerInstance = serverInstance;
			this.ReportParameters = new List<ReportParameter>();
			this.ParseParameters(parameters);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003F24 File Offset: 0x00002124
		public ClockWorkServerJobRunningResult Run()
		{
			string executingPath = Path.Combine(this.ServerInstance.InstallationPath, "bin");
			RunReportResult runReportResult = ((IReportManager)new ReportManager(new OperationContext
			{
				WhoAmI = 0,
				AppContext = new ApplicationContext
				{
					ExecutingPath = executingPath
				}
			})).ExecuteReport2(this.ReportId, this.ReportParameters.ToArray<ReportParameter>());
			eRunStatusStep eRunStatusStep = (runReportResult != null && runReportResult.ReportStatus != null) ? runReportResult.ReportStatus.LastStatusStep : eRunStatusStep.Failed;
			if (eRunStatusStep == eRunStatusStep.CompletedSuccessfully)
			{
				return new ClockWorkServerJobRunningResult
				{
					JobName = this.JobName,
					Status = eClockWorkServerJobResult.Success,
					Message = string.Empty
				};
			}
			string str = (runReportResult != null && runReportResult.ReportStatus != null) ? (runReportResult.ReportStatus.ErrorMessage ?? string.Empty) : string.Empty;
			return new ClockWorkServerJobRunningResult
			{
				JobName = this.JobName,
				Status = eClockWorkServerJobResult.Error,
				Message = eRunStatusStep.ToString() + ": " + str
			};
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004024 File Offset: 0x00002224
		private void ParseParameters(string parameters)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			XElement xelement = XDocument.Load(new StringReader(parameters)).Element("ClockWorkServerJobReportParameters");
			if (xelement == null)
			{
				return;
			}
			XElement xelement2 = xelement.Element("ReportId");
			if (xelement2 == null)
			{
				return;
			}
			int reportId;
			if (!string.IsNullOrEmpty(xelement2.Value) && int.TryParse(xelement2.Value, out reportId))
			{
				this.ReportId = reportId;
			}
			XElement xelement3 = xelement.Element("OptionalReportParameters");
			if (xelement3 != null)
			{
				foreach (XElement xelement4 in from item in xelement3.Elements("Item")
				select item)
				{
					XAttribute xattribute = xelement4.Attribute("key");
					XAttribute xattribute2 = xelement4.Attribute("value");
					if (xattribute != null && xattribute2 != null)
					{
						dictionary.Add(xattribute.Value, xattribute2.Value);
					}
				}
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					this.ReportParameters.Add(new ReportParameter
					{
						Name = keyValuePair.Key,
						Value = keyValuePair.Value
					});
				}
			}
		}
	}
}
