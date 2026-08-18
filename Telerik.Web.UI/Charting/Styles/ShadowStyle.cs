using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017BE RID: 6078
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class ShadowStyle : StateManagedObject, ICloneable
	{
		// Token: 0x17004792 RID: 18322
		// (get) Token: 0x0600EC86 RID: 60550 RVA: 0x0035EDD8 File Offset: 0x0035CFD8
		// (set) Token: 0x0600EC87 RID: 60551 RVA: 0x0035EDFD File Offset: 0x0035CFFD
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "0, 0, 0")]
		[TypeConverter(typeof(ColorConverter))]
		public virtual Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_SHADOW_COLOR);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x17004793 RID: 18323
		// (get) Token: 0x0600EC88 RID: 60552 RVA: 0x0035EE18 File Offset: 0x0035D018
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public byte ColorOpacity
		{
			get
			{
				return this.Color.A;
			}
		}

		// Token: 0x17004794 RID: 18324
		// (get) Token: 0x0600EC89 RID: 60553 RVA: 0x0035EE33 File Offset: 0x0035D033
		// (set) Token: 0x0600EC8A RID: 60554 RVA: 0x0035EE54 File Offset: 0x0035D054
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ShadowPosition), "BottomRight")]
		[SkinnableProperty]
		public virtual ShadowPosition Position
		{
			get
			{
				return (ShadowPosition)(base.ViewState["Position"] ?? ShadowPosition.BottomRight);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17004795 RID: 18325
		// (get) Token: 0x0600EC8B RID: 60555 RVA: 0x0035EE6C File Offset: 0x0035D06C
		// (set) Token: 0x0600EC8C RID: 60556 RVA: 0x0035EE91 File Offset: 0x0035D091
		[SkinnableProperty]
		[DefaultValue(0f)]
		[NotifyParentProperty(true)]
		public virtual float Blur
		{
			get
			{
				return (float)(base.ViewState["Blur"] ?? 0f);
			}
			set
			{
				base.ViewState["Blur"] = value;
			}
		}

		// Token: 0x17004796 RID: 18326
		// (get) Token: 0x0600EC8D RID: 60557 RVA: 0x0035EEA9 File Offset: 0x0035D0A9
		// (set) Token: 0x0600EC8E RID: 60558 RVA: 0x0035EECE File Offset: 0x0035D0CE
		[SkinnableProperty]
		[DefaultValue(0f)]
		[NotifyParentProperty(true)]
		public virtual float Distance
		{
			get
			{
				return (float)(base.ViewState["Distance"] ?? 0f);
			}
			set
			{
				base.ViewState["Distance"] = value;
			}
		}

		// Token: 0x0600EC8F RID: 60559 RVA: 0x0035EEE6 File Offset: 0x0035D0E6
		public ShadowStyle()
		{
		}

		// Token: 0x0600EC90 RID: 60560 RVA: 0x0035EEEE File Offset: 0x0035D0EE
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ShadowStyle(Color shadowColor, float shadowBlur, float shadowDistance, ShadowPosition shadowPosition)
		{
			this.Distance = shadowDistance;
			this.Blur = shadowBlur;
			this.Position = shadowPosition;
			this.Color = shadowColor;
		}

		// Token: 0x0600EC91 RID: 60561 RVA: 0x0035EF14 File Offset: 0x0035D114
		internal virtual void Reset()
		{
			this.Distance = (this.Blur = 0f);
			this.Position = ShadowPosition.BottomRight;
			this.Color = DefaultValues.DEFAULT_SHADOW_COLOR;
		}

		// Token: 0x0600EC92 RID: 60562 RVA: 0x0035EF48 File Offset: 0x0035D148
		public override bool Equals(object obj)
		{
			ShadowStyle shadowStyle = obj as ShadowStyle;
			if (shadowStyle != null)
			{
				return this.Blur.Equals(shadowStyle.Blur) && this.Color.Equals(shadowStyle.Color) && this.Distance == shadowStyle.Distance && this.Position.Equals(shadowStyle.Position);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EC93 RID: 60563 RVA: 0x0035EFCC File Offset: 0x0035D1CC
		public override int GetHashCode()
		{
			return this.Blur.GetHashCode() ^ this.Color.GetHashCode() ^ this.Distance.GetHashCode() ^ this.Position.GetHashCode();
		}

		// Token: 0x0600EC94 RID: 60564 RVA: 0x0035F01C File Offset: 0x0035D21C
		public virtual object Clone()
		{
			ShadowStyle shadowStyle = (ShadowStyle)base.MemberwiseClone();
			shadowStyle.ViewState = base.CloneState();
			return shadowStyle;
		}
	}
}
