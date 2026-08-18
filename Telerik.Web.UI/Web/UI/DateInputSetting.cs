using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200055C RID: 1372
	public class DateInputSetting : InputSetting, IRadDateInput
	{
		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x000A2514 File Offset: 0x000A0714
		// (set) Token: 0x06003165 RID: 12645 RVA: 0x000A2548 File Offset: 0x000A0748
		[Description("Culture used by RadDateInput to format the date.")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public CultureInfo Culture
		{
			get
			{
				if (base.ViewState["Culture"] == null)
				{
					return Thread.CurrentThread.CurrentCulture;
				}
				return (CultureInfo)base.ViewState["Culture"];
			}
			set
			{
				base.ViewState["Culture"] = value;
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000A255B File Offset: 0x000A075B
		// (set) Token: 0x06003167 RID: 12647 RVA: 0x000A2591 File Offset: 0x000A0791
		[Category("Behavior")]
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		[NotifyParentProperty(true)]
		[Description("The smallest date allowed by DateInput.")]
		public DateTime MinDate
		{
			get
			{
				if (base.ViewState["MinDate"] == null)
				{
					return new DateTime(1980, 1, 1);
				}
				return (DateTime)base.ViewState["MinDate"];
			}
			set
			{
				base.ViewState["MinDate"] = value;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000A25A9 File Offset: 0x000A07A9
		// (set) Token: 0x06003169 RID: 12649 RVA: 0x000A25E1 File Offset: 0x000A07E1
		[Description("The largest date allowed by DateInput.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(DateTime), "12/31/2099")]
		[Category("Behavior")]
		public DateTime MaxDate
		{
			get
			{
				if (base.ViewState["MaxDate"] == null)
				{
					return new DateTime(2099, 12, 31);
				}
				return (DateTime)base.ViewState["MaxDate"];
			}
			set
			{
				base.ViewState["MaxDate"] = value;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000A260C File Offset: 0x000A080C
		// (set) Token: 0x0600316A RID: 12650 RVA: 0x000A25F9 File Offset: 0x000A07F9
		[Description("Date and time format used by DateInput.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual string DateFormat
		{
			get
			{
				string text = (string)base.ViewState["DateFormat"];
				if (text == null)
				{
					text = "d";
				}
				return InputUtil.MapDateFormatShortCuts(text, this.Culture.DateTimeFormat);
			}
			set
			{
				base.ViewState["DateFormat"] = value;
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000A267C File Offset: 0x000A087C
		// (set) Token: 0x0600316C RID: 12652 RVA: 0x000A2649 File Offset: 0x000A0849
		[Category("Behavior")]
		[Description("Date and time format used by RadDateInput.")]
		[NotifyParentProperty(true)]
		public virtual string DisplayDateFormat
		{
			get
			{
				string text = (string)base.ViewState["DisplayDateFormat"];
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
					base.ViewState["DisplayDateFormat"] = value;
					return;
				}
				base.ViewState["DisplayDateFormat"] = null;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000A26BC File Offset: 0x000A08BC
		// (set) Token: 0x0600316F RID: 12655 RVA: 0x000A26EB File Offset: 0x000A08EB
		[DefaultValue(2029)]
		[Category("Behavior")]
		[Description("Indicates the end of the century that is used to interpret the year value when a short year is entered in the input.")]
		[NotifyParentProperty(true)]
		public int ShortYearCenturyEnd
		{
			get
			{
				int result = 2029;
				object obj = base.ViewState["ShortYearCenturyEnd"];
				if (obj == null)
				{
					return result;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["ShortYearCenturyEnd"] = value;
			}
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000A2704 File Offset: 0x000A0904
		internal override void Describe(IScriptDescriptor descriptor)
		{
			base.Describe(descriptor);
			descriptor.AddProperty("dateFormat", this.DateFormat);
			descriptor.AddProperty("displayDateFormat", this.DisplayDateFormat);
			if (this.MinDate != new DateTime(1980, 1, 1))
			{
				descriptor.AddProperty("minDate", this.MinDate.ToString("yyyy-MM-dd-HH-mm-ss"));
			}
			if (this.MaxDate != new DateTime(2099, 12, 31))
			{
				descriptor.AddProperty("maxDate", this.MaxDate.ToString("yyyy-MM-dd-HH-mm-ss"));
			}
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000A27AC File Offset: 0x000A09AC
		public override void Validate(TextBox input, object context)
		{
			base.Validate(input, context);
			if (this._isValid)
			{
				bool flag = string.IsNullOrEmpty(input.Text);
				this.UpdateValue(input, false);
				if (!flag && string.IsNullOrEmpty(input.Text))
				{
					this._isValid = false;
					this.invalidIds.Add(input.ID);
				}
			}
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000A2805 File Offset: 0x000A0A05
		public override void Validate(TextBox input)
		{
			this.Validate(input, null);
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000A2810 File Offset: 0x000A0A10
		internal override void UpdateValue(TextBox input, bool shouldFormat)
		{
			if (!string.IsNullOrEmpty(input.Text))
			{
				DateTime minValue = DateTime.MinValue;
				if (!this.TryParseDate(input, out minValue))
				{
					input.Text = "";
				}
				else if (shouldFormat)
				{
					if (minValue > this.MaxDate || minValue < this.MinDate)
					{
						input.Text = "";
					}
					else
					{
						input.Text = minValue.ToString(this.DisplayDateFormat, this.Culture);
					}
				}
			}
			base.UpdateValue(input, shouldFormat);
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000A2898 File Offset: 0x000A0A98
		protected bool TryParseDate(TextBox input, out DateTime date)
		{
			bool flag = DateTime.TryParseExact(input.Text, this.DateFormat, this.Culture, DateTimeStyles.None, out date);
			if (!flag)
			{
				flag = DateTime.TryParseExact(input.Text, this.DateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
			}
			if (!flag)
			{
				flag = DateTime.TryParseExact(input.Text, this.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
			}
			if (!flag)
			{
				flag = DateTime.TryParse(input.Text, this.Culture, DateTimeStyles.None, out date);
			}
			if (!flag)
			{
				flag = DateTime.TryParse(input.Text, CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
			}
			if (!flag)
			{
				flag = DateTime.TryParse(input.Text, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
			}
			return flag;
		}
	}
}
