using System;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x0200038B RID: 907
	public interface IChartErrorBars
	{
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06003760 RID: 14176
		IChartBorder Border { get; }

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06003761 RID: 14177
		// (set) Token: 0x06003762 RID: 14178
		ErrorBarIncludeType Include { get; set; }

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06003763 RID: 14179
		// (set) Token: 0x06003764 RID: 14180
		bool HasCap { get; set; }

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06003765 RID: 14181
		// (set) Token: 0x06003766 RID: 14182
		ErrorBarType Type { get; set; }

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06003767 RID: 14183
		// (set) Token: 0x06003768 RID: 14184
		double NumberValue { get; set; }

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06003769 RID: 14185
		// (set) Token: 0x0600376A RID: 14186
		IXLSRange PlusRange { get; set; }

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x0600376B RID: 14187
		// (set) Token: 0x0600376C RID: 14188
		IXLSRange MinusRange { get; set; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x0600376D RID: 14189
		IShadow Shadow { get; }

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600376E RID: 14190
		IFormat3D Chart3DOptions { get; }

		// Token: 0x0600376F RID: 14191
		void ClearFormats();

		// Token: 0x06003770 RID: 14192
		void Delete();
	}
}
