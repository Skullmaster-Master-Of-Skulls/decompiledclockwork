using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000700 RID: 1792
	internal sealed class IdentityHolder
	{
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x000D92E5 File Offset: 0x000D82E5
		internal static Hashtable URITable
		{
			get
			{
				return IdentityHolder._URITable;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x000D92EC File Offset: 0x000D82EC
		internal static Context DefaultContext
		{
			get
			{
				if (IdentityHolder._cachedDefaultContext == null)
				{
					IdentityHolder._cachedDefaultContext = Thread.GetDomain().GetDefaultContext();
				}
				return IdentityHolder._cachedDefaultContext;
			}
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x000D9309 File Offset: 0x000D8309
		private static string MakeURIKey(string uri)
		{
			return Identity.RemoveAppNameOrAppGuidIfNecessary(uri.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x000D931B File Offset: 0x000D831B
		private static string MakeURIKeyNoLower(string uri)
		{
			return Identity.RemoveAppNameOrAppGuidIfNecessary(uri);
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x000D9323 File Offset: 0x000D8323
		internal static ReaderWriterLock TableLock
		{
			get
			{
				return Thread.GetDomain().RemotingData.IDTableLock;
			}
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x000D9334 File Offset: 0x000D8334
		private static void CleanupIdentities(object state)
		{
			IDictionaryEnumerator enumerator = IdentityHolder.URITable.GetEnumerator();
			ArrayList arrayList = new ArrayList();
			while (enumerator.MoveNext())
			{
				object value = enumerator.Value;
				WeakReference weakReference = value as WeakReference;
				if (weakReference != null && weakReference.Target == null)
				{
					arrayList.Add(enumerator.Key);
				}
			}
			foreach (object obj in arrayList)
			{
				string key = (string)obj;
				IdentityHolder.URITable.Remove(key);
			}
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x000D93D8 File Offset: 0x000D83D8
		internal static void FlushIdentityTable()
		{
			ReaderWriterLock tableLock = IdentityHolder.TableLock;
			bool flag = !tableLock.IsWriterLockHeld;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (flag)
				{
					tableLock.AcquireWriterLock(int.MaxValue);
				}
				IdentityHolder.CleanupIdentities(null);
			}
			finally
			{
				if (flag && tableLock.IsWriterLockHeld)
				{
					tableLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x000D9434 File Offset: 0x000D8434
		private IdentityHolder()
		{
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x000D943C File Offset: 0x000D843C
		internal static Identity ResolveIdentity(string URI)
		{
			if (URI == null)
			{
				throw new ArgumentNullException("URI");
			}
			ReaderWriterLock tableLock = IdentityHolder.TableLock;
			bool flag = !tableLock.IsReaderLockHeld;
			RuntimeHelpers.PrepareConstrainedRegions();
			Identity result;
			try
			{
				if (flag)
				{
					tableLock.AcquireReaderLock(int.MaxValue);
				}
				result = IdentityHolder.ResolveReference(IdentityHolder.URITable[IdentityHolder.MakeURIKey(URI)]);
			}
			finally
			{
				if (flag && tableLock.IsReaderLockHeld)
				{
					tableLock.ReleaseReaderLock();
				}
			}
			return result;
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x000D94B8 File Offset: 0x000D84B8
		internal static Identity CasualResolveIdentity(string uri)
		{
			if (uri == null)
			{
				return null;
			}
			Identity identity = IdentityHolder.CasualResolveReference(IdentityHolder.URITable[IdentityHolder.MakeURIKeyNoLower(uri)]);
			if (identity == null)
			{
				identity = IdentityHolder.CasualResolveReference(IdentityHolder.URITable[IdentityHolder.MakeURIKey(uri)]);
				if (identity == null)
				{
					identity = RemotingConfigHandler.CreateWellKnownObject(uri);
				}
			}
			return identity;
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x000D9504 File Offset: 0x000D8504
		private static Identity ResolveReference(object o)
		{
			WeakReference weakReference = o as WeakReference;
			if (weakReference != null)
			{
				return (Identity)weakReference.Target;
			}
			return (Identity)o;
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x000D9530 File Offset: 0x000D8530
		private static Identity CasualResolveReference(object o)
		{
			WeakReference weakReference = o as WeakReference;
			if (weakReference != null)
			{
				return (Identity)weakReference.Target;
			}
			return (Identity)o;
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x000D955C File Offset: 0x000D855C
		internal static ServerIdentity FindOrCreateServerIdentity(MarshalByRefObject obj, string objURI, int flags)
		{
			ServerIdentity serverIdentity = null;
			bool flag;
			serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(obj, out flag);
			if (serverIdentity == null)
			{
				Context serverCtx;
				if (obj is ContextBoundObject)
				{
					serverCtx = Thread.CurrentContext;
				}
				else
				{
					serverCtx = IdentityHolder.DefaultContext;
				}
				ServerIdentity serverIdentity2 = new ServerIdentity(obj, serverCtx);
				if (flag)
				{
					serverIdentity = obj.__RaceSetServerIdentity(serverIdentity2);
				}
				else
				{
					RealProxy realProxy = RemotingServices.GetRealProxy(obj);
					realProxy.IdentityObject = serverIdentity2;
					serverIdentity = (ServerIdentity)realProxy.IdentityObject;
				}
			}
			if (IdOps.bStrongIdentity(flags))
			{
				ReaderWriterLock tableLock = IdentityHolder.TableLock;
				bool flag2 = !tableLock.IsWriterLockHeld;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					if (flag2)
					{
						tableLock.AcquireWriterLock(int.MaxValue);
					}
					if (serverIdentity.ObjURI == null || !serverIdentity.IsInIDTable())
					{
						IdentityHolder.SetIdentity(serverIdentity, objURI, DuplicateIdentityOption.Unique);
					}
					if (serverIdentity.IsDisconnected())
					{
						serverIdentity.SetFullyConnected();
					}
				}
				finally
				{
					if (flag2 && tableLock.IsWriterLockHeld)
					{
						tableLock.ReleaseWriterLock();
					}
				}
			}
			return serverIdentity;
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x000D9648 File Offset: 0x000D8648
		internal static Identity FindOrCreateIdentity(string objURI, string URL, ObjRef objectRef)
		{
			Identity identity = null;
			bool flag = URL != null;
			identity = IdentityHolder.ResolveIdentity(flag ? URL : objURI);
			if (flag && identity != null && identity is ServerIdentity)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CantDirectlyConnect"), new object[]
				{
					URL
				}));
			}
			if (identity == null)
			{
				identity = new Identity(objURI, URL);
				ReaderWriterLock tableLock = IdentityHolder.TableLock;
				bool flag2 = !tableLock.IsWriterLockHeld;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					if (flag2)
					{
						tableLock.AcquireWriterLock(int.MaxValue);
					}
					identity = IdentityHolder.SetIdentity(identity, null, DuplicateIdentityOption.UseExisting);
					identity.RaceSetObjRef(objectRef);
				}
				finally
				{
					if (flag2 && tableLock.IsWriterLockHeld)
					{
						tableLock.ReleaseWriterLock();
					}
				}
			}
			return identity;
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x000D9708 File Offset: 0x000D8708
		private static Identity SetIdentity(Identity idObj, string URI, DuplicateIdentityOption duplicateOption)
		{
			bool flag = idObj is ServerIdentity;
			if (idObj.URI == null)
			{
				idObj.SetOrCreateURI(URI);
				if (idObj.ObjectRef != null)
				{
					idObj.ObjectRef.URI = idObj.URI;
				}
			}
			string key = IdentityHolder.MakeURIKey(idObj.URI);
			object obj = IdentityHolder.URITable[key];
			if (obj != null)
			{
				WeakReference weakReference = obj as WeakReference;
				Identity identity;
				bool flag2;
				if (weakReference != null)
				{
					identity = (Identity)weakReference.Target;
					flag2 = (identity is ServerIdentity);
				}
				else
				{
					identity = (Identity)obj;
					flag2 = (identity is ServerIdentity);
				}
				if (identity != null && identity != idObj)
				{
					switch (duplicateOption)
					{
					case DuplicateIdentityOption.Unique:
					{
						string uri = idObj.URI;
						throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_URIClash"), new object[]
						{
							uri
						}));
					}
					case DuplicateIdentityOption.UseExisting:
						idObj = identity;
						break;
					}
				}
				else if (weakReference != null)
				{
					if (flag2)
					{
						IdentityHolder.URITable[key] = idObj;
					}
					else
					{
						weakReference.Target = idObj;
					}
				}
			}
			else
			{
				object value;
				if (flag)
				{
					value = idObj;
					((ServerIdentity)idObj).SetHandle();
				}
				else
				{
					value = new WeakReference(idObj);
				}
				IdentityHolder.URITable.Add(key, value);
				idObj.SetInIDTable();
				IdentityHolder.SetIDCount++;
				if (IdentityHolder.SetIDCount % 64 == 0)
				{
					IdentityHolder.CleanupIdentities(null);
				}
			}
			return idObj;
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x000D9867 File Offset: 0x000D8867
		internal static void RemoveIdentity(string uri)
		{
			IdentityHolder.RemoveIdentity(uri, true);
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x000D9870 File Offset: 0x000D8870
		internal static void RemoveIdentity(string uri, bool bResetURI)
		{
			string key = IdentityHolder.MakeURIKey(uri);
			ReaderWriterLock tableLock = IdentityHolder.TableLock;
			bool flag = !tableLock.IsWriterLockHeld;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (flag)
				{
					tableLock.AcquireWriterLock(int.MaxValue);
				}
				object obj = IdentityHolder.URITable[key];
				WeakReference weakReference = obj as WeakReference;
				Identity identity;
				if (weakReference != null)
				{
					identity = (Identity)weakReference.Target;
					weakReference.Target = null;
				}
				else
				{
					identity = (Identity)obj;
					if (identity != null)
					{
						((ServerIdentity)identity).ResetHandle();
					}
				}
				if (identity != null)
				{
					IdentityHolder.URITable.Remove(key);
					identity.ResetInIDTable(bResetURI);
				}
			}
			finally
			{
				if (flag && tableLock.IsWriterLockHeld)
				{
					tableLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000D9928 File Offset: 0x000D8928
		internal static bool AddDynamicProperty(MarshalByRefObject obj, IDynamicProperty prop)
		{
			if (RemotingServices.IsObjectOutOfContext(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				return realProxy.IdentityObject.AddProxySideDynamicProperty(prop);
			}
			MarshalByRefObject obj2 = (MarshalByRefObject)RemotingServices.AlwaysUnwrap((ContextBoundObject)obj);
			ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(obj2);
			if (serverIdentity != null)
			{
				return serverIdentity.AddServerSideDynamicProperty(prop);
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000D9988 File Offset: 0x000D8988
		internal static bool RemoveDynamicProperty(MarshalByRefObject obj, string name)
		{
			if (RemotingServices.IsObjectOutOfContext(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				return realProxy.IdentityObject.RemoveProxySideDynamicProperty(name);
			}
			MarshalByRefObject obj2 = (MarshalByRefObject)RemotingServices.AlwaysUnwrap((ContextBoundObject)obj);
			ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity(obj2);
			if (serverIdentity != null)
			{
				return serverIdentity.RemoveServerSideDynamicProperty(name);
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
		}

		// Token: 0x0400203F RID: 8255
		private const int CleanUpCountInterval = 64;

		// Token: 0x04002040 RID: 8256
		private const int INFINITE = 2147483647;

		// Token: 0x04002041 RID: 8257
		private static int SetIDCount = 0;

		// Token: 0x04002042 RID: 8258
		private static Hashtable _URITable = new Hashtable();

		// Token: 0x04002043 RID: 8259
		private static Context _cachedDefaultContext = null;
	}
}
