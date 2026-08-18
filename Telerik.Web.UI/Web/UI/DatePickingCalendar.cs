using System;
using System.ComponentModel;
using Telerik.Web.UI.Calendar.Utils;

namespace Telerik.Web.UI
{
	// Token: 0x02000FEA RID: 4074
	[ToolboxItem(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class DatePickingCalendar : RadCalendar, ICustomTypeDescriptor
	{
		// Token: 0x06009EA7 RID: 40615 RVA: 0x002356C0 File Offset: 0x002338C0
		public override string ToString()
		{
			return "RadCalendar";
		}

		// Token: 0x17003212 RID: 12818
		// (get) Token: 0x06009EA8 RID: 40616 RVA: 0x002356C7 File Offset: 0x002338C7
		// (set) Token: 0x06009EA9 RID: 40617 RVA: 0x002356CA File Offset: 0x002338CA
		[Browsable(false)]
		[NotifyParentProperty(true)]
		public override bool UseRowHeadersAsSelectors
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17003213 RID: 12819
		// (get) Token: 0x06009EAA RID: 40618 RVA: 0x002356CC File Offset: 0x002338CC
		// (set) Token: 0x06009EAB RID: 40619 RVA: 0x002356CF File Offset: 0x002338CF
		[Browsable(false)]
		[NotifyParentProperty(true)]
		public override bool UseColumnHeadersAsSelectors
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17003214 RID: 12820
		// (get) Token: 0x06009EAC RID: 40620 RVA: 0x002356D1 File Offset: 0x002338D1
		// (set) Token: 0x06009EAD RID: 40621 RVA: 0x002356D9 File Offset: 0x002338D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x17003215 RID: 12821
		// (get) Token: 0x06009EAE RID: 40622 RVA: 0x002356E2 File Offset: 0x002338E2
		// (set) Token: 0x06009EAF RID: 40623 RVA: 0x002356EA File Offset: 0x002338EA
		[Browsable(false)]
		public override string ImagesPath
		{
			get
			{
				return base.ImagesPath;
			}
			set
			{
				base.ImagesPath = value;
			}
		}

		// Token: 0x06009EB0 RID: 40624 RVA: 0x002356F3 File Offset: 0x002338F3
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06009EB1 RID: 40625 RVA: 0x002356FC File Offset: 0x002338FC
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06009EB2 RID: 40626 RVA: 0x00235705 File Offset: 0x00233905
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06009EB3 RID: 40627 RVA: 0x0023570E File Offset: 0x0023390E
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06009EB4 RID: 40628 RVA: 0x00235717 File Offset: 0x00233917
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06009EB5 RID: 40629 RVA: 0x00235720 File Offset: 0x00233920
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06009EB6 RID: 40630 RVA: 0x00235729 File Offset: 0x00233929
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06009EB7 RID: 40631 RVA: 0x00235733 File Offset: 0x00233933
		public EventDescriptorCollection GetEvents()
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x06009EB8 RID: 40632 RVA: 0x00235740 File Offset: 0x00233940
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x06009EB9 RID: 40633 RVA: 0x00235750 File Offset: 0x00233950
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			return PropertyFilter.Filter(properties);
		}

		// Token: 0x06009EBA RID: 40634 RVA: 0x0023576C File Offset: 0x0023396C
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			return PropertyFilter.Filter(properties);
		}

		// Token: 0x06009EBB RID: 40635 RVA: 0x00235788 File Offset: 0x00233988
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x06009EBC RID: 40636 RVA: 0x0023578C File Offset: 0x0023398C
		protected override void NavigateWithStep(int navigationStep)
		{
			base.NavigateWithStep(navigationStep);
			if (this.Parent != null)
			{
				RadDatePicker radDatePicker = this.Parent as RadDatePicker;
				if (radDatePicker != null)
				{
					radDatePicker.ShowPopupOnInit = true;
				}
			}
		}
	}
}
