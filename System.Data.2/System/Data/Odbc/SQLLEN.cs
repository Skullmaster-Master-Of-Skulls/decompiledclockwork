using System;

namespace System.Data.Odbc
{
	// Token: 0x020002AF RID: 687
	internal struct SQLLEN
	{
		// Token: 0x060029B9 RID: 10681 RVA: 0x00114BAC File Offset: 0x00113FAC
		internal SQLLEN(int value)
		{
			this._value = new IntPtr(value);
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x00114BC8 File Offset: 0x00113FC8
		internal SQLLEN(long value)
		{
			this._value = new IntPtr(value);
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x00114BE4 File Offset: 0x00113FE4
		internal SQLLEN(IntPtr value)
		{
			this._value = value;
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x00114BF8 File Offset: 0x00113FF8
		public static implicit operator SQLLEN(int value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x00114C0C File Offset: 0x0011400C
		public static explicit operator SQLLEN(long value)
		{
			return new SQLLEN(value);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x00114C20 File Offset: 0x00114020
		public static implicit operator int(SQLLEN value)
		{
			long num = value._value.ToInt64();
			return checked((int)num);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x00114C3C File Offset: 0x0011403C
		public static explicit operator long(SQLLEN value)
		{
			return value._value.ToInt64();
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00114C58 File Offset: 0x00114058
		public long ToInt64()
		{
			return this._value.ToInt64();
		}

		// Token: 0x04001AEA RID: 6890
		private IntPtr _value;
	}
}
