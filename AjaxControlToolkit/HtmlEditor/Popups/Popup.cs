using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor.Popups
{
	// Token: 0x020000DE RID: 222
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Popups.Popup", "HtmlEditor.Popups.Popup")]
	public abstract class Popup : ScriptControlBase
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x00010D1C File Offset: 0x0000EF1C
		public static Popup GetExistingPopup(Control parent, Type type)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control.GetType().Equals(type))
				{
					return control as Popup;
				}
				Popup existingPopup = Popup.GetExistingPopup(control, type);
				if (existingPopup != null)
				{
					return existingPopup;
				}
			}
			return null;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00010D9C File Offset: 0x0000EF9C
		protected Popup() : base(false, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00010DC4 File Offset: 0x0000EFC4
		private bool isDesign
		{
			get
			{
				bool result;
				try
				{
					bool flag = this.Context == null || (base.Site != null && base.Site.DesignMode);
					result = flag;
				}
				catch
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00010E14 File Offset: 0x0000F014
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x00010E1C File Offset: 0x0000F01C
		[ExtenderControlProperty]
		[ClientPropertyName("autoDimensions")]
		[DefaultValue(true)]
		[Category("behavior")]
		public bool AutoDimensions
		{
			get
			{
				return this._autoDimensions;
			}
			set
			{
				this._autoDimensions = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00010E25 File Offset: 0x0000F025
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x00010E2D File Offset: 0x0000F02D
		[Category("Appearance")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("initialContent")]
		public string InitialContent
		{
			get
			{
				return this._initialContent;
			}
			set
			{
				this._initialContent = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00010E36 File Offset: 0x0000F036
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00010E3E File Offset: 0x0000F03E
		[ExtenderControlProperty]
		[DefaultValue("")]
		[Category("Appearance")]
		[ClientPropertyName("cssPath")]
		public string CssPath
		{
			get
			{
				return this._cssPath;
			}
			set
			{
				this._cssPath = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00010E47 File Offset: 0x0000F047
		public Collection<RegisteredField> RegisteredFields
		{
			get
			{
				if (this._registeredFields == null)
				{
					this._registeredFields = new Collection<RegisteredField>();
				}
				return this._registeredFields;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00010E64 File Offset: 0x0000F064
		private string RegisteredFieldsIds
		{
			get
			{
				string str = "[";
				for (int i = 0; i < this.RegisteredFields.Count; i++)
				{
					if (i > 0)
					{
						str += ",";
					}
					str += "{name: ";
					str = str + "'" + this.RegisteredFields[i].Name + "'";
					str += ", clientID: ";
					str = str + "'" + this.RegisteredFields[i].Control.ClientID + "'";
					str += "}";
				}
				return str + "]";
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00010F1D File Offset: 0x0000F11D
		public Collection<RegisteredField> RegisteredHandlers
		{
			get
			{
				if (this._registeredHandlers == null)
				{
					this._registeredHandlers = new Collection<RegisteredField>();
				}
				return this._registeredHandlers;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00010F38 File Offset: 0x0000F138
		private string RegisteredHandlersIds
		{
			get
			{
				string str = "[";
				for (int i = 0; i < this.RegisteredHandlers.Count; i++)
				{
					if (i > 0)
					{
						str += ",";
					}
					str += "{name: ";
					str = str + "'" + this.RegisteredHandlers[i].Name + "'";
					str += ", clientID: ";
					str = str + "'" + this.RegisteredHandlers[i].Control.ClientID + "'";
					str += ", callMethod: null";
					str += "}";
				}
				return str + "]";
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00011000 File Offset: 0x0000F200
		private string GetResourceString(string key)
		{
			switch (key)
			{
			case "HtmlEditor_toolbar_popup_LinkProperties_button_Cancel":
				return "Cancel";
			case "HtmlEditor_toolbar_popup_LinkProperties_button_OK":
				return "OK";
			case "HtmlEditor_toolbar_popup_LinkProperties_field_URL":
				return "URL";
			case "HtmlEditor_toolbar_popup_LinkProperties_field_Target":
				return "Target";
			case "HtmlEditor_toolbar_popup_LinkProperties_field_Target_New":
				return "New window";
			case "HtmlEditor_toolbar_popup_LinkProperties_field_Target_Current":
				return "Current window";
			case "HtmlEditor_toolbar_popup_LinkProperties_field_Target_Parent":
				return "Parent window";
			case "HtmlEditor_toolbar_popup_LinkProperties_field_Target_Top":
				return "Top window";
			}
			throw new ArgumentOutOfRangeException("key", key, "Unknown resource key");
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00011104 File Offset: 0x0000F304
		protected string GetButton(string name)
		{
			return this.GetResourceString("HtmlEditor_toolbar_popup_" + base.GetType().Name + "_button_" + name);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00011127 File Offset: 0x0000F327
		protected string GetField(string name)
		{
			return this.GetResourceString("HtmlEditor_toolbar_popup_" + base.GetType().Name + "_field_" + name);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001114A File Offset: 0x0000F34A
		protected string GetField(string name, string subName)
		{
			return this.GetField(name + "_" + subName);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00011160 File Offset: 0x0000F360
		protected override Style CreateControlStyle()
		{
			return new Popup.PopupStyle(this.ViewState, this);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001117C File Offset: 0x0000F37C
		protected override void OnInit(EventArgs e)
		{
			this._rm = new ResourceManager("ScriptResources.BaseScriptsResources", Assembly.GetExecutingAssembly());
			base.OnInit(e);
			if (this.isDesign)
			{
				return;
			}
			this._iframe = new HtmlGenericControl("iframe");
			this._iframe.Attributes.Add("scrolling", "no");
			this._iframe.Attributes.Add("marginHeight", "0");
			this._iframe.Attributes.Add("marginWidth", "0");
			this._iframe.Attributes.Add("frameborder", "0");
			this._iframe.Attributes.Add("tabindex", "-1");
			this.Controls.Add(this._iframe);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00011254 File Offset: 0x0000F454
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this._iframe != null)
			{
				string text = (this._savedCSS != null) ? this._savedCSS : base.Style.Value;
				if (text != null && text.Length > 0)
				{
					this._iframe.Style.Value = text;
				}
				if (this.Height.ToString().Length > 0)
				{
					this._iframe.Style[HtmlTextWriterStyle.Height] = this.Height.ToString();
				}
				if (this.Width.ToString().Length > 0)
				{
					this._iframe.Style[HtmlTextWriterStyle.Width] = this.Width.ToString();
				}
				this._iframe.Attributes.Add("id", this._iframe.ClientID);
			}
			this.Height = this.Height;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001135D File Offset: 0x0000F55D
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.isDesign)
			{
				base.Render(writer);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00011370 File Offset: 0x0000F570
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddElementProperty("iframe", this._iframe.ClientID);
			descriptor.AddProperty("registeredFields", this.RegisteredFieldsIds);
			descriptor.AddProperty("registeredHandlers", this.RegisteredHandlersIds);
		}

		// Token: 0x040002E6 RID: 742
		private ResourceManager _rm;

		// Token: 0x040002E7 RID: 743
		private HtmlGenericControl _iframe;

		// Token: 0x040002E8 RID: 744
		private Collection<RegisteredField> _registeredFields;

		// Token: 0x040002E9 RID: 745
		private Collection<RegisteredField> _registeredHandlers;

		// Token: 0x040002EA RID: 746
		private string _savedCSS;

		// Token: 0x040002EB RID: 747
		private string _initialContent = string.Empty;

		// Token: 0x040002EC RID: 748
		private string _cssPath = string.Empty;

		// Token: 0x040002ED RID: 749
		private bool _autoDimensions = true;

		// Token: 0x020000DF RID: 223
		private sealed class PopupStyle : Style
		{
			// Token: 0x0600066D RID: 1645 RVA: 0x000113BC File Offset: 0x0000F5BC
			public PopupStyle(StateBag state, Popup popup) : base(state)
			{
				this._popup = popup;
			}

			// Token: 0x0600066E RID: 1646 RVA: 0x000113CC File Offset: 0x0000F5CC
			protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
			{
				this._popup._savedCSS = attributes.Value;
				attributes.Add(HtmlTextWriterStyle.Position, "absolute");
				attributes.Add(HtmlTextWriterStyle.Top, "-2000px");
				attributes.Add(HtmlTextWriterStyle.Left, "-2000px");
			}

			// Token: 0x040002EE RID: 750
			private Popup _popup;
		}
	}
}
