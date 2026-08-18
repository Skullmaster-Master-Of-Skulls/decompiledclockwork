using System;
using System.Globalization;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000496 RID: 1174
	internal class Com2Enum
	{
		// Token: 0x06004E6F RID: 20079 RVA: 0x00143044 File Offset: 0x00141244
		public Com2Enum(string[] names, object[] values, bool allowUnknownValues)
		{
			this.allowUnknownValues = allowUnknownValues;
			if (names == null || values == null || names.Length != values.Length)
			{
				throw new ArgumentException(SR.GetString("COM2NamesAndValuesNotEqual"));
			}
			this.PopulateArrays(names, values);
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06004E70 RID: 20080 RVA: 0x00143079 File Offset: 0x00141279
		public bool IsStrictEnum
		{
			get
			{
				return !this.allowUnknownValues;
			}
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06004E71 RID: 20081 RVA: 0x00143084 File Offset: 0x00141284
		public virtual object[] Values
		{
			get
			{
				return (object[])this.values.Clone();
			}
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06004E72 RID: 20082 RVA: 0x00143096 File Offset: 0x00141296
		public virtual string[] Names
		{
			get
			{
				return (string[])this.names.Clone();
			}
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x001430A8 File Offset: 0x001412A8
		public virtual object FromString(string s)
		{
			int num = -1;
			for (int i = 0; i < this.stringValues.Length; i++)
			{
				if (string.Compare(this.names[i], s, true, CultureInfo.InvariantCulture) == 0 || string.Compare(this.stringValues[i], s, true, CultureInfo.InvariantCulture) == 0)
				{
					return this.values[i];
				}
				if (num == -1 && string.Compare(this.names[i], s, true, CultureInfo.InvariantCulture) == 0)
				{
					num = i;
				}
			}
			if (num != -1)
			{
				return this.values[num];
			}
			if (!this.allowUnknownValues)
			{
				return null;
			}
			return s;
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x00143134 File Offset: 0x00141334
		protected virtual void PopulateArrays(string[] names, object[] values)
		{
			this.names = new string[names.Length];
			this.stringValues = new string[names.Length];
			this.values = new object[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				this.names[i] = names[i];
				this.values[i] = values[i];
				if (values[i] != null)
				{
					this.stringValues[i] = values[i].ToString();
				}
			}
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x001431A4 File Offset: 0x001413A4
		public virtual string ToString(object v)
		{
			if (v != null)
			{
				if (this.values.Length != 0 && v.GetType() != this.values[0].GetType())
				{
					try
					{
						v = Convert.ChangeType(v, this.values[0].GetType(), CultureInfo.InvariantCulture);
					}
					catch
					{
					}
				}
				string text = v.ToString();
				for (int i = 0; i < this.values.Length; i++)
				{
					if (string.Compare(this.stringValues[i], text, true, CultureInfo.InvariantCulture) == 0)
					{
						return this.names[i];
					}
				}
				if (this.allowUnknownValues)
				{
					return text;
				}
			}
			return "";
		}

		// Token: 0x0400340C RID: 13324
		private string[] names;

		// Token: 0x0400340D RID: 13325
		private object[] values;

		// Token: 0x0400340E RID: 13326
		private string[] stringValues;

		// Token: 0x0400340F RID: 13327
		private bool allowUnknownValues;
	}
}
