using System;
using System.Collections;

namespace DynamicScreens
{
	// Token: 0x0200005B RID: 91
	public class DynamicDataFieldCollection : CollectionBase
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x000407EE File Offset: 0x0003F7EE
		public void Add(DynamicDataField dynamicDataField)
		{
			base.List.Add(dynamicDataField);
		}

		// Token: 0x17000162 RID: 354
		public DynamicDataField this[int index]
		{
			get
			{
				return (DynamicDataField)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x17000163 RID: 355
		public DynamicDataField this[string ControlCaption]
		{
			get
			{
				int num = this.IndexOf(ControlCaption);
				DynamicDataField result;
				if (num < 0)
				{
					result = null;
				}
				else
				{
					result = this[num];
				}
				return result;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00040864 File Offset: 0x0003F864
		public int IndexOf(string ControlCaption)
		{
			string strB = ControlCaption.ToLower();
			for (int i = 0; i < base.List.Count; i++)
			{
				DynamicDataField dynamicDataField = (DynamicDataField)base.List[i];
				if (dynamicDataField.ControlCaption.ToLower().CompareTo(strB) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000408D0 File Offset: 0x0003F8D0
		public bool Contains(string ControlCaption)
		{
			return this[ControlCaption] != null;
		}
	}
}
