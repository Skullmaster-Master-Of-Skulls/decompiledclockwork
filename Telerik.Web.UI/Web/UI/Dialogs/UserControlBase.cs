using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Editor;
using Telerik.Web.UI.Editor.DialogControls;

namespace Telerik.Web.UI.Dialogs
{
	// Token: 0x0200026C RID: 620
	public abstract class UserControlBase : RadWebControl, ILocalizableControl
	{
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06001663 RID: 5731 RVA: 0x0004C0F8 File Offset: 0x0004A2F8
		// (remove) Token: 0x06001664 RID: 5732 RVA: 0x0004C130 File Offset: 0x0004A330
		public event RendeModeChangedHandler RenderModeChanged;

		// Token: 0x06001665 RID: 5733 RVA: 0x0004C165 File Offset: 0x0004A365
		protected virtual void OnRenderModeChanged(RenderModeChangedEventArgs e)
		{
			if (this.RenderModeChanged != null)
			{
				this.RenderModeChanged(this, e);
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0004C17C File Offset: 0x0004A37C
		// (set) Token: 0x06001667 RID: 5735 RVA: 0x0004C184 File Offset: 0x0004A384
		[Category("Appearance")]
		[DefaultValue(RenderMode.Classic)]
		[NotifyParentProperty(true)]
		[Description("Specifies the rendering mode of the control")]
		public override RenderMode RenderMode
		{
			get
			{
				return base.RenderMode;
			}
			set
			{
				base.RenderMode = value;
				this.OnRenderModeChanged(new RenderModeChangedEventArgs(value));
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0004C199 File Offset: 0x0004A399
		internal override bool ShouldRegisterCssReferences
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0004C19C File Offset: 0x0004A39C
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0004C1BD File Offset: 0x0004A3BD
		internal bool IsInAccessibleMode
		{
			get
			{
				return (bool)(this.ViewState["IsInAccessibleMode"] ?? false);
			}
			set
			{
				this.ViewState["IsInAccessibleMode"] = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0004C1DC File Offset: 0x0004A3DC
		protected override string CssClassFormatString
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0004C1E3 File Offset: 0x0004A3E3
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600166D RID: 5741
		public abstract string DialogName { get; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0004C1E7 File Offset: 0x0004A3E7
		// (set) Token: 0x0600166F RID: 5743 RVA: 0x0004C1F9 File Offset: 0x0004A3F9
		public virtual string Title
		{
			get
			{
				return this._title ?? this.DialogName;
			}
			set
			{
				this._title = value;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0004C202 File Offset: 0x0004A402
		public virtual string ControlName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0004C209 File Offset: 0x0004A409
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x0004C23D File Offset: 0x0004A43D
		[Description("Gets or sets a string containing the localization language for the RadEditor UI.")]
		[MergableProperty(true)]
		[Category("Appearance")]
		public string Language
		{
			get
			{
				if (this.ViewState["Language"] == null)
				{
					return CultureInfo.CurrentUICulture.Name;
				}
				return (string)this.ViewState["Language"];
			}
			set
			{
				this.ViewState["Language"] = value;
				this._culture = ((value == null) ? null : CultureInfo.GetCultureInfo(value));
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0004C262 File Offset: 0x0004A462
		CultureInfo ILocalizableControl.Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0004C26A File Offset: 0x0004A46A
		protected ToolsStrings ToolsLocalization
		{
			get
			{
				if (this._toolsLocalization == null)
				{
					this._toolsLocalization = new ToolsStrings(new LocalizationProvider("RadEditor.Tools", this, this.LocalizationPath), false);
				}
				return this._toolsLocalization;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0004C297 File Offset: 0x0004A497
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x0004C2B7 File Offset: 0x0004A4B7
		[DefaultValue("")]
		[Category("Dialog Configuration")]
		public string ExternalDialogsPath
		{
			get
			{
				return ((string)this.ViewState["ExternalDialogsPath"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ExternalDialogsPath"] = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0004C2D1 File Offset: 0x0004A4D1
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x0004C2F1 File Offset: 0x0004A4F1
		[Category("Misc")]
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where the control will look for its .resx localization files.")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["LocalizationPath"] = value;
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0004C304 File Offset: 0x0004A504
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			UserControlResources userControlResources = new UserControlResources();
			userControlResources.ID = "resources" + this.DialogName;
			this.Controls.Add(userControlResources);
			string resourceContent = this.GetResourceContent();
			this.Controls.Add(this.Page.ParseControl(resourceContent));
			if (!string.IsNullOrEmpty(this.ControlName))
			{
				PlaceHolder placeHolder = (PlaceHolder)this.FindControlRecursive("PreviewerPlaceHolder");
				if (placeHolder != null)
				{
					placeHolder.Controls.Add(this.Page.ParseControl(this.GetResourceContent(this.ControlName + "Manager")));
				}
			}
			this.UpdateChildControls(this);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0004C3B1 File Offset: 0x0004A5B1
		private string GetResourceContent()
		{
			return this.GetResourceContent(this.DialogName);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0004C3C0 File Offset: 0x0004A5C0
		protected string GetResourceContent(string name)
		{
			string text = string.Empty;
			if (this.ExternalDialogsPath.Length > 0)
			{
				try
				{
					string path = this.Page.Server.MapPath(this.ExternalDialogsPath + name + ".ascx");
					if (File.Exists(path))
					{
						using (StreamReader streamReader = new StreamReader(path))
						{
							text = streamReader.ReadToEnd();
						}
					}
				}
				catch (Exception)
				{
				}
			}
			if (text.Length == 0)
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				Encoding utf = Encoding.UTF8;
				if (this.IsInAccessibleMode)
				{
					string format = "Telerik.Web.UI.Editor.AccessibleEditor.DialogControls.{0}.ascx";
					using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(string.Format(format, name)))
					{
						if (manifestResourceStream != null)
						{
							byte[] array = new byte[manifestResourceStream.Length];
							manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
							text = utf.GetString(array);
						}
					}
				}
				if (text.Length == 0)
				{
					string format = "Telerik.Web.UI.Editor.DialogControls.{0}.ascx";
					using (Stream manifestResourceStream2 = executingAssembly.GetManifestResourceStream(string.Format(format, name)))
					{
						byte[] array2 = new byte[manifestResourceStream2.Length];
						manifestResourceStream2.Read(array2, 0, (int)manifestResourceStream2.Length);
						text = utf.GetString(array2);
					}
				}
			}
			text = text.Replace("Assembly=\"Telerik.Web.UI\"", string.Format("Assembly=\"Telerik.Web.UI{0}\"", ", Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4".Substring(20)));
			return text;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0004C559 File Offset: 0x0004A759
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
			this.UpdateChildrenRenderMode(this);
			this.RenderModeChanged += delegate(object sender, EventArgs ev)
			{
				this.UpdateChildrenRenderMode(this);
			};
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0004C584 File Offset: 0x0004A784
		private void UpdateChildrenRenderMode(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				ISkinnableControl skinnableControl = control as ISkinnableControl;
				if (skinnableControl != null)
				{
					skinnableControl.RenderMode = this.RenderMode;
				}
				this.UpdateChildrenRenderMode(control);
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0004C5F4 File Offset: 0x0004A7F4
		public Control FindControlRecursive(string id)
		{
			this.EnsureChildControls();
			return this.FindControlRecursiveInt(this, id);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0004C614 File Offset: 0x0004A814
		private Control FindControlRecursiveInt(Control parent, string id)
		{
			Control control = parent.FindControl(id);
			int num = 0;
			while (control == null && num < parent.Controls.Count)
			{
				control = this.FindControlRecursiveInt(parent.Controls[num++], id);
			}
			return control;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0004C658 File Offset: 0x0004A858
		protected void SetControlRenderMode(string controlId)
		{
			ISkinnableControl skinnableControl = this.FindControlRecursive(controlId) as ISkinnableControl;
			if (skinnableControl != null)
			{
				skinnableControl.RenderMode = this.RenderMode;
			}
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0004C684 File Offset: 0x0004A884
		internal void UpdateChildControls(Control currentControl)
		{
			foreach (object obj in currentControl.Controls)
			{
				Control control = (Control)obj;
				ISkinnableControl skinnableControl = control as ISkinnableControl;
				if (skinnableControl != null)
				{
					skinnableControl.Skin = base.RuntimeSkin;
					skinnableControl.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
					skinnableControl.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				}
				UserControlBase userControlBase = control as UserControlBase;
				if (userControlBase != null)
				{
					userControlBase.Language = this.Language;
					userControlBase.RenderMode = this.RenderMode;
					if (!string.IsNullOrEmpty(this.LocalizationPath))
					{
						userControlBase.LocalizationPath = this.LocalizationPath;
					}
					if (!string.IsNullOrEmpty(this.ExternalDialogsPath))
					{
						userControlBase.ExternalDialogsPath = this.ExternalDialogsPath;
						userControlBase.EnsureChildControls();
					}
					if (this.IsInAccessibleMode)
					{
						userControlBase.IsInAccessibleMode = this.IsInAccessibleMode;
						userControlBase.EnsureChildControls();
					}
					userControlBase.UpdateChildControls(userControlBase);
				}
				else
				{
					UserControlResources userControlResources = control as UserControlResources;
					if (userControlResources != null)
					{
						userControlResources.Language = this.Language;
						if (userControlResources.ID == "resources" + this.DialogName)
						{
							string dialogName = string.IsNullOrEmpty(this.ControlName) ? this.DialogName : (this.ControlName + "Manager");
							this.Localization = new DialogsStrings(new LocalizationProvider("RadEditor.Dialogs", userControlResources, this.LocalizationPath), dialogName, false);
							userControlResources.Localization = this.Localization;
						}
					}
					this.UpdateChildControls(control);
				}
			}
		}

		// Token: 0x040005EF RID: 1519
		private string _title;

		// Token: 0x040005F0 RID: 1520
		private CultureInfo _culture;

		// Token: 0x040005F1 RID: 1521
		private ToolsStrings _toolsLocalization;

		// Token: 0x040005F2 RID: 1522
		protected DialogLocalizationStrings Localization;
	}
}
