using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200079D RID: 1949
	internal class SynchronizedClientContextSink : InternalSink, IMessageSink
	{
		// Token: 0x0600457E RID: 17790 RVA: 0x000EC67A File Offset: 0x000EB67A
		internal SynchronizedClientContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
		{
			this._property = prop;
			this._nextSink = nextSink;
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x000EC690 File Offset: 0x000EB690
		~SynchronizedClientContextSink()
		{
			this._property.Dispose();
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x000EC6C4 File Offset: 0x000EB6C4
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message;
			if (this._property.IsReEntrant)
			{
				this._property.HandleThreadExit();
				message = this._nextSink.SyncProcessMessage(reqMsg);
				this._property.HandleThreadReEntry();
			}
			else
			{
				LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
				string text = logicalCallContext.RemotingData.LogicalCallID;
				bool flag = false;
				if (text == null)
				{
					text = Identity.GetNewLogicalCallID();
					logicalCallContext.RemotingData.LogicalCallID = text;
					flag = true;
				}
				bool flag2 = false;
				if (this._property.SyncCallOutLCID == null)
				{
					this._property.SyncCallOutLCID = text;
					flag2 = true;
				}
				message = this._nextSink.SyncProcessMessage(reqMsg);
				if (flag2)
				{
					this._property.SyncCallOutLCID = null;
					if (flag)
					{
						LogicalCallContext logicalCallContext2 = (LogicalCallContext)message.Properties[Message.CallContextKey];
						logicalCallContext2.RemotingData.LogicalCallID = null;
					}
				}
			}
			return message;
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x000EC7A8 File Offset: 0x000EB7A8
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			if (!this._property.IsReEntrant)
			{
				LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
				string newLogicalCallID = Identity.GetNewLogicalCallID();
				logicalCallContext.RemotingData.LogicalCallID = newLogicalCallID;
				this._property.AsyncCallOutLCIDList.Add(newLogicalCallID);
			}
			SynchronizedClientContextSink.AsyncReplySink replySink2 = new SynchronizedClientContextSink.AsyncReplySink(replySink, this._property);
			return this._nextSink.AsyncProcessMessage(reqMsg, replySink2);
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004582 RID: 17794 RVA: 0x000EC81A File Offset: 0x000EB81A
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x0400228D RID: 8845
		internal IMessageSink _nextSink;

		// Token: 0x0400228E RID: 8846
		internal SynchronizationAttribute _property;

		// Token: 0x0200079E RID: 1950
		internal class AsyncReplySink : IMessageSink
		{
			// Token: 0x06004583 RID: 17795 RVA: 0x000EC822 File Offset: 0x000EB822
			internal AsyncReplySink(IMessageSink nextSink, SynchronizationAttribute prop)
			{
				this._nextSink = nextSink;
				this._property = prop;
			}

			// Token: 0x06004584 RID: 17796 RVA: 0x000EC838 File Offset: 0x000EB838
			public virtual IMessage SyncProcessMessage(IMessage reqMsg)
			{
				WorkItem workItem = new WorkItem(reqMsg, this._nextSink, null);
				this._property.HandleWorkRequest(workItem);
				if (!this._property.IsReEntrant)
				{
					this._property.AsyncCallOutLCIDList.Remove(((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]).RemotingData.LogicalCallID);
				}
				return workItem.ReplyMessage;
			}

			// Token: 0x06004585 RID: 17797 RVA: 0x000EC8A1 File Offset: 0x000EB8A1
			public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000C37 RID: 3127
			// (get) Token: 0x06004586 RID: 17798 RVA: 0x000EC8A8 File Offset: 0x000EB8A8
			public IMessageSink NextSink
			{
				get
				{
					return this._nextSink;
				}
			}

			// Token: 0x0400228F RID: 8847
			internal IMessageSink _nextSink;

			// Token: 0x04002290 RID: 8848
			internal SynchronizationAttribute _property;
		}
	}
}
