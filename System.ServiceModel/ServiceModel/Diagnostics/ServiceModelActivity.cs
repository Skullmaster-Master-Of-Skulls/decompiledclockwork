using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A92 RID: 2706
	internal class ServiceModelActivity : IDisposable
	{
		// Token: 0x06006B09 RID: 27401 RVA: 0x0018F02C File Offset: 0x0018D22C
		static ServiceModelActivity()
		{
			ServiceModelActivity.ActivityTypeNames[0] = "Unknown";
			ServiceModelActivity.ActivityTypeNames[1] = "Close";
			ServiceModelActivity.ActivityTypeNames[2] = "Construct";
			ServiceModelActivity.ActivityTypeNames[3] = "ExecuteUserCode";
			ServiceModelActivity.ActivityTypeNames[4] = "ListenAt";
			ServiceModelActivity.ActivityTypeNames[5] = "Open";
			ServiceModelActivity.ActivityTypeNames[6] = "Open";
			ServiceModelActivity.ActivityTypeNames[7] = "ProcessMessage";
			ServiceModelActivity.ActivityTypeNames[8] = "ProcessAction";
			ServiceModelActivity.ActivityTypeNames[9] = "ReceiveBytes";
			ServiceModelActivity.ActivityTypeNames[10] = "SecuritySetup";
			ServiceModelActivity.ActivityTypeNames[11] = "TransferToComPlus";
			ServiceModelActivity.ActivityTypeNames[12] = "WmiGetObject";
			ServiceModelActivity.ActivityTypeNames[13] = "WmiPutInstance";
		}

		// Token: 0x06006B0A RID: 27402 RVA: 0x0018F0F8 File Offset: 0x0018D2F8
		private ServiceModelActivity(Guid activityId)
		{
			this.activityId = activityId;
			this.previousActivity = ServiceModelActivity.Current;
		}

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06006B0B RID: 27403 RVA: 0x0018F112 File Offset: 0x0018D312
		private static string ActivityBoundaryDescription
		{
			get
			{
				if (ServiceModelActivity.activityBoundaryDescription == null)
				{
					ServiceModelActivity.activityBoundaryDescription = TraceSR.GetString("ActivityBoundary");
				}
				return ServiceModelActivity.activityBoundaryDescription;
			}
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x06006B0C RID: 27404 RVA: 0x0018F12F File Offset: 0x0018D32F
		internal ActivityType ActivityType
		{
			get
			{
				return this.activityType;
			}
		}

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x06006B0D RID: 27405 RVA: 0x0018F137 File Offset: 0x0018D337
		internal ServiceModelActivity PreviousActivity
		{
			get
			{
				return this.previousActivity;
			}
		}

		// Token: 0x06006B0E RID: 27406 RVA: 0x0018F13F File Offset: 0x0018D33F
		internal static Activity BoundOperation(ServiceModelActivity activity)
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			return ServiceModelActivity.BoundOperation(activity, false);
		}

		// Token: 0x06006B0F RID: 27407 RVA: 0x0018F151 File Offset: 0x0018D351
		internal static Activity BoundOperation(ServiceModelActivity activity, bool addTransfer)
		{
			if (activity != null)
			{
				return ServiceModelActivity.BoundOperationCore(activity, addTransfer);
			}
			return null;
		}

		// Token: 0x06006B10 RID: 27408 RVA: 0x0018F160 File Offset: 0x0018D360
		private static Activity BoundOperationCore(ServiceModelActivity activity, bool addTransfer)
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			ServiceModelActivity.TransferActivity transferActivity = null;
			if (activity != null)
			{
				transferActivity = ServiceModelActivity.TransferActivity.CreateActivity(activity.activityId, addTransfer);
				if (transferActivity != null)
				{
					transferActivity.SetPreviousServiceModelActivity(ServiceModelActivity.Current);
				}
				ServiceModelActivity.Current = activity;
			}
			return transferActivity;
		}

		// Token: 0x06006B11 RID: 27409 RVA: 0x0018F19D File Offset: 0x0018D39D
		internal static ServiceModelActivity CreateActivity()
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			return ServiceModelActivity.CreateActivity(Guid.NewGuid(), true);
		}

		// Token: 0x06006B12 RID: 27410 RVA: 0x0018F1B4 File Offset: 0x0018D3B4
		internal static ServiceModelActivity CreateActivity(bool autoStop)
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			ServiceModelActivity serviceModelActivity = ServiceModelActivity.CreateActivity(Guid.NewGuid(), true);
			if (serviceModelActivity != null)
			{
				serviceModelActivity.autoStop = autoStop;
			}
			return serviceModelActivity;
		}

		// Token: 0x06006B13 RID: 27411 RVA: 0x0018F1E4 File Offset: 0x0018D3E4
		internal static ServiceModelActivity CreateActivity(bool autoStop, string activityName, ActivityType activityType)
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			ServiceModelActivity result = ServiceModelActivity.CreateActivity(autoStop);
			ServiceModelActivity.Start(result, activityName, activityType);
			return result;
		}

		// Token: 0x06006B14 RID: 27412 RVA: 0x0018F20C File Offset: 0x0018D40C
		internal static ServiceModelActivity CreateAsyncActivity()
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			ServiceModelActivity serviceModelActivity = ServiceModelActivity.CreateActivity(true);
			if (serviceModelActivity != null)
			{
				serviceModelActivity.isAsync = true;
			}
			return serviceModelActivity;
		}

		// Token: 0x06006B15 RID: 27413 RVA: 0x0018F234 File Offset: 0x0018D434
		internal static ServiceModelActivity CreateBoundedActivity()
		{
			return ServiceModelActivity.CreateBoundedActivity(false);
		}

		// Token: 0x06006B16 RID: 27414 RVA: 0x0018F23C File Offset: 0x0018D43C
		internal static ServiceModelActivity CreateBoundedActivity(bool suspendCurrent)
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			ServiceModelActivity serviceModelActivity = ServiceModelActivity.Current;
			ServiceModelActivity serviceModelActivity2 = ServiceModelActivity.CreateActivity(true);
			if (serviceModelActivity2 != null)
			{
				serviceModelActivity2.activity = (ServiceModelActivity.TransferActivity)ServiceModelActivity.BoundOperation(serviceModelActivity2, true);
				serviceModelActivity2.activity.SetPreviousServiceModelActivity(serviceModelActivity);
				if (suspendCurrent)
				{
					serviceModelActivity2.autoResume = true;
				}
			}
			if (suspendCurrent && serviceModelActivity != null)
			{
				serviceModelActivity.Suspend();
			}
			return serviceModelActivity2;
		}

		// Token: 0x06006B17 RID: 27415 RVA: 0x0018F298 File Offset: 0x0018D498
		internal static ServiceModelActivity CreateBoundedActivity(Guid activityId)
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			ServiceModelActivity serviceModelActivity = ServiceModelActivity.CreateActivity(activityId, true);
			if (serviceModelActivity != null)
			{
				serviceModelActivity.activity = (ServiceModelActivity.TransferActivity)ServiceModelActivity.BoundOperation(serviceModelActivity, true);
			}
			return serviceModelActivity;
		}

		// Token: 0x06006B18 RID: 27416 RVA: 0x0018F2CC File Offset: 0x0018D4CC
		internal static ServiceModelActivity CreateBoundedActivityWithTransferInOnly(Guid activityId)
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			ServiceModelActivity serviceModelActivity = ServiceModelActivity.CreateActivity(activityId, true);
			if (serviceModelActivity != null)
			{
				if (FxTrace.Trace != null)
				{
					FxTrace.Trace.TraceTransfer(activityId);
				}
				serviceModelActivity.activity = (ServiceModelActivity.TransferActivity)ServiceModelActivity.BoundOperation(serviceModelActivity);
			}
			return serviceModelActivity;
		}

		// Token: 0x06006B19 RID: 27417 RVA: 0x0018F311 File Offset: 0x0018D511
		internal static ServiceModelActivity CreateLightWeightAsyncActivity(Guid activityId)
		{
			return new ServiceModelActivity(activityId);
		}

		// Token: 0x06006B1A RID: 27418 RVA: 0x0018F31C File Offset: 0x0018D51C
		internal static ServiceModelActivity CreateActivity(Guid activityId)
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			ServiceModelActivity serviceModelActivity = null;
			if (activityId != Guid.Empty)
			{
				serviceModelActivity = new ServiceModelActivity(activityId);
			}
			if (serviceModelActivity != null)
			{
				ServiceModelActivity.Current = serviceModelActivity;
			}
			return serviceModelActivity;
		}

		// Token: 0x06006B1B RID: 27419 RVA: 0x0018F354 File Offset: 0x0018D554
		internal static ServiceModelActivity CreateActivity(Guid activityId, bool autoStop)
		{
			if (!DiagnosticUtility.ShouldUseActivity)
			{
				return null;
			}
			ServiceModelActivity serviceModelActivity = ServiceModelActivity.CreateActivity(activityId);
			if (serviceModelActivity != null)
			{
				serviceModelActivity.autoStop = autoStop;
			}
			return serviceModelActivity;
		}

		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x06006B1C RID: 27420 RVA: 0x0018F37C File Offset: 0x0018D57C
		// (set) Token: 0x06006B1D RID: 27421 RVA: 0x0018F383 File Offset: 0x0018D583
		internal static ServiceModelActivity Current
		{
			get
			{
				return ServiceModelActivity.currentActivity;
			}
			private set
			{
				ServiceModelActivity.currentActivity = value;
			}
		}

		// Token: 0x06006B1E RID: 27422 RVA: 0x0018F38C File Offset: 0x0018D58C
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				try
				{
					if (this.activity != null)
					{
						this.activity.Dispose();
					}
					if (this.autoStop)
					{
						this.Stop();
					}
					if (this.autoResume && ServiceModelActivity.Current != null)
					{
						ServiceModelActivity.Current.Resume();
					}
				}
				finally
				{
					ServiceModelActivity.Current = this.previousActivity;
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x06006B1F RID: 27423 RVA: 0x0018F408 File Offset: 0x0018D608
		internal Guid Id
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x06006B20 RID: 27424 RVA: 0x0018F410 File Offset: 0x0018D610
		// (set) Token: 0x06006B21 RID: 27425 RVA: 0x0018F418 File Offset: 0x0018D618
		private ServiceModelActivity.ActivityState LastState
		{
			get
			{
				return this.lastState;
			}
			set
			{
				this.lastState = value;
			}
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x06006B22 RID: 27426 RVA: 0x0018F421 File Offset: 0x0018D621
		// (set) Token: 0x06006B23 RID: 27427 RVA: 0x0018F429 File Offset: 0x0018D629
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x06006B24 RID: 27428 RVA: 0x0018F432 File Offset: 0x0018D632
		internal void Resume()
		{
			if (this.LastState == ServiceModelActivity.ActivityState.Suspend)
			{
				this.LastState = ServiceModelActivity.ActivityState.Resume;
				this.TraceMilestone(TraceEventType.Resume);
			}
		}

		// Token: 0x06006B25 RID: 27429 RVA: 0x0018F44F File Offset: 0x0018D64F
		internal void Resume(string activityName)
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				this.name = activityName;
			}
			this.Resume();
		}

		// Token: 0x06006B26 RID: 27430 RVA: 0x0018F46B File Offset: 0x0018D66B
		internal static void Start(ServiceModelActivity activity, string activityName, ActivityType activityType)
		{
			if (activity != null && activity.LastState == ServiceModelActivity.ActivityState.Unknown)
			{
				activity.LastState = ServiceModelActivity.ActivityState.Start;
				activity.name = activityName;
				activity.activityType = activityType;
				activity.TraceMilestone(TraceEventType.Start);
			}
		}

		// Token: 0x06006B27 RID: 27431 RVA: 0x0018F498 File Offset: 0x0018D698
		internal void Stop()
		{
			int num = 0;
			if (this.isAsync)
			{
				num = Interlocked.Increment(ref this.stopCount);
			}
			if (this.LastState != ServiceModelActivity.ActivityState.Stop && (!this.isAsync || (this.isAsync && num >= 2)))
			{
				this.LastState = ServiceModelActivity.ActivityState.Stop;
				this.TraceMilestone(TraceEventType.Stop);
			}
		}

		// Token: 0x06006B28 RID: 27432 RVA: 0x0018F4EA File Offset: 0x0018D6EA
		internal static void Stop(ServiceModelActivity activity)
		{
			if (activity != null)
			{
				activity.Stop();
			}
		}

		// Token: 0x06006B29 RID: 27433 RVA: 0x0018F4F5 File Offset: 0x0018D6F5
		internal void Suspend()
		{
			if (this.LastState != ServiceModelActivity.ActivityState.Stop)
			{
				this.LastState = ServiceModelActivity.ActivityState.Suspend;
				this.TraceMilestone(TraceEventType.Suspend);
			}
		}

		// Token: 0x06006B2A RID: 27434 RVA: 0x0018F514 File Offset: 0x0018D714
		public override string ToString()
		{
			return this.Id.ToString();
		}

		// Token: 0x06006B2B RID: 27435 RVA: 0x0018F538 File Offset: 0x0018D738
		private void TraceMilestone(TraceEventType type)
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				if (FxTrace.Trace != null)
				{
					this.CallEtwMileStoneEvent(type, null);
				}
				if (DiagnosticUtility.DiagnosticTrace != null)
				{
					TraceUtility.TraceEventNoCheck(type, 131085, ServiceModelActivity.ActivityBoundaryDescription, null, ServiceModelActivity.ActivityBoundaryDescription, null);
					return;
				}
			}
			else
			{
				if (FxTrace.Trace != null)
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>(2);
					dictionary["ActivityName"] = this.Name;
					dictionary["ActivityType"] = ServiceModelActivity.ActivityTypeNames[(int)this.activityType];
					using ((DiagnosticUtility.ShouldUseActivity && Guid.Empty == this.activityId) ? null : Activity.CreateActivity(this.Id))
					{
						this.CallEtwMileStoneEvent(type, new DictionaryTraceRecord(dictionary));
					}
				}
				if (DiagnosticUtility.DiagnosticTrace != null)
				{
					Dictionary<string, string> dictionary2 = new Dictionary<string, string>(2);
					dictionary2["ActivityName"] = this.Name;
					dictionary2["ActivityType"] = ServiceModelActivity.ActivityTypeNames[(int)this.activityType];
					TraceUtility.TraceEventNoCheck(type, 131085, ServiceModelActivity.ActivityBoundaryDescription, new DictionaryTraceRecord(dictionary2), null, null, this.Id);
				}
			}
		}

		// Token: 0x06006B2C RID: 27436 RVA: 0x0018F660 File Offset: 0x0018D860
		private void CallEtwMileStoneEvent(TraceEventType type, DictionaryTraceRecord record)
		{
			if (type <= TraceEventType.Stop)
			{
				if (type != TraceEventType.Start)
				{
					if (type != TraceEventType.Stop)
					{
						return;
					}
					if (TD.StopSignpostEventIsEnabled())
					{
						TD.StopSignpostEvent(record);
						return;
					}
				}
				else if (TD.StartSignpostEventIsEnabled())
				{
					TD.StartSignpostEvent(record);
					return;
				}
			}
			else if (type != TraceEventType.Suspend)
			{
				if (type != TraceEventType.Resume)
				{
					return;
				}
				if (TD.ResumeSignpostEventIsEnabled())
				{
					TD.ResumeSignpostEvent(record);
				}
			}
			else if (TD.SuspendSignpostEventIsEnabled())
			{
				TD.SuspendSignpostEvent(record);
				return;
			}
		}

		// Token: 0x04003CCA RID: 15562
		[ThreadStatic]
		private static ServiceModelActivity currentActivity;

		// Token: 0x04003CCB RID: 15563
		private static string[] ActivityTypeNames = new string[14];

		// Token: 0x04003CCC RID: 15564
		private ServiceModelActivity previousActivity;

		// Token: 0x04003CCD RID: 15565
		private static string activityBoundaryDescription = null;

		// Token: 0x04003CCE RID: 15566
		private ServiceModelActivity.ActivityState lastState;

		// Token: 0x04003CCF RID: 15567
		private string name;

		// Token: 0x04003CD0 RID: 15568
		private bool autoStop;

		// Token: 0x04003CD1 RID: 15569
		private bool autoResume;

		// Token: 0x04003CD2 RID: 15570
		private Guid activityId;

		// Token: 0x04003CD3 RID: 15571
		private bool disposed;

		// Token: 0x04003CD4 RID: 15572
		private bool isAsync;

		// Token: 0x04003CD5 RID: 15573
		private int stopCount;

		// Token: 0x04003CD6 RID: 15574
		private const int AsyncStopCount = 2;

		// Token: 0x04003CD7 RID: 15575
		private ServiceModelActivity.TransferActivity activity;

		// Token: 0x04003CD8 RID: 15576
		private ActivityType activityType;

		// Token: 0x02000EBE RID: 3774
		private enum ActivityState
		{
			// Token: 0x04004C71 RID: 19569
			Unknown,
			// Token: 0x04004C72 RID: 19570
			Start,
			// Token: 0x04004C73 RID: 19571
			Suspend,
			// Token: 0x04004C74 RID: 19572
			Resume,
			// Token: 0x04004C75 RID: 19573
			Stop
		}

		// Token: 0x02000EBF RID: 3775
		private class TransferActivity : Activity
		{
			// Token: 0x06008469 RID: 33897 RVA: 0x001E97AC File Offset: 0x001E79AC
			private TransferActivity(Guid activityId, Guid parentId) : base(activityId, parentId)
			{
			}

			// Token: 0x0600846A RID: 33898 RVA: 0x001E97B8 File Offset: 0x001E79B8
			internal static ServiceModelActivity.TransferActivity CreateActivity(Guid activityId, bool addTransfer)
			{
				if (!DiagnosticUtility.ShouldUseActivity)
				{
					return null;
				}
				ServiceModelActivity.TransferActivity result = null;
				if (DiagnosticUtility.TracingEnabled && activityId != Guid.Empty)
				{
					Guid activityId2 = DiagnosticTraceBase.ActivityId;
					if (activityId != activityId2)
					{
						if (addTransfer && FxTrace.Trace != null)
						{
							FxTrace.Trace.TraceTransfer(activityId);
						}
						result = new ServiceModelActivity.TransferActivity(activityId, activityId2)
						{
							addTransfer = addTransfer
						};
					}
				}
				return result;
			}

			// Token: 0x0600846B RID: 33899 RVA: 0x001E981A File Offset: 0x001E7A1A
			internal void SetPreviousServiceModelActivity(ServiceModelActivity previous)
			{
				this.previousActivity = previous;
				this.changeCurrentServiceModelActivity = true;
			}

			// Token: 0x0600846C RID: 33900 RVA: 0x001E982C File Offset: 0x001E7A2C
			public override void Dispose()
			{
				try
				{
					if (this.addTransfer)
					{
						using (Activity.CreateActivity(base.Id))
						{
							if (FxTrace.Trace != null)
							{
								FxTrace.Trace.TraceTransfer(this.parentId);
							}
						}
					}
				}
				finally
				{
					if (this.changeCurrentServiceModelActivity)
					{
						ServiceModelActivity.Current = this.previousActivity;
					}
					base.Dispose();
				}
			}

			// Token: 0x04004C76 RID: 19574
			private bool addTransfer;

			// Token: 0x04004C77 RID: 19575
			private bool changeCurrentServiceModelActivity;

			// Token: 0x04004C78 RID: 19576
			private ServiceModelActivity previousActivity;
		}
	}
}
