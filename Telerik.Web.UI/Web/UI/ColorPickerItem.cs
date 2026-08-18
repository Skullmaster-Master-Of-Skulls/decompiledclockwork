using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI
{
	// Token: 0x0200106C RID: 4204
	public class ColorPickerItem : StateManager
	{
		// Token: 0x0600A995 RID: 43413 RVA: 0x0024D727 File Offset: 0x0024B927
		public ColorPickerItem()
		{
		}

		// Token: 0x0600A996 RID: 43414 RVA: 0x0024D72F File Offset: 0x0024B92F
		public ColorPickerItem(Color value)
		{
			this.Value = value;
		}

		// Token: 0x0600A997 RID: 43415 RVA: 0x0024D73E File Offset: 0x0024B93E
		internal ColorPickerItem(Color value, bool isPresetColor)
		{
			this.Value = value;
			this.IsPresetColor = isPresetColor;
		}

		// Token: 0x0600A998 RID: 43416 RVA: 0x0024D754 File Offset: 0x0024B954
		public ColorPickerItem(Color value, string title) : this(value)
		{
			this.Title = title;
		}

		// Token: 0x17003679 RID: 13945
		// (get) Token: 0x0600A999 RID: 43417 RVA: 0x0024D764 File Offset: 0x0024B964
		// (set) Token: 0x0600A99A RID: 43418 RVA: 0x0024D785 File Offset: 0x0024B985
		internal bool IsPresetColor
		{
			get
			{
				return (bool)(base.ViewState["IsPresetColor"] ?? false);
			}
			set
			{
				base.ViewState["IsPresetColor"] = value;
			}
		}

		// Token: 0x1700367A RID: 13946
		// (get) Token: 0x0600A99B RID: 43419 RVA: 0x0024D79D File Offset: 0x0024B99D
		// (set) Token: 0x0600A99C RID: 43420 RVA: 0x0024D7A5 File Offset: 0x0024B9A5
		[Browsable(false)]
		[DefaultValue(0)]
		[Description("Gets or sets the index of the ColorPickerItem.")]
		public int Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x1700367B RID: 13947
		// (get) Token: 0x0600A99D RID: 43421 RVA: 0x0024D7AE File Offset: 0x0024B9AE
		// (set) Token: 0x0600A99E RID: 43422 RVA: 0x0024D7D4 File Offset: 0x0024B9D4
		[Description("Gets or sets the tooltip text of the ColorPickerItem.")]
		public string Title
		{
			get
			{
				return ((string)base.ViewState["Title"]) ?? ColorTranslator.ToHtml(this.Value);
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x1700367C RID: 13948
		// (get) Token: 0x0600A99F RID: 43423 RVA: 0x0024D7E7 File Offset: 0x0024B9E7
		// (set) Token: 0x0600A9A0 RID: 43424 RVA: 0x0024D80C File Offset: 0x0024BA0C
		[Description("Gets or sets the Color value of the ColorPickerItem.")]
		public Color Value
		{
			get
			{
				return (Color)(base.ViewState["Value"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x04002DB7 RID: 11703
		private int _index;
	}
}
