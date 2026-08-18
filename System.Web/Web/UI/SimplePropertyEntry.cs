using System;
using System.CodeDom;
using System.Security.Permissions;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x0200045F RID: 1119
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SimplePropertyEntry : PropertyEntry
	{
		// Token: 0x060034F6 RID: 13558 RVA: 0x000E5524 File Offset: 0x000E4524
		internal SimplePropertyEntry()
		{
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000E552C File Offset: 0x000E452C
		// (set) Token: 0x060034F8 RID: 13560 RVA: 0x000E5534 File Offset: 0x000E4534
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

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000E553D File Offset: 0x000E453D
		// (set) Token: 0x060034FA RID: 13562 RVA: 0x000E5545 File Offset: 0x000E4545
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

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x000E554E File Offset: 0x000E454E
		// (set) Token: 0x060034FC RID: 13564 RVA: 0x000E5556 File Offset: 0x000E4556
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

		// Token: 0x060034FD RID: 13565 RVA: 0x000E5560 File Offset: 0x000E4560
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

		// Token: 0x0400250C RID: 9484
		private string _persistedValue;

		// Token: 0x0400250D RID: 9485
		private bool _useSetAttribute;

		// Token: 0x0400250E RID: 9486
		private object _value;
	}
}
