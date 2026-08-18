using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x0200000A RID: 10
	internal sealed class FieldNameLookup
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000291C File Offset: 0x00000B1C
		public FieldNameLookup(ReadOnlyCollection<string> columnNames, int defaultLocaleID)
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

		// Token: 0x06000027 RID: 39 RVA: 0x00002968 File Offset: 0x00000B68
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

		// Token: 0x06000028 RID: 40 RVA: 0x000029B0 File Offset: 0x00000BB0
		public int GetOrdinal(string fieldName)
		{
			if (fieldName == null)
			{
				throw EntityUtil.ArgumentNull("fieldName");
			}
			int num = this.IndexOf(fieldName);
			if (-1 == num)
			{
				throw EntityUtil.IndexOutOfRange(fieldName);
			}
			return num;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000029E0 File Offset: 0x00000BE0
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

		// Token: 0x0600002A RID: 42 RVA: 0x00002A2C File Offset: 0x00000C2C
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

		// Token: 0x0600002B RID: 43 RVA: 0x00002AAC File Offset: 0x00000CAC
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

		// Token: 0x0400007B RID: 123
		private Hashtable _fieldNameLookup;

		// Token: 0x0400007C RID: 124
		private string[] _fieldNames;

		// Token: 0x0400007D RID: 125
		private CompareInfo _compareInfo;

		// Token: 0x0400007E RID: 126
		private int _defaultLocaleID;
	}
}
