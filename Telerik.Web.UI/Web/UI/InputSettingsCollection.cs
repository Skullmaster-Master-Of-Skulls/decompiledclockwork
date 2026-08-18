using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200190E RID: 6414
	public class InputSettingsCollection : StateManagedCollection
	{
		// Token: 0x17004B37 RID: 19255
		public InputSetting this[int index]
		{
			get
			{
				return (InputSetting)((IList)this)[index];
			}
			set
			{
				((IList)this)[index] = value;
			}
		}

		// Token: 0x0600F8E6 RID: 63718 RVA: 0x003832C8 File Offset: 0x003814C8
		public int Add(InputSetting value)
		{
			return ((IList)this).Add(value);
		}

		// Token: 0x0600F8E7 RID: 63719 RVA: 0x003832D1 File Offset: 0x003814D1
		public int IndexOf(InputSetting value)
		{
			return ((IList)this).IndexOf(value);
		}

		// Token: 0x0600F8E8 RID: 63720 RVA: 0x003832DA File Offset: 0x003814DA
		public void Insert(int index, InputSetting value)
		{
			((IList)this).Insert(index, value);
		}

		// Token: 0x0600F8E9 RID: 63721 RVA: 0x003832E4 File Offset: 0x003814E4
		public void CopyTo(InputSetting[] inputSettings, int index)
		{
			base.CopyTo(inputSettings, index);
		}

		// Token: 0x0600F8EA RID: 63722 RVA: 0x003832F0 File Offset: 0x003814F0
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new TextBoxSetting();
			case 1:
				return new DateInputSetting();
			case 2:
				return new RegExpTextBoxSetting();
			case 3:
				return new NumericTextBoxSetting();
			case 4:
				return new MaskedTextBoxSetting();
			case 5:
				return new DatePickerSetting();
			default:
				throw new ArgumentOutOfRangeException("Unknown Type");
			}
		}

		// Token: 0x0600F8EB RID: 63723 RVA: 0x0038334D File Offset: 0x0038154D
		protected override Type[] GetKnownTypes()
		{
			return InputSettingsCollection.knownTypes;
		}

		// Token: 0x0600F8EC RID: 63724 RVA: 0x00383354 File Offset: 0x00381554
		public void Remove(InputSetting value)
		{
			((IList)this).Remove(value);
		}

		// Token: 0x0600F8ED RID: 63725 RVA: 0x0038335D File Offset: 0x0038155D
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x0600F8EE RID: 63726 RVA: 0x00383366 File Offset: 0x00381566
		public bool Contains(InputSetting value)
		{
			return ((IList)this).Contains(value);
		}

		// Token: 0x0600F8EF RID: 63727 RVA: 0x0038336F File Offset: 0x0038156F
		protected override void OnValidate(object value)
		{
			if (!(value is InputSetting))
			{
				throw new ArgumentException("value must be of type MyControlItem.", "value");
			}
		}

		// Token: 0x0600F8F0 RID: 63728 RVA: 0x0038338C File Offset: 0x0038158C
		protected override void SetDirtyObject(object o)
		{
			InputSetting inputSetting = o as InputSetting;
			if (inputSetting != null)
			{
				inputSetting.SetDirty();
			}
		}

		// Token: 0x040046D2 RID: 18130
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(TextBoxSetting),
			typeof(DateInputSetting),
			typeof(RegExpTextBoxSetting),
			typeof(NumericTextBoxSetting),
			typeof(MaskedTextBoxSetting),
			typeof(DatePickerSetting)
		};
	}
}
