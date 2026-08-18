using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000784 RID: 1924
	internal class EntitySetTypePair : Tuple<EntitySet, Type>
	{
		// Token: 0x06005724 RID: 22308 RVA: 0x001782D9 File Offset: 0x001764D9
		public EntitySetTypePair(EntitySet entitySet, Type type) : base(entitySet, type)
		{
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06005725 RID: 22309 RVA: 0x001782E3 File Offset: 0x001764E3
		public EntitySet EntitySet
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06005726 RID: 22310 RVA: 0x001782EB File Offset: 0x001764EB
		public Type BaseType
		{
			get
			{
				return base.Item2;
			}
		}
	}
}
