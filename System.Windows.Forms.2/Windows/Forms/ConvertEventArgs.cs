using System;

namespace System.Windows.Forms
{
	// Token: 0x02000173 RID: 371
	public class ConvertEventArgs : EventArgs
	{
		// Token: 0x06001383 RID: 4995 RVA: 0x0004152E File Offset: 0x0003F72E
		public ConvertEventArgs(object value, Type desiredType)
		{
			this.value = value;
			this.desiredType = desiredType;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00041544 File Offset: 0x0003F744
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x0004154C File Offset: 0x0003F74C
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00041555 File Offset: 0x0003F755
		public Type DesiredType
		{
			get
			{
				return this.desiredType;
			}
		}

		// Token: 0x0400093B RID: 2363
		private object value;

		// Token: 0x0400093C RID: 2364
		private Type desiredType;
	}
}
