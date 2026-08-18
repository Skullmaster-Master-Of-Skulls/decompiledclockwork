using System;
using System.IO;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003B4 RID: 948
	internal sealed class MsmqIntegrationOutputChannel : TransportOutputChannel
	{
		// Token: 0x06002369 RID: 9065 RVA: 0x00081C67 File Offset: 0x0007FE67
		public MsmqIntegrationOutputChannel(MsmqIntegrationChannelFactory factory, EndpointAddress to, Uri via, bool manualAddressing) : base(factory, to, via, manualAddressing, factory.MessageVersion)
		{
			this.factory = factory;
			if (factory.IsMsmqX509SecurityConfigured)
			{
				this.certificateTokenProvider = factory.CreateX509TokenProvider(to, via);
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x00081C97 File Offset: 0x0007FE97
		private void CloseQueue()
		{
			if (this.msmqQueue != null)
			{
				this.msmqQueue.Dispose();
			}
			this.msmqQueue = null;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00081CB3 File Offset: 0x0007FEB3
		private void OnCloseCore(bool isAborting, TimeSpan timeout)
		{
			this.CloseQueue();
			if (this.certificateTokenProvider != null)
			{
				if (isAborting)
				{
					this.certificateTokenProvider.Abort();
					return;
				}
				this.certificateTokenProvider.Close(timeout);
			}
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00081CDE File Offset: 0x0007FEDE
		protected override void OnAbort()
		{
			this.OnCloseCore(true, TimeSpan.Zero);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x00081CEC File Offset: 0x0007FEEC
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnCloseCore(false, timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00081CFD File Offset: 0x0007FEFD
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00081D05 File Offset: 0x0007FF05
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseCore(false, timeout);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00081D10 File Offset: 0x0007FF10
		private void OpenQueue()
		{
			try
			{
				this.msmqQueue = new MsmqQueue(this.factory.AddressTranslator.UriToFormatName(this.RemoteAddress.Uri), 2);
			}
			catch (MsmqException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
			if (this.factory.ExactlyOnce)
			{
				this.transactionMode = MsmqTransactionMode.CurrentOrSingle;
				return;
			}
			this.transactionMode = MsmqTransactionMode.None;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00081D84 File Offset: 0x0007FF84
		private void OnOpenCore(TimeSpan timeout)
		{
			this.OpenQueue();
			if (this.certificateTokenProvider != null)
			{
				this.certificateTokenProvider.Open(timeout);
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00081DA0 File Offset: 0x0007FFA0
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnOpenCore(timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x00081DB0 File Offset: 0x0007FFB0
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x00081DB8 File Offset: 0x0007FFB8
		protected override void OnOpen(TimeSpan timeout)
		{
			this.OnOpenCore(timeout);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x00081DC1 File Offset: 0x0007FFC1
		protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnSend(message, timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x00081DD3 File Offset: 0x0007FFD3
		protected override void OnEndSend(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00081DDC File Offset: 0x0007FFDC
		protected override void OnSend(Message message, TimeSpan timeout)
		{
			MessageProperties properties = message.Properties;
			Stream stream = null;
			MsmqIntegrationMessageProperty msmqIntegrationMessageProperty = MsmqIntegrationMessageProperty.Get(message);
			if (msmqIntegrationMessageProperty == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("MsmqMessageDoesntHaveIntegrationProperty")));
			}
			if (msmqIntegrationMessageProperty.Body != null)
			{
				stream = this.factory.Serialize(msmqIntegrationMessageProperty);
			}
			int num;
			if (stream == null)
			{
				num = 0;
			}
			else
			{
				if (stream.Length > 2147483647L)
				{
					throw TraceUtility.ThrowHelperError(new ProtocolException(SR.GetString("MessageSizeMustBeInIntegerRange")), message);
				}
				num = (int)stream.Length;
			}
			using (MsmqIntegrationOutputChannel.MsmqIntegrationOutputMessage msmqIntegrationOutputMessage = new MsmqIntegrationOutputChannel.MsmqIntegrationOutputMessage(this.factory, num, this.RemoteAddress, msmqIntegrationMessageProperty))
			{
				msmqIntegrationOutputMessage.ApplyCertificateIfNeeded(this.certificateTokenProvider, this.factory.MsmqTransportSecurity.MsmqAuthenticationMode, timeout);
				if (stream != null)
				{
					stream.Position = 0L;
					int num2;
					for (int i = num; i > 0; i -= num2)
					{
						num2 = stream.Read(msmqIntegrationOutputMessage.Body.Buffer, 0, i);
					}
				}
				bool flag = false;
				try
				{
					Msmq.EnterXPSendLock(out flag, this.factory.MsmqTransportSecurity.MsmqProtectionLevel);
					this.msmqQueue.Send(msmqIntegrationOutputMessage, this.transactionMode);
					MsmqDiagnostics.DatagramSent(msmqIntegrationOutputMessage.MessageId, message);
					msmqIntegrationMessageProperty.Id = MsmqMessageId.ToString(msmqIntegrationOutputMessage.MessageId.Buffer);
				}
				catch (MsmqException ex)
				{
					if (ex.FaultSender)
					{
						base.Fault();
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
				}
				finally
				{
					if (flag)
					{
						Msmq.LeaveXPSendLock();
					}
				}
			}
		}

		// Token: 0x04002004 RID: 8196
		private MsmqQueue msmqQueue;

		// Token: 0x04002005 RID: 8197
		private MsmqTransactionMode transactionMode;

		// Token: 0x04002006 RID: 8198
		private MsmqIntegrationChannelFactory factory;

		// Token: 0x04002007 RID: 8199
		private SecurityTokenProviderContainer certificateTokenProvider;

		// Token: 0x02000B9E RID: 2974
		private class MsmqIntegrationOutputMessage : MsmqOutputMessage<IOutputChannel>
		{
			// Token: 0x0600739B RID: 29595 RVA: 0x001AF698 File Offset: 0x001AD898
			public MsmqIntegrationOutputMessage(MsmqChannelFactoryBase<IOutputChannel> factory, int bodySize, EndpointAddress remoteAddress, MsmqIntegrationMessageProperty property) : base(factory, bodySize, remoteAddress, 8)
			{
				if (property.AcknowledgeType != null)
				{
					this.EnsureAcknowledgeProperty((byte)property.AcknowledgeType.Value);
				}
				if (null != property.AdministrationQueue)
				{
					this.EnsureAdminQueueProperty(property.AdministrationQueue, false);
				}
				if (property.AppSpecific != null)
				{
					this.appSpecific = new NativeMsmqMessage.IntProperty(this, 8, property.AppSpecific.Value);
				}
				if (property.BodyType != null)
				{
					base.EnsureBodyTypeProperty(property.BodyType.Value);
				}
				if (property.CorrelationId != null)
				{
					this.correlationId = new NativeMsmqMessage.BufferProperty(this, 3, MsmqMessageId.FromString(property.CorrelationId));
				}
				if (property.Extension != null)
				{
					this.extension = new NativeMsmqMessage.BufferProperty(this, 35, property.Extension);
				}
				if (property.Label != null)
				{
					this.label = new NativeMsmqMessage.StringProperty(this, 11, property.Label);
				}
				if (property.Priority != null)
				{
					this.priority = new NativeMsmqMessage.ByteProperty(this, 4, (byte)property.Priority.Value);
				}
				if (null != property.ResponseQueue)
				{
					this.EnsureResponseQueueProperty(property.ResponseQueue);
				}
				if (property.TimeToReachQueue != null)
				{
					base.EnsureTimeToReachQueueProperty(MsmqDuration.FromTimeSpan(property.TimeToReachQueue.Value));
				}
			}

			// Token: 0x0600739C RID: 29596 RVA: 0x001AF81C File Offset: 0x001ADA1C
			private void EnsureAcknowledgeProperty(byte value)
			{
				if (this.acknowledge == null)
				{
					this.acknowledge = new NativeMsmqMessage.ByteProperty(this, 6);
				}
				this.acknowledge.Value = value;
			}

			// Token: 0x0600739D RID: 29597 RVA: 0x001AF840 File Offset: 0x001ADA40
			private void EnsureAdminQueueProperty(Uri value, bool useNetMsmqTranslator)
			{
				if (null != value)
				{
					string value2 = useNetMsmqTranslator ? MsmqUri.NetMsmqAddressTranslator.UriToFormatName(value) : MsmqUri.FormatNameAddressTranslator.UriToFormatName(value);
					if (this.adminQueue == null)
					{
						this.adminQueue = new NativeMsmqMessage.StringProperty(this, 17, value2);
						return;
					}
					this.adminQueue.SetValue(value2);
				}
			}

			// Token: 0x0600739E RID: 29598 RVA: 0x001AF898 File Offset: 0x001ADA98
			private void EnsureResponseQueueProperty(Uri value)
			{
				if (null != value)
				{
					string value2 = MsmqUri.FormatNameAddressTranslator.UriToFormatName(value);
					if (this.responseQueue == null)
					{
						this.responseQueue = new NativeMsmqMessage.StringProperty(this, 54, value2);
						return;
					}
					this.responseQueue.SetValue(value2);
				}
			}

			// Token: 0x04004176 RID: 16758
			private NativeMsmqMessage.ByteProperty acknowledge;

			// Token: 0x04004177 RID: 16759
			private NativeMsmqMessage.StringProperty adminQueue;

			// Token: 0x04004178 RID: 16760
			private NativeMsmqMessage.IntProperty appSpecific;

			// Token: 0x04004179 RID: 16761
			private NativeMsmqMessage.BufferProperty correlationId;

			// Token: 0x0400417A RID: 16762
			private NativeMsmqMessage.BufferProperty extension;

			// Token: 0x0400417B RID: 16763
			private NativeMsmqMessage.StringProperty label;

			// Token: 0x0400417C RID: 16764
			private NativeMsmqMessage.ByteProperty priority;

			// Token: 0x0400417D RID: 16765
			private NativeMsmqMessage.StringProperty responseQueue;
		}
	}
}
