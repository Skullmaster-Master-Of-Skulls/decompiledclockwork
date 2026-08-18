using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004CB RID: 1227
	internal class FlowLayout : LayoutEngine
	{
		// Token: 0x0600508B RID: 20619 RVA: 0x0014F434 File Offset: 0x0014D634
		internal static FlowLayoutSettings CreateSettings(IArrangedElement owner)
		{
			return new FlowLayoutSettings(owner);
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x0014F43C File Offset: 0x0014D63C
		internal override bool LayoutCore(IArrangedElement container, LayoutEventArgs args)
		{
			CommonProperties.SetLayoutBounds(container, this.xLayout(container, container.DisplayRectangle, false));
			return CommonProperties.GetAutoSize(container);
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x0014F458 File Offset: 0x0014D658
		internal override Size GetPreferredSize(IArrangedElement container, Size proposedConstraints)
		{
			Rectangle displayRect = new Rectangle(new Point(0, 0), proposedConstraints);
			Size size = this.xLayout(container, displayRect, true);
			if (size.Width > proposedConstraints.Width || size.Height > proposedConstraints.Height)
			{
				displayRect.Size = size;
				size = this.xLayout(container, displayRect, true);
			}
			return size;
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x0014F4B1 File Offset: 0x0014D6B1
		private static FlowLayout.ContainerProxy CreateContainerProxy(IArrangedElement container, FlowDirection flowDirection)
		{
			switch (flowDirection)
			{
			case FlowDirection.TopDown:
				return new FlowLayout.TopDownProxy(container);
			case FlowDirection.RightToLeft:
				return new FlowLayout.RightToLeftProxy(container);
			case FlowDirection.BottomUp:
				return new FlowLayout.BottomUpProxy(container);
			}
			return new FlowLayout.ContainerProxy(container);
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x0014F4E8 File Offset: 0x0014D6E8
		private Size xLayout(IArrangedElement container, Rectangle displayRect, bool measureOnly)
		{
			FlowDirection flowDirection = FlowLayout.GetFlowDirection(container);
			bool wrapContents = FlowLayout.GetWrapContents(container);
			FlowLayout.ContainerProxy containerProxy = FlowLayout.CreateContainerProxy(container, flowDirection);
			containerProxy.DisplayRect = displayRect;
			displayRect = containerProxy.DisplayRect;
			FlowLayout.ElementProxy elementProxy = containerProxy.ElementProxy;
			Size empty = Size.Empty;
			if (!wrapContents)
			{
				displayRect.Width = int.MaxValue - displayRect.X;
			}
			int num;
			for (int i = 0; i < container.Children.Count; i = num)
			{
				Size size = Size.Empty;
				Rectangle displayRectangle = new Rectangle(displayRect.X, displayRect.Y, displayRect.Width, displayRect.Height - empty.Height);
				size = this.MeasureRow(containerProxy, elementProxy, i, displayRectangle, out num);
				if (!measureOnly)
				{
					Rectangle rowBounds = new Rectangle(displayRect.X, empty.Height + displayRect.Y, size.Width, size.Height);
					this.LayoutRow(containerProxy, elementProxy, i, num, rowBounds);
				}
				empty.Width = Math.Max(empty.Width, size.Width);
				empty.Height += size.Height;
			}
			if (container.Children.Count != 0)
			{
			}
			return LayoutUtils.FlipSizeIf(flowDirection == FlowDirection.TopDown || FlowLayout.GetFlowDirection(container) == FlowDirection.BottomUp, empty);
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0014F630 File Offset: 0x0014D830
		private void LayoutRow(FlowLayout.ContainerProxy containerProxy, FlowLayout.ElementProxy elementProxy, int startIndex, int endIndex, Rectangle rowBounds)
		{
			int num;
			Size size = this.xLayoutRow(containerProxy, elementProxy, startIndex, endIndex, rowBounds, out num, false);
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x0014F64E File Offset: 0x0014D84E
		private Size MeasureRow(FlowLayout.ContainerProxy containerProxy, FlowLayout.ElementProxy elementProxy, int startIndex, Rectangle displayRectangle, out int breakIndex)
		{
			return this.xLayoutRow(containerProxy, elementProxy, startIndex, containerProxy.Container.Children.Count, displayRectangle, out breakIndex, true);
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x0014F670 File Offset: 0x0014D870
		private Size xLayoutRow(FlowLayout.ContainerProxy containerProxy, FlowLayout.ElementProxy elementProxy, int startIndex, int endIndex, Rectangle rowBounds, out int breakIndex, bool measureOnly)
		{
			Point location = rowBounds.Location;
			Size empty = Size.Empty;
			int num = 0;
			breakIndex = startIndex;
			bool wrapContents = FlowLayout.GetWrapContents(containerProxy.Container);
			bool flag = false;
			ArrangedElementCollection children = containerProxy.Container.Children;
			int i = startIndex;
			while (i < endIndex)
			{
				elementProxy.Element = children[i];
				if (elementProxy.ParticipatesInLayout)
				{
					Size size2;
					if (elementProxy.AutoSize)
					{
						Size size = new Size(int.MaxValue, rowBounds.Height - elementProxy.Margin.Size.Height);
						if (i == startIndex)
						{
							size.Width = rowBounds.Width - empty.Width - elementProxy.Margin.Size.Width;
						}
						size = LayoutUtils.UnionSizes(new Size(1, 1), size);
						size2 = elementProxy.GetPreferredSize(size);
					}
					else
					{
						size2 = elementProxy.SpecifiedSize;
						if (elementProxy.Stretches)
						{
							size2.Height = 0;
						}
						if (size2.Height < elementProxy.MinimumSize.Height)
						{
							size2.Height = elementProxy.MinimumSize.Height;
						}
					}
					Size size3 = size2 + elementProxy.Margin.Size;
					if (!measureOnly)
					{
						Rectangle rectangle = new Rectangle(location, new Size(size3.Width, rowBounds.Height));
						rectangle = LayoutUtils.DeflateRect(rectangle, elementProxy.Margin);
						AnchorStyles anchorStyles = elementProxy.AnchorStyles;
						containerProxy.Bounds = LayoutUtils.AlignAndStretch(size2, rectangle, anchorStyles);
					}
					location.X += size3.Width;
					if (num > 0 && location.X > rowBounds.Right)
					{
						break;
					}
					empty.Width = location.X - rowBounds.X;
					empty.Height = Math.Max(empty.Height, size3.Height);
					if (wrapContents)
					{
						if (flag)
						{
							break;
						}
						if (i + 1 < endIndex && CommonProperties.GetFlowBreak(elementProxy.Element))
						{
							if (num != 0)
							{
								breakIndex++;
								break;
							}
							flag = true;
						}
					}
					num++;
				}
				i++;
				breakIndex++;
			}
			return empty;
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x0014F89C File Offset: 0x0014DA9C
		public static bool GetWrapContents(IArrangedElement container)
		{
			int integer = container.Properties.GetInteger(FlowLayout._wrapContentsProperty);
			return integer == 0;
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x0014F8BE File Offset: 0x0014DABE
		public static void SetWrapContents(IArrangedElement container, bool value)
		{
			container.Properties.SetInteger(FlowLayout._wrapContentsProperty, value ? 0 : 1);
			LayoutTransaction.DoLayout(container, container, PropertyNames.WrapContents);
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x0014F8E3 File Offset: 0x0014DAE3
		public static FlowDirection GetFlowDirection(IArrangedElement container)
		{
			return (FlowDirection)container.Properties.GetInteger(FlowLayout._flowDirectionProperty);
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x0014F8F8 File Offset: 0x0014DAF8
		public static void SetFlowDirection(IArrangedElement container, FlowDirection value)
		{
			if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(FlowDirection));
			}
			container.Properties.SetInteger(FlowLayout._flowDirectionProperty, (int)value);
			LayoutTransaction.DoLayout(container, container, PropertyNames.FlowDirection);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG_VERIFY_ALIGNMENT")]
		private void Debug_VerifyAlignment(IArrangedElement container, FlowDirection flowDirection)
		{
		}

		// Token: 0x0400349A RID: 13466
		internal static readonly FlowLayout Instance = new FlowLayout();

		// Token: 0x0400349B RID: 13467
		private static readonly int _wrapContentsProperty = PropertyStore.CreateKey();

		// Token: 0x0400349C RID: 13468
		private static readonly int _flowDirectionProperty = PropertyStore.CreateKey();

		// Token: 0x02000860 RID: 2144
		private class ContainerProxy
		{
			// Token: 0x060070E8 RID: 28904 RVA: 0x0019E842 File Offset: 0x0019CA42
			public ContainerProxy(IArrangedElement container)
			{
				this._container = container;
				this._isContainerRTL = false;
				if (this._container is Control)
				{
					this._isContainerRTL = (((Control)this._container).RightToLeft == RightToLeft.Yes);
				}
			}

			// Token: 0x1700189A RID: 6298
			// (set) Token: 0x060070E9 RID: 28905 RVA: 0x0019E880 File Offset: 0x0019CA80
			public virtual Rectangle Bounds
			{
				set
				{
					if (this.IsContainerRTL)
					{
						if (this.IsVertical)
						{
							value.Y = this.DisplayRect.Bottom - value.Bottom;
						}
						else
						{
							value.X = this.DisplayRect.Right - value.Right;
						}
						FlowLayoutPanel flowLayoutPanel = this.Container as FlowLayoutPanel;
						if (flowLayoutPanel != null)
						{
							Point autoScrollPosition = flowLayoutPanel.AutoScrollPosition;
							if (autoScrollPosition != Point.Empty)
							{
								Point location = new Point(value.X, value.Y);
								if (this.IsVertical)
								{
									location.Offset(0, autoScrollPosition.X);
								}
								else
								{
									location.Offset(autoScrollPosition.X, 0);
								}
								value.Location = location;
							}
						}
					}
					this.ElementProxy.Bounds = value;
				}
			}

			// Token: 0x1700189B RID: 6299
			// (get) Token: 0x060070EA RID: 28906 RVA: 0x0019E950 File Offset: 0x0019CB50
			public IArrangedElement Container
			{
				get
				{
					return this._container;
				}
			}

			// Token: 0x1700189C RID: 6300
			// (get) Token: 0x060070EB RID: 28907 RVA: 0x00011A20 File Offset: 0x0000FC20
			protected virtual bool IsVertical
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700189D RID: 6301
			// (get) Token: 0x060070EC RID: 28908 RVA: 0x0019E958 File Offset: 0x0019CB58
			protected bool IsContainerRTL
			{
				get
				{
					return this._isContainerRTL;
				}
			}

			// Token: 0x1700189E RID: 6302
			// (get) Token: 0x060070ED RID: 28909 RVA: 0x0019E960 File Offset: 0x0019CB60
			// (set) Token: 0x060070EE RID: 28910 RVA: 0x0019E968 File Offset: 0x0019CB68
			public Rectangle DisplayRect
			{
				get
				{
					return this._displayRect;
				}
				set
				{
					if (this._displayRect != value)
					{
						this._displayRect = LayoutUtils.FlipRectangleIf(this.IsVertical, value);
					}
				}
			}

			// Token: 0x1700189F RID: 6303
			// (get) Token: 0x060070EF RID: 28911 RVA: 0x0019E98A File Offset: 0x0019CB8A
			public FlowLayout.ElementProxy ElementProxy
			{
				get
				{
					if (this._elementProxy == null)
					{
						this._elementProxy = (this.IsVertical ? new FlowLayout.VerticalElementProxy() : new FlowLayout.ElementProxy());
					}
					return this._elementProxy;
				}
			}

			// Token: 0x060070F0 RID: 28912 RVA: 0x0019E9B4 File Offset: 0x0019CBB4
			protected Rectangle RTLTranslateNoMarginSwap(Rectangle bounds)
			{
				Rectangle result = bounds;
				result.X = this.DisplayRect.Right - bounds.X - bounds.Width + this.ElementProxy.Margin.Left - this.ElementProxy.Margin.Right;
				FlowLayoutPanel flowLayoutPanel = this.Container as FlowLayoutPanel;
				if (flowLayoutPanel != null)
				{
					Point autoScrollPosition = flowLayoutPanel.AutoScrollPosition;
					if (autoScrollPosition != Point.Empty)
					{
						Point location = new Point(result.X, result.Y);
						if (this.IsVertical)
						{
							location.Offset(autoScrollPosition.Y, 0);
						}
						else
						{
							location.Offset(autoScrollPosition.X, 0);
						}
						result.Location = location;
					}
				}
				return result;
			}

			// Token: 0x040043F6 RID: 17398
			private IArrangedElement _container;

			// Token: 0x040043F7 RID: 17399
			private FlowLayout.ElementProxy _elementProxy;

			// Token: 0x040043F8 RID: 17400
			private Rectangle _displayRect;

			// Token: 0x040043F9 RID: 17401
			private bool _isContainerRTL;
		}

		// Token: 0x02000861 RID: 2145
		private class RightToLeftProxy : FlowLayout.ContainerProxy
		{
			// Token: 0x060070F1 RID: 28913 RVA: 0x0019EA7C File Offset: 0x0019CC7C
			public RightToLeftProxy(IArrangedElement container) : base(container)
			{
			}

			// Token: 0x170018A0 RID: 6304
			// (set) Token: 0x060070F2 RID: 28914 RVA: 0x0019EA85 File Offset: 0x0019CC85
			public override Rectangle Bounds
			{
				set
				{
					base.Bounds = base.RTLTranslateNoMarginSwap(value);
				}
			}
		}

		// Token: 0x02000862 RID: 2146
		private class TopDownProxy : FlowLayout.ContainerProxy
		{
			// Token: 0x060070F3 RID: 28915 RVA: 0x0019EA7C File Offset: 0x0019CC7C
			public TopDownProxy(IArrangedElement container) : base(container)
			{
			}

			// Token: 0x170018A1 RID: 6305
			// (get) Token: 0x060070F4 RID: 28916 RVA: 0x00013062 File Offset: 0x00011262
			protected override bool IsVertical
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x02000863 RID: 2147
		private class BottomUpProxy : FlowLayout.ContainerProxy
		{
			// Token: 0x060070F5 RID: 28917 RVA: 0x0019EA7C File Offset: 0x0019CC7C
			public BottomUpProxy(IArrangedElement container) : base(container)
			{
			}

			// Token: 0x170018A2 RID: 6306
			// (get) Token: 0x060070F6 RID: 28918 RVA: 0x00013062 File Offset: 0x00011262
			protected override bool IsVertical
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170018A3 RID: 6307
			// (set) Token: 0x060070F7 RID: 28919 RVA: 0x0019EA85 File Offset: 0x0019CC85
			public override Rectangle Bounds
			{
				set
				{
					base.Bounds = base.RTLTranslateNoMarginSwap(value);
				}
			}
		}

		// Token: 0x02000864 RID: 2148
		private class ElementProxy
		{
			// Token: 0x170018A4 RID: 6308
			// (get) Token: 0x060070F8 RID: 28920 RVA: 0x0019EA94 File Offset: 0x0019CC94
			public virtual AnchorStyles AnchorStyles
			{
				get
				{
					AnchorStyles unifiedAnchor = LayoutUtils.GetUnifiedAnchor(this.Element);
					bool flag = (unifiedAnchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom);
					bool flag2 = (unifiedAnchor & AnchorStyles.Top) > AnchorStyles.None;
					bool flag3 = (unifiedAnchor & AnchorStyles.Bottom) > AnchorStyles.None;
					if (flag)
					{
						return AnchorStyles.Top | AnchorStyles.Bottom;
					}
					if (flag2)
					{
						return AnchorStyles.Top;
					}
					if (flag3)
					{
						return AnchorStyles.Bottom;
					}
					return AnchorStyles.None;
				}
			}

			// Token: 0x170018A5 RID: 6309
			// (get) Token: 0x060070F9 RID: 28921 RVA: 0x0019EAD2 File Offset: 0x0019CCD2
			public bool AutoSize
			{
				get
				{
					return CommonProperties.GetAutoSize(this._element);
				}
			}

			// Token: 0x170018A6 RID: 6310
			// (set) Token: 0x060070FA RID: 28922 RVA: 0x0019EADF File Offset: 0x0019CCDF
			public virtual Rectangle Bounds
			{
				set
				{
					this._element.SetBounds(value, BoundsSpecified.None);
				}
			}

			// Token: 0x170018A7 RID: 6311
			// (get) Token: 0x060070FB RID: 28923 RVA: 0x0019EAEE File Offset: 0x0019CCEE
			// (set) Token: 0x060070FC RID: 28924 RVA: 0x0019EAF6 File Offset: 0x0019CCF6
			public IArrangedElement Element
			{
				get
				{
					return this._element;
				}
				set
				{
					this._element = value;
				}
			}

			// Token: 0x170018A8 RID: 6312
			// (get) Token: 0x060070FD RID: 28925 RVA: 0x0019EB00 File Offset: 0x0019CD00
			public bool Stretches
			{
				get
				{
					AnchorStyles anchorStyles = this.AnchorStyles;
					return ((AnchorStyles.Top | AnchorStyles.Bottom) & anchorStyles) == (AnchorStyles.Top | AnchorStyles.Bottom);
				}
			}

			// Token: 0x170018A9 RID: 6313
			// (get) Token: 0x060070FE RID: 28926 RVA: 0x0019EB1D File Offset: 0x0019CD1D
			public virtual Padding Margin
			{
				get
				{
					return CommonProperties.GetMargin(this.Element);
				}
			}

			// Token: 0x170018AA RID: 6314
			// (get) Token: 0x060070FF RID: 28927 RVA: 0x0019EB2A File Offset: 0x0019CD2A
			public virtual Size MinimumSize
			{
				get
				{
					return CommonProperties.GetMinimumSize(this.Element, Size.Empty);
				}
			}

			// Token: 0x170018AB RID: 6315
			// (get) Token: 0x06007100 RID: 28928 RVA: 0x0019EB3C File Offset: 0x0019CD3C
			public bool ParticipatesInLayout
			{
				get
				{
					return this._element.ParticipatesInLayout;
				}
			}

			// Token: 0x170018AC RID: 6316
			// (get) Token: 0x06007101 RID: 28929 RVA: 0x0019EB4C File Offset: 0x0019CD4C
			public virtual Size SpecifiedSize
			{
				get
				{
					return CommonProperties.GetSpecifiedBounds(this._element).Size;
				}
			}

			// Token: 0x06007102 RID: 28930 RVA: 0x0019EB6C File Offset: 0x0019CD6C
			public virtual Size GetPreferredSize(Size proposedSize)
			{
				return this._element.GetPreferredSize(proposedSize);
			}

			// Token: 0x040043FA RID: 17402
			private IArrangedElement _element;
		}

		// Token: 0x02000865 RID: 2149
		private class VerticalElementProxy : FlowLayout.ElementProxy
		{
			// Token: 0x170018AD RID: 6317
			// (get) Token: 0x06007104 RID: 28932 RVA: 0x0019EB7C File Offset: 0x0019CD7C
			public override AnchorStyles AnchorStyles
			{
				get
				{
					AnchorStyles unifiedAnchor = LayoutUtils.GetUnifiedAnchor(base.Element);
					bool flag = (unifiedAnchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right);
					bool flag2 = (unifiedAnchor & AnchorStyles.Left) > AnchorStyles.None;
					bool flag3 = (unifiedAnchor & AnchorStyles.Right) > AnchorStyles.None;
					if (flag)
					{
						return AnchorStyles.Top | AnchorStyles.Bottom;
					}
					if (flag2)
					{
						return AnchorStyles.Top;
					}
					if (flag3)
					{
						return AnchorStyles.Bottom;
					}
					return AnchorStyles.None;
				}
			}

			// Token: 0x170018AE RID: 6318
			// (set) Token: 0x06007105 RID: 28933 RVA: 0x0019EBBC File Offset: 0x0019CDBC
			public override Rectangle Bounds
			{
				set
				{
					base.Bounds = LayoutUtils.FlipRectangle(value);
				}
			}

			// Token: 0x170018AF RID: 6319
			// (get) Token: 0x06007106 RID: 28934 RVA: 0x0019EBCA File Offset: 0x0019CDCA
			public override Padding Margin
			{
				get
				{
					return LayoutUtils.FlipPadding(base.Margin);
				}
			}

			// Token: 0x170018B0 RID: 6320
			// (get) Token: 0x06007107 RID: 28935 RVA: 0x0019EBD7 File Offset: 0x0019CDD7
			public override Size MinimumSize
			{
				get
				{
					return LayoutUtils.FlipSize(base.MinimumSize);
				}
			}

			// Token: 0x170018B1 RID: 6321
			// (get) Token: 0x06007108 RID: 28936 RVA: 0x0019EBE4 File Offset: 0x0019CDE4
			public override Size SpecifiedSize
			{
				get
				{
					return LayoutUtils.FlipSize(base.SpecifiedSize);
				}
			}

			// Token: 0x06007109 RID: 28937 RVA: 0x0019EBF1 File Offset: 0x0019CDF1
			public override Size GetPreferredSize(Size proposedSize)
			{
				return LayoutUtils.FlipSize(base.GetPreferredSize(LayoutUtils.FlipSize(proposedSize)));
			}
		}
	}
}
