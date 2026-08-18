using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02000FF8 RID: 4088
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DatePickerClientEvents : ObjectWithState
	{
		// Token: 0x06009FF3 RID: 40947 RVA: 0x0023A1BB File Offset: 0x002383BB
		internal static string ToLower(Match m)
		{
			return m.ToString().ToLower();
		}

		// Token: 0x06009FF4 RID: 40948 RVA: 0x0023A1C8 File Offset: 0x002383C8
		public DatePickerClientEvents(StateBag OwnerStateBag, RadDatePicker owner) : base("dpce_", OwnerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x06009FF5 RID: 40949 RVA: 0x0023A1E0 File Offset: 0x002383E0
		internal void DescribeEvents(IScriptDescriptor descriptor)
		{
			string[] array = new string[]
			{
				"DateSelected",
				"PopupOpening",
				"PopupClosing"
			};
			foreach (string text in array)
			{
				string text2 = (string)DataBinder.GetPropertyValue(this, string.Format("On{0}", text));
				if (!string.IsNullOrEmpty(text2))
				{
					descriptor.AddEvent(Regex.Replace(text, "^[A-Z]", new MatchEvaluator(DatePickerClientEvents.ToLower)), text2);
				}
			}
		}

		// Token: 0x1700328C RID: 12940
		// (get) Token: 0x06009FF6 RID: 40950 RVA: 0x0023A267 File Offset: 0x00238467
		// (set) Token: 0x06009FF7 RID: 40951 RVA: 0x0023A287 File Offset: 0x00238487
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[Description("The name of the client-side event handler that is executed whenever the selected date of the datepicker is changed.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnDateSelected
		{
			get
			{
				return (base.ViewState["OnDateSelected"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnDateSelected"] = value;
			}
		}

		// Token: 0x1700328D RID: 12941
		// (get) Token: 0x06009FF8 RID: 40952 RVA: 0x0023A29A File Offset: 0x0023849A
		// (set) Token: 0x06009FF9 RID: 40953 RVA: 0x0023A2BA File Offset: 0x002384BA
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Client-side events")]
		[Description("")]
		public string OnPopupOpening
		{
			get
			{
				return (base.ViewState["OnPopupOpening"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnPopupOpening"] = value;
			}
		}

		// Token: 0x1700328E RID: 12942
		// (get) Token: 0x06009FFA RID: 40954 RVA: 0x0023A2CD File Offset: 0x002384CD
		// (set) Token: 0x06009FFB RID: 40955 RVA: 0x0023A2ED File Offset: 0x002384ED
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[Description("")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string OnPopupClosing
		{
			get
			{
				return (base.ViewState["OnPopupClosing"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnPopupClosing"] = value;
			}
		}

		// Token: 0x04002CCE RID: 11470
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		public RadDatePicker owner;
	}
}
