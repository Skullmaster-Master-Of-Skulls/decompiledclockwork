using System;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A2F RID: 2607
	internal class PeerChannelAuthenticatorExtension : IExtension<IPeerNeighbor>
	{
		// Token: 0x0600676C RID: 26476 RVA: 0x00182378 File Offset: 0x00180578
		public PeerChannelAuthenticatorExtension(PeerSecurityManager securityManager, EventHandler onSucceeded, EventArgs args, string meshId)
		{
			this.securityManager = securityManager;
			this.state = PeerChannelAuthenticatorExtension.PeerAuthState.Created;
			this.originalArgs = args;
			this.onSucceeded = onSucceeded;
			this.meshId = meshId;
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x0600676D RID: 26477 RVA: 0x001823AF File Offset: 0x001805AF
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x001823B8 File Offset: 0x001805B8
		public void Attach(IPeerNeighbor host)
		{
			Fx.AssertAndThrow(this.securityManager.AuthenticationMode == PeerAuthenticationMode.Password, "Invalid AuthenticationMode!");
			Fx.AssertAndThrow(host != null, "unrecognized host!");
			this.host = host;
			this.timer = new IOThreadTimer(new Action<object>(this.OnTimeout), null, true);
			this.timer.Set(PeerChannelAuthenticatorExtension.Timeout);
		}

		// Token: 0x0600676F RID: 26479 RVA: 0x0018241C File Offset: 0x0018061C
		public static void OnNeighborClosed(IPeerNeighbor neighbor)
		{
			PeerChannelAuthenticatorExtension peerChannelAuthenticatorExtension = neighbor.Extensions.Find<PeerChannelAuthenticatorExtension>();
			if (peerChannelAuthenticatorExtension != null)
			{
				neighbor.Extensions.Remove(peerChannelAuthenticatorExtension);
			}
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x00182445 File Offset: 0x00180645
		public void Detach(IPeerNeighbor host)
		{
			if (host.State < PeerNeighborState.Authenticated)
			{
				this.OnFailed(host);
			}
			this.host = null;
			this.timer.Cancel();
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x0018246C File Offset: 0x0018066C
		private void OnTimeout(object state)
		{
			IPeerNeighbor peerNeighbor = this.host;
			if (peerNeighbor == null)
			{
				return;
			}
			if (peerNeighbor.State < PeerNeighborState.Authenticated)
			{
				this.OnFailed(peerNeighbor);
			}
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x00182494 File Offset: 0x00180694
		public void InitiateHandShake()
		{
			IPeerNeighbor peerNeighbor = this.host;
			using (new OperationContextScope(new OperationContext(null)))
			{
				PeerHashToken selfToken = this.securityManager.GetSelfToken();
				Message message = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "RequestSecurityToken", new PeerRequestSecurityToken(selfToken));
				bool flag = false;
				try
				{
					Message message2 = peerNeighbor.RequestSecurityToken(message);
					if (message2 == null)
					{
						throw Fx.AssertAndThrow("SecurityHandshake return empty message!");
					}
					this.ProcessRstr(peerNeighbor, message2, PeerSecurityManager.FindClaim(ServiceSecurityContext.Current));
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						flag = true;
						throw;
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					this.state = PeerChannelAuthenticatorExtension.PeerAuthState.Failed;
					if (DiagnosticUtility.ShouldTraceError)
					{
						ServiceSecurityContext serviceSecurityContext = ServiceSecurityContext.Current;
						ClaimSet claimSet = null;
						if (serviceSecurityContext != null && serviceSecurityContext.AuthorizationContext != null && serviceSecurityContext.AuthorizationContext.ClaimSets != null && serviceSecurityContext.AuthorizationContext.ClaimSets.Count > 0)
						{
							claimSet = serviceSecurityContext.AuthorizationContext.ClaimSets[0];
						}
						PeerAuthenticationFailureTraceRecord extendedData = new PeerAuthenticationFailureTraceRecord(this.meshId, peerNeighbor.ListenAddress.EndpointAddress.ToString(), claimSet, ex);
						TraceUtility.TraceEvent(TraceEventType.Error, 262221, SR.GetString("TraceCodePeerNodeAuthenticationFailure"), extendedData, this, null);
					}
					peerNeighbor.Abort(PeerCloseReason.AuthenticationFailure, PeerCloseInitiator.LocalNode);
				}
				finally
				{
					if (!flag)
					{
						message.Close();
					}
				}
			}
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x00182628 File Offset: 0x00180828
		public Message ProcessRst(Message message, Claim claim)
		{
			IPeerNeighbor peerNeighbor = this.host;
			Message result = null;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state != PeerChannelAuthenticatorExtension.PeerAuthState.Created || peerNeighbor == null || peerNeighbor.IsInitiator || peerNeighbor.State != PeerNeighborState.Opened)
				{
					this.OnFailed(peerNeighbor);
					return null;
				}
			}
			try
			{
				PeerHashToken obj2 = PeerRequestSecurityToken.CreateHashTokenFrom(message);
				PeerHashToken expectedTokenForClaim = this.securityManager.GetExpectedTokenForClaim(claim);
				if (!expectedTokenForClaim.Equals(obj2))
				{
					this.OnFailed(peerNeighbor);
				}
				else
				{
					this.state = PeerChannelAuthenticatorExtension.PeerAuthState.Authenticated;
					PeerHashToken selfToken = this.securityManager.GetSelfToken();
					PeerRequestSecurityTokenResponse body = new PeerRequestSecurityTokenResponse(selfToken);
					result = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "RequestSecurityTokenResponse", body);
					this.OnAuthenticated();
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				this.OnFailed(peerNeighbor);
			}
			return result;
		}

		// Token: 0x06006774 RID: 26484 RVA: 0x00182728 File Offset: 0x00180928
		public void ProcessRstr(IPeerNeighbor neighbor, Message message, Claim claim)
		{
			PeerHashToken peerHashToken = PeerRequestSecurityTokenResponse.CreateHashTokenFrom(message);
			if (!peerHashToken.IsValid)
			{
				this.OnFailed(neighbor);
				return;
			}
			PeerHashToken expectedTokenForClaim = this.securityManager.GetExpectedTokenForClaim(claim);
			if (!expectedTokenForClaim.Equals(peerHashToken))
			{
				this.OnFailed(neighbor);
				return;
			}
			this.OnAuthenticated();
		}

		// Token: 0x06006775 RID: 26485 RVA: 0x00182770 File Offset: 0x00180970
		public void OnAuthenticated()
		{
			IPeerNeighbor peerNeighbor = null;
			object obj = this.ThisLock;
			lock (obj)
			{
				this.timer.Cancel();
				peerNeighbor = this.host;
				this.state = PeerChannelAuthenticatorExtension.PeerAuthState.Authenticated;
			}
			if (peerNeighbor == null)
			{
				return;
			}
			peerNeighbor.TrySetState(PeerNeighborState.Authenticated);
			this.onSucceeded(peerNeighbor, this.originalArgs);
		}

		// Token: 0x06006776 RID: 26486 RVA: 0x001827E4 File Offset: 0x001809E4
		private void OnFailed(IPeerNeighbor neighbor)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.state = PeerChannelAuthenticatorExtension.PeerAuthState.Failed;
				this.timer.Cancel();
				this.host = null;
			}
			if (DiagnosticUtility.ShouldTraceError)
			{
				string remoteAddress = "";
				PeerNodeAddress listenAddress = neighbor.ListenAddress;
				if (listenAddress != null)
				{
					remoteAddress = listenAddress.EndpointAddress.ToString();
				}
				OperationContext operationContext = OperationContext.Current;
				if (operationContext != null)
				{
					remoteAddress = operationContext.IncomingMessageProperties.Via.ToString();
					ServiceSecurityContext serviceSecurityContext = operationContext.ServiceSecurityContext;
					if (serviceSecurityContext != null)
					{
						PeerAuthenticationFailureTraceRecord extendedData = new PeerAuthenticationFailureTraceRecord(this.meshId, remoteAddress, serviceSecurityContext.AuthorizationContext.ClaimSets[0], null);
						if (DiagnosticUtility.ShouldTraceError)
						{
							TraceUtility.TraceEvent(TraceEventType.Error, 262221, SR.GetString("TraceCodePeerNodeAuthenticationFailure"), extendedData, this, null);
						}
					}
				}
				else
				{
					PeerAuthenticationFailureTraceRecord extendedData = new PeerAuthenticationFailureTraceRecord(this.meshId, remoteAddress);
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 262222, SR.GetString("TraceCodePeerNodeAuthenticationTimeout"), extendedData, this, null);
					}
				}
			}
			neighbor.Abort(PeerCloseReason.AuthenticationFailure, PeerCloseInitiator.LocalNode);
		}

		// Token: 0x04003B65 RID: 15205
		private IPeerNeighbor host;

		// Token: 0x04003B66 RID: 15206
		private PeerSecurityManager securityManager;

		// Token: 0x04003B67 RID: 15207
		private PeerChannelAuthenticatorExtension.PeerAuthState state;

		// Token: 0x04003B68 RID: 15208
		private EventArgs originalArgs;

		// Token: 0x04003B69 RID: 15209
		private EventHandler onSucceeded;

		// Token: 0x04003B6A RID: 15210
		private IOThreadTimer timer;

		// Token: 0x04003B6B RID: 15211
		private object thisLock = new object();

		// Token: 0x04003B6C RID: 15212
		private static TimeSpan Timeout = new TimeSpan(0, 2, 0);

		// Token: 0x04003B6D RID: 15213
		private string meshId;

		// Token: 0x02000E70 RID: 3696
		private enum PeerAuthState
		{
			// Token: 0x04004B12 RID: 19218
			Created,
			// Token: 0x04004B13 RID: 19219
			Authenticated,
			// Token: 0x04004B14 RID: 19220
			Failed
		}
	}
}
