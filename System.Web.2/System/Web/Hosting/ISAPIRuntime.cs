using System;
using System.Security.Permissions;
using System.Threading;
using System.Web.Management;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007C1 RID: 1985
	public sealed class ISAPIRuntime : MarshalByRefObject, IISAPIRuntime, IISAPIRuntime2, IRegisteredObject
	{
		// Token: 0x06005F19 RID: 24345 RVA: 0x000474BC File Offset: 0x000456BC
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public ISAPIRuntime()
		{
			HostingEnvironment.RegisterObject(this);
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x00006164 File Offset: 0x00004364
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public void StartProcessing()
		{
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x00148557 File Offset: 0x00146757
		void IISAPIRuntime2.StartProcessing()
		{
			this.StartProcessing();
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x00047683 File Offset: 0x00045883
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public void StopProcessing()
		{
			HostingEnvironment.UnregisterObject(this);
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x0014855F File Offset: 0x0014675F
		void IISAPIRuntime2.StopProcessing()
		{
			this.StopProcessing();
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x00148568 File Offset: 0x00146768
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public int ProcessRequest(IntPtr ecb, int iWRType)
		{
			IntPtr intPtr = IntPtr.Zero;
			if (iWRType == 2)
			{
				intPtr = ecb;
				ecb = UnsafeNativeMethods.GetEcb(intPtr);
			}
			ISAPIWorkerRequest isapiworkerRequest = null;
			int result;
			try
			{
				bool useOOP = iWRType == 1;
				isapiworkerRequest = ISAPIWorkerRequest.CreateWorkerRequest(ecb, useOOP);
				isapiworkerRequest.Initialize();
				string appPathTranslated = isapiworkerRequest.GetAppPathTranslated();
				string appDomainAppPathInternal = HttpRuntime.AppDomainAppPathInternal;
				if (appDomainAppPathInternal == null || StringUtil.EqualsIgnoreCase(appPathTranslated, appDomainAppPathInternal))
				{
					HttpRuntime.ProcessRequestNoDemand(isapiworkerRequest);
					result = 0;
				}
				else
				{
					HttpRuntime.ShutdownAppDomain(ApplicationShutdownReason.PhysicalApplicationPathChanged, SR.GetString("Hosting_Phys_Path_Changed", new object[]
					{
						appDomainAppPathInternal,
						appPathTranslated
					}));
					result = 1;
				}
			}
			catch (Exception ex)
			{
				try
				{
					WebBaseEvent.RaiseRuntimeError(ex, this);
				}
				catch
				{
				}
				if (isapiworkerRequest == null || !(isapiworkerRequest.Ecb == IntPtr.Zero))
				{
					throw;
				}
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.SetDoneWithSessionCalled(intPtr);
				}
				if (ex is ThreadAbortException)
				{
					Thread.ResetAbort();
				}
				result = 0;
			}
			return result;
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x00148658 File Offset: 0x00146858
		int IISAPIRuntime2.ProcessRequest(IntPtr ecb, int iWRType)
		{
			return this.ProcessRequest(ecb, iWRType);
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x00148664 File Offset: 0x00146864
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public void DoGCCollect()
		{
			for (int i = 10; i > 0; i--)
			{
				GC.Collect();
			}
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x00148683 File Offset: 0x00146883
		void IISAPIRuntime2.DoGCCollect()
		{
			this.DoGCCollect();
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x0014868B File Offset: 0x0014688B
		void IRegisteredObject.Stop(bool immediate)
		{
			ISAPIRuntime.RemoveThisAppDomainFromUnmanagedTable();
			HostingEnvironment.UnregisterObject(this);
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x00148698 File Offset: 0x00146898
		internal static void RemoveThisAppDomainFromUnmanagedTable()
		{
			if (Interlocked.Exchange(ref ISAPIRuntime._isThisAppDomainRemovedFromUnmanagedTable, 1) != 0)
			{
				return;
			}
			try
			{
				string appDomainAppId = HttpRuntime.AppDomainAppId;
				if (appDomainAppId != null)
				{
					UnsafeNativeMethods.AppDomainRestart(appDomainAppId);
				}
				HttpRuntime.AddAppDomainTraceMessage(SR.GetString("App_Domain_Restart"));
			}
			catch
			{
			}
		}

		// Token: 0x04003193 RID: 12691
		private const int WORKER_REQUEST_TYPE_IN_PROC = 0;

		// Token: 0x04003194 RID: 12692
		private const int WORKER_REQUEST_TYPE_OOP = 1;

		// Token: 0x04003195 RID: 12693
		private const int WORKER_REQUEST_TYPE_IN_PROC_VERSION_2 = 2;

		// Token: 0x04003196 RID: 12694
		private static int _isThisAppDomainRemovedFromUnmanagedTable;
	}
}
