using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	// Token: 0x020003EB RID: 1003
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.None)]
	public class ToolStripOverflowButton : ToolStripDropDownButton
	{
		// Token: 0x06004455 RID: 17493 RVA: 0x00121054 File Offset: 0x0011F254
		internal ToolStripOverflowButton(ToolStrip parentToolStrip)
		{
			if (!ToolStripOverflowButton.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					ToolStripOverflowButton.maxWidth = DpiHelper.LogicalToDeviceUnitsX(16);
					ToolStripOverflowButton.maxHeight = DpiHelper.LogicalToDeviceUnitsY(16);
				}
				ToolStripOverflowButton.isScalingInitialized = true;
			}
			base.SupportsItemClick = false;
			this.parentToolStrip = parentToolStrip;
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x001210A1 File Offset: 0x0011F2A1
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.HasDropDownItems)
			{
				base.DropDown.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected internal override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x001210C0 File Offset: 0x0011F2C0
		public override bool HasDropDownItems
		{
			get
			{
				return base.ParentInternal.OverflowItems.Count > 0;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool OppositeDropDownAlign
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x001210D5 File Offset: 0x0011F2D5
		internal ToolStrip ParentToolStrip
		{
			get
			{
				return this.parentToolStrip;
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x00111EB0 File Offset: 0x001100B0
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x00111EB8 File Offset: 0x001100B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool RightToLeftAutoMirrorImage
		{
			get
			{
				return base.RightToLeftAutoMirrorImage;
			}
			set
			{
				base.RightToLeftAutoMirrorImage = value;
			}
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x001210DD File Offset: 0x0011F2DD
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject(this);
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x001210E5 File Offset: 0x0011F2E5
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripOverflow(this);
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x001210F0 File Offset: 0x0011F2F0
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size sz = constrainingSize;
			if (base.ParentInternal != null)
			{
				if (base.ParentInternal.Orientation == Orientation.Horizontal)
				{
					sz.Width = Math.Min(constrainingSize.Width, ToolStripOverflowButton.maxWidth);
				}
				else
				{
					sz.Height = Math.Min(constrainingSize.Height, ToolStripOverflowButton.maxHeight);
				}
			}
			return sz + this.Padding.Size;
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x0012115C File Offset: 0x0011F35C
		protected internal override void SetBounds(Rectangle bounds)
		{
			if (base.ParentInternal != null && base.ParentInternal.LayoutEngine is ToolStripSplitStackLayout)
			{
				if (base.ParentInternal.Orientation == Orientation.Horizontal)
				{
					bounds.Height = base.ParentInternal.Height;
					bounds.Y = 0;
				}
				else
				{
					bounds.Width = base.ParentInternal.Width;
					bounds.X = 0;
				}
			}
			base.SetBounds(bounds);
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x001211D0 File Offset: 0x0011F3D0
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.ParentInternal != null)
			{
				ToolStripRenderer renderer = base.ParentInternal.Renderer;
				renderer.DrawOverflowButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			}
		}

		// Token: 0x04002623 RID: 9763
		private ToolStrip parentToolStrip;

		// Token: 0x04002624 RID: 9764
		private static bool isScalingInitialized = false;

		// Token: 0x04002625 RID: 9765
		private const int MAX_WIDTH = 16;

		// Token: 0x04002626 RID: 9766
		private const int MAX_HEIGHT = 16;

		// Token: 0x04002627 RID: 9767
		private static int maxWidth = 16;

		// Token: 0x04002628 RID: 9768
		private static int maxHeight = 16;

		// Token: 0x0200080D RID: 2061
		internal class ToolStripOverflowButtonAccessibleObject : ToolStripDropDownItemAccessibleObject
		{
			// Token: 0x06006F49 RID: 28489 RVA: 0x00196A38 File Offset: 0x00194C38
			public ToolStripOverflowButtonAccessibleObject(ToolStripOverflowButton owner) : base(owner)
			{
			}

			// Token: 0x17001853 RID: 6227
			// (get) Token: 0x06006F4A RID: 28490 RVA: 0x00198578 File Offset: 0x00196778
			// (set) Token: 0x06006F4B RID: 28491 RVA: 0x00196DA3 File Offset: 0x00194FA3
			public override string Name
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return string.Empty;
					}
					string accessibleName = base.Owner.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					if (string.IsNullOrEmpty(this.stockName))
					{
						this.stockName = SR.GetString("ToolStripOptions");
					}
					return this.stockName;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x06006F4C RID: 28492 RVA: 0x001985C7 File Offset: 0x001967C7
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50011;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x0400431C RID: 17180
			private string stockName;
		}
	}
}
