using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Configuration
{
	// Token: 0x02000061 RID: 97
	public sealed class GenericEnumConverter : ConfigurationConverterBase
	{
		// Token: 0x060003CF RID: 975 RVA: 0x00013DB5 File Offset: 0x00011FB5
		public GenericEnumConverter(Type typeEnum)
		{
			if (typeEnum == null)
			{
				throw new ArgumentNullException("typeEnum");
			}
			this._enumType = typeEnum;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00013DD8 File Offset: 0x00011FD8
		public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
		{
			return value.ToString();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00013DE0 File Offset: 0x00011FE0
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			object result = null;
			try
			{
				string text = (string)data;
				if (string.IsNullOrEmpty(text))
				{
					throw new Exception();
				}
				if (!string.IsNullOrEmpty(text) && (char.IsDigit(text[0]) || text[0] == '-' || text[0] == '+'))
				{
					throw new Exception();
				}
				if (text != text.Trim())
				{
					throw new Exception();
				}
				result = Enum.Parse(this._enumType, text);
			}
			catch
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in Enum.GetNames(this._enumType))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(value);
				}
				throw new ArgumentException(SR.GetString("Invalid_enum_value", new object[]
				{
					stringBuilder.ToString()
				}));
			}
			return result;
		}

		// Token: 0x04000281 RID: 641
		private Type _enumType;
	}
}
