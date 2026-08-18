using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000465 RID: 1125
	internal class EndpointAddressMessageFilterHelper
	{
		// Token: 0x06002BA1 RID: 11169 RVA: 0x000AAC48 File Offset: 0x000A8E48
		internal EndpointAddressMessageFilterHelper(EndpointAddress address)
		{
			this.address = address;
			if (this.address.Headers.Count > 0)
			{
				this.CreateMask();
				this.processorPool = new WeakReference(null);
				return;
			}
			this.qnameLookup = null;
			this.headerLookup = null;
			this.size = 0;
			this.mask = null;
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000AACA4 File Offset: 0x000A8EA4
		private void CreateMask()
		{
			int num = 0;
			this.qnameLookup = new Dictionary<EndpointAddressProcessor.QName, int>(EndpointAddressProcessor.QNameComparer);
			this.headerLookup = new Dictionary<string, EndpointAddressProcessor.HeaderBit[]>();
			StringBuilder stringBuilder = null;
			for (int i = 0; i < this.address.Headers.Count; i++)
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				else
				{
					stringBuilder.Remove(0, stringBuilder.Length);
				}
				string comparableForm = this.address.Headers[i].GetComparableForm(stringBuilder);
				EndpointAddressProcessor.HeaderBit[] array;
				if (this.headerLookup.TryGetValue(comparableForm, out array))
				{
					Array.Resize<EndpointAddressProcessor.HeaderBit>(ref array, array.Length + 1);
					array[array.Length - 1] = new EndpointAddressProcessor.HeaderBit(num++);
					this.headerLookup[comparableForm] = array;
				}
				else
				{
					this.headerLookup.Add(comparableForm, new EndpointAddressProcessor.HeaderBit[]
					{
						new EndpointAddressProcessor.HeaderBit(num++)
					});
					AddressHeader addressHeader = this.address.Headers[i];
					EndpointAddressProcessor.QName key;
					key.name = addressHeader.Name;
					key.ns = addressHeader.Namespace;
					this.qnameLookup[key] = 1;
				}
			}
			if (num == 0)
			{
				this.size = 0;
			}
			else
			{
				this.size = (num - 1) / 8 + 1;
			}
			if (this.size > 0)
			{
				this.mask = new byte[this.size];
				for (int j = 0; j < this.size - 1; j++)
				{
					this.mask[j] = byte.MaxValue;
				}
				if (num % 8 == 0)
				{
					this.mask[this.size - 1] = byte.MaxValue;
					return;
				}
				this.mask[this.size - 1] = (byte)((1 << num % 8) - 1);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000AAE59 File Offset: 0x000A9059
		internal Dictionary<string, EndpointAddressProcessor.HeaderBit[]> HeaderLookup
		{
			get
			{
				if (this.headerLookup == null)
				{
					this.headerLookup = new Dictionary<string, EndpointAddressProcessor.HeaderBit[]>();
				}
				return this.headerLookup;
			}
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000AAE74 File Offset: 0x000A9074
		private EndpointAddressProcessor CreateProcessor(int length)
		{
			if (this.processorPool.Target != null)
			{
				WeakReference obj = this.processorPool;
				lock (obj)
				{
					object target = this.processorPool.Target;
					if (target != null)
					{
						EndpointAddressProcessor endpointAddressProcessor = (EndpointAddressProcessor)target;
						this.processorPool.Target = endpointAddressProcessor.Next;
						endpointAddressProcessor.Next = null;
						endpointAddressProcessor.Clear(length);
						return endpointAddressProcessor;
					}
				}
			}
			return new EndpointAddressProcessor(length);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000AAF00 File Offset: 0x000A9100
		internal bool Match(Message message)
		{
			if (this.size == 0)
			{
				return true;
			}
			EndpointAddressProcessor endpointAddressProcessor = this.CreateProcessor(this.size);
			endpointAddressProcessor.ProcessHeaders(message, this.qnameLookup, this.headerLookup);
			bool result = endpointAddressProcessor.TestExact(this.mask);
			this.ReleaseProcessor(endpointAddressProcessor);
			return result;
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000AAF4C File Offset: 0x000A914C
		private void ReleaseProcessor(EndpointAddressProcessor context)
		{
			WeakReference obj = this.processorPool;
			lock (obj)
			{
				context.Next = (this.processorPool.Target as EndpointAddressProcessor);
				this.processorPool.Target = context;
			}
		}

		// Token: 0x0400241F RID: 9247
		private EndpointAddress address;

		// Token: 0x04002420 RID: 9248
		private WeakReference processorPool;

		// Token: 0x04002421 RID: 9249
		private int size;

		// Token: 0x04002422 RID: 9250
		private byte[] mask;

		// Token: 0x04002423 RID: 9251
		private Dictionary<EndpointAddressProcessor.QName, int> qnameLookup;

		// Token: 0x04002424 RID: 9252
		private Dictionary<string, EndpointAddressProcessor.HeaderBit[]> headerLookup;
	}
}
