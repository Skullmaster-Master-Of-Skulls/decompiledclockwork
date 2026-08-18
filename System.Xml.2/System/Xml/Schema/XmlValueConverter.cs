using System;

namespace System.Xml.Schema
{
	// Token: 0x020002C0 RID: 704
	internal abstract class XmlValueConverter
	{
		// Token: 0x060028A8 RID: 10408
		public abstract bool ToBoolean(bool value);

		// Token: 0x060028A9 RID: 10409
		public abstract bool ToBoolean(long value);

		// Token: 0x060028AA RID: 10410
		public abstract bool ToBoolean(int value);

		// Token: 0x060028AB RID: 10411
		public abstract bool ToBoolean(decimal value);

		// Token: 0x060028AC RID: 10412
		public abstract bool ToBoolean(float value);

		// Token: 0x060028AD RID: 10413
		public abstract bool ToBoolean(double value);

		// Token: 0x060028AE RID: 10414
		public abstract bool ToBoolean(DateTime value);

		// Token: 0x060028AF RID: 10415
		public abstract bool ToBoolean(DateTimeOffset value);

		// Token: 0x060028B0 RID: 10416
		public abstract bool ToBoolean(string value);

		// Token: 0x060028B1 RID: 10417
		public abstract bool ToBoolean(object value);

		// Token: 0x060028B2 RID: 10418
		public abstract int ToInt32(bool value);

		// Token: 0x060028B3 RID: 10419
		public abstract int ToInt32(int value);

		// Token: 0x060028B4 RID: 10420
		public abstract int ToInt32(long value);

		// Token: 0x060028B5 RID: 10421
		public abstract int ToInt32(decimal value);

		// Token: 0x060028B6 RID: 10422
		public abstract int ToInt32(float value);

		// Token: 0x060028B7 RID: 10423
		public abstract int ToInt32(double value);

		// Token: 0x060028B8 RID: 10424
		public abstract int ToInt32(DateTime value);

		// Token: 0x060028B9 RID: 10425
		public abstract int ToInt32(DateTimeOffset value);

		// Token: 0x060028BA RID: 10426
		public abstract int ToInt32(string value);

		// Token: 0x060028BB RID: 10427
		public abstract int ToInt32(object value);

		// Token: 0x060028BC RID: 10428
		public abstract long ToInt64(bool value);

		// Token: 0x060028BD RID: 10429
		public abstract long ToInt64(int value);

		// Token: 0x060028BE RID: 10430
		public abstract long ToInt64(long value);

		// Token: 0x060028BF RID: 10431
		public abstract long ToInt64(decimal value);

		// Token: 0x060028C0 RID: 10432
		public abstract long ToInt64(float value);

		// Token: 0x060028C1 RID: 10433
		public abstract long ToInt64(double value);

		// Token: 0x060028C2 RID: 10434
		public abstract long ToInt64(DateTime value);

		// Token: 0x060028C3 RID: 10435
		public abstract long ToInt64(DateTimeOffset value);

		// Token: 0x060028C4 RID: 10436
		public abstract long ToInt64(string value);

		// Token: 0x060028C5 RID: 10437
		public abstract long ToInt64(object value);

		// Token: 0x060028C6 RID: 10438
		public abstract decimal ToDecimal(bool value);

		// Token: 0x060028C7 RID: 10439
		public abstract decimal ToDecimal(int value);

		// Token: 0x060028C8 RID: 10440
		public abstract decimal ToDecimal(long value);

		// Token: 0x060028C9 RID: 10441
		public abstract decimal ToDecimal(decimal value);

		// Token: 0x060028CA RID: 10442
		public abstract decimal ToDecimal(float value);

		// Token: 0x060028CB RID: 10443
		public abstract decimal ToDecimal(double value);

		// Token: 0x060028CC RID: 10444
		public abstract decimal ToDecimal(DateTime value);

		// Token: 0x060028CD RID: 10445
		public abstract decimal ToDecimal(DateTimeOffset value);

		// Token: 0x060028CE RID: 10446
		public abstract decimal ToDecimal(string value);

		// Token: 0x060028CF RID: 10447
		public abstract decimal ToDecimal(object value);

		// Token: 0x060028D0 RID: 10448
		public abstract double ToDouble(bool value);

		// Token: 0x060028D1 RID: 10449
		public abstract double ToDouble(int value);

		// Token: 0x060028D2 RID: 10450
		public abstract double ToDouble(long value);

		// Token: 0x060028D3 RID: 10451
		public abstract double ToDouble(decimal value);

		// Token: 0x060028D4 RID: 10452
		public abstract double ToDouble(float value);

		// Token: 0x060028D5 RID: 10453
		public abstract double ToDouble(double value);

		// Token: 0x060028D6 RID: 10454
		public abstract double ToDouble(DateTime value);

		// Token: 0x060028D7 RID: 10455
		public abstract double ToDouble(DateTimeOffset value);

		// Token: 0x060028D8 RID: 10456
		public abstract double ToDouble(string value);

		// Token: 0x060028D9 RID: 10457
		public abstract double ToDouble(object value);

		// Token: 0x060028DA RID: 10458
		public abstract float ToSingle(bool value);

		// Token: 0x060028DB RID: 10459
		public abstract float ToSingle(int value);

		// Token: 0x060028DC RID: 10460
		public abstract float ToSingle(long value);

