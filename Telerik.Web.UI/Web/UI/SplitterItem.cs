using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000FBC RID: 4028
	[ClientScriptResource("Telerik.Web.UI.SplitterItem", "Telerik.Web.UI.Splitter.RadSplitterScripts.js")]
	public abstract class SplitterItem : RadWebControl
	{
		// Token: 0x1700315C RID: 12636
		// (get) Token: 0x06009BD9 RID: 39897 RVA: 0x0022B355 File Offset: 0x00229555
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new string Skin
		{
			get
			{
				return this._skin;
			}
		}

		// Token: 0x1700315D RID: 12637
		// (get) Token: 0x06009BDA RID: 39898 RVA: 0x0022B35D File Offset: 0x0022955D
		[DefaultValue(false)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700315E RID: 12638
		// (get) Token: 0x06009BDB RID: 39899 RVA: 0x0022B360 File Offset: 0x00229560
		[DefaultValue(false)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700315F RID: 12639
		// (get) Token: 0x06009BDC RID: 39900 RVA: 0x0022B363 File Offset: 0x00229563
		// (set) Token: 0x06009BDD RID: 39901 RVA: 0x0022B36B File Offset: 0x0022956B
		[DefaultValue(-1)]
		[Browsable(false)]
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

		// Token: 0x06009BDE RID: 39902
		internal abstract void RegisterInitializeScriptWithScriptManager();

		// Token: 0x06009BDF RID: 39903 RVA: 0x0022B374 File Offset: 0x00229574
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			RadPane radPane = this as RadPane;
			if (radPane != null)
			{
				descriptor.AddProperty("_splitterOrientation", radPane.Splitter.Orientation);
				return;
			}
			RadSplitBar radSplitBar = this as RadSplitBar;
			if (radSplitBar != null)
			{
				descriptor.AddProperty("_splitterOrientation", radSplitBar.Splitter.Orientation);
				return;
			}
			RadSlidingPane radSlidingPane = this as RadSlidingPane;
			if (radSlidingPane != null)
			{
				descriptor.AddProperty("_splitterOrientation", radSlidingPane.SlidingZone.Splitter.Orientation);
			}
		}

		// Token: 0x04002C0A RID: 11274
		private int _index = -1;

		// Token: 0x04002C0B RID: 11275
		private readonly string _skin = string.Empty;
	}
}
