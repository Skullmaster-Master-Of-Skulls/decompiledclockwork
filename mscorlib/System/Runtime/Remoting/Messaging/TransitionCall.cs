using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000722 RID: 1826
	[Serializable]
	internal class TransitionCall : IMessage, IInternalMessage, IMessageSink, ISerializable
	{
		// Token: 0x06004171 RID: 16753 RVA: 0x000DEF14 File Offset: 0x000DDF14
		internal TransitionCall(IntPtr targetCtxID, CrossContextDelegate deleg)
		{
			this._sourceCtxID = Thread.CurrentContext.InternalContextID;
			this._targetCtxID = targetCtxID;
			this._delegate = deleg;
			this._targetDomainID = 0;
			this._eeData = IntPtr.Zero;
			this._srvID = new ServerIdentity(null, Thread.GetContextInternal(this._targetCtxID));
			this._ID = this._srvID;
			this._ID.RaceSetChannelSink(CrossContextChannel.MessageSink);
			this._srvID.RaceSetServerObjectChain(this);
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000DEF98 File Offset: 0x000DDF98
		internal TransitionCall(IntPtr targetCtxID, IntPtr eeData, int targetDomainID)
		{
			this._sourceCtxID = Thread.CurrentContext.InternalContextID;
			this._targetCtxID = targetCtxID;
			this._delegate = null;
			this._targetDomainID = targetDomainID;
			this._eeData = eeData;
			this._srvID = null;
			this._ID = new Identity("TransitionCallURI", null);
			CrossAppDomainData data = new CrossAppDomainData(this._targetCtxID, this._targetDomainID, Identity.ProcessGuid);
			string text;
			IMessageSink channelSink = CrossAppDomainChannel.AppDomainChannel.CreateMessageSink(null, data, out text);
			this._ID.RaceSetChannelSink(channelSink);
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000DF024 File Offset: 0x000DE024
		internal TransitionCall(SerializationInfo info, StreamingContext context)
		{
			if (info == null || context.State != StreamingContextStates.CrossAppDomain)
			{
				throw new ArgumentNullException("info");
			}
			this._props = (IDictionary)info.GetValue("props", typeof(IDictionary));
			this._delegate = (CrossContextDelegate)info.GetValue("delegate", typeof(CrossContextDelegate));
			this._sourceCtxID = (IntPtr)info.GetValue("sourceCtxID", typeof(IntPtr));
			this._targetCtxID = (IntPtr)info.GetValue("targetCtxID", typeof(IntPtr));
			this._eeData = (IntPtr)info.GetValue("eeData", typeof(IntPtr));
			this._targetDomainID = info.GetInt32("targetDomainID");
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06004174 RID: 16756 RVA: 0x000DF104 File Offset: 0x000DE104
		public IDictionary Properties
		{
			get
			{
				if (this._props == null)
				{
					lock (this)
					{
						if (this._props == null)
						{
							this._props = new Hashtable();
						}
					}
				}
				return this._props;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000DF154 File Offset: 0x000DE154
		// (set) Token: 0x06004176 RID: 16758 RVA: 0x000DF1D4 File Offset: 0x000DE1D4
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			get
			{
				if (this._targetDomainID != 0 && this._srvID == null)
				{
					lock (this)
					{
						if (Thread.GetContextInternal(this._targetCtxID) == null)
						{
							Context defaultContext = Context.DefaultContext;
						}
						this._srvID = new ServerIdentity(null, Thread.GetContextInternal(this._targetCtxID));
						this._srvID.RaceSetServerObjectChain(this);
					}
				}
				return this._srvID;
			}
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000DF1E5 File Offset: 0x000DE1E5
		// (set) Token: 0x06004178 RID: 16760 RVA: 0x000DF1ED File Offset: 0x000DE1ED
		Identity IInternalMessage.IdentityObject
		{
			get
			{
				return this._ID;
			}
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000DF1FE File Offset: 0x000DE1FE
		void IInternalMessage.SetURI(string uri)
		{
			throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000DF20F File Offset: 0x000DE20F
		void IInternalMessage.SetCallContext(LogicalCallContext callContext)
		{
			throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x000DF220 File Offset: 0x000DE220
		bool IInternalMessage.HasProperties()
		{
			throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000DF234 File Offset: 0x000DE234
		public IMessage SyncProcessMessage(IMessage msg)
		{
			try
			{
				LogicalCallContext oldcctx = Message.PropagateCallContextFromMessageToThread(msg);
				if (this._delegate != null)
				{
					this._delegate();
				}
				else
				{
					CallBackHelper @object = new CallBackHelper(this._eeData, true, this._targetDomainID);
					CrossContextDelegate crossContextDelegate = new CrossContextDelegate(@object.Func);
					crossContextDelegate();
				}
				Message.PropagateCallContextFromThreadToMessage(msg, oldcctx);
			}
			catch (Exception e)
			{
				ReturnMessage returnMessage = new ReturnMessage(e, new ErrorMessage());
				returnMessage.SetLogicalCallContext((LogicalCallContext)msg.Properties[Message.CallContextKey]);
				return returnMessage;
			}
			return this;
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000DF2D4 File Offset: 0x000DE2D4
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMessage msg2 = this.SyncProcessMessage(msg);
			replySink.SyncProcessMessage(msg2);
			return null;
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x000DF2F2 File Offset: 0x000DE2F2
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000DF2F8 File Offset: 0x000DE2F8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null || context.State != StreamingContextStates.CrossAppDomain)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("props", this._props, typeof(IDictionary));
			info.AddValue("delegate", this._delegate, typeof(CrossContextDelegate));
			info.AddValue("sourceCtxID", this._sourceCtxID);
			info.AddValue("targetCtxID", this._targetCtxID);
			info.AddValue("targetDomainID", this._targetDomainID);
			info.AddValue("eeData", this._eeData);
		}

		// Token: 0x040020E5 RID: 8421
		private IDictionary _props;

		// Token: 0x040020E6 RID: 8422
		private IntPtr _sourceCtxID;

		// Token: 0x040020E7 RID: 8423
		private IntPtr _targetCtxID;

		// Token: 0x040020E8 RID: 8424
		private int _targetDomainID;

		// Token: 0x040020E9 RID: 8425
		private ServerIdentity _srvID;

		// Token: 0x040020EA RID: 8426
		private Identity _ID;

		// Token: 0x040020EB RID: 8427
		private CrossContextDelegate _delegate;

		// Token: 0x040020EC RID: 8428
		private IntPtr _eeData;
	}
}
