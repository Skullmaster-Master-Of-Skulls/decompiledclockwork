using System;
using System.Collections.Generic;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001F8 RID: 504
	public class Participants
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x000473E2 File Offset: 0x000455E2
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x000473EA File Offset: 0x000455EA
		public EndpointReference Primary
		{
			get
			{
				return this._primary;
			}
			set
			{
				this._primary = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x000473F3 File Offset: 0x000455F3
		public List<EndpointReference> Participant
		{
			get
			{
				return this._participant;
			}
		}

		// Token: 0x04000E74 RID: 3700
		private EndpointReference _primary;

		// Token: 0x04000E75 RID: 3701
		private List<EndpointReference> _participant = new List<EndpointReference>();
	}
}
