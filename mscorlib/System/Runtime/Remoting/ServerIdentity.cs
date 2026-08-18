using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000771 RID: 1905
	internal class ServerIdentity : Identity
	{
		// Token: 0x060043F3 RID: 17395 RVA: 0x000E8820 File Offset: 0x000E7820
		internal Type GetLastCalledType(string newTypeName)
		{
			ServerIdentity.LastCalledType lastCalledType = this._lastCalledType;
			if (lastCalledType == null)
			{
				return null;
			}
			string typeName = lastCalledType.typeName;
			Type type = lastCalledType.type;
			if (typeName == null || type == null)
			{
				return null;
			}
			if (typeName.Equals(newTypeName))
			{
				return type;
			}
			return null;
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x000E885C File Offset: 0x000E785C
		internal void SetLastCalledType(string newTypeName, Type newType)
		{
			this._lastCalledType = new ServerIdentity.LastCalledType
			{
				typeName = newTypeName,
				type = newType
			};
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x000E8884 File Offset: 0x000E7884
		internal void SetHandle()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.ReliableEnter(this, ref flag);
				if (!this._srvIdentityHandle.IsAllocated)
				{
					this._srvIdentityHandle = new GCHandle(this, GCHandleType.Normal);
				}
				else
				{
					this._srvIdentityHandle.Target = this;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x000E88E4 File Offset: 0x000E78E4
		internal void ResetHandle()
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.ReliableEnter(this, ref flag);
				this._srvIdentityHandle.Target = null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x000E8928 File Offset: 0x000E7928
		internal GCHandle GetHandle()
		{
			return this._srvIdentityHandle;
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000E8930 File Offset: 0x000E7930
		internal ServerIdentity(MarshalByRefObject obj, Context serverCtx) : base(obj is ContextBoundObject)
		{
			if (obj != null)
			{
				if (!RemotingServices.IsTransparentProxy(obj))
				{
					this._srvType = obj.GetType();
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(obj);
					this._srvType = realProxy.GetProxiedType();
				}
			}
			this._srvCtx = serverCtx;
			this._serverObjectChain = null;
			this._stackBuilderSink = null;
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x000E898D File Offset: 0x000E798D
		internal ServerIdentity(MarshalByRefObject obj, Context serverCtx, string uri) : this(obj, serverCtx)
		{
			base.SetOrCreateURI(uri, true);
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x000E899F File Offset: 0x000E799F
		internal Context ServerContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._srvCtx;
			}
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x000E89A7 File Offset: 0x000E79A7
		internal void SetSingleCallObjectMode()
		{
			this._flags |= 512;
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x000E89BB File Offset: 0x000E79BB
		internal void SetSingletonObjectMode()
		{
			this._flags |= 1024;
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x000E89CF File Offset: 0x000E79CF
		internal bool IsSingleCall()
		{
			return (this._flags & 512) != 0;
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x000E89E3 File Offset: 0x000E79E3
		internal bool IsSingleton()
		{
			return (this._flags & 1024) != 0;
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x000E89F8 File Offset: 0x000E79F8
		internal IMessageSink GetServerObjectChain(out MarshalByRefObject obj)
		{
			obj = null;
			if (!this.IsSingleCall())
			{
				if (this._serverObjectChain == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.ReliableEnter(this, ref flag);
						if (this._serverObjectChain == null)
						{
							MarshalByRefObject tporObject = base.TPOrObject;
							this._serverObjectChain = this._srvCtx.CreateServerObjectChain(tporObject);
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(this);
						}
					}
				}
				return this._serverObjectChain;
			}
			MarshalByRefObject marshalByRefObject;
			IMessageSink messageSink;
			if (this._tpOrObject != null && this._firstCallDispatched == 0 && Interlocked.CompareExchange(ref this._firstCallDispatched, 1, 0) == 0)
			{
				marshalByRefObject = (MarshalByRefObject)this._tpOrObject;
				messageSink = this._serverObjectChain;
				if (messageSink == null)
				{
					messageSink = this._srvCtx.CreateServerObjectChain(marshalByRefObject);
				}
			}
			else
			{
				marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._srvType, true);
				string objectUri = RemotingServices.GetObjectUri(marshalByRefObject);
				if (objectUri != null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CtorCantMarshal"), new object[]
					{
						base.URI
					}));
				}
				if (!RemotingServices.IsTransparentProxy(marshalByRefObject))
				{
					marshalByRefObject.__RaceSetServerIdentity(this);
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(marshalByRefObject);
					realProxy.IdentityObject = this;
				}
				messageSink = this._srvCtx.CreateServerObjectChain(marshalByRefObject);
			}
			obj = marshalByRefObject;
			return messageSink;
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x000E8B38 File Offset: 0x000E7B38
		// (set) Token: 0x06004401 RID: 17409 RVA: 0x000E8B40 File Offset: 0x000E7B40
		internal Type ServerType
		{
			get
			{
				return this._srvType;
			}
			set
			{
				this._srvType = value;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x000E8B49 File Offset: 0x000E7B49
		// (set) Token: 0x06004403 RID: 17411 RVA: 0x000E8B51 File Offset: 0x000E7B51
		internal bool MarshaledAsSpecificType
		{
			get
			{
				return this._bMarshaledAsSpecificType;
			}
			set
			{
				this._bMarshaledAsSpecificType = value;
			}
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000E8B5C File Offset: 0x000E7B5C
		internal IMessageSink RaceSetServerObjectChain(IMessageSink serverObjectChain)
		{
			if (this._serverObjectChain == null)
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.ReliableEnter(this, ref flag);
					if (this._serverObjectChain == null)
					{
						this._serverObjectChain = serverObjectChain;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}
			return this._serverObjectChain;
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000E8BB4 File Offset: 0x000E7BB4
		internal bool AddServerSideDynamicProperty(IDynamicProperty prop)
		{
			if (this._dphSrv == null)
			{
				DynamicPropertyHolder dphSrv = new DynamicPropertyHolder();
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.ReliableEnter(this, ref flag);
					if (this._dphSrv == null)
					{
						this._dphSrv = dphSrv;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}
			return this._dphSrv.AddDynamicProperty(prop);
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000E8C18 File Offset: 0x000E7C18
		internal bool RemoveServerSideDynamicProperty(string name)
		{
			if (this._dphSrv == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_PropNotFound"));
			}
			return this._dphSrv.RemoveDynamicProperty(name);
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004407 RID: 17415 RVA: 0x000E8C3E File Offset: 0x000E7C3E
		internal ArrayWithSize ServerSideDynamicSinks
		{
			get
			{
				if (this._dphSrv == null)
				{
					return null;
				}
				return this._dphSrv.DynamicSinks;
			}
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000E8C55 File Offset: 0x000E7C55
		internal override void AssertValid()
		{
			if (base.TPOrObject != null)
			{
				RemotingServices.IsTransparentProxy(base.TPOrObject);
			}
		}

		// Token: 0x04002207 RID: 8711
		internal Context _srvCtx;

		// Token: 0x04002208 RID: 8712
		internal IMessageSink _serverObjectChain;

		// Token: 0x04002209 RID: 8713
		internal StackBuilderSink _stackBuilderSink;

		// Token: 0x0400220A RID: 8714
		internal DynamicPropertyHolder _dphSrv;

		// Token: 0x0400220B RID: 8715
		internal Type _srvType;

		// Token: 0x0400220C RID: 8716
		private ServerIdentity.LastCalledType _lastCalledType;

		// Token: 0x0400220D RID: 8717
		internal bool _bMarshaledAsSpecificType;

		// Token: 0x0400220E RID: 8718
		internal int _firstCallDispatched;

		// Token: 0x0400220F RID: 8719
		internal GCHandle _srvIdentityHandle;

		// Token: 0x02000772 RID: 1906
		private class LastCalledType
		{
			// Token: 0x04002210 RID: 8720
			public string typeName;

			// Token: 0x04002211 RID: 8721
			public Type type;
		}
	}
}
