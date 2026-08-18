using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001909 RID: 6409
	[LightweightRendering]
	[Themeable(true)]
	public abstract class InputManagerBase : RadControl, INamingContainer
	{
		// Token: 0x0600F8B3 RID: 63667 RVA: 0x00382CE8 File Offset: 0x00380EE8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ScriptManager.RegisterOnSubmitStatement(this, base.GetType(), "TextBoxWatermarkExtenderOnSubmit", "null;");
		}

		// Token: 0x0600F8B4 RID: 63668 RVA: 0x00382D08 File Offset: 0x00380F08
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(this.RenderAsDiv ? HtmlTextWriterTag.Div : HtmlTextWriterTag.Span);
			base.Render(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600F8B5 RID: 63669 RVA: 0x00382D60 File Offset: 0x00380F60
		protected virtual string FormatCssClass(string prefix, string userDefined)
		{
			string text = this.EmptySkin ? string.Format("RadInputMgr {0}", prefix) : string.Format("RadInputMgr RadInputMgr_{1} {0}_{1}", prefix, base.RuntimeSkin);
			if (userDefined.Contains(" "))
			{
				string[] array = userDefined.Split(new char[]
				{
					' '
				});
				List<string> list = new List<string>();
				foreach (string text2 in array)
				{
					if (!text2.Contains("RadInputMgr") && !text2.Contains("RadInput_"))
					{
						list.Add(text2);
					}
				}
				userDefined = string.Join(" ", list.ToArray());
			}
			if (userDefined.IndexOf(text) >= 0)
			{
				return userDefined;
			}
			if (string.IsNullOrEmpty(userDefined))
			{
				return text;
			}
			return string.Format("{0} {1}", text, userDefined);
		}

		// Token: 0x17004B27 RID: 19239
		// (get) Token: 0x0600F8B6 RID: 63670 RVA: 0x00382E2F File Offset: 0x0038102F
		protected internal bool EmptySkin
		{
			get
			{
				return string.IsNullOrEmpty(base.RuntimeSkin);
			}
		}

		// Token: 0x0600F8B7 RID: 63671 RVA: 0x00382E3C File Offset: 0x0038103C
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			base.RenderChildren(writer);
		}

		// Token: 0x17004B28 RID: 19240
		// (get) Token: 0x0600F8B8 RID: 63672 RVA: 0x00382E45 File Offset: 0x00381045
		// (set) Token: 0x0600F8B9 RID: 63673 RVA: 0x00382E4D File Offset: 0x0038104D
		[Browsable(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x040046CA RID: 18122
		protected internal bool RenderAsDiv;
	}
}
