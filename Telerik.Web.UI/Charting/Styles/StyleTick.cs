using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017EF RID: 6127
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class StyleTick : LineStyle
	{
		// Token: 0x1700482D RID: 18477
		// (get) Token: 0x0600EE76 RID: 61046 RVA: 0x00364FD1 File Offset: 0x003631D1
		// (set) Token: 0x0600EE77 RID: 61047 RVA: 0x00364FF2 File Offset: 0x003631F2
		[DefaultValue(5)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		public virtual int Length
		{
			get
			{
				return (int)(base.ViewState["Length"] ?? 5);
			}
			set
			{
				base.ViewState["Length"] = value;
			}
		}

		// Token: 0x1700482E RID: 18478
		// (get) Token: 0x0600EE78 RID: 61048 RVA: 0x0036500A File Offset: 0x0036320A
		// (set) Token: 0x0600EE79 RID: 61049 RVA: 0x0036502F File Offset: 0x0036322F
		[TypeConverter(typeof(ColorConverter))]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "160, 160, 160")]
		[SkinnableProperty]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_TICK_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x1700482F RID: 18479
		// (get) Token: 0x0600EE7A RID: 61050 RVA: 0x00365038 File Offset: 0x00363238
		// (set) Token: 0x0600EE7B RID: 61051 RVA: 0x0036505D File Offset: 0x0036325D
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DefaultValue(1f)]
		public override float Width
		{
			get
			{
				return (float)(base.ViewState["Width"] ?? 1f);
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x17004830 RID: 18480
		internal override object this[StyleProperties name]
		{
			get
			{
				if (name == StyleProperties.Length)
				{
					return this.Length;
				}
				return base[name];
			}
		}

		// Token: 0x0600EE7D RID: 61053 RVA: 0x0036508F File Offset: 0x0036328F
		public StyleTick()
		{
		}

		// Token: 0x0600EE7E RID: 61054 RVA: 0x00365097 File Offset: 0x00363297
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleTick(int length) : this()
		{
			this.Length = length;
		}

		// Token: 0x0600EE7F RID: 61055 RVA: 0x003650A6 File Offset: 0x003632A6
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleTick(int length, bool visible) : this(length)
		{
			this.Visible = visible;
		}

		// Token: 0x0600EE80 RID: 61056 RVA: 0x003650B6 File Offset: 0x003632B6
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleTick(bool visible, int length, Color color) : this(length, visible)
		{
			this.Color = color;
		}

		// Token: 0x0600EE81 RID: 61057 RVA: 0x003650C7 File Offset: 0x003632C7
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_TICK_COLOR;
			this.Width = 1f;
			this.Length = 5;
		}
	}
}
