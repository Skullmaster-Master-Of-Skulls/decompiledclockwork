using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x02000157 RID: 343
	public class OptionalReliableSession : ReliableSession
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x00026524 File Offset: 0x00024724
		public OptionalReliableSession()
		{
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002652C File Offset: 0x0002472C
		public OptionalReliableSession(ReliableSessionBindingElement reliableSessionBindingElement) : base(reliableSessionBindingElement)
		{
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00026535 File Offset: 0x00024735
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x0002653D File Offset: 0x0002473D
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00026546 File Offset: 0x00024746
		internal void CopySettings(OptionalReliableSession copyFrom)
		{
			base.CopySettings(copyFrom);
			this.Enabled = copyFrom.Enabled;
		}

		// Token: 0x04000B9A RID: 2970
		private bool enabled;
	}
}
