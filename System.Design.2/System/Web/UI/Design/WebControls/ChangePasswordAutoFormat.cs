using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000A6 RID: 166
	internal sealed class ChangePasswordAutoFormat : BaseAutoFormat<ChangePassword>
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x000180BC File Offset: 0x000162BC
		public ChangePasswordAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 400;
			base.Style.Height = 250;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00018110 File Offset: 0x00016310
		protected override void Apply(ChangePassword changePassword)
		{
			changePassword.BackColor = ColorTranslator.FromHtml(this._backColor);
			changePassword.BorderColor = ColorTranslator.FromHtml(this._borderColor);
			changePassword.BorderWidth = new Unit(this._borderWidth, CultureInfo.InvariantCulture);
			if (this._borderStyle >= 0 && this._borderStyle <= 9)
			{
				changePassword.BorderStyle = (BorderStyle)this._borderStyle;
			}
			else
			{
				changePassword.BorderStyle = BorderStyle.NotSet;
			}
			changePassword.Font.Size = new FontUnit(this._fontSize, CultureInfo.InvariantCulture);
			changePassword.Font.Name = this._fontName;
			changePassword.Font.ClearDefaults();
			changePassword.TitleTextStyle.BackColor = ColorTranslator.FromHtml(this._titleTextBackColor);
			changePassword.TitleTextStyle.ForeColor = ColorTranslator.FromHtml(this._titleTextForeColor);
			changePassword.TitleTextStyle.Font.Bold = ((this._titleTextFont & 1) != 0);
			changePassword.TitleTextStyle.Font.Size = new FontUnit(this._titleTextFontSize, CultureInfo.InvariantCulture);
			changePassword.TitleTextStyle.Font.ClearDefaults();
			changePassword.BorderPadding = this._borderPadding;
			changePassword.InstructionTextStyle.ForeColor = ColorTranslator.FromHtml(this._instructionTextForeColor);
			changePassword.InstructionTextStyle.Font.Italic = ((this._instructionTextFont & 2) != 0);
			changePassword.InstructionTextStyle.Font.ClearDefaults();
			changePassword.TextBoxStyle.Font.Size = new FontUnit(this._textboxFontSize, CultureInfo.InvariantCulture);
			changePassword.TextBoxStyle.Font.ClearDefaults();
			changePassword.ChangePasswordButtonStyle.BackColor = ColorTranslator.FromHtml(this._buttonBackColor);
			changePassword.ChangePasswordButtonStyle.ForeColor = ColorTranslator.FromHtml(this._buttonForeColor);
			changePassword.ChangePasswordButtonStyle.Font.Size = new FontUnit(this._buttonFontSize, CultureInfo.InvariantCulture);
			changePassword.ChangePasswordButtonStyle.Font.Name = this._buttonFontName;
			changePassword.ChangePasswordButtonStyle.BorderColor = ColorTranslator.FromHtml(this._buttonBorderColor);
			changePassword.ChangePasswordButtonStyle.BorderWidth = new Unit(this._buttonBorderWidth, CultureInfo.InvariantCulture);
			if (this._buttonBorderStyle >= 0 && this._buttonBorderStyle <= 9)
			{
				changePassword.ChangePasswordButtonStyle.BorderStyle = (BorderStyle)this._buttonBorderStyle;
			}
			else
			{
				changePassword.ChangePasswordButtonStyle.BorderStyle = BorderStyle.NotSet;
			}
			changePassword.ChangePasswordButtonStyle.Font.ClearDefaults();
			changePassword.ContinueButtonStyle.BackColor = ColorTranslator.FromHtml(this._buttonBackColor);
			changePassword.ContinueButtonStyle.ForeColor = ColorTranslator.FromHtml(this._buttonForeColor);
			changePassword.ContinueButtonStyle.Font.Size = new FontUnit(this._buttonFontSize, CultureInfo.InvariantCulture);
			changePassword.ContinueButtonStyle.Font.Name = this._buttonFontName;
			changePassword.ContinueButtonStyle.BorderColor = ColorTranslator.FromHtml(this._buttonBorderColor);
			changePassword.ContinueButtonStyle.BorderWidth = new Unit(this._buttonBorderWidth, CultureInfo.InvariantCulture);
			if (this._buttonBorderStyle >= 0 && this._buttonBorderStyle <= 9)
			{
				changePassword.ContinueButtonStyle.BorderStyle = (BorderStyle)this._buttonBorderStyle;
			}
			else
			{
				changePassword.ContinueButtonStyle.BorderStyle = BorderStyle.NotSet;
			}
			changePassword.ContinueButtonStyle.Font.ClearDefaults();
			changePassword.CancelButtonStyle.BackColor = ColorTranslator.FromHtml(this._buttonBackColor);
			changePassword.CancelButtonStyle.ForeColor = ColorTranslator.FromHtml(this._buttonForeColor);
			changePassword.CancelButtonStyle.Font.Size = new FontUnit(this._buttonFontSize, CultureInfo.InvariantCulture);
			changePassword.CancelButtonStyle.Font.Name = this._buttonFontName;
			changePassword.CancelButtonStyle.BorderColor = ColorTranslator.FromHtml(this._buttonBorderColor);
			changePassword.CancelButtonStyle.BorderWidth = new Unit(this._buttonBorderWidth, CultureInfo.InvariantCulture);
			if (this._buttonBorderStyle >= 0 && this._buttonBorderStyle <= 9)
			{
				changePassword.CancelButtonStyle.BorderStyle = (BorderStyle)this._buttonBorderStyle;
			}
			else
			{
				changePassword.CancelButtonStyle.BorderStyle = BorderStyle.NotSet;
			}
			changePassword.CancelButtonStyle.Font.ClearDefaults();
			changePassword.PasswordHintStyle.ForeColor = ColorTranslator.FromHtml(this._passwordHintForeColor);
			changePassword.PasswordHintStyle.Font.Italic = ((this._passwordHintFont & 2) != 0);
			changePassword.PasswordHintStyle.Font.ClearDefaults();
			changePassword.RenderOuterTable = bool.Parse(this._renderOuterTable);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001857C File Offset: 0x0001677C
		protected override void Initialize(DataRow schemeData)
		{
			this._backColor = BaseAutoFormat<ChangePassword>.GetStringProperty("BackColor", schemeData);
			this._borderColor = BaseAutoFormat<ChangePassword>.GetStringProperty("BorderColor", schemeData);
			this._borderWidth = BaseAutoFormat<ChangePassword>.GetStringProperty("BorderWidth", schemeData);
			this._borderStyle = BaseAutoFormat<ChangePassword>.GetIntProperty("BorderStyle", -1, schemeData);
			this._fontSize = BaseAutoFormat<ChangePassword>.GetStringProperty("FontSize", schemeData);
			this._fontName = BaseAutoFormat<ChangePassword>.GetStringProperty("FontName", schemeData);
			this._titleTextBackColor = BaseAutoFormat<ChangePassword>.GetStringProperty("TitleTextBackColor", schemeData);
			this._titleTextForeColor = BaseAutoFormat<ChangePassword>.GetStringProperty("TitleTextForeColor", schemeData);
			this._titleTextFont = BaseAutoFormat<ChangePassword>.GetIntProperty("TitleTextFont", schemeData);
			this._titleTextFontSize = BaseAutoFormat<ChangePassword>.GetStringProperty("TitleTextFontSize", schemeData);
			this._instructionTextForeColor = BaseAutoFormat<ChangePassword>.GetStringProperty("InstructionTextForeColor", schemeData);
			this._instructionTextFont = BaseAutoFormat<ChangePassword>.GetIntProperty("InstructionTextFont", schemeData);
			this._borderPadding = BaseAutoFormat<ChangePassword>.GetIntProperty("BorderPadding", 1, schemeData);
			this._textboxFontSize = BaseAutoFormat<ChangePassword>.GetStringProperty("TextboxFontSize", schemeData);
			this._buttonBackColor = BaseAutoFormat<ChangePassword>.GetStringProperty("ButtonBackColor", schemeData);
			this._buttonForeColor = BaseAutoFormat<ChangePassword>.GetStringProperty("ButtonForeColor", schemeData);
			this._buttonFontSize = BaseAutoFormat<ChangePassword>.GetStringProperty("ButtonFontSize", schemeData);
			this._buttonFontName = BaseAutoFormat<ChangePassword>.GetStringProperty("ButtonFontName", schemeData);
			this._buttonBorderColor = BaseAutoFormat<ChangePassword>.GetStringProperty("ButtonBorderColor", schemeData);
			this._buttonBorderWidth = BaseAutoFormat<ChangePassword>.GetStringProperty("ButtonBorderWidth", schemeData);
			this._buttonBorderStyle = BaseAutoFormat<ChangePassword>.GetIntProperty("ButtonBorderStyle", -1, schemeData);
			this._passwordHintForeColor = BaseAutoFormat<ChangePassword>.GetStringProperty("PasswordHintForeColor", schemeData);
			this._passwordHintFont = BaseAutoFormat<ChangePassword>.GetIntProperty("PasswordHintFont", schemeData);
			this._renderOuterTable = BaseAutoFormat<ChangePassword>.GetStringProperty("RenderOuterTable", schemeData);
		}

		// Token: 0x04000264 RID: 612
		private string _backColor;

		// Token: 0x04000265 RID: 613
		private string _borderColor;

		// Token: 0x04000266 RID: 614
		private string _borderWidth;

		// Token: 0x04000267 RID: 615
		private int _borderStyle = -1;

		// Token: 0x04000268 RID: 616
		private string _fontSize;

		// Token: 0x04000269 RID: 617
		private string _fontName;

		// Token: 0x0400026A RID: 618
		private string _titleTextBackColor;

		// Token: 0x0400026B RID: 619
		private string _titleTextForeColor;

		// Token: 0x0400026C RID: 620
		private int _titleTextFont;

		// Token: 0x0400026D RID: 621
		private string _titleTextFontSize;

		// Token: 0x0400026E RID: 622
		private int _borderPadding = 1;

		// Token: 0x0400026F RID: 623
		private string _instructionTextForeColor;

		// Token: 0x04000270 RID: 624
		private int _instructionTextFont;

		// Token: 0x04000271 RID: 625
		private string _textboxFontSize;

		// Token: 0x04000272 RID: 626
		private string _buttonBackColor;

		// Token: 0x04000273 RID: 627
		private string _buttonForeColor;

		// Token: 0x04000274 RID: 628
		private string _buttonFontSize;

		// Token: 0x04000275 RID: 629
		private string _buttonFontName;

		// Token: 0x04000276 RID: 630
		private string _buttonBorderColor;

		// Token: 0x04000277 RID: 631
		private string _buttonBorderWidth;

		// Token: 0x04000278 RID: 632
		private int _buttonBorderStyle = -1;

		// Token: 0x04000279 RID: 633
		private string _passwordHintForeColor;

		// Token: 0x0400027A RID: 634
		private int _passwordHintFont;

		// Token: 0x0400027B RID: 635
		private string _renderOuterTable;

		// Token: 0x0400027C RID: 636
		private const int FONT_BOLD = 1;

		// Token: 0x0400027D RID: 637
		private const int FONT_ITALIC = 2;
	}
}
