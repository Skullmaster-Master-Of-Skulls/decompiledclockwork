using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x0200017A RID: 378
	internal class Image
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00053D80 File Offset: 0x00051F80
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x00053D88 File Offset: 0x00051F88
		public string Class { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00053D91 File Offset: 0x00051F91
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x00053D99 File Offset: 0x00051F99
		public string CodeBehind { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x00053DA2 File Offset: 0x00051FA2
		// (set) Token: 0x06000D73 RID: 3443 RVA: 0x00053DAA File Offset: 0x00051FAA
		public string CompilerOptions { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00053DB3 File Offset: 0x00051FB3
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00053DBB File Offset: 0x00051FBB
		public string CustomErrorImageUrl { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00053DC4 File Offset: 0x00051FC4
		// (set) Token: 0x06000D77 RID: 3447 RVA: 0x00053DCC File Offset: 0x00051FCC
		[Filterable(false)]
		public bool Debug { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x00053DD5 File Offset: 0x00051FD5
		// (set) Token: 0x06000D79 RID: 3449 RVA: 0x00053DDD File Offset: 0x00051FDD
		[Filterable(false)]
		public string Description { get; set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00053DE6 File Offset: 0x00051FE6
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x00053DEE File Offset: 0x00051FEE
		[ReadOnly(true)]
		[Directive(ServerLanguageNames = true)]
		public string Language { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00053DF7 File Offset: 0x00051FF7
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x00053DFF File Offset: 0x00051FFF
		[Filterable(false)]
		[TypeConverter(typeof(WarningLevelConverter))]
		public WarningLevel WarningLevel { get; set; }
	}
}
