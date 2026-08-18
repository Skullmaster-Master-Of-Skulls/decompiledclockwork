using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200054C RID: 1356
	internal class SharedRuntimeState
	{
		// Token: 0x060033A6 RID: 13222 RVA: 0x000C73DC File Offset: 0x000C55DC
		internal SharedRuntimeState(bool isOnServer)
		{
			this.isOnServer = isOnServer;
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000C73F9 File Offset: 0x000C55F9
		// (set) Token: 0x060033A8 RID: 13224 RVA: 0x000C7401 File Offset: 0x000C5601
		internal bool EnableFaults
		{
			get
			{
				return this.enableFaults;
			}
			set
			{
				this.enableFaults = value;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x000C740A File Offset: 0x000C560A
		internal bool IsOnServer
		{
			get
			{
				return this.isOnServer;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x000C7412 File Offset: 0x000C5612
		// (set) Token: 0x060033AB RID: 13227 RVA: 0x000C741A File Offset: 0x000C561A
		internal bool ManualAddressing
		{
			get
			{
				return this.manualAddressing;
			}
			set
			{
				this.manualAddressing = value;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000C7423 File Offset: 0x000C5623
		// (set) Token: 0x060033AD RID: 13229 RVA: 0x000C742B File Offset: 0x000C562B
		internal bool ValidateMustUnderstand
		{
			get
			{
				return this.validateMustUnderstand;
			}
			set
			{
				this.validateMustUnderstand = value;
			}
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000C7434 File Offset: 0x000C5634
		internal void LockDownProperties()
		{
			this.isImmutable = true;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000C7440 File Offset: 0x000C5640
		internal void ThrowIfImmutable()
		{
			if (!this.isImmutable)
			{
				return;
			}
			if (this.IsOnServer)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxImmutableServiceHostBehavior0")));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxImmutableChannelFactoryBehavior0")));
		}

		// Token: 0x0400279C RID: 10140
		private bool isImmutable;

		// Token: 0x0400279D RID: 10141
		private bool enableFaults = true;

		// Token: 0x0400279E RID: 10142
		private bool isOnServer;

		// Token: 0x0400279F RID: 10143
		private bool manualAddressing;

		// Token: 0x040027A0 RID: 10144
		private bool validateMustUnderstand = true;
	}
}
