using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005AB RID: 1451
	internal class SynchronizedChannelCollection<TChannel> : SynchronizedCollection<TChannel> where TChannel : IChannel
	{
		// Token: 0x0600389C RID: 14492 RVA: 0x000DA45A File Offset: 0x000D865A
		internal SynchronizedChannelCollection(object syncRoot) : base(syncRoot)
		{
			this.onChannelClosed = new EventHandler(this.OnChannelClosed);
			this.onChannelFaulted = new EventHandler(this.OnChannelFaulted);
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000DA487 File Offset: 0x000D8687
		private void AddingChannel(TChannel channel)
		{
			channel.Faulted += this.onChannelFaulted;
			channel.Closed += this.onChannelClosed;
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000DA4AF File Offset: 0x000D86AF
		private void RemovingChannel(TChannel channel)
		{
			channel.Faulted -= this.onChannelFaulted;
			channel.Closed -= this.onChannelClosed;
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000DA4D8 File Offset: 0x000D86D8
		private void OnChannelClosed(object sender, EventArgs args)
		{
			TChannel item = (TChannel)((object)sender);
			base.Remove(item);
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000DA4F4 File Offset: 0x000D86F4
		private void OnChannelFaulted(object sender, EventArgs args)
		{
			TChannel item = (TChannel)((object)sender);
			base.Remove(item);
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000DA510 File Offset: 0x000D8710
		protected override void ClearItems()
		{
			List<TChannel> items = base.Items;
			for (int i = 0; i < items.Count; i++)
			{
				this.RemovingChannel(items[i]);
			}
			base.ClearItems();
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000DA548 File Offset: 0x000D8748
		protected override void InsertItem(int index, TChannel item)
		{
			this.AddingChannel(item);
			base.InsertItem(index, item);
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000DA55C File Offset: 0x000D875C
		protected override void RemoveItem(int index)
		{
			TChannel channel = base.Items[index];
			base.RemoveItem(index);
			this.RemovingChannel(channel);
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000DA584 File Offset: 0x000D8784
		protected override void SetItem(int index, TChannel item)
		{
			TChannel channel = base.Items[index];
			this.AddingChannel(item);
			base.SetItem(index, item);
			this.RemovingChannel(channel);
		}

		// Token: 0x040029A5 RID: 10661
		private EventHandler onChannelClosed;

		// Token: 0x040029A6 RID: 10662
		private EventHandler onChannelFaulted;
	}
}
