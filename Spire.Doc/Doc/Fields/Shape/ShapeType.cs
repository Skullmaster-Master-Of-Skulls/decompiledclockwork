using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x02000074 RID: 116
	internal enum ShapeType
	{
		// Token: 0x04000730 RID: 1840
		Group = -1,
		// Token: 0x04000731 RID: 1841
		Image = 75,
		// Token: 0x04000732 RID: 1842
		TextBox = 202,
		// Token: 0x04000733 RID: 1843
		OleObject = -2,
		// Token: 0x04000734 RID: 1844
		OleControl = 201,
		// Token: 0x04000735 RID: 1845
		NonPrimitive = 0,
		// Token: 0x04000736 RID: 1846
		Rectangle,
		// Token: 0x04000737 RID: 1847
		RoundRectangle,
		// Token: 0x04000738 RID: 1848
		Ellipse,
		// Token: 0x04000739 RID: 1849
		Diamond,
		// Token: 0x0400073A RID: 1850
		Triangle,
		// Token: 0x0400073B RID: 1851
		RightTriangle,
		// Token: 0x0400073C RID: 1852
		Parallelogram,
		// Token: 0x0400073D RID: 1853
		Trapezoid,
		// Token: 0x0400073E RID: 1854
		Hexagon,
		// Token: 0x0400073F RID: 1855
		Octagon,
		// Token: 0x04000740 RID: 1856
		Plus,
		// Token: 0x04000741 RID: 1857
		Star,
		// Token: 0x04000742 RID: 1858
		Arrow,
		// Token: 0x04000743 RID: 1859
		ThickArrow,
		// Token: 0x04000744 RID: 1860
		HomePlate,
		// Token: 0x04000745 RID: 1861
		Cube,
		// Token: 0x04000746 RID: 1862
		Balloon,
		// Token: 0x04000747 RID: 1863
		Seal,
		// Token: 0x04000748 RID: 1864
		Arc,
		// Token: 0x04000749 RID: 1865
		Line,
		// Token: 0x0400074A RID: 1866
		Plaque,
		// Token: 0x0400074B RID: 1867
		Can,
		// Token: 0x0400074C RID: 1868
		Donut,
		// Token: 0x0400074D RID: 1869
		TextSimple,
		// Token: 0x0400074E RID: 1870
		TextOctagon,
		// Token: 0x0400074F RID: 1871
		TextHexagon,
		// Token: 0x04000750 RID: 1872
		TextCurve,
		// Token: 0x04000751 RID: 1873
		TextWave,
		// Token: 0x04000752 RID: 1874
		TextRing,
		// Token: 0x04000753 RID: 1875
		TextOnCurve,
		// Token: 0x04000754 RID: 1876
		TextOnRing,
		// Token: 0x04000755 RID: 1877
		StraightConnector1,
		// Token: 0x04000756 RID: 1878
		BentConnector2,
		// Token: 0x04000757 RID: 1879
		BentConnector3,
		// Token: 0x04000758 RID: 1880
		BentConnector4,
		// Token: 0x04000759 RID: 1881
		BentConnector5,
		// Token: 0x0400075A RID: 1882
		CurvedConnector2,
		// Token: 0x0400075B RID: 1883
		CurvedConnector3,
		// Token: 0x0400075C RID: 1884
		CurvedConnector4,
		// Token: 0x0400075D RID: 1885
		CurvedConnector5,
		// Token: 0x0400075E RID: 1886
		Callout1,
		// Token: 0x0400075F RID: 1887
		Callout2,
		// Token: 0x04000760 RID: 1888
		Callout3,
		// Token: 0x04000761 RID: 1889
		AccentCallout1,
		// Token: 0x04000762 RID: 1890
		AccentCallout2,
		// Token: 0x04000763 RID: 1891
		AccentCallout3,
		// Token: 0x04000764 RID: 1892
		BorderCallout1,
		// Token: 0x04000765 RID: 1893
		BorderCallout2,
		// Token: 0x04000766 RID: 1894
		BorderCallout3,
		// Token: 0x04000767 RID: 1895
		AccentBorderCallout1,
		// Token: 0x04000768 RID: 1896
		AccentBorderCallout2,
		// Token: 0x04000769 RID: 1897
		AccentBorderCallout3,
		// Token: 0x0400076A RID: 1898
		AccentBorderCallout90 = 181,
		// Token: 0x0400076B RID: 1899
		Ribbon = 53,
		// Token: 0x0400076C RID: 1900
		Ribbon2,
		// Token: 0x0400076D RID: 1901
		Chevron,
		// Token: 0x0400076E RID: 1902
		Pentagon,
		// Token: 0x0400076F RID: 1903
		NoSmoking,
		// Token: 0x04000770 RID: 1904
		Seal8,
		// Token: 0x04000771 RID: 1905
		Seal16,
		// Token: 0x04000772 RID: 1906
		Seal32,
		// Token: 0x04000773 RID: 1907
		WedgeRectCallout,
		// Token: 0x04000774 RID: 1908
		WedgeRRectCallout,
		// Token: 0x04000775 RID: 1909
		WedgeEllipseCallout,
		// Token: 0x04000776 RID: 1910
		Wave,
		// Token: 0x04000777 RID: 1911
		FoldedCorner,
		// Token: 0x04000778 RID: 1912
		LeftArrow,
		// Token: 0x04000779 RID: 1913
		DownArrow,
		// Token: 0x0400077A RID: 1914
		UpArrow,
		// Token: 0x0400077B RID: 1915
		LeftRightArrow,
		// Token: 0x0400077C RID: 1916
		UpDownArrow,
		// Token: 0x0400077D RID: 1917
		IrregularSeal1,
		// Token: 0x0400077E RID: 1918
		IrregularSeal2,
		// Token: 0x0400077F RID: 1919
		LightningBolt,
		// Token: 0x04000780 RID: 1920
		Heart,
		// Token: 0x04000781 RID: 1921
		QuadArrow = 76,
		// Token: 0x04000782 RID: 1922
		LeftArrowCallout,
		// Token: 0x04000783 RID: 1923
		RightArrowCallout,
		// Token: 0x04000784 RID: 1924
		UpArrowCallout,
		// Token: 0x04000785 RID: 1925
		DownArrowCallout,
		// Token: 0x04000786 RID: 1926
		LeftRightArrowCallout,
		// Token: 0x04000787 RID: 1927
		UpDownArrowCallout,
		// Token: 0x04000788 RID: 1928
		QuadArrowCallout,
		// Token: 0x04000789 RID: 1929
		Bevel,
		// Token: 0x0400078A RID: 1930
		LeftBracket,
		// Token: 0x0400078B RID: 1931
		RightBracket,
		// Token: 0x0400078C RID: 1932
		LeftBrace,
		// Token: 0x0400078D RID: 1933
		RightBrace,
		// Token: 0x0400078E RID: 1934
		LeftUpArrow,
		// Token: 0x0400078F RID: 1935
		BentUpArrow,
		// Token: 0x04000790 RID: 1936
		BentArrow,
		// Token: 0x04000791 RID: 1937
		Seal24,
		// Token: 0x04000792 RID: 1938
		StripedRightArrow,
		// Token: 0x04000793 RID: 1939
		NotchedRightArrow,
		// Token: 0x04000794 RID: 1940
		BlockArc,
		// Token: 0x04000795 RID: 1941
		SmileyFace,
		// Token: 0x04000796 RID: 1942
		VerticalScroll,
		// Token: 0x04000797 RID: 1943
		HorizontalScroll,
		// Token: 0x04000798 RID: 1944
		CircularArrow,
		// Token: 0x04000799 RID: 1945
		CustomShape,
		// Token: 0x0400079A RID: 1946
		UturnArrow,
		// Token: 0x0400079B RID: 1947
		CurvedRightArrow,
		// Token: 0x0400079C RID: 1948
		CurvedLeftArrow,
		// Token: 0x0400079D RID: 1949
		CurvedUpArrow,
		// Token: 0x0400079E RID: 1950
		CurvedDownArrow,
		// Token: 0x0400079F RID: 1951
		CloudCallout,
		// Token: 0x040007A0 RID: 1952
		EllipseRibbon,
		// Token: 0x040007A1 RID: 1953
		EllipseRibbon2,
		// Token: 0x040007A2 RID: 1954
		FlowChartProcess,
		// Token: 0x040007A3 RID: 1955
		FlowChartDecision,
		// Token: 0x040007A4 RID: 1956
		FlowChartInputOutput,
		// Token: 0x040007A5 RID: 1957
		FlowChartPredefinedProcess,
		// Token: 0x040007A6 RID: 1958
		FlowChartInternalStorage,
		// Token: 0x040007A7 RID: 1959
		FlowChartDocument,
		// Token: 0x040007A8 RID: 1960
		FlowChartMultidocument,
		// Token: 0x040007A9 RID: 1961
		FlowChartTerminator,
		// Token: 0x040007AA RID: 1962
		FlowChartPreparation,
		// Token: 0x040007AB RID: 1963
		FlowChartManualInput,
		// Token: 0x040007AC RID: 1964
		FlowChartManualOperation,
		// Token: 0x040007AD RID: 1965
		FlowChartConnector,
		// Token: 0x040007AE RID: 1966
		FlowChartPunchedCard,
		// Token: 0x040007AF RID: 1967
		FlowChartPunchedTape,
		// Token: 0x040007B0 RID: 1968
		FlowChartSummingJunction,
		// Token: 0x040007B1 RID: 1969
		FlowChartOr,
		// Token: 0x040007B2 RID: 1970
		FlowChartCollate,
		// Token: 0x040007B3 RID: 1971
		FlowChartSort,
		// Token: 0x040007B4 RID: 1972
		FlowChartExtract,
		// Token: 0x040007B5 RID: 1973
		FlowChartMerge,
		// Token: 0x040007B6 RID: 1974
		FlowChartOfflineStorage,
		// Token: 0x040007B7 RID: 1975
		FlowChartOnlineStorage,
		// Token: 0x040007B8 RID: 1976
		FlowChartMagneticTape,
		// Token: 0x040007B9 RID: 1977
		FlowChartMagneticDisk,
		// Token: 0x040007BA RID: 1978
		FlowChartMagneticDrum,
		// Token: 0x040007BB RID: 1979
		FlowChartDisplay,
		// Token: 0x040007BC RID: 1980
		FlowChartDelay,
		// Token: 0x040007BD RID: 1981
		TextPlainText,
		// Token: 0x040007BE RID: 1982
		TextStop,
		// Token: 0x040007BF RID: 1983
		TextTriangle,
		// Token: 0x040007C0 RID: 1984
		TextTriangleInverted,
		// Token: 0x040007C1 RID: 1985
		TextChevron,
		// Token: 0x040007C2 RID: 1986
		TextChevronInverted,
		// Token: 0x040007C3 RID: 1987
		TextRingInside,
		// Token: 0x040007C4 RID: 1988
		TextRingOutside,
		// Token: 0x040007C5 RID: 1989
		TextArchUpCurve,
		// Token: 0x040007C6 RID: 1990
		TextArchDownCurve,
		// Token: 0x040007C7 RID: 1991
		TextCircleCurve,
		// Token: 0x040007C8 RID: 1992
		TextButtonCurve,
		// Token: 0x040007C9 RID: 1993
		TextArchUpPour,
		// Token: 0x040007CA RID: 1994
		TextArchDownPour,
		// Token: 0x040007CB RID: 1995
		TextCirclePour,
		// Token: 0x040007CC RID: 1996
		TextButtonPour,
		// Token: 0x040007CD RID: 1997
		TextCurveUp,
		// Token: 0x040007CE RID: 1998
		TextCurveDown,
		// Token: 0x040007CF RID: 1999
		TextCascadeUp,
		// Token: 0x040007D0 RID: 2000
		TextCascadeDown,
		// Token: 0x040007D1 RID: 2001
		TextWave1,
		// Token: 0x040007D2 RID: 2002
		TextWave2,
		// Token: 0x040007D3 RID: 2003
		TextWave3,
		// Token: 0x040007D4 RID: 2004
		TextWave4,
		// Token: 0x040007D5 RID: 2005
		TextInflate,
		// Token: 0x040007D6 RID: 2006
		TextDeflate,
		// Token: 0x040007D7 RID: 2007
		TextInflateBottom,
		// Token: 0x040007D8 RID: 2008
		TextDeflateBottom,
		// Token: 0x040007D9 RID: 2009
		TextInflateTop,
		// Token: 0x040007DA RID: 2010
		TextDeflateTop,
		// Token: 0x040007DB RID: 2011
		TextDeflateInflate,
		// Token: 0x040007DC RID: 2012
		TextDeflateInflateDeflate,
		// Token: 0x040007DD RID: 2013
		TextFadeRight,
		// Token: 0x040007DE RID: 2014
		TextFadeLeft,
		// Token: 0x040007DF RID: 2015
		TextFadeUp,
		// Token: 0x040007E0 RID: 2016
		TextFadeDown,
		// Token: 0x040007E1 RID: 2017
		TextSlantUp,
		// Token: 0x040007E2 RID: 2018
		TextSlantDown,
		// Token: 0x040007E3 RID: 2019
		TextCanUp,
		// Token: 0x040007E4 RID: 2020
		TextCanDown,
		// Token: 0x040007E5 RID: 2021
		FlowChartAlternateProcess,
		// Token: 0x040007E6 RID: 2022
		FlowChartOffpageConnector,
		// Token: 0x040007E7 RID: 2023
		Callout90,
		// Token: 0x040007E8 RID: 2024
		AccentCallout90,
		// Token: 0x040007E9 RID: 2025
		BorderCallout90,
		// Token: 0x040007EA RID: 2026
		LeftRightUpArrow = 182,
		// Token: 0x040007EB RID: 2027
		Sun,
		// Token: 0x040007EC RID: 2028
		Moon,
		// Token: 0x040007ED RID: 2029
		BracketPair,
		// Token: 0x040007EE RID: 2030
		BracePair,
		// Token: 0x040007EF RID: 2031
		Seal4,
		// Token: 0x040007F0 RID: 2032
		DoubleWave,
		// Token: 0x040007F1 RID: 2033
		ActionButtonBlank,
		// Token: 0x040007F2 RID: 2034
		ActionButtonHome,
		// Token: 0x040007F3 RID: 2035
		ActionButtonHelp,
		// Token: 0x040007F4 RID: 2036
		ActionButtonInformation,
		// Token: 0x040007F5 RID: 2037
		ActionButtonForwardNext,
		// Token: 0x040007F6 RID: 2038
		ActionButtonBackPrevious,
		// Token: 0x040007F7 RID: 2039
		ActionButtonEnd,
		// Token: 0x040007F8 RID: 2040
		ActionButtonBeginning,
		// Token: 0x040007F9 RID: 2041
		ActionButtonReturn,
		// Token: 0x040007FA RID: 2042
		ActionButtonDocument,
		// Token: 0x040007FB RID: 2043
		ActionButtonSound,
		// Token: 0x040007FC RID: 2044
		ActionButtonMovie,
		// Token: 0x040007FD RID: 2045
		MinValue = -2
	}
}
