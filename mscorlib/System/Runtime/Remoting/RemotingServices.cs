using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000769 RID: 1897
	[ComVisible(true)]
	public sealed class RemotingServices
	{
		// Token: 0x06004367 RID: 17255 RVA: 0x000E6484 File Offset: 0x000E5484
		static RemotingServices()
		{
			CodeAccessPermission.AssertAllPossible();
			RemotingServices.s_RemotingInfrastructurePermission = new SecurityPermission(SecurityPermissionFlag.Infrastructure);
			RemotingServices.s_MscorlibAssembly = typeof(RemotingServices).Assembly;
			RemotingServices.s_FieldGetterMB = null;
			RemotingServices.s_FieldSetterMB = null;
			RemotingServices.s_bRemoteActivationConfigured = false;
			RemotingServices.s_bRegisteredWellKnownChannels = false;
			RemotingServices.s_bInProcessOfRegisteringWellKnownChannels = false;
			RemotingServices.s_delayLoadChannelLock = new object();
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000E64E1 File Offset: 0x000E54E1
		private RemotingServices()
		{
		}

		// Token: 0x06004369 RID: 17257
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsTransparentProxy(object proxy);

		// Token: 0x0600436A RID: 17258 RVA: 0x000E64EC File Offset: 0x000E54EC
		public static bool IsObjectOutOfContext(object tp)
		{
			if (!RemotingServices.IsTransparentProxy(tp))
			{
				return false;
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			ServerIdentity serverIdentity = identityObject as ServerIdentity;
			return serverIdentity == null || !(realProxy is RemotingProxy) || Thread.CurrentContext != serverIdentity.ServerContext;
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x000E6535 File Offset: 0x000E5535
		public static bool IsObjectOutOfAppDomain(object tp)
		{
			return RemotingServices.IsClientProxy(tp);
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x000E6540 File Offset: 0x000E5540
		internal static bool IsClientProxy(object obj)
		{
			MarshalByRefObject marshalByRefObject = obj as MarshalByRefObject;
			if (marshalByRefObject == null)
			{
				return false;
			}
			bool result = false;
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(marshalByRefObject, out flag);
			if (identity != null && !(identity is ServerIdentity))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x000E6574 File Offset: 0x000E5574
		internal static bool IsObjectOutOfProcess(object tp)
		{
			if (!RemotingServices.IsTransparentProxy(tp))
			{
				return false;
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject is ServerIdentity)
			{
				return false;
			}
			if (identityObject != null)
			{
				ObjRef objectRef = identityObject.ObjectRef;
				return objectRef == null || !objectRef.IsFromThisProcess();
			}
			return true;
		}

		// Token: 0x0600436E RID: 17262
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern RealProxy GetRealProxy(object proxy);

		// Token: 0x0600436F RID: 17263
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateTransparentProxy(RealProxy rp, RuntimeType typeToProxy, IntPtr stub, object stubData);

		// Token: 0x06004370 RID: 17264 RVA: 0x000E65C0 File Offset: 0x000E55C0
		internal static object CreateTransparentProxy(RealProxy rp, Type typeToProxy, IntPtr stub, object stubData)
		{
			RuntimeType runtimeType = typeToProxy as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					"typeToProxy"
				}));
			}
			return RemotingServices.CreateTransparentProxy(rp, runtimeType, stub, stubData);
		}

		// Token: 0x06004371 RID: 17265
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarshalByRefObject AllocateUninitializedObject(RuntimeType objectType);

		// Token: 0x06004372 RID: 17266
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CallDefaultCtor(object o);

		// Token: 0x06004373 RID: 17267 RVA: 0x000E660C File Offset: 0x000E560C
		internal static MarshalByRefObject AllocateUninitializedObject(Type objectType)
		{
			RuntimeType runtimeType = objectType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					"objectType"
				}));
			}
			return RemotingServices.AllocateUninitializedObject(runtimeType);
		}

		// Token: 0x06004374 RID: 17268
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MarshalByRefObject AllocateInitializedObject(RuntimeType objectType);

		// Token: 0x06004375 RID: 17269 RVA: 0x000E6654 File Offset: 0x000E5654
		internal static MarshalByRefObject AllocateInitializedObject(Type objectType)
		{
			RuntimeType runtimeType = objectType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					"objectType"
				}));
			}
			return RemotingServices.AllocateInitializedObject(runtimeType);
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x000E669C File Offset: 0x000E569C
		internal static bool RegisterWellKnownChannels()
		{
			if (!RemotingServices.s_bRegisteredWellKnownChannels)
			{
				bool flag = false;
				object configLock = Thread.GetDomain().RemotingData.ConfigLock;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.ReliableEnter(configLock, ref flag);
					if (!RemotingServices.s_bRegisteredWellKnownChannels && !RemotingServices.s_bInProcessOfRegisteringWellKnownChannels)
					{
						RemotingServices.s_bInProcessOfRegisteringWellKnownChannels = true;
						CrossAppDomainChannel.RegisterChannel();
						RemotingServices.s_bRegisteredWellKnownChannels = true;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(configLock);
					}
				}
			}
			return true;
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x000E670C File Offset: 0x000E570C
		internal static void InternalSetRemoteActivationConfigured()
		{
			if (!RemotingServices.s_bRemoteActivationConfigured)
			{
				RemotingServices.nSetRemoteActivationConfigured();
				RemotingServices.s_bRemoteActivationConfigured = true;
			}
		}

		// Token: 0x06004378 RID: 17272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void nSetRemoteActivationConfigured();

		// Token: 0x06004379 RID: 17273 RVA: 0x000E6720 File Offset: 0x000E5720
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static string GetSessionIdForMethodMessage(IMethodMessage msg)
		{
			return msg.Uri;
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x000E6728 File Offset: 0x000E5728
		public static object GetLifetimeService(MarshalByRefObject obj)
		{
			if (obj != null)
			{
				return obj.GetLifetimeService();
			}
			return null;
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x000E6738 File Offset: 0x000E5738
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static string GetObjectUri(MarshalByRefObject obj)
		{
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			if (identity != null)
			{
				return identity.URI;
			}
			return null;
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x000E675C File Offset: 0x000E575C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
		{
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			Identity identity2 = identity as ServerIdentity;
			if (identity != null && identity2 == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__ObjectNeedsToBeLocal"));
			}
			if (identity != null && identity.URI != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
			}
			if (identity == null)
			{
				Context defaultContext = Thread.GetDomain().GetDefaultContext();
				ServerIdentity serverIdentity = new ServerIdentity(obj, defaultContext, uri);
				identity = obj.__RaceSetServerIdentity(serverIdentity);
				if (identity != serverIdentity)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
				}
			}
			else
			{
				identity.SetOrCreateURI(uri, true);
			}
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x000E67EE File Offset: 0x000E57EE
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj)
		{
			return RemotingServices.MarshalInternal(Obj, null, null);
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x000E67F8 File Offset: 0x000E57F8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
		{
			return RemotingServices.MarshalInternal(Obj, URI, null);
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x000E6802 File Offset: 0x000E5802
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType);
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x000E680C File Offset: 0x000E580C
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
		{
			return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, true);
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000E6818 File Offset: 0x000E5818
		internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData)
		{
			if (Obj == null)
			{
				return null;
			}
			ObjRef objRef = null;
			Identity orCreateIdentity = RemotingServices.GetOrCreateIdentity(Obj, ObjURI);
			if (RequestedType != null)
			{
				ServerIdentity serverIdentity = orCreateIdentity as ServerIdentity;
				if (serverIdentity != null)
				{
					serverIdentity.ServerType = RequestedType;
					serverIdentity.MarshaledAsSpecificType = true;
				}
			}
			objRef = orCreateIdentity.ObjectRef;
			if (objRef == null)
			{
				if (RemotingServices.IsTransparentProxy(Obj))
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
					objRef = realProxy.CreateObjRef(RequestedType);
				}
				else
				{
					objRef = Obj.CreateObjRef(RequestedType);
				}
				objRef = orCreateIdentity.RaceSetObjRef(objRef);
			}
			ServerIdentity serverIdentity2 = orCreateIdentity as ServerIdentity;
			if (serverIdentity2 != null)
			{
				MarshalByRefObject marshalByRefObject = null;
				serverIdentity2.GetServerObjectChain(out marshalByRefObject);
				Lease lease = orCreateIdentity.Lease;
				if (lease != null)
				{
					lock (lease)
					{
						if (lease.CurrentState == LeaseState.Expired)
						{
							lease.ActivateLease();
						}
						else
						{
							lease.RenewInternal(orCreateIdentity.Lease.InitialLeaseTime);
						}
					}
				}
				if (updateChannelData && objRef.ChannelInfo != null)
				{
					object[] currentChannelData = ChannelServices.CurrentChannelData;
					if (!(Obj is AppDomain))
					{
						objRef.ChannelInfo.ChannelData = currentChannelData;
					}
					else
					{
						int num = currentChannelData.Length;
						object[] array = new object[num];
						Array.Copy(currentChannelData, array, num);
						for (int i = 0; i < num; i++)
						{
							if (!(array[i] is CrossAppDomainData))
							{
								array[i] = null;
							}
						}
						objRef.ChannelInfo.ChannelData = array;
					}
				}
			}
			TrackingServices.MarshaledObject(Obj, objRef);
			return objRef;
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x000E6974 File Offset: 0x000E5974
		private static Identity GetOrCreateIdentity(MarshalByRefObject Obj, string ObjURI)
		{
			Identity identity;
			if (RemotingServices.IsTransparentProxy(Obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(Obj);
				identity = realProxy.IdentityObject;
				if (identity == null)
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, 2);
					identity.RaceSetTransparentProxy(Obj);
				}
				ServerIdentity serverIdentity = identity as ServerIdentity;
				if (serverIdentity != null)
				{
					identity = IdentityHolder.FindOrCreateServerIdentity(serverIdentity.TPOrObject, ObjURI, 2);
					if (ObjURI != null && ObjURI != Identity.RemoveAppNameOrAppGuidIfNecessary(identity.ObjURI))
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_URIExists"));
					}
				}
				else if (ObjURI != null && ObjURI != identity.ObjURI)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_URIToProxy"));
				}
			}
			else
			{
				identity = IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, 2);
			}
			return identity;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000E6A1C File Offset: 0x000E5A1C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			ObjRef objRef = RemotingServices.MarshalInternal((MarshalByRefObject)obj, null, null);
			objRef.GetObjectData(info, context);
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000E6A5B File Offset: 0x000E5A5B
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static object Unmarshal(ObjRef objectRef)
		{
			return RemotingServices.InternalUnmarshal(objectRef, null, false);
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000E6A65 File Offset: 0x000E5A65
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static object Unmarshal(ObjRef objectRef, bool fRefine)
		{
			return RemotingServices.InternalUnmarshal(objectRef, null, fRefine);
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x000E6A6F File Offset: 0x000E5A6F
		[ComVisible(true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static object Connect(Type classToProxy, string url)
		{
			return RemotingServices.Unmarshal(classToProxy, url, null);
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x000E6A79 File Offset: 0x000E5A79
		[ComVisible(true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static object Connect(Type classToProxy, string url, object data)
		{
			return RemotingServices.Unmarshal(classToProxy, url, data);
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x000E6A83 File Offset: 0x000E5A83
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool Disconnect(MarshalByRefObject obj)
		{
			return RemotingServices.Disconnect(obj, true);
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x000E6A8C File Offset: 0x000E5A8C
		internal static bool Disconnect(MarshalByRefObject obj, bool bResetURI)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			bool result = false;
			if (identity != null)
			{
				if (!(identity is ServerIdentity))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_CantDisconnectClientProxy"));
				}
				if (identity.IsInIDTable())
				{
					IdentityHolder.RemoveIdentity(identity.URI, bResetURI);
					result = true;
				}
				TrackingServices.DisconnectedObject(obj);
			}
			return result;
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x000E6AEC File Offset: 0x000E5AEC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
		{
			IMessageSink result = null;
			if (RemotingServices.IsObjectOutOfContext(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity identityObject = realProxy.IdentityObject;
				if (identityObject != null)
				{
					result = identityObject.EnvoyChain;
				}
			}
			return result;
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x000E6B1C File Offset: 0x000E5B1C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
		{
			ObjRef result = null;
			if (!RemotingServices.IsTransparentProxy(obj))
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_BadType"));
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(obj);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject != null)
			{
				result = identityObject.ObjectRef;
			}
			return result;
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x000E6B5C File Offset: 0x000E5B5C
		internal static object Unmarshal(Type classToProxy, string url)
		{
			return RemotingServices.Unmarshal(classToProxy, url, null);
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x000E6B68 File Offset: 0x000E5B68
		internal static object Unmarshal(Type classToProxy, string url, object data)
		{
			if (classToProxy == null)
			{
				throw new ArgumentNullException("classToProxy");
			}
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_NotRemotableByReference"));
			}
			Identity identity = IdentityHolder.ResolveIdentity(url);
			if (identity == null || identity.ChannelSink == null || identity.EnvoyChain == null)
			{
				IMessageSink messageSink = null;
				IMessageSink envoySink = null;
				string text = RemotingServices.CreateEnvoyAndChannelSinks(url, data, out messageSink, out envoySink);
				if (messageSink == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Connect_CantCreateChannelSink"), new object[]
					{
						url
					}));
				}
				if (text == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
				}
				identity = IdentityHolder.FindOrCreateIdentity(text, url, null);
				RemotingServices.SetEnvoyAndChannelSinks(identity, messageSink, envoySink);
			}
			return RemotingServices.GetOrCreateProxy(classToProxy, identity);
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x000E6C3A File Offset: 0x000E5C3A
		internal static object Wrap(ContextBoundObject obj)
		{
			return RemotingServices.Wrap(obj, null, true);
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x000E6C44 File Offset: 0x000E5C44
		internal static object Wrap(ContextBoundObject obj, object proxy, bool fCreateSinks)
		{
			if (obj != null && !RemotingServices.IsTransparentProxy(obj))
			{
				Identity idObj;
				if (proxy != null)
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(proxy);
					if (realProxy.UnwrappedServerObject == null)
					{
						realProxy.AttachServerHelper(obj);
					}
					idObj = MarshalByRefObject.GetIdentity(obj);
				}
				else
				{
					idObj = IdentityHolder.FindOrCreateServerIdentity(obj, null, 0);
				}
				proxy = RemotingServices.GetOrCreateProxy(idObj, proxy, true);
				RemotingServices.GetRealProxy(proxy).Wrap();
				if (fCreateSinks)
				{
					IMessageSink chnlSink = null;
					IMessageSink envoySink = null;
					RemotingServices.CreateEnvoyAndChannelSinks((MarshalByRefObject)proxy, null, out chnlSink, out envoySink);
					RemotingServices.SetEnvoyAndChannelSinks(idObj, chnlSink, envoySink);
				}
				RealProxy realProxy2 = RemotingServices.GetRealProxy(proxy);
				if (realProxy2.UnwrappedServerObject == null)
				{
					realProxy2.AttachServerHelper(obj);
				}
				return proxy;
			}
			return obj;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x000E6CDC File Offset: 0x000E5CDC
		internal static string GetObjectUriFromFullUri(string fullUri)
		{
			if (fullUri == null)
			{
				return null;
			}
			int num = fullUri.LastIndexOf('/');
			if (num == -1)
			{
				return fullUri;
			}
			return fullUri.Substring(num + 1);
		}

		// Token: 0x06004391 RID: 17297
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object Unwrap(ContextBoundObject obj);

		// Token: 0x06004392 RID: 17298
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object AlwaysUnwrap(ContextBoundObject obj);

		// Token: 0x06004393 RID: 17299 RVA: 0x000E6D08 File Offset: 0x000E5D08
		internal static object InternalUnmarshal(ObjRef objectRef, object proxy, bool fRefine)
		{
			Context currentContext = Thread.CurrentContext;
			if (!ObjRef.IsWellFormed(objectRef))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_BadObjRef"), new object[]
				{
					"Unmarshal"
				}));
			}
			object obj;
			Identity identity;
			if (objectRef.IsWellKnown())
			{
				obj = RemotingServices.Unmarshal(typeof(MarshalByRefObject), objectRef.URI);
				identity = IdentityHolder.ResolveIdentity(objectRef.URI);
				if (identity.ObjectRef == null)
				{
					identity.RaceSetObjRef(objectRef);
				}
				return obj;
			}
			identity = IdentityHolder.FindOrCreateIdentity(objectRef.URI, null, objectRef);
			Context currentContext2 = Thread.CurrentContext;
			ServerIdentity serverIdentity = identity as ServerIdentity;
			if (serverIdentity != null)
			{
				Context currentContext3 = Thread.CurrentContext;
				if (!serverIdentity.IsContextBound)
				{
					if (proxy != null)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_ProxySameAppDomain"), new object[0]));
					}
					obj = serverIdentity.TPOrObject;
				}
				else
				{
					IMessageSink chnlSink = null;
					IMessageSink envoySink = null;
					RemotingServices.CreateEnvoyAndChannelSinks(serverIdentity.TPOrObject, null, out chnlSink, out envoySink);
					RemotingServices.SetEnvoyAndChannelSinks(identity, chnlSink, envoySink);
					obj = RemotingServices.GetOrCreateProxy(identity, proxy, true);
				}
			}
			else
			{
				IMessageSink chnlSink2 = null;
				IMessageSink envoySink2 = null;
				if (!objectRef.IsObjRefLite())
				{
					RemotingServices.CreateEnvoyAndChannelSinks(null, objectRef, out chnlSink2, out envoySink2);
				}
				else
				{
					RemotingServices.CreateEnvoyAndChannelSinks(objectRef.URI, null, out chnlSink2, out envoySink2);
				}
				RemotingServices.SetEnvoyAndChannelSinks(identity, chnlSink2, envoySink2);
				if (objectRef.HasProxyAttribute())
				{
					fRefine = true;
				}
				obj = RemotingServices.GetOrCreateProxy(identity, proxy, fRefine);
			}
			TrackingServices.UnmarshaledObject(obj, objectRef);
			return obj;
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x000E6E64 File Offset: 0x000E5E64
		private static object GetOrCreateProxy(Identity idObj, object proxy, bool fRefine)
		{
			if (proxy == null)
			{
				ServerIdentity serverIdentity = idObj as ServerIdentity;
				Type type;
				if (serverIdentity != null)
				{
					type = serverIdentity.ServerType;
				}
				else
				{
					IRemotingTypeInfo typeInfo = idObj.ObjectRef.TypeInfo;
					type = null;
					if ((typeInfo is TypeInfo && !fRefine) || typeInfo == null)
					{
						type = typeof(MarshalByRefObject);
					}
					else
					{
						string typeName = typeInfo.TypeName;
						if (typeName != null)
						{
							string name = null;
							string assemblyName = null;
							TypeInfo.ParseTypeAndAssembly(typeName, out name, out assemblyName);
							Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(assemblyName);
							if (assembly != null)
							{
								type = assembly.GetType(name, false, false);
							}
						}
					}
					if (type == null)
					{
						throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), new object[]
						{
							typeInfo.TypeName
						}));
					}
				}
				proxy = RemotingServices.SetOrCreateProxy(idObj, type, null);
			}
			else
			{
				proxy = RemotingServices.SetOrCreateProxy(idObj, null, proxy);
			}
			if (proxy == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_UnexpectedNullTP"), new object[0]));
			}
			return proxy;
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x000E6F58 File Offset: 0x000E5F58
		private static object GetOrCreateProxy(Type classToProxy, Identity idObj)
		{
			object obj = idObj.TPOrObject;
			if (obj == null)
			{
				obj = RemotingServices.SetOrCreateProxy(idObj, classToProxy, null);
			}
			ServerIdentity serverIdentity = idObj as ServerIdentity;
			if (serverIdentity != null)
			{
				Type serverType = serverIdentity.ServerType;
				if (!classToProxy.IsAssignableFrom(serverType))
				{
					throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), new object[]
					{
						serverType.FullName,
						classToProxy.FullName
					}));
				}
			}
			return obj;
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x000E6FC8 File Offset: 0x000E5FC8
		private static MarshalByRefObject SetOrCreateProxy(Identity idObj, Type classToProxy, object proxy)
		{
			RealProxy realProxy = null;
			if (proxy == null)
			{
				ServerIdentity serverIdentity = idObj as ServerIdentity;
				if (idObj.ObjectRef != null)
				{
					ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(classToProxy);
					realProxy = proxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, null, null);
				}
				if (realProxy == null)
				{
					ProxyAttribute defaultProxyAttribute = ActivationServices.DefaultProxyAttribute;
					realProxy = defaultProxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, null, (serverIdentity == null) ? null : serverIdentity.ServerContext);
				}
			}
			else
			{
				realProxy = RemotingServices.GetRealProxy(proxy);
			}
			realProxy.IdentityObject = idObj;
			proxy = realProxy.GetTransparentProxy();
			proxy = idObj.RaceSetTransparentProxy(proxy);
			return (MarshalByRefObject)proxy;
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x000E704C File Offset: 0x000E604C
		private static bool AreChannelDataElementsNull(object[] channelData)
		{
			foreach (object obj in channelData)
			{
				if (obj != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x000E7078 File Offset: 0x000E6078
		internal static void CreateEnvoyAndChannelSinks(MarshalByRefObject tpOrObject, ObjRef objectRef, out IMessageSink chnlSink, out IMessageSink envoySink)
		{
			chnlSink = null;
			envoySink = null;
			if (objectRef == null)
			{
				chnlSink = ChannelServices.GetCrossContextChannelSink();
				envoySink = Thread.CurrentContext.CreateEnvoyChain(tpOrObject);
				return;
			}
			object[] channelData = objectRef.ChannelInfo.ChannelData;
			if (channelData != null && !RemotingServices.AreChannelDataElementsNull(channelData))
			{
				for (int i = 0; i < channelData.Length; i++)
				{
					chnlSink = ChannelServices.CreateMessageSink(channelData[i]);
					if (chnlSink != null)
					{
						break;
					}
				}
				if (chnlSink == null)
				{
					lock (RemotingServices.s_delayLoadChannelLock)
					{
						for (int j = 0; j < channelData.Length; j++)
						{
							chnlSink = ChannelServices.CreateMessageSink(channelData[j]);
							if (chnlSink != null)
							{
								break;
							}
						}
						if (chnlSink == null)
						{
							foreach (object data in channelData)
							{
								string text;
								chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(null, data, out text);
								if (chnlSink != null)
								{
									break;
								}
							}
						}
					}
				}
			}
			if (objectRef.EnvoyInfo != null && objectRef.EnvoyInfo.EnvoySinks != null)
			{
				envoySink = objectRef.EnvoyInfo.EnvoySinks;
				return;
			}
			envoySink = EnvoyTerminatorSink.MessageSink;
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x000E717C File Offset: 0x000E617C
		internal static string CreateEnvoyAndChannelSinks(string url, object data, out IMessageSink chnlSink, out IMessageSink envoySink)
		{
			string result = RemotingServices.CreateChannelSink(url, data, out chnlSink);
			envoySink = EnvoyTerminatorSink.MessageSink;
			return result;
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x000E719C File Offset: 0x000E619C
		private static string CreateChannelSink(string url, object data, out IMessageSink chnlSink)
		{
			string result = null;
			chnlSink = ChannelServices.CreateMessageSink(url, data, out result);
			if (chnlSink == null)
			{
				lock (RemotingServices.s_delayLoadChannelLock)
				{
					chnlSink = ChannelServices.CreateMessageSink(url, data, out result);
					if (chnlSink == null)
					{
						chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(url, data, out result);
					}
				}
			}
			return result;
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000E71FC File Offset: 0x000E61FC
		internal static void SetEnvoyAndChannelSinks(Identity idObj, IMessageSink chnlSink, IMessageSink envoySink)
		{
			if (idObj.ChannelSink == null && chnlSink != null)
			{
				idObj.RaceSetChannelSink(chnlSink);
			}
			if (idObj.EnvoyChain != null)
			{
				return;
			}
			if (envoySink != null)
			{
				idObj.RaceSetEnvoyChain(envoySink);
				return;
			}
			throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_FailEnvoySink"), new object[0]));
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x000E7250 File Offset: 0x000E6250
		private static bool CheckCast(RealProxy rp, Type castType)
		{
			bool result = false;
			if (castType == typeof(object))
			{
				return true;
			}
			if (!castType.IsInterface && !castType.IsMarshalByRef)
			{
				return false;
			}
			if (castType != typeof(IObjectReference))
			{
				IRemotingTypeInfo remotingTypeInfo = rp as IRemotingTypeInfo;
				if (remotingTypeInfo != null)
				{
					result = remotingTypeInfo.CanCastTo(castType, rp.GetTransparentProxy());
				}
				else
				{
					Identity identityObject = rp.IdentityObject;
					if (identityObject != null)
					{
						ObjRef objectRef = identityObject.ObjectRef;
						if (objectRef != null)
						{
							remotingTypeInfo = objectRef.TypeInfo;
							if (remotingTypeInfo != null)
							{
								result = remotingTypeInfo.CanCastTo(castType, rp.GetTransparentProxy());
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000E72D4 File Offset: 0x000E62D4
		internal static bool ProxyCheckCast(RealProxy rp, Type castType)
		{
			return RemotingServices.CheckCast(rp, castType);
		}

		// Token: 0x0600439E RID: 17310
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CheckCast(object objToExpand, Type type);

		// Token: 0x0600439F RID: 17311 RVA: 0x000E72E0 File Offset: 0x000E62E0
		internal static GCHandle CreateDelegateInvocation(WaitCallback waitDelegate, object state)
		{
			return GCHandle.Alloc(new object[]
			{
				waitDelegate,
				state
			});
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x000E7302 File Offset: 0x000E6302
		internal static void DisposeDelegateInvocation(GCHandle delegateCallToken)
		{
			delegateCallToken.Free();
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000E730C File Offset: 0x000E630C
		internal static object CreateProxyForDomain(int appDomainId, IntPtr defCtxID)
		{
			ObjRef objectRef = RemotingServices.CreateDataForDomain(appDomainId, defCtxID);
			return (AppDomain)RemotingServices.Unmarshal(objectRef);
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x000E7330 File Offset: 0x000E6330
		internal static object CreateDataForDomainCallback(object[] args)
		{
			RemotingServices.RegisterWellKnownChannels();
			ObjRef objRef = RemotingServices.MarshalInternal(Thread.CurrentContext.AppDomain, null, null, false);
			ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(Thread.CurrentContext.AppDomain);
			serverIdentity.SetHandle();
			objRef.SetServerIdentity(serverIdentity.GetHandle());
			objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
			return objRef;
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000E7390 File Offset: 0x000E6390
		internal static ObjRef CreateDataForDomain(int appDomainId, IntPtr defCtxID)
		{
			RemotingServices.RegisterWellKnownChannels();
			InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(RemotingServices.CreateDataForDomainCallback);
			return (ObjRef)Thread.CurrentThread.InternalCrossContextCallback(null, defCtxID, appDomainId, ftnToCall, null);
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000E73C4 File Offset: 0x000E63C4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static MethodBase GetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			return RemotingServices.InternalGetMethodBaseFromMethodMessage(msg);
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x000E73DC File Offset: 0x000E63DC
		internal static MethodBase InternalGetMethodBaseFromMethodMessage(IMethodMessage msg)
		{
			if (msg == null)
			{
				return null;
			}
			Type type = RemotingServices.InternalGetTypeFromQualifiedTypeName(msg.TypeName);
			if (type == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), new object[]
				{
					msg.TypeName
				}));
			}
			Type[] signature = (Type[])msg.MethodSignature;
			return RemotingServices.GetMethodBase(msg, type, signature);
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x000E743C File Offset: 0x000E643C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool IsMethodOverloaded(IMethodMessage msg)
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(msg.MethodBase);
			return reflectionCachedData.IsOverloaded();
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x000E745C File Offset: 0x000E645C
		private static MethodBase GetMethodBase(IMethodMessage msg, Type t, Type[] signature)
		{
			MethodBase result = null;
			if (msg is IConstructionCallMessage || msg is IConstructionReturnMessage)
			{
				if (signature == null)
				{
					RuntimeType runtimeType = t as RuntimeType;
					ConstructorInfo[] constructors;
					if (runtimeType == null)
					{
						constructors = t.GetConstructors();
					}
					else
					{
						constructors = runtimeType.GetConstructors();
					}
					if (1 != constructors.Length)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Remoting_AmbiguousCTOR"));
					}
					result = constructors[0];
				}
				else
				{
					RuntimeType runtimeType2 = t as RuntimeType;
					if (runtimeType2 == null)
					{
						result = t.GetConstructor(signature);
					}
					else
					{
						result = runtimeType2.GetConstructor(signature);
					}
				}
			}
			else if (msg is IMethodCallMessage || msg is IMethodReturnMessage)
			{
				if (signature == null)
				{
					RuntimeType runtimeType3 = t as RuntimeType;
					if (runtimeType3 == null)
					{
						result = t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					}
					else
					{
						result = runtimeType3.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					}
				}
				else
				{
					RuntimeType runtimeType4 = t as RuntimeType;
					if (runtimeType4 == null)
					{
						result = t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
					}
					else
					{
						result = runtimeType4.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, signature, null);
					}
				}
			}
			return result;
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000E7550 File Offset: 0x000E6550
		internal static bool IsMethodAllowedRemotely(MethodBase method)
		{
			if (RemotingServices.s_FieldGetterMB == null || RemotingServices.s_FieldSetterMB == null || RemotingServices.s_IsInstanceOfTypeMB == null || RemotingServices.s_InvokeMemberMB == null || RemotingServices.s_CanCastToXmlTypeMB == null)
			{
				CodeAccessPermission.AssertAllPossible();
				if (RemotingServices.s_FieldGetterMB == null)
				{
					RemotingServices.s_FieldGetterMB = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_FieldSetterMB == null)
				{
					RemotingServices.s_FieldSetterMB = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_IsInstanceOfTypeMB == null)
				{
					RemotingServices.s_IsInstanceOfTypeMB = typeof(MarshalByRefObject).GetMethod("IsInstanceOfType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_CanCastToXmlTypeMB == null)
				{
					RemotingServices.s_CanCastToXmlTypeMB = typeof(MarshalByRefObject).GetMethod("CanCastToXmlType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				if (RemotingServices.s_InvokeMemberMB == null)
				{
					RemotingServices.s_InvokeMemberMB = typeof(MarshalByRefObject).GetMethod("InvokeMember", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
			}
			return method == RemotingServices.s_FieldGetterMB || method == RemotingServices.s_FieldSetterMB || method == RemotingServices.s_IsInstanceOfTypeMB || method == RemotingServices.s_InvokeMemberMB || method == RemotingServices.s_CanCastToXmlTypeMB;
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x000E765C File Offset: 0x000E665C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static bool IsOneWay(MethodBase method)
		{
			if (method == null)
			{
				return false;
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(method);
			return reflectionCachedData.IsOneWayMethod();
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000E767C File Offset: 0x000E667C
		internal static bool FindAsyncMethodVersion(MethodInfo method, out MethodInfo beginMethod, out MethodInfo endMethod)
		{
			beginMethod = null;
			endMethod = null;
			string value = "Begin" + method.Name;
			string value2 = "End" + method.Name;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			Type typeFromHandle = typeof(IAsyncResult);
			Type returnType = method.ReturnType;
			ParameterInfo[] parameters = method.GetParameters();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (parameterInfo.IsOut)
				{
					arrayList2.Add(parameterInfo);
				}
				else if (parameterInfo.ParameterType.IsByRef)
				{
					arrayList.Add(parameterInfo);
					arrayList2.Add(parameterInfo);
				}
				else
				{
					arrayList.Add(parameterInfo);
				}
			}
			arrayList.Add(typeof(AsyncCallback));
			arrayList.Add(typeof(object));
			arrayList2.Add(typeof(IAsyncResult));
			Type declaringType = method.DeclaringType;
			MethodInfo[] methods = declaringType.GetMethods();
			foreach (MethodInfo methodInfo in methods)
			{
				ParameterInfo[] parameters2 = methodInfo.GetParameters();
				if (methodInfo.Name.Equals(value) && methodInfo.ReturnType == typeFromHandle && RemotingServices.CompareParameterList(arrayList, parameters2))
				{
					beginMethod = methodInfo;
				}
				else if (methodInfo.Name.Equals(value2) && methodInfo.ReturnType == returnType && RemotingServices.CompareParameterList(arrayList2, parameters2))
				{
					endMethod = methodInfo;
				}
			}
			return beginMethod != null && endMethod != null;
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000E7800 File Offset: 0x000E6800
		private static bool CompareParameterList(ArrayList params1, ParameterInfo[] params2)
		{
			if (params1.Count != params2.Length)
			{
				return false;
			}
			int num = 0;
			foreach (object obj in params1)
			{
				ParameterInfo parameterInfo = params2[num];
				ParameterInfo parameterInfo2 = obj as ParameterInfo;
				if (parameterInfo2 != null)
				{
					if (parameterInfo2.ParameterType != parameterInfo.ParameterType || parameterInfo2.IsIn != parameterInfo.IsIn || parameterInfo2.IsOut != parameterInfo.IsOut)
					{
						return false;
					}
				}
				else if ((Type)obj != parameterInfo.ParameterType && parameterInfo.IsIn)
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000E78C0 File Offset: 0x000E68C0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static Type GetServerTypeForUri(string URI)
		{
			Type result = null;
			if (URI != null)
			{
				ServerIdentity serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity(URI);
				if (serverIdentity == null)
				{
					result = RemotingConfigHandler.GetServerTypeForUri(URI);
				}
				else
				{
					result = serverIdentity.ServerType;
				}
			}
			return result;
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x000E78F2 File Offset: 0x000E68F2
		internal static void DomainUnloaded(int domainID)
		{
			IdentityHolder.FlushIdentityTable();
			CrossAppDomainSink.DomainUnloaded(domainID);
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x000E7900 File Offset: 0x000E6900
		internal static IntPtr GetServerContextForProxy(object tp)
		{
			ObjRef objRef = null;
			bool flag;
			int num;
			return RemotingServices.GetServerContextForProxy(tp, out objRef, out flag, out num);
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x000E791C File Offset: 0x000E691C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static int GetServerDomainIdForProxy(object tp)
		{
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			return identityObject.ObjectRef.GetServerDomainId();
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x000E7944 File Offset: 0x000E6944
		internal static void GetServerContextAndDomainIdForProxy(object tp, out IntPtr contextId, out int domainId)
		{
			ObjRef objRef;
			bool flag;
			contextId = RemotingServices.GetServerContextForProxy(tp, out objRef, out flag, out domainId);
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000E7964 File Offset: 0x000E6964
		private static IntPtr GetServerContextForProxy(object tp, out ObjRef objRef, out bool bSameDomain, out int domainId)
		{
			IntPtr result = IntPtr.Zero;
			objRef = null;
			bSameDomain = false;
			domainId = 0;
			if (RemotingServices.IsTransparentProxy(tp))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(tp);
				Identity identityObject = realProxy.IdentityObject;
				if (identityObject != null)
				{
					ServerIdentity serverIdentity = identityObject as ServerIdentity;
					if (serverIdentity != null)
					{
						bSameDomain = true;
						result = serverIdentity.ServerContext.InternalContextID;
						domainId = Thread.GetDomain().GetId();
					}
					else
					{
						objRef = identityObject.ObjectRef;
						if (objRef != null)
						{
							result = objRef.GetServerContext(out domainId);
						}
						else
						{
							result = IntPtr.Zero;
						}
					}
				}
				else
				{
					result = Context.DefaultContext.InternalContextID;
				}
			}
			return result;
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x000E79EC File Offset: 0x000E69EC
		internal static Context GetServerContext(MarshalByRefObject obj)
		{
			Context result = null;
			if (!RemotingServices.IsTransparentProxy(obj) && obj is ContextBoundObject)
			{
				result = Thread.CurrentContext;
			}
			else
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				Identity identityObject = realProxy.IdentityObject;
				ServerIdentity serverIdentity = identityObject as ServerIdentity;
				if (serverIdentity != null)
				{
					result = serverIdentity.ServerContext;
				}
			}
			return result;
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x000E7A34 File Offset: 0x000E6A34
		private static object GetType(object tp)
		{
			Type result = null;
			RealProxy realProxy = RemotingServices.GetRealProxy(tp);
			Identity identityObject = realProxy.IdentityObject;
			if (identityObject != null && identityObject.ObjectRef != null && identityObject.ObjectRef.TypeInfo != null)
			{
				IRemotingTypeInfo typeInfo = identityObject.ObjectRef.TypeInfo;
				string typeName = typeInfo.TypeName;
				if (typeName != null)
				{
					result = RemotingServices.InternalGetTypeFromQualifiedTypeName(typeName);
				}
			}
			return result;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x000E7A8C File Offset: 0x000E6A8C
		internal static byte[] MarshalToBuffer(object o)
		{
			MemoryStream memoryStream = new MemoryStream();
			RemotingSurrogateSelector surrogateSelector = new RemotingSurrogateSelector();
			new BinaryFormatter
			{
				SurrogateSelector = surrogateSelector,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.Serialize(memoryStream, o, null, false);
			return memoryStream.GetBuffer();
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x000E7AD0 File Offset: 0x000E6AD0
		internal static object UnmarshalFromBuffer(byte[] b)
		{
			MemoryStream serializationStream = new MemoryStream(b);
			return new BinaryFormatter
			{
				AssemblyFormat = FormatterAssemblyStyle.Simple,
				SurrogateSelector = null,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.Deserialize(serializationStream, null, false);
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x000E7B10 File Offset: 0x000E6B10
		internal static object UnmarshalReturnMessageFromBuffer(byte[] b, IMethodCallMessage msg)
		{
			MemoryStream serializationStream = new MemoryStream(b);
			return new BinaryFormatter
			{
				SurrogateSelector = null,
				Context = new StreamingContext(StreamingContextStates.Other)
			}.DeserializeMethodResponse(serializationStream, null, msg);
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x000E7B4C File Offset: 0x000E6B4C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			RealProxy realProxy = RemotingServices.GetRealProxy(target);
			if (realProxy is RemotingProxy && !realProxy.DoContextsMatch())
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_WrongContext"));
			}
			StackBuilderSink stackBuilderSink = new StackBuilderSink(target);
			return (IMethodReturnMessage)stackBuilderSink.SyncProcessMessage(reqMsg, 0, true);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x000E7BA8 File Offset: 0x000E6BA8
		internal static string DetermineDefaultQualifiedTypeName(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string str = null;
			string str2 = null;
			if (SoapServices.GetXmlTypeForInteropType(type, out str, out str2))
			{
				return "soap:" + str + ", " + str2;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x000E7BEC File Offset: 0x000E6BEC
		internal static string GetDefaultQualifiedTypeName(Type type)
		{
			RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(type);
			return reflectionCachedData.QualifiedTypeName;
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x000E7C08 File Offset: 0x000E6C08
		internal static string InternalGetClrTypeNameFromQualifiedTypeName(string qualifiedTypeName)
		{
			if (qualifiedTypeName.Length > 4 && string.CompareOrdinal(qualifiedTypeName, 0, "clr:", 0, 4) == 0)
			{
				return qualifiedTypeName.Substring(4);
			}
			return null;
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x000E7C39 File Offset: 0x000E6C39
		private static int IsSoapType(string qualifiedTypeName)
		{
			if (qualifiedTypeName.Length > 5 && string.CompareOrdinal(qualifiedTypeName, 0, "soap:", 0, 5) == 0)
			{
				return qualifiedTypeName.IndexOf(',', 5);
			}
			return -1;
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x000E7C60 File Offset: 0x000E6C60
		internal static string InternalGetSoapTypeNameFromQualifiedTypeName(string xmlTypeName, string xmlTypeNamespace)
		{
			string text;
			string str;
			if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out text, out str))
			{
				return null;
			}
			string str2;
			if (text != null && text.Length > 0)
			{
				str2 = text + "." + xmlTypeName;
			}
			else
			{
				str2 = xmlTypeName;
			}
			try
			{
				return str2 + ", " + str;
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x000E7CC4 File Offset: 0x000E6CC4
		internal static string InternalGetTypeNameFromQualifiedTypeName(string qualifiedTypeName)
		{
			if (qualifiedTypeName == null)
			{
				throw new ArgumentNullException("qualifiedTypeName");
			}
			string text = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
			if (text != null)
			{
				return text;
			}
			int num = RemotingServices.IsSoapType(qualifiedTypeName);
			if (num != -1)
			{
				string xmlTypeName = qualifiedTypeName.Substring(5, num - 5);
				string xmlTypeNamespace = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
				text = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(xmlTypeName, xmlTypeNamespace);
				if (text != null)
				{
					return text;
				}
			}
			return qualifiedTypeName;
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x000E7D24 File Offset: 0x000E6D24
		internal static Type InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName, bool partialFallback)
		{
			if (qualifiedTypeName == null)
			{
				throw new ArgumentNullException("qualifiedTypeName");
			}
			string text = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
			if (text != null)
			{
				return RemotingServices.LoadClrTypeWithPartialBindFallback(text, partialFallback);
			}
			int num = RemotingServices.IsSoapType(qualifiedTypeName);
			if (num != -1)
			{
				string text2 = qualifiedTypeName.Substring(5, num - 5);
				string xmlTypeNamespace = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
				Type interopTypeFromXmlType = SoapServices.GetInteropTypeFromXmlType(text2, xmlTypeNamespace);
				if (interopTypeFromXmlType != null)
				{
					return interopTypeFromXmlType;
				}
				text = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(text2, xmlTypeNamespace);
				if (text != null)
				{
					return RemotingServices.LoadClrTypeWithPartialBindFallback(text, true);
				}
			}
			return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName, partialFallback);
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x000E7DA5 File Offset: 0x000E6DA5
		internal static Type InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName)
		{
			return RemotingServices.InternalGetTypeFromQualifiedTypeName(qualifiedTypeName, true);
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x000E7DB0 File Offset: 0x000E6DB0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Type LoadClrTypeWithPartialBindFallback(string typeName, bool partialFallback)
		{
			if (!partialFallback)
			{
				return Type.GetType(typeName, false);
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return new RuntimeTypeHandle(RuntimeTypeHandle._GetTypeByName(typeName, false, false, false, ref stackCrawlMark, true)).GetRuntimeType();
		}

		// Token: 0x060043C1 RID: 17345
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemoting();

		// Token: 0x060043C2 RID: 17346
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemotingCookie();

		// Token: 0x060043C3 RID: 17347
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CORProfilerTrackRemotingAsync();

		// Token: 0x060043C4 RID: 17348
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingClientSendingMessage(out Guid id, bool fIsAsync);

		// Token: 0x060043C5 RID: 17349
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingClientReceivingReply(Guid id, bool fIsAsync);

		// Token: 0x060043C6 RID: 17350
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingServerReceivingMessage(Guid id, bool fIsAsync);

		// Token: 0x060043C7 RID: 17351
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CORProfilerRemotingServerSendingReply(out Guid id, bool fIsAsync);

		// Token: 0x060043C8 RID: 17352 RVA: 0x000E7DE3 File Offset: 0x000E6DE3
		[Obsolete("Use of this method is not recommended. The LogRemotingStage existed for internal diagnostic purposes only.")]
		[Conditional("REMOTING_PERF")]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public static void LogRemotingStage(int stage)
		{
		}

		// Token: 0x060043C9 RID: 17353
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetInterfaceCache(object proxy);

		// Token: 0x040021E1 RID: 8673
		private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040021E2 RID: 8674
		private const string FieldGetterName = "FieldGetter";

		// Token: 0x040021E3 RID: 8675
		private const string FieldSetterName = "FieldSetter";

		// Token: 0x040021E4 RID: 8676
		private const string IsInstanceOfTypeName = "IsInstanceOfType";

		// Token: 0x040021E5 RID: 8677
		private const string CanCastToXmlTypeName = "CanCastToXmlType";

		// Token: 0x040021E6 RID: 8678
		private const string InvokeMemberName = "InvokeMember";

		// Token: 0x040021E7 RID: 8679
		internal static SecurityPermission s_RemotingInfrastructurePermission;

		// Token: 0x040021E8 RID: 8680
		internal static Assembly s_MscorlibAssembly;

		// Token: 0x040021E9 RID: 8681
		private static MethodBase s_FieldGetterMB;

		// Token: 0x040021EA RID: 8682
		private static MethodBase s_FieldSetterMB;

		// Token: 0x040021EB RID: 8683
		private static MethodBase s_IsInstanceOfTypeMB;

		// Token: 0x040021EC RID: 8684
		private static MethodBase s_CanCastToXmlTypeMB;

		// Token: 0x040021ED RID: 8685
		private static MethodBase s_InvokeMemberMB;

		// Token: 0x040021EE RID: 8686
		private static bool s_bRemoteActivationConfigured;

		// Token: 0x040021EF RID: 8687
		private static bool s_bRegisteredWellKnownChannels;

		// Token: 0x040021F0 RID: 8688
		private static bool s_bInProcessOfRegisteringWellKnownChannels;

		// Token: 0x040021F1 RID: 8689
		private static object s_delayLoadChannelLock;
	}
}
