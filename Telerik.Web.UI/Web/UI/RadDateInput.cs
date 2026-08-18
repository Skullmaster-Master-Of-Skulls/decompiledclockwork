using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Design.DatePickerAttributes;
using Telerik.Web.UI.Input.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000FEC RID: 4076
	[Designer("Telerik.Web.Design.DateInputControlDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadDateInput), "Telerik.Web.UI.DateInput.png")]
	[DefaultProperty("Text")]
	[LightweightRendering]
	[Description("Telerik RadInput")]
	[RequiredScript(typeof(RadTextBox))]
	[ClientScriptResource("Telerik.Web.UI.RadDateInput", "Telerik.Web.UI.Input.DateInput.RadDateInputScript.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ControlValueProperty("SelectedDate")]
	[SupportsEventValidation]
	[ToolboxData("<{0}:RadDateInput Runat=server></{0}:RadDateInput>")]
	[TelerikToolboxCategory("Data Editing")]
	[ValidationProperty("Text")]
	[DefaultEvent("TextChanged")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadDateInput : RadInputControl, ICustomTypeDescriptor, IRadDateInput
	{
		// Token: 0x17003257 RID: 12887
		// (get) Token: 0x06009F62 RID: 40802 RVA: 0x002388A2 File Offset: 0x00236AA2
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x17003258 RID: 12888
		// (get) Token: 0x06009F63 RID: 40803 RVA: 0x002388A6 File Offset: 0x00236AA6
		// (set) Token: 0x06009F64 RID: 40804 RVA: 0x002388E5 File Offset: 0x00236AE5
		[Description("Toggles the smart date parsing. Default value is true. When disabled, the developer need to follow the datetime format carefully otherwise the control will display a warning message.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool EnableSmartParsing
		{
			get
			{
				if (this.ViewState["EnableSmartParsing"] == null)
				{
					this.ViewState["EnableSmartParsing"] = true;
				}
				return (bool)this.ViewState["EnableSmartParsing"];
			}
			set
			{
				this.ViewState["EnableSmartParsing"] = value;
			}
		}

		// Token: 0x17003259 RID: 12889
		// (get) Token: 0x06009F65 RID: 40805 RVA: 0x00238900 File Offset: 0x00236B00
		// (set) Token: 0x06009F66 RID: 40806 RVA: 0x0023892F File Offset: 0x00236B2F
		[Category("Behavior")]
		[DefaultValue(2029)]
		[NotifyParentProperty(true)]
		[Description("Indicates the end of the century that is used to interpret the year value when a short year is entered in the input.")]
		public int ShortYearCenturyEnd
		{
			get
			{
				int result = 2029;
				object obj = this.ViewState["ShortYearCenturyEnd"];
				if (obj == null)
				{
					return result;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["ShortYearCenturyEnd"] = value;
			}
		}

		// Token: 0x1700325A RID: 12890
		// (get) Token: 0x06009F67 RID: 40807 RVA: 0x00238947 File Offset: 0x00236B47
		[NotifyParentProperty(true)]
		[DefaultValue(1930)]
		[Category("Behavior")]
		[Description("Indicates the start of the century that is used to interpret the year value when a short year is entered in the input.")]
		public int ShortYearCenturyStart
		{
			get
			{
				return this.ShortYearCenturyEnd - 99;
			}
		}

		// Token: 0x1700325B RID: 12891
		// (get) Token: 0x06009F68 RID: 40808 RVA: 0x00238952 File Offset: 0x00236B52
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public InputIncrementSettings IncrementSettings
		{
			get
			{
				if (this.incrementSettings == null)
				{
					this.incrementSettings = new InputIncrementSettings(this.ViewState);
				}
				return this.incrementSettings;
			}
		}

		// Token: 0x1700325C RID: 12892
		// (get) Token: 0x06009F6A RID: 40810 RVA: 0x002389A8 File Offset: 0x00236BA8
		// (set) Token: 0x06009F69 RID: 40809 RVA: 0x00238973 File Offset: 0x00236B73
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Description("Date and time format used by RadDateInput.")]
		public virtual string DisplayDateFormat
		{
			get
			{
				string text = (string)this.ViewState["DisplayDateFormat"];
				if (text == null)
				{
					text = this.DateFormat;
				}
				return InputUtil.MapDateFormatShortCuts(text, this.Culture.DateTimeFormat);
			}
			set
			{
				if (value != this.DateFormat)
				{
					this.ViewState["DisplayDateFormat"] = value;
					return;
				}
				this.ViewState["DisplayDateFormat"] = null;
			}
		}

		// Token: 0x1700325D RID: 12893
		// (get) Token: 0x06009F6C RID: 40812 RVA: 0x002389FC File Offset: 0x00236BFC
		// (set) Token: 0x06009F6B RID: 40811 RVA: 0x002389E6 File Offset: 0x00236BE6
		[NotifyParentProperty(true)]
		[Description("Date and time format used by RadDateInput.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public virtual string DateFormat
		{
			get
			{
				string text = (string)this.ViewState["DateFormat"];
				if (text == null)
				{
					if (this.Parent is RadMonthYearPicker)
					{
						text = "z";
					}
					else
					{
						text = "d";
					}
				}
				return InputUtil.MapDateFormatShortCuts(text, this.Culture.DateTimeFormat);
			}
			set
			{
				this.ViewState["DateFormat"] = value;
			}
		}

		// Token: 0x1700325E RID: 12894
		// (get) Token: 0x06009F6D RID: 40813 RVA: 0x00238A4E File Offset: 0x00236C4E
		// (set) Token: 0x06009F6E RID: 40814 RVA: 0x00238A7C File Offset: 0x00236C7C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		[Browsable(false)]
		public override string Text
		{
			get
			{
				if (!string.IsNullOrEmpty(this.EmptyMessage) && base.Text == this.EmptyMessage)
				{
					return "";
				}
				return base.Text;
			}
			set
			{
				base.Text = this.RangeTextProperty(value);
			}
		}

		// Token: 0x1700325F RID: 12895
		// (get) Token: 0x06009F6F RID: 40815 RVA: 0x00238A8B File Offset: 0x00236C8B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		public string InvalidTextBoxValue
		{
			get
			{
				if (this.invalidDateStringFlag)
				{
					return this.invalidDateString;
				}
				return "";
			}
		}

		// Token: 0x17003260 RID: 12896
		// (get) Token: 0x06009F70 RID: 40816 RVA: 0x00238AA4 File Offset: 0x00236CA4
		// (set) Token: 0x06009F71 RID: 40817 RVA: 0x00238AEC File Offset: 0x00236CEC
		[Browsable(true)]
		[Bindable(true, BindingDirection.TwoWay)]
		[Description("The currently selected date and time.")]
		[NotifyParentProperty(true)]
		[Editor("System.ComponentModel.Design.DateTimeEditor", "System.Drawing.Design.UITypeEditor")]
		[Category("Behavior")]
		[DefaultValue(typeof(DateTime?), "")]
		public virtual DateTime? SelectedDate
		{
			get
			{
				if (string.IsNullOrEmpty(this.Text) || this.invalidDateStringFlag)
				{
					return null;
				}
				return new DateTime?(DateTime.ParseExact(this.Text, "yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo));
			}
			set
			{
				this.invalidDateStringFlag = false;
				if (value == null)
				{
					this.Text = null;
					return;
				}
				if (!this.SkipMinMaxDateValidationOnServer)
				{
					if (value > this.MaxDate || value < this.MinDate)
					{
						throw new ArgumentOutOfRangeException("SelectedDate", string.Format("Value of '{0}' is not valid for 'SelectedDate'. 'SelectedDate' should be between 'MinDate' and 'MaxDate'.", value));
					}
					this.Text = value.Value.ToString("yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo);
					return;
				}
				else
				{
					if (value > this.MaxDate || value < this.MinDate)
					{
						this.Text = null;
						this._outOfRangeDate = value;
						return;
					}
					this._outOfRangeDate = null;
					this.Text = value.Value.ToString("yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo);
					return;
				}
			}
		}

		// Token: 0x17003261 RID: 12897
		// (get) Token: 0x06009F72 RID: 40818 RVA: 0x00238C2C File Offset: 0x00236E2C
		// (set) Token: 0x06009F73 RID: 40819 RVA: 0x00238C6B File Offset: 0x00236E6B
		[Category("Behavior")]
		[Description("Switched on or off the server-side min/max date validation.")]
		[DefaultValue(false)]
		public bool SkipMinMaxDateValidationOnServer
		{
			get
			{
				if (this.ViewState["_skipMMValidation"] == null)
				{
					this.ViewState["_skipMMValidation"] = false;
				}
				return (bool)this.ViewState["_skipMMValidation"];
			}
			set
			{
				this.ViewState["_skipMMValidation"] = value;
			}
		}

		// Token: 0x17003262 RID: 12898
		// (get) Token: 0x06009F74 RID: 40820 RVA: 0x00238C83 File Offset: 0x00236E83
		// (set) Token: 0x06009F75 RID: 40821 RVA: 0x00238C90 File Offset: 0x00236E90
		[DatePickerBrowsable(false)]
		[Description("The currently selected date, boxed in object")]
		[Browsable(false)]
		[NotifyParentProperty(true)]
		[Bindable(true, BindingDirection.TwoWay)]
		public object DbSelectedDate
		{
			get
			{
				return this.SelectedDate;
			}
			set
			{
				string text = value as string;
				if (text != null)
				{
					if (string.IsNullOrEmpty(text))
					{
						this.SelectedDate = null;
						return;
					}
					DateTime dateTime;
					bool flag = DateTime.TryParseExact(text, this.DateFormat, this.Culture, DateTimeStyles.None, out dateTime);
					if (!flag)
					{
						flag = DateTime.TryParseExact(text, this.DisplayDateFormat, this.Culture, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParseExact(text, this.DateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParseExact(text, this.DisplayDateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParse(text, this.Culture, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParse(text, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						flag = DateTime.TryParseExact(text, "yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime);
					}
					if (!flag)
					{
						throw new FormatException("The string was not recognized as a valid DateTime.");
					}
					this.SelectedDate = new DateTime?(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, DateTimeKind.Unspecified));
					return;
				}
				else
				{
					if (value == DBNull.Value)
					{
						this.SelectedDate = null;
						return;
					}
					this.SelectedDate = (DateTime?)value;
					return;
				}
			}
		}

		// Token: 0x17003263 RID: 12899
		// (get) Token: 0x06009F76 RID: 40822 RVA: 0x00238DD8 File Offset: 0x00236FD8
		// (set) Token: 0x06009F77 RID: 40823 RVA: 0x00238E0C File Offset: 0x0023700C
		[Category("Behavior")]
		[Description("Culture used by RadDateInput to format the date.")]
		[DatePickerBrowsable(false)]
		[NotifyParentProperty(true)]
		public CultureInfo Culture
		{
			get
			{
				if (this.ViewState["Culture"] == null)
				{
					return Thread.CurrentThread.CurrentCulture;
				}
				return (CultureInfo)this.ViewState["Culture"];
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17003264 RID: 12900
		// (get) Token: 0x06009F78 RID: 40824 RVA: 0x00238E1F File Offset: 0x0023701F
		// (set) Token: 0x06009F79 RID: 40825 RVA: 0x00238E55 File Offset: 0x00237055
		[DatePickerBrowsable(false)]
		[NotifyParentProperty(true)]
		[Description("The smallest date allowed by RadDateInput.")]
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		[Category("Behavior")]
		[ClientControlProperty]
		public DateTime MinDate
		{
			get
			{
				if (this.ViewState["MinDate"] == null)
				{
					return new DateTime(1980, 1, 1);
				}
				return (DateTime)this.ViewState["MinDate"];
			}
			set
			{
				this.ViewState["MinDate"] = value;
				if (!this.invalidDateStringFlag)
				{
					this.Text = this.RangeTextProperty(this.Text);
				}
			}
		}

		// Token: 0x17003265 RID: 12901
		// (get) Token: 0x06009F7A RID: 40826 RVA: 0x00238E87 File Offset: 0x00237087
		// (set) Token: 0x06009F7B RID: 40827 RVA: 0x00238EBF File Offset: 0x002370BF
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(typeof(DateTime), "12/31/2099")]
		[Description("The largest date allowed by RadDateInput.")]
		[DatePickerBrowsable(false)]
		[ClientControlProperty]
		public DateTime MaxDate
		{
			get
			{
				if (this.ViewState["MaxDate"] == null)
				{
					return new DateTime(2099, 12, 31);
				}
				return (DateTime)this.ViewState["MaxDate"];
			}
			set
			{
				this.ViewState["MaxDate"] = value;
				if (!this.invalidDateStringFlag)
				{
					this.Text = this.RangeTextProperty(this.Text);
				}
			}
		}

		// Token: 0x17003266 RID: 12902
		// (get) Token: 0x06009F7C RID: 40828 RVA: 0x00238EF1 File Offset: 0x002370F1
		// (set) Token: 0x06009F7D RID: 40829 RVA: 0x00238F1C File Offset: 0x0023711C
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(SelectionOnFocus), "SelectAll")]
		[Themeable(true)]
		[Category("Behavior")]
		public override SelectionOnFocus SelectionOnFocus
		{
			get
			{
				if (this.ViewState["SelectionOnFocus"] == null)
				{
					return SelectionOnFocus.SelectAll;
				}
				return (SelectionOnFocus)this.ViewState["SelectionOnFocus"];
			}
			set
			{
				this.ViewState["SelectionOnFocus"] = value;
			}
		}

		// Token: 0x17003267 RID: 12903
		// (get) Token: 0x06009F7E RID: 40830 RVA: 0x00238F34 File Offset: 0x00237134
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.Text) || this.invalidDateStringFlag;
			}
		}

		// Token: 0x06009F7F RID: 40831 RVA: 0x00238F4C File Offset: 0x0023714C
		protected override void SetDesignTimeAttributes(HtmlTextWriter writer)
		{
			if (this.SelectedDate != null)
			{
				base.Attributes[HtmlTextWriterAttribute.Value.ToString().ToLower()] = this.SelectedDate.Value.ToString(this.DisplayDateFormat);
			}
			base.SetDesignTimeAttributes(writer);
		}

		// Token: 0x06009F80 RID: 40832 RVA: 0x00238FA8 File Offset: 0x002371A8
		public virtual void Clear()
		{
			this.invalidDateStringFlag = false;
			this.Text = "";
		}

		// Token: 0x06009F81 RID: 40833 RVA: 0x00238FBC File Offset: 0x002371BC
		protected virtual string RangeTextProperty(string value)
		{
			string result = null;
			if (!string.IsNullOrEmpty(value))
			{
				DateTime dateTime;
				try
				{
					dateTime = DateTime.ParseExact(value, "yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo);
				}
				catch (Exception ex)
				{
					throw new InvalidCastException("Text property cannot be set. " + ex.Message);
				}
				if (!this.SkipMinMaxDateValidationOnServer)
				{
					dateTime = ((this.MaxDate < dateTime) ? this.MaxDate : dateTime);
					dateTime = ((this.MinDate > dateTime) ? this.MinDate : dateTime);
				}
				result = dateTime.ToString("yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo);
			}
			return result;
		}

		// Token: 0x06009F82 RID: 40834 RVA: 0x00239058 File Offset: 0x00237258
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			if (this.MaxDate < this.MinDate)
			{
				throw new ArgumentOutOfRangeException("MinDate", "MinDate need to be less than MaxDate");
			}
			if (this.ViewState["OriginalValue"] == null && !this.EnableSingleInputRendering)
			{
				this.ViewState["OriginalValue"] = ((this.SelectedDate != null) ? this.SelectedDate.Value.ToString() : "");
			}
		}

		// Token: 0x17003268 RID: 12904
		// (get) Token: 0x06009F83 RID: 40835 RVA: 0x002390EC File Offset: 0x002372EC
		// (set) Token: 0x06009F84 RID: 40836 RVA: 0x00239186 File Offset: 0x00237386
		public override string DisplayText
		{
			get
			{
				if (!string.IsNullOrEmpty(this._displayText))
				{
					return this._displayText;
				}
				CultureInfo cultureInfo = new CultureInfo(this.Culture.Name);
				cultureInfo.DateTimeFormat.Calendar = new GregorianCalendar();
				string text = "";
				if (this.SelectedDate != null)
				{
					text = this.SelectedDate.Value.ToString(this.DisplayDateFormat, cultureInfo);
				}
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				if (!string.IsNullOrEmpty(this.EmptyMessage))
				{
					return this.EmptyMessage;
				}
				return "";
			}
			set
			{
				this._displayText = value;
			}
		}

		// Token: 0x06009F85 RID: 40837 RVA: 0x00239190 File Offset: 0x00237390
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new DateTimeFormatInfoConverter()
			});
			descriptor.AddScriptProperty("dateFormatInfo", javaScriptSerializer.Serialize(this));
			descriptor.AddScriptProperty("incrementSettings", InputUtil.IncrementSettingsToClient(this.IncrementSettings));
			if (!this.EnableSingleInputRendering)
			{
				descriptor.AddProperty("_originalValue", this.ViewState["OriginalValue"]);
			}
			if (!this.EnableSmartParsing)
			{
				descriptor.AddProperty("_enableSmartParsing", this.EnableSmartParsing);
			}
		}

		// Token: 0x06009F86 RID: 40838 RVA: 0x0023922C File Offset: 0x0023742C
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			if (clientState.ContainsKey("minDateStr") && clientState.ContainsKey("maxDateStr"))
			{
				DateTime dateTime;
				if (DateTime.TryParseExact((string)clientState["minDateStr"], "yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
				{
					this.MinDate = dateTime;
				}
				if (DateTime.TryParseExact((string)clientState["maxDateStr"], "yyyy-MM-dd-HH-mm-ss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
				{
					this.MaxDate = dateTime;
				}
			}
		}

		// Token: 0x06009F87 RID: 40839 RVA: 0x002392B0 File Offset: 0x002374B0
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool flag = false;
			bool result = true;
			try
			{
				result = base.LoadPostData(postDataKey, postCollection);
			}
			catch (ArgumentOutOfRangeException)
			{
				flag = true;
			}
			catch (InvalidCastException)
			{
				flag = true;
			}
			string text = postCollection[postDataKey];
			if (flag)
			{
				this.invalidDateStringFlag = true;
				this.invalidDateString = text;
				this.Text = "";
			}
			if (string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(text) && text != this._displayText && text != this.EmptyMessage)
			{
				this.invalidDateStringFlag = true;
				this.invalidDateString = text;
			}
			return result;
		}

		// Token: 0x17003269 RID: 12905
		// (get) Token: 0x06009F88 RID: 40840 RVA: 0x00239358 File Offset: 0x00237558
		// (set) Token: 0x06009F89 RID: 40841 RVA: 0x00239365 File Offset: 0x00237565
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("Client date changed")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Obsolete("Use the ClientEvents.OnValueChanged property instead.", false)]
		public string OnClientDateChanged
		{
			get
			{
				return base.ClientEvents.OnValueChanged;
			}
			set
			{
				base.ClientEvents.OnValueChanged = value;
			}
		}

		// Token: 0x06009F8A RID: 40842 RVA: 0x00239373 File Offset: 0x00237573
		public System.ComponentModel.AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06009F8B RID: 40843 RVA: 0x0023937C File Offset: 0x0023757C
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06009F8C RID: 40844 RVA: 0x00239385 File Offset: 0x00237585
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06009F8D RID: 40845 RVA: 0x0023938E File Offset: 0x0023758E
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06009F8E RID: 40846 RVA: 0x00239397 File Offset: 0x00237597
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06009F8F RID: 40847 RVA: 0x002393A0 File Offset: 0x002375A0
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06009F90 RID: 40848 RVA: 0x002393A9 File Offset: 0x002375A9
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06009F91 RID: 40849 RVA: 0x002393B3 File Offset: 0x002375B3
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06009F92 RID: 40850 RVA: 0x002393BC File Offset: 0x002375BC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06009F93 RID: 40851 RVA: 0x002393C6 File Offset: 0x002375C6
		public virtual PropertyDescriptorCollection GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}

		// Token: 0x06009F94 RID: 40852 RVA: 0x002393CF File Offset: 0x002375CF
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(this, attributes, true);
		}

		// Token: 0x06009F95 RID: 40853 RVA: 0x002393D9 File Offset: 0x002375D9
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x06009F96 RID: 40854 RVA: 0x002393DC File Offset: 0x002375DC
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "dateFormat", this.DateFormat, null);
			base.DescribeProperty<string>(descriptor, "displayDateFormat", this.DisplayDateFormat, null);
			base.DescribeProperty<string>(descriptor, "maxDate", this.MaxDate.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture), DateTime.Parse("12/31/2099", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture));
			base.DescribeProperty<string>(descriptor, "minDate", this.MinDate.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture), DateTime.Parse("1/1/1980", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture));
			base.DescribeProperty<object>(descriptor, "selectionOnFocus", this.SelectionOnFocus, Enum.Parse(typeof(SelectionOnFocus), "SelectAll"));
			if (this._outOfRangeDate != null)
			{
				descriptor.AddProperty("outOfRangeDate", this._outOfRangeDate);
			}
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06009F97 RID: 40855 RVA: 0x002394EE File Offset: 0x002376EE
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002C95 RID: 11413
		private const string hiddenFormat = "yyyy-MM-dd-HH-mm-ss";

		// Token: 0x04002C96 RID: 11414
		private InputIncrementSettings incrementSettings;

		// Token: 0x04002C97 RID: 11415
		protected bool invalidDateStringFlag;

		// Token: 0x04002C98 RID: 11416
		protected string invalidDateString;

		// Token: 0x04002C99 RID: 11417
		private DateTime? _outOfRangeDate = null;

		// Token: 0x04002C9A RID: 11418
		private string _displayText = string.Empty;
	}
}
