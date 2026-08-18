using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000271 RID: 625
	internal class TransactionProxyBuilder : IProxyCreator, IDisposable
	{
		// Token: 0x060011BE RID: 4542 RVA: 0x0004061D File Offset: 0x0003E81D
		private TransactionProxyBuilder(TransactionProxy proxy)
		{
			this.txProxy = proxy;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004062C File Offset: 0x0003E82C
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00040630 File Offset: 0x0003E830
		ComProxy IProxyCreator.CreateProxy(IntPtr outer, ref Guid riid)
		{
			if (riid != typeof(ITransactionProxy).GUID)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidCastException(SR.GetString("NoInterface", new object[]
				{
					riid
				})));
			}
			if (outer == IntPtr.Zero)
			{
				DiagnosticUtility.FailFast("OuterProxy cannot be null");
			}
			if (this.comProxy == null)
			{
				this.comProxy = ComProxy.Create(outer, this.txProxy, null);
				return this.comProxy;
			}
			return this.comProxy.Clone();
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x000406CC File Offset: 0x0003E8CC
		bool IProxyCreator.SupportsErrorInfo(ref Guid riid)
		{
			return !(riid != typeof(ITransactionProxy).GUID);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000406ED File Offset: 0x0003E8ED
		bool IProxyCreator.SupportsDispatch()
		{
			return false;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000406F0 File Offset: 0x0003E8F0
		bool IProxyCreator.SupportsIntrinsics()
		{
			return false;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000406F4 File Offset: 0x0003E8F4
		public static IntPtr CreateTransactionProxyTearOff(TransactionProxy txProxy)
		{
			IProxyCreator proxyCreator = new TransactionProxyBuilder(txProxy);
			IProxyManager proxyManager = new ProxyManager(proxyCreator);
			Guid guid = typeof(ITransactionProxy).GUID;
			return OuterProxyWrapper.CreateOuterProxyInstance(proxyManager, ref guid);
		}

		// Token: 0x040019A8 RID: 6568
		private ComProxy comProxy;

		// Token: 0x040019A9 RID: 6569
		private TransactionProxy txProxy;
	}
}
