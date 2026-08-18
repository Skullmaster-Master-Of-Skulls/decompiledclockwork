using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Transactions;
using System.Xml;
using Microsoft.Transactions.Wsat.Messaging;
using Microsoft.Transactions.Wsat.Protocol;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001BC RID: 444
	internal abstract class WsatTransactionFormatter : TransactionFormatter
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x00034891 File Offset: 0x00032A91
		protected WsatTransactionFormatter(ProtocolVersion protocolVersion)
		{
			this.protocolVersion = protocolVersion;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000348A0 File Offset: 0x00032AA0
		private void EnsureInitialized()
		{
			if (!this.initialized)
			{
				lock (this)
				{
					if (!this.initialized)
					{
						this.wsatConfig = new WsatConfiguration();
						this.wsatProxy = new WsatProxy(this.wsatConfig, this.protocolVersion);
						this.initialized = true;
					}
				}
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00034910 File Offset: 0x00032B10
		public override void WriteTransaction(Transaction transaction, Message message)
		{
			this.EnsureInitialized();
			this.ForcePromotion(transaction);
			CoordinationContext context;
			RequestSecurityTokenResponse requestSecurityTokenResponse;
			this.MarshalAsCoordinationContext(transaction, out context, out requestSecurityTokenResponse);
			if (requestSecurityTokenResponse != null)
			{
				CoordinationServiceSecurity.AddIssuedToken(message, requestSecurityTokenResponse);
			}
			WsatTransactionHeader header = new WsatTransactionHeader(context, this.protocolVersion);
			message.Headers.Add(header);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00034958 File Offset: 0x00032B58
		private void ForcePromotion(Transaction transaction)
		{
			TransactionInterop.GetTransmitterPropagationToken(transaction);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00034964 File Offset: 0x00032B64
		public override TransactionInfo ReadTransaction(Message message)
		{
			this.EnsureInitialized();
			CoordinationContext coordinationContext = WsatTransactionHeader.GetCoordinationContext(message, this.protocolVersion);
			if (coordinationContext == null)
			{
				return null;
			}
			RequestSecurityTokenResponse issuedToken;
			try
			{
				issuedToken = CoordinationServiceSecurity.GetIssuedToken(message, coordinationContext.Identifier, this.protocolVersion);
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TransactionException("FailedToDeserializeIssuedToken", innerException));
			}
			return new WsatTransactionInfo(this.wsatProxy, coordinationContext, issuedToken);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000349D4 File Offset: 0x00032BD4
		public WsatTransactionInfo CreateTransactionInfo(CoordinationContext context, RequestSecurityTokenResponse issuedToken)
		{
			return new WsatTransactionInfo(this.wsatProxy, context, issuedToken);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000349E4 File Offset: 0x00032BE4
		public void MarshalAsCoordinationContext(Transaction transaction, out CoordinationContext context, out RequestSecurityTokenResponse issuedToken)
		{
			Guid distributedIdentifier = transaction.TransactionInformation.DistributedIdentifier;
			string contextId = null;
			context = new CoordinationContext(this.protocolVersion);
			uint expires;
			IsolationFlags isolationFlags;
			string description;
			OleTxTransactionFormatter.GetTransactionAttributes(transaction, out expires, out isolationFlags, out description);
			context.IsolationFlags = isolationFlags;
			context.Description = description;
			WsatExtendedInformation wsatExtendedInformation;
			if (TransactionCache<Transaction, WsatExtendedInformation>.Find(transaction, out wsatExtendedInformation))
			{
				context.Expires = wsatExtendedInformation.Timeout;
				if (!string.IsNullOrEmpty(wsatExtendedInformation.Identifier))
				{
					context.Identifier = wsatExtendedInformation.Identifier;
					contextId = wsatExtendedInformation.Identifier;
				}
			}
			else
			{
				context.Expires = expires;
				if (context.Expires == 0U)
				{
					context.Expires = (uint)TimeoutHelper.ToMilliseconds(this.wsatConfig.MaxTimeout);
				}
			}
			if (context.Identifier == null)
			{
				context.Identifier = CoordinationContext.CreateNativeIdentifier(distributedIdentifier);
				contextId = null;
			}
			string tokenId;
			if (!this.wsatConfig.IssuedTokensEnabled)
			{
				tokenId = null;
				issuedToken = null;
			}
			else
			{
				CoordinationServiceSecurity.CreateIssuedToken(distributedIdentifier, context.Identifier, this.protocolVersion, out issuedToken, out tokenId);
			}
			AddressHeader refParam = new WsatRegistrationHeader(distributedIdentifier, contextId, tokenId);
			context.RegistrationService = this.wsatConfig.CreateRegistrationService(refParam, this.protocolVersion);
			context.IsolationLevel = transaction.IsolationLevel;
			context.LocalTransactionId = distributedIdentifier;
			if (this.wsatConfig.OleTxUpgradeEnabled)
			{
				context.PropagationToken = TransactionInterop.GetTransmitterPropagationToken(transaction);
			}
		}

		// Token: 0x0400176A RID: 5994
		private bool initialized;

		// Token: 0x0400176B RID: 5995
		private WsatConfiguration wsatConfig;

		// Token: 0x0400176C RID: 5996
		private WsatProxy wsatProxy;

		// Token: 0x0400176D RID: 5997
		private ProtocolVersion protocolVersion;
	}
}
