using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001E5 RID: 485
	internal class PrimitiveCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00067D92 File Offset: 0x00065F92
		internal new static PrimitiveCodeDomSerializer Default
		{
			get
			{
				if (PrimitiveCodeDomSerializer.defaultSerializer == null)
				{
					PrimitiveCodeDomSerializer.defaultSerializer = new PrimitiveCodeDomSerializer();
				}
				return PrimitiveCodeDomSerializer.defaultSerializer;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00067DAC File Offset: 0x00065FAC
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			using (CodeDomSerializerBase.TraceScope("PrimitiveCodeDomSerializer::Serialize"))
			{
			}
			CodeExpression codeExpression = new CodePrimitiveExpression(value);
			if (value != null)
			{
				if (value is bool || value is char || value is int || value is float || value is double)
				{
					CodeDomProvider codeDomProvider = manager.GetService(typeof(CodeDomProvider)) as CodeDomProvider;
					if (codeDomProvider != null && string.Equals(codeDomProvider.FileExtension, PrimitiveCodeDomSerializer.JSharpFileExtension))
					{
						ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
						if (expressionContext != null && expressionContext.ExpressionType == typeof(object))
						{
							codeExpression = new CodeCastExpression(value.GetType(), codeExpression);
							codeExpression.UserData.Add("CastIsBoxing", true);
						}
					}
				}
				else if (value is string)
				{
					string text = value as string;
					if (text != null && text.Length > 200)
					{
						codeExpression = base.SerializeToResourceExpression(manager, text);
					}
				}
				else
				{
					codeExpression = new CodeCastExpression(new CodeTypeReference(value.GetType()), codeExpression);
				}
			}
			return codeExpression;
		}

		// Token: 0x040009F9 RID: 2553
		private static readonly string JSharpFileExtension = ".jsl";

		// Token: 0x040009FA RID: 2554
		private static PrimitiveCodeDomSerializer defaultSerializer;
	}
}
