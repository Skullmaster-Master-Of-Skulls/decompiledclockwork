using System;
using System.Runtime.Serialization;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000E6 RID: 230
	[Serializable]
	public class ConversionNotSupportedException : ApplicationException
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x00014F6B File Offset: 0x0001316B
		public ConversionNotSupportedException()
		{
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00014F73 File Offset: 0x00013173
		public ConversionNotSupportedException(string message) : base(message)
		{
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00014F7C File Offset: 0x0001317C
		public ConversionNotSupportedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00014F86 File Offset: 0x00013186
		protected ConversionNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00014F90 File Offset: 0x00013190
		public static ConversionNotSupportedException Create(Type destinationType, object sourceValue)
		{
			return ConversionNotSupportedException.Create(destinationType, sourceValue, null);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00014F9C File Offset: 0x0001319C
		public static ConversionNotSupportedException Create(Type destinationType, object sourceValue, Exception innerException)
		{
			if (sourceValue == null)
			{
				return new ConversionNotSupportedException("Cannot convert value [null] to type [" + destinationType + "]", innerException);
			}
			return new ConversionNotSupportedException(string.Concat(new object[]
			{
				"Cannot convert from type [",
				sourceValue.GetType(),
				"] value [",
				sourceValue,
				"] to type [",
				destinationType,
				"]"
			}), innerException);
		}
	}
}
