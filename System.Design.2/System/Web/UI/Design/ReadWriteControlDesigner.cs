using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000062 RID: 98
	[Obsolete("The recommended alternative is ContainerControlDesigner because it uses an EditableDesignerRegion for editing the content. Designer regions allow for better control of the content being edited. http://go.microsoft.com/fwlink/?linkid=14202")]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ReadWriteControlDesigner : ControlDesigner
	{
		// Token: 0x060002EF RID: 751 RVA: 0x0000F8DE File Offset: 0x0000DADE
		public ReadWriteControlDesigner()
		{
			base.ReadOnlyInternal = false;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		public override string GetDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			base.OnComponentChanged(sender, ce);
			if (base.IsIgnoringComponentChanges)
			{
				return;
			}
			if (!base.IsWebControl || base.DesignTimeElementInternal == null)
			{
				return;
			}
			MemberDescriptor member = ce.Member;
			object obj = ce.NewValue;
			Type type = Type.GetType("System.ComponentModel.ReflectPropertyDescriptor, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			if (member != null && member.GetType() == type)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)member;
				if (member.Name.Equals("Font"))
				{
					WebControl webControl = (WebControl)base.Component;
					obj = webControl.Font.Name;
					this.MapPropertyToStyle("Font.Name", obj);
					obj = webControl.Font.Size;
					this.MapPropertyToStyle("Font.Size", obj);
					obj = webControl.Font.Bold;
					this.MapPropertyToStyle("Font.Bold", obj);
					obj = webControl.Font.Italic;
					this.MapPropertyToStyle("Font.Italic", obj);
					obj = webControl.Font.Underline;
					this.MapPropertyToStyle("Font.Underline", obj);
					obj = webControl.Font.Strikeout;
					this.MapPropertyToStyle("Font.Strikeout", obj);
					obj = webControl.Font.Overline;
					this.MapPropertyToStyle("Font.Overline", obj);
					return;
				}
				if (obj != null)
				{
					if (propertyDescriptor.PropertyType == typeof(Color))
					{
						obj = ColorTranslator.ToHtml((Color)obj);
					}
					this.MapPropertyToStyle(propertyDescriptor.Name, obj);
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000FA7C File Offset: 0x0000DC7C
		protected virtual void MapPropertyToStyle(string propName, object varPropValue)
		{
			if (this.BehaviorInternal == null)
			{
				return;
			}
			if (propName == null || varPropValue == null)
			{
				return;
			}
			try
			{
				if (propName.Equals("BackColor"))
				{
					this.BehaviorInternal.SetStyleAttribute("backgroundColor", true, varPropValue, true);
				}
				else if (propName.Equals("ForeColor"))
				{
					this.BehaviorInternal.SetStyleAttribute("color", true, varPropValue, true);
				}
				else if (propName.Equals("BorderWidth"))
				{
					string value = Convert.ToString(varPropValue, CultureInfo.InvariantCulture);
					this.BehaviorInternal.SetStyleAttribute("borderWidth", true, value, true);
				}
				else if (propName.Equals("BorderStyle"))
				{
					string value2;
					if ((BorderStyle)varPropValue == BorderStyle.NotSet)
					{
						value2 = string.Empty;
					}
					else
					{
						value2 = Enum.Format(typeof(BorderStyle), (BorderStyle)varPropValue, "G");
					}
					this.BehaviorInternal.SetStyleAttribute("borderStyle", true, value2, true);
				}
				else if (propName.Equals("BorderColor"))
				{
					this.BehaviorInternal.SetStyleAttribute("borderColor", true, Convert.ToString(varPropValue, CultureInfo.InvariantCulture), true);
				}
				else if (propName.Equals("Height"))
				{
					this.BehaviorInternal.SetStyleAttribute("height", true, Convert.ToString(varPropValue, CultureInfo.InvariantCulture), true);
				}
				else if (propName.Equals("Width"))
				{
					this.BehaviorInternal.SetStyleAttribute("width", true, Convert.ToString(varPropValue, CultureInfo.InvariantCulture), true);
				}
				else if (propName.Equals("Font.Name"))
				{
					this.BehaviorInternal.SetStyleAttribute("fontFamily", true, Convert.ToString(varPropValue, CultureInfo.InvariantCulture), true);
				}
				else if (propName.Equals("Font.Size"))
				{
					this.BehaviorInternal.SetStyleAttribute("fontSize", true, Convert.ToString(varPropValue, CultureInfo.InvariantCulture), true);
				}
				else if (propName.Equals("Font.Bold"))
				{
					string value3;
					if ((bool)varPropValue)
					{
						value3 = "bold";
					}
					else
					{
						value3 = "normal";
					}
					this.BehaviorInternal.SetStyleAttribute("fontWeight", true, value3, true);
				}
				else if (propName.Equals("Font.Italic"))
				{
					string value4;
					if ((bool)varPropValue)
					{
						value4 = "italic";
					}
					else
					{
						value4 = "normal";
					}
					this.BehaviorInternal.SetStyleAttribute("fontStyle", true, value4, true);
				}
				else if (propName.Equals("Font.Underline"))
				{
					string text = (string)this.BehaviorInternal.GetStyleAttribute("textDecoration", true, true);
					if ((bool)varPropValue)
					{
						if (text == null)
						{
							text = "underline";
						}
						else if (text.ToLower(CultureInfo.InvariantCulture).IndexOf("underline", StringComparison.Ordinal) < 0)
						{
							text += " underline";
						}
						this.BehaviorInternal.SetStyleAttribute("textDecoration", true, text, true);
					}
					else if (text != null)
					{
						int num = text.ToLower(CultureInfo.InvariantCulture).IndexOf("underline", StringComparison.Ordinal);
						if (num >= 0)
						{
							string value5 = text.Substring(0, num);
							if (num + 9 < text.Length)
							{
								value5 = " " + text.Substring(num + 9);
							}
							this.BehaviorInternal.SetStyleAttribute("textDecoration", true, value5, true);
						}
					}
				}
				else if (propName.Equals("Font.Strikeout"))
				{
					string text2 = (string)this.BehaviorInternal.GetStyleAttribute("textDecoration", true, true);
					if ((bool)varPropValue)
					{
						if (text2 == null)
						{
							text2 = "line-through";
						}
						else if (text2.ToLower(CultureInfo.InvariantCulture).IndexOf("line-through", StringComparison.Ordinal) < 0)
						{
							text2 += " line-through";
						}
						this.BehaviorInternal.SetStyleAttribute("textDecoration", true, text2, true);
					}
					else if (text2 != null)
					{
						int num2 = text2.ToLower(CultureInfo.InvariantCulture).IndexOf("line-through", StringComparison.Ordinal);
						if (num2 >= 0)
						{
							string value6 = text2.Substring(0, num2);
							if (num2 + 12 < text2.Length)
							{
								value6 = " " + text2.Substring(num2 + 12);
							}
							this.BehaviorInternal.SetStyleAttribute("textDecoration", true, value6, true);
						}
					}
				}
				else if (propName.Equals("Font.Overline"))
				{
					string text3 = (string)this.BehaviorInternal.GetStyleAttribute("textDecoration", true, true);
					if ((bool)varPropValue)
					{
						if (text3 == null)
						{
							text3 = "overline";
						}
						else if (text3.ToLower(CultureInfo.InvariantCulture).IndexOf("overline", StringComparison.Ordinal) < 0)
						{
							text3 += " overline";
						}
						this.BehaviorInternal.SetStyleAttribute("textDecoration", true, text3, true);
					}
					else if (text3 != null)
					{
						int num3 = text3.ToLower(CultureInfo.InvariantCulture).IndexOf("overline", StringComparison.Ordinal);
						if (num3 >= 0)
						{
							string value7 = text3.Substring(0, num3);
							if (num3 + 8 < text3.Length)
							{
								value7 = " " + text3.Substring(num3 + 8);
							}
							this.BehaviorInternal.SetStyleAttribute("textDecoration", true, value7, true);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		[Obsolete("The recommended alternative is ControlDesigner.Tag. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected override void OnBehaviorAttached()
		{
			base.OnBehaviorAttached();
			if (!base.IsWebControl)
			{
				return;
			}
			WebControl webControl = (WebControl)base.Component;
			string text = ColorTranslator.ToHtml(webControl.BackColor);
			if (text.Length > 0)
			{
				this.MapPropertyToStyle("BackColor", text);
			}
			text = ColorTranslator.ToHtml(webControl.ForeColor);
			if (text.Length > 0)
			{
				this.MapPropertyToStyle("ForeColor", text);
			}
			text = ColorTranslator.ToHtml(webControl.BorderColor);
			if (text.Length > 0)
			{
				this.MapPropertyToStyle("BorderColor", text);
			}
			BorderStyle borderStyle = webControl.BorderStyle;
			if (borderStyle != BorderStyle.NotSet)
			{
				this.MapPropertyToStyle("BorderStyle", borderStyle);
			}
			Unit borderWidth = webControl.BorderWidth;
			if (!borderWidth.IsEmpty && borderWidth.Value != 0.0)
			{
				this.MapPropertyToStyle("BorderWidth", borderWidth.ToString(CultureInfo.InvariantCulture));
			}
			Unit width = webControl.Width;
			if (!width.IsEmpty && width.Value != 0.0)
			{
				this.MapPropertyToStyle("Width", width.ToString(CultureInfo.InvariantCulture));
			}
			Unit height = webControl.Height;
			if (!height.IsEmpty && height.Value != 0.0)
			{
				this.MapPropertyToStyle("Height", height.ToString(CultureInfo.InvariantCulture));
			}
			string name = webControl.Font.Name;
			if (name.Length != 0)
			{
				this.MapPropertyToStyle("Font.Name", name);
			}
			FontUnit size = webControl.Font.Size;
			if (size != FontUnit.Empty)
			{
				this.MapPropertyToStyle("Font.Size", size.ToString(CultureInfo.InvariantCulture));
			}
			bool flag = webControl.Font.Bold;
			if (flag)
			{
				this.MapPropertyToStyle("Font.Bold", flag);
			}
			flag = webControl.Font.Italic;
			if (flag)
			{
				this.MapPropertyToStyle("Font.Italic", flag);
			}
			flag = webControl.Font.Underline;
			if (flag)
			{
				this.MapPropertyToStyle("Font.Underline", flag);
			}
			flag = webControl.Font.Strikeout;
			if (flag)
			{
				this.MapPropertyToStyle("Font.Strikeout", flag);
			}
			flag = webControl.Font.Overline;
			if (flag)
			{
				this.MapPropertyToStyle("Font.Overline", flag);
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00003937 File Offset: 0x00001B37
		public override void UpdateDesignTimeHtml()
		{
		}
	}
}
