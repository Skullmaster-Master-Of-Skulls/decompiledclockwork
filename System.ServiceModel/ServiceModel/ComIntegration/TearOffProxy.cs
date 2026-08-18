using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000270 RID: 624
	internal class TearOffProxy : RealProxy, IDisposable
	{
		// Token: 0x060011BB RID: 4539 RVA: 0x000404B9 File Offset: 0x0003E6B9
		internal TearOffProxy(ICreateServiceChannel serviceChannelCreator, Type proxiedType) : base(proxiedType)
		{
			if (serviceChannelCreator == null)
			{
				throw Fx.AssertAndThrow("ServiceChannelCreator cannot be null");
			}
			this.serviceChannelCreator = serviceChannelCreator;
			this.baseTypeToInterfaceMethod = new Dictionary<MethodBase, MethodBase>();
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000404E4 File Offset: 0x0003E6E4
		public override IMessage Invoke(IMessage message)
		{
			RealProxy realProxy = null;
			IMethodCallMessage methodCallMessage = message as IMethodCallMessage;
			try
			{
				realProxy = this.serviceChannelCreator.CreateChannel();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				return new ReturnMessage(DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(ex.GetBaseException().Message, Marshal.GetHRForException(ex.GetBaseException()))), methodCallMessage);
			}
			MethodBase methodBase = methodCallMessage.MethodBase;
			IRemotingTypeInfo remotingTypeInfo = realProxy as IRemotingTypeInfo;
			if (remotingTypeInfo == null)
			{
				throw Fx.AssertAndThrow("Type Info cannot be null");
			}
			if (!remotingTypeInfo.CanCastTo(methodBase.DeclaringType, null))
			{
				return new ReturnMessage(DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("OperationNotFound", new object[]
				{
					methodBase.Name
				}), HR.DISP_E_UNKNOWNNAME)), methodCallMessage);
			}
			IMessage message2 = realProxy.Invoke(message);
			ReturnMessage returnMessage = message2 as ReturnMessage;
			if (returnMessage == null || returnMessage.Exception == null)
			{
				return message2;
			}
			return new ReturnMessage(DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(returnMessage.Exception.GetBaseException().Message, Marshal.GetHRForException(returnMessage.Exception.GetBaseException()))), methodCallMessage);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00040614 File Offset: 0x0003E814
		void IDisposable.Dispose()
		{
			this.serviceChannelCreator = null;
		}

		// Token: 0x040019A6 RID: 6566
		private ICreateServiceChannel serviceChannelCreator;

		// Token: 0x040019A7 RID: 6567
		private Dictionary<MethodBase, MethodBase> baseTypeToInterfaceMethod;
	}
}
