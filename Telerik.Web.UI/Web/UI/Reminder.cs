using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000F9A RID: 3994
	public class Reminder : StateManager, ICloneable
	{
		// Token: 0x1700306C RID: 12396
		// (get) Token: 0x060098EC RID: 39148 RVA: 0x00221A9C File Offset: 0x0021FC9C
		// (set) Token: 0x060098ED RID: 39149 RVA: 0x00221AC1 File Offset: 0x0021FCC1
		public TimeSpan Trigger
		{
			get
			{
				return (TimeSpan)(base.ViewState["Trigger"] ?? TimeSpan.MaxValue);
			}
			set
			{
				base.ViewState["Trigger"] = value;
			}
		}

		// Token: 0x1700306D RID: 12397
		// (get) Token: 0x060098EE RID: 39150 RVA: 0x00221AD9 File Offset: 0x0021FCD9
		// (set) Token: 0x060098EF RID: 39151 RVA: 0x00221AF9 File Offset: 0x0021FCF9
		public string ID
		{
			get
			{
				return (string)(base.ViewState["ID"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ID"] = value;
			}
		}

		// Token: 0x1700306E RID: 12398
		// (get) Token: 0x060098F0 RID: 39152 RVA: 0x00221B0C File Offset: 0x0021FD0C
		[NonSerializedInControlState]
		[Browsable(false)]
		[ScriptIgnore]
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

		// Token: 0x060098F1 RID: 39153 RVA: 0x00221B2D File Offset: 0x0021FD2D
		public Reminder()
		{
		}

		// Token: 0x060098F2 RID: 39154 RVA: 0x00221B38 File Offset: 0x0021FD38
		public Reminder(TimeSpan trigger) : this(trigger, Guid.NewGuid().ToString())
		{
		}

		// Token: 0x060098F3 RID: 39155 RVA: 0x00221B60 File Offset: 0x0021FD60
		public Reminder(int triggerMinutes) : this(TimeSpan.FromMinutes((double)triggerMinutes), Guid.NewGuid().ToString())
		{
		}

		// Token: 0x060098F4 RID: 39156 RVA: 0x00221B8D File Offset: 0x0021FD8D
		public Reminder(TimeSpan trigger, string id)
		{
			this.Trigger = trigger;
			this.ID = id;
		}

		// Token: 0x060098F5 RID: 39157 RVA: 0x00221BA4 File Offset: 0x0021FDA4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("BEGIN:VALARM\r\n", new object[0]);
			stringBuilder.AppendFormat("TRIGGER:-PT{0}M\r\n", this.Trigger.TotalMinutes);
			foreach (object obj in this.Attributes.Keys)
			{
				string text = (string)obj;
				stringBuilder.AppendFormat("X-TELERIK-ATTRIBUTE;NAME={0};VALUE={1}\r\n", text, this.Attributes[text]);
			}
			stringBuilder.AppendFormat("X-TELERIK-UID:{0}\r\n", this.ID);
			stringBuilder.AppendFormat("END:VALARM\r\n", new object[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x060098F6 RID: 39158 RVA: 0x00221C78 File Offset: 0x0021FE78
		public static IList<Reminder> TryParse(string input)
		{
			IList<Reminder> result;
			Reminder.TryParse(input, out result);
			return result;
		}

		// Token: 0x060098F7 RID: 39159 RVA: 0x00221C90 File Offset: 0x0021FE90
		public static bool TryParse(string input, out IList<Reminder> parsedReminders)
		{
			if (string.IsNullOrEmpty(input))
			{
				parsedReminders = null;
				return false;
			}
			parsedReminders = new List<Reminder>();
			Reminder reminder = null;
			input = input.Trim();
			foreach (string text in input.Split(new char[]
			{
				'\n'
			}))
			{
				string input2 = text.Trim();
				Match match = Regex.Match(input2, "BEGIN\\s*:\\s*VALARM", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					reminder = new Reminder();
				}
				if (reminder != null)
				{
					Match match2 = Regex.Match(input2, "TRIGGER:-PT([0-9][0-9]*)M", RegexOptions.IgnoreCase);
					if (match2.Success)
					{
						reminder.Trigger = TimeSpan.FromMinutes((double)int.Parse(match2.Groups[1].Value));
					}
					Match match3 = Regex.Match(input2, "^(X-TELERIK-ATTRIBUTE;)(.*)$", RegexOptions.IgnoreCase);
					if (match3.Success)
					{
						string value = match3.Groups[2].Value;
						Match match4 = Regex.Match(value, "NAME=(.*);", RegexOptions.IgnoreCase);
						if (match4.Success)
						{
							string value2 = match4.Groups[1].Value;
							Match match5 = Regex.Match(value, "VALUE=(.*)", RegexOptions.IgnoreCase);
							if (match5.Success)
							{
								string value3 = match5.Groups[1].Value;
								reminder.Attributes.Add(value2, value3);
							}
						}
					}
					Match match6 = Regex.Match(input2, "X-TELERIK-UID:(.*)$", RegexOptions.IgnoreCase);
					if (match6.Success)
					{
						reminder.ID = match6.Groups[1].Value;
					}
					Match match7 = Regex.Match(input2, "END\\s*:\\s*VALARM", RegexOptions.IgnoreCase);
					if (match7.Success)
					{
						if (!reminder.IsValid())
						{
							parsedReminders = null;
							return false;
						}
						parsedReminders.Add(reminder);
						reminder = null;
					}
				}
			}
			if (parsedReminders.Count > 0)
			{
				return true;
			}
			parsedReminders = null;
			return false;
		}

		// Token: 0x060098F8 RID: 39160 RVA: 0x00221E5B File Offset: 0x0022005B
		private bool IsValid()
		{
			return !string.IsNullOrEmpty(this.ID) && !(this.Trigger == TimeSpan.MaxValue);
		}

		// Token: 0x1700306F RID: 12399
		// (get) Token: 0x060098F9 RID: 39161 RVA: 0x00221E81 File Offset: 0x00220081
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

		// Token: 0x060098FA RID: 39162 RVA: 0x00221EAF File Offset: 0x002200AF
		internal override void SetDirty()
		{
			base.SetDirty();
			this.AttributeState.SetDirty(true);
		}

		// Token: 0x060098FB RID: 39163 RVA: 0x00221EC4 File Offset: 0x002200C4
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.AttributeState).LoadViewState(array[1]);
		}

		// Token: 0x060098FC RID: 39164 RVA: 0x00221EF0 File Offset: 0x002200F0
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.AttributeState).SaveViewState()
			}.ToArray();
		}

		// Token: 0x060098FD RID: 39165 RVA: 0x00221F28 File Offset: 0x00220128
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._attributeState != null)
			{
				((IStateManager)this._attributeState).TrackViewState();
			}
		}

		// Token: 0x060098FE RID: 39166 RVA: 0x00221F43 File Offset: 0x00220143
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060098FF RID: 39167 RVA: 0x00221F4C File Offset: 0x0022014C
		public virtual Reminder Clone()
		{
			Reminder reminder = new Reminder(this.Trigger, this.ID);
			foreach (object obj in this.Attributes.Keys)
			{
				string key = (string)obj;
				reminder.Attributes.Add(key, this.Attributes[key]);
			}
			return reminder;
		}

		// Token: 0x04002B90 RID: 11152
		private System.Web.UI.AttributeCollection _attributes;

		// Token: 0x04002B91 RID: 11153
		private StateBag _attributeState;
	}
}
