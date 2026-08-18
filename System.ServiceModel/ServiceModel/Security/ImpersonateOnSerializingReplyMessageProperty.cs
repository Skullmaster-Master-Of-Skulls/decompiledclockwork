using System;
using System.Security;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Security
{
	// Token: 0x020002D5 RID: 725
	public class ImpersonateOnSerializingReplyMessageProperty : IMessageProperty
	{
		// Token: 0x060017BE RID: 6078 RVA: 0x0005A95C File Offset: 0x00058B5C
		internal ImpersonateOnSerializingReplyMessageProperty(ref MessageRpc rpc)
		{
			this.rpc = rpc;
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0005A970 File Offset: 0x00058B70
		public static string Name
		{
			get
			{
				return "ImpersonateOnSerializingReplyMessageProperty";
			}
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0005A977 File Offset: 0x00058B77
		public static bool TryGet(Message message, out ImpersonateOnSerializingReplyMessageProperty property)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return ImpersonateOnSerializingReplyMessageProperty.TryGet(message.Properties, out property);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0005A998 File Offset: 0x00058B98
		public static bool TryGet(MessageProperties properties, out ImpersonateOnSerializingReplyMessageProperty property)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			object obj = null;
			if (properties.TryGetValue("ImpersonateOnSerializingReplyMessageProperty", out obj))
			{
				property = (obj as ImpersonateOnSerializingReplyMessageProperty);
			}
			else
			{
				property = null;
			}
			return property != null;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0005A9DC File Offset: 0x00058BDC
		public IMessageProperty CreateCopy()
		{
			return new ImpersonateOnSerializingReplyMessageProperty(ref this.rpc);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0005A9F8 File Offset: 0x00058BF8
		[SecuritySafeCritical]
		public void StartImpersonation(out IDisposable impersonationContext, out IPrincipal originalPrincipal, out bool isThreadPrincipalSet)
		{
			impersonationContext = null;
			originalPrincipal = null;
			isThreadPrincipalSet = false;
			if (OperationContext.Current != null)
			{
				EndpointDispatcher endpointDispatcher = OperationContext.Current.EndpointDispatcher;
				if (endpointDispatcher != null)
				{
					DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
					ImmutableDispatchRuntime runtime = dispatchRuntime.GetRuntime();
					if (runtime != null && runtime.SecurityImpersonation != null)
					{
						runtime.SecurityImpersonation.StartImpersonation(ref this.rpc, out impersonationContext, out originalPrincipal, out isThreadPrincipalSet);
					}
				}
			}
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0005AA50 File Offset: 0x00058C50
		[SecuritySafeCritical]
		public void StopImpersonation(IDisposable impersonationContext, IPrincipal originalPrincipal, bool isThreadPrincipalSet)
		{
			if (OperationContext.Current != null)
			{
				EndpointDispatcher endpointDispatcher = OperationContext.Current.EndpointDispatcher;
				if (endpointDispatcher != null)
				{
					DispatchRuntime dispatchRuntime = endpointDispatcher.DispatchRuntime;
					ImmutableDispatchRuntime runtime = dispatchRuntime.GetRuntime();
					if (runtime != null && runtime.SecurityImpersonation != null)
					{
						runtime.SecurityImpersonation.StopImpersonation(ref this.rpc, impersonationContext, originalPrincipal, isThreadPrincipalSet);
					}
				}
			}
		}

		// Token: 0x04001C33 RID: 7219
		private const string PropertyName = "ImpersonateOnSerializingReplyMessageProperty";

		// Token: 0x04001C34 RID: 7220
		private MessageRpc rpc;
	}
}
