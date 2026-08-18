using System;
using System.IO;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000832 RID: 2098
	internal abstract class StreamSecurityUpgradeInitiatorBase : StreamSecurityUpgradeInitiator
	{
		// Token: 0x06004E63 RID: 20067 RVA: 0x0011E2FC File Offset: 0x0011C4FC
		protected StreamSecurityUpgradeInitiatorBase(string upgradeString, EndpointAddress remoteAddress, Uri via)
		{
			this.remoteAddress = remoteAddress;
			this.via = via;
			this.nextUpgrade = upgradeString;
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x0011E319 File Offset: 0x0011C519
		protected EndpointAddress RemoteAddress
		{
			get
			{
				return this.remoteAddress;
			}
		}

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06004E65 RID: 20069 RVA: 0x0011E321 File Offset: 0x0011C521
		protected Uri Via
		{
			get
			{
				return this.via;
			}
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x0011E329 File Offset: 0x0011C529
		public override IAsyncResult BeginInitiateUpgrade(Stream stream, AsyncCallback callback, object state)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (!this.isOpen)
			{
				this.Open(TimeSpan.Zero);
			}
			return this.OnBeginInitiateUpgrade(stream, callback, state);
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x0011E35C File Offset: 0x0011C55C
		public override Stream EndInitiateUpgrade(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			Stream result2 = this.OnEndInitiateUpgrade(result, out this.remoteSecurity);
			this.securityUpgraded = true;
			return result2;
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x0011E394 File Offset: 0x0011C594
		public override string GetNextUpgrade()
		{
			string result = this.nextUpgrade;
			this.nextUpgrade = null;
			return result;
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x0011E3B0 File Offset: 0x0011C5B0
		public override SecurityMessageProperty GetRemoteSecurity()
		{
			if (!this.securityUpgraded)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OperationInvalidBeforeSecurityNegotiation")));
			}
			return this.remoteSecurity;
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x0011E3DC File Offset: 0x0011C5DC
		public override Stream InitiateUpgrade(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (!this.isOpen)
			{
				this.Open(TimeSpan.Zero);
			}
			Stream result = this.OnInitiateUpgrade(stream, out this.remoteSecurity);
			this.securityUpgraded = true;
			return result;
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x0011E425 File Offset: 0x0011C625
		internal override void EndOpen(IAsyncResult result)
		{
			base.EndOpen(result);
			this.isOpen = true;
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x0011E435 File Offset: 0x0011C635
		internal override void Open(TimeSpan timeout)
		{
			base.Open(timeout);
			this.isOpen = true;
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x0011E445 File Offset: 0x0011C645
		internal override void EndClose(IAsyncResult result)
		{
			base.EndClose(result);
			this.isOpen = false;
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x0011E455 File Offset: 0x0011C655
		internal override void Close(TimeSpan timeout)
		{
			base.Close(timeout);
			this.isOpen = false;
		}

		// Token: 0x06004E6F RID: 20079
		protected abstract IAsyncResult OnBeginInitiateUpgrade(Stream stream, AsyncCallback callback, object state);

		// Token: 0x06004E70 RID: 20080
		protected abstract Stream OnEndInitiateUpgrade(IAsyncResult result, out SecurityMessageProperty remoteSecurity);

		// Token: 0x06004E71 RID: 20081
		protected abstract Stream OnInitiateUpgrade(Stream stream, out SecurityMessageProperty remoteSecurity);

		// Token: 0x040030E6 RID: 12518
		private EndpointAddress remoteAddress;

		// Token: 0x040030E7 RID: 12519
		private Uri via;

		// Token: 0x040030E8 RID: 12520
		private SecurityMessageProperty remoteSecurity;

		// Token: 0x040030E9 RID: 12521
		private bool securityUpgraded;

		// Token: 0x040030EA RID: 12522
		private string nextUpgrade;

		// Token: 0x040030EB RID: 12523
		private bool isOpen;
	}
}
