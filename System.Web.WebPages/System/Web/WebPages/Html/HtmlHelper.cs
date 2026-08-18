using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages.Resources;
using System.Web.WebPages.Scope;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages.Html
{
	// Token: 0x02000070 RID: 112
	public class HtmlHelper
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000A902 File Offset: 0x00008B02
		public IHtmlString CheckBox(string name)
		{
			return this.CheckBox(name, null);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000A90C File Offset: 0x00008B0C
		public IHtmlString CheckBox(string name, object htmlAttributes)
		{
			return this.CheckBox(name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000A91C File Offset: 0x00008B1C
		public IHtmlString CheckBox(string name, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildCheckBox(name, null, htmlAttributes);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000A952 File Offset: 0x00008B52
		public IHtmlString CheckBox(string name, bool isChecked)
		{
			return this.CheckBox(name, isChecked, null);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000A95D File Offset: 0x00008B5D
		public IHtmlString CheckBox(string name, bool isChecked, object htmlAttributes)
		{
			return this.CheckBox(name, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000A96D File Offset: 0x00008B6D
		public IHtmlString CheckBox(string name, bool isChecked, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildCheckBox(name, new bool?(isChecked), htmlAttributes);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000A998 File Offset: 0x00008B98
		private IHtmlString BuildCheckBox(string name, bool? isChecked, IDictionary<string, object> attributes)
		{
			TagBuilder tagBuilder = new TagBuilder("input");
			tagBuilder.MergeAttribute("type", "checkbox", true);
			tagBuilder.GenerateId(name);
			tagBuilder.MergeAttributes<string, object>(attributes, true);
			tagBuilder.MergeAttribute("name", name, true);
			if (HtmlHelper.UnobtrusiveJavaScriptEnabled)
			{
				IDictionary<string, object> unobtrusiveValidationAttributes = this._validationHelper.GetUnobtrusiveValidationAttributes(name);
				tagBuilder.MergeAttributes<string, object>(unobtrusiveValidationAttributes, false);
			}
			ModelState modelState = this.ModelState[name];
			if (modelState != null && modelState.Value != null)
			{
				bool flag = (bool)HtmlHelper.ConvertTo(modelState.Value, typeof(bool));
				isChecked = new bool?(isChecked ?? flag);
			}
			if (isChecked != null)
			{
				if (isChecked.Value)
				{
					tagBuilder.MergeAttribute("checked", "checked", true);
				}
				else
				{
					tagBuilder.Attributes.Remove("checked");
				}
			}
			this.AddErrorClass(tagBuilder, name);
			return tagBuilder.ToHtmlString(TagRenderMode.SelfClosing);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000AA8E File Offset: 0x00008C8E
		internal HtmlHelper(ModelStateDictionary modelState, ValidationHelper validationHelper)
		{
			this.ModelState = modelState;
			this._validationHelper = validationHelper;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000AAC1 File Offset: 0x00008CC1
		public static string IdAttributeDotReplacement
		{
			get
			{
				if (string.IsNullOrEmpty(HtmlHelper._idAttributeDotReplacement))
				{
					HtmlHelper._idAttributeDotReplacement = "_";
				}
				return HtmlHelper._idAttributeDotReplacement;
			}
			set
			{
				HtmlHelper._idAttributeDotReplacement = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000AAC9 File Offset: 0x00008CC9
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public static string ValidationInputValidCssClassName
		{
			get
			{
				return (ScopeStorage.CurrentScope[HtmlHelper._validationInputValidClassKey] as string) ?? "input-validation-valid";
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ScopeStorage.CurrentScope[HtmlHelper._validationInputValidClassKey] = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000AB08 File Offset: 0x00008D08
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000AB27 File Offset: 0x00008D27
		public static string ValidationInputCssClassName
		{
			get
			{
				return (ScopeStorage.CurrentScope[HtmlHelper._validationInputErrorClassKey] as string) ?? "input-validation-error";
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ScopeStorage.CurrentScope[HtmlHelper._validationInputErrorClassKey] = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000AB47 File Offset: 0x00008D47
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000AB66 File Offset: 0x00008D66
		public static string ValidationMessageValidCssClassName
		{
			get
			{
				return (ScopeStorage.CurrentScope[HtmlHelper._validationMessageValidClassKey] as string) ?? "field-validation-valid";
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ScopeStorage.CurrentScope[HtmlHelper._validationMessageValidClassKey] = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000AB86 File Offset: 0x00008D86
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000ABA5 File Offset: 0x00008DA5
		public static string ValidationMessageCssClassName
		{
			get
			{
				return (ScopeStorage.CurrentScope[HtmlHelper._validationMesssageErrorClassKey] as string) ?? "field-validation-error";
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ScopeStorage.CurrentScope[HtmlHelper._validationMesssageErrorClassKey] = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		public static string ValidationSummaryClass
		{
			get
			{
				return (ScopeStorage.CurrentScope[HtmlHelper._validationSummaryClassKey] as string) ?? "validation-summary-errors";
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ScopeStorage.CurrentScope[HtmlHelper._validationSummaryClassKey] = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000AC04 File Offset: 0x00008E04
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000AC23 File Offset: 0x00008E23
		public static string ValidationSummaryValidClass
		{
			get
			{
				return (ScopeStorage.CurrentScope[HtmlHelper._validationSummaryValidClassKey] as string) ?? "validation-summary-valid";
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ScopeStorage.CurrentScope[HtmlHelper._validationSummaryValidClassKey] = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000AC44 File Offset: 0x00008E44
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000AC7A File Offset: 0x00008E7A
		public static bool UnobtrusiveJavaScriptEnabled
		{
			get
			{
				return ((bool?)ScopeStorage.CurrentScope[HtmlHelper._unobtrusiveValidationKey]) ?? true;
			}
			set
			{
				ScopeStorage.CurrentScope[HtmlHelper._unobtrusiveValidationKey] = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000AC91 File Offset: 0x00008E91
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000AC99 File Offset: 0x00008E99
		private ModelStateDictionary ModelState { get; set; }

		// Token: 0x06000304 RID: 772 RVA: 0x0000ACA2 File Offset: 0x00008EA2
		public string AttributeEncode(object value)
		{
			return this.AttributeEncode(Convert.ToString(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000ACB5 File Offset: 0x00008EB5
		public string AttributeEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return HttpUtility.HtmlAttributeEncode(value);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000ACCB File Offset: 0x00008ECB
		public string Encode(object value)
		{
			return this.Encode(Convert.ToString(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000ACDE File Offset: 0x00008EDE
		public string Encode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return HttpUtility.HtmlEncode(value);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		public IHtmlString Raw(string value)
		{
			return new HtmlString(value);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000ACFC File Offset: 0x00008EFC
		public IHtmlString Raw(object value)
		{
			return new HtmlString((value == null) ? null : value.ToString());
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000AD10 File Offset: 0x00008F10
		public static RouteValueDictionary AnonymousObjectToHtmlAttributes(object htmlAttributes)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			if (htmlAttributes != null)
			{
				foreach (PropertyHelper propertyHelper in HtmlAttributePropertyHelper.GetProperties(htmlAttributes))
				{
					routeValueDictionary.Add(propertyHelper.Name, propertyHelper.GetValue(htmlAttributes));
				}
			}
			return routeValueDictionary;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000AD53 File Offset: 0x00008F53
		public static IDictionary<string, object> ObjectToDictionary(object value)
		{
			return TypeHelper.ObjectToDictionary(value);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000AD5B File Offset: 0x00008F5B
		public IHtmlString TextBox(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildInputField(name, HtmlHelper.InputType.Text, null, false, null);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000AD80 File Offset: 0x00008F80
		public IHtmlString TextBox(string name, object value)
		{
			return this.TextBox(name, value, null);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000AD8B File Offset: 0x00008F8B
		public IHtmlString TextBox(string name, object value, object htmlAttributes)
		{
			return this.TextBox(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000AD9B File Offset: 0x00008F9B
		public IHtmlString TextBox(string name, object value, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildInputField(name, HtmlHelper.InputType.Text, value, true, htmlAttributes);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		public IHtmlString Hidden(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildInputField(name, HtmlHelper.InputType.Hidden, null, false, null);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000ADE5 File Offset: 0x00008FE5
		public IHtmlString Hidden(string name, object value)
		{
			return this.Hidden(name, value, null);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		public IHtmlString Hidden(string name, object value, object htmlAttributes)
		{
			return this.Hidden(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000AE00 File Offset: 0x00009000
		public IHtmlString Hidden(string name, object value, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildInputField(name, HtmlHelper.InputType.Hidden, HtmlHelper.GetHiddenFieldValue(value), true, htmlAttributes);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000AE2C File Offset: 0x0000902C
		private static object GetHiddenFieldValue(object value)
		{
			Binary binary = value as Binary;
			if (binary != null)
			{
				value = binary.ToArray();
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				value = Convert.ToBase64String(array);
			}
			return value;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000AE64 File Offset: 0x00009064
		public IHtmlString Password(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildInputField(name, HtmlHelper.InputType.Password, null, false, null);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000AE89 File Offset: 0x00009089
		public IHtmlString Password(string name, object value)
		{
			return this.Password(name, value, null);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000AE94 File Offset: 0x00009094
		public IHtmlString Password(string name, object value, object htmlAttributes)
		{
			return this.Password(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000AEA4 File Offset: 0x000090A4
		public IHtmlString Password(string name, object value, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildInputField(name, HtmlHelper.InputType.Password, value, true, htmlAttributes);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000AECC File Offset: 0x000090CC
		private IHtmlString BuildInputField(string name, HtmlHelper.InputType type, object value, bool isExplicitValue, IDictionary<string, object> attributes)
		{
			TagBuilder tagBuilder = new TagBuilder("input");
			tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(type));
			tagBuilder.GenerateId(name);
			tagBuilder.MergeAttributes<string, object>(attributes, true);
			if (HtmlHelper.UnobtrusiveJavaScriptEnabled)
			{
				IDictionary<string, object> unobtrusiveValidationAttributes = this._validationHelper.GetUnobtrusiveValidationAttributes(name);
				tagBuilder.MergeAttributes<string, object>(unobtrusiveValidationAttributes, false);
			}
			tagBuilder.MergeAttribute("name", name, true);
			ModelState modelState = this.ModelState[name];
			if (type != HtmlHelper.InputType.Password && modelState != null)
			{
				object obj;
				if ((obj = value) == null)
				{
					obj = (modelState.Value ?? string.Empty);
				}
				value = obj;
			}
			if (type != HtmlHelper.InputType.Password || (type == HtmlHelper.InputType.Password && value != null))
			{
				tagBuilder.MergeAttribute("value", (string)HtmlHelper.ConvertTo(value, typeof(string)), isExplicitValue);
			}
			this.AddErrorClass(tagBuilder, name);
			return tagBuilder.ToHtmlString(TagRenderMode.SelfClosing);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000AF94 File Offset: 0x00009194
		private static string GetInputTypeString(HtmlHelper.InputType inputType)
		{
			if (!Enum.IsDefined(typeof(HtmlHelper.InputType), inputType))
			{
				inputType = HtmlHelper.InputType.Text;
			}
			return inputType.ToString().ToLowerInvariant();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000AFC0 File Offset: 0x000091C0
		private void AddErrorClass(TagBuilder tagBuilder, string name)
		{
			if (!this.ModelState.IsValidField(name))
			{
				tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000AFDB File Offset: 0x000091DB
		private static object ConvertTo(object value, Type type)
		{
			return HtmlHelper.UnwrapPossibleArrayType(value, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000AFEC File Offset: 0x000091EC
		private static object UnwrapPossibleArrayType(object value, Type destinationType, CultureInfo culture)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			Array array = value as Array;
			if (destinationType.IsArray)
			{
				Type elementType = destinationType.GetElementType();
				if (array != null)
				{
					IList list = Array.CreateInstance(elementType, array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						list[i] = HtmlHelper.ConvertSimpleType(array.GetValue(i), elementType, culture);
					}
					return list;
				}
				object value2 = HtmlHelper.ConvertSimpleType(value, elementType, culture);
				IList list2 = Array.CreateInstance(elementType, 1);
				list2[0] = value2;
				return list2;
			}
			else
			{
				if (array == null)
				{
					return HtmlHelper.ConvertSimpleType(value, destinationType, culture);
				}
				if (array.Length > 0)
				{
					value = array.GetValue(0);
					return HtmlHelper.ConvertSimpleType(value, destinationType, culture);
				}
				return null;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B0A0 File Offset: 0x000092A0
		private static object ConvertSimpleType(object value, Type destinationType, CultureInfo culture)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			string text = value as string;
			if (text != null && text.Trim().Length == 0)
			{
				return null;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(destinationType);
			bool flag = converter.CanConvertFrom(value.GetType());
			if (!flag)
			{
				converter = TypeDescriptor.GetConverter(value.GetType());
			}
			if (!flag && !converter.CanConvertTo(destinationType))
			{
				string message = string.Format(CultureInfo.CurrentCulture, WebPageResources.HtmlHelper_NoConverterExists, new object[]
				{
					value.GetType().FullName,
					destinationType.FullName
				});
				throw new InvalidOperationException(message);
			}
			object result;
			try
			{
				object obj = flag ? converter.ConvertFrom(null, culture, value) : converter.ConvertTo(null, culture, value, destinationType);
				result = obj;
			}
			catch (Exception innerException)
			{
				string message2 = string.Format(CultureInfo.CurrentUICulture, WebPageResources.HtmlHelper_ConversionThrew, new object[]
				{
					value.GetType().FullName,
					destinationType.FullName
				});
				throw new InvalidOperationException(message2, innerException);
			}
			return result;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public IHtmlString Label(string labelText)
		{
			return this.Label(labelText, null, null);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B1BB File Offset: 0x000093BB
		public IHtmlString Label(string labelText, string labelFor)
		{
			return this.Label(labelText, labelFor, null);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B1C6 File Offset: 0x000093C6
		public IHtmlString Label(string labelText, object attributes)
		{
			return this.Label(labelText, null, HtmlHelper.AnonymousObjectToHtmlAttributes(attributes));
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B1D6 File Offset: 0x000093D6
		public IHtmlString Label(string labelText, string labelFor, object attributes)
		{
			return this.Label(labelText, labelFor, HtmlHelper.AnonymousObjectToHtmlAttributes(attributes));
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000B1E8 File Offset: 0x000093E8
		public IHtmlString Label(string labelText, string labelFor, IDictionary<string, object> attributes)
		{
			if (string.IsNullOrEmpty(labelText))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "labelText");
			}
			labelFor = (labelFor ?? labelText);
			TagBuilder tagBuilder = new TagBuilder("label")
			{
				InnerHtml = this.Encode(labelText)
			};
			if (!string.IsNullOrEmpty(labelFor))
			{
				tagBuilder.MergeAttribute("for", labelFor);
			}
			tagBuilder.MergeAttributes<string, object>(attributes, false);
			return tagBuilder.ToHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000B252 File Offset: 0x00009452
		public IHtmlString RadioButton(string name, object value)
		{
			return this.RadioButton(name, value, null);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000B25D File Offset: 0x0000945D
		public IHtmlString RadioButton(string name, object value, object htmlAttributes)
		{
			return this.RadioButton(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000B270 File Offset: 0x00009470
		public IHtmlString RadioButton(string name, object value, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildRadioButton(name, value, null, htmlAttributes);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000B2A7 File Offset: 0x000094A7
		public IHtmlString RadioButton(string name, object value, bool isChecked)
		{
			return this.RadioButton(name, value, isChecked, null);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000B2B3 File Offset: 0x000094B3
		public IHtmlString RadioButton(string name, object value, bool isChecked, object htmlAttributes)
		{
			return this.RadioButton(name, value, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B2C5 File Offset: 0x000094C5
		public IHtmlString RadioButton(string name, object value, bool isChecked, IDictionary<string, object> htmlAttributes)
		{
			if (name == null)
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildRadioButton(name, value, new bool?(isChecked), htmlAttributes);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B2EC File Offset: 0x000094EC
		private IHtmlString BuildRadioButton(string name, object value, bool? isChecked, IDictionary<string, object> attributes)
		{
			string text = HtmlHelper.ConvertTo(value, typeof(string)) as string;
			TagBuilder tagBuilder = new TagBuilder("input");
			tagBuilder.MergeAttribute("type", "radio", true);
			tagBuilder.GenerateId(name);
			tagBuilder.MergeAttributes<string, object>(attributes, true);
			tagBuilder.MergeAttribute("value", text, true);
			tagBuilder.MergeAttribute("name", name, true);
			if (HtmlHelper.UnobtrusiveJavaScriptEnabled)
			{
				IDictionary<string, object> unobtrusiveValidationAttributes = this._validationHelper.GetUnobtrusiveValidationAttributes(name);
				tagBuilder.MergeAttributes<string, object>(unobtrusiveValidationAttributes, false);
			}
			ModelState modelState = this.ModelState[name];
			if (modelState != null)
			{
				string a = HtmlHelper.ConvertTo(modelState.Value, typeof(string)) as string;
				isChecked = new bool?(isChecked ?? string.Equals(a, text, StringComparison.OrdinalIgnoreCase));
			}
			if (isChecked != null)
			{
				if (isChecked.Value)
				{
					tagBuilder.MergeAttribute("checked", "checked", true);
				}
				else
				{
					tagBuilder.Attributes.Remove("checked");
				}
			}
			this.AddErrorClass(tagBuilder, name);
			return tagBuilder.ToHtmlString(TagRenderMode.SelfClosing);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B40A File Offset: 0x0000960A
		public IHtmlString ListBox(string name, IEnumerable<SelectListItem> selectList)
		{
			return this.ListBox(name, null, selectList, null);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000B416 File Offset: 0x00009616
		public IHtmlString ListBox(string name, string defaultOption, IEnumerable<SelectListItem> selectList)
		{
			return this.ListBox(name, defaultOption, selectList, null, null);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000B423 File Offset: 0x00009623
		public IHtmlString ListBox(string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return this.ListBox(name, null, selectList, null, htmlAttributes);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B430 File Offset: 0x00009630
		public IHtmlString ListBox(string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return this.ListBox(name, null, selectList, null, htmlAttributes);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B43D File Offset: 0x0000963D
		public IHtmlString ListBox(string name, string defaultOption, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return this.ListBox(name, defaultOption, selectList, null, htmlAttributes);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B44B File Offset: 0x0000964B
		public IHtmlString ListBox(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return this.ListBox(name, defaultOption, selectList, null, htmlAttributes);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B45C File Offset: 0x0000965C
		public IHtmlString ListBox(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object selectedValues, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildListBox(name, defaultOption, selectList, selectedValues, null, false, htmlAttributes);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B498 File Offset: 0x00009698
		public IHtmlString ListBox(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object selectedValues, object htmlAttributes)
		{
			return this.ListBox(name, defaultOption, selectList, selectedValues, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000B4AC File Offset: 0x000096AC
		public IHtmlString ListBox(string name, IEnumerable<SelectListItem> selectList, object selectedValues, int size, bool allowMultiple)
		{
			return this.ListBox(name, null, selectList, selectedValues, size, allowMultiple, null);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000B4BD File Offset: 0x000096BD
		public IHtmlString ListBox(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object selectedValues, int size, bool allowMultiple)
		{
			return this.ListBox(name, defaultOption, selectList, selectedValues, size, allowMultiple, null);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B4CF File Offset: 0x000096CF
		public IHtmlString ListBox(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object selectedValues, int size, bool allowMultiple, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildListBox(name, defaultOption, selectList, selectedValues, new int?(size), allowMultiple, htmlAttributes);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000B4FF File Offset: 0x000096FF
		public IHtmlString ListBox(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object selectedValues, int size, bool allowMultiple, object htmlAttributes)
		{
			return this.ListBox(name, defaultOption, selectList, selectedValues, size, allowMultiple, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B524 File Offset: 0x00009724
		private IHtmlString BuildListBox(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object selectedValues, int? size, bool allowMultiple, IDictionary<string, object> htmlAttributes)
		{
			ModelState modelState = this.ModelState[name];
			if (modelState != null)
			{
				selectedValues = (selectedValues ?? this.ModelState[name].Value);
			}
			if (selectedValues != null)
			{
				IEnumerable source = allowMultiple ? (HtmlHelper.ConvertTo(selectedValues, typeof(string[])) as string[]) : new object[]
				{
					HtmlHelper.ConvertTo(selectedValues, typeof(string))
				};
				HashSet<string> hashSet = new HashSet<string>(from object value in source
				select Convert.ToString(value, CultureInfo.CurrentCulture), StringComparer.OrdinalIgnoreCase);
				List<SelectListItem> list = new List<SelectListItem>();
				bool flag = false;
				foreach (SelectListItem selectListItem in selectList)
				{
					bool flag2 = false;
					if (allowMultiple || !flag)
					{
						flag2 = (selectListItem.Selected || hashSet.Contains(selectListItem.Value ?? selectListItem.Text));
					}
					flag = (flag || flag2);
					list.Add(new SelectListItem(selectListItem)
					{
						Selected = flag2
					});
				}
				selectList = list;
			}
			TagBuilder tagBuilder = new TagBuilder("select")
			{
				InnerHtml = HtmlHelper.BuildListOptions(selectList, defaultOption)
			};
			if (HtmlHelper.UnobtrusiveJavaScriptEnabled)
			{
				IDictionary<string, object> unobtrusiveValidationAttributes = this._validationHelper.GetUnobtrusiveValidationAttributes(name);
				tagBuilder.MergeAttributes<string, object>(unobtrusiveValidationAttributes, false);
			}
			tagBuilder.GenerateId(name);
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.MergeAttribute("name", name, true);
			if (size != null)
			{
				tagBuilder.MergeAttribute("size", size.ToString(), true);
			}
			if (allowMultiple)
			{
				tagBuilder.MergeAttribute("multiple", "multiple");
			}
			else if (tagBuilder.Attributes.ContainsKey("multiple"))
			{
				tagBuilder.Attributes.Remove("multiple");
			}
			this.AddErrorClass(tagBuilder, name);
			return tagBuilder.ToHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B734 File Offset: 0x00009934
		public IHtmlString DropDownList(string name, IEnumerable<SelectListItem> selectList)
		{
			return this.DropDownList(name, null, selectList, null);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B740 File Offset: 0x00009940
		public IHtmlString DropDownList(string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return this.DropDownList(name, null, selectList, null, htmlAttributes);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000B74D File Offset: 0x0000994D
		public IHtmlString DropDownList(string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return this.DropDownList(name, null, selectList, null, htmlAttributes);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000B75A File Offset: 0x0000995A
		public IHtmlString DropDownList(string name, string defaultOption, IEnumerable<SelectListItem> selectList)
		{
			return this.DropDownList(name, defaultOption, selectList, null, null);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000B767 File Offset: 0x00009967
		public IHtmlString DropDownList(string name, string defaultOption, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return this.DropDownList(name, defaultOption, selectList, null, htmlAttributes);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000B775 File Offset: 0x00009975
		public IHtmlString DropDownList(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return this.DropDownList(name, defaultOption, selectList, null, htmlAttributes);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000B783 File Offset: 0x00009983
		public IHtmlString DropDownList(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object selectedValue, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildDropDownList(name, defaultOption, selectList, selectedValue, htmlAttributes);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000B7AA File Offset: 0x000099AA
		public IHtmlString DropDownList(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object selectedValue, object htmlAttributes)
		{
			return this.DropDownList(name, defaultOption, selectList, selectedValue, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000B808 File Offset: 0x00009A08
		private IHtmlString BuildDropDownList(string name, string defaultOption, IEnumerable<SelectListItem> selectList, object selectedValue, IDictionary<string, object> htmlAttributes)
		{
			ModelState modelState = this.ModelState[name];
			if (modelState != null)
			{
				selectedValue = (selectedValue ?? this.ModelState[name].Value);
			}
			selectedValue = HtmlHelper.ConvertTo(selectedValue, typeof(string));
			if (selectedValue != null)
			{
				List<SelectListItem> list = new List<SelectListItem>(from item in selectList
				select new SelectListItem(item));
				StringComparer comparer = StringComparer.InvariantCultureIgnoreCase;
				SelectListItem selectListItem = list.FirstOrDefault((SelectListItem item) => item.Selected || comparer.Equals(item.Value ?? item.Text, selectedValue));
				if (selectListItem != null)
				{
					selectListItem.Selected = true;
					selectList = list;
				}
			}
			TagBuilder tagBuilder = new TagBuilder("select")
			{
				InnerHtml = HtmlHelper.BuildListOptions(selectList, defaultOption)
			};
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.MergeAttribute("name", name, true);
			tagBuilder.GenerateId(name);
			if (HtmlHelper.UnobtrusiveJavaScriptEnabled)
			{
				IDictionary<string, object> unobtrusiveValidationAttributes = this._validationHelper.GetUnobtrusiveValidationAttributes(name);
				tagBuilder.MergeAttributes<string, object>(unobtrusiveValidationAttributes, false);
			}
			this.AddErrorClass(tagBuilder, name);
			return tagBuilder.ToHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000B950 File Offset: 0x00009B50
		private static string BuildListOptions(IEnumerable<SelectListItem> selectList, string optionText)
		{
			StringBuilder stringBuilder = new StringBuilder().AppendLine();
			if (optionText != null)
			{
				stringBuilder.AppendLine(HtmlHelper.ListItemToOption(new SelectListItem
				{
					Text = optionText,
					Value = string.Empty
				}));
			}
			if (selectList != null)
			{
				foreach (SelectListItem item in selectList)
				{
					stringBuilder.AppendLine(HtmlHelper.ListItemToOption(item));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private static string ListItemToOption(SelectListItem item)
		{
			TagBuilder tagBuilder = new TagBuilder("option")
			{
				InnerHtml = HttpUtility.HtmlEncode(item.Text)
			};
			if (item.Value != null)
			{
				tagBuilder.Attributes["value"] = item.Value;
			}
			if (item.Selected)
			{
				tagBuilder.Attributes["selected"] = "selected";
			}
			return tagBuilder.ToString(TagRenderMode.Normal);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000BA4C File Offset: 0x00009C4C
		private static IDictionary<string, object> GetRowsAndColumnsDictionary(int rows, int columns)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (rows > 0)
			{
				dictionary.Add("rows", rows.ToString(CultureInfo.InvariantCulture));
			}
			if (columns > 0)
			{
				dictionary.Add("cols", columns.ToString(CultureInfo.InvariantCulture));
			}
			return dictionary;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000BA96 File Offset: 0x00009C96
		public IHtmlString TextArea(string name)
		{
			return this.TextArea(name, null, null);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000BAA1 File Offset: 0x00009CA1
		public IHtmlString TextArea(string name, object htmlAttributes)
		{
			return this.TextArea(name, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BAB1 File Offset: 0x00009CB1
		public IHtmlString TextArea(string name, IDictionary<string, object> htmlAttributes)
		{
			return this.TextArea(name, null, htmlAttributes);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000BABC File Offset: 0x00009CBC
		public IHtmlString TextArea(string name, string value)
		{
			return this.TextArea(name, value, null);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000BAC7 File Offset: 0x00009CC7
		public IHtmlString TextArea(string name, string value, object htmlAttributes)
		{
			return this.TextArea(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000BAD7 File Offset: 0x00009CD7
		public IHtmlString TextArea(string name, string value, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildTextArea(name, value, HtmlHelper._implicitRowsAndColumns, htmlAttributes);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BAFF File Offset: 0x00009CFF
		public IHtmlString TextArea(string name, string value, int rows, int columns, object htmlAttributes)
		{
			return this.TextArea(name, value, rows, columns, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BB13 File Offset: 0x00009D13
		public IHtmlString TextArea(string name, string value, int rows, int columns, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildTextArea(name, value, HtmlHelper.GetRowsAndColumnsDictionary(rows, columns), htmlAttributes);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BB40 File Offset: 0x00009D40
		private IHtmlString BuildTextArea(string name, string value, IDictionary<string, object> rowsAndColumnsDictionary, IDictionary<string, object> htmlAttributes)
		{
			TagBuilder tagBuilder = new TagBuilder("textarea");
			if (HtmlHelper.UnobtrusiveJavaScriptEnabled)
			{
				IDictionary<string, object> unobtrusiveValidationAttributes = this._validationHelper.GetUnobtrusiveValidationAttributes(name);
				tagBuilder.MergeAttributes<string, object>(unobtrusiveValidationAttributes, false);
			}
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.MergeAttributes<string, object>(rowsAndColumnsDictionary, rowsAndColumnsDictionary != HtmlHelper._implicitRowsAndColumns);
			ModelState modelState = this.ModelState[name];
			if (modelState != null)
			{
				value = (value ?? Convert.ToString(this.ModelState[name].Value, CultureInfo.CurrentCulture));
			}
			tagBuilder.InnerHtml = this.Encode(value);
			tagBuilder.MergeAttribute("name", name);
			tagBuilder.GenerateId(name);
			this.AddErrorClass(tagBuilder, name);
			return tagBuilder.ToHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BBEF File Offset: 0x00009DEF
		public IHtmlString ValidationMessage(string name)
		{
			return this.ValidationMessage(name, null, null);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BBFA File Offset: 0x00009DFA
		public IHtmlString ValidationMessage(string name, string message)
		{
			return this.ValidationMessage(name, message, null);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BC05 File Offset: 0x00009E05
		public IHtmlString ValidationMessage(string name, object htmlAttributes)
		{
			return this.ValidationMessage(name, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000BC15 File Offset: 0x00009E15
		public IHtmlString ValidationMessage(string name, IDictionary<string, object> htmlAttributes)
		{
			return this.ValidationMessage(name, null, htmlAttributes);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000BC20 File Offset: 0x00009E20
		public IHtmlString ValidationMessage(string name, string message, object htmlAttributes)
		{
			return this.ValidationMessage(name, message, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000BC30 File Offset: 0x00009E30
		public IHtmlString ValidationMessage(string name, string message, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			return this.BuildValidationMessage(name, message, htmlAttributes);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000BC54 File Offset: 0x00009E54
		private IHtmlString BuildValidationMessage(string name, string message, IDictionary<string, object> htmlAttributes)
		{
			ModelState modelState = this.ModelState[name];
			IEnumerable<string> enumerable = null;
			if (modelState != null)
			{
				enumerable = modelState.Errors;
			}
			bool flag = enumerable != null && enumerable.Any<string>();
			if (!flag && !HtmlHelper.UnobtrusiveJavaScriptEnabled)
			{
				return null;
			}
			string value = null;
			if (flag)
			{
				value = (message ?? enumerable.First<string>());
			}
			TagBuilder tagBuilder = new TagBuilder("span")
			{
				InnerHtml = this.Encode(value)
			};
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			if (HtmlHelper.UnobtrusiveJavaScriptEnabled)
			{
				bool flag2 = string.IsNullOrEmpty(message);
				tagBuilder.MergeAttribute("data-valmsg-for", name);
				tagBuilder.MergeAttribute("data-valmsg-replace", flag2.ToString().ToLowerInvariant());
			}
			tagBuilder.AddCssClass(flag ? HtmlHelper.ValidationMessageCssClassName : HtmlHelper.ValidationMessageValidCssClassName);
			return tagBuilder.ToHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000BD1C File Offset: 0x00009F1C
		public IHtmlString ValidationSummary()
		{
			return this.BuildValidationSummary(null, false, null);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000BD27 File Offset: 0x00009F27
		public IHtmlString ValidationSummary(string message)
		{
			return this.BuildValidationSummary(message, false, null);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000BD32 File Offset: 0x00009F32
		public IHtmlString ValidationSummary(bool excludeFieldErrors)
		{
			return this.ValidationSummary(null, excludeFieldErrors, null);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000BD3D File Offset: 0x00009F3D
		public IHtmlString ValidationSummary(object htmlAttributes)
		{
			return this.ValidationSummary(null, false, htmlAttributes);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000BD48 File Offset: 0x00009F48
		public IHtmlString ValidationSummary(IDictionary<string, object> htmlAttributes)
		{
			return this.ValidationSummary(null, false, htmlAttributes);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000BD53 File Offset: 0x00009F53
		public IHtmlString ValidationSummary(string message, object htmlAttributes)
		{
			return this.ValidationSummary(message, false, htmlAttributes);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000BD5E File Offset: 0x00009F5E
		public IHtmlString ValidationSummary(string message, IDictionary<string, object> htmlAttributes)
		{
			return this.ValidationSummary(message, false, htmlAttributes);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000BD69 File Offset: 0x00009F69
		public IHtmlString ValidationSummary(string message, bool excludeFieldErrors, object htmlAttributes)
		{
			return this.ValidationSummary(message, excludeFieldErrors, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000BD79 File Offset: 0x00009F79
		public IHtmlString ValidationSummary(string message, bool excludeFieldErrors, IDictionary<string, object> htmlAttributes)
		{
			return this.BuildValidationSummary(message, excludeFieldErrors, htmlAttributes);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000BD94 File Offset: 0x00009F94
		private IHtmlString BuildValidationSummary(string message, bool excludeFieldErrors, IDictionary<string, object> htmlAttributes)
		{
			IEnumerable<string> enumerable = null;
			if (excludeFieldErrors)
			{
				ModelState modelState = this.ModelState["_FORM"];
				if (modelState != null)
				{
					enumerable = modelState.Errors;
				}
			}
			else
			{
				enumerable = this.ModelState.SelectMany((KeyValuePair<string, ModelState> c) => c.Value.Errors);
			}
			bool flag = enumerable != null && enumerable.Any<string>();
			if (!flag && (!HtmlHelper.UnobtrusiveJavaScriptEnabled || excludeFieldErrors))
			{
				return null;
			}
			TagBuilder tagBuilder = new TagBuilder("div");
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.AddCssClass(flag ? HtmlHelper.ValidationSummaryClass : HtmlHelper.ValidationSummaryValidClass);
			if (HtmlHelper.UnobtrusiveJavaScriptEnabled && !excludeFieldErrors)
			{
				tagBuilder.MergeAttribute("data-valmsg-summary", "true");
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (message != null)
			{
				stringBuilder.Append("<span>");
				stringBuilder.Append(this.Encode(message));
				stringBuilder.AppendLine("</span>");
			}
			stringBuilder.AppendLine("<ul>");
			foreach (string value in enumerable)
			{
				stringBuilder.Append("<li>");
				stringBuilder.Append(this.Encode(value));
				stringBuilder.AppendLine("</li>");
			}
			stringBuilder.Append("</ul>");
			tagBuilder.InnerHtml = stringBuilder.ToString();
			return tagBuilder.ToHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x040000E5 RID: 229
		internal const string DefaultValidationInputErrorCssClass = "input-validation-error";

		// Token: 0x040000E6 RID: 230
		private const string DefaultValidationInputValidCssClass = "input-validation-valid";

		// Token: 0x040000E7 RID: 231
		private const string DefaultValidationMessageErrorCssClass = "field-validation-error";

		// Token: 0x040000E8 RID: 232
		private const string DefaultValidationMessageValidCssClass = "field-validation-valid";

		// Token: 0x040000E9 RID: 233
		private const string DefaultValidationSummaryErrorCssClass = "validation-summary-errors";

		// Token: 0x040000EA RID: 234
		private const string DefaultValidationSummaryValidCssClassName = "validation-summary-valid";

		// Token: 0x040000EB RID: 235
		private const int TextAreaRows = 2;

		// Token: 0x040000EC RID: 236
		private const int TextAreaColumns = 20;

		// Token: 0x040000ED RID: 237
		private static readonly object _validationMesssageErrorClassKey = new object();

		// Token: 0x040000EE RID: 238
		private static readonly object _validationMessageValidClassKey = new object();

		// Token: 0x040000EF RID: 239
		private static readonly object _validationInputErrorClassKey = new object();

		// Token: 0x040000F0 RID: 240
		private static readonly object _validationInputValidClassKey = new object();

		// Token: 0x040000F1 RID: 241
		private static readonly object _validationSummaryClassKey = new object();

		// Token: 0x040000F2 RID: 242
		private static readonly object _validationSummaryValidClassKey = new object();

		// Token: 0x040000F3 RID: 243
		private static readonly object _unobtrusiveValidationKey = new object();

		// Token: 0x040000F4 RID: 244
		private static string _idAttributeDotReplacement;

		// Token: 0x040000F5 RID: 245
		private readonly ValidationHelper _validationHelper;

		// Token: 0x040000F6 RID: 246
		private static readonly IDictionary<string, object> _implicitRowsAndColumns = new Dictionary<string, object>
		{
			{
				"rows",
				2.ToString(CultureInfo.InvariantCulture)
			},
			{
				"cols",
				20.ToString(CultureInfo.InvariantCulture)
			}
		};

		// Token: 0x02000071 RID: 113
		private enum InputType
		{
			// Token: 0x040000FC RID: 252
			Text,
			// Token: 0x040000FD RID: 253
			Password,
			// Token: 0x040000FE RID: 254
			Hidden
		}
	}
}
