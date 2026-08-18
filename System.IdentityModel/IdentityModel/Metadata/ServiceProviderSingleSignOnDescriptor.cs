using System;

namespace System.IdentityModel.Metadata
{
	// Token: 0x02000105 RID: 261
	public class ServiceProviderSingleSignOnDescriptor : SingleSignOnDescriptor
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x0001F15A File Offset: 0x0001D35A
		public ServiceProviderSingleSignOnDescriptor() : this(new IndexedProtocolEndpointDictionary())
		{
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001F167 File Offset: 0x0001D367
		public ServiceProviderSingleSignOnDescriptor(IndexedProtocolEndpointDictionary collection)
		{
			this._assertionConsumerServices = collection;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001F181 File Offset: 0x0001D381
		public IndexedProtocolEndpointDictionary AssertionConsumerServices
		{
			get
			{
				return this._assertionConsumerServices;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001F189 File Offset: 0x0001D389
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001F191 File Offset: 0x0001D391
		public bool AuthenticationRequestsSigned
		{
			get
			{
				return this._authenticationRequestsSigned;
			}
			set
			{
				this._authenticationRequestsSigned = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001F19A File Offset: 0x0001D39A
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x0001F1A2 File Offset: 0x0001D3A2
		public bool WantAssertionsSigned
		{
			get
			{
				return this._wantAssertionsSigned;
			}
			set
			{
				this._wantAssertionsSigned = value;
			}
		}

		// Token: 0x04000A9A RID: 2714
		private bool _authenticationRequestsSigned;

		// Token: 0x04000A9B RID: 2715
		private bool _wantAssertionsSigned;

		// Token: 0x04000A9C RID: 2716
		private IndexedProtocolEndpointDictionary _assertionConsumerServices = new IndexedProtocolEndpointDictionary();
	}
}
