using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000464 RID: 1124
	public class EndpointAddressMessageFilter : MessageFilter
	{
		// Token: 0x06002B98 RID: 11160 RVA: 0x000AAAF7 File Offset: 0x000A8CF7
		public EndpointAddressMessageFilter(EndpointAddress address) : this(address, false)
		{
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000AAB04 File Offset: 0x000A8D04
		public EndpointAddressMessageFilter(EndpointAddress address, bool includeHostNameInComparison)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			this.address = address;
			this.includeHostNameInComparison = includeHostNameInComparison;
			this.helper = new EndpointAddressMessageFilterHelper(this.address);
			if (includeHostNameInComparison)
			{
				this.comparer = EndpointAddressMessageFilter.HostUriComparer.Value;
				return;
			}
			this.comparer = EndpointAddressMessageFilter.NoHostUriComparer.Value;
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x000AAB69 File Offset: 0x000A8D69
		public EndpointAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x000AAB71 File Offset: 0x000A8D71
		public bool IncludeHostNameInComparison
		{
			get
			{
				return this.includeHostNameInComparison;
			}
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000AAB79 File Offset: 0x000A8D79
		protected internal override IMessageFilterTable<FilterData> CreateFilterTable<FilterData>()
		{
			return new EndpointAddressMessageFilterTable<FilterData>();
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000AAB80 File Offset: 0x000A8D80
		public override bool Match(MessageBuffer messageBuffer)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			Message message = messageBuffer.CreateMessage();
			bool result;
			try
			{
				result = this.Match(message);
			}
			finally
			{
				message.Close();
			}
			return result;
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000AABCC File Offset: 0x000A8DCC
		public override bool Match(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			Uri to = message.Headers.To;
			Uri uri = this.address.Uri;
			return !(to == null) && this.comparer.Equals(uri, to) && this.helper.Match(message);
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x000AAC2A File Offset: 0x000A8E2A
		internal Dictionary<string, EndpointAddressProcessor.HeaderBit[]> HeaderLookup
		{
			get
			{
				return this.helper.HeaderLookup;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (set) Token: 0x06002BA0 RID: 11168 RVA: 0x000AAC37 File Offset: 0x000A8E37
		internal bool ComparePort
		{
			set
			{
				this.comparer.ComparePort = value;
			}
		}

		// Token: 0x0400241B RID: 9243
		private EndpointAddress address;

		// Token: 0x0400241C RID: 9244
		private bool includeHostNameInComparison;

		// Token: 0x0400241D RID: 9245
		private EndpointAddressMessageFilterHelper helper;

		// Token: 0x0400241E RID: 9246
		private EndpointAddressMessageFilter.UriComparer comparer;

		// Token: 0x02000C36 RID: 3126
		internal abstract class UriComparer : EqualityComparer<Uri>
		{
			// Token: 0x06007741 RID: 30529 RVA: 0x001BDB6A File Offset: 0x001BBD6A
			protected UriComparer()
			{
				this.ComparePort = true;
			}

			// Token: 0x17001B4A RID: 6986
			// (get) Token: 0x06007742 RID: 30530
			protected abstract bool CompareHost { get; }

			// Token: 0x17001B4B RID: 6987
			// (get) Token: 0x06007743 RID: 30531 RVA: 0x001BDB79 File Offset: 0x001BBD79
			// (set) Token: 0x06007744 RID: 30532 RVA: 0x001BDB81 File Offset: 0x001BBD81
			internal bool ComparePort { get; set; }

			// Token: 0x06007745 RID: 30533 RVA: 0x001BDB8A File Offset: 0x001BBD8A
			public override bool Equals(Uri u1, Uri u2)
			{
				return EndpointAddress.UriEquals(u1, u2, true, this.CompareHost, this.ComparePort);
			}

			// Token: 0x06007746 RID: 30534 RVA: 0x001BDBA0 File Offset: 0x001BBDA0
			public override int GetHashCode(Uri uri)
			{
				return EndpointAddress.UriGetHashCode(uri, this.CompareHost, this.ComparePort);
			}
		}

		// Token: 0x02000C37 RID: 3127
		internal sealed class HostUriComparer : EndpointAddressMessageFilter.UriComparer
		{
			// Token: 0x06007747 RID: 30535 RVA: 0x001BDBB4 File Offset: 0x001BBDB4
			private HostUriComparer()
			{
			}

			// Token: 0x17001B4C RID: 6988
			// (get) Token: 0x06007748 RID: 30536 RVA: 0x001BDBBC File Offset: 0x001BBDBC
			protected override bool CompareHost
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04004438 RID: 17464
			internal static readonly EndpointAddressMessageFilter.UriComparer Value = new EndpointAddressMessageFilter.HostUriComparer();
		}

		// Token: 0x02000C38 RID: 3128
		internal sealed class NoHostUriComparer : EndpointAddressMessageFilter.UriComparer
		{
			// Token: 0x0600774A RID: 30538 RVA: 0x001BDBCB File Offset: 0x001BBDCB
			private NoHostUriComparer()
			{
			}

			// Token: 0x17001B4D RID: 6989
			// (get) Token: 0x0600774B RID: 30539 RVA: 0x001BDBD3 File Offset: 0x001BBDD3
			protected override bool CompareHost
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04004439 RID: 17465
			internal static readonly EndpointAddressMessageFilter.UriComparer Value = new EndpointAddressMessageFilter.NoHostUriComparer();
		}
	}
}
