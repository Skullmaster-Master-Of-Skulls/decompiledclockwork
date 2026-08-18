using System;
using System.CodeDom;
using System.Collections;
using System.Resources;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001F4 RID: 500
	internal class LocalizationCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x0006ED19 File Offset: 0x0006CF19
		internal LocalizationCodeDomSerializer(CodeDomLocalizationModel model, object currentSerializer)
		{
			this._model = model;
			this._currentSerializer = (currentSerializer as CodeDomSerializer);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0006ED34 File Offset: 0x0006CF34
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

		// Token: 0x060012FE RID: 4862 RVA: 0x0006ED84 File Offset: 0x0006CF84
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)manager.Context[typeof(PropertyDescriptor)];
			ExpressionContext expressionContext = (ExpressionContext)manager.Context[typeof(ExpressionContext)];
			bool flag = value == null || CodeDomSerializerBase.GetReflectionTypeHelper(manager, value).IsSerializable;
			bool flag2 = !flag;
			bool flag3 = propertyDescriptor != null && propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			if (!flag2)
			{
				flag2 = (expressionContext != null && expressionContext.PresetValue != null && expressionContext.PresetValue == value);
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

		// Token: 0x04000A51 RID: 2641
		private CodeDomLocalizationModel _model;

		// Token: 0x04000A52 RID: 2642
		private CodeDomSerializer _currentSerializer;

		// Token: 0x020004B9 RID: 1209
		private class ApplyMethodTable
		{
			// Token: 0x06002C2A RID: 11306 RVA: 0x00106E82 File Offset: 0x00105082
			internal bool Contains(object value)
			{
				return this._table.ContainsKey(value);
			}

			// Token: 0x06002C2B RID: 11307 RVA: 0x00106E90 File Offset: 0x00105090
			internal void Add(object value)
			{
				this._table.Add(value, value);
			}

			// Token: 0x04001E93 RID: 7827
			private Hashtable _table = new Hashtable();
		}
	}
}
