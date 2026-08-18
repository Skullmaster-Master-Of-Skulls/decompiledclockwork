using System;
using System.Collections;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace ClockWorkAPI
{
	// Token: 0x02000004 RID: 4
	public class Icons : CollectionBase
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00001060
		public int Add(Icon icon)
		{
			return base.List.Add(icon);
		}

		// Token: 0x17000001 RID: 1
		public Icon this[int index]
		{
			get
			{
				return (Icon)base.List[index];
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A3 File Offset: 0x000010A3
		public Icons()
		{
			this.importantIconIds = new int[0];
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020BA File Offset: 0x000010BA
		public Icons(int[] importantIconIds)
		{
			this.importantIconIds = importantIconIds;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020CC File Offset: 0x000010CC
		public bool Contains(Icon iconToFind)
		{
			foreach (object obj in base.List)
			{
				Icon icon = (Icon)obj;
				if (icon.IconID == iconToFind.IconID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002150 File Offset: 0x00001150
		public void Sort(IComparer Comparer)
		{
			base.InnerList.Sort(Comparer);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002160 File Offset: 0x00001160
		public void SortByImportantIcons()
		{
			if (this.importantIconIds != null && this.importantIconIds.Length > 0)
			{
				this.Sort(new ImportantIconComparer(this.importantIconIds));
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021A0 File Offset: 0x000011A0
		public ArrayList IconsArrayList
		{
			get
			{
				ArrayList arrayList = new ArrayList(base.List.Count);
				foreach (object obj in base.List)
				{
					Icon value = (Icon)obj;
					arrayList.Add(value);
				}
				return arrayList;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002224 File Offset: 0x00001224
		public List<AppointmentIconDTO> GetIconsList()
		{
			List<AppointmentIconDTO> list = new List<AppointmentIconDTO>(base.List.Count);
			foreach (object obj in base.List)
			{
				Icon icon = (Icon)obj;
				AppointmentIconDTO appointmentIconDTO = new AppointmentIconDTO();
				appointmentIconDTO.Icon = new IconInfoDTO();
				appointmentIconDTO.Icon.IconNum = icon.IconID;
				appointmentIconDTO.Icon.IconText = icon.IconText;
				appointmentIconDTO.Icon.IconLetterIdentifier = icon.IconLetterIdentifier.ToString();
				if (icon.ScreenNum > 0)
				{
					appointmentIconDTO.Screen = new DynamicFormDTO();
					appointmentIconDTO.Screen.ScreenNum = icon.ScreenNum;
				}
				list.Add(appointmentIconDTO);
			}
			return list;
		}

		// Token: 0x04000001 RID: 1
		private int[] importantIconIds;
	}
}
