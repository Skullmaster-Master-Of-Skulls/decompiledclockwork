using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020000D2 RID: 210
	internal struct StateManagerValue
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x00023C08 File Offset: 0x00021E08
		public StateManagerValue(StateManagerMemberMetadata metadata, object instance, object value)
		{
			this.MemberMetadata = metadata;
			this.UserObject = instance;
			this.OriginalValue = value;
		}

		// Token: 0x040001A7 RID: 423
		public StateManagerMemberMetadata MemberMetadata;

		// Token: 0x040001A8 RID: 424
		public object UserObject;

		// Token: 0x040001A9 RID: 425
		public object OriginalValue;
	}
}
