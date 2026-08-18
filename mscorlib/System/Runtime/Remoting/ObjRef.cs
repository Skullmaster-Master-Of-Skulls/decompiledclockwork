using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Runtime.Remoting
{
	// Token: 0x02000737 RID: 1847
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ObjRef : IObjectReference, ISerializable
	{
		// Token: 0x06004213 RID: 16915 RVA: 0x000E0A26 File Offset: 0x000DFA26
		internal void SetServerIdentity(GCHandle hndSrvIdentity)
		{
			this.srvIdentity = hndSrvIdentity;
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000E0A2F File Offset: 0x000DFA2F
		internal GCHandle GetServerIdentity()
		{
			return this.srvIdentity;
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000E0A37 File Offset: 0x000DFA37
		internal void SetDomainID(int id)
		{
			this.domainID = id;
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x000E0A40 File Offset: 0x000DFA40
		internal int GetDomainID()
		{
			return this.domainID;
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x000E0A48 File Offset: 0x000DFA48
		private ObjRef(ObjRef o)
		{
			this.uri = o.uri;
			this.typeInfo = o.typeInfo;
			this.envoyInfo = o.envoyInfo;
			this.channelInfo = o.channelInfo;
			this.objrefFlags = o.objrefFlags;
			this.SetServerIdentity(o.GetServerIdentity());
			this.SetDomainID(o.GetDomainID());
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x000E0AB0 File Offset: 0x000DFAB0
		public ObjRef(MarshalByRefObject o, Type requestedType)
		{
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(o, out flag);
			this.Init(o, identity, requestedType);
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x000E0AD8 File Offset: 0x000DFAD8
		protected ObjRef(SerializationInfo info, StreamingContext context)
		{
			string text = null;
			bool flag = false;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("uri"))
				{
					this.uri = (string)enumerator.Value;
				}
				else if (enumerator.Name.Equals("typeInfo"))
				{
					this.typeInfo = (IRemotingTypeInfo)enumerator.Value;
				}
				else if (enumerator.Name.Equals("envoyInfo"))
				{
					this.envoyInfo = (IEnvoyInfo)enumerator.Value;
				}
				else if (enumerator.Name.Equals("channelInfo"))
				{
					this.channelInfo = (IChannelInfo)enumerator.Value;
				}
				else if (enumerator.Name.Equals("objrefFlags"))
				{
					object value = enumerator.Value;
					if (value.GetType() == typeof(string))
					{
						this.objrefFlags = ((IConvertible)value).ToInt32(null);
					}
					else
					{
						this.objrefFlags = (int)value;
					}
				}
				else if (enumerator.Name.Equals("fIsMarshalled"))
				{
					object value2 = enumerator.Value;
					int num;
					if (value2.GetType() == typeof(string))
					{
						num = ((IConvertible)value2).ToInt32(null);
					}
					else
					{
						num = (int)value2;
					}
					if (num == 0)
					{
						flag = true;
					}
				}
				else if (enumerator.Name.Equals("url"))
				{
					text = (string)enumerator.Value;
				}
				else if (enumerator.Name.Equals("SrvIdentity"))
				{
					this.SetServerIdentity((GCHandle)enumerator.Value);
				}
				else if (enumerator.Name.Equals("DomainId"))
				{
					this.SetDomainID((int)enumerator.Value);
				}
			}
			if (!flag)
			{
				this.objrefFlags |= 1;
			}
			else
			{
				this.objrefFlags &= -2;
			}
			if (text != null)
			{
				this.uri = text;
				this.objrefFlags |= 4;
			}
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x000E0CEC File Offset: 0x000DFCEC
		internal bool CanSmuggle()
		{
			if (base.GetType() != typeof(ObjRef) || this.IsObjRefLite())
			{
				return false;
			}
			Type type = null;
			if (this.typeInfo != null)
			{
				type = this.typeInfo.GetType();
			}
			Type type2 = null;
			if (this.channelInfo != null)
			{
				type2 = this.channelInfo.GetType();
			}
			if ((type == null || type == typeof(TypeInfo) || type == typeof(DynamicTypeInfo)) && this.envoyInfo == null && (type2 == null || type2 == typeof(ChannelInfo)))
			{
				if (this.channelInfo != null)
				{
					foreach (object obj in this.channelInfo.ChannelData)
					{
						if (!(obj is CrossAppDomainData))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x000E0DB5 File Offset: 0x000DFDB5
		internal ObjRef CreateSmuggleableCopy()
		{
			return new ObjRef(this);
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x000E0DC0 File Offset: 0x000DFDC0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(ObjRef.orType);
			if (!this.IsObjRefLite())
			{
				info.AddValue("uri", this.uri, typeof(string));
				info.AddValue("objrefFlags", this.objrefFlags);
				info.AddValue("typeInfo", this.typeInfo, typeof(IRemotingTypeInfo));
				info.AddValue("envoyInfo", this.envoyInfo, typeof(IEnvoyInfo));
				info.AddValue("channelInfo", this.GetChannelInfoHelper(), typeof(IChannelInfo));
				return;
			}
			info.AddValue("url", this.uri, typeof(string));
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x000E0E88 File Offset: 0x000DFE88
		private IChannelInfo GetChannelInfoHelper()
		{
			ChannelInfo channelInfo = this.channelInfo as ChannelInfo;
			if (channelInfo == null)
			{
				return this.channelInfo;
			}
			object[] channelData = channelInfo.ChannelData;
			if (channelData == null)
			{
				return channelInfo;
			}
			string[] array = (string[])CallContext.GetData("__bashChannelUrl");
			if (array == null)
			{
				return channelInfo;
			}
			string value = array[0];
			string text = array[1];
			ChannelInfo channelInfo2 = new ChannelInfo();
			channelInfo2.ChannelData = new object[channelData.Length];
			for (int i = 0; i < channelData.Length; i++)
			{
				channelInfo2.ChannelData[i] = channelData[i];
				ChannelDataStore channelDataStore = channelInfo2.ChannelData[i] as ChannelDataStore;
				if (channelDataStore != null)
				{
					string[] channelUris = channelDataStore.ChannelUris;
					if (channelUris != null && channelUris.Length == 1 && channelUris[0].Equals(value))
					{
						ChannelDataStore channelDataStore2 = channelDataStore.InternalShallowCopy();
						channelDataStore2.ChannelUris = new string[1];
						channelDataStore2.ChannelUris[0] = text;
						channelInfo2.ChannelData[i] = channelDataStore2;
					}
				}
			}
			return channelInfo2;
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x000E0F6F File Offset: 0x000DFF6F
		// (set) Token: 0x0600421F RID: 16927 RVA: 0x000E0F77 File Offset: 0x000DFF77
		public virtual string URI
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x000E0F80 File Offset: 0x000DFF80
		// (set) Token: 0x06004221 RID: 16929 RVA: 0x000E0F88 File Offset: 0x000DFF88
		public virtual IRemotingTypeInfo TypeInfo
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				this.typeInfo = value;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x000E0F91 File Offset: 0x000DFF91
		// (set) Token: 0x06004223 RID: 16931 RVA: 0x000E0F99 File Offset: 0x000DFF99
		public virtual IEnvoyInfo EnvoyInfo
		{
			get
			{
				return this.envoyInfo;
			}
			set
			{
				this.envoyInfo = value;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x000E0FA2 File Offset: 0x000DFFA2
		// (set) Token: 0x06004225 RID: 16933 RVA: 0x000E0FAA File Offset: 0x000DFFAA
		public virtual IChannelInfo ChannelInfo
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.channelInfo;
			}
			set
			{
				this.channelInfo = value;
			}
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x000E0FB3 File Offset: 0x000DFFB3
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual object GetRealObject(StreamingContext context)
		{
			return this.GetRealObjectHelper();
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x000E0FBC File Offset: 0x000DFFBC
		internal object GetRealObjectHelper()
		{
			if (!this.IsMarshaledObject())
			{
				return this;
			}
			if (this.IsObjRefLite())
			{
				int num = this.uri.IndexOf(RemotingConfiguration.ApplicationId);
				if (num > 0)
				{
					this.uri = this.uri.Substring(num - 1);
				}
			}
			bool fRefine = base.GetType() != typeof(ObjRef);
			object ret = RemotingServices.Unmarshal(this, fRefine);
			return this.GetCustomMarshaledCOMObject(ret);
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000E102C File Offset: 0x000E002C
		private object GetCustomMarshaledCOMObject(object ret)
		{
			DynamicTypeInfo dynamicTypeInfo = this.TypeInfo as DynamicTypeInfo;
			if (dynamicTypeInfo != null)
			{
				IntPtr intPtr = Win32Native.NULL;
				if (this.IsFromThisProcess() && !this.IsFromThisAppDomain())
				{
					try
					{
						bool flag;
						intPtr = ((__ComObject)ret).GetIUnknown(out flag);
						if (intPtr != Win32Native.NULL && !flag)
						{
							string typeName = this.TypeInfo.TypeName;
							string name = null;
							string text = null;
							System.Runtime.Remoting.TypeInfo.ParseTypeAndAssembly(typeName, out name, out text);
							Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(text);
							if (assembly == null)
							{
								throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_AssemblyNotFound"), new object[]
								{
									text
								}));
							}
							Type type = assembly.GetType(name, false, false);
							if (type != null && !type.IsVisible)
							{
								type = null;
							}
							object typedObjectForIUnknown = Marshal.GetTypedObjectForIUnknown(intPtr, type);
							if (typedObjectForIUnknown != null)
							{
								ret = typedObjectForIUnknown;
							}
						}
					}
					finally
					{
						if (intPtr != Win32Native.NULL)
						{
							Marshal.Release(intPtr);
						}
					}
				}
			}
			return ret;
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000E1138 File Offset: 0x000E0138
		public ObjRef()
		{
			this.objrefFlags = 0;
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000E1147 File Offset: 0x000E0147
		internal bool IsMarshaledObject()
		{
			return (this.objrefFlags & 1) == 1;
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000E1154 File Offset: 0x000E0154
		internal void SetMarshaledObject()
		{
			this.objrefFlags |= 1;
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000E1164 File Offset: 0x000E0164
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal bool IsWellKnown()
		{
			return (this.objrefFlags & 2) == 2;
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000E1171 File Offset: 0x000E0171
		internal void SetWellKnown()
		{
			this.objrefFlags |= 2;
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000E1181 File Offset: 0x000E0181
		internal bool HasProxyAttribute()
		{
			return (this.objrefFlags & 8) == 8;
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x000E118E File Offset: 0x000E018E
		internal void SetHasProxyAttribute()
		{
			this.objrefFlags |= 8;
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x000E119E File Offset: 0x000E019E
		internal bool IsObjRefLite()
		{
			return (this.objrefFlags & 4) == 4;
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000E11AB File Offset: 0x000E01AB
		internal void SetObjRefLite()
		{
			this.objrefFlags |= 4;
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000E11BC File Offset: 0x000E01BC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private CrossAppDomainData GetAppDomainChannelData()
		{
			for (int i = 0; i < this.ChannelInfo.ChannelData.Length; i++)
			{
				CrossAppDomainData crossAppDomainData = this.ChannelInfo.ChannelData[i] as CrossAppDomainData;
				if (crossAppDomainData != null)
				{
					return crossAppDomainData;
				}
			}
			return null;
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x000E11FC File Offset: 0x000E01FC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool IsFromThisProcess()
		{
			if (this.IsWellKnown())
			{
				return false;
			}
			CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
			return appDomainChannelData != null && appDomainChannelData.IsFromThisProcess();
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x000E1228 File Offset: 0x000E0228
		public bool IsFromThisAppDomain()
		{
			CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
			return appDomainChannelData != null && appDomainChannelData.IsFromThisAppDomain();
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x000E1248 File Offset: 0x000E0248
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal int GetServerDomainId()
		{
			if (!this.IsFromThisProcess())
			{
				return 0;
			}
			CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
			return appDomainChannelData.DomainID;
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x000E126C File Offset: 0x000E026C
		internal IntPtr GetServerContext(out int domainId)
		{
			IntPtr result = IntPtr.Zero;
			domainId = 0;
			if (this.IsFromThisProcess())
			{
				CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
				domainId = appDomainChannelData.DomainID;
				if (AppDomain.IsDomainIdValid(appDomainChannelData.DomainID))
				{
					result = appDomainChannelData.ContextID;
				}
			}
			return result;
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x000E12B0 File Offset: 0x000E02B0
		internal void Init(object o, Identity idObj, Type requestedType)
		{
			this.uri = idObj.URI;
			MarshalByRefObject tporObject = idObj.TPOrObject;
			Type type;
			if (!RemotingServices.IsTransparentProxy(tporObject))
			{
				type = tporObject.GetType();
			}
			else
			{
				type = RemotingServices.GetRealProxy(tporObject).GetProxiedType();
			}
			Type type2 = (requestedType == null) ? type : requestedType;
			if (requestedType != null && !requestedType.IsAssignableFrom(type) && !typeof(IMessageSink).IsAssignableFrom(type))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_InvalidRequestedType"), new object[]
				{
					requestedType.ToString()
				}));
			}
			if (type.IsCOMObject)
			{
				DynamicTypeInfo dynamicTypeInfo = new DynamicTypeInfo(type2);
				this.TypeInfo = dynamicTypeInfo;
			}
			else
			{
				RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(type2);
				this.TypeInfo = reflectionCachedData.TypeInfo;
			}
			if (!idObj.IsWellKnown())
			{
				this.EnvoyInfo = System.Runtime.Remoting.EnvoyInfo.CreateEnvoyInfo(idObj as ServerIdentity);
				IChannelInfo channelInfo = new ChannelInfo();
				if (o is AppDomain)
				{
					object[] channelData = channelInfo.ChannelData;
					int num = channelData.Length;
					object[] array = new object[num];
					Array.Copy(channelData, array, num);
					for (int i = 0; i < num; i++)
					{
						if (!(array[i] is CrossAppDomainData))
						{
							array[i] = null;
						}
					}
					channelInfo.ChannelData = array;
				}
				this.ChannelInfo = channelInfo;
				if (type.HasProxyAttribute)
				{
					this.SetHasProxyAttribute();
				}
			}
			else
			{
				this.SetWellKnown();
			}
			if (ObjRef.ShouldUseUrlObjRef())
			{
				if (this.IsWellKnown())
				{
					this.SetObjRefLite();
					return;
				}
				string text = ChannelServices.FindFirstHttpUrlForObject(this.URI);
				if (text != null)
				{
					this.URI = text;
					this.SetObjRefLite();
				}
			}
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x000E143D File Offset: 0x000E043D
		internal static bool ShouldUseUrlObjRef()
		{
			return RemotingConfigHandler.UrlObjRefMode;
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x000E1444 File Offset: 0x000E0444
		internal static bool IsWellFormed(ObjRef objectRef)
		{
			bool result = true;
			if (objectRef == null || objectRef.URI == null || (!objectRef.IsWellKnown() && !objectRef.IsObjRefLite() && objectRef.GetType() == ObjRef.orType && objectRef.ChannelInfo == null))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0400211C RID: 8476
		internal const int FLG_MARSHALED_OBJECT = 1;

		// Token: 0x0400211D RID: 8477
		internal const int FLG_WELLKNOWN_OBJREF = 2;

		// Token: 0x0400211E RID: 8478
		internal const int FLG_LITE_OBJREF = 4;

		// Token: 0x0400211F RID: 8479
		internal const int FLG_PROXY_ATTRIBUTE = 8;

		// Token: 0x04002120 RID: 8480
		internal string uri;

		// Token: 0x04002121 RID: 8481
		internal IRemotingTypeInfo typeInfo;

		// Token: 0x04002122 RID: 8482
		internal IEnvoyInfo envoyInfo;

		// Token: 0x04002123 RID: 8483
		internal IChannelInfo channelInfo;

		// Token: 0x04002124 RID: 8484
		internal int objrefFlags;

		// Token: 0x04002125 RID: 8485
		internal GCHandle srvIdentity;

		// Token: 0x04002126 RID: 8486
		internal int domainID;

		// Token: 0x04002127 RID: 8487
		private static Type orType = typeof(ObjRef);
	}
}
