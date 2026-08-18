using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FC RID: 1276
	internal class CategoryGridEntry : GridEntry
	{
		// Token: 0x06005392 RID: 21394 RVA: 0x0015E2D8 File Offset: 0x0015C4D8
		public CategoryGridEntry(PropertyGrid ownerGrid, GridEntry peParent, string name, GridEntry[] childGridEntries) : base(ownerGrid, peParent)
		{
			this.name = name;
			if (CategoryGridEntry.categoryStates == null)
			{
				CategoryGridEntry.categoryStates = new Hashtable();
			}
			Hashtable obj = CategoryGridEntry.categoryStates;
			lock (obj)
			{
				if (!CategoryGridEntry.categoryStates.ContainsKey(name))
				{
					CategoryGridEntry.categoryStates.Add(name, true);
				}
			}
			this.IsExpandable = true;
			for (int i = 0; i < childGridEntries.Length; i++)
			{
				childGridEntries[i].ParentGridEntry = this;
			}
			base.ChildCollection = new GridEntryCollection(this, childGridEntries);
			Hashtable obj2 = CategoryGridEntry.categoryStates;
			lock (obj2)
			{
				this.InternalExpanded = (bool)CategoryGridEntry.categoryStates[name];
			}
			this.SetFlag(64, true);
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06005393 RID: 21395 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal override bool HasValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x0015E3C8 File Offset: 0x0015C5C8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.backBrush != null)
				{
					this.backBrush.Dispose();
					this.backBrush = null;
				}
				if (base.ChildCollection != null)
				{
					base.ChildCollection = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x000072B6 File Offset: 0x000054B6
		public override void DisposeChildren()
		{
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06005396 RID: 21398 RVA: 0x0015E3FD File Offset: 0x0015C5FD
		public override int PropertyDepth
		{
			get
			{
				return base.PropertyDepth - 1;
			}
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x0015E407 File Offset: 0x0015C607
		protected override GridEntry.GridEntryAccessibleObject GetAccessibilityObject()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new CategoryGridEntry.CategoryGridEntryAccessibleObject(this);
			}
			return base.GetAccessibilityObject();
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x0015E41D File Offset: 0x0015C61D
		protected override Brush GetBackgroundBrush(Graphics g)
		{
			return this.GridEntryHost.GetLineBrush(g);
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06005399 RID: 21401 RVA: 0x0015E42B File Offset: 0x0015C62B
		protected override Color LabelTextColor
		{
			get
			{
				return this.ownerGrid.CategoryForeColor;
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x0600539A RID: 21402 RVA: 0x0015E438 File Offset: 0x0015C638
		public override bool Expandable
		{
			get
			{
				return !this.GetFlagSet(524288);
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (set) Token: 0x0600539B RID: 21403 RVA: 0x0015E448 File Offset: 0x0015C648
		internal override bool InternalExpanded
		{
			set
			{
				base.InternalExpanded = value;
				Hashtable obj = CategoryGridEntry.categoryStates;
				lock (obj)
				{
					CategoryGridEntry.categoryStates[this.name] = value;
				}
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x0600539C RID: 21404 RVA: 0x00013062 File Offset: 0x00011262
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.Category;
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x0600539D RID: 21405 RVA: 0x00015ECC File Offset: 0x000140CC
		public override string HelpKeyword
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x0600539E RID: 21406 RVA: 0x0015E4A0 File Offset: 0x0015C6A0
		public override string PropertyLabel
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x0600539F RID: 21407 RVA: 0x0015E4A8 File Offset: 0x0015C6A8
		internal override int PropertyLabelIndent
		{
			get
			{
				PropertyGridView gridEntryHost = this.GridEntryHost;
				return 1 + gridEntryHost.GetOutlineIconSize() + 5 + base.PropertyDepth * gridEntryHost.GetDefaultOutlineIndent();
			}
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x000F1AC4 File Offset: 0x000EFCC4
		public override string GetPropertyTextValue(object o)
		{
			return "";
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x060053A1 RID: 21409 RVA: 0x0015E4D4 File Offset: 0x0015C6D4
		public override Type PropertyType
		{
			get
			{
				return typeof(void);
			}
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x0015E4E0 File Offset: 0x0015C6E0
		public override object GetChildValueOwner(GridEntry childEntry)
		{
			return this.ParentGridEntry.GetChildValueOwner(childEntry);
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x00013062 File Offset: 0x00011262
		protected override bool CreateChildren(bool diffOldChildren)
		{
			return true;
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x0015E4F0 File Offset: 0x0015C6F0
		public override string GetTestingInfo()
		{
			string str = "object = (";
			str += base.FullLabel;
			return str + "), Category = (" + this.PropertyLabel + ")";
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x0015E528 File Offset: 0x0015C728
		public override void PaintLabel(Graphics g, Rectangle rect, Rectangle clipRect, bool selected, bool paintFullLabel)
		{
			base.PaintLabel(g, rect, clipRect, false, true);
			if (selected && this.hasFocus)
			{
				bool boldFont = (this.Flags & 64) != 0;
				Font font = base.GetFont(boldFont);
				int labelTextWidth = base.GetLabelTextWidth(this.PropertyLabel, g, font);
				int x = this.PropertyLabelIndent - 2;
				Rectangle rectangle = new Rectangle(x, rect.Y, labelTextWidth + 3, rect.Height - 1);
				if (SystemInformation.HighContrast && !base.OwnerGrid.developerOverride && AccessibilityImprovements.Level1)
				{
					ControlPaint.DrawFocusRectangle(g, rectangle, SystemColors.ControlText, base.OwnerGrid.LineColor);
				}
				else
				{
					ControlPaint.DrawFocusRectangle(g, rectangle);
				}
			}
			if (this.parentPE.GetChildIndex(this) > 0)
			{
				using (Pen pen = new Pen(this.ownerGrid.CategorySplitterColor, 1f))
				{
					g.DrawLine(pen, rect.X - 1, rect.Y - 1, rect.Width + 2, rect.Y - 1);
				}
			}
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x0015E648 File Offset: 0x0015C848
		public override void PaintValue(object val, Graphics g, Rectangle rect, Rectangle clipRect, GridEntry.PaintValueFlags paintFlags)
		{
			base.PaintValue(val, g, rect, clipRect, paintFlags & ~GridEntry.PaintValueFlags.DrawSelected);
			if (this.parentPE.GetChildIndex(this) > 0)
			{
				using (Pen pen = new Pen(this.ownerGrid.CategorySplitterColor, 1f))
				{
					g.DrawLine(pen, rect.X - 2, rect.Y - 1, rect.Width + 1, rect.Y - 1);
				}
			}
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x0015E6D4 File Offset: 0x0015C8D4
		internal override bool NotifyChildValue(GridEntry pe, int type)
		{
			return this.parentPE.NotifyChildValue(pe, type);
		}

		// Token: 0x040036C0 RID: 14016
		internal string name;

		// Token: 0x040036C1 RID: 14017
		private Brush backBrush;

		// Token: 0x040036C2 RID: 14018
		private static Hashtable categoryStates;

		// Token: 0x0200088A RID: 2186
		[ComVisible(true)]
		internal class CategoryGridEntryAccessibleObject : GridEntry.GridEntryAccessibleObject
		{
			// Token: 0x06007212 RID: 29202 RVA: 0x001A2542 File Offset: 0x001A0742
			public CategoryGridEntryAccessibleObject(CategoryGridEntry owningCategoryGridEntry) : base(owningCategoryGridEntry)
			{
			}

			// Token: 0x06007213 RID: 29203 RVA: 0x001A254C File Offset: 0x001A074C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (!base.IsOwnerGridEntryCleared())
				{
					CategoryGridEntry categoryGridEntry = this.owner as CategoryGridEntry;
					if (categoryGridEntry != null)
					{
						PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = (PropertyGridView.PropertyGridViewAccessibleObject)this.Parent;
						switch (direction)
						{
						case UnsafeNativeMethods.NavigateDirection.Parent:
							return this.Parent;
						case UnsafeNativeMethods.NavigateDirection.NextSibling:
							return propertyGridViewAccessibleObject.GetNextCategory(categoryGridEntry);
						case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
							return propertyGridViewAccessibleObject.GetPreviousCategory(categoryGridEntry);
						case UnsafeNativeMethods.NavigateDirection.FirstChild:
							return propertyGridViewAccessibleObject.GetFirstChildProperty(categoryGridEntry);
						case UnsafeNativeMethods.NavigateDirection.LastChild:
							return propertyGridViewAccessibleObject.GetLastChildProperty(categoryGridEntry);
						default:
							return base.FragmentNavigate(direction);
						}
					}
				}
				return null;
			}

			// Token: 0x06007214 RID: 29204 RVA: 0x001A25C8 File Offset: 0x001A07C8
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerGridEntryCleared() && ((AccessibilityImprovements.Level4 && (patternId == 10007 || patternId == 10013)) || base.IsPatternSupported(patternId));
			}

			// Token: 0x06007215 RID: 29205 RVA: 0x001A25F4 File Offset: 0x001A07F4
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level4)
				{
					if (propertyID == 30003)
					{
						return 50024;
					}
					if (propertyID == 30004)
					{
						if (AccessibilityImprovements.Level5)
						{
							return SR.GetString("CategoryPropertyGridLocalizedControlType");
						}
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x170018FD RID: 6397
			// (get) Token: 0x06007216 RID: 29206 RVA: 0x001A2633 File Offset: 0x001A0833
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ButtonDropDownGrid;
				}
			}

			// Token: 0x170018FE RID: 6398
			// (get) Token: 0x06007217 RID: 29207 RVA: 0x001A2638 File Offset: 0x001A0838
			internal override int Row
			{
				get
				{
					if (base.IsOwnerGridEntryCleared())
					{
						return -1;
					}
					if (!AccessibilityImprovements.Level4)
					{
						return base.Row;
					}
					PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = this.Parent as PropertyGridView.PropertyGridViewAccessibleObject;
					if (propertyGridViewAccessibleObject == null)
					{
						return -1;
					}
					PropertyGridView propertyGridView = propertyGridViewAccessibleObject.Owner as PropertyGridView;
					if (propertyGridView == null || propertyGridView.OwnerGrid == null || !propertyGridView.OwnerGrid.SortedByCategories)
					{
						return -1;
					}
					GridEntryCollection topLevelGridEntries = propertyGridView.TopLevelGridEntries;
					if (topLevelGridEntries == null)
					{
						return -1;
					}
					CategoryGridEntry categoryGridEntry = this.owner as CategoryGridEntry;
					if (categoryGridEntry == null)
					{
						return -1;
					}
					int num = 0;
					foreach (object obj in topLevelGridEntries)
					{
						if (categoryGridEntry == obj)
						{
							return num;
						}
						if (obj is CategoryGridEntry)
						{
							num++;
						}
					}
					return -1;
				}
			}
		}
	}
}
