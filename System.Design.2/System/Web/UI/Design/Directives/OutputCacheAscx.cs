using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000183 RID: 387
	[SchemaElementName("OutputCache")]
	internal class OutputCacheAscx
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x000540A7 File Offset: 0x000522A7
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x000540AF File Offset: 0x000522AF
		[Filterable(false)]
		public int Duration { get; set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x000540B8 File Offset: 0x000522B8
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x000540C0 File Offset: 0x000522C0
		[Filterable(false)]
		public string ProviderName { get; set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x000540C9 File Offset: 0x000522C9
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x000540D1 File Offset: 0x000522D1
		public bool Shared { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x000540DA File Offset: 0x000522DA
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x000540E2 File Offset: 0x000522E2
		[Filterable(false)]
		public string SqlDependency { get; set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x000540EB File Offset: 0x000522EB
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x000540F3 File Offset: 0x000522F3
		[Filterable(false)]
		public string VaryByControl { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x000540FC File Offset: 0x000522FC
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x00054104 File Offset: 0x00052304
		[Filterable(false)]
		public string VaryByCustom { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0005410D File Offset: 0x0005230D
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x00054115 File Offset: 0x00052315
		[Filterable(false)]
		public string VaryByParam { get; set; }
	}
}
