using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200030C RID: 780
	internal class ListViewGroupCollectionEditor : CollectionEditor
	{
		// Token: 0x06001EDC RID: 7900 RVA: 0x00023ABB File Offset: 0x00021CBB
		public ListViewGroupCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000B89C4 File Offset: 0x000B6BC4
		protected override object CreateInstance(Type itemType)
		{
			ListViewGroup listViewGroup = (ListViewGroup)base.CreateInstance(itemType);
			listViewGroup.Name = this.CreateListViewGroupName((ListViewGroupCollection)this.editValue);
			return listViewGroup;
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000B89F8 File Offset: 0x000B6BF8
		private string CreateListViewGroupName(ListViewGroupCollection lvgCollection)
		{
			string text = "ListViewGroup";
			INameCreationService nameCreationService = base.GetService(typeof(INameCreationService)) as INameCreationService;
			IContainer container = base.GetService(typeof(IContainer)) as IContainer;
			if (nameCreationService != null && container != null)
			{
				text = nameCreationService.CreateName(container, typeof(ListViewGroup));
			}
			while (char.IsDigit(text[text.Length - 1]))
			{
				text = text.Substring(0, text.Length - 1);
			}
			int num = 1;
			string text2 = text + num.ToString(CultureInfo.CurrentCulture);
			while (lvgCollection[text2] != null)
			{
				num++;
				text2 = text + num.ToString(CultureInfo.CurrentCulture);
			}
			return text2;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000B8AB4 File Offset: 0x000B6CB4
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.editValue = value;
			object result = base.EditValue(context, provider, value);
			this.editValue = null;
			return result;
		}

		// Token: 0x040017DF RID: 6111
		private object editValue;
	}
}
