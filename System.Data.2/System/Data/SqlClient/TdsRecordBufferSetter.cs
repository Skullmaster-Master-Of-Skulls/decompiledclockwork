using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000234 RID: 564
	internal class TdsRecordBufferSetter : SmiRecordBuffer
	{
		// Token: 0x060022E9 RID: 8937 RVA: 0x000F1AF8 File Offset: 0x000F0EF8
		internal TdsRecordBufferSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._fieldSetters = new TdsValueSetter[md.FieldMetaData.Count];
			for (int i = 0; i < md.FieldMetaData.Count; i++)
			{
				this._fieldSetters[i] = new TdsValueSetter(stateObj, md.FieldMetaData[i]);
			}
			this._stateObj = stateObj;
			this._metaData = md;
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x000F1B60 File Offset: 0x000F0F60
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x000F1B70 File Offset: 0x000F0F70
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000F1B80 File Offset: 0x000F0F80
		public override void Close(SmiEventSink eventSink)
		{
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000F1B90 File Offset: 0x000F0F90
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._fieldSetters[ordinal].SetDBNull();
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000F1BAC File Offset: 0x000F0FAC
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._fieldSetters[ordinal].SetBoolean(value);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000F1BC8 File Offset: 0x000F0FC8
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._fieldSetters[ordinal].SetByte(value);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000F1BE4 File Offset: 0x000F0FE4
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000F1C08 File Offset: 0x000F1008
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetBytesLength(length);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000F1C24 File Offset: 0x000F1024
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000F1C48 File Offset: 0x000F1048
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetCharsLength(length);
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000F1C64 File Offset: 0x000F1064
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._fieldSetters[ordinal].SetString(value, offset, length);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000F1C84 File Offset: 0x000F1084
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._fieldSetters[ordinal].SetInt16(value);
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000F1CA0 File Offset: 0x000F10A0
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._fieldSetters[ordinal].SetInt32(value);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000F1CBC File Offset: 0x000F10BC
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._fieldSetters[ordinal].SetInt64(value);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000F1CD8 File Offset: 0x000F10D8
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._fieldSetters[ordinal].SetSingle(value);
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000F1CF4 File Offset: 0x000F10F4
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._fieldSetters[ordinal].SetDouble(value);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000F1D10 File Offset: 0x000F1110
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._fieldSetters[ordinal].SetSqlDecimal(value);
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000F1D2C File Offset: 0x000F112C
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._fieldSetters[ordinal].SetDateTime(value);
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x000F1D48 File Offset: 0x000F1148
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._fieldSetters[ordinal].SetGuid(value);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000F1D64 File Offset: 0x000F1164
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._fieldSetters[ordinal].SetTimeSpan(value);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000F1D80 File Offset: 0x000F1180
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._fieldSetters[ordinal].SetDateTimeOffset(value);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000F1D9C File Offset: 0x000F119C
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._fieldSetters[ordinal].SetVariantType(metaData);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000F1DB8 File Offset: 0x000F11B8
		internal override void NewElement(SmiEventSink sink)
		{
			this._stateObj.WriteByte(1);
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000F1DD4 File Offset: 0x000F11D4
		internal override void EndElements(SmiEventSink sink)
		{
			this._stateObj.WriteByte(0);
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000F1DF0 File Offset: 0x000F11F0
		[Conditional("DEBUG")]
		private void CheckWritingToColumn(int ordinal)
		{
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x000F1E00 File Offset: 0x000F1200
		[Conditional("DEBUG")]
		private void SkipPossibleDefaultedColumns(int targetColumn)
		{
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x000F1E10 File Offset: 0x000F1210
		[Conditional("DEBUG")]
		internal void CheckSettingColumn(int ordinal)
		{
		}

		// Token: 0x04001534 RID: 5428
		private TdsValueSetter[] _fieldSetters;

		// Token: 0x04001535 RID: 5429
		private TdsParserStateObject _stateObj;

		// Token: 0x04001536 RID: 5430
		private SmiMetaData _metaData;
	}
}
