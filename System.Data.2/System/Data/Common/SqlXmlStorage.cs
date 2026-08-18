using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000324 RID: 804
	internal sealed class SqlXmlStorage : DataStorage
	{
		// Token: 0x0600329E RID: 12958 RVA: 0x0013A810 File Offset: 0x00139C10
		public SqlXmlStorage(DataColumn column) : base(column, typeof(SqlXml), SqlXml.Null, SqlXml.Null, StorageType.Empty)
		{
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x0013A83C File Offset: 0x00139C3C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			try
			{
				if (kind != AggregateType.First)
				{
					if (kind == AggregateType.Count)
					{
						int num = 0;
						for (int i = 0; i < records.Length; i++)
						{
							if (!this.IsNull(records[i]))
							{
								num++;
							}
						}
						return num;
					}
				}
				else
				{
					if (records.Length != 0)
					{
						return this.values[records[0]];
					}
					return null;
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlXml));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x0013A8D0 File Offset: 0x00139CD0
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x0013A8E0 File Offset: 0x00139CE0
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x0013A8F0 File Offset: 0x00139CF0
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x0013A910 File Offset: 0x00139D10
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x0013A928 File Offset: 0x00139D28
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x0013A944 File Offset: 0x00139D44
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this.values[record] = SqlXml.Null;
				return;
			}
			this.values[record] = (SqlXml)value;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x0013A978 File Offset: 0x00139D78
		public override void SetCapacity(int capacity)
		{
			SqlXml[] destinationArray = new SqlXml[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x0013A9B8 File Offset: 0x00139DB8
		public override object ConvertXmlToObject(string s)
		{
			XmlTextReader value = new XmlTextReader(s, XmlNodeType.Element, null);
			return new SqlXml(value);
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x0013A9D4 File Offset: 0x00139DD4
		public override string ConvertObjectToXml(object value)
		{
			SqlXml sqlXml = (SqlXml)value;
			if (sqlXml.IsNull)
			{
				return ADP.StrEmpty;
			}
			return sqlXml.Value;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x0013A9FC File Offset: 0x00139DFC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlXml[recordCount];
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x0013AA10 File Offset: 0x00139E10
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlXml[] array = (SqlXml[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x0013AA40 File Offset: 0x00139E40
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlXml[])store;
		}

		// Token: 0x04001DC3 RID: 7619
		private SqlXml[] values;
	}
}
