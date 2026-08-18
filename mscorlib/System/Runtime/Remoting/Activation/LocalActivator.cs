using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200069E RID: 1694
	internal class LocalActivator : ContextAttribute, IActivator
	{
		// Token: 0x06003D4B RID: 15691 RVA: 0x000D1D31 File Offset: 0x000D0D31
		internal LocalActivator() : base("RemoteActivationService.rem")
		{
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x000D1D40 File Offset: 0x000D0D40
		public override bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (RemotingConfigHandler.Info == null)
			{
				return true;
			}
			WellKnownClientTypeEntry wellKnownClientTypeEntry = RemotingConfigHandler.IsWellKnownClientType(ctorMsg.ActivationType);
			string text = (wellKnownClientTypeEntry == null) ? null : wellKnownClientTypeEntry.ObjectUrl;
			if (text != null)
			{
				ctorMsg.Properties["Connect"] = text;
				return false;
			}
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfigHandler.IsRemotelyActivatedClientType(ctorMsg.ActivationType);
			string text2 = null;
			if (activatedClientTypeEntry == null)
			{
				object[] callSiteActivationAttributes = ctorMsg.CallSiteActivationAttributes;
				if (callSiteActivationAttributes != null)
				{
					for (int i = 0; i < callSiteActivationAttributes.Length; i++)
					{
						UrlAttribute urlAttribute = callSiteActivationAttributes[i] as UrlAttribute;
						if (urlAttribute != null)
						{
							text2 = urlAttribute.UrlValue;
						}
					}
				}
				if (text2 == null)
				{
					return true;
				}
			}
			else
			{
				text2 = activatedClientTypeEntry.ApplicationUrl;
			}
			string value;
			if (!text2.EndsWith("/", StringComparison.Ordinal))
			{
				value = text2 + "/RemoteActivationService.rem";
			}
			else
			{
				value = text2 + "RemoteActivationService.rem";
			}
			ctorMsg.Properties["Remote"] = value;
			return false;
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x000D1E1C File Offset: 0x000D0E1C
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg.Properties.Contains("Remote"))
			{
				string remActivatorURL = (string)ctorMsg.Properties["Remote"];
				AppDomainLevelActivator appDomainLevelActivator = new AppDomainLevelActivator(remActivatorURL);
				IActivator activator = ctorMsg.Activator;
				if (activator.Level < ActivatorLevel.AppDomain)
				{
					appDomainLevelActivator.NextActivator = activator;
					ctorMsg.Activator = appDomainLevelActivator;
					return;
				}
				if (activator.NextActivator == null)
				{
					activator.NextActivator = appDomainLevelActivator;
					return;
				}
				while (activator.NextActivator.Level >= ActivatorLevel.AppDomain)
				{
					activator = activator.NextActivator;
				}
				appDomainLevelActivator.NextActivator = activator.NextActivator;
				activator.NextActivator = appDomainLevelActivator;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000D1EB1 File Offset: 0x000D0EB1
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x000D1EB4 File Offset: 0x000D0EB4
		public virtual IActivator NextActivator
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000D1EBB File Offset: 0x000D0EBB
		public virtual ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x000D1EC0 File Offset: 0x000D0EC0
		private static MethodBase GetMethodBase(IConstructionCallMessage msg)
		{
			MethodBase methodBase = msg.MethodBase;
			if (methodBase == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), new object[]
				{
					msg.MethodName,
					msg.TypeName
				}));
			}
			return methodBase;
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000D1F0C File Offset: 0x000D0F0C
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (ctorMsg.Properties.Contains("Remote"))
			{
				return LocalActivator.DoRemoteActivation(ctorMsg);
			}
			if (ctorMsg.Properties.Contains("Permission"))
			{
				Type activationType = ctorMsg.ActivationType;
				object[] activationAttributes = null;
				if (activationType.IsContextful)
				{
					IList contextProperties = ctorMsg.ContextProperties;
					if (contextProperties != null && contextProperties.Count > 0)
					{
						RemotePropertyHolderAttribute remotePropertyHolderAttribute = new RemotePropertyHolderAttribute(contextProperties);
						activationAttributes = new object[]
						{
							remotePropertyHolderAttribute
						};
					}
				}
				MethodBase methodBase = LocalActivator.GetMethodBase(ctorMsg);
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(methodBase);
				object[] args = Message.CoerceArgs(ctorMsg, reflectionCachedData.Parameters);
				object obj = Activator.CreateInstance(activationType, args, activationAttributes);
				if (RemotingServices.IsClientProxy(obj))
				{
					RedirectionProxy redirectionProxy = new RedirectionProxy((MarshalByRefObject)obj, activationType);
					RemotingServices.MarshalInternal(redirectionProxy, null, activationType);
					obj = redirectionProxy;
				}
				return ActivationServices.SetupConstructionReply(obj, ctorMsg, null);
			}
			return ctorMsg.Activator.Activate(ctorMsg);
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000D1FF4 File Offset: 0x000D0FF4
		internal static IConstructionReturnMessage DoRemoteActivation(IConstructionCallMessage ctorMsg)
		{
			IActivator activator = null;
			string url = (string)ctorMsg.Properties["Remote"];
			try
			{
				activator = (IActivator)RemotingServices.Connect(typeof(IActivator), url);
			}
			catch (Exception ex)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_ConnectFailed"), new object[]
				{
					ex
				}));
			}
			ctorMsg.Properties.Remove("Remote");
			return activator.Activate(ctorMsg);
		}
	}
}
