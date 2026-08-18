using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Input.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001911 RID: 6417
	[RequiredScript(typeof(RadNumericTextBox))]
	[Description("Telerik RadInput")]
	[LightweightRendering]
	[ClientScriptResource("Telerik.Web.UI.RadInputManager", "Telerik.Web.UI.Calendar.RadPickersPopupDirectionEnumeration.js")]
	[ClientScriptResource("Telerik.Web.UI.RadInputManager", "Telerik.Web.UI.Input.InputManager.RadInputManagerScript.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Input", typeof(RadInputManager))]
	[EmbeddedSkin("Input", "Default", typeof(RadInputManager))]
	[RequiredScript(typeof(RadTextBox))]
	[RequiredScript(typeof(RadDateInput))]
	[RequiredScript(typeof(RadMaskedTextBox))]
	[RequiredScript(typeof(Core))]
	[ParseChildren(true, "InputSettings")]
	[ToolboxData("<{0}:RadInputManager runat=\"server\"></{0}:RadInputManager>")]
	[Designer("Telerik.Web.Design.InputManagerControlDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(RadInputManager), "Telerik.Web.UI.InputManager.png")]
	[TelerikToolboxCategory("Data Editing")]
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadInputManager : InputManagerBase
	{
		// Token: 0x0600F900 RID: 63744 RVA: 0x003835E8 File Offset: 0x003817E8
		protected virtual void SetStyleClasses(InputSetting inputSetting)
		{
			if (!string.IsNullOrEmpty(base.RuntimeSkin))
			{
				inputSetting.EmptyMessageCssClass = this.FormatCssClass("RadInput_Empty", inputSetting.EmptyMessageCssClass);
				inputSetting.EnabledCssClass = this.FormatCssClass("RadInput_Enabled", inputSetting.EnabledCssClass);
				inputSetting.FocusedCssClass = this.FormatCssClass("RadInput_Focused", inputSetting.FocusedCssClass);
				inputSetting.HoveredCssClass = this.FormatCssClass("RadInput_Hover", inputSetting.HoveredCssClass);
				inputSetting.InvalidCssClass = this.FormatCssClass("RadInput_Error", inputSetting.InvalidCssClass);
				inputSetting.ReadOnlyCssClass = this.FormatCssClass("RadInput_Read", inputSetting.ReadOnlyCssClass);
				inputSetting.DisabledCssClass = this.FormatCssClass("RadInput_Disabled", inputSetting.DisabledCssClass);
			}
		}

		// Token: 0x0600F901 RID: 63745 RVA: 0x003836A6 File Offset: 0x003818A6
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.RegisterCustomValidators();
		}

		// Token: 0x0600F902 RID: 63746 RVA: 0x003836B8 File Offset: 0x003818B8
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			foreach (object obj in this.InputSettings)
			{
				InputSetting inputSetting = (InputSetting)obj;
				this.SetStyleClasses(inputSetting);
				NumericTextBoxSetting numericTextBoxSetting = inputSetting as NumericTextBoxSetting;
				if (numericTextBoxSetting != null && !string.IsNullOrEmpty(base.RuntimeSkin) && numericTextBoxSetting.NegativeCssClass.IndexOf("RadInputMgr") == -1)
				{
					numericTextBoxSetting.NegativeCssClass = this.FormatCssClass("RadInput_Negative", numericTextBoxSetting.NegativeCssClass);
				}
			}
			this.PopulateInputCollection();
		}

		// Token: 0x17004B3E RID: 19262
		// (get) Token: 0x0600F903 RID: 63747 RVA: 0x00383760 File Offset: 0x00381960
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600F904 RID: 63748 RVA: 0x00383764 File Offset: 0x00381964
		private void PopulateInputCollection()
		{
			foreach (object obj in this.InputSettings)
			{
				InputSetting inputSetting = (InputSetting)obj;
				foreach (object obj2 in inputSetting.TargetControls)
				{
					TargetInput targetInput = (TargetInput)obj2;
					Control control = ChildControlHelper.FindControlRecursive(this, targetInput.ControlID, null);
					if (control != null)
					{
						if (control is TextBox && control.Visible)
						{
							this.AddInputControl(control, targetInput, inputSetting);
						}
						else if (control.Visible)
						{
							List<Control> allControls = ChildControlHelper.GetAllControls(new List<Control>(), typeof(TextBox), control);
							foreach (Control control2 in allControls)
							{
								if (control2 is TextBox)
								{
									this.AddInputControl(control2, targetInput, inputSetting);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600F905 RID: 63749 RVA: 0x003838A8 File Offset: 0x00381AA8
		private void AddInputControl(Control control, TargetInput inputControlItem, InputSetting setting)
		{
			TextBox textBox = control as TextBox;
			InputSettingCreatingEventArgs inputSettingCreatingEventArgs = new InputSettingCreatingEventArgs(textBox, inputControlItem, setting);
			this.OnInputSettingCreating(inputSettingCreatingEventArgs);
			if (inputSettingCreatingEventArgs.Canceled)
			{
				return;
			}
			if (inputControlItem.Enabled && this.Enabled)
			{
				this.SetClientAttributes(textBox, setting);
				TargetInput item = new TargetInput(control.ClientID, inputControlItem.Enabled);
				setting._internalTargetControls.Add(item);
			}
		}

		// Token: 0x0600F906 RID: 63750 RVA: 0x0038390C File Offset: 0x00381B0C
		private void SetClientAttributes(TextBox textBox, InputSetting setting)
		{
			if (setting.InitializeOnClient)
			{
				return;
			}
			setting.UpdateCssClass(textBox);
			setting.UpdateValue(textBox, true);
			bool attachKeyUp = false;
			bool attachKeyDown = false;
			TextBoxSetting textBoxSetting = setting as TextBoxSetting;
			if (textBoxSetting != null && textBoxSetting.PasswordStrengthSettings.ShowIndicator)
			{
				attachKeyUp = true;
			}
			if (setting is MaskedTextBoxSetting)
			{
				attachKeyDown = true;
			}
			if (!string.IsNullOrEmpty(setting.ClientEvents.OnValueChanged))
			{
				this.AddAttribute(textBox, "onchange", "$radIE.change(event);");
			}
			this.SetClientEvents(textBox, attachKeyUp, attachKeyDown);
		}

		// Token: 0x0600F907 RID: 63751 RVA: 0x00383984 File Offset: 0x00381B84
		private void SetClientEvents(TextBox textBox, bool attachKeyUp, bool attachKeyDown)
		{
			this.AddAttribute(textBox, "onmouseover", "$radIE.mouseOver(event);");
			this.AddAttribute(textBox, "onmouseout", "$radIE.mouseOut(event);");
			this.AddAttribute(textBox, "onkeypress", "$radIE.keyPress(event);");
			this.AddAttribute(textBox, "onblur", "$radIE.blur(event);");
			this.AddAttribute(textBox, "onfocus", "$radIE.focus(event);");
			if (attachKeyUp)
			{
				this.AddAttribute(textBox, "onkeyup", "$radIE.keyUp(event);");
			}
			if (attachKeyDown)
			{
				this.AddAttribute(textBox, "onkeydown", "$radIE.keyDown(event);");
			}
			if (this.Context.Request.Browser.IsBrowser("Safari") || this.Context.Request.Browser.IsBrowser("Chrome"))
			{
				this.AddAttribute(textBox, "onmouseup", "$radIE.mouseUp(event);");
			}
		}

		// Token: 0x0600F908 RID: 63752 RVA: 0x00383A58 File Offset: 0x00381C58
		private void AddAttribute(TextBox textBox, string key, string value)
		{
			string text = null;
			if (textBox.HasAttributes)
			{
				text = textBox.Attributes[key];
				if (text != null)
				{
					text = InputUtil.EnsureEndWithSemiColon(text);
					textBox.Attributes.Remove(key);
				}
			}
			text = InputUtil.MergeScript(text, value);
			textBox.Attributes[key] = text;
		}

		// Token: 0x0600F909 RID: 63753 RVA: 0x00383AA8 File Offset: 0x00381CA8
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>();
			foreach (ScriptReference item in scriptReferences)
			{
				list.Add(item);
			}
			bool flag = true;
			RadScriptManager radScriptManager = ScriptManager.GetCurrent(this.Page) as RadScriptManager;
			if (radScriptManager != null)
			{
				flag = radScriptManager.EnableEmbeddedjQuery;
			}
			if (this.HasLocationAndMethod() && flag)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.jQuery.js", Assembly.GetExecutingAssembly().FullName));
				list.Add(new ScriptReference("Telerik.Web.UI.Common.jQueryPlugins.js", Assembly.GetExecutingAssembly().FullName));
			}
			return list;
		}

		// Token: 0x0600F90A RID: 63754 RVA: 0x00383B64 File Offset: 0x00381D64
		private bool HasLocationAndMethod()
		{
			bool result = false;
			foreach (object obj in this.InputSettings)
			{
				InputSetting inputSetting = (InputSetting)obj;
				if (!string.IsNullOrEmpty(inputSetting.Validation.Location) && !string.IsNullOrEmpty(inputSetting.Validation.Method))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600F90B RID: 63755 RVA: 0x00383BE4 File Offset: 0x00381DE4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			List<ScriptDescriptor> list = new List<ScriptDescriptor>(base.GetScriptDescriptors());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			foreach (object obj in this.InputSettings)
			{
				InputSetting inputSetting = (InputSetting)obj;
				if (inputSetting is DateInputSetting)
				{
					this.DescribeDateInputComponents(inputSetting._internalTargetControls, list, (DateInputSetting)inputSetting);
				}
				if (inputSetting is TextBoxSetting)
				{
					this.DescribeTextBoxComponents(inputSetting._internalTargetControls, list, (TextBoxSetting)inputSetting);
				}
				if (inputSetting is NumericTextBoxSetting)
				{
					this.DescribeNumericTextBoxComponents(inputSetting._internalTargetControls, list, (NumericTextBoxSetting)inputSetting);
				}
				if (inputSetting is RegExpTextBoxSetting)
				{
					this.DescribeRegExpTextBoxComponents(inputSetting._internalTargetControls, list, (RegExpTextBoxSetting)inputSetting);
				}
				if (inputSetting is MaskedTextBoxSetting)
				{
					this.DescribeMaskedTextBoxComponents(inputSetting._internalTargetControls, list, (MaskedTextBoxSetting)inputSetting);
				}
				stringBuilder.AppendFormat("\"{0}\",", this.GetSettingClientID(inputSetting));
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append("]");
			if (this.InputSettings.Count != 0)
			{
				((ScriptControlDescriptor)list[0]).AddScriptProperty("behaviors", stringBuilder.ToString());
			}
			return list;
		}

		// Token: 0x0600F90C RID: 63756 RVA: 0x00383D40 File Offset: 0x00381F40
		private void DescribeDateInputComponents(List<TargetInput> targetInputs, List<ScriptDescriptor> descriptors, DateInputSetting dateInputSetting)
		{
			RadComponentScriptDescriptor radComponentScriptDescriptor = (dateInputSetting is DatePickerSetting) ? new RadComponentScriptDescriptor("Telerik.Web.UI.RadDatePickerComponent") : new RadComponentScriptDescriptor("Telerik.Web.UI.RadDateInputComponent");
			string str = string.Format("pattern:\"{0}\",AMDesignator:\"{1}\",PMDesignator:\"{2}\"", CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern, CultureInfo.CurrentCulture.DateTimeFormat.AMDesignator, CultureInfo.CurrentCulture.DateTimeFormat.PMDesignator);
			radComponentScriptDescriptor.AddScriptProperty("_currentCultureFormat", "{" + str + "}");
			radComponentScriptDescriptor.AddProperty("id", this.GetSettingClientID(dateInputSetting));
			radComponentScriptDescriptor.AddScriptProperty("targetControlIDs", this.DescribeIDs(targetInputs));
			radComponentScriptDescriptor.ID = this.GetSettingClientID(dateInputSetting);
			dateInputSetting.Describe(radComponentScriptDescriptor);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new DateTimeFormatInfoConverter()
			});
			radComponentScriptDescriptor.AddScriptProperty("dateFormatInfo", javaScriptSerializer.Serialize(dateInputSetting));
			descriptors.Add(radComponentScriptDescriptor);
		}

		// Token: 0x0600F90D RID: 63757 RVA: 0x00383E48 File Offset: 0x00382048
		private void DescribeTextBoxComponents(List<TargetInput> targetInputs, List<ScriptDescriptor> descriptors, TextBoxSetting textBoxSetting)
		{
			RadComponentScriptDescriptor radComponentScriptDescriptor = new RadComponentScriptDescriptor("Telerik.Web.UI.RadTextBoxComponent");
			radComponentScriptDescriptor.AddProperty("id", this.GetSettingClientID(textBoxSetting));
			radComponentScriptDescriptor.AddScriptProperty("targetControlIDs", this.DescribeIDs(targetInputs));
			radComponentScriptDescriptor.ID = this.GetSettingClientID(textBoxSetting);
			textBoxSetting.Describe(radComponentScriptDescriptor);
			descriptors.Add(radComponentScriptDescriptor);
		}

		// Token: 0x0600F90E RID: 63758 RVA: 0x00383EA0 File Offset: 0x003820A0
		private void DescribeNumericTextBoxComponents(List<TargetInput> targetInputs, List<ScriptDescriptor> descriptors, NumericTextBoxSetting numericTextBoxSetting)
		{
			RadComponentScriptDescriptor radComponentScriptDescriptor = new RadComponentScriptDescriptor("Telerik.Web.UI.RadNumericTextBoxComponent");
			radComponentScriptDescriptor.AddProperty("id", this.GetSettingClientID(numericTextBoxSetting));
			radComponentScriptDescriptor.AddScriptProperty("targetControlIDs", this.DescribeIDs(targetInputs));
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new NumberFormatSettingsConverter()
			});
			radComponentScriptDescriptor.AddScriptProperty("numberFormat", javaScriptSerializer.Serialize(numericTextBoxSetting.NumberFormat));
			string str;
			if (numericTextBoxSetting.Culture != CultureInfo.CurrentCulture)
			{
				str = string.Format("DecimalSeparator:\"{0}\",NegativeSign:\"{1}\"", numericTextBoxSetting.Culture.NumberFormat.NumberDecimalSeparator, numericTextBoxSetting.Culture.NumberFormat.NegativeSign);
			}
			else
			{
				str = string.Format("DecimalSeparator:\"{0}\",NegativeSign:\"{1}\"", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, CultureInfo.CurrentCulture.NumberFormat.NegativeSign);
			}
			radComponentScriptDescriptor.AddScriptProperty("_currentNumberFormat", "{" + str + "}");
			radComponentScriptDescriptor.ID = this.GetSettingClientID(numericTextBoxSetting);
			numericTextBoxSetting.Describe(radComponentScriptDescriptor);
			descriptors.Add(radComponentScriptDescriptor);
		}

		// Token: 0x0600F90F RID: 63759 RVA: 0x00383FA8 File Offset: 0x003821A8
		private string GetSettingClientID(InputSetting setting)
		{
			string arg = this.InputSettings.IndexOf(setting).ToString();
			if (!string.IsNullOrEmpty(setting.BehaviorID))
			{
				arg = setting.BehaviorID;
			}
			return string.Format("{0}_{1}", this.ClientID, arg);
		}

		// Token: 0x0600F910 RID: 63760 RVA: 0x00383FF0 File Offset: 0x003821F0
		private void DescribeMaskedTextBoxComponents(List<TargetInput> targetInputs, List<ScriptDescriptor> descriptors, MaskedTextBoxSetting maskedTextBoxSetting)
		{
			RadComponentScriptDescriptor radComponentScriptDescriptor = new RadComponentScriptDescriptor("Telerik.Web.UI.RadMaskedTextBoxComponent");
			radComponentScriptDescriptor.AddProperty("id", this.GetSettingClientID(maskedTextBoxSetting));
			radComponentScriptDescriptor.AddScriptProperty("targetControlIDs", this.DescribeIDs(targetInputs));
			radComponentScriptDescriptor.ID = this.GetSettingClientID(maskedTextBoxSetting);
			maskedTextBoxSetting.Describe(radComponentScriptDescriptor);
			descriptors.Add(radComponentScriptDescriptor);
		}

		// Token: 0x0600F911 RID: 63761 RVA: 0x00384048 File Offset: 0x00382248
		private void DescribeRegExpTextBoxComponents(List<TargetInput> targetInputs, List<ScriptDescriptor> descriptors, RegExpTextBoxSetting regExpTextBoxSetting)
		{
			RadComponentScriptDescriptor radComponentScriptDescriptor = new RadComponentScriptDescriptor("Telerik.Web.UI.RadRegExpTextBoxComponent");
			radComponentScriptDescriptor.AddProperty("id", this.GetSettingClientID(regExpTextBoxSetting));
			radComponentScriptDescriptor.AddScriptProperty("targetControlIDs", this.DescribeIDs(targetInputs));
			radComponentScriptDescriptor.ID = this.GetSettingClientID(regExpTextBoxSetting);
			regExpTextBoxSetting.Describe(radComponentScriptDescriptor);
			descriptors.Add(radComponentScriptDescriptor);
		}

		// Token: 0x0600F912 RID: 63762 RVA: 0x003840A0 File Offset: 0x003822A0
		protected virtual void RegisterCustomValidators()
		{
			foreach (object obj in this.InputSettings)
			{
				InputSetting inputSetting = (InputSetting)obj;
				InputSettingCustomValidator inputSettingCustomValidator = new InputSettingCustomValidator(this, inputSetting);
				string id = this.InputSettings.IndexOf(inputSetting).ToString();
				if (!string.IsNullOrEmpty(inputSetting.BehaviorID))
				{
					id = inputSetting.BehaviorID;
				}
				inputSettingCustomValidator.ID = id;
				inputSettingCustomValidator.ValidateEmptyText = inputSetting.Validation.IsRequired;
				inputSettingCustomValidator.ClientValidationFunction = "Telerik.Web.UI.RadInputManager.ClientValidationFunction";
				inputSettingCustomValidator.Display = ValidatorDisplay.None;
				inputSettingCustomValidator.ValidationGroup = inputSetting.Validation.ValidationGroup;
				inputSettingCustomValidator.ErrorMessage = inputSetting.ErrorMessage;
				this.Controls.Add(inputSettingCustomValidator);
				DatePickerSetting datePickerSetting = inputSetting as DatePickerSetting;
				if (datePickerSetting != null)
				{
					RadCalendar radCalendar = null;
					if (!string.IsNullOrEmpty(datePickerSetting.SharedCalendarID))
					{
						radCalendar = (this.NamingContainer.FindControl(datePickerSetting.SharedCalendarID) as RadCalendar);
						if (radCalendar == null)
						{
							radCalendar = (this.Page.FindControl(datePickerSetting.SharedCalendarID) as RadCalendar);
						}
					}
					if (radCalendar == null)
					{
						radCalendar = new DatePickingCalendar();
						radCalendar.PreRender += this.SharedCalendar_PreRender;
						this.Controls.Add(radCalendar);
						this.RenderAsDiv = true;
					}
					radCalendar.RenderInvisible = true;
					datePickerSetting.SharedCalendar = radCalendar;
				}
			}
		}

		// Token: 0x0600F913 RID: 63763 RVA: 0x00384224 File Offset: 0x00382424
		private void SharedCalendar_PreRender(object sender, EventArgs e)
		{
			RadCalendar radCalendar = sender as RadCalendar;
			if (radCalendar != null)
			{
				radCalendar.RenderMode = this.ResolvedRenderMode;
				radCalendar.Skin = base.RuntimeSkin;
			}
		}

		// Token: 0x0600F914 RID: 63764 RVA: 0x00384254 File Offset: 0x00382454
		private string DescribeIDs(List<TargetInput> targetInputs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			foreach (TargetInput targetInput in targetInputs)
			{
				stringBuilder.AppendFormat("\"{0}\",", targetInput.ControlID);
			}
			if (targetInputs.Count > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600F915 RID: 63765 RVA: 0x003842EC File Offset: 0x003824EC
		protected override void RegisterScriptControl()
		{
			if (this.RegisterWithScriptManager)
			{
				base.ScriptManager.RegisterScriptControl<RadControl>(this);
				return;
			}
			this.EnsureChildControls();
			ControlRenderer.EnsureChildControlsAreNotRegistered(this);
		}

		// Token: 0x140001D1 RID: 465
		// (add) Token: 0x0600F916 RID: 63766 RVA: 0x00384310 File Offset: 0x00382510
		// (remove) Token: 0x0600F917 RID: 63767 RVA: 0x00384348 File Offset: 0x00382548
		[Category("Action")]
		public event RadInputManager.InputSettingCreatingDelegate InputSettingCreating;

		// Token: 0x0600F918 RID: 63768 RVA: 0x0038437D File Offset: 0x0038257D
		protected virtual void OnInputSettingCreating(InputSettingCreatingEventArgs args)
		{
			if (this.InputSettingCreating != null)
			{
				this.InputSettingCreating(this, args);
			}
		}

		// Token: 0x0600F919 RID: 63769 RVA: 0x00384394 File Offset: 0x00382594
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				((IStateManager)this.InputSettings).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600F91A RID: 63770 RVA: 0x003843C4 File Offset: 0x003825C4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				(this.settings != null) ? ((IStateManager)this.settings).SaveViewState() : null
			};
		}

		// Token: 0x0600F91B RID: 63771 RVA: 0x003843FB File Offset: 0x003825FB
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.settings != null)
			{
				((IStateManager)this.settings).TrackViewState();
			}
		}

		// Token: 0x140001D2 RID: 466
		// (add) Token: 0x0600F91C RID: 63772 RVA: 0x00384418 File Offset: 0x00382618
		// (remove) Token: 0x0600F91D RID: 63773 RVA: 0x00384450 File Offset: 0x00382650
		[Category("Action")]
		public event RadInputManager.ValidatingDelegate Validating;

		// Token: 0x0600F91E RID: 63774 RVA: 0x00384485 File Offset: 0x00382685
		public virtual void OnValidating(InputManagerValidatingEventArgs args)
		{
			if (this.Validating != null)
			{
				this.Validating(this, args);
			}
		}

		// Token: 0x140001D3 RID: 467
		// (add) Token: 0x0600F91F RID: 63775 RVA: 0x0038449C File Offset: 0x0038269C
		// (remove) Token: 0x0600F920 RID: 63776 RVA: 0x003844D4 File Offset: 0x003826D4
		[Category("Action")]
		public event RadInputManager.ValidatedDelegate Validated;

		// Token: 0x0600F921 RID: 63777 RVA: 0x00384509 File Offset: 0x00382709
		public virtual void OnValidated(InputManagerValidatedEventArgs args)
		{
			if (this.Validated != null)
			{
				this.Validated(this, args);
			}
		}

		// Token: 0x17004B3F RID: 19263
		// (get) Token: 0x0600F922 RID: 63778 RVA: 0x00384520 File Offset: 0x00382720
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("A collection of InputSettings ")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Editor("Telerik.Web.Design.InputManagerSettingsTypeEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Behavior")]
		public InputSettingsCollection InputSettings
		{
			get
			{
				if (this.settings == null)
				{
					this.settings = new InputSettingsCollection();
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this.settings).TrackViewState();
				}
				return this.settings;
			}
		}

		// Token: 0x17004B40 RID: 19264
		// (get) Token: 0x0600F923 RID: 63779 RVA: 0x0038454E File Offset: 0x0038274E
		// (set) Token: 0x0600F924 RID: 63780 RVA: 0x00384579 File Offset: 0x00382779
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating the control should be enabled or not.")]
		[Category("Behavior")]
		public virtual bool Enabled
		{
			get
			{
				return this.ViewState["Enabled"] == null || (bool)this.ViewState["Enabled"];
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x0600F925 RID: 63781 RVA: 0x00384594 File Offset: 0x00382794
		public InputSetting GetSettingByBehaviorID(string behaviorID)
		{
			foreach (object obj in this.InputSettings)
			{
				InputSetting inputSetting = (InputSetting)obj;
				if (inputSetting.BehaviorID == behaviorID)
				{
					return inputSetting;
				}
			}
			return null;
		}

		// Token: 0x0600F926 RID: 63782 RVA: 0x003845FC File Offset: 0x003827FC
		public List<InputSetting> GetSettingsByType(Type type)
		{
			List<InputSetting> list = null;
			foreach (object obj in this.InputSettings)
			{
				InputSetting inputSetting = (InputSetting)obj;
				if (inputSetting.GetType() == type)
				{
					if (list == null)
					{
						list = new List<InputSetting>();
					}
					list.Add(inputSetting);
				}
			}
			return list;
		}

		// Token: 0x040046D9 RID: 18137
		private InputSettingsCollection settings;

		// Token: 0x02001912 RID: 6418
		// (Invoke) Token: 0x0600F929 RID: 63785
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public delegate void InputSettingCreatingDelegate(object sender, InputSettingCreatingEventArgs e);

		// Token: 0x02001913 RID: 6419
		// (Invoke) Token: 0x0600F92D RID: 63789
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public delegate void ValidatingDelegate(object sender, InputManagerValidatingEventArgs e);

		// Token: 0x02001914 RID: 6420
		// (Invoke) Token: 0x0600F931 RID: 63793
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public delegate void ValidatedDelegate(object sender, InputManagerValidatedEventArgs e);
	}
}
