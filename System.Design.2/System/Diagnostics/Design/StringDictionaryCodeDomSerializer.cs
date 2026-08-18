using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace System.Diagnostics.Design
{
	// Token: 0x0200020B RID: 523
	internal class StringDictionaryCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x00003598 File Offset: 0x00001798
		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			return null;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0006F7AC File Offset: 0x0006D9AC
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			object result = null;
			StringDictionary stringDictionary = value as StringDictionary;
			if (stringDictionary != null)
			{
				object obj = manager.Context.Current;
				ExpressionContext expressionContext = obj as ExpressionContext;
				if (expressionContext != null && expressionContext.Owner == value)
				{
					obj = expressionContext.Expression;
				}
				CodePropertyReferenceExpression codePropertyReferenceExpression = obj as CodePropertyReferenceExpression;
				if (codePropertyReferenceExpression != null)
				{
					object obj2 = base.DeserializeExpression(manager, null, codePropertyReferenceExpression.TargetObject);
					if (obj2 != null)
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj2)[codePropertyReferenceExpression.PropertyName];
						if (propertyDescriptor != null)
						{
							CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
							CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(codePropertyReferenceExpression, "Add");
							foreach (object obj3 in stringDictionary)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
								CodeExpression codeExpression = base.SerializeToExpression(manager, dictionaryEntry.Key);
								CodeExpression codeExpression2 = base.SerializeToExpression(manager, dictionaryEntry.Value);
								if (codeExpression != null && codeExpression2 != null)
								{
									codeStatementCollection.Add(new CodeMethodInvokeExpression
									{
										Method = method,
										Parameters = 
										{
											codeExpression,
											codeExpression2
										}
									});
								}
							}
							result = codeStatementCollection;
						}
					}
				}
			}
			return result;
		}
	}
}
