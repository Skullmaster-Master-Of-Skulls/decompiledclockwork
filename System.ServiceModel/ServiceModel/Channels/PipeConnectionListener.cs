using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000848 RID: 2120
	internal class PipeConnectionListener : IConnectionListener, IDisposable
	{
		// Token: 0x06004F60 RID: 20320 RVA: 0x00121F30 File Offset: 0x00120130
		public PipeConnectionListener(Uri pipeUri, HostNameComparisonMode hostNameComparisonMode, int bufferSize, List<SecurityIdentifier> allowedSids, bool useCompletionPort, int maxConnections)
		{
			PipeUri.Validate(pipeUri);
			this.pipeUri = pipeUri;
			this.hostNameComparisonMode = hostNameComparisonMode;
			this.allowedSids = allowedSids;
			this.bufferSize = bufferSize;
			this.pendingAccepts = new List<PipeConnectionListener.PendingAccept>();
			this.useCompletionPort = useCompletionPort;
			this.maxInstances = Math.Min(maxConnections, 255);
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06004F61 RID: 20321 RVA: 0x00121F8B File Offset: 0x0012018B
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06004F62 RID: 20322 RVA: 0x00121F8E File Offset: 0x0012018E
		public string PipeName
		{
			get
			{
				return this.sharedMemory.PipeName;
			}
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x00121F9C File Offset: 0x0012019C
		public IAsyncResult BeginAccept(AsyncCallback callback, object state)
		{
			object thisLock = this.ThisLock;
			IAsyncResult result;
			lock (thisLock)
			{
				if (this.isDisposed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException("", SR.GetString("PipeListenerDisposed")));
				}
				if (!this.isListening)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PipeListenerNotListening")));
				}
				PipeHandle pipeHandle = this.CreatePipe();
				PipeConnectionListener.PendingAccept pendingAccept = new PipeConnectionListener.PendingAccept(this, pipeHandle, this.useCompletionPort, callback, state);
				if (!pendingAccept.CompletedSynchronously)
				{
					this.pendingAccepts.Add(pendingAccept);
				}
				result = pendingAccept;
			}
			return result;
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x00122050 File Offset: 0x00120250
		public IConnection EndAccept(IAsyncResult result)
		{
			PipeConnectionListener.PendingAccept pendingAccept = result as PipeConnectionListener.PendingAccept;
			if (pendingAccept == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("result", SR.GetString("InvalidAsyncResult"));
			}
			PipeHandle pipeHandle = pendingAccept.End();
			if (pipeHandle == null)
			{
				return null;
			}
			return new PipeConnection(pipeHandle, this.bufferSize, pendingAccept.IsBoundToCompletionPort, pendingAccept.IsBoundToCompletionPort);
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x001220A8 File Offset: 0x001202A8
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private unsafe PipeHandle CreatePipe()
		{
			int num = 1073741827;
			if (!this.anyPipesCreated)
			{
				num |= 524288;
			}
			byte[] array;
			try
			{
				array = SecurityDescriptorHelper.FromSecurityIdentifiers(this.allowedSids, -1073741824);
			}
			catch (Win32Exception ex)
			{
				Exception ex2 = new PipeException(ex.Message, ex);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(ex2.Message, ex2));
			}
			byte[] array2;
			byte* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array2[0];
			}
			UnsafeNativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new UnsafeNativeMethods.SECURITY_ATTRIBUTES();
			security_ATTRIBUTES.lpSecurityDescriptor = (IntPtr)((void*)value);
			string pipeName = this.sharedMemory.PipeName;
			PipeHandle pipeHandle = UnsafeNativeMethods.CreateNamedPipe(pipeName, num, 6, this.maxInstances, this.bufferSize, this.bufferSize, 0, security_ATTRIBUTES);
			int lastWin32Error = Marshal.GetLastWin32Error();
			array2 = null;
			if (!pipeHandle.IsInvalid)
			{
				if (TD.NamedPipeCreatedIsEnabled())
				{
					TD.NamedPipeCreated(pipeName);
				}
				bool flag = true;
				PipeHandle result;
				try
				{
					if (this.useCompletionPort)
					{
						ThreadPool.BindHandle(pipeHandle);
					}
					this.anyPipesCreated = true;
					flag = false;
					result = pipeHandle;
				}
				finally
				{
					if (flag)
					{
						pipeHandle.Close();
					}
				}
				return result;
			}
			pipeHandle.SetHandleAsInvalid();
			Exception ex3 = new PipeException(SR.GetString("PipeListenFailed", new object[]
			{
				this.pipeUri.AbsoluteUri,
				PipeError.GetErrorString(lastWin32Error)
			}), lastWin32Error);
			if (lastWin32Error == 5)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AddressAccessDeniedException(ex3.Message, ex3));
			}
			if (lastWin32Error == 183)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AddressAlreadyInUseException(ex3.Message, ex3));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(ex3.Message, ex3));
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x00122264 File Offset: 0x00120464
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.isDisposed)
				{
					if (this.sharedMemory != null)
					{
						this.sharedMemory.Dispose();
					}
					for (int i = 0; i < this.pendingAccepts.Count; i++)
					{
						this.pendingAccepts[i].Abort();
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x001222E8 File Offset: 0x001204E8
		public void Listen()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.isListening)
				{
					bool flag2 = !LocalAppContextSwitches.AlwaysTryCreateNamedPipeInGlobalNamespace && AppContainerInfo.IsRunningInAppContainer;
					string sharedMemoryName = PipeUri.BuildSharedMemoryName(this.pipeUri, this.hostNameComparisonMode, true);
					if (flag2 || !PipeSharedMemory.TryCreate(this.allowedSids, this.pipeUri, sharedMemoryName, out this.sharedMemory))
					{
						PipeSharedMemory pipeSharedMemory = null;
						Uri uri = new Uri(this.pipeUri, Guid.NewGuid().ToString());
						string sharedMemoryName2 = PipeUri.BuildSharedMemoryName(uri, this.hostNameComparisonMode, true);
						if (!flag2 && PipeSharedMemory.TryCreate(this.allowedSids, uri, sharedMemoryName2, out pipeSharedMemory))
						{
							pipeSharedMemory.Dispose();
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeSharedMemory.CreatePipeNameInUseException(5, this.pipeUri));
						}
						sharedMemoryName = PipeUri.BuildSharedMemoryName(this.pipeUri, this.hostNameComparisonMode, false);
						this.sharedMemory = PipeSharedMemory.Create(this.allowedSids, this.pipeUri, sharedMemoryName);
					}
					this.isListening = true;
				}
			}
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x0012240C File Offset: 0x0012060C
		private void RemovePendingAccept(PipeConnectionListener.PendingAccept pendingAccept)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.pendingAccepts.Remove(pendingAccept);
			}
		}

		// Token: 0x04003141 RID: 12609
		private Uri pipeUri;

		// Token: 0x04003142 RID: 12610
		private int bufferSize;

		// Token: 0x04003143 RID: 12611
		private HostNameComparisonMode hostNameComparisonMode;

		// Token: 0x04003144 RID: 12612
		private bool isDisposed;

		// Token: 0x04003145 RID: 12613
		private bool isListening;

		// Token: 0x04003146 RID: 12614
		private List<PipeConnectionListener.PendingAccept> pendingAccepts;

		// Token: 0x04003147 RID: 12615
		private bool anyPipesCreated;

		// Token: 0x04003148 RID: 12616
		private PipeSharedMemory sharedMemory;

		// Token: 0x04003149 RID: 12617
		private List<SecurityIdentifier> allowedSids;

		// Token: 0x0400314A RID: 12618
		private bool useCompletionPort;

		// Token: 0x0400314B RID: 12619
		private int maxInstances;

		// Token: 0x02000D37 RID: 3383
		private class PendingAccept : AsyncResult
		{
			// Token: 0x06007C31 RID: 31793 RVA: 0x001D008C File Offset: 0x001CE28C
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			public PendingAccept(PipeConnectionListener listener, PipeHandle pipeHandle, bool isBoundToCompletionPort, AsyncCallback callback, object state) : base(callback, state)
			{
				this.pipeHandle = pipeHandle;
				this.result = pipeHandle;
				this.listener = listener;
				this.onAcceptComplete = new OverlappedIOCompleteCallback(this.OnAcceptComplete);
				this.overlapped = new OverlappedContext();
				this.isBoundToCompletionPort = isBoundToCompletionPort;
				if (TD.PipeConnectionAcceptStartIsEnabled())
				{
					this.eventTraceActivity = new EventTraceActivity(false);
					TD.PipeConnectionAcceptStart(this.eventTraceActivity, (this.listener.pipeUri != null) ? this.listener.pipeUri.ToString() : string.Empty);
				}
				if (!Thread.CurrentThread.IsThreadPoolThread)
				{
					if (PipeConnectionListener.PendingAccept.onStartAccept == null)
					{
						PipeConnectionListener.PendingAccept.onStartAccept = new Action<object>(PipeConnectionListener.PendingAccept.OnStartAccept);
					}
					ActionItem.Schedule(PipeConnectionListener.PendingAccept.onStartAccept, this);
					return;
				}
				this.StartAccept(true);
			}

			// Token: 0x17001BD3 RID: 7123
			// (get) Token: 0x06007C32 RID: 31794 RVA: 0x001D015B File Offset: 0x001CE35B
			public bool IsBoundToCompletionPort
			{
				get
				{
					return this.isBoundToCompletionPort;
				}
			}

			// Token: 0x06007C33 RID: 31795 RVA: 0x001D0164 File Offset: 0x001CE364
			private static void OnStartAccept(object state)
			{
				PipeConnectionListener.PendingAccept pendingAccept = (PipeConnectionListener.PendingAccept)state;
				pendingAccept.StartAccept(false);
			}

			// Token: 0x06007C34 RID: 31796 RVA: 0x001D0180 File Offset: 0x001CE380
			private Exception CreatePipeAcceptFailedException(int errorCode)
			{
				Exception ex = new PipeException(SR.GetString("PipeAcceptFailed", new object[]
				{
					PipeError.GetErrorString(errorCode)
				}), errorCode);
				return new CommunicationException(ex.Message, ex);
			}

			// Token: 0x06007C35 RID: 31797 RVA: 0x001D01BC File Offset: 0x001CE3BC
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			private void StartAccept(bool synchronous)
			{
				Exception exception = null;
				bool flag = false;
				try
				{
					try
					{
						try
						{
							this.overlapped.StartAsyncOperation(null, this.onAcceptComplete, this.isBoundToCompletionPort);
							while (UnsafeNativeMethods.ConnectNamedPipe(this.pipeHandle, this.overlapped.NativeOverlapped) == 0)
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								if (lastWin32Error != 232)
								{
									if (lastWin32Error != 535)
									{
										if (lastWin32Error != 997)
										{
											flag = true;
											throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreatePipeAcceptFailedException(lastWin32Error));
										}
									}
									else
									{
										flag = true;
									}
									goto IL_9D;
								}
								if (UnsafeNativeMethods.DisconnectNamedPipe(this.pipeHandle) == 0)
								{
									flag = true;
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreatePipeAcceptFailedException(lastWin32Error));
								}
							}
							flag = true;
						}
						catch (ObjectDisposedException exception2)
						{
							DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
							flag = true;
						}
						IL_9D:;
					}
					finally
					{
						if (flag)
						{
							this.overlapped.CancelAsyncOperation();
							this.overlapped.Free();
						}
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
					if (!synchronous)
					{
						this.listener.RemovePendingAccept(this);
					}
					base.Complete(synchronous, exception);
				}
			}

			// Token: 0x06007C36 RID: 31798 RVA: 0x001D02DC File Offset: 0x001CE4DC
			public void Abort()
			{
				this.result = null;
				this.pipeHandle.Close();
			}

			// Token: 0x06007C37 RID: 31799 RVA: 0x001D02F0 File Offset: 0x001CE4F0
			public PipeHandle End()
			{
				AsyncResult.End<PipeConnectionListener.PendingAccept>(this);
				return this.result;
			}

			// Token: 0x06007C38 RID: 31800 RVA: 0x001D0300 File Offset: 0x001CE500
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			private void OnAcceptComplete(bool haveResult, int error, int numBytes)
			{
				this.listener.RemovePendingAccept(this);
				if (!haveResult)
				{
					if (this.result != null && UnsafeNativeMethods.GetOverlappedResult(this.pipeHandle, this.overlapped.NativeOverlapped, out numBytes, 0) == 0)
					{
						error = Marshal.GetLastWin32Error();
					}
					else
					{
						error = 0;
					}
				}
				this.overlapped.Free();
				if (TD.PipeConnectionAcceptStopIsEnabled())
				{
					TD.PipeConnectionAcceptStop(this.eventTraceActivity);
				}
				if (error != 0)
				{
					this.pipeHandle.Close();
					base.Complete(false, this.CreatePipeAcceptFailedException(error));
					return;
				}
				base.Complete(false);
			}

			// Token: 0x04004755 RID: 18261
			private PipeHandle pipeHandle;

			// Token: 0x04004756 RID: 18262
			private PipeHandle result;

			// Token: 0x04004757 RID: 18263
			private OverlappedIOCompleteCallback onAcceptComplete;

			// Token: 0x04004758 RID: 18264
			private static Action<object> onStartAccept;

			// Token: 0x04004759 RID: 18265
			private OverlappedContext overlapped;

			// Token: 0x0400475A RID: 18266
			private bool isBoundToCompletionPort;

			// Token: 0x0400475B RID: 18267
			private PipeConnectionListener listener;

			// Token: 0x0400475C RID: 18268
			private EventTraceActivity eventTraceActivity;
		}
	}
}
