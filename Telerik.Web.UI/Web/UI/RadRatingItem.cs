using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019CE RID: 6606
	[ToolboxItem(false)]
	[DefaultProperty("ImageUrl")]
	public class RadRatingItem : StateManager
	{
		// Token: 0x17004D0B RID: 19723
		// (get) Token: 0x0600FF4F RID: 65359 RVA: 0x00395007 File Offset: 0x00393207
		// (set) Token: 0x0600FF50 RID: 65360 RVA: 0x0039500F File Offset: 0x0039320F
		internal RadRating Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		// Token: 0x0600FF51 RID: 65361 RVA: 0x00395018 File Offset: 0x00393218
		public RadRatingItem()
		{
		}

		// Token: 0x0600FF52 RID: 65362 RVA: 0x00395020 File Offset: 0x00393220
		public RadRatingItem(string imageUrl) : this()
		{
			this.ImageUrl = imageUrl;
		}

		// Token: 0x0600FF53 RID: 65363 RVA: 0x0039502F File Offset: 0x0039322F
		public RadRatingItem(string imageUrl, string selectedImageUrl) : this()
		{
			this.ImageUrl = imageUrl;
			this.SelectedImageUrl = selectedImageUrl;
		}

		// Token: 0x0600FF54 RID: 65364 RVA: 0x00395045 File Offset: 0x00393245
		public RadRatingItem(string imageUrl, string selectedImageUrl, string hoveredImageUrl) : this()
		{
			this.ImageUrl = imageUrl;
			this.SelectedImageUrl = selectedImageUrl;
			this.HoveredImageUrl = hoveredImageUrl;
		}

		// Token: 0x0600FF55 RID: 65365 RVA: 0x00395062 File Offset: 0x00393262
		public RadRatingItem(string imageUrl, string selectedImageUrl, string hoveredImageUrl, string hoveredSelectedImageUrl) : this()
		{
			this.ImageUrl = imageUrl;
			this.SelectedImageUrl = selectedImageUrl;
			this.HoveredImageUrl = hoveredImageUrl;
			this.HoveredSelectedImageUrl = hoveredSelectedImageUrl;
		}

		// Token: 0x0600FF56 RID: 65366 RVA: 0x00395088 File Offset: 0x00393288
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x0600FF57 RID: 65367 RVA: 0x003950A8 File Offset: 0x003932A8
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
		}

		// Token: 0x17004D0C RID: 19724
		// (get) Token: 0x0600FF58 RID: 65368 RVA: 0x003950C5 File Offset: 0x003932C5
		// (set) Token: 0x0600FF59 RID: 65369 RVA: 0x003950F5 File Offset: 0x003932F5
		[Description("The value of the Item.")]
		[Category("Behavior")]
		public decimal Value
		{
			get
			{
				return (decimal)(base.ViewState["Value"] ?? (++this.Index));
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x17004D0D RID: 19725
		// (get) Token: 0x0600FF5A RID: 65370 RVA: 0x00395110 File Offset: 0x00393310
		public int Index
		{
			get
			{
				RadRating radRating = this.Owner;
				int num = -1;
				if (radRating != null)
				{
					RadRatingItemCollection items = radRating.Items;
					num = ((items != null) ? items.IndexOf(this) : num);
				}
				return num;
			}
		}

		// Token: 0x17004D0E RID: 19726
		// (get) Token: 0x0600FF5B RID: 65371 RVA: 0x0039513F File Offset: 0x0039333F
		// (set) Token: 0x0600FF5C RID: 65372 RVA: 0x0039515F File Offset: 0x0039335F
		[Description("The tooltip for the Item.")]
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		public string ToolTip
		{
			get
			{
				return ((string)base.ViewState["ToolTip"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17004D0F RID: 19727
		// (get) Token: 0x0600FF5D RID: 65373 RVA: 0x00395172 File Offset: 0x00393372
		// (set) Token: 0x0600FF5E RID: 65374 RVA: 0x00395192 File Offset: 0x00393392
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The CSS class for the Item.")]
		public string CssClass
		{
			get
			{
				return ((string)base.ViewState["CssClass"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x17004D10 RID: 19728
		// (get) Token: 0x0600FF5F RID: 65375 RVA: 0x003951A5 File Offset: 0x003933A5
		// (set) Token: 0x0600FF60 RID: 65376 RVA: 0x003951C5 File Offset: 0x003933C5
		[UrlProperty]
		[Description("The URL for the image for the Item.")]
		[Category("Appearance")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				return ((string)base.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17004D11 RID: 19729
		// (get) Token: 0x0600FF61 RID: 65377 RVA: 0x003951D8 File Offset: 0x003933D8
		// (set) Token: 0x0600FF62 RID: 65378 RVA: 0x003951F8 File Offset: 0x003933F8
		[UrlProperty]
		[Category("Appearance")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("The path to an image to display for the item when it is hovered.")]
		public string HoveredImageUrl
		{
			get
			{
				return ((string)base.ViewState["HoveredImageUrl"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["HoveredImageUrl"] = value;
			}
		}

		// Token: 0x17004D12 RID: 19730
		// (get) Token: 0x0600FF63 RID: 65379 RVA: 0x0039520B File Offset: 0x0039340B
		// (set) Token: 0x0600FF64 RID: 65380 RVA: 0x0039522B File Offset: 0x0039342B
		[Description("The path to an image to display for the item when it is selected.")]
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		[Category("Appearance")]
		public string SelectedImageUrl
		{
			get
			{
				return ((string)base.ViewState["SelectedImageUrl"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["SelectedImageUrl"] = value;
			}
		}

		// Token: 0x17004D13 RID: 19731
		// (get) Token: 0x0600FF65 RID: 65381 RVA: 0x0039523E File Offset: 0x0039343E
		// (set) Token: 0x0600FF66 RID: 65382 RVA: 0x0039525E File Offset: 0x0039345E
		[Editor("Telerik.Web.Design.ControlItemImageUrlEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("The path to an image to display for the selected item when it is hovered.")]
		public string HoveredSelectedImageUrl
		{
			get
			{
				return ((string)base.ViewState["HoveredSelectedImageUrl"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["HoveredSelectedImageUrl"] = value;
			}
		}

		// Token: 0x0400485E RID: 18526
		private RadRating owner;
	}
}
