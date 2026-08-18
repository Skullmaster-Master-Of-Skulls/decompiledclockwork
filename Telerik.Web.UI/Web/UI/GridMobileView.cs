using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200038B RID: 907
	internal abstract class GridMobileView : WebControl, IScriptControl
	{
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x00062B5A File Offset: 0x00060D5A
		// (set) Token: 0x06001F44 RID: 8004 RVA: 0x00062B62 File Offset: 0x00060D62
		public GridTableView TableView { get; set; }

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x00062B6B File Offset: 0x00060D6B
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x00062B73 File Offset: 0x00060D73
		public string Title { get; set; }

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00062B7C File Offset: 0x00060D7C
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x00062B84 File Offset: 0x00060D84
		protected bool OverrideClientID { get; set; }

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00062B8D File Offset: 0x00060D8D
		protected GridStrings Localization
		{
			get
			{
				return this.TableView.OwnerGrid.Localization;
			}
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00062B9F File Offset: 0x00060D9F
		public GridMobileView(GridTableView tableView)
		{
			this.TableView = tableView;
			this.OverrideClientID = true;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00062BCB File Offset: 0x00060DCB
		public void EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001F4C RID: 8012
		public abstract GridMobileViewType Type { get; }

		// Token: 0x06001F4D RID: 8013
		protected abstract void CreateContent(HtmlGenericControl container);

		// Token: 0x06001F4E RID: 8014 RVA: 0x00062BD3 File Offset: 0x00060DD3
		protected virtual void CreateFooter(Control container)
		{
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00062BD5 File Offset: 0x00060DD5
		protected virtual void DescribeProperties(ScriptControlDescriptor descriptor)
		{
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00062BD7 File Offset: 0x00060DD7
		public override string ClientID
		{
			get
			{
				if (this.OverrideClientID)
				{
					return this.TableView.ClientID + "_" + this.ID;
				}
				return base.ClientID;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x00062C03 File Offset: 0x00060E03
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00062C08 File Offset: 0x00060E08
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = string.Format("rgMobileMenu " + cssClass, new object[0]).Trim();
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00062C4C File Offset: 0x00060E4C
		protected HtmlGenericControl CreateTitle(string text)
		{
			return new HtmlGenericControl("strong")
			{
				InnerText = text
			};
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00062C6C File Offset: 0x00060E6C
		protected HtmlGenericControl CreateLabel(string title, string cssClass = "")
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("label");
			htmlGenericControl.Attributes.Add("class", string.Format("rgLabel {0}", cssClass));
			htmlGenericControl.InnerText = title;
			return htmlGenericControl;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00062CA8 File Offset: 0x00060EA8
		protected HtmlGenericControl CreateOption(GridMobileViewOptionType type, string title, string name, string cssClass, bool isChecked = false)
		{
			HtmlGenericControl htmlGenericControl = this.CreateLabel(title, cssClass);
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("input");
			htmlGenericControl2.Attributes.Add("type", type.ToString().ToLower());
			if (isChecked)
			{
				htmlGenericControl2.Attributes.Add("checked", "checked");
			}
			if (type == GridMobileViewOptionType.Radio)
			{
				htmlGenericControl2.Attributes.Add("name", string.Format("{0}${1}", this.UniqueID, name));
			}
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			return htmlGenericControl;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00062D34 File Offset: 0x00060F34
		protected HtmlGenericControl CreateLink(string title, string cssClass)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
			htmlGenericControl.Attributes.Add("href", "#");
			htmlGenericControl.Attributes.Add("class", cssClass);
			htmlGenericControl.InnerText = title;
			return htmlGenericControl;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00062D7C File Offset: 0x00060F7C
		protected HtmlGenericControl CreateButton(string title, string cssClass = "")
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			htmlGenericControl.Attributes.Add("class", string.Format("rgButton {0}", cssClass).Trim());
			htmlGenericControl.InnerText = title;
			return htmlGenericControl;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00062DBC File Offset: 0x00060FBC
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.Controls.Add(this.CreateHeader());
			this.CreateBody();
			this.CreateFooter(this);
			base.ChildControlsCreated = true;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00062DF0 File Offset: 0x00060FF0
		private HtmlGenericControl CreateHeader()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", this.HeaderCssClass);
			htmlGenericControl.Controls.Add(RadGrid.CreateButton("Back", this.Localization.MobileViewBackButtonText, true));
			htmlGenericControl.Controls.Add(RadGrid.CreateButton("Cancel", this.Localization.MobileViewCancelButtonText, false));
			htmlGenericControl.Controls.Add(this.CreateTitle(this.Title));
			htmlGenericControl.Controls.Add(RadGrid.CreateButton("Done", this.Localization.MobileViewDoneButtonText, false));
			return htmlGenericControl;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00062E9C File Offset: 0x0006109C
		protected virtual void CreateBody()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", this.BodyCssClass);
			this.Controls.Add(htmlGenericControl);
			this.CreateContent(htmlGenericControl);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00062EE0 File Offset: 0x000610E0
		protected override void OnPreRender(EventArgs e)
		{
			if (!base.DesignMode)
			{
				ScriptManager current = ScriptManager.GetCurrent(this.Page);
				if (current != null)
				{
					current.RegisterScriptControl<GridMobileView>(this);
				}
			}
			base.OnPreRender(e);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00062F12 File Offset: 0x00061112
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
			base.Render(writer);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00062F34 File Offset: 0x00061134
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			List<ScriptDescriptor> list = new List<ScriptDescriptor>();
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("Telerik.Web.UI." + base.GetType().Name, this.ClientID);
			scriptControlDescriptor.AddProperty("_type", this.Type);
			this.DescribeProperties(scriptControlDescriptor);
			list.Add(scriptControlDescriptor);
			return list;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00062F90 File Offset: 0x00061190
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>();
		}

		// Token: 0x04000801 RID: 2049
		private readonly string HeaderCssClass = "rgCommandRow";

		// Token: 0x04000802 RID: 2050
		private readonly string BodyCssClass = "rgMobileForm";
	}
}
