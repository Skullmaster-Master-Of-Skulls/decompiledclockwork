using System;
using System.Collections;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x0200001C RID: 28
	public class DynamicDataFieldCollection : CollectionBase
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00023B41 File Offset: 0x00021D41
		public void Add(DynamicDataField dynamicDataField)
		{
			base.List.Add(dynamicDataField);
		}

		// Token: 0x17000071 RID: 113
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

		// Token: 0x17000072 RID: 114
		public DynamicDataField this[string ControlCaption]
		{
			get
			{
				int num = this.IndexOf(ControlCaption);
				bool flag = num < 0;
				DynamicDataField result;
				if (flag)
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

		// Token: 0x0600020D RID: 525 RVA: 0x000286DC File Offset: 0x000268DC
		public int IndexOf(string ControlCaption)
		{
			string strB = ControlCaption.ToLower();
			for (int i = 0; i < base.List.Count; i++)
			{
				DynamicDataField dynamicDataField = (DynamicDataField)base.List[i];
				bool flag = dynamicDataField.ControlCaption.ToLower().CompareTo(strB) == 0;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00028748 File Offset: 0x00026948
		public bool Contains(string ControlCaption)
		{
			return this[ControlCaption] != null;
		}
	}
}
