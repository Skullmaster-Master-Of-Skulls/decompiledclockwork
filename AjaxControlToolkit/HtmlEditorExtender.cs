using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.HtmlEditor.Sanitizer;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x020000A9 RID: 169
	[TargetControlType(typeof(TextBox))]
	[RequiredScript(typeof(ColorPickerExtender), 1)]
	[RequiredScript(typeof(CommonToolkitScripts), 0)]
	[ToolboxBitmap(typeof(Accessor), "HtmlEditorExtender.bmp")]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditorExtenderBehavior", "HtmlEditorExtender")]
	[ClientCssResource("HtmlEditorExtender")]
	public class HtmlEditorExtender : ExtenderControlBase
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		private static IHtmlSanitizer CreateSanitizer()
		{
			if (string.IsNullOrEmpty(ToolkitConfig.HtmlSanitizer))
			{
				return null;
			}
			Type type = Type.GetType(ToolkitConfig.HtmlSanitizer);
			object obj = Activator.CreateInstance(type);
			return (IHtmlSanitizer)obj;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000D8F7 File Offset: 0x0000BAF7
		public HtmlEditorExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000D90D File Offset: 0x0000BB0D
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0000D92C File Offset: 0x0000BB2C
		public IHtmlSanitizer Sanitizer
		{
			get
			{
				return HtmlEditorExtender._sanitizer.Value;
			}
			set
			{
				HtmlEditorExtender._sanitizer = new Lazy<IHtmlSanitizer>(() => value, true);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000D95D File Offset: 0x0000BB5D
		[ExtenderControlProperty(true, true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[ClientPropertyName("toolbarButtons")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HtmlEditorExtenderButtonCollection ToolbarButtons
		{
			get
			{
				this.EnsureButtons();
				return this.buttonList;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000D96B File Offset: 0x0000BB6B
		private void EnsureButtons()
		{
			if (this.buttonList == null || this.buttonList.Count == 0)
			{
				this.CreateButtons();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000D988 File Offset: 0x0000BB88
		[NotifyParentProperty(true)]
		[Description("Costumize visible buttons, leave empty to show all buttons")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor(typeof(HtmlEditorExtenderButtonCollectionEditor), typeof(UITypeEditor))]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public HtmlEditorExtenderButtonCollection Toolbar
		{
			get
			{
				if (this.buttonList == null)
				{
					this.buttonList = new HtmlEditorExtenderButtonCollection();
				}
				return this.buttonList;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000D9A3 File Offset: 0x0000BBA3
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0000D9B1 File Offset: 0x0000BBB1
		[ExtenderControlProperty]
		[DefaultValue(false)]
		[ClientPropertyName("displaySourceTab")]
		public bool DisplaySourceTab
		{
			get
			{
				return base.GetPropertyValue<bool>("DisplaySourceTab", false);
			}
			set
			{
				base.SetPropertyValue<bool>("DisplaySourceTab", value);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000D9BF File Offset: 0x0000BBBF
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0000D9D1 File Offset: 0x0000BBD1
		[ExtenderControlEvent]
		[DefaultValue("")]
		[ClientPropertyName("change")]
		public string OnClientChange
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientChange", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientChange", value);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000D9DF File Offset: 0x0000BBDF
		[Browsable(false)]
		public AjaxFileUpload AjaxFileUpload
		{
			get
			{
				return this.ajaxFileUpload;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000D9E7 File Offset: 0x0000BBE7
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000D9EF File Offset: 0x0000BBEF
		[Browsable(true)]
		[DefaultValue(true)]
		public bool EnableSanitization
		{
			get
			{
				return this.enableSanitization;
			}
			set
			{
				this.enableSanitization = value;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000517 RID: 1303 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		// (remove) Token: 0x06000518 RID: 1304 RVA: 0x0000DA30 File Offset: 0x0000BC30
		public event EventHandler<AjaxFileUploadEventArgs> ImageUploadComplete;

		// Token: 0x06000519 RID: 1305 RVA: 0x0000DA68 File Offset: 0x0000BC68
		public string Decode(string value)
		{
			this.EnsureButtons();
			string text = "font|div|span|br|strong|em|strike|sub|sup|center|blockquote|hr|ol|ul|li|br|s|p|b|i|u|img";
			string text2 = "style|size|color|face|align|dir|src|width|id|class";
			string text3 = "\\'\\,\\w\\-#\\s\\:\\;\\?\\&\\.\\-\\=";
			string text4 = Regex.Replace(value, "\\&quot\\;", "\"", RegexOptions.IgnoreCase);
			text4 = Regex.Replace(text4, "&apos;", "'", RegexOptions.IgnoreCase);
			text4 = Regex.Replace(text4, string.Concat(new string[]
			{
				"(?:\\&lt\\;|\\<)(\\/?)((?:",
				text,
				")(?:\\s(?:",
				text2,
				")=\"[",
				text3,
				"]*\")*)(?:\\&gt\\;|\\>)"
			}), "<$1$2>", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
			string text5 = "^\\\"\\>\\<\\\\";
			text4 = Regex.Replace(text4, string.Concat(new string[]
			{
				"(?:\\&lt\\;|\\<)(\\/?)(a(?:(?:\\shref\\=\\\"[",
				text5,
				"]*\\\")|(?:\\sstyle\\=\\\"[",
				text3,
				"]*\\\"))*)(?:\\&gt\\;|\\>)"
			}), "<$1$2>", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
			text4 = Regex.Replace(text4, "&?lt;", "<");
			text4 = Regex.Replace(text4, "&?gt;", ">");
			text4 = Regex.Replace(text4, "&amp;", "&", RegexOptions.IgnoreCase);
			text4 = Regex.Replace(text4, "&nbsp;", "\u00a0", RegexOptions.IgnoreCase);
			text4 = Regex.Replace(text4, "[^<]<[^>]*expression[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
			text4 = Regex.Replace(text4, "[^<]<[^>]*data\\:[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
			text4 = Regex.Replace(text4, "[^<]<[^>]*script[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
			text4 = Regex.Replace(text4, "[^<]<[^>]*filter[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
			text4 = Regex.Replace(text4, "[^<]<[^>]*behavior[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
			text4 = Regex.Replace(text4, "[^<]<[^>]*javascript\\:[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
			text4 = Regex.Replace(text4, "[^<]<[^>]*position\\:[^>]*>", "", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
			if (this.EnableSanitization && this.Sanitizer != null)
			{
				Dictionary<string, string[]> dictionary = this.MakeCombinedElementList();
				if (!dictionary.ContainsKey("span"))
				{
					dictionary.Add("span", new string[0]);
				}
				if (!dictionary.ContainsKey("br"))
				{
					dictionary.Add("br", new string[0]);
				}
				text4 = this.Sanitizer.GetSafeHtmlFragment(text4, dictionary);
			}
			return text4.Replace("<hr>", "<hr />");
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode)
			{
				if (this.EnableSanitization && this.Sanitizer == null)
				{
					throw new Exception("The Sanitizer is not configured in the web.config file. Either install the AjaxControlToolkit.HtmlEditor.Sanitizer NuGet package or set the EnableSanitization property to False (insecure).");
				}
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
				htmlGenericControl.Attributes.Add("Id", this.ClientID + "_popupDiv");
				htmlGenericControl.Attributes.Add("style", "opacity: 0;");
				htmlGenericControl.Attributes.Add("class", "ajax__html_editor_extender_popupDiv");
				this.ajaxFileUpload = new AjaxFileUpload();
				this.ajaxFileUpload.ID = this.ID + "_ajaxFileUpload";
				this.ajaxFileUpload.MaximumNumberOfFiles = 10;
				this.ajaxFileUpload.AllowedFileTypes = "jpg,jpeg,gif,png";
				this.ajaxFileUpload.Enabled = true;
				this.ajaxFileUpload.OnClientUploadComplete = "ajaxClientUploadComplete";
				if (this.ImageUploadComplete != null)
				{
					this.ajaxFileUpload.UploadComplete += this.ImageUploadComplete;
				}
				htmlGenericControl.Controls.Add(this.ajaxFileUpload);
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
				htmlGenericControl2.Attributes.Add("Id", this.ClientID + "_btnCancel");
				htmlGenericControl2.Attributes.Add("style", "float: right; position:relative; padding-left: 20px; top:10px; width: 55px; border-color:black;border-style: solid; border-width: 1px;cursor:pointer;");
				htmlGenericControl2.Attributes.Add("float", "right");
				htmlGenericControl2.Attributes.Add("unselectable", "on");
				htmlGenericControl2.InnerText = "Cancel";
				htmlGenericControl.Controls.Add(htmlGenericControl2);
				this.Controls.Add(htmlGenericControl);
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000DE54 File Offset: 0x0000C054
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ScriptManager.RegisterOnSubmitStatement(this, typeof(HtmlEditorExtender), "HtmlEditorExtenderOnSubmit", "null;");
			base.ClientState = ((string.Compare(this.Page.Form.DefaultFocus, base.TargetControlID, StringComparison.OrdinalIgnoreCase) == 0) ? "Focused" : null);
			TextBox textBox = (TextBox)base.TargetControl;
			if (textBox != null)
			{
				textBox.Text = this.Decode(textBox.Text);
			}
			bool flag = false;
			foreach (HtmlEditorExtenderButton htmlEditorExtenderButton in this.buttonList)
			{
				if (htmlEditorExtenderButton.CommandName == "InsertImage")
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.ajaxFileUpload.Visible = false;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000DF34 File Offset: 0x0000C134
		protected virtual void CreateButtons()
		{
			this.buttonList = new HtmlEditorExtenderButtonCollection();
			if (!this.tracked)
			{
				this.tracked = true;
				return;
			}
			this.tracked = false;
			this.buttonList.Add(new Undo());
			this.buttonList.Add(new Redo());
			this.buttonList.Add(new Bold());
			this.buttonList.Add(new Italic());
			this.buttonList.Add(new Underline());
			this.buttonList.Add(new StrikeThrough());
			this.buttonList.Add(new Subscript());
			this.buttonList.Add(new Superscript());
			this.buttonList.Add(new JustifyLeft());
			this.buttonList.Add(new JustifyCenter());
			this.buttonList.Add(new JustifyRight());
			this.buttonList.Add(new JustifyFull());
			this.buttonList.Add(new InsertOrderedList());
			this.buttonList.Add(new InsertUnorderedList());
			this.buttonList.Add(new CreateLink());
			this.buttonList.Add(new UnLink());
			this.buttonList.Add(new RemoveFormat());
			this.buttonList.Add(new SelectAll());
			this.buttonList.Add(new UnSelect());
			this.buttonList.Add(new Delete());
			this.buttonList.Add(new Cut());
			this.buttonList.Add(new Copy());
			this.buttonList.Add(new Paste());
			this.buttonList.Add(new BackgroundColorSelector());
			this.buttonList.Add(new ForeColorSelector());
			this.buttonList.Add(new FontNameSelector());
			this.buttonList.Add(new FontSizeSelector());
			this.buttonList.Add(new Indent());
			this.buttonList.Add(new Outdent());
			this.buttonList.Add(new InsertHorizontalRule());
			this.buttonList.Add(new HorizontalSeparator());
			this.buttonList.Add(new CleanWord());
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000E164 File Offset: 0x0000C364
		private Dictionary<string, string[]> MakeCombinedElementList()
		{
			Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
			foreach (HtmlEditorExtenderButton htmlEditorExtenderButton in this.ToolbarButtons)
			{
				if (htmlEditorExtenderButton.ElementWhiteList != null)
				{
					foreach (KeyValuePair<string, string[]> keyValuePair in htmlEditorExtenderButton.ElementWhiteList)
					{
						if (dictionary.ContainsKey(keyValuePair.Key))
						{
							bool flag = false;
							string[] source;
							if (dictionary.TryGetValue(keyValuePair.Key, out source))
							{
								List<string> list = source.ToList<string>();
								foreach (string text in keyValuePair.Value)
								{
									if (!source.Contains(text))
									{
										list.Add(text);
										flag = true;
									}
								}
								if (flag)
								{
									dictionary[keyValuePair.Key] = list.ToArray();
								}
							}
						}
						else
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x040002CC RID: 716
		internal const int ButtonWidthDef = 23;

		// Token: 0x040002CD RID: 717
		internal const int ButtonHeightDef = 21;

		// Token: 0x040002CE RID: 718
		private static Lazy<IHtmlSanitizer> _sanitizer = new Lazy<IHtmlSanitizer>(new Func<IHtmlSanitizer>(HtmlEditorExtender.CreateSanitizer), true);

		// Token: 0x040002CF RID: 719
		private HtmlEditorExtenderButtonCollection buttonList;

		// Token: 0x040002D0 RID: 720
		private AjaxFileUpload ajaxFileUpload;

		// Token: 0x040002D1 RID: 721
		private bool enableSanitization = true;

		// Token: 0x040002D3 RID: 723
		private bool tracked;
	}
}
