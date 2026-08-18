using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;

namespace System.Data.ProviderBase
{
	// Token: 0x0200014A RID: 330
	internal sealed class FieldNameLookup
	{
		// Token: 0x0600153E RID: 5438 RVA: 0x00243458 File Offset: 0x00242858
		public FieldNameLookup(string[] fieldNames, int defaultLocaleID)
		{
			if (fieldNames == null)
			{
				throw ADP.ArgumentNull("fieldNames");
			}
			this._fieldNames = fieldNames;
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00243488 File Offset: 0x00242888
		public FieldNameLookup(IDataReader reader, int defaultLocaleID)
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

		// Token: 0x06001540 RID: 5440 RVA: 0x002434D8 File Offset: 0x002428D8
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

		// Token: 0x06001541 RID: 5441 RVA: 0x00243508 File Offset: 0x00242908
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

		// Token: 0x06001542 RID: 5442 RVA: 0x00243548 File Offset: 0x00242948
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

		// Token: 0x06001543 RID: 5443 RVA: 0x00243598 File Offset: 0x00242998
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

		// Token: 0x06001544 RID: 5444 RVA: 0x00243618 File Offset: 0x00242A18
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

		// Token: 0x04000C8B RID: 3211
		private Hashtable _fieldNameLookup;

		// Token: 0x04000C8C RID: 3212
		private string[] _fieldNames;

		// Token: 0x04000C8D RID: 3213
		private CompareInfo _compareInfo;

		// Token: 0x04000C8E RID: 3214
		private int _defaultLocaleID;
	}
}
