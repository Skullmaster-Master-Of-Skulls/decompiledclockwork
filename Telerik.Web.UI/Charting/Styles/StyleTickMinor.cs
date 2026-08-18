using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017F0 RID: 6128
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleTickMinor : StyleTick
	{
		// Token: 0x17004831 RID: 18481
		// (get) Token: 0x0600EE82 RID: 61058 RVA: 0x003650EC File Offset: 0x003632EC
		// (set) Token: 0x0600EE83 RID: 61059 RVA: 0x00365111 File Offset: 0x00363311
		[DefaultValue(typeof(int), "3")]
		[NotifyParentProperty(true)]
		public int MinorTickCount
		{
			get
			{
				return (int)(base.ViewState["MinorTickCount"] ?? DefaultValues.DEFAULT_MINOR_TICK_COUNT);
			}
			set
			{
				base.ViewState["MinorTickCount"] = value;
			}
		}

		// Token: 0x17004832 RID: 18482
		// (get) Token: 0x0600EE84 RID: 61060 RVA: 0x00365129 File Offset: 0x00363329
		// (set) Token: 0x0600EE85 RID: 61061 RVA: 0x0036514A File Offset: 0x0036334A
		[DefaultValue(2)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		public override int Length
		{
			get
			{
				return (int)(base.ViewState["Length"] ?? 2);
			}
			set
			{
				base.Length = value;
			}
		}

		// Token: 0x17004833 RID: 18483
		internal override object this[StyleProperties name]
		{
			get
			{
				if (name == StyleProperties.MinorTickCount)
				{
					return this.MinorTickCount;
				}
				return base[name];
			}
		}

		// Token: 0x0600EE87 RID: 61063 RVA: 0x0036517B File Offset: 0x0036337B
		public StyleTickMinor()
		{
		}

		// Token: 0x0600EE88 RID: 61064 RVA: 0x00365183 File Offset: 0x00363383
		public StyleTickMinor(int count) : this()
		{
			this.MinorTickCount = count;
		}

		// Token: 0x0600EE89 RID: 61065 RVA: 0x00365192 File Offset: 0x00363392
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StyleTickMinor(bool visible) : this()
		{
			this.Visible = visible;
		}

		// Token: 0x0600EE8A RID: 61066 RVA: 0x003651A1 File Offset: 0x003633A1
		public StyleTickMinor(bool visible, int length, int count) : this(visible)
		{
			base.Length = length;
			this.MinorTickCount = count;
		}

		// Token: 0x0600EE8B RID: 61067 RVA: 0x003651B8 File Offset: 0x003633B8
		internal override void Reset()
		{
			base.Reset();
			base.Length = 2;
			base.Visible = true;
			this.MinorTickCount = DefaultValues.DEFAULT_MINOR_TICK_COUNT;
		}
	}
}
