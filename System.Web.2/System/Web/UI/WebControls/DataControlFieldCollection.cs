using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B5 RID: 949
	public sealed class DataControlFieldCollection : StateManagedCollection
	{
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06002DD5 RID: 11733 RVA: 0x00095CE8 File Offset: 0x00093EE8
		// (remove) Token: 0x06002DD6 RID: 11734 RVA: 0x00095D20 File Offset: 0x00093F20
		public event EventHandler FieldsChanged;

		// Token: 0x17000D18 RID: 3352
		[Browsable(false)]
		public DataControlField this[int index]
		{
			get
			{
				return ((IList)this)[index] as DataControlField;
			}
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x00095D63 File Offset: 0x00093F63
		public void Add(DataControlField field)
		{
			((IList)this).Add(field);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x00095D70 File Offset: 0x00093F70
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

		// Token: 0x06002DDA RID: 11738 RVA: 0x00095DD0 File Offset: 0x00093FD0
		public bool Contains(DataControlField field)
		{
			return ((IList)this).Contains(field);
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x00095DD9 File Offset: 0x00093FD9
		public void CopyTo(DataControlField[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x00095DE4 File Offset: 0x00093FE4
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

		// Token: 0x06002DDD RID: 11741 RVA: 0x00095E4E File Offset: 0x0009404E
		protected override Type[] GetKnownTypes()
		{
			return DataControlFieldCollection.knownTypes;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x00095E55 File Offset: 0x00094055
		public int IndexOf(DataControlField field)
		{
			return ((IList)this).IndexOf(field);
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x00095E5E File Offset: 0x0009405E
		public void Insert(int index, DataControlField field)
		{
			((IList)this).Insert(index, field);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x00095E68 File Offset: 0x00094068
		protected override void OnClearComplete()
		{
			this.OnFieldsChanged();
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x00095E68 File Offset: 0x00094068
		private void OnFieldChanged(object sender, EventArgs e)
		{
			this.OnFieldsChanged();
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x00095E70 File Offset: 0x00094070
		private void OnFieldsChanged()
		{
			if (this.FieldsChanged != null)
			{
				this.FieldsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x00095E8C File Offset: 0x0009408C
		protected override void OnInsertComplete(int index, object value)
		{
			DataControlField dataControlField = value as DataControlField;
			if (dataControlField != null)
			{
				dataControlField.FieldChanged += this.OnFieldChanged;
			}
			this.OnFieldsChanged();
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x00095EBC File Offset: 0x000940BC
		protected override void OnRemoveComplete(int index, object value)
		{
			DataControlField dataControlField = value as DataControlField;
			if (dataControlField != null)
			{
				dataControlField.FieldChanged -= this.OnFieldChanged;
			}
			this.OnFieldsChanged();
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x00095EEB File Offset: 0x000940EB
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is DataControlField))
			{
				throw new ArgumentException(SR.GetString("DataControlFieldCollection_InvalidType"));
			}
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x00095F0C File Offset: 0x0009410C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x00095F15 File Offset: 0x00094115
		public void Remove(DataControlField field)
		{
			((IList)this).Remove(field);
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x00095F1E File Offset: 0x0009411E
		protected override void SetDirtyObject(object o)
		{
			((DataControlField)o).SetDirty();
		}

		// Token: 0x04001FB3 RID: 8115
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
