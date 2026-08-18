using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000139 RID: 313
	internal sealed class WizardAutoFormat : BaseAutoFormat<Wizard>
	{
		// Token: 0x06000B4E RID: 2894 RVA: 0x00048FFA File Offset: 0x000471FA
		public WizardAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 350;
			base.Style.Height = 200;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00049030 File Offset: 0x00047230
		protected override void Apply(Wizard wizard)
		{
			wizard.Font.Name = this.FontName;
			wizard.Font.Size = this.FontSize;
			wizard.BackColor = this.BackColor;
			wizard.BorderColor = this.BorderColor;
			wizard.BorderWidth = this.BorderWidth;
			wizard.BorderStyle = this.BorderStyle;
			wizard.Font.ClearDefaults();
			wizard.NavigationButtonStyle.BorderWidth = this.NavigationButtonStyleBorderWidth;
			wizard.NavigationButtonStyle.Font.Name = this.NavigationButtonStyleFontName;
			wizard.NavigationButtonStyle.Font.Size = this.NavigationButtonStyleFontSize;
			wizard.NavigationButtonStyle.BorderStyle = this.NavigationButtonStyleBorderStyle;
			wizard.NavigationButtonStyle.BorderColor = this.NavigationButtonStyleBorderColor;
			wizard.NavigationButtonStyle.ForeColor = this.NavigationButtonStyleForeColor;
			wizard.NavigationButtonStyle.BackColor = this.NavigationButtonStyleBackColor;
			wizard.NavigationButtonStyle.Font.ClearDefaults();
			wizard.StepStyle.BorderWidth = this.StepStyleBorderWidth;
			wizard.StepStyle.BorderStyle = this.StepStyleBorderStyle;
			wizard.StepStyle.BorderColor = this.StepStyleBorderColor;
			wizard.StepStyle.ForeColor = this.StepStyleForeColor;
			wizard.StepStyle.BackColor = this.StepStyleBackColor;
			wizard.StepStyle.Font.Size = this.StepStyleFontSize;
			wizard.StepStyle.Font.ClearDefaults();
			wizard.SideBarButtonStyle.Font.Underline = this.SideBarButtonStyleFontUnderline;
			wizard.SideBarButtonStyle.Font.Name = this.SideBarButtonStyleFontName;
			wizard.SideBarButtonStyle.ForeColor = this.SideBarButtonStyleForeColor;
			wizard.SideBarButtonStyle.BorderWidth = this.SideBarButtonStyleBorderWidth;
			wizard.SideBarButtonStyle.BackColor = this.SideBarButtonStyleBackColor;
			wizard.SideBarButtonStyle.Font.ClearDefaults();
			wizard.HeaderStyle.ForeColor = this.HeaderStyleForeColor;
			wizard.HeaderStyle.BorderColor = this.HeaderStyleBorderColor;
			wizard.HeaderStyle.BackColor = this.HeaderStyleBackColor;
			wizard.HeaderStyle.Font.Size = this.HeaderStyleFontSize;
			wizard.HeaderStyle.Font.Bold = this.HeaderStyleFontBold;
			wizard.HeaderStyle.BorderWidth = this.HeaderStyleBorderWidth;
			wizard.HeaderStyle.HorizontalAlign = this.HeaderStyleHorizontalAlign;
			wizard.HeaderStyle.BorderStyle = this.HeaderStyleBorderStyle;
			wizard.HeaderStyle.Font.ClearDefaults();
			wizard.SideBarStyle.BackColor = this.SideBarStyleBackColor;
			wizard.SideBarStyle.VerticalAlign = this.SideBarStyleVerticalAlign;
			wizard.SideBarStyle.Font.Size = this.SideBarStyleFontSize;
			wizard.SideBarStyle.Font.Underline = this.SideBarStyleFontUnderline;
			wizard.SideBarStyle.Font.Strikeout = this.SideBarStyleFontStrikeout;
			wizard.SideBarStyle.BorderWidth = this.SideBarStyleBorderWidth;
			wizard.SideBarStyle.Font.ClearDefaults();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0004933C File Offset: 0x0004753C
		protected override void Initialize(DataRow schemeData)
		{
			if (schemeData == null)
			{
				return;
			}
			this.FontName = BaseAutoFormat<Wizard>.GetStringProperty("FontName", schemeData);
			this.FontSize = new FontUnit(BaseAutoFormat<Wizard>.GetStringProperty("FontSize", schemeData), CultureInfo.InvariantCulture);
			this.BackColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("BackColor", schemeData));
			this.BorderColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("BorderColor", schemeData));
			this.BorderWidth = new Unit(BaseAutoFormat<Wizard>.GetStringProperty("BorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.SideBarStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("SideBarStyleBackColor", schemeData));
			this.SideBarStyleVerticalAlign = (VerticalAlign)BaseAutoFormat<Wizard>.GetIntProperty("SideBarStyleVerticalAlign", schemeData);
			this.BorderStyle = (BorderStyle)BaseAutoFormat<Wizard>.GetIntProperty("BorderStyle", schemeData);
			this.NavigationButtonStyleBorderWidth = new Unit(BaseAutoFormat<Wizard>.GetStringProperty("NavigationButtonStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.NavigationButtonStyleFontName = BaseAutoFormat<Wizard>.GetStringProperty("NavigationButtonStyleFontName", schemeData);
			this.NavigationButtonStyleFontSize = new FontUnit(BaseAutoFormat<Wizard>.GetStringProperty("NavigationButtonStyleFontSize", schemeData), CultureInfo.InvariantCulture);
			this.NavigationButtonStyleBorderStyle = (BorderStyle)BaseAutoFormat<Wizard>.GetIntProperty("NavigationButtonStyleBorderStyle", schemeData);
			this.NavigationButtonStyleBorderColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("NavigationButtonStyleBorderColor", schemeData));
			this.NavigationButtonStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("NavigationButtonStyleForeColor", schemeData));
			this.NavigationButtonStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("NavigationButtonStyleBackColor", schemeData));
			this.StepStyleBorderWidth = new Unit(BaseAutoFormat<Wizard>.GetStringProperty("StepStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.StepStyleBorderStyle = (BorderStyle)BaseAutoFormat<Wizard>.GetIntProperty("StepStyleBorderStyle", schemeData);
			this.StepStyleBorderColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("StepStyleBorderColor", schemeData));
			this.StepStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("StepStyleForeColor", schemeData));
			this.StepStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("StepStyleBackColor", schemeData));
			this.StepStyleFontSize = new FontUnit(BaseAutoFormat<Wizard>.GetStringProperty("StepStyleFontSize", schemeData), CultureInfo.InvariantCulture);
			this.SideBarButtonStyleFontUnderline = BaseAutoFormat<Wizard>.GetBooleanProperty("SideBarButtonStyleFontUnderline", schemeData);
			this.SideBarButtonStyleFontName = BaseAutoFormat<Wizard>.GetStringProperty("SideBarButtonStyleFontName", schemeData);
			this.SideBarButtonStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("SideBarButtonStyleForeColor", schemeData));
			this.SideBarButtonStyleBorderWidth = new Unit(BaseAutoFormat<Wizard>.GetStringProperty("SideBarButtonStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.SideBarButtonStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("SideBarButtonStyleBackColor", schemeData));
			this.HeaderStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("HeaderStyleForeColor", schemeData));
			this.HeaderStyleBorderColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("HeaderStyleBorderColor", schemeData));
			this.HeaderStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("HeaderStyleBackColor", schemeData));
			this.HeaderStyleFontSize = new FontUnit(BaseAutoFormat<Wizard>.GetStringProperty("HeaderStyleFontSize", schemeData), CultureInfo.InvariantCulture);
			this.HeaderStyleFontBold = BaseAutoFormat<Wizard>.GetBooleanProperty("HeaderStyleFontBold", schemeData);
			this.HeaderStyleBorderWidth = new Unit(BaseAutoFormat<Wizard>.GetStringProperty("HeaderStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
			this.HeaderStyleHorizontalAlign = (HorizontalAlign)BaseAutoFormat<Wizard>.GetIntProperty("HeaderStyleHorizontalAlign", schemeData);
			this.HeaderStyleBorderStyle = (BorderStyle)BaseAutoFormat<Wizard>.GetIntProperty("HeaderStyleBorderStyle", schemeData);
			this.SideBarStyleBackColor = ColorTranslator.FromHtml(BaseAutoFormat<Wizard>.GetStringProperty("SideBarStyleBackColor", schemeData));
			this.SideBarStyleVerticalAlign = (VerticalAlign)BaseAutoFormat<Wizard>.GetIntProperty("SideBarStyleVerticalAlign", schemeData);
			this.SideBarStyleFontSize = new FontUnit(BaseAutoFormat<Wizard>.GetStringProperty("SideBarStyleFontSize", schemeData), CultureInfo.InvariantCulture);
			this.SideBarStyleFontUnderline = BaseAutoFormat<Wizard>.GetBooleanProperty("SideBarStyleFontUnderline", schemeData);
			this.SideBarStyleFontStrikeout = BaseAutoFormat<Wizard>.GetBooleanProperty("SideBarStyleFontStrikeout", schemeData);
			this.SideBarStyleBorderWidth = new Unit(BaseAutoFormat<Wizard>.GetStringProperty("SideBarStyleBorderWidth", schemeData), CultureInfo.InvariantCulture);
		}

		// Token: 0x040006C4 RID: 1732
		private string FontName;

		// Token: 0x040006C5 RID: 1733
		private FontUnit FontSize;

		// Token: 0x040006C6 RID: 1734
		private Color BackColor;

		// Token: 0x040006C7 RID: 1735
		private Color BorderColor;

		// Token: 0x040006C8 RID: 1736
		private Unit BorderWidth;

		// Token: 0x040006C9 RID: 1737
		private BorderStyle BorderStyle;

		// Token: 0x040006CA RID: 1738
		private Unit NavigationButtonStyleBorderWidth;

		// Token: 0x040006CB RID: 1739
		private string NavigationButtonStyleFontName;

		// Token: 0x040006CC RID: 1740
		private FontUnit NavigationButtonStyleFontSize;

		// Token: 0x040006CD RID: 1741
		private BorderStyle NavigationButtonStyleBorderStyle;

		// Token: 0x040006CE RID: 1742
		private Color NavigationButtonStyleBorderColor;

		// Token: 0x040006CF RID: 1743
		private Color NavigationButtonStyleForeColor;

		// Token: 0x040006D0 RID: 1744
		private Color NavigationButtonStyleBackColor;

		// Token: 0x040006D1 RID: 1745
		private Unit StepStyleBorderWidth;

		// Token: 0x040006D2 RID: 1746
		private BorderStyle StepStyleBorderStyle;

		// Token: 0x040006D3 RID: 1747
		private Color StepStyleBorderColor;

		// Token: 0x040006D4 RID: 1748
		private Color StepStyleForeColor;

		// Token: 0x040006D5 RID: 1749
		private Color StepStyleBackColor;

		// Token: 0x040006D6 RID: 1750
		private FontUnit StepStyleFontSize;

		// Token: 0x040006D7 RID: 1751
		private bool SideBarButtonStyleFontUnderline;

		// Token: 0x040006D8 RID: 1752
		private string SideBarButtonStyleFontName;

		// Token: 0x040006D9 RID: 1753
		private Color SideBarButtonStyleForeColor;

		// Token: 0x040006DA RID: 1754
		private Unit SideBarButtonStyleBorderWidth;

		// Token: 0x040006DB RID: 1755
		private Color SideBarButtonStyleBackColor;

		// Token: 0x040006DC RID: 1756
		private Color HeaderStyleForeColor;

		// Token: 0x040006DD RID: 1757
		private Color HeaderStyleBorderColor;

		// Token: 0x040006DE RID: 1758
		private Color HeaderStyleBackColor;

		// Token: 0x040006DF RID: 1759
		private FontUnit HeaderStyleFontSize;

		// Token: 0x040006E0 RID: 1760
		private bool HeaderStyleFontBold;

		// Token: 0x040006E1 RID: 1761
		private Unit HeaderStyleBorderWidth;

		// Token: 0x040006E2 RID: 1762
		private HorizontalAlign HeaderStyleHorizontalAlign;

		// Token: 0x040006E3 RID: 1763
		private BorderStyle HeaderStyleBorderStyle;

		// Token: 0x040006E4 RID: 1764
		private Color SideBarStyleBackColor;

		// Token: 0x040006E5 RID: 1765
		private VerticalAlign SideBarStyleVerticalAlign;

		// Token: 0x040006E6 RID: 1766
		private FontUnit SideBarStyleFontSize;

		// Token: 0x040006E7 RID: 1767
		private bool SideBarStyleFontUnderline;

		// Token: 0x040006E8 RID: 1768
		private bool SideBarStyleFontStrikeout;

		// Token: 0x040006E9 RID: 1769
		private Unit SideBarStyleBorderWidth;
	}
}
