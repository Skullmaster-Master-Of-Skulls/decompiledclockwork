using System;
using System.CodeDom;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020002F9 RID: 761
	public class SimplePropertyEntry : PropertyEntry
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x000552AB File Offset: 0x000534AB
		internal SimplePropertyEntry()
		{
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x00072842 File Offset: 0x00070A42
		// (set) Token: 0x0600232B RID: 9003 RVA: 0x0007284A File Offset: 0x00070A4A
		public string PersistedValue
		{
			get
			{
				return this._persistedValue;
			}
			set
			{
				this._persistedValue = value;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x00072853 File Offset: 0x00070A53
		// (set) Token: 0x0600232D RID: 9005 RVA: 0x0007285B File Offset: 0x00070A5B
		public bool UseSetAttribute
		{
			get
			{
				return this._useSetAttribute;
			}
			set
			{
				this._useSetAttribute = value;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x00072864 File Offset: 0x00070A64
		// (set) Token: 0x0600232F RID: 9007 RVA: 0x0007286C File Offset: 0x00070A6C
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00072878 File Offset: 0x00070A78
		internal CodeStatement GetCodeStatement(BaseTemplateCodeDomTreeGenerator generator, CodeExpression ctrlRefExpr)
		{
			if (this.UseSetAttribute)
			{
				return new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeCastExpression(typeof(IAttributeAccessor), ctrlRefExpr), "SetAttribute", new CodeExpression[0])
				{
					Parameters = 
					{
						new CodePrimitiveExpression(base.Name),
						new CodePrimitiveExpression(this.Value)
					}
				});
			}
			CodeExpression left;
			if (base.PropertyInfo != null)
			{
				left = CodeDomUtility.BuildPropertyReferenceExpression(ctrlRefExpr, base.Name);
			}
			else
			{
				left = new CodeFieldReferenceExpression(ctrlRefExpr, base.Name);
			}
			CodeExpression right;
			if (base.Type == typeof(string))
			{
				right = generator.BuildStringPropertyExpression(this);
			}
			else
			{
				right = CodeDomUtility.GenerateExpressionForValue(base.PropertyInfo, this.Value, base.Type);
			}
			return new CodeAssignStatement(left, right);
		}

		// Token: 0x04001CA2 RID: 7330
		private string _persistedValue;

		// Token: 0x04001CA3 RID: 7331
		private bool _useSetAttribute;

		// Token: 0x04001CA4 RID: 7332
		private object _value;
	}
}
