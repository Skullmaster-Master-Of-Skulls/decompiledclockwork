using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020006A6 RID: 1702
	[ComVisible(true)]
	public class AsyncResult : IAsyncResult, IMessageSink
	{
		// Token: 0x06003D75 RID: 15733 RVA: 0x000D23F9 File Offset: 0x000D13F9
		internal AsyncResult(Message m)
		{
			m.GetAsyncBeginInfo(out this._acbd, out this._asyncState);
			this._asyncDelegate = (Delegate)m.GetThisPtr();
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x000D2424 File Offset: 0x000D1424
		public virtual bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06003D77 RID: 15735 RVA: 0x000D242C File Offset: 0x000D142C
		public virtual object AsyncDelegate
		{
			get
			{
				return this._asyncDelegate;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x000D2434 File Offset: 0x000D1434
		public virtual object AsyncState
		{
			get
			{
				return this._asyncState;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x000D243C File Offset: 0x000D143C
		public virtual bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06003D7A RID: 15738 RVA: 0x000D243F File Offset: 0x000D143F
		// (set) Token: 0x06003D7B RID: 15739 RVA: 0x000D2447 File Offset: 0x000D1447
		public bool EndInvokeCalled
		{
			get
			{
				return this._endInvokeCalled;
			}
			set
			{
				this._endInvokeCalled = value;
			}
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x000D2450 File Offset: 0x000D1450
		private void FaultInWaitHandle()
		{
			lock (this)
			{
				if (this._AsyncWaitHandle == null)
				{
					this._AsyncWaitHandle = new ManualResetEvent(this._isCompleted);
				}
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x000D2498 File Offset: 0x000D1498
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				this.FaultInWaitHandle();
				return this._AsyncWaitHandle;
			}
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000D24A6 File Offset: 0x000D14A6
		public virtual void SetMessageCtrl(IMessageCtrl mc)
		{
			this._mc = mc;
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x000D24B0 File Offset: 0x000D14B0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			if (msg == null)
			{
				this._replyMsg = new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_NullMessage")), new ErrorMessage());
			}
			else if (!(msg is IMethodReturnMessage))
			{
				this._replyMsg = new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_Message_BadType")), new ErrorMessage());
			}
			else
			{
				this._replyMsg = msg;
			}
			lock (this)
			{
				this._isCompleted = true;
				if (this._AsyncWaitHandle != null)
				{
					this._AsyncWaitHandle.Set();
				}
			}
			if (this._acbd != null)
			{
				this._acbd(this);
			}
			return null;
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x000D2564 File Offset: 0x000D1564
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000D2575 File Offset: 0x000D1575
		public IMessageSink NextSink
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
			get
			{
				return null;
			}
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x000D2578 File Offset: 0x000D1578
		public virtual IMessage GetReplyMessage()
		{
			return this._replyMsg;
		}

		// Token: 0x04001F70 RID: 8048
		private IMessageCtrl _mc;

		// Token: 0x04001F71 RID: 8049
		private AsyncCallback _acbd;

		// Token: 0x04001F72 RID: 8050
		private IMessage _replyMsg;

		// Token: 0x04001F73 RID: 8051
		private bool _isCompleted;

		// Token: 0x04001F74 RID: 8052
		private bool _endInvokeCalled;

		// Token: 0x04001F75 RID: 8053
		private ManualResetEvent _AsyncWaitHandle;

		// Token: 0x04001F76 RID: 8054
		private Delegate _asyncDelegate;

		// Token: 0x04001F77 RID: 8055
		private object _asyncState;
	}
}
