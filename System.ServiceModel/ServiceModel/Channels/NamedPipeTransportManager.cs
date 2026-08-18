using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000845 RID: 2117
	internal abstract class NamedPipeTransportManager : ConnectionOrientedTransportManager<NamedPipeChannelListener>, ITransportManagerRegistration
	{
		// Token: 0x06004F0B RID: 20235 RVA: 0x0011FB18 File Offset: 0x0011DD18
		protected NamedPipeTransportManager(Uri listenUri)
		{
			this.listenUri = listenUri;
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x0011FB27 File Offset: 0x0011DD27
		protected void SetAllowedUsers(List<SecurityIdentifier> allowedUsers)
		{
			this.allowedUsers = allowedUsers;
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x0011FB30 File Offset: 0x0011DD30
		protected void SetHostNameComparisonMode(HostNameComparisonMode hostNameComparisonMode)
		{
			this.hostNameComparisonMode = hostNameComparisonMode;
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x0011FB39 File Offset: 0x0011DD39
		internal List<SecurityIdentifier> AllowedUsers
		{
			get
			{
				return this.allowedUsers;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06004F0F RID: 20239 RVA: 0x0011FB41 File Offset: 0x0011DD41
		// (set) Token: 0x06004F10 RID: 20240 RVA: 0x0011FB4C File Offset: 0x0011DD4C
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.hostNameComparisonMode;
			}
			protected set
			{
				HostNameComparisonModeHelper.Validate(value);
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					base.ThrowIfOpen();
					this.hostNameComparisonMode = value;
				}
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06004F11 RID: 20241 RVA: 0x0011FB9C File Offset: 0x0011DD9C
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06004F12 RID: 20242 RVA: 0x0011FBA4 File Offset: 0x0011DDA4
		internal override string Scheme
		{
			get
			{
				return Uri.UriSchemeNetPipe;
			}
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x0011FBAB File Offset: 0x0011DDAB
		private bool AreAllowedUsersEqual(List<SecurityIdentifier> otherAllowedUsers)
		{
			return this.allowedUsers == otherAllowedUsers || (NamedPipeTransportManager.IsSubset(this.allowedUsers, otherAllowedUsers) && NamedPipeTransportManager.IsSubset(otherAllowedUsers, this.allowedUsers));
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x0011FBD4 File Offset: 0x0011DDD4
		protected virtual bool IsCompatible(NamedPipeChannelListener channelListener)
		{
			return channelListener.InheritBaseAddressSettings || (base.IsCompatible(channelListener) && this.AreAllowedUsersEqual(channelListener.AllowedUsers) && this.HostNameComparisonMode == channelListener.HostNameComparisonMode);
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x0011FC08 File Offset: 0x0011DE08
		private static bool IsSubset(List<SecurityIdentifier> users1, List<SecurityIdentifier> users2)
		{
			if (users1 == null)
			{
				return true;
			}
			foreach (SecurityIdentifier item in users1)
			{
				if (!users2.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x0011FC64 File Offset: 0x0011DE64
		internal override void OnClose(TimeSpan timeout)
		{
			this.Cleanup();
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x0011FC6C File Offset: 0x0011DE6C
		internal override void OnAbort()
		{
			this.Cleanup();
			base.OnAbort();
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x0011FC7A File Offset: 0x0011DE7A
		private void Cleanup()
		{
			NamedPipeChannelListener.StaticTransportManagerTable.UnregisterUri(this.ListenUri, this.HostNameComparisonMode);
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x0011FC92 File Offset: 0x0011DE92
		protected virtual void OnSelecting(NamedPipeChannelListener channelListener)
		{
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x0011FC94 File Offset: 0x0011DE94
		IList<TransportManager> ITransportManagerRegistration.Select(TransportChannelListener channelListener)
		{
			this.OnSelecting((NamedPipeChannelListener)channelListener);
			IList<TransportManager> list = null;
			if (this.IsCompatible((NamedPipeChannelListener)channelListener))
			{
				list = new List<TransportManager>();
				list.Add(this);
			}
			return list;
		}

		// Token: 0x04003112 RID: 12562
		private List<SecurityIdentifier> allowedUsers;

		// Token: 0x04003113 RID: 12563
		private HostNameComparisonMode hostNameComparisonMode;

		// Token: 0x04003114 RID: 12564
		private Uri listenUri;
	}
}
