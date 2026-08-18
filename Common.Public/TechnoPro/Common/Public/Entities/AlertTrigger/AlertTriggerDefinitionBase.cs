using System;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x020005A0 RID: 1440
	[Serializable]
	public class AlertTriggerDefinitionBase : ICloneable<AlertTriggerDefinitionBase>, ICloneable, IAlertTriggerDefinitionBase, IAlertTriggerDefinitionCommon
	{
		// Token: 0x06002EBF RID: 11967 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AlertTriggerDefinitionBase()
		{
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x00033772 File Offset: 0x00031972
		public AlertTriggerDefinitionBase(AlertTriggerDefinitionBase item)
		{
			AlertTriggerDefinitionBase.Clone(item, this);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x00033784 File Offset: 0x00031984
		private static void Clone(IAlertTriggerDefinitionCommon itemSource, IAlertTriggerDefinitionCommon itemDest)
		{
			bool flag = itemDest == null;
			if (!flag)
			{
				itemDest.Name = itemSource.Name;
				itemDest.IsDisabled = itemSource.IsDisabled;
				itemDest.OrderNum = itemSource.OrderNum;
				itemDest.Note = itemSource.Note;
				itemDest.DontAllowAppointmentBooking = itemSource.DontAllowAppointmentBooking;
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x000337DD File Offset: 0x000319DD
		// (set) Token: 0x06002EC3 RID: 11971 RVA: 0x000337E5 File Offset: 0x000319E5
		public bool DontAllowAppointmentBooking { get; set; }

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000337EE File Offset: 0x000319EE
		// (set) Token: 0x06002EC5 RID: 11973 RVA: 0x000337F6 File Offset: 0x000319F6
		public string Name { get; set; }

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06002EC6 RID: 11974 RVA: 0x000337FF File Offset: 0x000319FF
		// (set) Token: 0x06002EC7 RID: 11975 RVA: 0x00033807 File Offset: 0x00031A07
		public bool IsDisabled { get; set; }

		// Token: 0x06002EC8 RID: 11976 RVA: 0x00033810 File Offset: 0x00031A10
		public T Clone<T>() where T : IAlertTriggerDefinitionCommon
		{
			T t = Activator.CreateInstance<T>();
			AlertTriggerDefinitionBase.Clone(this, t);
			return t;
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x00033836 File Offset: 0x00031A36
		// (set) Token: 0x06002ECA RID: 11978 RVA: 0x0003383E File Offset: 0x00031A3E
		public int OrderNum { get; set; }

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x00033847 File Offset: 0x00031A47
		// (set) Token: 0x06002ECC RID: 11980 RVA: 0x0003384F File Offset: 0x00031A4F
		public string Note { get; set; }

		// Token: 0x06002ECD RID: 11981 RVA: 0x00033858 File Offset: 0x00031A58
		public AlertTriggerDefinitionBase Clone()
		{
			return new AlertTriggerDefinitionBase(this);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x00033870 File Offset: 0x00031A70
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
