using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
	// Token: 0x0200075C RID: 1884
	[ComVisible(true)]
	public static class RemotingConfiguration
	{
		// Token: 0x06004303 RID: 17155 RVA: 0x000E553C File Offset: 0x000E453C
		[Obsolete("Use System.Runtime.Remoting.RemotingConfiguration.Configure(string fileName, bool ensureSecurity) instead.", false)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void Configure(string filename)
		{
			RemotingConfiguration.Configure(filename, false);
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x000E5545 File Offset: 0x000E4545
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void Configure(string filename, bool ensureSecurity)
		{
			RemotingConfigHandler.DoConfiguration(filename, ensureSecurity);
			RemotingServices.InternalSetRemoteActivationConfigured();
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x000E5553 File Offset: 0x000E4553
		// (set) Token: 0x06004306 RID: 17158 RVA: 0x000E5563 File Offset: 0x000E4563
		public static string ApplicationName
		{
			get
			{
				if (!RemotingConfigHandler.HasApplicationNameBeenSet())
				{
					return null;
				}
				return RemotingConfigHandler.ApplicationName;
			}
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				RemotingConfigHandler.ApplicationName = value;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x000E556B File Offset: 0x000E456B
		public static string ApplicationId
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
			get
			{
				return Identity.AppDomainUniqueId;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004308 RID: 17160 RVA: 0x000E5572 File Offset: 0x000E4572
		public static string ProcessId
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
			get
			{
				return Identity.ProcessGuid;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x000E5579 File Offset: 0x000E4579
		// (set) Token: 0x0600430A RID: 17162 RVA: 0x000E5580 File Offset: 0x000E4580
		public static CustomErrorsModes CustomErrorsMode
		{
			get
			{
				return RemotingConfigHandler.CustomErrorsMode;
			}
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
			set
			{
				RemotingConfigHandler.CustomErrorsMode = value;
			}
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x000E5588 File Offset: 0x000E4588
		public static bool CustomErrorsEnabled(bool isLocalRequest)
		{
			switch (RemotingConfiguration.CustomErrorsMode)
			{
			case CustomErrorsModes.On:
				return true;
			case CustomErrorsModes.Off:
				return false;
			case CustomErrorsModes.RemoteOnly:
				return !isLocalRequest;
			default:
				return true;
			}
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000E55BC File Offset: 0x000E45BC
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedServiceType(Type type)
		{
			ActivatedServiceTypeEntry entry = new ActivatedServiceTypeEntry(type);
			RemotingConfiguration.RegisterActivatedServiceType(entry);
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000E55D6 File Offset: 0x000E45D6
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			RemotingConfigHandler.RegisterActivatedServiceType(entry);
			if (!RemotingConfiguration.s_ListeningForActivationRequests)
			{
				RemotingConfiguration.s_ListeningForActivationRequests = true;
				ActivationServices.StartListeningForRemoteRequests();
			}
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x000E55F0 File Offset: 0x000E45F0
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
		{
			WellKnownServiceTypeEntry entry = new WellKnownServiceTypeEntry(type, objectUri, mode);
			RemotingConfiguration.RegisterWellKnownServiceType(entry);
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x000E560C File Offset: 0x000E460C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			RemotingConfigHandler.RegisterWellKnownServiceType(entry);
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x000E5614 File Offset: 0x000E4614
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedClientType(Type type, string appUrl)
		{
			ActivatedClientTypeEntry entry = new ActivatedClientTypeEntry(type, appUrl);
			RemotingConfiguration.RegisterActivatedClientType(entry);
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x000E562F File Offset: 0x000E462F
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			RemotingConfigHandler.RegisterActivatedClientType(entry);
			RemotingServices.InternalSetRemoteActivationConfigured();
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x000E563C File Offset: 0x000E463C
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownClientType(Type type, string objectUrl)
		{
			WellKnownClientTypeEntry entry = new WellKnownClientTypeEntry(type, objectUrl);
			RemotingConfiguration.RegisterWellKnownClientType(entry);
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x000E5657 File Offset: 0x000E4657
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			RemotingConfigHandler.RegisterWellKnownClientType(entry);
			RemotingServices.InternalSetRemoteActivationConfigured();
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x000E5664 File Offset: 0x000E4664
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
		{
			return RemotingConfigHandler.GetRegisteredActivatedServiceTypes();
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x000E566B File Offset: 0x000E466B
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
		{
			return RemotingConfigHandler.GetRegisteredWellKnownServiceTypes();
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x000E5672 File Offset: 0x000E4672
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
		{
			return RemotingConfigHandler.GetRegisteredActivatedClientTypes();
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x000E5679 File Offset: 0x000E4679
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
		{
			return RemotingConfigHandler.GetRegisteredWellKnownClientTypes();
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x000E5680 File Offset: 0x000E4680
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
		{
			return RemotingConfigHandler.IsRemotelyActivatedClientType(svrType);
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x000E5688 File Offset: 0x000E4688
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.IsRemotelyActivatedClientType(typeName, assemblyName);
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x000E5691 File Offset: 0x000E4691
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
		{
			return RemotingConfigHandler.IsWellKnownClientType(svrType);
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x000E5699 File Offset: 0x000E4699
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
		{
			return RemotingConfigHandler.IsWellKnownClientType(typeName, assemblyName);
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x000E56A2 File Offset: 0x000E46A2
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		public static bool IsActivationAllowed(Type svrType)
		{
			return RemotingConfigHandler.IsActivationAllowed(svrType);
		}

		// Token: 0x040021C3 RID: 8643
		private static bool s_ListeningForActivationRequests;
	}
}
