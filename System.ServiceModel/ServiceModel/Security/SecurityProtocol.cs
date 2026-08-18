using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002C0 RID: 704
	internal abstract class SecurityProtocol : ISecurityCommunicationObject
	{
		// Token: 0x06001646 RID: 5702 RVA: 0x00054909 File Offset: 0x00052B09
		protected SecurityProtocol(SecurityProtocolFactory factory, EndpointAddress target, Uri via)
		{
			this.factory = factory;
			this.target = target;
			this.via = via;
			this.communicationObject = new WrapperSecurityCommunicationObject(this);
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x00054932 File Offset: 0x00052B32
		protected WrapperSecurityCommunicationObject CommunicationObject
		{
			get
			{
				return this.communicationObject;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0005493A File Offset: 0x00052B3A
		public SecurityProtocolFactory SecurityProtocolFactory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00054942 File Offset: 0x00052B42
		public EndpointAddress Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0005494A File Offset: 0x00052B4A
		public Uri Via
		{
			get
			{
				return this.via;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00054952 File Offset: 0x00052B52
		public ICollection<SupportingTokenProviderSpecification> ChannelSupportingTokenProviderSpecification
		{
			get
			{
				return this.channelSupportingTokenProviderSpecification;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0005495A File Offset: 0x00052B5A
		public Dictionary<string, ICollection<SupportingTokenProviderSpecification>> ScopedSupportingTokenProviderSpecification
		{
			get
			{
				return this.scopedSupportingTokenProviderSpecification;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00054962 File Offset: 0x00052B62
		private static ReadOnlyCollection<SupportingTokenProviderSpecification> EmptyTokenProviders
		{
			get
			{
				if (SecurityProtocol.emptyTokenProviders == null)
				{
					SecurityProtocol.emptyTokenProviders = new ReadOnlyCollection<SupportingTokenProviderSpecification>(new List<SupportingTokenProviderSpecification>());
				}
				return SecurityProtocol.emptyTokenProviders;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0005497F File Offset: 0x00052B7F
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x00054987 File Offset: 0x00052B87
		public ChannelParameterCollection ChannelParameters
		{
			get
			{
				return this.channelParameters;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.channelParameters = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0005499B File Offset: 0x00052B9B
		public TimeSpan DefaultOpenTimeout
		{
			get
			{
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000549A2 File Offset: 0x00052BA2
		public TimeSpan DefaultCloseTimeout
		{
			get
			{
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x000549A9 File Offset: 0x00052BA9
		public IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnClose), timeout, callback, state);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x000549C0 File Offset: 0x00052BC0
		public IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnOpen), timeout, callback, state);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000549D7 File Offset: 0x00052BD7
		public void OnClosed()
		{
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000549D9 File Offset: 0x00052BD9
		public void OnClosing()
		{
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000549DB File Offset: 0x00052BDB
		public void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000549E3 File Offset: 0x00052BE3
		public void OnEndOpen(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000549EB File Offset: 0x00052BEB
		public void OnFaulted()
		{
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000549ED File Offset: 0x00052BED
		public void OnOpened()
		{
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000549EF File Offset: 0x00052BEF
		public void OnOpening()
		{
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000549F4 File Offset: 0x00052BF4
		internal IList<SupportingTokenProviderSpecification> GetSupportingTokenProviders(string action)
		{
			if (this.mergedSupportingTokenProvidersMap != null && this.mergedSupportingTokenProvidersMap.Count > 0)
			{
				if (action != null && this.mergedSupportingTokenProvidersMap.ContainsKey(action))
				{
					return this.mergedSupportingTokenProvidersMap[action];
				}
				if (this.mergedSupportingTokenProvidersMap.ContainsKey("*"))
				{
					return this.mergedSupportingTokenProvidersMap["*"];
				}
			}
			if (this.channelSupportingTokenProviderSpecification != SecurityProtocol.EmptyTokenProviders)
			{
				return (IList<SupportingTokenProviderSpecification>)this.channelSupportingTokenProviderSpecification;
			}
			return null;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00054A74 File Offset: 0x00052C74
		protected InitiatorServiceModelSecurityTokenRequirement CreateInitiatorSecurityTokenRequirement()
		{
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
			initiatorServiceModelSecurityTokenRequirement.TargetAddress = this.Target;
			initiatorServiceModelSecurityTokenRequirement.Via = this.via;
			initiatorServiceModelSecurityTokenRequirement.SecurityBindingElement = this.factory.SecurityBindingElement;
			initiatorServiceModelSecurityTokenRequirement.SecurityAlgorithmSuite = this.factory.OutgoingAlgorithmSuite;
			initiatorServiceModelSecurityTokenRequirement.MessageSecurityVersion = this.factory.MessageSecurityVersion.SecurityTokenVersion;
			if (this.factory.PrivacyNoticeUri != null)
			{
				initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.PrivacyNoticeUriProperty] = this.factory.PrivacyNoticeUri;
			}
			if (this.channelParameters != null)
			{
				initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty] = this.channelParameters;
			}
			initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.PrivacyNoticeVersionProperty] = this.factory.PrivacyNoticeVersion;
			return initiatorServiceModelSecurityTokenRequirement;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00054B44 File Offset: 0x00052D44
		private InitiatorServiceModelSecurityTokenRequirement CreateInitiatorSecurityTokenRequirement(SecurityTokenParameters parameters, SecurityTokenAttachmentMode attachmentMode)
		{
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = this.CreateInitiatorSecurityTokenRequirement();
			parameters.InitializeSecurityTokenRequirement(initiatorServiceModelSecurityTokenRequirement);
			initiatorServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Signature;
			initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = MessageDirection.Output;
			initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.SupportingTokenAttachmentModeProperty] = attachmentMode;
			return initiatorServiceModelSecurityTokenRequirement;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00054B94 File Offset: 0x00052D94
		private void AddSupportingTokenProviders(SupportingTokenParameters supportingTokenParameters, bool isOptional, IList<SupportingTokenProviderSpecification> providerSpecList)
		{
			for (int i = 0; i < supportingTokenParameters.Endorsing.Count; i++)
			{
				SecurityTokenRequirement securityTokenRequirement = this.CreateInitiatorSecurityTokenRequirement(supportingTokenParameters.Endorsing[i], SecurityTokenAttachmentMode.Endorsing);
				try
				{
					if (isOptional)
					{
						securityTokenRequirement.IsOptionalToken = true;
					}
					SecurityTokenProvider securityTokenProvider = this.factory.SecurityTokenManager.CreateSecurityTokenProvider(securityTokenRequirement);
					if (securityTokenProvider != null)
					{
						SupportingTokenProviderSpecification item = new SupportingTokenProviderSpecification(securityTokenProvider, SecurityTokenAttachmentMode.Endorsing, supportingTokenParameters.Endorsing[i]);
						providerSpecList.Add(item);
					}
				}
				catch (Exception exception)
				{
					if (!isOptional || Fx.IsFatal(exception))
					{
						throw;
					}
				}
			}
			for (int j = 0; j < supportingTokenParameters.SignedEndorsing.Count; j++)
			{
				SecurityTokenRequirement securityTokenRequirement2 = this.CreateInitiatorSecurityTokenRequirement(supportingTokenParameters.SignedEndorsing[j], SecurityTokenAttachmentMode.SignedEndorsing);
				try
				{
					if (isOptional)
					{
						securityTokenRequirement2.IsOptionalToken = true;
					}
					SecurityTokenProvider securityTokenProvider2 = this.factory.SecurityTokenManager.CreateSecurityTokenProvider(securityTokenRequirement2);
					if (securityTokenProvider2 != null)
					{
						SupportingTokenProviderSpecification item2 = new SupportingTokenProviderSpecification(securityTokenProvider2, SecurityTokenAttachmentMode.SignedEndorsing, supportingTokenParameters.SignedEndorsing[j]);
						providerSpecList.Add(item2);
					}
				}
				catch (Exception exception2)
				{
					if (!isOptional || Fx.IsFatal(exception2))
					{
						throw;
					}
				}
			}
			for (int k = 0; k < supportingTokenParameters.SignedEncrypted.Count; k++)
			{
				SecurityTokenRequirement securityTokenRequirement3 = this.CreateInitiatorSecurityTokenRequirement(supportingTokenParameters.SignedEncrypted[k], SecurityTokenAttachmentMode.SignedEncrypted);
				try
				{
					if (isOptional)
					{
						securityTokenRequirement3.IsOptionalToken = true;
					}
					SecurityTokenProvider securityTokenProvider3 = this.factory.SecurityTokenManager.CreateSecurityTokenProvider(securityTokenRequirement3);
					if (securityTokenProvider3 != null)
					{
						SupportingTokenProviderSpecification item3 = new SupportingTokenProviderSpecification(securityTokenProvider3, SecurityTokenAttachmentMode.SignedEncrypted, supportingTokenParameters.SignedEncrypted[k]);
						providerSpecList.Add(item3);
					}
				}
				catch (Exception exception3)
				{
					if (!isOptional || Fx.IsFatal(exception3))
					{
						throw;
					}
				}
			}
			for (int l = 0; l < supportingTokenParameters.Signed.Count; l++)
			{
				SecurityTokenRequirement securityTokenRequirement4 = this.CreateInitiatorSecurityTokenRequirement(supportingTokenParameters.Signed[l], SecurityTokenAttachmentMode.Signed);
				try
				{
					if (isOptional)
					{
						securityTokenRequirement4.IsOptionalToken = true;
					}
					SecurityTokenProvider securityTokenProvider4 = this.factory.SecurityTokenManager.CreateSecurityTokenProvider(securityTokenRequirement4);
					if (securityTokenProvider4 != null)
					{
						SupportingTokenProviderSpecification item4 = new SupportingTokenProviderSpecification(securityTokenProvider4, SecurityTokenAttachmentMode.Signed, supportingTokenParameters.Signed[l]);
						providerSpecList.Add(item4);
					}
				}
				catch (Exception exception4)
				{
					if (!isOptional || Fx.IsFatal(exception4))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00054DF0 File Offset: 0x00052FF0
		private void MergeSupportingTokenProviders(TimeSpan timeout)
		{
			if (this.ScopedSupportingTokenProviderSpecification.Count == 0)
			{
				this.mergedSupportingTokenProvidersMap = null;
				return;
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.factory.ExpectSupportingTokens = true;
			this.mergedSupportingTokenProvidersMap = new Dictionary<string, Collection<SupportingTokenProviderSpecification>>();
			foreach (string key in this.ScopedSupportingTokenProviderSpecification.Keys)
			{
				ICollection<SupportingTokenProviderSpecification> collection = this.ScopedSupportingTokenProviderSpecification[key];
				if (collection != null && collection.Count != 0)
				{
					Collection<SupportingTokenProviderSpecification> collection2 = new Collection<SupportingTokenProviderSpecification>();
					foreach (SupportingTokenProviderSpecification item in this.channelSupportingTokenProviderSpecification)
					{
						collection2.Add(item);
					}
					foreach (SupportingTokenProviderSpecification supportingTokenProviderSpecification in collection)
					{
						SecurityUtils.OpenTokenProviderIfRequired(supportingTokenProviderSpecification.TokenProvider, timeoutHelper.RemainingTime());
						if ((supportingTokenProviderSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || supportingTokenProviderSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing) && supportingTokenProviderSpecification.TokenParameters.RequireDerivedKeys && !supportingTokenProviderSpecification.TokenParameters.HasAsymmetricKey)
						{
							this.factory.ExpectKeyDerivation = true;
						}
						collection2.Add(supportingTokenProviderSpecification);
					}
					this.mergedSupportingTokenProvidersMap.Add(key, collection2);
				}
			}
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00054FA4 File Offset: 0x000531A4
		public void Open(TimeSpan timeout)
		{
			this.communicationObject.Open(timeout);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00054FB2 File Offset: 0x000531B2
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00054FC2 File Offset: 0x000531C2
		public void EndOpen(IAsyncResult result)
		{
			this.communicationObject.EndOpen(result);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00054FD0 File Offset: 0x000531D0
		public virtual void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.factory.ActAsInitiator)
			{
				this.channelSupportingTokenProviderSpecification = new Collection<SupportingTokenProviderSpecification>();
				this.scopedSupportingTokenProviderSpecification = new Dictionary<string, ICollection<SupportingTokenProviderSpecification>>();
				this.AddSupportingTokenProviders(this.factory.SecurityBindingElement.EndpointSupportingTokenParameters, false, (IList<SupportingTokenProviderSpecification>)this.channelSupportingTokenProviderSpecification);
				this.AddSupportingTokenProviders(this.factory.SecurityBindingElement.OptionalEndpointSupportingTokenParameters, true, (IList<SupportingTokenProviderSpecification>)this.channelSupportingTokenProviderSpecification);
				foreach (string key in this.factory.SecurityBindingElement.OperationSupportingTokenParameters.Keys)
				{
					Collection<SupportingTokenProviderSpecification> collection = new Collection<SupportingTokenProviderSpecification>();
					this.AddSupportingTokenProviders(this.factory.SecurityBindingElement.OperationSupportingTokenParameters[key], false, collection);
					this.scopedSupportingTokenProviderSpecification.Add(key, collection);
				}
				foreach (string key2 in this.factory.SecurityBindingElement.OptionalOperationSupportingTokenParameters.Keys)
				{
					ICollection<SupportingTokenProviderSpecification> collection2;
					Collection<SupportingTokenProviderSpecification> collection3;
					if (this.scopedSupportingTokenProviderSpecification.TryGetValue(key2, out collection2))
					{
						collection3 = (Collection<SupportingTokenProviderSpecification>)collection2;
					}
					else
					{
						collection3 = new Collection<SupportingTokenProviderSpecification>();
						this.scopedSupportingTokenProviderSpecification.Add(key2, collection3);
					}
					this.AddSupportingTokenProviders(this.factory.SecurityBindingElement.OptionalOperationSupportingTokenParameters[key2], true, collection3);
				}
				if (!this.channelSupportingTokenProviderSpecification.IsReadOnly)
				{
					if (this.channelSupportingTokenProviderSpecification.Count == 0)
					{
						this.channelSupportingTokenProviderSpecification = SecurityProtocol.EmptyTokenProviders;
					}
					else
					{
						this.factory.ExpectSupportingTokens = true;
						foreach (SupportingTokenProviderSpecification supportingTokenProviderSpecification in this.channelSupportingTokenProviderSpecification)
						{
							SecurityUtils.OpenTokenProviderIfRequired(supportingTokenProviderSpecification.TokenProvider, timeoutHelper.RemainingTime());
							if ((supportingTokenProviderSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || supportingTokenProviderSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing) && supportingTokenProviderSpecification.TokenParameters.RequireDerivedKeys && !supportingTokenProviderSpecification.TokenParameters.HasAsymmetricKey)
							{
								this.factory.ExpectKeyDerivation = true;
							}
						}
						this.channelSupportingTokenProviderSpecification = new ReadOnlyCollection<SupportingTokenProviderSpecification>((Collection<SupportingTokenProviderSpecification>)this.channelSupportingTokenProviderSpecification);
					}
				}
				this.MergeSupportingTokenProviders(timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0005524C File Offset: 0x0005344C
		public void Close(bool aborted, TimeSpan timeout)
		{
			if (aborted)
			{
				this.communicationObject.Abort();
				return;
			}
			this.communicationObject.Close(timeout);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0005526C File Offset: 0x0005346C
		public virtual void OnAbort()
		{
			if (this.factory.ActAsInitiator)
			{
				foreach (SupportingTokenProviderSpecification supportingTokenProviderSpecification in this.channelSupportingTokenProviderSpecification)
				{
					SecurityUtils.AbortTokenProviderIfRequired(supportingTokenProviderSpecification.TokenProvider);
				}
				foreach (string key in this.scopedSupportingTokenProviderSpecification.Keys)
				{
					ICollection<SupportingTokenProviderSpecification> collection = this.scopedSupportingTokenProviderSpecification[key];
					foreach (SupportingTokenProviderSpecification supportingTokenProviderSpecification2 in collection)
					{
						SecurityUtils.AbortTokenProviderIfRequired(supportingTokenProviderSpecification2.TokenProvider);
					}
				}
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00055360 File Offset: 0x00053560
		public virtual void OnClose(TimeSpan timeout)
		{
			if (this.factory.ActAsInitiator)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				foreach (SupportingTokenProviderSpecification supportingTokenProviderSpecification in this.channelSupportingTokenProviderSpecification)
				{
					SecurityUtils.CloseTokenProviderIfRequired(supportingTokenProviderSpecification.TokenProvider, timeoutHelper.RemainingTime());
				}
				foreach (string key in this.scopedSupportingTokenProviderSpecification.Keys)
				{
					ICollection<SupportingTokenProviderSpecification> collection = this.scopedSupportingTokenProviderSpecification[key];
					foreach (SupportingTokenProviderSpecification supportingTokenProviderSpecification2 in collection)
					{
						SecurityUtils.CloseTokenProviderIfRequired(supportingTokenProviderSpecification2.TokenProvider, timeoutHelper.RemainingTime());
					}
				}
			}
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0005546C File Offset: 0x0005366C
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginClose(timeout, callback, state);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0005547C File Offset: 0x0005367C
		public void EndClose(IAsyncResult result)
		{
			this.communicationObject.EndClose(result);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0005548C File Offset: 0x0005368C
		private static void SetSecurityHeaderId(SendSecurityHeader securityHeader, Message message)
		{
			SecurityMessageProperty security = message.Properties.Security;
			if (security != null)
			{
				securityHeader.IdPrefix = security.SenderIdPrefix;
			}
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x000554B4 File Offset: 0x000536B4
		private void AddSupportingTokenSpecification(SecurityMessageProperty security, IList<SecurityToken> tokens, SecurityTokenAttachmentMode attachmentMode, IDictionary<SecurityToken, ReadOnlyCollection<IAuthorizationPolicy>> tokenPoliciesMapping)
		{
			if (tokens == null || tokens.Count == 0)
			{
				return;
			}
			for (int i = 0; i < tokens.Count; i++)
			{
				security.IncomingSupportingTokens.Add(new SupportingTokenSpecification(tokens[i], tokenPoliciesMapping[tokens[i]], attachmentMode));
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00055504 File Offset: 0x00053704
		protected void AddSupportingTokenSpecification(SecurityMessageProperty security, IList<SecurityToken> basicTokens, IList<SecurityToken> endorsingTokens, IList<SecurityToken> signedEndorsingTokens, IList<SecurityToken> signedTokens, IDictionary<SecurityToken, ReadOnlyCollection<IAuthorizationPolicy>> tokenPoliciesMapping)
		{
			this.AddSupportingTokenSpecification(security, basicTokens, SecurityTokenAttachmentMode.SignedEncrypted, tokenPoliciesMapping);
			this.AddSupportingTokenSpecification(security, endorsingTokens, SecurityTokenAttachmentMode.Endorsing, tokenPoliciesMapping);
			this.AddSupportingTokenSpecification(security, signedEndorsingTokens, SecurityTokenAttachmentMode.SignedEndorsing, tokenPoliciesMapping);
			this.AddSupportingTokenSpecification(security, signedTokens, SecurityTokenAttachmentMode.Signed, tokenPoliciesMapping);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00055534 File Offset: 0x00053734
		protected SendSecurityHeader CreateSendSecurityHeader(Message message, string actor, SecurityProtocolFactory factory)
		{
			return this.CreateSendSecurityHeader(message, actor, factory, true);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00055540 File Offset: 0x00053740
		protected SendSecurityHeader CreateSendSecurityHeaderForTransportProtocol(Message message, string actor, SecurityProtocolFactory factory)
		{
			return this.CreateSendSecurityHeader(message, actor, factory, false);
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0005554C File Offset: 0x0005374C
		private SendSecurityHeader CreateSendSecurityHeader(Message message, string actor, SecurityProtocolFactory factory, bool requireMessageProtection)
		{
			MessageDirection direction = factory.ActAsInitiator ? MessageDirection.Input : MessageDirection.Output;
			SendSecurityHeader sendSecurityHeader = factory.StandardsManager.CreateSendSecurityHeader(message, actor, true, false, factory.OutgoingAlgorithmSuite, direction);
			sendSecurityHeader.Layout = factory.SecurityHeaderLayout;
			sendSecurityHeader.RequireMessageProtection = requireMessageProtection;
			SecurityProtocol.SetSecurityHeaderId(sendSecurityHeader, message);
			if (factory.AddTimestamp)
			{
				sendSecurityHeader.AddTimestamp(factory.TimestampValidityDuration);
			}
			sendSecurityHeader.StreamBufferManager = factory.StreamBufferManager;
			return sendSecurityHeader;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000555BC File Offset: 0x000537BC
		internal void AddMessageSupportingTokens(Message message, ref IList<SupportingTokenSpecification> supportingTokens)
		{
			SecurityMessageProperty security = message.Properties.Security;
			if (security != null && security.HasOutgoingSupportingTokens)
			{
				if (supportingTokens == null)
				{
					supportingTokens = new Collection<SupportingTokenSpecification>();
				}
				for (int i = 0; i < security.OutgoingSupportingTokens.Count; i++)
				{
					SupportingTokenSpecification supportingTokenSpecification = security.OutgoingSupportingTokens[i];
					if (supportingTokenSpecification.SecurityTokenParameters == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SenderSideSupportingTokensMustSpecifySecurityTokenParameters")));
					}
					supportingTokens.Add(supportingTokenSpecification);
				}
			}
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00055638 File Offset: 0x00053838
		internal bool TryGetSupportingTokens(SecurityProtocolFactory factory, EndpointAddress target, Uri via, Message message, TimeSpan timeout, bool isBlockingCall, out IList<SupportingTokenSpecification> supportingTokens)
		{
			if (!factory.ActAsInitiator)
			{
				supportingTokens = null;
				return true;
			}
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			supportingTokens = null;
			IList<SupportingTokenProviderSpecification> supportingTokenProviders = this.GetSupportingTokenProviders(message.Headers.Action);
			if (supportingTokenProviders != null && supportingTokenProviders.Count > 0)
			{
				if (!isBlockingCall)
				{
					return false;
				}
				supportingTokens = new Collection<SupportingTokenSpecification>();
				for (int i = 0; i < supportingTokenProviders.Count; i++)
				{
					SupportingTokenProviderSpecification supportingTokenProviderSpecification = supportingTokenProviders[i];
					SecurityToken token;
					if (this is TransportSecurityProtocol && supportingTokenProviderSpecification.TokenParameters is KerberosSecurityTokenParameters)
					{
						token = new ProviderBackedSecurityToken(supportingTokenProviderSpecification.TokenProvider, timeoutHelper.RemainingTime());
					}
					else
					{
						token = supportingTokenProviderSpecification.TokenProvider.GetToken(timeoutHelper.RemainingTime());
					}
					supportingTokens.Add(new SupportingTokenSpecification(token, EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance, supportingTokenProviderSpecification.SecurityTokenAttachmentMode, supportingTokenProviderSpecification.TokenParameters));
				}
			}
			this.AddMessageSupportingTokens(message, ref supportingTokens);
			return true;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0005572C File Offset: 0x0005392C
		protected IList<SupportingTokenAuthenticatorSpecification> GetSupportingTokenAuthenticatorsAndSetExpectationFlags(SecurityProtocolFactory factory, Message message, ReceiveSecurityHeader securityHeader)
		{
			if (factory.ActAsInitiator)
			{
				return null;
			}
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			bool expectSignedTokens;
			bool expectBasicTokens;
			bool expectEndorsingTokens;
			IList<SupportingTokenAuthenticatorSpecification> supportingTokenAuthenticators = factory.GetSupportingTokenAuthenticators(message.Headers.Action, out expectSignedTokens, out expectBasicTokens, out expectEndorsingTokens);
			securityHeader.ExpectBasicTokens = expectBasicTokens;
			securityHeader.ExpectEndorsingTokens = expectEndorsingTokens;
			securityHeader.ExpectSignedTokens = expectSignedTokens;
			return supportingTokenAuthenticators;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00055784 File Offset: 0x00053984
		protected ReadOnlyCollection<SecurityTokenResolver> MergeOutOfBandResolvers(IList<SupportingTokenAuthenticatorSpecification> supportingAuthenticators, ReadOnlyCollection<SecurityTokenResolver> primaryResolvers)
		{
			Collection<SecurityTokenResolver> collection = null;
			if (supportingAuthenticators != null && supportingAuthenticators.Count > 0)
			{
				for (int i = 0; i < supportingAuthenticators.Count; i++)
				{
					if (supportingAuthenticators[i].TokenResolver != null)
					{
						collection = (collection ?? new Collection<SecurityTokenResolver>());
						collection.Add(supportingAuthenticators[i].TokenResolver);
					}
				}
			}
			if (collection != null)
			{
				if (primaryResolvers != null)
				{
					for (int j = 0; j < primaryResolvers.Count; j++)
					{
						collection.Insert(0, primaryResolvers[j]);
					}
				}
				return new ReadOnlyCollection<SecurityTokenResolver>(collection);
			}
			return primaryResolvers ?? EmptyReadOnlyCollection<SecurityTokenResolver>.Instance;
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00055814 File Offset: 0x00053A14
		protected void AddSupportingTokens(SendSecurityHeader securityHeader, IList<SupportingTokenSpecification> supportingTokens)
		{
			if (supportingTokens != null)
			{
				for (int i = 0; i < supportingTokens.Count; i++)
				{
					SecurityToken securityToken = supportingTokens[i].SecurityToken;
					SecurityTokenParameters securityTokenParameters = supportingTokens[i].SecurityTokenParameters;
					switch (supportingTokens[i].SecurityTokenAttachmentMode)
					{
					case SecurityTokenAttachmentMode.Signed:
						securityHeader.AddSignedSupportingToken(securityToken, securityTokenParameters);
						break;
					case SecurityTokenAttachmentMode.Endorsing:
						securityHeader.AddEndorsingSupportingToken(securityToken, securityTokenParameters);
						break;
					case SecurityTokenAttachmentMode.SignedEndorsing:
						securityHeader.AddSignedEndorsingSupportingToken(securityToken, securityTokenParameters);
						break;
					case SecurityTokenAttachmentMode.SignedEncrypted:
						securityHeader.AddBasicSupportingToken(securityToken, securityTokenParameters);
						break;
					default:
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnknownTokenAttachmentMode", new object[]
						{
							supportingTokens[i].SecurityTokenAttachmentMode.ToString()
						})));
					}
				}
			}
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x000558E3 File Offset: 0x00053AE3
		public virtual IAsyncResult BeginSecureOutgoingMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.SecureOutgoingMessage(ref message, timeout);
			return new CompletedAsyncResult<Message>(message, callback, state);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000558F8 File Offset: 0x00053AF8
		public virtual IAsyncResult BeginSecureOutgoingMessage(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state)
		{
			SecurityProtocolCorrelationState parameter = this.SecureOutgoingMessage(ref message, timeout, correlationState);
			return new CompletedAsyncResult<Message, SecurityProtocolCorrelationState>(message, parameter, callback, state);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0005591B File Offset: 0x00053B1B
		public virtual IAsyncResult BeginVerifyIncomingMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.VerifyIncomingMessage(ref message, timeout);
			return new CompletedAsyncResult<Message>(message, callback, state);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00055930 File Offset: 0x00053B30
		public virtual IAsyncResult BeginVerifyIncomingMessage(Message message, TimeSpan timeout, SecurityProtocolCorrelationState[] correlationStates, AsyncCallback callback, object state)
		{
			SecurityProtocolCorrelationState parameter = this.VerifyIncomingMessage(ref message, timeout, correlationStates);
			return new CompletedAsyncResult<Message, SecurityProtocolCorrelationState>(message, parameter, callback, state);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00055953 File Offset: 0x00053B53
		public virtual void EndSecureOutgoingMessage(IAsyncResult result, out Message message)
		{
			message = CompletedAsyncResult<Message>.End(result);
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0005595D File Offset: 0x00053B5D
		public virtual void EndSecureOutgoingMessage(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState)
		{
			message = CompletedAsyncResult<Message, SecurityProtocolCorrelationState>.End(result, out newCorrelationState);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00055968 File Offset: 0x00053B68
		public virtual void EndVerifyIncomingMessage(IAsyncResult result, out Message message)
		{
			message = CompletedAsyncResult<Message>.End(result);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00055972 File Offset: 0x00053B72
		public virtual void EndVerifyIncomingMessage(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState)
		{
			message = CompletedAsyncResult<Message, SecurityProtocolCorrelationState>.End(result, out newCorrelationState);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00055980 File Offset: 0x00053B80
		internal static SecurityToken GetToken(SecurityTokenProvider provider, EndpointAddress target, TimeSpan timeout)
		{
			if (provider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenProviderCannotGetTokensForTarget", new object[]
				{
					target
				})));
			}
			SecurityToken result = null;
			try
			{
				result = provider.GetToken(timeout);
			}
			catch (SecurityTokenException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenProviderCannotGetTokensForTarget", new object[]
				{
					target
				}), innerException));
			}
			catch (SecurityNegotiationException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("TokenProviderCannotGetTokensForTarget", new object[]
				{
					target
				}), innerException2));
			}
			return result;
		}

		// Token: 0x0600167D RID: 5757
		public abstract void SecureOutgoingMessage(ref Message message, TimeSpan timeout);

		// Token: 0x0600167E RID: 5758 RVA: 0x00055A2C File Offset: 0x00053C2C
		public virtual SecurityProtocolCorrelationState SecureOutgoingMessage(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
		{
			this.SecureOutgoingMessage(ref message, timeout);
			return null;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00055A37 File Offset: 0x00053C37
		protected virtual void OnOutgoingMessageSecured(Message securedMessage)
		{
			SecurityTraceRecordHelper.TraceOutgoingMessageSecured(this, securedMessage);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00055A40 File Offset: 0x00053C40
		protected virtual void OnSecureOutgoingMessageFailure(Message message)
		{
			SecurityTraceRecordHelper.TraceSecureOutgoingMessageFailure(this, message);
		}

		// Token: 0x06001681 RID: 5761
		public abstract void VerifyIncomingMessage(ref Message message, TimeSpan timeout);

		// Token: 0x06001682 RID: 5762 RVA: 0x00055A49 File Offset: 0x00053C49
		public virtual SecurityProtocolCorrelationState VerifyIncomingMessage(ref Message message, TimeSpan timeout, params SecurityProtocolCorrelationState[] correlationStates)
		{
			this.VerifyIncomingMessage(ref message, timeout);
			return null;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00055A54 File Offset: 0x00053C54
		protected virtual void OnIncomingMessageVerified(Message verifiedMessage)
		{
			SecurityTraceRecordHelper.TraceIncomingMessageVerified(this, verifiedMessage);
			if (AuditLevel.Success == (this.factory.MessageAuthenticationAuditLevel & AuditLevel.Success))
			{
				SecurityAuditHelper.WriteMessageAuthenticationSuccessEvent(this.factory.AuditLogLocation, this.factory.SuppressAuditFailure, verifiedMessage, verifiedMessage.Headers.To, verifiedMessage.Headers.Action, SecurityUtils.GetIdentityNamesFromContext(verifiedMessage.Properties.Security.ServiceSecurityContext.AuthorizationContext));
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00055AC4 File Offset: 0x00053CC4
		protected virtual void OnVerifyIncomingMessageFailure(Message message, Exception exception)
		{
			SecurityTraceRecordHelper.TraceVerifyIncomingMessageFailure(this, message);
			if (PerformanceCounters.PerformanceCountersEnabled && null != this.factory.ListenUri && (exception.GetType() == typeof(MessageSecurityException) || exception.GetType().IsSubclassOf(typeof(MessageSecurityException)) || exception.GetType() == typeof(SecurityTokenException) || exception.GetType().IsSubclassOf(typeof(SecurityTokenException))))
			{
				PerformanceCounters.AuthenticationFailed(message, this.factory.ListenUri);
			}
			if (AuditLevel.Failure == (this.factory.MessageAuthenticationAuditLevel & AuditLevel.Failure))
			{
				try
				{
					SecurityMessageProperty security = message.Properties.Security;
					string clientIdentity;
					if (security != null && security.ServiceSecurityContext != null)
					{
						clientIdentity = SecurityUtils.GetIdentityNamesFromContext(security.ServiceSecurityContext.AuthorizationContext);
					}
					else
					{
						clientIdentity = SecurityUtils.AnonymousIdentity.Name;
					}
					SecurityAuditHelper.WriteMessageAuthenticationFailureEvent(this.factory.AuditLogLocation, this.factory.SuppressAuditFailure, message, message.Headers.To, message.Headers.Action, clientIdentity, exception);
				}
				catch (Exception exception2)
				{
					if (Fx.IsFatal(exception2))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
				}
			}
		}

		// Token: 0x04001BB9 RID: 7097
		private static ReadOnlyCollection<SupportingTokenProviderSpecification> emptyTokenProviders;

		// Token: 0x04001BBA RID: 7098
		private ICollection<SupportingTokenProviderSpecification> channelSupportingTokenProviderSpecification;

		// Token: 0x04001BBB RID: 7099
		private Dictionary<string, ICollection<SupportingTokenProviderSpecification>> scopedSupportingTokenProviderSpecification;

		// Token: 0x04001BBC RID: 7100
		private Dictionary<string, Collection<SupportingTokenProviderSpecification>> mergedSupportingTokenProvidersMap;

		// Token: 0x04001BBD RID: 7101
		private SecurityProtocolFactory factory;

		// Token: 0x04001BBE RID: 7102
		private EndpointAddress target;

		// Token: 0x04001BBF RID: 7103
		private Uri via;

		// Token: 0x04001BC0 RID: 7104
		private WrapperSecurityCommunicationObject communicationObject;

		// Token: 0x04001BC1 RID: 7105
		private ChannelParameterCollection channelParameters;

		// Token: 0x02000B4B RID: 2891
		protected abstract class GetSupportingTokensAsyncResult : AsyncResult
		{
			// Token: 0x060070F2 RID: 28914 RVA: 0x001A44F3 File Offset: 0x001A26F3
			public GetSupportingTokensAsyncResult(Message m, SecurityProtocol binding, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.message = m;
				this.binding = binding;
				this.timeoutHelper = new TimeoutHelper(timeout);
			}

			// Token: 0x17001A58 RID: 6744
			// (get) Token: 0x060070F3 RID: 28915 RVA: 0x001A4519 File Offset: 0x001A2719
			protected IList<SupportingTokenSpecification> SupportingTokens
			{
				get
				{
					return this.supportingTokens;
				}
			}

			// Token: 0x060070F4 RID: 28916
			protected abstract bool OnGetSupportingTokensDone(TimeSpan timeout);

			// Token: 0x060070F5 RID: 28917 RVA: 0x001A4524 File Offset: 0x001A2724
			private static void GetSupportingTokenCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SecurityProtocol.GetSupportingTokensAsyncResult getSupportingTokensAsyncResult = (SecurityProtocol.GetSupportingTokensAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag;
				try
				{
					getSupportingTokensAsyncResult.AddSupportingToken(result);
					flag = getSupportingTokensAsyncResult.AddSupportingTokens();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					getSupportingTokensAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x060070F6 RID: 28918 RVA: 0x001A4584 File Offset: 0x001A2784
			private void AddSupportingToken(IAsyncResult result)
			{
				SupportingTokenProviderSpecification supportingTokenProviderSpecification = this.supportingTokenProviders[this.currentTokenProviderIndex];
				SecurityTokenProvider.SecurityTokenAsyncResult securityTokenAsyncResult = result as SecurityTokenProvider.SecurityTokenAsyncResult;
				if (securityTokenAsyncResult != null)
				{
					this.supportingTokens.Add(new SupportingTokenSpecification(SecurityTokenProvider.SecurityTokenAsyncResult.End(result), EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance, supportingTokenProviderSpecification.SecurityTokenAttachmentMode, supportingTokenProviderSpecification.TokenParameters));
				}
				else
				{
					this.supportingTokens.Add(new SupportingTokenSpecification(supportingTokenProviderSpecification.TokenProvider.EndGetToken(result), EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance, supportingTokenProviderSpecification.SecurityTokenAttachmentMode, supportingTokenProviderSpecification.TokenParameters));
				}
				this.currentTokenProviderIndex++;
			}

			// Token: 0x060070F7 RID: 28919 RVA: 0x001A4614 File Offset: 0x001A2814
			private bool AddSupportingTokens()
			{
				while (this.currentTokenProviderIndex < this.supportingTokenProviders.Count)
				{
					SupportingTokenProviderSpecification supportingTokenProviderSpecification = this.supportingTokenProviders[this.currentTokenProviderIndex];
					IAsyncResult asyncResult;
					if (this.binding is TransportSecurityProtocol && supportingTokenProviderSpecification.TokenParameters is KerberosSecurityTokenParameters)
					{
						asyncResult = new SecurityTokenProvider.SecurityTokenAsyncResult(new ProviderBackedSecurityToken(supportingTokenProviderSpecification.TokenProvider, this.timeoutHelper.RemainingTime()), null, this);
					}
					else
					{
						asyncResult = supportingTokenProviderSpecification.TokenProvider.BeginGetToken(this.timeoutHelper.RemainingTime(), SecurityProtocol.GetSupportingTokensAsyncResult.getSupportingTokensCallback, this);
					}
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.AddSupportingToken(asyncResult);
				}
				this.binding.AddMessageSupportingTokens(this.message, ref this.supportingTokens);
				return this.OnGetSupportingTokensDone(this.timeoutHelper.RemainingTime());
			}

			// Token: 0x060070F8 RID: 28920 RVA: 0x001A46E0 File Offset: 0x001A28E0
			protected void Start()
			{
				bool flag;
				if (this.binding.TryGetSupportingTokens(this.binding.SecurityProtocolFactory, this.binding.Target, this.binding.Via, this.message, this.timeoutHelper.RemainingTime(), false, out this.supportingTokens))
				{
					flag = this.OnGetSupportingTokensDone(this.timeoutHelper.RemainingTime());
				}
				else
				{
					this.supportingTokens = new Collection<SupportingTokenSpecification>();
					this.supportingTokenProviders = this.binding.GetSupportingTokenProviders(this.message.Headers.Action);
					if (this.supportingTokenProviders == null || this.supportingTokenProviders.Count <= 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException("There must be at least 1 supporting token provider"));
					}
					flag = this.AddSupportingTokens();
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x04004038 RID: 16440
			private static AsyncCallback getSupportingTokensCallback = Fx.ThunkCallback(new AsyncCallback(SecurityProtocol.GetSupportingTokensAsyncResult.GetSupportingTokenCallback));

			// Token: 0x04004039 RID: 16441
			private SecurityProtocol binding;

			// Token: 0x0400403A RID: 16442
			private Message message;

			// Token: 0x0400403B RID: 16443
			private IList<SupportingTokenSpecification> supportingTokens;

			// Token: 0x0400403C RID: 16444
			private int currentTokenProviderIndex;

			// Token: 0x0400403D RID: 16445
			private IList<SupportingTokenProviderSpecification> supportingTokenProviders;

			// Token: 0x0400403E RID: 16446
			private TimeoutHelper timeoutHelper;
		}
	}
}
