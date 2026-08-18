using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200076C RID: 1900
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class RemotingSurrogateSelector : ISurrogateSelector
	{
		// Token: 0x060043D8 RID: 17368 RVA: 0x000E7F67 File Offset: 0x000E6F67
		public RemotingSurrogateSelector()
		{
			this._messageSurrogate = new MessageSurrogate(this);
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060043DA RID: 17370 RVA: 0x000E7F9A File Offset: 0x000E6F9A
		// (set) Token: 0x060043D9 RID: 17369 RVA: 0x000E7F91 File Offset: 0x000E6F91
		public MessageSurrogateFilter Filter
		{
			get
			{
				return this._filter;
			}
			set
			{
				this._filter = value;
			}
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x000E7FA4 File Offset: 0x000E6FA4
		public void SetRootObject(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			this._rootObj = obj;
			SoapMessageSurrogate soapMessageSurrogate = this._messageSurrogate as SoapMessageSurrogate;
			if (soapMessageSurrogate != null)
			{
				soapMessageSurrogate.SetRootObject(this._rootObj);
			}
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x000E7FE1 File Offset: 0x000E6FE1
		public object GetRootObject()
		{
			return this._rootObj;
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x000E7FE9 File Offset: 0x000E6FE9
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void ChainSelector(ISurrogateSelector selector)
		{
			this._next = selector;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x000E7FF4 File Offset: 0x000E6FF4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector ssout)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsMarshalByRef)
			{
				ssout = this;
				return this._remotingSurrogate;
			}
			if (RemotingSurrogateSelector.s_IMethodCallMessageType.IsAssignableFrom(type) || RemotingSurrogateSelector.s_IMethodReturnMessageType.IsAssignableFrom(type))
			{
				ssout = this;
				return this._messageSurrogate;
			}
			if (RemotingSurrogateSelector.s_ObjRefType.IsAssignableFrom(type))
			{
				ssout = this;
				return this._objRefSurrogate;
			}
			if (this._next != null)
			{
				return this._next.GetSurrogate(type, context, out ssout);
			}
			ssout = null;
			return null;
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x000E8077 File Offset: 0x000E7077
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual ISurrogateSelector GetNextSelector()
		{
			return this._next;
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x000E807F File Offset: 0x000E707F
		public virtual void UseSoapFormat()
		{
			this._messageSurrogate = new SoapMessageSurrogate(this);
			((SoapMessageSurrogate)this._messageSurrogate).SetRootObject(this._rootObj);
		}

		// Token: 0x040021F2 RID: 8690
		private static Type s_IMethodCallMessageType = typeof(IMethodCallMessage);

		// Token: 0x040021F3 RID: 8691
		private static Type s_IMethodReturnMessageType = typeof(IMethodReturnMessage);

		// Token: 0x040021F4 RID: 8692
		private static Type s_ObjRefType = typeof(ObjRef);

		// Token: 0x040021F5 RID: 8693
		private object _rootObj;

		// Token: 0x040021F6 RID: 8694
		private ISurrogateSelector _next;

		// Token: 0x040021F7 RID: 8695
		private RemotingSurrogate _remotingSurrogate = new RemotingSurrogate();

		// Token: 0x040021F8 RID: 8696
		private ObjRefSurrogate _objRefSurrogate = new ObjRefSurrogate();

		// Token: 0x040021F9 RID: 8697
		private ISerializationSurrogate _messageSurrogate;

		// Token: 0x040021FA RID: 8698
		private MessageSurrogateFilter _filter;
	}
}
