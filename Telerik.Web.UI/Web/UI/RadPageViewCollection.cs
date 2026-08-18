using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001AD8 RID: 6872
	public class RadPageViewCollection : ControlCollection
	{
		// Token: 0x06010A11 RID: 68113 RVA: 0x003B5B73 File Offset: 0x003B3D73
		public RadPageViewCollection(RadMultiPage multiPage) : base(multiPage)
		{
		}

		// Token: 0x06010A12 RID: 68114 RVA: 0x003B5B7C File Offset: 0x003B3D7C
		public void Add(RadPageView pageView)
		{
			base.Add(pageView);
		}

		// Token: 0x06010A13 RID: 68115 RVA: 0x003B5B85 File Offset: 0x003B3D85
		public void Insert(int index, RadPageView pageView)
		{
			base.AddAt(index, pageView);
		}

		// Token: 0x06010A14 RID: 68116 RVA: 0x003B5B90 File Offset: 0x003B3D90
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Add(Control child)
		{
			RadPageView radPageView = child as RadPageView;
			if (radPageView == null)
			{
				throw new ArgumentException("RadPageViewCollection must contain RadPageView objects");
			}
			this.Add(radPageView);
		}

		// Token: 0x06010A15 RID: 68117 RVA: 0x003B5BBC File Offset: 0x003B3DBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void AddAt(int index, Control child)
		{
			RadPageView radPageView = child as RadPageView;
			if (radPageView == null)
			{
				throw new ArgumentException("RadPageViewCollection must contain RadPageView objects");
			}
			this.Insert(index, radPageView);
		}

		// Token: 0x06010A16 RID: 68118 RVA: 0x003B5BE6 File Offset: 0x003B3DE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int IndexOf(Control value)
		{
			return base.IndexOf(value);
		}

		// Token: 0x06010A17 RID: 68119 RVA: 0x003B5BEF File Offset: 0x003B3DEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Remove(Control value)
		{
			base.Remove(value);
		}

		// Token: 0x06010A18 RID: 68120 RVA: 0x003B5BF8 File Offset: 0x003B3DF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Contains(Control c)
		{
			return base.Contains(c);
		}

		// Token: 0x06010A19 RID: 68121 RVA: 0x003B5C01 File Offset: 0x003B3E01
		public void Remove(RadPageView pageView)
		{
			base.Remove(pageView);
		}

		// Token: 0x06010A1A RID: 68122 RVA: 0x003B5C0A File Offset: 0x003B3E0A
		public int IndexOf(RadPageView pageView)
		{
			return base.IndexOf(pageView);
		}

		// Token: 0x170050DA RID: 20698
		public RadPageView this[int index]
		{
			get
			{
				return base[index] as RadPageView;
			}
		}
	}
}
