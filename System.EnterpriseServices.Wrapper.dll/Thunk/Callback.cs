using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000054 RID: 84
	internal class Callback
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00003848 File Offset: 0x00002C48
		private unsafe static int CallbackFunction(tagComCallData* pData)
		{
			UserCallData userCallData = null;
			bool flag = false;
			try
			{
				IntPtr pinned = new IntPtr(*(long*)(pData + 8L / (long)sizeof(tagComCallData)));
				userCallData = UserCallData.Get(pinned);
				IProxyInvoke proxyInvoke = (IProxyInvoke)RemotingServices.GetRealProxy(userCallData.otp);
				userCallData.msg = proxyInvoke.LocalInvoke(userCallData.msg);
			}
			catch (Exception except)
			{
				flag = true;
				if (userCallData != null)
				{
					userCallData.except = except;
				}
			}
			catch
			{
				flag = true;
			}
			IMethodReturnMessage methodReturnMessage = userCallData.msg as IMethodReturnMessage;
			uint result;
			if ((methodReturnMessage != null && methodReturnMessage.Exception != null) || flag)
			{
				if (userCallData != null && userCallData.fIsAutoDone)
				{
					IUnknown* pDestCtx = userCallData.pDestCtx;
					IObjectContext* ptr = null;
					if (calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pDestCtx, ref <Module>.IID_IObjectContext, ref ptr, *(*(long*)pDestCtx)) >= 0)
					{
						IObjectContext* ptr2 = ptr;
						object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 40L));
						IObjectContext* ptr3 = ptr;
						object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
					}
				}
				result = 2148734208U;
			}
			else
			{
				result = 0U;
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003958 File Offset: 0x00002D58
		private unsafe static int MarshalCallback(tagComCallData* pData)
		{
			IntPtr pinned = new IntPtr(*(long*)(pData + 8L / (long)sizeof(tagComCallData)));
			UserMarshalData userMarshalData = UserMarshalData.Get(pinned);
			uint num = 0U;
			IUnknown* ptr = userMarshalData.pUnk.ToInt64();
			int num2 = <Module>.CoGetMarshalSizeMax((uint*)(&num), ref <Module>.IID_IUnknown, ptr, 3, null, 0);
			if (num2 >= 0)
			{
				num = (uint)((ulong)num + 4UL);
				try
				{
					userMarshalData.buffer = new byte[num];
				}
				catch (OutOfMemoryException)
				{
					num2 = -2147024882;
				}
				if (num2 >= 0)
				{
					fixed (byte* ptr2 = &userMarshalData.buffer[0])
					{
						try
						{
							num2 = <Module>.MarshalInterface(ptr2, (int)num, ptr, 3, 0);
						}
						catch
						{
							fixed (byte* ptr2 = null)
							{
							}
							throw;
						}
						fixed (byte* ptr2 = null)
						{
						}
					}
				}
			}
			return num2;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B18 File Offset: 0x00002F18
		public unsafe IMessage DoCallback(object otp, IMessage msg, IntPtr ctx, [MarshalAs(UnmanagedType.U1)] bool fIsAutoDone, MemberInfo mb, [MarshalAs(UnmanagedType.U1)] bool bHasGit)
		{
			Proxy.Init();
			IUnknown* ptr = null;
			IContextCallback* ptr2 = null;
			IMessage result = null;
			UserCallData userCallData = null;
			tagComCallData2 tagComCallData = 0;
			*(ref tagComCallData + 4) = 0;
			*(ref tagComCallData + 8) = 0L;
			*(ref tagComCallData + 16) = Callback._pfn;
			try
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(otp);
				if (bHasGit)
				{
					ptr = realProxy.GetCOMIUnknown(false).ToInt64();
				}
				IUnknown* ptr3 = ctx.ToInt64();
				int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr3, ref <Module>.IID_IContextCallback, ref ptr2, *(*(long*)ptr3));
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				int num2 = fIsAutoDone ? 7 : 8;
				_GUID iid_IRemoteDispatch = <Module>.IID_IRemoteDispatch;
				Type reflectedType = mb.ReflectedType;
				if (reflectedType.IsInterface)
				{
					Guid guid = Marshal.GenerateGuidForType(reflectedType);
					cpblk(ref iid_IRemoteDispatch, ref guid, 16);
					num2 = Marshal.GetComSlotForMethodInfo(mb);
				}
				userCallData = new UserCallData(otp, msg, ctx, fIsAutoDone, mb);
				IntPtr intPtr = userCallData.Pin();
				*(ref tagComCallData + 8) = intPtr.ToInt64();
				int num3 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.EnterpriseServices.Thunk.tagComCallData*),System.EnterpriseServices.Thunk.tagComCallData*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Int32,IUnknown*), ptr2, <Module>.__unep@?FilteringCallbackFunction@Thunk@EnterpriseServices@System@@$$FYAJPEAUtagComCallData@123@@Z, ref tagComCallData, ref iid_IRemoteDispatch, num2, ptr, *(*(long*)ptr2 + 24L));
				result = userCallData.msg;
				object except = userCallData.except;
				if (except != null)
				{
					throw except;
				}
				if (num3 < 0 && num3 != -2146233088)
				{
					Marshal.ThrowExceptionForHR(num3);
				}
			}
			finally
			{
				if (*(ref tagComCallData + 8) != 0L)
				{
					IntPtr pinned = new IntPtr(*(ref tagComCallData + 8));
					userCallData.Unpin(pinned);
				}
				if (ptr != null)
				{
					IUnknown* ptr4 = ptr;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
				}
				if (ptr2 != null)
				{
					IContextCallback* ptr5 = ptr2;
					object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr5, *(*(long*)ptr5 + 16L));
				}
			}
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003A18 File Offset: 0x00002E18
		public unsafe byte[] SwitchMarshal(IntPtr ctx, IntPtr pUnk)
		{
			Proxy.Init();
			byte[] result = null;
			IUnknown* ptr = pUnk.ToInt64();
			IContextCallback* ptr2 = null;
			UserMarshalData userMarshalData = null;
			tagComCallData2 tagComCallData = 0;
			*(ref tagComCallData + 4) = 0;
			*(ref tagComCallData + 8) = 0L;
			*(ref tagComCallData + 16) = Callback._pfnMarshal;
			try
			{
				IUnknown* ptr3 = ctx.ToInt64();
				int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr3, ref <Module>.IID_IContextCallback, ref ptr2, *(*(long*)ptr3));
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				userMarshalData = new UserMarshalData(pUnk);
				IntPtr intPtr = userMarshalData.Pin();
				*(ref tagComCallData + 8) = intPtr.ToInt64();
				int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.EnterpriseServices.Thunk.tagComCallData*),System.EnterpriseServices.Thunk.tagComCallData*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Int32,IUnknown*), ptr2, <Module>.__unep@?FilteringCallbackFunction@Thunk@EnterpriseServices@System@@$$FYAJPEAUtagComCallData@123@@Z, ref tagComCallData, ref <Module>.IID_IUnknown, 2, ptr, *(*(long*)ptr2 + 24L));
				if (num2 < 0)
				{
					Marshal.ThrowExceptionForHR(num2);
				}
				result = userMarshalData.buffer;
			}
			finally
			{
				if (*(ref tagComCallData + 8) != 0L)
				{
					IntPtr pinned = new IntPtr(*(ref tagComCallData + 8));
					userMarshalData.Unpin(pinned);
				}
				if (ptr2 != null)
				{
					IContextCallback* ptr4 = ptr2;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
				}
			}
			return result;
		}

		// Token: 0x04000113 RID: 275
		private static ContextCallbackFunction _cb = new ContextCallbackFunction(Callback.CallbackFunction);

		// Token: 0x04000114 RID: 276
		private static method _pfn = Marshal.GetFunctionPointerForDelegate(Callback._cb).ToPointer();

		// Token: 0x04000115 RID: 277
		private static ContextCallbackFunction _cbMarshal = new ContextCallbackFunction(Callback.MarshalCallback);

		// Token: 0x04000116 RID: 278
		private static method _pfnMarshal = Marshal.GetFunctionPointerForDelegate(Callback._cbMarshal).ToPointer();
	}
}
