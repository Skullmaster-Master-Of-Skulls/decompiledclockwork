using System;

namespace System.IdentityModel.Metadata
{
	// Token: 0x02000101 RID: 257
	public class ProtocolEndpoint
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x0001F046 File Offset: 0x0001D246
		public ProtocolEndpoint() : this(null, null)
		{
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001F050 File Offset: 0x0001D250
		public ProtocolEndpoint(Uri binding, Uri location)
		{
			this.Binding = binding;
			this.Location = location;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001F066 File Offset: 0x0001D266
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001F06E File Offset: 0x0001D26E
		public Uri Binding
		{
			get
			{
				return this.binding;
			}
			set
			{
				this.binding = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001F077 File Offset: 0x0001D277
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001F07F File Offset: 0x0001D27F
		public Uri Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001F088 File Offset: 0x0001D288
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001F090 File Offset: 0x0001D290
		public Uri ResponseLocation
		{
			get
			{
				return this.responseLocation;
			}
			set
			{
				this.responseLocation = value;
			}
		}

		// Token: 0x04000A8E RID: 2702
		private Uri binding;

		// Token: 0x04000A8F RID: 2703
		private Uri location;

		// Token: 0x04000A90 RID: 2704
		private Uri responseLocation;
	}
}
