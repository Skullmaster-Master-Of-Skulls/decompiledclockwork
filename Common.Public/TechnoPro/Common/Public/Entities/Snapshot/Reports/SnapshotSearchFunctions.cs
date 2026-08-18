using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.Reports
{
	// Token: 0x020001B9 RID: 441
	public class SnapshotSearchFunctions
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00014162 File Offset: 0x00012362
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x0001416A File Offset: 0x0001236A
		public int SearchFunctionId { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00014173 File Offset: 0x00012373
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0001417B File Offset: 0x0001237B
		public int SearchInfoId { get; set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00014184 File Offset: 0x00012384
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x0001418C File Offset: 0x0001238C
		public int FunctionCode { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00014195 File Offset: 0x00012395
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x0001419D File Offset: 0x0001239D
		public string FunctionParameters { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x000141A6 File Offset: 0x000123A6
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x000141AE File Offset: 0x000123AE
		public int OrderNum { get; set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x000141B7 File Offset: 0x000123B7
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x000141BF File Offset: 0x000123BF
		public string Custom { get; set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x000141C8 File Offset: 0x000123C8
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x000141D0 File Offset: 0x000123D0
		public string CustomSqlInjection { get; set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x000141D9 File Offset: 0x000123D9
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x000141E1 File Offset: 0x000123E1
		public string CustomSqlInjectionOperator { get; set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x000141EA File Offset: 0x000123EA
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x000141F2 File Offset: 0x000123F2
		public bool IsActive { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x000141FB File Offset: 0x000123FB
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x00014203 File Offset: 0x00012403
		public bool RunOnClient { get; set; }
	}
}
