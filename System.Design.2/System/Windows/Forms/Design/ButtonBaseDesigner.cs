using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200029F RID: 671
	internal class ButtonBaseDesigner : ControlDesigner
	{
		// Token: 0x060019E2 RID: 6626 RVA: 0x00093E53 File Offset: 0x00092053
		public ButtonBaseDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00093E64 File Offset: 0x00092064
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["UseVisualStyleBackColor"];
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool) && !propertyDescriptor.IsReadOnly && propertyDescriptor.IsBrowsable && !propertyDescriptor.ShouldSerializeValue(base.Component))
			{
				propertyDescriptor.SetValue(base.Component, true);
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x00093ED8 File Offset: 0x000920D8
		public override IList SnapLines
		{
			get
			{
				ArrayList arrayList = base.SnapLines as ArrayList;
				FlatStyle flatStyle = FlatStyle.Standard;
				ContentAlignment alignment = ContentAlignment.MiddleCenter;
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.Component);
				PropertyDescriptor propertyDescriptor;
				if ((propertyDescriptor = properties["TextAlign"]) != null)
				{
					alignment = (ContentAlignment)propertyDescriptor.GetValue(base.Component);
				}
				if ((propertyDescriptor = properties["FlatStyle"]) != null)
				{
					flatStyle = (FlatStyle)propertyDescriptor.GetValue(base.Component);
				}
				int num = DesignerUtils.GetTextBaseline(this.Control, alignment);
				if (this.Control is CheckBox || this.Control is RadioButton)
				{
					Appearance appearance = Appearance.Normal;
					if ((propertyDescriptor = properties["Appearance"]) != null)
					{
						appearance = (Appearance)propertyDescriptor.GetValue(base.Component);
					}
					if (appearance == Appearance.Normal)
					{
						if (this.Control is CheckBox)
						{
							num += this.CheckboxBaselineOffset(alignment, flatStyle);
						}
						else
						{
							num += this.RadiobuttonBaselineOffset(alignment, flatStyle);
						}
					}
					else
					{
						num += this.DefaultBaselineOffset(alignment, flatStyle);
					}
				}
				else
				{
					num += this.DefaultBaselineOffset(alignment, flatStyle);
				}
				arrayList.Add(new SnapLine(SnapLineType.Baseline, num, SnapLinePriority.Medium));
				return arrayList;
			}
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00093FF4 File Offset: 0x000921F4
		private int CheckboxBaselineOffset(ContentAlignment alignment, FlatStyle flatStyle)
		{
			if ((alignment & DesignerUtils.anyMiddleAlignment) != (ContentAlignment)0)
			{
				if (flatStyle == FlatStyle.Standard || flatStyle == FlatStyle.System)
				{
					return -1;
				}
				return 0;
			}
			else if ((alignment & DesignerUtils.anyTopAlignment) != (ContentAlignment)0)
			{
				if (flatStyle == FlatStyle.Standard)
				{
					return 1;
				}
				if (flatStyle == FlatStyle.System)
				{
					return 0;
				}
				if (flatStyle == FlatStyle.Flat || flatStyle == FlatStyle.Popup)
				{
					return 2;
				}
				return 0;
			}
			else
			{
				if (flatStyle == FlatStyle.Standard)
				{
					return -3;
				}
				if (flatStyle == FlatStyle.System)
				{
					return 0;
				}
				if (flatStyle == FlatStyle.Flat || flatStyle == FlatStyle.Popup)
				{
					return -2;
				}
				return 0;
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0009404E File Offset: 0x0009224E
		private int RadiobuttonBaselineOffset(ContentAlignment alignment, FlatStyle flatStyle)
		{
			if ((alignment & DesignerUtils.anyMiddleAlignment) != (ContentAlignment)0)
			{
				if (flatStyle == FlatStyle.System)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (flatStyle != FlatStyle.Standard && flatStyle != FlatStyle.Flat && flatStyle != FlatStyle.Popup)
				{
					return 0;
				}
				if ((alignment & DesignerUtils.anyTopAlignment) == (ContentAlignment)0)
				{
					return -2;
				}
				return 2;
			}
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00094080 File Offset: 0x00092280
		private int DefaultBaselineOffset(ContentAlignment alignment, FlatStyle flatStyle)
		{
			if ((alignment & DesignerUtils.anyMiddleAlignment) != (ContentAlignment)0)
			{
				return 0;
			}
			if (flatStyle == FlatStyle.Standard || flatStyle == FlatStyle.Popup)
			{
				if ((alignment & DesignerUtils.anyTopAlignment) == (ContentAlignment)0)
				{
					return -4;
				}
				return 4;
			}
			else if (flatStyle == FlatStyle.System)
			{
				if ((alignment & DesignerUtils.anyTopAlignment) == (ContentAlignment)0)
				{
					return -3;
				}
				return 3;
			}
			else
			{
				if (flatStyle != FlatStyle.Flat)
				{
					return 0;
				}
				if ((alignment & DesignerUtils.anyTopAlignment) == (ContentAlignment)0)
				{
					return -5;
				}
				return 5;
			}
		}
	}
}
