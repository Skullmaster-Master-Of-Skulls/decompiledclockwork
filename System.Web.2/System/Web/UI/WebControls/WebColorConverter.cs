using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000513 RID: 1299
	public class WebColorConverter : ColorConverter
	{
		// Token: 0x06004134 RID: 16692 RVA: 0x000D54A4 File Offset: 0x000D36A4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				Color empty = Color.Empty;
				if (string.IsNullOrEmpty(text))
				{
					return empty;
				}
				if (text[0] == '#')
				{
					return base.ConvertFrom(context, culture, value);
				}
				if (StringUtil.EqualsIgnoreCase(text, "LightGrey"))
				{
					return Color.LightGray;
				}
				if (WebColorConverter.htmlSysColorTable == null)
				{
					WebColorConverter.InitializeHTMLSysColorTable();
				}
				object obj = WebColorConverter.htmlSysColorTable[text];
				if (obj != null)
				{
					return (Color)obj;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x000D5538 File Offset: 0x000D3738
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null)
			{
				Color left = (Color)value;
				if (left == Color.Empty)
				{
					return string.Empty;
				}
				if (!left.IsKnownColor)
				{
					StringBuilder stringBuilder = new StringBuilder("#", 7);
					stringBuilder.Append(left.R.ToString("X2", CultureInfo.InvariantCulture));
					stringBuilder.Append(left.G.ToString("X2", CultureInfo.InvariantCulture));
					stringBuilder.Append(left.B.ToString("X2", CultureInfo.InvariantCulture));
					return stringBuilder.ToString();
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000D5618 File Offset: 0x000D3818
		private static void InitializeHTMLSysColorTable()
		{
			Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			hashtable["activeborder"] = Color.FromKnownColor(KnownColor.ActiveBorder);
			hashtable["activecaption"] = Color.FromKnownColor(KnownColor.ActiveCaption);
			hashtable["appworkspace"] = Color.FromKnownColor(KnownColor.AppWorkspace);
			hashtable["background"] = Color.FromKnownColor(KnownColor.Desktop);
			hashtable["buttonface"] = Color.FromKnownColor(KnownColor.Control);
			hashtable["buttonhighlight"] = Color.FromKnownColor(KnownColor.ControlLightLight);
			hashtable["buttonshadow"] = Color.FromKnownColor(KnownColor.ControlDark);
			hashtable["buttontext"] = Color.FromKnownColor(KnownColor.ControlText);
			hashtable["captiontext"] = Color.FromKnownColor(KnownColor.ActiveCaptionText);
			hashtable["graytext"] = Color.FromKnownColor(KnownColor.GrayText);
			hashtable["highlight"] = Color.FromKnownColor(KnownColor.Highlight);
			hashtable["highlighttext"] = Color.FromKnownColor(KnownColor.HighlightText);
			hashtable["inactiveborder"] = Color.FromKnownColor(KnownColor.InactiveBorder);
			hashtable["inactivecaption"] = Color.FromKnownColor(KnownColor.InactiveCaption);
			hashtable["inactivecaptiontext"] = Color.FromKnownColor(KnownColor.InactiveCaptionText);
			hashtable["infobackground"] = Color.FromKnownColor(KnownColor.Info);
			hashtable["infotext"] = Color.FromKnownColor(KnownColor.InfoText);
			hashtable["menu"] = Color.FromKnownColor(KnownColor.Menu);
			hashtable["menutext"] = Color.FromKnownColor(KnownColor.MenuText);
			hashtable["scrollbar"] = Color.FromKnownColor(KnownColor.ScrollBar);
			hashtable["threeddarkshadow"] = Color.FromKnownColor(KnownColor.ControlDarkDark);
			hashtable["threedface"] = Color.FromKnownColor(KnownColor.Control);
			hashtable["threedhighlight"] = Color.FromKnownColor(KnownColor.ControlLight);
			hashtable["threedlightshadow"] = Color.FromKnownColor(KnownColor.ControlLightLight);
			hashtable["window"] = Color.FromKnownColor(KnownColor.Window);
			hashtable["windowframe"] = Color.FromKnownColor(KnownColor.WindowFrame);
			hashtable["windowtext"] = Color.FromKnownColor(KnownColor.WindowText);
			WebColorConverter.htmlSysColorTable = hashtable;
		}

		// Token: 0x04002509 RID: 9481
		private static Hashtable htmlSysColorTable;
	}
}
