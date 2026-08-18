using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200179A RID: 6042
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class StyleBorder : StateManagedObject, ICloneable
	{
		// Token: 0x1700474C RID: 18252
		// (get) Token: 0x0600EB7B RID: 60283 RVA: 0x00359E1B File Offset: 0x0035801B
		// (set) Token: 0x0600EB7C RID: 60284 RVA: 0x00359E40 File Offset: 0x00358040
		[DefaultValue(typeof(Color), "")]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[TypeConverter(typeof(ColorConverter))]
		[Description("Line color")]
		public virtual Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x1700474D RID: 18253
		// (get) Token: 0x0600EB7D RID: 60285 RVA: 0x00359E58 File Offset: 0x00358058
		// (set) Token: 0x0600EB7E RID: 60286 RVA: 0x00359E79 File Offset: 0x00358079
		[SkinnableProperty]
		[DefaultValue(typeof(DashStyle), "Solid")]
		[NotifyParentProperty(true)]
		public virtual DashStyle PenStyle
		{
			get
			{
				return (DashStyle)(base.ViewState["PenStyle"] ?? DashStyle.Solid);
			}
			set
			{
				base.ViewState["PenStyle"] = value;
			}
		}

		// Token: 0x1700474E RID: 18254
		// (get) Token: 0x0600EB7F RID: 60287 RVA: 0x00359E91 File Offset: 0x00358091
		// (set) Token: 0x0600EB80 RID: 60288 RVA: 0x00359EB6 File Offset: 0x003580B6
		[DefaultValue(1f)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public virtual float Width
		{
			get
			{
				return (float)(base.ViewState["Width"] ?? 1f);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x0600EB81 RID: 60289 RVA: 0x00359ED0 File Offset: 0x003580D0
		internal bool IsVisible()
		{
			return this != null && this.Visible && this.Width > 0f && !this.Color.IsEmpty;
		}

		// Token: 0x1700474F RID: 18255
		// (get) Token: 0x0600EB82 RID: 60290 RVA: 0x00359F08 File Offset: 0x00358108
		// (set) Token: 0x0600EB83 RID: 60291 RVA: 0x00359F29 File Offset: 0x00358129
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17004750 RID: 18256
		internal virtual object this[StyleProperties name]
		{
			get
			{
				if (name == StyleProperties.Visible)
				{
					return this.Visible;
				}
				if (name == StyleProperties.Width)
				{
					return this.Width;
				}
				switch (name)
				{
				case StyleProperties.Color:
					return this.Color;
				case StyleProperties.PenStyle:
					return this.PenStyle;
				default:
					return null;
				}
			}
		}

		// Token: 0x0600EB85 RID: 60293 RVA: 0x00359FA0 File Offset: 0x003581A0
		public StyleBorder(object containerObject) : this()
		{
			this.lineStyleContainerObject = containerObject;
		}

		// Token: 0x0600EB86 RID: 60294 RVA: 0x00359FAF File Offset: 0x003581AF
		public StyleBorder()
		{
		}

		// Token: 0x0600EB87 RID: 60295 RVA: 0x00359FB7 File Offset: 0x003581B7
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleBorder(bool visible) : this()
		{
			this.Visible = visible;
		}

		// Token: 0x0600EB88 RID: 60296 RVA: 0x00359FC6 File Offset: 0x003581C6
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleBorder(Color color) : this()
		{
			this.Color = color;
		}

		// Token: 0x0600EB89 RID: 60297 RVA: 0x00359FD5 File Offset: 0x003581D5
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleBorder(Color color, float width) : this(color)
		{
			this.Width = width;
		}

		// Token: 0x0600EB8A RID: 60298 RVA: 0x00359FE5 File Offset: 0x003581E5
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleBorder(Color color, float width, DashStyle penStyle) : this(color, width)
		{
			this.PenStyle = penStyle;
		}

		// Token: 0x0600EB8B RID: 60299 RVA: 0x00359FF6 File Offset: 0x003581F6
		internal virtual void Reset()
		{
			this.Visible = true;
			this.PenStyle = DashStyle.Solid;
			this.Color = Color.Empty;
			this.Width = 1f;
		}

		// Token: 0x0600EB8C RID: 60300 RVA: 0x0035A01C File Offset: 0x0035821C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			StyleBorder styleBorder = obj as StyleBorder;
			if (styleBorder != null)
			{
				return styleBorder.Width.Equals(this.Width) && styleBorder.PenStyle.Equals(this.PenStyle) && styleBorder.Color.Equals(this.Color) && styleBorder.Visible == this.Visible;
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EB8D RID: 60301 RVA: 0x0035A0A4 File Offset: 0x003582A4
		public override int GetHashCode()
		{
			return this.Width.GetHashCode() ^ this.PenStyle.GetHashCode() ^ this.Color.GetHashCode() ^ this.Visible.GetHashCode();
		}

		// Token: 0x0600EB8E RID: 60302 RVA: 0x0035A0F4 File Offset: 0x003582F4
		public virtual object Clone()
		{
			StyleBorder styleBorder = (StyleBorder)base.MemberwiseClone();
			styleBorder.ViewState = base.CloneState();
			return styleBorder;
		}

		// Token: 0x04004419 RID: 17433
		internal object lineStyleContainerObject;
	}
}
