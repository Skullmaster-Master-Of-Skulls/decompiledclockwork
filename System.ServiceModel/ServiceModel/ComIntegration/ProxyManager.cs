using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000243 RID: 579
	internal class ProxyManager : IProxyManager
	{
		// Token: 0x0600111B RID: 4379 RVA: 0x0003F00A File Offset: 0x0003D20A
		internal ProxyManager(IProxyCreator proxyCreator)
		{
			this.proxyCreator = proxyCreator;
			this.InterfaceIDToComProxy = new Dictionary<Guid, ComProxy>();
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0003F024 File Offset: 0x0003D224
		private bool IsIntrinsic(ref Guid riid)
		{
			return riid == typeof(IChannelOptions).GUID || riid == typeof(IChannelCredentials).GUID;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0003F064 File Offset: 0x0003D264
		void IProxyManager.TearDownChannels()
		{
			lock (this)
			{
				IEnumerator<KeyValuePair<Guid, ComProxy>> enumerator = this.InterfaceIDToComProxy.GetEnumerator();
				while (enumerator.MoveNext())
				{
					KeyValuePair<Guid, ComProxy> keyValuePair = enumerator.Current;
					IDisposable value = keyValuePair.Value;
					if (value != null)
					{
						value.Dispose();
					}
				}
				this.InterfaceIDToComProxy.Clear();
				this.proxyCreator.Dispose();
				enumerator.Dispose();
				this.proxyCreator = null;
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003F0F4 File Offset: 0x0003D2F4
		private ComProxy CreateServiceChannel(IntPtr outerProxy, ref Guid riid)
		{
			return this.proxyCreator.CreateProxy(outerProxy, ref riid);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0003F104 File Offset: 0x0003D304
		private ComProxy GenerateIntrinsic(IntPtr outerProxy, ref Guid riid)
		{
			if (!this.proxyCreator.SupportsIntrinsics())
			{
				throw Fx.AssertAndThrow("proxyCreator does not support intrinsic");
			}
			if (riid == typeof(IChannelOptions).GUID)
			{
				return ChannelOptions.Create(outerProxy, this.proxyCreator as IProvideChannelBuilderSettings);
			}
			if (riid == typeof(IChannelCredentials).GUID)
			{
				return ChannelCredentials.Create(outerProxy, this.proxyCreator as IProvideChannelBuilderSettings);
			}
			throw Fx.AssertAndThrow("Given IID is not an intrinsic");
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0003F190 File Offset: 0x0003D390
		private void FindOrCreateProxyInternal(IntPtr outerProxy, ref Guid riid, out ComProxy comProxy)
		{
			comProxy = null;
			lock (this)
			{
				this.InterfaceIDToComProxy.TryGetValue(riid, out comProxy);
				if (comProxy == null)
				{
					if (this.IsIntrinsic(ref riid))
					{
						comProxy = this.GenerateIntrinsic(outerProxy, ref riid);
					}
					else
					{
						comProxy = this.CreateServiceChannel(outerProxy, ref riid);
					}
					this.InterfaceIDToComProxy[riid] = comProxy;
				}
			}
			if (comProxy == null)
			{
				throw Fx.AssertAndThrow("comProxy should not be null at this point");
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0003F220 File Offset: 0x0003D420
		int IProxyManager.FindOrCreateProxy(IntPtr outerProxy, ref Guid riid, out IntPtr tearOff)
		{
			tearOff = IntPtr.Zero;
			int result;
			try
			{
				ComProxy comProxy = null;
				this.FindOrCreateProxyInternal(outerProxy, ref riid, out comProxy);
				comProxy.QueryInterface(ref riid, out tearOff);
				result = HR.S_OK;
			}
			catch (Exception baseException)
			{
				if (Fx.IsFatal(baseException))
				{
					throw;
				}
				baseException = baseException.GetBaseException();
				result = Marshal.GetHRForException(baseException);
			}
			return result;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003F27C File Offset: 0x0003D47C
		int IProxyManager.InterfaceSupportsErrorInfo(ref Guid riid)
		{
			if (this.IsIntrinsic(ref riid))
			{
				return HR.S_OK;
			}
			if (!this.proxyCreator.SupportsErrorInfo(ref riid))
			{
				return HR.S_FALSE;
			}
			return HR.S_OK;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0003F2A8 File Offset: 0x0003D4A8
		void IProxyManager.GetIDsOfNames([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr pDispID)
		{
			int val = -1;
			if (!(name == "ChannelOptions"))
			{
				if (name == "ChannelCredentials")
				{
					val = 2;
				}
			}
			else
			{
				val = 1;
			}
			Marshal.WriteInt32(pDispID, val);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0003F2E0 File Offset: 0x0003D4E0
		int IProxyManager.Invoke(uint dispIdMember, IntPtr outerProxy, IntPtr pVarResult, IntPtr pExcepInfo)
		{
			int result;
			try
			{
				ComProxy comProxy = null;
				Guid guid;
				if (dispIdMember == 1U)
				{
					guid = typeof(IChannelOptions).GUID;
				}
				else
				{
					if (dispIdMember != 2U)
					{
						return HR.DISP_E_MEMBERNOTFOUND;
					}
					guid = typeof(IChannelCredentials).GUID;
				}
				this.FindOrCreateProxyInternal(outerProxy, ref guid, out comProxy);
				TagVariant tagVariant = default(TagVariant);
				tagVariant.vt = 9;
				IntPtr zero = IntPtr.Zero;
				comProxy.QueryInterface(ref guid, out zero);
				tagVariant.ptr = zero;
				Marshal.StructureToPtr(tagVariant, pVarResult, true);
				result = HR.S_OK;
			}
			catch (Exception baseException)
			{
				if (Fx.IsFatal(baseException))
				{
					throw;
				}
				if (pExcepInfo != IntPtr.Zero)
				{
					System.Runtime.InteropServices.ComTypes.EXCEPINFO excepinfo = default(System.Runtime.InteropServices.ComTypes.EXCEPINFO);
					baseException = baseException.GetBaseException();
					excepinfo.bstrDescription = baseException.Message;
					excepinfo.bstrSource = baseException.Source;
					excepinfo.scode = Marshal.GetHRForException(baseException);
					Marshal.StructureToPtr(excepinfo, pExcepInfo, false);
				}
				result = HR.DISP_E_EXCEPTION;
			}
			return result;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0003F3F4 File Offset: 0x0003D5F4
		int IProxyManager.SupportsDispatch()
		{
			if (this.proxyCreator.SupportsDispatch())
			{
				return HR.S_OK;
			}
			return HR.E_FAIL;
		}

		// Token: 0x0400189D RID: 6301
		private Dictionary<Guid, ComProxy> InterfaceIDToComProxy;

		// Token: 0x0400189E RID: 6302
		private IProxyCreator proxyCreator;
	}
}
