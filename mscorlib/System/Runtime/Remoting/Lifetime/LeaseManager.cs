using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x0200070B RID: 1803
	internal class LeaseManager
	{
		// Token: 0x06004016 RID: 16406 RVA: 0x000DA354 File Offset: 0x000D9354
		internal static bool IsInitialized()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			LeaseManager leaseManager = remotingData.LeaseManager;
			return leaseManager != null;
		}

		// Token: 0x06004017 RID: 16407 RVA: 0x000DA37C File Offset: 0x000D937C
		internal static LeaseManager GetLeaseManager(TimeSpan pollTime)
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			LeaseManager leaseManager = remotingData.LeaseManager;
			if (leaseManager == null)
			{
				lock (remotingData)
				{
					if (remotingData.LeaseManager == null)
					{
						remotingData.LeaseManager = new LeaseManager(pollTime);
					}
					leaseManager = remotingData.LeaseManager;
				}
			}
			return leaseManager;
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x000DA3DC File Offset: 0x000D93DC
		internal static LeaseManager GetLeaseManager()
		{
			DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
			return remotingData.LeaseManager;
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x000DA3FC File Offset: 0x000D93FC
		private LeaseManager(TimeSpan pollTime)
		{
			this.pollTime = pollTime;
			this.leaseTimeAnalyzerDelegate = new TimerCallback(this.LeaseTimeAnalyzer);
			this.waitHandle = new AutoResetEvent(false);
			this.leaseTimer = new Timer(this.leaseTimeAnalyzerDelegate, null, -1, -1);
			this.leaseTimer.Change((int)pollTime.TotalMilliseconds, -1);
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x000DA484 File Offset: 0x000D9484
		internal void ChangePollTime(TimeSpan pollTime)
		{
			this.pollTime = pollTime;
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x000DA490 File Offset: 0x000D9490
		internal void ActivateLease(Lease lease)
		{
			lock (this.leaseToTimeTable)
			{
				this.leaseToTimeTable[lease] = lease.leaseTime;
			}
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x000DA4DC File Offset: 0x000D94DC
		internal void DeleteLease(Lease lease)
		{
			lock (this.leaseToTimeTable)
			{
				this.leaseToTimeTable.Remove(lease);
			}
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x000DA51C File Offset: 0x000D951C
		[Conditional("_LOGGING")]
		internal void DumpLeases(Lease[] leases)
		{
			for (int i = 0; i < leases.Length; i++)
			{
			}
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x000DA538 File Offset: 0x000D9538
		internal ILease GetLease(MarshalByRefObject obj)
		{
			bool flag = true;
			Identity identity = MarshalByRefObject.GetIdentity(obj, out flag);
			if (identity == null)
			{
				return null;
			}
			return identity.Lease;
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x000DA55C File Offset: 0x000D955C
		internal void ChangedLeaseTime(Lease lease, DateTime newTime)
		{
			lock (this.leaseToTimeTable)
			{
				this.leaseToTimeTable[lease] = newTime;
			}
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000DA5A4 File Offset: 0x000D95A4
		internal void RegisterSponsorCall(Lease lease, object sponsorId, TimeSpan sponsorshipTimeOut)
		{
			lock (this.sponsorTable)
			{
				DateTime sponsorWaitTime = DateTime.UtcNow.Add(sponsorshipTimeOut);
				this.sponsorTable[sponsorId] = new LeaseManager.SponsorInfo(lease, sponsorId, sponsorWaitTime);
			}
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x000DA5FC File Offset: 0x000D95FC
		internal void DeleteSponsor(object sponsorId)
		{
			lock (this.sponsorTable)
			{
				this.sponsorTable.Remove(sponsorId);
			}
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000DA63C File Offset: 0x000D963C
		private void LeaseTimeAnalyzer(object state)
		{
			DateTime utcNow = DateTime.UtcNow;
			lock (this.leaseToTimeTable)
			{
				IDictionaryEnumerator enumerator = this.leaseToTimeTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DateTime dateTime = (DateTime)enumerator.Value;
					Lease value = (Lease)enumerator.Key;
					if (dateTime.CompareTo(utcNow) < 0)
					{
						this.tempObjects.Add(value);
					}
				}
				for (int i = 0; i < this.tempObjects.Count; i++)
				{
					Lease key = (Lease)this.tempObjects[i];
					this.leaseToTimeTable.Remove(key);
				}
			}
			for (int j = 0; j < this.tempObjects.Count; j++)
			{
				Lease lease = (Lease)this.tempObjects[j];
				if (lease != null)
				{
					lease.LeaseExpired(utcNow);
				}
			}
			this.tempObjects.Clear();
			lock (this.sponsorTable)
			{
				IDictionaryEnumerator enumerator2 = this.sponsorTable.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					object key2 = enumerator2.Key;
					LeaseManager.SponsorInfo sponsorInfo = (LeaseManager.SponsorInfo)enumerator2.Value;
					if (sponsorInfo.sponsorWaitTime.CompareTo(utcNow) < 0)
					{
						this.tempObjects.Add(sponsorInfo);
					}
				}
				for (int k = 0; k < this.tempObjects.Count; k++)
				{
					LeaseManager.SponsorInfo sponsorInfo2 = (LeaseManager.SponsorInfo)this.tempObjects[k];
					this.sponsorTable.Remove(sponsorInfo2.sponsorId);
				}
			}
			for (int l = 0; l < this.tempObjects.Count; l++)
			{
				LeaseManager.SponsorInfo sponsorInfo3 = (LeaseManager.SponsorInfo)this.tempObjects[l];
				if (sponsorInfo3 != null && sponsorInfo3.lease != null)
				{
					sponsorInfo3.lease.SponsorTimeout(sponsorInfo3.sponsorId);
					this.tempObjects[l] = null;
				}
			}
			this.tempObjects.Clear();
			this.leaseTimer.Change((int)this.pollTime.TotalMilliseconds, -1);
		}

		// Token: 0x04002058 RID: 8280
		private Hashtable leaseToTimeTable = new Hashtable();

		// Token: 0x04002059 RID: 8281
		private Hashtable sponsorTable = new Hashtable();

		// Token: 0x0400205A RID: 8282
		private TimeSpan pollTime;

		// Token: 0x0400205B RID: 8283
		private AutoResetEvent waitHandle;

		// Token: 0x0400205C RID: 8284
		private TimerCallback leaseTimeAnalyzerDelegate;

		// Token: 0x0400205D RID: 8285
		private volatile Timer leaseTimer;

		// Token: 0x0400205E RID: 8286
		private ArrayList tempObjects = new ArrayList(10);

		// Token: 0x0200070C RID: 1804
		internal class SponsorInfo
		{
			// Token: 0x06004023 RID: 16419 RVA: 0x000DA870 File Offset: 0x000D9870
			internal SponsorInfo(Lease lease, object sponsorId, DateTime sponsorWaitTime)
			{
				this.lease = lease;
				this.sponsorId = sponsorId;
				this.sponsorWaitTime = sponsorWaitTime;
			}

			// Token: 0x0400205F RID: 8287
			internal Lease lease;

			// Token: 0x04002060 RID: 8288
			internal object sponsorId;

			// Token: 0x04002061 RID: 8289
			internal DateTime sponsorWaitTime;
		}
	}
}
