using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006BA RID: 1722
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ServerChannelSinkStack : IServerChannelSinkStack, IServerResponseChannelSinkStack
	{
		// Token: 0x06003E08 RID: 15880 RVA: 0x000D3FAC File Offset: 0x000D2FAC
		public void Push(IServerChannelSink sink, object state)
		{
			this._stack = new ServerChannelSinkStack.SinkStack
			{
				PrevStack = this._stack,
				Sink = sink,
				State = state
			};
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000D3FE0 File Offset: 0x000D2FE0
		public object Pop(IServerChannelSink sink)
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

		// Token: 0x06003E0A RID: 15882 RVA: 0x000D4068 File Offset: 0x000D3068
		public void Store(IServerChannelSink sink, object state)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnEmptySinkStack"));
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
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnSinkStackWithoutPush"));
			}
			this._rememberedStack = new ServerChannelSinkStack.SinkStack
			{
				PrevStack = this._rememberedStack,
				Sink = sink,
				State = state
			};
			this.Pop(sink);
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x000D4100 File Offset: 0x000D3100
		public void StoreAndDispatch(IServerChannelSink sink, object state)
		{
			this.Store(sink, state);
			this.FlipRememberedStack();
			CrossContextChannel.DoAsyncDispatch(this._asyncMsg, null);
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x000D4120 File Offset: 0x000D3120
		private void FlipRememberedStack()
		{
			if (this._stack != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallFRSWhenStackEmtpy"));
			}
			while (this._rememberedStack != null)
			{
				this._stack = new ServerChannelSinkStack.SinkStack
				{
					PrevStack = this._stack,
					Sink = this._rememberedStack.Sink,
					State = this._rememberedStack.State
				};
				this._rememberedStack = this._rememberedStack.PrevStack;
			}
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x000D419C File Offset: 0x000D319C
		public void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
			}
			IServerChannelSink sink = this._stack.Sink;
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			sink.AsyncProcessResponse(this, state, msg, headers, stream);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000D41F8 File Offset: 0x000D31F8
		public Stream GetResponseStream(IMessage msg, ITransportHeaders headers)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallGetResponseStreamWhenStackEmpty"));
			}
			IServerChannelSink sink = this._stack.Sink;
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			Stream responseStream = sink.GetResponseStream(this, state, msg, headers);
			this.Push(sink, state);
			return responseStream;
		}

		// Token: 0x17000A53 RID: 2643
		// (set) Token: 0x06003E0F RID: 15887 RVA: 0x000D425A File Offset: 0x000D325A
		internal object ServerObject
		{
			set
			{
				this._serverObject = value;
			}
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000D4264 File Offset: 0x000D3264
		public void ServerCallback(IAsyncResult ar)
		{
			if (this._asyncEnd != null)
			{
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this._asyncEnd);
				MethodInfo mi = (MethodInfo)this._msg.MethodBase;
				RemotingMethodCachedData reflectionCachedData2 = InternalRemotingServices.GetReflectionCachedData(mi);
				ParameterInfo[] parameters = reflectionCachedData.Parameters;
				object[] array = new object[parameters.Length];
				array[parameters.Length - 1] = ar;
				object[] args = this._msg.Args;
				AsyncMessageHelper.GetOutArgs(reflectionCachedData2.Parameters, args, array);
				StackBuilderSink stackBuilderSink = new StackBuilderSink(this._serverObject);
				object[] array2;
				object ret = stackBuilderSink.PrivateProcessMessage(this._asyncEnd.MethodHandle, Message.CoerceArgs(this._asyncEnd, array, parameters), this._serverObject, 0, false, out array2);
				if (array2 != null)
				{
					array2 = ArgMapper.ExpandAsyncEndArgsToSyncArgs(reflectionCachedData2, array2);
				}
				stackBuilderSink.CopyNonByrefOutArgsFromOriginalArgs(reflectionCachedData2, args, ref array2);
				IMessage msg = new ReturnMessage(ret, array2, this._msg.ArgCount, CallContext.GetLogicalCallContext(), this._msg);
				this.AsyncProcessResponse(msg, null, null);
			}
		}

		// Token: 0x04001F9F RID: 8095
		private ServerChannelSinkStack.SinkStack _stack;

		// Token: 0x04001FA0 RID: 8096
		private ServerChannelSinkStack.SinkStack _rememberedStack;

		// Token: 0x04001FA1 RID: 8097
		private IMessage _asyncMsg;

		// Token: 0x04001FA2 RID: 8098
		private MethodInfo _asyncEnd;

		// Token: 0x04001FA3 RID: 8099
		private object _serverObject;

		// Token: 0x04001FA4 RID: 8100
		private IMethodCallMessage _msg;

		// Token: 0x020006BB RID: 1723
		private class SinkStack
		{
			// Token: 0x04001FA5 RID: 8101
			public ServerChannelSinkStack.SinkStack PrevStack;

			// Token: 0x04001FA6 RID: 8102
			public IServerChannelSink Sink;

			// Token: 0x04001FA7 RID: 8103
			public object State;
		}
	}
}
