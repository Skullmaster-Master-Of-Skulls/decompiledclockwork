using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.ProviderBase
{
	// Token: 0x020002B5 RID: 693
	internal sealed class FieldNameLookup
	{
		// Token: 0x060029F1 RID: 10737 RVA: 0x00115C38 File Offset: 0x00115038
		public FieldNameLookup(string[] fieldNames, int defaultLocaleID)
		{
			if (fieldNames == null)
			{
				throw ADP.ArgumentNull("fieldNames");
			}
			this._fieldNames = fieldNames;
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00115C68 File Offset: 0x00115068
		public FieldNameLookup(System.Collections.ObjectModel.ReadOnlyCollection<string> columnNames, int defaultLocaleID)
		{
			int count = columnNames.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = columnNames[i];
			}
			this._fieldNames = array;
			this._defaultLocaleID = defaultLocaleID;
			this.GenerateLookup();
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00115CB4 File Offset: 0x001150B4
		public FieldNameLookup(IDataRecord reader, int defaultLocaleID)
		{
			int fieldCount = reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = reader.GetName(i);
			}
			this._fieldNames = array;
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x00115CFC File Offset: 0x001150FC
		public int GetOrdinal(string fieldName)
		{
			if (fieldName == null)
			{
				throw ADP.ArgumentNull("fieldName");
			}
			int num = this.IndexOf(fieldName);
			if (-1 == num)
			{
				throw ADP.IndexOutOfRange(fieldName);
			}
			return num;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00115D2C File Offset: 0x0011512C
		public int IndexOfName(string fieldName)
		{
			if (this._fieldNameLookup == null)
			{
				this.GenerateLookup();
			}
			object obj = this._fieldNameLookup[fieldName];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x00115D60 File Offset: 0x00115160
		public int IndexOf(string fieldName)
		{
			if (this._fieldNameLookup == null)
			{
				this.GenerateLookup();
			}
			object obj = this._fieldNameLookup[fieldName];
			int num;
			if (obj != null)
			{
				num = (int)obj;
			}
			else
			{
				num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase);
				if (-1 == num)
				{
					num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				}
			}
			return num;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x00115DAC File Offset: 0x001151AC
		private int LinearIndexOf(string fieldName, CompareOptions compareOptions)
		{
			CompareInfo compareInfo = this._compareInfo;
			if (compareInfo == null)
			{
				if (-1 != this._defaultLocaleID)
				{
					compareInfo = CompareInfo.GetCompareInfo(this._defaultLocaleID);
				}
				if (compareInfo == null)
				{
					compareInfo = CultureInfo.InvariantCulture.CompareInfo;
				}
				this._compareInfo = compareInfo;
			}
			int num = this._fieldNames.Length;
			for (int i = 0; i < num; i++)
			{
				if (compareInfo.Compare(fieldName, this._fieldNames[i], compareOptions) == 0)
				{
					this._fieldNameLookup[fieldName] = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x00115E2C File Offset: 0x0011522C
		private void GenerateLookup()
		{
			int num = this._fieldNames.Length;
			Hashtable hashtable = new Hashtable(num);
			int num2 = num - 1;
			while (0 <= num2)
			{
				string key = this._fieldNames[num2];
				hashtable[key] = num2;
				num2--;
			}
			this._fieldNameLookup = hashtable;
		}

		// Token: 0x04001B0D RID: 6925
		private Hashtable _fieldNameLookup;

		// Token: 0x04001B0E RID: 6926
		private string[] _fieldNames;

		// Token: 0x04001B0F RID: 6927
		private CompareInfo _compareInfo;

		// Token: 0x04001B10 RID: 6928
		private int _defaultLocaleID;
	}
}
