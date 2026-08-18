using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001DF RID: 479
	internal class EnumCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000679CE File Offset: 0x00065BCE
		internal new static EnumCodeDomSerializer Default
		{
			get
			{
				if (EnumCodeDomSerializer.defaultSerializer == null)
				{
					EnumCodeDomSerializer.defaultSerializer = new EnumCodeDomSerializer();
				}
				return EnumCodeDomSerializer.defaultSerializer;
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x000679E8 File Offset: 0x00065BE8
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			CodeExpression codeExpression = null;
			using (CodeDomSerializerBase.TraceScope("EnumCodeDomSerializer::Serialize"))
			{
				if (value is Enum)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(value);
					Enum[] array;
					bool flag;
					if (converter != null && converter.CanConvertTo(typeof(Enum[])))
					{
						array = (Enum[])converter.ConvertTo(value, typeof(Enum[]));
						flag = (array.Length > 1);
					}
					else
					{
						array = new Enum[]
						{
							(Enum)value
						};
						flag = true;
					}
					CodeTypeReferenceExpression targetObject = new CodeTypeReferenceExpression(value.GetType());
					TypeConverter typeConverter = new EnumConverter(value.GetType());
					foreach (Enum value2 in array)
					{
						string text = (typeConverter != null) ? typeConverter.ConvertToString(value2) : null;
						CodeExpression codeExpression2 = (!string.IsNullOrEmpty(text)) ? new CodeFieldReferenceExpression(targetObject, text) : null;
						if (codeExpression2 != null)
						{
							if (codeExpression == null)
							{
								codeExpression = codeExpression2;
							}
							else
							{
								codeExpression = new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.BitwiseOr, codeExpression2);
							}
						}
					}
					if (codeExpression != null && flag)
					{
						codeExpression = new CodeCastExpression(value.GetType(), codeExpression);
					}
				}
			}
			return codeExpression;
		}

		// Token: 0x040009F1 RID: 2545
		private static EnumCodeDomSerializer defaultSerializer;
	}
}
