using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x0200051F RID: 1311
	[ToolboxItem(false)]
	public abstract class ImageEditorDialog : WebControl
	{
		// Token: 0x06002EBE RID: 11966 RVA: 0x00098C43 File Offset: 0x00096E43
		public ImageEditorDialog()
		{
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x00098C4B File Offset: 0x00096E4B
		public ImageEditorDialog(string skin, RadImageEditor parentImageEditor)
		{
			this.Skin = skin;
			this.ParentImageEditor = parentImageEditor;
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06002EC0 RID: 11968
		public abstract string DialogName { get; }

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06002EC1 RID: 11969
		public abstract string ScriptUrl { get; }

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06002EC2 RID: 11970
		public abstract string Title { get; }

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x00098C61 File Offset: 0x00096E61
		public string ExternalDialogsPath
		{
			get
			{
				return this.ParentImageEditor.ExternalDialogsPath;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x00098C6E File Offset: 0x00096E6E
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x00098C72 File Offset: 0x00096E72
		// (set) Token: 0x06002EC6 RID: 11974 RVA: 0x00098C7A File Offset: 0x00096E7A
		protected string Skin { get; set; }

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x00098C83 File Offset: 0x00096E83
		// (set) Token: 0x06002EC8 RID: 11976 RVA: 0x00098C8B File Offset: 0x00096E8B
		protected RadImageEditor ParentImageEditor { get; set; }

		// Token: 0x06002EC9 RID: 11977 RVA: 0x00098C94 File Offset: 0x00096E94
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			string resourceContent = this.GetResourceContent();
			this.Controls.Add(this.Page.ParseControl(resourceContent));
			this.SetChildrensProperties();
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x00098CCB File Offset: 0x00096ECB
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x00098CDA File Offset: 0x00096EDA
		protected override void OnPreRender(EventArgs e)
		{
			this.ApplySkin(this, this.ParentImageEditor.Skin);
			base.OnPreRender(e);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x00098CF5 File Offset: 0x00096EF5
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ParentImageEditor.ToolsLoadPanelType == ToolsLoadPanelTypes.XmlHttpPanel)
			{
				this.ApplySkin(this, this.ParentImageEditor.Skin);
			}
			base.Render(writer);
			this.RenderScript(writer);
			this.RenderTitle(writer);
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x00098D2C File Offset: 0x00096F2C
		protected virtual void RenderTitle(HtmlTextWriter writer)
		{
			writer.Write("<script type=\"text/javascript\">var {0}_dockTitle = '{1}';</script>", this.ParentImageEditor.ClientID, HttpUtility.HtmlEncode(this.Title));
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x00098D50 File Offset: 0x00096F50
		protected virtual void RenderScript(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(this.ScriptUrl))
			{
				return;
			}
			writer.AddAttribute("src", base.ResolveUrl(this.ScriptUrl));
			writer.AddAttribute("type", "text/javascript");
			writer.RenderBeginTag("script");
			writer.RenderEndTag();
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x00098DA4 File Offset: 0x00096FA4
		protected virtual void ApplySkin(Control target, string skin)
		{
			if (!target.Visible)
			{
				return;
			}
			foreach (object obj in target.Controls)
			{
				Control control = (Control)obj;
				ISkinnableControl skinnableControl = control as ISkinnableControl;
				if (skinnableControl != null)
				{
					skinnableControl.EnableEmbeddedBaseStylesheet = this.ParentImageEditor.EnableEmbeddedBaseStylesheet;
					skinnableControl.EnableEmbeddedSkins = this.ParentImageEditor.EnableEmbeddedSkins;
					skinnableControl.EnableEmbeddedScripts = this.ParentImageEditor.EnableEmbeddedScripts;
					if (skin != "Default")
					{
						skinnableControl.Skin = skin;
					}
				}
				this.ApplySkin(control, skin);
			}
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x00098E58 File Offset: 0x00097058
		protected bool IsTouchSkin()
		{
			return this.Skin.EndsWith("Touch");
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x00098E6A File Offset: 0x0009706A
		protected virtual void SetChildrensProperties()
		{
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x00098E6C File Offset: 0x0009706C
		private string GetResourceContent()
		{
			return this.GetResourceContent(this.DialogName);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x00098E7C File Offset: 0x0009707C
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
				if (text.Length == 0)
				{
					string format = "Telerik.Web.UI.ImageEditor.ToolControls.{0}.ascx";
					using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(string.Format(format, name)))
					{
						byte[] array = new byte[manifestResourceStream.Length];
						manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
						text = utf.GetString(array);
					}
				}
			}
			text = text.Replace("Assembly=\"Telerik.Web.UI\"", string.Format("Assembly=\"Telerik.Web.UI{0}\"", ", Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4".Substring(20)));
			return text;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x00098FA0 File Offset: 0x000971A0
		public Control FindControlRecursive(string id)
		{
			this.EnsureChildControls();
			return this.FindControlRecursiveInt(this, id);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x00098FC0 File Offset: 0x000971C0
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

		// Token: 0x06002ED6 RID: 11990 RVA: 0x00099004 File Offset: 0x00097204
		public void SetChildControlRenderMode(string id)
		{
			Control control = this.FindControlRecursive(id);
			this.SetChildControlRenderMode(control as RadWebControl);
			this.SetChildControlRenderMode(control as ControlItemContainer);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x00099031 File Offset: 0x00097231
		public void SetChildControlRenderMode(RadWebControl control)
		{
			if (control != null)
			{
				control.RenderMode = this.ParentImageEditor.RenderMode;
			}
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x00099047 File Offset: 0x00097247
		public void SetChildControlRenderMode(RadDataBoundControl control)
		{
			if (control != null)
			{
				control.RenderMode = this.ParentImageEditor.RenderMode;
			}
		}
	}
}
