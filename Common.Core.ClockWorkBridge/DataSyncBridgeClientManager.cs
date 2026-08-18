using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using TechnoPro.ClockWorkBridge.Contracts;
using TechnoPro.ClockWorkBridge.Contracts.DTO;
using TechnoPro.ClockWorkBridge.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.Common.ICore.ClockWorkBridge;
using TechnoPro.Common.WCF;

namespace TechnoPro.Common.Core.ClockWorkBridge.DataSyncBridge
{
	// Token: 0x02000002 RID: 2
	public class DataSyncBridgeClientManager : IDataSyncBridgeClientManager
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public IList<DataSyncBridgeStudentDataCollection> LoadStudentDataPreview(string Student_No)
		{
			IDataSyncBridge reusableInstance = WCFClientProxy<IDataSyncBridge>.GetReusableInstance(BindingHelper.GetNetNamedPipeBinding(new NetNamedBindingSettings(), NetNamedPipeSecurityMode.None), new EndpointAddress("net.pipe://localhost/ClockWorkBridge/DataSyncBridge.svc"));
			LoadStudentDataPreviewResp loadStudentDataPreviewResp = reusableInstance.LoadStudentDataPreview(new LoadStudentDataPreviewReq
			{
				Student_No = Student_No
			});
			return this.GetItemsFromStudentDataList(loadStudentDataPreviewResp.BridgeStudentDataSetRows);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000209C File Offset: 0x0000029C
		private IList<DataSyncBridgeStudentDataCollection> GetItemsFromStudentDataList(List<List<DataSyncBridgeStudentDataItem>> items)
		{
			List<DataSyncBridgeStudentDataCollection> result = new List<DataSyncBridgeStudentDataCollection>();
			foreach (List<DataSyncBridgeStudentDataItem> list in items)
			{
				List<DataSyncBridgeStudentDataItem> list2 = new List<DataSyncBridgeStudentDataItem>();
				foreach (DataSyncBridgeStudentDataItem dataSyncBridgeStudentDataItem in list)
				{
					list2.Add(new DataSyncBridgeStudentDataItem
					{
						Key = dataSyncBridgeStudentDataItem.Key,
						Value = dataSyncBridgeStudentDataItem.Value
					});
				}
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002154 File Offset: 0x00000354
		public DataSyncStudentDataAndCourses LoadStudentDataAndCourses(string Student_No)
		{
			IDataSyncBridge reusableInstance = WCFClientProxy<IDataSyncBridge>.GetReusableInstance(BindingHelper.GetNetNamedPipeBinding(new NetNamedBindingSettings(), NetNamedPipeSecurityMode.None), new EndpointAddress("net.pipe://localhost/ClockWorkBridge/DataSyncBridge.svc"));
			LoadStudentDataAndCoursesResp loadStudentDataAndCoursesResp = reusableInstance.LoadStudentDataAndCourses(new LoadStudentDataAndCoursesReq
			{
				Student_No = Student_No
			});
			return new DataSyncStudentDataAndCourses
			{
				StudentData = this.GetItemsFromStudentDataList(loadStudentDataAndCoursesResp.BridgeStudentDataSetRows),
				Courses = ((loadStudentDataAndCoursesResp.Courses == null) ? null : loadStudentDataAndCoursesResp.Courses.ToList<DataSyncBridgeStudentCourse>())
			};
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021C8 File Offset: 0x000003C8
		public bool MoveLookupDataIntoClockWork(string ClockWorkTableNameWithoutCustomPrefix, MoveLookupDataIntoClockWorkOptions Options, IDictionary<string, object> Args = null)
		{
			IDataSyncBridge reusableInstance = WCFClientProxy<IDataSyncBridge>.GetReusableInstance(BindingHelper.GetNetNamedPipeBinding(new NetNamedBindingSettings(), NetNamedPipeSecurityMode.None), new EndpointAddress("net.pipe://localhost/ClockWorkBridge/DataSyncBridge.svc"));
			List<DataSyncBridgeArgument> list;
			if (Args != null)
			{
				list = new List<DataSyncBridgeArgument>();
				using (IEnumerator<KeyValuePair<string, object>> enumerator = Args.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> keyValuePair = enumerator.Current;
						list.Add(new DataSyncBridgeArgument
						{
							Key = keyValuePair.Key,
							Value = keyValuePair.Value
						});
					}
					goto IL_76;
				}
			}
			list = null;
			IL_76:
			return reusableInstance.MoveLookupDataIntoClockWork(new MoveLookupDataIntoClockWorkReq
			{
				ClockWorkTableNameWithoutCustomPrefix = ClockWorkTableNameWithoutCustomPrefix,
				Options = Options,
				Args = list
			}).Worked;
		}

		// Token: 0x04000001 RID: 1
		private const string EndpointAddressUri = "net.pipe://localhost/ClockWorkBridge/DataSyncBridge.svc";
	}
}
