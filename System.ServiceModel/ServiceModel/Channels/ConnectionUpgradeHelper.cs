using System;
using System.IO;
using System.Runtime;
using System.Security.Principal;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007E9 RID: 2025
	internal class ConnectionUpgradeHelper
	{
		// Token: 0x06004CA2 RID: 19618 RVA: 0x00117986 File Offset: 0x00115B86
		public static IAsyncResult BeginDecodeFramingFault(ClientFramingDecoder decoder, IConnection connection, Uri via, string contentType, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
		{
			return new ConnectionUpgradeHelper.DecodeFailedUpgradeAsyncResult(decoder, connection, via, contentType, ref timeoutHelper, callback, state);
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x00117997 File Offset: 0x00115B97
		public static void EndDecodeFramingFault(IAsyncResult result)
		{
			ConnectionUpgradeHelper.DecodeFailedUpgradeAsyncResult.End(result);
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x001179A0 File Offset: 0x00115BA0
		public static void DecodeFramingFault(ClientFramingDecoder decoder, IConnection connection, Uri via, string contentType, ref TimeoutHelper timeoutHelper)
		{
			ConnectionUpgradeHelper.ValidateReadingFaultString(decoder);
			int num = 0;
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(256);
			int i = connection.Read(array, num, array.Length, timeoutHelper.RemainingTime());
			while (i > 0)
			{
				int num2 = decoder.Decode(array, num, i);
				num += num2;
				i -= num2;
				if (decoder.CurrentState == ClientFramingDecoderState.Fault)
				{
					ConnectionUtilities.CloseNoThrow(connection, timeoutHelper.RemainingTime());
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(FaultStringDecoder.GetFaultException(decoder.Fault, via.ToString(), contentType));
				}
				if (decoder.CurrentState != ClientFramingDecoderState.ReadingFaultString)
				{
					throw Fx.AssertAndThrow("invalid framing client state machine");
				}
				if (i == 0)
				{
					num = 0;
					i = connection.Read(array, num, array.Length, timeoutHelper.RemainingTime());
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(decoder.CreatePrematureEOFException());
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x00117A60 File Offset: 0x00115C60
		public static IAsyncResult BeginInitiateUpgrade(IDefaultCommunicationTimeouts timeouts, EndpointAddress remoteAddress, IConnection connection, ClientFramingDecoder decoder, StreamUpgradeInitiator upgradeInitiator, string contentType, WindowsIdentity identityToImpersonate, TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
		{
			return new ConnectionUpgradeHelper.InitiateUpgradeAsyncResult(timeouts, remoteAddress, connection, decoder, upgradeInitiator, contentType, identityToImpersonate, timeoutHelper, callback, state);
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x00117A82 File Offset: 0x00115C82
		public static IConnection EndInitiateUpgrade(IAsyncResult result)
		{
			return ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.End(result);
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x00117A8C File Offset: 0x00115C8C
		public static bool InitiateUpgrade(StreamUpgradeInitiator upgradeInitiator, ref IConnection connection, ClientFramingDecoder decoder, IDefaultCommunicationTimeouts defaultTimeouts, ref TimeoutHelper timeoutHelper)
		{
			for (string nextUpgrade = upgradeInitiator.GetNextUpgrade(); nextUpgrade != null; nextUpgrade = upgradeInitiator.GetNextUpgrade())
			{
				EncodedUpgrade encodedUpgrade = new EncodedUpgrade(nextUpgrade);
				connection.Write(encodedUpgrade.EncodedBytes, 0, encodedUpgrade.EncodedBytes.Length, true, timeoutHelper.RemainingTime());
				byte[] array = new byte[1];
				int count = connection.Read(array, 0, array.Length, timeoutHelper.RemainingTime());
				if (!ConnectionUpgradeHelper.ValidateUpgradeResponse(array, count, decoder))
				{
					return false;
				}
				ConnectionStream connectionStream = new ConnectionStream(connection, defaultTimeouts, timeoutHelper.RemainingTime(), true);
				Stream stream = upgradeInitiator.InitiateUpgrade(connectionStream);
				connectionStream.CompleteOpen();
				connection = new StreamConnection(stream, connectionStream);
			}
			return true;
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x00117B2D File Offset: 0x00115D2D
		private static void ValidateReadingFaultString(ClientFramingDecoder decoder)
		{
			if (decoder.CurrentState != ClientFramingDecoderState.ReadingFaultString)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("ServerRejectedUpgradeRequest")));
			}
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x00117B54 File Offset: 0x00115D54
		public static bool ValidatePreambleResponse(byte[] buffer, int count, ClientFramingDecoder decoder, Uri via)
		{
			if (count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("ServerRejectedSessionPreamble", new object[]
				{
					via
				}), decoder.CreatePrematureEOFException()));
			}
			while (decoder.Decode(buffer, 0, count) == 0)
			{
			}
			return decoder.CurrentState == ClientFramingDecoderState.Start;
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x00117BA5 File Offset: 0x00115DA5
		private static bool ValidateUpgradeResponse(byte[] buffer, int count, ClientFramingDecoder decoder)
		{
			if (count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("ServerRejectedUpgradeRequest"), decoder.CreatePrematureEOFException()));
			}
			while (decoder.Decode(buffer, 0, count) == 0)
			{
			}
			return decoder.CurrentState == ClientFramingDecoderState.UpgradeResponse;
		}

		// Token: 0x02000D0C RID: 3340
		private class DecodeFailedUpgradeAsyncResult : AsyncResult
		{
			// Token: 0x06007B21 RID: 31521 RVA: 0x001CACB0 File Offset: 0x001C8EB0
			public DecodeFailedUpgradeAsyncResult(ClientFramingDecoder decoder, IConnection connection, Uri via, string contentType, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state) : base(callback, state)
			{
				ConnectionUpgradeHelper.ValidateReadingFaultString(decoder);
				this.decoder = decoder;
				this.connection = connection;
				this.via = via;
				this.contentType = contentType;
				this.timeoutHelper = timeoutHelper;
				if (connection.BeginRead(0, Math.Min(256, connection.AsyncReadBufferSize), timeoutHelper.RemainingTime(), ConnectionUpgradeHelper.DecodeFailedUpgradeAsyncResult.onReadFaultData, this) == AsyncCompletionResult.Queued)
				{
					return;
				}
				this.CompleteReadFaultData();
			}

			// Token: 0x06007B22 RID: 31522 RVA: 0x001CAD24 File Offset: 0x001C8F24
			private void CompleteReadFaultData()
			{
				int num = 0;
				int i = this.connection.EndRead();
				while (i > 0)
				{
					int num2 = this.decoder.Decode(this.connection.AsyncReadBuffer, num, i);
					num += num2;
					i -= num2;
					if (this.decoder.CurrentState == ClientFramingDecoderState.Fault)
					{
						ConnectionUtilities.CloseNoThrow(this.connection, this.timeoutHelper.RemainingTime());
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(FaultStringDecoder.GetFaultException(this.decoder.Fault, this.via.ToString(), this.contentType));
					}
					if (this.decoder.CurrentState != ClientFramingDecoderState.ReadingFaultString)
					{
						throw Fx.AssertAndThrow("invalid framing client state machine");
					}
					if (i == 0)
					{
						num = 0;
						if (this.connection.BeginRead(0, Math.Min(256, this.connection.AsyncReadBufferSize), this.timeoutHelper.RemainingTime(), ConnectionUpgradeHelper.DecodeFailedUpgradeAsyncResult.onReadFaultData, this) == AsyncCompletionResult.Queued)
						{
							return;
						}
						i = this.connection.EndRead();
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.decoder.CreatePrematureEOFException());
			}

			// Token: 0x06007B23 RID: 31523 RVA: 0x001CAE30 File Offset: 0x001C9030
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ConnectionUpgradeHelper.DecodeFailedUpgradeAsyncResult>(result);
			}

			// Token: 0x06007B24 RID: 31524 RVA: 0x001CAE3C File Offset: 0x001C903C
			private static void OnReadFaultData(object state)
			{
				ConnectionUpgradeHelper.DecodeFailedUpgradeAsyncResult decodeFailedUpgradeAsyncResult = (ConnectionUpgradeHelper.DecodeFailedUpgradeAsyncResult)state;
				Exception ex = null;
				try
				{
					decodeFailedUpgradeAsyncResult.CompleteReadFaultData();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					decodeFailedUpgradeAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x04004661 RID: 18017
			private ClientFramingDecoder decoder;

			// Token: 0x04004662 RID: 18018
			private IConnection connection;

			// Token: 0x04004663 RID: 18019
			private Uri via;

			// Token: 0x04004664 RID: 18020
			private string contentType;

			// Token: 0x04004665 RID: 18021
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004666 RID: 18022
			private static WaitCallback onReadFaultData = new WaitCallback(ConnectionUpgradeHelper.DecodeFailedUpgradeAsyncResult.OnReadFaultData);
		}

		// Token: 0x02000D0D RID: 3341
		private class InitiateUpgradeAsyncResult : AsyncResult
		{
			// Token: 0x06007B26 RID: 31526 RVA: 0x001CAE98 File Offset: 0x001C9098
			public InitiateUpgradeAsyncResult(IDefaultCommunicationTimeouts timeouts, EndpointAddress remoteAddress, IConnection connection, ClientFramingDecoder decoder, StreamUpgradeInitiator upgradeInitiator, string contentType, WindowsIdentity identityToImpersonate, TimeoutHelper timeoutHelper, AsyncCallback callback, object state) : base(callback, state)
			{
				this.defaultTimeouts = timeouts;
				this.decoder = decoder;
				this.upgradeInitiator = upgradeInitiator;
				this.contentType = contentType;
				this.timeoutHelper = timeoutHelper;
				this.connection = connection;
				this.remoteAddress = remoteAddress;
				this.identityToImpersonate = identityToImpersonate;
				if (this.Begin())
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007B27 RID: 31527 RVA: 0x001CAEFC File Offset: 0x001C90FC
			private bool Begin()
			{
				for (string nextUpgrade = this.upgradeInitiator.GetNextUpgrade(); nextUpgrade != null; nextUpgrade = this.upgradeInitiator.GetNextUpgrade())
				{
					EncodedUpgrade encodedUpgrade = new EncodedUpgrade(nextUpgrade);
					if (this.connection.BeginWrite(encodedUpgrade.EncodedBytes, 0, encodedUpgrade.EncodedBytes.Length, true, this.timeoutHelper.RemainingTime(), ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.onWriteUpgradeBytes, this) == AsyncCompletionResult.Queued)
					{
						return false;
					}
					if (!this.CompleteWriteUpgradeBytes())
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06007B28 RID: 31528 RVA: 0x001CAF6A File Offset: 0x001C916A
			private bool CompleteWriteUpgradeBytes()
			{
				this.connection.EndWrite();
				return this.connection.BeginRead(0, ServerSessionEncoder.UpgradeResponseBytes.Length, this.timeoutHelper.RemainingTime(), ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.onReadUpgradeResponse, this) != AsyncCompletionResult.Queued && this.CompleteReadUpgradeResponse();
			}

			// Token: 0x06007B29 RID: 31529 RVA: 0x001CAFA8 File Offset: 0x001C91A8
			private bool CompleteReadUpgradeResponse()
			{
				int count = this.connection.EndRead();
				if (!ConnectionUpgradeHelper.ValidateUpgradeResponse(this.connection.AsyncReadBuffer, count, this.decoder))
				{
					if (ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.onFailedUpgrade == null)
					{
						ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.onFailedUpgrade = Fx.ThunkCallback(new AsyncCallback(ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.OnFailedUpgrade));
					}
					IAsyncResult asyncResult = ConnectionUpgradeHelper.BeginDecodeFramingFault(this.decoder, this.connection, this.remoteAddress.Uri, this.contentType, ref this.timeoutHelper, ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.onFailedUpgrade, this);
					if (asyncResult.CompletedSynchronously)
					{
						ConnectionUpgradeHelper.EndDecodeFramingFault(asyncResult);
					}
					return asyncResult.CompletedSynchronously;
				}
				this.connectionStream = new ConnectionStream(this.connection, this.defaultTimeouts, this.timeoutHelper.RemainingTime(), true);
				IAsyncResult asyncResult2 = null;
				WindowsImpersonationContext windowsImpersonationContext = (this.identityToImpersonate == null) ? null : this.identityToImpersonate.Impersonate();
				try
				{
					using (windowsImpersonationContext)
					{
						asyncResult2 = this.upgradeInitiator.BeginInitiateUpgrade(this.connectionStream, ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.onInitiateUpgrade, this);
					}
				}
				catch
				{
					throw;
				}
				if (!asyncResult2.CompletedSynchronously)
				{
					return false;
				}
				this.CompleteUpgrade(asyncResult2);
				return true;
			}

			// Token: 0x06007B2A RID: 31530 RVA: 0x001CB0D4 File Offset: 0x001C92D4
			private void CompleteUpgrade(IAsyncResult result)
			{
				Stream stream = this.upgradeInitiator.EndInitiateUpgrade(result);
				this.connectionStream.CompleteOpen();
				this.connection = new StreamConnection(stream, this.connectionStream);
			}

			// Token: 0x06007B2B RID: 31531 RVA: 0x001CB10C File Offset: 0x001C930C
			public static IConnection End(IAsyncResult result)
			{
				ConnectionUpgradeHelper.InitiateUpgradeAsyncResult initiateUpgradeAsyncResult = AsyncResult.End<ConnectionUpgradeHelper.InitiateUpgradeAsyncResult>(result);
				return initiateUpgradeAsyncResult.connection;
			}

			// Token: 0x06007B2C RID: 31532 RVA: 0x001CB128 File Offset: 0x001C9328
			private static void OnReadUpgradeResponse(object state)
			{
				ConnectionUpgradeHelper.InitiateUpgradeAsyncResult initiateUpgradeAsyncResult = (ConnectionUpgradeHelper.InitiateUpgradeAsyncResult)state;
				Exception exception = null;
				bool flag = false;
				try
				{
					if (initiateUpgradeAsyncResult.CompleteReadUpgradeResponse())
					{
						flag = initiateUpgradeAsyncResult.Begin();
					}
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
					initiateUpgradeAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B2D RID: 31533 RVA: 0x001CB180 File Offset: 0x001C9380
			private static void OnFailedUpgrade(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ConnectionUpgradeHelper.InitiateUpgradeAsyncResult initiateUpgradeAsyncResult = (ConnectionUpgradeHelper.InitiateUpgradeAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					ConnectionUpgradeHelper.EndDecodeFramingFault(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				initiateUpgradeAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007B2E RID: 31534 RVA: 0x001CB1D4 File Offset: 0x001C93D4
			private static void OnWriteUpgradeBytes(object asyncState)
			{
				ConnectionUpgradeHelper.InitiateUpgradeAsyncResult initiateUpgradeAsyncResult = (ConnectionUpgradeHelper.InitiateUpgradeAsyncResult)asyncState;
				Exception exception = null;
				bool flag = false;
				try
				{
					if (initiateUpgradeAsyncResult.CompleteWriteUpgradeBytes())
					{
						flag = initiateUpgradeAsyncResult.Begin();
					}
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
					initiateUpgradeAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007B2F RID: 31535 RVA: 0x001CB22C File Offset: 0x001C942C
			private static void OnInitiateUpgrade(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ConnectionUpgradeHelper.InitiateUpgradeAsyncResult initiateUpgradeAsyncResult = (ConnectionUpgradeHelper.InitiateUpgradeAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag;
				try
				{
					initiateUpgradeAsyncResult.CompleteUpgrade(result);
					flag = initiateUpgradeAsyncResult.Begin();
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
					initiateUpgradeAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x04004667 RID: 18023
			private IDefaultCommunicationTimeouts defaultTimeouts;

			// Token: 0x04004668 RID: 18024
			private IConnection connection;

			// Token: 0x04004669 RID: 18025
			private ConnectionStream connectionStream;

			// Token: 0x0400466A RID: 18026
			private string contentType;

			// Token: 0x0400466B RID: 18027
			private ClientFramingDecoder decoder;

			// Token: 0x0400466C RID: 18028
			private static AsyncCallback onInitiateUpgrade = Fx.ThunkCallback(new AsyncCallback(ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.OnInitiateUpgrade));

			// Token: 0x0400466D RID: 18029
			private static WaitCallback onReadUpgradeResponse = new WaitCallback(ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.OnReadUpgradeResponse);

			// Token: 0x0400466E RID: 18030
			private static AsyncCallback onFailedUpgrade;

			// Token: 0x0400466F RID: 18031
			private static WaitCallback onWriteUpgradeBytes = Fx.ThunkCallback(new WaitCallback(ConnectionUpgradeHelper.InitiateUpgradeAsyncResult.OnWriteUpgradeBytes));

			// Token: 0x04004670 RID: 18032
			private EndpointAddress remoteAddress;

			// Token: 0x04004671 RID: 18033
			private StreamUpgradeInitiator upgradeInitiator;

			// Token: 0x04004672 RID: 18034
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004673 RID: 18035
			private WindowsIdentity identityToImpersonate;
		}
	}
}
