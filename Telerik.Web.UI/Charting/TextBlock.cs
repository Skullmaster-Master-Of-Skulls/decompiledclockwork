using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200170E RID: 5902
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[DefaultProperty("Text")]
	public class TextBlock : LayoutElement
	{
		// Token: 0x170045E7 RID: 17895
		// (get) Token: 0x0600E56C RID: 58732 RVA: 0x0032F746 File Offset: 0x0032D946
		// (set) Token: 0x0600E56D RID: 58733 RVA: 0x0032F753 File Offset: 0x0032D953
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public virtual bool Visible
		{
			get
			{
				return this.appearance.Visible;
			}
			set
			{
				this.appearance.Visible = value;
			}
		}

		// Token: 0x170045E8 RID: 17896
		// (get) Token: 0x0600E56E RID: 58734 RVA: 0x0032F761 File Offset: 0x0032D961
		// (set) Token: 0x0600E56F RID: 58735 RVA: 0x0032F769 File Offset: 0x0032D969
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		public ChartBaseLabel Parent
		{
			get
			{
				return this.textBlockParent;
			}
			set
			{
				this.textBlockParent = value;
			}
		}

		// Token: 0x170045E9 RID: 17897
		// (get) Token: 0x0600E570 RID: 58736 RVA: 0x0032F772 File Offset: 0x0032D972
		// (set) Token: 0x0600E571 RID: 58737 RVA: 0x0032F794 File Offset: 0x0032D994
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue("")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? this.DEFAULT_TEXT);
			}
			set
			{
				string oldText = (string)(base.ViewState["Text"] ?? this.DEFAULT_TEXT);
				base.ViewState["Text"] = value;
				this.textBlockWrappedText = value;
				this.CheckToolTip(oldText);
			}
		}

		// Token: 0x170045EA RID: 17898
		// (get) Token: 0x0600E572 RID: 58738 RVA: 0x0032F7E0 File Offset: 0x0032D9E0
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public StyleTextBlock Appearance
		{
			get
			{
				return (StyleTextBlock)this.appearance;
			}
		}

		// Token: 0x170045EB RID: 17899
		// (get) Token: 0x0600E573 RID: 58739 RVA: 0x0032F7F0 File Offset: 0x0032D9F0
		internal string VisibleText
		{
			get
			{
				if (string.IsNullOrEmpty(this.textBlockWrappedText))
				{
					this.textBlockWrappedText = this.Text;
				}
				int num = Math.Min(this.Appearance.MaxLength, this.textBlockCalculatedMaxLength);
				if (this.textBlockWrappedText.Length > num)
				{
					return this.textBlockWrappedText.Substring(0, num) + "...";
				}
				return this.textBlockWrappedText;
			}
		}

		// Token: 0x0600E574 RID: 58740 RVA: 0x0032F859 File Offset: 0x0032DA59
		public TextBlock() : this(null, null, new StyleTextBlock(), null)
		{
		}

		// Token: 0x0600E575 RID: 58741 RVA: 0x0032F869 File Offset: 0x0032DA69
		public TextBlock(StyleTextBlock appearance) : this(null, null, appearance, null)
		{
		}

		// Token: 0x0600E576 RID: 58742 RVA: 0x0032F875 File Offset: 0x0032DA75
		public TextBlock(string text) : this(null, null, new StyleTextBlock(), text)
		{
		}

		// Token: 0x0600E577 RID: 58743 RVA: 0x0032F885 File Offset: 0x0032DA85
		public TextBlock(StyleTextBlock appearance, string text) : this(null, null, appearance, text)
		{
		}

		// Token: 0x0600E578 RID: 58744 RVA: 0x0032F891 File Offset: 0x0032DA91
		public TextBlock(ChartBaseLabel parent, IContainer container) : this(parent, container, new StyleTextBlock(), null)
		{
		}

		// Token: 0x0600E579 RID: 58745 RVA: 0x0032F8A1 File Offset: 0x0032DAA1
		public TextBlock(ChartBaseLabel parent, IContainer container, string text) : this(parent, container, new StyleTextBlock(), text)
		{
		}

		// Token: 0x0600E57A RID: 58746 RVA: 0x0032F8B1 File Offset: 0x0032DAB1
		public TextBlock(ChartBaseLabel parent, IContainer container, StyleTextBlock appearance) : this(parent, container, appearance, null)
		{
		}

		// Token: 0x0600E57B RID: 58747 RVA: 0x0032F8C0 File Offset: 0x0032DAC0
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TextBlock(ChartBaseLabel parent, IContainer container, StyleTextBlock appearance, string text) : base(appearance, container)
		{
			this.textBlockParent = parent;
			this.textBlockWrappedText = string.Empty;
			if (text != null)
			{
				this.Text = text;
			}
			this.Appearance.MaxLengthChanged += this.textBlockAppearance_MaxLengthChanged;
			this.textBlockCalculatedMaxLength = int.MaxValue;
			this.DEFAULT_TEXT = string.Empty;
		}

		// Token: 0x170045EC RID: 17900
		// (get) Token: 0x0600E57C RID: 58748 RVA: 0x0032F920 File Offset: 0x0032DB20
		internal bool IsVisible
		{
			get
			{
				return !string.IsNullOrEmpty(this.Text) && this.Visible;
			}
		}

		// Token: 0x0600E57D RID: 58749 RVA: 0x0032F937 File Offset: 0x0032DB37
		internal void CheckToolTip()
		{
			this.CheckToolTip(this.Text);
		}

		// Token: 0x0600E57E RID: 58750 RVA: 0x0032F948 File Offset: 0x0032DB48
		internal void CheckToolTip(string oldText)
		{
			if (this.Text.Length > this.Appearance.MaxLength)
			{
				if (this.textBlockParent != null && (string.IsNullOrEmpty(this.textBlockParent.ActiveRegion.Tooltip) || string.Compare(oldText, this.textBlockParent.ActiveRegion.Tooltip, false) == 0))
				{
					this.textBlockParent.ActiveRegion.Tooltip = this.Text;
					return;
				}
			}
			else if (string.Compare(oldText, this.textBlockParent.ActiveRegion.Tooltip, false) == 0)
			{
				this.textBlockParent.ActiveRegion.Tooltip = string.Empty;
			}
		}

		// Token: 0x0600E57F RID: 58751 RVA: 0x0032F9EA File Offset: 0x0032DBEA
		internal void textBlockAppearance_MaxLengthChanged(object sender, EventArgs e)
		{
			this.CheckToolTip();
		}

		// Token: 0x0600E580 RID: 58752 RVA: 0x0032F9F4 File Offset: 0x0032DBF4
		internal virtual SizeF Measure(RenderEngine renderEngine)
		{
			if (!this.IsVisible)
			{
				return default(SizeF);
			}
			SizeF result = this.Appearance.Dimensions.AutoSize ? renderEngine.graphics.MeasureString(this.VisibleText, this.Appearance.TextProperties.Font) : new SizeF(this.Appearance.Dimensions.Width.PixelValue, this.Appearance.Dimensions.Height.PixelValue);
			if (this.Appearance.Dimensions.AutoSize)
			{
				result.Height += this.Appearance.dimensions.Paddings.Top.PixelValue + this.Appearance.dimensions.Paddings.Bottom.PixelValue;
				result.Width += this.Appearance.dimensions.Paddings.Left.PixelValue + this.Appearance.dimensions.Paddings.Right.PixelValue;
			}
			return result;
		}

		// Token: 0x0600E581 RID: 58753 RVA: 0x0032FB14 File Offset: 0x0032DD14
		internal override void CalculatePosition(RenderEngine renderEngine)
		{
			this.Appearance.SetStringFormat();
			base.CalculatePosition(renderEngine);
		}

		// Token: 0x0600E582 RID: 58754 RVA: 0x0032FB28 File Offset: 0x0032DD28
		public void CopyFrom(TextBlock textBlock)
		{
			base.ViewState = textBlock.CloneState();
			this.appearance = (StyleTextBlock)textBlock.Appearance.Clone();
			this.objectContainer = textBlock.Container;
			this.textBlockParent = textBlock.Parent;
		}

		// Token: 0x0400420D RID: 16909
		protected int textBlockCalculatedMaxLength;

		// Token: 0x0400420E RID: 16910
		internal WrapContext textBlockWrapContext;

		// Token: 0x0400420F RID: 16911
		internal string textBlockWrappedText;

		// Token: 0x04004210 RID: 16912
		protected ChartBaseLabel textBlockParent;

		// Token: 0x04004211 RID: 16913
		protected string DEFAULT_TEXT;
	}
}
