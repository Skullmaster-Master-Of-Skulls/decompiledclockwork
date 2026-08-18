using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017F3 RID: 6131
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class TextProperties : StateManagedObject, ICloneable
	{
		// Token: 0x17004834 RID: 18484
		// (get) Token: 0x0600EE8D RID: 61069 RVA: 0x003651E1 File Offset: 0x003633E1
		// (set) Token: 0x0600EE8E RID: 61070 RVA: 0x00365206 File Offset: 0x00363406
		[TypeConverter(typeof(ColorConverter))]
		[SkinnableProperty]
		[DefaultValue(typeof(Color), "51, 51, 51")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the text color")]
		public virtual Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_TEXT_COLOR);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x17004835 RID: 18485
		// (get) Token: 0x0600EE8F RID: 61071 RVA: 0x0036521E File Offset: 0x0036341E
		// (set) Token: 0x0600EE90 RID: 61072 RVA: 0x0036523E File Offset: 0x0036343E
		[TypeConverter(typeof(FontConverter))]
		[SkinnableProperty]
		[DefaultValue(typeof(Font), "Verdana, 8.25pt")]
		[NotifyParentProperty(true)]
		public virtual Font Font
		{
			get
			{
				return (Font)(base.ViewState["Font"] ?? DefaultValues.DEFAULT_TEXT_FONT);
			}
			set
			{
				base.ViewState["Font"] = value;
			}
		}

		// Token: 0x17004836 RID: 18486
		internal object this[StyleProperties name]
		{
			get
			{
				if (name == StyleProperties.Color)
				{
					return this.Color;
				}
				if (name != StyleProperties.Font)
				{
					return null;
				}
				return this.Font;
			}
		}

		// Token: 0x0600EE92 RID: 61074 RVA: 0x00365283 File Offset: 0x00363483
		public TextProperties()
		{
		}

		// Token: 0x0600EE93 RID: 61075 RVA: 0x0036528B File Offset: 0x0036348B
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TextProperties(Color color) : this()
		{
			this.Color = color;
		}

		// Token: 0x0600EE94 RID: 61076 RVA: 0x0036529A File Offset: 0x0036349A
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TextProperties(Color color, Font font) : this(color)
		{
			this.Font = font;
		}

		// Token: 0x0600EE95 RID: 61077 RVA: 0x003652AA File Offset: 0x003634AA
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public TextProperties(Color color, string familyName, float emSize, FontStyle fontStyle, GraphicsUnit grUnit) : this()
		{
			this.Color = color;
			this.Font = new Font(familyName, emSize, fontStyle, grUnit);
		}

		// Token: 0x0600EE96 RID: 61078 RVA: 0x003652CA File Offset: 0x003634CA
		internal virtual void Reset()
		{
			this.Font = DefaultValues.DEFAULT_TEXT_FONT;
			this.Color = DefaultValues.DEFAULT_TEXT_COLOR;
		}

		// Token: 0x0600EE97 RID: 61079 RVA: 0x003652E4 File Offset: 0x003634E4
		public object Clone()
		{
			TextProperties textProperties = (TextProperties)base.MemberwiseClone();
			textProperties.ViewState = base.CloneState();
			return textProperties;
		}

		// Token: 0x0600EE98 RID: 61080 RVA: 0x0036530C File Offset: 0x0036350C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			TextProperties textProperties = obj as TextProperties;
			if (textProperties != null)
			{
				return textProperties.Color == this.Color && textProperties.Font.Equals(this.Font);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EE99 RID: 61081 RVA: 0x00365358 File Offset: 0x00363558
		public override int GetHashCode()
		{
			return this.Color.GetHashCode() ^ this.Font.GetHashCode();
		}

		// Token: 0x040044DD RID: 17629
		internal object textPropertiesContainerObject;
	}
}
