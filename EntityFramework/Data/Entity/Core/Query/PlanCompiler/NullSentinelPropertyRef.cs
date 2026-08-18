using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000689 RID: 1673
	internal class NullSentinelPropertyRef : PropertyRef
	{
		// Token: 0x060041FC RID: 16892 RVA: 0x00137540 File Offset: 0x00135740
		private NullSentinelPropertyRef()
		{
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x00137548 File Offset: 0x00135748
		internal static NullSentinelPropertyRef Instance
		{
			get
			{
				return NullSentinelPropertyRef._singleton;
			}
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x0013754F File Offset: 0x0013574F
		public override string ToString()
		{
			return "NULLSENTINEL";
		}

		// Token: 0x04001871 RID: 6257
		private static readonly NullSentinelPropertyRef _singleton = new NullSentinelPropertyRef();
	}
}
