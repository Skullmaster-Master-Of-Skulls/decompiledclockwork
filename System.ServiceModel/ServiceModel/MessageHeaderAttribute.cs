using System;

namespace System.ServiceModel
{
	// Token: 0x020000D8 RID: 216
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class MessageHeaderAttribute : MessageContractMemberAttribute
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000158F7 File Offset: 0x00013AF7
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x000158FF File Offset: 0x00013AFF
		public bool MustUnderstand
		{
			get
			{
				return this.mustUnderstand;
			}
			set
			{
				this.mustUnderstand = value;
				this.isMustUnderstandSet = true;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0001590F File Offset: 0x00013B0F
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00015917 File Offset: 0x00013B17
		public bool Relay
		{
			get
			{
				return this.relay;
			}
			set
			{
				this.relay = value;
				this.isRelaySet = true;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00015927 File Offset: 0x00013B27
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0001592F File Offset: 0x00013B2F
		public string Actor
		{
			get
			{
				return this.actor;
			}
			set
			{
				this.actor = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00015938 File Offset: 0x00013B38
		internal bool IsMustUnderstandSet
		{
			get
			{
				return this.isMustUnderstandSet;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00015940 File Offset: 0x00013B40
		internal bool IsRelaySet
		{
			get
			{
				return this.isRelaySet;
			}
		}

		// Token: 0x040009BC RID: 2492
		private bool mustUnderstand;

		// Token: 0x040009BD RID: 2493
		private bool isMustUnderstandSet;

		// Token: 0x040009BE RID: 2494
		private bool relay;

		// Token: 0x040009BF RID: 2495
		private bool isRelaySet;

		// Token: 0x040009C0 RID: 2496
		private string actor;
	}
}
