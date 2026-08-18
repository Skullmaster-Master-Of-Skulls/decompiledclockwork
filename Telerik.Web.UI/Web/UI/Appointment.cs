using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x020012C8 RID: 4808
	[Serializable]
	public class Appointment : StateManager, ISerializable, ICustomTypeDescriptor, ICloneable, IDisposable
	{
		// Token: 0x1700413F RID: 16703
		// (get) Token: 0x0600C9B6 RID: 51638 RVA: 0x002D0419 File Offset: 0x002CE619
		[ScriptIgnore]
		[Browsable(false)]
		[NonSerializedInControlState]
		public IList<AppointmentControl> AppointmentControls
		{
			get
			{
				return this._appointmentControls;
			}
		}

		// Token: 0x17004140 RID: 16704
		// (get) Token: 0x0600C9B7 RID: 51639 RVA: 0x002D0421 File Offset: 0x002CE621
		[Browsable(false)]
		[ScriptIgnore]
		[NonSerializedInControlState]
		public string ClientID
		{
			get
			{
				if (this.DomElements.Count > 0)
				{
					return this.DomElements[0];
				}
				return string.Empty;
			}
		}

		// Token: 0x17004141 RID: 16705
		// (get) Token: 0x0600C9B8 RID: 51640 RVA: 0x002D0444 File Offset: 0x002CE644
		[Browsable(false)]
		[NonSerializedInControlState]
		public IList<string> DomElements
		{
			get
			{
				List<string> list = new List<string>(this._appointmentControls.Count);
				foreach (AppointmentControl appointmentControl in this._appointmentControls)
				{
					list.Add(appointmentControl.ClientID);
				}
				return list;
			}
		}

		// Token: 0x17004142 RID: 16706
		// (get) Token: 0x0600C9B9 RID: 51641 RVA: 0x002D04A8 File Offset: 0x002CE6A8
		[ScriptIgnore]
		[Browsable(false)]
		[NonSerializedInControlState]
		public System.Web.UI.AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new System.Web.UI.AttributeCollection(this.AttributeState);
				}
				return this._attributes;
			}
		}

		// Token: 0x17004143 RID: 16707
		// (get) Token: 0x0600C9BA RID: 51642 RVA: 0x002D04C9 File Offset: 0x002CE6C9
		// (set) Token: 0x0600C9BB RID: 51643 RVA: 0x002D04F7 File Offset: 0x002CE6F7
		[ScriptIgnore]
		[NonSerializedInControlState]
		[Browsable(false)]
		public ResourceCollection Resources
		{
			get
			{
				if (this._resources == null)
				{
					this._resources = new ResourceCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._resources).TrackViewState();
					}
				}
				return this._resources;
			}
			set
			{
				this._resources = value;
			}
		}

		// Token: 0x17004144 RID: 16708
		// (get) Token: 0x0600C9BC RID: 51644 RVA: 0x002D0500 File Offset: 0x002CE700
		// (set) Token: 0x0600C9BD RID: 51645 RVA: 0x002D052E File Offset: 0x002CE72E
		[NonSerializedInControlState]
		[ScriptIgnore]
		[Browsable(false)]
		public ReminderCollection Reminders
		{
			get
			{
				if (this._reminders == null)
				{
					this._reminders = new ReminderCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._reminders).TrackViewState();
					}
				}
				return this._reminders;
			}
			set
			{
				this._reminders = value;
			}
		}

		// Token: 0x17004145 RID: 16709
		// (get) Token: 0x0600C9BE RID: 51646 RVA: 0x002D0538 File Offset: 0x002CE738
		// (set) Token: 0x0600C9BF RID: 51647 RVA: 0x002D0565 File Offset: 0x002CE765
		[Category("Appearance")]
		[ScriptIgnore]
		[DefaultValue("")]
		[NonSerializedInControlState]
		public string CssClass
		{
			get
			{
				object obj = base.ViewState["CssClass"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x17004146 RID: 16710
		// (get) Token: 0x0600C9C0 RID: 51648 RVA: 0x002D0578 File Offset: 0x002CE778
		// (set) Token: 0x0600C9C1 RID: 51649 RVA: 0x002D0585 File Offset: 0x002CE785
		[TypeConverter(typeof(WebColorConverter))]
		[DefaultValue(typeof(Color), "")]
		[ScriptIgnore]
		[Category("Appearance")]
		[NonSerializedInControlState]
		public virtual Color BackColor
		{
			get
			{
				return this.ControlStyle.BackColor;
			}
			set
			{
				this.ControlStyle.BackColor = value;
			}
		}

		// Token: 0x17004147 RID: 16711
		// (get) Token: 0x0600C9C2 RID: 51650 RVA: 0x002D0593 File Offset: 0x002CE793
		// (set) Token: 0x0600C9C3 RID: 51651 RVA: 0x002D05A0 File Offset: 0x002CE7A0
		[DefaultValue(typeof(Color), "")]
		[NonSerializedInControlState]
		[TypeConverter(typeof(WebColorConverter))]
		[Category("Appearance")]
		[ScriptIgnore]
		public virtual Color BorderColor
		{
			get
			{
				return this.ControlStyle.BorderColor;
			}
			set
			{
				this.ControlStyle.BorderColor = value;
			}
		}

		// Token: 0x17004148 RID: 16712
		// (get) Token: 0x0600C9C4 RID: 51652 RVA: 0x002D05AE File Offset: 0x002CE7AE
		// (set) Token: 0x0600C9C5 RID: 51653 RVA: 0x002D05BB File Offset: 0x002CE7BB
		[Category("Appearance")]
		[ScriptIgnore]
		[NonSerializedInControlState]
		[DefaultValue(typeof(Unit), "")]
		public virtual Unit BorderWidth
		{
			get
			{
				return this.ControlStyle.BorderWidth;
			}
			set
			{
				this.ControlStyle.BorderWidth = value;
			}
		}

		// Token: 0x17004149 RID: 16713
		// (get) Token: 0x0600C9C6 RID: 51654 RVA: 0x002D05C9 File Offset: 0x002CE7C9
		// (set) Token: 0x0600C9C7 RID: 51655 RVA: 0x002D05D6 File Offset: 0x002CE7D6
		[NonSerializedInControlState]
		[Category("Appearance")]
		[DefaultValue(BorderStyle.NotSet)]
		[ScriptIgnore]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return this.ControlStyle.BorderStyle;
			}
			set
			{
				this.ControlStyle.BorderStyle = value;
			}
		}

		// Token: 0x1700414A RID: 16714
		// (get) Token: 0x0600C9C8 RID: 51656 RVA: 0x002D05E4 File Offset: 0x002CE7E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ScriptIgnore]
		[NonSerializedInControlState]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public virtual FontInfo Font
		{
			get
			{
				return this.ControlStyle.Font;
			}
		}

		// Token: 0x1700414B RID: 16715
		// (get) Token: 0x0600C9C9 RID: 51657 RVA: 0x002D05F1 File Offset: 0x002CE7F1
		// (set) Token: 0x0600C9CA RID: 51658 RVA: 0x002D05FE File Offset: 0x002CE7FE
		[ScriptIgnore]
		[NonSerializedInControlState]
		[TypeConverter(typeof(WebColorConverter))]
		[Category("Appearance")]
		[DefaultValue(typeof(Color), "")]
		public virtual Color ForeColor
		{
			get
			{
				return this.ControlStyle.ForeColor;
			}
			set
			{
				this.ControlStyle.ForeColor = value;
			}
		}

		// Token: 0x1700414C RID: 16716
		// (get) Token: 0x0600C9CB RID: 51659 RVA: 0x002D060C File Offset: 0x002CE80C
		// (set) Token: 0x0600C9CC RID: 51660 RVA: 0x002D061E File Offset: 0x002CE81E
		[ClientPropertyName("id")]
		public object ID
		{
			get
			{
				return base.ViewState["ID"];
			}
			set
			{
				base.ViewState["ID"] = value;
			}
		}

		// Token: 0x1700414D RID: 16717
		// (get) Token: 0x0600C9CD RID: 51661 RVA: 0x002D0631 File Offset: 0x002CE831
		// (set) Token: 0x0600C9CE RID: 51662 RVA: 0x002D0652 File Offset: 0x002CE852
		public bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x1700414E RID: 16718
		// (get) Token: 0x0600C9CF RID: 51663 RVA: 0x002D066A File Offset: 0x002CE86A
		// (set) Token: 0x0600C9D0 RID: 51664 RVA: 0x002D068F File Offset: 0x002CE88F
		[ScriptIgnore]
		public DateTime Start
		{
			get
			{
				return (DateTime)(base.ViewState["StartDate"] ?? DateTime.MinValue);
			}
			set
			{
				base.ViewState["StartDate"] = DateHelper.AssumeUtc(value);
			}
		}

		// Token: 0x1700414F RID: 16719
		// (get) Token: 0x0600C9D1 RID: 51665 RVA: 0x002D06AC File Offset: 0x002CE8AC
		// (set) Token: 0x0600C9D2 RID: 51666 RVA: 0x002D06C4 File Offset: 0x002CE8C4
		[NonSerializedInControlState]
		[ScriptIgnore]
		public DateTime StartLocal
		{
			get
			{
				return TimeZoneInfoProvider.UtcToLocal(this.Start, TimeZoneInfoProvider.GetTimeZoneModelById(this.TimeZoneID));
			}
			set
			{
				this.Start = TimeZoneInfoProvider.LocalToUtc(value, TimeZoneInfoProvider.GetTimeZoneModelById(this.TimeZoneID));
			}
		}

		// Token: 0x17004150 RID: 16720
		// (get) Token: 0x0600C9D3 RID: 51667 RVA: 0x002D06DD File Offset: 0x002CE8DD
		// (set) Token: 0x0600C9D4 RID: 51668 RVA: 0x002D0702 File Offset: 0x002CE902
		[ScriptIgnore]
		public DateTime End
		{
			get
			{
				return (DateTime)(base.ViewState["EndDate"] ?? DateTime.MinValue);
			}
			set
			{
				base.ViewState["EndDate"] = DateHelper.AssumeUtc(value);
			}
		}

		// Token: 0x17004151 RID: 16721
		// (get) Token: 0x0600C9D5 RID: 51669 RVA: 0x002D071F File Offset: 0x002CE91F
		// (set) Token: 0x0600C9D6 RID: 51670 RVA: 0x002D0737 File Offset: 0x002CE937
		[ScriptIgnore]
		[NonSerializedInControlState]
		public DateTime EndLocal
		{
			get
			{
				return TimeZoneInfoProvider.UtcToLocal(this.End, TimeZoneInfoProvider.GetTimeZoneModelById(this.TimeZoneID));
			}
			set
			{
				this.End = TimeZoneInfoProvider.LocalToUtc(value, TimeZoneInfoProvider.GetTimeZoneModelById(this.TimeZoneID));
			}
		}

		// Token: 0x17004152 RID: 16722
		// (get) Token: 0x0600C9D7 RID: 51671 RVA: 0x002D0750 File Offset: 0x002CE950
		[NonSerializedInControlState]
		[ScriptIgnore]
		[Browsable(false)]
		public TimeSpan Duration
		{
			get
			{
				return this.End - this.Start;
			}
		}

		// Token: 0x17004153 RID: 16723
		// (get) Token: 0x0600C9D8 RID: 51672 RVA: 0x002D0763 File Offset: 0x002CE963
		// (set) Token: 0x0600C9D9 RID: 51673 RVA: 0x002D0783 File Offset: 0x002CE983
		public string Subject
		{
			get
			{
				return ((string)base.ViewState["Subject"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Subject"] = value;
			}
		}

		// Token: 0x17004154 RID: 16724
		// (get) Token: 0x0600C9DA RID: 51674 RVA: 0x002D0796 File Offset: 0x002CE996
		// (set) Token: 0x0600C9DB RID: 51675 RVA: 0x002D07B6 File Offset: 0x002CE9B6
		public string TimeZoneID
		{
			get
			{
				return ((string)base.ViewState["TimeZoneID"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["TimeZoneID"] = value;
			}
		}

		// Token: 0x17004155 RID: 16725
		// (get) Token: 0x0600C9DC RID: 51676 RVA: 0x002D07C9 File Offset: 0x002CE9C9
		// (set) Token: 0x0600C9DD RID: 51677 RVA: 0x002D07E9 File Offset: 0x002CE9E9
		public string Description
		{
			get
			{
				return ((string)base.ViewState["Description"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x17004156 RID: 16726
		// (get) Token: 0x0600C9DE RID: 51678 RVA: 0x002D07FC File Offset: 0x002CE9FC
		// (set) Token: 0x0600C9DF RID: 51679 RVA: 0x002D081D File Offset: 0x002CEA1D
		public string ToolTip
		{
			get
			{
				return ((string)base.ViewState["ToolTip"]) ?? this.Subject;
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17004157 RID: 16727
		// (get) Token: 0x0600C9E0 RID: 51680 RVA: 0x002D0830 File Offset: 0x002CEA30
		// (set) Token: 0x0600C9E1 RID: 51681 RVA: 0x002D0850 File Offset: 0x002CEA50
		[ScriptIgnore]
		public string RecurrenceRule
		{
			get
			{
				return ((string)base.ViewState["RecurrenceRule"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["RecurrenceRule"] = value;
			}
		}

		// Token: 0x17004158 RID: 16728
		// (get) Token: 0x0600C9E2 RID: 51682 RVA: 0x002D0863 File Offset: 0x002CEA63
		// (set) Token: 0x0600C9E3 RID: 51683 RVA: 0x002D0875 File Offset: 0x002CEA75
		public object RecurrenceParentID
		{
			get
			{
				return base.ViewState["RecurrenceParentID"];
			}
			set
			{
				base.ViewState["RecurrenceParentID"] = value;
			}
		}

		// Token: 0x17004159 RID: 16729
		// (get) Token: 0x0600C9E4 RID: 51684 RVA: 0x002D0888 File Offset: 0x002CEA88
		// (set) Token: 0x0600C9E5 RID: 51685 RVA: 0x002D08A9 File Offset: 0x002CEAA9
		public RecurrenceState RecurrenceState
		{
			get
			{
				return (RecurrenceState)(base.ViewState["RecurrenceState"] ?? RecurrenceState.NotRecurring);
			}
			set
			{
				base.ViewState["RecurrenceState"] = value;
			}
		}

		// Token: 0x1700415A RID: 16730
		// (get) Token: 0x0600C9E6 RID: 51686 RVA: 0x002D08C1 File Offset: 0x002CEAC1
		// (set) Token: 0x0600C9E7 RID: 51687 RVA: 0x002D08C9 File Offset: 0x002CEAC9
		[ScriptIgnore]
		[NonSerializedInControlState]
		public RadScheduler Owner
		{
			get
			{
				return this._scheduler;
			}
			set
			{
				this._scheduler = value;
			}
		}

		// Token: 0x1700415B RID: 16731
		// (get) Token: 0x0600C9E8 RID: 51688 RVA: 0x002D08D2 File Offset: 0x002CEAD2
		// (set) Token: 0x0600C9E9 RID: 51689 RVA: 0x002D08F2 File Offset: 0x002CEAF2
		[DefaultValue("")]
		public string ContextMenuID
		{
			get
			{
				return ((string)base.ViewState["ContextMenuID"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ContextMenuID"] = value;
			}
		}

		// Token: 0x1700415C RID: 16732
		// (get) Token: 0x0600C9EA RID: 51690 RVA: 0x002D0908 File Offset: 0x002CEB08
		// (set) Token: 0x0600C9EB RID: 51691 RVA: 0x002D094A File Offset: 0x002CEB4A
		public bool AllowEdit
		{
			get
			{
				bool flag = true;
				if (this.Owner != null)
				{
					flag = this.Owner.AllowEdit;
				}
				return (bool)(base.ViewState["AllowEdit"] ?? flag);
			}
			set
			{
				base.ViewState["AllowEdit"] = value;
			}
		}

		// Token: 0x1700415D RID: 16733
		// (get) Token: 0x0600C9EC RID: 51692 RVA: 0x002D0964 File Offset: 0x002CEB64
		// (set) Token: 0x0600C9ED RID: 51693 RVA: 0x002D09A6 File Offset: 0x002CEBA6
		public bool AllowDelete
		{
			get
			{
				bool flag = true;
				if (this.Owner != null)
				{
					flag = this.Owner.AllowDelete;
				}
				return (bool)(base.ViewState["AllowDelete"] ?? flag);
			}
			set
			{
				base.ViewState["AllowDelete"] = value;
			}
		}

		// Token: 0x1700415E RID: 16734
		// (get) Token: 0x0600C9EE RID: 51694 RVA: 0x002D09BE File Offset: 0x002CEBBE
		// (set) Token: 0x0600C9EF RID: 51695 RVA: 0x002D09C6 File Offset: 0x002CEBC6
		public virtual object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x1700415F RID: 16735
		// (get) Token: 0x0600C9F0 RID: 51696 RVA: 0x002D09CF File Offset: 0x002CEBCF
		private StateBag AttributeState
		{
			get
			{
				if (this._attributeState == null)
				{
					this._attributeState = new StateBag();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._attributeState).TrackViewState();
					}
				}
				return this._attributeState;
			}
		}

		// Token: 0x17004160 RID: 16736
		// (get) Token: 0x0600C9F1 RID: 51697 RVA: 0x002D09FD File Offset: 0x002CEBFD
		private Style ControlStyle
		{
			get
			{
				if (this._controlStyle == null)
				{
					this._controlStyle = new Style();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._controlStyle).TrackViewState();
					}
				}
				return this._controlStyle;
			}
		}

		// Token: 0x0600C9F2 RID: 51698 RVA: 0x002D0A2B File Offset: 0x002CEC2B
		internal override void SetDirty()
		{
			base.SetDirty();
			this.AttributeState.SetDirty(true);
			this.Resources.SetDirty();
			this.Reminders.SetDirty();
			this.ControlStyle.SetDirty();
		}

		// Token: 0x0600C9F3 RID: 51699 RVA: 0x002D0A60 File Offset: 0x002CEC60
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Resources).LoadViewState(array[1]);
			((IStateManager)this.ControlStyle).LoadViewState(array[2]);
			((IStateManager)this.AttributeState).LoadViewState(array[3]);
			((IStateManager)this.Reminders).LoadViewState(array[4]);
		}

		// Token: 0x0600C9F4 RID: 51700 RVA: 0x002D0AB8 File Offset: 0x002CECB8
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Resources).SaveViewState(),
				((IStateManager)this.ControlStyle).SaveViewState(),
				((IStateManager)this.AttributeState).SaveViewState(),
				((IStateManager)this.Reminders).SaveViewState()
			};
			return arrayList.ToArray();
		}

		// Token: 0x0600C9F5 RID: 51701 RVA: 0x002D0B28 File Offset: 0x002CED28
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._controlStyle != null)
			{
				((IStateManager)this._controlStyle).TrackViewState();
			}
			if (this._attributeState != null)
			{
				((IStateManager)this._attributeState).TrackViewState();
			}
		}

		// Token: 0x0600C9F6 RID: 51702 RVA: 0x002D0B56 File Offset: 0x002CED56
		public Appointment()
		{
		}

		// Token: 0x0600C9F7 RID: 51703 RVA: 0x002D0B69 File Offset: 0x002CED69
		public Appointment(object id, DateTime startDate, DateTime endDate, string subject) : this(id, startDate, endDate, subject, string.Empty, null, RecurrenceState.NotRecurring)
		{
		}

		// Token: 0x0600C9F8 RID: 51704 RVA: 0x002D0B7D File Offset: 0x002CED7D
		public Appointment(object id, DateTime startDate, DateTime endDate, string subject, string recurrenceRule) : this(id, startDate, endDate, subject, recurrenceRule, null, RecurrenceState.Master)
		{
		}

		// Token: 0x0600C9F9 RID: 51705 RVA: 0x002D0B90 File Offset: 0x002CED90
		public Appointment(object id, DateTime startDate, DateTime endDate, string subject, string recurrenceRule, object recurrenceParentId, RecurrenceState initialState) : this(id, startDate, endDate, subject, recurrenceRule, recurrenceParentId, initialState, string.Empty)
		{
		}

		// Token: 0x0600C9FA RID: 51706 RVA: 0x002D0BB4 File Offset: 0x002CEDB4
		public Appointment(object id, DateTime startDate, DateTime endDate, string subject, string recurrenceRule, object recurrenceParentId, RecurrenceState initialState, string timeZoneId)
		{
			this.ID = id;
			this.Start = startDate;
			this.End = endDate;
			this.Subject = subject;
			this.RecurrenceRule = recurrenceRule;
			this.RecurrenceParentID = recurrenceParentId;
			this.RecurrenceState = initialState;
			this.TimeZoneID = timeZoneId;
		}

		// Token: 0x0600C9FB RID: 51707 RVA: 0x002D0C10 File Offset: 0x002CEE10
		internal IOrderedDictionary GetData()
		{
			IOrderedDictionary orderedDictionary = new OrderedDictionary();
			orderedDictionary["Subject"] = this.Subject;
			orderedDictionary["Start"] = this.Start;
			orderedDictionary["End"] = this.End;
			orderedDictionary["RecurrenceRule"] = this.RecurrenceRule;
			orderedDictionary["RecurrenceParentID"] = this.RecurrenceParentID;
			if (this.Owner != null && this.Owner.HasDescriptionField)
			{
				orderedDictionary["$$Description$$"] = this.Description;
			}
			foreach (string text in this.Resources.GetResourceTypes())
			{
				object[] resourceKeysByType = this.Resources.GetResourceKeysByType(text);
				orderedDictionary[text] = ((resourceKeysByType.Length == 1) ? resourceKeysByType[0] : resourceKeysByType);
			}
			foreach (object obj in this.Attributes.Keys)
			{
				string key = (string)obj;
				orderedDictionary[key] = this.Attributes[key];
			}
			orderedDictionary["$$Reminders$$"] = this.Reminders.ToString();
			if (!string.IsNullOrEmpty(this.TimeZoneID))
			{
				orderedDictionary["TimeZoneID"] = this.TimeZoneID;
			}
			return orderedDictionary;
		}

		// Token: 0x0600C9FC RID: 51708 RVA: 0x002D0D88 File Offset: 0x002CEF88
		internal void LoadFromDictionary(IOrderedDictionary value)
		{
			if (value.Contains("Subject"))
			{
				this.Subject = (string)value["Subject"];
			}
			if (value.Contains("$$Description$$"))
			{
				this.Description = (string)value["$$Description$$"];
			}
			if (value.Contains("Start"))
			{
				this.Start = (DateTime)value["Start"];
			}
			if (value.Contains("TimeZoneID"))
			{
				this.TimeZoneID = (string)value["TimeZoneID"];
			}
			if (value.Contains("End"))
			{
				this.End = (DateTime)value["End"];
			}
			if (value.Contains("RecurrenceRule"))
			{
				this.RecurrenceRule = (string)value["RecurrenceRule"];
				if (!string.IsNullOrEmpty(this.RecurrenceRule) && this.RecurrenceState == RecurrenceState.NotRecurring)
				{
					this.RecurrenceState = RecurrenceState.Master;
				}
			}
			if (value.Contains("RecurrenceParentID"))
			{
				this.RecurrenceParentID = value["RecurrenceParentID"];
				if (this.RecurrenceParentID != null && this.RecurrenceState == RecurrenceState.NotRecurring)
				{
					this.RecurrenceState = RecurrenceState.Exception;
				}
			}
			if (value.Contains("$$Reminders$$"))
			{
				this.Reminders.Clear();
				IList<Reminder> list = Reminder.TryParse((string)value["$$Reminders$$"]);
				if (list != null)
				{
					this.Reminders.AddRange(list);
				}
			}
			bool flag = false;
			foreach (object obj in value)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key.ToString();
				if (dictionaryEntry.Value != null && !(text == "Subject") && !(text == "$$Description$$") && !(text == "Start") && !(text == "End") && !(text == "RecurrenceRule") && !(text == "RecurrenceParentID") && !(text == "$$Reminders$$") && !(text == "TimeZoneID"))
				{
					this.Resources.ClearByType(text);
					if (this.Owner != null)
					{
						ResourceType resourceType = this.Owner.ResourceTypes.FindByForeignKey(text);
						if (resourceType != null)
						{
							this.Resources.ClearByType(resourceType.Name);
						}
						object[] array = dictionaryEntry.Value as object[];
						if (array != null)
						{
							foreach (object key in array)
							{
								Resource resource = this.Owner.Resources.GetResource(text, key);
								if (resource != null)
								{
									this.Resources.Add(resource);
								}
							}
							continue;
						}
						Resource resource2 = this.Owner.Resources.GetResource(text, dictionaryEntry.Value);
						if (resource2 != null)
						{
							this.Resources.Add(resource2);
							continue;
						}
						if (this.Owner.Resources.GetResourcesByType(text).Count > 0)
						{
							continue;
						}
					}
					if (!flag)
					{
						this.Attributes.Clear();
						flag = true;
					}
					this.Attributes[text] = dictionaryEntry.Value.ToString();
				}
			}
		}

		// Token: 0x0600C9FD RID: 51709 RVA: 0x002D1108 File Offset: 0x002CF308
		internal bool Overlaps(DateTime rangeStart, DateTime rangeEnd)
		{
			bool flag = this.Start < rangeEnd && this.End > rangeStart;
			if (this.Duration == TimeSpan.Zero)
			{
				return flag || this.Start == rangeStart;
			}
			return flag;
		}

		// Token: 0x0600C9FE RID: 51710 RVA: 0x002D1158 File Offset: 0x002CF358
		internal void Validate()
		{
			if (this.End < this.Start)
			{
				throw new Exception("Appointment is invalid: Start time must be before the End time.");
			}
		}

		// Token: 0x0600C9FF RID: 51711 RVA: 0x002D1178 File Offset: 0x002CF378
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected Appointment(SerializationInfo info, StreamingContext context)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.GetType());
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Value != null)
				{
					PropertyDescriptor propertyDescriptor = properties.Find(serializationEntry.Name, false);
					if (propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(this, serializationEntry.Value);
					}
				}
			}
			this.DeserializeResources(info);
			this.DeserializeAttributes(info);
			this.DeserializeReminders(info);
		}

		// Token: 0x0600CA00 RID: 51712 RVA: 0x002D11F8 File Offset: 0x002CF3F8
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x0600CA01 RID: 51713 RVA: 0x002D1204 File Offset: 0x002CF404
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.GetType());
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!propertyDescriptor.Attributes.Contains(new NonSerializedInControlStateAttribute()))
				{
					info.AddValue(propertyDescriptor.Name, propertyDescriptor.GetValue(this));
				}
			}
			this.SerializeResources(info);
			this.SerializeAttributes(info);
			this.SerializeReminders(info);
		}

		// Token: 0x0600CA02 RID: 51714 RVA: 0x002D1298 File Offset: 0x002CF498
		private void SerializeResources(SerializationInfo info)
		{
			string[] resourceTypes = this.Resources.GetResourceTypes();
			foreach (string text in resourceTypes)
			{
				ArrayList arrayList = new ArrayList();
				List<string> list = new List<string>();
				List<bool> list2 = new List<bool>();
				foreach (Resource resource in this.Resources.GetResourcesByType(text))
				{
					arrayList.Add(resource.Key);
					list.Add(resource.Text);
					list2.Add(resource.Available);
				}
				KeyValuePair<string, object[]> keyValuePair = new KeyValuePair<string, object[]>(text, arrayList.ToArray());
				info.AddValue(text + "_Keys", keyValuePair);
				KeyValuePair<string, string[]> keyValuePair2 = new KeyValuePair<string, string[]>(text, list.ToArray());
				info.AddValue(text + "_Texts", keyValuePair2);
				KeyValuePair<string, bool[]> keyValuePair3 = new KeyValuePair<string, bool[]>(text, list2.ToArray());
				info.AddValue(text + "_Avail", keyValuePair3);
			}
			info.AddValue("ResourceTypes", string.Join(",", resourceTypes));
		}

		// Token: 0x0600CA03 RID: 51715 RVA: 0x002D13E0 File Offset: 0x002CF5E0
		private void DeserializeResources(SerializationInfo info)
		{
			string @string = info.GetString("ResourceTypes");
			if (!string.IsNullOrEmpty(@string))
			{
				foreach (string text in @string.Split(new char[]
				{
					','
				}))
				{
					KeyValuePair<string, object[]> keyValuePair = (KeyValuePair<string, object[]>)info.GetValue(text + "_Keys", typeof(KeyValuePair<string, object[]>));
					KeyValuePair<string, string[]> keyValuePair2 = (KeyValuePair<string, string[]>)info.GetValue(text + "_Texts", typeof(KeyValuePair<string, string[]>));
					KeyValuePair<string, bool[]> keyValuePair3 = (KeyValuePair<string, bool[]>)info.GetValue(text + "_Avail", typeof(KeyValuePair<string, bool[]>));
					for (int j = 0; j < keyValuePair.Value.Length; j++)
					{
						Resource item = new Resource
						{
							Type = text,
							Key = keyValuePair.Value[j],
							Text = keyValuePair2.Value[j],
							Available = keyValuePair3.Value[j]
						};
						this.Resources.Add(item);
					}
				}
			}
		}

		// Token: 0x0600CA04 RID: 51716 RVA: 0x002D150A File Offset: 0x002CF70A
		private void SerializeReminders(SerializationInfo info)
		{
			info.AddValue("$$Reminders$$", this.Reminders.ToString());
		}

		// Token: 0x0600CA05 RID: 51717 RVA: 0x002D1524 File Offset: 0x002CF724
		private void DeserializeReminders(SerializationInfo info)
		{
			string @string = info.GetString("$$Reminders$$");
			if (!string.IsNullOrEmpty(@string))
			{
				IList<Reminder> list = Reminder.TryParse(@string);
				if (list != null)
				{
					this.Reminders.AddRange(list);
				}
			}
		}

		// Token: 0x0600CA06 RID: 51718 RVA: 0x002D155C File Offset: 0x002CF75C
		private void SerializeAttributes(SerializationInfo info)
		{
			List<string> list = new List<string>(this.Attributes.Keys.Count);
			foreach (object obj in this.Attributes.Keys)
			{
				string text = (string)obj;
				list.Add(text);
				info.AddValue("Attr_" + text, this.Attributes[text]);
			}
			info.AddValue("AttributeKeys", string.Join(",", list.ToArray()));
		}

		// Token: 0x0600CA07 RID: 51719 RVA: 0x002D1608 File Offset: 0x002CF808
		private void DeserializeAttributes(SerializationInfo info)
		{
			string @string = info.GetString("AttributeKeys");
			if (!string.IsNullOrEmpty(@string))
			{
				foreach (string text in @string.Split(new char[]
				{
					','
				}))
				{
					this.Attributes[text] = info.GetString("Attr_" + text);
				}
			}
		}

		// Token: 0x0600CA08 RID: 51720 RVA: 0x002D1671 File Offset: 0x002CF871
		System.ComponentModel.AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x0600CA09 RID: 51721 RVA: 0x002D167A File Offset: 0x002CF87A
		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x0600CA0A RID: 51722 RVA: 0x002D1683 File Offset: 0x002CF883
		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x0600CA0B RID: 51723 RVA: 0x002D168C File Offset: 0x002CF88C
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x0600CA0C RID: 51724 RVA: 0x002D1695 File Offset: 0x002CF895
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x0600CA0D RID: 51725 RVA: 0x002D169E File Offset: 0x002CF89E
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600CA0E RID: 51726 RVA: 0x002D16A7 File Offset: 0x002CF8A7
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600CA0F RID: 51727 RVA: 0x002D16B1 File Offset: 0x002CF8B1
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x0600CA10 RID: 51728 RVA: 0x002D16BE File Offset: 0x002CF8BE
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(new EventDescriptor[0]);
		}

		// Token: 0x0600CA11 RID: 51729 RVA: 0x002D16CC File Offset: 0x002CF8CC
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			return this.GetProperties(properties);
		}

		// Token: 0x0600CA12 RID: 51730 RVA: 0x002D16EC File Offset: 0x002CF8EC
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			return this.GetProperties(properties);
		}

		// Token: 0x0600CA13 RID: 51731 RVA: 0x002D1708 File Offset: 0x002CF908
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x0600CA14 RID: 51732 RVA: 0x002D1730 File Offset: 0x002CF930
		private PropertyDescriptorCollection GetProperties(PropertyDescriptorCollection properties)
		{
			PropertyDescriptor propertyDescriptor = properties.Find("Description", true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)obj;
				if (propertyDescriptor2 != propertyDescriptor)
				{
					list.Add(propertyDescriptor2);
				}
			}
			this.MapAttributesAndResourcesToProperties(list);
			if (list.FindAll((PropertyDescriptor prop) => prop.Name == "Description").Count == 0)
			{
				list.Add(propertyDescriptor);
			}
			if (list.FindAll((PropertyDescriptor prop) => prop.Name == "Reminder").Count == 0)
			{
				list.Add(new ReminderPropertyDescriptor());
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600CA15 RID: 51733 RVA: 0x002D1818 File Offset: 0x002CFA18
		private void MapAttributesAndResourcesToProperties(ICollection<PropertyDescriptor> properties)
		{
			if (this.Owner != null)
			{
				this.MapCommonAttributes(properties);
				this.MapCommonResources(properties);
				return;
			}
			this.MapOwnAttributes(properties);
			this.MapOwnResources(properties);
		}

		// Token: 0x0600CA16 RID: 51734 RVA: 0x002D1840 File Offset: 0x002CFA40
		private void MapCommonAttributes(ICollection<PropertyDescriptor> properties)
		{
			foreach (string propertyName in this.Owner.CustomAttributeNames)
			{
				properties.Add(new AppointmentAttributePropertyDescriptor(propertyName));
			}
		}

		// Token: 0x0600CA17 RID: 51735 RVA: 0x002D1878 File Offset: 0x002CFA78
		private void MapCommonResources(ICollection<PropertyDescriptor> properties)
		{
			foreach (object obj in this.Owner.ResourceTypes)
			{
				ResourceType resourceType = (ResourceType)obj;
				properties.Add(new AppointmentResourceTextPropertyDescriptor(resourceType.Name));
				if (string.IsNullOrEmpty(resourceType.ForeignKeyField))
				{
					properties.Add(new AppointmentResourceKeyPropertyDescriptor(resourceType.Name, resourceType.Name));
				}
				else
				{
					properties.Add(new AppointmentResourceKeyPropertyDescriptor(resourceType.ForeignKeyField, resourceType.Name));
				}
			}
		}

		// Token: 0x0600CA18 RID: 51736 RVA: 0x002D1920 File Offset: 0x002CFB20
		private void MapOwnAttributes(ICollection<PropertyDescriptor> properties)
		{
			foreach (object obj in this.Attributes.Keys)
			{
				string propertyName = (string)obj;
				properties.Add(new AppointmentAttributePropertyDescriptor(propertyName));
			}
		}

		// Token: 0x0600CA19 RID: 51737 RVA: 0x002D1984 File Offset: 0x002CFB84
		private void MapOwnResources(ICollection<PropertyDescriptor> properties)
		{
			foreach (object obj in this.Resources)
			{
				Resource resource = (Resource)obj;
				properties.Add(new AppointmentResourceTextPropertyDescriptor(resource.Type));
				ResourceType resourceType = this.Owner.ResourceTypes.FindByName(resource.Type);
				if (string.IsNullOrEmpty(resourceType.ForeignKeyField))
				{
					properties.Add(new AppointmentResourceKeyPropertyDescriptor(resource.Type, resource.Type));
				}
				else
				{
					properties.Add(new AppointmentResourceKeyPropertyDescriptor(resourceType.ForeignKeyField, resource.Type));
				}
			}
		}

		// Token: 0x0600CA1A RID: 51738 RVA: 0x002D1A3C File Offset: 0x002CFC3C
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600CA1B RID: 51739 RVA: 0x002D1A44 File Offset: 0x002CFC44
		protected virtual Appointment CreateAppointment()
		{
			if (this.Owner != null)
			{
				return this.Owner.CreateAppointment();
			}
			return new Appointment();
		}

		// Token: 0x0600CA1C RID: 51740 RVA: 0x002D1A60 File Offset: 0x002CFC60
		public virtual Appointment Clone()
		{
			Appointment appointment = this.CreateAppointment();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(appointment.GetType());
			appointment.Resources.AddRange(this.Resources);
			foreach (object obj in this.Attributes.Keys)
			{
				string key = (string)obj;
				appointment.Attributes.Add(key, this.Attributes[key]);
			}
			appointment.Owner = this.Owner;
			foreach (object obj2 in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
				if (!propertyDescriptor.Attributes.Contains(new NonSerializedInControlStateAttribute()))
				{
					propertyDescriptor.SetValue(appointment, propertyDescriptor.GetValue(this));
				}
			}
			foreach (object obj3 in this.Reminders)
			{
				Reminder reminder = (Reminder)obj3;
				appointment.Reminders.Add(reminder.Clone());
			}
			return appointment;
		}

		// Token: 0x0600CA1D RID: 51741 RVA: 0x002D1BC8 File Offset: 0x002CFDC8
		public bool IsMaster()
		{
			return this.RecurrenceState == RecurrenceState.Master;
		}

		// Token: 0x0600CA1E RID: 51742 RVA: 0x002D1BD3 File Offset: 0x002CFDD3
		public bool IsException()
		{
			return this.RecurrenceState == RecurrenceState.Exception;
		}

		// Token: 0x0600CA1F RID: 51743 RVA: 0x002D1BDE File Offset: 0x002CFDDE
		public bool IsNotRecurring()
		{
			return this.RecurrenceState == RecurrenceState.NotRecurring;
		}

		// Token: 0x0600CA20 RID: 51744 RVA: 0x002D1BE9 File Offset: 0x002CFDE9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600CA21 RID: 51745 RVA: 0x002D1BF8 File Offset: 0x002CFDF8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._controlStyle.Dispose();
			}
		}

		// Token: 0x040034F5 RID: 13557
		private Style _controlStyle;

		// Token: 0x040034F6 RID: 13558
		private System.Web.UI.AttributeCollection _attributes;

		// Token: 0x040034F7 RID: 13559
		private StateBag _attributeState;

		// Token: 0x040034F8 RID: 13560
		private ResourceCollection _resources;

		// Token: 0x040034F9 RID: 13561
		private ReminderCollection _reminders;

		// Token: 0x040034FA RID: 13562
		private RadScheduler _scheduler;

		// Token: 0x040034FB RID: 13563
		private readonly IList<AppointmentControl> _appointmentControls = new List<AppointmentControl>();

		// Token: 0x040034FC RID: 13564
		private object _dataItem;

		// Token: 0x020012C9 RID: 4809
		internal static class DataKeys
		{
			// Token: 0x040034FF RID: 13567
			public const string Subject = "Subject";

			// Token: 0x04003500 RID: 13568
			public const string Start = "Start";

			// Token: 0x04003501 RID: 13569
			public const string End = "End";

			// Token: 0x04003502 RID: 13570
			public const string RecurrenceRule = "RecurrenceRule";

			// Token: 0x04003503 RID: 13571
			public const string RecurrenceParentKey = "RecurrenceParentID";

			// Token: 0x04003504 RID: 13572
			public const string TimeZoneId = "TimeZoneID";

			// Token: 0x04003505 RID: 13573
			public const string Description = "$$Description$$";

			// Token: 0x04003506 RID: 13574
			public const string Reminders = "$$Reminders$$";
		}
	}
}
