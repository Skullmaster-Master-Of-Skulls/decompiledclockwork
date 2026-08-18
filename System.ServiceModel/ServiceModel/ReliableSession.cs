using System;
using System.ComponentModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x02000156 RID: 342
	public class ReliableSession
	{
		// Token: 0x060009E6 RID: 2534 RVA: 0x0002646D File Offset: 0x0002466D
		public ReliableSession()
		{
			this.element = new ReliableSessionBindingElement();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00026480 File Offset: 0x00024680
		public ReliableSession(ReliableSessionBindingElement reliableSessionBindingElement)
		{
			if (reliableSessionBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reliableSessionBindingElement");
			}
			this.element = reliableSessionBindingElement;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000264A2 File Offset: 0x000246A2
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x000264AF File Offset: 0x000246AF
		[DefaultValue(true)]
		public bool Ordered
		{
			get
			{
				return this.element.Ordered;
			}
			set
			{
				this.element.Ordered = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x000264BD File Offset: 0x000246BD
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x000264CA File Offset: 0x000246CA
		public TimeSpan InactivityTimeout
		{
			get
			{
				return this.element.InactivityTimeout;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.element.InactivityTimeout = value;
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002650A File Offset: 0x0002470A
		internal void CopySettings(ReliableSession copyFrom)
		{
			this.Ordered = copyFrom.Ordered;
			this.InactivityTimeout = copyFrom.InactivityTimeout;
		}

		// Token: 0x04000B99 RID: 2969
		private ReliableSessionBindingElement element;
	}
}
