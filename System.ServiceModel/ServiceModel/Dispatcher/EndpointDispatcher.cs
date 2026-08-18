using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000557 RID: 1367
	[__DynamicallyInvokable]
	public class EndpointDispatcher
	{
		// Token: 0x0600353E RID: 13630 RVA: 0x000CF3E2 File Offset: 0x000CD5E2
		internal EndpointDispatcher(EndpointAddress address, string contractName, string contractNamespace, string id, bool isSystemEndpoint) : this(address, contractName, contractNamespace)
		{
			this.id = id;
			this.isSystemEndpoint = isSystemEndpoint;
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000CF3FD File Offset: 0x000CD5FD
		public EndpointDispatcher(EndpointAddress address, string contractName, string contractNamespace) : this(address, contractName, contractNamespace, false)
		{
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000CF40C File Offset: 0x000CD60C
		public EndpointDispatcher(EndpointAddress address, string contractName, string contractNamespace, bool isSystemEndpoint)
		{
			this.originalAddress = address;
			this.contractName = contractName;
			this.contractNamespace = contractNamespace;
			if (address != null)
			{
				this.addressFilter = new EndpointAddressMessageFilter(address);
			}
			else
			{
				this.addressFilter = new MatchAllMessageFilter();
			}
			this.contractFilter = new MatchAllMessageFilter();
			this.dispatchRuntime = new DispatchRuntime(this);
			this.filterPriority = 0;
			this.isSystemEndpoint = isSystemEndpoint;
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000CF47C File Offset: 0x000CD67C
		private EndpointDispatcher(EndpointDispatcher baseEndpoint, IEnumerable<AddressHeader> headers)
		{
			EndpointAddressBuilder endpointAddressBuilder = new EndpointAddressBuilder(baseEndpoint.EndpointAddress);
			foreach (AddressHeader item in headers)
			{
				endpointAddressBuilder.Headers.Add(item);
			}
			EndpointAddress address = endpointAddressBuilder.ToEndpointAddress();
			this.addressFilter = new EndpointAddressMessageFilter(address);
			this.contractFilter = baseEndpoint.ContractFilter;
			this.contractName = baseEndpoint.ContractName;
			this.contractNamespace = baseEndpoint.ContractNamespace;
			this.dispatchRuntime = baseEndpoint.DispatchRuntime;
			this.filterPriority = baseEndpoint.FilterPriority + 1;
			this.originalAddress = address;
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				this.perfCounterId = baseEndpoint.perfCounterId;
				this.perfCounterBaseId = baseEndpoint.perfCounterBaseId;
			}
			this.id = baseEndpoint.id;
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000CF560 File Offset: 0x000CD760
		// (set) Token: 0x06003543 RID: 13635 RVA: 0x000CF568 File Offset: 0x000CD768
		public MessageFilter AddressFilter
		{
			get
			{
				return this.addressFilter;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.ThrowIfDisposedOrImmutable();
				this.addressFilter = value;
				this.addressFilterSetExplicit = true;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000CF591 File Offset: 0x000CD791
		internal bool AddressFilterSetExplicit
		{
			get
			{
				return this.addressFilterSetExplicit;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000CF599 File Offset: 0x000CD799
		public ChannelDispatcher ChannelDispatcher
		{
			get
			{
				return this.channelDispatcher;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x000CF5A1 File Offset: 0x000CD7A1
		// (set) Token: 0x06003547 RID: 13639 RVA: 0x000CF5A9 File Offset: 0x000CD7A9
		public MessageFilter ContractFilter
		{
			get
			{
				return this.contractFilter;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.ThrowIfDisposedOrImmutable();
				this.contractFilter = value;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000CF5CB File Offset: 0x000CD7CB
		public string ContractName
		{
			get
			{
				return this.contractName;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x000CF5D3 File Offset: 0x000CD7D3
		public string ContractNamespace
		{
			get
			{
				return this.contractNamespace;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x000CF5DB File Offset: 0x000CD7DB
		// (set) Token: 0x0600354B RID: 13643 RVA: 0x000CF5E3 File Offset: 0x000CD7E3
		internal ServiceChannel DatagramChannel
		{
			get
			{
				return this.datagramChannel;
			}
			set
			{
				this.datagramChannel = value;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000CF5EC File Offset: 0x000CD7EC
		public DispatchRuntime DispatchRuntime
		{
			get
			{
				return this.dispatchRuntime;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000CF5F4 File Offset: 0x000CD7F4
		internal Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x000CF5FC File Offset: 0x000CD7FC
		internal EndpointAddress OriginalAddress
		{
			get
			{
				return this.originalAddress;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x000CF604 File Offset: 0x000CD804
		public EndpointAddress EndpointAddress
		{
			get
			{
				if (this.channelDispatcher == null)
				{
					return this.originalAddress;
				}
				if (this.originalAddress != null && this.originalAddress.Identity != null)
				{
					return this.originalAddress;
				}
				IChannelListener listener = this.channelDispatcher.Listener;
				EndpointIdentity property = listener.GetProperty<EndpointIdentity>();
				if (this.originalAddress != null && property == null)
				{
					return this.originalAddress;
				}
				EndpointAddressBuilder endpointAddressBuilder;
				if (this.originalAddress != null)
				{
					endpointAddressBuilder = new EndpointAddressBuilder(this.originalAddress);
				}
				else
				{
					endpointAddressBuilder = new EndpointAddressBuilder();
					endpointAddressBuilder.Uri = listener.Uri;
				}
				endpointAddressBuilder.Identity = property;
				return endpointAddressBuilder.ToEndpointAddress();
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000CF6A8 File Offset: 0x000CD8A8
		public bool IsSystemEndpoint
		{
			get
			{
				return this.isSystemEndpoint;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x000CF6B0 File Offset: 0x000CD8B0
		internal MessageFilter EndpointFilter
		{
			get
			{
				if (this.endpointFilter == null)
				{
					MessageFilter filter = this.addressFilter;
					MessageFilter messageFilter = this.contractFilter;
					if (messageFilter is MatchAllMessageFilter)
					{
						this.endpointFilter = filter;
					}
					else
					{
						this.endpointFilter = new AndMessageFilter(filter, messageFilter);
					}
				}
				return this.endpointFilter;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06003552 RID: 13650 RVA: 0x000CF6F7 File Offset: 0x000CD8F7
		// (set) Token: 0x06003553 RID: 13651 RVA: 0x000CF6FF File Offset: 0x000CD8FF
		public int FilterPriority
		{
			get
			{
				return this.filterPriority;
			}
			set
			{
				this.filterPriority = value;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x000CF708 File Offset: 0x000CD908
		// (set) Token: 0x06003555 RID: 13653 RVA: 0x000CF710 File Offset: 0x000CD910
		internal string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x000CF719 File Offset: 0x000CD919
		internal string PerfCounterId
		{
			get
			{
				return this.perfCounterId;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x000CF721 File Offset: 0x000CD921
		internal string PerfCounterBaseId
		{
			get
			{
				return this.perfCounterBaseId;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x000CF729 File Offset: 0x000CD929
		// (set) Token: 0x06003559 RID: 13657 RVA: 0x000CF731 File Offset: 0x000CD931
		internal int PerfCounterInstanceId { get; set; }

		// Token: 0x0600355A RID: 13658 RVA: 0x000CF73C File Offset: 0x000CD93C
		internal static EndpointDispatcher AddEndpointDispatcher(EndpointDispatcher baseEndpoint, IEnumerable<AddressHeader> headers)
		{
			EndpointDispatcher endpointDispatcher = new EndpointDispatcher(baseEndpoint, headers);
			baseEndpoint.ChannelDispatcher.Endpoints.Add(endpointDispatcher);
			return endpointDispatcher;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000CF764 File Offset: 0x000CD964
		internal void Attach(ChannelDispatcher channelDispatcher)
		{
			if (channelDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelDispatcher");
			}
			if (this.channelDispatcher != null)
			{
				Exception exception = new InvalidOperationException(SR.GetString("SFxEndpointDispatcherMultipleChannelDispatcher0"));
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			this.channelDispatcher = channelDispatcher;
			this.listenUri = channelDispatcher.Listener.Uri;
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000CF7C0 File Offset: 0x000CD9C0
		internal void Detach(ChannelDispatcher channelDispatcher)
		{
			if (channelDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelDispatcher");
			}
			if (this.channelDispatcher != channelDispatcher)
			{
				Exception exception = new InvalidOperationException(SR.GetString("SFxEndpointDispatcherDifferentChannelDispatcher0"));
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			this.ReleasePerformanceCounters();
			this.channelDispatcher = null;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000CF812 File Offset: 0x000CDA12
		internal void ReleasePerformanceCounters()
		{
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				PerformanceCounters.ReleasePerformanceCountersForEndpoint(this.perfCounterId, this.perfCounterBaseId);
			}
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000CF82C File Offset: 0x000CDA2C
		internal bool SetPerfCounterId()
		{
			Uri uri = null;
			if (null != this.ListenUri)
			{
				uri = this.ListenUri;
			}
			else
			{
				EndpointAddress endpointAddress = this.EndpointAddress;
				if (null != endpointAddress)
				{
					uri = endpointAddress.Uri;
				}
			}
			if (null != uri)
			{
				this.perfCounterBaseId = uri.AbsoluteUri.ToUpperInvariant();
				this.perfCounterId = this.perfCounterBaseId + "/" + this.contractName.ToUpperInvariant();
				return true;
			}
			return false;
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000CF8A8 File Offset: 0x000CDAA8
		private void ThrowIfDisposedOrImmutable()
		{
			ChannelDispatcher channelDispatcher = this.channelDispatcher;
			if (channelDispatcher != null)
			{
				channelDispatcher.ThrowIfDisposedOrImmutable();
			}
		}

		// Token: 0x04002865 RID: 10341
		private MessageFilter addressFilter;

		// Token: 0x04002866 RID: 10342
		private bool addressFilterSetExplicit;

		// Token: 0x04002867 RID: 10343
		private ChannelDispatcher channelDispatcher;

		// Token: 0x04002868 RID: 10344
		private MessageFilter contractFilter;

		// Token: 0x04002869 RID: 10345
		private string contractName;

		// Token: 0x0400286A RID: 10346
		private string contractNamespace;

		// Token: 0x0400286B RID: 10347
		private ServiceChannel datagramChannel;

		// Token: 0x0400286C RID: 10348
		private DispatchRuntime dispatchRuntime;

		// Token: 0x0400286D RID: 10349
		private MessageFilter endpointFilter;

		// Token: 0x0400286E RID: 10350
		private int filterPriority;

		// Token: 0x0400286F RID: 10351
		private Uri listenUri;

		// Token: 0x04002870 RID: 10352
		private EndpointAddress originalAddress;

		// Token: 0x04002871 RID: 10353
		private string perfCounterId;

		// Token: 0x04002872 RID: 10354
		private string perfCounterBaseId;

		// Token: 0x04002873 RID: 10355
		private string id;

		// Token: 0x04002874 RID: 10356
		private bool isSystemEndpoint;
	}
}
