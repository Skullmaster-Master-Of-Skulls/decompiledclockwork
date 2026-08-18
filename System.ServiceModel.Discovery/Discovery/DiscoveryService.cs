using System;
using System.Globalization;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Discovery.Version11;
using System.ServiceModel.Discovery.VersionApril2005;
using System.ServiceModel.Discovery.VersionCD1;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000027 RID: 39
	public abstract class DiscoveryService : IDiscoveryContractAdhocApril2005, IDiscoveryContractApril2005, IDiscoveryContractManagedApril2005, IDiscoveryContractAdhoc11, IDiscoveryContractManaged11, IDiscoveryContractAdhocCD1, IDiscoveryContractManagedCD1, IDiscoveryServiceImplementation
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00006CB6 File Offset: 0x00004EB6
		protected DiscoveryService() : this(new DiscoveryMessageSequenceGenerator())
		{
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006CC3 File Offset: 0x00004EC3
		protected DiscoveryService(DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator) : this(discoveryMessageSequenceGenerator, 2056)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006CD4 File Offset: 0x00004ED4
		protected DiscoveryService(DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator, int duplicateMessageHistoryLength)
		{
			if (discoveryMessageSequenceGenerator == null)
			{
				throw FxTrace.Exception.ArgumentNull("messageSequenceGenerator");
			}
			if (duplicateMessageHistoryLength < 0)
			{
				throw FxTrace.Exception.ArgumentOutOfRange("duplicateMessageHistoryLength", duplicateMessageHistoryLength, SR.DiscoveryNegativeDuplicateMessageHistoryLength);
			}
			if (duplicateMessageHistoryLength > 0)
			{
				this.duplicateDetector = new DuplicateDetector<UniqueId>(duplicateMessageHistoryLength);
			}
			this.messageSequenceGenerator = discoveryMessageSequenceGenerator;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00006D30 File Offset: 0x00004F30
		internal DiscoveryMessageSequenceGenerator MessageSequenceGenerator
		{
			get
			{
				return this.messageSequenceGenerator;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractApril2005.ProbeOperation(ProbeMessageApril2005 request)
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006D38 File Offset: 0x00004F38
		IAsyncResult IDiscoveryContractApril2005.BeginProbeOperation(ProbeMessageApril2005 request, AsyncCallback callback, object state)
		{
			return new ProbeDuplexApril2005AsyncResult(request, this, null, callback, state);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006B40 File Offset: 0x00004D40
		void IDiscoveryContractApril2005.EndProbeOperation(IAsyncResult result)
		{
			ProbeDuplexApril2005AsyncResult.End(result);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractApril2005.ResolveOperation(ResolveMessageApril2005 request)
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006D44 File Offset: 0x00004F44
		IAsyncResult IDiscoveryContractApril2005.BeginResolveOperation(ResolveMessageApril2005 request, AsyncCallback callback, object state)
		{
			return new ResolveDuplexApril2005AsyncResult(request, this, null, callback, state);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006B54 File Offset: 0x00004D54
		void IDiscoveryContractApril2005.EndResolveOperation(IAsyncResult result)
		{
			ResolveDuplexApril2005AsyncResult.End(result);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractAdhoc11.ProbeOperation(ProbeMessage11 request)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006D50 File Offset: 0x00004F50
		IAsyncResult IDiscoveryContractAdhoc11.BeginProbeOperation(ProbeMessage11 request, AsyncCallback callback, object state)
		{
			return new ProbeDuplex11AsyncResult(request, this, null, callback, state);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006B68 File Offset: 0x00004D68
		void IDiscoveryContractAdhoc11.EndProbeOperation(IAsyncResult result)
		{
			ProbeDuplex11AsyncResult.End(result);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractAdhoc11.ResolveOperation(ResolveMessage11 request)
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006D5C File Offset: 0x00004F5C
		IAsyncResult IDiscoveryContractAdhoc11.BeginResolveOperation(ResolveMessage11 request, AsyncCallback callback, object state)
		{
			return new ResolveDuplex11AsyncResult(request, this, null, callback, state);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006B7C File Offset: 0x00004D7C
		void IDiscoveryContractAdhoc11.EndResolveOperation(IAsyncResult result)
		{
			ResolveDuplex11AsyncResult.End(result);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006B84 File Offset: 0x00004D84
		ProbeMatchesMessage11 IDiscoveryContractManaged11.ProbeOperation(ProbeMessage11 request)
		{
			return null;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006B87 File Offset: 0x00004D87
		IAsyncResult IDiscoveryContractManaged11.BeginProbeOperation(ProbeMessage11 request, AsyncCallback callback, object state)
		{
			return new ProbeRequestResponse11AsyncResult(request, this, callback, state);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006B92 File Offset: 0x00004D92
		ProbeMatchesMessage11 IDiscoveryContractManaged11.EndProbeOperation(IAsyncResult result)
		{
			return ProbeRequestResponse11AsyncResult.End(result);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006B84 File Offset: 0x00004D84
		ResolveMatchesMessage11 IDiscoveryContractManaged11.ResolveOperation(ResolveMessage11 request)
		{
			return null;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006B9A File Offset: 0x00004D9A
		IAsyncResult IDiscoveryContractManaged11.BeginResolveOperation(ResolveMessage11 request, AsyncCallback callback, object state)
		{
			return new ResolveRequestResponse11AsyncResult(request, this, callback, state);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006BA5 File Offset: 0x00004DA5
		ResolveMatchesMessage11 IDiscoveryContractManaged11.EndResolveOperation(IAsyncResult result)
		{
			return ResolveRequestResponse11AsyncResult.End(result);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractAdhocCD1.ProbeOperation(ProbeMessageCD1 request)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006D68 File Offset: 0x00004F68
		IAsyncResult IDiscoveryContractAdhocCD1.BeginProbeOperation(ProbeMessageCD1 request, AsyncCallback callback, object state)
		{
			return new ProbeDuplexCD1AsyncResult(request, this, null, callback, state);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006BB9 File Offset: 0x00004DB9
		void IDiscoveryContractAdhocCD1.EndProbeOperation(IAsyncResult result)
		{
			ProbeDuplexCD1AsyncResult.End(result);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractAdhocCD1.ResolveOperation(ResolveMessageCD1 request)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006D74 File Offset: 0x00004F74
		IAsyncResult IDiscoveryContractAdhocCD1.BeginResolveOperation(ResolveMessageCD1 request, AsyncCallback callback, object state)
		{
			return new ResolveDuplexCD1AsyncResult(request, this, null, callback, state);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006BCD File Offset: 0x00004DCD
		void IDiscoveryContractAdhocCD1.EndResolveOperation(IAsyncResult result)
		{
			ResolveDuplexCD1AsyncResult.End(result);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006B84 File Offset: 0x00004D84
		ProbeMatchesMessageCD1 IDiscoveryContractManagedCD1.ProbeOperation(ProbeMessageCD1 request)
		{
			return null;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006BD5 File Offset: 0x00004DD5
		IAsyncResult IDiscoveryContractManagedCD1.BeginProbeOperation(ProbeMessageCD1 request, AsyncCallback callback, object state)
		{
			return new ProbeRequestResponseCD1AsyncResult(request, this, callback, state);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006BE0 File Offset: 0x00004DE0
		ProbeMatchesMessageCD1 IDiscoveryContractManagedCD1.EndProbeOperation(IAsyncResult result)
		{
			return ProbeRequestResponseCD1AsyncResult.End(result);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006B84 File Offset: 0x00004D84
		ResolveMatchesMessageCD1 IDiscoveryContractManagedCD1.ResolveOperation(ResolveMessageCD1 request)
		{
			return null;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006BE8 File Offset: 0x00004DE8
		IAsyncResult IDiscoveryContractManagedCD1.BeginResolveOperation(ResolveMessageCD1 request, AsyncCallback callback, object state)
		{
			return new ResolveRequestResponseCD1AsyncResult(request, this, callback, state);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006BF3 File Offset: 0x00004DF3
		ResolveMatchesMessageCD1 IDiscoveryContractManagedCD1.EndResolveOperation(IAsyncResult result)
		{
			return ResolveRequestResponseCD1AsyncResult.End(result);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006D80 File Offset: 0x00004F80
		bool IDiscoveryServiceImplementation.IsDuplicate(UniqueId messageId)
		{
			return this.duplicateDetector != null && !this.duplicateDetector.AddIfNotDuplicate(messageId);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006D9B File Offset: 0x00004F9B
		DiscoveryMessageSequence IDiscoveryServiceImplementation.GetNextMessageSequence()
		{
			return this.messageSequenceGenerator.Next();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006DA8 File Offset: 0x00004FA8
		IAsyncResult IDiscoveryServiceImplementation.BeginFind(FindRequestContext findRequestContext, AsyncCallback callback, object state)
		{
			return this.OnBeginFind(findRequestContext, callback, state);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006DB3 File Offset: 0x00004FB3
		void IDiscoveryServiceImplementation.EndFind(IAsyncResult result)
		{
			this.OnEndFind(result);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006DBC File Offset: 0x00004FBC
		IAsyncResult IDiscoveryServiceImplementation.BeginResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			return this.OnBeginResolve(resolveCriteria, callback, state);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006DC7 File Offset: 0x00004FC7
		EndpointDiscoveryMetadata IDiscoveryServiceImplementation.EndResolve(IAsyncResult result)
		{
			return this.OnEndResolve(result);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006DD0 File Offset: 0x00004FD0
		internal static bool EnsureMessageId()
		{
			if (OperationContext.Current.IncomingMessageHeaders.MessageId == null)
			{
				if (TD.DiscoveryMessageWithNullMessageIdIsEnabled())
				{
					TD.DiscoveryMessageWithNullMessageId(null, string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
					{
						"Probe",
						"Resolve"
					}));
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006E2C File Offset: 0x0000502C
		internal static bool EnsureReplyTo()
		{
			OperationContext operationContext = OperationContext.Current;
			if (operationContext.IncomingMessageHeaders.ReplyTo == null)
			{
				if (TD.DiscoveryMessageWithNullReplyToIsEnabled())
				{
					EventTraceActivity eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(operationContext.IncomingMessage);
					TD.DiscoveryMessageWithNullReplyTo(eventTraceActivity, operationContext.IncomingMessageHeaders.MessageId.ToString());
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600022E RID: 558
		protected abstract IAsyncResult OnBeginFind(FindRequestContext findRequestContext, AsyncCallback callback, object state);

		// Token: 0x0600022F RID: 559
		protected abstract void OnEndFind(IAsyncResult result);

		// Token: 0x06000230 RID: 560
		protected abstract IAsyncResult OnBeginResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state);

		// Token: 0x06000231 RID: 561
		protected abstract EndpointDiscoveryMetadata OnEndResolve(IAsyncResult result);

		// Token: 0x04000078 RID: 120
		private DiscoveryMessageSequenceGenerator messageSequenceGenerator;

		// Token: 0x04000079 RID: 121
		private DuplicateDetector<UniqueId> duplicateDetector;
	}
}
