using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000968 RID: 2408
	internal abstract class TreeListMobileView : WebControl, IScriptControl
	{
		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x06005BA2 RID: 23458 RVA: 0x001174A5 File Offset: 0x001156A5
		// (set) Token: 0x06005BA3 RID: 23459 RVA: 0x001174AD File Offset: 0x001156AD
		public RadTreeList TreeList { get; set; }

		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x06005BA4 RID: 23460 RVA: 0x001174B6 File Offset: 0x001156B6
		// (set) Token: 0x06005BA5 RID: 23461 RVA: 0x001174BE File Offset: 0x001156BE
		public string Title { get; set; }

		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x06005BA6 RID: 23462 RVA: 0x001174C7 File Offset: 0x001156C7
		// (set) Token: 0x06005BA7 RID: 23463 RVA: 0x001174CF File Offset: 0x001156CF
		protected bool OverrideClientID { get; set; }

		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x06005BA8 RID: 23464 RVA: 0x001174D8 File Offset: 0x001156D8
		protected TreeListLocalizationStrings Localization
		{
			get
			{
				return this.TreeList.Localization;
			}
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x001174E5 File Offset: 0x001156E5
		public TreeListMobileView(RadTreeList treelist)
		{
			this.TreeList = treelist;
			this.OverrideClientID = true;
		}

		// Token: 0x06005BAA RID: 23466 RVA: 0x00117511 File Offset: 0x00115711
		public void EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x06005BAB RID: 23467
		public abstract TreeListMobileViewType Type { get; }

		// Token: 0x06005BAC RID: 23468
		protected abstract void CreateContent(HtmlGenericControl container);

		// Token: 0x06005BAD RID: 23469 RVA: 0x00117519 File Offset: 0x00115719
		protected virtual void CreateFooter(Control container)
		{
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x0011751B File Offset: 0x0011571B
		protected virtual void DescribeProperties(ScriptControlDescriptor descriptor)
		{
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x06005BAF RID: 23471 RVA: 0x0011751D File Offset: 0x0011571D
		public override string ClientID
		{
			get
			{
				if (this.OverrideClientID)
				{
					return this.TreeList.ClientID + "_" + this.ID;
				}
				return base.ClientID;
			}
		}

		// Token: 0x17001E3C RID: 7740
		// (get) Token: 0x06005BB0 RID: 23472 RVA: 0x00117549 File Offset: 0x00115749
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x00117550 File Offset: 0x00115750
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = string.Format("rtlMobileMenu " + cssClass, new object[0]).Trim();
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x00117594 File Offset: 0x00115794
		protected HtmlGenericControl CreateTitle(string text)
		{
			return new HtmlGenericControl("strong")
			{
				InnerText = text
			};
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x001175B4 File Offset: 0x001157B4
		protected HtmlGenericControl CreateLabel(string title, string cssClass = "")
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("label");
			htmlGenericControl.Attributes.Add("class", string.Format("rtlLabel {0}", cssClass));
			htmlGenericControl.InnerText = title;
			return htmlGenericControl;
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x001175F0 File Offset: 0x001157F0
		protected HtmlGenericControl CreateOption(TreeListMobileViewOptionType type, string title, string name, string cssClass, bool isChecked = false)
		{
			HtmlGenericControl htmlGenericControl = this.CreateLabel(title, cssClass);
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("input");
			htmlGenericControl2.Attributes.Add("type", type.ToString().ToLower());
			if (isChecked)
			{
				htmlGenericControl2.Attributes.Add("checked", "checked");
			}
			if (type == TreeListMobileViewOptionType.Radio)
			{
				htmlGenericControl2.Attributes.Add("name", string.Format("{0}${1}", this.UniqueID, name));
			}
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			return htmlGenericControl;
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x0011767C File Offset: 0x0011587C
		protected HtmlGenericControl CreateLink(string title, string cssClass)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
			htmlGenericControl.Attributes.Add("href", "#");
			htmlGenericControl.Attributes.Add("class", cssClass);
			htmlGenericControl.InnerText = title;
			return htmlGenericControl;
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x001176C4 File Offset: 0x001158C4
		protected HtmlGenericControl CreateButton(string title, string cssClass = "")
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			htmlGenericControl.Attributes.Add("class", string.Format("rtlButton {0}", cssClass).Trim());
			htmlGenericControl.InnerText = title;
			return htmlGenericControl;
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x00117704 File Offset: 0x00115904
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.Controls.Add(this.CreateHeader());
			this.CreateBody();
			this.CreateFooter(this);
			base.ChildControlsCreated = true;
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x00117738 File Offset: 0x00115938
		private HtmlGenericControl CreateHeader()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", this.HeaderCssClass);
			htmlGenericControl.Controls.Add(RadTreeList.CreateButton("Back", this.Localization.MobileViewBackButtonText, true));
			htmlGenericControl.Controls.Add(RadTreeList.CreateButton("Cancel", this.Localization.MobileViewCancelButtonText, false));
			htmlGenericControl.Controls.Add(this.CreateTitle(this.Title));
			htmlGenericControl.Controls.Add(RadTreeList.CreateButton("Done", this.Localization.MobileViewDoneButtonText, false));
			return htmlGenericControl;
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x001177E4 File Offset: 0x001159E4
		protected virtual void CreateBody()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", this.BodyCssClass);
			this.Controls.Add(htmlGenericControl);
			this.CreateContent(htmlGenericControl);
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x00117828 File Offset: 0x00115A28
		protected override void OnPreRender(EventArgs e)
		{
			if (!base.DesignMode)
			{
				ScriptManager current = ScriptManager.GetCurrent(this.Page);
				if (current != null)
				{
					current.RegisterScriptControl<TreeListMobileView>(this);
				}
			}
			base.OnPreRender(e);
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x0011785A File Offset: 0x00115A5A
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
			base.Render(writer);
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x0011787C File Offset: 0x00115A7C
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			List<ScriptDescriptor> list = new List<ScriptDescriptor>();
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("Telerik.Web.UI." + base.GetType().Name, this.ClientID);
			scriptControlDescriptor.AddProperty("_type", this.Type);
			this.DescribeProperties(scriptControlDescriptor);
			list.Add(scriptControlDescriptor);
			return list;
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x001178D8 File Offset: 0x00115AD8
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>
			{
				new ScriptReference("Telerik.Web.UI.TreeList.Mobile.TreeListMobileView.js", Assembly.GetExecutingAssembly().FullName),
				new ScriptReference(string.Format("Telerik.Web.UI.TreeList.Mobile.{0}.js", base.GetType().Name), Assembly.GetExecutingAssembly().FullName)
			};
		}

		// Token: 0x04001605 RID: 5637
		private readonly string HeaderCssClass = "rtlCommand";

		// Token: 0x04001606 RID: 5638
		private readonly string BodyCssClass = "rtlMobileForm";
	}
}
