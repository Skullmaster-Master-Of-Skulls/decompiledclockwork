using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;
using System.ServiceModel.Transactions;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A61 RID: 2657
	internal abstract class TransactionChannel<TChannel> : LayeredChannel<TChannel>, ITransactionChannel where TChannel : class, IChannel
	{
		// Token: 0x060068F1 RID: 26865 RVA: 0x0018827C File Offset: 0x0018647C
		protected TransactionChannel(ChannelManagerBase channelManager, TChannel innerChannel) : base(channelManager, innerChannel)
		{
			this.factory = (ITransactionChannelManager)channelManager;
			if (this.factory.TransactionProtocol == TransactionProtocol.OleTransactions)
			{
				this.formatter = TransactionFormatter.OleTxFormatter;
				return;
			}
			if (this.factory.TransactionProtocol == TransactionProtocol.WSAtomicTransactionOctober2004)
			{
				this.formatter = TransactionFormatter.WsatFormatter10;
				return;
			}
			if (this.factory.TransactionProtocol == TransactionProtocol.WSAtomicTransaction11)
			{
				this.formatter = TransactionFormatter.WsatFormatter11;
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxBadTransactionProtocols")));
		}

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x060068F2 RID: 26866 RVA: 0x00188310 File Offset: 0x00186510
		internal TransactionFormatter Formatter
		{
			get
			{
				return this.formatter;
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x060068F3 RID: 26867 RVA: 0x00188318 File Offset: 0x00186518
		internal TransactionProtocol Protocol
		{
			get
			{
				return this.factory.TransactionProtocol;
			}
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x00188325 File Offset: 0x00186525
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(FaultConverter))
			{
				return (T)((object)new TransactionChannelFaultConverter<TChannel>(this));
			}
			return base.GetProperty<T>();
		}

		// Token: 0x060068F5 RID: 26869 RVA: 0x00188354 File Offset: 0x00186554
		public T GetInnerProperty<T>() where T : class
		{
			return base.InnerChannel.GetProperty<T>();
		}

		// Token: 0x060068F6 RID: 26870 RVA: 0x00188366 File Offset: 0x00186566
		private static bool Found(int index)
		{
			return index != -1;
		}

		// Token: 0x060068F7 RID: 26871 RVA: 0x00188370 File Offset: 0x00186570
		private void FaultOnMessage(Message message, string reason, string codeString)
		{
			FaultCode code = FaultCode.CreateSenderFaultCode(codeString, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/transactions");
			FaultException exception = new FaultException(reason, code, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/transactions/fault");
			throw TraceUtility.ThrowHelperError(exception, message);
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x0018839D File Offset: 0x0018659D
		private ICollection<RequestSecurityTokenResponse> GetIssuedTokens(Message message)
		{
			return IssuedTokensHeader.ExtractIssuances(message, this.factory.StandardsManager, message.Version.Envelope.UltimateDestinationActorValues, null);
		}

		// Token: 0x060068F9 RID: 26873 RVA: 0x001883C4 File Offset: 0x001865C4
		public void ReadIssuedTokens(Message message, MessageDirection direction)
		{
			TransactionFlowOption flowIssuedTokens = this.factory.FlowIssuedTokens;
			ICollection<RequestSecurityTokenResponse> issuedTokens = this.GetIssuedTokens(message);
			if (issuedTokens != null && issuedTokens.Count != 0)
			{
				if (flowIssuedTokens == TransactionFlowOption.NotAllowed)
				{
					this.FaultOnMessage(message, SR.GetString("IssuedTokenFlowNotAllowed"), "IssuedTokenFlowNotAllowed");
				}
				foreach (RequestSecurityTokenResponse item in issuedTokens)
				{
					TransactionFlowProperty.Ensure(message).IssuedTokens.Add(item);
				}
			}
		}

		// Token: 0x060068FA RID: 26874 RVA: 0x00188450 File Offset: 0x00186650
		private void ReadTransactionFromMessage(Message message, TransactionFlowOption txFlowOption)
		{
			TransactionInfo transactionInfo = null;
			try
			{
				transactionInfo = this.formatter.ReadTransaction(message);
			}
			catch (TransactionException ex)
			{
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Error);
				this.FaultOnMessage(message, SR.GetString("SFxTransactionDeserializationFailed", new object[]
				{
					ex.Message
				}), "TransactionHeaderMalformed");
			}
			if (transactionInfo != null)
			{
				TransactionMessageProperty.Set(transactionInfo, message);
				return;
			}
			if (txFlowOption == TransactionFlowOption.Mandatory)
			{
				this.FaultOnMessage(message, SR.GetString("SFxTransactionFlowRequired"), "TransactionHeaderMissing");
			}
		}

		// Token: 0x060068FB RID: 26875 RVA: 0x001884D4 File Offset: 0x001866D4
		public virtual void ReadTransactionDataFromMessage(Message message, MessageDirection direction)
		{
			this.ReadIssuedTokens(message, direction);
			TransactionFlowOption transaction = this.factory.GetTransaction(direction, message.Headers.Action);
			if (TransactionFlowOptionHelper.AllowedOrRequired(transaction))
			{
				this.ReadTransactionFromMessage(message, transaction);
			}
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x00188514 File Offset: 0x00186714
		public void WriteTransactionDataToMessage(Message message, MessageDirection direction)
		{
			TransactionFlowOption transaction = this.factory.GetTransaction(direction, message.Headers.Action);
			if (TransactionFlowOptionHelper.AllowedOrRequired(transaction))
			{
				this.WriteTransactionToMessage(message, transaction);
			}
			if (TransactionFlowOptionHelper.AllowedOrRequired(this.factory.FlowIssuedTokens))
			{
				this.WriteIssuedTokens(message, direction);
			}
		}

		// Token: 0x060068FD RID: 26877 RVA: 0x00188564 File Offset: 0x00186764
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void WriteTransactionToMessage(Message message, TransactionFlowOption txFlowOption)
		{
			Transaction transaction = TransactionFlowProperty.TryGetTransaction(message);
			if (transaction != null)
			{
				try
				{
					this.formatter.WriteTransaction(transaction, message);
					return;
				}
				catch (TransactionException ex)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(ex.Message, ex));
				}
			}
			if (txFlowOption == TransactionFlowOption.Mandatory)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("SFxTransactionFlowRequired")));
			}
		}

		// Token: 0x060068FE RID: 26878 RVA: 0x001885D8 File Offset: 0x001867D8
		public void WriteIssuedTokens(Message message, MessageDirection direction)
		{
			ICollection<RequestSecurityTokenResponse> collection = TransactionFlowProperty.TryGetIssuedTokens(message);
			if (collection != null)
			{
				IssuedTokensHeader header = new IssuedTokensHeader(collection, this.factory.StandardsManager);
				message.Headers.Add(header);
			}
		}

		// Token: 0x04003C20 RID: 15392
		private ITransactionChannelManager factory;

		// Token: 0x04003C21 RID: 15393
		private TransactionFormatter formatter;
	}
}
