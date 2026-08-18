using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000F8 RID: 248
	internal sealed class PasswordRecoveryAutoFormat : BaseAutoFormat<PasswordRecovery>
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x000322D4 File Offset: 0x000304D4
		public PasswordRecoveryAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 500;
			base.Style.Height = 300;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00032328 File Offset: 0x00030528
		protected override void Apply(PasswordRecovery passwordRecovery)
		{
			passwordRecovery.BackColor = ColorTranslator.FromHtml(this.backColor);
			passwordRecovery.BorderColor = ColorTranslator.FromHtml(this.borderColor);
			passwordRecovery.BorderWidth = new Unit(this.borderWidth, CultureInfo.InvariantCulture);
			if (this.borderStyle >= 0 && this.borderStyle <= 9)
			{
				passwordRecovery.BorderStyle = (BorderStyle)this.borderStyle;
			}
			else
			{
				passwordRecovery.BorderStyle = BorderStyle.NotSet;
			}
			passwordRecovery.Font.Size = new FontUnit(this.fontSize, CultureInfo.InvariantCulture);
			passwordRecovery.Font.Name = this.fontName;
			passwordRecovery.Font.ClearDefaults();
			passwordRecovery.TitleTextStyle.BackColor = ColorTranslator.FromHtml(this.titleTextBackColor);
			passwordRecovery.TitleTextStyle.ForeColor = ColorTranslator.FromHtml(this.titleTextForeColor);
			passwordRecovery.TitleTextStyle.Font.Bold = ((this.titleTextFont & 1) != 0);
			passwordRecovery.TitleTextStyle.Font.Size = new FontUnit(this.titleTextFontSize, CultureInfo.InvariantCulture);
			passwordRecovery.TitleTextStyle.Font.ClearDefaults();
			passwordRecovery.BorderPadding = this.borderPadding;
			passwordRecovery.InstructionTextStyle.ForeColor = ColorTranslator.FromHtml(this.instructionTextForeColor);
			passwordRecovery.InstructionTextStyle.Font.Italic = ((this.instructionTextFont & 2) != 0);
			passwordRecovery.InstructionTextStyle.Font.ClearDefaults();
			passwordRecovery.TextBoxStyle.Font.Size = new FontUnit(this.textboxFontSize, CultureInfo.InvariantCulture);
			passwordRecovery.TextBoxStyle.Font.ClearDefaults();
			passwordRecovery.SubmitButtonStyle.BackColor = ColorTranslator.FromHtml(this.submitButtonBackColor);
			passwordRecovery.SubmitButtonStyle.ForeColor = ColorTranslator.FromHtml(this.submitButtonForeColor);
			passwordRecovery.SubmitButtonStyle.Font.Size = new FontUnit(this.submitButtonFontSize, CultureInfo.InvariantCulture);
			passwordRecovery.SubmitButtonStyle.Font.Name = this.submitButtonFontName;
			passwordRecovery.SubmitButtonStyle.BorderColor = ColorTranslator.FromHtml(this.submitButtonBorderColor);
			passwordRecovery.SubmitButtonStyle.BorderWidth = new Unit(this.submitButtonBorderWidth, CultureInfo.InvariantCulture);
			if (this.submitButtonBorderStyle >= 0 && this.submitButtonBorderStyle <= 9)
			{
				passwordRecovery.SubmitButtonStyle.BorderStyle = (BorderStyle)this.submitButtonBorderStyle;
			}
			else
			{
				passwordRecovery.SubmitButtonStyle.BorderStyle = BorderStyle.NotSet;
			}
			passwordRecovery.SubmitButtonStyle.Font.ClearDefaults();
			passwordRecovery.SuccessTextStyle.ForeColor = ColorTranslator.FromHtml(this.successTextForeColor);
			passwordRecovery.SuccessTextStyle.Font.Bold = ((this.successTextFont & 1) != 0);
			passwordRecovery.SuccessTextStyle.Font.ClearDefaults();
			passwordRecovery.RenderOuterTable = bool.Parse(this.renderOuterTable);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000325E8 File Offset: 0x000307E8
		protected override void Initialize(DataRow schemeData)
		{
			this.backColor = BaseAutoFormat<PasswordRecovery>.GetStringProperty("BackColor", schemeData);
			this.borderColor = BaseAutoFormat<PasswordRecovery>.GetStringProperty("BorderColor", schemeData);
			this.borderWidth = BaseAutoFormat<PasswordRecovery>.GetStringProperty("BorderWidth", schemeData);
			this.borderStyle = BaseAutoFormat<PasswordRecovery>.GetIntProperty("BorderStyle", -1, schemeData);
			this.fontSize = BaseAutoFormat<PasswordRecovery>.GetStringProperty("FontSize", schemeData);
			this.fontName = BaseAutoFormat<PasswordRecovery>.GetStringProperty("FontName", schemeData);
			this.titleTextBackColor = BaseAutoFormat<PasswordRecovery>.GetStringProperty("TitleTextBackColor", schemeData);
			this.titleTextForeColor = BaseAutoFormat<PasswordRecovery>.GetStringProperty("TitleTextForeColor", schemeData);
			this.titleTextFont = BaseAutoFormat<PasswordRecovery>.GetIntProperty("TitleTextFont", schemeData);
			this.titleTextFontSize = BaseAutoFormat<PasswordRecovery>.GetStringProperty("TitleTextFontSize", schemeData);
			this.instructionTextForeColor = BaseAutoFormat<PasswordRecovery>.GetStringProperty("InstructionTextForeColor", schemeData);
			this.instructionTextFont = BaseAutoFormat<PasswordRecovery>.GetIntProperty("InstructionTextFont", schemeData);
			this.borderPadding = BaseAutoFormat<PasswordRecovery>.GetIntProperty("BorderPadding", 1, schemeData);
			this.textboxFontSize = BaseAutoFormat<PasswordRecovery>.GetStringProperty("TextboxFontSize", schemeData);
			this.submitButtonBackColor = BaseAutoFormat<PasswordRecovery>.GetStringProperty("SubmitButtonBackColor", schemeData);
			this.submitButtonForeColor = BaseAutoFormat<PasswordRecovery>.GetStringProperty("SubmitButtonForeColor", schemeData);
			this.submitButtonFontSize = BaseAutoFormat<PasswordRecovery>.GetStringProperty("SubmitButtonFontSize", schemeData);
			this.submitButtonFontName = BaseAutoFormat<PasswordRecovery>.GetStringProperty("SubmitButtonFontName", schemeData);
			this.submitButtonBorderColor = BaseAutoFormat<PasswordRecovery>.GetStringProperty("SubmitButtonBorderColor", schemeData);
			this.submitButtonBorderWidth = BaseAutoFormat<PasswordRecovery>.GetStringProperty("SubmitButtonBorderWidth", schemeData);
			this.submitButtonBorderStyle = BaseAutoFormat<PasswordRecovery>.GetIntProperty("SubmitButtonBorderStyle", -1, schemeData);
			this.successTextForeColor = BaseAutoFormat<PasswordRecovery>.GetStringProperty("SuccessTextForeColor", schemeData);
			this.successTextFont = BaseAutoFormat<PasswordRecovery>.GetIntProperty("SuccessTextFont", schemeData);
			this.renderOuterTable = BaseAutoFormat<PasswordRecovery>.GetStringProperty("RenderOuterTable", schemeData);
		}

		// Token: 0x04000515 RID: 1301
		private string backColor;

		// Token: 0x04000516 RID: 1302
		private string borderColor;

		// Token: 0x04000517 RID: 1303
		private string borderWidth;

		// Token: 0x04000518 RID: 1304
		private int borderStyle = -1;

		// Token: 0x04000519 RID: 1305
		private string fontSize;

		// Token: 0x0400051A RID: 1306
		private string fontName;

		// Token: 0x0400051B RID: 1307
		private string titleTextBackColor;

		// Token: 0x0400051C RID: 1308
		private string titleTextForeColor;

		// Token: 0x0400051D RID: 1309
		private int titleTextFont;

		// Token: 0x0400051E RID: 1310
		private string titleTextFontSize;

		// Token: 0x0400051F RID: 1311
		private int borderPadding = 1;

		// Token: 0x04000520 RID: 1312
		private string instructionTextForeColor;

		// Token: 0x04000521 RID: 1313
		private int instructionTextFont;

		// Token: 0x04000522 RID: 1314
		private string textboxFontSize;

		// Token: 0x04000523 RID: 1315
		private string submitButtonBackColor;

		// Token: 0x04000524 RID: 1316
		private string submitButtonForeColor;

		// Token: 0x04000525 RID: 1317
		private string submitButtonFontSize;

		// Token: 0x04000526 RID: 1318
		private string submitButtonFontName;

		// Token: 0x04000527 RID: 1319
		private string submitButtonBorderColor;

		// Token: 0x04000528 RID: 1320
		private string submitButtonBorderWidth;

		// Token: 0x04000529 RID: 1321
		private int submitButtonBorderStyle = -1;

		// Token: 0x0400052A RID: 1322
		private string successTextForeColor;

		// Token: 0x0400052B RID: 1323
		private int successTextFont;

		// Token: 0x0400052C RID: 1324
		private string renderOuterTable;

		// Token: 0x0400052D RID: 1325
		private const int FONT_BOLD = 1;

		// Token: 0x0400052E RID: 1326
		private const int FONT_ITALIC = 2;
	}
}
