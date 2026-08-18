using System;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000847 RID: 2119
	internal class PipeConnectionInitiator : IConnectionInitiator
	{
		// Token: 0x06004F57 RID: 20311 RVA: 0x00121A1C File Offset: 0x0011FC1C
		public PipeConnectionInitiator(int bufferSize, IPipeTransportFactorySettings pipeSettings)
		{
			this.bufferSize = bufferSize;
			this.pipeSettings = pipeSettings;
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x00121A32 File Offset: 0x0011FC32
		private Exception CreateConnectFailedException(Uri remoteUri, PipeException innerException)
		{
			return new CommunicationException(SR.GetString("PipeConnectFailed", new object[]
			{
				remoteUri.AbsoluteUri
			}), innerException);
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x00121A54 File Offset: 0x0011FC54
		public IConnection Connect(Uri remoteUri, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			string resolvedAddress;
			BackoffTimeoutHelper backoffTimeoutHelper;
			this.PrepareConnect(remoteUri, timeoutHelper.RemainingTime(), out resolvedAddress, out backoffTimeoutHelper);
			IConnection connection = null;
			while (connection == null)
			{
				connection = this.TryConnect(remoteUri, resolvedAddress, backoffTimeoutHelper);
				if (connection == null)
				{
					backoffTimeoutHelper.WaitAndBackoff();
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 262193, SR.GetString("TraceCodeFailedPipeConnect", new object[]
						{
							timeoutHelper.RemainingTime(),
							remoteUri
						}));
					}
				}
			}
			return connection;
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x00121ACC File Offset: 0x0011FCCC
		internal static string GetPipeName(Uri uri, IPipeTransportFactorySettings transportFactorySettings)
		{
			AppContainerInfo appContainerInfo = PipeConnectionInitiator.GetAppContainerInfo(transportFactorySettings);
			string[] array = new string[]
			{
				"+",
				uri.Host,
				"*"
			};
			bool[] array2 = new bool[2];
			array2[0] = true;
			bool[] array3 = array2;
			string text = string.Empty;
			string text2 = null;
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array3.Length; j++)
				{
					if (appContainerInfo == null || !array3[j])
					{
						string text3 = PipeUri.GetPath(uri);
						while (text3.Length > 0)
						{
							string sharedMemoryName = PipeUri.BuildSharedMemoryName(array[i], text3, array3[j], appContainerInfo);
							try
							{
								PipeSharedMemory pipeSharedMemory = PipeSharedMemory.Open(sharedMemoryName, uri);
								if (pipeSharedMemory != null)
								{
									try
									{
										string pipeName = pipeSharedMemory.GetPipeName(appContainerInfo);
										if (pipeName != null)
										{
											if (!ServiceModelAppSettings.UseBestMatchNamedPipeUri)
											{
												return pipeName;
											}
											if (text3.Length > text.Length)
											{
												text = text3;
												text2 = pipeName;
											}
										}
									}
									finally
									{
										pipeSharedMemory.Dispose();
									}
								}
							}
							catch (AddressAccessDeniedException innerException)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("EndpointNotFound", new object[]
								{
									uri.AbsoluteUri
								}), innerException));
							}
							text3 = PipeUri.GetParentPath(text3);
						}
					}
				}
			}
			if (string.IsNullOrEmpty(text2))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("EndpointNotFound", new object[]
				{
					uri.AbsoluteUri
				}), new PipeException(SR.GetString("PipeEndpointNotFound", new object[]
				{
					uri.AbsoluteUri
				}))));
			}
			return text2;
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x00121C74 File Offset: 0x0011FE74
		public IAsyncResult BeginConnect(Uri uri, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new PipeConnectionInitiator.ConnectAsyncResult(this, uri, timeout, callback, state);
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x00121C81 File Offset: 0x0011FE81
		public IConnection EndConnect(IAsyncResult result)
		{
			return PipeConnectionInitiator.ConnectAsyncResult.End(result);
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x00121C8C File Offset: 0x0011FE8C
		private void PrepareConnect(Uri remoteUri, TimeSpan timeout, out string resolvedAddress, out BackoffTimeoutHelper backoffHelper)
		{
			PipeUri.Validate(remoteUri);
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262186, SR.GetString("TraceCodeInitiatingNamedPipeConnection"), new StringTraceRecord("Uri", remoteUri.ToString()), this, null);
			}
			resolvedAddress = PipeConnectionInitiator.GetPipeName(remoteUri, this.pipeSettings);
			TimeSpan timeout2;
			if (timeout >= TimeSpan.FromMilliseconds(300.0))
			{
				timeout2 = TimeoutHelper.Add(timeout, TimeSpan.Zero - TimeSpan.FromMilliseconds(150.0));
			}
			else
			{
				timeout2 = Ticks.ToTimeSpan(Ticks.FromMilliseconds(150) / 2L + 1L);
			}
			backoffHelper = new BackoffTimeoutHelper(timeout2, TimeSpan.FromMinutes(5.0));
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x00121D40 File Offset: 0x0011FF40
		private IConnection TryConnect(Uri remoteUri, string resolvedAddress, BackoffTimeoutHelper backoffHelper)
		{
			bool flag = backoffHelper.IsExpired();
			int num = 1073741824;
			num |= 1048576;
			PipeHandle pipeHandle = UnsafeNativeMethods.CreateFile(resolvedAddress, -1073741824, 0, IntPtr.Zero, 3, num, IntPtr.Zero);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (pipeHandle.IsInvalid)
			{
				pipeHandle.SetHandleAsInvalid();
				if (lastWin32Error != 2 && lastWin32Error != 231)
				{
					PipeException innerException = new PipeException(SR.GetString("PipeConnectAddressFailed", new object[]
					{
						resolvedAddress,
						PipeError.GetErrorString(lastWin32Error)
					}), lastWin32Error);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateConnectFailedException(remoteUri, innerException));
				}
				if (flag)
				{
					Exception innerException2 = new PipeException(SR.GetString("PipeConnectAddressFailed", new object[]
					{
						resolvedAddress,
						PipeError.GetErrorString(lastWin32Error)
					}), lastWin32Error);
					string absoluteUri = remoteUri.AbsoluteUri;
					TimeoutException exception;
					if (lastWin32Error == 231)
					{
						exception = new TimeoutException(SR.GetString("PipeConnectTimedOutServerTooBusy", new object[]
						{
							absoluteUri,
							backoffHelper.OriginalTimeout
						}), innerException2);
					}
					else
					{
						exception = new TimeoutException(SR.GetString("PipeConnectTimedOut", new object[]
						{
							absoluteUri,
							backoffHelper.OriginalTimeout
						}), innerException2);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
				}
				return null;
			}
			else
			{
				int num2 = 2;
				if (UnsafeNativeMethods.SetNamedPipeHandleState(pipeHandle, ref num2, IntPtr.Zero, IntPtr.Zero) == 0)
				{
					lastWin32Error = Marshal.GetLastWin32Error();
					pipeHandle.Close();
					PipeException innerException3 = new PipeException(SR.GetString("PipeModeChangeFailed", new object[]
					{
						PipeError.GetErrorString(lastWin32Error)
					}), lastWin32Error);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateConnectFailedException(remoteUri, innerException3));
				}
				return new PipeConnection(pipeHandle, this.bufferSize, false, true);
			}
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00121EE4 File Offset: 0x001200E4
		private static AppContainerInfo GetAppContainerInfo(IPipeTransportFactorySettings transportFactorySettings)
		{
			if (AppContainerInfo.IsAppContainerSupported && transportFactorySettings != null && transportFactorySettings.PipeSettings != null)
			{
				ApplicationContainerSettings applicationContainerSettings = transportFactorySettings.PipeSettings.ApplicationContainerSettings;
				if (applicationContainerSettings != null && applicationContainerSettings.TargetingAppContainer)
				{
					return AppContainerInfo.CreateAppContainerInfo(applicationContainerSettings.PackageFullName, applicationContainerSettings.SessionId);
				}
			}
			return null;
		}

		// Token: 0x0400313F RID: 12607
		private int bufferSize;

		// Token: 0x04003140 RID: 12608
		private IPipeTransportFactorySettings pipeSettings;

		// Token: 0x02000D36 RID: 3382
		private class ConnectAsyncResult : AsyncResult
		{
			// Token: 0x06007C2D RID: 31789 RVA: 0x001CFF1C File Offset: 0x001CE11C
			public ConnectAsyncResult(PipeConnectionInitiator parent, Uri remoteUri, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
				this.remoteUri = remoteUri;
				this.timeoutHelper = new TimeoutHelper(timeout);
				parent.PrepareConnect(remoteUri, this.timeoutHelper.RemainingTime(), out this.resolvedAddress, out this.backoffHelper);
				if (this.ConnectAndWait())
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007C2E RID: 31790 RVA: 0x001CFF7C File Offset: 0x001CE17C
			private bool ConnectAndWait()
			{
				this.connection = this.parent.TryConnect(this.remoteUri, this.resolvedAddress, this.backoffHelper);
				bool flag = this.connection != null;
				if (!flag)
				{
					if (PipeConnectionInitiator.ConnectAsyncResult.waitCompleteCallback == null)
					{
						PipeConnectionInitiator.ConnectAsyncResult.waitCompleteCallback = new Action<object>(PipeConnectionInitiator.ConnectAsyncResult.OnWaitComplete);
					}
					this.backoffHelper.WaitAndBackoff(PipeConnectionInitiator.ConnectAsyncResult.waitCompleteCallback, this);
				}
				return flag;
			}

			// Token: 0x06007C2F RID: 31791 RVA: 0x001CFFE4 File Offset: 0x001CE1E4
			public static IConnection End(IAsyncResult result)
			{
				PipeConnectionInitiator.ConnectAsyncResult connectAsyncResult = AsyncResult.End<PipeConnectionInitiator.ConnectAsyncResult>(result);
				return connectAsyncResult.connection;
			}

			// Token: 0x06007C30 RID: 31792 RVA: 0x001D0000 File Offset: 0x001CE200
			private static void OnWaitComplete(object state)
			{
				Exception exception = null;
				PipeConnectionInitiator.ConnectAsyncResult connectAsyncResult = (PipeConnectionInitiator.ConnectAsyncResult)state;
				bool flag = true;
				try
				{
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 262193, SR.GetString("TraceCodeFailedPipeConnect", new object[]
						{
							connectAsyncResult.timeoutHelper.RemainingTime(),
							connectAsyncResult.remoteUri
						}));
					}
					flag = connectAsyncResult.ConnectAndWait();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				if (flag)
				{
					connectAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x0400474E RID: 18254
			private PipeConnectionInitiator parent;

			// Token: 0x0400474F RID: 18255
			private Uri remoteUri;

			// Token: 0x04004750 RID: 18256
			private string resolvedAddress;

			// Token: 0x04004751 RID: 18257
			private BackoffTimeoutHelper backoffHelper;

			// Token: 0x04004752 RID: 18258
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004753 RID: 18259
			private IConnection connection;

			// Token: 0x04004754 RID: 18260
			private static Action<object> waitCompleteCallback;
		}
	}
}
