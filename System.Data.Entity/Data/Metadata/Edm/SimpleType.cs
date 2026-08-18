using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001F7 RID: 503
	public abstract class SimpleType : EdmType
	{
		// Token: 0x06002127 RID: 8487 RVA: 0x00074B3E File Offset: 0x00072D3E
		internal SimpleType()
		{
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00074B46 File Offset: 0x00072D46
		internal SimpleType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
		}
	}
}
