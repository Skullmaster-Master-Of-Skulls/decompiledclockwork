using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CE RID: 1230
	public abstract class SimpleType : EdmType
	{
		// Token: 0x06002D75 RID: 11637 RVA: 0x000DC0E0 File Offset: 0x000DA2E0
		internal SimpleType()
		{
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000DC0E8 File Offset: 0x000DA2E8
		internal SimpleType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
		}
	}
}
