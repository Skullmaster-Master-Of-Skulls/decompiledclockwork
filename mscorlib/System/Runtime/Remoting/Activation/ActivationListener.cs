using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200069F RID: 1695
	internal class ActivationListener : MarshalByRefObject, IActivator
	{
		// Token: 0x06003D54 RID: 15700 RVA: 0x000D2080 File Offset: 0x000D1080
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x000D2083 File Offset: 0x000D1083
		// (set) Token: 0x06003D56 RID: 15702 RVA: 0x000D2086 File Offset: 0x000D1086
		public virtual IActivator NextActivator
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06003D57 RID: 15703 RVA: 0x000D208D File Offset: 0x000D108D
		public virtual ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000D2094 File Offset: 0x000D1094
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null || RemotingServices.IsTransparentProxy(ctorMsg))
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.Properties["Permission"] = "allowed";
			string activationTypeName = ctorMsg.ActivationTypeName;
			if (!RemotingConfigHandler.IsActivationAllowed(activationTypeName))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_PermissionDenied"), new object[]
				{
					ctorMsg.ActivationTypeName
				}));
			}
			if (ctorMsg.ActivationType == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), new object[]
				{
					ctorMsg.ActivationTypeName
				}));
			}
			return ActivationServices.GetActivator().Activate(ctorMsg);
		}
	}
}
