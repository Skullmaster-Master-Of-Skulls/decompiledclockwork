using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000843 RID: 2115
	internal class NamedPipeConnectionPoolRegistry : ConnectionPoolRegistry
	{
		// Token: 0x06004F04 RID: 20228 RVA: 0x0011FAA1 File Offset: 0x0011DCA1
		protected override ConnectionPool CreatePool(IConnectionOrientedTransportChannelFactorySettings settings)
		{
			return new NamedPipeConnectionPoolRegistry.NamedPipeConnectionPool((IPipeTransportFactorySettings)settings);
		}

		// Token: 0x02000D31 RID: 3377
		private class NamedPipeConnectionPool : ConnectionPool
		{
			// Token: 0x06007C1E RID: 31774 RVA: 0x001CFC9D File Offset: 0x001CDE9D
			public NamedPipeConnectionPool(IPipeTransportFactorySettings settings) : base(settings, TimeSpan.MaxValue)
			{
				this.pipeNameCache = new NamedPipeConnectionPoolRegistry.PipeNameCache();
				this.transportFactorySettings = settings;
			}

			// Token: 0x06007C1F RID: 31775 RVA: 0x001CFCBD File Offset: 0x001CDEBD
			protected override CommunicationPool<string, IConnection>.EndpointConnectionPool CreateEndpointConnectionPool(string key)
			{
				return new NamedPipeConnectionPoolRegistry.NamedPipeConnectionPool.NamedPipeEndpointConnectionPool(this, key);
			}

			// Token: 0x06007C20 RID: 31776 RVA: 0x001CFCC8 File Offset: 0x001CDEC8
			protected override string GetPoolKey(EndpointAddress address, Uri via)
			{
				object thisLock = base.ThisLock;
				string pipeName;
				lock (thisLock)
				{
					if (!this.pipeNameCache.TryGetValue(via, out pipeName))
					{
						pipeName = PipeConnectionInitiator.GetPipeName(via, this.transportFactorySettings);
						this.pipeNameCache.Add(via, pipeName);
					}
				}
				return pipeName;
			}

			// Token: 0x06007C21 RID: 31777 RVA: 0x001CFD30 File Offset: 0x001CDF30
			protected override void OnClosed()
			{
				base.OnClosed();
				this.pipeNameCache.Clear();
			}

			// Token: 0x06007C22 RID: 31778 RVA: 0x001CFD44 File Offset: 0x001CDF44
			private void OnConnectionAborted(string pipeName)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.pipeNameCache.Purge(pipeName);
				}
			}

			// Token: 0x04004742 RID: 18242
			private NamedPipeConnectionPoolRegistry.PipeNameCache pipeNameCache;

			// Token: 0x04004743 RID: 18243
			private IPipeTransportFactorySettings transportFactorySettings;

			// Token: 0x02000F54 RID: 3924
			protected class NamedPipeEndpointConnectionPool : IdlingCommunicationPool<string, IConnection>.IdleTimeoutEndpointConnectionPool
			{
				// Token: 0x0600871B RID: 34587 RVA: 0x001F4EA8 File Offset: 0x001F30A8
				public NamedPipeEndpointConnectionPool(NamedPipeConnectionPoolRegistry.NamedPipeConnectionPool parent, string key) : base(parent, key)
				{
					this.parent = parent;
				}

				// Token: 0x0600871C RID: 34588 RVA: 0x001F4EB9 File Offset: 0x001F30B9
				protected override void OnConnectionAborted()
				{
					this.parent.OnConnectionAborted(base.Key);
				}

				// Token: 0x04004E97 RID: 20119
				private NamedPipeConnectionPoolRegistry.NamedPipeConnectionPool parent;
			}
		}

		// Token: 0x02000D32 RID: 3378
		private class PipeNameCache
		{
			// Token: 0x06007C23 RID: 31779 RVA: 0x001CFD8C File Offset: 0x001CDF8C
			public void Add(Uri uri, string pipeName)
			{
				this.forwardTable.Add(uri, pipeName);
				ICollection<Uri> collection;
				if (!this.reverseTable.TryGetValue(pipeName, out collection))
				{
					collection = new Collection<Uri>();
					this.reverseTable.Add(pipeName, collection);
				}
				collection.Add(uri);
			}

			// Token: 0x06007C24 RID: 31780 RVA: 0x001CFDD0 File Offset: 0x001CDFD0
			public void Clear()
			{
				this.forwardTable.Clear();
				this.reverseTable.Clear();
			}

			// Token: 0x06007C25 RID: 31781 RVA: 0x001CFDE8 File Offset: 0x001CDFE8
			public void Purge(string pipeName)
			{
				ICollection<Uri> collection;
				if (this.reverseTable.TryGetValue(pipeName, out collection))
				{
					this.reverseTable.Remove(pipeName);
					foreach (Uri key in collection)
					{
						this.forwardTable.Remove(key);
					}
				}
			}

			// Token: 0x06007C26 RID: 31782 RVA: 0x001CFE54 File Offset: 0x001CE054
			public bool TryGetValue(Uri uri, out string pipeName)
			{
				return this.forwardTable.TryGetValue(uri, out pipeName);
			}

			// Token: 0x04004744 RID: 18244
			private Dictionary<Uri, string> forwardTable = new Dictionary<Uri, string>();

			// Token: 0x04004745 RID: 18245
			private Dictionary<string, ICollection<Uri>> reverseTable = new Dictionary<string, ICollection<Uri>>();
		}
	}
}