		// Token: 0x060028DD RID: 10461
		public abstract float ToSingle(decimal value);

		// Token: 0x060028DE RID: 10462
		public abstract float ToSingle(float value);

		// Token: 0x060028DF RID: 10463
		public abstract float ToSingle(double value);

		// Token: 0x060028E0 RID: 10464
		public abstract float ToSingle(DateTime value);

		// Token: 0x060028E1 RID: 10465
		public abstract float ToSingle(DateTimeOffset value);

		// Token: 0x060028E2 RID: 10466
		public abstract float ToSingle(string value);

		// Token: 0x060028E3 RID: 10467
		public abstract float ToSingle(object value);

		// Token: 0x060028E4 RID: 10468
		public abstract DateTime ToDateTime(bool value);

		// Token: 0x060028E5 RID: 10469
		public abstract DateTime ToDateTime(int value);

		// Token: 0x060028E6 RID: 10470
		public abstract DateTime ToDateTime(long value);

		// Token: 0x060028E7 RID: 10471
		public abstract DateTime ToDateTime(decimal value);

		// Token: 0x060028E8 RID: 10472
		public abstract DateTime ToDateTime(float value);

		// Token: 0x060028E9 RID: 10473
		public abstract DateTime ToDateTime(double value);

		// Token: 0x060028EA RID: 10474
		public abstract DateTime ToDateTime(DateTime value);

		// Token: 0x060028EB RID: 10475
		public abstract DateTime ToDateTime(DateTimeOffset value);

		// Token: 0x060028EC RID: 10476
		public abstract DateTime ToDateTime(string value);

		// Token: 0x060028ED RID: 10477
		public abstract DateTime ToDateTime(object value);

		// Token: 0x060028EE RID: 10478
		public abstract DateTimeOffset ToDateTimeOffset(bool value);

		// Token: 0x060028EF RID: 10479
		public abstract DateTimeOffset ToDateTimeOffset(int value);

		// Token: 0x060028F0 RID: 10480
		public abstract DateTimeOffset ToDateTimeOffset(long value);

		// Token: 0x060028F1 RID: 10481
		public abstract DateTimeOffset ToDateTimeOffset(decimal value);

		// Token: 0x060028F2 RID: 10482
		public abstract DateTimeOffset ToDateTimeOffset(float value);

		// Token: 0x060028F3 RID: 10483
		public abstract DateTimeOffset ToDateTimeOffset(double value);

		// Token: 0x060028F4 RID: 10484
		public abstract DateTimeOffset ToDateTimeOffset(DateTime value);

		// Token: 0x060028F5 RID: 10485
		public abstract DateTimeOffset ToDateTimeOffset(DateTimeOffset value);

		// Token: 0x060028F6 RID: 10486
		public abstract DateTimeOffset ToDateTimeOffset(string value);

		// Token: 0x060028F7 RID: 10487
		public abstract DateTimeOffset ToDateTimeOffset(object value);

		// Token: 0x060028F8 RID: 10488
		public abstract string ToString(bool value);

		// Token: 0x060028F9 RID: 10489
		public abstract string ToString(int value);

		// Token: 0x060028FA RID: 10490
		public abstract string ToString(long value);

		// Token: 0x060028FB RID: 10491
		public abstract string ToString(decimal value);

		// Token: 0x060028FC RID: 10492
		public abstract string ToString(float value);

		// Token: 0x060028FD RID: 10493
		public abstract string ToString(double value);

		// Token: 0x060028FE RID: 10494
		public abstract string ToString(DateTime value);

		// Token: 0x060028FF RID: 10495
		public abstract string ToString(DateTimeOffset value);

		// Token: 0x06002900 RID: 10496
		public abstract string ToString(string value);

		// Token: 0x06002901 RID: 10497
		public abstract string ToString(string value, IXmlNamespaceResolver nsResolver);

		// Token: 0x06002902 RID: 10498
		public abstract string ToString(object value);

		// Token: 0x06002903 RID: 10499
		public abstract string ToString(object value, IXmlNamespaceResolver nsResolver);

		// Token: 0x06002904 RID: 10500
		public abstract object ChangeType(bool value, Type destinationType);

		// Token: 0x06002905 RID: 10501
		public abstract object ChangeType(int value, Type destinationType);

		// Token: 0x06002906 RID: 10502
		public abstract object ChangeType(long value, Type destinationType);

		// Token: 0x06002907 RID: 10503
		public abstract object ChangeType(decimal value, Type destinationType);

		// Token: 0x06002908 RID: 10504
		public abstract object ChangeType(float value, Type destinationType);

		// Token: 0x06002909 RID: 10505
		public abstract object ChangeType(double value, Type destinationType);

		// Token: 0x0600290A RID: 10506
		public abstract object ChangeType(DateTime value, Type destinationType);

		// Token: 0x0600290B RID: 10507
		public abstract object ChangeType(DateTimeOffset value, Type destinationType);

		// Token: 0x0600290C RID: 10508
		public abstract object ChangeType(string value, Type destinationType);

		// Token: 0x0600290D RID: 10509
		public abstract object ChangeType(string value, Type destinationType, IXmlNamespaceResolver nsResolver);

		// Token: 0x0600290E RID: 10510
		public abstract object ChangeType(object value, Type destinationType);

		// Token: 0x0600290F RID: 10511
		public abstract object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver);
	}
}
