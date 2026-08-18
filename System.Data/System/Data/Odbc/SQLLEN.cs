using System;

namespace System.Data.Odbc
{
	// Token: 0x02000205 RID: 517
	internal struct SQLLEN
	{
		// Token: 0x06001C7C RID: 7292 RVA: 0x002691F8 File Offset: 0x002685F8
		internal SQLLEN(int value)
		{
			this._value = (long)value;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00269218 File Offset: 0x00268618
		internal SQLLEN(long value)
		{
			this._value = value;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x00269238 File Offset: 0x00268638
		internal SQLLEN(IntPtr value)
		{
			this._value = value.ToInt64();
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x00269258 File Offset: 0x00268658
		public static implicit operator SQLLEN(int value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00269278 File Offset: 0x00268678
		public static explicit operator SQLLEN(long value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00269298 File Offset: 0x00268698
		public static implicit operator int(SQLLEN value)
		{
			long value2 = value._value;
			return checked((int)value2);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x002692B8 File Offset: 0x002686B8
		public static explicit operator long(SQLLEN value)
		{
			return value._value;
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x002692D8 File Offset: 0x002686D8
		public long ToInt64()
		{
			return this._value;
		}

		// Token: 0x04001078 RID: 4216
		internal long _value;
	}
}
