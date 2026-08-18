using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02000983 RID: 2435
	internal class ClassicProgressTemplate : BasicProgressTemplate
	{
		// Token: 0x06005C97 RID: 23703 RVA: 0x0011AD20 File Offset: 0x00118F20
		public ClassicProgressTemplate(RadProgressArea progressArea) : base(progressArea)
		{
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x0011AD2C File Offset: 0x00118F2C
		internal override void LayoutControls(ProgressPanel panel)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("ul");
			htmlGenericControl.Attributes["class"] = "ruProgress";
			this.AddListItemControl(htmlGenericControl, "ruProgressHeader", new Control[]
			{
				base.CreateSpan(RadProgressArea.ProgressAreaHeader, "ProgressArea Header")
			});
			base.LayoutTotalProgressData(htmlGenericControl, panel);
			base.LayoutFileCountData(htmlGenericControl, panel);
			if (panel.CurrentFileName != null)
			{
				this.AddListItemControl(htmlGenericControl, "ruCurrentFile", new Control[]
				{
					new LiteralControl(panel.Localization.CurrentFileName),
					new LiteralControl(" "),
					panel.CurrentFileName
				});
			}
			base.LayoutTimeData(htmlGenericControl, panel);
			if (panel.CancelButton != null)
			{
				this.AddListItemControl(htmlGenericControl, "ruActions", new Control[]
				{
					panel.CancelButton
				});
			}
			panel.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x0011AE10 File Offset: 0x00119010
		internal override HtmlGenericControl CreateProgressBarArea(string outerDivId, string innerDivID, int designTimePercents)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes["class"] = "ruBar";
			htmlGenericControl.ID = outerDivId;
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
			htmlGenericControl2.InnerHtml = "<!-- -->";
			htmlGenericControl2.ID = innerDivID;
			if (this._progressArea.isInDesignMode)
			{
				htmlGenericControl2.Style[HtmlTextWriterStyle.Width] = Unit.Percentage((double)designTimePercents).ToString();
			}
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			return htmlGenericControl;
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x0011AEA0 File Offset: 0x001190A0
		internal override void AddListItemControl(HtmlGenericControl list, string className, params Control[] controls)
		{
			if (controls.Length == 0)
			{
				return;
			}
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("li");
			htmlGenericControl.Attributes["class"] = className;
			foreach (Control child in controls)
			{
				htmlGenericControl.Controls.Add(child);
			}
			list.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x0011AEFC File Offset: 0x001190FC
		internal override HtmlInputButton CreateButton(string id, string text)
		{
			HtmlInputButton htmlInputButton = new HtmlInputButton("button");
			htmlInputButton.ID = id;
			htmlInputButton.Value = text;
			htmlInputButton.Attributes["class"] = "ruButton ruCancel";
			return htmlInputButton;
		}
	}
}
