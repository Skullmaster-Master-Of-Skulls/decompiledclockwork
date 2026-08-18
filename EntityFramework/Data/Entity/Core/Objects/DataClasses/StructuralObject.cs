using System;
using System.ComponentModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200052E RID: 1326
	[DataContract(IsReference = true)]
	[Serializable]
	public abstract class StructuralObject : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06003274 RID: 12916 RVA: 0x000F0288 File Offset: 0x000EE488
		// (remove) Token: 0x06003275 RID: 12917 RVA: 0x000F02C0 File Offset: 0x000EE4C0
		[NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06003276 RID: 12918 RVA: 0x000F02F8 File Offset: 0x000EE4F8
		// (remove) Token: 0x06003277 RID: 12919 RVA: 0x000F0330 File Offset: 0x000EE530
		[NonSerialized]
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x06003278 RID: 12920 RVA: 0x000F0365 File Offset: 0x000EE565
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000F0381 File Offset: 0x000EE581
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property")]
		protected virtual void OnPropertyChanging(string property)
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, new PropertyChangingEventArgs(property));
			}
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000F039D File Offset: 0x000EE59D
		protected static DateTime DefaultDateTimeValue()
		{
			return DateTime.Now;
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000F03A4 File Offset: 0x000EE5A4
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property")]
		protected virtual void ReportPropertyChanging(string property)
		{
			Check.NotEmpty(property, "property");
			this.OnPropertyChanging(property);
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000F03B9 File Offset: 0x000EE5B9
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property")]
		protected virtual void ReportPropertyChanged(string property)
		{
			Check.NotEmpty(property, "property");
			this.OnPropertyChanged(property);
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000F03CE File Offset: 0x000EE5CE
		protected internal T GetValidValue<T>(T currentValue, string property, bool isNullable, bool isInitialized) where T : ComplexObject, new()
		{
			if (!isNullable && !isInitialized)
			{
				currentValue = this.SetValidValue<T>(currentValue, Activator.CreateInstance<T>(), property);
			}
			return currentValue;
		}

		// Token: 0x0600327E RID: 12926
		internal abstract void ReportComplexPropertyChanging(string entityMemberName, ComplexObject complexObject, string complexMemberName);

		// Token: 0x0600327F RID: 12927
		internal abstract void ReportComplexPropertyChanged(string entityMemberName, ComplexObject complexObject, string complexMemberName);

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06003280 RID: 12928
		internal abstract bool IsChangeTracked { get; }

		// Token: 0x06003281 RID: 12929 RVA: 0x000F03E7 File Offset: 0x000EE5E7
		protected internal static bool BinaryEquals(byte[] first, byte[] second)
		{
			return object.ReferenceEquals(first, second) || (first != null && second != null && ByValueEqualityComparer.CompareBinaryValues(first, second));
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000F0403 File Offset: 0x000EE603
		protected internal static byte[] GetValidValue(byte[] currentValue)
		{
			if (currentValue == null)
			{
				return null;
			}
			return (byte[])currentValue.Clone();
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000F0415 File Offset: 0x000EE615
		protected internal static byte[] SetValidValue(byte[] value, bool isNullable, string propertyName)
		{
			if (value == null)
			{
				if (!isNullable)
				{
					EntityUtil.ThrowPropertyIsNotNullable(propertyName);
				}
				return value;
			}
			return (byte[])value.Clone();
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000F0430 File Offset: 0x000EE630
		protected internal static byte[] SetValidValue(byte[] value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000F043A File Offset: 0x000EE63A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static bool SetValidValue(bool value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000F043D File Offset: 0x000EE63D
		protected internal static bool SetValidValue(bool value)
		{
			return value;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000F0440 File Offset: 0x000EE640
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static bool? SetValidValue(bool? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000F0443 File Offset: 0x000EE643
		protected internal static bool? SetValidValue(bool? value)
		{
			return value;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000F0446 File Offset: 0x000EE646
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static byte SetValidValue(byte value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000F0449 File Offset: 0x000EE649
		protected internal static byte SetValidValue(byte value)
		{
			return value;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000F044C File Offset: 0x000EE64C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static byte? SetValidValue(byte? value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000F044F File Offset: 0x000EE64F
		protected internal static byte? SetValidValue(byte? value)
		{
			return value;
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000F0452 File Offset: 0x000EE652
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		[CLSCompliant(false)]
		protected internal static sbyte SetValidValue(sbyte value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000F0455 File Offset: 0x000EE655
		[CLSCompliant(false)]
		protected internal static sbyte SetValidValue(sbyte value)
		{
			return value;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000F0458 File Offset: 0x000EE658
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		[CLSCompliant(false)]
		protected internal static sbyte? SetValidValue(sbyte? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000F045B File Offset: 0x000EE65B
		[CLSCompliant(false)]
		protected internal static sbyte? SetValidValue(sbyte? value)
		{
			return value;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000F045E File Offset: 0x000EE65E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static DateTime SetValidValue(DateTime value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000F0461 File Offset: 0x000EE661
		protected internal static DateTime SetValidValue(DateTime value)
		{
			return value;
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000F0464 File Offset: 0x000EE664
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static DateTime? SetValidValue(DateTime? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000F0467 File Offset: 0x000EE667
		protected internal static DateTime? SetValidValue(DateTime? value)
		{
			return value;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000F046A File Offset: 0x000EE66A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static TimeSpan SetValidValue(TimeSpan value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000F046D File Offset: 0x000EE66D
		protected internal static TimeSpan SetValidValue(TimeSpan value)
		{
			return value;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000F0470 File Offset: 0x000EE670
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static TimeSpan? SetValidValue(TimeSpan? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000F0473 File Offset: 0x000EE673
		protected internal static TimeSpan? SetValidValue(TimeSpan? value)
		{
			return value;
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000F0476 File Offset: 0x000EE676
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static DateTimeOffset SetValidValue(DateTimeOffset value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000F0479 File Offset: 0x000EE679
		protected internal static DateTimeOffset SetValidValue(DateTimeOffset value)
		{
			return value;
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000F047C File Offset: 0x000EE67C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static DateTimeOffset? SetValidValue(DateTimeOffset? value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000F047F File Offset: 0x000EE67F
		protected internal static DateTimeOffset? SetValidValue(DateTimeOffset? value)
		{
			return value;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000F0482 File Offset: 0x000EE682
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static decimal SetValidValue(decimal value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000F0485 File Offset: 0x000EE685
		protected internal static decimal SetValidValue(decimal value)
		{
			return value;
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000F0488 File Offset: 0x000EE688
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static decimal? SetValidValue(decimal? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000F048B File Offset: 0x000EE68B
		protected internal static decimal? SetValidValue(decimal? value)
		{
			return value;
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000F048E File Offset: 0x000EE68E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static double SetValidValue(double value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000F0491 File Offset: 0x000EE691
		protected internal static double SetValidValue(double value)
		{
			return value;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000F0494 File Offset: 0x000EE694
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static double? SetValidValue(double? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000F0497 File Offset: 0x000EE697
		protected internal static double? SetValidValue(double? value)
		{
			return value;
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000F049A File Offset: 0x000EE69A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static float SetValidValue(float value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000F049D File Offset: 0x000EE69D
		protected internal static float SetValidValue(float value)
		{
			return value;
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000F04A0 File Offset: 0x000EE6A0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static float? SetValidValue(float? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000F04A3 File Offset: 0x000EE6A3
		protected internal static float? SetValidValue(float? value)
		{
			return value;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000F04A6 File Offset: 0x000EE6A6
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static Guid SetValidValue(Guid value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000F04A9 File Offset: 0x000EE6A9
		protected internal static Guid SetValidValue(Guid value)
		{
			return value;
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000F04AC File Offset: 0x000EE6AC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static Guid? SetValidValue(Guid? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000F04AF File Offset: 0x000EE6AF
		protected internal static Guid? SetValidValue(Guid? value)
		{
			return value;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000F04B2 File Offset: 0x000EE6B2
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static short SetValidValue(short value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000F04B5 File Offset: 0x000EE6B5
		protected internal static short SetValidValue(short value)
		{
			return value;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000F04B8 File Offset: 0x000EE6B8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static short? SetValidValue(short? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000F04BB File Offset: 0x000EE6BB
		protected internal static short? SetValidValue(short? value)
		{
			return value;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000F04BE File Offset: 0x000EE6BE
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static int SetValidValue(int value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000F04C1 File Offset: 0x000EE6C1
		protected internal static int SetValidValue(int value)
		{
			return value;
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000F04C4 File Offset: 0x000EE6C4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static int? SetValidValue(int? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x000F04C7 File Offset: 0x000EE6C7
		protected internal static int? SetValidValue(int? value)
		{
			return value;
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000F04CA File Offset: 0x000EE6CA
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static long SetValidValue(long value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000F04CD File Offset: 0x000EE6CD
		protected internal static long SetValidValue(long value)
		{
			return value;
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000F04D0 File Offset: 0x000EE6D0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static long? SetValidValue(long? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000F04D3 File Offset: 0x000EE6D3
		protected internal static long? SetValidValue(long? value)
		{
			return value;
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000F04D6 File Offset: 0x000EE6D6
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		[CLSCompliant(false)]
		protected internal static ushort SetValidValue(ushort value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000F04D9 File Offset: 0x000EE6D9
		[CLSCompliant(false)]
		protected internal static ushort SetValidValue(ushort value)
		{
			return value;
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000F04DC File Offset: 0x000EE6DC
		[CLSCompliant(false)]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		protected internal static ushort? SetValidValue(ushort? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000F04DF File Offset: 0x000EE6DF
		[CLSCompliant(false)]
		protected internal static ushort? SetValidValue(ushort? value)
		{
			return value;
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000F04E2 File Offset: 0x000EE6E2
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		[CLSCompliant(false)]
		protected internal static uint SetValidValue(uint value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000F04E5 File Offset: 0x000EE6E5
		[CLSCompliant(false)]
		protected internal static uint SetValidValue(uint value)
		{
			return value;
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000F04E8 File Offset: 0x000EE6E8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		[CLSCompliant(false)]
		protected internal static uint? SetValidValue(uint? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x000F04EB File Offset: 0x000EE6EB
		[CLSCompliant(false)]
		protected internal static uint? SetValidValue(uint? value)
		{
			return value;
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000F04EE File Offset: 0x000EE6EE
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		[CLSCompliant(false)]
		protected internal static ulong SetValidValue(ulong value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000F04F1 File Offset: 0x000EE6F1
		[CLSCompliant(false)]
		protected internal static ulong SetValidValue(ulong value)
		{
			return value;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000F04F4 File Offset: 0x000EE6F4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "propertyName")]
		[CLSCompliant(false)]
		protected internal static ulong? SetValidValue(ulong? value, string propertyName)
		{
			return value;
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000F04F7 File Offset: 0x000EE6F7
		[CLSCompliant(false)]
		protected internal static ulong? SetValidValue(ulong? value)
		{
			return value;
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000F04FA File Offset: 0x000EE6FA
		protected internal static string SetValidValue(string value, bool isNullable, string propertyName)
		{
			if (value == null && !isNullable)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return value;
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000F0509 File Offset: 0x000EE709
		protected internal static string SetValidValue(string value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000F0513 File Offset: 0x000EE713
		protected internal static DbGeography SetValidValue(DbGeography value, bool isNullable, string propertyName)
		{
			if (value == null && !isNullable)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return value;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000F0522 File Offset: 0x000EE722
		protected internal static DbGeography SetValidValue(DbGeography value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000F052C File Offset: 0x000EE72C
		protected internal static DbGeometry SetValidValue(DbGeometry value, bool isNullable, string propertyName)
		{
			if (value == null && !isNullable)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return value;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000F053B File Offset: 0x000EE73B
		protected internal static DbGeometry SetValidValue(DbGeometry value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000F0548 File Offset: 0x000EE748
		protected internal T SetValidValue<T>(T oldValue, T newValue, string property) where T : ComplexObject
		{
			if (newValue == null && this.IsChangeTracked)
			{
				throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(property));
			}
			if (oldValue != null)
			{
				oldValue.DetachFromParent();
			}
			if (newValue != null)
			{
				newValue.AttachToParent(this, property);
			}
			return newValue;
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000F059E File Offset: 0x000EE79E
		protected internal static TComplex VerifyComplexObjectIsNotNull<TComplex>(TComplex complexObject, string propertyName) where TComplex : ComplexObject
		{
			if (complexObject == null)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return complexObject;
		}

		// Token: 0x04001366 RID: 4966
		public const string EntityKeyPropertyName = "-EntityKey-";
	}
}
