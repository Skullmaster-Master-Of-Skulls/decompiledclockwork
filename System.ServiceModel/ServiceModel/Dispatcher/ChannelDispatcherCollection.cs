using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000551 RID: 1361
	public class ChannelDispatcherCollection : SynchronizedCollection<ChannelDispatcherBase>
	{
		// Token: 0x0600342C RID: 13356 RVA: 0x000C8E36 File Offset: 0x000C7036
		internal ChannelDispatcherCollection(ServiceHostBase service, object syncRoot) : base(syncRoot)
		{
			if (service == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("service");
			}
			this.service = service;
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000C8E5C File Offset: 0x000C705C
		protected override void ClearItems()
		{
			ChannelDispatcherBase[] array = new ChannelDispatcherBase[base.Count];
			base.CopyTo(array, 0);
			base.ClearItems();
			if (this.service != null)
			{
				foreach (ChannelDispatcherBase channelDispatcher in array)
				{
					this.service.OnRemoveChannelDispatcher(channelDispatcher);
				}
			}
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000C8EAC File Offset: 0x000C70AC
		protected override void InsertItem(int index, ChannelDispatcherBase item)
		{
			if (this.service != null)
			{
				if (this.service.State == CommunicationState.Closed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(this.service.GetType().ToString()));
				}
				this.service.OnAddChannelDispatcher(item);
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000C8F04 File Offset: 0x000C7104
		protected override void RemoveItem(int index)
		{
			ChannelDispatcherBase channelDispatcher = base.Items[index];
			base.RemoveItem(index);
			if (this.service != null)
			{
				this.service.OnRemoveChannelDispatcher(channelDispatcher);
			}
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000C8F3C File Offset: 0x000C713C
		protected override void SetItem(int index, ChannelDispatcherBase item)
		{
			if (this.service != null && this.service.State == CommunicationState.Closed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(this.service.GetType().ToString()));
			}
			if (this.service != null)
			{
				this.service.OnAddChannelDispatcher(item);
			}
			object syncRoot = base.SyncRoot;
			ChannelDispatcherBase channelDispatcher;
			lock (syncRoot)
			{
				channelDispatcher = base.Items[index];
				base.SetItem(index, item);
			}
			if (this.service != null)
			{
				this.service.OnRemoveChannelDispatcher(channelDispatcher);
			}
		}

		// Token: 0x040027CB RID: 10187
		private ServiceHostBase service;
	}
}
