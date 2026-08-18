using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F8E RID: 3982
	[DataContract]
	[Serializable]
	public class ReminderData : IReminderData
	{
		// Token: 0x17003037 RID: 12343
		// (get) Token: 0x06009873 RID: 39027 RVA: 0x002213D5 File Offset: 0x0021F5D5
		// (set) Token: 0x06009874 RID: 39028 RVA: 0x002213DD File Offset: 0x0021F5DD
		[DataMember]
		public virtual string ID { get; set; }

		// Token: 0x17003038 RID: 12344
		// (get) Token: 0x06009875 RID: 39029 RVA: 0x002213E6 File Offset: 0x0021F5E6
		// (set) Token: 0x06009876 RID: 39030 RVA: 0x002213EE File Offset: 0x0021F5EE
		[DataMember]
		public virtual int TriggerMinutes { get; set; }

		// Token: 0x17003039 RID: 12345
		// (get) Token: 0x06009877 RID: 39031 RVA: 0x002213F7 File Offset: 0x0021F5F7
		// (set) Token: 0x06009878 RID: 39032 RVA: 0x00221412 File Offset: 0x0021F612
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

		// Token: 0x06009879 RID: 39033 RVA: 0x0022141C File Offset: 0x0021F61C
		public virtual void CopyFrom(Reminder srcReminder)
		{
			this.ID = srcReminder.ID;
			this.TriggerMinutes = (int)srcReminder.Trigger.TotalMinutes;
			foreach (object obj in srcReminder.Attributes.Keys)
			{
				string key = (string)obj;
				this.Attributes.Add(key, srcReminder.Attributes[key]);
			}
		}

		// Token: 0x0600987A RID: 39034 RVA: 0x002214AC File Offset: 0x0021F6AC
		public virtual void CopyTo(Reminder destReminder)
		{
			destReminder.ID = this.ID;
			destReminder.Trigger = TimeSpan.FromMinutes((double)this.TriggerMinutes);
			foreach (string key in this.Attributes.Keys)
			{
				destReminder.Attributes.Add(key, this.Attributes[key]);
			}
		}

		// Token: 0x04002B83 RID: 11139
		private IDictionary<string, string> _attributes;
	}
}
