using System;
using System.IO;
using System.Runtime.Diagnostics;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200082F RID: 2095
	internal abstract class StreamSecurityUpgradeAcceptorBase : StreamSecurityUpgradeAcceptor
	{
		// Token: 0x06004E4D RID: 20045 RVA: 0x0011E038 File Offset: 0x0011C238
		protected StreamSecurityUpgradeAcceptorBase(string upgradeString)
		{
			this.upgradeString = upgradeString;
		}

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06004E4E RID: 20046 RVA: 0x0011E047 File Offset: 0x0011C247
		internal EventTraceActivity EventTraceActivity
		{
			get
			{
				if (this.eventTraceActivity == null)
				{
					this.eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
				}
				return this.eventTraceActivity;
			}
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x0011E064 File Offset: 0x0011C264
		public override Stream AcceptUpgrade(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			Stream result = this.OnAcceptUpgrade(stream, out this.remoteSecurity);
			this.securityUpgraded = true;
			return result;
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x0011E09A File Offset: 0x0011C29A
		public override IAsyncResult BeginAcceptUpgrade(Stream stream, AsyncCallback callback, object state)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			return this.OnBeginAcceptUpgrade(stream, callback, state);
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x0011E0B8 File Offset: 0x0011C2B8
		public override bool CanUpgrade(string contentType)
		{
			return !this.securityUpgraded && contentType == this.upgradeString;
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x0011E0D0 File Offset: 0x0011C2D0
		public override Stream EndAcceptUpgrade(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			Stream result2 = this.OnEndAcceptUpgrade(result, out this.remoteSecurity);
			this.securityUpgraded = true;
			return result2;
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x0011E106 File Offset: 0x0011C306
		public override SecurityMessageProperty GetRemoteSecurity()
		{
			return this.remoteSecurity;
		}

		// Token: 0x06004E54 RID: 20052
		protected abstract Stream OnAcceptUpgrade(Stream stream, out SecurityMessageProperty remoteSecurity);

		// Token: 0x06004E55 RID: 20053
		protected abstract IAsyncResult OnBeginAcceptUpgrade(Stream stream, AsyncCallback callback, object state);

		// Token: 0x06004E56 RID: 20054
		protected abstract Stream OnEndAcceptUpgrade(IAsyncResult result, out SecurityMessageProperty remoteSecurity);

		// Token: 0x040030DE RID: 12510
		private SecurityMessageProperty remoteSecurity;

		// Token: 0x040030DF RID: 12511
		private bool securityUpgraded;

		// Token: 0x040030E0 RID: 12512
		private string upgradeString;

		// Token: 0x040030E1 RID: 12513
		private EventTraceActivity eventTraceActivity;
	}
}
