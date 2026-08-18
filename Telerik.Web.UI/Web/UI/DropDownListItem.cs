using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B17 RID: 2839
	[ToolboxItem(false)]
	[XmlRoot("Item")]
	public class DropDownListItem : ControlItem, IComparable
	{
		// Token: 0x06006A33 RID: 27187 RVA: 0x0018E8E4 File Offset: 0x0018CAE4
		public DropDownListItem()
		{
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x0018E8EC File Offset: 0x0018CAEC
		public DropDownListItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x0018E8FB File Offset: 0x0018CAFB
		public DropDownListItem(string text, string value) : this()
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x06006A36 RID: 27190 RVA: 0x0018E911 File Offset: 0x0018CB11
		// (set) Token: 0x06006A37 RID: 27191 RVA: 0x0018E919 File Offset: 0x0018CB19
		[Browsable(false)]
		public RadDropDownList DropDownList { get; internal set; }

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x06006A38 RID: 27192 RVA: 0x0018E922 File Offset: 0x0018CB22
		// (set) Token: 0x06006A39 RID: 27193 RVA: 0x0018E943 File Offset: 0x0018CB43
		[Category("Behavior")]
		[Description("Whether the item is selected or not.")]
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				return (bool)(this.ViewState["Selected"] ?? false);
			}
			set
			{
				this.ViewState["Selected"] = value;
			}
		}

		// Token: 0x170022C2 RID: 8898
		// (get) Token: 0x06006A3A RID: 27194 RVA: 0x0018E95B File Offset: 0x0018CB5B
		// (set) Token: 0x06006A3B RID: 27195 RVA: 0x0018E97B File Offset: 0x0018CB7B
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Appearance")]
		[Description("The URL for the image for the Item.")]
		[UrlProperty]
		[ClientPersistedProperty]
		public string ImageUrl
		{
			get
			{
				return ((string)this.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x0018E98E File Offset: 0x0018CB8E
		public void Remove()
		{
			if (this.DropDownList == null)
			{
				return;
			}
			this.DropDownList.Items.Remove(this);
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x0018E9AC File Offset: 0x0018CBAC
		public int CompareTo(object obj)
		{
			DropDownListItem dropDownListItem = obj as DropDownListItem;
			if (dropDownListItem == null)
			{
				throw new ArgumentException();
			}
			DropDownListItem dropDownListItem2 = dropDownListItem;
			return string.Compare(this.Text, dropDownListItem2.Text, true);
		}

		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x06006A3E RID: 27198 RVA: 0x0018E9E0 File Offset: 0x0018CBE0
		internal IRenderer Renderer
		{
			get
			{
				if (this._renderer != null)
				{
					return this._renderer;
				}
				return this._renderer = new DropDownListItemRenderer(this);
			}
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x0018EA0B File Offset: 0x0018CC0B
		protected override ControlItemCollection CreateChildItemCollection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170022C4 RID: 8900
		// (get) Token: 0x06006A40 RID: 27200 RVA: 0x0018EA12 File Offset: 0x0018CC12
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x0018EA1F File Offset: 0x0018CC1F
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x0018EA2D File Offset: 0x0018CC2D
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Templated || this.Controls.IsReadOnly)
			{
				this.RenderTemplate(writer);
				return;
			}
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x0018EA58 File Offset: 0x0018CC58
		protected void RenderTemplate(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}

		// Token: 0x04001CBE RID: 7358
		private IRenderer _renderer;
	}
}
