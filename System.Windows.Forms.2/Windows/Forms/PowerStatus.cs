using System;

namespace System.Windows.Forms
{
	// Token: 0x02000323 RID: 803
	public class PowerStatus
	{
		// Token: 0x06003302 RID: 13058 RVA: 0x00002843 File Offset: 0x00000A43
		internal PowerStatus()
		{
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x000E3B20 File Offset: 0x000E1D20
		public PowerLineStatus PowerLineStatus
		{
			get
			{
				this.UpdateSystemPowerStatus();
				return (PowerLineStatus)this.systemPowerStatus.ACLineStatus;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000E3B33 File Offset: 0x000E1D33
		public BatteryChargeStatus BatteryChargeStatus
		{
			get
			{
				this.UpdateSystemPowerStatus();
				return (BatteryChargeStatus)this.systemPowerStatus.BatteryFlag;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000E3B46 File Offset: 0x000E1D46
		public int BatteryFullLifetime
		{
			get
			{
				this.UpdateSystemPowerStatus();
				return this.systemPowerStatus.BatteryFullLifeTime;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x000E3B5C File Offset: 0x000E1D5C
		public float BatteryLifePercent
		{
			get
			{
				this.UpdateSystemPowerStatus();
				float num = (float)this.systemPowerStatus.BatteryLifePercent / 100f;
				if (num <= 1f)
				{
					return num;
				}
				return 1f;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x000E3B91 File Offset: 0x000E1D91
		public int BatteryLifeRemaining
		{
			get
			{
				this.UpdateSystemPowerStatus();
				return this.systemPowerStatus.BatteryLifeTime;
			}
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000E3BA4 File Offset: 0x000E1DA4
		private void UpdateSystemPowerStatus()
		{
			UnsafeNativeMethods.GetSystemPowerStatus(ref this.systemPowerStatus);
		}

		// Token: 0x04001EBC RID: 7868
		private NativeMethods.SYSTEM_POWER_STATUS systemPowerStatus;
	}
}
