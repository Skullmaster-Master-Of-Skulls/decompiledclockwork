using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Runtime;
using System.ServiceModel.Configuration;
using System.Threading;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200021C RID: 540
	internal class DllHostInitializeWorker
	{
		// Token: 0x06001065 RID: 4197 RVA: 0x0003C7C4 File Offset: 0x0003A9C4
		public static void PingProc(object o)
		{
			IProcessInitControl processInitControl = o as IProcessInitControl;
			try
			{
				for (int i = 0; i < 200; i++)
				{
					Thread.Sleep(10000);
					processInitControl.ResetInitializerTimeout(30);
				}
			}
			catch (ThreadAbortException)
			{
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003C810 File Offset: 0x0003AA10
		public void Startup(IProcessInitControl control)
		{
			this.applicationId = ContextUtil.ApplicationId;
			ComPlusDllHostInitializerTrace.Trace(TraceEventType.Information, 327688, "TraceCodeComIntegrationDllHostInitializerStarting", this.applicationId);
			Thread thread = null;
			try
			{
				thread = new Thread(new ParameterizedThreadStart(DllHostInitializeWorker.PingProc));
				thread.Start(control);
				ComCatalogObject comCatalogObject = CatalogUtil.FindApplication(this.applicationId);
				if (comCatalogObject == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("ApplicationNotFound", new object[]
					{
						this.applicationId.ToString("B").ToUpperInvariant()
					})));
				}
				bool flag = (int)comCatalogObject.GetValue("ConcurrentApps") > 1;
				if (flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("PooledApplicationNotSupportedForComplusHostedScenarios", new object[]
					{
						this.applicationId.ToString("B").ToUpperInvariant()
					})));
				}
				bool flag2 = (int)comCatalogObject.GetValue("RecycleLifetimeLimit") > 0 || (int)comCatalogObject.GetValue("RecycleCallLimit") > 0 || (int)comCatalogObject.GetValue("RecycleActivationLimit") > 0 || (int)comCatalogObject.GetValue("RecycleMemoryLimit") > 0;
				if (flag2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("RecycledApplicationNotSupportedForComplusHostedScenarios", new object[]
					{
						this.applicationId.ToString("B").ToUpperInvariant()
					})));
				}
				ComCatalogCollection collection = comCatalogObject.GetCollection("Components");
				ServicesSection section = ServicesSection.GetSection();
				bool flag3 = false;
				foreach (object obj in section.Services)
				{
					ServiceElement serviceElement = (ServiceElement)obj;
					Guid empty = Guid.Empty;
					Guid empty2 = Guid.Empty;
					string[] array = serviceElement.Name.Split(new char[]
					{
						','
					});
					if (array.Length != 2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OnlyClsidsAllowedForServiceType", new object[]
						{
							serviceElement.Name
						})));
					}
					if (!DiagnosticUtility.Utility.TryCreateGuid(array[0], out empty2))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OnlyClsidsAllowedForServiceType", new object[]
						{
							serviceElement.Name
						})));
					}
					if (!DiagnosticUtility.Utility.TryCreateGuid(array[1], out empty))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OnlyClsidsAllowedForServiceType", new object[]
						{
							serviceElement.Name
						})));
					}
					flag3 = false;
					foreach (ComCatalogObject comCatalogObject2 in collection)
					{
						Guid guid = Fx.CreateGuid((string)comCatalogObject2.GetValue("CLSID"));
						if (guid == empty && this.applicationId == empty2)
						{
							flag3 = true;
							ComPlusDllHostInitializerTrace.Trace(TraceEventType.Verbose, 327689, "TraceCodeComIntegrationDllHostInitializerAddingHost", this.applicationId, guid, serviceElement);
							this.hosts.Add(new DllHostedComPlusServiceHost(guid, serviceElement, comCatalogObject, comCatalogObject2));
						}
					}
					if (!flag3)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotFindClsidInApplication", new object[]
						{
							empty.ToString("B").ToUpperInvariant(),
							this.applicationId.ToString("B").ToUpperInvariant()
						})));
					}
				}
				if (!flag3)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.DllHostInitializerFoundNoServices());
				}
				foreach (ComPlusServiceHost comPlusServiceHost in this.hosts)
				{
					comPlusServiceHost.Open();
				}
			}
			catch (Exception ex)
			{
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 10, 3221356567U, new string[]
				{
					this.applicationId.ToString(),
					ex.ToString()
				});
				throw;
			}
			finally
			{
				if (thread != null)
				{
					thread.Abort();
				}
			}
			ComPlusDllHostInitializerTrace.Trace(TraceEventType.Information, 327690, "TraceCodeComIntegrationDllHostInitializerStarted", this.applicationId);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0003CC88 File Offset: 0x0003AE88
		public void Shutdown()
		{
			ComPlusDllHostInitializerTrace.Trace(TraceEventType.Information, 327691, "TraceCodeComIntegrationDllHostInitializerStopping", this.applicationId);
			foreach (ComPlusServiceHost comPlusServiceHost in this.hosts)
			{
				comPlusServiceHost.Close();
			}
			ComPlusDllHostInitializerTrace.Trace(TraceEventType.Information, 327692, "TraceCodeComIntegrationDllHostInitializerStopped", this.applicationId);
		}

		// Token: 0x04001878 RID: 6264
		private List<ComPlusServiceHost> hosts = new List<ComPlusServiceHost>();

		// Token: 0x04001879 RID: 6265
		private Guid applicationId;
	}
}
