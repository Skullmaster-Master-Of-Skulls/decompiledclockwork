using System;
using System.Text;
using a;

namespace MailBee.SmtpMail
{
	// Token: 0x02000138 RID: 312
	public class DeliveryNotificationOptions
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x0002E108 File Offset: 0x0002D108
		internal DeliveryNotificationOptions()
		{
			this.Reset();
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0002E116 File Offset: 0x0002D116
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x0002E11E File Offset: 0x0002D11E
		public string TrackingID
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0002E127 File Offset: 0x0002D127
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x0002E12F File Offset: 0x0002D12F
		public DsnNotifyCondition NotifyCondition
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0002E138 File Offset: 0x0002D138
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0002E140 File Offset: 0x0002D140
		public DsnReturnPortion ReturnPortion
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002E149 File Offset: 0x0002D149
		public void Reset()
		{
			this.a = string.Empty;
			this.b = DsnNotifyCondition.Default;
			this.c = DsnReturnPortion.Default;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002E164 File Offset: 0x0002D164
		internal string a(Encoding A_0)
		{
			string text = string.Empty;
			switch (this.c)
			{
			case DsnReturnPortion.Header:
				text += " RET=HDRS";
				break;
			case DsnReturnPortion.FullMessage:
				text += " RET=FULL";
				break;
			}
			if (this.a != null && this.a != string.Empty)
			{
				text += " ENVID=";
				text += bb.a(this.a, A_0);
			}
			return text;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002E1E8 File Offset: 0x0002D1E8
		internal string a(string A_0, Encoding A_1)
		{
			if (this.b == DsnNotifyCondition.Default)
			{
				return string.Empty;
			}
			if (this.b == DsnNotifyCondition.Never)
			{
				return " NOTIFY=NEVER";
			}
			string str = " NOTIFY=";
			bool flag = false;
			if ((this.b & DsnNotifyCondition.Success) > DsnNotifyCondition.Default)
			{
				str += "SUCCESS";
				flag = true;
			}
			if ((this.b & DsnNotifyCondition.Failure) > DsnNotifyCondition.Default)
			{
				if (flag)
				{
					str += ",";
				}
				str += "FAILURE";
				flag = true;
			}
			if ((this.b & DsnNotifyCondition.Delay) > DsnNotifyCondition.Default)
			{
				if (flag)
				{
					str += ",";
				}
				str += "DELAY";
			}
			return str + " ORCPT=rfc822;" + bb.a(A_0, A_1);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002E297 File Offset: 0x0002D297
		internal DeliveryNotificationOptions a()
		{
			return new DeliveryNotificationOptions
			{
				NotifyCondition = this.b,
				ReturnPortion = this.c,
				TrackingID = this.a
			};
		}

		// Token: 0x040007CF RID: 1999
		private string a;

		// Token: 0x040007D0 RID: 2000
		private DsnNotifyCondition b;

		// Token: 0x040007D1 RID: 2001
		private DsnReturnPortion c;
	}
}
