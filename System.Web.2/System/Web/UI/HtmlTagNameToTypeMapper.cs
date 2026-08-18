using System;
using System.Collections;
using System.Web.Compilation;
using System.Web.UI.HtmlControls;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000292 RID: 658
	internal class HtmlTagNameToTypeMapper : ITagNameToTypeMapper
	{
		// Token: 0x06001F04 RID: 7940 RVA: 0x000030B5 File Offset: 0x000012B5
		internal HtmlTagNameToTypeMapper()
		{
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000636E0 File Offset: 0x000618E0
		Type ITagNameToTypeMapper.GetControlType(string tagName, IDictionary attributeBag)
		{
			if (HtmlTagNameToTypeMapper._tagMap == null)
			{
				Hashtable hashtable = new Hashtable(10, StringComparer.OrdinalIgnoreCase);
				hashtable.Add("a", typeof(HtmlAnchor));
				hashtable.Add("button", typeof(HtmlButton));
				hashtable.Add("form", typeof(HtmlForm));
				hashtable.Add("head", typeof(HtmlHead));
				hashtable.Add("img", typeof(HtmlImage));
				hashtable.Add("textarea", typeof(HtmlTextArea));
				hashtable.Add("select", typeof(HtmlSelect));
				hashtable.Add("table", typeof(HtmlTable));
				hashtable.Add("tr", typeof(HtmlTableRow));
				hashtable.Add("td", typeof(HtmlTableCell));
				hashtable.Add("th", typeof(HtmlTableCell));
				if (MultiTargetingUtil.IsTargetFramework45OrAbove)
				{
					hashtable.Add("audio", typeof(HtmlAudio));
					hashtable.Add("video", typeof(HtmlVideo));
					hashtable.Add("track", typeof(HtmlTrack));
					hashtable.Add("source", typeof(HtmlSource));
					hashtable.Add("iframe", typeof(HtmlIframe));
					hashtable.Add("embed", typeof(HtmlEmbed));
					hashtable.Add("area", typeof(HtmlArea));
					hashtable.Add("html", typeof(HtmlElement));
				}
				HtmlTagNameToTypeMapper._tagMap = hashtable;
			}
			if (HtmlTagNameToTypeMapper._inputTypes == null)
			{
				HtmlTagNameToTypeMapper._inputTypes = new Hashtable(10, StringComparer.OrdinalIgnoreCase)
				{
					{
						"text",
						typeof(HtmlInputText)
					},
					{
						"password",
						typeof(HtmlInputPassword)
					},
					{
						"button",
						typeof(HtmlInputButton)
					},
					{
						"submit",
						typeof(HtmlInputSubmit)
					},
					{
						"reset",
						typeof(HtmlInputReset)
					},
					{
						"image",
						typeof(HtmlInputImage)
					},
					{
						"checkbox",
						typeof(HtmlInputCheckBox)
					},
					{
						"radio",
						typeof(HtmlInputRadioButton)
					},
					{
						"hidden",
						typeof(HtmlInputHidden)
					},
					{
						"file",
						typeof(HtmlInputFile)
					}
				};
			}
			Type type;
			if (StringUtil.EqualsIgnoreCase("input", tagName))
			{
				string text = "text";
				if (attributeBag != null)
				{
					text = (((string)attributeBag["type"]) ?? text);
				}
				type = (Type)HtmlTagNameToTypeMapper._inputTypes[text];
				if (type == null)
				{
					if (!MultiTargetingUtil.IsTargetFramework45OrAbove)
					{
						throw new HttpException(SR.GetString("Invalid_type_for_input_tag", new object[]
						{
							text
						}));
					}
					type = typeof(HtmlInputGenericControl);
				}
			}
			else
			{
				type = (Type)HtmlTagNameToTypeMapper._tagMap[tagName];
				if (type == null)
				{
					type = typeof(HtmlGenericControl);
				}
			}
			return type;
		}

		// Token: 0x040019C8 RID: 6600
		private static Hashtable _tagMap;

		// Token: 0x040019C9 RID: 6601
		private static Hashtable _inputTypes;
	}
}
