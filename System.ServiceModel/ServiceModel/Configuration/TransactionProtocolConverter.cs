using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006DE RID: 1758
	internal class TransactionProtocolConverter : TypeConverter
	{
		// Token: 0x060043E4 RID: 17380 RVA: 0x001006BE File Offset: 0x000FE8BE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x001006DC File Offset: 0x000FE8DC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x001006FC File Offset: 0x000FE8FC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (text == "OleTransactions")
			{
				return TransactionProtocol.OleTransactions;
			}
			if (text == "WSAtomicTransactionOctober2004")
			{
				return TransactionProtocol.WSAtomicTransactionOctober2004;
			}
			if (!(text == "WSAtomicTransaction11"))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ConfigInvalidTransactionFlowProtocolValue", new object[]
				{
					text
				}));
			}
			return TransactionProtocol.WSAtomicTransaction11;
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x00100778 File Offset: 0x000FE978
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (typeof(string) == destinationType && value is TransactionProtocol)
			{
				TransactionProtocol transactionProtocol = (TransactionProtocol)value;
				return transactionProtocol.Name;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
