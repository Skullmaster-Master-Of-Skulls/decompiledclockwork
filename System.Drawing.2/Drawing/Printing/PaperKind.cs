using System;

namespace System.Drawing.Printing
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public enum PaperKind
	{
		// Token: 0x0400062A RID: 1578
		Custom,
		// Token: 0x0400062B RID: 1579
		Letter,
		// Token: 0x0400062C RID: 1580
		Legal = 5,
		// Token: 0x0400062D RID: 1581
		A4 = 9,
		// Token: 0x0400062E RID: 1582
		CSheet = 24,
		// Token: 0x0400062F RID: 1583
		DSheet,
		// Token: 0x04000630 RID: 1584
		ESheet,
		// Token: 0x04000631 RID: 1585
		LetterSmall = 2,
		// Token: 0x04000632 RID: 1586
		Tabloid,
		// Token: 0x04000633 RID: 1587
		Ledger,
		// Token: 0x04000634 RID: 1588
		Statement = 6,
		// Token: 0x04000635 RID: 1589
		Executive,
		// Token: 0x04000636 RID: 1590
		A3,
		// Token: 0x04000637 RID: 1591
		A4Small = 10,
		// Token: 0x04000638 RID: 1592
		A5,
		// Token: 0x04000639 RID: 1593
		B4,
		// Token: 0x0400063A RID: 1594
		B5,
		// Token: 0x0400063B RID: 1595
		Folio,
		// Token: 0x0400063C RID: 1596
		Quarto,
		// Token: 0x0400063D RID: 1597
		Standard10x14,
		// Token: 0x0400063E RID: 1598
		Standard11x17,
		// Token: 0x0400063F RID: 1599
		Note,
		// Token: 0x04000640 RID: 1600
		Number9Envelope,
		// Token: 0x04000641 RID: 1601
		Number10Envelope,
		// Token: 0x04000642 RID: 1602
		Number11Envelope,
		// Token: 0x04000643 RID: 1603
		Number12Envelope,
		// Token: 0x04000644 RID: 1604
		Number14Envelope,
		// Token: 0x04000645 RID: 1605
		DLEnvelope = 27,
		// Token: 0x04000646 RID: 1606
		C5Envelope,
		// Token: 0x04000647 RID: 1607
		C3Envelope,
		// Token: 0x04000648 RID: 1608
		C4Envelope,
		// Token: 0x04000649 RID: 1609
		C6Envelope,
		// Token: 0x0400064A RID: 1610
		C65Envelope,
		// Token: 0x0400064B RID: 1611
		B4Envelope,
		// Token: 0x0400064C RID: 1612
		B5Envelope,
		// Token: 0x0400064D RID: 1613
		B6Envelope,
		// Token: 0x0400064E RID: 1614
		ItalyEnvelope,
		// Token: 0x0400064F RID: 1615
		MonarchEnvelope,
		// Token: 0x04000650 RID: 1616
		PersonalEnvelope,
		// Token: 0x04000651 RID: 1617
		USStandardFanfold,
		// Token: 0x04000652 RID: 1618
		GermanStandardFanfold,
		// Token: 0x04000653 RID: 1619
		GermanLegalFanfold,
		// Token: 0x04000654 RID: 1620
		IsoB4,
		// Token: 0x04000655 RID: 1621
		JapanesePostcard,
		// Token: 0x04000656 RID: 1622
		Standard9x11,
		// Token: 0x04000657 RID: 1623
		Standard10x11,
		// Token: 0x04000658 RID: 1624
		Standard15x11,
		// Token: 0x04000659 RID: 1625
		InviteEnvelope,
		// Token: 0x0400065A RID: 1626
		LetterExtra = 50,
		// Token: 0x0400065B RID: 1627
		LegalExtra,
		// Token: 0x0400065C RID: 1628
		TabloidExtra,
		// Token: 0x0400065D RID: 1629
		A4Extra,
		// Token: 0x0400065E RID: 1630
		LetterTransverse,
		// Token: 0x0400065F RID: 1631
		A4Transverse,
		// Token: 0x04000660 RID: 1632
		LetterExtraTransverse,
		// Token: 0x04000661 RID: 1633
		APlus,
		// Token: 0x04000662 RID: 1634
		BPlus,
		// Token: 0x04000663 RID: 1635
		LetterPlus,
		// Token: 0x04000664 RID: 1636
		A4Plus,
		// Token: 0x04000665 RID: 1637
		A5Transverse,
		// Token: 0x04000666 RID: 1638
		B5Transverse,
		// Token: 0x04000667 RID: 1639
		A3Extra,
		// Token: 0x04000668 RID: 1640
		A5Extra,
		// Token: 0x04000669 RID: 1641
		B5Extra,
		// Token: 0x0400066A RID: 1642
		A2,
		// Token: 0x0400066B RID: 1643
		A3Transverse,
		// Token: 0x0400066C RID: 1644
		A3ExtraTransverse,
		// Token: 0x0400066D RID: 1645
		JapaneseDoublePostcard,
		// Token: 0x0400066E RID: 1646
		A6,
		// Token: 0x0400066F RID: 1647
		JapaneseEnvelopeKakuNumber2,
		// Token: 0x04000670 RID: 1648
		JapaneseEnvelopeKakuNumber3,
		// Token: 0x04000671 RID: 1649
		JapaneseEnvelopeChouNumber3,
		// Token: 0x04000672 RID: 1650
		JapaneseEnvelopeChouNumber4,
		// Token: 0x04000673 RID: 1651
		LetterRotated,
		// Token: 0x04000674 RID: 1652
		A3Rotated,
		// Token: 0x04000675 RID: 1653
		A4Rotated,
		// Token: 0x04000676 RID: 1654
		A5Rotated,
		// Token: 0x04000677 RID: 1655
		B4JisRotated,
		// Token: 0x04000678 RID: 1656
		B5JisRotated,
		// Token: 0x04000679 RID: 1657
		JapanesePostcardRotated,
		// Token: 0x0400067A RID: 1658
		JapaneseDoublePostcardRotated,
		// Token: 0x0400067B RID: 1659
		A6Rotated,
		// Token: 0x0400067C RID: 1660
		JapaneseEnvelopeKakuNumber2Rotated,
		// Token: 0x0400067D RID: 1661
		JapaneseEnvelopeKakuNumber3Rotated,
		// Token: 0x0400067E RID: 1662
		JapaneseEnvelopeChouNumber3Rotated,
		// Token: 0x0400067F RID: 1663
		JapaneseEnvelopeChouNumber4Rotated,
		// Token: 0x04000680 RID: 1664
		B6Jis,
		// Token: 0x04000681 RID: 1665
		B6JisRotated,
		// Token: 0x04000682 RID: 1666
		Standard12x11,
		// Token: 0x04000683 RID: 1667
		JapaneseEnvelopeYouNumber4,
		// Token: 0x04000684 RID: 1668
		JapaneseEnvelopeYouNumber4Rotated,
		// Token: 0x04000685 RID: 1669
		Prc16K,
		// Token: 0x04000686 RID: 1670
		Prc32K,
		// Token: 0x04000687 RID: 1671
		Prc32KBig,
		// Token: 0x04000688 RID: 1672
		PrcEnvelopeNumber1,
		// Token: 0x04000689 RID: 1673
		PrcEnvelopeNumber2,
		// Token: 0x0400068A RID: 1674
		PrcEnvelopeNumber3,
		// Token: 0x0400068B RID: 1675
		PrcEnvelopeNumber4,
		// Token: 0x0400068C RID: 1676
		PrcEnvelopeNumber5,
		// Token: 0x0400068D RID: 1677
		PrcEnvelopeNumber6,
		// Token: 0x0400068E RID: 1678
		PrcEnvelopeNumber7,
		// Token: 0x0400068F RID: 1679
		PrcEnvelopeNumber8,
		// Token: 0x04000690 RID: 1680
		PrcEnvelopeNumber9,
		// Token: 0x04000691 RID: 1681
		PrcEnvelopeNumber10,
		// Token: 0x04000692 RID: 1682
		Prc16KRotated,
		// Token: 0x04000693 RID: 1683
		Prc32KRotated,
		// Token: 0x04000694 RID: 1684
		Prc32KBigRotated,
		// Token: 0x04000695 RID: 1685
		PrcEnvelopeNumber1Rotated,
		// Token: 0x04000696 RID: 1686
		PrcEnvelopeNumber2Rotated,
		// Token: 0x04000697 RID: 1687
		PrcEnvelopeNumber3Rotated,
		// Token: 0x04000698 RID: 1688
		PrcEnvelopeNumber4Rotated,
		// Token: 0x04000699 RID: 1689
		PrcEnvelopeNumber5Rotated,
		// Token: 0x0400069A RID: 1690
		PrcEnvelopeNumber6Rotated,
		// Token: 0x0400069B RID: 1691
		PrcEnvelopeNumber7Rotated,
		// Token: 0x0400069C RID: 1692
		PrcEnvelopeNumber8Rotated,
		// Token: 0x0400069D RID: 1693
		PrcEnvelopeNumber9Rotated,
		// Token: 0x0400069E RID: 1694
		PrcEnvelopeNumber10Rotated
	}
}
