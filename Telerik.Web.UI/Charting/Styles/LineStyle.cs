using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200179B RID: 6043
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class LineStyle : StyleBorder
	{
		// Token: 0x17004751 RID: 18257
		// (get) Token: 0x0600EB8F RID: 60303 RVA: 0x0035A11A File Offset: 0x0035831A
		// (set) Token: 0x0600EB90 RID: 60304 RVA: 0x0035A13C File Offset: 0x0035833C
		[DefaultValue(typeof(LineCap), "NoAnchor")]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		public virtual LineCap EndCap
		{
			get
			{
				return (LineCap)(base.ViewState["EndCap"] ?? LineCap.NoAnchor);
			}
			set
			{
				base.ViewState["EndCap"] = value;
			}
		}

		// Token: 0x17004752 RID: 18258
		// (get) Token: 0x0600EB91 RID: 60305 RVA: 0x0035A154 File Offset: 0x00358354
		// (set) Token: 0x0600EB92 RID: 60306 RVA: 0x0035A176 File Offset: 0x00358376
		[SkinnableProperty]
		[DefaultValue(typeof(LineCap), "NoAnchor")]
		[NotifyParentProperty(true)]
		public virtual LineCap StartCap
		{
			get
			{
				return (LineCap)(base.ViewState["StartCap"] ?? LineCap.NoAnchor);
			}
			set
			{
				base.ViewState["StartCap"] = value;
			}
		}

		// Token: 0x17004753 RID: 18259
		internal override object this[StyleProperties name]
		{
			get
			{
				switch (name)
				{
				case StyleProperties.StartCap:
					return this.EndCap;
				case StyleProperties.EndCap:
					return this.EndCap;
				default:
					return base[name];
				}
			}
		}

		// Token: 0x0600EB94 RID: 60308 RVA: 0x0035A1D1 File Offset: 0x003583D1
		public LineStyle(object containerObject) : base(containerObject)
		{
		}

		// Token: 0x0600EB95 RID: 60309 RVA: 0x0035A1DA File Offset: 0x003583DA
		public LineStyle()
		{
		}

		// Token: 0x0600EB96 RID: 60310 RVA: 0x0035A1E2 File Offset: 0x003583E2
		public LineStyle(bool visible) : base(visible)
		{
		}

		// Token: 0x0600EB97 RID: 60311 RVA: 0x0035A1EB File Offset: 0x003583EB
		public LineStyle(Color color) : base(color)
		{
		}

		// Token: 0x0600EB98 RID: 60312 RVA: 0x0035A1F4 File Offset: 0x003583F4
		public LineStyle(Color color, float width) : base(color, width)
		{
		}

		// Token: 0x0600EB99 RID: 60313 RVA: 0x0035A1FE File Offset: 0x003583FE
		public LineStyle(Color color, float width, DashStyle penStyle) : base(color, width, penStyle)
		{
		}

		// Token: 0x0600EB9A RID: 60314 RVA: 0x0035A209 File Offset: 0x00358409
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public LineStyle(Color color, float width, DashStyle penStyle, LineCap endCap) : base(color, width, penStyle)
		{
			this.EndCap = endCap;
		}

		// Token: 0x0600EB9B RID: 60315 RVA: 0x0035A21C File Offset: 0x0035841C
		internal override void Reset()
		{
			this.Visible = true;
			this.StartCap = LineCap.NoAnchor;
			this.EndCap = LineCap.NoAnchor;
			this.PenStyle = DashStyle.Solid;
			this.Color = Color.Empty;
			this.Width = 0f;
		}

		// Token: 0x0600EB9C RID: 60316 RVA: 0x0035A254 File Offset: 0x00358454
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			LineStyle lineStyle = obj as LineStyle;
			if (lineStyle != null)
			{
				return lineStyle.Width.Equals(this.Width) && lineStyle.PenStyle.Equals(this.PenStyle) && lineStyle.Color.Equals(this.Color) && lineStyle.StartCap.Equals(this.StartCap) && lineStyle.EndCap.Equals(this.EndCap) && lineStyle.Visible == this.Visible;
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EB9D RID: 60317 RVA: 0x0035A31C File Offset: 0x0035851C
		public override int GetHashCode()
		{
			return this.Width.GetHashCode() ^ this.PenStyle.GetHashCode() ^ this.StartCap.GetHashCode() ^ this.EndCap.GetHashCode() ^ this.Color.GetHashCode() ^ this.Visible.GetHashCode();
		}

		// Token: 0x0600EB9E RID: 60318 RVA: 0x0035A390 File Offset: 0x00358590
		public override object Clone()
		{
			LineStyle lineStyle = (LineStyle)base.MemberwiseClone();
			lineStyle.ViewState = base.CloneState();
			return lineStyle;
		}
	}
}
