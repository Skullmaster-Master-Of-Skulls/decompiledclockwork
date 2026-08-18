using System;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002F1 RID: 753
	internal struct TranslatorArg
	{
		// Token: 0x06001AAD RID: 6829 RVA: 0x000852FD File Offset: 0x000834FD
		internal TranslatorArg(Type requestedType)
		{
			this.RequestedType = requestedType;
		}

		// Token: 0x04000933 RID: 2355
		internal readonly Type RequestedType;
	}
}
