using System;
using System.Net.Security;

namespace System.ServiceModel
{
	// Token: 0x02000169 RID: 361
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public sealed class PeerHopCountAttribute : MessageHeaderAttribute
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x000282B6 File Offset: 0x000264B6
		public PeerHopCountAttribute()
		{
			base.Name = "Hops";
			base.Namespace = "http://schemas.microsoft.com/net/2006/05/peer/HopCount";
			base.ProtectionLevel = ProtectionLevel.None;
			base.MustUnderstand = false;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x000282E2 File Offset: 0x000264E2
		public new bool MustUnderstand
		{
			get
			{
				return base.MustUnderstand;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x000282EA File Offset: 0x000264EA
		public new bool Relay
		{
			get
			{
				return base.Relay;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x000282F2 File Offset: 0x000264F2
		public new string Actor
		{
			get
			{
				return base.Actor;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x000282FA File Offset: 0x000264FA
		public new string Namespace
		{
			get
			{
				return base.Namespace;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00028302 File Offset: 0x00026502
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002830A File Offset: 0x0002650A
		public new ProtectionLevel ProtectionLevel
		{
			get
			{
				return base.ProtectionLevel;
			}
		}
	}
}
