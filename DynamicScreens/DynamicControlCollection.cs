using System;
using System.Collections;

namespace DynamicScreens
{
	// Token: 0x02000070 RID: 112
	public class DynamicControlCollection : CollectionBase
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x00042B71 File Offset: 0x00041B71
		public DynamicControlCollection()
		{
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00042B7C File Offset: 0x00041B7C
		public DynamicControlCollection(string s)
		{
			string[] array = s.Split(new char[]
			{
				'`'
			});
			for (int i = 0; i < array.Length; i++)
			{
				this.Add(new DynamicControl(array[i])
				{
					ControlId = -(i + 1)
				});
			}
		}

		// Token: 0x170001A1 RID: 417
		public DynamicControl this[int index]
		{
			get
			{
				return (DynamicControl)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00042C0C File Offset: 0x00041C0C
		public int Add(DynamicControl dc)
		{
			return base.List.Add(dc);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00042C2C File Offset: 0x00041C2C
		public int AddNoDuplicates(DynamicControl dc)
		{
			int result;
			if (this.Find(dc.ControlId) == null)
			{
				result = base.List.Add(dc);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00042C64 File Offset: 0x00041C64
		public string[] GetControlCaptionArraySorted()
		{
			string[] array = new string[base.List.Count];
			for (int i = 0; i < base.List.Count; i++)
			{
				array[i] = ((DynamicControl)base.List[i]).ControlCaption;
			}
			Array.Sort<string>(array);
			return array;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00042CC4 File Offset: 0x00041CC4
		public void Remove(DynamicControl dc)
		{
			if (base.List.Contains(dc))
			{
				base.List.Remove(dc);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00042CF4 File Offset: 0x00041CF4
		public void Remove(int ControlId)
		{
			int num = this.FindIndex(ControlId);
			if (num >= 0)
			{
				base.List.RemoveAt(num);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00042D20 File Offset: 0x00041D20
		public DynamicControl Find(int ControlId)
		{
			int num = this.FindIndex(ControlId);
			return (num >= 0) ? ((DynamicControl)base.List[num]) : null;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00042D54 File Offset: 0x00041D54
		public int FindIndex(int ControlId)
		{
			for (int i = 0; i < base.List.Count; i++)
			{
				DynamicControl dynamicControl = (DynamicControl)base.List[i];
				if (dynamicControl.ControlId == ControlId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00042DA9 File Offset: 0x00041DA9
		public void Insert(int index, DynamicControl dynamicControl)
		{
			base.List.Insert(index, dynamicControl);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00042DBC File Offset: 0x00041DBC
		public bool Contains(DynamicControl dynamicControl)
		{
			return base.List.Contains(dynamicControl);
		}
	}
}
