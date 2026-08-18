using System;
using System.Runtime;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000076 RID: 118
	internal sealed class ResolveRequestResponseCD1AsyncResult : ResolveRequestResponseAsyncResult<ResolveMessageCD1, ResolveMatchesMessageCD1>
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x00010045 File Offset: 0x0000E245
		internal ResolveRequestResponseCD1AsyncResult(ResolveMessageCD1 resolveMessage, IDiscoveryServiceImplementation discoveryServiceImpl, AsyncCallback callback, object state) : base(resolveMessage, discoveryServiceImpl, callback, state)
		{
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00010054 File Offset: 0x0000E254
		public static ResolveMatchesMessageCD1 End(IAsyncResult result)
		{
			ResolveRequestResponseCD1AsyncResult resolveRequestResponseCD1AsyncResult = AsyncResult.End<ResolveRequestResponseCD1AsyncResult>(result);
			return resolveRequestResponseCD1AsyncResult.End();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001006E File Offset: 0x0000E26E
		protected override bool ValidateContent(ResolveMessageCD1 resolveMessage)
		{
			if (resolveMessage == null || resolveMessage.Resolve == null)
			{
				if (TD.DiscoveryMessageWithNoContentIsEnabled())
				{
					TD.DiscoveryMessageWithNoContent(base.Context.EventTraceActivity, "Resolve");
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000FF96 File Offset: 0x0000E196
		protected override ResolveCriteria GetResolveCriteria(ResolveMessageCD1 resolveMessage)
		{
			return resolveMessage.Resolve.ToResolveCriteria();
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001009A File Offset: 0x0000E29A
		protected override ResolveMatchesMessageCD1 GetResolveResponse(DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint)
		{
			return ResolveMatchesMessageCD1.Create(discoveryMessageSequence, matchingEndpoint);
		}
	}
}
