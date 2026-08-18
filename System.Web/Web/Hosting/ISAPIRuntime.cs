using System;
using System.Security.Permissions;
using System.Threading;
using System.Web.Management;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x0200029E RID: 670
	public sealed class ISAPIRuntime : MarshalByRefObject, IISAPIRuntime, IRegisteredObject
	{
		// Token: 0x060022F3 RID: 8947 RVA: 0x00096877 File Offset: 0x00095877
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public ISAPIRuntime()
		{
			HostingEnvironment.RegisterObject(this);
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x00096885 File Offset: 0x00095885
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x00096888 File Offset: 0x00095888
		public void StartProcessing()
		{
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x0009688A File Offset: 0x0009588A
		public void StopProcessing()
		{
			HostingEnvironment.UnregisterObject(this);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x00096894 File Offset: 0x00095894
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

		// Token: 0x060022F8 RID: 8952 RVA: 0x00096988 File Offset: 0x00095988
		public void DoGCCollect()
		{
			for (int i = 10; i > 0; i--)
			{
				GC.Collect();
			}
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000969A7 File Offset: 0x000959A7
		void IRegisteredObject.Stop(bool immediate)
		{
			ISAPIRuntime.RemoveThisAppDomainFromUnmanagedTable();
			HostingEnvironment.UnregisterObject(this);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000969B4 File Offset: 0x000959B4
		internal static void RemoveThisAppDomainFromUnmanagedTable()
		{
			if (Interlocked.Exchange(ref ISAPIRuntime._isThisAppDomainRemovedFromUnmanagedTable, 1) != 0)
			{
				return;
			}
			try
			{
				string appDomainAppIdInternal = HttpRuntime.AppDomainAppIdInternal;
				if (appDomainAppIdInternal != null)
				{
					UnsafeNativeMethods.AppDomainRestart(appDomainAppIdInternal);
				}
				HttpRuntime.AddAppDomainTraceMessage(SR.GetString("App_Domain_Restart"));
			}
			catch
			{
			}
		}

		// Token: 0x04001B7B RID: 7035
		private const int WORKER_REQUEST_TYPE_IN_PROC = 0;

		// Token: 0x04001B7C RID: 7036
		private const int WORKER_REQUEST_TYPE_OOP = 1;

		// Token: 0x04001B7D RID: 7037
		private const int WORKER_REQUEST_TYPE_IN_PROC_VERSION_2 = 2;

		// Token: 0x04001B7E RID: 7038
		private static int _isThisAppDomainRemovedFromUnmanagedTable;
	}
}
