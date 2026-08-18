using System;
using System.ComponentModel;
using System.Web.Configuration;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000185 RID: 389
	internal class Page
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x000541A6 File Offset: 0x000523A6
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x000541AE File Offset: 0x000523AE
		[Filterable(false)]
		public bool AspCompat { get; set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x000541B7 File Offset: 0x000523B7
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x000541BF File Offset: 0x000523BF
		[Filterable(false)]
		public bool Async { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x000541C8 File Offset: 0x000523C8
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x000541D0 File Offset: 0x000523D0
		[Filterable(false)]
		public string AsyncTimeout { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x000541D9 File Offset: 0x000523D9
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x000541E1 File Offset: 0x000523E1
		[Filterable(false)]
		public bool AutoEventWireup { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x000541EA File Offset: 0x000523EA
		// (set) Token: 0x06000DFE RID: 3582 RVA: 0x000541F2 File Offset: 0x000523F2
		public bool Buffer { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x000541FB File Offset: 0x000523FB
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x00054203 File Offset: 0x00052403
		[Filterable(false)]
		public string ClassName { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0005420C File Offset: 0x0005240C
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x00054214 File Offset: 0x00052414
		[Browsable(false)]
		public string ClientTarget { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0005421D File Offset: 0x0005241D
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00054225 File Offset: 0x00052425
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string CodeBehind { get; set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0005422E File Offset: 0x0005242E
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x00054236 File Offset: 0x00052436
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string CodeFile { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0005423F File Offset: 0x0005243F
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x00054247 File Offset: 0x00052447
		[Filterable(false)]
		public string CodeFileBaseClass { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00054250 File Offset: 0x00052450
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00054258 File Offset: 0x00052458
		public string CodePage { get; set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x00054261 File Offset: 0x00052461
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x00054269 File Offset: 0x00052469
		[Filterable(false)]
		public CompilationMode CompilationMode { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00054272 File Offset: 0x00052472
		// (set) Token: 0x06000E0E RID: 3598 RVA: 0x0005427A File Offset: 0x0005247A
		[Filterable(false)]
		public string CompilerOptions { get; set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00054283 File Offset: 0x00052483
		// (set) Token: 0x06000E10 RID: 3600 RVA: 0x0005428B File Offset: 0x0005248B
		public string ContentType { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00054294 File Offset: 0x00052494
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0005429C File Offset: 0x0005249C
		[Directive(Culture = true)]
		public string Culture { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x000542A5 File Offset: 0x000524A5
		// (set) Token: 0x06000E14 RID: 3604 RVA: 0x000542AD File Offset: 0x000524AD
		[Filterable(false)]
		public bool Debug { get; set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x000542B6 File Offset: 0x000524B6
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x000542BE File Offset: 0x000524BE
		[Filterable(false)]
		public bool EnableEventValidation { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x000542C7 File Offset: 0x000524C7
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x000542CF File Offset: 0x000524CF
		[Filterable(false)]
		public PagesEnableSessionState EnableSessionState { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x000542D8 File Offset: 0x000524D8
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x000542E0 File Offset: 0x000524E0
		[Directive(AllowedOnMobilePages = false)]
		public bool EnableTheming { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x000542E9 File Offset: 0x000524E9
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x000542F1 File Offset: 0x000524F1
		public bool EnableViewState { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x000542FA File Offset: 0x000524FA
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x00054302 File Offset: 0x00052502
		public bool EnableViewStateMac { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0005430B File Offset: 0x0005250B
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00054313 File Offset: 0x00052513
		[UrlProperty("*.asp;*.aspx;*.cshtml;*.vbhtml;*.asmx;*.asax;*.ashx;*.asix;*.ascx;*.htm;*.html;*.xml;*.xsd;*.config;*.asa;*.css;*.shtm;*.shtml;*.php;*.jsp")]
		public string ErrorPage { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0005431C File Offset: 0x0005251C
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x00054324 File Offset: 0x00052524
		[Filterable(false)]
		public bool Explicit { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0005432D File Offset: 0x0005252D
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x00054335 File Offset: 0x00052535
		[Filterable(false)]
		[Directive(RenameType = "class")]
		public string Inherits { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0005433E File Offset: 0x0005253E
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x00054346 File Offset: 0x00052546
		[Filterable(false)]
		[ReadOnly(true)]
		[Directive(ServerLanguageNames = true)]
		public string Language { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0005434F File Offset: 0x0005254F
		// (set) Token: 0x06000E28 RID: 3624 RVA: 0x00054357 File Offset: 0x00052557
		public string LCID { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00054360 File Offset: 0x00052560
		// (set) Token: 0x06000E2A RID: 3626 RVA: 0x00054368 File Offset: 0x00052568
		[Filterable(false)]
		public bool LinePragmas { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00054371 File Offset: 0x00052571
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x00054379 File Offset: 0x00052579
		public bool MaintainScrollPositionOnPostback { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00054382 File Offset: 0x00052582
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x0005438A File Offset: 0x0005258A
		[Directive(BuilderType = "master", AllowedOnMobilePages = false)]
		[UrlProperty("*.master")]
		public string MasterPageFile { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00054393 File Offset: 0x00052593
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x0005439B File Offset: 0x0005259B
		public string ResponseEncoding { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x000543A4 File Offset: 0x000525A4
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x000543AC File Offset: 0x000525AC
		[Browsable(false)]
		[Filterable(false)]
		public bool SmartNavigation { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x000543B5 File Offset: 0x000525B5
		// (set) Token: 0x06000E34 RID: 3636 RVA: 0x000543BD File Offset: 0x000525BD
		[Filterable(false)]
		[Directive(ServerLanguageExtensions = true)]
		[UrlProperty]
		public string Src { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x000543C6 File Offset: 0x000525C6
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x000543CE File Offset: 0x000525CE
		[Filterable(false)]
		public bool Strict { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x000543D7 File Offset: 0x000525D7
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x000543DF File Offset: 0x000525DF
		[Directive(AllowedOnMobilePages = false)]
		public string StylesheetTheme { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x000543E8 File Offset: 0x000525E8
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x000543F0 File Offset: 0x000525F0
		[Browsable(false)]
		[Filterable(false)]
		public string TargetSchema { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x000543F9 File Offset: 0x000525F9
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x00054401 File Offset: 0x00052601
		[Directive(AllowedOnMobilePages = false)]
		public string Theme { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0005440A File Offset: 0x0005260A
		// (set) Token: 0x06000E3E RID: 3646 RVA: 0x00054412 File Offset: 0x00052612
		[Directive(AllowedOnMobilePages = false)]
		public string Title { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0005441B File Offset: 0x0005261B
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x00054423 File Offset: 0x00052623
		[Filterable(false)]
		public bool Trace { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0005442C File Offset: 0x0005262C
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x00054434 File Offset: 0x00052634
		[Filterable(false)]
		public TraceMode TraceMode { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0005443D File Offset: 0x0005263D
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x00054445 File Offset: 0x00052645
		[Filterable(false)]
		public Transaction Transaction { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0005444E File Offset: 0x0005264E
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x00054456 File Offset: 0x00052656
		public string UICulture { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0005445F File Offset: 0x0005265F
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00054467 File Offset: 0x00052667
		[Filterable(false)]
		public bool ValidateRequest { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00054470 File Offset: 0x00052670
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00054478 File Offset: 0x00052678
		[Filterable(false)]
		public ViewStateEncryptionMode ViewStateEncryptionMode { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00054481 File Offset: 0x00052681
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00054489 File Offset: 0x00052689
		[Filterable(false)]
		[TypeConverter(typeof(WarningLevelConverter))]
		public WarningLevel WarningLevel { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00054492 File Offset: 0x00052692
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x0005449A File Offset: 0x0005269A
		[DefaultValue("Inherit")]
		public ClientIDMode ClientIDMode { get; set; }
	}
}
