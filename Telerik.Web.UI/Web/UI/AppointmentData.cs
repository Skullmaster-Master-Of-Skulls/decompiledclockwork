using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A26 RID: 6694
	[DataContract]
	[Serializable]
	public class AppointmentData : IAppointmentData
	{
		// Token: 0x060103E4 RID: 66532 RVA: 0x003A107B File Offset: 0x0039F27B
		public AppointmentData()
		{
			this._visible = true;
		}

		// Token: 0x17004EB0 RID: 20144
		// (get) Token: 0x060103E5 RID: 66533 RVA: 0x003A108A File Offset: 0x0039F28A
		// (set) Token: 0x060103E6 RID: 66534 RVA: 0x003A10B8 File Offset: 0x0039F2B8
		[DataMember]
		public virtual object ID
		{
			get
			{
				if (this._id == null && !string.IsNullOrEmpty(this.EncodedID))
				{
					this._id = LosSerializer.Deserialize(this.EncodedID);
				}
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x17004EB1 RID: 20145
		// (get) Token: 0x060103E7 RID: 66535 RVA: 0x003A10C1 File Offset: 0x0039F2C1
		// (set) Token: 0x060103E8 RID: 66536 RVA: 0x003A10EF File Offset: 0x0039F2EF
		[DataMember]
		public virtual string EncodedID
		{
			get
			{
				if (string.IsNullOrEmpty(this._encodedId) && this._id != null)
				{
					this._encodedId = LosSerializer.Serialize(this._id);
				}
				return this._encodedId;
			}
			set
			{
				this._encodedId = value;
			}
		}

		// Token: 0x17004EB2 RID: 20146
		// (get) Token: 0x060103E9 RID: 66537 RVA: 0x003A10F8 File Offset: 0x0039F2F8
		// (set) Token: 0x060103EA RID: 66538 RVA: 0x003A1100 File Offset: 0x0039F300
		[DataMember]
		public virtual DateTime Start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = value;
			}
		}

		// Token: 0x17004EB3 RID: 20147
		// (get) Token: 0x060103EB RID: 66539 RVA: 0x003A1109 File Offset: 0x0039F309
		// (set) Token: 0x060103EC RID: 66540 RVA: 0x003A1111 File Offset: 0x0039F311
		[DataMember]
		public virtual DateTime End
		{
			get
			{
				return this._end;
			}
			set
			{
				this._end = value;
			}
		}

		// Token: 0x17004EB4 RID: 20148
		// (get) Token: 0x060103ED RID: 66541 RVA: 0x003A111A File Offset: 0x0039F31A
		// (set) Token: 0x060103EE RID: 66542 RVA: 0x003A1122 File Offset: 0x0039F322
		[DataMember]
		public virtual string Subject
		{
			get
			{
				return this._subject;
			}
			set
			{
				this._subject = value;
			}
		}

		// Token: 0x17004EB5 RID: 20149
		// (get) Token: 0x060103EF RID: 66543 RVA: 0x003A112B File Offset: 0x0039F32B
		// (set) Token: 0x060103F0 RID: 66544 RVA: 0x003A1133 File Offset: 0x0039F333
		[DataMember]
		public virtual string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x17004EB6 RID: 20150
		// (get) Token: 0x060103F1 RID: 66545 RVA: 0x003A113C File Offset: 0x0039F33C
		// (set) Token: 0x060103F2 RID: 66546 RVA: 0x003A1144 File Offset: 0x0039F344
		[DataMember]
		public virtual RecurrenceState RecurrenceState
		{
			get
			{
				return this._recurrenceState;
			}
			set
			{
				this._recurrenceState = value;
			}
		}

		// Token: 0x17004EB7 RID: 20151
		// (get) Token: 0x060103F3 RID: 66547 RVA: 0x003A114D File Offset: 0x0039F34D
		// (set) Token: 0x060103F4 RID: 66548 RVA: 0x003A117B File Offset: 0x0039F37B
		[DataMember]
		public virtual object RecurrenceParentID
		{
			get
			{
				if (this._recurrenceParentID == null && !string.IsNullOrEmpty(this.EncodedRecurrenceParentID))
				{
					this._recurrenceParentID = LosSerializer.Deserialize(this.EncodedRecurrenceParentID);
				}
				return this._recurrenceParentID;
			}
			set
			{
				this._recurrenceParentID = value;
			}
		}

		// Token: 0x17004EB8 RID: 20152
		// (get) Token: 0x060103F5 RID: 66549 RVA: 0x003A1184 File Offset: 0x0039F384
		// (set) Token: 0x060103F6 RID: 66550 RVA: 0x003A11B2 File Offset: 0x0039F3B2
		[DataMember]
		public virtual string EncodedRecurrenceParentID
		{
			get
			{
				if (string.IsNullOrEmpty(this._encodedeRecurrenceParentID) && this._recurrenceParentID != null)
				{
					this._encodedeRecurrenceParentID = LosSerializer.Serialize(this._recurrenceParentID);
				}
				return this._encodedeRecurrenceParentID;
			}
			set
			{
				this._encodedeRecurrenceParentID = value;
			}
		}

		// Token: 0x17004EB9 RID: 20153
		// (get) Token: 0x060103F7 RID: 66551 RVA: 0x003A11BB File Offset: 0x0039F3BB
		// (set) Token: 0x060103F8 RID: 66552 RVA: 0x003A11C3 File Offset: 0x0039F3C3
		[DataMember]
		public virtual string RecurrenceRule
		{
			get
			{
				return this._recurrenceRule;
			}
			set
			{
				this._recurrenceRule = value;
			}
		}

		// Token: 0x17004EBA RID: 20154
		// (get) Token: 0x060103F9 RID: 66553 RVA: 0x003A11CC File Offset: 0x0039F3CC
		// (set) Token: 0x060103FA RID: 66554 RVA: 0x003A11D4 File Offset: 0x0039F3D4
		[DataMember]
		public virtual bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				this._visible = value;
			}
		}

		// Token: 0x17004EBB RID: 20155
		// (get) Token: 0x060103FB RID: 66555 RVA: 0x003A11DD File Offset: 0x0039F3DD
		// (set) Token: 0x060103FC RID: 66556 RVA: 0x003A11E5 File Offset: 0x0039F3E5
		[DataMember]
		public virtual string TimeZoneID
		{
			get
			{
				return this._timeZoneId;
			}
			set
			{
				this._timeZoneId = value;
			}
		}

		// Token: 0x17004EBC RID: 20156
		// (get) Token: 0x060103FD RID: 66557 RVA: 0x003A11EE File Offset: 0x0039F3EE
		// (set) Token: 0x060103FE RID: 66558 RVA: 0x003A1209 File Offset: 0x0039F409
		[DataMember]
		public virtual IList<ResourceData> Resources
		{
			get
			{
				if (this._resources == null)
				{
					this._resources = new List<ResourceData>();
				}
				return this._resources;
			}
			set
			{
				this._resources = value;
			}
		}

		// Token: 0x17004EBD RID: 20157
		// (get) Token: 0x060103FF RID: 66559 RVA: 0x003A1212 File Offset: 0x0039F412
		// (set) Token: 0x06010400 RID: 66560 RVA: 0x003A122D File Offset: 0x0039F42D
		[DataMember]
		public virtual IDictionary<string, string> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Dictionary<string, string>();
				}
				return this._attributes;
			}
			set
			{
				this._attributes = value;
			}
		}

		// Token: 0x17004EBE RID: 20158
		// (get) Token: 0x06010401 RID: 66561 RVA: 0x003A1236 File Offset: 0x0039F436
		// (set) Token: 0x06010402 RID: 66562 RVA: 0x003A1251 File Offset: 0x0039F451
		[DataMember]
		public virtual IList<ReminderData> Reminders
		{
			get
			{
				if (this._reminders == null)
				{
					this._reminders = new List<ReminderData>();
				}
				return this._reminders;
			}
			set
			{
				this._reminders = value;
			}
		}

		// Token: 0x06010403 RID: 66563 RVA: 0x003A125C File Offset: 0x0039F45C
		public virtual void CopyFrom(Appointment srcAppointment)
		{
			this.ID = srcAppointment.ID;
			this.Start = srcAppointment.Start;
			this.End = srcAppointment.End;
			this.Subject = srcAppointment.Subject;
			this.Description = srcAppointment.Description;
			this.RecurrenceState = srcAppointment.RecurrenceState;
			this.RecurrenceParentID = srcAppointment.RecurrenceParentID;
			this.RecurrenceRule = srcAppointment.RecurrenceRule;
			this.Visible = srcAppointment.Visible;
			this.TimeZoneID = srcAppointment.TimeZoneID;
			foreach (object obj in srcAppointment.Resources)
			{
				Resource srcResource = (Resource)obj;
				ResourceData resourceData = new ResourceData();
				resourceData.CopyFrom(srcResource);
				this.Resources.Add(resourceData);
			}
			foreach (object obj2 in srcAppointment.Attributes.Keys)
			{
				string key = (string)obj2;
				this.Attributes.Add(key, srcAppointment.Attributes[key]);
			}
			foreach (object obj3 in srcAppointment.Reminders)
			{
				Reminder srcReminder = (Reminder)obj3;
				ReminderData reminderData = new ReminderData();
				reminderData.CopyFrom(srcReminder);
				this.Reminders.Add(reminderData);
			}
		}

		// Token: 0x06010404 RID: 66564 RVA: 0x003A1410 File Offset: 0x0039F610
		public virtual void CopyTo(Appointment destAppointment)
		{
			destAppointment.ID = this.ID;
			destAppointment.Start = this.Start;
			destAppointment.End = this.End;
			destAppointment.Subject = this.Subject;
			destAppointment.Description = this.Description;
			destAppointment.RecurrenceState = this.RecurrenceState;
			destAppointment.RecurrenceParentID = this.RecurrenceParentID;
			destAppointment.RecurrenceRule = this.RecurrenceRule;
			destAppointment.Visible = this.Visible;
			destAppointment.TimeZoneID = this.TimeZoneID;
			foreach (IResourceData resourceData in this.Resources)
			{
				Resource resource = new Resource();
				resourceData.CopyTo(resource);
				destAppointment.Resources.Add(resource);
			}
			foreach (string key in this.Attributes.Keys)
			{
				destAppointment.Attributes.Add(key, this.Attributes[key]);
			}
			foreach (IReminderData reminderData in this.Reminders)
			{
				Reminder reminder = new Reminder();
				reminderData.CopyTo(reminder);
				destAppointment.Reminders.Add(reminder);
			}
		}

		// Token: 0x04004939 RID: 18745
		private object _id;

		// Token: 0x0400493A RID: 18746
		private DateTime _start;

		// Token: 0x0400493B RID: 18747
		private DateTime _end;

		// Token: 0x0400493C RID: 18748
		private string _subject;

		// Token: 0x0400493D RID: 18749
		private string _description;

		// Token: 0x0400493E RID: 18750
		private string _encodedId;

		// Token: 0x0400493F RID: 18751
		private RecurrenceState _recurrenceState;

		// Token: 0x04004940 RID: 18752
		private object _recurrenceParentID;

		// Token: 0x04004941 RID: 18753
		private string _encodedeRecurrenceParentID;

		// Token: 0x04004942 RID: 18754
		private string _recurrenceRule;

		// Token: 0x04004943 RID: 18755
		private bool _visible;

		// Token: 0x04004944 RID: 18756
		private IList<ResourceData> _resources;

		// Token: 0x04004945 RID: 18757
		private IDictionary<string, string> _attributes;

		// Token: 0x04004946 RID: 18758
		private IList<ReminderData> _reminders;

		// Token: 0x04004947 RID: 18759
		private string _timeZoneId;
	}
}
