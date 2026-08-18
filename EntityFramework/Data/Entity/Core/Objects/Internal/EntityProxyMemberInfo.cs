using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200057C RID: 1404
	internal sealed class EntityProxyMemberInfo
	{
		// Token: 0x060036DA RID: 14042 RVA: 0x00104955 File Offset: 0x00102B55
		internal EntityProxyMemberInfo(EdmMember member, int propertyIndex)
		{
			this._member = member;
			this._propertyIndex = propertyIndex;
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x0010496B File Offset: 0x00102B6B
		internal EdmMember EdmMember
		{
			get
			{
				return this._member;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x00104973 File Offset: 0x00102B73
		internal int PropertyIndex
		{
			get
			{
				return this._propertyIndex;
			}
		}

		// Token: 0x04001503 RID: 5379
		private readonly EdmMember _member;

		// Token: 0x04001504 RID: 5380
		private readonly int _propertyIndex;
	}
}
