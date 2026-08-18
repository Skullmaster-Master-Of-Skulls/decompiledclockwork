using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000493 RID: 1171
	[ParseChildren(true)]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PersistenceSettingsCollection : CollectionBase
	{
		// Token: 0x17000D66 RID: 3430
		public PersistenceSetting this[int index]
		{
			get
			{
				return base.List[index] as PersistenceSetting;
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x00085892 File Offset: 0x00083A92
		public int Add(PersistenceSetting setting)
		{
			return base.List.Add(setting);
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000858A0 File Offset: 0x00083AA0
		public void Remove(PersistenceSetting setting)
		{
			base.List.Remove(setting);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000858AE File Offset: 0x00083AAE
		public bool Contains(PersistenceSetting setting)
		{
			return base.List.Contains(setting);
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000858BC File Offset: 0x00083ABC
		public int IndexOf(PersistenceSetting setting)
		{
			return base.List.IndexOf(setting);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000858CA File Offset: 0x00083ACA
		public void Insert(int index, PersistenceSetting setting)
		{
			base.List.Insert(index, setting);
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000858DC File Offset: 0x00083ADC
		public void AddSetting(Control controlToPersist)
		{
			PersistenceSetting persistenceSetting = new PersistenceSetting();
			persistenceSetting.SettingType = PersistenceSettingType.ControlInstance;
			persistenceSetting.ControlInstance = controlToPersist;
			base.List.Add(persistenceSetting);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x0008590C File Offset: 0x00083B0C
		public void AddSetting(string controlID)
		{
			PersistenceSetting persistenceSetting = new PersistenceSetting();
			persistenceSetting.SettingType = PersistenceSettingType.ControlID;
			persistenceSetting.ControlID = controlID;
			base.List.Add(persistenceSetting);
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x0008593C File Offset: 0x00083B3C
		public void AddSetting(Type controlType)
		{
			PersistenceSetting persistenceSetting = new PersistenceSetting();
			persistenceSetting.SettingType = PersistenceSettingType.ControlType;
			persistenceSetting.ControlType = controlType;
			base.List.Add(persistenceSetting);
		}
	}
}
