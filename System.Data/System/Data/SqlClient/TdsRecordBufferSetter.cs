using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000336 RID: 822
	internal class TdsRecordBufferSetter : SmiRecordBuffer
	{
		// Token: 0x06002AE6 RID: 10982 RVA: 0x002C1BB8 File Offset: 0x002C0FB8
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

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x002C1C28 File Offset: 0x002C1028
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x002C1C38 File Offset: 0x002C1038
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x002C1C48 File Offset: 0x002C1048
		public override void Close(SmiEventSink eventSink)
		{
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x002C1C58 File Offset: 0x002C1058
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._fieldSetters[ordinal].SetDBNull();
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x002C1C78 File Offset: 0x002C1078
		public override void SetBoolean(SmiEventSink sink, int ordinal, bool value)
		{
			this._fieldSetters[ordinal].SetBoolean(value);
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x002C1C98 File Offset: 0x002C1098
		public override void SetByte(SmiEventSink sink, int ordinal, byte value)
		{
			this._fieldSetters[ordinal].SetByte(value);
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x002C1CB8 File Offset: 0x002C10B8
		public override int SetBytes(SmiEventSink sink, int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetBytes(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x002C1CE8 File Offset: 0x002C10E8
		public override void SetBytesLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetBytesLength(length);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x002C1D08 File Offset: 0x002C1108
		public override int SetChars(SmiEventSink sink, int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return this._fieldSetters[ordinal].SetChars(fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x002C1D38 File Offset: 0x002C1138
		public override void SetCharsLength(SmiEventSink sink, int ordinal, long length)
		{
			this._fieldSetters[ordinal].SetCharsLength(length);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x002C1D58 File Offset: 0x002C1158
		public override void SetString(SmiEventSink sink, int ordinal, string value, int offset, int length)
		{
			this._fieldSetters[ordinal].SetString(value, offset, length);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x002C1D78 File Offset: 0x002C1178
		public override void SetInt16(SmiEventSink sink, int ordinal, short value)
		{
			this._fieldSetters[ordinal].SetInt16(value);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x002C1D98 File Offset: 0x002C1198
		public override void SetInt32(SmiEventSink sink, int ordinal, int value)
		{
			this._fieldSetters[ordinal].SetInt32(value);
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x002C1DB8 File Offset: 0x002C11B8
		public override void SetInt64(SmiEventSink sink, int ordinal, long value)
		{
			this._fieldSetters[ordinal].SetInt64(value);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x002C1DD8 File Offset: 0x002C11D8
		public override void SetSingle(SmiEventSink sink, int ordinal, float value)
		{
			this._fieldSetters[ordinal].SetSingle(value);
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x002C1DF8 File Offset: 0x002C11F8
		public override void SetDouble(SmiEventSink sink, int ordinal, double value)
		{
			this._fieldSetters[ordinal].SetDouble(value);
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x002C1E18 File Offset: 0x002C1218
		public override void SetSqlDecimal(SmiEventSink sink, int ordinal, SqlDecimal value)
		{
			this._fieldSetters[ordinal].SetSqlDecimal(value);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x002C1E38 File Offset: 0x002C1238
		public override void SetDateTime(SmiEventSink sink, int ordinal, DateTime value)
		{
			this._fieldSetters[ordinal].SetDateTime(value);
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x002C1E58 File Offset: 0x002C1258
		public override void SetGuid(SmiEventSink sink, int ordinal, Guid value)
		{
			this._fieldSetters[ordinal].SetGuid(value);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x002C1E78 File Offset: 0x002C1278
		public override void SetTimeSpan(SmiEventSink sink, int ordinal, TimeSpan value)
		{
			this._fieldSetters[ordinal].SetTimeSpan(value);
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x002C1E98 File Offset: 0x002C1298
		public override void SetDateTimeOffset(SmiEventSink sink, int ordinal, DateTimeOffset value)
		{
			this._fieldSetters[ordinal].SetDateTimeOffset(value);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x002C1EB8 File Offset: 0x002C12B8
		public override void SetVariantMetaData(SmiEventSink sink, int ordinal, SmiMetaData metaData)
		{
			this._fieldSetters[ordinal].SetVariantType(metaData);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x002C1ED8 File Offset: 0x002C12D8
		internal override void NewElement(SmiEventSink sink)
		{
			this._stateObj.Parser.WriteByte(1, this._stateObj);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x002C1F08 File Offset: 0x002C1308
		internal override void EndElements(SmiEventSink sink)
		{
			this._stateObj.Parser.WriteByte(0, this._stateObj);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x002C1F38 File Offset: 0x002C1338
		[Conditional("DEBUG")]
		private void CheckWritingToColumn(int ordinal)
		{
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x002C1F48 File Offset: 0x002C1348
		[Conditional("DEBUG")]
		private void SkipPossibleDefaultedColumns(int targetColumn)
		{
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x002C1F58 File Offset: 0x002C1358
		[Conditional("DEBUG")]
		internal void CheckSettingColumn(int ordinal)
		{
		}

		// Token: 0x04001C46 RID: 7238
		private TdsValueSetter[] _fieldSetters;

		// Token: 0x04001C47 RID: 7239
		private TdsParserStateObject _stateObj;

		// Token: 0x04001C48 RID: 7240
		private SmiMetaData _metaData;
	}
}
