using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000342 RID: 834
	internal class RelatedPropertyManager : PropertyManager
	{
		// Token: 0x060035D8 RID: 13784 RVA: 0x000F3773 File Offset: 0x000F1973
		internal RelatedPropertyManager(BindingManagerBase parentManager, string dataField) : base(RelatedPropertyManager.GetCurrentOrNull(parentManager), dataField)
		{
			this.Bind(parentManager, dataField);
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000F378C File Offset: 0x000F198C
		private void Bind(BindingManagerBase parentManager, string dataField)
		{
			this.parentManager = parentManager;
			this.dataField = dataField;
			this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
			if (this.fieldInfo == null)
			{
				throw new ArgumentException(SR.GetString("RelatedListManagerChild", new object[]
				{
					dataField
				}));
			}
			parentManager.CurrentItemChanged += this.ParentManager_CurrentItemChanged;
			this.Refresh();
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000F37F4 File Offset: 0x000F19F4
		internal override string GetListName()
		{
			string listName = this.GetListName(new ArrayList());
			if (listName.Length > 0)
			{
				return listName;
			}
			return base.GetListName();
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000F381E File Offset: 0x000F1A1E
		protected internal override string GetListName(ArrayList listAccessors)
		{
			listAccessors.Insert(0, this.fieldInfo);
			return this.parentManager.GetListName(listAccessors);
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000F383C File Offset: 0x000F1A3C
		internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			PropertyDescriptor[] array;
			if (listAccessors != null && listAccessors.Length != 0)
			{
				array = new PropertyDescriptor[listAccessors.Length + 1];
				listAccessors.CopyTo(array, 1);
			}
			else
			{
				array = new PropertyDescriptor[1];
			}
			array[0] = this.fieldInfo;
			return this.parentManager.GetItemProperties(array);
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000F3881 File Offset: 0x000F1A81
		private void ParentManager_CurrentItemChanged(object sender, EventArgs e)
		{
			this.Refresh();
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000F3889 File Offset: 0x000F1A89
		private void Refresh()
		{
			this.EndCurrentEdit();
			this.SetDataSource(RelatedPropertyManager.GetCurrentOrNull(this.parentManager));
			this.OnCurrentChanged(EventArgs.Empty);
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x000F38AD File Offset: 0x000F1AAD
		internal override Type BindType
		{
			get
			{
				return this.fieldInfo.PropertyType;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x000F38BA File Offset: 0x000F1ABA
		public override object Current
		{
			get
			{
				if (this.DataSource == null)
				{
					return null;
				}
				return this.fieldInfo.GetValue(this.DataSource);
			}
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000F38D8 File Offset: 0x000F1AD8
		private static object GetCurrentOrNull(BindingManagerBase parentManager)
		{
			if (parentManager.Position < 0 || parentManager.Position >= parentManager.Count)
			{
				return null;
			}
			return parentManager.Current;
		}

		// Token: 0x04001F72 RID: 8050
		private BindingManagerBase parentManager;

		// Token: 0x04001F73 RID: 8051
		private string dataField;

		// Token: 0x04001F74 RID: 8052
		private PropertyDescriptor fieldInfo;
	}
}
