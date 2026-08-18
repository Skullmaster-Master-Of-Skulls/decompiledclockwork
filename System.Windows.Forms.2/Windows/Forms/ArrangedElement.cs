using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000125 RID: 293
	internal abstract class ArrangedElement : Component, IArrangedElement, IComponent, IDisposable
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x00019B88 File Offset: 0x00017D88
		internal ArrangedElement()
		{
			this.Padding = this.DefaultPadding;
			this.Margin = this.DefaultMargin;
			this.state[ArrangedElement.stateVisible] = true;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00019BE5 File Offset: 0x00017DE5
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00019BED File Offset: 0x00017DED
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return this.GetChildren();
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00019BF5 File Offset: 0x00017DF5
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				return this.GetContainer();
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected virtual Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected virtual Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00019C04 File Offset: 0x00017E04
		public virtual Rectangle DisplayRectangle
		{
			get
			{
				return this.Bounds;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000968 RID: 2408
		public abstract LayoutEngine LayoutEngine { get; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00019C19 File Offset: 0x00017E19
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x00019C21 File Offset: 0x00017E21
		public Padding Margin
		{
			get
			{
				return CommonProperties.GetMargin(this);
			}
			set
			{
				value = LayoutUtils.ClampNegativePaddingToZero(value);
				if (this.Margin != value)
				{
					CommonProperties.SetMargin(this, value);
				}
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00019C40 File Offset: 0x00017E40
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00019C4E File Offset: 0x00017E4E
		public virtual Padding Padding
		{
			get
			{
				return CommonProperties.GetPadding(this, this.DefaultPadding);
			}
			set
			{
				value = LayoutUtils.ClampNegativePaddingToZero(value);
				if (this.Padding != value)
				{
					CommonProperties.SetPadding(this, value);
				}
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00019C6D File Offset: 0x00017E6D
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x00019C75 File Offset: 0x00017E75
		public virtual IArrangedElement Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00019C7E File Offset: 0x00017E7E
		public virtual bool ParticipatesInLayout
		{
			get
			{
				return this.Visible;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00019C86 File Offset: 0x00017E86
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00019C8E File Offset: 0x00017E8E
		private PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00019C96 File Offset: 0x00017E96
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00019CA8 File Offset: 0x00017EA8
		public virtual bool Visible
		{
			get
			{
				return this.state[ArrangedElement.stateVisible];
			}
			set
			{
				if (this.state[ArrangedElement.stateVisible] != value)
				{
					this.state[ArrangedElement.stateVisible] = value;
					if (this.Parent != null)
					{
						LayoutTransaction.DoLayout(this.Parent, this, PropertyNames.Visible);
					}
				}
			}
		}

		// Token: 0x06000974 RID: 2420
		protected abstract IArrangedElement GetContainer();

		// Token: 0x06000975 RID: 2421
		protected abstract ArrangedElementCollection GetChildren();

		// Token: 0x06000976 RID: 2422 RVA: 0x00019CE8 File Offset: 0x00017EE8
		public virtual Size GetPreferredSize(Size constrainingSize)
		{
			return this.LayoutEngine.GetPreferredSize(this, constrainingSize - this.Padding.Size) + this.Padding.Size;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00019D2A File Offset: 0x00017F2A
		public virtual void PerformLayout(IArrangedElement container, string propertyName)
		{
			if (this.suspendCount <= 0)
			{
				this.OnLayout(new LayoutEventArgs(container, propertyName));
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00019D44 File Offset: 0x00017F44
		protected virtual void OnLayout(LayoutEventArgs e)
		{
			bool flag = this.LayoutEngine.Layout(this, e);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00019D5F File Offset: 0x00017F5F
		protected virtual void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
		{
			((IArrangedElement)this).PerformLayout(this, PropertyNames.Size);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00019D6D File Offset: 0x00017F6D
		public void SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			this.SetBoundsCore(bounds, specified);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00019D78 File Offset: 0x00017F78
		protected virtual void SetBoundsCore(Rectangle bounds, BoundsSpecified specified)
		{
			if (bounds != this.bounds)
			{
				Rectangle oldBounds = this.bounds;
				this.bounds = bounds;
				this.OnBoundsChanged(oldBounds, bounds);
			}
		}

		// Token: 0x040005FA RID: 1530
		private Rectangle bounds = Rectangle.Empty;

		// Token: 0x040005FB RID: 1531
		private IArrangedElement parent;

		// Token: 0x040005FC RID: 1532
		private BitVector32 state;

		// Token: 0x040005FD RID: 1533
		private PropertyStore propertyStore = new PropertyStore();

		// Token: 0x040005FE RID: 1534
		private int suspendCount;

		// Token: 0x040005FF RID: 1535
		private static readonly int stateVisible = BitVector32.CreateMask();

		// Token: 0x04000600 RID: 1536
		private static readonly int stateDisposing = BitVector32.CreateMask(ArrangedElement.stateVisible);

		// Token: 0x04000601 RID: 1537
		private static readonly int stateLocked = BitVector32.CreateMask(ArrangedElement.stateDisposing);

		// Token: 0x04000602 RID: 1538
		private static readonly int PropControlsCollection = PropertyStore.CreateKey();

		// Token: 0x04000603 RID: 1539
		private Control spacer = new Control();
	}
}
