using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Threading;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001C1 RID: 449
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	[ServiceBehavior(UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class CustomPeerResolverService : IPeerResolverContract
	{
		// Token: 0x06000EA5 RID: 3749 RVA: 0x00034D80 File Offset: 0x00032F80
		public CustomPeerResolverService()
		{
			this.isCleaning = false;
			this.gate = new ReaderWriterLock();
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00034E0B File Offset: 0x0003300B
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x00034E14 File Offset: 0x00033014
		public TimeSpan CleanupInterval
		{
			get
			{
				return this.cleanupInterval;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfOpened("Set CleanupInterval");
					this.cleanupInterval = value;
				}
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00034EC0 File Offset: 0x000330C0
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00034EC8 File Offset: 0x000330C8
		public TimeSpan RefreshInterval
		{
			get
			{
				return this.refreshInterval;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfOpened("Set RefreshInterval");
					this.refreshInterval = value;
				}
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00034F74 File Offset: 0x00033174
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00034F7C File Offset: 0x0003317C
		public bool ControlShape
		{
			get
			{
				return this.controlShape;
			}
			set
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfOpened("Set ControlShape");
					this.controlShape = value;
				}
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00034FC8 File Offset: 0x000331C8
		private CustomPeerResolverService.MeshEntry GetMeshEntry(string meshId)
		{
			return this.GetMeshEntry(meshId, true);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00034FD4 File Offset: 0x000331D4
		private CustomPeerResolverService.MeshEntry GetMeshEntry(string meshId, bool createIfNotExists)
		{
			CustomPeerResolverService.MeshEntry meshEntry = null;
			CustomPeerResolverService.LiteLock liteLock = null;
			try
			{
				CustomPeerResolverService.LiteLock.Acquire(out liteLock, this.gate);
				if (!this.meshId2Entry.TryGetValue(meshId, out meshEntry) && createIfNotExists)
				{
					meshEntry = new CustomPeerResolverService.MeshEntry();
					try
					{
						liteLock.UpgradeToWriterLock();
						this.meshId2Entry.Add(meshId, meshEntry);
					}
					finally
					{
						liteLock.DowngradeFromWriterLock();
					}
				}
			}
			finally
			{
				CustomPeerResolverService.LiteLock.Release(liteLock);
			}
			return meshEntry;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00035050 File Offset: 0x00033250
		public virtual RegisterResponseInfo Register(Guid clientId, string meshId, PeerNodeAddress address)
		{
			Guid guid = Guid.NewGuid();
			DateTime expires = DateTime.UtcNow + this.RefreshInterval;
			object obj = this.ThisLock;
			lock (obj)
			{
				CustomPeerResolverService.RegistrationEntry registrationEntry = new CustomPeerResolverService.RegistrationEntry(clientId, guid, meshId, expires, address);
				CustomPeerResolverService.MeshEntry meshEntry = this.GetMeshEntry(meshId);
				if (meshEntry.Service2EntryTable.ContainsKey(address.ServicePath))
				{
					PeerExceptionHelper.ThrowInvalidOperation_DuplicatePeerRegistration(address.ServicePath);
				}
				CustomPeerResolverService.LiteLock liteLock = null;
				try
				{
					if (!meshEntry.Gate.IsWriterLockHeld)
					{
						CustomPeerResolverService.LiteLock.Acquire(out liteLock, meshEntry.Gate, true);
					}
					meshEntry.EntryTable.Add(guid, registrationEntry);
					meshEntry.EntryList.Add(registrationEntry);
					meshEntry.Service2EntryTable.Add(address.ServicePath, registrationEntry);
				}
				finally
				{
					if (liteLock != null)
					{
						CustomPeerResolverService.LiteLock.Release(liteLock);
					}
				}
			}
			return new RegisterResponseInfo(guid, this.RefreshInterval);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00035148 File Offset: 0x00033348
		public virtual RegisterResponseInfo Register(RegisterInfo registerInfo)
		{
			if (registerInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("registerInfo", SR.GetString("PeerNullRegistrationInfo"));
			}
			this.ThrowIfClosed("Register");
			if (!registerInfo.HasBody() || string.IsNullOrEmpty(registerInfo.MeshId))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("registerInfo", SR.GetString("PeerInvalidMessageBody", new object[]
				{
					registerInfo
				}));
			}
			return this.Register(registerInfo.ClientId, registerInfo.MeshId, registerInfo.NodeAddress);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000351D0 File Offset: 0x000333D0
		public virtual RegisterResponseInfo Update(UpdateInfo updateInfo)
		{
			if (updateInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("updateInfo", SR.GetString("PeerNullRegistrationInfo"));
			}
			this.ThrowIfClosed("Update");
			if (!updateInfo.HasBody() || string.IsNullOrEmpty(updateInfo.MeshId) || updateInfo.NodeAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("updateInfo", SR.GetString("PeerInvalidMessageBody", new object[]
				{
					updateInfo
				}));
			}
			Guid registrationId = updateInfo.RegistrationId;
			CustomPeerResolverService.MeshEntry meshEntry = this.GetMeshEntry(updateInfo.MeshId);
			CustomPeerResolverService.LiteLock liteLock = null;
			if (updateInfo.RegistrationId == Guid.Empty || meshEntry == null)
			{
				return this.Register(updateInfo.ClientId, updateInfo.MeshId, updateInfo.NodeAddress);
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				try
				{
					CustomPeerResolverService.LiteLock.Acquire(out liteLock, meshEntry.Gate);
					CustomPeerResolverService.RegistrationEntry registrationEntry;
					if (!meshEntry.EntryTable.TryGetValue(updateInfo.RegistrationId, out registrationEntry))
					{
						try
						{
							liteLock.UpgradeToWriterLock();
							return this.Register(updateInfo.ClientId, updateInfo.MeshId, updateInfo.NodeAddress);
						}
						finally
						{
							liteLock.DowngradeFromWriterLock();
						}
					}
					CustomPeerResolverService.RegistrationEntry obj2 = registrationEntry;
					lock (obj2)
					{
						registrationEntry.Address = updateInfo.NodeAddress;
						registrationEntry.Expires = DateTime.UtcNow + this.RefreshInterval;
					}
				}
				finally
				{
					CustomPeerResolverService.LiteLock.Release(liteLock);
				}
			}
			return new RegisterResponseInfo(registrationId, this.RefreshInterval);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00035380 File Offset: 0x00033580
		public virtual ResolveResponseInfo Resolve(ResolveInfo resolveInfo)
		{
			if (resolveInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("resolveInfo", SR.GetString("PeerNullResolveInfo"));
			}
			this.ThrowIfClosed("Resolve");
			if (!resolveInfo.HasBody())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("resolveInfo", SR.GetString("PeerInvalidMessageBody", new object[]
				{
					resolveInfo
				}));
			}
			int i = 0;
			int maxAddresses = resolveInfo.MaxAddresses;
			ResolveResponseInfo resolveResponseInfo = new ResolveResponseInfo();
			List<PeerNodeAddress> list = new List<PeerNodeAddress>();
			CustomPeerResolverService.MeshEntry meshEntry = this.GetMeshEntry(resolveInfo.MeshId, false);
			if (meshEntry != null)
			{
				CustomPeerResolverService.LiteLock liteLock = null;
				try
				{
					CustomPeerResolverService.LiteLock.Acquire(out liteLock, meshEntry.Gate);
					List<CustomPeerResolverService.RegistrationEntry> entryList = meshEntry.EntryList;
					if (entryList.Count <= maxAddresses)
					{
						using (List<CustomPeerResolverService.RegistrationEntry>.Enumerator enumerator = entryList.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								CustomPeerResolverService.RegistrationEntry registrationEntry = enumerator.Current;
								list.Add(registrationEntry.Address);
							}
							goto IL_135;
						}
					}
					Random random = new Random();
					while (i < maxAddresses)
					{
						int index = random.Next(entryList.Count);
						CustomPeerResolverService.RegistrationEntry registrationEntry2 = entryList[index];
						PeerNodeAddress address = registrationEntry2.Address;
						if (!list.Contains(address))
						{
							list.Add(address);
						}
						i++;
					}
				}
				finally
				{
					CustomPeerResolverService.LiteLock.Release(liteLock);
				}
			}
			IL_135:
			resolveResponseInfo.Addresses = list.ToArray();
			return resolveResponseInfo;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000354EC File Offset: 0x000336EC
		public virtual void Unregister(UnregisterInfo unregisterInfo)
		{
			if (unregisterInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("unregisterinfo", SR.GetString("PeerNullRegistrationInfo"));
			}
			this.ThrowIfClosed("Unregister");
			if (!unregisterInfo.HasBody() || string.IsNullOrEmpty(unregisterInfo.MeshId) || unregisterInfo.RegistrationId == Guid.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("unregisterInfo", SR.GetString("PeerInvalidMessageBody", new object[]
				{
					unregisterInfo
				}));
			}
			CustomPeerResolverService.RegistrationEntry registrationEntry = null;
			CustomPeerResolverService.MeshEntry meshEntry = this.GetMeshEntry(unregisterInfo.MeshId, false);
			CustomPeerResolverService.LiteLock liteLock = null;
			try
			{
				CustomPeerResolverService.LiteLock.Acquire(out liteLock, meshEntry.Gate, true);
				if (!meshEntry.EntryTable.TryGetValue(unregisterInfo.RegistrationId, out registrationEntry))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("unregisterInfo", SR.GetString("PeerInvalidMessageBody", new object[]
					{
						unregisterInfo
					}));
				}
				meshEntry.EntryTable.Remove(unregisterInfo.RegistrationId);
				meshEntry.EntryList.Remove(registrationEntry);
				meshEntry.Service2EntryTable.Remove(registrationEntry.Address.ServicePath);
				registrationEntry.State = CustomPeerResolverService.RegistrationState.Deleted;
			}
			finally
			{
				CustomPeerResolverService.LiteLock.Release(liteLock);
			}
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003561C File Offset: 0x0003381C
		public virtual RefreshResponseInfo Refresh(RefreshInfo refreshInfo)
		{
			if (refreshInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("refreshInfo", SR.GetString("PeerNullRefreshInfo"));
			}
			this.ThrowIfClosed("Refresh");
			if (!refreshInfo.HasBody() || string.IsNullOrEmpty(refreshInfo.MeshId) || refreshInfo.RegistrationId == Guid.Empty)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("refreshInfo", SR.GetString("PeerInvalidMessageBody", new object[]
				{
					refreshInfo
				}));
			}
			RefreshResult result = RefreshResult.RegistrationNotFound;
			CustomPeerResolverService.RegistrationEntry registrationEntry = null;
			CustomPeerResolverService.MeshEntry meshEntry = this.GetMeshEntry(refreshInfo.MeshId, false);
			CustomPeerResolverService.LiteLock liteLock = null;
			if (meshEntry != null)
			{
				try
				{
					CustomPeerResolverService.LiteLock.Acquire(out liteLock, meshEntry.Gate);
					if (!meshEntry.EntryTable.TryGetValue(refreshInfo.RegistrationId, out registrationEntry))
					{
						return new RefreshResponseInfo(this.RefreshInterval, result);
					}
					CustomPeerResolverService.RegistrationEntry obj = registrationEntry;
					lock (obj)
					{
						if (registrationEntry.State == CustomPeerResolverService.RegistrationState.OK)
						{
							registrationEntry.Expires = DateTime.UtcNow + this.RefreshInterval;
							result = RefreshResult.Success;
						}
					}
				}
				finally
				{
					CustomPeerResolverService.LiteLock.Release(liteLock);
				}
			}
			return new RefreshResponseInfo(this.RefreshInterval, result);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00035754 File Offset: 0x00033954
		public virtual ServiceSettingsResponseInfo GetServiceSettings()
		{
			this.ThrowIfClosed("GetServiceSettings");
			return new ServiceSettingsResponseInfo(this.ControlShape);
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003577C File Offset: 0x0003397C
		public virtual void Open()
		{
			this.ThrowIfOpened("Open");
			if (this.refreshInterval <= TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("RefreshInterval", SR.GetString("RefreshIntervalMustBeGreaterThanZero", new object[]
				{
					this.refreshInterval
				}));
			}
			if (this.CleanupInterval <= TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("CleanupInterval", SR.GetString("CleanupIntervalMustBeGreaterThanZero", new object[]
				{
					this.cleanupInterval
				}));
			}
			this.timer = new IOThreadTimer(new Action<object>(this.CleanupActivity), null, false);
			this.timer.Set(this.CleanupInterval);
			this.opened = true;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00035846 File Offset: 0x00033A46
		public virtual void Close()
		{
			this.ThrowIfClosed("Close");
			this.timer.Cancel();
			this.opened = false;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00035868 File Offset: 0x00033A68
		internal virtual void CleanupActivity(object state)
		{
			if (!this.opened)
			{
				return;
			}
			if (!this.isCleaning)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.isCleaning)
					{
						this.isCleaning = true;
						try
						{
							ICollection<string> collection = null;
							CustomPeerResolverService.LiteLock liteLock = null;
							try
							{
								CustomPeerResolverService.LiteLock.Acquire(out liteLock, this.gate);
								collection = this.meshId2Entry.Keys;
							}
							finally
							{
								CustomPeerResolverService.LiteLock.Release(liteLock);
							}
							foreach (string meshId in collection)
							{
								CustomPeerResolverService.MeshEntry meshEntry = this.GetMeshEntry(meshId);
								this.CleanupMeshEntry(meshEntry);
							}
						}
						finally
						{
							this.isCleaning = false;
							if (this.opened)
							{
								this.timer.Set(this.CleanupInterval);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00035974 File Offset: 0x00033B74
		private void CleanupMeshEntry(CustomPeerResolverService.MeshEntry meshEntry)
		{
			List<Guid> list = new List<Guid>();
			if (!this.opened)
			{
				return;
			}
			CustomPeerResolverService.LiteLock liteLock = null;
			try
			{
				CustomPeerResolverService.LiteLock.Acquire(out liteLock, meshEntry.Gate, true);
				foreach (KeyValuePair<Guid, CustomPeerResolverService.RegistrationEntry> keyValuePair in meshEntry.EntryTable)
				{
					if (keyValuePair.Value.Expires <= DateTime.UtcNow || keyValuePair.Value.State == CustomPeerResolverService.RegistrationState.Deleted)
					{
						list.Add(keyValuePair.Key);
						meshEntry.EntryList.Remove(keyValuePair.Value);
						meshEntry.Service2EntryTable.Remove(keyValuePair.Value.Address.ServicePath);
					}
				}
				foreach (Guid key in list)
				{
					meshEntry.EntryTable.Remove(key);
				}
			}
			finally
			{
				CustomPeerResolverService.LiteLock.Release(liteLock);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00035A9C File Offset: 0x00033C9C
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00035AA4 File Offset: 0x00033CA4
		private void ThrowIfOpened(string operation)
		{
			if (this.opened)
			{
				PeerExceptionHelper.ThrowInvalidOperation_NotValidWhenOpen(operation);
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00035AB4 File Offset: 0x00033CB4
		private void ThrowIfClosed(string operation)
		{
			if (!this.opened)
			{
				PeerExceptionHelper.ThrowInvalidOperation_NotValidWhenClosed(operation);
			}
		}

		// Token: 0x04001776 RID: 6006
		private Dictionary<string, CustomPeerResolverService.MeshEntry> meshId2Entry = new Dictionary<string, CustomPeerResolverService.MeshEntry>();

		// Token: 0x04001777 RID: 6007
		private ReaderWriterLock gate;

		// Token: 0x04001778 RID: 6008
		private TimeSpan timeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x04001779 RID: 6009
		private TimeSpan cleanupInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400177A RID: 6010
		private TimeSpan refreshInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x0400177B RID: 6011
		private bool controlShape;

		// Token: 0x0400177C RID: 6012
		private bool isCleaning;

		// Token: 0x0400177D RID: 6013
		private IOThreadTimer timer;

		// Token: 0x0400177E RID: 6014
		private object thisLock = new object();

		// Token: 0x0400177F RID: 6015
		private bool opened;

		// Token: 0x04001780 RID: 6016
		private TimeSpan LockWait = TimeSpan.FromSeconds(5.0);

		// Token: 0x02000AFE RID: 2814
		internal enum RegistrationState
		{
			// Token: 0x04003F71 RID: 16241
			OK,
			// Token: 0x04003F72 RID: 16242
			Deleted
		}

		// Token: 0x02000AFF RID: 2815
		internal class RegistrationEntry
		{
			// Token: 0x06006F38 RID: 28472 RVA: 0x0019D70A File Offset: 0x0019B90A
			public RegistrationEntry(Guid clientId, Guid registrationId, string meshId, DateTime expires, PeerNodeAddress address)
			{
				this.ClientId = clientId;
				this.RegistrationId = registrationId;
				this.MeshId = meshId;
				this.Expires = expires;
				this.Address = address;
				this.State = CustomPeerResolverService.RegistrationState.OK;
			}

			// Token: 0x170019F0 RID: 6640
			// (get) Token: 0x06006F39 RID: 28473 RVA: 0x0019D73E File Offset: 0x0019B93E
			// (set) Token: 0x06006F3A RID: 28474 RVA: 0x0019D746 File Offset: 0x0019B946
			public Guid ClientId
			{
				get
				{
					return this.clientId;
				}
				set
				{
					this.clientId = value;
				}
			}

			// Token: 0x170019F1 RID: 6641
			// (get) Token: 0x06006F3B RID: 28475 RVA: 0x0019D74F File Offset: 0x0019B94F
			// (set) Token: 0x06006F3C RID: 28476 RVA: 0x0019D757 File Offset: 0x0019B957
			public Guid RegistrationId
			{
				get
				{
					return this.registrationId;
				}
				set
				{
					this.registrationId = value;
				}
			}

			// Token: 0x170019F2 RID: 6642
			// (get) Token: 0x06006F3D RID: 28477 RVA: 0x0019D760 File Offset: 0x0019B960
			// (set) Token: 0x06006F3E RID: 28478 RVA: 0x0019D768 File Offset: 0x0019B968
			public string MeshId
			{
				get
				{
					return this.meshId;
				}
				set
				{
					this.meshId = value;
				}
			}

			// Token: 0x170019F3 RID: 6643
			// (get) Token: 0x06006F3F RID: 28479 RVA: 0x0019D771 File Offset: 0x0019B971
			// (set) Token: 0x06006F40 RID: 28480 RVA: 0x0019D779 File Offset: 0x0019B979
			public DateTime Expires
			{
				get
				{
					return this.expires;
				}
				set
				{
					this.expires = value;
				}
			}

			// Token: 0x170019F4 RID: 6644
			// (get) Token: 0x06006F41 RID: 28481 RVA: 0x0019D782 File Offset: 0x0019B982
			// (set) Token: 0x06006F42 RID: 28482 RVA: 0x0019D78A File Offset: 0x0019B98A
			public PeerNodeAddress Address
			{
				get
				{
					return this.address;
				}
				set
				{
					this.address = value;
				}
			}

			// Token: 0x170019F5 RID: 6645
			// (get) Token: 0x06006F43 RID: 28483 RVA: 0x0019D793 File Offset: 0x0019B993
			// (set) Token: 0x06006F44 RID: 28484 RVA: 0x0019D79B File Offset: 0x0019B99B
			public CustomPeerResolverService.RegistrationState State
			{
				get
				{
					return this.state;
				}
				set
				{
					this.state = value;
				}
			}

			// Token: 0x04003F73 RID: 16243
			private Guid clientId;

			// Token: 0x04003F74 RID: 16244
			private Guid registrationId;

			// Token: 0x04003F75 RID: 16245
			private string meshId;

			// Token: 0x04003F76 RID: 16246
			private DateTime expires;

			// Token: 0x04003F77 RID: 16247
			private PeerNodeAddress address;

			// Token: 0x04003F78 RID: 16248
			private CustomPeerResolverService.RegistrationState state;
		}

		// Token: 0x02000B00 RID: 2816
		internal class LiteLock
		{
			// Token: 0x06006F45 RID: 28485 RVA: 0x0019D7A4 File Offset: 0x0019B9A4
			private LiteLock(ReaderWriterLock locker, bool forWrite)
			{
				this.locker = locker;
				this.forWrite = forWrite;
			}

			// Token: 0x06006F46 RID: 28486 RVA: 0x0019D7CE File Offset: 0x0019B9CE
			public static void Acquire(out CustomPeerResolverService.LiteLock liteLock, ReaderWriterLock locker)
			{
				CustomPeerResolverService.LiteLock.Acquire(out liteLock, locker, false);
			}

			// Token: 0x06006F47 RID: 28487 RVA: 0x0019D7D8 File Offset: 0x0019B9D8
			public static void Acquire(out CustomPeerResolverService.LiteLock liteLock, ReaderWriterLock locker, bool forWrite)
			{
				CustomPeerResolverService.LiteLock liteLock2 = new CustomPeerResolverService.LiteLock(locker, forWrite);
				try
				{
				}
				finally
				{
					if (forWrite)
					{
						locker.AcquireWriterLock(liteLock2.timeout);
					}
					else
					{
						locker.AcquireReaderLock(liteLock2.timeout);
					}
					liteLock = liteLock2;
				}
			}

			// Token: 0x06006F48 RID: 28488 RVA: 0x0019D820 File Offset: 0x0019BA20
			public static void Release(CustomPeerResolverService.LiteLock liteLock)
			{
				if (liteLock == null)
				{
					return;
				}
				if (liteLock.forWrite)
				{
					liteLock.locker.ReleaseWriterLock();
					return;
				}
				liteLock.locker.ReleaseReaderLock();
			}

			// Token: 0x06006F49 RID: 28489 RVA: 0x0019D848 File Offset: 0x0019BA48
			public void UpgradeToWriterLock()
			{
				try
				{
				}
				finally
				{
					this.lc = this.locker.UpgradeToWriterLock(this.timeout);
					this.upgraded = true;
				}
			}

			// Token: 0x06006F4A RID: 28490 RVA: 0x0019D888 File Offset: 0x0019BA88
			public void DowngradeFromWriterLock()
			{
				if (this.upgraded)
				{
					this.locker.DowngradeFromWriterLock(ref this.lc);
					this.upgraded = false;
				}
			}

			// Token: 0x04003F79 RID: 16249
			private bool forWrite;

			// Token: 0x04003F7A RID: 16250
			private bool upgraded;

			// Token: 0x04003F7B RID: 16251
			private ReaderWriterLock locker;

			// Token: 0x04003F7C RID: 16252
			private TimeSpan timeout = TimeSpan.FromMinutes(1.0);

			// Token: 0x04003F7D RID: 16253
			private LockCookie lc;
		}

		// Token: 0x02000B01 RID: 2817
		internal class MeshEntry
		{
			// Token: 0x06006F4B RID: 28491 RVA: 0x0019D8AA File Offset: 0x0019BAAA
			internal MeshEntry()
			{
				this.EntryTable = new Dictionary<Guid, CustomPeerResolverService.RegistrationEntry>();
				this.Service2EntryTable = new Dictionary<string, CustomPeerResolverService.RegistrationEntry>();
				this.EntryList = new List<CustomPeerResolverService.RegistrationEntry>();
				this.Gate = new ReaderWriterLock();
			}

			// Token: 0x170019F6 RID: 6646
			// (get) Token: 0x06006F4C RID: 28492 RVA: 0x0019D8DE File Offset: 0x0019BADE
			// (set) Token: 0x06006F4D RID: 28493 RVA: 0x0019D8E6 File Offset: 0x0019BAE6
			public Dictionary<Guid, CustomPeerResolverService.RegistrationEntry> EntryTable
			{
				get
				{
					return this.entryTable;
				}
				set
				{
					this.entryTable = value;
				}
			}

			// Token: 0x170019F7 RID: 6647
			// (get) Token: 0x06006F4E RID: 28494 RVA: 0x0019D8EF File Offset: 0x0019BAEF
			// (set) Token: 0x06006F4F RID: 28495 RVA: 0x0019D8F7 File Offset: 0x0019BAF7
			public Dictionary<string, CustomPeerResolverService.RegistrationEntry> Service2EntryTable
			{
				get
				{
					return this.service2EntryTable;
				}
				set
				{
					this.service2EntryTable = value;
				}
			}

			// Token: 0x170019F8 RID: 6648
			// (get) Token: 0x06006F50 RID: 28496 RVA: 0x0019D900 File Offset: 0x0019BB00
			// (set) Token: 0x06006F51 RID: 28497 RVA: 0x0019D908 File Offset: 0x0019BB08
			public List<CustomPeerResolverService.RegistrationEntry> EntryList
			{
				get
				{
					return this.entryList;
				}
				set
				{
					this.entryList = value;
				}
			}

			// Token: 0x170019F9 RID: 6649
			// (get) Token: 0x06006F52 RID: 28498 RVA: 0x0019D911 File Offset: 0x0019BB11
			// (set) Token: 0x06006F53 RID: 28499 RVA: 0x0019D919 File Offset: 0x0019BB19
			public ReaderWriterLock Gate
			{
				get
				{
					return this.gate;
				}
				set
				{
					this.gate = value;
				}
			}

			// Token: 0x04003F7E RID: 16254
			private Dictionary<Guid, CustomPeerResolverService.RegistrationEntry> entryTable;

			// Token: 0x04003F7F RID: 16255
			private Dictionary<string, CustomPeerResolverService.RegistrationEntry> service2EntryTable;

			// Token: 0x04003F80 RID: 16256
			private List<CustomPeerResolverService.RegistrationEntry> entryList;

			// Token: 0x04003F81 RID: 16257
			private ReaderWriterLock gate;
		}
	}
}
