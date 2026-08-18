using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001AC5 RID: 6853
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[XmlRoot("Item")]
	public class RadSliderItem : ControlItem
	{
		// Token: 0x0601095A RID: 67930 RVA: 0x003B3109 File Offset: 0x003B1309
		public RadSliderItem()
		{
		}

		// Token: 0x0601095B RID: 67931 RVA: 0x003B3111 File Offset: 0x003B1311
		public RadSliderItem(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x0601095C RID: 67932 RVA: 0x003B3120 File Offset: 0x003B1320
		public RadSliderItem(string text, string value) : this()
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x170050A4 RID: 20644
		// (get) Token: 0x0601095D RID: 67933 RVA: 0x003B3136 File Offset: 0x003B1336
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x0601095E RID: 67934 RVA: 0x003B313A File Offset: 0x003B133A
		protected override ControlItemCollection CreateChildItemCollection()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0601095F RID: 67935 RVA: 0x003B3144 File Offset: 0x003B1344
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			if (obj == null)
			{
				obj = new object[]
				{
					obj
				};
			}
			return obj;
		}

		// Token: 0x06010960 RID: 67936 RVA: 0x003B316C File Offset: 0x003B136C
		protected override void LoadViewState(object savedState)
		{
			if (!(savedState is object[]))
			{
				base.LoadViewState(savedState);
			}
		}

		// Token: 0x06010961 RID: 67937 RVA: 0x003B318C File Offset: 0x003B138C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetCssClass());
			RadSlider owner = this.Owner;
			bool flag = owner.Orientation == Orientation.Horizontal;
			int itemLength = owner.GetItemLength(base.Index, 0, owner.Items.Count);
			Dictionary<string, int> itemsWrapperSize = owner.GetItemsWrapperSize();
			writer.AddStyleAttribute(HtmlTextWriterStyle.Height, Unit.Pixel(flag ? itemsWrapperSize["height"] : itemLength).ToString());
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, Unit.Pixel(flag ? itemLength : itemsWrapperSize["width"]).ToString());
			if (base.DesignMode && flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "relative");
			}
			base.AddAttributes(writer);
		}

		// Token: 0x06010962 RID: 67938 RVA: 0x003B3254 File Offset: 0x003B1454
		private string GetCssClass()
		{
			int index = base.Index;
			RadSlider owner = this.Owner;
			bool isDirectionReversed = owner.IsDirectionReversed;
			int count = owner.Items.Count;
			int num = isDirectionReversed ? (count - 1) : 0;
			int num2 = isDirectionReversed ? 0 : (count - 1);
			StringBuilder stringBuilder = new StringBuilder("rslItem");
			if (index == num)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append("rslItemFirst");
			}
			else if (index == num2)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append("rslItemLast");
			}
			string cssClass = this.CssClass;
			if (!string.IsNullOrEmpty(cssClass))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(cssClass);
			}
			if (!this.Enabled)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append("rslItemDisabled");
			}
			bool isSelectionRangeEnabled = owner.IsSelectionRangeEnabled;
			if ((isSelectionRangeEnabled && (index == owner.SelectionStart || index == owner.SelectionEnd)) || (!isSelectionRangeEnabled && index == owner.Value))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append("rslItemSelected");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06010963 RID: 67939 RVA: 0x003B3398 File Offset: 0x003B1598
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode && this.Owner.Orientation == Orientation.Horizontal)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "0");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.Text);
			writer.RenderEndTag();
		}

		// Token: 0x170050A5 RID: 20645
		// (get) Token: 0x06010964 RID: 67940 RVA: 0x003B33FB File Offset: 0x003B15FB
		// (set) Token: 0x06010965 RID: 67941 RVA: 0x003B3403 File Offset: 0x003B1603
		[DefaultValue("")]
		[Description("The display text of the item.")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x170050A6 RID: 20646
		// (get) Token: 0x06010966 RID: 67942 RVA: 0x003B340C File Offset: 0x003B160C
		// (set) Token: 0x06010967 RID: 67943 RVA: 0x003B3414 File Offset: 0x003B1614
		[Category("Misc")]
		[Description("The value of the slider item")]
		[DefaultValue("")]
		public override string Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
			}
		}

		// Token: 0x170050A7 RID: 20647
		// (get) Token: 0x06010968 RID: 67944 RVA: 0x003B341D File Offset: 0x003B161D
		// (set) Token: 0x06010969 RID: 67945 RVA: 0x003B3425 File Offset: 0x003B1625
		[Browsable(false)]
		public RadSlider Owner
		{
			get
			{
				return this._owner;
			}
			internal set
			{
				this._owner = value;
			}
		}

		// Token: 0x170050A8 RID: 20648
		// (get) Token: 0x0601096A RID: 67946 RVA: 0x003B342E File Offset: 0x003B162E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadSlider SliderParent
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x170050A9 RID: 20649
		// (get) Token: 0x0601096B RID: 67947 RVA: 0x003B3438 File Offset: 0x003B1638
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool Selected
		{
			get
			{
				bool result = false;
				if (this.SliderParent.IsSelectionRangeEnabled)
				{
					if (this.SliderParent.SelectionStart == base.Index || this.SliderParent.SelectionEnd == base.Index)
					{
						result = true;
					}
				}
				else if (this.SliderParent.Value == base.Index)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x170050AA RID: 20650
		// (get) Token: 0x0601096C RID: 67948 RVA: 0x003B34B2 File Offset: 0x003B16B2
		// (set) Token: 0x0601096D RID: 67949 RVA: 0x003B34D2 File Offset: 0x003B16D2
		[DefaultValue("")]
		public override string ToolTip
		{
			get
			{
				return (string)(this.ViewState["ToolTip"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x170050AB RID: 20651
		// (get) Token: 0x0601096E RID: 67950 RVA: 0x003B34E8 File Offset: 0x003B16E8
		[DefaultValue(null)]
		internal Hashtable CustomAttributes
		{
			get
			{
				Hashtable hashtable = new Hashtable();
				ICollection keys = base.Attributes.Keys;
				foreach (object obj in keys)
				{
					string key = (string)obj;
					hashtable.Add(key, base.Attributes[key]);
				}
				if (hashtable.Count > 0)
				{
					return hashtable;
				}
				return null;
			}
		}

		// Token: 0x04004A1E RID: 18974
		internal const string ItemCssClass = "rslItem";

		// Token: 0x04004A1F RID: 18975
		internal const string ItemFirstCssClass = "rslItemFirst";

		// Token: 0x04004A20 RID: 18976
		internal const string ItemLastCssClass = "rslItemLast";

		// Token: 0x04004A21 RID: 18977
		internal const string ItemSelectedCssClass = "rslItemSelected";

		// Token: 0x04004A22 RID: 18978
		internal const string ItemDisabledCssClass = "rslItemDisabled";

		// Token: 0x04004A23 RID: 18979
		private RadSlider _owner;
	}
}
