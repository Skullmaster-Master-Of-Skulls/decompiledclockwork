using System;

namespace log4net.Core
{
	// Token: 0x0200005C RID: 92
	public class ExceptionEvaluator : ITriggeringEventEvaluator
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000B2BA File Offset: 0x000094BA
		public ExceptionEvaluator()
		{
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000B2C2 File Offset: 0x000094C2
		public ExceptionEvaluator(Type exType, bool triggerOnSubClass)
		{
			if (exType == null)
			{
				throw new ArgumentNullException("exType");
			}
			this.m_type = exType;
			this.m_triggerOnSubclass = triggerOnSubClass;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000B2EC File Offset: 0x000094EC
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public Type ExceptionType
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000B2FD File Offset: 0x000094FD
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000B305 File Offset: 0x00009505
		public bool TriggerOnSubclass
		{
			get
			{
				return this.m_triggerOnSubclass;
			}
			set
			{
				this.m_triggerOnSubclass = value;
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B310 File Offset: 0x00009510
		public bool IsTriggeringEvent(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.m_triggerOnSubclass && loggingEvent.ExceptionObject != null)
			{
				Type type = loggingEvent.ExceptionObject.GetType();
				return type == this.m_type || type.IsSubclassOf(this.m_type);
			}
			return !this.m_triggerOnSubclass && loggingEvent.ExceptionObject != null && loggingEvent.ExceptionObject.GetType() == this.m_type;
		}

		// Token: 0x0400016C RID: 364
		private Type m_type;

		// Token: 0x0400016D RID: 365
		private bool m_triggerOnSubclass;
	}
}
