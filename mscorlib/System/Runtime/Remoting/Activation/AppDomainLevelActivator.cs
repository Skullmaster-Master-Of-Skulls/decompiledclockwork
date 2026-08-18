using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020006A0 RID: 1696
	internal class AppDomainLevelActivator : IActivator
	{
		// Token: 0x06003D5A RID: 15706 RVA: 0x000D214E File Offset: 0x000D114E
		internal AppDomainLevelActivator(string remActivatorURL)
		{
			this.m_RemActivatorURL = remActivatorURL;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000D215D File Offset: 0x000D115D
		internal AppDomainLevelActivator(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_NextActivator = (IActivator)info.GetValue("m_NextActivator", typeof(IActivator));
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06003D5C RID: 15708 RVA: 0x000D2193 File Offset: 0x000D1193
		// (set) Token: 0x06003D5D RID: 15709 RVA: 0x000D219B File Offset: 0x000D119B
		public virtual IActivator NextActivator
		{
			get
			{
				return this.m_NextActivator;
			}
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x000D21A4 File Offset: 0x000D11A4
		public virtual ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000D21A8 File Offset: 0x000D11A8
		[ComVisible(true)]
		public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
		{
			ctorMsg.Activator = this.m_NextActivator;
			return ActivationServices.GetActivator().Activate(ctorMsg);
		}

		// Token: 0x04001F69 RID: 8041
		private IActivator m_NextActivator;

		// Token: 0x04001F6A RID: 8042
		private string m_RemActivatorURL;
	}
}
