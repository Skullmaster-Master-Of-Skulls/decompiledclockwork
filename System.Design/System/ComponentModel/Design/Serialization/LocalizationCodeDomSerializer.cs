using System;
using System.CodeDom;
using System.Collections;
using System.Resources;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200058C RID: 1420
	internal class LocalizationCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06003263 RID: 12899 RVA: 0x0011D1F8 File Offset: 0x0011C1F8
		internal LocalizationCodeDomSerializer(CodeDomLocalizationModel model, object currentSerializer)
		{
			this._model = model;
			this._currentSerializer = (currentSerializer as CodeDomSerializer);
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x0011D214 File Offset: 0x0011C214
		private bool EmitApplyMethod(IDesignerSerializationManager manager, object owner)
		{
			LocalizationCodeDomSerializer.ApplyMethodTable applyMethodTable = (LocalizationCodeDomSerializer.ApplyMethodTable)manager.Context[typeof(LocalizationCodeDomSerializer.ApplyMethodTable)];
			if (applyMethodTable == null)
			{
				applyMethodTable = new LocalizationCodeDomSerializer.ApplyMethodTable();
				manager.Context.Append(applyMethodTable);
			}
			if (!applyMethodTable.Contains(owner))
			{
				applyMethodTable.Add(owner);
				return true;
			}
			return false;
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x0011D264 File Offset: 0x0011C264
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)manager.Context[typeof(PropertyDescriptor)];
			ExpressionContext expressionContext = (ExpressionContext)manager.Context[typeof(ExpressionContext)];
			bool flag = value == null || TypeDescriptor.GetReflectionType(value).IsSerializable;
			bool flag2 = !flag;
			bool flag3 = propertyDescriptor != null && propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			if (!flag2)
			{
				flag2 = (expressionContext != null && expressionContext.PresetValue == value);
			}
			if (this._model == CodeDomLocalizationModel.PropertyReflection && !flag3 && !flag2)
			{
				CodeStatementCollection codeStatementCollection = (CodeStatementCollection)manager.Context[typeof(CodeStatementCollection)];
				bool flag4 = false;
				if (propertyDescriptor != null)
				{
					ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = propertyDescriptor.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
					if (extenderProvidedPropertyAttribute != null && extenderProvidedPropertyAttribute.ExtenderProperty != null)
					{
						flag4 = true;
					}
				}
				if (!flag4 && expressionContext != null && codeStatementCollection != null)
				{
					string text = manager.GetName(expressionContext.Owner);
					CodeExpression codeExpression = base.SerializeToExpression(manager, expressionContext.Owner);
					if (text != null && codeExpression != null)
					{
						RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
						if (rootContext != null && rootContext.Value == expressionContext.Owner)
						{
							text = "$this";
						}
						base.SerializeToResourceExpression(manager, value, false);
						if (this.EmitApplyMethod(manager, expressionContext.Owner))
						{
							ResourceManager value2 = manager.Context[typeof(ResourceManager)] as ResourceManager;
							CodeExpression expression = base.GetExpression(manager, value2);
							CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(expression, "ApplyResources");
							codeStatementCollection.Add(new CodeMethodInvokeExpression
							{
								Method = method,
								Parameters = 
								{
									codeExpression,
									new CodePrimitiveExpression(text)
								}
							});
						}
						return null;
					}
				}
			}
			if (flag2)
			{
				return this._currentSerializer.Serialize(manager, value);
			}
			return base.SerializeToResourceExpression(manager, value);
		}

		// Token: 0x04002177 RID: 8567
		private CodeDomLocalizationModel _model;

		// Token: 0x04002178 RID: 8568
		private CodeDomSerializer _currentSerializer;

		// Token: 0x0200058D RID: 1421
		private class ApplyMethodTable
		{
			// Token: 0x06003266 RID: 12902 RVA: 0x0011D46E File Offset: 0x0011C46E
			internal bool Contains(object value)
			{
				return this._table.ContainsKey(value);
			}

			// Token: 0x06003267 RID: 12903 RVA: 0x0011D47C File Offset: 0x0011C47C
			internal void Add(object value)
			{
				this._table.Add(value, value);
			}

			// Token: 0x04002179 RID: 8569
			private Hashtable _table = new Hashtable();
		}
	}
}
