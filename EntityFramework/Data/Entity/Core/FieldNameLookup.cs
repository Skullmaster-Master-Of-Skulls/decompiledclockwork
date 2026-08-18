using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D4 RID: 724
	internal sealed class FieldNameLookup
	{
		// Token: 0x06001962 RID: 6498 RVA: 0x0007E9CC File Offset: 0x0007CBCC
		public FieldNameLookup(ReadOnlyCollection<string> columnNames)
		{
			int count = columnNames.Count;
			this._fieldNames = new string[count];
			for (int i = 0; i < count; i++)
			{
				this._fieldNames[i] = columnNames[i];
			}
			this.GenerateLookup();
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0007EA20 File Offset: 0x0007CC20
		public FieldNameLookup(IDataRecord reader)
		{
			int fieldCount = reader.FieldCount;
			this._fieldNames = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				this._fieldNames[i] = reader.GetName(i);
			}
			this.GenerateLookup();
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0007EA74 File Offset: 0x0007CC74
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		public int GetOrdinal(string fieldName)
		{
			Check.NotNull<string>(fieldName, "fieldName");
			int num = this.IndexOf(fieldName);
			if (num == -1)
			{
				throw new IndexOutOfRangeException(fieldName);
			}
			return num;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0007EAA4 File Offset: 0x0007CCA4
		private int IndexOf(string fieldName)
		{
			int num;
			if (!this._fieldNameLookup.TryGetValue(fieldName, out num))
			{
				num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase);
				if (num == -1)
				{
					num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				}
			}
			return num;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0007EADC File Offset: 0x0007CCDC
		private int LinearIndexOf(string fieldName, CompareOptions compareOptions)
		{
			for (int i = 0; i < this._fieldNames.Length; i++)
			{
				if (CultureInfo.InvariantCulture.CompareInfo.Compare(fieldName, this._fieldNames[i], compareOptions) == 0)
				{
					this._fieldNameLookup[fieldName] = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0007EB28 File Offset: 0x0007CD28
		private void GenerateLookup()
		{
			int num = this._fieldNames.Length - 1;
			while (0 <= num)
			{
				this._fieldNameLookup[this._fieldNames[num]] = num;
				num--;
			}
		}

		// Token: 0x040008B2 RID: 2226
		private readonly Dictionary<string, int> _fieldNameLookup = new Dictionary<string, int>();

		// Token: 0x040008B3 RID: 2227
		private readonly string[] _fieldNames;
	}
}
