using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002EA RID: 746
	internal class GroupedContextMenuStrip : ContextMenuStrip
	{
		// Token: 0x1700066B RID: 1643
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x000B642E File Offset: 0x000B462E
		public bool Populated
		{
			set
			{
				this.populated = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x000B643F File Offset: 0x000B463F
		public ContextMenuStripGroupCollection Groups
		{
			get
			{
				if (this.groups == null)
				{
					this.groups = new ContextMenuStripGroupCollection();
				}
				return this.groups;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000B645A File Offset: 0x000B465A
		public StringCollection GroupOrdering
		{
			get
			{
				if (this.groupOrdering == null)
				{
					this.groupOrdering = new StringCollection();
				}
				return this.groupOrdering;
			}
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000B6478 File Offset: 0x000B4678
		public void Populate()
		{
			this.Items.Clear();
			foreach (string key in this.GroupOrdering)
			{
				if (this.groups.ContainsKey(key))
				{
					List<ToolStripItem> items = this.groups[key].Items;
					if (this.Items.Count > 0 && items.Count > 0)
					{
						this.Items.Add(new ToolStripSeparator());
					}
					foreach (ToolStripItem value in items)
					{
						this.Items.Add(value);
					}
				}
			}
			this.populated = true;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000B6570 File Offset: 0x000B4770
		protected override void OnOpening(CancelEventArgs e)
		{
			base.SuspendLayout();
			if (!this.populated)
			{
				this.Populate();
			}
			this.RefreshItems();
			base.ResumeLayout(true);
			base.PerformLayout();
			e.Cancel = (this.Items.Count == 0);
			base.OnOpening(e);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void RefreshItems()
		{
		}

		// Token: 0x040017B4 RID: 6068
		private StringCollection groupOrdering;

		// Token: 0x040017B5 RID: 6069
		private ContextMenuStripGroupCollection groups;

		// Token: 0x040017B6 RID: 6070
		private bool populated;
	}
}
