using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x0200017D RID: 381
	internal class Master
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00053E2A File Offset: 0x0005202A
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x00053E32 File Offset: 0x00052032
		[Filterable(false)]
		public bool AutoEventWireup { get; set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00053E3B File Offset: 0x0005203B
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x00053E43 File Offset: 0x00052043
		[Filterable(false)]
		public string ClassName { get; set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00053E4C File Offset: 0x0005204C
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x00053E54 File Offset: 0x00052054
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string CodeBehind { get; set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00053E5D File Offset: 0x0005205D
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x00053E65 File Offset: 0x00052065
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string CodeFile { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00053E6E File Offset: 0x0005206E
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x00053E76 File Offset: 0x00052076
		[Filterable(false)]
		public string CodeFileBaseClass { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00053E7F File Offset: 0x0005207F
		// (set) Token: 0x06000D90 RID: 3472 RVA: 0x00053E87 File Offset: 0x00052087
		[Filterable(false)]
		public CompilationMode CompilationMode { get; set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00053E90 File Offset: 0x00052090
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x00053E98 File Offset: 0x00052098
		[Filterable(false)]
		public string CompilerOptions { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00053EA1 File Offset: 0x000520A1
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x00053EA9 File Offset: 0x000520A9
		[Filterable(false)]
		public bool Debug { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00053EB2 File Offset: 0x000520B2
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x00053EBA File Offset: 0x000520BA
		[Directive(AllowedOnMobilePages = false)]
		public bool EnableTheming { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00053EC3 File Offset: 0x000520C3
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x00053ECB File Offset: 0x000520CB
		public bool EnableViewState { get; set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00053ED4 File Offset: 0x000520D4
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x00053EDC File Offset: 0x000520DC
		[Filterable(false)]
		public bool Explicit { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00053EE5 File Offset: 0x000520E5
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x00053EED File Offset: 0x000520ED
		[Filterable(false)]
		[Directive(RenameType = "class")]
		public string Inherits { get; set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00053EF6 File Offset: 0x000520F6
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00053EFE File Offset: 0x000520FE
		[Filterable(false)]
		[ReadOnly(true)]
		[Directive(ServerLanguageNames = true)]
		public string Language { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00053F07 File Offset: 0x00052107
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x00053F0F File Offset: 0x0005210F
		[Filterable(false)]
		public bool LinePragmas { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00053F18 File Offset: 0x00052118
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x00053F20 File Offset: 0x00052120
		[Directive(AllowedOnMobilePages = false, BuilderType = "master")]
		[UrlProperty("*.master")]
		public string MasterPageFile { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00053F29 File Offset: 0x00052129
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x00053F31 File Offset: 0x00052131
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string Src { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00053F3A File Offset: 0x0005213A
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00053F42 File Offset: 0x00052142
		[Filterable(false)]
		public bool Strict { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00053F4B File Offset: 0x0005214B
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x00053F53 File Offset: 0x00052153
		[Browsable(false)]
		[Filterable(false)]
		public string TargetSchema { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00053F5C File Offset: 0x0005215C
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x00053F64 File Offset: 0x00052164
		[Filterable(false)]
		[TypeConverter(typeof(WarningLevelConverter))]
		public WarningLevel WarningLevel { get; set; }
	}
}
