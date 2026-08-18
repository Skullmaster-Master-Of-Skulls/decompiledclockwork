using System;
using System.Collections.ObjectModel;
using System.Runtime;
using System.ServiceModel.Discovery.Version11;
using System.ServiceModel.Discovery.VersionApril2005;
using System.ServiceModel.Discovery.VersionCD1;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000026 RID: 38
	public abstract class DiscoveryProxy : IAnnouncementContractApril2005, IAnnouncementContract11, IAnnouncementContractCD1, IDiscoveryContractAdhocApril2005, IDiscoveryContractApril2005, IDiscoveryContractManagedApril2005, IDiscoveryContractAdhoc11, IDiscoveryContractManaged11, IDiscoveryContractAdhocCD1, IDiscoveryContractManagedCD1, IAnnouncementServiceImplementation, IDiscoveryServiceImplementation, IMulticastSuppressionImplementation
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00006ABC File Offset: 0x00004CBC
		protected DiscoveryProxy() : this(new DiscoveryMessageSequenceGenerator())
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006AC9 File Offset: 0x00004CC9
		protected DiscoveryProxy(DiscoveryMessageSequenceGenerator messageSequenceGenerator) : this(messageSequenceGenerator, 2056)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006AD8 File Offset: 0x00004CD8
		protected DiscoveryProxy(DiscoveryMessageSequenceGenerator messageSequenceGenerator, int duplicateMessageHistoryLength)
		{
			if (messageSequenceGenerator == null)
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
			this.messageSequenceGenerator = messageSequenceGenerator;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContractApril2005.HelloOperation(HelloMessageApril2005 message)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000030E3 File Offset: 0x000012E3
		IAsyncResult IAnnouncementContractApril2005.BeginHelloOperation(HelloMessageApril2005 message, AsyncCallback callback, object state)
		{
			return new HelloOperationApril2005AsyncResult(this, message, callback, state);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000030EE File Offset: 0x000012EE
		void IAnnouncementContractApril2005.EndHelloOperation(IAsyncResult result)
		{
			HelloOperationApril2005AsyncResult.End(result);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContractApril2005.ByeOperation(ByeMessageApril2005 message)
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000030F6 File Offset: 0x000012F6
		IAsyncResult IAnnouncementContractApril2005.BeginByeOperation(ByeMessageApril2005 message, AsyncCallback callback, object state)
		{
			return new ByeOperationApril2005AsyncResult(this, message, callback, state);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00003101 File Offset: 0x00001301
		void IAnnouncementContractApril2005.EndByeOperation(IAsyncResult result)
		{
			ByeOperationApril2005AsyncResult.End(result);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContract11.HelloOperation(HelloMessage11 message)
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00003109 File Offset: 0x00001309
		IAsyncResult IAnnouncementContract11.BeginHelloOperation(HelloMessage11 message, AsyncCallback callback, object state)
		{
			return new HelloOperation11AsyncResult(this, message, callback, state);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00003114 File Offset: 0x00001314
		void IAnnouncementContract11.EndHelloOperation(IAsyncResult result)
		{
			HelloOperation11AsyncResult.End(result);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContract11.ByeOperation(ByeMessage11 message)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000311C File Offset: 0x0000131C
		IAsyncResult IAnnouncementContract11.BeginByeOperation(ByeMessage11 message, AsyncCallback callback, object state)
		{
			return new ByeOperation11AsyncResult(this, message, callback, state);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00003127 File Offset: 0x00001327
		void IAnnouncementContract11.EndByeOperation(IAsyncResult result)
		{
			ByeOperation11AsyncResult.End(result);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContractCD1.HelloOperation(HelloMessageCD1 message)
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000312F File Offset: 0x0000132F
		IAsyncResult IAnnouncementContractCD1.BeginHelloOperation(HelloMessageCD1 message, AsyncCallback callback, object state)
		{
			return new HelloOperationCD1AsyncResult(this, message, callback, state);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000313A File Offset: 0x0000133A
		void IAnnouncementContractCD1.EndHelloOperation(IAsyncResult result)
		{
			HelloOperationCD1AsyncResult.End(result);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContractCD1.ByeOperation(ByeMessageCD1 message)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00003142 File Offset: 0x00001342
		IAsyncResult IAnnouncementContractCD1.BeginByeOperation(ByeMessageCD1 message, AsyncCallback callback, object state)
		{
			return new ByeOperationCD1AsyncResult(this, message, callback, state);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000314D File Offset: 0x0000134D
		void IAnnouncementContractCD1.EndByeOperation(IAsyncResult result)
		{
			ByeOperationCD1AsyncResult.End(result);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractApril2005.ProbeOperation(ProbeMessageApril2005 request)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006B34 File Offset: 0x00004D34
		IAsyncResult IDiscoveryContractApril2005.BeginProbeOperation(ProbeMessageApril2005 request, AsyncCallback callback, object state)
		{
			return new ProbeDuplexApril2005AsyncResult(request, this, this, callback, state);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006B40 File Offset: 0x00004D40
		void IDiscoveryContractApril2005.EndProbeOperation(IAsyncResult result)
		{
			ProbeDuplexApril2005AsyncResult.End(result);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractApril2005.ResolveOperation(ResolveMessageApril2005 request)
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006B48 File Offset: 0x00004D48
		IAsyncResult IDiscoveryContractApril2005.BeginResolveOperation(ResolveMessageApril2005 request, AsyncCallback callback, object state)
		{
			return new ResolveDuplexApril2005AsyncResult(request, this, this, callback, state);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006B54 File Offset: 0x00004D54
		void IDiscoveryContractApril2005.EndResolveOperation(IAsyncResult result)
		{
			ResolveDuplexApril2005AsyncResult.End(result);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractAdhoc11.ProbeOperation(ProbeMessage11 request)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006B5C File Offset: 0x00004D5C
		IAsyncResult IDiscoveryContractAdhoc11.BeginProbeOperation(ProbeMessage11 request, AsyncCallback callback, object state)
		{
			return new ProbeDuplex11AsyncResult(request, this, this, callback, state);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006B68 File Offset: 0x00004D68
		void IDiscoveryContractAdhoc11.EndProbeOperation(IAsyncResult result)
		{
			ProbeDuplex11AsyncResult.End(result);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractAdhoc11.ResolveOperation(ResolveMessage11 request)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006B70 File Offset: 0x00004D70
		IAsyncResult IDiscoveryContractAdhoc11.BeginResolveOperation(ResolveMessage11 request, AsyncCallback callback, object state)
		{
			return new ResolveDuplex11AsyncResult(request, this, this, callback, state);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006B7C File Offset: 0x00004D7C
		void IDiscoveryContractAdhoc11.EndResolveOperation(IAsyncResult result)
		{
			ResolveDuplex11AsyncResult.End(result);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006B84 File Offset: 0x00004D84
		ProbeMatchesMessage11 IDiscoveryContractManaged11.ProbeOperation(ProbeMessage11 request)
		{
			return null;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006B87 File Offset: 0x00004D87
		IAsyncResult IDiscoveryContractManaged11.BeginProbeOperation(ProbeMessage11 request, AsyncCallback callback, object state)
		{
			return new ProbeRequestResponse11AsyncResult(request, this, callback, state);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006B92 File Offset: 0x00004D92
		ProbeMatchesMessage11 IDiscoveryContractManaged11.EndProbeOperation(IAsyncResult result)
		{
			return ProbeRequestResponse11AsyncResult.End(result);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006B84 File Offset: 0x00004D84
		ResolveMatchesMessage11 IDiscoveryContractManaged11.ResolveOperation(ResolveMessage11 request)
		{
			return null;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006B9A File Offset: 0x00004D9A
		IAsyncResult IDiscoveryContractManaged11.BeginResolveOperation(ResolveMessage11 request, AsyncCallback callback, object state)
		{
			return new ResolveRequestResponse11AsyncResult(request, this, callback, state);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006BA5 File Offset: 0x00004DA5
		ResolveMatchesMessage11 IDiscoveryContractManaged11.EndResolveOperation(IAsyncResult result)
		{
			return ResolveRequestResponse11AsyncResult.End(result);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractAdhocCD1.ProbeOperation(ProbeMessageCD1 request)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006BAD File Offset: 0x00004DAD
		IAsyncResult IDiscoveryContractAdhocCD1.BeginProbeOperation(ProbeMessageCD1 request, AsyncCallback callback, object state)
		{
			return new ProbeDuplexCD1AsyncResult(request, this, this, callback, state);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006BB9 File Offset: 0x00004DB9
		void IDiscoveryContractAdhocCD1.EndProbeOperation(IAsyncResult result)
		{
			ProbeDuplexCD1AsyncResult.End(result);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000030E1 File Offset: 0x000012E1
		void IDiscoveryContractAdhocCD1.ResolveOperation(ResolveMessageCD1 request)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006BC1 File Offset: 0x00004DC1
		IAsyncResult IDiscoveryContractAdhocCD1.BeginResolveOperation(ResolveMessageCD1 request, AsyncCallback callback, object state)
		{
			return new ResolveDuplexCD1AsyncResult(request, this, this, callback, state);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006BCD File Offset: 0x00004DCD
		void IDiscoveryContractAdhocCD1.EndResolveOperation(IAsyncResult result)
		{
			ResolveDuplexCD1AsyncResult.End(result);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006B84 File Offset: 0x00004D84
		ProbeMatchesMessageCD1 IDiscoveryContractManagedCD1.ProbeOperation(ProbeMessageCD1 request)
		{
			return null;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006BD5 File Offset: 0x00004DD5
		IAsyncResult IDiscoveryContractManagedCD1.BeginProbeOperation(ProbeMessageCD1 request, AsyncCallback callback, object state)
		{
			return new ProbeRequestResponseCD1AsyncResult(request, this, callback, state);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006BE0 File Offset: 0x00004DE0
		ProbeMatchesMessageCD1 IDiscoveryContractManagedCD1.EndProbeOperation(IAsyncResult result)
		{
			return ProbeRequestResponseCD1AsyncResult.End(result);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006B84 File Offset: 0x00004D84
		ResolveMatchesMessageCD1 IDiscoveryContractManagedCD1.ResolveOperation(ResolveMessageCD1 request)
		{
			return null;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006BE8 File Offset: 0x00004DE8
		IAsyncResult IDiscoveryContractManagedCD1.BeginResolveOperation(ResolveMessageCD1 request, AsyncCallback callback, object state)
		{
			return new ResolveRequestResponseCD1AsyncResult(request, this, callback, state);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006BF3 File Offset: 0x00004DF3
		ResolveMatchesMessageCD1 IDiscoveryContractManagedCD1.EndResolveOperation(IAsyncResult result)
		{
			return ResolveRequestResponseCD1AsyncResult.End(result);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006BFB File Offset: 0x00004DFB
		bool IAnnouncementServiceImplementation.IsDuplicate(UniqueId messageId)
		{
			return this.duplicateDetector != null && !this.duplicateDetector.AddIfNotDuplicate(messageId);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006C16 File Offset: 0x00004E16
		IAsyncResult IAnnouncementServiceImplementation.OnBeginOnlineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return this.OnBeginOnlineAnnouncement(messageSequence, endpointDiscoveryMetadata, callback, state);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006C23 File Offset: 0x00004E23
		void IAnnouncementServiceImplementation.OnEndOnlineAnnouncement(IAsyncResult result)
		{
			this.OnEndOnlineAnnouncement(result);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006C2C File Offset: 0x00004E2C
		IAsyncResult IAnnouncementServiceImplementation.OnBeginOfflineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return this.OnBeginOfflineAnnouncement(messageSequence, endpointDiscoveryMetadata, callback, state);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006C39 File Offset: 0x00004E39
		void IAnnouncementServiceImplementation.OnEndOfflineAnnouncement(IAsyncResult result)
		{
			this.OnEndOfflineAnnouncement(result);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006BFB File Offset: 0x00004DFB
		bool IDiscoveryServiceImplementation.IsDuplicate(UniqueId messageId)
		{
			return this.duplicateDetector != null && !this.duplicateDetector.AddIfNotDuplicate(messageId);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006C42 File Offset: 0x00004E42
		DiscoveryMessageSequence IDiscoveryServiceImplementation.GetNextMessageSequence()
		{
			return this.messageSequenceGenerator.Next();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006C4F File Offset: 0x00004E4F
		IAsyncResult IDiscoveryServiceImplementation.BeginFind(FindRequestContext findRequestContext, AsyncCallback callback, object state)
		{
			return this.OnBeginFind(findRequestContext, callback, state);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006C5A File Offset: 0x00004E5A
		void IDiscoveryServiceImplementation.EndFind(IAsyncResult result)
		{
			this.OnEndFind(result);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006C63 File Offset: 0x00004E63
		IAsyncResult IDiscoveryServiceImplementation.BeginResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			return this.OnBeginResolve(resolveCriteria, callback, state);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006C6E File Offset: 0x00004E6E
		EndpointDiscoveryMetadata IDiscoveryServiceImplementation.EndResolve(IAsyncResult result)
		{
			return this.OnEndResolve(result);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006C77 File Offset: 0x00004E77
		IAsyncResult IMulticastSuppressionImplementation.BeginShouldRedirectFind(FindCriteria findCriteria, AsyncCallback callback, object state)
		{
			return this.BeginShouldRedirectFind(findCriteria, callback, state);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006C82 File Offset: 0x00004E82
		bool IMulticastSuppressionImplementation.EndShouldRedirectFind(IAsyncResult result, out Collection<EndpointDiscoveryMetadata> redirectionEndpoints)
		{
			return this.EndShouldRedirectFind(result, out redirectionEndpoints);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006C8C File Offset: 0x00004E8C
		IAsyncResult IMulticastSuppressionImplementation.BeginShouldRedirectResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			return this.BeginShouldRedirectResolve(resolveCriteria, callback, state);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006C97 File Offset: 0x00004E97
		bool IMulticastSuppressionImplementation.EndShouldRedirectResolve(IAsyncResult result, out Collection<EndpointDiscoveryMetadata> redirectionEndpoints)
		{
			return this.EndShouldRedirectResolve(result, out redirectionEndpoints);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006CA1 File Offset: 0x00004EA1
		protected virtual IAsyncResult BeginShouldRedirectFind(FindCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult<bool>(false, callback, state);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006CAB File Offset: 0x00004EAB
		protected virtual bool EndShouldRedirectFind(IAsyncResult result, out Collection<EndpointDiscoveryMetadata> redirectionEndpoints)
		{
			redirectionEndpoints = null;
			return CompletedAsyncResult<bool>.End(result);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006CA1 File Offset: 0x00004EA1
		protected virtual IAsyncResult BeginShouldRedirectResolve(ResolveCriteria findCriteria, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult<bool>(false, callback, state);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006CAB File Offset: 0x00004EAB
		protected virtual bool EndShouldRedirectResolve(IAsyncResult result, out Collection<EndpointDiscoveryMetadata> redirectionEndpoints)
		{
			redirectionEndpoints = null;
			return CompletedAsyncResult<bool>.End(result);
		}

		// Token: 0x060001FC RID: 508
		protected abstract IAsyncResult OnBeginOnlineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state);

		// Token: 0x060001FD RID: 509
		protected abstract void OnEndOnlineAnnouncement(IAsyncResult result);

		// Token: 0x060001FE RID: 510
		protected abstract IAsyncResult OnBeginOfflineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state);

		// Token: 0x060001FF RID: 511
		protected abstract void OnEndOfflineAnnouncement(IAsyncResult result);

		// Token: 0x06000200 RID: 512
		protected abstract IAsyncResult OnBeginFind(FindRequestContext findRequestContext, AsyncCallback callback, object state);

		// Token: 0x06000201 RID: 513
		protected abstract void OnEndFind(IAsyncResult result);

		// Token: 0x06000202 RID: 514
		protected abstract IAsyncResult OnBeginResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state);

		// Token: 0x06000203 RID: 515
		protected abstract EndpointDiscoveryMetadata OnEndResolve(IAsyncResult result);

		// Token: 0x04000076 RID: 118
		private DiscoveryMessageSequenceGenerator messageSequenceGenerator;

		// Token: 0x04000077 RID: 119
		private DuplicateDetector<UniqueId> duplicateDetector;
	}
}
