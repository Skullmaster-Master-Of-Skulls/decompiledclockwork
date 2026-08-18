using System;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000011 RID: 17
	public sealed class ApplicationPoolRecycling : ConfigurationElement
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00004511 File Offset: 0x00003511
		internal ApplicationPoolRecycling()
		{
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004519 File Offset: 0x00003519
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000452B File Offset: 0x0000352B
		public bool DisallowOverlappingRotation
		{
			get
			{
				return (bool)base.GetAttributeValue("disallowOverlappingRotation");
			}
			set
			{
				base["disallowOverlappingRotation"] = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000453E File Offset: 0x0000353E
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004550 File Offset: 0x00003550
		public bool DisallowRotationOnConfigChange
		{
			get
			{
				return (bool)base.GetAttributeValue("disallowRotationOnConfigChange");
			}
			set
			{
				base["disallowRotationOnConfigChange"] = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004563 File Offset: 0x00003563
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004575 File Offset: 0x00003575
		public RecyclingLogEventOnRecycle LogEventOnRecycle
		{
			get
			{
				return (RecyclingLogEventOnRecycle)base.GetAttributeValue("logEventOnRecycle");
			}
			set
			{
				base["logEventOnRecycle"] = (int)value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004588 File Offset: 0x00003588
		public ApplicationPoolPeriodicRestart PeriodicRestart
		{
			get
			{
				if (this._periodicRestart == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("periodicRestart");
					this._periodicRestart = new ApplicationPoolPeriodicRestart();
					this._periodicRestart.Initialize(base.Configuration, elementByName);
				}
				return this._periodicRestart;
			}
		}

		// Token: 0x04000029 RID: 41
		private ApplicationPoolPeriodicRestart _periodicRestart;
	}
}
