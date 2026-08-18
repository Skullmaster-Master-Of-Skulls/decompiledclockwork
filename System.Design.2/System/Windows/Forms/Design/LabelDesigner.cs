using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000303 RID: 771
	internal class LabelDesigner : ControlDesigner
	{
		// Token: 0x06001E99 RID: 7833 RVA: 0x00093E53 File Offset: 0x00092053
		public LabelDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x000B73E8 File Offset: 0x000B55E8
		public override IList SnapLines
		{
			get
			{
				ArrayList arrayList = base.SnapLines as ArrayList;
				ContentAlignment alignment = ContentAlignment.TopLeft;
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.Component);
				PropertyDescriptor propertyDescriptor;
				if ((propertyDescriptor = properties["TextAlign"]) != null)
				{
					alignment = (ContentAlignment)propertyDescriptor.GetValue(base.Component);
				}
				int num = DesignerUtils.GetTextBaseline(this.Control, alignment);
				if ((propertyDescriptor = properties["AutoSize"]) != null && !(bool)propertyDescriptor.GetValue(base.Component))
				{
					BorderStyle borderStyle = BorderStyle.None;
					if ((propertyDescriptor = properties["BorderStyle"]) != null)
					{
						borderStyle = (BorderStyle)propertyDescriptor.GetValue(base.Component);
					}
					num += this.LabelBaselineOffset(alignment, borderStyle);
				}
				arrayList.Add(new SnapLine(SnapLineType.Baseline, num, SnapLinePriority.Medium));
				Label label = this.Control as Label;
				if (label != null && label.BorderStyle == BorderStyle.None)
				{
					Type type = Type.GetType("System.Windows.Forms.Label");
					if (type != null)
					{
						MethodInfo method = type.GetMethod("GetLeadingTextPaddingFromTextFormatFlags", BindingFlags.Instance | BindingFlags.NonPublic);
						if (method != null)
						{
							int num2 = (int)method.Invoke(base.Component, null);
							bool flag = label.RightToLeft == RightToLeft.Yes;
							for (int i = 0; i < arrayList.Count; i++)
							{
								SnapLine snapLine = arrayList[i] as SnapLine;
								if (snapLine != null && snapLine.SnapLineType == (flag ? SnapLineType.Right : SnapLineType.Left))
								{
									snapLine.AdjustOffset(flag ? (-num2) : num2);
									break;
								}
							}
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000B7565 File Offset: 0x000B5765
		private int LabelBaselineOffset(ContentAlignment alignment, BorderStyle borderStyle)
		{
			if ((alignment & DesignerUtils.anyMiddleAlignment) != (ContentAlignment)0 || (alignment & DesignerUtils.anyTopAlignment) != (ContentAlignment)0)
			{
				if (borderStyle == BorderStyle.None)
				{
					return 0;
				}
				if (borderStyle == BorderStyle.FixedSingle || borderStyle == BorderStyle.Fixed3D)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (borderStyle == BorderStyle.None)
				{
					return -1;
				}
				if (borderStyle != BorderStyle.FixedSingle)
				{
				}
				return 0;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x000B7598 File Offset: 0x000B5798
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				object component = base.Component;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["AutoSize"];
				if (propertyDescriptor != null)
				{
					bool flag = (bool)propertyDescriptor.GetValue(component);
					if (flag)
					{
						selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
					}
				}
				return selectionRules;
			}
		}
	}
}
