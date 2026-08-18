using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FCB RID: 4043
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class AjaxUpdatedControlsCollection : CollectionBase
	{
		// Token: 0x170031B4 RID: 12724
		public AjaxUpdatedControl this[int index]
		{
			get
			{
				return base.List[index] as AjaxUpdatedControl;
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06009CF9 RID: 40185 RVA: 0x0022EFF0 File Offset: 0x0022D1F0
		internal string SerializeToJavascript(RadAjaxManager manager)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			int num = base.Count;
			foreach (object obj in this)
			{
				AjaxUpdatedControl ajaxUpdatedControl = (AjaxUpdatedControl)obj;
				stringBuilder.AppendFormat("{0}", ajaxUpdatedControl.SerializeToJavascript(manager));
				if (num > 1)
				{
					stringBuilder.Append(",");
				}
				num--;
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06009CFA RID: 40186 RVA: 0x0022F094 File Offset: 0x0022D294
		public int Add(AjaxUpdatedControl newItem)
		{
			int num = this.IndexOf(newItem);
			if (num != -1)
			{
				base.RemoveAt(num);
			}
			return base.List.Add(newItem);
		}

		// Token: 0x06009CFB RID: 40187 RVA: 0x0022F0C0 File Offset: 0x0022D2C0
		public void Remove(AjaxUpdatedControl updatedControl)
		{
			base.List.Remove(updatedControl);
		}

		// Token: 0x06009CFC RID: 40188 RVA: 0x0022F0CE File Offset: 0x0022D2CE
		public bool Contains(AjaxUpdatedControl updatedControl)
		{
			return base.List.Contains(updatedControl);
		}

		// Token: 0x06009CFD RID: 40189 RVA: 0x0022F0DC File Offset: 0x0022D2DC
		public int IndexOf(AjaxUpdatedControl updatedControl)
		{
			return base.List.IndexOf(updatedControl);
		}

		// Token: 0x06009CFE RID: 40190 RVA: 0x0022F0EA File Offset: 0x0022D2EA
		public void Insert(int index, AjaxUpdatedControl updatedControl)
		{
			base.List.Insert(index, updatedControl);
		}

		// Token: 0x06009CFF RID: 40191 RVA: 0x0022F0FC File Offset: 0x0022D2FC
		public void AddRange(AjaxUpdatedControlsCollection controls)
		{
			foreach (object obj in controls)
			{
				AjaxUpdatedControl newItem = (AjaxUpdatedControl)obj;
				this.Add(newItem);
			}
		}
	}
}
