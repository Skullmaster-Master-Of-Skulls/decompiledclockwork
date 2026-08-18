using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000176 RID: 374
	internal class Control
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00053BE9 File Offset: 0x00051DE9
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00053BF1 File Offset: 0x00051DF1
		[Filterable(false)]
		public bool AutoEventWireup { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00053BFA File Offset: 0x00051DFA
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00053C02 File Offset: 0x00051E02
		[Filterable(false)]
		public string ClassName { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00053C0B File Offset: 0x00051E0B
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x00053C13 File Offset: 0x00051E13
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string CodeBehind { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00053C1C File Offset: 0x00051E1C
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x00053C24 File Offset: 0x00051E24
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string CodeFile { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00053C2D File Offset: 0x00051E2D
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x00053C35 File Offset: 0x00051E35
		[Filterable(false)]
		public string CodeFileBaseClass { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00053C3E File Offset: 0x00051E3E
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00053C46 File Offset: 0x00051E46
		[Filterable(false)]
		public CompilationMode CompilationMode { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00053C4F File Offset: 0x00051E4F
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x00053C57 File Offset: 0x00051E57
		[Filterable(false)]
		public string CompilerOptions { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00053C60 File Offset: 0x00051E60
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x00053C68 File Offset: 0x00051E68
		[Filterable(false)]
		public bool Debug { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00053C71 File Offset: 0x00051E71
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x00053C79 File Offset: 0x00051E79
		[Directive(AllowedOnMobilePages = false)]
		public bool EnableTheming { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00053C82 File Offset: 0x00051E82
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x00053C8A File Offset: 0x00051E8A
		public bool EnableViewState { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00053C93 File Offset: 0x00051E93
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x00053C9B File Offset: 0x00051E9B
		[Filterable(false)]
		public bool Explicit { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00053CA4 File Offset: 0x00051EA4
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x00053CAC File Offset: 0x00051EAC
		[Filterable(false)]
		[Directive(RenameType = "class")]
		public string Inherits { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00053CB5 File Offset: 0x00051EB5
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x00053CBD File Offset: 0x00051EBD
		[Filterable(false)]
		[ReadOnly(true)]
		[Directive(ServerLanguageNames = true)]
		public string Language { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00053CC6 File Offset: 0x00051EC6
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x00053CCE File Offset: 0x00051ECE
		[Filterable(false)]
		public bool LinePragmas { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00053CD7 File Offset: 0x00051ED7
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x00053CDF File Offset: 0x00051EDF
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string Src { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00053CE8 File Offset: 0x00051EE8
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x00053CF0 File Offset: 0x00051EF0
		[Filterable(false)]
		public bool Strict { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00053CF9 File Offset: 0x00051EF9
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00053D01 File Offset: 0x00051F01
		[Browsable(false)]
		[Filterable(false)]
		public string TargetSchema { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x00053D0A File Offset: 0x00051F0A
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x00053D12 File Offset: 0x00051F12
		[Filterable(false)]
		[TypeConverter(typeof(WarningLevelConverter))]
		public WarningLevel WarningLevel { get; set; }
	}
}
