using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x0200016E RID: 366
	[DataContract(Name = "PeerNodeAddress", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
	[KnownType(typeof(IPAddress[]))]
	public sealed class PeerNodeAddress
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0002868C File Offset: 0x0002688C
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x000286A9 File Offset: 0x000268A9
		[DataMember(Name = "EndpointAddress")]
		internal EndpointAddress10 InnerEPR
		{
			get
			{
				if (!(this.endpointAddress == null))
				{
					return EndpointAddress10.FromEndpointAddress(this.endpointAddress);
				}
				return null;
			}
			set
			{
				this.endpointAddress = ((value == null) ? null : value.ToEndpointAddress());
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x000286BD File Offset: 0x000268BD
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x000286C8 File Offset: 0x000268C8
		[DataMember(Name = "IPAddresses")]
		internal IList<IPAddress> ipAddressesDataMember
		{
			get
			{
				return this.ipAddresses;
			}
			set
			{
				IList<IPAddress> list;
				if (value != null)
				{
					list = value;
				}
				else
				{
					IList<IPAddress> list2 = new IPAddress[0];
					list = list2;
				}
				this.ipAddresses = new ReadOnlyCollection<IPAddress>(list);
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000286F0 File Offset: 0x000268F0
		public PeerNodeAddress(EndpointAddress endpointAddress, ReadOnlyCollection<IPAddress> ipAddresses)
		{
			if (endpointAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("endpointAddress"));
			}
			if (ipAddresses == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("ipAddresses"));
			}
			this.Initialize(endpointAddress, ipAddresses);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00028741 File Offset: 0x00026941
		private void Initialize(EndpointAddress endpointAddress, ReadOnlyCollection<IPAddress> ipAddresses)
		{
			this.endpointAddress = endpointAddress;
			this.servicePath = this.endpointAddress.Uri.PathAndQuery.ToUpperInvariant();
			this.ipAddresses = ipAddresses;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0002876C File Offset: 0x0002696C
		public EndpointAddress EndpointAddress
		{
			get
			{
				return this.endpointAddress;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00028774 File Offset: 0x00026974
		internal string ServicePath
		{
			get
			{
				if (this.servicePath == null)
				{
					this.servicePath = this.endpointAddress.Uri.PathAndQuery.ToUpperInvariant();
				}
				return this.servicePath;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0002879F File Offset: 0x0002699F
		public ReadOnlyCollection<IPAddress> IPAddresses
		{
			get
			{
				if (this.ipAddresses == null)
				{
					this.ipAddresses = new ReadOnlyCollection<IPAddress>(new IPAddress[0]);
				}
				return this.ipAddresses;
			}
		}

		// Token: 0x04000BE2 RID: 3042
		private EndpointAddress endpointAddress;

		// Token: 0x04000BE3 RID: 3043
		private string servicePath;

		// Token: 0x04000BE4 RID: 3044
		private ReadOnlyCollection<IPAddress> ipAddresses;
	}
}
