using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000AF RID: 175
	internal sealed class CreateUserWizardAutoFormat : BaseAutoFormat<CreateUserWizard>
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00019EBC File Offset: 0x000180BC
		public CreateUserWizardAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 500;
			base.Style.Height = 400;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00019EF8 File Offset: 0x000180F8
		protected override void Apply(CreateUserWizard createUserWizard)
		{
			createUserWizard.StepStyle.Reset();
			createUserWizard.BackColor = ColorTranslator.FromHtml(this.backColor);
			createUserWizard.BorderColor = ColorTranslator.FromHtml(this.borderColor);
			createUserWizard.BorderWidth = new Unit(this.borderWidth, CultureInfo.InvariantCulture);
			if (this.borderStyle >= 0 && this.borderStyle <= 9)
			{
				createUserWizard.BorderStyle = (BorderStyle)this.borderStyle;
			}
			else
			{
				createUserWizard.BorderStyle = BorderStyle.NotSet;
			}
			createUserWizard.Font.Size = new FontUnit(this.fontSize, CultureInfo.InvariantCulture);
			createUserWizard.Font.Name = this.fontName;
			createUserWizard.Font.ClearDefaults();
			createUserWizard.TitleTextStyle.BackColor = ColorTranslator.FromHtml(this.titleTextBackColor);
			createUserWizard.TitleTextStyle.ForeColor = ColorTranslator.FromHtml(this.titleTextForeColor);
			createUserWizard.TitleTextStyle.Font.Bold = ((this.titleTextFont & 1) != 0);
			createUserWizard.TitleTextStyle.Font.ClearDefaults();
			createUserWizard.StepStyle.BorderWidth = this.StepStyleBorderWidth;
			createUserWizard.StepStyle.BorderStyle = this.StepStyleBorderStyle;
			createUserWizard.StepStyle.BorderColor = this.StepStyleBorderColor;
			createUserWizard.StepStyle.ForeColor = this.StepStyleForeColor;
			createUserWizard.StepStyle.BackColor = this.StepStyleBackColor;
			createUserWizard.StepStyle.Font.Size = this.StepStyleFontSize;
			createUserWizard.StepStyle.Font.ClearDefaults();
			createUserWizard.SideBarButtonStyle.Font.Underline = this.SideBarButtonStyleFontUnderline;
			createUserWizard.SideBarButtonStyle.Font.Name = this.SideBarButtonStyleFontName;
			createUserWizard.SideBarButtonStyle.ForeColor = this.SideBarButtonStyleForeColor;
			createUserWizard.SideBarButtonStyle.BorderWidth = this.SideBarButtonStyleBorderWidth;
			createUserWizard.SideBarButtonStyle.BackColor = this.SideBarButtonStyleBackColor;
			createUserWizard.SideBarButtonStyle.Font.ClearDefaults();
			createUserWizard.NavigationButtonStyle.BorderWidth = this.NavigationButtonStyleBorderWidth;
			createUserWizard.NavigationButtonStyle.Font.Name = this.NavigationButtonStyleFontName;
			createUserWizard.NavigationButtonStyle.Font.Size = this.NavigationButtonStyleFontSize;
			createUserWizard.NavigationButtonStyle.BorderStyle = this.NavigationButtonStyleBorderStyle;
			createUserWizard.NavigationButtonStyle.BorderColor = this.NavigationButtonStyleBorderColor;
			createUserWizard.NavigationButtonStyle.ForeColor = this.NavigationButtonStyleForeColor;
			createUserWizard.NavigationButtonStyle.BackColor = this.NavigationButtonStyleBackColor;
			createUserWizard.NavigationButtonStyle.Font.ClearDefaults();
			createUserWizard.ContinueButtonStyle.BorderWidth = this.NavigationButtonStyleBorderWidth;
			createUserWizard.ContinueButtonStyle.Font.Name = this.NavigationButtonStyleFontName;
			createUserWizard.ContinueButtonStyle.Font.Size = this.NavigationButtonStyleFontSize;
			createUserWizard.ContinueButtonStyle.BorderStyle = this.NavigationButtonStyleBorderStyle;
			createUserWizard.ContinueButtonStyle.BorderColor = this.NavigationButtonStyleBorderColor;
			createUserWizard.ContinueButtonStyle.ForeColor = this.NavigationButtonStyleForeColor;
			createUserWizard.ContinueButtonStyle.BackColor = this.NavigationButtonStyleBackColor;
			createUserWizard.ContinueButtonStyle.Font.ClearDefaults();
			createUserWizard.CreateUserButtonStyle.BorderWidth = this.NavigationButtonStyleBorderWidth;
			createUserWizard.CreateUserButtonStyle.Font.Name = this.NavigationButtonStyleFontName;
			createUserWizard.CreateUserButtonStyle.Font.Size = this.NavigationButtonStyleFontSize;
			createUserWizard.CreateUserButtonStyle.BorderStyle = this.NavigationButtonStyleBorderStyle;
			createUserWizard.CreateUserButtonStyle.BorderColor = this.NavigationButtonStyleBorderColor;
			createUserWizard.CreateUserButtonStyle.ForeColor = this.NavigationButtonStyleForeColor;
			createUserWizard.CreateUserButtonStyle.BackColor = this.NavigationButtonStyleBackColor;
			createUserWizard.CreateUserButtonStyle.Font.ClearDefaults();
			createUserWizard.HeaderStyle.ForeColor = this.HeaderStyleForeColor;
			createUserWizard.HeaderStyle.BorderColor = this.HeaderStyleBorderColor;
			createUserWizard.HeaderStyle.BackColor = this.HeaderStyleBackColor;
			createUserWizard.HeaderStyle.Font.Size = this.HeaderStyleFontSize;
			createUserWizard.HeaderStyle.Font.Bold = this.HeaderStyleFontBold;
			createUserWizard.HeaderStyle.BorderWidth = this.HeaderStyleBorderWidth;
			createUserWizard.HeaderStyle.HorizontalAlign = this.HeaderStyleHorizontalAlign;
			createUserWizard.HeaderStyle.BorderStyle = this.HeaderStyleBorderStyle;
			createUserWizard.HeaderStyle.Font.ClearDefaults();
			createUserWizard.SideBarStyle.BackColor = this.SideBarStyleBackColor;
			createUserWizard.SideBarStyle.VerticalAlign = this.SideBarStyleVerticalAlign;
			createUserWizard.SideBarStyle.Font.Size = this.SideBarStyleFontSize;
			createUserWizard.SideBarStyle.Font.Underline = this.SideBarStyleFontUnderline;
			createUserWizard.SideBarStyle.Font.Strikeout = this.SideBarStyleFontStrikeout;
			createUserWizard.SideBarStyle.BorderWidth = this.SideBarStyleBorderWidth;
			createUserWizard.SideBarStyle.Font.ClearDefaults();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001A3C4 File Offset: 0x000185C4
		protected override void Initialize(DataRow schemeData)
		{
			this.backColor = BaseAutoFormat<CreateUserWizard>.GetStringProperty("BackColor", schemeData);
			this.borderColor = BaseAutoFormat<CreateUserWizard>.GetStringProperty("BorderColor", schemeData);
			this.borderWidth = BaseAutoFormat<CreateUserWizard>.GetStringProperty("BorderWidth", schemeData);
			this.borderStyle = BaseAutoFormat<CreateUserWizard>.GetIntProperty("BorderStyle", -1, schemeData);
			this.fontSize = BaseAutoFormat<CreateUserWizard>.GetStringProperty("FontSize", schemeData);
			this.fontName = BaseAutoFormat<CreateUserWizard>.GetStringProperty("FontName", schemeData);
			this.titleTextBackColor = BaseAutoFormat<CreateUserWizard>.GetStringProperty("TitleTextBackColor", schemeData);
			this.titleTextForeColor = BaseAutoFormat<CreateUserWizard>.GetStringProperty("TitleTextForeColor", schemeData);
			this.titleTextFont = BaseAutoFormat<CreateUserWizard>.GetIntProperty("TitleTextFont", schemeData);
			this.NavigationButtonStyleBorderWidth = new Unit(BaseAutoFormat<CreateUserWizard>.GetStringProperty("NavigationButtonStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.NavigationButtonStyleFontName = BaseAutoFormat<CreateUserWizard>.GetStringProperty("NavigationButtonStyleFontName", schemeData);
			this.NavigationButtonStyleFontSize = new FontUnit(BaseAutoFormat<CreateUserWizard>.GetStringProperty("NavigationButtonStyleFontSize", schemeData), CultureInfo.InvariantCulture);
			this.NavigationButtonStyleBorderStyle = (BorderStyle)BaseAutoFormat<CreateUserWizard>.GetIntProperty("NavigationButtonStyleBorderStyle", schemeData);
			this.NavigationButtonStyleBorderColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("NavigationButtonStyleBorderColor", schemeData));
			this.NavigationButtonStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("NavigationButtonStyleForeColor", schemeData));
			this.NavigationButtonStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("NavigationButtonStyleBackColor", schemeData));
			this.StepStyleBorderWidth = new Unit(BaseAutoFormat<CreateUserWizard>.GetStringProperty("StepStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.StepStyleBorderStyle = (BorderStyle)BaseAutoFormat<CreateUserWizard>.GetIntProperty("StepStyleBorderStyle", schemeData);
			this.StepStyleBorderColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("StepStyleBorderColor", schemeData));
			this.StepStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("StepStyleForeColor", schemeData));
			this.StepStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("StepStyleBackColor", schemeData));
			this.StepStyleFontSize = new FontUnit(BaseAutoFormat<CreateUserWizard>.GetStringProperty("StepStyleFontSize", schemeData), CultureInfo.InvariantCulture);
			this.SideBarButtonStyleFontUnderline = BaseAutoFormat<CreateUserWizard>.GetBooleanProperty("SideBarButtonStyleFontUnderline", schemeData);
			this.SideBarButtonStyleFontName = BaseAutoFormat<CreateUserWizard>.GetStringProperty("SideBarButtonStyleFontName", schemeData);
			this.SideBarButtonStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("SideBarButtonStyleForeColor", schemeData));
			this.SideBarButtonStyleBorderWidth = new Unit(BaseAutoFormat<CreateUserWizard>.GetStringProperty("SideBarButtonStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.SideBarButtonStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("SideBarButtonStyleBackColor", schemeData));
			this.HeaderStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("HeaderStyleForeColor", schemeData));
			this.HeaderStyleBorderColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("HeaderStyleBorderColor", schemeData));
			this.HeaderStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("HeaderStyleBackColor", schemeData));
			this.HeaderStyleFontSize = new FontUnit(BaseAutoFormat<CreateUserWizard>.GetStringProperty("HeaderStyleFontSize", schemeData), CultureInfo.InvariantCulture);
			this.HeaderStyleFontBold = BaseAutoFormat<CreateUserWizard>.GetBooleanProperty("HeaderStyleFontBold", schemeData);
			this.HeaderStyleBorderWidth = new Unit(BaseAutoFormat<CreateUserWizard>.GetStringProperty("HeaderStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.HeaderStyleHorizontalAlign = (HorizontalAlign)BaseAutoFormat<CreateUserWizard>.GetIntProperty("HeaderStyleHorizontalAlign", schemeData);
			this.HeaderStyleBorderStyle = (BorderStyle)BaseAutoFormat<CreateUserWizard>.GetIntProperty("HeaderStyleBorderStyle", schemeData);
			this.SideBarStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<CreateUserWizard>.GetStringProperty("SideBarStyleBackColor", schemeData));
			this.SideBarStyleVerticalAlign = (VerticalAlign)BaseAutoFormat<CreateUserWizard>.GetIntProperty("SideBarStyleVerticalAlign", schemeData);
			this.SideBarStyleFontSize = new FontUnit(BaseAutoFormat<CreateUserWizard>.GetStringProperty("SideBarStyleFontSize", schemeData), CultureInfo.InvariantCulture);
			this.SideBarStyleFontUnderline = BaseAutoFormat<CreateUserWizard>.GetBooleanProperty("SideBarStyleFontUnderline", schemeData);
			this.SideBarStyleFontStrikeout = BaseAutoFormat<CreateUserWizard>.GetBooleanProperty("SideBarStyleFontStrikeout", schemeData);
			this.SideBarStyleBorderWidth = new Unit(BaseAutoFormat<CreateUserWizard>.GetStringProperty("SideBarStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
		}

		// Token: 0x04000298 RID: 664
		private string backColor;

		// Token: 0x04000299 RID: 665
		private string borderColor;

		// Token: 0x0400029A RID: 666
		private string borderWidth;

		// Token: 0x0400029B RID: 667
		private int borderStyle = -1;

		// Token: 0x0400029C RID: 668
		private string fontSize;

		// Token: 0x0400029D RID: 669
		private string fontName;

		// Token: 0x0400029E RID: 670
		private string titleTextBackColor;

		// Token: 0x0400029F RID: 671
		private string titleTextForeColor;

		// Token: 0x040002A0 RID: 672
		private int titleTextFont;

		// Token: 0x040002A1 RID: 673
		private Unit NavigationButtonStyleBorderWidth;

		// Token: 0x040002A2 RID: 674
		private string NavigationButtonStyleFontName;

		// Token: 0x040002A3 RID: 675
		private FontUnit NavigationButtonStyleFontSize;

		// Token: 0x040002A4 RID: 676
		private BorderStyle NavigationButtonStyleBorderStyle;

		// Token: 0x040002A5 RID: 677
		private Color NavigationButtonStyleBorderColor;

		// Token: 0x040002A6 RID: 678
		private Color NavigationButtonStyleForeColor;

		// Token: 0x040002A7 RID: 679
		private Color NavigationButtonStyleBackColor;

		// Token: 0x040002A8 RID: 680
		private Unit StepStyleBorderWidth;

		// Token: 0x040002A9 RID: 681
		private BorderStyle StepStyleBorderStyle;

		// Token: 0x040002AA RID: 682
		private Color StepStyleBorderColor;

		// Token: 0x040002AB RID: 683
		private Color StepStyleForeColor;

		// Token: 0x040002AC RID: 684
		private Color StepStyleBackColor;

		// Token: 0x040002AD RID: 685
		private FontUnit StepStyleFontSize;

		// Token: 0x040002AE RID: 686
		private bool SideBarButtonStyleFontUnderline;

		// Token: 0x040002AF RID: 687
		private string SideBarButtonStyleFontName;

		// Token: 0x040002B0 RID: 688
		private Color SideBarButtonStyleForeColor;

		// Token: 0x040002B1 RID: 689
		private Unit SideBarButtonStyleBorderWidth;

		// Token: 0x040002B2 RID: 690
		private Color SideBarButtonStyleBackColor;

		// Token: 0x040002B3 RID: 691
		private Color HeaderStyleForeColor;

		// Token: 0x040002B4 RID: 692
		private Color HeaderStyleBorderColor;

		// Token: 0x040002B5 RID: 693
		private Color HeaderStyleBackColor;

		// Token: 0x040002B6 RID: 694
		private FontUnit HeaderStyleFontSize;

		// Token: 0x040002B7 RID: 695
		private bool HeaderStyleFontBold;

		// Token: 0x040002B8 RID: 696
		private Unit HeaderStyleBorderWidth;

		// Token: 0x040002B9 RID: 697
		private HorizontalAlign HeaderStyleHorizontalAlign;

		// Token: 0x040002BA RID: 698
		private BorderStyle HeaderStyleBorderStyle;

		// Token: 0x040002BB RID: 699
		private Color SideBarStyleBackColor;

		// Token: 0x040002BC RID: 700
		private VerticalAlign SideBarStyleVerticalAlign;

		// Token: 0x040002BD RID: 701
		private FontUnit SideBarStyleFontSize;

		// Token: 0x040002BE RID: 702
		private bool SideBarStyleFontUnderline;

		// Token: 0x040002BF RID: 703
		private bool SideBarStyleFontStrikeout;

		// Token: 0x040002C0 RID: 704
		private Unit SideBarStyleBorderWidth;

		// Token: 0x040002C1 RID: 705
		private const int FONT_BOLD = 1;
	}
}
