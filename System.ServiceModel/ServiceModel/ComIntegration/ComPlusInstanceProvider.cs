using System;
using System.Diagnostics;
using System.EnterpriseServices;
using System.IdentityModel;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200020F RID: 527
	internal class ComPlusInstanceProvider : IInstanceProvider
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x0003957B File Offset: 0x0003777B
		public ComPlusInstanceProvider(ServiceInfo info)
		{
			this.info = info;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0003958A File Offset: 0x0003778A
		public object GetInstance(InstanceContext instanceContext)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ComPlusInstanceProviderRequiresMessage0")));
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000395A8 File Offset: 0x000377A8
		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			object obj = null;
			Guid incomingTransactionID = Guid.Empty;
			if (ContextUtil.IsInTransaction)
			{
				incomingTransactionID = ContextUtil.TransactionId;
			}
			ComPlusInstanceCreationTrace.Trace(TraceEventType.Verbose, 327698, "TraceCodeComIntegrationInstanceCreationRequest", this.info, message, incomingTransactionID);
			WindowsIdentity windowsIdentity = null;
			windowsIdentity = MessageUtil.GetMessageIdentity(message);
			WindowsImpersonationContext windowsImpersonationContext = null;
			try
			{
				try
				{
					if (this.info.HostingMode == HostingMode.WebHostOutOfProcess)
					{
						if (!SecurityUtils.IsAtleastImpersonationToken(new SafeCloseHandle(windowsIdentity.Token, false)))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("BadImpersonationLevelForOutOfProcWas"), HR.ERROR_BAD_IMPERSONATION_LEVEL));
						}
						windowsImpersonationContext = windowsIdentity.Impersonate();
					}
					CLSCTX clsctx = CLSCTX.SERVER;
					if (ComPlusInstanceProvider.PlatformSupportsBitness && this.info.HostingMode == HostingMode.WebHostOutOfProcess)
					{
						if (this.info.Bitness == Bitness.Bitness32)
						{
							clsctx |= CLSCTX.ACTIVATE_32_BIT_SERVER;
						}
						else
						{
							clsctx |= CLSCTX.ACTIVATE_64_BIT_SERVER;
						}
					}
					obj = SafeNativeMethods.CoCreateInstance(this.info.Clsid, null, clsctx, ComPlusInstanceProvider.IID_IUnknown);
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				Uri uri = null;
				if (message.Headers.From != null)
				{
					uri = message.Headers.From.Uri;
				}
				DiagnosticUtility.EventLog.LogEvent(TraceEventType.Error, 10, 3221356570U, new string[]
				{
					(uri == null) ? string.Empty : uri.ToString(),
					this.info.AppID.ToString(),
					this.info.Clsid.ToString(),
					incomingTransactionID.ToString(),
					windowsIdentity.Name,
					ex.ToString()
				});
				throw TraceUtility.ThrowHelperError(ex, message);
			}
			TransactionProxy transactionProxy = instanceContext.Extensions.Find<TransactionProxy>();
			if (transactionProxy != null)
			{
				transactionProxy.InstanceID = obj.GetHashCode();
			}
			ComPlusInstanceCreationTrace.Trace(TraceEventType.Verbose, 327699, "TraceCodeComIntegrationInstanceCreationSuccess", this.info, message, obj.GetHashCode(), incomingTransactionID);
			return obj;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000397C8 File Offset: 0x000379C8
		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			int hashCode = instance.GetHashCode();
			IDisposable disposable = instance as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			else
			{
				Marshal.ReleaseComObject(instance);
			}
			ComPlusInstanceCreationTrace.Trace(TraceEventType.Verbose, 327700, "TraceCodeComIntegrationInstanceReleased", this.info, instanceContext, hashCode);
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00039810 File Offset: 0x00037A10
		private static bool PlatformSupportsBitness
		{
			get
			{
				if (!ComPlusInstanceProvider.platformSupportsBitnessSet)
				{
					if (Environment.OSVersion.Version.Major > 5)
					{
						ComPlusInstanceProvider.platformSupportsBitness = true;
					}
					else if (Environment.OSVersion.Version.Major == 5)
					{
						if (Environment.OSVersion.Version.Minor > 2)
						{
							ComPlusInstanceProvider.platformSupportsBitness = true;
						}
						else if (Environment.OSVersion.Version.Minor == 2 && !string.IsNullOrEmpty(Environment.OSVersion.ServicePack))
						{
							ComPlusInstanceProvider.platformSupportsBitness = true;
						}
					}
					ComPlusInstanceProvider.platformSupportsBitnessSet = true;
				}
				return ComPlusInstanceProvider.platformSupportsBitness;
			}
		}

		// Token: 0x04001855 RID: 6229
		private ServiceInfo info;

		// Token: 0x04001856 RID: 6230
		private static readonly Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");

		// Token: 0x04001857 RID: 6231
		private static bool platformSupportsBitness;

		// Token: 0x04001858 RID: 6232
		private static bool platformSupportsBitnessSet;
	}
}
