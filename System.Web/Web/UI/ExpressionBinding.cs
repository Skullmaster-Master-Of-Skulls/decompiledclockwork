using System;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020003F1 RID: 1009
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ExpressionBinding
	{
		// Token: 0x060031E2 RID: 12770 RVA: 0x000DBCB6 File Offset: 0x000DACB6
		public ExpressionBinding(string propertyName, Type propertyType, string expressionPrefix, string expression) : this(propertyName, propertyType, expressionPrefix, expression, false, null)
		{
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000DBCC5 File Offset: 0x000DACC5
		internal ExpressionBinding(string propertyName, Type propertyType, string expressionPrefix, string expression, bool generated, object parsedExpressionData)
		{
			this._propertyName = propertyName;
			this._propertyType = propertyType;
			this._expression = expression;
			this._expressionPrefix = expressionPrefix;
			this._generated = generated;
			this._parsedExpressionData = parsedExpressionData;
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000DBCFA File Offset: 0x000DACFA
		// (set) Token: 0x060031E5 RID: 12773 RVA: 0x000DBD02 File Offset: 0x000DAD02
		public string Expression
		{
			get
			{
				return this._expression;
			}
			set
			{
				this._expression = value;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000DBD0B File Offset: 0x000DAD0B
		// (set) Token: 0x060031E7 RID: 12775 RVA: 0x000DBD13 File Offset: 0x000DAD13
		public string ExpressionPrefix
		{
			get
			{
				return this._expressionPrefix;
			}
			set
			{
				this._expressionPrefix = value;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000DBD1C File Offset: 0x000DAD1C
		public bool Generated
		{
			get
			{
				return this._generated;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000DBD24 File Offset: 0x000DAD24
		public object ParsedExpressionData
		{
			get
			{
				return this._parsedExpressionData;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x000DBD2C File Offset: 0x000DAD2C
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000DBD34 File Offset: 0x000DAD34
		public Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000DBD3C File Offset: 0x000DAD3C
		public override int GetHashCode()
		{
			return this._propertyName.ToLower(CultureInfo.InvariantCulture).GetHashCode();
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000DBD54 File Offset: 0x000DAD54
		public override bool Equals(object obj)
		{
			if (obj != null && obj is ExpressionBinding)
			{
				ExpressionBinding expressionBinding = (ExpressionBinding)obj;
				return StringUtil.EqualsIgnoreCase(this._propertyName, expressionBinding.PropertyName);
			}
			return false;
		}

		// Token: 0x040022E4 RID: 8932
		private string _propertyName;

		// Token: 0x040022E5 RID: 8933
		private Type _propertyType;

		// Token: 0x040022E6 RID: 8934
		private string _expression;

		// Token: 0x040022E7 RID: 8935
		private string _expressionPrefix;

		// Token: 0x040022E8 RID: 8936
		private bool _generated;

		// Token: 0x040022E9 RID: 8937
		private object _parsedExpressionData;
	}
}
