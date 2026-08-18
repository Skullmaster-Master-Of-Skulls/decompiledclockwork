using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000710 RID: 1808
	[Serializable]
	internal class Message : IMethodCallMessage, IMethodMessage, IMessage, IInternalMessage, ISerializable
	{
		// Token: 0x06004038 RID: 16440 RVA: 0x000DACEC File Offset: 0x000D9CEC
		public virtual Exception GetFault()
		{
			return this._Fault;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000DACF4 File Offset: 0x000D9CF4
		public virtual void SetFault(Exception e)
		{
			this._Fault = e;
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000DACFD File Offset: 0x000D9CFD
		internal virtual void SetOneWay()
		{
			this._flags |= 8;
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000DAD0D File Offset: 0x000D9D0D
		public virtual int GetCallType()
		{
			this.InitIfNecessary();
			return this._flags;
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x000DAD1B File Offset: 0x000D9D1B
		internal IntPtr GetFramePtr()
		{
			return this._frame;
		}

		// Token: 0x0600403D RID: 16445
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetAsyncBeginInfo(out AsyncCallback acbd, out object state);

		// Token: 0x0600403E RID: 16446
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetThisPtr();

		// Token: 0x0600403F RID: 16447
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IAsyncResult GetAsyncResult();

		// Token: 0x06004040 RID: 16448 RVA: 0x000DAD23 File Offset: 0x000D9D23
		public void Init()
		{
		}

		// Token: 0x06004041 RID: 16449
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetReturnValue();

		// Token: 0x06004042 RID: 16450 RVA: 0x000DAD25 File Offset: 0x000D9D25
		internal Message()
		{
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x000DAD30 File Offset: 0x000D9D30
		internal void InitFields(MessageData msgData)
		{
			this._frame = msgData.pFrame;
			this._delegateMD = msgData.pDelegateMD;
			this._methodDesc = msgData.pMethodDesc;
			this._flags = msgData.iFlags;
			this._initDone = true;
			this._metaSigHolder = msgData.pSig;
			this._governingType = msgData.thGoverningType;
			this._MethodName = null;
			this._MethodSignature = null;
			this._MethodBase = null;
			this._URI = null;
			this._Fault = null;
			this._ID = null;
			this._srvID = null;
			this._callContext = null;
			if (this._properties != null)
			{
				((IDictionary)this._properties).Clear();
			}
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x000DADE2 File Offset: 0x000D9DE2
		private void InitIfNecessary()
		{
			if (!this._initDone)
			{
				this.Init();
				this._initDone = true;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06004045 RID: 16453 RVA: 0x000DADF9 File Offset: 0x000D9DF9
		// (set) Token: 0x06004046 RID: 16454 RVA: 0x000DAE01 File Offset: 0x000D9E01
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			get
			{
				return this._srvID;
			}
			set
			{
				this._srvID = value;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x000DAE0A File Offset: 0x000D9E0A
		// (set) Token: 0x06004048 RID: 16456 RVA: 0x000DAE12 File Offset: 0x000D9E12
		Identity IInternalMessage.IdentityObject
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x000DAE1B File Offset: 0x000D9E1B
		void IInternalMessage.SetURI(string URI)
		{
			this._URI = URI;
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x000DAE24 File Offset: 0x000D9E24
		void IInternalMessage.SetCallContext(LogicalCallContext callContext)
		{
			this._callContext = callContext;
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x000DAE2D File Offset: 0x000D9E2D
		bool IInternalMessage.HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x000DAE3B File Offset: 0x000D9E3B
		public IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					Interlocked.CompareExchange(ref this._properties, new MCMDictionary(this, null), null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x000DAE64 File Offset: 0x000D9E64
		// (set) Token: 0x0600404E RID: 16462 RVA: 0x000DAE6C File Offset: 0x000D9E6C
		public string Uri
		{
			get
			{
				return this._URI;
			}
			set
			{
				this._URI = value;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x000DAE78 File Offset: 0x000D9E78
		public bool HasVarArgs
		{
			get
			{
				if ((this._flags & 16) == 0 && (this._flags & 32) == 0)
				{
					if (!this.InternalHasVarArgs())
					{
						this._flags |= 16;
					}
					else
					{
						this._flags |= 32;
					}
				}
				return 1 == (this._flags & 32);
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x000DAECF File Offset: 0x000D9ECF
		public int ArgCount
		{
			get
			{
				return this.InternalGetArgCount();
			}
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x000DAED7 File Offset: 0x000D9ED7
		public object GetArg(int argNum)
		{
			return this.InternalGetArg(argNum);
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x000DAEE0 File Offset: 0x000D9EE0
		public string GetArgName(int index)
		{
			if (index >= this.ArgCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.GetMethodBase());
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			if (index < parameters.Length)
			{
				return parameters[index].Name;
			}
			return "VarArg" + (index - parameters.Length);
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06004053 RID: 16467 RVA: 0x000DAF37 File Offset: 0x000D9F37
		public object[] Args
		{
			get
			{
				return this.InternalGetArgs();
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x000DAF3F File Offset: 0x000D9F3F
		public int InArgCount
		{
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x000DAF61 File Offset: 0x000D9F61
		public object GetInArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x000DAF84 File Offset: 0x000D9F84
		public string GetInArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, false);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x000DAFA7 File Offset: 0x000D9FA7
		public object[] InArgs
		{
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, false);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x000DAFCC File Offset: 0x000D9FCC
		private void UpdateNames()
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.GetMethodBase());
			this._typeName = reflectionCachedData.TypeAndAssemblyName;
			this._MethodName = reflectionCachedData.MethodName;
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06004059 RID: 16473 RVA: 0x000DAFFD File Offset: 0x000D9FFD
		public string MethodName
		{
			get
			{
				if (this._MethodName == null)
				{
					this.UpdateNames();
				}
				return this._MethodName;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600405A RID: 16474 RVA: 0x000DB013 File Offset: 0x000DA013
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					this.UpdateNames();
				}
				return this._typeName;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x000DB029 File Offset: 0x000DA029
		public object MethodSignature
		{
			get
			{
				if (this._MethodSignature == null)
				{
					this._MethodSignature = Message.GenerateMethodSignature(this.GetMethodBase());
				}
				return this._MethodSignature;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x000DB04A File Offset: 0x000DA04A
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x000DB052 File Offset: 0x000DA052
		public MethodBase MethodBase
		{
			get
			{
				return this.GetMethodBase();
			}
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x000DB05A File Offset: 0x000DA05A
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000DB06C File Offset: 0x000DA06C
		internal unsafe MethodBase GetMethodBase()
		{
			if (this._MethodBase == null)
			{
				RuntimeMethodHandle methodHandle = new RuntimeMethodHandle((void*)this._methodDesc);
				RuntimeTypeHandle reflectedTypeHandle = new RuntimeTypeHandle((void*)this._governingType);
				this._MethodBase = RuntimeType.GetMethodBase(reflectedTypeHandle, methodHandle);
			}
			return this._MethodBase;
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000DB0B8 File Offset: 0x000DA0B8
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			LogicalCallContext callContext = this._callContext;
			this._callContext = callCtx;
			return callContext;
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x000DB0D4 File Offset: 0x000DA0D4
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			return this._callContext;
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000DB0F0 File Offset: 0x000DA0F0
		internal static Type[] GenerateMethodSignature(MethodBase mb)
		{
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(mb);
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x000DB130 File Offset: 0x000DA130
		internal static object[] CoerceArgs(IMethodMessage m)
		{
			MethodBase methodBase = m.MethodBase;
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
			return Message.CoerceArgs(m, reflectionCachedData.Parameters);
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000DB157 File Offset: 0x000DA157
		internal static object[] CoerceArgs(IMethodMessage m, ParameterInfo[] pi)
		{
			return Message.CoerceArgs(m.MethodBase, m.Args, pi);
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x000DB16C File Offset: 0x000DA16C
		internal static object[] CoerceArgs(MethodBase mb, object[] args, ParameterInfo[] pi)
		{
			if (pi == null)
			{
				throw new ArgumentNullException("pi");
			}
			if (pi.Length != args.Length)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_ArgMismatch"), new object[]
				{
					mb.DeclaringType.FullName,
					mb.Name,
					args.Length,
					pi.Length
				}));
			}
			for (int i = 0; i < pi.Length; i++)
			{
				ParameterInfo parameterInfo = pi[i];
				Type parameterType = parameterInfo.ParameterType;
				object obj = args[i];
				if (obj != null)
				{
					args[i] = Message.CoerceArg(obj, parameterType);
				}
				else if (parameterType.IsByRef)
				{
					Type elementType = parameterType.GetElementType();
					if (elementType.IsValueType)
					{
						if (parameterInfo.IsOut)
						{
							args[i] = Activator.CreateInstance(elementType, true);
						}
						else if (!elementType.IsGenericType || elementType.GetGenericTypeDefinition() != typeof(Nullable<>))
						{
							throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MissingArgValue"), new object[]
							{
								elementType.FullName,
								i
							}));
						}
					}
				}
				else if (parameterType.IsValueType && (!parameterType.IsGenericType || parameterType.GetGenericTypeDefinition() != typeof(Nullable<>)))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MissingArgValue"), new object[]
					{
						parameterType.FullName,
						i
					}));
				}
			}
			return args;
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000DB304 File Offset: 0x000DA304
		internal static object CoerceArg(object value, Type pt)
		{
			object obj = null;
			if (value != null)
			{
				Exception innerException = null;
				try
				{
					if (pt.IsByRef)
					{
						pt = pt.GetElementType();
					}
					if (pt.IsInstanceOfType(value))
					{
						obj = value;
					}
					else
					{
						obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
					}
				}
				catch (Exception ex)
				{
					innerException = ex;
				}
				if (obj == null)
				{
					string text;
					if (RemotingServices.IsTransparentProxy(value))
					{
						text = typeof(MarshalByRefObject).ToString();
					}
					else
					{
						text = value.ToString();
					}
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), new object[]
					{
						text,
						pt
					}), innerException);
				}
			}
			return obj;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000DB3B4 File Offset: 0x000DA3B4
		internal static object SoapCoerceArg(object value, Type pt, Hashtable keyToNamespaceTable)
		{
			object obj = null;
			if (value != null)
			{
				try
				{
					if (pt.IsByRef)
					{
						pt = pt.GetElementType();
					}
					if (pt.IsInstanceOfType(value))
					{
						obj = value;
					}
					else
					{
						string text = value as string;
						if (text != null)
						{
							if (pt == typeof(double))
							{
								if (text == "INF")
								{
									obj = double.PositiveInfinity;
								}
								else if (text == "-INF")
								{
									obj = double.NegativeInfinity;
								}
								else
								{
									obj = double.Parse(text, CultureInfo.InvariantCulture);
								}
							}
							else if (pt == typeof(float))
							{
								if (text == "INF")
								{
									obj = float.PositiveInfinity;
								}
								else if (text == "-INF")
								{
									obj = float.NegativeInfinity;
								}
								else
								{
									obj = float.Parse(text, CultureInfo.InvariantCulture);
								}
							}
							else if (SoapType.typeofISoapXsd.IsAssignableFrom(pt))
							{
								if (pt == SoapType.typeofSoapTime)
								{
									obj = SoapTime.Parse(text);
								}
								else if (pt == SoapType.typeofSoapDate)
								{
									obj = SoapDate.Parse(text);
								}
								else if (pt == SoapType.typeofSoapYearMonth)
								{
									obj = SoapYearMonth.Parse(text);
								}
								else if (pt == SoapType.typeofSoapYear)
								{
									obj = SoapYear.Parse(text);
								}
								else if (pt == SoapType.typeofSoapMonthDay)
								{
									obj = SoapMonthDay.Parse(text);
								}
								else if (pt == SoapType.typeofSoapDay)
								{
									obj = SoapDay.Parse(text);
								}
								else if (pt == SoapType.typeofSoapMonth)
								{
									obj = SoapMonth.Parse(text);
								}
								else if (pt == SoapType.typeofSoapHexBinary)
								{
									obj = SoapHexBinary.Parse(text);
								}
								else if (pt == SoapType.typeofSoapBase64Binary)
								{
									obj = SoapBase64Binary.Parse(text);
								}
								else if (pt == SoapType.typeofSoapInteger)
								{
									obj = SoapInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapPositiveInteger)
								{
									obj = SoapPositiveInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNonPositiveInteger)
								{
									obj = SoapNonPositiveInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNonNegativeInteger)
								{
									obj = SoapNonNegativeInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNegativeInteger)
								{
									obj = SoapNegativeInteger.Parse(text);
								}
								else if (pt == SoapType.typeofSoapAnyUri)
								{
									obj = SoapAnyUri.Parse(text);
								}
								else if (pt == SoapType.typeofSoapQName)
								{
									obj = SoapQName.Parse(text);
									SoapQName soapQName = (SoapQName)obj;
									if (soapQName.Key.Length == 0)
									{
										soapQName.Namespace = (string)keyToNamespaceTable["xmlns"];
									}
									else
									{
										soapQName.Namespace = (string)keyToNamespaceTable["xmlns:" + soapQName.Key];
									}
								}
								else if (pt == SoapType.typeofSoapNotation)
								{
									obj = SoapNotation.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNormalizedString)
								{
									obj = SoapNormalizedString.Parse(text);
								}
								else if (pt == SoapType.typeofSoapToken)
								{
									obj = SoapToken.Parse(text);
								}
								else if (pt == SoapType.typeofSoapLanguage)
								{
									obj = SoapLanguage.Parse(text);
								}
								else if (pt == SoapType.typeofSoapName)
								{
									obj = SoapName.Parse(text);
								}
								else if (pt == SoapType.typeofSoapIdrefs)
								{
									obj = SoapIdrefs.Parse(text);
								}
								else if (pt == SoapType.typeofSoapEntities)
								{
									obj = SoapEntities.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNmtoken)
								{
									obj = SoapNmtoken.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNmtokens)
								{
									obj = SoapNmtokens.Parse(text);
								}
								else if (pt == SoapType.typeofSoapNcName)
								{
									obj = SoapNcName.Parse(text);
								}
								else if (pt == SoapType.typeofSoapId)
								{
									obj = SoapId.Parse(text);
								}
								else if (pt == SoapType.typeofSoapIdref)
								{
									obj = SoapIdref.Parse(text);
								}
								else if (pt == SoapType.typeofSoapEntity)
								{
									obj = SoapEntity.Parse(text);
								}
							}
							else if (pt == typeof(bool))
							{
								if (text == "1" || text == "true")
								{
									obj = true;
								}
								else
								{
									if (!(text == "0") && !(text == "false"))
									{
										throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), new object[]
										{
											text,
											pt
										}));
									}
									obj = false;
								}
							}
							else if (pt == typeof(DateTime))
							{
								obj = SoapDateTime.Parse(text);
							}
							else if (pt.IsPrimitive)
							{
								obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
							}
							else if (pt == typeof(TimeSpan))
							{
								obj = SoapDuration.Parse(text);
							}
							else if (pt == typeof(char))
							{
								obj = text[0];
							}
							else
							{
								obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
							}
						}
						else
						{
							obj = Convert.ChangeType(value, pt, CultureInfo.InvariantCulture);
						}
					}
				}
				catch (Exception)
				{
				}
				if (obj == null)
				{
					string text2;
					if (RemotingServices.IsTransparentProxy(value))
					{
						text2 = typeof(MarshalByRefObject).ToString();
					}
					else
					{
						text2 = value.ToString();
					}
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), new object[]
					{
						text2,
						pt
					}));
				}
			}
			return obj;
		}

		// Token: 0x06004068 RID: 16488
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool InternalHasVarArgs();

		// Token: 0x06004069 RID: 16489
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int InternalGetArgCount();

		// Token: 0x0600406A RID: 16490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object InternalGetArg(int argNum);

		// Token: 0x0600406B RID: 16491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object[] InternalGetArgs();

		// Token: 0x0600406C RID: 16492
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PropagateOutParameters(object[] OutArgs, object retVal);

		// Token: 0x0600406D RID: 16493
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Dispatch(object target, bool fExecuteInContext);

		// Token: 0x0600406E RID: 16494 RVA: 0x000DB8E4 File Offset: 0x000DA8E4
		[Conditional("_REMOTING_DEBUG")]
		public static void DebugOut(string s)
		{
			Message.OutToUnmanagedDebugger(string.Concat(new object[]
			{
				"\nRMTING: Thrd ",
				Thread.CurrentThread.GetHashCode(),
				" : ",
				s
			}));
		}

		// Token: 0x0600406F RID: 16495
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void OutToUnmanagedDebugger(string s);

		// Token: 0x06004070 RID: 16496 RVA: 0x000DB929 File Offset: 0x000DA929
		internal static LogicalCallContext PropagateCallContextFromMessageToThread(IMessage msg)
		{
			return CallContext.SetLogicalCallContext((LogicalCallContext)msg.Properties[Message.CallContextKey]);
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x000DB948 File Offset: 0x000DA948
		internal static void PropagateCallContextFromThreadToMessage(IMessage msg)
		{
			LogicalCallContext logicalCallContext = CallContext.GetLogicalCallContext();
			msg.Properties[Message.CallContextKey] = logicalCallContext;
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x000DB96C File Offset: 0x000DA96C
		internal static void PropagateCallContextFromThreadToMessage(IMessage msg, LogicalCallContext oldcctx)
		{
			Message.PropagateCallContextFromThreadToMessage(msg);
			CallContext.SetLogicalCallContext(oldcctx);
		}

		// Token: 0x04002070 RID: 8304
		internal const int Sync = 0;

		// Token: 0x04002071 RID: 8305
		internal const int BeginAsync = 1;

		// Token: 0x04002072 RID: 8306
		internal const int EndAsync = 2;

		// Token: 0x04002073 RID: 8307
		internal const int Ctor = 4;

		// Token: 0x04002074 RID: 8308
		internal const int OneWay = 8;

		// Token: 0x04002075 RID: 8309
		internal const int CallMask = 15;

		// Token: 0x04002076 RID: 8310
		internal const int FixedArgs = 16;

		// Token: 0x04002077 RID: 8311
		internal const int VarArgs = 32;

		// Token: 0x04002078 RID: 8312
		private string _MethodName;

		// Token: 0x04002079 RID: 8313
		private Type[] _MethodSignature;

		// Token: 0x0400207A RID: 8314
		private MethodBase _MethodBase;

		// Token: 0x0400207B RID: 8315
		private object _properties;

		// Token: 0x0400207C RID: 8316
		private string _URI;

		// Token: 0x0400207D RID: 8317
		private string _typeName;

		// Token: 0x0400207E RID: 8318
		private Exception _Fault;

		// Token: 0x0400207F RID: 8319
		private Identity _ID;

		// Token: 0x04002080 RID: 8320
		private ServerIdentity _srvID;

		// Token: 0x04002081 RID: 8321
		private ArgMapper _argMapper;

		// Token: 0x04002082 RID: 8322
		private LogicalCallContext _callContext;

		// Token: 0x04002083 RID: 8323
		private IntPtr _frame;

		// Token: 0x04002084 RID: 8324
		private IntPtr _methodDesc;

		// Token: 0x04002085 RID: 8325
		private IntPtr _metaSigHolder;

		// Token: 0x04002086 RID: 8326
		private IntPtr _delegateMD;

		// Token: 0x04002087 RID: 8327
		private IntPtr _governingType;

		// Token: 0x04002088 RID: 8328
		private int _flags;

		// Token: 0x04002089 RID: 8329
		private bool _initDone;

		// Token: 0x0400208A RID: 8330
		internal static string CallContextKey = "__CallContext";

		// Token: 0x0400208B RID: 8331
		internal static string UriKey = "__Uri";
	}
}
