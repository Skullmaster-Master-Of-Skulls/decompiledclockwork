using System;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200054D RID: 1357
	internal class TerminatingOperationBehavior
	{
		// Token: 0x060033B0 RID: 13232 RVA: 0x000C7491 File Offset: 0x000C5691
		private static void AbortChannel(object state)
		{
			((IChannel)state).Abort();
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000C749E File Offset: 0x000C569E
		public static TerminatingOperationBehavior CreateIfNecessary(DispatchRuntime dispatch)
		{
			if (TerminatingOperationBehavior.IsTerminatingOperationBehaviorNeeded(dispatch))
			{
				return new TerminatingOperationBehavior();
			}
			return null;
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000C74B0 File Offset: 0x000C56B0
		private static bool IsTerminatingOperationBehaviorNeeded(DispatchRuntime dispatch)
		{
			for (int i = 0; i < dispatch.Operations.Count; i++)
			{
				DispatchOperation dispatchOperation = dispatch.Operations[i];
				if (dispatchOperation.IsTerminating)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000C74EC File Offset: 0x000C56EC
		internal void AfterReply(ref MessageRpc rpc)
		{
			if (rpc.Operation.IsTerminating && rpc.Channel.HasSession)
			{
				IOThreadTimer iothreadTimer = new IOThreadTimer(new Action<object>(TerminatingOperationBehavior.AbortChannel), rpc.Channel.Binder.Channel, false);
				iothreadTimer.Set(rpc.Channel.CloseTimeout);
			}
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000C7548 File Offset: 0x000C5748
		internal static void AfterReply(ref ProxyRpc rpc)
		{
			if (rpc.Operation.IsTerminating && rpc.Channel.HasSession)
			{
				IChannel channel = rpc.Channel.Binder.Channel;
				rpc.Channel.Close(rpc.TimeoutHelper.RemainingTime());
			}
		}
	}
}
