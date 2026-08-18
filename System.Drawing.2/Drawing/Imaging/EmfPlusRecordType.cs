using System;

namespace System.Drawing.Imaging
{
	// Token: 0x02000096 RID: 150
	public enum EmfPlusRecordType
	{
		// Token: 0x04000774 RID: 1908
		WmfRecordBase = 65536,
		// Token: 0x04000775 RID: 1909
		WmfSetBkColor = 66049,
		// Token: 0x04000776 RID: 1910
		WmfSetBkMode = 65794,
		// Token: 0x04000777 RID: 1911
		WmfSetMapMode,
		// Token: 0x04000778 RID: 1912
		WmfSetROP2,
		// Token: 0x04000779 RID: 1913
		WmfSetRelAbs,
		// Token: 0x0400077A RID: 1914
		WmfSetPolyFillMode,
		// Token: 0x0400077B RID: 1915
		WmfSetStretchBltMode,
		// Token: 0x0400077C RID: 1916
		WmfSetTextCharExtra,
		// Token: 0x0400077D RID: 1917
		WmfSetTextColor = 66057,
		// Token: 0x0400077E RID: 1918
		WmfSetTextJustification,
		// Token: 0x0400077F RID: 1919
		WmfSetWindowOrg,
		// Token: 0x04000780 RID: 1920
		WmfSetWindowExt,
		// Token: 0x04000781 RID: 1921
		WmfSetViewportOrg,
		// Token: 0x04000782 RID: 1922
		WmfSetViewportExt,
		// Token: 0x04000783 RID: 1923
		WmfOffsetWindowOrg,
		// Token: 0x04000784 RID: 1924
		WmfScaleWindowExt = 66576,
		// Token: 0x04000785 RID: 1925
		WmfOffsetViewportOrg = 66065,
		// Token: 0x04000786 RID: 1926
		WmfScaleViewportExt = 66578,
		// Token: 0x04000787 RID: 1927
		WmfLineTo = 66067,
		// Token: 0x04000788 RID: 1928
		WmfMoveTo,
		// Token: 0x04000789 RID: 1929
		WmfExcludeClipRect = 66581,
		// Token: 0x0400078A RID: 1930
		WmfIntersectClipRect,
		// Token: 0x0400078B RID: 1931
		WmfArc = 67607,
		// Token: 0x0400078C RID: 1932
		WmfEllipse = 66584,
		// Token: 0x0400078D RID: 1933
		WmfFloodFill,
		// Token: 0x0400078E RID: 1934
		WmfPie = 67610,
		// Token: 0x0400078F RID: 1935
		WmfRectangle = 66587,
		// Token: 0x04000790 RID: 1936
		WmfRoundRect = 67100,
		// Token: 0x04000791 RID: 1937
		WmfPatBlt,
		// Token: 0x04000792 RID: 1938
		WmfSaveDC = 65566,
		// Token: 0x04000793 RID: 1939
		WmfSetPixel = 66591,
		// Token: 0x04000794 RID: 1940
		WmfOffsetCilpRgn = 66080,
		// Token: 0x04000795 RID: 1941
		WmfTextOut = 66849,
		// Token: 0x04000796 RID: 1942
		WmfBitBlt = 67874,
		// Token: 0x04000797 RID: 1943
		WmfStretchBlt = 68387,
		// Token: 0x04000798 RID: 1944
		WmfPolygon = 66340,
		// Token: 0x04000799 RID: 1945
		WmfPolyline,
		// Token: 0x0400079A RID: 1946
		WmfEscape = 67110,
		// Token: 0x0400079B RID: 1947
		WmfRestoreDC = 65831,
		// Token: 0x0400079C RID: 1948
		WmfFillRegion = 66088,
		// Token: 0x0400079D RID: 1949
		WmfFrameRegion = 66601,
		// Token: 0x0400079E RID: 1950
		WmfInvertRegion = 65834,
		// Token: 0x0400079F RID: 1951
		WmfPaintRegion,
		// Token: 0x040007A0 RID: 1952
		WmfSelectClipRegion,
		// Token: 0x040007A1 RID: 1953
		WmfSelectObject,
		// Token: 0x040007A2 RID: 1954
		WmfSetTextAlign,
		// Token: 0x040007A3 RID: 1955
		WmfChord = 67632,
		// Token: 0x040007A4 RID: 1956
		WmfSetMapperFlags = 66097,
		// Token: 0x040007A5 RID: 1957
		WmfExtTextOut = 68146,
		// Token: 0x040007A6 RID: 1958
		WmfSetDibToDev = 68915,
		// Token: 0x040007A7 RID: 1959
		WmfSelectPalette = 66100,
		// Token: 0x040007A8 RID: 1960
		WmfRealizePalette = 65589,
		// Token: 0x040007A9 RID: 1961
		WmfAnimatePalette = 66614,
		// Token: 0x040007AA RID: 1962
		WmfSetPalEntries = 65591,
		// Token: 0x040007AB RID: 1963
		WmfPolyPolygon = 66872,
		// Token: 0x040007AC RID: 1964
		WmfResizePalette = 65849,
		// Token: 0x040007AD RID: 1965
		WmfDibBitBlt = 67904,
		// Token: 0x040007AE RID: 1966
		WmfDibStretchBlt = 68417,
		// Token: 0x040007AF RID: 1967
		WmfDibCreatePatternBrush = 65858,
		// Token: 0x040007B0 RID: 1968
		WmfStretchDib = 69443,
		// Token: 0x040007B1 RID: 1969
		WmfExtFloodFill = 66888,
		// Token: 0x040007B2 RID: 1970
		WmfSetLayout = 65865,
		// Token: 0x040007B3 RID: 1971
		WmfDeleteObject = 66032,
		// Token: 0x040007B4 RID: 1972
		WmfCreatePalette = 65783,
		// Token: 0x040007B5 RID: 1973
		WmfCreatePatternBrush = 66041,
		// Token: 0x040007B6 RID: 1974
		WmfCreatePenIndirect = 66298,
		// Token: 0x040007B7 RID: 1975
		WmfCreateFontIndirect,
		// Token: 0x040007B8 RID: 1976
		WmfCreateBrushIndirect,
		// Token: 0x040007B9 RID: 1977
		WmfCreateRegion = 67327,
		// Token: 0x040007BA RID: 1978
		EmfHeader = 1,
		// Token: 0x040007BB RID: 1979
		EmfPolyBezier,
		// Token: 0x040007BC RID: 1980
		EmfPolygon,
		// Token: 0x040007BD RID: 1981
		EmfPolyline,
		// Token: 0x040007BE RID: 1982
		EmfPolyBezierTo,
		// Token: 0x040007BF RID: 1983
		EmfPolyLineTo,
		// Token: 0x040007C0 RID: 1984
		EmfPolyPolyline,
		// Token: 0x040007C1 RID: 1985
		EmfPolyPolygon,
		// Token: 0x040007C2 RID: 1986
		EmfSetWindowExtEx,
		// Token: 0x040007C3 RID: 1987
		EmfSetWindowOrgEx,
		// Token: 0x040007C4 RID: 1988
		EmfSetViewportExtEx,
		// Token: 0x040007C5 RID: 1989
		EmfSetViewportOrgEx,
		// Token: 0x040007C6 RID: 1990
		EmfSetBrushOrgEx,
		// Token: 0x040007C7 RID: 1991
		EmfEof,
		// Token: 0x040007C8 RID: 1992
		EmfSetPixelV,
		// Token: 0x040007C9 RID: 1993
		EmfSetMapperFlags,
		// Token: 0x040007CA RID: 1994
		EmfSetMapMode,
		// Token: 0x040007CB RID: 1995
		EmfSetBkMode,
		// Token: 0x040007CC RID: 1996
		EmfSetPolyFillMode,
		// Token: 0x040007CD RID: 1997
		EmfSetROP2,
		// Token: 0x040007CE RID: 1998
		EmfSetStretchBltMode,
		// Token: 0x040007CF RID: 1999
		EmfSetTextAlign,
		// Token: 0x040007D0 RID: 2000
		EmfSetColorAdjustment,
		// Token: 0x040007D1 RID: 2001
		EmfSetTextColor,
		// Token: 0x040007D2 RID: 2002
		EmfSetBkColor,
		// Token: 0x040007D3 RID: 2003
		EmfOffsetClipRgn,
		// Token: 0x040007D4 RID: 2004
		EmfMoveToEx,
		// Token: 0x040007D5 RID: 2005
		EmfSetMetaRgn,
		// Token: 0x040007D6 RID: 2006
		EmfExcludeClipRect,
		// Token: 0x040007D7 RID: 2007
		EmfIntersectClipRect,
		// Token: 0x040007D8 RID: 2008
		EmfScaleViewportExtEx,
		// Token: 0x040007D9 RID: 2009
		EmfScaleWindowExtEx,
		// Token: 0x040007DA RID: 2010
		EmfSaveDC,
		// Token: 0x040007DB RID: 2011
		EmfRestoreDC,
		// Token: 0x040007DC RID: 2012
		EmfSetWorldTransform,
		// Token: 0x040007DD RID: 2013
		EmfModifyWorldTransform,
		// Token: 0x040007DE RID: 2014
		EmfSelectObject,
		// Token: 0x040007DF RID: 2015
		EmfCreatePen,
		// Token: 0x040007E0 RID: 2016
		EmfCreateBrushIndirect,
		// Token: 0x040007E1 RID: 2017
		EmfDeleteObject,
		// Token: 0x040007E2 RID: 2018
		EmfAngleArc,
		// Token: 0x040007E3 RID: 2019
		EmfEllipse,
		// Token: 0x040007E4 RID: 2020
		EmfRectangle,
		// Token: 0x040007E5 RID: 2021
		EmfRoundRect,
		// Token: 0x040007E6 RID: 2022
		EmfRoundArc,
		// Token: 0x040007E7 RID: 2023
		EmfChord,
		// Token: 0x040007E8 RID: 2024
		EmfPie,
		// Token: 0x040007E9 RID: 2025
		EmfSelectPalette,
		// Token: 0x040007EA RID: 2026
		EmfCreatePalette,
		// Token: 0x040007EB RID: 2027
		EmfSetPaletteEntries,
		// Token: 0x040007EC RID: 2028
		EmfResizePalette,
		// Token: 0x040007ED RID: 2029
		EmfRealizePalette,
		// Token: 0x040007EE RID: 2030
		EmfExtFloodFill,
		// Token: 0x040007EF RID: 2031
		EmfLineTo,
		// Token: 0x040007F0 RID: 2032
		EmfArcTo,
		// Token: 0x040007F1 RID: 2033
		EmfPolyDraw,
		// Token: 0x040007F2 RID: 2034
		EmfSetArcDirection,
		// Token: 0x040007F3 RID: 2035
		EmfSetMiterLimit,
		// Token: 0x040007F4 RID: 2036
		EmfBeginPath,
		// Token: 0x040007F5 RID: 2037
		EmfEndPath,
		// Token: 0x040007F6 RID: 2038
		EmfCloseFigure,
		// Token: 0x040007F7 RID: 2039
		EmfFillPath,
		// Token: 0x040007F8 RID: 2040
		EmfStrokeAndFillPath,
		// Token: 0x040007F9 RID: 2041
		EmfStrokePath,
		// Token: 0x040007FA RID: 2042
		EmfFlattenPath,
		// Token: 0x040007FB RID: 2043
		EmfWidenPath,
		// Token: 0x040007FC RID: 2044
		EmfSelectClipPath,
		// Token: 0x040007FD RID: 2045
		EmfAbortPath,
		// Token: 0x040007FE RID: 2046
		EmfReserved069,
		// Token: 0x040007FF RID: 2047
		EmfGdiComment,
		// Token: 0x04000800 RID: 2048
		EmfFillRgn,
		// Token: 0x04000801 RID: 2049
		EmfFrameRgn,
		// Token: 0x04000802 RID: 2050
		EmfInvertRgn,
		// Token: 0x04000803 RID: 2051
		EmfPaintRgn,
		// Token: 0x04000804 RID: 2052
		EmfExtSelectClipRgn,
		// Token: 0x04000805 RID: 2053
		EmfBitBlt,
		// Token: 0x04000806 RID: 2054
		EmfStretchBlt,
		// Token: 0x04000807 RID: 2055
		EmfMaskBlt,
		// Token: 0x04000808 RID: 2056
		EmfPlgBlt,
		// Token: 0x04000809 RID: 2057
		EmfSetDIBitsToDevice,
		// Token: 0x0400080A RID: 2058
		EmfStretchDIBits,
		// Token: 0x0400080B RID: 2059
		EmfExtCreateFontIndirect,
		// Token: 0x0400080C RID: 2060
		EmfExtTextOutA,
		// Token: 0x0400080D RID: 2061
		EmfExtTextOutW,
		// Token: 0x0400080E RID: 2062
		EmfPolyBezier16,
		// Token: 0x0400080F RID: 2063
		EmfPolygon16,
		// Token: 0x04000810 RID: 2064
		EmfPolyline16,
		// Token: 0x04000811 RID: 2065
		EmfPolyBezierTo16,
		// Token: 0x04000812 RID: 2066
		EmfPolylineTo16,
		// Token: 0x04000813 RID: 2067
		EmfPolyPolyline16,
		// Token: 0x04000814 RID: 2068
		EmfPolyPolygon16,
		// Token: 0x04000815 RID: 2069
		EmfPolyDraw16,
		// Token: 0x04000816 RID: 2070
		EmfCreateMonoBrush,
		// Token: 0x04000817 RID: 2071
		EmfCreateDibPatternBrushPt,
		// Token: 0x04000818 RID: 2072
		EmfExtCreatePen,
		// Token: 0x04000819 RID: 2073
		EmfPolyTextOutA,
		// Token: 0x0400081A RID: 2074
		EmfPolyTextOutW,
		// Token: 0x0400081B RID: 2075
		EmfSetIcmMode,
		// Token: 0x0400081C RID: 2076
		EmfCreateColorSpace,
		// Token: 0x0400081D RID: 2077
		EmfSetColorSpace,
		// Token: 0x0400081E RID: 2078
		EmfDeleteColorSpace,
		// Token: 0x0400081F RID: 2079
		EmfGlsRecord,
		// Token: 0x04000820 RID: 2080
		EmfGlsBoundedRecord,
		// Token: 0x04000821 RID: 2081
		EmfPixelFormat,
		// Token: 0x04000822 RID: 2082
		EmfDrawEscape,
		// Token: 0x04000823 RID: 2083
		EmfExtEscape,
		// Token: 0x04000824 RID: 2084
		EmfStartDoc,
		// Token: 0x04000825 RID: 2085
		EmfSmallTextOut,
		// Token: 0x04000826 RID: 2086
		EmfForceUfiMapping,
		// Token: 0x04000827 RID: 2087
		EmfNamedEscpae,
		// Token: 0x04000828 RID: 2088
		EmfColorCorrectPalette,
		// Token: 0x04000829 RID: 2089
		EmfSetIcmProfileA,
		// Token: 0x0400082A RID: 2090
		EmfSetIcmProfileW,
		// Token: 0x0400082B RID: 2091
		EmfAlphaBlend,
		// Token: 0x0400082C RID: 2092
		EmfSetLayout,
		// Token: 0x0400082D RID: 2093
		EmfTransparentBlt,
		// Token: 0x0400082E RID: 2094
		EmfReserved117,
		// Token: 0x0400082F RID: 2095
		EmfGradientFill,
		// Token: 0x04000830 RID: 2096
		EmfSetLinkedUfis,
		// Token: 0x04000831 RID: 2097
		EmfSetTextJustification,
		// Token: 0x04000832 RID: 2098
		EmfColorMatchToTargetW,
		// Token: 0x04000833 RID: 2099
		EmfCreateColorSpaceW,
		// Token: 0x04000834 RID: 2100
		EmfMax = 122,
		// Token: 0x04000835 RID: 2101
		EmfMin = 1,
		// Token: 0x04000836 RID: 2102
		EmfPlusRecordBase = 16384,
		// Token: 0x04000837 RID: 2103
		Invalid = 16384,
		// Token: 0x04000838 RID: 2104
		Header,
		// Token: 0x04000839 RID: 2105
		EndOfFile,
		// Token: 0x0400083A RID: 2106
		Comment,
		// Token: 0x0400083B RID: 2107
		GetDC,
		// Token: 0x0400083C RID: 2108
		MultiFormatStart,
		// Token: 0x0400083D RID: 2109
		MultiFormatSection,
		// Token: 0x0400083E RID: 2110
		MultiFormatEnd,
		// Token: 0x0400083F RID: 2111
		Object,
		// Token: 0x04000840 RID: 2112
		Clear,
		// Token: 0x04000841 RID: 2113
		FillRects,
		// Token: 0x04000842 RID: 2114
		DrawRects,
		// Token: 0x04000843 RID: 2115
		FillPolygon,
		// Token: 0x04000844 RID: 2116
		DrawLines,
		// Token: 0x04000845 RID: 2117
		FillEllipse,
		// Token: 0x04000846 RID: 2118
		DrawEllipse,
		// Token: 0x04000847 RID: 2119
		FillPie,
		// Token: 0x04000848 RID: 2120
		DrawPie,
		// Token: 0x04000849 RID: 2121
		DrawArc,
		// Token: 0x0400084A RID: 2122
		FillRegion,
		// Token: 0x0400084B RID: 2123
		FillPath,
		// Token: 0x0400084C RID: 2124
		DrawPath,
		// Token: 0x0400084D RID: 2125
		FillClosedCurve,
		// Token: 0x0400084E RID: 2126
		DrawClosedCurve,
		// Token: 0x0400084F RID: 2127
		DrawCurve,
		// Token: 0x04000850 RID: 2128
		DrawBeziers,
		// Token: 0x04000851 RID: 2129
		DrawImage,
		// Token: 0x04000852 RID: 2130
		DrawImagePoints,
		// Token: 0x04000853 RID: 2131
		DrawString,
		// Token: 0x04000854 RID: 2132
		SetRenderingOrigin,
		// Token: 0x04000855 RID: 2133
		SetAntiAliasMode,
		// Token: 0x04000856 RID: 2134
		SetTextRenderingHint,
		// Token: 0x04000857 RID: 2135
		SetTextContrast,
		// Token: 0x04000858 RID: 2136
		SetInterpolationMode,
		// Token: 0x04000859 RID: 2137
		SetPixelOffsetMode,
		// Token: 0x0400085A RID: 2138
		SetCompositingMode,
		// Token: 0x0400085B RID: 2139
		SetCompositingQuality,
		// Token: 0x0400085C RID: 2140
		Save,
		// Token: 0x0400085D RID: 2141
		Restore,
		// Token: 0x0400085E RID: 2142
		BeginContainer,
		// Token: 0x0400085F RID: 2143
		BeginContainerNoParams,
		// Token: 0x04000860 RID: 2144
		EndContainer,
		// Token: 0x04000861 RID: 2145
		SetWorldTransform,
		// Token: 0x04000862 RID: 2146
		ResetWorldTransform,
		// Token: 0x04000863 RID: 2147
		MultiplyWorldTransform,
		// Token: 0x04000864 RID: 2148
		TranslateWorldTransform,
		// Token: 0x04000865 RID: 2149
		ScaleWorldTransform,
		// Token: 0x04000866 RID: 2150
		RotateWorldTransform,
		// Token: 0x04000867 RID: 2151
		SetPageTransform,
		// Token: 0x04000868 RID: 2152
		ResetClip,
		// Token: 0x04000869 RID: 2153
		SetClipRect,
		// Token: 0x0400086A RID: 2154
		SetClipPath,
		// Token: 0x0400086B RID: 2155
		SetClipRegion,
		// Token: 0x0400086C RID: 2156
		OffsetClip,
		// Token: 0x0400086D RID: 2157
		DrawDriverString,
		// Token: 0x0400086E RID: 2158
		Total,
		// Token: 0x0400086F RID: 2159
		Max = 16438,
		// Token: 0x04000870 RID: 2160
		Min = 16385
	}
}
