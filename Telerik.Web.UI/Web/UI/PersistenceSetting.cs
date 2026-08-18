using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000492 RID: 1170
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class PersistenceSetting
	{
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x00085807 File Offset: 0x00083A07
		// (set) Token: 0x0600296C RID: 10604 RVA: 0x0008580F File Offset: 0x00083A0F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PersistenceSettingType SettingType
		{
			get
			{
				return this.settingType;
			}
			internal set
			{
				this.settingType = value;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x00085818 File Offset: 0x00083A18
		// (set) Token: 0x0600296E RID: 10606 RVA: 0x00085820 File Offset: 0x00083A20
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Type ControlType
		{
			get
			{
				return this.controlType;
			}
			set
			{
				this.controlType = value;
				this.SettingType = PersistenceSettingType.ControlType;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x00085830 File Offset: 0x00083A30
		// (set) Token: 0x06002970 RID: 10608 RVA: 0x00085838 File Offset: 0x00083A38
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string ControlID
		{
			get
			{
				return this.controlID;
			}
			set
			{
				this.controlID = value;
				this.SettingType = PersistenceSettingType.ControlID;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x00085848 File Offset: 0x00083A48
		// (set) Token: 0x06002972 RID: 10610 RVA: 0x00085850 File Offset: 0x00083A50
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control ControlInstance
		{
			get
			{
				return this.controlInstance;
			}
			set
			{
				this.controlInstance = value;
				this.SettingType = PersistenceSettingType.ControlInstance;
			}
		}

		// Token: 0x04000A8D RID: 2701
		private Type controlType;

		// Token: 0x04000A8E RID: 2702
		private Control controlInstance;

		// Token: 0x04000A8F RID: 2703
		private string controlID;

		// Token: 0x04000A90 RID: 2704
		private PersistenceSettingType settingType;
	}
}
