using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000740 RID: 1856
	[__DynamicallyInvokable]
	public abstract class CommunicationObject : ICommunicationObject
	{
		// Token: 0x0600468D RID: 18061 RVA: 0x00106F71 File Offset: 0x00105171
		[__DynamicallyInvokable]
		protected CommunicationObject() : this(new object())
		{
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x00106F7E File Offset: 0x0010517E
		[__DynamicallyInvokable]
		protected CommunicationObject(object mutex)
		{
			this.mutex = mutex;
			this.eventSender = this;
			this.state = CommunicationState.Created;
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x00106F9B File Offset: 0x0010519B
		internal CommunicationObject(object mutex, object eventSender)
		{
			this.mutex = mutex;
			this.eventSender = eventSender;
			this.state = CommunicationState.Created;
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06004690 RID: 18064 RVA: 0x00106FB8 File Offset: 0x001051B8
		internal bool Aborted
		{
			get
			{
				return this.aborted;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06004691 RID: 18065 RVA: 0x00106FC0 File Offset: 0x001051C0
		// (set) Token: 0x06004692 RID: 18066 RVA: 0x00106FC8 File Offset: 0x001051C8
		internal object EventSender
		{
			get
			{
				return this.eventSender;
			}
			set
			{
				this.eventSender = value;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x00106FD1 File Offset: 0x001051D1
		[__DynamicallyInvokable]
		protected bool IsDisposed
		{
			[__DynamicallyInvokable]
			get
			{
				return this.state == CommunicationState.Closed;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x00106FDC File Offset: 0x001051DC
		[__DynamicallyInvokable]
		public CommunicationState State
		{
			[__DynamicallyInvokable]
			get
			{
				return this.state;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x00106FE4 File Offset: 0x001051E4
		[__DynamicallyInvokable]
		protected object ThisLock
		{
			[__DynamicallyInvokable]
			get
			{
				return this.mutex;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06004696 RID: 18070
		[__DynamicallyInvokable]
		protected abstract TimeSpan DefaultCloseTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06004697 RID: 18071
		[__DynamicallyInvokable]
		protected abstract TimeSpan DefaultOpenTimeout { [__DynamicallyInvokable] get; }

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06004698 RID: 18072 RVA: 0x00106FEC File Offset: 0x001051EC
		internal TimeSpan InternalCloseTimeout
		{
			get
			{
				return this.DefaultCloseTimeout;
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x00106FF4 File Offset: 0x001051F4
		internal TimeSpan InternalOpenTimeout
		{
			get
			{
				return this.DefaultOpenTimeout;
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600469A RID: 18074 RVA: 0x00106FFC File Offset: 0x001051FC
		// (remove) Token: 0x0600469B RID: 18075 RVA: 0x00107034 File Offset: 0x00105234
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		public event EventHandler Closed;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600469C RID: 18076 RVA: 0x0010706C File Offset: 0x0010526C
		// (remove) Token: 0x0600469D RID: 18077 RVA: 0x001070A4 File Offset: 0x001052A4
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		public event EventHandler Closing;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x0600469E RID: 18078 RVA: 0x001070DC File Offset: 0x001052DC
		// (remove) Token: 0x0600469F RID: 18079 RVA: 0x00107114 File Offset: 0x00105314
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		public event EventHandler Faulted;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060046A0 RID: 18080 RVA: 0x0010714C File Offset: 0x0010534C
		// (remove) Token: 0x060046A1 RID: 18081 RVA: 0x00107184 File Offset: 0x00105384
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		public event EventHandler Opened;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060046A2 RID: 18082 RVA: 0x001071BC File Offset: 0x001053BC
		// (remove) Token: 0x060046A3 RID: 18083 RVA: 0x001071F4 File Offset: 0x001053F4
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		public event EventHandler Opening;

		// Token: 0x060046A4 RID: 18084 RVA: 0x0010722C File Offset: 0x0010542C
		[__DynamicallyInvokable]
		public void Abort()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.aborted || this.state == CommunicationState.Closed)
				{
					return;
				}
				this.aborted = true;
				this.state = CommunicationState.Closing;
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 524290, SR.GetString("TraceCodeCommunicationObjectAborted", new object[]
				{
					TraceUtility.CreateSourceString(this)
				}), this);
			}
			bool flag2 = true;
			try
			{
				this.OnClosing();
				if (!this.onClosingCalled)
				{
					throw TraceUtility.ThrowHelperError(this.CreateBaseClassMethodNotCalledException("OnClosing"), Guid.Empty, this);
				}
				this.OnAbort();
				this.OnClosed();
				if (!this.onClosedCalled)
				{
					throw TraceUtility.ThrowHelperError(this.CreateBaseClassMethodNotCalledException("OnClosed"), Guid.Empty, this);
				}
				flag2 = false;
			}
			finally
			{
				if (flag2 && DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 524291, SR.GetString("TraceCodeCommunicationObjectAbortFailed", new object[]
					{
						this.GetCommunicationObjectType().ToString()
					}), this);
				}
			}
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x00107350 File Offset: 0x00105550
		[__DynamicallyInvokable]
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.BeginClose(this.DefaultCloseTimeout, callback, state);
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x00107360 File Offset: 0x00105560
		[__DynamicallyInvokable]
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", SR.GetString("SFxTimeoutOutOfRange0")));
			}
			IAsyncResult result2;
			using ((DiagnosticUtility.ShouldUseActivity && this.TraceOpenAndClose) ? this.CreateCloseActivity() : null)
			{
				object thisLock = this.ThisLock;
				CommunicationState communicationState;
				lock (thisLock)
				{
					communicationState = this.state;
					if (communicationState != CommunicationState.Closed)
					{
						this.state = CommunicationState.Closing;
					}
					this.closeCalled = true;
				}
				switch (communicationState)
				{
				case CommunicationState.Created:
				case CommunicationState.Opening:
				case CommunicationState.Faulted:
					this.Abort();
					if (communicationState == CommunicationState.Faulted)
					{
						throw TraceUtility.ThrowHelperError(this.CreateFaultedException(), Guid.Empty, this);
					}
					return new CommunicationObject.AlreadyClosedAsyncResult(callback, state);
				case CommunicationState.Opened:
				{
					bool flag2 = true;
					try
					{
						this.OnClosing();
						if (!this.onClosingCalled)
						{
							throw TraceUtility.ThrowHelperError(this.CreateBaseClassMethodNotCalledException("OnClosing"), Guid.Empty, this);
						}
						IAsyncResult result = new CommunicationObject.CloseAsyncResult(this, timeout, callback, state);
						flag2 = false;
						return result;
					}
					finally
					{
						if (flag2)
						{
							if (DiagnosticUtility.ShouldTraceWarning)
							{
								TraceUtility.TraceEvent(TraceEventType.Warning, 524292, SR.GetString("TraceCodeCommunicationObjectCloseFailed", new object[]
								{
									this.GetCommunicationObjectType().ToString()
								}), this);
							}
							this.Abort();
						}
					}
					break;
				}
				case CommunicationState.Closing:
				case CommunicationState.Closed:
					break;
				default:
					throw Fx.AssertAndThrow("CommunicationObject.BeginClose: Unknown CommunicationState");
				}
				result2 = new CommunicationObject.AlreadyClosedAsyncResult(callback, state);
			}
			return result2;
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x0010751C File Offset: 0x0010571C
		[__DynamicallyInvokable]
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.BeginOpen(this.DefaultOpenTimeout, callback, state);
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x0010752C File Offset: 0x0010572C
		[__DynamicallyInvokable]
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", SR.GetString("SFxTimeoutOutOfRange0")));
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.ThrowIfDisposedOrImmutable();
				this.state = CommunicationState.Opening;
			}
			bool flag2 = true;
			IAsyncResult result;
			try
			{
				this.OnOpening();
				if (!this.onOpeningCalled)
				{
					throw TraceUtility.ThrowHelperError(this.CreateBaseClassMethodNotCalledException("OnOpening"), Guid.Empty, this);
				}
				IAsyncResult asyncResult = new CommunicationObject.OpenAsyncResult(this, timeout, callback, state);
				flag2 = false;
				result = asyncResult;
			}
			finally
			{
				if (flag2)
				{
					if (DiagnosticUtility.ShouldTraceWarning)
					{
						TraceUtility.TraceEvent(TraceEventType.Warning, 524293, SR.GetString("TraceCodeCommunicationObjectOpenFailed", new object[]
						{
							this.GetCommunicationObjectType().ToString()
						}), this);
					}
					this.Fault();
				}
			}
			return result;
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x00107620 File Offset: 0x00105820
		[__DynamicallyInvokable]
		public void Close()
		{
			this.Close(this.DefaultCloseTimeout);
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x00107630 File Offset: 0x00105830
		[__DynamicallyInvokable]
		public void Close(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", SR.GetString("SFxTimeoutOutOfRange0")));
			}
			using ((DiagnosticUtility.ShouldUseActivity && this.TraceOpenAndClose) ? this.CreateCloseActivity() : null)
			{
				object thisLock = this.ThisLock;
				CommunicationState communicationState;
				lock (thisLock)
				{
					communicationState = this.state;
					if (communicationState != CommunicationState.Closed)
					{
						this.state = CommunicationState.Closing;
					}
					this.closeCalled = true;
				}
				switch (communicationState)
				{
				case CommunicationState.Created:
				case CommunicationState.Opening:
				case CommunicationState.Faulted:
					this.Abort();
					if (communicationState == CommunicationState.Faulted)
					{
						throw TraceUtility.ThrowHelperError(this.CreateFaultedException(), Guid.Empty, this);
					}
					goto IL_16B;
				case CommunicationState.Opened:
				{
					bool flag2 = true;
					try
					{
						TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
						this.OnClosing();
						if (!this.onClosingCalled)
						{
							throw TraceUtility.ThrowHelperError(this.CreateBaseClassMethodNotCalledException("OnClosing"), Guid.Empty, this);
						}
						this.OnClose(timeoutHelper.RemainingTime());
						this.OnClosed();
						if (!this.onClosedCalled)
						{
							throw TraceUtility.ThrowHelperError(this.CreateBaseClassMethodNotCalledException("OnClosed"), Guid.Empty, this);
						}
						flag2 = false;
						return;
					}
					finally
					{
						if (flag2)
						{
							if (DiagnosticUtility.ShouldTraceWarning)
							{
								TraceUtility.TraceEvent(TraceEventType.Warning, 524292, SR.GetString("TraceCodeCommunicationObjectCloseFailed", new object[]
								{
									this.GetCommunicationObjectType().ToString()
								}), this);
							}
							this.Abort();
						}
					}
					break;
				}
				case CommunicationState.Closing:
				case CommunicationState.Closed:
					goto IL_16B;
				}
				throw Fx.AssertAndThrow("CommunicationObject.BeginClose: Unknown CommunicationState");
				IL_16B:;
			}
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x00107800 File Offset: 0x00105A00
		private Exception CreateNotOpenException()
		{
			return new InvalidOperationException(SR.GetString("CommunicationObjectCannotBeUsed", new object[]
			{
				this.GetCommunicationObjectType().ToString(),
				this.state.ToString()
			}));
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x00107839 File Offset: 0x00105A39
		private Exception CreateImmutableException()
		{
			return new InvalidOperationException(SR.GetString("CommunicationObjectCannotBeModifiedInState", new object[]
			{
				this.GetCommunicationObjectType().ToString(),
				this.state.ToString()
			}));
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x00107872 File Offset: 0x00105A72
		private Exception CreateBaseClassMethodNotCalledException(string method)
		{
			return new InvalidOperationException(SR.GetString("CommunicationObjectBaseClassMethodNotCalled", new object[]
			{
				this.GetCommunicationObjectType().ToString(),
				method
			}));
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x0010789B File Offset: 0x00105A9B
		internal Exception CreateClosedException()
		{
			if (!this.closeCalled)
			{
				return this.CreateAbortedException();
			}
			return new ObjectDisposedException(this.GetCommunicationObjectType().ToString());
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x001078BC File Offset: 0x00105ABC
		internal Exception CreateFaultedException()
		{
			string @string = SR.GetString("CommunicationObjectFaulted1", new object[]
			{
				this.GetCommunicationObjectType().ToString()
			});
			return new CommunicationObjectFaultedException(@string);
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x001078EE File Offset: 0x00105AEE
		internal Exception CreateAbortedException()
		{
			return new CommunicationObjectAbortedException(SR.GetString("CommunicationObjectAborted1", new object[]
			{
				this.GetCommunicationObjectType().ToString()
			}));
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x060046B1 RID: 18097 RVA: 0x00107913 File Offset: 0x00105B13
		internal virtual string CloseActivityName
		{
			get
			{
				return SR.GetString("ActivityClose", new object[]
				{
					base.GetType().FullName
				});
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x060046B2 RID: 18098 RVA: 0x00107933 File Offset: 0x00105B33
		internal virtual string OpenActivityName
		{
			get
			{
				return SR.GetString("ActivityOpen", new object[]
				{
					base.GetType().FullName
				});
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x060046B3 RID: 18099 RVA: 0x00107953 File Offset: 0x00105B53
		internal virtual ActivityType OpenActivityType
		{
			get
			{
				return ActivityType.Open;
			}
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x00107958 File Offset: 0x00105B58
		private ServiceModelActivity CreateCloseActivity()
		{
			ServiceModelActivity serviceModelActivity = ServiceModelActivity.CreateBoundedActivity();
			if (DiagnosticUtility.ShouldUseActivity)
			{
				ServiceModelActivity.Start(serviceModelActivity, this.CloseActivityName, ActivityType.Close);
			}
			return serviceModelActivity;
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x00107984 File Offset: 0x00105B84
		internal bool DoneReceivingInCurrentState()
		{
			this.ThrowPending();
			switch (this.state)
			{
			case CommunicationState.Created:
				throw TraceUtility.ThrowHelperError(this.CreateNotOpenException(), Guid.Empty, this);
			case CommunicationState.Opening:
				throw TraceUtility.ThrowHelperError(this.CreateNotOpenException(), Guid.Empty, this);
			case CommunicationState.Opened:
				return false;
			case CommunicationState.Closing:
				return true;
			case CommunicationState.Closed:
				return true;
			case CommunicationState.Faulted:
				return true;
			default:
				throw Fx.AssertAndThrow("DoneReceivingInCurrentState: Unknown CommunicationObject.state");
			}
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x001079F4 File Offset: 0x00105BF4
		[__DynamicallyInvokable]
		public void EndClose(IAsyncResult result)
		{
			if (result is CommunicationObject.AlreadyClosedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			CommunicationObject.CloseAsyncResult.End(result);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x00107A0B File Offset: 0x00105C0B
		[__DynamicallyInvokable]
		public void EndOpen(IAsyncResult result)
		{
			CommunicationObject.OpenAsyncResult.End(result);
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x00107A14 File Offset: 0x00105C14
		[__DynamicallyInvokable]
		protected void Fault()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.state == CommunicationState.Closed || this.state == CommunicationState.Closing)
				{
					return;
				}
				if (this.state == CommunicationState.Faulted)
				{
					return;
				}
				this.state = CommunicationState.Faulted;
			}
			this.OnFaulted();
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x00107A7C File Offset: 0x00105C7C
		internal void Fault(Exception exception)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.exceptionQueue == null)
				{
					this.exceptionQueue = new CommunicationObject.ExceptionQueue(this.ThisLock);
				}
			}
			if (exception != null && DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 524298, SR.GetString("TraceCodeCommunicationObjectFaultReason"), exception, null);
			}
			this.exceptionQueue.AddException(exception);
			this.Fault();
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x00107B04 File Offset: 0x00105D04
		internal void AddPendingException(Exception exception)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.exceptionQueue == null)
				{
					this.exceptionQueue = new CommunicationObject.ExceptionQueue(this.ThisLock);
				}
			}
			this.exceptionQueue.AddException(exception);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x00107B64 File Offset: 0x00105D64
		internal Exception GetPendingException()
		{
			CommunicationState communicationState = this.state;
			CommunicationObject.ExceptionQueue exceptionQueue = this.exceptionQueue;
			if (exceptionQueue != null)
			{
				return exceptionQueue.GetException();
			}
			return null;
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x00107B8C File Offset: 0x00105D8C
		internal Exception GetTerminalException()
		{
			Exception pendingException = this.GetPendingException();
			if (pendingException != null)
			{
				return pendingException;
			}
			CommunicationState communicationState = this.state;
			if (communicationState - CommunicationState.Closing <= 1)
			{
				return new CommunicationException(SR.GetString("CommunicationObjectCloseInterrupted1", new object[]
				{
					this.GetCommunicationObjectType().ToString()
				}));
			}
			if (communicationState != CommunicationState.Faulted)
			{
				throw Fx.AssertAndThrow("GetTerminalException: Invalid CommunicationObject.state");
			}
			return this.CreateFaultedException();
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x00107BED File Offset: 0x00105DED
		[__DynamicallyInvokable]
		public void Open()
		{
			this.Open(this.DefaultOpenTimeout);
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x00107BFC File Offset: 0x00105DFC
		[__DynamicallyInvokable]
		public void Open(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", SR.GetString("SFxTimeoutOutOfRange0")));
			}
			using (ServiceModelActivity serviceModelActivity = (DiagnosticUtility.ShouldUseActivity && this.TraceOpenAndClose) ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, this.OpenActivityName, this.OpenActivityType);
				}
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.ThrowIfDisposedOrImmutable();
					this.state = CommunicationState.Opening;
				}
				bool flag2 = true;
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					this.OnOpening();
					if (!this.onOpeningCalled)
					{
						throw TraceUtility.ThrowHelperError(this.CreateBaseClassMethodNotCalledException("OnOpening"), Guid.Empty, this);
					}
					this.OnOpen(timeoutHelper.RemainingTime());
					this.OnOpened();
					if (!this.onOpenedCalled)
					{
						throw TraceUtility.ThrowHelperError(this.CreateBaseClassMethodNotCalledException("OnOpened"), Guid.Empty, this);
					}
					flag2 = false;
				}
				finally
				{
					if (flag2)
					{
						if (DiagnosticUtility.ShouldTraceWarning)
						{
							TraceUtility.TraceEvent(TraceEventType.Warning, 524293, SR.GetString("TraceCodeCommunicationObjectOpenFailed", new object[]
							{
								this.GetCommunicationObjectType().ToString()
							}), this);
						}
						this.Fault();
					}
				}
			}
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x00107D64 File Offset: 0x00105F64
		[__DynamicallyInvokable]
		protected virtual void OnClosed()
		{
			this.onClosedCalled = true;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.raisedClosed)
				{
					return;
				}
				this.raisedClosed = true;
				this.state = CommunicationState.Closed;
			}
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 524295, SR.GetString("TraceCodeCommunicationObjectClosed", new object[]
				{
					TraceUtility.CreateSourceString(this)
				}), this);
			}
			EventHandler closed = this.Closed;
			if (closed != null)
			{
				try
				{
					closed(this.eventSender, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x00107E2C File Offset: 0x0010602C
		[__DynamicallyInvokable]
		protected virtual void OnClosing()
		{
			this.onClosingCalled = true;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.raisedClosing)
				{
					return;
				}
				this.raisedClosing = true;
			}
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 524294, SR.GetString("TraceCodeCommunicationObjectClosing", new object[]
				{
					TraceUtility.CreateSourceString(this)
				}), this);
			}
			EventHandler closing = this.Closing;
			if (closing != null)
			{
				try
				{
					closing(this.eventSender, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x00107EEC File Offset: 0x001060EC
		[__DynamicallyInvokable]
		protected virtual void OnFaulted()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.raisedFaulted)
				{
					return;
				}
				this.raisedFaulted = true;
			}
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 524299, SR.GetString("TraceCodeCommunicationObjectFaulted", new object[]
				{
					this.GetCommunicationObjectType().ToString()
				}), this);
			}
			EventHandler faulted = this.Faulted;
			if (faulted != null)
			{
				try
				{
					faulted(this.eventSender, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x00107FA8 File Offset: 0x001061A8
		[__DynamicallyInvokable]
		protected virtual void OnOpened()
		{
			this.onOpenedCalled = true;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.aborted || this.state != CommunicationState.Opening)
				{
					return;
				}
				this.state = CommunicationState.Opened;
			}
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 524301, SR.GetString("TraceCodeCommunicationObjectOpened", new object[]
				{
					TraceUtility.CreateSourceString(this)
				}), this);
			}
			EventHandler opened = this.Opened;
			if (opened != null)
			{
				try
				{
					opened(this.eventSender, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x00108070 File Offset: 0x00106270
		[__DynamicallyInvokable]
		protected virtual void OnOpening()
		{
			this.onOpeningCalled = true;
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 524300, SR.GetString("TraceCodeCommunicationObjectOpening", new object[]
				{
					TraceUtility.CreateSourceString(this)
				}), this);
			}
			EventHandler opening = this.Opening;
			if (opening != null)
			{
				try
				{
					opening(this.eventSender, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x001080F8 File Offset: 0x001062F8
		internal void ThrowIfFaulted()
		{
			this.ThrowPending();
			switch (this.state)
			{
			case CommunicationState.Created:
			case CommunicationState.Opening:
			case CommunicationState.Opened:
			case CommunicationState.Closing:
			case CommunicationState.Closed:
				return;
			case CommunicationState.Faulted:
				throw TraceUtility.ThrowHelperError(this.CreateFaultedException(), Guid.Empty, this);
			default:
				throw Fx.AssertAndThrow("ThrowIfFaulted: Unknown CommunicationObject.state");
			}
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x0010814F File Offset: 0x0010634F
		internal void ThrowIfAborted()
		{
			if (this.aborted && !this.closeCalled)
			{
				throw TraceUtility.ThrowHelperError(this.CreateAbortedException(), Guid.Empty, this);
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x060046C6 RID: 18118 RVA: 0x00108173 File Offset: 0x00106373
		// (set) Token: 0x060046C7 RID: 18119 RVA: 0x0010817B File Offset: 0x0010637B
		internal bool TraceOpenAndClose
		{
			get
			{
				return this.traceOpenAndClose;
			}
			set
			{
				this.traceOpenAndClose = (value && DiagnosticUtility.ShouldUseActivity);
			}
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x00108190 File Offset: 0x00106390
		internal void ThrowIfClosed()
		{
			this.ThrowPending();
			switch (this.state)
			{
			case CommunicationState.Created:
			case CommunicationState.Opening:
			case CommunicationState.Opened:
			case CommunicationState.Closing:
				return;
			case CommunicationState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateClosedException(), Guid.Empty, this);
			case CommunicationState.Faulted:
				throw TraceUtility.ThrowHelperError(this.CreateFaultedException(), Guid.Empty, this);
			default:
				throw Fx.AssertAndThrow("ThrowIfClosed: Unknown CommunicationObject.state");
			}
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x001081F9 File Offset: 0x001063F9
		[__DynamicallyInvokable]
		protected virtual Type GetCommunicationObjectType()
		{
			return base.GetType();
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x00108204 File Offset: 0x00106404
		protected internal void ThrowIfDisposed()
		{
			this.ThrowPending();
			switch (this.state)
			{
			case CommunicationState.Created:
			case CommunicationState.Opening:
			case CommunicationState.Opened:
				return;
			case CommunicationState.Closing:
				throw TraceUtility.ThrowHelperError(this.CreateClosedException(), Guid.Empty, this);
			case CommunicationState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateClosedException(), Guid.Empty, this);
			case CommunicationState.Faulted:
				throw TraceUtility.ThrowHelperError(this.CreateFaultedException(), Guid.Empty, this);
			default:
				throw Fx.AssertAndThrow("ThrowIfDisposed: Unknown CommunicationObject.state");
			}
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x00108280 File Offset: 0x00106480
		internal void ThrowIfClosedOrOpened()
		{
			this.ThrowPending();
			switch (this.state)
			{
			case CommunicationState.Created:
			case CommunicationState.Opening:
				return;
			case CommunicationState.Opened:
				throw TraceUtility.ThrowHelperError(this.CreateImmutableException(), Guid.Empty, this);
			case CommunicationState.Closing:
				throw TraceUtility.ThrowHelperError(this.CreateImmutableException(), Guid.Empty, this);
			case CommunicationState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateClosedException(), Guid.Empty, this);
			case CommunicationState.Faulted:
				throw TraceUtility.ThrowHelperError(this.CreateFaultedException(), Guid.Empty, this);
			default:
				throw Fx.AssertAndThrow("ThrowIfClosedOrOpened: Unknown CommunicationObject.state");
			}
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x00108310 File Offset: 0x00106510
		protected internal void ThrowIfDisposedOrImmutable()
		{
			this.ThrowPending();
			switch (this.state)
			{
			case CommunicationState.Created:
				return;
			case CommunicationState.Opening:
				throw TraceUtility.ThrowHelperError(this.CreateImmutableException(), Guid.Empty, this);
			case CommunicationState.Opened:
				throw TraceUtility.ThrowHelperError(this.CreateImmutableException(), Guid.Empty, this);
			case CommunicationState.Closing:
				throw TraceUtility.ThrowHelperError(this.CreateClosedException(), Guid.Empty, this);
			case CommunicationState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateClosedException(), Guid.Empty, this);
			case CommunicationState.Faulted:
				throw TraceUtility.ThrowHelperError(this.CreateFaultedException(), Guid.Empty, this);
			default:
				throw Fx.AssertAndThrow("ThrowIfDisposedOrImmutable: Unknown CommunicationObject.state");
			}
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x001083B0 File Offset: 0x001065B0
		protected internal void ThrowIfDisposedOrNotOpen()
		{
			this.ThrowPending();
			switch (this.state)
			{
			case CommunicationState.Created:
				throw TraceUtility.ThrowHelperError(this.CreateNotOpenException(), Guid.Empty, this);
			case CommunicationState.Opening:
				throw TraceUtility.ThrowHelperError(this.CreateNotOpenException(), Guid.Empty, this);
			case CommunicationState.Opened:
				return;
			case CommunicationState.Closing:
				throw TraceUtility.ThrowHelperError(this.CreateClosedException(), Guid.Empty, this);
			case CommunicationState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateClosedException(), Guid.Empty, this);
			case CommunicationState.Faulted:
				throw TraceUtility.ThrowHelperError(this.CreateFaultedException(), Guid.Empty, this);
			default:
				throw Fx.AssertAndThrow("ThrowIfDisposedOrNotOpen: Unknown CommunicationObject.state");
			}
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x0010844F File Offset: 0x0010664F
		internal void ThrowIfNotOpened()
		{
			if (this.state == CommunicationState.Created || this.state == CommunicationState.Opening)
			{
				throw TraceUtility.ThrowHelperError(this.CreateNotOpenException(), Guid.Empty, this);
			}
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x00108474 File Offset: 0x00106674
		internal void ThrowIfClosedOrNotOpen()
		{
			this.ThrowPending();
			switch (this.state)
			{
			case CommunicationState.Created:
				throw TraceUtility.ThrowHelperError(this.CreateNotOpenException(), Guid.Empty, this);
			case CommunicationState.Opening:
				throw TraceUtility.ThrowHelperError(this.CreateNotOpenException(), Guid.Empty, this);
			case CommunicationState.Opened:
			case CommunicationState.Closing:
				return;
			case CommunicationState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateClosedException(), Guid.Empty, this);
			case CommunicationState.Faulted:
				throw TraceUtility.ThrowHelperError(this.CreateFaultedException(), Guid.Empty, this);
			default:
				throw Fx.AssertAndThrow("ThrowIfClosedOrNotOpen: Unknown CommunicationObject.state");
			}
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x00108504 File Offset: 0x00106704
		internal void ThrowPending()
		{
			CommunicationObject.ExceptionQueue exceptionQueue = this.exceptionQueue;
			if (exceptionQueue != null)
			{
				Exception exception = exceptionQueue.GetException();
				if (exception != null)
				{
					throw TraceUtility.ThrowHelperError(exception, Guid.Empty, this);
				}
			}
		}

		// Token: 0x060046D1 RID: 18129
		[__DynamicallyInvokable]
		protected abstract void OnAbort();

		// Token: 0x060046D2 RID: 18130
		[__DynamicallyInvokable]
		protected abstract void OnClose(TimeSpan timeout);

		// Token: 0x060046D3 RID: 18131
		[__DynamicallyInvokable]
		protected abstract void OnEndClose(IAsyncResult result);

		// Token: 0x060046D4 RID: 18132
		[__DynamicallyInvokable]
		protected abstract IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060046D5 RID: 18133
		[__DynamicallyInvokable]
		protected abstract void OnOpen(TimeSpan timeout);

		// Token: 0x060046D6 RID: 18134
		[__DynamicallyInvokable]
		protected abstract IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060046D7 RID: 18135
		[__DynamicallyInvokable]
		protected abstract void OnEndOpen(IAsyncResult result);

		// Token: 0x04002D90 RID: 11664
		private bool aborted;

		// Token: 0x04002D91 RID: 11665
		private bool closeCalled;

		// Token: 0x04002D92 RID: 11666
		private CommunicationObject.ExceptionQueue exceptionQueue;

		// Token: 0x04002D93 RID: 11667
		private object mutex;

		// Token: 0x04002D94 RID: 11668
		private bool onClosingCalled;

		// Token: 0x04002D95 RID: 11669
		private bool onClosedCalled;

		// Token: 0x04002D96 RID: 11670
		private bool onOpeningCalled;

		// Token: 0x04002D97 RID: 11671
		private bool onOpenedCalled;

		// Token: 0x04002D98 RID: 11672
		private bool raisedClosed;

		// Token: 0x04002D99 RID: 11673
		private bool raisedClosing;

		// Token: 0x04002D9A RID: 11674
		private bool raisedFaulted;

		// Token: 0x04002D9B RID: 11675
		private bool traceOpenAndClose;

		// Token: 0x04002D9C RID: 11676
		private object eventSender;

		// Token: 0x04002D9D RID: 11677
		private CommunicationState state;

		// Token: 0x02000CD3 RID: 3283
		private class AlreadyClosedAsyncResult : CompletedAsyncResult
		{
			// Token: 0x060079D2 RID: 31186 RVA: 0x001C6670 File Offset: 0x001C4870
			public AlreadyClosedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}
		}

		// Token: 0x02000CD4 RID: 3284
		private class ExceptionQueue
		{
			// Token: 0x060079D3 RID: 31187 RVA: 0x001C667A File Offset: 0x001C487A
			internal ExceptionQueue(object thisLock)
			{
				this.thisLock = thisLock;
			}

			// Token: 0x17001B9C RID: 7068
			// (get) Token: 0x060079D4 RID: 31188 RVA: 0x001C6694 File Offset: 0x001C4894
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x060079D5 RID: 31189 RVA: 0x001C669C File Offset: 0x001C489C
			public void AddException(Exception exception)
			{
				if (exception == null)
				{
					return;
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					this.exceptions.Enqueue(exception);
				}
			}

			// Token: 0x060079D6 RID: 31190 RVA: 0x001C66E8 File Offset: 0x001C48E8
			public Exception GetException()
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.exceptions.Count > 0)
					{
						return this.exceptions.Dequeue();
					}
				}
				return null;
			}

			// Token: 0x040045B4 RID: 17844
			private Queue<Exception> exceptions = new Queue<Exception>();

			// Token: 0x040045B5 RID: 17845
			private object thisLock;
		}

		// Token: 0x02000CD5 RID: 3285
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x060079D7 RID: 31191 RVA: 0x001C6744 File Offset: 0x001C4944
			public OpenAsyncResult(CommunicationObject communicationObject, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.communicationObject = communicationObject;
				this.timeout = new TimeoutHelper(timeout);
				base.OnCompleting = new Action<AsyncResult, Exception>(this.OnOpenCompleted);
				if (this.InvokeOpen())
				{
					base.Complete(true);
				}
			}

			// Token: 0x060079D8 RID: 31192 RVA: 0x001C6784 File Offset: 0x001C4984
			private bool InvokeOpen()
			{
				IAsyncResult asyncResult = this.communicationObject.OnBeginOpen(this.timeout.RemainingTime(), base.PrepareAsyncCompletion(CommunicationObject.OpenAsyncResult.onOpenCompletion), this);
				return asyncResult.CompletedSynchronously && CommunicationObject.OpenAsyncResult.OnOpenCompletion(asyncResult);
			}

			// Token: 0x060079D9 RID: 31193 RVA: 0x001C67C4 File Offset: 0x001C49C4
			private void NotifyOpened()
			{
				this.communicationObject.OnOpened();
				if (!this.communicationObject.onOpenedCalled)
				{
					throw TraceUtility.ThrowHelperError(this.communicationObject.CreateBaseClassMethodNotCalledException("OnOpened"), Guid.Empty, this.communicationObject);
				}
			}

			// Token: 0x060079DA RID: 31194 RVA: 0x001C6800 File Offset: 0x001C4A00
			private void OnOpenCompleted(AsyncResult result, Exception exception)
			{
				if (exception != null)
				{
					if (DiagnosticUtility.ShouldTraceWarning)
					{
						TraceUtility.TraceEvent(TraceEventType.Warning, 524293, SR.GetString("TraceCodeCommunicationObjectOpenFailed", new object[]
						{
							this.communicationObject.GetCommunicationObjectType().ToString()
						}), this, exception);
					}
					this.communicationObject.Fault();
				}
			}

			// Token: 0x060079DB RID: 31195 RVA: 0x001C6854 File Offset: 0x001C4A54
			private static bool OnOpenCompletion(IAsyncResult result)
			{
				CommunicationObject.OpenAsyncResult openAsyncResult = (CommunicationObject.OpenAsyncResult)result.AsyncState;
				openAsyncResult.communicationObject.OnEndOpen(result);
				openAsyncResult.NotifyOpened();
				return true;
			}

			// Token: 0x060079DC RID: 31196 RVA: 0x001C6880 File Offset: 0x001C4A80
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<CommunicationObject.OpenAsyncResult>(result);
			}

			// Token: 0x040045B6 RID: 17846
			private static AsyncResult.AsyncCompletion onOpenCompletion = new AsyncResult.AsyncCompletion(CommunicationObject.OpenAsyncResult.OnOpenCompletion);

			// Token: 0x040045B7 RID: 17847
			private CommunicationObject communicationObject;

			// Token: 0x040045B8 RID: 17848
			private TimeoutHelper timeout;
		}

		// Token: 0x02000CD6 RID: 3286
		private class CloseAsyncResult : TraceAsyncResult
		{
			// Token: 0x060079DE RID: 31198 RVA: 0x001C689C File Offset: 0x001C4A9C
			public CloseAsyncResult(CommunicationObject communicationObject, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.communicationObject = communicationObject;
				this.timeout = new TimeoutHelper(timeout);
				base.OnCompleting = new Action<AsyncResult, Exception>(this.OnCloseCompleted);
				if (this.InvokeClose())
				{
					base.Complete(true);
				}
			}

			// Token: 0x060079DF RID: 31199 RVA: 0x001C68DC File Offset: 0x001C4ADC
			private bool InvokeClose()
			{
				IAsyncResult asyncResult = this.communicationObject.OnBeginClose(this.timeout.RemainingTime(), base.PrepareAsyncCompletion(CommunicationObject.CloseAsyncResult.onCloseCompletion), this);
				return asyncResult.CompletedSynchronously && CommunicationObject.CloseAsyncResult.OnCloseCompletion(asyncResult);
			}

			// Token: 0x060079E0 RID: 31200 RVA: 0x001C691C File Offset: 0x001C4B1C
			private void NotifyClosed()
			{
				this.communicationObject.OnClosed();
				if (!this.communicationObject.onClosedCalled)
				{
					throw TraceUtility.ThrowHelperError(this.communicationObject.CreateBaseClassMethodNotCalledException("OnClosed"), Guid.Empty, this.communicationObject);
				}
			}

			// Token: 0x060079E1 RID: 31201 RVA: 0x001C6958 File Offset: 0x001C4B58
			private void OnCloseCompleted(AsyncResult result, Exception exception)
			{
				if (exception != null)
				{
					if (DiagnosticUtility.ShouldTraceWarning)
					{
						TraceUtility.TraceEvent(TraceEventType.Warning, 524292, SR.GetString("TraceCodeCommunicationObjectCloseFailed", new object[]
						{
							this.communicationObject.GetCommunicationObjectType().ToString()
						}), this, exception);
					}
					this.communicationObject.Abort();
				}
			}

			// Token: 0x060079E2 RID: 31202 RVA: 0x001C69AC File Offset: 0x001C4BAC
			private static bool OnCloseCompletion(IAsyncResult result)
			{
				CommunicationObject.CloseAsyncResult closeAsyncResult = (CommunicationObject.CloseAsyncResult)result.AsyncState;
				closeAsyncResult.communicationObject.OnEndClose(result);
				closeAsyncResult.NotifyClosed();
				return true;
			}

			// Token: 0x060079E3 RID: 31203 RVA: 0x001C69D8 File Offset: 0x001C4BD8
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<CommunicationObject.CloseAsyncResult>(result);
			}

			// Token: 0x040045B9 RID: 17849
			private static AsyncResult.AsyncCompletion onCloseCompletion = new AsyncResult.AsyncCompletion(CommunicationObject.CloseAsyncResult.OnCloseCompletion);

			// Token: 0x040045BA RID: 17850
			private CommunicationObject communicationObject;

			// Token: 0x040045BB RID: 17851
			private TimeoutHelper timeout;
		}
	}
}
