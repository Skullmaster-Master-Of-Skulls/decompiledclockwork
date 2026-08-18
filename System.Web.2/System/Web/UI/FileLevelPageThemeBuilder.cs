using System;

namespace System.Web.UI
{
	// Token: 0x020002DF RID: 735
	internal class FileLevelPageThemeBuilder : RootBuilder
	{
		// Token: 0x0600223B RID: 8763 RVA: 0x0006FFE5 File Offset: 0x0006E1E5
		public override void AppendLiteralString(string s)
		{
			if (s != null && !Util.IsWhiteSpaceString(s))
			{
				throw new HttpException(SR.GetString("Literal_content_not_allowed", new object[]
				{
					SR.GetString("Page_theme_skin_file"),
					s.Trim()
				}));
			}
			base.AppendLiteralString(s);
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00070028 File Offset: 0x0006E228
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			Type controlType = subBuilder.ControlType;
			if (!typeof(Control).IsAssignableFrom(controlType))
			{
				throw new HttpException(SR.GetString("Page_theme_only_controls_allowed", new object[]
				{
					(controlType == null) ? string.Empty : controlType.ToString()
				}));
			}
			if (base.InPageTheme && !ThemeableAttribute.IsTypeThemeable(subBuilder.ControlType))
			{
				throw new HttpParseException(SR.GetString("Type_theme_disabled", new object[]
				{
					subBuilder.ControlType.FullName
				}), null, subBuilder.VirtualPath, null, subBuilder.Line);
			}
			base.AppendSubBuilder(subBuilder);
		}
	}
}
