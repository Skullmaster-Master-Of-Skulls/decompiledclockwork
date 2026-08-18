using System;
using System.ComponentModel;
using System.Data.Common.Utils;
using System.Data.Spatial;
using System.Runtime.Serialization;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000199 RID: 409
	[DataContract(IsReference = true)]
	[Serializable]
	public abstract class StructuralObject : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001D8A RID: 7562 RVA: 0x0006658C File Offset: 0x0006478C
		// (remove) Token: 0x06001D8B RID: 7563 RVA: 0x000665C4 File Offset: 0x000647C4
		[NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001D8C RID: 7564 RVA: 0x000665FC File Offset: 0x000647FC
		// (remove) Token: 0x06001D8D RID: 7565 RVA: 0x00066634 File Offset: 0x00064834
		[NonSerialized]
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x06001D8E RID: 7566 RVA: 0x00066669 File Offset: 0x00064869
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x00066685 File Offset: 0x00064885
		protected virtual void OnPropertyChanging(string property)
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, new PropertyChangingEventArgs(property));
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000666A1 File Offset: 0x000648A1
		protected static DateTime DefaultDateTimeValue()
		{
			return DateTime.Now;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000666A8 File Offset: 0x000648A8
		protected virtual void ReportPropertyChanging(string property)
		{
			EntityUtil.CheckStringArgument(property, "property");
			this.OnPropertyChanging(property);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000666BC File Offset: 0x000648BC
		protected virtual void ReportPropertyChanged(string property)
		{
			EntityUtil.CheckStringArgument(property, "property");
			this.OnPropertyChanged(property);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000666D0 File Offset: 0x000648D0
		protected internal T GetValidValue<T>(T currentValue, string property, bool isNullable, bool isInitialized) where T : ComplexObject, new()
		{
			if (!isNullable && !isInitialized)
			{
				currentValue = this.SetValidValue<T>(currentValue, Activator.CreateInstance<T>(), property);
			}
			return currentValue;
		}

		// Token: 0x06001D94 RID: 7572
		internal abstract void ReportComplexPropertyChanging(string entityMemberName, ComplexObject complexObject, string complexMemberName);

		// Token: 0x06001D95 RID: 7573
		internal abstract void ReportComplexPropertyChanged(string entityMemberName, ComplexObject complexObject, string complexMemberName);

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001D96 RID: 7574
		internal abstract bool IsChangeTracked { get; }

		// Token: 0x06001D97 RID: 7575 RVA: 0x000666E9 File Offset: 0x000648E9
		protected internal static bool BinaryEquals(byte[] first, byte[] second)
		{
			return first == second || (first != null && second != null && ByValueEqualityComparer.CompareBinaryValues(first, second));
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00066700 File Offset: 0x00064900
		protected internal static byte[] GetValidValue(byte[] currentValue)
		{
			if (currentValue == null)
			{
				return null;
			}
			return (byte[])currentValue.Clone();
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00066712 File Offset: 0x00064912
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

		// Token: 0x06001D9A RID: 7578 RVA: 0x0006672D File Offset: 0x0006492D
		protected internal static byte[] SetValidValue(byte[] value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static bool SetValidValue(bool value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static bool SetValidValue(bool value)
		{
			return value;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static bool? SetValidValue(bool? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static bool? SetValidValue(bool? value)
		{
			return value;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static byte SetValidValue(byte value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static byte SetValidValue(byte value)
		{
			return value;
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static byte? SetValidValue(byte? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static byte? SetValidValue(byte? value)
		{
			return value;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static sbyte SetValidValue(sbyte value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static sbyte SetValidValue(sbyte value)
		{
			return value;
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static sbyte? SetValidValue(sbyte? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static sbyte? SetValidValue(sbyte? value)
		{
			return value;
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static DateTime SetValidValue(DateTime value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static DateTime SetValidValue(DateTime value)
		{
			return value;
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static DateTime? SetValidValue(DateTime? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static DateTime? SetValidValue(DateTime? value)
		{
			return value;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static TimeSpan SetValidValue(TimeSpan value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static TimeSpan SetValidValue(TimeSpan value)
		{
			return value;
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static TimeSpan? SetValidValue(TimeSpan? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static TimeSpan? SetValidValue(TimeSpan? value)
		{
			return value;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static DateTimeOffset SetValidValue(DateTimeOffset value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static DateTimeOffset SetValidValue(DateTimeOffset value)
		{
			return value;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static DateTimeOffset? SetValidValue(DateTimeOffset? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static DateTimeOffset? SetValidValue(DateTimeOffset? value)
		{
			return value;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static decimal SetValidValue(decimal value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static decimal SetValidValue(decimal value)
		{
			return value;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static decimal? SetValidValue(decimal? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static decimal? SetValidValue(decimal? value)
		{
			return value;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static double SetValidValue(double value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static double SetValidValue(double value)
		{
			return value;
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static double? SetValidValue(double? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static double? SetValidValue(double? value)
		{
			return value;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static float SetValidValue(float value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static float SetValidValue(float value)
		{
			return value;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static float? SetValidValue(float? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static float? SetValidValue(float? value)
		{
			return value;
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static Guid SetValidValue(Guid value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static Guid SetValidValue(Guid value)
		{
			return value;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static Guid? SetValidValue(Guid? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static Guid? SetValidValue(Guid? value)
		{
			return value;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static short SetValidValue(short value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static short SetValidValue(short value)
		{
			return value;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static short? SetValidValue(short? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static short? SetValidValue(short? value)
		{
			return value;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static int SetValidValue(int value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static int SetValidValue(int value)
		{
			return value;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static int? SetValidValue(int? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static int? SetValidValue(int? value)
		{
			return value;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static long SetValidValue(long value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static long SetValidValue(long value)
		{
			return value;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static long? SetValidValue(long? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00048AC0 File Offset: 0x00046CC0
		protected internal static long? SetValidValue(long? value)
		{
			return value;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static ushort SetValidValue(ushort value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static ushort SetValidValue(ushort value)
		{
			return value;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static ushort? SetValidValue(ushort? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static ushort? SetValidValue(ushort? value)
		{
			return value;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static uint SetValidValue(uint value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static uint SetValidValue(uint value)
		{
			return value;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static uint? SetValidValue(uint? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static uint? SetValidValue(uint? value)
		{
			return value;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static ulong SetValidValue(ulong value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static ulong SetValidValue(ulong value)
		{
			return value;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static ulong? SetValidValue(ulong? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x00048AC0 File Offset: 0x00046CC0
		[CLSCompliant(false)]
		protected internal static ulong? SetValidValue(ulong? value)
		{
			return value;
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00066737 File Offset: 0x00064937
		protected internal static string SetValidValue(string value, bool isNullable, string propertyName)
		{
			if (value == null && !isNullable)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return value;
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00066746 File Offset: 0x00064946
		protected internal static string SetValidValue(string value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00066737 File Offset: 0x00064937
		protected internal static DbGeography SetValidValue(DbGeography value, bool isNullable, string propertyName)
		{
			if (value == null && !isNullable)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return value;
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00066750 File Offset: 0x00064950
		protected internal static DbGeography SetValidValue(DbGeography value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00066737 File Offset: 0x00064937
		protected internal static DbGeometry SetValidValue(DbGeometry value, bool isNullable, string propertyName)
		{
			if (value == null && !isNullable)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return value;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0006675A File Offset: 0x0006495A
		protected internal static DbGeometry SetValidValue(DbGeometry value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00066764 File Offset: 0x00064964
		protected internal T SetValidValue<T>(T oldValue, T newValue, string property) where T : ComplexObject
		{
			if (newValue == null && this.IsChangeTracked)
			{
				throw EntityUtil.NullableComplexTypesNotSupported(property);
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

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000667B1 File Offset: 0x000649B1
		protected internal static TComplex VerifyComplexObjectIsNotNull<TComplex>(TComplex complexObject, string propertyName) where TComplex : ComplexObject
		{
			if (complexObject == null)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return complexObject;
		}

		// Token: 0x04000BD4 RID: 3028
		public static readonly string EntityKeyPropertyName = "-EntityKey-";
	}
}
