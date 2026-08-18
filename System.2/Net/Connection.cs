using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A3 RID: 419
	internal class Connection : PooledStream
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00053B19 File Offset: 0x00051D19
		internal override ServicePoint ServicePoint
		{
			get
			{
				return this.ConnectionGroup.ServicePoint;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00053B26 File Offset: 0x00051D26
		private ConnectionGroup ConnectionGroup
		{
			get
			{
				return this.m_ConnectionGroup;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00053B2E File Offset: 0x00051D2E
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x00053B38 File Offset: 0x00051D38
		internal HttpWebRequest LockedRequest
		{
			get
			{
				return this.m_LockedRequest;
			}
			set
			{
				HttpWebRequest lockedRequest = this.m_LockedRequest;
				if (value == lockedRequest)
				{
					if (value != null && value.UnlockConnectionDelegate != this.m_ConnectionUnlock)
					{
						throw new InternalException();
					}
					return;
				}
				else
				{
					object obj = (lockedRequest == null) ? null : lockedRequest.UnlockConnectionDelegate;
					if (obj != null && (value != null || this.m_ConnectionUnlock != obj))
					{
						throw new InternalException();
					}
					if (value == null)
					{
						this.m_LockedRequest = null;
						lockedRequest.UnlockConnectionDelegate = null;
						return;
					}
					UnlockConnectionDelegate unlockConnectionDelegate = value.UnlockConnectionDelegate;
					if (unlockConnectionDelegate != null)
					{
						if (unlockConnectionDelegate == this.m_ConnectionUnlock)
						{
							throw new InternalException();
						}
						unlockConnectionDelegate();
					}
					value.UnlockConnectionDelegate = this.m_ConnectionUnlock;
					this.m_LockedRequest = value;
					return;
				}
			}
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00053BCE File Offset: 0x00051DCE
		private void UnlockRequest()
		{
			this.LockedRequest = null;
			if (this.ConnectionGroup != null)
			{
				this.ConnectionGroup.ConnectionGoneIdle();
			}
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00053BEC File Offset: 0x00051DEC
		internal Connection(ConnectionGroup connectionGroup) : base(null)
		{
			this.m_MaximumUnauthorizedUploadLength = (long)SettingsSectionInternal.Section.MaximumUnauthorizedUploadLength;
			if (this.m_MaximumUnauthorizedUploadLength > 0L)
			{
				this.m_MaximumUnauthorizedUploadLength *= 1024L;
			}
			this.m_ResponseData = new CoreResponseData();
			this.m_ConnectionGroup = connectionGroup;
			if (ServicePointManager.UseHttpPipeliningAndBufferPooling)
			{
				this.m_ReadBuffer = Connection.s_PinnableBufferCache.AllocateBuffer();
				this.m_ReadBufferFromPinnableCache = true;
			}
			else
			{
				this.m_ReadBuffer = new byte[4096];
			}
			this.m_ReadState = ReadState.Start;
			this.m_WaitList = new List<Connection.WaitListItem>();
			this.m_WriteList = new ArrayList();
			this.m_AbortDelegate = new HttpAbortDelegate(this.AbortOrDisassociate);
			this.m_ConnectionUnlock = new UnlockConnectionDelegate(this.UnlockRequest);
			this.m_StatusLineValues = new Connection.StatusLineValues();
			this.m_RecycleTimer = this.ConnectionGroup.ServicePoint.ConnectionLeaseTimerQueue.CreateTimer();
			this.ConnectionGroup.Associate(this);
			this.m_ReadDone = true;
			this.m_WriteDone = true;
			this.m_Error = WebExceptionStatus.Success;
			if (PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				PinnableBufferCacheEventSource.Log.DebugMessage1("CTOR: In System.Net.Connection.Connnection", (long)this.GetHashCode());
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00053D38 File Offset: 0x00051F38
		~Connection()
		{
			if (this.m_ReadBufferFromPinnableCache && PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				PinnableBufferCacheEventSource.Log.DebugMessage1("DTOR: ERROR Needing to Free m_ReadBuffer in Connection Destructor", (long)this.m_ReadBuffer.GetHashCode());
			}
			this.FreeReadBuffer();
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00053D94 File Offset: 0x00051F94
		private void FreeReadBuffer()
		{
			if (this.m_ReadBufferFromPinnableCache)
			{
				Connection.s_PinnableBufferCache.FreeBuffer(this.m_ReadBuffer);
				this.m_ReadBufferFromPinnableCache = false;
			}
			this.m_ReadBuffer = null;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00053DBC File Offset: 0x00051FBC
		protected override void Dispose(bool disposing)
		{
			if (PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				PinnableBufferCacheEventSource.Log.DebugMessage1("In System.Net.Connection.Dispose()", (long)this.GetHashCode());
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00053DE7 File Offset: 0x00051FE7
		internal int BusyCount
		{
			get
			{
				return (this.m_ReadDone ? 0 : 1) + 2 * (this.m_WaitList.Count + this.m_WriteList.Count) + this.m_ReservedCount;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x00053E16 File Offset: 0x00052016
		internal int IISVersion
		{
			get
			{
				return this.m_IISVersion;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00053E1E File Offset: 0x0005201E
		internal bool AtLeastOneResponseReceived
		{
			get
			{
				return this.m_AtLeastOneResponseReceived;
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00053E28 File Offset: 0x00052028
		internal bool SubmitRequest(HttpWebRequest request, bool forcedsubmit)
		{
			TriState triState = TriState.Unspecified;
			ConnectionReturnResult responses = null;
			bool flag = false;
			lock (this)
			{
				request.AbortDelegate = this.m_AbortDelegate;
				if (request.Aborted)
				{
					this.UnlockIfNeeded(request);
					return true;
				}
				if (!base.CanBePooled)
				{
					this.UnlockIfNeeded(request);
					return false;
				}
				if (!forcedsubmit && this.NonKeepAliveRequestPipelined)
				{
					this.UnlockIfNeeded(request);
					return false;
				}
				if (this.m_RecycleTimer.Duration != this.ServicePoint.ConnectionLeaseTimerQueue.Duration)
				{
					this.m_RecycleTimer.Cancel();
					this.m_RecycleTimer = this.ServicePoint.ConnectionLeaseTimerQueue.CreateTimer();
				}
				if (this.m_RecycleTimer.HasExpired)
				{
					request.KeepAlive = false;
				}
				if (this.LockedRequest != null && this.LockedRequest != request)
				{
					return false;
				}
				if (!forcedsubmit && !this.m_NonKeepAliveRequestPipelined)
				{
					this.m_NonKeepAliveRequestPipelined = (!request.KeepAlive && !request.NtlmKeepAlive);
				}
				if (this.m_Free && this.m_WriteDone && (this.m_WriteList.Count == 0 || (request.Pipelined && !request.HasEntityBody && this.m_CanPipeline && this.m_Pipelining && !this.m_IsPipelinePaused && !forcedsubmit)))
				{
					this.m_Free = false;
					triState = this.StartRequest(request, true);
					if (triState == TriState.Unspecified)
					{
						flag = true;
						this.PrepareCloseConnectionSocket(ref responses, 0);
						base.Close(0);
					}
				}
				else
				{
					this.m_WaitList.Add(new Connection.WaitListItem(request, NetworkingPerfCounters.GetTimestamp()));
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.HttpWebRequestQueued);
					this.CheckNonIdle();
				}
			}
			if (flag)
			{
				ConnectionReturnResult.SetResponses(responses);
				return false;
			}
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, request);
			}
			if (triState != TriState.Unspecified)
			{
				this.CompleteStartRequest(true, request, triState);
			}
			if (!request.Async)
			{
				object obj = request.ConnectionAsyncResult.InternalWaitForCompletion();
				ConnectStream connectStream = obj as ConnectStream;
				Connection.AsyncTriState asyncTriState = null;
				if (connectStream == null)
				{
					asyncTriState = (obj as Connection.AsyncTriState);
				}
				if (triState == TriState.Unspecified && asyncTriState != null)
				{
					this.CompleteStartRequest(true, request, asyncTriState.Value);
				}
				else if (connectStream != null)
				{
					request.SetRequestSubmitDone(connectStream);
				}
			}
			return true;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00054070 File Offset: 0x00052270
		private void UnlockIfNeeded(HttpWebRequest request)
		{
			if (this.LockedRequest == request)
			{
				this.UnlockRequest();
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00054084 File Offset: 0x00052284
		private TriState StartRequest(HttpWebRequest request, bool canPollRead)
		{
			if (this.m_WriteList.Count == 0)
			{
				if (this.ServicePoint.MaxIdleTime != -1 && this.m_IdleSinceUtc != DateTime.MinValue && this.m_IdleSinceUtc + TimeSpan.FromMilliseconds((double)this.ServicePoint.MaxIdleTime) < DateTime.UtcNow)
				{
					return TriState.Unspecified;
				}
				if (canPollRead && !this.IsConnectionReusable())
				{
					return TriState.Unspecified;
				}
			}
			TriState result = TriState.False;
			this.m_IdleSinceUtc = DateTime.MinValue;
			if (!this.m_IsPipelinePaused)
			{
				this.m_IsPipelinePaused = (this.m_WriteList.Count >= Connection.s_MaxPipelinedCount);
			}
			this.m_Pipelining = (this.m_CanPipeline && request.Pipelined && !request.HasEntityBody);
			this.m_WriteDone = false;
			this.m_WriteList.Add(request);
			this.CheckNonIdle();
			if (base.IsInitalizing)
			{
				result = TriState.True;
			}
			return result;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0005416C File Offset: 0x0005236C
		private bool IsConnectionReusable()
		{
			try
			{
				if (base.PollRead())
				{
					return false;
				}
			}
			catch (SocketException ex)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, this, "IsConnectionReusable", ex.ToString());
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000541BC File Offset: 0x000523BC
		private void CompleteStartRequest(bool onSubmitThread, HttpWebRequest request, TriState needReConnect)
		{
			if (needReConnect == TriState.True)
			{
				try
				{
					if (request.Async)
					{
						this.CompleteStartConnection(true, request);
					}
					else if (onSubmitThread)
					{
						this.CompleteStartConnection(false, request);
					}
				}
				catch (Exception exception)
				{
					if (NclUtilities.IsFatal(exception))
					{
						throw;
					}
				}
				if (!request.Async)
				{
					request.ConnectionAsyncResult.InvokeCallback(new Connection.AsyncTriState(needReConnect));
				}
				return;
			}
			if (request.Async)
			{
				request.OpenWriteSideResponseWindow();
			}
			ConnectStream connectStream = new ConnectStream(this, request);
			if (request.Async || onSubmitThread)
			{
				request.SetRequestSubmitDone(connectStream);
				return;
			}
			request.ConnectionAsyncResult.InvokeCallback(connectStream);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00054258 File Offset: 0x00052458
		private HttpWebRequest CheckNextRequest()
		{
			if (this.m_WaitList.Count == 0)
			{
				this.m_Free = this.m_KeepAlive;
				return null;
			}
			if (!base.CanBePooled)
			{
				return null;
			}
			Connection.WaitListItem waitListItem = this.m_WaitList[0];
			HttpWebRequest httpWebRequest = waitListItem.Request;
			if (this.m_IsPipelinePaused)
			{
				this.m_IsPipelinePaused = (this.m_WriteList.Count > Connection.s_MinPipelinedCount);
			}
			if ((!httpWebRequest.Pipelined || httpWebRequest.HasEntityBody || !this.m_CanPipeline || !this.m_Pipelining || this.m_IsPipelinePaused) && this.m_WriteList.Count != 0)
			{
				httpWebRequest = null;
			}
			if (httpWebRequest != null)
			{
				NetworkingPerfCounters.Instance.IncrementAverage(NetworkingPerfCounterName.HttpWebRequestAvgQueueTime, waitListItem.QueueStartTime);
				this.m_WaitList.RemoveAt(0);
				this.CheckIdle();
			}
			return httpWebRequest;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00054320 File Offset: 0x00052520
		private void CompleteStartConnection(bool async, HttpWebRequest httpWebRequest)
		{
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.ConnectFailure;
			this.m_InnerException = null;
			bool flag = true;
			try
			{
				if ((httpWebRequest.IsWebSocketRequest || httpWebRequest.Address.Scheme == Uri.UriSchemeHttps) && this.ServicePoint.InternalProxyServicePoint)
				{
					if (!this.TunnelThroughProxy(this.ServicePoint.InternalAddress, httpWebRequest, async))
					{
						webExceptionStatus = WebExceptionStatus.ConnectFailure;
						flag = false;
					}
					if (async && flag)
					{
						return;
					}
				}
				else if (!base.Activate(httpWebRequest, async, new GeneralAsyncDelegate(this.CompleteConnectionWrapper)))
				{
					return;
				}
			}
			catch (Exception ex)
			{
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				if (ex is WebException)
				{
					webExceptionStatus = ((WebException)ex).Status;
				}
				flag = false;
			}
			if (!flag)
			{
				ConnectionReturnResult responses = null;
				this.HandleError(false, false, webExceptionStatus, ref responses);
				ConnectionReturnResult.SetResponses(responses);
				return;
			}
			this.CompleteConnection(async, httpWebRequest);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x000543F8 File Offset: 0x000525F8
		private void CompleteConnectionWrapper(object request, object state)
		{
			Exception ex = state as Exception;
			if (ex != null)
			{
				ConnectionReturnResult responses = null;
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				this.HandleError(false, false, WebExceptionStatus.ConnectFailure, ref responses);
				ConnectionReturnResult.SetResponses(responses);
			}
			this.CompleteConnection(true, (HttpWebRequest)request);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00054440 File Offset: 0x00052640
		private void CompleteConnection(bool async, HttpWebRequest request)
		{
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.ConnectFailure;
			if (request.Async)
			{
				request.OpenWriteSideResponseWindow();
			}
			try
			{
				try
				{
					if (request.Address.Scheme == Uri.UriSchemeHttps)
					{
						TlsStream networkStream = new TlsStream(request.GetRemoteResourceUri().IdnHost, base.NetworkStream, request.CheckCertificateRevocationList, request.SslProtocols, request.ClientCertificates, this.ServicePoint, request, request.Async ? request.GetConnectingContext().ContextCopy : null);
						base.NetworkStream = networkStream;
					}
					webExceptionStatus = WebExceptionStatus.Success;
				}
				catch
				{
					base.NetworkStream.Close();
					throw;
				}
				finally
				{
					this.m_ReadState = ReadState.Start;
					this.ClearReaderState();
					request.SetRequestSubmitDone(new ConnectStream(this, request));
				}
			}
			catch (Exception ex)
			{
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					webExceptionStatus = ex2.Status;
				}
			}
			if (webExceptionStatus != WebExceptionStatus.Success)
			{
				ConnectionReturnResult responses = null;
				this.HandleError(false, false, webExceptionStatus, ref responses);
				ConnectionReturnResult.SetResponses(responses);
				if (Logging.On)
				{
					Logging.PrintError(Logging.Web, this, "CompleteConnection", "on error");
				}
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00054574 File Offset: 0x00052774
		private void InternalWriteStartNextRequest(HttpWebRequest request, ref bool calledCloseConnection, ref TriState startRequestResult, ref HttpWebRequest nextRequest, ref ConnectionReturnResult returnResult)
		{
			lock (this)
			{
				this.m_WriteDone = true;
				if (!this.m_KeepAlive || this.m_Error != WebExceptionStatus.Success || !base.CanBePooled)
				{
					if (this.m_ReadDone)
					{
						if (this.m_Error == WebExceptionStatus.Success)
						{
							this.m_Error = WebExceptionStatus.KeepAliveFailure;
						}
						this.PrepareCloseConnectionSocket(ref returnResult, 0);
						calledCloseConnection = true;
						this.Close();
					}
					else if (this.m_Error != WebExceptionStatus.Success)
					{
					}
				}
				else
				{
					if (this.m_Pipelining || this.m_ReadDone)
					{
						nextRequest = this.CheckNextRequest();
					}
					if (nextRequest != null)
					{
						startRequestResult = this.StartRequest(nextRequest, false);
					}
				}
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00054628 File Offset: 0x00052828
		internal void WriteStartNextRequest(HttpWebRequest request, ref ConnectionReturnResult returnResult)
		{
			TriState triState = TriState.Unspecified;
			HttpWebRequest request2 = null;
			bool flag = false;
			this.InternalWriteStartNextRequest(request, ref flag, ref triState, ref request2, ref returnResult);
			if (!flag && triState != TriState.Unspecified)
			{
				this.CompleteStartRequest(false, request2, triState);
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00054659 File Offset: 0x00052859
		internal void SetLeftoverBytes(byte[] buffer, int bufferOffset, int bufferCount)
		{
			if (bufferOffset > 0)
			{
				Buffer.BlockCopy(buffer, bufferOffset, buffer, 0, bufferCount);
			}
			if (this.m_ReadBuffer != buffer)
			{
				this.FreeReadBuffer();
				this.m_ReadBuffer = buffer;
			}
			this.m_BytesScanned = 0;
			this.m_BytesRead = bufferCount;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00054690 File Offset: 0x00052890
		internal void ReadStartNextRequest(WebRequest currentRequest, ref ConnectionReturnResult returnResult)
		{
			HttpWebRequest httpWebRequest = null;
			TriState triState = TriState.Unspecified;
			bool flag = false;
			bool flag2 = false;
			int num = Interlocked.Decrement(ref this.m_ReservedCount);
			try
			{
				lock (this)
				{
					if (this.m_WriteList.Count > 0 && currentRequest == this.m_WriteList[0])
					{
						this.m_ReadState = ReadState.Start;
						this.m_WriteList.RemoveAt(0);
						this.m_ResponseData.m_ConnectStream = null;
					}
					else
					{
						flag2 = true;
					}
					if (!flag2)
					{
						if (this.m_ReadDone)
						{
							throw new InternalException();
						}
						if (!this.m_KeepAlive || this.m_Error != WebExceptionStatus.Success || !base.CanBePooled)
						{
							this.m_ReadDone = true;
							if (this.m_WriteDone)
							{
								if (this.m_Error == WebExceptionStatus.Success)
								{
									this.m_Error = WebExceptionStatus.KeepAliveFailure;
								}
								this.PrepareCloseConnectionSocket(ref returnResult, 0);
								HttpWebRequest httpWebRequest2 = currentRequest as HttpWebRequest;
								if (httpWebRequest2 != null && httpWebRequest2.TunnelConnection != null)
								{
									httpWebRequest2.TunnelConnection.RemoveFromConnectionList();
								}
								flag = true;
								this.Close();
							}
						}
						else
						{
							this.m_AtLeastOneResponseReceived = true;
							if (this.m_WriteList.Count != 0)
							{
								httpWebRequest = (this.m_WriteList[0] as HttpWebRequest);
								if (!httpWebRequest.HeadersCompleted)
								{
									httpWebRequest = null;
									this.m_ReadDone = true;
								}
							}
							else
							{
								this.m_ReadDone = true;
								if (this.m_WriteDone)
								{
									httpWebRequest = this.CheckNextRequest();
									if (httpWebRequest != null)
									{
										if (httpWebRequest.HeadersCompleted)
										{
											throw new InternalException();
										}
										triState = this.StartRequest(httpWebRequest, false);
									}
									else
									{
										this.m_Free = true;
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this.CheckIdle();
				if (returnResult != null)
				{
					ConnectionReturnResult.SetResponses(returnResult);
				}
			}
			if (!flag2 && !flag)
			{
				if (triState != TriState.Unspecified)
				{
					this.CompleteStartRequest(false, httpWebRequest, triState);
					return;
				}
				if (httpWebRequest != null)
				{
					if (!httpWebRequest.Async)
					{
						httpWebRequest.ConnectionReaderAsyncResult.InvokeCallback();
						return;
					}
					if (this.m_BytesScanned < this.m_BytesRead)
					{
						this.ReadComplete(0, WebExceptionStatus.Success);
						return;
					}
					if (Thread.CurrentThread.IsThreadPoolThread)
					{
						this.PostReceive();
						return;
					}
					ThreadPool.UnsafeQueueUserWorkItem(Connection.m_PostReceiveDelegate, this);
				}
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000548B0 File Offset: 0x00052AB0
		internal void MarkAsReserved()
		{
			int num = Interlocked.Increment(ref this.m_ReservedCount);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000548CC File Offset: 0x00052ACC
		internal void CheckStartReceive(HttpWebRequest request)
		{
			lock (this)
			{
				request.HeadersCompleted = true;
				if (this.m_WriteList.Count == 0)
				{
					return;
				}
				if (!this.m_ReadDone || this.m_WriteList[0] != request)
				{
					return;
				}
				this.m_ReadDone = false;
				this.m_CurrentRequest = (HttpWebRequest)this.m_WriteList[0];
			}
			if (!request.Async)
			{
				request.ConnectionReaderAsyncResult.InvokeCallback();
				return;
			}
			if (this.m_BytesScanned < this.m_BytesRead)
			{
				this.ReadComplete(0, WebExceptionStatus.Success);
				return;
			}
			if (Thread.CurrentThread.IsThreadPoolThread)
			{
				this.PostReceive();
				return;
			}
			ThreadPool.UnsafeQueueUserWorkItem(Connection.m_PostReceiveDelegate, this);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0005499C File Offset: 0x00052B9C
		private void InitializeParseStatusLine()
		{
			this.m_StatusState = 0;
			this.m_StatusLineValues.MajorVersion = 0;
			this.m_StatusLineValues.MinorVersion = 0;
			this.m_StatusLineValues.StatusCode = 0;
			this.m_StatusLineValues.StatusDescription = null;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000549D8 File Offset: 0x00052BD8
		private DataParseStatus ParseStatusLine(byte[] statusLine, int statusLineLength, ref int bytesParsed, ref int[] statusLineInts, ref string statusDescription, ref int statusState, ref WebParseError parseError)
		{
			DataParseStatus dataParseStatus = DataParseStatus.Done;
			int num = -1;
			int num2 = 0;
			while (bytesParsed < statusLineLength && statusLine[bytesParsed] != 13 && statusLine[bytesParsed] != 10)
			{
				switch (statusState)
				{
				case 0:
					if (statusLine[bytesParsed] == 47)
					{
						statusState++;
					}
					else if (statusLine[bytesParsed] == 32)
					{
						statusState = 3;
					}
					break;
				case 1:
					if (statusLine[bytesParsed] != 46)
					{
						goto IL_6D;
					}
					statusState++;
					break;
				case 2:
					goto IL_6D;
				case 3:
					goto IL_7F;
				case 4:
					if (statusLine[bytesParsed] != 32)
					{
						num2 = bytesParsed;
						if (num == -1)
						{
							num = bytesParsed;
						}
					}
					break;
				}
				IL_E2:
				bytesParsed++;
				if (this.m_MaximumResponseHeadersLength < 0)
				{
					continue;
				}
				int num3 = this.m_TotalResponseHeadersLength + 1;
				this.m_TotalResponseHeadersLength = num3;
				if (num3 >= this.m_MaximumResponseHeadersLength)
				{
					dataParseStatus = DataParseStatus.DataTooBig;
					IL_1D9:
					if (dataParseStatus == DataParseStatus.Done && statusState != 4 && (statusState != 3 || statusLineInts[3] <= 0))
					{
						dataParseStatus = DataParseStatus.Invalid;
					}
					if (dataParseStatus == DataParseStatus.Invalid)
					{
						parseError.Section = WebParseErrorSection.ResponseStatusLine;
						parseError.Code = WebParseErrorCode.Generic;
					}
					return dataParseStatus;
				}
				continue;
				IL_6D:
				if (statusLine[bytesParsed] == 32)
				{
					statusState++;
					goto IL_E2;
				}
				IL_7F:
				if (char.IsDigit((char)statusLine[bytesParsed]))
				{
					int num4 = (int)(statusLine[bytesParsed] - 48);
					statusLineInts[statusState] = statusLineInts[statusState] * 10 + num4;
					goto IL_E2;
				}
				if (statusLineInts[3] > 0)
				{
					statusState++;
					goto IL_E2;
				}
				if (!char.IsWhiteSpace((char)statusLine[bytesParsed]))
				{
					statusLineInts[statusState] = -1;
					goto IL_E2;
				}
				goto IL_E2;
			}
			int num5 = bytesParsed;
			if (num != -1)
			{
				statusDescription += WebHeaderCollection.HeaderEncoding.GetString(statusLine, num, num2 - num + 1);
			}
			if (bytesParsed == statusLineLength)
			{
				return DataParseStatus.NeedMoreData;
			}
			while (bytesParsed < statusLineLength && (statusLine[bytesParsed] == 13 || statusLine[bytesParsed] == 32))
			{
				bytesParsed++;
				if (this.m_MaximumResponseHeadersLength >= 0)
				{
					int num3 = this.m_TotalResponseHeadersLength + 1;
					this.m_TotalResponseHeadersLength = num3;
					if (num3 >= this.m_MaximumResponseHeadersLength)
					{
						dataParseStatus = DataParseStatus.DataTooBig;
						goto IL_1D9;
					}
				}
			}
			if (bytesParsed == statusLineLength)
			{
				dataParseStatus = DataParseStatus.NeedMoreData;
				goto IL_1D9;
			}
			if (statusLine[bytesParsed] == 10)
			{
				bytesParsed++;
				if (this.m_MaximumResponseHeadersLength >= 0)
				{
					int num3 = this.m_TotalResponseHeadersLength + 1;
					this.m_TotalResponseHeadersLength = num3;
					if (num3 >= this.m_MaximumResponseHeadersLength)
					{
						dataParseStatus = DataParseStatus.DataTooBig;
						goto IL_1D9;
					}
				}
				dataParseStatus = DataParseStatus.Done;
				goto IL_1D9;
			}
			goto IL_1D9;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00054BF0 File Offset: 0x00052DF0
		private unsafe static DataParseStatus ParseStatusLineStrict(byte[] statusLine, int statusLineLength, ref int bytesParsed, ref int statusState, Connection.StatusLineValues statusLineValues, int maximumHeaderLength, ref int totalBytesParsed, ref WebParseError parseError)
		{
			int num = bytesParsed;
			DataParseStatus dataParseStatus = DataParseStatus.DataTooBig;
			int num2 = (maximumHeaderLength <= 0) ? int.MaxValue : (maximumHeaderLength - totalBytesParsed + bytesParsed);
			if (statusLineLength < num2)
			{
				dataParseStatus = DataParseStatus.NeedMoreData;
				num2 = statusLineLength;
			}
			if (bytesParsed < num2)
			{
				try
				{
					fixed (byte[] array = statusLine)
					{
						byte* ptr;
						if (statusLine == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						int num3;
						switch (statusState)
						{
						case 0:
							while (totalBytesParsed - num + bytesParsed < "HTTP/".Length)
							{
								if ((byte)"HTTP/"[totalBytesParsed - num + bytesParsed] != ptr[bytesParsed])
								{
									dataParseStatus = DataParseStatus.Invalid;
									goto IL_447;
								}
								num3 = bytesParsed + 1;
								bytesParsed = num3;
								if (num3 == num2)
								{
									goto IL_447;
								}
							}
							if (ptr[bytesParsed] == 46)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusState = 1;
							break;
						case 1:
							break;
						case 2:
							goto IL_18B;
						case 3:
							goto IL_1F6;
						case 4:
							goto IL_2AB;
						case 5:
							goto IL_42C;
						default:
							goto IL_447;
						}
						while (ptr[bytesParsed] != 46)
						{
							if (ptr[bytesParsed] < 48 || ptr[bytesParsed] > 57)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusLineValues.MajorVersion = statusLineValues.MajorVersion * 10 + (int)ptr[bytesParsed] - 48;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
						}
						if (bytesParsed + 1 == num2)
						{
							goto IL_447;
						}
						bytesParsed++;
						if (ptr[bytesParsed] == 32)
						{
							dataParseStatus = DataParseStatus.Invalid;
							goto IL_447;
						}
						statusState = 2;
						IL_18B:
						while (ptr[bytesParsed] != 32)
						{
							if (ptr[bytesParsed] < 48 || ptr[bytesParsed] > 57)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusLineValues.MinorVersion = statusLineValues.MinorVersion * 10 + (int)ptr[bytesParsed] - 48;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
						}
						statusState = 3;
						statusLineValues.StatusCode = 1;
						num3 = bytesParsed + 1;
						bytesParsed = num3;
						if (num3 == num2)
						{
							goto IL_447;
						}
						IL_1F6:
						while (ptr[bytesParsed] >= 48 && ptr[bytesParsed] <= 57)
						{
							if (statusLineValues.StatusCode >= 1000)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusLineValues.StatusCode = statusLineValues.StatusCode * 10 + (int)ptr[bytesParsed] - 48;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
						}
						if (ptr[bytesParsed] != 32 || statusLineValues.StatusCode < 1000)
						{
							if (ptr[bytesParsed] != 13 || statusLineValues.StatusCode < 1000)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							statusLineValues.StatusDescription = (statusLineValues.StatusDescription ?? string.Empty);
							statusLineValues.StatusCode -= 1000;
							statusState = 5;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
							goto IL_42C;
						}
						else
						{
							statusLineValues.StatusCode -= 1000;
							statusState = 4;
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								goto IL_447;
							}
						}
						IL_2AB:
						if (statusLineValues.StatusDescription == null)
						{
							string[] array2 = Connection.s_ShortcutStatusDescriptions;
							int i = 0;
							while (i < array2.Length)
							{
								string text = array2[i];
								if (bytesParsed < num2 - text.Length && ptr[bytesParsed] == (byte)text[0])
								{
									byte* ptr2 = ptr + bytesParsed + 1;
									int num4 = 1;
									while (num4 < text.Length && *(ptr2++) == (byte)text[num4])
									{
										num4++;
									}
									if (num4 == text.Length)
									{
										statusLineValues.StatusDescription = text;
										bytesParsed += text.Length;
										break;
									}
									break;
								}
								else
								{
									i++;
								}
							}
						}
						int num5 = bytesParsed;
						while (ptr[bytesParsed] != 13)
						{
							if (ptr[bytesParsed] < 32 || ptr[bytesParsed] == 127)
							{
								dataParseStatus = DataParseStatus.Invalid;
								goto IL_447;
							}
							num3 = bytesParsed + 1;
							bytesParsed = num3;
							if (num3 == num2)
							{
								string @string = WebHeaderCollection.HeaderEncoding.GetString(ptr + num5, bytesParsed - num5);
								if (statusLineValues.StatusDescription == null)
								{
									statusLineValues.StatusDescription = @string;
									goto IL_447;
								}
								statusLineValues.StatusDescription += @string;
								goto IL_447;
							}
						}
						if (bytesParsed > num5)
						{
							string string2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num5, bytesParsed - num5);
							if (statusLineValues.StatusDescription == null)
							{
								statusLineValues.StatusDescription = string2;
							}
							else
							{
								statusLineValues.StatusDescription += string2;
							}
						}
						else if (statusLineValues.StatusDescription == null)
						{
							statusLineValues.StatusDescription = "";
						}
						statusState = 5;
						num3 = bytesParsed + 1;
						bytesParsed = num3;
						if (num3 == num2)
						{
							goto IL_447;
						}
						IL_42C:
						if (ptr[bytesParsed] != 10)
						{
							dataParseStatus = DataParseStatus.Invalid;
						}
						else
						{
							dataParseStatus = DataParseStatus.Done;
							bytesParsed++;
						}
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			IL_447:
			totalBytesParsed += bytesParsed - num;
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseStatusLine;
				parseError.Code = WebParseErrorCode.Generic;
			}
			return dataParseStatus;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00055080 File Offset: 0x00053280
		private void SetStatusLineParsed()
		{
			this.m_ResponseData.m_StatusCode = (HttpStatusCode)this.m_StatusLineValues.StatusCode;
			this.m_ResponseData.m_StatusDescription = this.m_StatusLineValues.StatusDescription;
			this.m_ResponseData.m_IsVersionHttp11 = (this.m_StatusLineValues.MajorVersion >= 1 && this.m_StatusLineValues.MinorVersion >= 1);
			if (this.ServicePoint.HttpBehaviour == HttpBehaviour.Unknown || (this.ServicePoint.HttpBehaviour == HttpBehaviour.HTTP11 && !this.m_ResponseData.m_IsVersionHttp11))
			{
				this.ServicePoint.HttpBehaviour = (this.m_ResponseData.m_IsVersionHttp11 ? HttpBehaviour.HTTP11 : HttpBehaviour.HTTP10);
			}
			if (ServicePointManager.UseHttpPipeliningAndBufferPooling)
			{
				this.m_CanPipeline = this.ServicePoint.SupportsPipelining;
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00055144 File Offset: 0x00053344
		private long ProcessHeaderData(ref bool fHaveChunked, HttpWebRequest request, out bool dummyResponseStream)
		{
			long num = -1L;
			fHaveChunked = false;
			string text = this.m_ResponseData.m_ResponseHeaders["Transfer-Encoding"];
			if (text != null)
			{
				text = text.ToLower(CultureInfo.InvariantCulture);
				fHaveChunked = (text.IndexOf("chunked") != -1);
			}
			if (!fHaveChunked)
			{
				string text2 = this.m_ResponseData.m_ResponseHeaders.ContentLength;
				if (text2 != null)
				{
					int num2 = text2.IndexOf(':');
					if (num2 != -1)
					{
						text2 = text2.Substring(num2 + 1);
					}
					if (!long.TryParse(text2, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat, out num))
					{
						num = -1L;
						num2 = text2.LastIndexOf(',');
						if (num2 != -1)
						{
							text2 = text2.Substring(num2 + 1);
							if (!long.TryParse(text2, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat, out num))
							{
								num = -1L;
							}
						}
					}
					if (num < 0L)
					{
						num = -2L;
					}
				}
			}
			dummyResponseStream = (!request.CanGetResponseStream || this.m_ResponseData.m_StatusCode < HttpStatusCode.OK || this.m_ResponseData.m_StatusCode == HttpStatusCode.NoContent || (this.m_ResponseData.m_StatusCode == HttpStatusCode.NotModified && num < 0L));
			if (this.m_KeepAlive)
			{
				bool flag = false;
				if (!dummyResponseStream && num < 0L && !fHaveChunked)
				{
					flag = true;
				}
				else if (this.m_ResponseData.m_StatusCode == HttpStatusCode.Forbidden && base.NetworkStream is TlsStream)
				{
					flag = true;
				}
				else if (this.m_ResponseData.m_StatusCode > (HttpStatusCode)299 && (request.CurrentMethod == KnownHttpVerb.Post || request.CurrentMethod == KnownHttpVerb.Put) && this.m_MaximumUnauthorizedUploadLength >= 0L && request.ContentLength > this.m_MaximumUnauthorizedUploadLength && (request.CurrentAuthenticationState == null || request.CurrentAuthenticationState.Module == null))
				{
					flag = true;
				}
				else
				{
					bool flag2 = false;
					bool flag3 = false;
					string text3 = this.m_ResponseData.m_ResponseHeaders["Connection"];
					if (text3 == null && (this.ServicePoint.InternalProxyServicePoint || request.IsTunnelRequest))
					{
						text3 = this.m_ResponseData.m_ResponseHeaders["Proxy-Connection"];
					}
					if (text3 != null)
					{
						text3 = text3.ToLower(CultureInfo.InvariantCulture);
						if (text3.IndexOf("keep-alive") != -1)
						{
							flag3 = true;
						}
						else if (text3.IndexOf("close") != -1)
						{
							flag2 = true;
						}
					}
					if ((flag2 && this.ServicePoint.HttpBehaviour == HttpBehaviour.HTTP11) || (!flag3 && this.ServicePoint.HttpBehaviour <= HttpBehaviour.HTTP10))
					{
						flag = true;
					}
				}
				if (flag)
				{
					lock (this)
					{
						this.m_KeepAlive = false;
						this.m_Free = false;
					}
				}
			}
			return num;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x000553FC File Offset: 0x000535FC
		internal bool KeepAlive
		{
			get
			{
				return this.m_KeepAlive;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00055404 File Offset: 0x00053604
		internal bool NonKeepAliveRequestPipelined
		{
			get
			{
				return this.m_NonKeepAliveRequestPipelined;
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0005540C File Offset: 0x0005360C
		private DataParseStatus ParseStreamData(ref ConnectionReturnResult returnResult)
		{
			if (this.m_CurrentRequest == null)
			{
				this.m_ParseError.Section = WebParseErrorSection.Generic;
				this.m_ParseError.Code = WebParseErrorCode.UnexpectedServerResponse;
				return DataParseStatus.Invalid;
			}
			bool flag = false;
			bool flag2;
			long num = this.ProcessHeaderData(ref flag, this.m_CurrentRequest, out flag2);
			if (num == -2L)
			{
				this.m_ParseError.Section = WebParseErrorSection.ResponseHeader;
				this.m_ParseError.Code = WebParseErrorCode.InvalidContentLength;
				return DataParseStatus.Invalid;
			}
			int num2 = this.m_BytesRead - this.m_BytesScanned;
			if (this.m_ResponseData.m_StatusCode > (HttpStatusCode)299)
			{
				this.m_CurrentRequest.ErrorStatusCodeNotify(this, this.m_KeepAlive, false);
			}
			int num3;
			if (flag2)
			{
				num3 = 0;
				flag = false;
			}
			else
			{
				num3 = -1;
				if (!flag && num <= 2147483647L)
				{
					num3 = (int)num;
				}
			}
			DataParseStatus result;
			if (this.m_CurrentRequest.IsWebSocketRequest && this.m_ResponseData.m_StatusCode == HttpStatusCode.SwitchingProtocols)
			{
				this.m_ResponseData.m_ConnectStream = new ConnectStream(this, this.m_ReadBuffer, this.m_BytesScanned, num2, (long)num2, flag, this.m_CurrentRequest);
				result = DataParseStatus.Done;
				this.ClearReaderState();
			}
			else if (num3 != -1 && num3 <= num2)
			{
				this.m_ResponseData.m_ConnectStream = new ConnectStream(this, this.m_ReadBuffer, this.m_BytesScanned, num3, flag2 ? 0L : num, flag, this.m_CurrentRequest);
				result = DataParseStatus.ContinueParsing;
				this.m_BytesScanned += num3;
			}
			else
			{
				this.m_ResponseData.m_ConnectStream = new ConnectStream(this, this.m_ReadBuffer, this.m_BytesScanned, num2, flag2 ? 0L : num, flag, this.m_CurrentRequest);
				result = DataParseStatus.Done;
				this.ClearReaderState();
			}
			this.m_ResponseData.m_ContentLength = num;
			ConnectionReturnResult.Add(ref returnResult, this.m_CurrentRequest, this.m_ResponseData.Clone());
			return result;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000555B6 File Offset: 0x000537B6
		private void ClearReaderState()
		{
			this.m_BytesRead = 0;
			this.m_BytesScanned = 0;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000555C8 File Offset: 0x000537C8
		private DataParseStatus ParseResponseData(ref ConnectionReturnResult returnResult, out bool requestDone, out CoreResponseData continueResponseData)
		{
			DataParseStatus result = DataParseStatus.NeedMoreData;
			requestDone = false;
			continueResponseData = null;
			switch (this.m_ReadState)
			{
			case ReadState.Start:
				break;
			case ReadState.StatusLine:
				goto IL_F4;
			case ReadState.Headers:
				goto IL_28D;
			case ReadState.Data:
				goto IL_546;
			default:
				goto IL_551;
			}
			IL_2A:
			if (this.m_CurrentRequest == null)
			{
				lock (this)
				{
					if (this.m_WriteList.Count == 0 || (this.m_CurrentRequest = (this.m_WriteList[0] as HttpWebRequest)) == null)
					{
						this.m_ParseError.Section = WebParseErrorSection.Generic;
						this.m_ParseError.Code = WebParseErrorCode.Generic;
						result = DataParseStatus.Invalid;
						goto IL_551;
					}
				}
			}
			this.m_KeepAlive &= (this.m_CurrentRequest.KeepAlive || this.m_CurrentRequest.NtlmKeepAlive);
			this.m_MaximumResponseHeadersLength = this.m_CurrentRequest.MaximumResponseHeadersLength * 1024;
			this.m_ResponseData = new CoreResponseData();
			this.m_ReadState = ReadState.StatusLine;
			this.m_TotalResponseHeadersLength = 0;
			this.InitializeParseStatusLine();
			IL_F4:
			DataParseStatus dataParseStatus;
			if (SettingsSectionInternal.Section.UseUnsafeHeaderParsing)
			{
				int[] array = new int[]
				{
					0,
					this.m_StatusLineValues.MajorVersion,
					this.m_StatusLineValues.MinorVersion,
					this.m_StatusLineValues.StatusCode
				};
				if (this.m_StatusLineValues.StatusDescription == null)
				{
					this.m_StatusLineValues.StatusDescription = "";
				}
				dataParseStatus = this.ParseStatusLine(this.m_ReadBuffer, this.m_BytesRead, ref this.m_BytesScanned, ref array, ref this.m_StatusLineValues.StatusDescription, ref this.m_StatusState, ref this.m_ParseError);
				this.m_StatusLineValues.MajorVersion = array[1];
				this.m_StatusLineValues.MinorVersion = array[2];
				this.m_StatusLineValues.StatusCode = array[3];
			}
			else
			{
				dataParseStatus = Connection.ParseStatusLineStrict(this.m_ReadBuffer, this.m_BytesRead, ref this.m_BytesScanned, ref this.m_StatusState, this.m_StatusLineValues, this.m_MaximumResponseHeadersLength, ref this.m_TotalResponseHeadersLength, ref this.m_ParseError);
			}
			if (dataParseStatus == DataParseStatus.Done)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_received_status_line", new object[]
					{
						this.m_StatusLineValues.MajorVersion.ToString() + "." + this.m_StatusLineValues.MinorVersion.ToString(),
						this.m_StatusLineValues.StatusCode,
						this.m_StatusLineValues.StatusDescription
					}));
				}
				this.SetStatusLineParsed();
				this.m_ReadState = ReadState.Headers;
				this.m_ResponseData.m_ResponseHeaders = new WebHeaderCollection(WebHeaderCollectionType.HttpWebResponse);
			}
			else
			{
				if (dataParseStatus != DataParseStatus.NeedMoreData)
				{
					result = dataParseStatus;
					goto IL_551;
				}
				goto IL_551;
			}
			IL_28D:
			if (this.m_BytesScanned >= this.m_BytesRead)
			{
				goto IL_551;
			}
			if (SettingsSectionInternal.Section.UseUnsafeHeaderParsing)
			{
				dataParseStatus = this.m_ResponseData.m_ResponseHeaders.ParseHeaders(this.m_ReadBuffer, this.m_BytesRead, ref this.m_BytesScanned, ref this.m_TotalResponseHeadersLength, this.m_MaximumResponseHeadersLength, ref this.m_ParseError);
			}
			else
			{
				dataParseStatus = this.m_ResponseData.m_ResponseHeaders.ParseHeadersStrict(this.m_ReadBuffer, this.m_BytesRead, ref this.m_BytesScanned, ref this.m_TotalResponseHeadersLength, this.m_MaximumResponseHeadersLength, ref this.m_ParseError);
			}
			if (dataParseStatus == DataParseStatus.Invalid || dataParseStatus == DataParseStatus.DataTooBig)
			{
				result = dataParseStatus;
				goto IL_551;
			}
			if (dataParseStatus != DataParseStatus.Done)
			{
				goto IL_551;
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_received_headers", new object[]
				{
					this.m_ResponseData.m_ResponseHeaders.ToString(true)
				}));
			}
			if (this.m_IISVersion == -1)
			{
				string server = this.m_ResponseData.m_ResponseHeaders.Server;
				if (server != null && server.ToLower(CultureInfo.InvariantCulture).Contains("microsoft-iis"))
				{
					int num = server.IndexOf("/");
					if (num++ > 0 && num < server.Length)
					{
						this.m_IISVersion = (int)(server[num++] - '0');
						while (num < server.Length && char.IsDigit(server[num]))
						{
							this.m_IISVersion = this.m_IISVersion * 10 + (int)server[num++] - 48;
						}
					}
				}
				if (this.m_IISVersion == -1 && this.m_ResponseData.m_StatusCode != HttpStatusCode.Continue)
				{
					this.m_IISVersion = 0;
				}
			}
			bool flag2 = ServicePointManager.UseStrictRfcInterimResponseHandling && this.m_ResponseData.m_StatusCode > HttpStatusCode.SwitchingProtocols && this.m_ResponseData.m_StatusCode < HttpStatusCode.OK;
			if (this.m_ResponseData.m_StatusCode == HttpStatusCode.Continue || this.m_ResponseData.m_StatusCode == HttpStatusCode.BadRequest || flag2)
			{
				if (this.m_ResponseData.m_StatusCode == HttpStatusCode.BadRequest)
				{
					if (this.ServicePoint.HttpBehaviour == HttpBehaviour.HTTP11 && this.m_CurrentRequest.HttpWriteMode == HttpWriteMode.Chunked && this.m_ResponseData.m_ResponseHeaders.Via != null && string.Compare(this.m_ResponseData.m_StatusDescription, "Bad Request ( The HTTP request includes a non-supported header. Contact the Server administrator.  )", StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.ServicePoint.HttpBehaviour = HttpBehaviour.HTTP11PartiallyCompliant;
					}
				}
				else
				{
					if (this.m_ResponseData.m_StatusCode == HttpStatusCode.Continue)
					{
						this.m_CurrentRequest.Saw100Continue = true;
						if (!this.ServicePoint.Understands100Continue)
						{
							this.ServicePoint.Understands100Continue = true;
						}
						continueResponseData = this.m_ResponseData;
						goto IL_2A;
					}
					goto IL_2A;
				}
			}
			this.m_ReadState = ReadState.Data;
			IL_546:
			requestDone = true;
			result = this.ParseStreamData(ref returnResult);
			IL_551:
			if (this.m_BytesScanned == this.m_BytesRead)
			{
				this.ClearReaderState();
			}
			return result;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00055B4C File Offset: 0x00053D4C
		internal void CloseOnIdle()
		{
			lock (this)
			{
				this.m_KeepAlive = false;
				this.m_RemovedFromConnectionList = true;
				if (!this.m_Idle)
				{
					this.CheckIdle();
				}
				if (this.m_Idle)
				{
					this.AbortSocket(false);
					GC.SuppressFinalize(this);
				}
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00055BB4 File Offset: 0x00053DB4
		internal bool AbortOrDisassociate(HttpWebRequest request, WebException webException)
		{
			ConnectionReturnResult responses = null;
			lock (this)
			{
				int num = this.m_WriteList.IndexOf(request);
				if (num == -1)
				{
					Connection.WaitListItem waitListItem = null;
					if (this.m_WaitList.Count > 0)
					{
						waitListItem = this.m_WaitList.Find((Connection.WaitListItem o) => o.Request == request);
					}
					if (waitListItem != null)
					{
						NetworkingPerfCounters.Instance.IncrementAverage(NetworkingPerfCounterName.HttpWebRequestAvgQueueTime, waitListItem.QueueStartTime);
						this.m_WaitList.Remove(waitListItem);
						this.UnlockIfNeeded(waitListItem.Request);
					}
					return true;
				}
				this.m_KeepAlive = false;
				if (webException != null && this.m_InnerException == null)
				{
					this.m_InnerException = webException;
					this.m_Error = webException.Status;
				}
				else
				{
					this.m_Error = WebExceptionStatus.RequestCanceled;
				}
				this.PrepareCloseConnectionSocket(ref responses, num);
				base.Close(0);
			}
			ConnectionReturnResult.SetResponses(responses);
			return false;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00055CBC File Offset: 0x00053EBC
		internal void AbortSocket(bool isAbortState)
		{
			this.m_AbortSocketCalledUtc = DateTime.UtcNow;
			if (isAbortState)
			{
				this.UnlockRequest();
				this.CheckIdle();
			}
			else
			{
				this.m_Error = WebExceptionStatus.KeepAliveFailure;
			}
			lock (this)
			{
				base.Close(0);
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00055D1C File Offset: 0x00053F1C
		private void PrepareCloseConnectionSocket(ref ConnectionReturnResult returnResult, int abortedPipelinedRequestIndex = 0)
		{
			this.m_PrepareCloseConnectionSocketCalledUtc = DateTime.UtcNow;
			this.m_IdleSinceUtc = DateTime.MinValue;
			base.CanBePooled = false;
			if (this.m_WriteList.Count != 0 || this.m_WaitList.Count != 0)
			{
				HttpWebRequest lockedRequest = this.LockedRequest;
				if (lockedRequest != null)
				{
					bool flag = false;
					foreach (object obj in this.m_WriteList)
					{
						HttpWebRequest httpWebRequest = (HttpWebRequest)obj;
						if (httpWebRequest == lockedRequest)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						foreach (Connection.WaitListItem waitListItem in this.m_WaitList)
						{
							if (waitListItem.Request == lockedRequest)
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						this.UnlockRequest();
					}
				}
				if (this.m_WaitList.Count != 0)
				{
					HttpWebRequest[] array = new HttpWebRequest[this.m_WaitList.Count];
					for (int i = 0; i < this.m_WaitList.Count; i++)
					{
						array[i] = this.m_WaitList[i].Request;
					}
					ConnectionReturnResult.AddExceptionRange(ref returnResult, array, ExceptionHelper.IsolatedException);
				}
				if (this.m_WriteList.Count != 0)
				{
					Exception ex = this.m_InnerException;
					if (!(ex is WebException) && !(ex is SecurityException))
					{
						if (this.m_Error == WebExceptionStatus.ServerProtocolViolation)
						{
							string text = NetRes.GetWebStatusString(this.m_Error);
							string text2 = "";
							if (this.m_ParseError.Section != WebParseErrorSection.Generic)
							{
								text2 = text2 + " Section=" + this.m_ParseError.Section.ToString();
							}
							if (this.m_ParseError.Code != WebParseErrorCode.Generic)
							{
								text2 = text2 + " Detail=" + SR.GetString("net_WebResponseParseError_" + this.m_ParseError.Code.ToString());
							}
							if (text2.Length != 0)
							{
								text = text + "." + text2;
							}
							ex = new WebException(text, ex, this.m_Error, null, WebExceptionInternalStatus.RequestFatal);
						}
						else if (this.m_Error == WebExceptionStatus.SecureChannelFailure)
						{
							ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.SecureChannelFailure), WebExceptionStatus.SecureChannelFailure);
						}
						else if (this.m_Error == WebExceptionStatus.Timeout)
						{
							ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.Timeout), WebExceptionStatus.Timeout);
						}
						else if (this.m_Error == WebExceptionStatus.RequestCanceled)
						{
							ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled, WebExceptionInternalStatus.RequestFatal, ex);
						}
						else if (this.m_Error == WebExceptionStatus.MessageLengthLimitExceeded || this.m_Error == WebExceptionStatus.TrustFailure)
						{
							ex = new WebException(NetRes.GetWebStatusString("net_connclosed", this.m_Error), this.m_Error, WebExceptionInternalStatus.RequestFatal, ex);
						}
						else
						{
							if (this.m_Error == WebExceptionStatus.Success)
							{
								throw new InternalException();
							}
							bool flag2 = false;
							bool flag3 = false;
							if (this.m_WriteList.Count != 1)
							{
								flag2 = true;
							}
							else if (this.m_Error == WebExceptionStatus.KeepAliveFailure)
							{
								HttpWebRequest httpWebRequest2 = (HttpWebRequest)this.m_WriteList[0];
								if (!httpWebRequest2.BodyStarted)
								{
									flag3 = true;
								}
							}
							else
							{
								flag2 = (!this.AtLeastOneResponseReceived && !((HttpWebRequest)this.m_WriteList[0]).BodyStarted);
							}
							ex = new WebException(NetRes.GetWebStatusString("net_connclosed", this.m_Error), this.m_Error, flag3 ? WebExceptionInternalStatus.Isolated : (flag2 ? WebExceptionInternalStatus.Recoverable : WebExceptionInternalStatus.RequestFatal), ex);
						}
					}
					WebException exception = new WebException(NetRes.GetWebStatusString("net_connclosed", WebExceptionStatus.PipelineFailure), WebExceptionStatus.PipelineFailure, WebExceptionInternalStatus.Recoverable, ex);
					HttpWebRequest[] array = new HttpWebRequest[this.m_WriteList.Count];
					this.m_WriteList.CopyTo(array, 0);
					ConnectionReturnResult.AddExceptionRange(ref returnResult, array, abortedPipelinedRequestIndex, exception, ex);
				}
				this.m_WriteList.Clear();
				foreach (Connection.WaitListItem waitListItem2 in this.m_WaitList)
				{
					NetworkingPerfCounters.Instance.IncrementAverage(NetworkingPerfCounterName.HttpWebRequestAvgQueueTime, waitListItem2.QueueStartTime);
				}
				this.m_WaitList.Clear();
			}
			this.CheckIdle();
			if (this.m_Idle)
			{
				GC.SuppressFinalize(this);
			}
			if (!this.m_RemovedFromConnectionList && this.ConnectionGroup != null)
			{
				this.RemoveFromConnectionList();
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00056188 File Offset: 0x00054388
		internal void RemoveFromConnectionList()
		{
			this.m_RemovedFromConnectionList = true;
			this.ConnectionGroup.Disassociate(this);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x000561A0 File Offset: 0x000543A0
		internal void HandleConnectStreamException(bool writeDone, bool readDone, WebExceptionStatus webExceptionStatus, ref ConnectionReturnResult returnResult, Exception e)
		{
			if (this.m_InnerException == null)
			{
				this.m_InnerException = e;
				if (!(e is WebException) && base.NetworkStream is TlsStream)
				{
					webExceptionStatus = ((TlsStream)base.NetworkStream).ExceptionStatus;
				}
				else if (e is ObjectDisposedException)
				{
					webExceptionStatus = WebExceptionStatus.RequestCanceled;
				}
			}
			this.HandleError(writeDone, readDone, webExceptionStatus, ref returnResult);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x000561FE File Offset: 0x000543FE
		private void HandleErrorWithReadDone(WebExceptionStatus webExceptionStatus, ref ConnectionReturnResult returnResult)
		{
			this.HandleError(false, true, webExceptionStatus, ref returnResult);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0005620C File Offset: 0x0005440C
		private void HandleError(bool writeDone, bool readDone, WebExceptionStatus webExceptionStatus, ref ConnectionReturnResult returnResult)
		{
			lock (this)
			{
				if (writeDone)
				{
					this.m_WriteDone = true;
				}
				if (readDone)
				{
					this.m_ReadDone = true;
				}
				if (webExceptionStatus == WebExceptionStatus.Success)
				{
					throw new InternalException();
				}
				this.m_Error = webExceptionStatus;
				this.PrepareCloseConnectionSocket(ref returnResult, 0);
				base.Close(0);
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00056278 File Offset: 0x00054478
		private static void ReadCallbackWrapper(IAsyncResult asyncResult)
		{
			if (asyncResult.CompletedSynchronously)
			{
				return;
			}
			((Connection)asyncResult.AsyncState).ReadCallback(asyncResult);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00056294 File Offset: 0x00054494
		private void ReadCallback(IAsyncResult asyncResult)
		{
			int num = -1;
			WebExceptionStatus errorStatus = WebExceptionStatus.ReceiveFailure;
			try
			{
				num = this.EndRead(asyncResult);
				if (num == 0)
				{
					num = -1;
				}
				errorStatus = WebExceptionStatus.Success;
			}
			catch (Exception ex)
			{
				HttpWebRequest currentRequest = this.m_CurrentRequest;
				if (currentRequest != null)
				{
					currentRequest.ErrorStatusCodeNotify(this, false, true);
				}
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				if (ex.GetType() == typeof(ObjectDisposedException))
				{
					errorStatus = WebExceptionStatus.RequestCanceled;
				}
				if (base.NetworkStream is TlsStream)
				{
					errorStatus = ((TlsStream)base.NetworkStream).ExceptionStatus;
				}
				else
				{
					errorStatus = WebExceptionStatus.ReceiveFailure;
				}
			}
			this.ReadComplete(num, errorStatus);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00056330 File Offset: 0x00054530
		internal void PollAndRead(HttpWebRequest request, bool userRetrievedStream)
		{
			request.NeedsToReadForResponse = true;
			if (request.ConnectionReaderAsyncResult.InternalPeekCompleted && request.ConnectionReaderAsyncResult.Result == null && base.CanBePooled)
			{
				this.SyncRead(request, userRetrievedStream, true);
			}
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00056364 File Offset: 0x00054564
		internal void SyncRead(HttpWebRequest request, bool userRetrievedStream, bool probeRead)
		{
			if (Connection.t_SyncReadNesting > 0)
			{
				return;
			}
			bool flag = !probeRead;
			try
			{
				Connection.t_SyncReadNesting++;
				int num = probeRead ? request.RequestContinueCount : 0;
				int num2 = -1;
				WebExceptionStatus errorStatus = WebExceptionStatus.ReceiveFailure;
				if (this.m_BytesScanned < this.m_BytesRead)
				{
					flag = true;
					num2 = 0;
					errorStatus = WebExceptionStatus.Success;
				}
				bool flag2;
				do
				{
					flag2 = true;
					try
					{
						if (num2 != 0)
						{
							errorStatus = WebExceptionStatus.ReceiveFailure;
							if (!flag)
							{
								TlsStream tlsStream = (!ServicePointManager.DisableExpect100ContinueTls13Fix) ? (base.NetworkStream as TlsStream) : null;
								if (tlsStream != null && tlsStream.IsTls13)
								{
									flag = tlsStream.PollForApplicationData(request.ContinueTimeout * 1000);
								}
								else
								{
									flag = base.Poll(request.ContinueTimeout * 1000, SelectMode.SelectRead);
								}
							}
							if (flag)
							{
								this.ReadTimeout = request.Timeout;
								num2 = this.Read(this.m_ReadBuffer, this.m_BytesRead, this.m_ReadBuffer.Length - this.m_BytesRead);
								errorStatus = WebExceptionStatus.Success;
								if (num2 == 0)
								{
									num2 = -1;
								}
							}
						}
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						if (this.m_InnerException == null)
						{
							this.m_InnerException = ex;
						}
						if (ex.GetType() == typeof(ObjectDisposedException))
						{
							errorStatus = WebExceptionStatus.RequestCanceled;
						}
						else if (base.NetworkStream is TlsStream)
						{
							errorStatus = ((TlsStream)base.NetworkStream).ExceptionStatus;
						}
						else
						{
							SocketException ex2 = ex.InnerException as SocketException;
							if (ex2 != null)
							{
								if (ex2.ErrorCode == 10060)
								{
									errorStatus = WebExceptionStatus.Timeout;
								}
								else
								{
									errorStatus = WebExceptionStatus.ReceiveFailure;
								}
							}
						}
					}
					if (flag)
					{
						flag2 = this.ReadComplete(num2, errorStatus);
					}
					num2 = -1;
				}
				while (!flag2 && (userRetrievedStream || num == request.RequestContinueCount));
			}
			finally
			{
				Connection.t_SyncReadNesting--;
			}
			if (probeRead)
			{
				request.FinishContinueWait();
				if (flag)
				{
					if (!request.Saw100Continue && !userRetrievedStream)
					{
						request.NeedsToReadForResponse = false;
						return;
					}
				}
				else
				{
					request.SetRequestContinue();
				}
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00056564 File Offset: 0x00054764
		private bool ReadComplete(int bytesRead, WebExceptionStatus errorStatus)
		{
			bool result = true;
			CoreResponseData coreResponseData = null;
			ConnectionReturnResult connectionReturnResult = null;
			HttpWebRequest httpWebRequest = null;
			try
			{
				if (bytesRead < 0)
				{
					if (this.m_ReadState == ReadState.Start && this.m_AtLeastOneResponseReceived)
					{
						if (errorStatus == WebExceptionStatus.Success || errorStatus == WebExceptionStatus.ReceiveFailure)
						{
							errorStatus = WebExceptionStatus.KeepAliveFailure;
						}
					}
					else if (errorStatus == WebExceptionStatus.Success)
					{
						errorStatus = WebExceptionStatus.ConnectionClosed;
					}
					HttpWebRequest currentRequest = this.m_CurrentRequest;
					if (currentRequest != null)
					{
						currentRequest.ErrorStatusCodeNotify(this, false, true);
					}
					this.HandleErrorWithReadDone(errorStatus, ref connectionReturnResult);
				}
				else
				{
					bytesRead += this.m_BytesRead;
					if (bytesRead > this.m_ReadBuffer.Length)
					{
						throw new InternalException();
					}
					this.m_BytesRead = bytesRead;
					DataParseStatus dataParseStatus = this.ParseResponseData(ref connectionReturnResult, out result, out coreResponseData);
					httpWebRequest = this.m_CurrentRequest;
					if (dataParseStatus != DataParseStatus.NeedMoreData)
					{
						this.m_CurrentRequest = null;
					}
					if (dataParseStatus == DataParseStatus.Invalid || dataParseStatus == DataParseStatus.DataTooBig)
					{
						if (httpWebRequest != null)
						{
							httpWebRequest.ErrorStatusCodeNotify(this, false, false);
						}
						if (dataParseStatus == DataParseStatus.Invalid)
						{
							this.HandleErrorWithReadDone(WebExceptionStatus.ServerProtocolViolation, ref connectionReturnResult);
						}
						else
						{
							this.HandleErrorWithReadDone(WebExceptionStatus.MessageLengthLimitExceeded, ref connectionReturnResult);
						}
					}
					else if (dataParseStatus != DataParseStatus.Done)
					{
						if (dataParseStatus == DataParseStatus.NeedMoreData)
						{
							int num = this.m_BytesRead - this.m_BytesScanned;
							if (num != 0)
							{
								if (this.m_BytesScanned == 0 && this.m_BytesRead == this.m_ReadBuffer.Length)
								{
									byte[] array = new byte[this.m_ReadBuffer.Length * 2];
									Buffer.BlockCopy(this.m_ReadBuffer, 0, array, 0, this.m_BytesRead);
									this.FreeReadBuffer();
									this.m_ReadBuffer = array;
								}
								else
								{
									Buffer.BlockCopy(this.m_ReadBuffer, this.m_BytesScanned, this.m_ReadBuffer, 0, num);
								}
							}
							this.m_BytesRead = num;
							this.m_BytesScanned = 0;
							if (httpWebRequest != null && httpWebRequest.Async)
							{
								if (Thread.CurrentThread.IsThreadPoolThread)
								{
									this.PostReceive();
								}
								else
								{
									ThreadPool.UnsafeQueueUserWorkItem(Connection.m_PostReceiveDelegate, this);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				result = true;
				if (this.m_InnerException == null)
				{
					this.m_InnerException = ex;
				}
				HttpWebRequest currentRequest2 = this.m_CurrentRequest;
				if (currentRequest2 != null)
				{
					currentRequest2.ErrorStatusCodeNotify(this, false, true);
				}
				this.HandleErrorWithReadDone(WebExceptionStatus.ReceiveFailure, ref connectionReturnResult);
			}
			try
			{
				if (httpWebRequest != null && httpWebRequest.HttpWriteMode != HttpWriteMode.None && (coreResponseData != null || (connectionReturnResult != null && connectionReturnResult.IsNotEmpty && httpWebRequest.AllowWriteStreamBuffering)) && httpWebRequest.FinishContinueWait())
				{
					httpWebRequest.SetRequestContinue(coreResponseData);
				}
			}
			finally
			{
				ConnectionReturnResult.SetResponses(connectionReturnResult);
			}
			return result;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000567B4 File Offset: 0x000549B4
		internal void Write(ScatterGatherBuffers writeBuffer)
		{
			BufferOffsetSize[] buffers = writeBuffer.GetBuffers();
			if (buffers != null)
			{
				base.MultipleWrite(buffers);
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000567D4 File Offset: 0x000549D4
		private static void PostReceiveWrapper(object state)
		{
			Connection connection = state as Connection;
			connection.PostReceive();
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000567F0 File Offset: 0x000549F0
		private void PostReceive()
		{
			try
			{
				if (this.m_LastAsyncResult != null && !this.m_LastAsyncResult.IsCompleted)
				{
					throw new InternalException();
				}
				this.m_LastAsyncResult = this.UnsafeBeginRead(this.m_ReadBuffer, this.m_BytesRead, this.m_ReadBuffer.Length - this.m_BytesRead, Connection.m_ReadCallback, this);
				if (this.m_LastAsyncResult.CompletedSynchronously)
				{
					this.ReadCallback(this.m_LastAsyncResult);
				}
			}
			catch (Exception ex)
			{
				HttpWebRequest currentRequest = this.m_CurrentRequest;
				if (currentRequest != null)
				{
					currentRequest.ErrorStatusCodeNotify(this, false, true);
				}
				ConnectionReturnResult responses = null;
				this.HandleErrorWithReadDone(WebExceptionStatus.ReceiveFailure, ref responses);
				ConnectionReturnResult.SetResponses(responses);
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00056898 File Offset: 0x00054A98
		private static void TunnelThroughProxyWrapper(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			bool flag = false;
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.ConnectFailure;
			HttpWebRequest httpWebRequest = (HttpWebRequest)((LazyAsyncResult)result).AsyncObject;
			Connection connection = ((TunnelStateObject)result.AsyncState).Connection;
			HttpWebRequest originalRequest = ((TunnelStateObject)result.AsyncState).OriginalRequest;
			try
			{
				httpWebRequest.EndGetResponse(result);
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				ConnectStream connectStream = (ConnectStream)httpWebResponse.GetResponseStream();
				connection.NetworkStream = new NetworkStream(connectStream.Connection.NetworkStream, true);
				connectStream.Connection.NetworkStream.ConvertToNotSocketOwner();
				if (ServicePointManager.FinishProxyTunnelConnectionEarly)
				{
					connectStream.Connection.ForceFinishTunnelConnection();
				}
				else
				{
					originalRequest.TunnelConnection = connectStream.Connection;
				}
				flag = true;
			}
			catch (Exception ex)
			{
				if (connection.m_InnerException == null)
				{
					connection.m_InnerException = ex;
				}
				if (ex is WebException)
				{
					webExceptionStatus = ((WebException)ex).Status;
				}
			}
			if (!flag)
			{
				ConnectionReturnResult responses = null;
				connection.HandleError(false, false, webExceptionStatus, ref responses);
				ConnectionReturnResult.SetResponses(responses);
				return;
			}
			connection.CompleteConnection(true, originalRequest);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000569B8 File Offset: 0x00054BB8
		private bool TunnelThroughProxy(Uri proxy, HttpWebRequest originalRequest, bool async)
		{
			bool result = false;
			HttpWebRequest httpWebRequest = null;
			try
			{
				new WebPermission(NetworkAccess.Connect, proxy).Assert();
				try
				{
					httpWebRequest = new HttpWebRequest(proxy, originalRequest.Address, originalRequest);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				httpWebRequest.Credentials = ((originalRequest.InternalProxy == null) ? null : originalRequest.InternalProxy.Credentials);
				httpWebRequest.InternalProxy = null;
				httpWebRequest.PreAuthenticate = true;
				httpWebRequest.UserAgent = originalRequest.UserAgent;
				HttpWebResponse httpWebResponse;
				if (async)
				{
					TunnelStateObject tunnelStateObject = new TunnelStateObject(originalRequest, this);
					IAsyncResult asyncResult = httpWebRequest.BeginGetResponse(Connection.m_TunnelCallback, tunnelStateObject);
					if (!asyncResult.CompletedSynchronously)
					{
						return true;
					}
					httpWebResponse = (HttpWebResponse)httpWebRequest.EndGetResponse(asyncResult);
				}
				else
				{
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				}
				ConnectStream connectStream = (ConnectStream)httpWebResponse.GetResponseStream();
				base.NetworkStream = new NetworkStream(connectStream.Connection.NetworkStream, true);
				connectStream.Connection.NetworkStream.ConvertToNotSocketOwner();
				if (ServicePointManager.FinishProxyTunnelConnectionEarly)
				{
					connectStream.Connection.ForceFinishTunnelConnection();
				}
				result = true;
			}
			catch (Exception innerException)
			{
				if (this.m_InnerException == null)
				{
					this.m_InnerException = innerException;
				}
			}
			return result;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00056AEC File Offset: 0x00054CEC
		private void ForceFinishTunnelConnection()
		{
			this.ServicePoint.DecrementConnection();
			this.ConnectionGroup.DecrementConnection();
			this.RemoveFromConnectionList();
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00056B0A File Offset: 0x00054D0A
		private void CheckNonIdle()
		{
			if (this.m_Idle && this.BusyCount != 0)
			{
				this.m_Idle = false;
				this.ServicePoint.IncrementConnection();
				this.ConnectionGroup.IncrementConnection();
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00056B3C File Offset: 0x00054D3C
		private void CheckIdle()
		{
			if (!this.m_Idle && this.BusyCount == 0)
			{
				this.m_Idle = true;
				this.ServicePoint.DecrementConnection();
				if (this.ConnectionGroup != null)
				{
					this.ConnectionGroup.DecrementConnection();
					this.ConnectionGroup.ConnectionGoneIdle();
				}
				this.m_IdleSinceUtc = DateTime.UtcNow;
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00056B94 File Offset: 0x00054D94
		[Conditional("TRAVE")]
		private void DebugDumpWriteListEntries()
		{
			for (int i = 0; i < this.m_WriteList.Count; i++)
			{
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00056BB8 File Offset: 0x00054DB8
		[Conditional("TRAVE")]
		private void DebugDumpWaitListEntries()
		{
			for (int i = 0; i < this.m_WaitList.Count; i++)
			{
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00056BDB File Offset: 0x00054DDB
		[Conditional("TRAVE")]
		private void DebugDumpListEntry(int currentPos, HttpWebRequest req, string listType)
		{
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00056BDD File Offset: 0x00054DDD
		[Conditional("DEBUG")]
		internal void DebugMembers(int requestHash)
		{
		}

		// Token: 0x0400133A RID: 4922
		[ThreadStatic]
		private static int t_SyncReadNesting;

		// Token: 0x0400133B RID: 4923
		private const int CRLFSize = 2;

		// Token: 0x0400133C RID: 4924
		private const long c_InvalidContentLength = -2L;

		// Token: 0x0400133D RID: 4925
		private const int CachedBufferSize = 4096;

		// Token: 0x0400133E RID: 4926
		private static PinnableBufferCache s_PinnableBufferCache = new PinnableBufferCache("System.Net.Connection", 4096);

		// Token: 0x0400133F RID: 4927
		private WebExceptionStatus m_Error;

		// Token: 0x04001340 RID: 4928
		internal Exception m_InnerException;

		// Token: 0x04001341 RID: 4929
		internal int m_IISVersion = -1;

		// Token: 0x04001342 RID: 4930
		private byte[] m_ReadBuffer;

		// Token: 0x04001343 RID: 4931
		private bool m_ReadBufferFromPinnableCache;

		// Token: 0x04001344 RID: 4932
		private int m_BytesRead;

		// Token: 0x04001345 RID: 4933
		private int m_BytesScanned;

		// Token: 0x04001346 RID: 4934
		private int m_TotalResponseHeadersLength;

		// Token: 0x04001347 RID: 4935
		private int m_MaximumResponseHeadersLength;

		// Token: 0x04001348 RID: 4936
		private long m_MaximumUnauthorizedUploadLength;

		// Token: 0x04001349 RID: 4937
		private CoreResponseData m_ResponseData;

		// Token: 0x0400134A RID: 4938
		private ReadState m_ReadState;

		// Token: 0x0400134B RID: 4939
		private Connection.StatusLineValues m_StatusLineValues;

		// Token: 0x0400134C RID: 4940
		private int m_StatusState;

		// Token: 0x0400134D RID: 4941
		private List<Connection.WaitListItem> m_WaitList;

		// Token: 0x0400134E RID: 4942
		private ArrayList m_WriteList;

		// Token: 0x0400134F RID: 4943
		private IAsyncResult m_LastAsyncResult;

		// Token: 0x04001350 RID: 4944
		private TimerThread.Timer m_RecycleTimer;

		// Token: 0x04001351 RID: 4945
		private WebParseError m_ParseError;

		// Token: 0x04001352 RID: 4946
		private bool m_AtLeastOneResponseReceived;

		// Token: 0x04001353 RID: 4947
		private static readonly WaitCallback m_PostReceiveDelegate = new WaitCallback(Connection.PostReceiveWrapper);

		// Token: 0x04001354 RID: 4948
		private static readonly AsyncCallback m_ReadCallback = new AsyncCallback(Connection.ReadCallbackWrapper);

		// Token: 0x04001355 RID: 4949
		private static readonly AsyncCallback m_TunnelCallback = new AsyncCallback(Connection.TunnelThroughProxyWrapper);

		// Token: 0x04001356 RID: 4950
		private static byte[] s_NullBuffer = new byte[0];

		// Token: 0x04001357 RID: 4951
		private HttpAbortDelegate m_AbortDelegate;

		// Token: 0x04001358 RID: 4952
		private ConnectionGroup m_ConnectionGroup;

		// Token: 0x04001359 RID: 4953
		private UnlockConnectionDelegate m_ConnectionUnlock;

		// Token: 0x0400135A RID: 4954
		private DateTime m_PrepareCloseConnectionSocketCalledUtc;

		// Token: 0x0400135B RID: 4955
		private DateTime m_AbortSocketCalledUtc;

		// Token: 0x0400135C RID: 4956
		private DateTime m_IdleSinceUtc;

		// Token: 0x0400135D RID: 4957
		private HttpWebRequest m_LockedRequest;

		// Token: 0x0400135E RID: 4958
		private HttpWebRequest m_CurrentRequest;

		// Token: 0x0400135F RID: 4959
		private bool m_CanPipeline;

		// Token: 0x04001360 RID: 4960
		private bool m_Free = true;

		// Token: 0x04001361 RID: 4961
		private bool m_Idle = true;

		// Token: 0x04001362 RID: 4962
		private bool m_KeepAlive = true;

		// Token: 0x04001363 RID: 4963
		private bool m_Pipelining;

		// Token: 0x04001364 RID: 4964
		private int m_ReservedCount;

		// Token: 0x04001365 RID: 4965
		private bool m_ReadDone;

		// Token: 0x04001366 RID: 4966
		private bool m_WriteDone;

		// Token: 0x04001367 RID: 4967
		private bool m_RemovedFromConnectionList;

		// Token: 0x04001368 RID: 4968
		private bool m_NonKeepAliveRequestPipelined;

		// Token: 0x04001369 RID: 4969
		private bool m_IsPipelinePaused;

		// Token: 0x0400136A RID: 4970
		private static int s_MaxPipelinedCount = 10;

		// Token: 0x0400136B RID: 4971
		private static int s_MinPipelinedCount = 5;

		// Token: 0x0400136C RID: 4972
		private const int BeforeVersionNumbers = 0;

		// Token: 0x0400136D RID: 4973
		private const int MajorVersionNumber = 1;

		// Token: 0x0400136E RID: 4974
		private const int MinorVersionNumber = 2;

		// Token: 0x0400136F RID: 4975
		private const int StatusCodeNumber = 3;

		// Token: 0x04001370 RID: 4976
		private const int AfterStatusCode = 4;

		// Token: 0x04001371 RID: 4977
		private const int AfterCarriageReturn = 5;

		// Token: 0x04001372 RID: 4978
		private const string BeforeVersionNumberBytes = "HTTP/";

		// Token: 0x04001373 RID: 4979
		private static readonly string[] s_ShortcutStatusDescriptions = new string[]
		{
			"OK",
			"Continue",
			"Unauthorized"
		};

		// Token: 0x02000748 RID: 1864
		private class StatusLineValues
		{
			// Token: 0x040031EC RID: 12780
			internal int MajorVersion;

			// Token: 0x040031ED RID: 12781
			internal int MinorVersion;

			// Token: 0x040031EE RID: 12782
			internal int StatusCode;

			// Token: 0x040031EF RID: 12783
			internal string StatusDescription;
		}

		// Token: 0x02000749 RID: 1865
		private class WaitListItem
		{
			// Token: 0x17000F14 RID: 3860
			// (get) Token: 0x060041F2 RID: 16882 RVA: 0x00112387 File Offset: 0x00110587
			public HttpWebRequest Request
			{
				get
				{
					return this.request;
				}
			}

			// Token: 0x17000F15 RID: 3861
			// (get) Token: 0x060041F3 RID: 16883 RVA: 0x0011238F File Offset: 0x0011058F
			public long QueueStartTime
			{
				get
				{
					return this.queueStartTime;
				}
			}

			// Token: 0x060041F4 RID: 16884 RVA: 0x00112397 File Offset: 0x00110597
			public WaitListItem(HttpWebRequest request, long queueStartTime)
			{
				this.request = request;
				this.queueStartTime = queueStartTime;
			}

			// Token: 0x040031F0 RID: 12784
			private HttpWebRequest request;

			// Token: 0x040031F1 RID: 12785
			private long queueStartTime;
		}

		// Token: 0x0200074A RID: 1866
		private class AsyncTriState
		{
			// Token: 0x060041F5 RID: 16885 RVA: 0x001123AD File Offset: 0x001105AD
			public AsyncTriState(TriState newValue)
			{
				this.Value = newValue;
			}

			// Token: 0x040031F2 RID: 12786
			public TriState Value;
		}
	}
}
