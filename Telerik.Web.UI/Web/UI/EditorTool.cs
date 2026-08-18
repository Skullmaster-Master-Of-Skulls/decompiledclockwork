using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x0200028C RID: 652
	public class EditorTool : EditorToolBase
	{
		// Token: 0x06001738 RID: 5944 RVA: 0x0004E3C2 File Offset: 0x0004C5C2
		public EditorTool()
		{
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0004E3CA File Offset: 0x0004C5CA
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorTool(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0004E3D9 File Offset: 0x0004C5D9
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorTool(string name, string shortCut)
		{
			this.Name = name;
			this.ShortCut = shortCut;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0004E3F0 File Offset: 0x0004C5F0
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorTool(EditorTool tool)
		{
			this.Name = tool.Name;
			this.ShortCut = tool.ShortCut;
			this.Visible = tool.Visible;
			this.Enabled = tool.Enabled;
			this.Text = tool.Text;
			this.ShowIcon = tool.ShowIcon;
			this.ShowText = tool.ShowText;
			this.RenderMode = tool.RenderMode;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0004E463 File Offset: 0x0004C663
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal EditorTool(EditorToolStrip toolStrip)
		{
			this.Name = toolStrip.Name;
			this.ShowText = toolStrip.ShowText;
			this.Text = toolStrip.Text;
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x0004E48F File Offset: 0x0004C68F
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x0004E4BA File Offset: 0x0004C6BA
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool Enabled
		{
			get
			{
				return base.ViewState["Enabled"] == null || (bool)base.ViewState["Enabled"];
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0004E4D2 File Offset: 0x0004C6D2
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x0004E4F4 File Offset: 0x0004C6F4
		[DefaultValue("")]
		[TypeConverter("Telerik.Web.Design.EditorToolNameTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[NotifyParentProperty(true)]
		public virtual string Name
		{
			get
			{
				return ((string)base.ViewState["Name"]) ?? string.Empty;
			}
			set
			{
				string commandName = EditorToolNames.GetCommandName(value);
				base.ViewState["Name"] = commandName;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0004E519 File Offset: 0x0004C719
		// (set) Token: 0x06001742 RID: 5954 RVA: 0x0004E539 File Offset: 0x0004C739
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[ScriptIgnore]
		public virtual string Text
		{
			get
			{
				return ((string)base.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0004E54C File Offset: 0x0004C74C
		// (set) Token: 0x06001744 RID: 5956 RVA: 0x0004E57B File Offset: 0x0004C77B
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string ShortCut
		{
			get
			{
				if (base.ViewState["ShortCut"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ShortCut"];
			}
			set
			{
				base.ViewState["ShortCut"] = value;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0004E58E File Offset: 0x0004C78E
		// (set) Token: 0x06001746 RID: 5958 RVA: 0x0004E5BD File Offset: 0x0004C7BD
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				if (base.ViewState["ImageUrl"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ImageUrl"];
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0004E5D0 File Offset: 0x0004C7D0
		// (set) Token: 0x06001748 RID: 5960 RVA: 0x0004E5FF File Offset: 0x0004C7FF
		[DefaultValue("")]
		public string ImageUrlLarge
		{
			get
			{
				if (base.ViewState["ImageUrlLarge"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ImageUrlLarge"];
			}
			set
			{
				base.ViewState["ImageUrlLarge"] = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x0004E612 File Offset: 0x0004C812
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x0004E63D File Offset: 0x0004C83D
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool ShowIcon
		{
			get
			{
				return base.ViewState["ShowIcon"] == null || (bool)base.ViewState["ShowIcon"];
			}
			set
			{
				base.ViewState["ShowIcon"] = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x0004E655 File Offset: 0x0004C855
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x0004E680 File Offset: 0x0004C880
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool ShowText
		{
			get
			{
				return base.ViewState["ShowText"] != null && (bool)base.ViewState["ShowText"];
			}
			set
			{
				base.ViewState["ShowText"] = value;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x0004E698 File Offset: 0x0004C898
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x0004E6C3 File Offset: 0x0004C8C3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(EditorToolType.Button)]
		[NotifyParentProperty(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override EditorToolType Type
		{
			get
			{
				if (base.ViewState["Type"] == null)
				{
					return EditorToolType.Button;
				}
				return (EditorToolType)base.ViewState["Type"];
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0004E6DB File Offset: 0x0004C8DB
		protected internal void EnsureName()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				throw new InvalidOperationException("RadEditorTool has empty command name.");
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0004E6F5 File Offset: 0x0004C8F5
		// (set) Token: 0x06001751 RID: 5969 RVA: 0x0004E6FD File Offset: 0x0004C8FD
		internal bool InToolStrip
		{
			get
			{
				return this._inToolStrip;
			}
			set
			{
				this._inToolStrip = value;
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0004E706 File Offset: 0x0004C906
		public void RenderControl(HtmlTextWriter writer)
		{
			this.Renderer.Render(writer);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0004E714 File Offset: 0x0004C914
		protected virtual void AddAnchorAttributes(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
			if (this.AnchorCssClass != this.Renderer.CssClassString)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.AnchorCssClass);
			}
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0004E748 File Offset: 0x0004C948
		protected internal void EnsureIconText()
		{
			if (!this.ShowText && !this.ShowIcon)
			{
				throw new InvalidOperationException("You cannot set both ShowIcon and ShowText to false.");
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0004E775 File Offset: 0x0004C975
		protected virtual string AnchorCssClass
		{
			get
			{
				return this.Renderer.CssClassString;
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0004E782 File Offset: 0x0004C982
		protected virtual string GetAnchorCssClass()
		{
			return this.Renderer.GetCssClassString();
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0004E78F File Offset: 0x0004C98F
		protected virtual IEditorToolRenderer Renderer
		{
			get
			{
				if (this.toolRenderer == null)
				{
					this.toolRenderer = RendererFactory.GetRenderer(this);
				}
				return this.toolRenderer;
			}
		}

		// Token: 0x04000618 RID: 1560
		private bool _inToolStrip;

		// Token: 0x04000619 RID: 1561
		protected IEditorToolRenderer toolRenderer;
	}
}
