using System;

namespace Google.Apis.Discovery
{
	// Token: 0x02000038 RID: 56
	public class Parameter : IParameter
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000447A File Offset: 0x0000267A
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00004482 File Offset: 0x00002682
		public string Name { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000448B File Offset: 0x0000268B
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004493 File Offset: 0x00002693
		public string Pattern { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000449C File Offset: 0x0000269C
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000044A4 File Offset: 0x000026A4
		public bool IsRequired { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000044AD File Offset: 0x000026AD
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000044B5 File Offset: 0x000026B5
		public string ParameterType { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000044BE File Offset: 0x000026BE
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000044C6 File Offset: 0x000026C6
		public string DefaultValue { get; set; }
	}
}
