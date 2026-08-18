using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001A2D RID: 6701
	public class AdvancedFormSettings : ObjectWithState
	{
		// Token: 0x17004EC7 RID: 20167
		// (get) Token: 0x06010433 RID: 66611 RVA: 0x003A27B6 File Offset: 0x003A09B6
		internal IScheduler Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06010434 RID: 66612 RVA: 0x003A27BE File Offset: 0x003A09BE
		internal AdvancedFormSettings(IScheduler owner, StateBag ownerViewState) : base("AdvancedFormSettings", ownerViewState)
		{
			this._owner = owner;
		}

		// Token: 0x17004EC8 RID: 20168
		// (get) Token: 0x06010435 RID: 66613 RVA: 0x003A27D3 File Offset: 0x003A09D3
		// (set) Token: 0x06010436 RID: 66614 RVA: 0x003A27F4 File Offset: 0x003A09F4
		[Category("Behavior")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Enables the advanced insert/edit form.")]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? true);
			}
			set
			{
				base.ViewState["Enabled"] = value;
				this.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004EC9 RID: 20169
		// (get) Token: 0x06010437 RID: 66615 RVA: 0x003A2817 File Offset: 0x003A0A17
		// (set) Token: 0x06010438 RID: 66616 RVA: 0x003A2838 File Offset: 0x003A0A38
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Sets the default edit mode")]
		[NotifyParentProperty(true)]
		public bool Modal
		{
			get
			{
				return (bool)(base.ViewState["Modal"] ?? false);
			}
			set
			{
				base.ViewState["Modal"] = value;
				this.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004ECA RID: 20170
		// (get) Token: 0x06010439 RID: 66617 RVA: 0x003A285B File Offset: 0x003A0A5B
		// (set) Token: 0x0601043A RID: 66618 RVA: 0x003A2880 File Offset: 0x003A0A80
		[DefaultValue(2500)]
		[Description("Sets the z-index of the modal dialog")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public int ZIndex
		{
			get
			{
				return (int)(base.ViewState["ZIndex"] ?? 2500);
			}
			set
			{
				base.ViewState["ZIndex"] = value;
				this.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004ECB RID: 20171
		// (get) Token: 0x0601043B RID: 66619 RVA: 0x003A28A3 File Offset: 0x003A0AA3
		// (set) Token: 0x0601043C RID: 66620 RVA: 0x003A28C4 File Offset: 0x003A0AC4
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Controls the visibility of the resource selection drop-downs in the advanced form.")]
		[DefaultValue(true)]
		public bool EnableResourceEditing
		{
			get
			{
				return (bool)(base.ViewState["EnableResourceEditing"] ?? true);
			}
			set
			{
				base.ViewState["EnableResourceEditing"] = value;
				this.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004ECC RID: 20172
		// (get) Token: 0x0601043D RID: 66621 RVA: 0x003A28E7 File Offset: 0x003A0AE7
		// (set) Token: 0x0601043E RID: 66622 RVA: 0x003A2908 File Offset: 0x003A0B08
		[Description("Controls whether one can chose custom time zone for appointment, i.e. different from the one RadScheduler is operating in.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool EnableTimeZonesEditing
		{
			get
			{
				return (bool)(base.ViewState["EnableTimeZonesEditing"] ?? false);
			}
			set
			{
				base.ViewState["EnableTimeZonesEditing"] = value;
				this.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004ECD RID: 20173
		// (get) Token: 0x0601043F RID: 66623 RVA: 0x003A292B File Offset: 0x003A0B2B
		// (set) Token: 0x06010440 RID: 66624 RVA: 0x003A294C File Offset: 0x003A0B4C
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Controls the visibility of the attribute selection text boxes in the advanced form.")]
		[DefaultValue(false)]
		public bool EnableCustomAttributeEditing
		{
			get
			{
				return (bool)(base.ViewState["EnableCustomAttributeEditing"] ?? false);
			}
			set
			{
				base.ViewState["EnableCustomAttributeEditing"] = value;
				this.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x17004ECE RID: 20174
		// (get) Token: 0x06010441 RID: 66625 RVA: 0x003A296F File Offset: 0x003A0B6F
		// (set) Token: 0x06010442 RID: 66626 RVA: 0x003A29AD File Offset: 0x003A0BAD
		[NotifyParentProperty(true)]
		[Description("The edit form date format string.")]
		[Category("Appearance")]
		public string DateFormat
		{
			get
			{
				if (base.ViewState["DateFormat"] == null)
				{
					return Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
				}
				return (string)base.ViewState["DateFormat"];
			}
			set
			{
				base.ViewState["DateFormat"] = value;
				this.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x06010443 RID: 66627 RVA: 0x003A29CB File Offset: 0x003A0BCB
		private bool ShouldSerializeDateFormat()
		{
			return this.DateFormat != Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
		}

		// Token: 0x17004ECF RID: 20175
		// (get) Token: 0x06010444 RID: 66628 RVA: 0x003A29EC File Offset: 0x003A0BEC
		// (set) Token: 0x06010445 RID: 66629 RVA: 0x003A2A2A File Offset: 0x003A0C2A
		[NotifyParentProperty(true)]
		[Description("The edit form time format string.")]
		[Category("Appearance")]
		public string TimeFormat
		{
			get
			{
				if (base.ViewState["TimeFormat"] == null)
				{
					return Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern;
				}
				return (string)base.ViewState["TimeFormat"];
			}
			set
			{
				base.ViewState["TimeFormat"] = value;
				this.Owner.NotifyDataPropertyChanged();
			}
		}

		// Token: 0x06010446 RID: 66630 RVA: 0x003A2A48 File Offset: 0x003A0C48
		private bool ShouldSerializeTimeFormat()
		{
			return this.TimeFormat != Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern;
		}

		// Token: 0x17004ED0 RID: 20176
		// (get) Token: 0x06010447 RID: 66631 RVA: 0x003A2A69 File Offset: 0x003A0C69
		// (set) Token: 0x06010448 RID: 66632 RVA: 0x003A2A93 File Offset: 0x003A0C93
		[DefaultValue(typeof(Unit), "550px")]
		[Category("Appearance")]
		[Description("The height of each RadScheduler row")]
		public Unit MaximumHeight
		{
			get
			{
				return (Unit)(base.ViewState["MaximumHeight"] ?? Unit.Pixel(550));
			}
			set
			{
				base.ViewState["MaximumHeight"] = value;
			}
		}

		// Token: 0x17004ED1 RID: 20177
		// (get) Token: 0x06010449 RID: 66633 RVA: 0x003A2AAB File Offset: 0x003A0CAB
		// (set) Token: 0x0601044A RID: 66634 RVA: 0x003A2AD5 File Offset: 0x003A0CD5
		[DefaultValue(typeof(Unit), "700px")]
		[Description("the width of the modal advanced form")]
		[Category("Appearance")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Pixel(700));
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x0601044B RID: 66635 RVA: 0x003A2AED File Offset: 0x003A0CED
		internal JavaScriptConverter GetConverter()
		{
			return new AdvancedFormSettingsConverter();
		}

		// Token: 0x0601044C RID: 66636 RVA: 0x003A2AF4 File Offset: 0x003A0CF4
		internal void Describe(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			JavaScriptConverter converter = this.GetConverter();
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				converter
			});
			IDictionary<string, object> dictionary = converter.Serialize(this, serializer);
			if (dictionary.Count > 0)
			{
				descriptor.AddScriptProperty(propertyName, serializer.Serialize(this));
			}
		}

		// Token: 0x0400494E RID: 18766
		private readonly IScheduler _owner;
	}
}
