using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Transactions.Diagnostics;

namespace System.Transactions.Oletx
{
	// Token: 0x02000087 RID: 135
	internal class DtcTransactionManager
	{
		// Token: 0x0600036C RID: 876 RVA: 0x00037224 File Offset: 0x00036624
		internal DtcTransactionManager(string nodeName, OletxTransactionManager oletxTm)
		{
			this.nodeName = nodeName;
			this.oletxTm = oletxTm;
			this.initialized = false;
			this.proxyShimFactory = OletxTransactionManager.proxyShimFactory;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00037264 File Offset: 0x00036664
		private void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			OletxInternalResourceManager internalResourceManager = this.oletxTm.internalResourceManager;
			IntPtr intPtr = IntPtr.Zero;
			IResourceManagerShim resourceManagerShim = null;
			bool flag = false;
			CoTaskMemHandle coTaskMemHandle = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				intPtr = HandleTable.AllocHandle(internalResourceManager);
				this.proxyShimFactory.ConnectToProxy(this.nodeName, internalResourceManager.Identifier, intPtr, out flag, out this.whereaboutsSize, out coTaskMemHandle, out resourceManagerShim);
				if (!flag)
				{
					throw new NotSupportedException(SR.GetString("ProxyCannotSupportMultipleNodeNames"));
				}
				if (coTaskMemHandle != null && this.whereaboutsSize != 0U)
				{
					this.whereabouts = new byte[this.whereaboutsSize];
					Marshal.Copy(coTaskMemHandle.DangerousGetHandle(), this.whereabouts, 0, Convert.ToInt32(this.whereaboutsSize));
				}
				internalResourceManager.resourceManagerShim = resourceManagerShim;
				internalResourceManager.CallReenlistComplete();
				this.initialized = true;
			}
			catch (COMException ex)
			{
				if (NativeMethods.XACT_E_NOTSUPPORTED == ex.ErrorCode)
				{
					throw new NotSupportedException(SR.GetString("CannotSupportNodeNameSpecification"));
				}
				OletxTransactionManager.ProxyException(ex);
				throw TransactionManagerCommunicationException.Create(SR.GetString("TraceSourceOletx"), SR.GetString("TransactionManagerCommunicationException"), ex);
			}
			finally
			{
				if (coTaskMemHandle != null)
				{
					coTaskMemHandle.Close();
				}
				if (!this.initialized)
				{
					if (intPtr != IntPtr.Zero && resourceManagerShim == null)
					{
						HandleTable.FreeHandle(intPtr);
					}
					if (this.whereabouts != null)
					{
						this.whereabouts = null;
						this.whereaboutsSize = 0U;
					}
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000373E4 File Offset: 0x000367E4
		internal IDtcProxyShimFactory ProxyShimFactory
		{
			get
			{
				if (!this.initialized)
				{
					lock (this)
					{
						this.Initialize();
					}
				}
				return this.proxyShimFactory;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00037434 File Offset: 0x00036834
		internal void ReleaseProxy()
		{
			lock (this)
			{
				this.whereabouts = null;
				this.whereaboutsSize = 0U;
				this.initialized = false;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00037484 File Offset: 0x00036884
		internal byte[] Whereabouts
		{
			get
			{
				if (!this.initialized)
				{
					lock (this)
					{
						this.Initialize();
					}
				}
				return this.whereabouts;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000374D4 File Offset: 0x000368D4
		internal static uint AdjustTimeout(TimeSpan timeout)
		{
			uint result = 0U;
			try
			{
				result = Convert.ToUInt32(timeout.TotalMilliseconds, CultureInfo.CurrentCulture);
			}
			catch (OverflowException exception)
			{
				if (DiagnosticTrace.Verbose)
				{
					ExceptionConsumedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), exception);
				}
				result = uint.MaxValue;
			}
			return result;
		}

		// Token: 0x040001C6 RID: 454
		private string nodeName;

		// Token: 0x040001C7 RID: 455
		private OletxTransactionManager oletxTm;

		// Token: 0x040001C8 RID: 456
		private IDtcProxyShimFactory proxyShimFactory;

		// Token: 0x040001C9 RID: 457
		private uint whereaboutsSize;

		// Token: 0x040001CA RID: 458
		private byte[] whereabouts;

		// Token: 0x040001CB RID: 459
		private bool initialized;
	}
}
