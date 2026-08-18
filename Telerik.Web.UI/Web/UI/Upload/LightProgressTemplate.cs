using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02000984 RID: 2436
	internal class LightProgressTemplate : BasicProgressTemplate
	{
		// Token: 0x06005C9C RID: 23708 RVA: 0x0011AF38 File Offset: 0x00119138
		public LightProgressTemplate(RadProgressArea progressArea) : base(progressArea)
		{
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x0011AF41 File Offset: 0x00119141
		internal override void LayoutControls(ProgressPanel panel)
		{
			this.AddHeader(panel);
			this.AddBody(panel);
			if (panel.CancelButton != null)
			{
				this.AddFooter(panel);
			}
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x0011AF60 File Offset: 0x00119160
		private void AddHeader(ProgressPanel panel)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes["class"] = "ruHeader";
			if (!string.IsNullOrEmpty(this._progressArea.HeaderText))
			{
				htmlGenericControl.InnerHtml = this._progressArea.HeaderText;
			}
			this._progressArea.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x0011AFC4 File Offset: 0x001191C4
		private void AddBody(ProgressPanel panel)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes["class"] = "ruBody";
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
			htmlGenericControl2.Attributes["class"] = "ruFileProgress";
			base.LayoutTotalProgressData(htmlGenericControl2, panel);
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("div");
			htmlGenericControl3.Attributes["class"] = "ruOverallProgress";
			base.LayoutFileCountData(htmlGenericControl3, panel);
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			htmlGenericControl.Controls.Add(htmlGenericControl3);
			if (panel.CurrentFileName != null)
			{
				HtmlGenericControl htmlGenericControl4 = new HtmlGenericControl("p");
				htmlGenericControl4.Attributes["class"] = "ruCurrentFile";
				htmlGenericControl4.Controls.Add(new LiteralControl(panel.Localization.CurrentFileName));
				htmlGenericControl4.Controls.Add(new LiteralControl(" "));
				htmlGenericControl4.Controls.Add(panel.CurrentFileName);
				htmlGenericControl.Controls.Add(htmlGenericControl4);
			}
			HtmlGenericControl htmlGenericControl5 = new HtmlGenericControl("p");
			htmlGenericControl5.Attributes["class"] = "ruTimeSpeed";
			base.LayoutTimeData(htmlGenericControl5, panel);
			htmlGenericControl.Controls.Add(htmlGenericControl5);
			this._progressArea.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x0011B118 File Offset: 0x00119318
		private void AddFooter(ProgressPanel panel)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes["class"] = "ruFooter";
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
			htmlGenericControl2.Attributes["class"] = "radButton ruCancel";
			htmlGenericControl2.InnerText = panel.Localization.Cancel;
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			this._progressArea.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x0011B194 File Offset: 0x00119394
		internal override HtmlGenericControl CreateProgressBarArea(string outerElemId, string innerElemID, int designTimePercents)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			htmlGenericControl.Attributes["class"] = "ruProgressBar";
			htmlGenericControl.ID = outerElemId;
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
			htmlGenericControl2.Attributes["class"] = "ruProgress";
			htmlGenericControl2.ID = innerElemID;
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			return htmlGenericControl;
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x0011B1FC File Offset: 0x001193FC
		internal override void AddListItemControl(HtmlGenericControl list, string className, params Control[] controls)
		{
			if (controls.Length == 0)
			{
				return;
			}
			foreach (Control child in controls)
			{
				list.Controls.Add(child);
			}
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x0011B230 File Offset: 0x00119430
		internal override HtmlInputButton CreateButton(string id, string text)
		{
			HtmlInputButton htmlInputButton = new HtmlInputButton("button");
			htmlInputButton.ID = id;
			htmlInputButton.Value = text;
			htmlInputButton.Attributes["class"] = "radButton ruCancel";
			return htmlInputButton;
		}
	}
}
