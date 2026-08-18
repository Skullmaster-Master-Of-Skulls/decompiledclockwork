using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003EA RID: 1002
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ToolStripOverflow : ToolStripDropDown, IArrangedElement, IComponent, IDisposable
	{
		// Token: 0x06004447 RID: 17479 RVA: 0x00120EF3 File Offset: 0x0011F0F3
		public ToolStripOverflow(ToolStripItem parentItem) : base(parentItem)
		{
			if (parentItem == null)
			{
				throw new ArgumentNullException("parentItem");
			}
			this.ownerItem = (parentItem as ToolStripOverflowButton);
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x00120F18 File Offset: 0x0011F118
		protected internal override ToolStripItemCollection DisplayedItems
		{
			get
			{
				if (this.ParentToolStrip != null)
				{
					return this.ParentToolStrip.OverflowItems;
				}
				return new ToolStripItemCollection(null, false);
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x00120F42 File Offset: 0x0011F142
		public override ToolStripItemCollection Items
		{
			get
			{
				return new ToolStripItemCollection(null, false, true);
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x00120F4C File Offset: 0x0011F14C
		private ToolStrip ParentToolStrip
		{
			get
			{
				if (this.ownerItem != null)
				{
					return this.ownerItem.ParentToolStrip;
				}
				return null;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x00120F63 File Offset: 0x0011F163
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return this.DisplayedItems;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x0600444C RID: 17484 RVA: 0x0003BB15 File Offset: 0x00039D15
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				return this.ParentInternal;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x0003BB1D File Offset: 0x00039D1D
		bool IArrangedElement.ParticipatesInLayout
		{
			get
			{
				return base.GetState(2);
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x0003BB35 File Offset: 0x00039D35
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x001104DB File Offset: 0x0010E6DB
		void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			this.SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x000AFBF0 File Offset: 0x000ADDF0
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		// Token: 0x06004451 RID: 17489 RVA: 0x00120F6B File Offset: 0x0011F16B
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripOverflow.ToolStripOverflowAccessibleObject(this);
		}

		// Token: 0x06004452 RID: 17490 RVA: 0x00120F73 File Offset: 0x0011F173
		public override Size GetPreferredSize(Size constrainingSize)
		{
			constrainingSize.Width = 200;
			return base.GetPreferredSize(constrainingSize);
		}

		// Token: 0x06004453 RID: 17491 RVA: 0x00120F88 File Offset: 0x0011F188
		protected override void OnLayout(LayoutEventArgs e)
		{
			if (this.ParentToolStrip != null && this.ParentToolStrip.IsInDesignMode)
			{
				if (FlowLayout.GetFlowDirection(this) != FlowDirection.TopDown)
				{
					FlowLayout.SetFlowDirection(this, FlowDirection.TopDown);
				}
				if (FlowLayout.GetWrapContents(this))
				{
					FlowLayout.SetWrapContents(this, false);
				}
			}
			else
			{
				if (FlowLayout.GetFlowDirection(this) != FlowDirection.LeftToRight)
				{
					FlowLayout.SetFlowDirection(this, FlowDirection.LeftToRight);
				}
				if (!FlowLayout.GetWrapContents(this))
				{
					FlowLayout.SetWrapContents(this, true);
				}
			}
			base.OnLayout(e);
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x00120FF0 File Offset: 0x0011F1F0
		protected override void SetDisplayedItems()
		{
			Size size = Size.Empty;
			for (int i = 0; i < this.DisplayedItems.Count; i++)
			{
				ToolStripItem toolStripItem = this.DisplayedItems[i];
				if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
				{
					base.HasVisibleItems = true;
					size = LayoutUtils.UnionSizes(size, toolStripItem.Bounds.Size);
				}
			}
			base.SetLargestItemSize(size);
		}

		// Token: 0x04002621 RID: 9761
		internal static readonly TraceSwitch PopupLayoutDebug;

		// Token: 0x04002622 RID: 9762
		private ToolStripOverflowButton ownerItem;

		// Token: 0x0200080C RID: 2060
		internal class ToolStripOverflowAccessibleObject : ToolStrip.ToolStripAccessibleObject
		{
			// Token: 0x06006F46 RID: 28486 RVA: 0x0018CF24 File Offset: 0x0018B124
			public ToolStripOverflowAccessibleObject(ToolStripOverflow owner) : base(owner)
			{
			}

			// Token: 0x06006F47 RID: 28487 RVA: 0x00198530 File Offset: 0x00196730
			public override AccessibleObject GetChild(int index)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				return ((ToolStripOverflow)base.Owner).DisplayedItems[index].AccessibilityObject;
			}

			// Token: 0x06006F48 RID: 28488 RVA: 0x00198557 File Offset: 0x00196757
			public override int GetChildCount()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return 0;
				}
				return ((ToolStripOverflow)base.Owner).DisplayedItems.Count;
			}
		}
	}
}
