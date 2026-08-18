using System;

namespace Spire.Doc.Documents
{
	// Token: 0x020004CC RID: 1228
	public enum BuiltinStyle
	{
		// Token: 0x0400319B RID: 12699
		Normal,
		// Token: 0x0400319C RID: 12700
		Heading1,
		// Token: 0x0400319D RID: 12701
		Heading2,
		// Token: 0x0400319E RID: 12702
		Heading3,
		// Token: 0x0400319F RID: 12703
		Heading4,
		// Token: 0x040031A0 RID: 12704
		Heading5,
		// Token: 0x040031A1 RID: 12705
		Heading6,
		// Token: 0x040031A2 RID: 12706
		Heading7,
		// Token: 0x040031A3 RID: 12707
		Heading8,
		// Token: 0x040031A4 RID: 12708
		Heading9,
		// Token: 0x040031A5 RID: 12709
		Index1,
		// Token: 0x040031A6 RID: 12710
		Index2,
		// Token: 0x040031A7 RID: 12711
		Index3,
		// Token: 0x040031A8 RID: 12712
		Index4,
		// Token: 0x040031A9 RID: 12713
		Index5,
		// Token: 0x040031AA RID: 12714
		Index6,
		// Token: 0x040031AB RID: 12715
		Index7,
		// Token: 0x040031AC RID: 12716
		Index8,
		// Token: 0x040031AD RID: 12717
		Index9,
		// Token: 0x040031AE RID: 12718
		Toc1,
		// Token: 0x040031AF RID: 12719
		Toc2,
		// Token: 0x040031B0 RID: 12720
		Toc3,
		// Token: 0x040031B1 RID: 12721
		Toc4,
		// Token: 0x040031B2 RID: 12722
		Toc5,
		// Token: 0x040031B3 RID: 12723
		Toc6,
		// Token: 0x040031B4 RID: 12724
		Toc7,
		// Token: 0x040031B5 RID: 12725
		Toc8,
		// Token: 0x040031B6 RID: 12726
		Toc9,
		// Token: 0x040031B7 RID: 12727
		NormalIndent,
		// Token: 0x040031B8 RID: 12728
		FootnoteText,
		// Token: 0x040031B9 RID: 12729
		CommentText,
		// Token: 0x040031BA RID: 12730
		Header,
		// Token: 0x040031BB RID: 12731
		Footer,
		// Token: 0x040031BC RID: 12732
		IndexHeading,
		// Token: 0x040031BD RID: 12733
		Caption,
		// Token: 0x040031BE RID: 12734
		TableOfFigures,
		// Token: 0x040031BF RID: 12735
		FootnoteReference,
		// Token: 0x040031C0 RID: 12736
		CommentReference,
		// Token: 0x040031C1 RID: 12737
		LineNumber,
		// Token: 0x040031C2 RID: 12738
		PageNumber,
		// Token: 0x040031C3 RID: 12739
		EndnoteReference,
		// Token: 0x040031C4 RID: 12740
		EndnoteText,
		// Token: 0x040031C5 RID: 12741
		TableOfAuthorities,
		// Token: 0x040031C6 RID: 12742
		MacroText,
		// Token: 0x040031C7 RID: 12743
		ToaHeading,
		// Token: 0x040031C8 RID: 12744
		List,
		// Token: 0x040031C9 RID: 12745
		ListBullet,
		// Token: 0x040031CA RID: 12746
		ListNumber,
		// Token: 0x040031CB RID: 12747
		List2,
		// Token: 0x040031CC RID: 12748
		List3,
		// Token: 0x040031CD RID: 12749
		List4,
		// Token: 0x040031CE RID: 12750
		List5,
		// Token: 0x040031CF RID: 12751
		ListBullet2,
		// Token: 0x040031D0 RID: 12752
		ListBullet3,
		// Token: 0x040031D1 RID: 12753
		ListBullet4,
		// Token: 0x040031D2 RID: 12754
		ListBullet5,
		// Token: 0x040031D3 RID: 12755
		ListNumber2,
		// Token: 0x040031D4 RID: 12756
		ListNumber3,
		// Token: 0x040031D5 RID: 12757
		ListNumber4,
		// Token: 0x040031D6 RID: 12758
		ListNumber5,
		// Token: 0x040031D7 RID: 12759
		Title,
		// Token: 0x040031D8 RID: 12760
		Closing,
		// Token: 0x040031D9 RID: 12761
		Signature,
		// Token: 0x040031DA RID: 12762
		DefaultParagraphFont,
		// Token: 0x040031DB RID: 12763
		BodyText,
		// Token: 0x040031DC RID: 12764
		BodyTextInd,
		// Token: 0x040031DD RID: 12765
		ListContinue,
		// Token: 0x040031DE RID: 12766
		ListContinue2,
		// Token: 0x040031DF RID: 12767
		ListContinue3,
		// Token: 0x040031E0 RID: 12768
		ListContinue4,
		// Token: 0x040031E1 RID: 12769
		ListContinue5,
		// Token: 0x040031E2 RID: 12770
		MessageHeader,
		// Token: 0x040031E3 RID: 12771
		Subtitle,
		// Token: 0x040031E4 RID: 12772
		Salutation,
		// Token: 0x040031E5 RID: 12773
		Date,
		// Token: 0x040031E6 RID: 12774
		BodyText1I,
		// Token: 0x040031E7 RID: 12775
		BodyText1I2,
		// Token: 0x040031E8 RID: 12776
		NoteHeading,
		// Token: 0x040031E9 RID: 12777
		BodyText2,
		// Token: 0x040031EA RID: 12778
		BodyText3,
		// Token: 0x040031EB RID: 12779
		BodyTextInd2,
		// Token: 0x040031EC RID: 12780
		BodyTextInd3,
		// Token: 0x040031ED RID: 12781
		BlockText,
		// Token: 0x040031EE RID: 12782
		Hyperlink,
		// Token: 0x040031EF RID: 12783
		FollowedHyperlink,
		// Token: 0x040031F0 RID: 12784
		Strong,
		// Token: 0x040031F1 RID: 12785
		Emphasis,
		// Token: 0x040031F2 RID: 12786
		DocumentMap,
		// Token: 0x040031F3 RID: 12787
		PlainText,
		// Token: 0x040031F4 RID: 12788
		EmailSignature,
		// Token: 0x040031F5 RID: 12789
		NormalWeb,
		// Token: 0x040031F6 RID: 12790
		HtmlAcronym,
		// Token: 0x040031F7 RID: 12791
		HtmlAddress,
		// Token: 0x040031F8 RID: 12792
		HtmlCite,
		// Token: 0x040031F9 RID: 12793
		HtmlCode,
		// Token: 0x040031FA RID: 12794
		HtmlDefinition,
		// Token: 0x040031FB RID: 12795
		HtmlKeyboard,
		// Token: 0x040031FC RID: 12796
		HtmlPreformatted,
		// Token: 0x040031FD RID: 12797
		HtmlSample,
		// Token: 0x040031FE RID: 12798
		HtmlTypewriter,
		// Token: 0x040031FF RID: 12799
		HtmlVariable,
		// Token: 0x04003200 RID: 12800
		CommentSubject,
		// Token: 0x04003201 RID: 12801
		NoList,
		// Token: 0x04003202 RID: 12802
		BalloonText,
		// Token: 0x04003203 RID: 12803
		User,
		// Token: 0x04003204 RID: 12804
		NoStyle
	}
}
