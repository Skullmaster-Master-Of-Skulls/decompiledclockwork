using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004CA RID: 1226
	internal class DefaultLayout : LayoutEngine
	{
		// Token: 0x0600506C RID: 20588 RVA: 0x0014E16C File Offset: 0x0014C36C
		private static void LayoutAutoSizedControls(IArrangedElement container)
		{
			ArrangedElementCollection children = container.Children;
			for (int i = children.Count - 1; i >= 0; i--)
			{
				IArrangedElement arrangedElement = children[i];
				if (CommonProperties.xGetAutoSizedAndAnchored(arrangedElement))
				{
					Rectangle cachedBounds = DefaultLayout.GetCachedBounds(arrangedElement);
					AnchorStyles anchor = DefaultLayout.GetAnchor(arrangedElement);
					Size maxSize = LayoutUtils.MaxSize;
					if ((anchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right))
					{
						maxSize.Width = cachedBounds.Width;
					}
					if ((anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom))
					{
						maxSize.Height = cachedBounds.Height;
					}
					Size preferredSize = arrangedElement.GetPreferredSize(maxSize);
					Rectangle rectangle = cachedBounds;
					if (CommonProperties.GetAutoSizeMode(arrangedElement) == AutoSizeMode.GrowAndShrink)
					{
						rectangle = DefaultLayout.GetGrowthBounds(arrangedElement, preferredSize);
					}
					else if (cachedBounds.Width < preferredSize.Width || cachedBounds.Height < preferredSize.Height)
					{
						Size newSize = LayoutUtils.UnionSizes(cachedBounds.Size, preferredSize);
						rectangle = DefaultLayout.GetGrowthBounds(arrangedElement, newSize);
					}
					if (rectangle != cachedBounds)
					{
						DefaultLayout.SetCachedBounds(arrangedElement, rectangle);
					}
				}
			}
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x0014E25C File Offset: 0x0014C45C
		private static Rectangle GetGrowthBounds(IArrangedElement element, Size newSize)
		{
			DefaultLayout.GrowthDirection growthDirection = DefaultLayout.GetGrowthDirection(element);
			Rectangle cachedBounds = DefaultLayout.GetCachedBounds(element);
			Point location = cachedBounds.Location;
			if ((growthDirection & DefaultLayout.GrowthDirection.Left) != DefaultLayout.GrowthDirection.None)
			{
				location.X -= newSize.Width - cachedBounds.Width;
			}
			if ((growthDirection & DefaultLayout.GrowthDirection.Upward) != DefaultLayout.GrowthDirection.None)
			{
				location.Y -= newSize.Height - cachedBounds.Height;
			}
			Rectangle result = new Rectangle(location, newSize);
			return result;
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x0014E2D0 File Offset: 0x0014C4D0
		private static DefaultLayout.GrowthDirection GetGrowthDirection(IArrangedElement element)
		{
			AnchorStyles anchor = DefaultLayout.GetAnchor(element);
			DefaultLayout.GrowthDirection growthDirection = DefaultLayout.GrowthDirection.None;
			if ((anchor & AnchorStyles.Right) != AnchorStyles.None && (anchor & AnchorStyles.Left) == AnchorStyles.None)
			{
				growthDirection |= DefaultLayout.GrowthDirection.Left;
			}
			else
			{
				growthDirection |= DefaultLayout.GrowthDirection.Right;
			}
			if ((anchor & AnchorStyles.Bottom) != AnchorStyles.None && (anchor & AnchorStyles.Top) == AnchorStyles.None)
			{
				growthDirection |= DefaultLayout.GrowthDirection.Upward;
			}
			else
			{
				growthDirection |= DefaultLayout.GrowthDirection.Downward;
			}
			return growthDirection;
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x0014E310 File Offset: 0x0014C510
		private static Rectangle GetAnchorDestination(IArrangedElement element, Rectangle displayRect, bool measureOnly)
		{
			DefaultLayout.AnchorInfo anchorInfo = DefaultLayout.GetAnchorInfo(element);
			int num = anchorInfo.Left + displayRect.X;
			int num2 = anchorInfo.Top + displayRect.Y;
			int num3 = anchorInfo.Right + displayRect.X;
			int num4 = anchorInfo.Bottom + displayRect.Y;
			AnchorStyles anchor = DefaultLayout.GetAnchor(element);
			if (DefaultLayout.IsAnchored(anchor, AnchorStyles.Right))
			{
				num3 += displayRect.Width;
				if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Left))
				{
					num += displayRect.Width;
				}
			}
			else if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Left))
			{
				num3 += displayRect.Width / 2;
				num += displayRect.Width / 2;
			}
			if (DefaultLayout.IsAnchored(anchor, AnchorStyles.Bottom))
			{
				num4 += displayRect.Height;
				if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Top))
				{
					num2 += displayRect.Height;
				}
			}
			else if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Top))
			{
				num4 += displayRect.Height / 2;
				num2 += displayRect.Height / 2;
			}
			if (!measureOnly)
			{
				if (num3 < num)
				{
					num3 = num;
				}
				if (num4 < num2)
				{
					num4 = num2;
				}
			}
			else
			{
				Rectangle cachedBounds = DefaultLayout.GetCachedBounds(element);
				if (num3 < num || cachedBounds.Width != element.Bounds.Width || cachedBounds.X != element.Bounds.X)
				{
					if (cachedBounds != element.Bounds)
					{
						num = Math.Max(Math.Abs(num), Math.Abs(cachedBounds.Left));
					}
					num3 = num + Math.Max(element.Bounds.Width, cachedBounds.Width) + Math.Abs(num3);
				}
				else
				{
					num = ((num > 0) ? num : element.Bounds.Left);
					num3 = ((num3 > 0) ? num3 : (element.Bounds.Right + Math.Abs(num3)));
				}
				if (num4 < num2 || cachedBounds.Height != element.Bounds.Height || cachedBounds.Y != element.Bounds.Y)
				{
					if (cachedBounds != element.Bounds)
					{
						num2 = Math.Max(Math.Abs(num2), Math.Abs(cachedBounds.Top));
					}
					num4 = num2 + Math.Max(element.Bounds.Height, cachedBounds.Height) + Math.Abs(num4);
				}
				else
				{
					num2 = ((num2 > 0) ? num2 : element.Bounds.Top);
					num4 = ((num4 > 0) ? num4 : (element.Bounds.Bottom + Math.Abs(num4)));
				}
			}
			return new Rectangle(num, num2, num3 - num, num4 - num2);
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x0014E5AC File Offset: 0x0014C7AC
		private static void LayoutAnchoredControls(IArrangedElement container)
		{
			Rectangle displayRectangle = container.DisplayRectangle;
			if (CommonProperties.GetAutoSize(container) && (displayRectangle.Width == 0 || displayRectangle.Height == 0))
			{
				return;
			}
			ArrangedElementCollection children = container.Children;
			for (int i = children.Count - 1; i >= 0; i--)
			{
				IArrangedElement element = children[i];
				if (CommonProperties.GetNeedsAnchorLayout(element))
				{
					DefaultLayout.SetCachedBounds(element, DefaultLayout.GetAnchorDestination(element, displayRectangle, false));
				}
			}
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x0014E614 File Offset: 0x0014C814
		private static Size LayoutDockedControls(IArrangedElement container, bool measureOnly)
		{
			Rectangle bounds = measureOnly ? Rectangle.Empty : container.DisplayRectangle;
			Size empty = Size.Empty;
			IArrangedElement arrangedElement = null;
			ArrangedElementCollection children = container.Children;
			for (int i = children.Count - 1; i >= 0; i--)
			{
				IArrangedElement arrangedElement2 = children[i];
				if (CommonProperties.GetNeedsDockLayout(arrangedElement2))
				{
					switch (DefaultLayout.GetDock(arrangedElement2))
					{
					case DockStyle.Top:
					{
						Size verticalDockedSize = DefaultLayout.GetVerticalDockedSize(arrangedElement2, bounds.Size, measureOnly);
						Rectangle newElementBounds = new Rectangle(bounds.X, bounds.Y, verticalDockedSize.Width, verticalDockedSize.Height);
						DefaultLayout.xLayoutDockedControl(arrangedElement2, newElementBounds, measureOnly, ref empty, ref bounds);
						bounds.Y += arrangedElement2.Bounds.Height;
						bounds.Height -= arrangedElement2.Bounds.Height;
						break;
					}
					case DockStyle.Bottom:
					{
						Size verticalDockedSize2 = DefaultLayout.GetVerticalDockedSize(arrangedElement2, bounds.Size, measureOnly);
						Rectangle newElementBounds2 = new Rectangle(bounds.X, bounds.Bottom - verticalDockedSize2.Height, verticalDockedSize2.Width, verticalDockedSize2.Height);
						DefaultLayout.xLayoutDockedControl(arrangedElement2, newElementBounds2, measureOnly, ref empty, ref bounds);
						bounds.Height -= arrangedElement2.Bounds.Height;
						break;
					}
					case DockStyle.Left:
					{
						Size horizontalDockedSize = DefaultLayout.GetHorizontalDockedSize(arrangedElement2, bounds.Size, measureOnly);
						Rectangle newElementBounds3 = new Rectangle(bounds.X, bounds.Y, horizontalDockedSize.Width, horizontalDockedSize.Height);
						DefaultLayout.xLayoutDockedControl(arrangedElement2, newElementBounds3, measureOnly, ref empty, ref bounds);
						bounds.X += arrangedElement2.Bounds.Width;
						bounds.Width -= arrangedElement2.Bounds.Width;
						break;
					}
					case DockStyle.Right:
					{
						Size horizontalDockedSize2 = DefaultLayout.GetHorizontalDockedSize(arrangedElement2, bounds.Size, measureOnly);
						Rectangle newElementBounds4 = new Rectangle(bounds.Right - horizontalDockedSize2.Width, bounds.Y, horizontalDockedSize2.Width, horizontalDockedSize2.Height);
						DefaultLayout.xLayoutDockedControl(arrangedElement2, newElementBounds4, measureOnly, ref empty, ref bounds);
						bounds.Width -= arrangedElement2.Bounds.Width;
						break;
					}
					case DockStyle.Fill:
						if (arrangedElement2 is MdiClient)
						{
							arrangedElement = arrangedElement2;
						}
						else
						{
							Size size = bounds.Size;
							Rectangle newElementBounds5 = new Rectangle(bounds.X, bounds.Y, size.Width, size.Height);
							DefaultLayout.xLayoutDockedControl(arrangedElement2, newElementBounds5, measureOnly, ref empty, ref bounds);
						}
						break;
					}
				}
				if (arrangedElement != null)
				{
					DefaultLayout.SetCachedBounds(arrangedElement, bounds);
				}
			}
			return empty;
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x0014E8D0 File Offset: 0x0014CAD0
		private static void xLayoutDockedControl(IArrangedElement element, Rectangle newElementBounds, bool measureOnly, ref Size preferredSize, ref Rectangle remainingBounds)
		{
			if (measureOnly)
			{
				Size size = new Size(Math.Max(0, newElementBounds.Width - remainingBounds.Width), Math.Max(0, newElementBounds.Height - remainingBounds.Height));
				DockStyle dock = DefaultLayout.GetDock(element);
				if (dock == DockStyle.Top || dock == DockStyle.Bottom)
				{
					size.Width = 0;
				}
				if (dock == DockStyle.Left || dock == DockStyle.Right)
				{
					size.Height = 0;
				}
				if (dock != DockStyle.Fill)
				{
					preferredSize += size;
					remainingBounds.Size += size;
					return;
				}
				if (dock == DockStyle.Fill && CommonProperties.GetAutoSize(element))
				{
					Size preferredSize2 = element.GetPreferredSize(size);
					remainingBounds.Size += preferredSize2;
					preferredSize += preferredSize2;
					return;
				}
			}
			else
			{
				element.SetBounds(newElementBounds, BoundsSpecified.None);
			}
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x0014E9A8 File Offset: 0x0014CBA8
		private static Size GetVerticalDockedSize(IArrangedElement element, Size remainingSize, bool measureOnly)
		{
			Size result = DefaultLayout.xGetDockedSize(element, remainingSize, new Size(remainingSize.Width, 1), measureOnly);
			if (!measureOnly)
			{
				result.Width = remainingSize.Width;
			}
			else
			{
				result.Width = Math.Max(result.Width, remainingSize.Width);
			}
			return result;
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x0014E9FC File Offset: 0x0014CBFC
		private static Size GetHorizontalDockedSize(IArrangedElement element, Size remainingSize, bool measureOnly)
		{
			Size result = DefaultLayout.xGetDockedSize(element, remainingSize, new Size(1, remainingSize.Height), measureOnly);
			if (!measureOnly)
			{
				result.Height = remainingSize.Height;
			}
			else
			{
				result.Height = Math.Max(result.Height, remainingSize.Height);
			}
			return result;
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x0014EA50 File Offset: 0x0014CC50
		private static Size xGetDockedSize(IArrangedElement element, Size remainingSize, Size constraints, bool measureOnly)
		{
			Size result;
			if (CommonProperties.GetAutoSize(element))
			{
				result = element.GetPreferredSize(constraints);
			}
			else
			{
				result = element.Bounds.Size;
			}
			return result;
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x0014EA80 File Offset: 0x0014CC80
		internal override bool LayoutCore(IArrangedElement container, LayoutEventArgs args)
		{
			Size size;
			return DefaultLayout.xLayout(container, false, out size);
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x0014EA98 File Offset: 0x0014CC98
		private static bool xLayout(IArrangedElement container, bool measureOnly, out Size preferredSize)
		{
			ArrangedElementCollection children = container.Children;
			preferredSize = new Size(-7103, -7105);
			if (!measureOnly && children.Count == 0)
			{
				return CommonProperties.GetAutoSize(container);
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = children.Count - 1; i >= 0; i--)
			{
				IArrangedElement element = children[i];
				if (CommonProperties.GetNeedsDockAndAnchorLayout(element))
				{
					if (!flag && CommonProperties.GetNeedsDockLayout(element))
					{
						flag = true;
					}
					if (!flag2 && CommonProperties.GetNeedsAnchorLayout(element))
					{
						flag2 = true;
					}
					if (!flag3 && CommonProperties.xGetAutoSizedAndAnchored(element))
					{
						flag3 = true;
					}
				}
			}
			Size a = Size.Empty;
			Size b = Size.Empty;
			if (flag)
			{
				a = DefaultLayout.LayoutDockedControls(container, measureOnly);
			}
			if (flag2 && !measureOnly)
			{
				DefaultLayout.LayoutAnchoredControls(container);
			}
			if (flag3)
			{
				DefaultLayout.LayoutAutoSizedControls(container);
			}
			if (!measureOnly)
			{
				DefaultLayout.ApplyCachedBounds(container);
			}
			else
			{
				b = DefaultLayout.GetAnchorPreferredSize(container);
				Padding padding = Padding.Empty;
				Control control = container as Control;
				if (control != null)
				{
					padding = control.Padding;
				}
				else
				{
					padding = CommonProperties.GetPadding(container, Padding.Empty);
				}
				b.Width -= padding.Left;
				b.Height -= padding.Top;
				DefaultLayout.ClearCachedBounds(container);
				preferredSize = LayoutUtils.UnionSizes(a, b);
			}
			return CommonProperties.GetAutoSize(container);
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x0014EBE0 File Offset: 0x0014CDE0
		private static void UpdateAnchorInfo(IArrangedElement element)
		{
			DefaultLayout.AnchorInfo anchorInfo = DefaultLayout.GetAnchorInfo(element);
			if (anchorInfo == null)
			{
				anchorInfo = new DefaultLayout.AnchorInfo();
				DefaultLayout.SetAnchorInfo(element, anchorInfo);
			}
			if (CommonProperties.GetNeedsAnchorLayout(element) && element.Container != null)
			{
				Rectangle cachedBounds = DefaultLayout.GetCachedBounds(element);
				DefaultLayout.AnchorInfo anchorInfo2 = new DefaultLayout.AnchorInfo();
				anchorInfo2.Left = anchorInfo.Left;
				anchorInfo2.Top = anchorInfo.Top;
				anchorInfo2.Right = anchorInfo.Right;
				anchorInfo2.Bottom = anchorInfo.Bottom;
				anchorInfo.Left = element.Bounds.Left;
				anchorInfo.Top = element.Bounds.Top;
				anchorInfo.Right = element.Bounds.Right;
				anchorInfo.Bottom = element.Bounds.Bottom;
				Rectangle displayRectangle = element.Container.DisplayRectangle;
				int width = displayRectangle.Width;
				int height = displayRectangle.Height;
				anchorInfo.Left -= displayRectangle.X;
				anchorInfo.Top -= displayRectangle.Y;
				anchorInfo.Right -= displayRectangle.X;
				anchorInfo.Bottom -= displayRectangle.Y;
				AnchorStyles anchor = DefaultLayout.GetAnchor(element);
				if (DefaultLayout.IsAnchored(anchor, AnchorStyles.Right))
				{
					if (DpiHelper.EnableAnchorLayoutHighDpiImprovements && anchorInfo.Right - width > 0 && anchorInfo2.Right < 0)
					{
						anchorInfo.Right = anchorInfo2.Right;
						if (DpiHelper.EnableAnchorLayoutHighDpiImprovementsV2)
						{
							if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Left))
							{
								anchorInfo.Left = anchorInfo2.Right - cachedBounds.Width;
							}
						}
						else
						{
							anchorInfo.Left = anchorInfo2.Right - cachedBounds.Width;
						}
					}
					else
					{
						anchorInfo.Right -= width;
						if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Left))
						{
							anchorInfo.Left -= width;
						}
					}
				}
				else if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Left))
				{
					anchorInfo.Right -= width / 2;
					anchorInfo.Left -= width / 2;
				}
				if (DefaultLayout.IsAnchored(anchor, AnchorStyles.Bottom))
				{
					if (DpiHelper.EnableAnchorLayoutHighDpiImprovements && anchorInfo.Bottom - height > 0 && anchorInfo2.Bottom < 0)
					{
						anchorInfo.Bottom = anchorInfo2.Bottom;
						if (!DpiHelper.EnableAnchorLayoutHighDpiImprovementsV2)
						{
							anchorInfo.Top = anchorInfo2.Bottom - cachedBounds.Height;
							return;
						}
						if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Top))
						{
							anchorInfo.Top = anchorInfo2.Bottom - cachedBounds.Height;
							return;
						}
					}
					else
					{
						anchorInfo.Bottom -= height;
						if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Top))
						{
							anchorInfo.Top -= height;
							return;
						}
					}
				}
				else if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Top))
				{
					anchorInfo.Bottom -= height / 2;
					anchorInfo.Top -= height / 2;
				}
			}
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x0011933B File Offset: 0x0011753B
		public static AnchorStyles GetAnchor(IArrangedElement element)
		{
			return CommonProperties.xGetAnchor(element);
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x0014EEB4 File Offset: 0x0014D0B4
		public static void SetAnchor(IArrangedElement container, IArrangedElement element, AnchorStyles value)
		{
			AnchorStyles anchor = DefaultLayout.GetAnchor(element);
			if (anchor != value)
			{
				if (CommonProperties.GetNeedsDockLayout(element))
				{
					DefaultLayout.SetDock(element, DockStyle.None);
				}
				CommonProperties.xSetAnchor(element, value);
				if (CommonProperties.GetNeedsAnchorLayout(element))
				{
					DefaultLayout.UpdateAnchorInfo(element);
				}
				else
				{
					DefaultLayout.SetAnchorInfo(element, null);
				}
				if (element.Container != null)
				{
					bool flag = DefaultLayout.IsAnchored(anchor, AnchorStyles.Right) && !DefaultLayout.IsAnchored(value, AnchorStyles.Right);
					bool flag2 = DefaultLayout.IsAnchored(anchor, AnchorStyles.Bottom) && !DefaultLayout.IsAnchored(value, AnchorStyles.Bottom);
					if (element.Container.Container != null && (flag || flag2))
					{
						LayoutTransaction.DoLayout(element.Container.Container, element, PropertyNames.Anchor);
					}
					LayoutTransaction.DoLayout(element.Container, element, PropertyNames.Anchor);
				}
			}
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x0011936E File Offset: 0x0011756E
		public static DockStyle GetDock(IArrangedElement element)
		{
			return CommonProperties.xGetDock(element);
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x0014EF6C File Offset: 0x0014D16C
		public static void SetDock(IArrangedElement element, DockStyle value)
		{
			if (DefaultLayout.GetDock(element) != value)
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 5))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DockStyle));
				}
				bool needsDockLayout = CommonProperties.GetNeedsDockLayout(element);
				CommonProperties.xSetDock(element, value);
				using (new LayoutTransaction(element.Container as Control, element, PropertyNames.Dock))
				{
					if (value == DockStyle.None)
					{
						if (needsDockLayout)
						{
							element.SetBounds(CommonProperties.GetSpecifiedBounds(element), BoundsSpecified.None);
							DefaultLayout.UpdateAnchorInfo(element);
						}
					}
					else
					{
						element.SetBounds(CommonProperties.GetSpecifiedBounds(element), BoundsSpecified.All);
					}
				}
			}
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x0014F018 File Offset: 0x0014D218
		public static void ScaleAnchorInfo(IArrangedElement element, SizeF factor)
		{
			DefaultLayout.AnchorInfo anchorInfo = DefaultLayout.GetAnchorInfo(element);
			if (anchorInfo != null)
			{
				anchorInfo.Left = (int)((float)anchorInfo.Left * factor.Width);
				anchorInfo.Top = (int)((float)anchorInfo.Top * factor.Height);
				anchorInfo.Right = (int)((float)anchorInfo.Right * factor.Width);
				anchorInfo.Bottom = (int)((float)anchorInfo.Bottom * factor.Height);
				DefaultLayout.SetAnchorInfo(element, anchorInfo);
			}
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x0014F090 File Offset: 0x0014D290
		private static Rectangle GetCachedBounds(IArrangedElement element)
		{
			if (element.Container != null)
			{
				IDictionary dictionary = (IDictionary)element.Container.Properties.GetObject(DefaultLayout._cachedBoundsProperty);
				if (dictionary != null)
				{
					object obj = dictionary[element];
					if (obj != null)
					{
						return (Rectangle)obj;
					}
				}
			}
			return element.Bounds;
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x0014F0DB File Offset: 0x0014D2DB
		private static bool HasCachedBounds(IArrangedElement container)
		{
			return container != null && container.Properties.GetObject(DefaultLayout._cachedBoundsProperty) != null;
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x0014F0F8 File Offset: 0x0014D2F8
		private static void ApplyCachedBounds(IArrangedElement container)
		{
			if (CommonProperties.GetAutoSize(container))
			{
				Rectangle displayRectangle = container.DisplayRectangle;
				if (displayRectangle.Width == 0 || displayRectangle.Height == 0)
				{
					DefaultLayout.ClearCachedBounds(container);
					return;
				}
			}
			IDictionary dictionary = (IDictionary)container.Properties.GetObject(DefaultLayout._cachedBoundsProperty);
			if (dictionary != null)
			{
				foreach (object obj in dictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					IArrangedElement arrangedElement = (IArrangedElement)dictionaryEntry.Key;
					Rectangle bounds = (Rectangle)dictionaryEntry.Value;
					arrangedElement.SetBounds(bounds, BoundsSpecified.None);
				}
				DefaultLayout.ClearCachedBounds(container);
			}
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x0014F1B8 File Offset: 0x0014D3B8
		private static void ClearCachedBounds(IArrangedElement container)
		{
			container.Properties.SetObject(DefaultLayout._cachedBoundsProperty, null);
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x0014F1CC File Offset: 0x0014D3CC
		private static void SetCachedBounds(IArrangedElement element, Rectangle bounds)
		{
			if (bounds != DefaultLayout.GetCachedBounds(element))
			{
				IDictionary dictionary = (IDictionary)element.Container.Properties.GetObject(DefaultLayout._cachedBoundsProperty);
				if (dictionary == null)
				{
					dictionary = new HybridDictionary();
					element.Container.Properties.SetObject(DefaultLayout._cachedBoundsProperty, dictionary);
				}
				dictionary[element] = bounds;
			}
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x0014F22E File Offset: 0x0014D42E
		private static DefaultLayout.AnchorInfo GetAnchorInfo(IArrangedElement element)
		{
			return (DefaultLayout.AnchorInfo)element.Properties.GetObject(DefaultLayout._layoutInfoProperty);
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0014F245 File Offset: 0x0014D445
		private static void SetAnchorInfo(IArrangedElement element, DefaultLayout.AnchorInfo value)
		{
			element.Properties.SetObject(DefaultLayout._layoutInfoProperty, value);
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x0014F258 File Offset: 0x0014D458
		internal override void InitLayoutCore(IArrangedElement element, BoundsSpecified specified)
		{
			if (specified != BoundsSpecified.None && CommonProperties.GetNeedsAnchorLayout(element))
			{
				DefaultLayout.UpdateAnchorInfo(element);
			}
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x0014F26C File Offset: 0x0014D46C
		internal override Size GetPreferredSize(IArrangedElement container, Size proposedBounds)
		{
			Size result;
			DefaultLayout.xLayout(container, true, out result);
			return result;
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x0014F284 File Offset: 0x0014D484
		private static Size GetAnchorPreferredSize(IArrangedElement container)
		{
			Size empty = Size.Empty;
			ArrangedElementCollection children = container.Children;
			for (int i = children.Count - 1; i >= 0; i--)
			{
				IArrangedElement arrangedElement = container.Children[i];
				if (!CommonProperties.GetNeedsDockLayout(arrangedElement) && arrangedElement.ParticipatesInLayout)
				{
					AnchorStyles anchor = DefaultLayout.GetAnchor(arrangedElement);
					Padding margin = CommonProperties.GetMargin(arrangedElement);
					Rectangle rectangle = LayoutUtils.InflateRect(DefaultLayout.GetCachedBounds(arrangedElement), margin);
					if (DefaultLayout.IsAnchored(anchor, AnchorStyles.Left) && !DefaultLayout.IsAnchored(anchor, AnchorStyles.Right))
					{
						empty.Width = Math.Max(empty.Width, rectangle.Right);
					}
					if (!DefaultLayout.IsAnchored(anchor, AnchorStyles.Bottom))
					{
						empty.Height = Math.Max(empty.Height, rectangle.Bottom);
					}
					if (DefaultLayout.IsAnchored(anchor, AnchorStyles.Right))
					{
						Rectangle anchorDestination = DefaultLayout.GetAnchorDestination(arrangedElement, Rectangle.Empty, true);
						if (anchorDestination.Width < 0)
						{
							empty.Width = Math.Max(empty.Width, rectangle.Right + anchorDestination.Width);
						}
						else
						{
							empty.Width = Math.Max(empty.Width, anchorDestination.Right);
						}
					}
					if (DefaultLayout.IsAnchored(anchor, AnchorStyles.Bottom))
					{
						Rectangle anchorDestination2 = DefaultLayout.GetAnchorDestination(arrangedElement, Rectangle.Empty, true);
						if (anchorDestination2.Height < 0)
						{
							empty.Height = Math.Max(empty.Height, rectangle.Bottom + anchorDestination2.Height);
						}
						else
						{
							empty.Height = Math.Max(empty.Height, anchorDestination2.Bottom);
						}
					}
				}
			}
			return empty;
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x000DE15F File Offset: 0x000DC35F
		public static bool IsAnchored(AnchorStyles anchor, AnchorStyles desiredAnchor)
		{
			return (anchor & desiredAnchor) == desiredAnchor;
		}

		// Token: 0x04003497 RID: 13463
		internal static readonly DefaultLayout Instance = new DefaultLayout();

		// Token: 0x04003498 RID: 13464
		private static readonly int _layoutInfoProperty = PropertyStore.CreateKey();

		// Token: 0x04003499 RID: 13465
		private static readonly int _cachedBoundsProperty = PropertyStore.CreateKey();

		// Token: 0x0200085E RID: 2142
		[Flags]
		private enum GrowthDirection
		{
			// Token: 0x040043ED RID: 17389
			None = 0,
			// Token: 0x040043EE RID: 17390
			Upward = 1,
			// Token: 0x040043EF RID: 17391
			Downward = 2,
			// Token: 0x040043F0 RID: 17392
			Left = 4,
			// Token: 0x040043F1 RID: 17393
			Right = 8
		}

		// Token: 0x0200085F RID: 2143
		private sealed class AnchorInfo
		{
			// Token: 0x040043F2 RID: 17394
			public int Left;

			// Token: 0x040043F3 RID: 17395
			public int Top;

			// Token: 0x040043F4 RID: 17396
			public int Right;

			// Token: 0x040043F5 RID: 17397
			public int Bottom;
		}
	}
}
