using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000699 RID: 1689
	internal static class ActivationServices
	{
		// Token: 0x06003D1E RID: 15646 RVA: 0x000D11F0 File Offset: 0x000D01F0
		private static void Startup()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (!remotingData.ActivationInitialized || remotingData.InitializingActivation)
			{
				object configLock = remotingData.ConfigLock;
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.ReliableEnter(configLock, ref flag);
					remotingData.InitializingActivation = true;
					if (!remotingData.ActivationInitialized)
					{
						remotingData.LocalActivator = new LocalActivator();
						remotingData.ActivationListener = new ActivationListener();
						remotingData.ActivationInitialized = true;
					}
					remotingData.InitializingActivation = false;
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(configLock);
					}
				}
			}
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000D1280 File Offset: 0x000D0280
		private static void InitActivationServices()
		{
			if (ActivationServices.activator == null)
			{
				ActivationServices.activator = ActivationServices.GetActivator();
				if (ActivationServices.activator == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_ActivationFailure"), new object[0]));
				}
			}
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x000D12BC File Offset: 0x000D02BC
		private static MarshalByRefObject IsCurrentContextOK(Type serverType, object[] props, bool bNewObj)
		{
			ActivationServices.InitActivationServices();
			ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(serverType);
			MarshalByRefObject marshalByRefObject;
			if (object.ReferenceEquals(proxyAttribute, ActivationServices.DefaultProxyAttribute))
			{
				marshalByRefObject = proxyAttribute.CreateInstanceInternal(serverType);
			}
			else
			{
				marshalByRefObject = proxyAttribute.CreateInstance(serverType);
				if (marshalByRefObject != null && !RemotingServices.IsTransparentProxy(marshalByRefObject) && !serverType.IsAssignableFrom(marshalByRefObject.GetType()))
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_BadObject"), new object[]
					{
						serverType
					}));
				}
			}
			return marshalByRefObject;
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000D1338 File Offset: 0x000D0338
		private static MarshalByRefObject CreateObjectForCom(Type serverType, object[] props, bool bNewObj)
		{
			if (ActivationServices.PeekActivationAttributes(serverType) != null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ActivForCom"));
			}
			ActivationServices.InitActivationServices();
			ProxyAttribute proxyAttribute = ActivationServices.GetProxyAttribute(serverType);
			MarshalByRefObject result;
			if (proxyAttribute is ICustomFactory)
			{
				result = ((ICustomFactory)proxyAttribute).CreateInstance(serverType);
			}
			else
			{
				result = (MarshalByRefObject)Activator.CreateInstance(serverType, true);
			}
			return result;
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x000D1390 File Offset: 0x000D0390
		private static bool IsCurrentContextOK(Type serverType, object[] props, ref ConstructorCallMessage ctorCallMsg)
		{
			object[] array = ActivationServices.PeekActivationAttributes(serverType);
			if (array != null)
			{
				ActivationServices.PopActivationAttributes(serverType);
			}
			object[] array2 = new object[]
			{
				ActivationServices.GetGlobalAttribute()
			};
			object[] contextAttributesForType = ActivationServices.GetContextAttributesForType(serverType);
			Context currentContext = Thread.CurrentContext;
			ctorCallMsg = new ConstructorCallMessage(array, array2, contextAttributesForType, serverType);
			ctorCallMsg.Activator = new ConstructionLevelActivator();
			bool flag = ActivationServices.QueryAttributesIfContextOK(currentContext, ctorCallMsg, array2);
			if (flag)
			{
				flag = ActivationServices.QueryAttributesIfContextOK(currentContext, ctorCallMsg, array);
				if (flag)
				{
					flag = ActivationServices.QueryAttributesIfContextOK(currentContext, ctorCallMsg, contextAttributesForType);
				}
			}
			return flag;
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x000D140B File Offset: 0x000D040B
		private static void CheckForInfrastructurePermission(Assembly asm)
		{
			if (asm != RemotingServices.s_MscorlibAssembly)
			{
				CodeAccessSecurityEngine.CheckAssembly(asm, RemotingServices.s_RemotingInfrastructurePermission);
			}
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x000D1420 File Offset: 0x000D0420
		private static bool QueryAttributesIfContextOK(Context ctx, IConstructionCallMessage ctorMsg, object[] attributes)
		{
			bool flag = true;
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Length; i++)
				{
					IContextAttribute contextAttribute = attributes[i] as IContextAttribute;
					if (contextAttribute == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_BadAttribute"));
					}
					Assembly assembly = contextAttribute.GetType().Assembly;
					ActivationServices.CheckForInfrastructurePermission(assembly);
					flag = contextAttribute.IsContextOK(ctx, ctorMsg);
					if (!flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x000D1480 File Offset: 0x000D0480
		internal static void GetPropertiesFromAttributes(IConstructionCallMessage ctorMsg, object[] attributes)
		{
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Length; i++)
				{
					IContextAttribute contextAttribute = attributes[i] as IContextAttribute;
					if (contextAttribute == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_BadAttribute"));
					}
					Assembly assembly = contextAttribute.GetType().Assembly;
					ActivationServices.CheckForInfrastructurePermission(assembly);
					contextAttribute.GetPropertiesForNewContext(ctorMsg);
				}
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x000D14D5 File Offset: 0x000D04D5
		internal static ProxyAttribute DefaultProxyAttribute
		{
			get
			{
				return ActivationServices._proxyAttribute;
			}
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000D14DC File Offset: 0x000D04DC
		internal static ProxyAttribute GetProxyAttribute(Type serverType)
		{
			if (!serverType.HasProxyAttribute)
			{
				return ActivationServices.DefaultProxyAttribute;
			}
			ProxyAttribute proxyAttribute = ActivationServices._proxyTable[serverType] as ProxyAttribute;
			if (proxyAttribute == null)
			{
				object[] customAttributes = Attribute.GetCustomAttributes(serverType, ActivationServices.proxyAttributeType, true);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					if (!serverType.IsContextful)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
					}
					proxyAttribute = (customAttributes[0] as ProxyAttribute);
				}
				if (!ActivationServices._proxyTable.Contains(serverType))
				{
					lock (ActivationServices._proxyTable)
					{
						if (!ActivationServices._proxyTable.Contains(serverType))
						{
							ActivationServices._proxyTable.Add(serverType, proxyAttribute);
						}
					}
				}
			}
			return proxyAttribute;
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000D1590 File Offset: 0x000D0590
		internal static MarshalByRefObject CreateInstance(Type serverType)
		{
			ConstructorCallMessage constructorCallMessage = null;
			bool flag = ActivationServices.IsCurrentContextOK(serverType, null, ref constructorCallMessage);
			MarshalByRefObject marshalByRefObject;
			if (flag && !serverType.IsContextful)
			{
				marshalByRefObject = RemotingServices.AllocateUninitializedObject(serverType);
			}
			else
			{
				marshalByRefObject = (MarshalByRefObject)ActivationServices.ConnectIfNecessary(constructorCallMessage);
				RemotingProxy remotingProxy;
				if (marshalByRefObject == null)
				{
					remotingProxy = new RemotingProxy(serverType);
					marshalByRefObject = (MarshalByRefObject)remotingProxy.GetTransparentProxy();
				}
				else
				{
					remotingProxy = (RemotingProxy)RemotingServices.GetRealProxy(marshalByRefObject);
				}
				remotingProxy.ConstructorMessage = constructorCallMessage;
				if (!flag)
				{
					ContextLevelActivator contextLevelActivator = new ContextLevelActivator();
					contextLevelActivator.NextActivator = constructorCallMessage.Activator;
					constructorCallMessage.Activator = contextLevelActivator;
				}
				else
				{
					constructorCallMessage.ActivateInContext = true;
				}
			}
			return marshalByRefObject;
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x000D1620 File Offset: 0x000D0620
		internal static IConstructionReturnMessage Activate(RemotingProxy remProxy, IConstructionCallMessage ctorMsg)
		{
			IConstructionReturnMessage constructionReturnMessage;
			if (((ConstructorCallMessage)ctorMsg).ActivateInContext)
			{
				constructionReturnMessage = ctorMsg.Activator.Activate(ctorMsg);
				if (constructionReturnMessage.Exception != null)
				{
					throw constructionReturnMessage.Exception;
				}
			}
			else
			{
				ActivationServices.GetPropertiesFromAttributes(ctorMsg, ctorMsg.CallSiteActivationAttributes);
				ActivationServices.GetPropertiesFromAttributes(ctorMsg, ((ConstructorCallMessage)ctorMsg).GetWOMAttributes());
				ActivationServices.GetPropertiesFromAttributes(ctorMsg, ((ConstructorCallMessage)ctorMsg).GetTypeAttributes());
				IMessageSink clientContextChain = Thread.CurrentContext.GetClientContextChain();
				IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage)clientContextChain.SyncProcessMessage(ctorMsg);
				constructionReturnMessage = (methodReturnMessage as IConstructionReturnMessage);
				if (methodReturnMessage == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Activation_Failed"));
				}
				if (methodReturnMessage.Exception != null)
				{
					throw methodReturnMessage.Exception;
				}
			}
			return constructionReturnMessage;
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x000D16C8 File Offset: 0x000D06C8
		internal static IConstructionReturnMessage DoCrossContextActivation(IConstructionCallMessage reqMsg)
		{
			bool isContextful = reqMsg.ActivationType.IsContextful;
			Context context = null;
			if (isContextful)
			{
				context = new Context();
				ArrayList arrayList = (ArrayList)reqMsg.ContextProperties;
				for (int i = 0; i < arrayList.Count; i++)
				{
					IContextProperty contextProperty = arrayList[i] as IContextProperty;
					if (contextProperty == null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_BadAttribute"));
					}
					Assembly assembly = contextProperty.GetType().Assembly;
					ActivationServices.CheckForInfrastructurePermission(assembly);
					if (context.GetProperty(contextProperty.Name) == null)
					{
						context.SetProperty(contextProperty);
					}
				}
				context.Freeze();
				for (int j = 0; j < arrayList.Count; j++)
				{
					if (!((IContextProperty)arrayList[j]).IsNewContextOK(context))
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Activation_PropertyUnhappy"));
					}
				}
			}
			InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(ActivationServices.DoCrossContextActivationCallback);
			object[] args = new object[]
			{
				reqMsg
			};
			IConstructionReturnMessage result;
			if (isContextful)
			{
				result = (Thread.CurrentThread.InternalCrossContextCallback(context, internalCrossContextDelegate, args) as IConstructionReturnMessage);
			}
			else
			{
				result = (internalCrossContextDelegate(args) as IConstructionReturnMessage);
			}
			return result;
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x000D17EC File Offset: 0x000D07EC
		internal static object DoCrossContextActivationCallback(object[] args)
		{
			IConstructionCallMessage constructionCallMessage = (IConstructionCallMessage)args[0];
			IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage)Thread.CurrentContext.GetServerContextChain().SyncProcessMessage(constructionCallMessage);
			IConstructionReturnMessage constructionReturnMessage = methodReturnMessage as IConstructionReturnMessage;
			if (constructionReturnMessage == null)
			{
				Exception e;
				if (methodReturnMessage != null)
				{
					e = methodReturnMessage.Exception;
				}
				else
				{
					e = new RemotingException(Environment.GetResourceString("Remoting_Activation_Failed"));
				}
				constructionReturnMessage = new ConstructorReturnMessage(e, null);
				((ConstructorReturnMessage)constructionReturnMessage).SetLogicalCallContext((LogicalCallContext)constructionCallMessage.Properties[Message.CallContextKey]);
			}
			return constructionReturnMessage;
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x000D186C File Offset: 0x000D086C
		internal static IConstructionReturnMessage DoServerContextActivation(IConstructionCallMessage reqMsg)
		{
			Exception e = null;
			Type activationType = reqMsg.ActivationType;
			object serverObj = ActivationServices.ActivateWithMessage(activationType, reqMsg, null, out e);
			return ActivationServices.SetupConstructionReply(serverObj, reqMsg, e);
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x000D1898 File Offset: 0x000D0898
		internal static IConstructionReturnMessage SetupConstructionReply(object serverObj, IConstructionCallMessage ctorMsg, Exception e)
		{
			IConstructionReturnMessage constructionReturnMessage;
			if (e == null)
			{
				constructionReturnMessage = new ConstructorReturnMessage((MarshalByRefObject)serverObj, null, 0, (LogicalCallContext)ctorMsg.Properties[Message.CallContextKey], ctorMsg);
			}
			else
			{
				constructionReturnMessage = new ConstructorReturnMessage(e, null);
				((ConstructorReturnMessage)constructionReturnMessage).SetLogicalCallContext((LogicalCallContext)ctorMsg.Properties[Message.CallContextKey]);
			}
			return constructionReturnMessage;
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000D18FC File Offset: 0x000D08FC
		internal static object ActivateWithMessage(Type serverType, IMessage msg, ServerIdentity srvIdToBind, out Exception e)
		{
			e = null;
			object obj = RemotingServices.AllocateUninitializedObject(serverType);
			object obj2;
			if (serverType.IsContextful)
			{
				if (msg is ConstructorCallMessage)
				{
					obj2 = ((ConstructorCallMessage)msg).GetThisPtr();
				}
				else
				{
					obj2 = null;
				}
				obj2 = RemotingServices.Wrap((ContextBoundObject)obj, obj2, false);
			}
			else
			{
				if (Thread.CurrentContext != Context.DefaultContext)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Activation_Failed"));
				}
				obj2 = obj;
			}
			IMessageSink messageSink = new StackBuilderSink(obj2);
			IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage)messageSink.SyncProcessMessage(msg);
			if (methodReturnMessage.Exception != null)
			{
				e = methodReturnMessage.Exception;
				return null;
			}
			if (serverType.IsContextful)
			{
				return RemotingServices.Wrap((ContextBoundObject)obj);
			}
			return obj;
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x000D19A0 File Offset: 0x000D09A0
		internal static void StartListeningForRemoteRequests()
		{
			ActivationServices.Startup();
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (!remotingData.ActivatorListening)
			{
				object configLock = remotingData.ConfigLock;
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.ReliableEnter(configLock, ref flag);
					if (!remotingData.ActivatorListening)
					{
						RemotingServices.MarshalInternal(Thread.GetDomain().RemotingData.ActivationListener, "RemoteActivationService.rem", typeof(IActivator));
						ServerIdentity serverIdentity = (ServerIdentity)IdentityHolder.ResolveIdentity("RemoteActivationService.rem");
						serverIdentity.SetSingletonObjectMode();
						remotingData.ActivatorListening = true;
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
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x000D1A40 File Offset: 0x000D0A40
		internal static IActivator GetActivator()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (remotingData.LocalActivator == null)
			{
				ActivationServices.Startup();
			}
			return remotingData.LocalActivator;
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000D1A6B File Offset: 0x000D0A6B
		internal static void Initialize()
		{
			ActivationServices.GetActivator();
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000D1A74 File Offset: 0x000D0A74
		internal static ContextAttribute GetGlobalAttribute()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			if (remotingData.LocalActivator == null)
			{
				ActivationServices.Startup();
			}
			return remotingData.LocalActivator;
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000D1AA0 File Offset: 0x000D0AA0
		internal static IContextAttribute[] GetContextAttributesForType(Type serverType)
		{
			if (!typeof(ContextBoundObject).IsAssignableFrom(serverType) || serverType.IsCOMObject)
			{
				return new ContextAttribute[0];
			}
			int num = 8;
			IContextAttribute[] array = new IContextAttribute[num];
			int num2 = 0;
			object[] customAttributes = serverType.GetCustomAttributes(typeof(IContextAttribute), true);
			foreach (IContextAttribute contextAttribute in customAttributes)
			{
				Type type = contextAttribute.GetType();
				bool flag = false;
				for (int j = 0; j < num2; j++)
				{
					if (type.Equals(array[j].GetType()))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					num2++;
					if (num2 > num - 1)
					{
						IContextAttribute[] array3 = new IContextAttribute[2 * num];
						Array.Copy(array, 0, array3, 0, num);
						array = array3;
						num *= 2;
					}
					array[num2 - 1] = contextAttribute;
				}
			}
			IContextAttribute[] array4 = new IContextAttribute[num2];
			Array.Copy(array, array4, num2);
			return array4;
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000D1B94 File Offset: 0x000D0B94
		internal static object ConnectIfNecessary(IConstructionCallMessage ctorMsg)
		{
			string text = (string)ctorMsg.Properties["Connect"];
			object result = null;
			if (text != null)
			{
				result = RemotingServices.Connect(ctorMsg.ActivationType, text);
			}
			return result;
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000D1BCC File Offset: 0x000D0BCC
		internal static object CheckIfConnected(RemotingProxy proxy, IConstructionCallMessage ctorMsg)
		{
			string text = (string)ctorMsg.Properties["Connect"];
			object result = null;
			if (text != null)
			{
				result = proxy.GetTransparentProxy();
			}
			return result;
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000D1BFC File Offset: 0x000D0BFC
		internal static void PushActivationAttributes(Type serverType, object[] attributes)
		{
			if (ActivationServices._attributeStack == null)
			{
				ActivationServices._attributeStack = new ActivationAttributeStack();
			}
			ActivationServices._attributeStack.Push(serverType, attributes);
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000D1C1B File Offset: 0x000D0C1B
		internal static object[] PeekActivationAttributes(Type serverType)
		{
			if (ActivationServices._attributeStack == null)
			{
				return null;
			}
			return ActivationServices._attributeStack.Peek(serverType);
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000D1C31 File Offset: 0x000D0C31
		internal static void PopActivationAttributes(Type serverType)
		{
			ActivationServices._attributeStack.Pop(serverType);
		}

		// Token: 0x04001F5F RID: 8031
		internal const string ActivationServiceURI = "RemoteActivationService.rem";

		// Token: 0x04001F60 RID: 8032
		internal const string RemoteActivateKey = "Remote";

		// Token: 0x04001F61 RID: 8033
		internal const string PermissionKey = "Permission";

		// Token: 0x04001F62 RID: 8034
		internal const string ConnectKey = "Connect";

		// Token: 0x04001F63 RID: 8035
		private static IActivator activator = null;

		// Token: 0x04001F64 RID: 8036
		private static Hashtable _proxyTable = new Hashtable();

		// Token: 0x04001F65 RID: 8037
		private static Type proxyAttributeType = typeof(ProxyAttribute);

		// Token: 0x04001F66 RID: 8038
		private static ProxyAttribute _proxyAttribute = new ProxyAttribute();

		// Token: 0x04001F67 RID: 8039
		[ThreadStatic]
		internal static ActivationAttributeStack _attributeStack;
	}
}
