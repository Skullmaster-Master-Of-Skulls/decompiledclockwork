using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System.Xml
{
	// Token: 0x0200012C RID: 300
	internal sealed class Res
	{
		// Token: 0x060015ED RID: 5613 RVA: 0x000610F2 File Offset: 0x0005F2F2
		internal Res()
		{
			this.resources = new ResourceManager("System.Xml", base.GetType().Assembly);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00061118 File Offset: 0x0005F318
		private static Res GetLoader()
		{
			if (Res.loader == null)
			{
				Res value = new Res();
				Interlocked.CompareExchange<Res>(ref Res.loader, value, null);
			}
			return Res.loader;
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00061144 File Offset: 0x0005F344
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00061147 File Offset: 0x0005F347
		public static ResourceManager Resources
		{
			get
			{
				return Res.GetLoader().resources;
			}
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00061154 File Offset: 0x0005F354
		public static string GetString(string name, params object[] args)
		{
			Res res = Res.GetLoader();
			if (res == null)
			{
				return null;
			}
			string @string = res.resources.GetString(name, Res.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000611D4 File Offset: 0x0005F3D4
		public static string GetString(string name)
		{
			Res res = Res.GetLoader();
			if (res == null)
			{
				return null;
			}
			return res.resources.GetString(name, Res.Culture);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000611FD File Offset: 0x0005F3FD
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return Res.GetString(name);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00061208 File Offset: 0x0005F408
		public static object GetObject(string name)
		{
			Res res = Res.GetLoader();
			if (res == null)
			{
				return null;
			}
			return res.resources.GetObject(name, Res.Culture);
		}

		// Token: 0x04000657 RID: 1623
		internal const string Xml_UserException = "Xml_UserException";

		// Token: 0x04000658 RID: 1624
		internal const string Xml_DefaultException = "Xml_DefaultException";

		// Token: 0x04000659 RID: 1625
		internal const string Xml_InvalidOperation = "Xml_InvalidOperation";

		// Token: 0x0400065A RID: 1626
		internal const string Xml_ErrorFilePosition = "Xml_ErrorFilePosition";

		// Token: 0x0400065B RID: 1627
		internal const string Xml_StackOverflow = "Xml_StackOverflow";

		// Token: 0x0400065C RID: 1628
		internal const string Xslt_NoStylesheetLoaded = "Xslt_NoStylesheetLoaded";

		// Token: 0x0400065D RID: 1629
		internal const string Xslt_NotCompiledStylesheet = "Xslt_NotCompiledStylesheet";

		// Token: 0x0400065E RID: 1630
		internal const string Xslt_IncompatibleCompiledStylesheetVersion = "Xslt_IncompatibleCompiledStylesheetVersion";

		// Token: 0x0400065F RID: 1631
		internal const string Xml_AsyncIsRunningException = "Xml_AsyncIsRunningException";

		// Token: 0x04000660 RID: 1632
		internal const string Xml_ReaderAsyncNotSetException = "Xml_ReaderAsyncNotSetException";

		// Token: 0x04000661 RID: 1633
		internal const string Xml_UnclosedQuote = "Xml_UnclosedQuote";

		// Token: 0x04000662 RID: 1634
		internal const string Xml_UnexpectedEOF = "Xml_UnexpectedEOF";

		// Token: 0x04000663 RID: 1635
		internal const string Xml_UnexpectedEOF1 = "Xml_UnexpectedEOF1";

		// Token: 0x04000664 RID: 1636
		internal const string Xml_UnexpectedEOFInElementContent = "Xml_UnexpectedEOFInElementContent";

		// Token: 0x04000665 RID: 1637
		internal const string Xml_BadStartNameChar = "Xml_BadStartNameChar";

		// Token: 0x04000666 RID: 1638
		internal const string Xml_BadNameChar = "Xml_BadNameChar";

		// Token: 0x04000667 RID: 1639
		internal const string Xml_BadDecimalEntity = "Xml_BadDecimalEntity";

		// Token: 0x04000668 RID: 1640
		internal const string Xml_BadHexEntity = "Xml_BadHexEntity";

		// Token: 0x04000669 RID: 1641
		internal const string Xml_MissingByteOrderMark = "Xml_MissingByteOrderMark";

		// Token: 0x0400066A RID: 1642
		internal const string Xml_UnknownEncoding = "Xml_UnknownEncoding";

		// Token: 0x0400066B RID: 1643
		internal const string Xml_InternalError = "Xml_InternalError";

		// Token: 0x0400066C RID: 1644
		internal const string Xml_InvalidCharInThisEncoding = "Xml_InvalidCharInThisEncoding";

		// Token: 0x0400066D RID: 1645
		internal const string Xml_ErrorPosition = "Xml_ErrorPosition";

		// Token: 0x0400066E RID: 1646
		internal const string Xml_MessageWithErrorPosition = "Xml_MessageWithErrorPosition";

		// Token: 0x0400066F RID: 1647
		internal const string Xml_UnexpectedTokenEx = "Xml_UnexpectedTokenEx";

		// Token: 0x04000670 RID: 1648
		internal const string Xml_UnexpectedTokens2 = "Xml_UnexpectedTokens2";

		// Token: 0x04000671 RID: 1649
		internal const string Xml_ExpectingWhiteSpace = "Xml_ExpectingWhiteSpace";

		// Token: 0x04000672 RID: 1650
		internal const string Xml_TagMismatch = "Xml_TagMismatch";

		// Token: 0x04000673 RID: 1651
		internal const string Xml_TagMismatchEx = "Xml_TagMismatchEx";

		// Token: 0x04000674 RID: 1652
		internal const string Xml_UnexpectedEndTag = "Xml_UnexpectedEndTag";

		// Token: 0x04000675 RID: 1653
		internal const string Xml_UnknownNs = "Xml_UnknownNs";

		// Token: 0x04000676 RID: 1654
		internal const string Xml_BadAttributeChar = "Xml_BadAttributeChar";

		// Token: 0x04000677 RID: 1655
		internal const string Xml_ExpectExternalOrClose = "Xml_ExpectExternalOrClose";

		// Token: 0x04000678 RID: 1656
		internal const string Xml_MissingRoot = "Xml_MissingRoot";

		// Token: 0x04000679 RID: 1657
		internal const string Xml_MultipleRoots = "Xml_MultipleRoots";

		// Token: 0x0400067A RID: 1658
		internal const string Xml_InvalidRootData = "Xml_InvalidRootData";

		// Token: 0x0400067B RID: 1659
		internal const string Xml_XmlDeclNotFirst = "Xml_XmlDeclNotFirst";

		// Token: 0x0400067C RID: 1660
		internal const string Xml_InvalidXmlDecl = "Xml_InvalidXmlDecl";

		// Token: 0x0400067D RID: 1661
		internal const string Xml_InvalidNodeType = "Xml_InvalidNodeType";

		// Token: 0x0400067E RID: 1662
		internal const string Xml_InvalidPIName = "Xml_InvalidPIName";

		// Token: 0x0400067F RID: 1663
		internal const string Xml_InvalidXmlSpace = "Xml_InvalidXmlSpace";

		// Token: 0x04000680 RID: 1664
		internal const string Xml_InvalidVersionNumber = "Xml_InvalidVersionNumber";

		// Token: 0x04000681 RID: 1665
		internal const string Xml_DupAttributeName = "Xml_DupAttributeName";

		// Token: 0x04000682 RID: 1666
		internal const string Xml_BadDTDLocation = "Xml_BadDTDLocation";

		// Token: 0x04000683 RID: 1667
		internal const string Xml_ElementNotFound = "Xml_ElementNotFound";

		// Token: 0x04000684 RID: 1668
		internal const string Xml_ElementNotFoundNs = "Xml_ElementNotFoundNs";

		// Token: 0x04000685 RID: 1669
		internal const string Xml_PartialContentNodeTypeNotSupportedEx = "Xml_PartialContentNodeTypeNotSupportedEx";

		// Token: 0x04000686 RID: 1670
		internal const string Xml_MultipleDTDsProvided = "Xml_MultipleDTDsProvided";

		// Token: 0x04000687 RID: 1671
		internal const string Xml_CanNotBindToReservedNamespace = "Xml_CanNotBindToReservedNamespace";

		// Token: 0x04000688 RID: 1672
		internal const string Xml_InvalidCharacter = "Xml_InvalidCharacter";

		// Token: 0x04000689 RID: 1673
		internal const string Xml_InvalidBinHexValue = "Xml_InvalidBinHexValue";

		// Token: 0x0400068A RID: 1674
		internal const string Xml_InvalidBinHexValueOddCount = "Xml_InvalidBinHexValueOddCount";

		// Token: 0x0400068B RID: 1675
		internal const string Xml_InvalidTextDecl = "Xml_InvalidTextDecl";

		// Token: 0x0400068C RID: 1676
		internal const string Xml_InvalidBase64Value = "Xml_InvalidBase64Value";

		// Token: 0x0400068D RID: 1677
		internal const string Xml_UndeclaredEntity = "Xml_UndeclaredEntity";

		// Token: 0x0400068E RID: 1678
		internal const string Xml_RecursiveParEntity = "Xml_RecursiveParEntity";

		// Token: 0x0400068F RID: 1679
		internal const string Xml_RecursiveGenEntity = "Xml_RecursiveGenEntity";

		// Token: 0x04000690 RID: 1680
		internal const string Xml_ExternalEntityInAttValue = "Xml_ExternalEntityInAttValue";

		// Token: 0x04000691 RID: 1681
		internal const string Xml_UnparsedEntityRef = "Xml_UnparsedEntityRef";

		// Token: 0x04000692 RID: 1682
		internal const string Xml_NotSameNametable = "Xml_NotSameNametable";

		// Token: 0x04000693 RID: 1683
		internal const string Xml_NametableMismatch = "Xml_NametableMismatch";

		// Token: 0x04000694 RID: 1684
		internal const string Xml_BadNamespaceDecl = "Xml_BadNamespaceDecl";

		// Token: 0x04000695 RID: 1685
		internal const string Xml_ErrorParsingEntityName = "Xml_ErrorParsingEntityName";

		// Token: 0x04000696 RID: 1686
		internal const string Xml_InvalidNmToken = "Xml_InvalidNmToken";

		// Token: 0x04000697 RID: 1687
		internal const string Xml_EntityRefNesting = "Xml_EntityRefNesting";

		// Token: 0x04000698 RID: 1688
		internal const string Xml_CannotResolveEntity = "Xml_CannotResolveEntity";

		// Token: 0x04000699 RID: 1689
		internal const string Xml_CannotResolveEntityDtdIgnored = "Xml_CannotResolveEntityDtdIgnored";

		// Token: 0x0400069A RID: 1690
		internal const string Xml_CannotResolveExternalSubset = "Xml_CannotResolveExternalSubset";

		// Token: 0x0400069B RID: 1691
		internal const string Xml_CannotResolveUrl = "Xml_CannotResolveUrl";

		// Token: 0x0400069C RID: 1692
		internal const string Xml_CDATAEndInText = "Xml_CDATAEndInText";

		// Token: 0x0400069D RID: 1693
		internal const string Xml_ExternalEntityInStandAloneDocument = "Xml_ExternalEntityInStandAloneDocument";

		// Token: 0x0400069E RID: 1694
		internal const string Xml_DtdAfterRootElement = "Xml_DtdAfterRootElement";

		// Token: 0x0400069F RID: 1695
		internal const string Xml_ReadOnlyProperty = "Xml_ReadOnlyProperty";

		// Token: 0x040006A0 RID: 1696
		internal const string Xml_DtdIsProhibited = "Xml_DtdIsProhibited";

		// Token: 0x040006A1 RID: 1697
		internal const string Xml_DtdIsProhibitedEx = "Xml_DtdIsProhibitedEx";

		// Token: 0x040006A2 RID: 1698
		internal const string Xml_ReadSubtreeNotOnElement = "Xml_ReadSubtreeNotOnElement";

		// Token: 0x040006A3 RID: 1699
		internal const string Xml_DtdNotAllowedInFragment = "Xml_DtdNotAllowedInFragment";

		// Token: 0x040006A4 RID: 1700
		internal const string Xml_CannotStartDocumentOnFragment = "Xml_CannotStartDocumentOnFragment";

		// Token: 0x040006A5 RID: 1701
		internal const string Xml_ErrorOpeningExternalDtd = "Xml_ErrorOpeningExternalDtd";

		// Token: 0x040006A6 RID: 1702
		internal const string Xml_ErrorOpeningExternalEntity = "Xml_ErrorOpeningExternalEntity";

		// Token: 0x040006A7 RID: 1703
		internal const string Xml_ReadBinaryContentNotSupported = "Xml_ReadBinaryContentNotSupported";

		// Token: 0x040006A8 RID: 1704
		internal const string Xml_ReadValueChunkNotSupported = "Xml_ReadValueChunkNotSupported";

		// Token: 0x040006A9 RID: 1705
		internal const string Xml_InvalidReadContentAs = "Xml_InvalidReadContentAs";

		// Token: 0x040006AA RID: 1706
		internal const string Xml_InvalidReadElementContentAs = "Xml_InvalidReadElementContentAs";

		// Token: 0x040006AB RID: 1707
		internal const string Xml_MixedReadElementContentAs = "Xml_MixedReadElementContentAs";

		// Token: 0x040006AC RID: 1708
		internal const string Xml_MixingReadValueChunkWithBinary = "Xml_MixingReadValueChunkWithBinary";

		// Token: 0x040006AD RID: 1709
		internal const string Xml_MixingBinaryContentMethods = "Xml_MixingBinaryContentMethods";

		// Token: 0x040006AE RID: 1710
		internal const string Xml_MixingV1StreamingWithV2Binary = "Xml_MixingV1StreamingWithV2Binary";

		// Token: 0x040006AF RID: 1711
		internal const string Xml_InvalidReadValueChunk = "Xml_InvalidReadValueChunk";

		// Token: 0x040006B0 RID: 1712
		internal const string Xml_ReadContentAsFormatException = "Xml_ReadContentAsFormatException";

		// Token: 0x040006B1 RID: 1713
		internal const string Xml_DoubleBaseUri = "Xml_DoubleBaseUri";

		// Token: 0x040006B2 RID: 1714
		internal const string Xml_NotEnoughSpaceForSurrogatePair = "Xml_NotEnoughSpaceForSurrogatePair";

		// Token: 0x040006B3 RID: 1715
		internal const string Xml_EmptyUrl = "Xml_EmptyUrl";

		// Token: 0x040006B4 RID: 1716
		internal const string Xml_UnexpectedNodeInSimpleContent = "Xml_UnexpectedNodeInSimpleContent";

		// Token: 0x040006B5 RID: 1717
		internal const string Xml_InvalidWhitespaceCharacter = "Xml_InvalidWhitespaceCharacter";

		// Token: 0x040006B6 RID: 1718
		internal const string Xml_IncompatibleConformanceLevel = "Xml_IncompatibleConformanceLevel";

		// Token: 0x040006B7 RID: 1719
		internal const string Xml_LimitExceeded = "Xml_LimitExceeded";

		// Token: 0x040006B8 RID: 1720
		internal const string Xml_ClosedOrErrorReader = "Xml_ClosedOrErrorReader";

		// Token: 0x040006B9 RID: 1721
		internal const string Xml_CharEntityOverflow = "Xml_CharEntityOverflow";

		// Token: 0x040006BA RID: 1722
		internal const string Xml_BadNameCharWithPos = "Xml_BadNameCharWithPos";

		// Token: 0x040006BB RID: 1723
		internal const string Xml_XmlnsBelongsToReservedNs = "Xml_XmlnsBelongsToReservedNs";

		// Token: 0x040006BC RID: 1724
		internal const string Xml_UndeclaredParEntity = "Xml_UndeclaredParEntity";

		// Token: 0x040006BD RID: 1725
		internal const string Xml_InvalidXmlDocument = "Xml_InvalidXmlDocument";

		// Token: 0x040006BE RID: 1726
		internal const string Xml_NoDTDPresent = "Xml_NoDTDPresent";

		// Token: 0x040006BF RID: 1727
		internal const string Xml_MultipleValidaitonTypes = "Xml_MultipleValidaitonTypes";

		// Token: 0x040006C0 RID: 1728
		internal const string Xml_NoValidation = "Xml_NoValidation";

		// Token: 0x040006C1 RID: 1729
		internal const string Xml_WhitespaceHandling = "Xml_WhitespaceHandling";

		// Token: 0x040006C2 RID: 1730
		internal const string Xml_InvalidResetStateCall = "Xml_InvalidResetStateCall";

		// Token: 0x040006C3 RID: 1731
		internal const string Xml_EntityHandling = "Xml_EntityHandling";

		// Token: 0x040006C4 RID: 1732
		internal const string Xml_AttlistDuplEnumValue = "Xml_AttlistDuplEnumValue";

		// Token: 0x040006C5 RID: 1733
		internal const string Xml_AttlistDuplNotationValue = "Xml_AttlistDuplNotationValue";

		// Token: 0x040006C6 RID: 1734
		internal const string Xml_EncodingSwitchAfterResetState = "Xml_EncodingSwitchAfterResetState";

		// Token: 0x040006C7 RID: 1735
		internal const string Xml_UnexpectedNodeType = "Xml_UnexpectedNodeType";

		// Token: 0x040006C8 RID: 1736
		internal const string Xml_InvalidConditionalSection = "Xml_InvalidConditionalSection";

		// Token: 0x040006C9 RID: 1737
		internal const string Xml_UnexpectedCDataEnd = "Xml_UnexpectedCDataEnd";

		// Token: 0x040006CA RID: 1738
		internal const string Xml_UnclosedConditionalSection = "Xml_UnclosedConditionalSection";

		// Token: 0x040006CB RID: 1739
		internal const string Xml_ExpectDtdMarkup = "Xml_ExpectDtdMarkup";

		// Token: 0x040006CC RID: 1740
		internal const string Xml_IncompleteDtdContent = "Xml_IncompleteDtdContent";

		// Token: 0x040006CD RID: 1741
		internal const string Xml_EnumerationRequired = "Xml_EnumerationRequired";

		// Token: 0x040006CE RID: 1742
		internal const string Xml_InvalidContentModel = "Xml_InvalidContentModel";

		// Token: 0x040006CF RID: 1743
		internal const string Xml_FragmentId = "Xml_FragmentId";

		// Token: 0x040006D0 RID: 1744
		internal const string Xml_ExpectPcData = "Xml_ExpectPcData";

		// Token: 0x040006D1 RID: 1745
		internal const string Xml_ExpectNoWhitespace = "Xml_ExpectNoWhitespace";

		// Token: 0x040006D2 RID: 1746
		internal const string Xml_ExpectOp = "Xml_ExpectOp";

		// Token: 0x040006D3 RID: 1747
		internal const string Xml_InvalidAttributeType = "Xml_InvalidAttributeType";

		// Token: 0x040006D4 RID: 1748
		internal const string Xml_InvalidAttributeType1 = "Xml_InvalidAttributeType1";

		// Token: 0x040006D5 RID: 1749
		internal const string Xml_ExpectAttType = "Xml_ExpectAttType";

		// Token: 0x040006D6 RID: 1750
		internal const string Xml_ColonInLocalName = "Xml_ColonInLocalName";

		// Token: 0x040006D7 RID: 1751
		internal const string Xml_InvalidParEntityRef = "Xml_InvalidParEntityRef";

		// Token: 0x040006D8 RID: 1752
		internal const string Xml_ExpectSubOrClose = "Xml_ExpectSubOrClose";

		// Token: 0x040006D9 RID: 1753
		internal const string Xml_ExpectExternalOrPublicId = "Xml_ExpectExternalOrPublicId";

		// Token: 0x040006DA RID: 1754
		internal const string Xml_ExpectExternalIdOrEntityValue = "Xml_ExpectExternalIdOrEntityValue";

		// Token: 0x040006DB RID: 1755
		internal const string Xml_ExpectIgnoreOrInclude = "Xml_ExpectIgnoreOrInclude";

		// Token: 0x040006DC RID: 1756
		internal const string Xml_UnsupportedClass = "Xml_UnsupportedClass";

		// Token: 0x040006DD RID: 1757
		internal const string Xml_NullResolver = "Xml_NullResolver";

		// Token: 0x040006DE RID: 1758
		internal const string Xml_RelativeUriNotSupported = "Xml_RelativeUriNotSupported";

		// Token: 0x040006DF RID: 1759
		internal const string Xml_UntrustedCodeSettingResolver = "Xml_UntrustedCodeSettingResolver";

		// Token: 0x040006E0 RID: 1760
		internal const string Xml_WriterAsyncNotSetException = "Xml_WriterAsyncNotSetException";

		// Token: 0x040006E1 RID: 1761
		internal const string Xml_PrefixForEmptyNs = "Xml_PrefixForEmptyNs";

		// Token: 0x040006E2 RID: 1762
		internal const string Xml_InvalidCommentChars = "Xml_InvalidCommentChars";

		// Token: 0x040006E3 RID: 1763
		internal const string Xml_UndefNamespace = "Xml_UndefNamespace";

		// Token: 0x040006E4 RID: 1764
		internal const string Xml_EmptyName = "Xml_EmptyName";

		// Token: 0x040006E5 RID: 1765
		internal const string Xml_EmptyLocalName = "Xml_EmptyLocalName";

		// Token: 0x040006E6 RID: 1766
		internal const string Xml_InvalidNameCharsDetail = "Xml_InvalidNameCharsDetail";

		// Token: 0x040006E7 RID: 1767
		internal const string Xml_NoStartTag = "Xml_NoStartTag";

		// Token: 0x040006E8 RID: 1768
		internal const string Xml_ClosedOrError = "Xml_ClosedOrError";

		// Token: 0x040006E9 RID: 1769
		internal const string Xml_WrongToken = "Xml_WrongToken";

		// Token: 0x040006EA RID: 1770
		internal const string Xml_XmlPrefix = "Xml_XmlPrefix";

		// Token: 0x040006EB RID: 1771
		internal const string Xml_XmlnsPrefix = "Xml_XmlnsPrefix";

		// Token: 0x040006EC RID: 1772
		internal const string Xml_NamespaceDeclXmlXmlns = "Xml_NamespaceDeclXmlXmlns";

		// Token: 0x040006ED RID: 1773
		internal const string Xml_NonWhitespace = "Xml_NonWhitespace";

		// Token: 0x040006EE RID: 1774
		internal const string Xml_DupXmlDecl = "Xml_DupXmlDecl";

		// Token: 0x040006EF RID: 1775
		internal const string Xml_CannotWriteXmlDecl = "Xml_CannotWriteXmlDecl";

		// Token: 0x040006F0 RID: 1776
		internal const string Xml_NoRoot = "Xml_NoRoot";

		// Token: 0x040006F1 RID: 1777
		internal const string Xml_InvalidPosition = "Xml_InvalidPosition";

		// Token: 0x040006F2 RID: 1778
		internal const string Xml_IncompleteEntity = "Xml_IncompleteEntity";

		// Token: 0x040006F3 RID: 1779
		internal const string Xml_InvalidSurrogateHighChar = "Xml_InvalidSurrogateHighChar";

		// Token: 0x040006F4 RID: 1780
		internal const string Xml_InvalidSurrogateMissingLowChar = "Xml_InvalidSurrogateMissingLowChar";

		// Token: 0x040006F5 RID: 1781
		internal const string Xml_InvalidSurrogatePairWithArgs = "Xml_InvalidSurrogatePairWithArgs";

		// Token: 0x040006F6 RID: 1782
		internal const string Xml_RedefinePrefix = "Xml_RedefinePrefix";

		// Token: 0x040006F7 RID: 1783
		internal const string Xml_DtdAlreadyWritten = "Xml_DtdAlreadyWritten";

		// Token: 0x040006F8 RID: 1784
		internal const string Xml_InvalidCharsInIndent = "Xml_InvalidCharsInIndent";

		// Token: 0x040006F9 RID: 1785
		internal const string Xml_IndentCharsNotWhitespace = "Xml_IndentCharsNotWhitespace";

		// Token: 0x040006FA RID: 1786
		internal const string Xml_ConformanceLevelFragment = "Xml_ConformanceLevelFragment";

		// Token: 0x040006FB RID: 1787
		internal const string Xml_InvalidQuote = "Xml_InvalidQuote";

		// Token: 0x040006FC RID: 1788
		internal const string Xml_UndefPrefix = "Xml_UndefPrefix";

		// Token: 0x040006FD RID: 1789
		internal const string Xml_NoNamespaces = "Xml_NoNamespaces";

		// Token: 0x040006FE RID: 1790
		internal const string Xml_InvalidCDataChars = "Xml_InvalidCDataChars";

		// Token: 0x040006FF RID: 1791
		internal const string Xml_NotTheFirst = "Xml_NotTheFirst";

		// Token: 0x04000700 RID: 1792
		internal const string Xml_InvalidPiChars = "Xml_InvalidPiChars";

		// Token: 0x04000701 RID: 1793
		internal const string Xml_InvalidNameChars = "Xml_InvalidNameChars";

		// Token: 0x04000702 RID: 1794
		internal const string Xml_Closed = "Xml_Closed";

		// Token: 0x04000703 RID: 1795
		internal const string Xml_InvalidPrefix = "Xml_InvalidPrefix";

		// Token: 0x04000704 RID: 1796
		internal const string Xml_InvalidIndentation = "Xml_InvalidIndentation";

		// Token: 0x04000705 RID: 1797
		internal const string Xml_NotInWriteState = "Xml_NotInWriteState";

		// Token: 0x04000706 RID: 1798
		internal const string Xml_SurrogatePairSplit = "Xml_SurrogatePairSplit";

		// Token: 0x04000707 RID: 1799
		internal const string Xml_NoMultipleRoots = "Xml_NoMultipleRoots";

		// Token: 0x04000708 RID: 1800
		internal const string XmlBadName = "XmlBadName";

		// Token: 0x04000709 RID: 1801
		internal const string XmlNoNameAllowed = "XmlNoNameAllowed";

		// Token: 0x0400070A RID: 1802
		internal const string XmlConvert_BadUri = "XmlConvert_BadUri";

		// Token: 0x0400070B RID: 1803
		internal const string XmlConvert_BadFormat = "XmlConvert_BadFormat";

		// Token: 0x0400070C RID: 1804
		internal const string XmlConvert_Overflow = "XmlConvert_Overflow";

		// Token: 0x0400070D RID: 1805
		internal const string XmlConvert_TypeBadMapping = "XmlConvert_TypeBadMapping";

		// Token: 0x0400070E RID: 1806
		internal const string XmlConvert_TypeBadMapping2 = "XmlConvert_TypeBadMapping2";

		// Token: 0x0400070F RID: 1807
		internal const string XmlConvert_TypeListBadMapping = "XmlConvert_TypeListBadMapping";

		// Token: 0x04000710 RID: 1808
		internal const string XmlConvert_TypeListBadMapping2 = "XmlConvert_TypeListBadMapping2";

		// Token: 0x04000711 RID: 1809
		internal const string XmlConvert_TypeToString = "XmlConvert_TypeToString";

		// Token: 0x04000712 RID: 1810
		internal const string XmlConvert_TypeFromString = "XmlConvert_TypeFromString";

		// Token: 0x04000713 RID: 1811
		internal const string XmlConvert_TypeNoPrefix = "XmlConvert_TypeNoPrefix";

		// Token: 0x04000714 RID: 1812
		internal const string XmlConvert_TypeNoNamespace = "XmlConvert_TypeNoNamespace";

		// Token: 0x04000715 RID: 1813
		internal const string XmlConvert_NotOneCharString = "XmlConvert_NotOneCharString";

		// Token: 0x04000716 RID: 1814
		internal const string Sch_ParEntityRefNesting = "Sch_ParEntityRefNesting";

		// Token: 0x04000717 RID: 1815
		internal const string Sch_NotTokenString = "Sch_NotTokenString";

		// Token: 0x04000718 RID: 1816
		internal const string Sch_XsdDateTimeCompare = "Sch_XsdDateTimeCompare";

		// Token: 0x04000719 RID: 1817
		internal const string Sch_InvalidNullCast = "Sch_InvalidNullCast";

		// Token: 0x0400071A RID: 1818
		internal const string Sch_InvalidDateTimeOption = "Sch_InvalidDateTimeOption";

		// Token: 0x0400071B RID: 1819
		internal const string Sch_StandAloneNormalization = "Sch_StandAloneNormalization";

		// Token: 0x0400071C RID: 1820
		internal const string Sch_UnSpecifiedDefaultAttributeInExternalStandalone = "Sch_UnSpecifiedDefaultAttributeInExternalStandalone";

		// Token: 0x0400071D RID: 1821
		internal const string Sch_DefaultException = "Sch_DefaultException";

		// Token: 0x0400071E RID: 1822
		internal const string Sch_DupElementDecl = "Sch_DupElementDecl";

		// Token: 0x0400071F RID: 1823
		internal const string Sch_IdAttrDeclared = "Sch_IdAttrDeclared";

		// Token: 0x04000720 RID: 1824
		internal const string Sch_RootMatchDocType = "Sch_RootMatchDocType";

		// Token: 0x04000721 RID: 1825
		internal const string Sch_DupId = "Sch_DupId";

		// Token: 0x04000722 RID: 1826
		internal const string Sch_UndeclaredElement = "Sch_UndeclaredElement";

		// Token: 0x04000723 RID: 1827
		internal const string Sch_UndeclaredAttribute = "Sch_UndeclaredAttribute";

		// Token: 0x04000724 RID: 1828
		internal const string Sch_UndeclaredNotation = "Sch_UndeclaredNotation";

		// Token: 0x04000725 RID: 1829
		internal const string Sch_UndeclaredId = "Sch_UndeclaredId";

		// Token: 0x04000726 RID: 1830
		internal const string Sch_SchemaRootExpected = "Sch_SchemaRootExpected";

		// Token: 0x04000727 RID: 1831
		internal const string Sch_XSDSchemaRootExpected = "Sch_XSDSchemaRootExpected";

		// Token: 0x04000728 RID: 1832
		internal const string Sch_UnsupportedAttribute = "Sch_UnsupportedAttribute";

		// Token: 0x04000729 RID: 1833
		internal const string Sch_UnsupportedElement = "Sch_UnsupportedElement";

		// Token: 0x0400072A RID: 1834
		internal const string Sch_MissAttribute = "Sch_MissAttribute";

		// Token: 0x0400072B RID: 1835
		internal const string Sch_AnnotationLocation = "Sch_AnnotationLocation";

		// Token: 0x0400072C RID: 1836
		internal const string Sch_DataTypeTextOnly = "Sch_DataTypeTextOnly";

		// Token: 0x0400072D RID: 1837
		internal const string Sch_UnknownModel = "Sch_UnknownModel";

		// Token: 0x0400072E RID: 1838
		internal const string Sch_UnknownOrder = "Sch_UnknownOrder";

		// Token: 0x0400072F RID: 1839
		internal const string Sch_UnknownContent = "Sch_UnknownContent";

		// Token: 0x04000730 RID: 1840
		internal const string Sch_UnknownRequired = "Sch_UnknownRequired";

		// Token: 0x04000731 RID: 1841
		internal const string Sch_UnknownDtType = "Sch_UnknownDtType";

		// Token: 0x04000732 RID: 1842
		internal const string Sch_MixedMany = "Sch_MixedMany";

		// Token: 0x04000733 RID: 1843
		internal const string Sch_GroupDisabled = "Sch_GroupDisabled";

		// Token: 0x04000734 RID: 1844
		internal const string Sch_MissDtvalue = "Sch_MissDtvalue";

		// Token: 0x04000735 RID: 1845
		internal const string Sch_MissDtvaluesAttribute = "Sch_MissDtvaluesAttribute";

		// Token: 0x04000736 RID: 1846
		internal const string Sch_DupDtType = "Sch_DupDtType";

		// Token: 0x04000737 RID: 1847
		internal const string Sch_DupAttribute = "Sch_DupAttribute";

		// Token: 0x04000738 RID: 1848
		internal const string Sch_RequireEnumeration = "Sch_RequireEnumeration";

		// Token: 0x04000739 RID: 1849
		internal const string Sch_DefaultIdValue = "Sch_DefaultIdValue";

		// Token: 0x0400073A RID: 1850
		internal const string Sch_ElementNotAllowed = "Sch_ElementNotAllowed";

		// Token: 0x0400073B RID: 1851
		internal const string Sch_ElementMissing = "Sch_ElementMissing";

		// Token: 0x0400073C RID: 1852
		internal const string Sch_ManyMaxOccurs = "Sch_ManyMaxOccurs";

		// Token: 0x0400073D RID: 1853
		internal const string Sch_MaxOccursInvalid = "Sch_MaxOccursInvalid";

		// Token: 0x0400073E RID: 1854
		internal const string Sch_MinOccursInvalid = "Sch_MinOccursInvalid";

		// Token: 0x0400073F RID: 1855
		internal const string Sch_DtMaxLengthInvalid = "Sch_DtMaxLengthInvalid";

		// Token: 0x04000740 RID: 1856
		internal const string Sch_DtMinLengthInvalid = "Sch_DtMinLengthInvalid";

		// Token: 0x04000741 RID: 1857
		internal const string Sch_DupDtMaxLength = "Sch_DupDtMaxLength";

		// Token: 0x04000742 RID: 1858
		internal const string Sch_DupDtMinLength = "Sch_DupDtMinLength";

		// Token: 0x04000743 RID: 1859
		internal const string Sch_DtMinMaxLength = "Sch_DtMinMaxLength";

		// Token: 0x04000744 RID: 1860
		internal const string Sch_DupElement = "Sch_DupElement";

		// Token: 0x04000745 RID: 1861
		internal const string Sch_DupGroupParticle = "Sch_DupGroupParticle";

		// Token: 0x04000746 RID: 1862
		internal const string Sch_InvalidValue = "Sch_InvalidValue";

		// Token: 0x04000747 RID: 1863
		internal const string Sch_InvalidValueDetailed = "Sch_InvalidValueDetailed";

		// Token: 0x04000748 RID: 1864
		internal const string Sch_InvalidValueDetailedAttribute = "Sch_InvalidValueDetailedAttribute";

		// Token: 0x04000749 RID: 1865
		internal const string Sch_MissRequiredAttribute = "Sch_MissRequiredAttribute";

		// Token: 0x0400074A RID: 1866
		internal const string Sch_FixedAttributeValue = "Sch_FixedAttributeValue";

		// Token: 0x0400074B RID: 1867
		internal const string Sch_FixedElementValue = "Sch_FixedElementValue";

		// Token: 0x0400074C RID: 1868
		internal const string Sch_AttributeValueDataTypeDetailed = "Sch_AttributeValueDataTypeDetailed";

		// Token: 0x0400074D RID: 1869
		internal const string Sch_AttributeDefaultDataType = "Sch_AttributeDefaultDataType";

		// Token: 0x0400074E RID: 1870
		internal const string Sch_IncludeLocation = "Sch_IncludeLocation";

		// Token: 0x0400074F RID: 1871
		internal const string Sch_ImportLocation = "Sch_ImportLocation";

		// Token: 0x04000750 RID: 1872
		internal const string Sch_RedefineLocation = "Sch_RedefineLocation";

		// Token: 0x04000751 RID: 1873
		internal const string Sch_InvalidBlockDefaultValue = "Sch_InvalidBlockDefaultValue";

		// Token: 0x04000752 RID: 1874
		internal const string Sch_InvalidFinalDefaultValue = "Sch_InvalidFinalDefaultValue";

		// Token: 0x04000753 RID: 1875
		internal const string Sch_InvalidElementBlockValue = "Sch_InvalidElementBlockValue";

		// Token: 0x04000754 RID: 1876
		internal const string Sch_InvalidElementFinalValue = "Sch_InvalidElementFinalValue";

		// Token: 0x04000755 RID: 1877
		internal const string Sch_InvalidSimpleTypeFinalValue = "Sch_InvalidSimpleTypeFinalValue";

		// Token: 0x04000756 RID: 1878
		internal const string Sch_InvalidComplexTypeBlockValue = "Sch_InvalidComplexTypeBlockValue";

		// Token: 0x04000757 RID: 1879
		internal const string Sch_InvalidComplexTypeFinalValue = "Sch_InvalidComplexTypeFinalValue";

		// Token: 0x04000758 RID: 1880
		internal const string Sch_DupIdentityConstraint = "Sch_DupIdentityConstraint";

		// Token: 0x04000759 RID: 1881
		internal const string Sch_DupGlobalElement = "Sch_DupGlobalElement";

		// Token: 0x0400075A RID: 1882
		internal const string Sch_DupGlobalAttribute = "Sch_DupGlobalAttribute";

		// Token: 0x0400075B RID: 1883
		internal const string Sch_DupSimpleType = "Sch_DupSimpleType";

		// Token: 0x0400075C RID: 1884
		internal const string Sch_DupComplexType = "Sch_DupComplexType";

		// Token: 0x0400075D RID: 1885
		internal const string Sch_DupGroup = "Sch_DupGroup";

		// Token: 0x0400075E RID: 1886
		internal const string Sch_DupAttributeGroup = "Sch_DupAttributeGroup";

		// Token: 0x0400075F RID: 1887
		internal const string Sch_DupNotation = "Sch_DupNotation";

		// Token: 0x04000760 RID: 1888
		internal const string Sch_DefaultFixedAttributes = "Sch_DefaultFixedAttributes";

		// Token: 0x04000761 RID: 1889
		internal const string Sch_FixedInRef = "Sch_FixedInRef";

		// Token: 0x04000762 RID: 1890
		internal const string Sch_FixedDefaultInRef = "Sch_FixedDefaultInRef";

		// Token: 0x04000763 RID: 1891
		internal const string Sch_DupXsdElement = "Sch_DupXsdElement";

		// Token: 0x04000764 RID: 1892
		internal const string Sch_ForbiddenAttribute = "Sch_ForbiddenAttribute";

		// Token: 0x04000765 RID: 1893
		internal const string Sch_AttributeIgnored = "Sch_AttributeIgnored";

		// Token: 0x04000766 RID: 1894
		internal const string Sch_ElementRef = "Sch_ElementRef";

		// Token: 0x04000767 RID: 1895
		internal const string Sch_TypeMutualExclusive = "Sch_TypeMutualExclusive";

		// Token: 0x04000768 RID: 1896
		internal const string Sch_ElementNameRef = "Sch_ElementNameRef";

		// Token: 0x04000769 RID: 1897
		internal const string Sch_AttributeNameRef = "Sch_AttributeNameRef";

		// Token: 0x0400076A RID: 1898
		internal const string Sch_TextNotAllowed = "Sch_TextNotAllowed";

		// Token: 0x0400076B RID: 1899
		internal const string Sch_UndeclaredType = "Sch_UndeclaredType";

		// Token: 0x0400076C RID: 1900
		internal const string Sch_UndeclaredSimpleType = "Sch_UndeclaredSimpleType";

		// Token: 0x0400076D RID: 1901
		internal const string Sch_UndeclaredEquivClass = "Sch_UndeclaredEquivClass";

		// Token: 0x0400076E RID: 1902
		internal const string Sch_AttListPresence = "Sch_AttListPresence";

		// Token: 0x0400076F RID: 1903
		internal const string Sch_NotationValue = "Sch_NotationValue";

		// Token: 0x04000770 RID: 1904
		internal const string Sch_EnumerationValue = "Sch_EnumerationValue";

		// Token: 0x04000771 RID: 1905
		internal const string Sch_EmptyAttributeValue = "Sch_EmptyAttributeValue";

		// Token: 0x04000772 RID: 1906
		internal const string Sch_InvalidLanguageId = "Sch_InvalidLanguageId";

		// Token: 0x04000773 RID: 1907
		internal const string Sch_XmlSpace = "Sch_XmlSpace";

		// Token: 0x04000774 RID: 1908
		internal const string Sch_InvalidXsdAttributeValue = "Sch_InvalidXsdAttributeValue";

		// Token: 0x04000775 RID: 1909
		internal const string Sch_InvalidXsdAttributeDatatypeValue = "Sch_InvalidXsdAttributeDatatypeValue";

		// Token: 0x04000776 RID: 1910
		internal const string Sch_ElementValueDataTypeDetailed = "Sch_ElementValueDataTypeDetailed";

		// Token: 0x04000777 RID: 1911
		internal const string Sch_InvalidElementDefaultValue = "Sch_InvalidElementDefaultValue";

		// Token: 0x04000778 RID: 1912
		internal const string Sch_NonDeterministic = "Sch_NonDeterministic";

		// Token: 0x04000779 RID: 1913
		internal const string Sch_NonDeterministicAnyEx = "Sch_NonDeterministicAnyEx";

		// Token: 0x0400077A RID: 1914
		internal const string Sch_NonDeterministicAnyAny = "Sch_NonDeterministicAnyAny";

		// Token: 0x0400077B RID: 1915
		internal const string Sch_StandAlone = "Sch_StandAlone";

		// Token: 0x0400077C RID: 1916
		internal const string Sch_XmlNsAttribute = "Sch_XmlNsAttribute";

		// Token: 0x0400077D RID: 1917
		internal const string Sch_AllElement = "Sch_AllElement";

		// Token: 0x0400077E RID: 1918
		internal const string Sch_MismatchTargetNamespaceInclude = "Sch_MismatchTargetNamespaceInclude";

		// Token: 0x0400077F RID: 1919
		internal const string Sch_MismatchTargetNamespaceImport = "Sch_MismatchTargetNamespaceImport";

		// Token: 0x04000780 RID: 1920
		internal const string Sch_MismatchTargetNamespaceEx = "Sch_MismatchTargetNamespaceEx";

		// Token: 0x04000781 RID: 1921
		internal const string Sch_XsiTypeNotFound = "Sch_XsiTypeNotFound";

		// Token: 0x04000782 RID: 1922
		internal const string Sch_XsiTypeAbstract = "Sch_XsiTypeAbstract";

		// Token: 0x04000783 RID: 1923
		internal const string Sch_ListFromNonatomic = "Sch_ListFromNonatomic";

		// Token: 0x04000784 RID: 1924
		internal const string Sch_UnionFromUnion = "Sch_UnionFromUnion";

		// Token: 0x04000785 RID: 1925
		internal const string Sch_DupLengthFacet = "Sch_DupLengthFacet";

		// Token: 0x04000786 RID: 1926
		internal const string Sch_DupMinLengthFacet = "Sch_DupMinLengthFacet";

		// Token: 0x04000787 RID: 1927
		internal const string Sch_DupMaxLengthFacet = "Sch_DupMaxLengthFacet";

		// Token: 0x04000788 RID: 1928
		internal const string Sch_DupWhiteSpaceFacet = "Sch_DupWhiteSpaceFacet";

		// Token: 0x04000789 RID: 1929
		internal const string Sch_DupMaxInclusiveFacet = "Sch_DupMaxInclusiveFacet";

		// Token: 0x0400078A RID: 1930
		internal const string Sch_DupMaxExclusiveFacet = "Sch_DupMaxExclusiveFacet";

		// Token: 0x0400078B RID: 1931
		internal const string Sch_DupMinInclusiveFacet = "Sch_DupMinInclusiveFacet";

		// Token: 0x0400078C RID: 1932
		internal const string Sch_DupMinExclusiveFacet = "Sch_DupMinExclusiveFacet";

		// Token: 0x0400078D RID: 1933
		internal const string Sch_DupTotalDigitsFacet = "Sch_DupTotalDigitsFacet";

		// Token: 0x0400078E RID: 1934
		internal const string Sch_DupFractionDigitsFacet = "Sch_DupFractionDigitsFacet";

		// Token: 0x0400078F RID: 1935
		internal const string Sch_LengthFacetProhibited = "Sch_LengthFacetProhibited";

		// Token: 0x04000790 RID: 1936
		internal const string Sch_MinLengthFacetProhibited = "Sch_MinLengthFacetProhibited";

		// Token: 0x04000791 RID: 1937
		internal const string Sch_MaxLengthFacetProhibited = "Sch_MaxLengthFacetProhibited";

		// Token: 0x04000792 RID: 1938
		internal const string Sch_PatternFacetProhibited = "Sch_PatternFacetProhibited";

		// Token: 0x04000793 RID: 1939
		internal const string Sch_EnumerationFacetProhibited = "Sch_EnumerationFacetProhibited";

		// Token: 0x04000794 RID: 1940
		internal const string Sch_WhiteSpaceFacetProhibited = "Sch_WhiteSpaceFacetProhibited";

		// Token: 0x04000795 RID: 1941
		internal const string Sch_MaxInclusiveFacetProhibited = "Sch_MaxInclusiveFacetProhibited";

		// Token: 0x04000796 RID: 1942
		internal const string Sch_MaxExclusiveFacetProhibited = "Sch_MaxExclusiveFacetProhibited";

		// Token: 0x04000797 RID: 1943
		internal const string Sch_MinInclusiveFacetProhibited = "Sch_MinInclusiveFacetProhibited";

		// Token: 0x04000798 RID: 1944
		internal const string Sch_MinExclusiveFacetProhibited = "Sch_MinExclusiveFacetProhibited";

		// Token: 0x04000799 RID: 1945
		internal const string Sch_TotalDigitsFacetProhibited = "Sch_TotalDigitsFacetProhibited";

		// Token: 0x0400079A RID: 1946
		internal const string Sch_FractionDigitsFacetProhibited = "Sch_FractionDigitsFacetProhibited";

		// Token: 0x0400079B RID: 1947
		internal const string Sch_LengthFacetInvalid = "Sch_LengthFacetInvalid";

		// Token: 0x0400079C RID: 1948
		internal const string Sch_MinLengthFacetInvalid = "Sch_MinLengthFacetInvalid";

		// Token: 0x0400079D RID: 1949
		internal const string Sch_MaxLengthFacetInvalid = "Sch_MaxLengthFacetInvalid";

		// Token: 0x0400079E RID: 1950
		internal const string Sch_MaxInclusiveFacetInvalid = "Sch_MaxInclusiveFacetInvalid";

		// Token: 0x0400079F RID: 1951
		internal const string Sch_MaxExclusiveFacetInvalid = "Sch_MaxExclusiveFacetInvalid";

		// Token: 0x040007A0 RID: 1952
		internal const string Sch_MinInclusiveFacetInvalid = "Sch_MinInclusiveFacetInvalid";

		// Token: 0x040007A1 RID: 1953
		internal const string Sch_MinExclusiveFacetInvalid = "Sch_MinExclusiveFacetInvalid";

		// Token: 0x040007A2 RID: 1954
		internal const string Sch_TotalDigitsFacetInvalid = "Sch_TotalDigitsFacetInvalid";

		// Token: 0x040007A3 RID: 1955
		internal const string Sch_FractionDigitsFacetInvalid = "Sch_FractionDigitsFacetInvalid";

		// Token: 0x040007A4 RID: 1956
		internal const string Sch_PatternFacetInvalid = "Sch_PatternFacetInvalid";

		// Token: 0x040007A5 RID: 1957
		internal const string Sch_EnumerationFacetInvalid = "Sch_EnumerationFacetInvalid";

		// Token: 0x040007A6 RID: 1958
		internal const string Sch_InvalidWhiteSpace = "Sch_InvalidWhiteSpace";

		// Token: 0x040007A7 RID: 1959
		internal const string Sch_UnknownFacet = "Sch_UnknownFacet";

		// Token: 0x040007A8 RID: 1960
		internal const string Sch_LengthAndMinMax = "Sch_LengthAndMinMax";

		// Token: 0x040007A9 RID: 1961
		internal const string Sch_MinLengthGtMaxLength = "Sch_MinLengthGtMaxLength";

		// Token: 0x040007AA RID: 1962
		internal const string Sch_FractionDigitsGtTotalDigits = "Sch_FractionDigitsGtTotalDigits";

		// Token: 0x040007AB RID: 1963
		internal const string Sch_LengthConstraintFailed = "Sch_LengthConstraintFailed";

		// Token: 0x040007AC RID: 1964
		internal const string Sch_MinLengthConstraintFailed = "Sch_MinLengthConstraintFailed";

		// Token: 0x040007AD RID: 1965
		internal const string Sch_MaxLengthConstraintFailed = "Sch_MaxLengthConstraintFailed";

		// Token: 0x040007AE RID: 1966
		internal const string Sch_PatternConstraintFailed = "Sch_PatternConstraintFailed";

		// Token: 0x040007AF RID: 1967
		internal const string Sch_EnumerationConstraintFailed = "Sch_EnumerationConstraintFailed";

		// Token: 0x040007B0 RID: 1968
		internal const string Sch_MaxInclusiveConstraintFailed = "Sch_MaxInclusiveConstraintFailed";

		// Token: 0x040007B1 RID: 1969
		internal const string Sch_MaxExclusiveConstraintFailed = "Sch_MaxExclusiveConstraintFailed";

		// Token: 0x040007B2 RID: 1970
		internal const string Sch_MinInclusiveConstraintFailed = "Sch_MinInclusiveConstraintFailed";

		// Token: 0x040007B3 RID: 1971
		internal const string Sch_MinExclusiveConstraintFailed = "Sch_MinExclusiveConstraintFailed";

		// Token: 0x040007B4 RID: 1972
		internal const string Sch_TotalDigitsConstraintFailed = "Sch_TotalDigitsConstraintFailed";

		// Token: 0x040007B5 RID: 1973
		internal const string Sch_FractionDigitsConstraintFailed = "Sch_FractionDigitsConstraintFailed";

		// Token: 0x040007B6 RID: 1974
		internal const string Sch_UnionFailedEx = "Sch_UnionFailedEx";

		// Token: 0x040007B7 RID: 1975
		internal const string Sch_NotationRequired = "Sch_NotationRequired";

		// Token: 0x040007B8 RID: 1976
		internal const string Sch_DupNotationAttribute = "Sch_DupNotationAttribute";

		// Token: 0x040007B9 RID: 1977
		internal const string Sch_MissingPublicSystemAttribute = "Sch_MissingPublicSystemAttribute";

		// Token: 0x040007BA RID: 1978
		internal const string Sch_NotationAttributeOnEmptyElement = "Sch_NotationAttributeOnEmptyElement";

		// Token: 0x040007BB RID: 1979
		internal const string Sch_RefNotInScope = "Sch_RefNotInScope";

		// Token: 0x040007BC RID: 1980
		internal const string Sch_UndeclaredIdentityConstraint = "Sch_UndeclaredIdentityConstraint";

		// Token: 0x040007BD RID: 1981
		internal const string Sch_RefInvalidIdentityConstraint = "Sch_RefInvalidIdentityConstraint";

		// Token: 0x040007BE RID: 1982
		internal const string Sch_RefInvalidCardin = "Sch_RefInvalidCardin";

		// Token: 0x040007BF RID: 1983
		internal const string Sch_ReftoKeyref = "Sch_ReftoKeyref";

		// Token: 0x040007C0 RID: 1984
		internal const string Sch_EmptyXPath = "Sch_EmptyXPath";

		// Token: 0x040007C1 RID: 1985
		internal const string Sch_UnresolvedPrefix = "Sch_UnresolvedPrefix";

		// Token: 0x040007C2 RID: 1986
		internal const string Sch_UnresolvedKeyref = "Sch_UnresolvedKeyref";

		// Token: 0x040007C3 RID: 1987
		internal const string Sch_ICXpathError = "Sch_ICXpathError";

		// Token: 0x040007C4 RID: 1988
		internal const string Sch_SelectorAttr = "Sch_SelectorAttr";

		// Token: 0x040007C5 RID: 1989
		internal const string Sch_FieldSimpleTypeExpected = "Sch_FieldSimpleTypeExpected";

		// Token: 0x040007C6 RID: 1990
		internal const string Sch_FieldSingleValueExpected = "Sch_FieldSingleValueExpected";

		// Token: 0x040007C7 RID: 1991
		internal const string Sch_MissingKey = "Sch_MissingKey";

		// Token: 0x040007C8 RID: 1992
		internal const string Sch_DuplicateKey = "Sch_DuplicateKey";

		// Token: 0x040007C9 RID: 1993
		internal const string Sch_TargetNamespaceXsi = "Sch_TargetNamespaceXsi";

		// Token: 0x040007CA RID: 1994
		internal const string Sch_UndeclaredEntity = "Sch_UndeclaredEntity";

		// Token: 0x040007CB RID: 1995
		internal const string Sch_UnparsedEntityRef = "Sch_UnparsedEntityRef";

		// Token: 0x040007CC RID: 1996
		internal const string Sch_MaxOccursInvalidXsd = "Sch_MaxOccursInvalidXsd";

		// Token: 0x040007CD RID: 1997
		internal const string Sch_MinOccursInvalidXsd = "Sch_MinOccursInvalidXsd";

		// Token: 0x040007CE RID: 1998
		internal const string Sch_MaxInclusiveExclusive = "Sch_MaxInclusiveExclusive";

		// Token: 0x040007CF RID: 1999
		internal const string Sch_MinInclusiveExclusive = "Sch_MinInclusiveExclusive";

		// Token: 0x040007D0 RID: 2000
		internal const string Sch_MinInclusiveGtMaxInclusive = "Sch_MinInclusiveGtMaxInclusive";

		// Token: 0x040007D1 RID: 2001
		internal const string Sch_MinExclusiveGtMaxExclusive = "Sch_MinExclusiveGtMaxExclusive";

		// Token: 0x040007D2 RID: 2002
		internal const string Sch_MinInclusiveGtMaxExclusive = "Sch_MinInclusiveGtMaxExclusive";

		// Token: 0x040007D3 RID: 2003
		internal const string Sch_MinExclusiveGtMaxInclusive = "Sch_MinExclusiveGtMaxInclusive";

		// Token: 0x040007D4 RID: 2004
		internal const string Sch_SimpleTypeRestriction = "Sch_SimpleTypeRestriction";

		// Token: 0x040007D5 RID: 2005
		internal const string Sch_InvalidFacetPosition = "Sch_InvalidFacetPosition";

		// Token: 0x040007D6 RID: 2006
		internal const string Sch_AttributeMutuallyExclusive = "Sch_AttributeMutuallyExclusive";

		// Token: 0x040007D7 RID: 2007
		internal const string Sch_AnyAttributeLastChild = "Sch_AnyAttributeLastChild";

		// Token: 0x040007D8 RID: 2008
		internal const string Sch_ComplexTypeContentModel = "Sch_ComplexTypeContentModel";

		// Token: 0x040007D9 RID: 2009
		internal const string Sch_ComplexContentContentModel = "Sch_ComplexContentContentModel";

		// Token: 0x040007DA RID: 2010
		internal const string Sch_NotNormalizedString = "Sch_NotNormalizedString";

		// Token: 0x040007DB RID: 2011
		internal const string Sch_FractionDigitsNotOnDecimal = "Sch_FractionDigitsNotOnDecimal";

		// Token: 0x040007DC RID: 2012
		internal const string Sch_ContentInNill = "Sch_ContentInNill";

		// Token: 0x040007DD RID: 2013
		internal const string Sch_NoElementSchemaFound = "Sch_NoElementSchemaFound";

		// Token: 0x040007DE RID: 2014
		internal const string Sch_NoAttributeSchemaFound = "Sch_NoAttributeSchemaFound";

		// Token: 0x040007DF RID: 2015
		internal const string Sch_InvalidNamespace = "Sch_InvalidNamespace";

		// Token: 0x040007E0 RID: 2016
		internal const string Sch_InvalidTargetNamespaceAttribute = "Sch_InvalidTargetNamespaceAttribute";

		// Token: 0x040007E1 RID: 2017
		internal const string Sch_InvalidNamespaceAttribute = "Sch_InvalidNamespaceAttribute";

		// Token: 0x040007E2 RID: 2018
		internal const string Sch_InvalidSchemaLocation = "Sch_InvalidSchemaLocation";

		// Token: 0x040007E3 RID: 2019
		internal const string Sch_ImportTargetNamespace = "Sch_ImportTargetNamespace";

		// Token: 0x040007E4 RID: 2020
		internal const string Sch_ImportTargetNamespaceNull = "Sch_ImportTargetNamespaceNull";

		// Token: 0x040007E5 RID: 2021
		internal const string Sch_GroupDoubleRedefine = "Sch_GroupDoubleRedefine";

		// Token: 0x040007E6 RID: 2022
		internal const string Sch_ComponentRedefineNotFound = "Sch_ComponentRedefineNotFound";

		// Token: 0x040007E7 RID: 2023
		internal const string Sch_GroupRedefineNotFound = "Sch_GroupRedefineNotFound";

		// Token: 0x040007E8 RID: 2024
		internal const string Sch_AttrGroupDoubleRedefine = "Sch_AttrGroupDoubleRedefine";

		// Token: 0x040007E9 RID: 2025
		internal const string Sch_AttrGroupRedefineNotFound = "Sch_AttrGroupRedefineNotFound";

		// Token: 0x040007EA RID: 2026
		internal const string Sch_ComplexTypeDoubleRedefine = "Sch_ComplexTypeDoubleRedefine";

		// Token: 0x040007EB RID: 2027
		internal const string Sch_ComplexTypeRedefineNotFound = "Sch_ComplexTypeRedefineNotFound";

		// Token: 0x040007EC RID: 2028
		internal const string Sch_SimpleToComplexTypeRedefine = "Sch_SimpleToComplexTypeRedefine";

		// Token: 0x040007ED RID: 2029
		internal const string Sch_SimpleTypeDoubleRedefine = "Sch_SimpleTypeDoubleRedefine";

		// Token: 0x040007EE RID: 2030
		internal const string Sch_ComplexToSimpleTypeRedefine = "Sch_ComplexToSimpleTypeRedefine";

		// Token: 0x040007EF RID: 2031
		internal const string Sch_SimpleTypeRedefineNotFound = "Sch_SimpleTypeRedefineNotFound";

		// Token: 0x040007F0 RID: 2032
		internal const string Sch_MinMaxGroupRedefine = "Sch_MinMaxGroupRedefine";

		// Token: 0x040007F1 RID: 2033
		internal const string Sch_MultipleGroupSelfRef = "Sch_MultipleGroupSelfRef";

		// Token: 0x040007F2 RID: 2034
		internal const string Sch_MultipleAttrGroupSelfRef = "Sch_MultipleAttrGroupSelfRef";

		// Token: 0x040007F3 RID: 2035
		internal const string Sch_InvalidTypeRedefine = "Sch_InvalidTypeRedefine";

		// Token: 0x040007F4 RID: 2036
		internal const string Sch_InvalidElementRef = "Sch_InvalidElementRef";

		// Token: 0x040007F5 RID: 2037
		internal const string Sch_MinGtMax = "Sch_MinGtMax";

		// Token: 0x040007F6 RID: 2038
		internal const string Sch_DupSelector = "Sch_DupSelector";

		// Token: 0x040007F7 RID: 2039
		internal const string Sch_IdConstraintNoSelector = "Sch_IdConstraintNoSelector";

		// Token: 0x040007F8 RID: 2040
		internal const string Sch_IdConstraintNoFields = "Sch_IdConstraintNoFields";

		// Token: 0x040007F9 RID: 2041
		internal const string Sch_IdConstraintNoRefer = "Sch_IdConstraintNoRefer";

		// Token: 0x040007FA RID: 2042
		internal const string Sch_SelectorBeforeFields = "Sch_SelectorBeforeFields";

		// Token: 0x040007FB RID: 2043
		internal const string Sch_NoSimpleTypeContent = "Sch_NoSimpleTypeContent";

		// Token: 0x040007FC RID: 2044
		internal const string Sch_SimpleTypeRestRefBase = "Sch_SimpleTypeRestRefBase";

		// Token: 0x040007FD RID: 2045
		internal const string Sch_SimpleTypeRestRefBaseNone = "Sch_SimpleTypeRestRefBaseNone";

		// Token: 0x040007FE RID: 2046
		internal const string Sch_SimpleTypeListRefBase = "Sch_SimpleTypeListRefBase";

		// Token: 0x040007FF RID: 2047
		internal const string Sch_SimpleTypeListRefBaseNone = "Sch_SimpleTypeListRefBaseNone";

		// Token: 0x04000800 RID: 2048
		internal const string Sch_SimpleTypeUnionNoBase = "Sch_SimpleTypeUnionNoBase";

		// Token: 0x04000801 RID: 2049
		internal const string Sch_NoRestOrExtQName = "Sch_NoRestOrExtQName";

		// Token: 0x04000802 RID: 2050
		internal const string Sch_NoRestOrExt = "Sch_NoRestOrExt";

		// Token: 0x04000803 RID: 2051
		internal const string Sch_NoGroupParticle = "Sch_NoGroupParticle";

		// Token: 0x04000804 RID: 2052
		internal const string Sch_InvalidAllMin = "Sch_InvalidAllMin";

		// Token: 0x04000805 RID: 2053
		internal const string Sch_InvalidAllMax = "Sch_InvalidAllMax";

		// Token: 0x04000806 RID: 2054
		internal const string Sch_InvalidFacet = "Sch_InvalidFacet";

		// Token: 0x04000807 RID: 2055
		internal const string Sch_AbstractElement = "Sch_AbstractElement";

		// Token: 0x04000808 RID: 2056
		internal const string Sch_XsiTypeBlockedEx = "Sch_XsiTypeBlockedEx";

		// Token: 0x04000809 RID: 2057
		internal const string Sch_InvalidXsiNill = "Sch_InvalidXsiNill";

		// Token: 0x0400080A RID: 2058
		internal const string Sch_SubstitutionNotAllowed = "Sch_SubstitutionNotAllowed";

		// Token: 0x0400080B RID: 2059
		internal const string Sch_SubstitutionBlocked = "Sch_SubstitutionBlocked";

		// Token: 0x0400080C RID: 2060
		internal const string Sch_InvalidElementInEmptyEx = "Sch_InvalidElementInEmptyEx";

		// Token: 0x0400080D RID: 2061
		internal const string Sch_InvalidElementInTextOnlyEx = "Sch_InvalidElementInTextOnlyEx";

		// Token: 0x0400080E RID: 2062
		internal const string Sch_InvalidTextInElement = "Sch_InvalidTextInElement";

		// Token: 0x0400080F RID: 2063
		internal const string Sch_InvalidElementContent = "Sch_InvalidElementContent";

		// Token: 0x04000810 RID: 2064
		internal const string Sch_InvalidElementContentComplex = "Sch_InvalidElementContentComplex";

		// Token: 0x04000811 RID: 2065
		internal const string Sch_IncompleteContent = "Sch_IncompleteContent";

		// Token: 0x04000812 RID: 2066
		internal const string Sch_IncompleteContentComplex = "Sch_IncompleteContentComplex";

		// Token: 0x04000813 RID: 2067
		internal const string Sch_InvalidTextInElementExpecting = "Sch_InvalidTextInElementExpecting";

		// Token: 0x04000814 RID: 2068
		internal const string Sch_InvalidElementContentExpecting = "Sch_InvalidElementContentExpecting";

		// Token: 0x04000815 RID: 2069
		internal const string Sch_InvalidElementContentExpectingComplex = "Sch_InvalidElementContentExpectingComplex";

		// Token: 0x04000816 RID: 2070
		internal const string Sch_IncompleteContentExpecting = "Sch_IncompleteContentExpecting";

		// Token: 0x04000817 RID: 2071
		internal const string Sch_IncompleteContentExpectingComplex = "Sch_IncompleteContentExpectingComplex";

		// Token: 0x04000818 RID: 2072
		internal const string Sch_InvalidElementSubstitution = "Sch_InvalidElementSubstitution";

		// Token: 0x04000819 RID: 2073
		internal const string Sch_ElementNameAndNamespace = "Sch_ElementNameAndNamespace";

		// Token: 0x0400081A RID: 2074
		internal const string Sch_ElementName = "Sch_ElementName";

		// Token: 0x0400081B RID: 2075
		internal const string Sch_ContinuationString = "Sch_ContinuationString";

		// Token: 0x0400081C RID: 2076
		internal const string Sch_AnyElementNS = "Sch_AnyElementNS";

		// Token: 0x0400081D RID: 2077
		internal const string Sch_AnyElement = "Sch_AnyElement";

		// Token: 0x0400081E RID: 2078
		internal const string Sch_InvalidTextInEmpty = "Sch_InvalidTextInEmpty";

		// Token: 0x0400081F RID: 2079
		internal const string Sch_InvalidWhitespaceInEmpty = "Sch_InvalidWhitespaceInEmpty";

		// Token: 0x04000820 RID: 2080
		internal const string Sch_InvalidPIComment = "Sch_InvalidPIComment";

		// Token: 0x04000821 RID: 2081
		internal const string Sch_InvalidAttributeRef = "Sch_InvalidAttributeRef";

		// Token: 0x04000822 RID: 2082
		internal const string Sch_OptionalDefaultAttribute = "Sch_OptionalDefaultAttribute";

		// Token: 0x04000823 RID: 2083
		internal const string Sch_AttributeCircularRef = "Sch_AttributeCircularRef";

		// Token: 0x04000824 RID: 2084
		internal const string Sch_IdentityConstraintCircularRef = "Sch_IdentityConstraintCircularRef";

		// Token: 0x04000825 RID: 2085
		internal const string Sch_SubstitutionCircularRef = "Sch_SubstitutionCircularRef";

		// Token: 0x04000826 RID: 2086
		internal const string Sch_InvalidAnyAttribute = "Sch_InvalidAnyAttribute";

		// Token: 0x04000827 RID: 2087
		internal const string Sch_DupIdAttribute = "Sch_DupIdAttribute";

		// Token: 0x04000828 RID: 2088
		internal const string Sch_InvalidAllElementMax = "Sch_InvalidAllElementMax";

		// Token: 0x04000829 RID: 2089
		internal const string Sch_InvalidAny = "Sch_InvalidAny";

		// Token: 0x0400082A RID: 2090
		internal const string Sch_InvalidAnyDetailed = "Sch_InvalidAnyDetailed";

		// Token: 0x0400082B RID: 2091
		internal const string Sch_InvalidExamplar = "Sch_InvalidExamplar";

		// Token: 0x0400082C RID: 2092
		internal const string Sch_NoExamplar = "Sch_NoExamplar";

		// Token: 0x0400082D RID: 2093
		internal const string Sch_InvalidSubstitutionMember = "Sch_InvalidSubstitutionMember";

		// Token: 0x0400082E RID: 2094
		internal const string Sch_RedefineNoSchema = "Sch_RedefineNoSchema";

		// Token: 0x0400082F RID: 2095
		internal const string Sch_ProhibitedAttribute = "Sch_ProhibitedAttribute";

		// Token: 0x04000830 RID: 2096
		internal const string Sch_TypeCircularRef = "Sch_TypeCircularRef";

		// Token: 0x04000831 RID: 2097
		internal const string Sch_TwoIdAttrUses = "Sch_TwoIdAttrUses";

		// Token: 0x04000832 RID: 2098
		internal const string Sch_AttrUseAndWildId = "Sch_AttrUseAndWildId";

		// Token: 0x04000833 RID: 2099
		internal const string Sch_MoreThanOneWildId = "Sch_MoreThanOneWildId";

		// Token: 0x04000834 RID: 2100
		internal const string Sch_BaseFinalExtension = "Sch_BaseFinalExtension";

		// Token: 0x04000835 RID: 2101
		internal const string Sch_NotSimpleContent = "Sch_NotSimpleContent";

		// Token: 0x04000836 RID: 2102
		internal const string Sch_NotComplexContent = "Sch_NotComplexContent";

		// Token: 0x04000837 RID: 2103
		internal const string Sch_BaseFinalRestriction = "Sch_BaseFinalRestriction";

		// Token: 0x04000838 RID: 2104
		internal const string Sch_BaseFinalList = "Sch_BaseFinalList";

		// Token: 0x04000839 RID: 2105
		internal const string Sch_BaseFinalUnion = "Sch_BaseFinalUnion";

		// Token: 0x0400083A RID: 2106
		internal const string Sch_UndefBaseRestriction = "Sch_UndefBaseRestriction";

		// Token: 0x0400083B RID: 2107
		internal const string Sch_UndefBaseExtension = "Sch_UndefBaseExtension";

		// Token: 0x0400083C RID: 2108
		internal const string Sch_DifContentType = "Sch_DifContentType";

		// Token: 0x0400083D RID: 2109
		internal const string Sch_InvalidContentRestriction = "Sch_InvalidContentRestriction";

		// Token: 0x0400083E RID: 2110
		internal const string Sch_InvalidContentRestrictionDetailed = "Sch_InvalidContentRestrictionDetailed";

		// Token: 0x0400083F RID: 2111
		internal const string Sch_InvalidBaseToEmpty = "Sch_InvalidBaseToEmpty";

		// Token: 0x04000840 RID: 2112
		internal const string Sch_InvalidBaseToMixed = "Sch_InvalidBaseToMixed";

		// Token: 0x04000841 RID: 2113
		internal const string Sch_DupAttributeUse = "Sch_DupAttributeUse";

		// Token: 0x04000842 RID: 2114
		internal const string Sch_InvalidParticleRestriction = "Sch_InvalidParticleRestriction";

		// Token: 0x04000843 RID: 2115
		internal const string Sch_InvalidParticleRestrictionDetailed = "Sch_InvalidParticleRestrictionDetailed";

		// Token: 0x04000844 RID: 2116
		internal const string Sch_ForbiddenDerivedParticleForAll = "Sch_ForbiddenDerivedParticleForAll";

		// Token: 0x04000845 RID: 2117
		internal const string Sch_ForbiddenDerivedParticleForElem = "Sch_ForbiddenDerivedParticleForElem";

		// Token: 0x04000846 RID: 2118
		internal const string Sch_ForbiddenDerivedParticleForChoice = "Sch_ForbiddenDerivedParticleForChoice";

		// Token: 0x04000847 RID: 2119
		internal const string Sch_ForbiddenDerivedParticleForSeq = "Sch_ForbiddenDerivedParticleForSeq";

		// Token: 0x04000848 RID: 2120
		internal const string Sch_ElementFromElement = "Sch_ElementFromElement";

		// Token: 0x04000849 RID: 2121
		internal const string Sch_ElementFromAnyRule1 = "Sch_ElementFromAnyRule1";

		// Token: 0x0400084A RID: 2122
		internal const string Sch_ElementFromAnyRule2 = "Sch_ElementFromAnyRule2";

		// Token: 0x0400084B RID: 2123
		internal const string Sch_AnyFromAnyRule1 = "Sch_AnyFromAnyRule1";

		// Token: 0x0400084C RID: 2124
		internal const string Sch_AnyFromAnyRule2 = "Sch_AnyFromAnyRule2";

		// Token: 0x0400084D RID: 2125
		internal const string Sch_AnyFromAnyRule3 = "Sch_AnyFromAnyRule3";

		// Token: 0x0400084E RID: 2126
		internal const string Sch_GroupBaseFromAny1 = "Sch_GroupBaseFromAny1";

		// Token: 0x0400084F RID: 2127
		internal const string Sch_GroupBaseFromAny2 = "Sch_GroupBaseFromAny2";

		// Token: 0x04000850 RID: 2128
		internal const string Sch_ElementFromGroupBase1 = "Sch_ElementFromGroupBase1";

		// Token: 0x04000851 RID: 2129
		internal const string Sch_ElementFromGroupBase2 = "Sch_ElementFromGroupBase2";

		// Token: 0x04000852 RID: 2130
		internal const string Sch_ElementFromGroupBase3 = "Sch_ElementFromGroupBase3";

		// Token: 0x04000853 RID: 2131
		internal const string Sch_GroupBaseRestRangeInvalid = "Sch_GroupBaseRestRangeInvalid";

		// Token: 0x04000854 RID: 2132
		internal const string Sch_GroupBaseRestNoMap = "Sch_GroupBaseRestNoMap";

		// Token: 0x04000855 RID: 2133
		internal const string Sch_GroupBaseRestNotEmptiable = "Sch_GroupBaseRestNotEmptiable";

		// Token: 0x04000856 RID: 2134
		internal const string Sch_SeqFromAll = "Sch_SeqFromAll";

		// Token: 0x04000857 RID: 2135
		internal const string Sch_SeqFromChoice = "Sch_SeqFromChoice";

		// Token: 0x04000858 RID: 2136
		internal const string Sch_UndefGroupRef = "Sch_UndefGroupRef";

		// Token: 0x04000859 RID: 2137
		internal const string Sch_GroupCircularRef = "Sch_GroupCircularRef";

		// Token: 0x0400085A RID: 2138
		internal const string Sch_AllRefNotRoot = "Sch_AllRefNotRoot";

		// Token: 0x0400085B RID: 2139
		internal const string Sch_AllRefMinMax = "Sch_AllRefMinMax";

		// Token: 0x0400085C RID: 2140
		internal const string Sch_NotAllAlone = "Sch_NotAllAlone";

		// Token: 0x0400085D RID: 2141
		internal const string Sch_AttributeGroupCircularRef = "Sch_AttributeGroupCircularRef";

		// Token: 0x0400085E RID: 2142
		internal const string Sch_UndefAttributeGroupRef = "Sch_UndefAttributeGroupRef";

		// Token: 0x0400085F RID: 2143
		internal const string Sch_InvalidAttributeExtension = "Sch_InvalidAttributeExtension";

		// Token: 0x04000860 RID: 2144
		internal const string Sch_InvalidAnyAttributeRestriction = "Sch_InvalidAnyAttributeRestriction";

		// Token: 0x04000861 RID: 2145
		internal const string Sch_AttributeRestrictionProhibited = "Sch_AttributeRestrictionProhibited";

		// Token: 0x04000862 RID: 2146
		internal const string Sch_AttributeRestrictionInvalid = "Sch_AttributeRestrictionInvalid";

		// Token: 0x04000863 RID: 2147
		internal const string Sch_AttributeFixedInvalid = "Sch_AttributeFixedInvalid";

		// Token: 0x04000864 RID: 2148
		internal const string Sch_AttributeUseInvalid = "Sch_AttributeUseInvalid";

		// Token: 0x04000865 RID: 2149
		internal const string Sch_AttributeRestrictionInvalidFromWildcard = "Sch_AttributeRestrictionInvalidFromWildcard";

		// Token: 0x04000866 RID: 2150
		internal const string Sch_NoDerivedAttribute = "Sch_NoDerivedAttribute";

		// Token: 0x04000867 RID: 2151
		internal const string Sch_UnexpressibleAnyAttribute = "Sch_UnexpressibleAnyAttribute";

		// Token: 0x04000868 RID: 2152
		internal const string Sch_RefInvalidAttribute = "Sch_RefInvalidAttribute";

		// Token: 0x04000869 RID: 2153
		internal const string Sch_ElementCircularRef = "Sch_ElementCircularRef";

		// Token: 0x0400086A RID: 2154
		internal const string Sch_RefInvalidElement = "Sch_RefInvalidElement";

		// Token: 0x0400086B RID: 2155
		internal const string Sch_ElementCannotHaveValue = "Sch_ElementCannotHaveValue";

		// Token: 0x0400086C RID: 2156
		internal const string Sch_ElementInMixedWithFixed = "Sch_ElementInMixedWithFixed";

		// Token: 0x0400086D RID: 2157
		internal const string Sch_ElementTypeCollision = "Sch_ElementTypeCollision";

		// Token: 0x0400086E RID: 2158
		internal const string Sch_InvalidIncludeLocation = "Sch_InvalidIncludeLocation";

		// Token: 0x0400086F RID: 2159
		internal const string Sch_CannotLoadSchema = "Sch_CannotLoadSchema";

		// Token: 0x04000870 RID: 2160
		internal const string Sch_CannotLoadSchemaLocation = "Sch_CannotLoadSchemaLocation";

		// Token: 0x04000871 RID: 2161
		internal const string Sch_LengthGtBaseLength = "Sch_LengthGtBaseLength";

		// Token: 0x04000872 RID: 2162
		internal const string Sch_MinLengthGtBaseMinLength = "Sch_MinLengthGtBaseMinLength";

		// Token: 0x04000873 RID: 2163
		internal const string Sch_MaxLengthGtBaseMaxLength = "Sch_MaxLengthGtBaseMaxLength";

		// Token: 0x04000874 RID: 2164
		internal const string Sch_MaxMinLengthBaseLength = "Sch_MaxMinLengthBaseLength";

		// Token: 0x04000875 RID: 2165
		internal const string Sch_MaxInclusiveMismatch = "Sch_MaxInclusiveMismatch";

		// Token: 0x04000876 RID: 2166
		internal const string Sch_MaxExclusiveMismatch = "Sch_MaxExclusiveMismatch";

		// Token: 0x04000877 RID: 2167
		internal const string Sch_MinInclusiveMismatch = "Sch_MinInclusiveMismatch";

		// Token: 0x04000878 RID: 2168
		internal const string Sch_MinExclusiveMismatch = "Sch_MinExclusiveMismatch";

		// Token: 0x04000879 RID: 2169
		internal const string Sch_MinExlIncMismatch = "Sch_MinExlIncMismatch";

		// Token: 0x0400087A RID: 2170
		internal const string Sch_MinExlMaxExlMismatch = "Sch_MinExlMaxExlMismatch";

		// Token: 0x0400087B RID: 2171
		internal const string Sch_MinIncMaxExlMismatch = "Sch_MinIncMaxExlMismatch";

		// Token: 0x0400087C RID: 2172
		internal const string Sch_MinIncExlMismatch = "Sch_MinIncExlMismatch";

		// Token: 0x0400087D RID: 2173
		internal const string Sch_MaxIncExlMismatch = "Sch_MaxIncExlMismatch";

		// Token: 0x0400087E RID: 2174
		internal const string Sch_MaxExlIncMismatch = "Sch_MaxExlIncMismatch";

		// Token: 0x0400087F RID: 2175
		internal const string Sch_TotalDigitsMismatch = "Sch_TotalDigitsMismatch";

		// Token: 0x04000880 RID: 2176
		internal const string Sch_FacetBaseFixed = "Sch_FacetBaseFixed";

		// Token: 0x04000881 RID: 2177
		internal const string Sch_WhiteSpaceRestriction1 = "Sch_WhiteSpaceRestriction1";

		// Token: 0x04000882 RID: 2178
		internal const string Sch_WhiteSpaceRestriction2 = "Sch_WhiteSpaceRestriction2";

		// Token: 0x04000883 RID: 2179
		internal const string Sch_XsiNilAndFixed = "Sch_XsiNilAndFixed";

		// Token: 0x04000884 RID: 2180
		internal const string Sch_MixSchemaTypes = "Sch_MixSchemaTypes";

		// Token: 0x04000885 RID: 2181
		internal const string Sch_XSDSchemaOnly = "Sch_XSDSchemaOnly";

		// Token: 0x04000886 RID: 2182
		internal const string Sch_InvalidPublicAttribute = "Sch_InvalidPublicAttribute";

		// Token: 0x04000887 RID: 2183
		internal const string Sch_InvalidSystemAttribute = "Sch_InvalidSystemAttribute";

		// Token: 0x04000888 RID: 2184
		internal const string Sch_TypeAfterConstraints = "Sch_TypeAfterConstraints";

		// Token: 0x04000889 RID: 2185
		internal const string Sch_XsiNilAndType = "Sch_XsiNilAndType";

		// Token: 0x0400088A RID: 2186
		internal const string Sch_DupSimpleTypeChild = "Sch_DupSimpleTypeChild";

		// Token: 0x0400088B RID: 2187
		internal const string Sch_InvalidIdAttribute = "Sch_InvalidIdAttribute";

		// Token: 0x0400088C RID: 2188
		internal const string Sch_InvalidNameAttributeEx = "Sch_InvalidNameAttributeEx";

		// Token: 0x0400088D RID: 2189
		internal const string Sch_InvalidAttribute = "Sch_InvalidAttribute";

		// Token: 0x0400088E RID: 2190
		internal const string Sch_EmptyChoice = "Sch_EmptyChoice";

		// Token: 0x0400088F RID: 2191
		internal const string Sch_DerivedNotFromBase = "Sch_DerivedNotFromBase";

		// Token: 0x04000890 RID: 2192
		internal const string Sch_NeedSimpleTypeChild = "Sch_NeedSimpleTypeChild";

		// Token: 0x04000891 RID: 2193
		internal const string Sch_InvalidCollection = "Sch_InvalidCollection";

		// Token: 0x04000892 RID: 2194
		internal const string Sch_UnrefNS = "Sch_UnrefNS";

		// Token: 0x04000893 RID: 2195
		internal const string Sch_InvalidSimpleTypeRestriction = "Sch_InvalidSimpleTypeRestriction";

		// Token: 0x04000894 RID: 2196
		internal const string Sch_MultipleRedefine = "Sch_MultipleRedefine";

		// Token: 0x04000895 RID: 2197
		internal const string Sch_NullValue = "Sch_NullValue";

		// Token: 0x04000896 RID: 2198
		internal const string Sch_ComplexContentModel = "Sch_ComplexContentModel";

		// Token: 0x04000897 RID: 2199
		internal const string Sch_SchemaNotPreprocessed = "Sch_SchemaNotPreprocessed";

		// Token: 0x04000898 RID: 2200
		internal const string Sch_SchemaNotRemoved = "Sch_SchemaNotRemoved";

		// Token: 0x04000899 RID: 2201
		internal const string Sch_ComponentAlreadySeenForNS = "Sch_ComponentAlreadySeenForNS";

		// Token: 0x0400089A RID: 2202
		internal const string Sch_DefaultAttributeNotApplied = "Sch_DefaultAttributeNotApplied";

		// Token: 0x0400089B RID: 2203
		internal const string Sch_NotXsiAttribute = "Sch_NotXsiAttribute";

		// Token: 0x0400089C RID: 2204
		internal const string Sch_SchemaDoesNotExist = "Sch_SchemaDoesNotExist";

		// Token: 0x0400089D RID: 2205
		internal const string XmlDocument_ValidateInvalidNodeType = "XmlDocument_ValidateInvalidNodeType";

		// Token: 0x0400089E RID: 2206
		internal const string XmlDocument_NodeNotFromDocument = "XmlDocument_NodeNotFromDocument";

		// Token: 0x0400089F RID: 2207
		internal const string XmlDocument_NoNodeSchemaInfo = "XmlDocument_NoNodeSchemaInfo";

		// Token: 0x040008A0 RID: 2208
		internal const string XmlDocument_NoSchemaInfo = "XmlDocument_NoSchemaInfo";

		// Token: 0x040008A1 RID: 2209
		internal const string Sch_InvalidStartTransition = "Sch_InvalidStartTransition";

		// Token: 0x040008A2 RID: 2210
		internal const string Sch_InvalidStateTransition = "Sch_InvalidStateTransition";

		// Token: 0x040008A3 RID: 2211
		internal const string Sch_InvalidEndValidation = "Sch_InvalidEndValidation";

		// Token: 0x040008A4 RID: 2212
		internal const string Sch_InvalidEndElementCall = "Sch_InvalidEndElementCall";

		// Token: 0x040008A5 RID: 2213
		internal const string Sch_InvalidEndElementCallTyped = "Sch_InvalidEndElementCallTyped";

		// Token: 0x040008A6 RID: 2214
		internal const string Sch_InvalidEndElementMultiple = "Sch_InvalidEndElementMultiple";

		// Token: 0x040008A7 RID: 2215
		internal const string Sch_DuplicateAttribute = "Sch_DuplicateAttribute";

		// Token: 0x040008A8 RID: 2216
		internal const string Sch_InvalidPartialValidationType = "Sch_InvalidPartialValidationType";

		// Token: 0x040008A9 RID: 2217
		internal const string Sch_SchemaElementNameMismatch = "Sch_SchemaElementNameMismatch";

		// Token: 0x040008AA RID: 2218
		internal const string Sch_SchemaAttributeNameMismatch = "Sch_SchemaAttributeNameMismatch";

		// Token: 0x040008AB RID: 2219
		internal const string Sch_ValidateAttributeInvalidCall = "Sch_ValidateAttributeInvalidCall";

		// Token: 0x040008AC RID: 2220
		internal const string Sch_ValidateElementInvalidCall = "Sch_ValidateElementInvalidCall";

		// Token: 0x040008AD RID: 2221
		internal const string Sch_EnumNotStarted = "Sch_EnumNotStarted";

		// Token: 0x040008AE RID: 2222
		internal const string Sch_EnumFinished = "Sch_EnumFinished";

		// Token: 0x040008AF RID: 2223
		internal const string SchInf_schema = "SchInf_schema";

		// Token: 0x040008B0 RID: 2224
		internal const string SchInf_entity = "SchInf_entity";

		// Token: 0x040008B1 RID: 2225
		internal const string SchInf_simplecontent = "SchInf_simplecontent";

		// Token: 0x040008B2 RID: 2226
		internal const string SchInf_extension = "SchInf_extension";

		// Token: 0x040008B3 RID: 2227
		internal const string SchInf_particle = "SchInf_particle";

		// Token: 0x040008B4 RID: 2228
		internal const string SchInf_ct = "SchInf_ct";

		// Token: 0x040008B5 RID: 2229
		internal const string SchInf_seq = "SchInf_seq";

		// Token: 0x040008B6 RID: 2230
		internal const string SchInf_noseq = "SchInf_noseq";

		// Token: 0x040008B7 RID: 2231
		internal const string SchInf_noct = "SchInf_noct";

		// Token: 0x040008B8 RID: 2232
		internal const string SchInf_UnknownParticle = "SchInf_UnknownParticle";

		// Token: 0x040008B9 RID: 2233
		internal const string SchInf_schematype = "SchInf_schematype";

		// Token: 0x040008BA RID: 2234
		internal const string SchInf_NoElement = "SchInf_NoElement";

		// Token: 0x040008BB RID: 2235
		internal const string Xp_UnclosedString = "Xp_UnclosedString";

		// Token: 0x040008BC RID: 2236
		internal const string Xp_ExprExpected = "Xp_ExprExpected";

		// Token: 0x040008BD RID: 2237
		internal const string Xp_InvalidArgumentType = "Xp_InvalidArgumentType";

		// Token: 0x040008BE RID: 2238
		internal const string Xp_InvalidNumArgs = "Xp_InvalidNumArgs";

		// Token: 0x040008BF RID: 2239
		internal const string Xp_InvalidName = "Xp_InvalidName";

		// Token: 0x040008C0 RID: 2240
		internal const string Xp_InvalidToken = "Xp_InvalidToken";

		// Token: 0x040008C1 RID: 2241
		internal const string Xp_NodeSetExpected = "Xp_NodeSetExpected";

		// Token: 0x040008C2 RID: 2242
		internal const string Xp_NotSupported = "Xp_NotSupported";

		// Token: 0x040008C3 RID: 2243
		internal const string Xp_InvalidPattern = "Xp_InvalidPattern";

		// Token: 0x040008C4 RID: 2244
		internal const string Xp_InvalidKeyPattern = "Xp_InvalidKeyPattern";

		// Token: 0x040008C5 RID: 2245
		internal const string Xp_BadQueryObject = "Xp_BadQueryObject";

		// Token: 0x040008C6 RID: 2246
		internal const string Xp_UndefinedXsltContext = "Xp_UndefinedXsltContext";

		// Token: 0x040008C7 RID: 2247
		internal const string Xp_NoContext = "Xp_NoContext";

		// Token: 0x040008C8 RID: 2248
		internal const string Xp_UndefVar = "Xp_UndefVar";

		// Token: 0x040008C9 RID: 2249
		internal const string Xp_UndefFunc = "Xp_UndefFunc";

		// Token: 0x040008CA RID: 2250
		internal const string Xp_FunctionFailed = "Xp_FunctionFailed";

		// Token: 0x040008CB RID: 2251
		internal const string Xp_CurrentNotAllowed = "Xp_CurrentNotAllowed";

		// Token: 0x040008CC RID: 2252
		internal const string Xp_QueryTooComplex = "Xp_QueryTooComplex";

		// Token: 0x040008CD RID: 2253
		internal const string Xdom_DualDocumentTypeNode = "Xdom_DualDocumentTypeNode";

		// Token: 0x040008CE RID: 2254
		internal const string Xdom_DualDocumentElementNode = "Xdom_DualDocumentElementNode";

		// Token: 0x040008CF RID: 2255
		internal const string Xdom_DualDeclarationNode = "Xdom_DualDeclarationNode";

		// Token: 0x040008D0 RID: 2256
		internal const string Xdom_Import = "Xdom_Import";

		// Token: 0x040008D1 RID: 2257
		internal const string Xdom_Import_NullNode = "Xdom_Import_NullNode";

		// Token: 0x040008D2 RID: 2258
		internal const string Xdom_NoRootEle = "Xdom_NoRootEle";

		// Token: 0x040008D3 RID: 2259
		internal const string Xdom_Attr_Name = "Xdom_Attr_Name";

		// Token: 0x040008D4 RID: 2260
		internal const string Xdom_AttrCol_Object = "Xdom_AttrCol_Object";

		// Token: 0x040008D5 RID: 2261
		internal const string Xdom_AttrCol_Insert = "Xdom_AttrCol_Insert";

		// Token: 0x040008D6 RID: 2262
		internal const string Xdom_NamedNode_Context = "Xdom_NamedNode_Context";

		// Token: 0x040008D7 RID: 2263
		internal const string Xdom_Version = "Xdom_Version";

		// Token: 0x040008D8 RID: 2264
		internal const string Xdom_standalone = "Xdom_standalone";

		// Token: 0x040008D9 RID: 2265
		internal const string Xdom_Ele_Prefix = "Xdom_Ele_Prefix";

		// Token: 0x040008DA RID: 2266
		internal const string Xdom_Ent_Innertext = "Xdom_Ent_Innertext";

		// Token: 0x040008DB RID: 2267
		internal const string Xdom_EntRef_SetVal = "Xdom_EntRef_SetVal";

		// Token: 0x040008DC RID: 2268
		internal const string Xdom_WS_Char = "Xdom_WS_Char";

		// Token: 0x040008DD RID: 2269
		internal const string Xdom_Node_SetVal = "Xdom_Node_SetVal";

		// Token: 0x040008DE RID: 2270
		internal const string Xdom_Empty_LocalName = "Xdom_Empty_LocalName";

		// Token: 0x040008DF RID: 2271
		internal const string Xdom_Set_InnerXml = "Xdom_Set_InnerXml";

		// Token: 0x040008E0 RID: 2272
		internal const string Xdom_Attr_InUse = "Xdom_Attr_InUse";

		// Token: 0x040008E1 RID: 2273
		internal const string Xdom_Enum_ElementList = "Xdom_Enum_ElementList";

		// Token: 0x040008E2 RID: 2274
		internal const string Xdom_Invalid_NT_String = "Xdom_Invalid_NT_String";

		// Token: 0x040008E3 RID: 2275
		internal const string Xdom_InvalidCharacter_EntityReference = "Xdom_InvalidCharacter_EntityReference";

		// Token: 0x040008E4 RID: 2276
		internal const string Xdom_IndexOutOfRange = "Xdom_IndexOutOfRange";

		// Token: 0x040008E5 RID: 2277
		internal const string Xdom_Document_Innertext = "Xdom_Document_Innertext";

		// Token: 0x040008E6 RID: 2278
		internal const string Xpn_BadPosition = "Xpn_BadPosition";

		// Token: 0x040008E7 RID: 2279
		internal const string Xpn_MissingParent = "Xpn_MissingParent";

		// Token: 0x040008E8 RID: 2280
		internal const string Xpn_NoContent = "Xpn_NoContent";

		// Token: 0x040008E9 RID: 2281
		internal const string Xdom_Load_NoDocument = "Xdom_Load_NoDocument";

		// Token: 0x040008EA RID: 2282
		internal const string Xdom_Load_NoReader = "Xdom_Load_NoReader";

		// Token: 0x040008EB RID: 2283
		internal const string Xdom_Node_Null_Doc = "Xdom_Node_Null_Doc";

		// Token: 0x040008EC RID: 2284
		internal const string Xdom_Node_Insert_Child = "Xdom_Node_Insert_Child";

		// Token: 0x040008ED RID: 2285
		internal const string Xdom_Node_Insert_Contain = "Xdom_Node_Insert_Contain";

		// Token: 0x040008EE RID: 2286
		internal const string Xdom_Node_Insert_Path = "Xdom_Node_Insert_Path";

		// Token: 0x040008EF RID: 2287
		internal const string Xdom_Node_Insert_Context = "Xdom_Node_Insert_Context";

		// Token: 0x040008F0 RID: 2288
		internal const string Xdom_Node_Insert_Location = "Xdom_Node_Insert_Location";

		// Token: 0x040008F1 RID: 2289
		internal const string Xdom_Node_Insert_TypeConflict = "Xdom_Node_Insert_TypeConflict";

		// Token: 0x040008F2 RID: 2290
		internal const string Xdom_Node_Remove_Contain = "Xdom_Node_Remove_Contain";

		// Token: 0x040008F3 RID: 2291
		internal const string Xdom_Node_Remove_Child = "Xdom_Node_Remove_Child";

		// Token: 0x040008F4 RID: 2292
		internal const string Xdom_Node_Modify_ReadOnly = "Xdom_Node_Modify_ReadOnly";

		// Token: 0x040008F5 RID: 2293
		internal const string Xdom_TextNode_SplitText = "Xdom_TextNode_SplitText";

		// Token: 0x040008F6 RID: 2294
		internal const string Xdom_Attr_Reserved_XmlNS = "Xdom_Attr_Reserved_XmlNS";

		// Token: 0x040008F7 RID: 2295
		internal const string Xdom_Node_Cloning = "Xdom_Node_Cloning";

		// Token: 0x040008F8 RID: 2296
		internal const string Xnr_ResolveEntity = "Xnr_ResolveEntity";

		// Token: 0x040008F9 RID: 2297
		internal const string XPathDocument_MissingSchemas = "XPathDocument_MissingSchemas";

		// Token: 0x040008FA RID: 2298
		internal const string XPathDocument_NotEnoughSchemaInfo = "XPathDocument_NotEnoughSchemaInfo";

		// Token: 0x040008FB RID: 2299
		internal const string XPathDocument_ValidateInvalidNodeType = "XPathDocument_ValidateInvalidNodeType";

		// Token: 0x040008FC RID: 2300
		internal const string XPathDocument_SchemaSetNotAllowed = "XPathDocument_SchemaSetNotAllowed";

		// Token: 0x040008FD RID: 2301
		internal const string XmlBin_MissingEndCDATA = "XmlBin_MissingEndCDATA";

		// Token: 0x040008FE RID: 2302
		internal const string XmlBin_InvalidQNameID = "XmlBin_InvalidQNameID";

		// Token: 0x040008FF RID: 2303
		internal const string XmlBinary_UnexpectedToken = "XmlBinary_UnexpectedToken";

		// Token: 0x04000900 RID: 2304
		internal const string XmlBinary_InvalidSqlDecimal = "XmlBinary_InvalidSqlDecimal";

		// Token: 0x04000901 RID: 2305
		internal const string XmlBinary_InvalidSignature = "XmlBinary_InvalidSignature";

		// Token: 0x04000902 RID: 2306
		internal const string XmlBinary_InvalidProtocolVersion = "XmlBinary_InvalidProtocolVersion";

		// Token: 0x04000903 RID: 2307
		internal const string XmlBinary_UnsupportedCodePage = "XmlBinary_UnsupportedCodePage";

		// Token: 0x04000904 RID: 2308
		internal const string XmlBinary_InvalidStandalone = "XmlBinary_InvalidStandalone";

		// Token: 0x04000905 RID: 2309
		internal const string XmlBinary_NoParserContext = "XmlBinary_NoParserContext";

		// Token: 0x04000906 RID: 2310
		internal const string XmlBinary_ListsOfValuesNotSupported = "XmlBinary_ListsOfValuesNotSupported";

		// Token: 0x04000907 RID: 2311
		internal const string XmlBinary_CastNotSupported = "XmlBinary_CastNotSupported";

		// Token: 0x04000908 RID: 2312
		internal const string XmlBinary_NoRemapPrefix = "XmlBinary_NoRemapPrefix";

		// Token: 0x04000909 RID: 2313
		internal const string XmlBinary_AttrWithNsNoPrefix = "XmlBinary_AttrWithNsNoPrefix";

		// Token: 0x0400090A RID: 2314
		internal const string XmlBinary_ValueTooBig = "XmlBinary_ValueTooBig";

		// Token: 0x0400090B RID: 2315
		internal const string SqlTypes_ArithOverflow = "SqlTypes_ArithOverflow";

		// Token: 0x0400090C RID: 2316
		internal const string SqlTypes_ArithTruncation = "SqlTypes_ArithTruncation";

		// Token: 0x0400090D RID: 2317
		internal const string SqlTypes_DivideByZero = "SqlTypes_DivideByZero";

		// Token: 0x0400090E RID: 2318
		internal const string XmlMissingType = "XmlMissingType";

		// Token: 0x0400090F RID: 2319
		internal const string XmlUnsupportedType = "XmlUnsupportedType";

		// Token: 0x04000910 RID: 2320
		internal const string XmlSerializerUnsupportedType = "XmlSerializerUnsupportedType";

		// Token: 0x04000911 RID: 2321
		internal const string XmlSerializerUnsupportedMember = "XmlSerializerUnsupportedMember";

		// Token: 0x04000912 RID: 2322
		internal const string XmlUnsupportedTypeKind = "XmlUnsupportedTypeKind";

		// Token: 0x04000913 RID: 2323
		internal const string XmlUnsupportedSoapTypeKind = "XmlUnsupportedSoapTypeKind";

		// Token: 0x04000914 RID: 2324
		internal const string XmlUnsupportedIDictionary = "XmlUnsupportedIDictionary";

		// Token: 0x04000915 RID: 2325
		internal const string XmlUnsupportedIDictionaryDetails = "XmlUnsupportedIDictionaryDetails";

		// Token: 0x04000916 RID: 2326
		internal const string XmlDuplicateTypeName = "XmlDuplicateTypeName";

		// Token: 0x04000917 RID: 2327
		internal const string XmlSerializableNameMissing1 = "XmlSerializableNameMissing1";

		// Token: 0x04000918 RID: 2328
		internal const string XmlConstructorInaccessible = "XmlConstructorInaccessible";

		// Token: 0x04000919 RID: 2329
		internal const string XmlTypeInaccessible = "XmlTypeInaccessible";

		// Token: 0x0400091A RID: 2330
		internal const string XmlTypeStatic = "XmlTypeStatic";

		// Token: 0x0400091B RID: 2331
		internal const string XmlNoDefaultAccessors = "XmlNoDefaultAccessors";

		// Token: 0x0400091C RID: 2332
		internal const string XmlNoAddMethod = "XmlNoAddMethod";

		// Token: 0x0400091D RID: 2333
		internal const string XmlReadOnlyPropertyError = "XmlReadOnlyPropertyError";

		// Token: 0x0400091E RID: 2334
		internal const string XmlAttributeSetAgain = "XmlAttributeSetAgain";

		// Token: 0x0400091F RID: 2335
		internal const string XmlIllegalWildcard = "XmlIllegalWildcard";

		// Token: 0x04000920 RID: 2336
		internal const string XmlIllegalArrayElement = "XmlIllegalArrayElement";

		// Token: 0x04000921 RID: 2337
		internal const string XmlIllegalForm = "XmlIllegalForm";

		// Token: 0x04000922 RID: 2338
		internal const string XmlBareTextMember = "XmlBareTextMember";

		// Token: 0x04000923 RID: 2339
		internal const string XmlBareAttributeMember = "XmlBareAttributeMember";

		// Token: 0x04000924 RID: 2340
		internal const string XmlReflectionError = "XmlReflectionError";

		// Token: 0x04000925 RID: 2341
		internal const string XmlTypeReflectionError = "XmlTypeReflectionError";

		// Token: 0x04000926 RID: 2342
		internal const string XmlPropertyReflectionError = "XmlPropertyReflectionError";

		// Token: 0x04000927 RID: 2343
		internal const string XmlFieldReflectionError = "XmlFieldReflectionError";

		// Token: 0x04000928 RID: 2344
		internal const string XmlInvalidDataTypeUsage = "XmlInvalidDataTypeUsage";

		// Token: 0x04000929 RID: 2345
		internal const string XmlInvalidXsdDataType = "XmlInvalidXsdDataType";

		// Token: 0x0400092A RID: 2346
		internal const string XmlDataTypeMismatch = "XmlDataTypeMismatch";

		// Token: 0x0400092B RID: 2347
		internal const string XmlIllegalTypeContext = "XmlIllegalTypeContext";

		// Token: 0x0400092C RID: 2348
		internal const string XmlUdeclaredXsdType = "XmlUdeclaredXsdType";

		// Token: 0x0400092D RID: 2349
		internal const string XmlAnyElementNamespace = "XmlAnyElementNamespace";

		// Token: 0x0400092E RID: 2350
		internal const string XmlInvalidConstantAttribute = "XmlInvalidConstantAttribute";

		// Token: 0x0400092F RID: 2351
		internal const string XmlIllegalDefault = "XmlIllegalDefault";

		// Token: 0x04000930 RID: 2352
		internal const string XmlIllegalAttributesArrayAttribute = "XmlIllegalAttributesArrayAttribute";

		// Token: 0x04000931 RID: 2353
		internal const string XmlIllegalElementsArrayAttribute = "XmlIllegalElementsArrayAttribute";

		// Token: 0x04000932 RID: 2354
		internal const string XmlIllegalArrayArrayAttribute = "XmlIllegalArrayArrayAttribute";

		// Token: 0x04000933 RID: 2355
		internal const string XmlIllegalAttribute = "XmlIllegalAttribute";

		// Token: 0x04000934 RID: 2356
		internal const string XmlIllegalType = "XmlIllegalType";

		// Token: 0x04000935 RID: 2357
		internal const string XmlIllegalAttrOrText = "XmlIllegalAttrOrText";

		// Token: 0x04000936 RID: 2358
		internal const string XmlIllegalSoapAttribute = "XmlIllegalSoapAttribute";

		// Token: 0x04000937 RID: 2359
		internal const string XmlIllegalAttrOrTextInterface = "XmlIllegalAttrOrTextInterface";

		// Token: 0x04000938 RID: 2360
		internal const string XmlIllegalAttributeFlagsArray = "XmlIllegalAttributeFlagsArray";

		// Token: 0x04000939 RID: 2361
		internal const string XmlIllegalAnyElement = "XmlIllegalAnyElement";

		// Token: 0x0400093A RID: 2362
		internal const string XmlInvalidIsNullable = "XmlInvalidIsNullable";

		// Token: 0x0400093B RID: 2363
		internal const string XmlInvalidNotNullable = "XmlInvalidNotNullable";

		// Token: 0x0400093C RID: 2364
		internal const string XmlInvalidFormUnqualified = "XmlInvalidFormUnqualified";

		// Token: 0x0400093D RID: 2365
		internal const string XmlDuplicateNamespace = "XmlDuplicateNamespace";

		// Token: 0x0400093E RID: 2366
		internal const string XmlElementHasNoName = "XmlElementHasNoName";

		// Token: 0x0400093F RID: 2367
		internal const string XmlAttributeHasNoName = "XmlAttributeHasNoName";

		// Token: 0x04000940 RID: 2368
		internal const string XmlElementImportedTwice = "XmlElementImportedTwice";

		// Token: 0x04000941 RID: 2369
		internal const string XmlHiddenMember = "XmlHiddenMember";

		// Token: 0x04000942 RID: 2370
		internal const string XmlInvalidXmlOverride = "XmlInvalidXmlOverride";

		// Token: 0x04000943 RID: 2371
		internal const string XmlMembersDeriveError = "XmlMembersDeriveError";

		// Token: 0x04000944 RID: 2372
		internal const string XmlTypeUsedTwice = "XmlTypeUsedTwice";

		// Token: 0x04000945 RID: 2373
		internal const string XmlMissingGroup = "XmlMissingGroup";

		// Token: 0x04000946 RID: 2374
		internal const string XmlMissingAttributeGroup = "XmlMissingAttributeGroup";

		// Token: 0x04000947 RID: 2375
		internal const string XmlMissingDataType = "XmlMissingDataType";

		// Token: 0x04000948 RID: 2376
		internal const string XmlInvalidEncoding = "XmlInvalidEncoding";

		// Token: 0x04000949 RID: 2377
		internal const string XmlMissingElement = "XmlMissingElement";

		// Token: 0x0400094A RID: 2378
		internal const string XmlMissingAttribute = "XmlMissingAttribute";

		// Token: 0x0400094B RID: 2379
		internal const string XmlMissingMethodEnum = "XmlMissingMethodEnum";

		// Token: 0x0400094C RID: 2380
		internal const string XmlNoAttributeHere = "XmlNoAttributeHere";

		// Token: 0x0400094D RID: 2381
		internal const string XmlNeedAttributeHere = "XmlNeedAttributeHere";

		// Token: 0x0400094E RID: 2382
		internal const string XmlElementNameMismatch = "XmlElementNameMismatch";

		// Token: 0x0400094F RID: 2383
		internal const string XmlUnsupportedDefaultType = "XmlUnsupportedDefaultType";

		// Token: 0x04000950 RID: 2384
		internal const string XmlUnsupportedDefaultValue = "XmlUnsupportedDefaultValue";

		// Token: 0x04000951 RID: 2385
		internal const string XmlInvalidDefaultValue = "XmlInvalidDefaultValue";

		// Token: 0x04000952 RID: 2386
		internal const string XmlInvalidDefaultEnumValue = "XmlInvalidDefaultEnumValue";

		// Token: 0x04000953 RID: 2387
		internal const string XmlUnknownNode = "XmlUnknownNode";

		// Token: 0x04000954 RID: 2388
		internal const string XmlUnknownConstant = "XmlUnknownConstant";

		// Token: 0x04000955 RID: 2389
		internal const string XmlSerializeError = "XmlSerializeError";

		// Token: 0x04000956 RID: 2390
		internal const string XmlSerializeErrorDetails = "XmlSerializeErrorDetails";

		// Token: 0x04000957 RID: 2391
		internal const string XmlCompilerError = "XmlCompilerError";

		// Token: 0x04000958 RID: 2392
		internal const string XmlSchemaDuplicateNamespace = "XmlSchemaDuplicateNamespace";

		// Token: 0x04000959 RID: 2393
		internal const string XmlSchemaCompiled = "XmlSchemaCompiled";

		// Token: 0x0400095A RID: 2394
		internal const string XmlInvalidSchemaExtension = "XmlInvalidSchemaExtension";

		// Token: 0x0400095B RID: 2395
		internal const string XmlInvalidArrayDimentions = "XmlInvalidArrayDimentions";

		// Token: 0x0400095C RID: 2396
		internal const string XmlInvalidArrayTypeName = "XmlInvalidArrayTypeName";

		// Token: 0x0400095D RID: 2397
		internal const string XmlInvalidArrayTypeNamespace = "XmlInvalidArrayTypeNamespace";

		// Token: 0x0400095E RID: 2398
		internal const string XmlMissingArrayType = "XmlMissingArrayType";

		// Token: 0x0400095F RID: 2399
		internal const string XmlEmptyArrayType = "XmlEmptyArrayType";

		// Token: 0x04000960 RID: 2400
		internal const string XmlInvalidArraySyntax = "XmlInvalidArraySyntax";

		// Token: 0x04000961 RID: 2401
		internal const string XmlInvalidArrayTypeSyntax = "XmlInvalidArrayTypeSyntax";

		// Token: 0x04000962 RID: 2402
		internal const string XmlMismatchedArrayBrackets = "XmlMismatchedArrayBrackets";

		// Token: 0x04000963 RID: 2403
		internal const string XmlInvalidArrayLength = "XmlInvalidArrayLength";

		// Token: 0x04000964 RID: 2404
		internal const string XmlMissingHref = "XmlMissingHref";

		// Token: 0x04000965 RID: 2405
		internal const string XmlInvalidHref = "XmlInvalidHref";

		// Token: 0x04000966 RID: 2406
		internal const string XmlUnknownType = "XmlUnknownType";

		// Token: 0x04000967 RID: 2407
		internal const string XmlAbstractType = "XmlAbstractType";

		// Token: 0x04000968 RID: 2408
		internal const string XmlMappingsScopeMismatch = "XmlMappingsScopeMismatch";

		// Token: 0x04000969 RID: 2409
		internal const string XmlMethodTypeNameConflict = "XmlMethodTypeNameConflict";

		// Token: 0x0400096A RID: 2410
		internal const string XmlCannotReconcileAccessor = "XmlCannotReconcileAccessor";

		// Token: 0x0400096B RID: 2411
		internal const string XmlCannotReconcileAttributeAccessor = "XmlCannotReconcileAttributeAccessor";

		// Token: 0x0400096C RID: 2412
		internal const string XmlCannotReconcileAccessorDefault = "XmlCannotReconcileAccessorDefault";

		// Token: 0x0400096D RID: 2413
		internal const string XmlInvalidTypeAttributes = "XmlInvalidTypeAttributes";

		// Token: 0x0400096E RID: 2414
		internal const string XmlInvalidAttributeUse = "XmlInvalidAttributeUse";

		// Token: 0x0400096F RID: 2415
		internal const string XmlTypesDuplicate = "XmlTypesDuplicate";

		// Token: 0x04000970 RID: 2416
		internal const string XmlInvalidSoapArray = "XmlInvalidSoapArray";

		// Token: 0x04000971 RID: 2417
		internal const string XmlCannotIncludeInSchema = "XmlCannotIncludeInSchema";

		// Token: 0x04000972 RID: 2418
		internal const string XmlSoapCannotIncludeInSchema = "XmlSoapCannotIncludeInSchema";

		// Token: 0x04000973 RID: 2419
		internal const string XmlInvalidSerializable = "XmlInvalidSerializable";

		// Token: 0x04000974 RID: 2420
		internal const string XmlInvalidUseOfType = "XmlInvalidUseOfType";

		// Token: 0x04000975 RID: 2421
		internal const string XmlUnxpectedType = "XmlUnxpectedType";

		// Token: 0x04000976 RID: 2422
		internal const string XmlUnknownAnyElement = "XmlUnknownAnyElement";

		// Token: 0x04000977 RID: 2423
		internal const string XmlMultipleAttributeOverrides = "XmlMultipleAttributeOverrides";

		// Token: 0x04000978 RID: 2424
		internal const string XmlInvalidEnumAttribute = "XmlInvalidEnumAttribute";

		// Token: 0x04000979 RID: 2425
		internal const string XmlInvalidReturnPosition = "XmlInvalidReturnPosition";

		// Token: 0x0400097A RID: 2426
		internal const string XmlInvalidElementAttribute = "XmlInvalidElementAttribute";

		// Token: 0x0400097B RID: 2427
		internal const string XmlInvalidVoid = "XmlInvalidVoid";

		// Token: 0x0400097C RID: 2428
		internal const string XmlInvalidContent = "XmlInvalidContent";

		// Token: 0x0400097D RID: 2429
		internal const string XmlInvalidSchemaElementType = "XmlInvalidSchemaElementType";

		// Token: 0x0400097E RID: 2430
		internal const string XmlInvalidSubstitutionGroupUse = "XmlInvalidSubstitutionGroupUse";

		// Token: 0x0400097F RID: 2431
		internal const string XmlElementMissingType = "XmlElementMissingType";

		// Token: 0x04000980 RID: 2432
		internal const string XmlInvalidAnyAttributeUse = "XmlInvalidAnyAttributeUse";

		// Token: 0x04000981 RID: 2433
		internal const string XmlSoapInvalidAttributeUse = "XmlSoapInvalidAttributeUse";

		// Token: 0x04000982 RID: 2434
		internal const string XmlSoapInvalidChoice = "XmlSoapInvalidChoice";

		// Token: 0x04000983 RID: 2435
		internal const string XmlSoapUnsupportedGroupRef = "XmlSoapUnsupportedGroupRef";

		// Token: 0x04000984 RID: 2436
		internal const string XmlSoapUnsupportedGroupRepeat = "XmlSoapUnsupportedGroupRepeat";

		// Token: 0x04000985 RID: 2437
		internal const string XmlSoapUnsupportedGroupNested = "XmlSoapUnsupportedGroupNested";

		// Token: 0x04000986 RID: 2438
		internal const string XmlSoapUnsupportedGroupAny = "XmlSoapUnsupportedGroupAny";

		// Token: 0x04000987 RID: 2439
		internal const string XmlInvalidEnumContent = "XmlInvalidEnumContent";

		// Token: 0x04000988 RID: 2440
		internal const string XmlInvalidAttributeType = "XmlInvalidAttributeType";

		// Token: 0x04000989 RID: 2441
		internal const string XmlInvalidBaseType = "XmlInvalidBaseType";

		// Token: 0x0400098A RID: 2442
		internal const string XmlPrimitiveBaseType = "XmlPrimitiveBaseType";

		// Token: 0x0400098B RID: 2443
		internal const string XmlInvalidIdentifier = "XmlInvalidIdentifier";

		// Token: 0x0400098C RID: 2444
		internal const string XmlGenError = "XmlGenError";

		// Token: 0x0400098D RID: 2445
		internal const string XmlInvalidXmlns = "XmlInvalidXmlns";

		// Token: 0x0400098E RID: 2446
		internal const string XmlCircularReference = "XmlCircularReference";

		// Token: 0x0400098F RID: 2447
		internal const string XmlCircularReference2 = "XmlCircularReference2";

		// Token: 0x04000990 RID: 2448
		internal const string XmlAnonymousBaseType = "XmlAnonymousBaseType";

		// Token: 0x04000991 RID: 2449
		internal const string XmlMissingSchema = "XmlMissingSchema";

		// Token: 0x04000992 RID: 2450
		internal const string XmlNoSerializableMembers = "XmlNoSerializableMembers";

		// Token: 0x04000993 RID: 2451
		internal const string XmlIllegalOverride = "XmlIllegalOverride";

		// Token: 0x04000994 RID: 2452
		internal const string XmlReadOnlyCollection = "XmlReadOnlyCollection";

		// Token: 0x04000995 RID: 2453
		internal const string XmlRpcNestedValueType = "XmlRpcNestedValueType";

		// Token: 0x04000996 RID: 2454
		internal const string XmlRpcRefsInValueType = "XmlRpcRefsInValueType";

		// Token: 0x04000997 RID: 2455
		internal const string XmlRpcArrayOfValueTypes = "XmlRpcArrayOfValueTypes";

		// Token: 0x04000998 RID: 2456
		internal const string XmlDuplicateElementName = "XmlDuplicateElementName";

		// Token: 0x04000999 RID: 2457
		internal const string XmlDuplicateAttributeName = "XmlDuplicateAttributeName";

		// Token: 0x0400099A RID: 2458
		internal const string XmlBadBaseElement = "XmlBadBaseElement";

		// Token: 0x0400099B RID: 2459
		internal const string XmlBadBaseType = "XmlBadBaseType";

		// Token: 0x0400099C RID: 2460
		internal const string XmlUndefinedAlias = "XmlUndefinedAlias";

		// Token: 0x0400099D RID: 2461
		internal const string XmlChoiceIdentifierType = "XmlChoiceIdentifierType";

		// Token: 0x0400099E RID: 2462
		internal const string XmlChoiceIdentifierArrayType = "XmlChoiceIdentifierArrayType";

		// Token: 0x0400099F RID: 2463
		internal const string XmlChoiceIdentifierTypeEnum = "XmlChoiceIdentifierTypeEnum";

		// Token: 0x040009A0 RID: 2464
		internal const string XmlChoiceIdentiferMemberMissing = "XmlChoiceIdentiferMemberMissing";

		// Token: 0x040009A1 RID: 2465
		internal const string XmlChoiceIdentiferAmbiguous = "XmlChoiceIdentiferAmbiguous";

		// Token: 0x040009A2 RID: 2466
		internal const string XmlChoiceIdentiferMissing = "XmlChoiceIdentiferMissing";

		// Token: 0x040009A3 RID: 2467
		internal const string XmlChoiceMissingValue = "XmlChoiceMissingValue";

		// Token: 0x040009A4 RID: 2468
		internal const string XmlChoiceMissingAnyValue = "XmlChoiceMissingAnyValue";

		// Token: 0x040009A5 RID: 2469
		internal const string XmlChoiceMismatchChoiceException = "XmlChoiceMismatchChoiceException";

		// Token: 0x040009A6 RID: 2470
		internal const string XmlArrayItemAmbiguousTypes = "XmlArrayItemAmbiguousTypes";

		// Token: 0x040009A7 RID: 2471
		internal const string XmlUnsupportedInterface = "XmlUnsupportedInterface";

		// Token: 0x040009A8 RID: 2472
		internal const string XmlUnsupportedInterfaceDetails = "XmlUnsupportedInterfaceDetails";

		// Token: 0x040009A9 RID: 2473
		internal const string XmlUnsupportedRank = "XmlUnsupportedRank";

		// Token: 0x040009AA RID: 2474
		internal const string XmlUnsupportedInheritance = "XmlUnsupportedInheritance";

		// Token: 0x040009AB RID: 2475
		internal const string XmlIllegalMultipleText = "XmlIllegalMultipleText";

		// Token: 0x040009AC RID: 2476
		internal const string XmlIllegalMultipleTextMembers = "XmlIllegalMultipleTextMembers";

		// Token: 0x040009AD RID: 2477
		internal const string XmlIllegalArrayTextAttribute = "XmlIllegalArrayTextAttribute";

		// Token: 0x040009AE RID: 2478
		internal const string XmlIllegalTypedTextAttribute = "XmlIllegalTypedTextAttribute";

		// Token: 0x040009AF RID: 2479
		internal const string XmlIllegalSimpleContentExtension = "XmlIllegalSimpleContentExtension";

		// Token: 0x040009B0 RID: 2480
		internal const string XmlInvalidCast = "XmlInvalidCast";

		// Token: 0x040009B1 RID: 2481
		internal const string XmlInvalidCastWithId = "XmlInvalidCastWithId";

		// Token: 0x040009B2 RID: 2482
		internal const string XmlInvalidArrayRef = "XmlInvalidArrayRef";

		// Token: 0x040009B3 RID: 2483
		internal const string XmlInvalidNullCast = "XmlInvalidNullCast";

		// Token: 0x040009B4 RID: 2484
		internal const string XmlMultipleXmlns = "XmlMultipleXmlns";

		// Token: 0x040009B5 RID: 2485
		internal const string XmlMultipleXmlnsMembers = "XmlMultipleXmlnsMembers";

		// Token: 0x040009B6 RID: 2486
		internal const string XmlXmlnsInvalidType = "XmlXmlnsInvalidType";

		// Token: 0x040009B7 RID: 2487
		internal const string XmlSoleXmlnsAttribute = "XmlSoleXmlnsAttribute";

		// Token: 0x040009B8 RID: 2488
		internal const string XmlConstructorHasSecurityAttributes = "XmlConstructorHasSecurityAttributes";

		// Token: 0x040009B9 RID: 2489
		internal const string XmlPropertyHasSecurityAttributes = "XmlPropertyHasSecurityAttributes";

		// Token: 0x040009BA RID: 2490
		internal const string XmlMethodHasSecurityAttributes = "XmlMethodHasSecurityAttributes";

		// Token: 0x040009BB RID: 2491
		internal const string XmlDefaultAccessorHasSecurityAttributes = "XmlDefaultAccessorHasSecurityAttributes";

		// Token: 0x040009BC RID: 2492
		internal const string XmlInvalidChoiceIdentifierValue = "XmlInvalidChoiceIdentifierValue";

		// Token: 0x040009BD RID: 2493
		internal const string XmlAnyElementDuplicate = "XmlAnyElementDuplicate";

		// Token: 0x040009BE RID: 2494
		internal const string XmlChoiceIdDuplicate = "XmlChoiceIdDuplicate";

		// Token: 0x040009BF RID: 2495
		internal const string XmlChoiceIdentifierMismatch = "XmlChoiceIdentifierMismatch";

		// Token: 0x040009C0 RID: 2496
		internal const string XmlUnsupportedRedefine = "XmlUnsupportedRedefine";

		// Token: 0x040009C1 RID: 2497
		internal const string XmlDuplicateElementInScope = "XmlDuplicateElementInScope";

		// Token: 0x040009C2 RID: 2498
		internal const string XmlDuplicateElementInScope1 = "XmlDuplicateElementInScope1";

		// Token: 0x040009C3 RID: 2499
		internal const string XmlNoPartialTrust = "XmlNoPartialTrust";

		// Token: 0x040009C4 RID: 2500
		internal const string XmlInvalidEncodingNotEncoded1 = "XmlInvalidEncodingNotEncoded1";

		// Token: 0x040009C5 RID: 2501
		internal const string XmlInvalidEncoding3 = "XmlInvalidEncoding3";

		// Token: 0x040009C6 RID: 2502
		internal const string XmlInvalidSpecifiedType = "XmlInvalidSpecifiedType";

		// Token: 0x040009C7 RID: 2503
		internal const string XmlUnsupportedOpenGenericType = "XmlUnsupportedOpenGenericType";

		// Token: 0x040009C8 RID: 2504
		internal const string XmlMismatchSchemaObjects = "XmlMismatchSchemaObjects";

		// Token: 0x040009C9 RID: 2505
		internal const string XmlCircularTypeReference = "XmlCircularTypeReference";

		// Token: 0x040009CA RID: 2506
		internal const string XmlCircularGroupReference = "XmlCircularGroupReference";

		// Token: 0x040009CB RID: 2507
		internal const string XmlRpcLitElementNamespace = "XmlRpcLitElementNamespace";

		// Token: 0x040009CC RID: 2508
		internal const string XmlRpcLitElementNullable = "XmlRpcLitElementNullable";

		// Token: 0x040009CD RID: 2509
		internal const string XmlRpcLitElements = "XmlRpcLitElements";

		// Token: 0x040009CE RID: 2510
		internal const string XmlRpcLitArrayElement = "XmlRpcLitArrayElement";

		// Token: 0x040009CF RID: 2511
		internal const string XmlRpcLitAttributeAttributes = "XmlRpcLitAttributeAttributes";

		// Token: 0x040009D0 RID: 2512
		internal const string XmlRpcLitAttributes = "XmlRpcLitAttributes";

		// Token: 0x040009D1 RID: 2513
		internal const string XmlSequenceMembers = "XmlSequenceMembers";

		// Token: 0x040009D2 RID: 2514
		internal const string XmlRpcLitXmlns = "XmlRpcLitXmlns";

		// Token: 0x040009D3 RID: 2515
		internal const string XmlDuplicateNs = "XmlDuplicateNs";

		// Token: 0x040009D4 RID: 2516
		internal const string XmlAnonymousInclude = "XmlAnonymousInclude";

		// Token: 0x040009D5 RID: 2517
		internal const string RefSyntaxNotSupportedForElements0 = "RefSyntaxNotSupportedForElements0";

		// Token: 0x040009D6 RID: 2518
		internal const string XmlSchemaIncludeLocation = "XmlSchemaIncludeLocation";

		// Token: 0x040009D7 RID: 2519
		internal const string XmlSerializableSchemaError = "XmlSerializableSchemaError";

		// Token: 0x040009D8 RID: 2520
		internal const string XmlGetSchemaMethodName = "XmlGetSchemaMethodName";

		// Token: 0x040009D9 RID: 2521
		internal const string XmlGetSchemaMethodMissing = "XmlGetSchemaMethodMissing";

		// Token: 0x040009DA RID: 2522
		internal const string XmlGetSchemaMethodReturnType = "XmlGetSchemaMethodReturnType";

		// Token: 0x040009DB RID: 2523
		internal const string XmlGetSchemaEmptyTypeName = "XmlGetSchemaEmptyTypeName";

		// Token: 0x040009DC RID: 2524
		internal const string XmlGetSchemaTypeMissing = "XmlGetSchemaTypeMissing";

		// Token: 0x040009DD RID: 2525
		internal const string XmlGetSchemaInclude = "XmlGetSchemaInclude";

		// Token: 0x040009DE RID: 2526
		internal const string XmlSerializableAttributes = "XmlSerializableAttributes";

		// Token: 0x040009DF RID: 2527
		internal const string XmlSerializableMergeItem = "XmlSerializableMergeItem";

		// Token: 0x040009E0 RID: 2528
		internal const string XmlSerializableBadDerivation = "XmlSerializableBadDerivation";

		// Token: 0x040009E1 RID: 2529
		internal const string XmlSerializableMissingClrType = "XmlSerializableMissingClrType";

		// Token: 0x040009E2 RID: 2530
		internal const string XmlCircularDerivation = "XmlCircularDerivation";

		// Token: 0x040009E3 RID: 2531
		internal const string XmlSerializerAccessDenied = "XmlSerializerAccessDenied";

		// Token: 0x040009E4 RID: 2532
		internal const string XmlIdentityAccessDenied = "XmlIdentityAccessDenied";

		// Token: 0x040009E5 RID: 2533
		internal const string XmlMelformMapping = "XmlMelformMapping";

		// Token: 0x040009E6 RID: 2534
		internal const string XmlSchemaSyntaxErrorDetails = "XmlSchemaSyntaxErrorDetails";

		// Token: 0x040009E7 RID: 2535
		internal const string XmlSchemaElementReference = "XmlSchemaElementReference";

		// Token: 0x040009E8 RID: 2536
		internal const string XmlSchemaAttributeReference = "XmlSchemaAttributeReference";

		// Token: 0x040009E9 RID: 2537
		internal const string XmlSchemaItem = "XmlSchemaItem";

		// Token: 0x040009EA RID: 2538
		internal const string XmlSchemaNamedItem = "XmlSchemaNamedItem";

		// Token: 0x040009EB RID: 2539
		internal const string XmlSchemaContentDef = "XmlSchemaContentDef";

		// Token: 0x040009EC RID: 2540
		internal const string XmlSchema = "XmlSchema";

		// Token: 0x040009ED RID: 2541
		internal const string XmlSerializerCompileFailed = "XmlSerializerCompileFailed";

		// Token: 0x040009EE RID: 2542
		internal const string XmlSerializableRootDupName = "XmlSerializableRootDupName";

		// Token: 0x040009EF RID: 2543
		internal const string XmlDropDefaultAttribute = "XmlDropDefaultAttribute";

		// Token: 0x040009F0 RID: 2544
		internal const string XmlDropAttributeValue = "XmlDropAttributeValue";

		// Token: 0x040009F1 RID: 2545
		internal const string XmlDropArrayAttributeValue = "XmlDropArrayAttributeValue";

		// Token: 0x040009F2 RID: 2546
		internal const string XmlDropNonPrimitiveAttributeValue = "XmlDropNonPrimitiveAttributeValue";

		// Token: 0x040009F3 RID: 2547
		internal const string XmlNotKnownDefaultValue = "XmlNotKnownDefaultValue";

		// Token: 0x040009F4 RID: 2548
		internal const string XmlRemarks = "XmlRemarks";

		// Token: 0x040009F5 RID: 2549
		internal const string XmlCodegenWarningDetails = "XmlCodegenWarningDetails";

		// Token: 0x040009F6 RID: 2550
		internal const string XmlExtensionComment = "XmlExtensionComment";

		// Token: 0x040009F7 RID: 2551
		internal const string XmlExtensionDuplicateDefinition = "XmlExtensionDuplicateDefinition";

		// Token: 0x040009F8 RID: 2552
		internal const string XmlImporterExtensionBadLocalTypeName = "XmlImporterExtensionBadLocalTypeName";

		// Token: 0x040009F9 RID: 2553
		internal const string XmlImporterExtensionBadTypeName = "XmlImporterExtensionBadTypeName";

		// Token: 0x040009FA RID: 2554
		internal const string XmlConfigurationDuplicateExtension = "XmlConfigurationDuplicateExtension";

		// Token: 0x040009FB RID: 2555
		internal const string XmlPregenMissingDirectory = "XmlPregenMissingDirectory";

		// Token: 0x040009FC RID: 2556
		internal const string XmlPregenMissingTempDirectory = "XmlPregenMissingTempDirectory";

		// Token: 0x040009FD RID: 2557
		internal const string XmlPregenTypeDynamic = "XmlPregenTypeDynamic";

		// Token: 0x040009FE RID: 2558
		internal const string XmlSerializerExpiredDetails = "XmlSerializerExpiredDetails";

		// Token: 0x040009FF RID: 2559
		internal const string XmlSerializerExpired = "XmlSerializerExpired";

		// Token: 0x04000A00 RID: 2560
		internal const string XmlPregenAssemblyDynamic = "XmlPregenAssemblyDynamic";

		// Token: 0x04000A01 RID: 2561
		internal const string XmlNotSerializable = "XmlNotSerializable";

		// Token: 0x04000A02 RID: 2562
		internal const string XmlPregenOrphanType = "XmlPregenOrphanType";

		// Token: 0x04000A03 RID: 2563
		internal const string XmlPregenCannotLoad = "XmlPregenCannotLoad";

		// Token: 0x04000A04 RID: 2564
		internal const string XmlPregenInvalidXmlSerializerAssemblyAttribute = "XmlPregenInvalidXmlSerializerAssemblyAttribute";

		// Token: 0x04000A05 RID: 2565
		internal const string XmlSequenceInconsistent = "XmlSequenceInconsistent";

		// Token: 0x04000A06 RID: 2566
		internal const string XmlSequenceUnique = "XmlSequenceUnique";

		// Token: 0x04000A07 RID: 2567
		internal const string XmlSequenceHierarchy = "XmlSequenceHierarchy";

		// Token: 0x04000A08 RID: 2568
		internal const string XmlSequenceMatch = "XmlSequenceMatch";

		// Token: 0x04000A09 RID: 2569
		internal const string XmlDisallowNegativeValues = "XmlDisallowNegativeValues";

		// Token: 0x04000A0A RID: 2570
		internal const string Xml_BadComment = "Xml_BadComment";

		// Token: 0x04000A0B RID: 2571
		internal const string Xml_NumEntityOverflow = "Xml_NumEntityOverflow";

		// Token: 0x04000A0C RID: 2572
		internal const string Xml_UnexpectedCharacter = "Xml_UnexpectedCharacter";

		// Token: 0x04000A0D RID: 2573
		internal const string Xml_UnexpectedToken1 = "Xml_UnexpectedToken1";

		// Token: 0x04000A0E RID: 2574
		internal const string Xml_TagMismatchFileName = "Xml_TagMismatchFileName";

		// Token: 0x04000A0F RID: 2575
		internal const string Xml_ReservedNs = "Xml_ReservedNs";

		// Token: 0x04000A10 RID: 2576
		internal const string Xml_BadElementData = "Xml_BadElementData";

		// Token: 0x04000A11 RID: 2577
		internal const string Xml_UnexpectedElement = "Xml_UnexpectedElement";

		// Token: 0x04000A12 RID: 2578
		internal const string Xml_TagNotInTheSameEntity = "Xml_TagNotInTheSameEntity";

		// Token: 0x04000A13 RID: 2579
		internal const string Xml_InvalidPartialContentData = "Xml_InvalidPartialContentData";

		// Token: 0x04000A14 RID: 2580
		internal const string Xml_CanNotStartWithXmlInNamespace = "Xml_CanNotStartWithXmlInNamespace";

		// Token: 0x04000A15 RID: 2581
		internal const string Xml_UnparsedEntity = "Xml_UnparsedEntity";

		// Token: 0x04000A16 RID: 2582
		internal const string Xml_InvalidContentForThisNode = "Xml_InvalidContentForThisNode";

		// Token: 0x04000A17 RID: 2583
		internal const string Xml_MissingEncodingDecl = "Xml_MissingEncodingDecl";

		// Token: 0x04000A18 RID: 2584
		internal const string Xml_InvalidSurrogatePair = "Xml_InvalidSurrogatePair";

		// Token: 0x04000A19 RID: 2585
		internal const string Sch_ErrorPosition = "Sch_ErrorPosition";

		// Token: 0x04000A1A RID: 2586
		internal const string Sch_ReservedNsDecl = "Sch_ReservedNsDecl";

		// Token: 0x04000A1B RID: 2587
		internal const string Sch_NotInSchemaCollection = "Sch_NotInSchemaCollection";

		// Token: 0x04000A1C RID: 2588
		internal const string Sch_NotationNotAttr = "Sch_NotationNotAttr";

		// Token: 0x04000A1D RID: 2589
		internal const string Sch_InvalidContent = "Sch_InvalidContent";

		// Token: 0x04000A1E RID: 2590
		internal const string Sch_InvalidContentExpecting = "Sch_InvalidContentExpecting";

		// Token: 0x04000A1F RID: 2591
		internal const string Sch_InvalidTextWhiteSpace = "Sch_InvalidTextWhiteSpace";

		// Token: 0x04000A20 RID: 2592
		internal const string Sch_XSCHEMA = "Sch_XSCHEMA";

		// Token: 0x04000A21 RID: 2593
		internal const string Sch_DubSchema = "Sch_DubSchema";

		// Token: 0x04000A22 RID: 2594
		internal const string Xp_TokenExpected = "Xp_TokenExpected";

		// Token: 0x04000A23 RID: 2595
		internal const string Xp_NodeTestExpected = "Xp_NodeTestExpected";

		// Token: 0x04000A24 RID: 2596
		internal const string Xp_NumberExpected = "Xp_NumberExpected";

		// Token: 0x04000A25 RID: 2597
		internal const string Xp_QueryExpected = "Xp_QueryExpected";

		// Token: 0x04000A26 RID: 2598
		internal const string Xp_InvalidArgument = "Xp_InvalidArgument";

		// Token: 0x04000A27 RID: 2599
		internal const string Xp_FunctionExpected = "Xp_FunctionExpected";

		// Token: 0x04000A28 RID: 2600
		internal const string Xp_InvalidPatternString = "Xp_InvalidPatternString";

		// Token: 0x04000A29 RID: 2601
		internal const string Xp_BadQueryString = "Xp_BadQueryString";

		// Token: 0x04000A2A RID: 2602
		internal const string XdomXpNav_NullParam = "XdomXpNav_NullParam";

		// Token: 0x04000A2B RID: 2603
		internal const string Xdom_Load_NodeType = "Xdom_Load_NodeType";

		// Token: 0x04000A2C RID: 2604
		internal const string XmlMissingMethod = "XmlMissingMethod";

		// Token: 0x04000A2D RID: 2605
		internal const string XmlIncludeSerializableError = "XmlIncludeSerializableError";

		// Token: 0x04000A2E RID: 2606
		internal const string XmlCompilerDynModule = "XmlCompilerDynModule";

		// Token: 0x04000A2F RID: 2607
		internal const string XmlInvalidSchemaType = "XmlInvalidSchemaType";

		// Token: 0x04000A30 RID: 2608
		internal const string XmlInvalidAnyUse = "XmlInvalidAnyUse";

		// Token: 0x04000A31 RID: 2609
		internal const string XmlSchemaSyntaxError = "XmlSchemaSyntaxError";

		// Token: 0x04000A32 RID: 2610
		internal const string XmlDuplicateChoiceElement = "XmlDuplicateChoiceElement";

		// Token: 0x04000A33 RID: 2611
		internal const string XmlConvert_BadTimeSpan = "XmlConvert_BadTimeSpan";

		// Token: 0x04000A34 RID: 2612
		internal const string XmlConvert_BadBoolean = "XmlConvert_BadBoolean";

		// Token: 0x04000A35 RID: 2613
		internal const string Xml_UnexpectedToken = "Xml_UnexpectedToken";

		// Token: 0x04000A36 RID: 2614
		internal const string Xml_PartialContentNodeTypeNotSupported = "Xml_PartialContentNodeTypeNotSupported";

		// Token: 0x04000A37 RID: 2615
		internal const string Sch_AttributeValueDataType = "Sch_AttributeValueDataType";

		// Token: 0x04000A38 RID: 2616
		internal const string Sch_ElementValueDataType = "Sch_ElementValueDataType";

		// Token: 0x04000A39 RID: 2617
		internal const string Sch_NonDeterministicAny = "Sch_NonDeterministicAny";

		// Token: 0x04000A3A RID: 2618
		internal const string Sch_MismatchTargetNamespace = "Sch_MismatchTargetNamespace";

		// Token: 0x04000A3B RID: 2619
		internal const string Sch_UnionFailed = "Sch_UnionFailed";

		// Token: 0x04000A3C RID: 2620
		internal const string Sch_XsiTypeBlocked = "Sch_XsiTypeBlocked";

		// Token: 0x04000A3D RID: 2621
		internal const string Sch_InvalidElementInEmpty = "Sch_InvalidElementInEmpty";

		// Token: 0x04000A3E RID: 2622
		internal const string Sch_InvalidElementInTextOnly = "Sch_InvalidElementInTextOnly";

		// Token: 0x04000A3F RID: 2623
		internal const string Sch_InvalidNameAttribute = "Sch_InvalidNameAttribute";

		// Token: 0x04000A40 RID: 2624
		internal const string XmlInternalError = "XmlInternalError";

		// Token: 0x04000A41 RID: 2625
		internal const string XmlInternalErrorDetails = "XmlInternalErrorDetails";

		// Token: 0x04000A42 RID: 2626
		internal const string XmlInternalErrorMethod = "XmlInternalErrorMethod";

		// Token: 0x04000A43 RID: 2627
		internal const string XmlInternalErrorReaderAdvance = "XmlInternalErrorReaderAdvance";

		// Token: 0x04000A44 RID: 2628
		internal const string Enc_InvalidByteInEncoding = "Enc_InvalidByteInEncoding";

		// Token: 0x04000A45 RID: 2629
		internal const string Arg_ExpectingXmlTextReader = "Arg_ExpectingXmlTextReader";

		// Token: 0x04000A46 RID: 2630
		internal const string Arg_CannotCreateNode = "Arg_CannotCreateNode";

		// Token: 0x04000A47 RID: 2631
		internal const string Arg_IncompatibleParamType = "Arg_IncompatibleParamType";

		// Token: 0x04000A48 RID: 2632
		internal const string XmlNonCLSCompliantException = "XmlNonCLSCompliantException";

		// Token: 0x04000A49 RID: 2633
		internal const string Xml_XapResolverCannotOpenUri = "Xml_XapResolverCannotOpenUri";

		// Token: 0x04000A4A RID: 2634
		private static Res loader;

		// Token: 0x04000A4B RID: 2635
		private ResourceManager resources;
	}
}
