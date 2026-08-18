using System;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001EA RID: 490
	public sealed class SerializeAbsoluteContext
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x0000362F File Offset: 0x0000182F
		public SerializeAbsoluteContext()
		{
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0006A47A File Offset: 0x0006867A
		public SerializeAbsoluteContext(MemberDescriptor member)
		{
			this._member = member;
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0006A489 File Offset: 0x00068689
		public MemberDescriptor Member
		{
			get
			{
				return this._member;
			}
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0006A491 File Offset: 0x00068691
		public bool ShouldSerialize(MemberDescriptor member)
		{
			return this._member == null || this._member == member;
		}

		// Token: 0x04000A05 RID: 2565
		private MemberDescriptor _member;
	}
}
