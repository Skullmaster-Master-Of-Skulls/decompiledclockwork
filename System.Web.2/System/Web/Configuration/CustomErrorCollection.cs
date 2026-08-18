using System;
using System.Configuration;
using System.Globalization;

namespace System.Web.Configuration
{
	// Token: 0x020006CD RID: 1741
	[ConfigurationCollection(typeof(CustomError), AddItemName = "error", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class CustomErrorCollection : ConfigurationElementCollection
	{
		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x00126B50 File Offset: 0x00124D50
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomErrorCollection._properties;
			}
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x060053E8 RID: 21480 RVA: 0x00126B58 File Offset: 0x00124D58
		public string[] AllKeys
		{
			get
			{
				object[] array = base.BaseGetAllKeys();
				string[] array2 = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = ((int)array[i]).ToString(CultureInfo.InvariantCulture);
				}
				return array2;
			}
		}

		// Token: 0x170017EE RID: 6126
		public CustomError this[string statusCode]
		{
			get
			{
				return (CustomError)base.BaseGet(int.Parse(statusCode, CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x170017EF RID: 6127
		public CustomError this[int index]
		{
			get
			{
				return (CustomError)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x00126BC7 File Offset: 0x00124DC7
		protected override ConfigurationElement CreateNewElement()
		{
			return new CustomError();
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x00126BCE File Offset: 0x00124DCE
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CustomError)element).StatusCode;
		}

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x060053EE RID: 21486 RVA: 0x00126BE0 File Offset: 0x00124DE0
		protected override string ElementName
		{
			get
			{
				return "error";
			}
		}

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x060053EF RID: 21487 RVA: 0x00007722 File Offset: 0x00005922
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(CustomError customError)
		{
			this.BaseAdd(customError);
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x00126BB9 File Offset: 0x00124DB9
		public CustomError Get(int index)
		{
			return (CustomError)base.BaseGet(index);
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x00126B9C File Offset: 0x00124D9C
		public CustomError Get(string statusCode)
		{
			return (CustomError)base.BaseGet(int.Parse(statusCode, CultureInfo.InvariantCulture));
		}

		// Token: 0x060053F4 RID: 21492 RVA: 0x00126BE8 File Offset: 0x00124DE8
		public string GetKey(int index)
		{
			return ((int)base.BaseGetKey(index)).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x00126C0E File Offset: 0x00124E0E
		public void Remove(string statusCode)
		{
			base.BaseRemove(int.Parse(statusCode, CultureInfo.InvariantCulture));
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x00126C26 File Offset: 0x00124E26
		public void Set(CustomError customError)
		{
			base.BaseAdd(customError, false);
		}

		// Token: 0x04002C25 RID: 11301
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
