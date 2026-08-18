using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x02000409 RID: 1033
	public interface IShapeFill
	{
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06003E22 RID: 15906
		// (set) Token: 0x06003E23 RID: 15907
		ShapeFillType FillType { get; set; }

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06003E24 RID: 15908
		// (set) Token: 0x06003E25 RID: 15909
		GradientStyleType GradientStyle { get; set; }

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003E26 RID: 15910
		// (set) Token: 0x06003E27 RID: 15911
		GradientVariantsType GradientVariant { get; set; }

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003E28 RID: 15912
		// (set) Token: 0x06003E29 RID: 15913
		double TransparencyTo { get; set; }

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06003E2A RID: 15914
		// (set) Token: 0x06003E2B RID: 15915
		double TransparencyFrom { get; set; }

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003E2C RID: 15916
		// (set) Token: 0x06003E2D RID: 15917
		GradientColorType GradientColorType { get; set; }

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06003E2E RID: 15918
		// (set) Token: 0x06003E2F RID: 15919
		GradientPatternType Pattern { get; set; }

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06003E30 RID: 15920
		// (set) Token: 0x06003E31 RID: 15921
		GradientTextureType Texture { get; set; }

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06003E32 RID: 15922
		// (set) Token: 0x06003E33 RID: 15923
		ExcelColors BackKnownColor { get; set; }

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003E34 RID: 15924
		// (set) Token: 0x06003E35 RID: 15925
		ExcelColors ForeKnownColor { get; set; }

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003E36 RID: 15926
		// (set) Token: 0x06003E37 RID: 15927
		Color BackColor { get; set; }

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003E38 RID: 15928
		// (set) Token: 0x06003E39 RID: 15929
		Color ForeColor { get; set; }

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003E3A RID: 15930
		// (set) Token: 0x06003E3B RID: 15931
		GradientPresetType PresetGradientType { get; set; }

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003E3C RID: 15932
		Image Picture { get; }

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003E3D RID: 15933
		string PictureName { get; }

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003E3E RID: 15934
		// (set) Token: 0x06003E3F RID: 15935
		bool Visible { get; set; }

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06003E40 RID: 15936
		// (set) Token: 0x06003E41 RID: 15937
		double GradientDegree { get; set; }

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003E42 RID: 15938
		// (set) Token: 0x06003E43 RID: 15939
		double Transparency { get; set; }

		// Token: 0x06003E44 RID: 15940
		void CustomPicture(string path);

		// Token: 0x06003E45 RID: 15941
		void CustomPicture(Image im, string name);

		// Token: 0x06003E46 RID: 15942
		void CustomTexture(string path);

		// Token: 0x06003E47 RID: 15943
		void CustomTexture(Image im, string name);

		// Token: 0x06003E48 RID: 15944
		void Patterned(GradientPatternType pattern);

		// Token: 0x06003E49 RID: 15945
		void PresetGradient(GradientPresetType grad);

		// Token: 0x06003E4A RID: 15946
		void PresetGradient(GradientPresetType grad, GradientStyleType shadStyle);

		// Token: 0x06003E4B RID: 15947
		void PresetGradient(GradientPresetType grad, GradientStyleType shadStyle, GradientVariantsType shadVar);

		// Token: 0x06003E4C RID: 15948
		void PresetTextured(GradientTextureType texture);

		// Token: 0x06003E4D RID: 15949
		void TwoColorGradient();

		// Token: 0x06003E4E RID: 15950
		void TwoColorGradient(GradientStyleType style);

		// Token: 0x06003E4F RID: 15951
		void TwoColorGradient(GradientStyleType style, GradientVariantsType variant);

		// Token: 0x06003E50 RID: 15952
		void OneColorGradient();

		// Token: 0x06003E51 RID: 15953
		void OneColorGradient(GradientStyleType style);

		// Token: 0x06003E52 RID: 15954
		void OneColorGradient(GradientStyleType style, GradientVariantsType variant);

		// Token: 0x06003E53 RID: 15955
		void Solid();
	}
}
