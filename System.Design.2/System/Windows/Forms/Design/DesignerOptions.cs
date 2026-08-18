using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002D7 RID: 727
	[ComVisible(true)]
	public class DesignerOptions
	{
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x000AE201 File Offset: 0x000AC401
		// (set) Token: 0x06001CE2 RID: 7394 RVA: 0x000AE20C File Offset: 0x000AC40C
		[SRCategory("DesignerOptions_LayoutSettings")]
		[SRDisplayName("DesignerOptions_GridSizeDisplayName")]
		[SRDescription("DesignerOptions_GridSizeDesc")]
		public virtual Size GridSize
		{
			get
			{
				return this.gridSize;
			}
			set
			{
				if (value.Width < 2)
				{
					value.Width = 2;
				}
				if (value.Height < 2)
				{
					value.Height = 2;
				}
				if (value.Width > 200)
				{
					value.Width = 200;
				}
				if (value.Height > 200)
				{
					value.Height = 200;
				}
				this.gridSize = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x000AE278 File Offset: 0x000AC478
		// (set) Token: 0x06001CE4 RID: 7396 RVA: 0x000AE280 File Offset: 0x000AC480
		[SRCategory("DesignerOptions_LayoutSettings")]
		[SRDisplayName("DesignerOptions_ShowGridDisplayName")]
		[SRDescription("DesignerOptions_ShowGridDesc")]
		public virtual bool ShowGrid
		{
			get
			{
				return this.showGrid;
			}
			set
			{
				this.showGrid = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x000AE289 File Offset: 0x000AC489
		// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x000AE291 File Offset: 0x000AC491
		[SRCategory("DesignerOptions_LayoutSettings")]
		[SRDisplayName("DesignerOptions_SnapToGridDisplayName")]
		[SRDescription("DesignerOptions_SnapToGridDesc")]
		public virtual bool SnapToGrid
		{
			get
			{
				return this.snapToGrid;
			}
			set
			{
				this.snapToGrid = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x000AE29A File Offset: 0x000AC49A
		// (set) Token: 0x06001CE8 RID: 7400 RVA: 0x000AE2A2 File Offset: 0x000AC4A2
		[SRCategory("DesignerOptions_LayoutSettings")]
		[SRDescription("DesignerOptions_UseSnapLines")]
		public virtual bool UseSnapLines
		{
			get
			{
				return this.useSnapLines;
			}
			set
			{
				this.useSnapLines = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x000AE2AB File Offset: 0x000AC4AB
		// (set) Token: 0x06001CEA RID: 7402 RVA: 0x000AE2B3 File Offset: 0x000AC4B3
		[SRCategory("DesignerOptions_LayoutSettings")]
		[SRDescription("DesignerOptions_UseSmartTags")]
		public virtual bool UseSmartTags
		{
			get
			{
				return this.useSmartTags;
			}
			set
			{
				this.useSmartTags = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x000AE2BC File Offset: 0x000AC4BC
		// (set) Token: 0x06001CEC RID: 7404 RVA: 0x000AE2C4 File Offset: 0x000AC4C4
		[SRDisplayName("DesignerOptions_ObjectBoundSmartTagAutoShowDisplayName")]
		[SRCategory("DesignerOptions_ObjectBoundSmartTagSettings")]
		[SRDescription("DesignerOptions_ObjectBoundSmartTagAutoShow")]
		public virtual bool ObjectBoundSmartTagAutoShow
		{
			get
			{
				return this.objectBoundSmartTagAutoShow;
			}
			set
			{
				this.objectBoundSmartTagAutoShow = value;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x000AE2CD File Offset: 0x000AC4CD
		// (set) Token: 0x06001CEE RID: 7406 RVA: 0x000AE2D5 File Offset: 0x000AC4D5
		[SRDisplayName("DesignerOptions_CodeGenDisplay")]
		[SRCategory("DesignerOptions_CodeGenSettings")]
		[SRDescription("DesignerOptions_OptimizedCodeGen")]
		public virtual bool UseOptimizedCodeGeneration
		{
			get
			{
				return this.enableComponentCache;
			}
			set
			{
				this.enableComponentCache = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x000AE2DE File Offset: 0x000AC4DE
		// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x000AE2E6 File Offset: 0x000AC4E6
		[SRDisplayName("DesignerOptions_EnableInSituEditingDisplay")]
		[SRCategory("DesignerOptions_EnableInSituEditingCat")]
		[SRDescription("DesignerOptions_EnableInSituEditingDesc")]
		[Browsable(false)]
		public virtual bool EnableInSituEditing
		{
			get
			{
				return this.enableInSituEditing;
			}
			set
			{
				this.enableInSituEditing = value;
			}
		}

		// Token: 0x0400171C RID: 5916
		private const int minGridSize = 2;

		// Token: 0x0400171D RID: 5917
		private const int maxGridSize = 200;

		// Token: 0x0400171E RID: 5918
		private bool showGrid = true;

		// Token: 0x0400171F RID: 5919
		private bool snapToGrid = true;

		// Token: 0x04001720 RID: 5920
		private Size gridSize = new Size(8, 8);

		// Token: 0x04001721 RID: 5921
		private bool useSnapLines;

		// Token: 0x04001722 RID: 5922
		private bool useSmartTags;

		// Token: 0x04001723 RID: 5923
		private bool objectBoundSmartTagAutoShow = true;

		// Token: 0x04001724 RID: 5924
		private bool enableComponentCache;

		// Token: 0x04001725 RID: 5925
		private bool enableInSituEditing = true;
	}
}
