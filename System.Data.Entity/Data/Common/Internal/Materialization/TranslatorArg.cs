using System;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003D4 RID: 980
	internal struct TranslatorArg
	{
		// Token: 0x060034D3 RID: 13523 RVA: 0x000CBF6D File Offset: 0x000CA16D
		internal TranslatorArg(Type requestedType)
		{
			this.RequestedType = requestedType;
		}

		// Token: 0x04001724 RID: 5924
		internal readonly Type RequestedType;
	}
}
