using System;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000066 RID: 102
	internal class NullSentinelPropertyRef : PropertyRef
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		private NullSentinelPropertyRef()
		{
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0002CC13 File Offset: 0x0002AE13
		internal static NullSentinelPropertyRef Instance
		{
			get
			{
				return NullSentinelPropertyRef.s_singleton;
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002CC1A File Offset: 0x0002AE1A
		public override string ToString()
		{
			return "NULLSENTINEL";
		}

		// Token: 0x040007F9 RID: 2041
		private static NullSentinelPropertyRef s_singleton = new NullSentinelPropertyRef();
	}
}
