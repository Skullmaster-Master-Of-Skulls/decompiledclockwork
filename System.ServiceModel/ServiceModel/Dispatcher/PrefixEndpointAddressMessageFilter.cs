using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000479 RID: 1145
	public class PrefixEndpointAddressMessageFilter : MessageFilter
	{
		// Token: 0x06002C90 RID: 11408 RVA: 0x000AE11C File Offset: 0x000AC31C
		public PrefixEndpointAddressMessageFilter(EndpointAddress address) : this(address, false)
		{
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000AE128 File Offset: 0x000AC328
		public PrefixEndpointAddressMessageFilter(EndpointAddress address, bool includeHostNameInComparison)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			this.address = address;
			this.helper = new EndpointAddressMessageFilterHelper(this.address);
			this.hostNameComparisonMode = (includeHostNameInComparison ? HostNameComparisonMode.Exact : HostNameComparisonMode.StrongWildcard);
			this.addressTable = new UriPrefixTable<object>();
			this.addressTable.RegisterUri(this.address.Uri, this.hostNameComparisonMode, new object());
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000AE1A5 File Offset: 0x000AC3A5
		public EndpointAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x000AE1AD File Offset: 0x000AC3AD
		public bool IncludeHostNameInComparison
		{
			get
			{
				return this.hostNameComparisonMode == HostNameComparisonMode.Exact;
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000AE1B8 File Offset: 0x000AC3B8
		protected internal override IMessageFilterTable<FilterData> CreateFilterTable<FilterData>()
		{
			return new PrefixEndpointAddressMessageFilterTable<FilterData>();
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000AE1C0 File Offset: 0x000AC3C0
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

		// Token: 0x06002C96 RID: 11414 RVA: 0x000AE20C File Offset: 0x000AC40C
		public override bool Match(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			Uri to = message.Headers.To;
			object obj;
			return !(to == null) && this.addressTable.TryLookupUri(to, this.hostNameComparisonMode, out obj) && this.helper.Match(message);
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000AE265 File Offset: 0x000AC465
		internal Dictionary<string, EndpointAddressProcessor.HeaderBit[]> HeaderLookup
		{
			get
			{
				return this.helper.HeaderLookup;
			}
		}

		// Token: 0x04002443 RID: 9283
		private EndpointAddress address;

		// Token: 0x04002444 RID: 9284
		private EndpointAddressMessageFilterHelper helper;

		// Token: 0x04002445 RID: 9285
		private UriPrefixTable<object> addressTable;

		// Token: 0x04002446 RID: 9286
		private HostNameComparisonMode hostNameComparisonMode;
	}
}
