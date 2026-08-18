using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020002B8 RID: 696
	public class KeysConverter : TypeConverter, IComparer
	{
		// Token: 0x06002A97 RID: 10903 RVA: 0x000C03BA File Offset: 0x000BE5BA
		private void AddKey(string key, Keys value)
		{
			this.keyNames[key] = value;
			this.displayOrder.Add(key);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000C03DC File Offset: 0x000BE5DC
		private void Initialize()
		{
			this.keyNames = new Hashtable(34);
			this.displayOrder = new List<string>(34);
			this.AddKey(SR.GetString("toStringEnter"), Keys.Return);
			this.AddKey("F12", Keys.F12);
			this.AddKey("F11", Keys.F11);
			this.AddKey("F10", Keys.F10);
			this.AddKey(SR.GetString("toStringEnd"), Keys.End);
			this.AddKey(SR.GetString("toStringControl"), Keys.Control);
			this.AddKey("F8", Keys.F8);
			this.AddKey("F9", Keys.F9);
			this.AddKey(SR.GetString("toStringAlt"), Keys.Alt);
			this.AddKey("F4", Keys.F4);
			this.AddKey("F5", Keys.F5);
			this.AddKey("F6", Keys.F6);
			this.AddKey("F7", Keys.F7);
			this.AddKey("F1", Keys.F1);
			this.AddKey("F2", Keys.F2);
			this.AddKey("F3", Keys.F3);
			this.AddKey(SR.GetString("toStringPageDown"), Keys.Next);
			this.AddKey(SR.GetString("toStringInsert"), Keys.Insert);
			this.AddKey(SR.GetString("toStringHome"), Keys.Home);
			this.AddKey(SR.GetString("toStringDelete"), Keys.Delete);
			this.AddKey(SR.GetString("toStringShift"), Keys.Shift);
			this.AddKey(SR.GetString("toStringPageUp"), Keys.Prior);
			this.AddKey(SR.GetString("toStringBack"), Keys.Back);
			this.AddKey("0", Keys.D0);
			this.AddKey("1", Keys.D1);
			this.AddKey("2", Keys.D2);
			this.AddKey("3", Keys.D3);
			this.AddKey("4", Keys.D4);
			this.AddKey("5", Keys.D5);
			this.AddKey("6", Keys.D6);
			this.AddKey("7", Keys.D7);
			this.AddKey("8", Keys.D8);
			this.AddKey("9", Keys.D9);
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000C05EF File Offset: 0x000BE7EF
		private IDictionary KeyNames
		{
			get
			{
				if (this.keyNames == null)
				{
					this.Initialize();
				}
				return this.keyNames;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x000C0605 File Offset: 0x000BE805
		private List<string> DisplayOrder
		{
			get
			{
				if (this.displayOrder == null)
				{
					this.Initialize();
				}
				return this.displayOrder;
			}
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000C061B File Offset: 0x000BE81B
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Enum[]) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000C064B File Offset: 0x000BE84B
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(Enum[]) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000C0669 File Offset: 0x000BE869
		public int Compare(object a, object b)
		{
			return string.Compare(base.ConvertToString(a), base.ConvertToString(b), false, CultureInfo.InvariantCulture);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000C0684 File Offset: 0x000BE884
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return null;
				}
				string[] array = text.Split(new char[]
				{
					'+'
				});
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array[i].Trim();
				}
				Keys keys = Keys.None;
				bool flag = false;
				for (int j = 0; j < array.Length; j++)
				{
					object obj = this.KeyNames[array[j]];
					if (obj == null)
					{
						obj = Enum.Parse(typeof(Keys), array[j]);
					}
					if (obj == null)
					{
						throw new FormatException(SR.GetString("KeysConverterInvalidKeyName", new object[]
						{
							array[j]
						}));
					}
					Keys keys2 = (Keys)obj;
					if ((keys2 & Keys.KeyCode) != Keys.None)
					{
						if (flag)
						{
							throw new FormatException(SR.GetString("KeysConverterInvalidKeyCombination"));
						}
						flag = true;
					}
					keys |= keys2;
				}
				return keys;
			}
			else
			{
				if (value is Enum[])
				{
					long num = 0L;
					foreach (Enum value2 in (Enum[])value)
					{
						num |= Convert.ToInt64(value2, CultureInfo.InvariantCulture);
					}
					return Enum.ToObject(typeof(Keys), num);
				}
				return base.ConvertFrom(context, culture, value);
			}
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000C07DC File Offset: 0x000BE9DC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is Keys || value is int)
			{
				bool flag = destinationType == typeof(string);
				bool flag2 = false;
				if (!flag)
				{
					flag2 = (destinationType == typeof(Enum[]));
				}
				if (flag || flag2)
				{
					Keys keys = (Keys)value;
					bool flag3 = false;
					ArrayList arrayList = new ArrayList();
					Keys keys2 = keys & Keys.Modifiers;
					for (int i = 0; i < this.DisplayOrder.Count; i++)
					{
						string text = this.DisplayOrder[i];
						Keys keys3 = (Keys)this.keyNames[text];
						if ((keys3 & keys2) != Keys.None)
						{
							if (flag)
							{
								if (flag3)
								{
									arrayList.Add("+");
								}
								arrayList.Add(text);
							}
							else
							{
								arrayList.Add(keys3);
							}
							flag3 = true;
						}
					}
					Keys keys4 = keys & Keys.KeyCode;
					bool flag4 = false;
					if (flag3 && flag)
					{
						arrayList.Add("+");
					}
					for (int j = 0; j < this.DisplayOrder.Count; j++)
					{
						string text2 = this.DisplayOrder[j];
						Keys keys5 = (Keys)this.keyNames[text2];
						if (keys5.Equals(keys4))
						{
							if (flag)
							{
								arrayList.Add(text2);
							}
							else
							{
								arrayList.Add(keys5);
							}
							flag4 = true;
							break;
						}
					}
					if (!flag4 && Enum.IsDefined(typeof(Keys), (int)keys4))
					{
						if (flag)
						{
							arrayList.Add(keys4.ToString());
						}
						else
						{
							arrayList.Add(keys4);
						}
					}
					if (flag)
					{
						StringBuilder stringBuilder = new StringBuilder(32);
						foreach (object obj in arrayList)
						{
							string value2 = (string)obj;
							stringBuilder.Append(value2);
						}
						return stringBuilder.ToString();
					}
					return (Enum[])arrayList.ToArray(typeof(Enum));
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000C0A34 File Offset: 0x000BEC34
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				ArrayList arrayList = new ArrayList();
				ICollection collection = this.KeyNames.Values;
				foreach (object value in collection)
				{
					arrayList.Add(value);
				}
				arrayList.Sort(this);
				this.values = new TypeConverter.StandardValuesCollection(arrayList.ToArray());
			}
			return this.values;
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x00011A20 File Offset: 0x0000FC20
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04001205 RID: 4613
		private IDictionary keyNames;

		// Token: 0x04001206 RID: 4614
		private List<string> displayOrder;

		// Token: 0x04001207 RID: 4615
		private TypeConverter.StandardValuesCollection values;

		// Token: 0x04001208 RID: 4616
		private const Keys FirstDigit = Keys.D0;

		// Token: 0x04001209 RID: 4617
		private const Keys LastDigit = Keys.D9;

		// Token: 0x0400120A RID: 4618
		private const Keys FirstAscii = Keys.A;

		// Token: 0x0400120B RID: 4619
		private const Keys LastAscii = Keys.Z;

		// Token: 0x0400120C RID: 4620
		private const Keys FirstNumpadDigit = Keys.NumPad0;

		// Token: 0x0400120D RID: 4621
		private const Keys LastNumpadDigit = Keys.NumPad9;
	}
}
