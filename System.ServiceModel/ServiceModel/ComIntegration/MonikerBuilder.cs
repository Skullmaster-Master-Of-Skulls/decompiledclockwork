using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Services;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000237 RID: 567
	internal class MonikerBuilder : IProxyCreator, IDisposable
	{
		// Token: 0x060010EB RID: 4331 RVA: 0x0003E018 File Offset: 0x0003C218
		private MonikerBuilder()
		{
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0003E020 File Offset: 0x0003C220
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0003E024 File Offset: 0x0003C224
		ComProxy IProxyCreator.CreateProxy(IntPtr outer, ref Guid riid)
		{
			if (riid != typeof(IMoniker).GUID && riid != typeof(IParseDisplayName).GUID)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidCastException(SR.GetString("NoInterface", new object[]
				{
					riid
				})));
			}
			if (outer == IntPtr.Zero)
			{
				throw Fx.AssertAndThrow("OuterProxy cannot be null");
			}
			if (this.comProxy == null)
			{
				ServiceMonikerInternal serviceMonikerInternal = null;
				try
				{
					serviceMonikerInternal = new ServiceMonikerInternal();
					this.comProxy = ComProxy.Create(outer, serviceMonikerInternal, serviceMonikerInternal);
					return this.comProxy;
				}
				finally
				{
					if (this.comProxy == null && serviceMonikerInternal != null)
					{
						((IDisposable)serviceMonikerInternal).Dispose();
					}
				}
			}
			return this.comProxy.Clone();
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0003E108 File Offset: 0x0003C308
		bool IProxyCreator.SupportsErrorInfo(ref Guid riid)
		{
			return !(riid != typeof(IMoniker).GUID) || !(riid != typeof(IParseDisplayName).GUID);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003E145 File Offset: 0x0003C345
		bool IProxyCreator.SupportsDispatch()
		{
			return false;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003E148 File Offset: 0x0003C348
		bool IProxyCreator.SupportsIntrinsics()
		{
			return false;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0003E14C File Offset: 0x0003C34C
		public static MarshalByRefObject CreateMonikerInstance()
		{
			IProxyCreator proxyCreator = new MonikerBuilder();
			IProxyManager proxyManager = new ProxyManager(proxyCreator);
			Guid guid = typeof(IMoniker).GUID;
			IntPtr intPtr = OuterProxyWrapper.CreateOuterProxyInstance(proxyManager, ref guid);
			MarshalByRefObject result = EnterpriseServicesHelper.WrapIUnknownWithComObject(intPtr) as MarshalByRefObject;
			Marshal.Release(intPtr);
			return result;
		}

		// Token: 0x04001892 RID: 6290
		private ComProxy comProxy;
	}
}
