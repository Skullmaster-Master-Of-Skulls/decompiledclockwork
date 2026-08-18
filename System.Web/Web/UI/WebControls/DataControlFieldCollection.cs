using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200052F RID: 1327
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataControlFieldCollection : StateManagedCollection
	{
		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06004136 RID: 16694 RVA: 0x0010ED46 File Offset: 0x0010DD46
		// (remove) Token: 0x06004137 RID: 16695 RVA: 0x0010ED5F File Offset: 0x0010DD5F
		public event EventHandler FieldsChanged;

		// Token: 0x17000FB5 RID: 4021
		[Browsable(false)]
		public DataControlField this[int index]
		{
			get
			{
				return ((IList)this)[index] as DataControlField;
			}
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x0010ED86 File Offset: 0x0010DD86
		public void Add(DataControlField field)
		{
			((IList)this).Add(field);
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x0010ED90 File Offset: 0x0010DD90
		public DataControlFieldCollection CloneFields()
		{
			DataControlFieldCollection dataControlFieldCollection = new DataControlFieldCollection();
			foreach (object obj in this)
			{
				DataControlField dataControlField = (DataControlField)obj;
				dataControlFieldCollection.Add(dataControlField.CloneField());
			}
			return dataControlFieldCollection;
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x0010EDF0 File Offset: 0x0010DDF0
		public bool Contains(DataControlField field)
		{
			return ((IList)this).Contains(field);
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x0010EDF9 File Offset: 0x0010DDF9
		public void CopyTo(DataControlField[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x0010EE04 File Offset: 0x0010DE04
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new BoundField();
			case 1:
				return new ButtonField();
			case 2:
				return new CheckBoxField();
			case 3:
				return new CommandField();
			case 4:
				return new HyperLinkField();
			case 5:
				return new ImageField();
			case 6:
				return new TemplateField();
			default:
				throw new ArgumentOutOfRangeException(SR.GetString("DataControlFieldCollection_InvalidTypeIndex"));
			}
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x0010EE70 File Offset: 0x0010DE70
		protected override Type[] GetKnownTypes()
		{
			return DataControlFieldCollection.knownTypes;
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x0010EE77 File Offset: 0x0010DE77
		public int IndexOf(DataControlField field)
		{
			return ((IList)this).IndexOf(field);
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x0010EE80 File Offset: 0x0010DE80
		public void Insert(int index, DataControlField field)
		{
			((IList)this).Insert(index, field);
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x0010EE8A File Offset: 0x0010DE8A
		protected override void OnClearComplete()
		{
			this.OnFieldsChanged();
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x0010EE92 File Offset: 0x0010DE92
		private void OnFieldChanged(object sender, EventArgs e)
		{
			this.OnFieldsChanged();
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x0010EE9A File Offset: 0x0010DE9A
		private void OnFieldsChanged()
		{
			if (this.FieldsChanged != null)
			{
				this.FieldsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x0010EEB8 File Offset: 0x0010DEB8
		protected override void OnInsertComplete(int index, object value)
		{
			DataControlField dataControlField = value as DataControlField;
			if (dataControlField != null)
			{
				dataControlField.FieldChanged += this.OnFieldChanged;
			}
			this.OnFieldsChanged();
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x0010EEE8 File Offset: 0x0010DEE8
		protected override void OnRemoveComplete(int index, object value)
		{
			DataControlField dataControlField = value as DataControlField;
			if (dataControlField != null)
			{
				dataControlField.FieldChanged -= this.OnFieldChanged;
			}
			this.OnFieldsChanged();
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x0010EF17 File Offset: 0x0010DF17
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is DataControlField))
			{
				throw new ArgumentException(SR.GetString("DataControlFieldCollection_InvalidType"));
			}
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x0010EF38 File Offset: 0x0010DF38
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x0010EF41 File Offset: 0x0010DF41
		public void Remove(DataControlField field)
		{
			((IList)this).Remove(field);
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x0010EF4A File Offset: 0x0010DF4A
		protected override void SetDirtyObject(object o)
		{
			((DataControlField)o).SetDirty();
		}

		// Token: 0x040028AD RID: 10413
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(BoundField),
			typeof(ButtonField),
			typeof(CheckBoxField),
			typeof(CommandField),
			typeof(HyperLinkField),
			typeof(ImageField),
			typeof(TemplateField)
		};
	}
}
