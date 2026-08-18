using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006B6 RID: 1718
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ClientChannelSinkStack : IClientChannelSinkStack, IClientResponseChannelSinkStack
	{
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000D3E49 File Offset: 0x000D2E49
		public ClientChannelSinkStack()
		{
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000D3E51 File Offset: 0x000D2E51
		public ClientChannelSinkStack(IMessageSink replySink)
		{
			this._replySink = replySink;
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000D3E60 File Offset: 0x000D2E60
		public void Push(IClientChannelSink sink, object state)
		{
			this._stack = new ClientChannelSinkStack.SinkStack
			{
				PrevStack = this._stack,
				Sink = sink,
				State = state
			};
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000D3E94 File Offset: 0x000D2E94
		public object Pop(IClientChannelSink sink)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopOnEmptySinkStack"));
			}
			while (this._stack.Sink != sink)
			{
				this._stack = this._stack.PrevStack;
				if (this._stack == null)
				{
					break;
				}
			}
			if (this._stack.Sink == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopFromSinkStackWithoutPush"));
			}
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			return state;
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000D3F1C File Offset: 0x000D2F1C
		public void AsyncProcessResponse(ITransportHeaders headers, Stream stream)
		{
			if (this._replySink != null)
			{
				if (this._stack == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
				}
				IClientChannelSink sink = this._stack.Sink;
				object state = this._stack.State;
				this._stack = this._stack.PrevStack;
				sink.AsyncProcessResponse(this, state, headers, stream);
			}
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000D3F7C File Offset: 0x000D2F7C
		public void DispatchReplyMessage(IMessage msg)
		{
			if (this._replySink != null)
			{
				this._replySink.SyncProcessMessage(msg);
			}
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000D3F93 File Offset: 0x000D2F93
		public void DispatchException(Exception e)
		{
			this.DispatchReplyMessage(new ReturnMessage(e, null));
		}

		// Token: 0x04001F9A RID: 8090
		private ClientChannelSinkStack.SinkStack _stack;

		// Token: 0x04001F9B RID: 8091
		private IMessageSink _replySink;

		// Token: 0x020006B7 RID: 1719
		private class SinkStack
		{
			// Token: 0x04001F9C RID: 8092
			public ClientChannelSinkStack.SinkStack PrevStack;

			// Token: 0x04001F9D RID: 8093
			public IClientChannelSink Sink;

			// Token: 0x04001F9E RID: 8094
			public object State;
		}
	}
}
