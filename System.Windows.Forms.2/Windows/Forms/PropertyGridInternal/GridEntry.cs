using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.Windows.Forms.Internal;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000502 RID: 1282
	internal abstract class GridEntry : GridItem, ITypeDescriptorContext, IServiceProvider
	{
		// Token: 0x060053D7 RID: 21463 RVA: 0x00040AFE File Offset: 0x0003ECFE
		private static Color InvertColor(Color color)
		{
			return Color.FromArgb((int)color.A, (int)(~color.R), (int)(~color.G), (int)(~color.B));
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x0015F2D0 File Offset: 0x0015D4D0
		protected GridEntry(PropertyGrid owner, GridEntry peParent)
		{
			this.parentPE = peParent;
			this.ownerGrid = owner;
			if (peParent != null)
			{
				this.propertyDepth = peParent.PropertyDepth + 1;
				this.PropertySort = peParent.PropertySort;
				if (peParent.ForceReadOnly)
				{
					this.flags |= 1024;
					return;
				}
			}
			else
			{
				this.propertyDepth = -1;
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x0015F351 File Offset: 0x0015D551
		private int OutlineIconPadding
		{
			get
			{
				if (DpiHelper.EnableDpiChangedHighDpiImprovements && this.GridEntryHost != null)
				{
					return this.GridEntryHost.LogicalToDeviceUnits(5);
				}
				return 5;
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x060053DA RID: 21466 RVA: 0x0015F370 File Offset: 0x0015D570
		private bool colorInversionNeededInHC
		{
			get
			{
				return SystemInformation.HighContrast && !this.OwnerGrid.developerOverride && AccessibilityImprovements.Level1;
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x0015F38D File Offset: 0x0015D58D
		public AccessibleObject AccessibilityObject
		{
			get
			{
				if (this.accessibleObject == null)
				{
					this.accessibleObject = this.GetAccessibilityObject();
				}
				return this.accessibleObject;
			}
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x0015F3A9 File Offset: 0x0015D5A9
		protected virtual GridEntry.GridEntryAccessibleObject GetAccessibilityObject()
		{
			return new GridEntry.GridEntryAccessibleObject(this);
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x060053DD RID: 21469 RVA: 0x00013062 File Offset: 0x00011262
		public virtual bool AllowMerge
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x060053DE RID: 21470 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool AlwaysAllowExpand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x060053DF RID: 21471 RVA: 0x0015F3B1 File Offset: 0x0015D5B1
		internal virtual AttributeCollection Attributes
		{
			get
			{
				return TypeDescriptor.GetAttributes(this.PropertyType);
			}
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0015F3BE File Offset: 0x0015D5BE
		protected virtual Brush GetBackgroundBrush(Graphics g)
		{
			return this.GridEntryHost.GetBackgroundBrush(g);
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x060053E1 RID: 21473 RVA: 0x0015F3CC File Offset: 0x0015D5CC
		protected virtual Color LabelTextColor
		{
			get
			{
				if (this.ShouldRenderReadOnly)
				{
					return this.GridEntryHost.GrayTextColor;
				}
				return this.GridEntryHost.GetTextColor();
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x060053E2 RID: 21474 RVA: 0x0015F3ED File Offset: 0x0015D5ED
		// (set) Token: 0x060053E3 RID: 21475 RVA: 0x0015F404 File Offset: 0x0015D604
		public virtual AttributeCollection BrowsableAttributes
		{
			get
			{
				if (this.parentPE != null)
				{
					return this.parentPE.BrowsableAttributes;
				}
				return null;
			}
			set
			{
				this.parentPE.BrowsableAttributes = value;
			}
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x060053E4 RID: 21476 RVA: 0x0015F414 File Offset: 0x0015D614
		public virtual IComponent Component
		{
			get
			{
				object valueOwner = this.GetValueOwner();
				if (valueOwner is IComponent)
				{
					return (IComponent)valueOwner;
				}
				if (this.parentPE != null)
				{
					return this.parentPE.Component;
				}
				return null;
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x060053E5 RID: 21477 RVA: 0x0015F44C File Offset: 0x0015D64C
		protected virtual IComponentChangeService ComponentChangeService
		{
			get
			{
				return this.parentPE.ComponentChangeService;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x060053E6 RID: 21478 RVA: 0x0015F45C File Offset: 0x0015D65C
		public virtual IContainer Container
		{
			get
			{
				IComponent component = this.Component;
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null)
					{
						return site.Container;
					}
				}
				return null;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x0015F485 File Offset: 0x0015D685
		// (set) Token: 0x060053E8 RID: 21480 RVA: 0x0015F4A2 File Offset: 0x0015D6A2
		protected GridEntryCollection ChildCollection
		{
			get
			{
				if (this.childCollection == null)
				{
					this.childCollection = new GridEntryCollection(this, null);
				}
				return this.childCollection;
			}
			set
			{
				if (this.childCollection != value)
				{
					if (this.childCollection != null)
					{
						this.childCollection.Dispose();
						this.childCollection = null;
					}
					this.childCollection = value;
				}
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x060053E9 RID: 21481 RVA: 0x0015F4CE File Offset: 0x0015D6CE
		public int ChildCount
		{
			get
			{
				if (this.Children != null)
				{
					return this.Children.Count;
				}
				return 0;
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x060053EA RID: 21482 RVA: 0x0015F4E5 File Offset: 0x0015D6E5
		public virtual GridEntryCollection Children
		{
			get
			{
				if (this.childCollection == null && !this.Disposed)
				{
					this.CreateChildren();
				}
				return this.childCollection;
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x060053EB RID: 21483 RVA: 0x0015F504 File Offset: 0x0015D704
		// (set) Token: 0x060053EC RID: 21484 RVA: 0x0015F51B File Offset: 0x0015D71B
		public virtual PropertyTab CurrentTab
		{
			get
			{
				if (this.parentPE != null)
				{
					return this.parentPE.CurrentTab;
				}
				return null;
			}
			set
			{
				if (this.parentPE != null)
				{
					this.parentPE.CurrentTab = value;
				}
			}
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x060053ED RID: 21485 RVA: 0x00015ECC File Offset: 0x000140CC
		// (set) Token: 0x060053EE RID: 21486 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual GridEntry DefaultChild
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x060053EF RID: 21487 RVA: 0x0015F531 File Offset: 0x0015D731
		// (set) Token: 0x060053F0 RID: 21488 RVA: 0x0015F548 File Offset: 0x0015D748
		internal virtual IDesignerHost DesignerHost
		{
			get
			{
				if (this.parentPE != null)
				{
					return this.parentPE.DesignerHost;
				}
				return null;
			}
			set
			{
				if (this.parentPE != null)
				{
					this.parentPE.DesignerHost = value;
				}
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x060053F1 RID: 21489 RVA: 0x0015F55E File Offset: 0x0015D75E
		internal bool Disposed
		{
			get
			{
				return this.GetFlagSet(8192);
			}
		}

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x060053F2 RID: 21490 RVA: 0x0015F56B File Offset: 0x0015D76B
		internal virtual bool Enumerable
		{
			get
			{
				return (this.Flags & 2) != 0;
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x060053F3 RID: 21491 RVA: 0x0015F578 File Offset: 0x0015D778
		public override bool Expandable
		{
			get
			{
				bool flag = this.GetFlagSet(131072);
				if (flag && this.childCollection != null && this.childCollection.Count > 0)
				{
					return true;
				}
				if (this.GetFlagSet(524288))
				{
					return false;
				}
				if (flag && (this.cacheItems == null || this.cacheItems.lastValue == null) && this.PropertyValue == null)
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x060053F4 RID: 21492 RVA: 0x0015F5DE File Offset: 0x0015D7DE
		// (set) Token: 0x060053F5 RID: 21493 RVA: 0x0015F5E6 File Offset: 0x0015D7E6
		public override bool Expanded
		{
			get
			{
				return this.InternalExpanded;
			}
			set
			{
				this.GridEntryHost.SetExpand(this, value);
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x060053F6 RID: 21494 RVA: 0x0015F5F5 File Offset: 0x0015D7F5
		internal virtual bool ForceReadOnly
		{
			get
			{
				return (this.flags & 1024) != 0;
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x0015F606 File Offset: 0x0015D806
		// (set) Token: 0x060053F8 RID: 21496 RVA: 0x0015F62C File Offset: 0x0015D82C
		internal virtual bool InternalExpanded
		{
			get
			{
				return this.childCollection != null && this.childCollection.Count != 0 && this.GetFlagSet(65536);
			}
			set
			{
				if (!this.Expandable || value == this.InternalExpanded)
				{
					return;
				}
				if (this.childCollection != null && this.childCollection.Count > 0)
				{
					this.SetFlag(65536, value);
				}
				else
				{
					this.SetFlag(65536, false);
					if (value)
					{
						bool fVal = this.CreateChildren();
						this.SetFlag(65536, fVal);
					}
				}
				if (AccessibilityImprovements.Level1 && this.GridItemType != GridItemType.Root)
				{
					int num = this.GridEntryHost.AccessibilityGetGridEntryChildID(this);
					if (num >= 0)
					{
						PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = (PropertyGridView.PropertyGridViewAccessibleObject)this.GridEntryHost.AccessibilityObject;
						propertyGridViewAccessibleObject.NotifyClients(AccessibleEvents.StateChange, num);
						propertyGridViewAccessibleObject.NotifyClients(AccessibleEvents.NameChange, num);
					}
				}
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x060053F9 RID: 21497 RVA: 0x0015F6DC File Offset: 0x0015D8DC
		// (set) Token: 0x060053FA RID: 21498 RVA: 0x0015F8B6 File Offset: 0x0015DAB6
		internal virtual int Flags
		{
			get
			{
				if ((this.flags & -2147483648) != 0)
				{
					return this.flags;
				}
				this.flags |= int.MinValue;
				TypeConverter typeConverter = this.TypeConverter;
				UITypeEditor uitypeEditor = this.UITypeEditor;
				object instance = this.Instance;
				bool flag = this.ForceReadOnly;
				if (instance != null)
				{
					flag |= TypeDescriptor.GetAttributes(instance).Contains(InheritanceAttribute.InheritedReadOnly);
				}
				if (typeConverter.GetStandardValuesSupported(this))
				{
					this.flags |= 2;
				}
				if (!flag && typeConverter.CanConvertFrom(this, typeof(string)) && !typeConverter.GetStandardValuesExclusive(this))
				{
					this.flags |= 1;
				}
				bool flag2 = TypeDescriptor.GetAttributes(this.PropertyType)[typeof(ImmutableObjectAttribute)].Equals(ImmutableObjectAttribute.Yes);
				bool flag3 = flag2 || typeConverter.GetCreateInstanceSupported(this);
				if (flag3)
				{
					this.flags |= 512;
				}
				if (typeConverter.GetPropertiesSupported(this))
				{
					this.flags |= 131072;
					if (!flag && (this.flags & 1) == 0 && !flag2)
					{
						this.flags |= 128;
					}
				}
				if (this.Attributes.Contains(PasswordPropertyTextAttribute.Yes))
				{
					this.flags |= 4096;
				}
				if (uitypeEditor != null)
				{
					if (uitypeEditor.GetPaintValueSupported(this))
					{
						this.flags |= 4;
					}
					bool flag4 = !flag;
					if (flag4)
					{
						UITypeEditorEditStyle editStyle = uitypeEditor.GetEditStyle(this);
						if (editStyle != UITypeEditorEditStyle.Modal)
						{
							if (editStyle == UITypeEditorEditStyle.DropDown)
							{
								this.flags |= 32;
							}
						}
						else
						{
							this.flags |= 16;
							if (!flag3 && !this.PropertyType.IsValueType)
							{
								this.flags |= 128;
							}
						}
					}
				}
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x060053FB RID: 21499 RVA: 0x0015F8BF File Offset: 0x0015DABF
		// (set) Token: 0x060053FC RID: 21500 RVA: 0x0015F8C8 File Offset: 0x0015DAC8
		public bool Focus
		{
			get
			{
				return this.hasFocus;
			}
			set
			{
				if (this.Disposed)
				{
					return;
				}
				if (this.cacheItems != null)
				{
					this.cacheItems.lastValueString = null;
					this.cacheItems.useValueString = false;
					this.cacheItems.useShouldSerialize = false;
				}
				if (this.hasFocus != value)
				{
					this.hasFocus = value;
					if (value)
					{
						int num = this.GridEntryHost.AccessibilityGetGridEntryChildID(this);
						if (num >= 0)
						{
							PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = (PropertyGridView.PropertyGridViewAccessibleObject)this.GridEntryHost.AccessibilityObject;
							propertyGridViewAccessibleObject.NotifyClients(AccessibleEvents.Focus, num);
							propertyGridViewAccessibleObject.NotifyClients(AccessibleEvents.Selection, num);
							if (AccessibilityImprovements.Level3)
							{
								this.AccessibilityObject.SetFocus();
							}
						}
					}
				}
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x060053FD RID: 21501 RVA: 0x0015F96C File Offset: 0x0015DB6C
		public string FullLabel
		{
			get
			{
				string text = null;
				if (this.parentPE != null)
				{
					text = this.parentPE.FullLabel;
				}
				if (text != null)
				{
					text += ".";
				}
				else
				{
					text = "";
				}
				return text + this.PropertyLabel;
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x060053FE RID: 21502 RVA: 0x0015F9B4 File Offset: 0x0015DBB4
		public override GridItemCollection GridItems
		{
			get
			{
				if (this.Disposed)
				{
					throw new ObjectDisposedException(SR.GetString("GridItemDisposed"));
				}
				if (this.IsExpandable && this.childCollection != null && this.childCollection.Count == 0)
				{
					this.CreateChildren();
				}
				return this.Children;
			}
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x0015FA03 File Offset: 0x0015DC03
		// (set) Token: 0x06005400 RID: 21504 RVA: 0x0000A547 File Offset: 0x00008747
		internal virtual PropertyGridView GridEntryHost
		{
			get
			{
				if (this.parentPE != null)
				{
					return this.parentPE.GridEntryHost;
				}
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06005401 RID: 21505 RVA: 0x00011A20 File Offset: 0x0000FC20
		public override GridItemType GridItemType
		{
			get
			{
				return GridItemType.Property;
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06005402 RID: 21506 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool HasValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06005403 RID: 21507 RVA: 0x0015FA1C File Offset: 0x0015DC1C
		public virtual string HelpKeyword
		{
			get
			{
				string text = null;
				if (this.parentPE != null)
				{
					text = this.parentPE.HelpKeyword;
				}
				if (text == null)
				{
					text = string.Empty;
				}
				return text;
			}
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06005404 RID: 21508 RVA: 0x0015FA49 File Offset: 0x0015DC49
		internal virtual string HelpKeywordInternal
		{
			get
			{
				return this.HelpKeyword;
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06005405 RID: 21509 RVA: 0x0015FA54 File Offset: 0x0015DC54
		public virtual bool IsCustomPaint
		{
			get
			{
				if ((this.flags & -2147483648) == 0)
				{
					UITypeEditor uitypeEditor = this.UITypeEditor;
					if (uitypeEditor != null)
					{
						if ((this.flags & 4) != 0 || (this.flags & 1048576) != 0)
						{
							return (this.flags & 4) != 0;
						}
						if (uitypeEditor.GetPaintValueSupported(this))
						{
							this.flags |= 4;
							return true;
						}
						this.flags |= 1048576;
						return false;
					}
				}
				return (this.Flags & 4) != 0;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06005406 RID: 21510 RVA: 0x0015FAD5 File Offset: 0x0015DCD5
		// (set) Token: 0x06005407 RID: 21511 RVA: 0x0015FADD File Offset: 0x0015DCDD
		public virtual bool IsExpandable
		{
			get
			{
				return this.Expandable;
			}
			set
			{
				if (value != this.GetFlagSet(131072))
				{
					this.SetFlag(524288, false);
					this.SetFlag(131072, value);
				}
			}
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06005408 RID: 21512 RVA: 0x0015FB05 File Offset: 0x0015DD05
		public virtual bool IsTextEditable
		{
			get
			{
				return this.IsValueEditable && (this.Flags & 1) != 0;
			}
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06005409 RID: 21513 RVA: 0x0015FB1C File Offset: 0x0015DD1C
		public virtual bool IsValueEditable
		{
			get
			{
				return !this.ForceReadOnly && (this.Flags & 51) != 0;
			}
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x0600540A RID: 21514 RVA: 0x0015FB34 File Offset: 0x0015DD34
		public virtual object Instance
		{
			get
			{
				object valueOwner = this.GetValueOwner();
				if (this.parentPE != null && valueOwner == null)
				{
					return this.parentPE.Instance;
				}
				return valueOwner;
			}
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x0600540B RID: 21515 RVA: 0x0015FB60 File Offset: 0x0015DD60
		public override string Label
		{
			get
			{
				return this.PropertyLabel;
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x0600540C RID: 21516 RVA: 0x00015ECC File Offset: 0x000140CC
		public override PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x0600540D RID: 21517 RVA: 0x0015FB68 File Offset: 0x0015DD68
		internal virtual int PropertyLabelIndent
		{
			get
			{
				int num = this.GridEntryHost.GetOutlineIconSize() + 5;
				return (this.propertyDepth + 1) * num + 1;
			}
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0015FB8F File Offset: 0x0015DD8F
		internal virtual Point GetLabelToolTipLocation(int mouseX, int mouseY)
		{
			return this.labelTipPoint;
		}

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x0600540F RID: 21519 RVA: 0x0015FB60 File Offset: 0x0015DD60
		internal virtual string LabelToolTipText
		{
			get
			{
				return this.PropertyLabel;
			}
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06005410 RID: 21520 RVA: 0x0015FB97 File Offset: 0x0015DD97
		public virtual bool NeedsDropDownButton
		{
			get
			{
				return (this.Flags & 32) != 0;
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06005411 RID: 21521 RVA: 0x0015FBA5 File Offset: 0x0015DDA5
		public virtual bool NeedsCustomEditorButton
		{
			get
			{
				return (this.Flags & 16) != 0 && (this.IsValueEditable || (this.Flags & 128) != 0);
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06005412 RID: 21522 RVA: 0x0015FBCD File Offset: 0x0015DDCD
		public PropertyGrid OwnerGrid
		{
			get
			{
				return this.ownerGrid;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06005413 RID: 21523 RVA: 0x0015FBD8 File Offset: 0x0015DDD8
		// (set) Token: 0x06005414 RID: 21524 RVA: 0x0015FC44 File Offset: 0x0015DE44
		public Rectangle OutlineRect
		{
			get
			{
				if (!this.outlineRect.IsEmpty)
				{
					return this.outlineRect;
				}
				PropertyGridView gridEntryHost = this.GridEntryHost;
				int outlineIconSize = gridEntryHost.GetOutlineIconSize();
				int num = outlineIconSize + this.OutlineIconPadding;
				int x = this.propertyDepth * num + this.OutlineIconPadding / 2;
				int y = (gridEntryHost.GetGridEntryHeight() - outlineIconSize) / 2;
				this.outlineRect = new Rectangle(x, y, outlineIconSize, outlineIconSize);
				return this.outlineRect;
			}
			set
			{
				if (value != this.outlineRect)
				{
					this.outlineRect = value;
				}
			}
		}

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x06005415 RID: 21525 RVA: 0x0015FC5B File Offset: 0x0015DE5B
		// (set) Token: 0x06005416 RID: 21526 RVA: 0x0015FC64 File Offset: 0x0015DE64
		public virtual GridEntry ParentGridEntry
		{
			get
			{
				return this.parentPE;
			}
			set
			{
				this.parentPE = value;
				if (value != null)
				{
					this.propertyDepth = value.PropertyDepth + 1;
					if (this.childCollection != null)
					{
						for (int i = 0; i < this.childCollection.Count; i++)
						{
							this.childCollection.GetEntry(i).ParentGridEntry = this;
						}
					}
				}
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06005417 RID: 21527 RVA: 0x0015FCBC File Offset: 0x0015DEBC
		public override GridItem Parent
		{
			get
			{
				if (this.Disposed)
				{
					throw new ObjectDisposedException(SR.GetString("GridItemDisposed"));
				}
				return this.ParentGridEntry;
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06005418 RID: 21528 RVA: 0x0015FCE9 File Offset: 0x0015DEE9
		public virtual string PropertyCategory
		{
			get
			{
				return CategoryAttribute.Default.Category;
			}
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x0015FCF5 File Offset: 0x0015DEF5
		public virtual int PropertyDepth
		{
			get
			{
				return this.propertyDepth;
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x0600541A RID: 21530 RVA: 0x00015ECC File Offset: 0x000140CC
		public virtual string PropertyDescription
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x0600541B RID: 21531 RVA: 0x00015ECC File Offset: 0x000140CC
		public virtual string PropertyLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x0600541C RID: 21532 RVA: 0x0015FB60 File Offset: 0x0015DD60
		public virtual string PropertyName
		{
			get
			{
				return this.PropertyLabel;
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x0600541D RID: 21533 RVA: 0x0015FD00 File Offset: 0x0015DF00
		public virtual Type PropertyType
		{
			get
			{
				object propertyValue = this.PropertyValue;
				if (propertyValue != null)
				{
					return propertyValue.GetType();
				}
				return null;
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x0600541E RID: 21534 RVA: 0x0015FD1F File Offset: 0x0015DF1F
		// (set) Token: 0x0600541F RID: 21535 RVA: 0x000072B6 File Offset: 0x000054B6
		public virtual object PropertyValue
		{
			get
			{
				if (this.cacheItems != null)
				{
					return this.cacheItems.lastValue;
				}
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06005420 RID: 21536 RVA: 0x0015FD36 File Offset: 0x0015DF36
		public virtual bool ShouldRenderPassword
		{
			get
			{
				return (this.Flags & 4096) != 0;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06005421 RID: 21537 RVA: 0x0015FD47 File Offset: 0x0015DF47
		public virtual bool ShouldRenderReadOnly
		{
			get
			{
				return this.ForceReadOnly || (this.Flags & 256) != 0 || (!this.IsValueEditable && (this.Flags & 128) == 0);
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06005422 RID: 21538 RVA: 0x0015FD7C File Offset: 0x0015DF7C
		internal virtual TypeConverter TypeConverter
		{
			get
			{
				if (this.converter == null)
				{
					object propertyValue = this.PropertyValue;
					if (propertyValue == null)
					{
						this.converter = TypeDescriptor.GetConverter(this.PropertyType);
					}
					else
					{
						this.converter = TypeDescriptor.GetConverter(propertyValue);
					}
				}
				return this.converter;
			}
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06005423 RID: 21539 RVA: 0x0015FDC0 File Offset: 0x0015DFC0
		internal virtual UITypeEditor UITypeEditor
		{
			get
			{
				if (this.editor == null && this.PropertyType != null)
				{
					this.editor = (UITypeEditor)TypeDescriptor.GetEditor(this.PropertyType, typeof(UITypeEditor));
				}
				return this.editor;
			}
		}

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x06005424 RID: 21540 RVA: 0x0015FDFE File Offset: 0x0015DFFE
		public override object Value
		{
			get
			{
				return this.PropertyValue;
			}
		}

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x06005425 RID: 21541 RVA: 0x0015FE06 File Offset: 0x0015E006
		// (set) Token: 0x06005426 RID: 21542 RVA: 0x0015FE1C File Offset: 0x0015E01C
		internal Point ValueToolTipLocation
		{
			get
			{
				if (!this.ShouldRenderPassword)
				{
					return this.valueTipPoint;
				}
				return GridEntry.InvalidPoint;
			}
			set
			{
				this.valueTipPoint = value;
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06005427 RID: 21543 RVA: 0x0015FE28 File Offset: 0x0015E028
		internal int VisibleChildCount
		{
			get
			{
				if (!this.Expanded)
				{
					return 0;
				}
				int childCount = this.ChildCount;
				int num = childCount;
				for (int i = 0; i < childCount; i++)
				{
					num += this.ChildCollection.GetEntry(i).VisibleChildCount;
				}
				return num;
			}
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0015FE69 File Offset: 0x0015E069
		public virtual void AddOnLabelClick(EventHandler h)
		{
			this.AddEventHandler(GridEntry.EVENT_LABEL_CLICK, h);
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0015FE77 File Offset: 0x0015E077
		public virtual void AddOnLabelDoubleClick(EventHandler h)
		{
			this.AddEventHandler(GridEntry.EVENT_LABEL_DBLCLICK, h);
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0015FE85 File Offset: 0x0015E085
		public virtual void AddOnValueClick(EventHandler h)
		{
			this.AddEventHandler(GridEntry.EVENT_VALUE_CLICK, h);
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x0015FE93 File Offset: 0x0015E093
		public virtual void AddOnValueDoubleClick(EventHandler h)
		{
			this.AddEventHandler(GridEntry.EVENT_VALUE_DBLCLICK, h);
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x0015FEA1 File Offset: 0x0015E0A1
		public virtual void AddOnOutlineClick(EventHandler h)
		{
			this.AddEventHandler(GridEntry.EVENT_OUTLINE_CLICK, h);
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0015FEAF File Offset: 0x0015E0AF
		public virtual void AddOnOutlineDoubleClick(EventHandler h)
		{
			this.AddEventHandler(GridEntry.EVENT_OUTLINE_DBLCLICK, h);
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0015FEBD File Offset: 0x0015E0BD
		public virtual void AddOnRecreateChildren(GridEntryRecreateChildrenEventHandler h)
		{
			this.AddEventHandler(GridEntry.EVENT_RECREATE_CHILDREN, h);
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0015FECB File Offset: 0x0015E0CB
		internal void ClearCachedValues()
		{
			this.ClearCachedValues(true);
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x0015FED4 File Offset: 0x0015E0D4
		internal void ClearCachedValues(bool clearChildren)
		{
			if (this.cacheItems != null)
			{
				this.cacheItems.useValueString = false;
				this.cacheItems.lastValue = null;
				this.cacheItems.useShouldSerialize = false;
			}
			if (clearChildren)
			{
				for (int i = 0; i < this.ChildCollection.Count; i++)
				{
					this.ChildCollection.GetEntry(i).ClearCachedValues();
				}
			}
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x0015FF37 File Offset: 0x0015E137
		public object ConvertTextToValue(string text)
		{
			if (this.TypeConverter.CanConvertFrom(this, typeof(string)))
			{
				return this.TypeConverter.ConvertFromString(this, text);
			}
			return text;
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x0015FF60 File Offset: 0x0015E160
		internal static IRootGridEntry Create(PropertyGridView view, object[] rgobjs, IServiceProvider baseProvider, IDesignerHost currentHost, PropertyTab tab, PropertySort initialSortType)
		{
			IRootGridEntry result = null;
			if (rgobjs == null || rgobjs.Length == 0)
			{
				return null;
			}
			try
			{
				if (rgobjs.Length == 1)
				{
					result = new SingleSelectRootGridEntry(view, rgobjs[0], baseProvider, currentHost, tab, initialSortType);
				}
				else
				{
					result = new MultiSelectRootGridEntry(view, rgobjs, baseProvider, currentHost, tab, initialSortType);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return result;
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0015FFB8 File Offset: 0x0015E1B8
		protected virtual bool CreateChildren()
		{
			return this.CreateChildren(false);
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x0015FFC4 File Offset: 0x0015E1C4
		protected virtual bool CreateChildren(bool diffOldChildren)
		{
			if (!this.GetFlagSet(131072))
			{
				if (this.childCollection != null)
				{
					this.childCollection.Clear();
				}
				else
				{
					this.childCollection = new GridEntryCollection(this, new GridEntry[0]);
				}
				return false;
			}
			if (!diffOldChildren && this.childCollection != null && this.childCollection.Count > 0)
			{
				return true;
			}
			GridEntry[] propEntries = this.GetPropEntries(this, this.PropertyValue, this.PropertyType);
			bool flag = propEntries != null && propEntries.Length != 0;
			if (diffOldChildren && this.childCollection != null && this.childCollection.Count > 0)
			{
				bool flag2 = true;
				if (propEntries.Length == this.childCollection.Count)
				{
					for (int i = 0; i < propEntries.Length; i++)
					{
						if (!propEntries[i].NonParentEquals(this.childCollection[i]))
						{
							flag2 = false;
							break;
						}
					}
				}
				else
				{
					flag2 = false;
				}
				if (flag2)
				{
					return true;
				}
			}
			if (!flag)
			{
				this.SetFlag(524288, true);
				if (this.childCollection != null)
				{
					this.childCollection.Clear();
				}
				else
				{
					this.childCollection = new GridEntryCollection(this, new GridEntry[0]);
				}
				if (this.InternalExpanded)
				{
					this.InternalExpanded = false;
				}
			}
			else if (this.childCollection != null)
			{
				this.childCollection.Clear();
				this.childCollection.AddRange(propEntries);
			}
			else
			{
				this.childCollection = new GridEntryCollection(this, propEntries);
			}
			return flag;
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x00160114 File Offset: 0x0015E314
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x00160124 File Offset: 0x0015E324
		protected virtual void Dispose(bool disposing)
		{
			this.flags |= int.MinValue;
			this.SetFlag(8192, true);
			this.cacheItems = null;
			this.converter = null;
			this.editor = null;
			this.accessibleObject = null;
			if (disposing)
			{
				this.DisposeChildren();
			}
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x00160174 File Offset: 0x0015E374
		public virtual void DisposeChildren()
		{
			if (this.childCollection != null)
			{
				this.childCollection.Dispose();
				this.childCollection = null;
			}
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x00160190 File Offset: 0x0015E390
		~GridEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x001601C0 File Offset: 0x0015E3C0
		internal virtual void EditPropertyValue(PropertyGridView iva)
		{
			if (this.UITypeEditor != null)
			{
				try
				{
					object propertyValue = this.PropertyValue;
					object obj = this.UITypeEditor.EditValue(this, this, propertyValue);
					if (!this.Disposed)
					{
						if (obj != propertyValue && this.IsValueEditable)
						{
							iva.CommitValue(this, obj, true);
						}
						if (this.InternalExpanded)
						{
							PropertyGridView.GridPositionData gridPositionData = this.GridEntryHost.CaptureGridPositionData();
							this.InternalExpanded = false;
							this.RecreateChildren();
							gridPositionData.Restore(this.GridEntryHost);
						}
						else
						{
							this.RecreateChildren();
						}
					}
				}
				catch (Exception ex)
				{
					IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
					if (iuiservice != null)
					{
						iuiservice.ShowError(ex);
					}
					else
					{
						RTLAwareMessageBox.Show(this.GridEntryHost, ex.Message, SR.GetString("PBRSErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					}
				}
			}
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x001602A4 File Offset: 0x0015E4A4
		public override bool Equals(object obj)
		{
			return this.NonParentEquals(obj) && ((GridEntry)obj).ParentGridEntry == this.ParentGridEntry;
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x001602C4 File Offset: 0x0015E4C4
		public virtual object FindPropertyValue(string propertyName, Type propertyType)
		{
			object valueOwner = this.GetValueOwner();
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(valueOwner)[propertyName];
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == propertyType)
			{
				return propertyDescriptor.GetValue(valueOwner);
			}
			if (this.parentPE != null)
			{
				return this.parentPE.FindPropertyValue(propertyName, propertyType);
			}
			return null;
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x00160315 File Offset: 0x0015E515
		internal virtual int GetChildIndex(GridEntry pe)
		{
			return this.Children.GetEntry(pe);
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00160324 File Offset: 0x0015E524
		public virtual IComponent[] GetComponents()
		{
			IComponent component = this.Component;
			if (component != null)
			{
				return new IComponent[]
				{
					component
				};
			}
			return null;
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x00160348 File Offset: 0x0015E548
		protected int GetLabelTextWidth(string labelText, Graphics g, Font f)
		{
			if (this.cacheItems == null)
			{
				this.cacheItems = new GridEntry.CacheItems();
			}
			else if (this.cacheItems.useCompatTextRendering == this.ownerGrid.UseCompatibleTextRendering && this.cacheItems.lastLabel == labelText && f.Equals(this.cacheItems.lastLabelFont))
			{
				return this.cacheItems.lastLabelWidth;
			}
			SizeF sizeF = PropertyGrid.MeasureTextHelper.MeasureText(this.ownerGrid, g, labelText, f);
			this.cacheItems.lastLabelWidth = (int)sizeF.Width;
			this.cacheItems.lastLabel = labelText;
			this.cacheItems.lastLabelFont = f;
			this.cacheItems.useCompatTextRendering = this.ownerGrid.UseCompatibleTextRendering;
			return this.cacheItems.lastLabelWidth;
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x00160410 File Offset: 0x0015E610
		internal int GetValueTextWidth(string valueString, Graphics g, Font f)
		{
			if (this.cacheItems == null)
			{
				this.cacheItems = new GridEntry.CacheItems();
			}
			else if (this.cacheItems.lastValueTextWidth != -1 && this.cacheItems.lastValueString == valueString && f.Equals(this.cacheItems.lastValueFont))
			{
				return this.cacheItems.lastValueTextWidth;
			}
			this.cacheItems.lastValueTextWidth = (int)g.MeasureString(valueString, f).Width;
			this.cacheItems.lastValueString = valueString;
			this.cacheItems.lastValueFont = f;
			return this.cacheItems.lastValueTextWidth;
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x001604B1 File Offset: 0x0015E6B1
		internal bool GetMultipleLines(string valueString)
		{
			return valueString.IndexOf('\n') > 0 || valueString.IndexOf('\r') > 0;
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x001604CC File Offset: 0x0015E6CC
		public virtual object GetValueOwner()
		{
			if (this.parentPE == null)
			{
				return this.PropertyValue;
			}
			return this.parentPE.GetChildValueOwner(this);
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x001604EC File Offset: 0x0015E6EC
		public virtual object[] GetValueOwners()
		{
			object valueOwner = this.GetValueOwner();
			if (valueOwner != null)
			{
				return new object[]
				{
					valueOwner
				};
			}
			return null;
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x0015FDFE File Offset: 0x0015DFFE
		public virtual object GetChildValueOwner(GridEntry childEntry)
		{
			return this.PropertyValue;
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x00160510 File Offset: 0x0015E710
		public virtual string GetTestingInfo()
		{
			string text = "object = (";
			string text2 = this.GetPropertyTextValue();
			if (text2 == null)
			{
				text2 = "(null)";
			}
			else
			{
				text2 = text2.Replace('\0', ' ');
			}
			Type type = this.PropertyType;
			if (type == null)
			{
				type = typeof(object);
			}
			text += this.FullLabel;
			return string.Concat(new string[]
			{
				text,
				"), property = (",
				this.PropertyLabel,
				",",
				type.AssemblyQualifiedName,
				"), value = [",
				text2,
				"], expandable = ",
				this.Expandable.ToString(),
				", readOnly = ",
				this.ShouldRenderReadOnly.ToString()
			});
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x001605DB File Offset: 0x0015E7DB
		public virtual Type GetValueType()
		{
			return this.PropertyType;
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x001605E4 File Offset: 0x0015E7E4
		protected virtual GridEntry[] GetPropEntries(GridEntry peParent, object obj, Type objType)
		{
			if (obj == null)
			{
				return null;
			}
			GridEntry[] array = null;
			Attribute[] array2 = new Attribute[this.BrowsableAttributes.Count];
			this.BrowsableAttributes.CopyTo(array2, 0);
			PropertyTab currentTab = this.CurrentTab;
			try
			{
				bool flag = this.ForceReadOnly;
				if (!flag)
				{
					ReadOnlyAttribute readOnlyAttribute = (ReadOnlyAttribute)TypeDescriptor.GetAttributes(obj)[typeof(ReadOnlyAttribute)];
					flag = (readOnlyAttribute != null && !readOnlyAttribute.IsDefaultAttribute());
				}
				if (this.TypeConverter.GetPropertiesSupported(this) || this.AlwaysAllowExpand)
				{
					PropertyDescriptor propertyDescriptor = null;
					PropertyDescriptorCollection propertyDescriptorCollection;
					if (currentTab != null)
					{
						propertyDescriptorCollection = currentTab.GetProperties(this, obj, array2);
						propertyDescriptor = currentTab.GetDefaultProperty(obj);
					}
					else
					{
						propertyDescriptorCollection = this.TypeConverter.GetProperties(this, obj, array2);
						propertyDescriptor = TypeDescriptor.GetDefaultProperty(obj);
					}
					if (propertyDescriptorCollection == null)
					{
						return null;
					}
					if ((this.PropertySort & PropertySort.Alphabetical) != PropertySort.NoSort)
					{
						if (objType == null || !objType.IsArray)
						{
							propertyDescriptorCollection = propertyDescriptorCollection.Sort(GridEntry.DisplayNameComparer);
						}
						PropertyDescriptor[] array3 = new PropertyDescriptor[propertyDescriptorCollection.Count];
						propertyDescriptorCollection.CopyTo(array3, 0);
						propertyDescriptorCollection = new PropertyDescriptorCollection(this.SortParenProperties(array3));
					}
					if (propertyDescriptor == null && propertyDescriptorCollection.Count > 0)
					{
						propertyDescriptor = propertyDescriptorCollection[0];
					}
					if ((propertyDescriptorCollection == null || propertyDescriptorCollection.Count == 0) && objType != null && objType.IsArray && obj != null)
					{
						Array array4 = (Array)obj;
						array = new GridEntry[array4.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = new ArrayElementGridEntry(this.ownerGrid, peParent, i);
						}
					}
					else
					{
						bool createInstanceSupported = this.TypeConverter.GetCreateInstanceSupported(this);
						array = new GridEntry[propertyDescriptorCollection.Count];
						int num = 0;
						foreach (object obj2 in propertyDescriptorCollection)
						{
							PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)obj2;
							bool hide = false;
							try
							{
								object component = obj;
								if (obj is ICustomTypeDescriptor)
								{
									component = ((ICustomTypeDescriptor)obj).GetPropertyOwner(propertyDescriptor2);
								}
								propertyDescriptor2.GetValue(component);
							}
							catch (Exception ex)
							{
								bool enabled = GridEntry.PbrsAssertPropsSwitch.Enabled;
								hide = true;
							}
							GridEntry gridEntry;
							if (createInstanceSupported)
							{
								gridEntry = new ImmutablePropertyDescriptorGridEntry(this.ownerGrid, peParent, propertyDescriptor2, hide);
							}
							else
							{
								gridEntry = new PropertyDescriptorGridEntry(this.ownerGrid, peParent, propertyDescriptor2, hide);
							}
							if (flag)
							{
								gridEntry.flags |= 1024;
							}
							if (propertyDescriptor2.Equals(propertyDescriptor))
							{
								this.DefaultChild = gridEntry;
							}
							array[num++] = gridEntry;
						}
					}
				}
			}
			catch (Exception ex2)
			{
			}
			return array;
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x001608C4 File Offset: 0x0015EAC4
		public virtual void ResetPropertyValue()
		{
			this.NotifyValue(1);
			this.Refresh();
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x001608D4 File Offset: 0x0015EAD4
		public virtual bool CanResetPropertyValue()
		{
			return this.NotifyValue(2);
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x001608DD File Offset: 0x0015EADD
		public virtual bool DoubleClickPropertyValue()
		{
			return this.NotifyValue(3);
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x001608E6 File Offset: 0x0015EAE6
		public virtual string GetPropertyTextValue()
		{
			return this.GetPropertyTextValue(this.PropertyValue);
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x001608F4 File Offset: 0x0015EAF4
		public virtual string GetPropertyTextValue(object value)
		{
			string text = null;
			TypeConverter typeConverter = this.TypeConverter;
			try
			{
				text = typeConverter.ConvertToString(this, value);
			}
			catch (Exception ex)
			{
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x00160934 File Offset: 0x0015EB34
		public virtual object[] GetPropertyValueList()
		{
			ICollection standardValues = this.TypeConverter.GetStandardValues(this);
			if (standardValues != null)
			{
				object[] array = new object[standardValues.Count];
				standardValues.CopyTo(array, 0);
				return array;
			}
			return new object[0];
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x00160970 File Offset: 0x0015EB70
		public override int GetHashCode()
		{
			object propertyLabel = this.PropertyLabel;
			object propertyType = this.PropertyType;
			uint num = (uint)((propertyLabel == null) ? 0 : propertyLabel.GetHashCode());
			uint num2 = (uint)((propertyType == null) ? 0 : propertyType.GetHashCode());
			uint hashCode = (uint)base.GetType().GetHashCode();
			return (int)(num ^ (num2 << 13 | num2 >> 19) ^ (hashCode << 26 | hashCode >> 6));
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x001609C8 File Offset: 0x0015EBC8
		protected virtual bool GetFlagSet(int flag)
		{
			return (flag & this.Flags) != 0;
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x001609D5 File Offset: 0x0015EBD5
		protected Font GetFont(bool boldFont)
		{
			if (boldFont)
			{
				return this.GridEntryHost.GetBoldFont();
			}
			return this.GridEntryHost.GetBaseFont();
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x001609F1 File Offset: 0x0015EBF1
		protected IntPtr GetHfont(bool boldFont)
		{
			if (boldFont)
			{
				return this.GridEntryHost.GetBoldHfont();
			}
			return this.GridEntryHost.GetBaseHfont();
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x00160A0D File Offset: 0x0015EC0D
		public virtual object GetService(Type serviceType)
		{
			if (serviceType == typeof(GridItem))
			{
				return this;
			}
			if (this.parentPE != null)
			{
				return this.parentPE.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x00160A3C File Offset: 0x0015EC3C
		internal virtual bool NonParentEquals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (!(obj is GridEntry))
			{
				return false;
			}
			GridEntry gridEntry = (GridEntry)obj;
			return gridEntry.PropertyLabel.Equals(this.PropertyLabel) && gridEntry.PropertyType.Equals(this.PropertyType) && gridEntry.PropertyDepth == this.PropertyDepth;
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x00160A9C File Offset: 0x0015EC9C
		public virtual void PaintLabel(Graphics g, Rectangle rect, Rectangle clipRect, bool selected, bool paintFullLabel)
		{
			PropertyGridView gridEntryHost = this.GridEntryHost;
			string propertyLabel = this.PropertyLabel;
			int num = gridEntryHost.GetOutlineIconSize() + 5;
			Brush brush = selected ? gridEntryHost.GetSelectedItemWithFocusBackBrush(g) : this.GetBackgroundBrush(g);
			if (selected && !this.hasFocus)
			{
				brush = gridEntryHost.GetLineBrush(g);
			}
			bool flag = (this.Flags & 64) != 0;
			Font font = this.GetFont(flag);
			int labelTextWidth = this.GetLabelTextWidth(propertyLabel, g, font);
			int num2 = paintFullLabel ? labelTextWidth : 0;
			int num3 = rect.X + this.PropertyLabelIndent;
			Brush brush2 = brush;
			if (paintFullLabel && num2 >= rect.Width - (num3 + 2))
			{
				int num4 = num3 + num2 + 2;
				g.FillRectangle(brush2, num - 1, rect.Y, num4 - num + 3, rect.Height);
				Pen pen = new Pen(gridEntryHost.GetLineColor());
				g.DrawLine(pen, num4, rect.Y, num4, rect.Height);
				pen.Dispose();
				rect.Width = num4 - rect.X;
			}
			else
			{
				g.FillRectangle(brush2, rect.X, rect.Y, rect.Width, rect.Height);
			}
			Brush lineBrush = gridEntryHost.GetLineBrush(g);
			g.FillRectangle(lineBrush, rect.X, rect.Y, num, rect.Height);
			if (selected && this.hasFocus)
			{
				g.FillRectangle(gridEntryHost.GetSelectedItemWithFocusBackBrush(g), num3, rect.Y, rect.Width - num3 - 1, rect.Height);
			}
			int num5 = Math.Min(rect.Width - num3 - 1, labelTextWidth + 2);
			Rectangle rectangle = new Rectangle(num3, rect.Y + 1, num5, rect.Height - 1);
			if (!Rectangle.Intersect(rectangle, clipRect).IsEmpty)
			{
				Region clip = g.Clip;
				g.SetClip(rectangle);
				bool flag2 = this.colorInversionNeededInHC && (flag || (selected && !this.hasFocus));
				Color color = (selected && this.hasFocus) ? gridEntryHost.GetSelectedItemWithFocusForeColor() : (flag2 ? GridEntry.InvertColor(this.ownerGrid.LineColor) : g.GetNearestColor(this.LabelTextColor));
				if (this.ownerGrid.UseCompatibleTextRendering)
				{
					using (Brush brush3 = new SolidBrush(color))
					{
						StringFormat stringFormat = new StringFormat(StringFormatFlags.NoWrap);
						stringFormat.Trimming = StringTrimming.None;
						g.DrawString(propertyLabel, font, brush3, rectangle, stringFormat);
						goto IL_293;
					}
				}
				TextRenderer.DrawText(g, propertyLabel, font, rectangle, color, PropertyGrid.MeasureTextHelper.GetTextRendererFlags());
				IL_293:
				g.SetClip(clip, CombineMode.Replace);
				clip.Dispose();
				if (num5 <= labelTextWidth)
				{
					this.labelTipPoint = new Point(num3 + 2, rect.Y + 1);
				}
				else
				{
					this.labelTipPoint = GridEntry.InvalidPoint;
				}
			}
			rect.Y--;
			rect.Height += 2;
			this.PaintOutline(g, rect);
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x00160DB0 File Offset: 0x0015EFB0
		public virtual void PaintOutline(Graphics g, Rectangle r)
		{
			if (this.GridEntryHost.IsExplorerTreeSupported)
			{
				if (!this.lastPaintWithExplorerStyle)
				{
					this.outlineRect = Rectangle.Empty;
					this.lastPaintWithExplorerStyle = true;
				}
				this.PaintOutlineWithExplorerTreeStyle(g, r, (DpiHelper.EnableDpiChangedHighDpiImprovements && this.GridEntryHost != null) ? this.GridEntryHost.HandleInternal : IntPtr.Zero);
				return;
			}
			if (this.lastPaintWithExplorerStyle)
			{
				this.outlineRect = Rectangle.Empty;
				this.lastPaintWithExplorerStyle = false;
			}
			this.PaintOutlineWithClassicStyle(g, r);
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x00160E30 File Offset: 0x0015F030
		private void PaintOutlineWithExplorerTreeStyle(Graphics g, Rectangle r, IntPtr handle)
		{
			if (this.Expandable)
			{
				bool internalExpanded = this.InternalExpanded;
				Rectangle rectangle = this.OutlineRect;
				rectangle = Rectangle.Intersect(r, rectangle);
				if (rectangle.IsEmpty)
				{
					return;
				}
				VisualStyleElement element;
				if (internalExpanded)
				{
					element = VisualStyleElement.ExplorerTreeView.Glyph.Opened;
				}
				else
				{
					element = VisualStyleElement.ExplorerTreeView.Glyph.Closed;
				}
				if (this.colorInversionNeededInHC)
				{
					Color color = GridEntry.InvertColor(this.ownerGrid.LineColor);
					if (g != null)
					{
						Brush brush = new SolidBrush(color);
						g.FillRectangle(brush, rectangle);
						brush.Dispose();
					}
				}
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(element);
				visualStyleRenderer.DrawBackground(g, rectangle, handle);
			}
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x00160EC0 File Offset: 0x0015F0C0
		private void PaintOutlineWithClassicStyle(Graphics g, Rectangle r)
		{
			if (this.Expandable)
			{
				bool internalExpanded = this.InternalExpanded;
				Rectangle rectangle = this.OutlineRect;
				rectangle = Rectangle.Intersect(r, rectangle);
				if (rectangle.IsEmpty)
				{
					return;
				}
				Brush backgroundBrush = this.GetBackgroundBrush(g);
				Color color = this.GridEntryHost.GetTextColor();
				if (this.colorInversionNeededInHC)
				{
					color = GridEntry.InvertColor(this.ownerGrid.LineColor);
				}
				else
				{
					g.FillRectangle(backgroundBrush, rectangle);
				}
				Pen pen;
				if (color.IsSystemColor)
				{
					pen = SystemPens.FromSystemColor(color);
				}
				else
				{
					pen = new Pen(color);
				}
				g.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				int num = 2;
				g.DrawLine(pen, rectangle.X + num, rectangle.Y + rectangle.Height / 2, rectangle.X + rectangle.Width - num - 1, rectangle.Y + rectangle.Height / 2);
				if (!internalExpanded)
				{
					g.DrawLine(pen, rectangle.X + rectangle.Width / 2, rectangle.Y + num, rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height - num - 1);
				}
				if (!color.IsSystemColor)
				{
					pen.Dispose();
				}
			}
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x00161018 File Offset: 0x0015F218
		public virtual void PaintValue(object val, Graphics g, Rectangle rect, Rectangle clipRect, GridEntry.PaintValueFlags paintFlags)
		{
			PropertyGridView gridEntryHost = this.GridEntryHost;
			int num = 0;
			Color color = gridEntryHost.GetTextColor();
			if (this.ShouldRenderReadOnly)
			{
				color = this.GridEntryHost.GrayTextColor;
			}
			string text;
			if ((paintFlags & GridEntry.PaintValueFlags.FetchValue) != GridEntry.PaintValueFlags.None)
			{
				if (this.cacheItems != null && this.cacheItems.useValueString)
				{
					text = this.cacheItems.lastValueString;
					val = this.cacheItems.lastValue;
				}
				else
				{
					val = this.PropertyValue;
					text = this.GetPropertyTextValue(val);
					if (this.cacheItems == null)
					{
						this.cacheItems = new GridEntry.CacheItems();
					}
					this.cacheItems.lastValueString = text;
					this.cacheItems.useValueString = true;
					this.cacheItems.lastValueTextWidth = -1;
					this.cacheItems.lastValueFont = null;
					this.cacheItems.lastValue = val;
				}
			}
			else
			{
				text = this.GetPropertyTextValue(val);
			}
			Brush brush = this.GetBackgroundBrush(g);
			if ((paintFlags & GridEntry.PaintValueFlags.DrawSelected) != GridEntry.PaintValueFlags.None)
			{
				brush = gridEntryHost.GetSelectedItemWithFocusBackBrush(g);
				color = gridEntryHost.GetSelectedItemWithFocusForeColor();
			}
			Brush brush2 = brush;
			g.FillRectangle(brush2, clipRect);
			if (this.IsCustomPaint)
			{
				num = gridEntryHost.GetValuePaintIndent();
				Rectangle rectangle = new Rectangle(rect.X + 1, rect.Y + 1, gridEntryHost.GetValuePaintWidth(), gridEntryHost.GetGridEntryHeight() - 2);
				if (!Rectangle.Intersect(rectangle, clipRect).IsEmpty)
				{
					UITypeEditor uitypeEditor = this.UITypeEditor;
					if (uitypeEditor != null)
					{
						uitypeEditor.PaintValue(new PaintValueEventArgs(this, val, g, rectangle));
					}
					int num2 = rectangle.Width;
					rectangle.Width = num2 - 1;
					num2 = rectangle.Height;
					rectangle.Height = num2 - 1;
					g.DrawRectangle(SystemPens.WindowText, rectangle);
				}
			}
			rect.X += num + gridEntryHost.GetValueStringIndent();
			rect.Width -= num + 2 * gridEntryHost.GetValueStringIndent();
			bool boldFont = (paintFlags & GridEntry.PaintValueFlags.CheckShouldSerialize) != GridEntry.PaintValueFlags.None && this.ShouldSerializePropertyValue();
			if (text != null && text.Length > 0)
			{
				Font font = this.GetFont(boldFont);
				if (text.Length > 1000)
				{
					text = text.Substring(0, 1000);
				}
				int valueTextWidth = this.GetValueTextWidth(text, g, font);
				bool flag = false;
				if (valueTextWidth >= rect.Width || this.GetMultipleLines(text))
				{
					flag = true;
				}
				if (Rectangle.Intersect(rect, clipRect).IsEmpty)
				{
					return;
				}
				if ((paintFlags & GridEntry.PaintValueFlags.PaintInPlace) != GridEntry.PaintValueFlags.None)
				{
					rect.Offset(1, 2);
				}
				else
				{
					rect.Offset(1, 1);
				}
				Matrix transform = g.Transform;
				IntPtr hdc = g.GetHdc();
				IntNativeMethods.RECT rect2 = IntNativeMethods.RECT.FromXYWH(rect.X + (int)transform.OffsetX + 2, rect.Y + (int)transform.OffsetY - 1, rect.Width - 4, rect.Height);
				IntPtr handle = this.GetHfont(boldFont);
				int crColor = 0;
				int clr = 0;
				Color color2 = ((paintFlags & GridEntry.PaintValueFlags.DrawSelected) != GridEntry.PaintValueFlags.None) ? this.GridEntryHost.GetSelectedItemWithFocusBackColor() : this.GridEntryHost.BackColor;
				try
				{
					crColor = SafeNativeMethods.SetTextColor(new HandleRef(g, hdc), SafeNativeMethods.RGBToCOLORREF(color.ToArgb()));
					clr = SafeNativeMethods.SetBkColor(new HandleRef(g, hdc), SafeNativeMethods.RGBToCOLORREF(color2.ToArgb()));
					handle = SafeNativeMethods.SelectObject(new HandleRef(g, hdc), new HandleRef(null, handle));
					int num3 = 10592;
					if (gridEntryHost.DrawValuesRightToLeft)
					{
						num3 |= 131074;
					}
					if (this.ShouldRenderPassword)
					{
						if (GridEntry.passwordReplaceChar == '\0')
						{
							if (Environment.OSVersion.Version.Major > 4)
							{
								GridEntry.passwordReplaceChar = '●';
							}
							else
							{
								GridEntry.passwordReplaceChar = '*';
							}
						}
						text = new string(GridEntry.passwordReplaceChar, text.Length);
					}
					IntUnsafeNativeMethods.DrawText(new HandleRef(g, hdc), text, ref rect2, num3);
				}
				finally
				{
					SafeNativeMethods.SetTextColor(new HandleRef(g, hdc), crColor);
					SafeNativeMethods.SetBkColor(new HandleRef(g, hdc), clr);
					handle = SafeNativeMethods.SelectObject(new HandleRef(g, hdc), new HandleRef(null, handle));
					g.ReleaseHdcInternal(hdc);
				}
				if (flag)
				{
					this.ValueToolTipLocation = new Point(rect.X + 2, rect.Y - 1);
					return;
				}
				this.ValueToolTipLocation = GridEntry.InvalidPoint;
			}
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x00161438 File Offset: 0x0015F638
		public virtual bool OnComponentChanging()
		{
			if (this.ComponentChangeService != null)
			{
				try
				{
					this.ComponentChangeService.OnComponentChanging(this.GetValueOwner(), this.PropertyDescriptor);
				}
				catch (CheckoutException ex)
				{
					if (ex == CheckoutException.Canceled)
					{
						return false;
					}
					throw ex;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x00161488 File Offset: 0x0015F688
		public virtual void OnComponentChanged()
		{
			if (this.ComponentChangeService != null)
			{
				this.ComponentChangeService.OnComponentChanged(this.GetValueOwner(), this.PropertyDescriptor, null, null);
			}
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x001614AB File Offset: 0x0015F6AB
		protected virtual void OnLabelClick(EventArgs e)
		{
			this.RaiseEvent(GridEntry.EVENT_LABEL_CLICK, e);
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x001614B9 File Offset: 0x0015F6B9
		protected virtual void OnLabelDoubleClick(EventArgs e)
		{
			this.RaiseEvent(GridEntry.EVENT_LABEL_DBLCLICK, e);
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x001614C8 File Offset: 0x0015F6C8
		public virtual bool OnMouseClick(int x, int y, int count, MouseButtons button)
		{
			PropertyGridView gridEntryHost = this.GridEntryHost;
			if ((button & MouseButtons.Left) != MouseButtons.Left)
			{
				return false;
			}
			int num = gridEntryHost.GetLabelWidth();
			if (x >= 0 && x <= num)
			{
				if (this.Expandable && this.OutlineRect.Contains(x, y))
				{
					if (count % 2 == 0)
					{
						this.OnOutlineDoubleClick(EventArgs.Empty);
					}
					else
					{
						this.OnOutlineClick(EventArgs.Empty);
					}
					return true;
				}
				if (count % 2 == 0)
				{
					this.OnLabelDoubleClick(EventArgs.Empty);
				}
				else
				{
					this.OnLabelClick(EventArgs.Empty);
				}
				return true;
			}
			else
			{
				num += gridEntryHost.GetSplitterWidth();
				if (x >= num && x <= num + gridEntryHost.GetValueWidth())
				{
					if (count % 2 == 0)
					{
						this.OnValueDoubleClick(EventArgs.Empty);
					}
					else
					{
						this.OnValueClick(EventArgs.Empty);
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0016158C File Offset: 0x0015F78C
		protected virtual void OnOutlineClick(EventArgs e)
		{
			this.RaiseEvent(GridEntry.EVENT_OUTLINE_CLICK, e);
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x0016159A File Offset: 0x0015F79A
		protected virtual void OnOutlineDoubleClick(EventArgs e)
		{
			this.RaiseEvent(GridEntry.EVENT_OUTLINE_DBLCLICK, e);
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x001615A8 File Offset: 0x0015F7A8
		protected virtual void OnRecreateChildren(GridEntryRecreateChildrenEventArgs e)
		{
			Delegate eventHandler = this.GetEventHandler(GridEntry.EVENT_RECREATE_CHILDREN);
			if (eventHandler != null)
			{
				((GridEntryRecreateChildrenEventHandler)eventHandler)(this, e);
			}
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x001615D1 File Offset: 0x0015F7D1
		protected virtual void OnValueClick(EventArgs e)
		{
			this.RaiseEvent(GridEntry.EVENT_VALUE_CLICK, e);
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x001615DF File Offset: 0x0015F7DF
		protected virtual void OnValueDoubleClick(EventArgs e)
		{
			this.RaiseEvent(GridEntry.EVENT_VALUE_DBLCLICK, e);
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x001615ED File Offset: 0x0015F7ED
		internal bool OnValueReturnKey()
		{
			return this.NotifyValue(5);
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x001615F6 File Offset: 0x0015F7F6
		protected virtual void SetFlag(int flag, bool fVal)
		{
			this.SetFlag(flag, fVal ? flag : 0);
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x00161606 File Offset: 0x0015F806
		protected virtual void SetFlag(int flag_valid, int flag, bool fVal)
		{
			this.SetFlag(flag_valid | flag, flag_valid | (fVal ? flag : 0));
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x0016161A File Offset: 0x0015F81A
		protected virtual void SetFlag(int flag, int val)
		{
			this.Flags = ((this.Flags & ~flag) | val);
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x00161630 File Offset: 0x0015F830
		public override bool Select()
		{
			if (this.Disposed)
			{
				return false;
			}
			try
			{
				this.GridEntryHost.SelectedGridEntry = this;
				return true;
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x00161670 File Offset: 0x0015F870
		internal virtual bool ShouldSerializePropertyValue()
		{
			if (this.cacheItems != null)
			{
				if (this.cacheItems.useShouldSerialize)
				{
					return this.cacheItems.lastShouldSerialize;
				}
				this.cacheItems.lastShouldSerialize = this.NotifyValue(4);
				this.cacheItems.useShouldSerialize = true;
			}
			else
			{
				this.cacheItems = new GridEntry.CacheItems();
				this.cacheItems.lastShouldSerialize = this.NotifyValue(4);
				this.cacheItems.useShouldSerialize = true;
			}
			return this.cacheItems.lastShouldSerialize;
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x001616F4 File Offset: 0x0015F8F4
		private PropertyDescriptor[] SortParenProperties(PropertyDescriptor[] props)
		{
			PropertyDescriptor[] array = null;
			int num = 0;
			for (int i = 0; i < props.Length; i++)
			{
				if (((ParenthesizePropertyNameAttribute)props[i].Attributes[typeof(ParenthesizePropertyNameAttribute)]).NeedParenthesis)
				{
					if (array == null)
					{
						array = new PropertyDescriptor[props.Length];
					}
					array[num++] = props[i];
					props[i] = null;
				}
			}
			if (num > 0)
			{
				for (int j = 0; j < props.Length; j++)
				{
					if (props[j] != null)
					{
						array[num++] = props[j];
					}
				}
				props = array;
			}
			return props;
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool NotifyValueGivenParent(object obj, int type)
		{
			return false;
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x00161775 File Offset: 0x0015F975
		internal virtual bool NotifyChildValue(GridEntry pe, int type)
		{
			return pe.NotifyValueGivenParent(pe.GetValueOwner(), type);
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x00161784 File Offset: 0x0015F984
		internal virtual bool NotifyValue(int type)
		{
			return this.parentPE == null || this.parentPE.NotifyChildValue(this, type);
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x0016179D File Offset: 0x0015F99D
		internal void RecreateChildren()
		{
			this.RecreateChildren(-1);
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x001617A8 File Offset: 0x0015F9A8
		internal void RecreateChildren(int oldCount)
		{
			bool internalExpanded = this.InternalExpanded || oldCount > 0;
			if (oldCount == -1)
			{
				oldCount = this.VisibleChildCount;
			}
			this.ResetState();
			if (oldCount == 0)
			{
				return;
			}
			foreach (object obj in this.ChildCollection)
			{
				GridEntry gridEntry = (GridEntry)obj;
				gridEntry.RecreateChildren();
			}
			this.DisposeChildren();
			this.InternalExpanded = internalExpanded;
			this.OnRecreateChildren(new GridEntryRecreateChildrenEventArgs(oldCount, this.VisibleChildCount));
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x00161848 File Offset: 0x0015FA48
		public virtual void Refresh()
		{
			Type propertyType = this.PropertyType;
			if (propertyType != null && propertyType.IsArray)
			{
				this.CreateChildren(true);
			}
			if (this.childCollection != null)
			{
				if (this.InternalExpanded && this.cacheItems != null && this.cacheItems.lastValue != null && this.cacheItems.lastValue != this.PropertyValue)
				{
					this.ClearCachedValues();
					this.RecreateChildren();
					return;
				}
				if (this.InternalExpanded)
				{
					foreach (object obj in this.childCollection)
					{
						GridEntry gridEntry = (GridEntry)obj;
						gridEntry.Refresh();
					}
				}
				else
				{
					this.DisposeChildren();
				}
			}
			this.ClearCachedValues();
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x001618FA File Offset: 0x0015FAFA
		public virtual void RemoveOnLabelClick(EventHandler h)
		{
			this.RemoveEventHandler(GridEntry.EVENT_LABEL_CLICK, h);
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x00161908 File Offset: 0x0015FB08
		public virtual void RemoveOnLabelDoubleClick(EventHandler h)
		{
			this.RemoveEventHandler(GridEntry.EVENT_LABEL_DBLCLICK, h);
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x00161916 File Offset: 0x0015FB16
		public virtual void RemoveOnValueClick(EventHandler h)
		{
			this.RemoveEventHandler(GridEntry.EVENT_VALUE_CLICK, h);
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x00161924 File Offset: 0x0015FB24
		public virtual void RemoveOnValueDoubleClick(EventHandler h)
		{
			this.RemoveEventHandler(GridEntry.EVENT_VALUE_DBLCLICK, h);
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x00161932 File Offset: 0x0015FB32
		public virtual void RemoveOnOutlineClick(EventHandler h)
		{
			this.RemoveEventHandler(GridEntry.EVENT_OUTLINE_CLICK, h);
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x00161940 File Offset: 0x0015FB40
		public virtual void RemoveOnOutlineDoubleClick(EventHandler h)
		{
			this.RemoveEventHandler(GridEntry.EVENT_OUTLINE_DBLCLICK, h);
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x0016194E File Offset: 0x0015FB4E
		public virtual void RemoveOnRecreateChildren(GridEntryRecreateChildrenEventHandler h)
		{
			this.RemoveEventHandler(GridEntry.EVENT_RECREATE_CHILDREN, h);
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x0016195C File Offset: 0x0015FB5C
		protected void ResetState()
		{
			this.Flags = 0;
			this.ClearCachedValues();
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x0016196C File Offset: 0x0015FB6C
		public virtual bool SetPropertyTextValue(string str)
		{
			bool flag = this.childCollection != null && this.childCollection.Count > 0;
			this.PropertyValue = this.ConvertTextToValue(str);
			this.CreateChildren();
			bool flag2 = this.childCollection != null && this.childCollection.Count > 0;
			return flag != flag2;
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x001619C8 File Offset: 0x0015FBC8
		public override string ToString()
		{
			return base.GetType().FullName + " " + this.PropertyLabel;
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x001619E8 File Offset: 0x0015FBE8
		protected virtual void AddEventHandler(object key, Delegate handler)
		{
			lock (this)
			{
				if (handler != null)
				{
					for (GridEntry.EventEntry next = this.eventList; next != null; next = next.next)
					{
						if (next.key == key)
						{
							next.handler = Delegate.Combine(next.handler, handler);
							return;
						}
					}
					this.eventList = new GridEntry.EventEntry(this.eventList, key, handler);
				}
			}
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x00161A68 File Offset: 0x0015FC68
		protected virtual void RaiseEvent(object key, EventArgs e)
		{
			Delegate eventHandler = this.GetEventHandler(key);
			if (eventHandler != null)
			{
				((EventHandler)eventHandler)(this, e);
			}
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x00161A90 File Offset: 0x0015FC90
		protected virtual Delegate GetEventHandler(object key)
		{
			Delegate result;
			lock (this)
			{
				for (GridEntry.EventEntry next = this.eventList; next != null; next = next.next)
				{
					if (next.key == key)
					{
						return next.handler;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x00161AF0 File Offset: 0x0015FCF0
		protected virtual void RemoveEventHandler(object key, Delegate handler)
		{
			lock (this)
			{
				if (handler != null)
				{
					GridEntry.EventEntry next = this.eventList;
					GridEntry.EventEntry eventEntry = null;
					while (next != null)
					{
						if (next.key == key)
						{
							next.handler = Delegate.Remove(next.handler, handler);
							if (next.handler == null)
							{
								if (eventEntry == null)
								{
									this.eventList = next.next;
								}
								else
								{
									eventEntry.next = next.next;
								}
							}
							break;
						}
						eventEntry = next;
						next = next.next;
					}
				}
			}
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x00161B84 File Offset: 0x0015FD84
		protected virtual void RemoveEventHandlers()
		{
			this.eventList = null;
		}

		// Token: 0x040036D4 RID: 14036
		protected static readonly Point InvalidPoint = new Point(int.MinValue, int.MinValue);

		// Token: 0x040036D5 RID: 14037
		private static BooleanSwitch PbrsAssertPropsSwitch = new BooleanSwitch("PbrsAssertProps", "PropertyBrowser : Assert on broken properties");

		// Token: 0x040036D6 RID: 14038
		internal static AttributeTypeSorter AttributeTypeSorter = new AttributeTypeSorter();

		// Token: 0x040036D7 RID: 14039
		internal const int FLAG_TEXT_EDITABLE = 1;

		// Token: 0x040036D8 RID: 14040
		internal const int FLAG_ENUMERABLE = 2;

		// Token: 0x040036D9 RID: 14041
		internal const int FLAG_CUSTOM_PAINT = 4;

		// Token: 0x040036DA RID: 14042
		internal const int FLAG_IMMEDIATELY_EDITABLE = 8;

		// Token: 0x040036DB RID: 14043
		internal const int FLAG_CUSTOM_EDITABLE = 16;

		// Token: 0x040036DC RID: 14044
		internal const int FLAG_DROPDOWN_EDITABLE = 32;

		// Token: 0x040036DD RID: 14045
		internal const int FLAG_LABEL_BOLD = 64;

		// Token: 0x040036DE RID: 14046
		internal const int FLAG_READONLY_EDITABLE = 128;

		// Token: 0x040036DF RID: 14047
		internal const int FLAG_RENDER_READONLY = 256;

		// Token: 0x040036E0 RID: 14048
		internal const int FLAG_IMMUTABLE = 512;

		// Token: 0x040036E1 RID: 14049
		internal const int FLAG_FORCE_READONLY = 1024;

		// Token: 0x040036E2 RID: 14050
		internal const int FLAG_RENDER_PASSWORD = 4096;

		// Token: 0x040036E3 RID: 14051
		internal const int FLAG_DISPOSED = 8192;

		// Token: 0x040036E4 RID: 14052
		internal const int FL_EXPAND = 65536;

		// Token: 0x040036E5 RID: 14053
		internal const int FL_EXPANDABLE = 131072;

		// Token: 0x040036E6 RID: 14054
		internal const int FL_EXPANDABLE_FAILED = 524288;

		// Token: 0x040036E7 RID: 14055
		internal const int FL_NO_CUSTOM_PAINT = 1048576;

		// Token: 0x040036E8 RID: 14056
		internal const int FL_CATEGORIES = 2097152;

		// Token: 0x040036E9 RID: 14057
		internal const int FL_CHECKED = -2147483648;

		// Token: 0x040036EA RID: 14058
		protected const int NOTIFY_RESET = 1;

		// Token: 0x040036EB RID: 14059
		protected const int NOTIFY_CAN_RESET = 2;

		// Token: 0x040036EC RID: 14060
		protected const int NOTIFY_DBL_CLICK = 3;

		// Token: 0x040036ED RID: 14061
		protected const int NOTIFY_SHOULD_PERSIST = 4;

		// Token: 0x040036EE RID: 14062
		protected const int NOTIFY_RETURN = 5;

		// Token: 0x040036EF RID: 14063
		protected const int OUTLINE_ICON_PADDING = 5;

		// Token: 0x040036F0 RID: 14064
		protected static IComparer DisplayNameComparer = new GridEntry.DisplayNameSortComparer();

		// Token: 0x040036F1 RID: 14065
		private static char passwordReplaceChar;

		// Token: 0x040036F2 RID: 14066
		private const int maximumLengthOfPropertyString = 1000;

		// Token: 0x040036F3 RID: 14067
		private GridEntry.CacheItems cacheItems;

		// Token: 0x040036F4 RID: 14068
		protected TypeConverter converter;

		// Token: 0x040036F5 RID: 14069
		protected UITypeEditor editor;

		// Token: 0x040036F6 RID: 14070
		internal GridEntry parentPE;

		// Token: 0x040036F7 RID: 14071
		private GridEntryCollection childCollection;

		// Token: 0x040036F8 RID: 14072
		internal int flags;

		// Token: 0x040036F9 RID: 14073
		private int propertyDepth;

		// Token: 0x040036FA RID: 14074
		protected bool hasFocus;

		// Token: 0x040036FB RID: 14075
		private Rectangle outlineRect = Rectangle.Empty;

		// Token: 0x040036FC RID: 14076
		protected PropertySort PropertySort;

		// Token: 0x040036FD RID: 14077
		protected Point labelTipPoint = GridEntry.InvalidPoint;

		// Token: 0x040036FE RID: 14078
		protected Point valueTipPoint = GridEntry.InvalidPoint;

		// Token: 0x040036FF RID: 14079
		protected PropertyGrid ownerGrid;

		// Token: 0x04003700 RID: 14080
		private static object EVENT_VALUE_CLICK = new object();

		// Token: 0x04003701 RID: 14081
		private static object EVENT_LABEL_CLICK = new object();

		// Token: 0x04003702 RID: 14082
		private static object EVENT_OUTLINE_CLICK = new object();

		// Token: 0x04003703 RID: 14083
		private static object EVENT_VALUE_DBLCLICK = new object();

		// Token: 0x04003704 RID: 14084
		private static object EVENT_LABEL_DBLCLICK = new object();

		// Token: 0x04003705 RID: 14085
		private static object EVENT_OUTLINE_DBLCLICK = new object();

		// Token: 0x04003706 RID: 14086
		private static object EVENT_RECREATE_CHILDREN = new object();

		// Token: 0x04003707 RID: 14087
		private GridEntry.GridEntryAccessibleObject accessibleObject;

		// Token: 0x04003708 RID: 14088
		private bool lastPaintWithExplorerStyle;

		// Token: 0x04003709 RID: 14089
		private GridEntry.EventEntry eventList;

		// Token: 0x0200088B RID: 2187
		[Flags]
		internal enum PaintValueFlags
		{
			// Token: 0x040044AF RID: 17583
			None = 0,
			// Token: 0x040044B0 RID: 17584
			DrawSelected = 1,
			// Token: 0x040044B1 RID: 17585
			FetchValue = 2,
			// Token: 0x040044B2 RID: 17586
			CheckShouldSerialize = 4,
			// Token: 0x040044B3 RID: 17587
			PaintInPlace = 8
		}

		// Token: 0x0200088C RID: 2188
		private class CacheItems
		{
			// Token: 0x040044B4 RID: 17588
			public string lastLabel;

			// Token: 0x040044B5 RID: 17589
			public Font lastLabelFont;

			// Token: 0x040044B6 RID: 17590
			public int lastLabelWidth;

			// Token: 0x040044B7 RID: 17591
			public string lastValueString;

			// Token: 0x040044B8 RID: 17592
			public Font lastValueFont;

			// Token: 0x040044B9 RID: 17593
			public int lastValueTextWidth;

			// Token: 0x040044BA RID: 17594
			public object lastValue;

			// Token: 0x040044BB RID: 17595
			public bool useValueString;

			// Token: 0x040044BC RID: 17596
			public bool lastShouldSerialize;

			// Token: 0x040044BD RID: 17597
			public bool useShouldSerialize;

			// Token: 0x040044BE RID: 17598
			public bool useCompatTextRendering;
		}

		// Token: 0x0200088D RID: 2189
		private sealed class EventEntry
		{
			// Token: 0x06007219 RID: 29209 RVA: 0x001A2718 File Offset: 0x001A0918
			internal EventEntry(GridEntry.EventEntry next, object key, Delegate handler)
			{
				this.next = next;
				this.key = key;
				this.handler = handler;
			}

			// Token: 0x040044BF RID: 17599
			internal GridEntry.EventEntry next;

			// Token: 0x040044C0 RID: 17600
			internal object key;

			// Token: 0x040044C1 RID: 17601
			internal Delegate handler;
		}

		// Token: 0x0200088E RID: 2190
		[ComVisible(true)]
		public class GridEntryAccessibleObject : AccessibleObject
		{
			// Token: 0x0600721A RID: 29210 RVA: 0x001A2735 File Offset: 0x001A0935
			public GridEntryAccessibleObject(GridEntry owner)
			{
				this.owner = owner;
			}

			// Token: 0x0600721B RID: 29211 RVA: 0x001A2744 File Offset: 0x001A0944
			internal void ClearOwnerGridEntry()
			{
				this.owner = null;
			}

			// Token: 0x0600721C RID: 29212 RVA: 0x001A274D File Offset: 0x001A094D
			internal bool IsOwnerGridEntryCleared()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.owner == null;
			}

			// Token: 0x170018FF RID: 6399
			// (get) Token: 0x0600721D RID: 29213 RVA: 0x001A2764 File Offset: 0x001A0964
			public override Rectangle Bounds
			{
				get
				{
					PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = (PropertyGridView.PropertyGridViewAccessibleObject)this.Parent;
					if (propertyGridViewAccessibleObject != null && !propertyGridViewAccessibleObject.IsOwnerControlDestroyed())
					{
						PropertyGridView propertyGridView = propertyGridViewAccessibleObject.Owner as PropertyGridView;
						if (propertyGridView != null)
						{
							return propertyGridView.AccessibilityGetGridEntryBounds(this.owner);
						}
					}
					return Rectangle.Empty;
				}
			}

			// Token: 0x17001900 RID: 6400
			// (get) Token: 0x0600721E RID: 29214 RVA: 0x001A27AC File Offset: 0x001A09AC
			public override string DefaultAction
			{
				get
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return string.Empty;
					}
					if (!this.owner.Expandable)
					{
						return base.DefaultAction;
					}
					if (this.owner.Expanded)
					{
						return SR.GetString("AccessibleActionCollapse");
					}
					return SR.GetString("AccessibleActionExpand");
				}
			}

			// Token: 0x17001901 RID: 6401
			// (get) Token: 0x0600721F RID: 29215 RVA: 0x001A27FD File Offset: 0x001A09FD
			public override string Description
			{
				get
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return string.Empty;
					}
					return this.owner.PropertyDescription;
				}
			}

			// Token: 0x17001902 RID: 6402
			// (get) Token: 0x06007220 RID: 29216 RVA: 0x001A2818 File Offset: 0x001A0A18
			public override string Help
			{
				get
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return string.Empty;
					}
					if (AccessibilityImprovements.Level1)
					{
						return this.owner.PropertyDescription;
					}
					return base.Help;
				}
			}

			// Token: 0x06007221 RID: 29217 RVA: 0x001A2844 File Offset: 0x001A0A44
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.IsOwnerGridEntryCleared())
				{
					return null;
				}
				if (AccessibilityImprovements.Level3)
				{
					switch (direction)
					{
					case UnsafeNativeMethods.NavigateDirection.Parent:
					{
						GridEntry parentGridEntry = this.owner.ParentGridEntry;
						if (parentGridEntry == null)
						{
							return this.Parent;
						}
						if (parentGridEntry is SingleSelectRootGridEntry)
						{
							return this.owner.OwnerGrid.GridViewAccessibleObject;
						}
						return parentGridEntry.AccessibilityObject;
					}
					case UnsafeNativeMethods.NavigateDirection.NextSibling:
						return this.Navigate(AccessibleNavigation.Next);
					case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
						return this.Navigate(AccessibleNavigation.Previous);
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x17001903 RID: 6403
			// (get) Token: 0x06007222 RID: 29218 RVA: 0x001A28C3 File Offset: 0x001A0AC3
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return null;
					}
					if (AccessibilityImprovements.Level3)
					{
						return (PropertyGridView.PropertyGridViewAccessibleObject)this.Parent;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x06007223 RID: 29219 RVA: 0x001A28E8 File Offset: 0x001A0AE8
			internal override bool IsIAccessibleExSupported()
			{
				return !this.IsOwnerGridEntryCleared() && (this.owner.Expandable && AccessibilityImprovements.Level1);
			}

			// Token: 0x17001904 RID: 6404
			// (get) Token: 0x06007224 RID: 29220 RVA: 0x001A290C File Offset: 0x001A0B0C
			internal override int[] RuntimeId
			{
				get
				{
					if (this.runtimeId == null)
					{
						this.runtimeId = new int[3];
						this.runtimeId[0] = 42;
						this.runtimeId[1] = (this.IsOwnerGridEntryCleared() ? 0 : ((int)((long)this.owner.GridEntryHost.Handle)));
						this.runtimeId[2] = this.GetHashCode();
					}
					return this.runtimeId;
				}
			}

			// Token: 0x06007225 RID: 29221 RVA: 0x001A2974 File Offset: 0x001A0B74
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID != 30003)
				{
					if (propertyID == 30005)
					{
						return this.Name;
					}
					if (propertyID == 30028)
					{
						return this.IsPatternSupported(10005);
					}
					if (AccessibilityImprovements.Level4 && (propertyID == 30029 || propertyID == 30039))
					{
						return true;
					}
					if (AccessibilityImprovements.Level3)
					{
						if (propertyID <= 30022)
						{
							switch (propertyID)
							{
							case 30007:
								return string.Empty;
							case 30008:
								return !this.IsOwnerGridEntryCleared() && this.owner.hasFocus;
							case 30009:
								return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
							case 30010:
								return true;
							case 30011:
								return this.GetHashCode().ToString();
							case 30012:
							case 30014:
							case 30015:
							case 30016:
							case 30017:
							case 30018:
								break;
							case 30013:
								return this.Help ?? string.Empty;
							case 30019:
								return false;
							default:
								if (propertyID == 30022)
								{
									return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
								}
								break;
							}
						}
						else
						{
							if (propertyID == 30095)
							{
								return this.Role;
							}
							if (propertyID == 30100)
							{
								return this.DefaultAction;
							}
						}
						return base.GetPropertyValue(propertyID);
					}
					return null;
				}
				else
				{
					if (AccessibilityImprovements.Level4)
					{
						return 50024;
					}
					if (AccessibilityImprovements.Level3)
					{
						return 50029;
					}
					return 50000;
				}
			}

			// Token: 0x06007226 RID: 29222 RVA: 0x001A2B1C File Offset: 0x001A0D1C
			internal override bool IsPatternSupported(int patternId)
			{
				if (this.IsOwnerGridEntryCleared())
				{
					return false;
				}
				if (this.owner.Expandable && patternId == 10005)
				{
					return true;
				}
				if (AccessibilityImprovements.Level3 && (patternId == 10000 || patternId == 10018))
				{
					return true;
				}
				if (AccessibilityImprovements.Level4)
				{
					if ((patternId == 10007 || patternId == 10013) && this.owner != null && this.owner.OwnerGrid != null && !this.owner.OwnerGrid.SortedByCategories)
					{
						GridEntry parentGridEntry = this.owner.ParentGridEntry;
						if (parentGridEntry is SingleSelectRootGridEntry)
						{
							return true;
						}
					}
					return base.IsPatternSupported(patternId);
				}
				return false;
			}

			// Token: 0x06007227 RID: 29223 RVA: 0x001A2BC0 File Offset: 0x001A0DC0
			internal override void Expand()
			{
				if (!this.IsOwnerGridEntryCleared() && this.owner.Expandable && !this.owner.Expanded)
				{
					this.owner.Expanded = true;
				}
			}

			// Token: 0x06007228 RID: 29224 RVA: 0x001A2BF0 File Offset: 0x001A0DF0
			internal override void Collapse()
			{
				if (!this.IsOwnerGridEntryCleared() && this.owner.Expandable && this.owner.Expanded)
				{
					this.owner.Expanded = false;
				}
			}

			// Token: 0x17001905 RID: 6405
			// (get) Token: 0x06007229 RID: 29225 RVA: 0x001A2C20 File Offset: 0x001A0E20
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					if (!this.owner.Expandable)
					{
						return UnsafeNativeMethods.ExpandCollapseState.LeafNode;
					}
					if (!this.owner.Expanded)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					return UnsafeNativeMethods.ExpandCollapseState.Expanded;
				}
			}

			// Token: 0x0600722A RID: 29226 RVA: 0x001A2C4B File Offset: 0x001A0E4B
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (this.IsOwnerGridEntryCleared())
				{
					return;
				}
				this.owner.OnOutlineClick(EventArgs.Empty);
			}

			// Token: 0x17001906 RID: 6406
			// (get) Token: 0x0600722B RID: 29227 RVA: 0x001A2C66 File Offset: 0x001A0E66
			public override string Name
			{
				get
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return string.Empty;
					}
					return this.owner.PropertyLabel;
				}
			}

			// Token: 0x17001907 RID: 6407
			// (get) Token: 0x0600722C RID: 29228 RVA: 0x001A2C81 File Offset: 0x001A0E81
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return null;
					}
					return this.owner.GridEntryHost.AccessibilityObject;
				}
			}

			// Token: 0x17001908 RID: 6408
			// (get) Token: 0x0600722D RID: 29229 RVA: 0x001A2C9D File Offset: 0x001A0E9D
			public override AccessibleRole Role
			{
				get
				{
					if (this.IsOwnerGridEntryCleared() || AccessibilityImprovements.Level3)
					{
						return AccessibleRole.Cell;
					}
					if (!AccessibilityImprovements.Level1)
					{
						return AccessibleRole.Row;
					}
					if (this.owner.Expandable)
					{
						return AccessibleRole.ButtonDropDownGrid;
					}
					return AccessibleRole.Cell;
				}
			}

			// Token: 0x17001909 RID: 6409
			// (get) Token: 0x0600722E RID: 29230 RVA: 0x001A2CD0 File Offset: 0x001A0ED0
			public override AccessibleStates State
			{
				get
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return AccessibleStates.None;
					}
					PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = this.Parent as PropertyGridView.PropertyGridViewAccessibleObject;
					if (propertyGridViewAccessibleObject != null && !propertyGridViewAccessibleObject.IsOwnerControlDestroyed())
					{
						PropertyGridView propertyGridView = propertyGridViewAccessibleObject.Owner as PropertyGridView;
						if (propertyGridView != null)
						{
							AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
							if (this.owner.Focus)
							{
								accessibleStates |= AccessibleStates.Focused;
							}
							if (propertyGridViewAccessibleObject.GetSelected() == this)
							{
								accessibleStates |= AccessibleStates.Selected;
							}
							if (this.owner.Expandable)
							{
								if (this.owner.Expanded)
								{
									accessibleStates |= AccessibleStates.Expanded;
								}
								else
								{
									accessibleStates |= AccessibleStates.Collapsed;
								}
							}
							if (this.owner.ShouldRenderReadOnly)
							{
								accessibleStates |= AccessibleStates.ReadOnly;
							}
							if (this.owner.ShouldRenderPassword)
							{
								accessibleStates |= AccessibleStates.Protected;
							}
							if (AccessibilityImprovements.Level4)
							{
								Rectangle boundingRectangle = this.BoundingRectangle;
								Rectangle toolNativeScreenRectangle = propertyGridView.GetToolNativeScreenRectangle();
								if (!boundingRectangle.IntersectsWith(toolNativeScreenRectangle))
								{
									accessibleStates |= AccessibleStates.Offscreen;
								}
							}
							return accessibleStates;
						}
					}
					return AccessibleStates.None;
				}
			}

			// Token: 0x1700190A RID: 6410
			// (get) Token: 0x0600722F RID: 29231 RVA: 0x001A2DB0 File Offset: 0x001A0FB0
			// (set) Token: 0x06007230 RID: 29232 RVA: 0x001A2DCB File Offset: 0x001A0FCB
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return string.Empty;
					}
					return this.owner.GetPropertyTextValue();
				}
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				set
				{
					if (this.IsOwnerGridEntryCleared())
					{
						return;
					}
					this.owner.SetPropertyTextValue(value);
				}
			}

			// Token: 0x06007231 RID: 29233 RVA: 0x001A2DE3 File Offset: 0x001A0FE3
			public override AccessibleObject GetFocused()
			{
				if (!this.IsOwnerGridEntryCleared() && this.owner.Focus)
				{
					return this;
				}
				return null;
			}

			// Token: 0x06007232 RID: 29234 RVA: 0x001A2E00 File Offset: 0x001A1000
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				if (this.IsOwnerGridEntryCleared())
				{
					return null;
				}
				PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = (PropertyGridView.PropertyGridViewAccessibleObject)this.Parent;
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					return propertyGridViewAccessibleObject.Previous(this.owner);
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					return propertyGridViewAccessibleObject.Next(this.owner);
				}
				return null;
			}

			// Token: 0x06007233 RID: 29235 RVA: 0x001A2E68 File Offset: 0x001A1068
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = this.Parent as PropertyGridView.PropertyGridViewAccessibleObject;
				if (propertyGridViewAccessibleObject != null && !propertyGridViewAccessibleObject.IsOwnerControlDestroyed())
				{
					PropertyGridView propertyGridView = propertyGridViewAccessibleObject.Owner as PropertyGridView;
					if (propertyGridView != null)
					{
						if (propertyGridView.InvokeRequired)
						{
							propertyGridView.Invoke(new GridEntry.GridEntryAccessibleObject.SelectDelegate(this.Select), new object[]
							{
								flags
							});
							return;
						}
						if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
						{
							bool flag = propertyGridView.FocusInternal();
						}
						if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
						{
							propertyGridView.AccessibilitySelect(this.owner);
						}
						return;
					}
				}
			}

			// Token: 0x06007234 RID: 29236 RVA: 0x001A2EE7 File Offset: 0x001A10E7
			internal override void SetFocus()
			{
				base.SetFocus();
				if (AccessibilityImprovements.Level3)
				{
					base.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x1700190B RID: 6411
			// (get) Token: 0x06007235 RID: 29237 RVA: 0x001A2F04 File Offset: 0x001A1104
			internal override int Row
			{
				get
				{
					if (this.IsOwnerGridEntryCleared())
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
					if (propertyGridView == null)
					{
						return -1;
					}
					GridEntryCollection topLevelGridEntries = propertyGridView.TopLevelGridEntries;
					if (topLevelGridEntries == null)
					{
						return -1;
					}
					for (int i = 0; i < topLevelGridEntries.Count; i++)
					{
						GridItem gridItem = topLevelGridEntries[i];
						if (this.owner == gridItem)
						{
							return i;
						}
					}
					return -1;
				}
			}

			// Token: 0x1700190C RID: 6412
			// (get) Token: 0x06007236 RID: 29238 RVA: 0x001A2F7E File Offset: 0x001A117E
			internal override int Column
			{
				get
				{
					if (AccessibilityImprovements.Level4)
					{
						return 0;
					}
					return base.Column;
				}
			}

			// Token: 0x1700190D RID: 6413
			// (get) Token: 0x06007237 RID: 29239 RVA: 0x001A2F90 File Offset: 0x001A1190
			internal override UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
			{
				get
				{
					if (AccessibilityImprovements.Level4)
					{
						PropertyGridView.PropertyGridViewAccessibleObject propertyGridViewAccessibleObject = this.Parent as PropertyGridView.PropertyGridViewAccessibleObject;
						if (propertyGridViewAccessibleObject != null && !propertyGridViewAccessibleObject.IsOwnerControlDestroyed())
						{
							PropertyGridView propertyGridView = propertyGridViewAccessibleObject.Owner as PropertyGridView;
							if (propertyGridView != null)
							{
								return propertyGridView.AccessibilityObject;
							}
						}
						return null;
					}
					return base.ContainingGrid;
				}
			}

			// Token: 0x040044C2 RID: 17602
			protected GridEntry owner;

			// Token: 0x040044C3 RID: 17603
			private int[] runtimeId;

			// Token: 0x02000981 RID: 2433
			// (Invoke) Token: 0x060075A2 RID: 30114
			private delegate void SelectDelegate(AccessibleSelection flags);
		}

		// Token: 0x0200088F RID: 2191
		public class DisplayNameSortComparer : IComparer
		{
			// Token: 0x06007238 RID: 29240 RVA: 0x001A2FD9 File Offset: 0x001A11D9
			public int Compare(object left, object right)
			{
				return string.Compare(((PropertyDescriptor)left).DisplayName, ((PropertyDescriptor)right).DisplayName, true, CultureInfo.CurrentCulture);
			}
		}
	}
}
