using System;
using System.Collections;
using a.b;
using MailBee.Mime;

namespace MailBee.Outlook
{
	// Token: 0x020005BA RID: 1466
	public class PstTask : PstItem
	{
		// Token: 0x0600312F RID: 12591 RVA: 0x000E6B54 File Offset: 0x000E5B54
		internal PstTask(cv A_0) : base(A_0)
		{
			this.c = "X-Task-";
			this.b["MessageDeliveryTime"] = A_0.c6();
			this.b["ClientSubmitTime"] = A_0.eu();
			this.b["TaskStatus"] = A_0.d();
			this.b["PercentComplete"] = A_0.n();
			this.b["Importance"] = A_0.e4();
			this.b["TeamTask"] = A_0.g();
			this.b["TaskDateCompleted"] = A_0.c();
			this.b["TaskActualEffort"] = A_0.b();
			this.b["TaskEstimatedEffort"] = A_0.f();
			this.b["TaskVersion"] = A_0.a();
			this.b["TaskComplete"] = A_0.e();
			this.b["TaskOwner"] = A_0.k();
			this.b["TaskAssigner"] = A_0.m();
			this.b["TaskLastUser"] = A_0.h();
			this.b["TaskOrdinal"] = A_0.l();
			this.b["TaskFRecurring"] = A_0.i();
			this.b["TaskRole"] = A_0.p();
			this.b["TaskOwnership"] = A_0.j();
			this.b["AcceptanceState"] = A_0.o();
			this.b["TaskSubject"] = A_0.dz();
			this.b["TaskBody"] = A_0.d6();
			this.b["TaskStartDate"] = A_0.co();
			this.b["TaskDueDate"] = A_0.da();
			string[] array = A_0.g9();
			if (array.Length != 0)
			{
				string text = string.Empty;
				foreach (string text2 in array)
				{
					text += ((text != string.Empty) ? (";" + text2) : text2);
				}
				this.b["Categories"] = text;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x000E6E1D File Offset: 0x000E5E1D
		public override PstItemType PstType
		{
			get
			{
				return base.PstType;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x000E6E25 File Offset: 0x000E5E25
		public override Hashtable PstFields
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000E6E30 File Offset: 0x000E5E30
		public override MailMessage GetAsMailMessage()
		{
			MailMessage a_ = new MailMessage();
			return base.a(a_);
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06003133 RID: 12595 RVA: 0x000E6E4A File Offset: 0x000E5E4A
		public override int PstID
		{
			get
			{
				return base.PstID;
			}
		}
	}
}
