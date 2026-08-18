using System;
using System.Drawing;

namespace Spire.Xls.Core.Interfaces
{
	// Token: 0x0200000F RID: 15
	public interface IShadow
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000D8 RID: 216
		// (set) Token: 0x060000D9 RID: 217
		XLSXChartShadowOuterType ShadowOuterType { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000DA RID: 218
		// (set) Token: 0x060000DB RID: 219
		XLSXChartShadowInnerType ShadowInnerType { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000DC RID: 220
		// (set) Token: 0x060000DD RID: 221
		XLSXChartPrespectiveType ShadowPrespectiveType { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000DE RID: 222
		// (set) Token: 0x060000DF RID: 223
		bool HasCustomStyle { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000E0 RID: 224
		// (set) Token: 0x060000E1 RID: 225
		int Transparency { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000E2 RID: 226
		// (set) Token: 0x060000E3 RID: 227
		int Size { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000E4 RID: 228
		// (set) Token: 0x060000E5 RID: 229
		int Blur { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000E6 RID: 230
		// (set) Token: 0x060000E7 RID: 231
		int Angle { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000E8 RID: 232
		// (set) Token: 0x060000E9 RID: 233
		int Distance { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000EA RID: 234
		// (set) Token: 0x060000EB RID: 235
		Color Color { get; set; }

		// Token: 0x060000EC RID: 236
		void CustomShadowStyles(XLSXChartShadowOuterType iOuter, int iTransparency, int iSize, int iBlur, int iAngle, int iDistance, bool iCustomShadowStyle);

		// Token: 0x060000ED RID: 237
		void CustomShadowStyles(XLSXChartShadowInnerType iInner, int iTransparency, int iBlur, int iAngle, int iDistance, bool iCustomShadowStyle);

		// Token: 0x060000EE RID: 238
		void CustomShadowStyles(XLSXChartPrespectiveType iPerspective, int iTransparency, int iSize, int iBlur, int iAngle, int iDistance, bool iCustomShadowStyle);
	}
}
