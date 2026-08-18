using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000739 RID: 1849
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ProxyAttribute : Attribute, IContextAttribute
	{
		// Token: 0x0600423D RID: 16957 RVA: 0x000E14A7 File Offset: 0x000E04A7
		public virtual MarshalByRefObject CreateInstance(Type serverType)
		{
			if (!serverType.IsContextful)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
			}
			if (serverType.IsAbstract)
			{
				throw new RemotingException(Environment.GetResourceString("Acc_CreateAbst"));
			}
			return this.CreateInstanceInternal(serverType);
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x000E14E0 File Offset: 0x000E04E0
		internal MarshalByRefObject CreateInstanceInternal(Type serverType)
		{
			return ActivationServices.CreateInstance(serverType);
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x000E14E8 File Offset: 0x000E04E8
		public virtual RealProxy CreateProxy(ObjRef objRef, Type serverType, object serverObject, Context serverContext)
		{
			RemotingProxy remotingProxy = new RemotingProxy(serverType);
			if (serverContext != null)
			{
				RealProxy.SetStubData(remotingProxy, serverContext.InternalContextID);
			}
			if (objRef != null && objRef.GetServerIdentity().IsAllocated)
			{
				remotingProxy.SetSrvInfo(objRef.GetServerIdentity(), objRef.GetDomainID());
			}
			remotingProxy.Initialized = true;
			if (!serverType.IsContextful && !serverType.IsMarshalByRef && serverContext != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
			}
			return remotingProxy;
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000E1565 File Offset: 0x000E0565
		[ComVisible(true)]
		public bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x000E1568 File Offset: 0x000E0568
		[ComVisible(true)]
		public void GetPropertiesForNewContext(IConstructionCallMessage msg)
		{
		}
	}
}
