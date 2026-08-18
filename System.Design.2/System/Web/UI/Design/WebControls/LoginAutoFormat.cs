using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000DD RID: 221
	internal sealed class LoginAutoFormat : BaseAutoFormat<Login>
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x00028D2C File Offset: 0x00026F2C
		public LoginAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 300;
			base.Style.Height = 200;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00028D7C File Offset: 0x00026F7C
		protected override void Apply(Login login)
		{
			login.BackColor = ColorTranslator.FromHtml(this.backColor);
			login.ForeColor = ColorTranslator.FromHtml(this.foreColor);
			login.BorderColor = ColorTranslator.FromHtml(this.borderColor);
			login.BorderWidth = new Unit(this.borderWidth, CultureInfo.InvariantCulture);
			if (this.borderStyle >= 0 && this.borderStyle <= 9)
			{
				login.BorderStyle = (BorderStyle)this.borderStyle;
			}
			else
			{
				login.BorderStyle = BorderStyle.NotSet;
			}
			login.Font.Size = new FontUnit(this.fontSize, CultureInfo.InvariantCulture);
			login.Font.Name = this.fontName;
			login.Font.ClearDefaults();
			login.TitleTextStyle.BackColor = ColorTranslator.FromHtml(this.titleTextBackColor);
			login.TitleTextStyle.ForeColor = ColorTranslator.FromHtml(this.titleTextForeColor);
			login.TitleTextStyle.Font.Bold = ((this.titleTextFont & 1) != 0);
			login.TitleTextStyle.Font.Size = new FontUnit(this.titleTextFontSize, CultureInfo.InvariantCulture);
			login.TitleTextStyle.Font.ClearDefaults();
			login.BorderPadding = this.borderPadding;
			if (this.textLayout > 0)
			{
				login.TextLayout = LoginTextLayout.TextOnTop;
			}
			else
			{
				login.TextLayout = LoginTextLayout.TextOnLeft;
			}
			login.InstructionTextStyle.ForeColor = ColorTranslator.FromHtml(this.instructionTextForeColor);
			login.InstructionTextStyle.Font.Italic = ((this.instructionTextFont & 2) != 0);
			login.InstructionTextStyle.Font.ClearDefaults();
			login.TextBoxStyle.Font.Size = new FontUnit(this.textboxFontSize, CultureInfo.InvariantCulture);
			login.TextBoxStyle.Font.ClearDefaults();
			login.LoginButtonStyle.BackColor = ColorTranslator.FromHtml(this._loginButtonBackColor);
			login.LoginButtonStyle.ForeColor = ColorTranslator.FromHtml(this._loginButtonForeColor);
			login.LoginButtonStyle.Font.Size = new FontUnit(this._loginButtonFontSize, CultureInfo.InvariantCulture);
			login.LoginButtonStyle.Font.Name = this._loginButtonFontName;
			login.LoginButtonStyle.BorderColor = ColorTranslator.FromHtml(this._loginButtonBorderColor);
			login.LoginButtonStyle.BorderWidth = new Unit(this._loginButtonBorderWidth, CultureInfo.InvariantCulture);
			if (this._loginButtonBorderStyle >= 0 && this._loginButtonBorderStyle <= 9)
			{
				login.LoginButtonStyle.BorderStyle = (BorderStyle)this._loginButtonBorderStyle;
			}
			else
			{
				login.LoginButtonStyle.BorderStyle = BorderStyle.NotSet;
			}
			login.LoginButtonStyle.Font.ClearDefaults();
			login.RenderOuterTable = bool.Parse(this._renderOuterTable);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00029024 File Offset: 0x00027224
		protected override void Initialize(DataRow schemeData)
		{
			this.backColor = BaseAutoFormat<Login>.GetStringProperty("BackColor", schemeData);
			this.foreColor = BaseAutoFormat<Login>.GetStringProperty("ForeColor", schemeData);
			this.borderColor = BaseAutoFormat<Login>.GetStringProperty("BorderColor", schemeData);
			this.borderWidth = BaseAutoFormat<Login>.GetStringProperty("BorderWidth", schemeData);
			this.borderStyle = BaseAutoFormat<Login>.GetIntProperty("BorderStyle", -1, schemeData);
			this.fontSize = BaseAutoFormat<Login>.GetStringProperty("FontSize", schemeData);
			this.fontName = BaseAutoFormat<Login>.GetStringProperty("FontName", schemeData);
			this.instructionTextForeColor = BaseAutoFormat<Login>.GetStringProperty("InstructionTextForeColor", schemeData);
			this.instructionTextFont = BaseAutoFormat<Login>.GetIntProperty("InstructionTextFont", schemeData);
			this.titleTextBackColor = BaseAutoFormat<Login>.GetStringProperty("TitleTextBackColor", schemeData);
			this.titleTextForeColor = BaseAutoFormat<Login>.GetStringProperty("TitleTextForeColor", schemeData);
			this.titleTextFont = BaseAutoFormat<Login>.GetIntProperty("TitleTextFont", schemeData);
			this.titleTextFontSize = BaseAutoFormat<Login>.GetStringProperty("TitleTextFontSize", schemeData);
			this.borderPadding = BaseAutoFormat<Login>.GetIntProperty("BorderPadding", 1, schemeData);
			this.textLayout = BaseAutoFormat<Login>.GetIntProperty("TextLayout", schemeData);
			this.textboxFontSize = BaseAutoFormat<Login>.GetStringProperty("TextboxFontSize", schemeData);
			this._loginButtonBackColor = BaseAutoFormat<Login>.GetStringProperty("SubmitButtonBackColor", schemeData);
			this._loginButtonForeColor = BaseAutoFormat<Login>.GetStringProperty("SubmitButtonForeColor", schemeData);
			this._loginButtonFontSize = BaseAutoFormat<Login>.GetStringProperty("SubmitButtonFontSize", schemeData);
			this._loginButtonFontName = BaseAutoFormat<Login>.GetStringProperty("SubmitButtonFontName", schemeData);
			this._loginButtonBorderColor = BaseAutoFormat<Login>.GetStringProperty("SubmitButtonBorderColor", schemeData);
			this._loginButtonBorderWidth = BaseAutoFormat<Login>.GetStringProperty("SubmitButtonBorderWidth", schemeData);
			this._loginButtonBorderStyle = BaseAutoFormat<Login>.GetIntProperty("SubmitButtonBorderStyle", -1, schemeData);
			this._renderOuterTable = BaseAutoFormat<Login>.GetStringProperty("RenderOuterTable", schemeData);
		}

		// Token: 0x04000469 RID: 1129
		private string backColor;

		// Token: 0x0400046A RID: 1130
		private string foreColor;

		// Token: 0x0400046B RID: 1131
		private string borderColor;

		// Token: 0x0400046C RID: 1132
		private string borderWidth;

		// Token: 0x0400046D RID: 1133
		private int borderStyle = -1;

		// Token: 0x0400046E RID: 1134
		private string fontSize;

		// Token: 0x0400046F RID: 1135
		private string fontName;

		// Token: 0x04000470 RID: 1136
		private string titleTextBackColor;

		// Token: 0x04000471 RID: 1137
		private string titleTextForeColor;

		// Token: 0x04000472 RID: 1138
		private int titleTextFont;

		// Token: 0x04000473 RID: 1139
		private string titleTextFontSize;

		// Token: 0x04000474 RID: 1140
		private int textLayout;

		// Token: 0x04000475 RID: 1141
		private int borderPadding;

		// Token: 0x04000476 RID: 1142
		private string instructionTextForeColor;

		// Token: 0x04000477 RID: 1143
		private int instructionTextFont;

		// Token: 0x04000478 RID: 1144
		private string textboxFontSize;

		// Token: 0x04000479 RID: 1145
		private string _loginButtonBackColor;

		// Token: 0x0400047A RID: 1146
		private string _loginButtonForeColor;

		// Token: 0x0400047B RID: 1147
		private string _loginButtonFontSize;

		// Token: 0x0400047C RID: 1148
		private string _loginButtonFontName;

		// Token: 0x0400047D RID: 1149
		private string _loginButtonBorderColor;

		// Token: 0x0400047E RID: 1150
		private string _loginButtonBorderWidth;

		// Token: 0x0400047F RID: 1151
		private int _loginButtonBorderStyle = -1;

		// Token: 0x04000480 RID: 1152
		private string _renderOuterTable;

		// Token: 0x04000481 RID: 1153
		private const int FONT_BOLD = 1;

		// Token: 0x04000482 RID: 1154
		private const int FONT_ITALIC = 2;
	}
}
