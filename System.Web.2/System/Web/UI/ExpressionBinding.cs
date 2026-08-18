using System;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000287 RID: 647
	public sealed class ExpressionBinding
	{
		// Token: 0x06001E7A RID: 7802 RVA: 0x00061DA6 File Offset: 0x0005FFA6
		public ExpressionBinding(string propertyName, Type propertyType, string expressionPrefix, string expression) : this(propertyName, propertyType, expressionPrefix, expression, false, null)
		{
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00061DB5 File Offset: 0x0005FFB5
		internal ExpressionBinding(string propertyName, Type propertyType, string expressionPrefix, string expression, bool generated, object parsedExpressionData)
		{
			this._propertyName = propertyName;
			this._propertyType = propertyType;
			this._expression = expression;
			this._expressionPrefix = expressionPrefix;
			this._generated = generated;
			this._parsedExpressionData = parsedExpressionData;
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00061DEA File Offset: 0x0005FFEA
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x00061DF2 File Offset: 0x0005FFF2
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

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00061DFB File Offset: 0x0005FFFB
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x00061E03 File Offset: 0x00060003
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

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00061E0C File Offset: 0x0006000C
		public bool Generated
		{
			get
			{
				return this._generated;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00061E14 File Offset: 0x00060014
		public object ParsedExpressionData
		{
			get
			{
				return this._parsedExpressionData;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x00061E1C File Offset: 0x0006001C
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x00061E24 File Offset: 0x00060024
		public Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00061E2C File Offset: 0x0006002C
		public override int GetHashCode()
		{
			return this._propertyName.ToLower(CultureInfo.InvariantCulture).GetHashCode();
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00061E44 File Offset: 0x00060044
		public override bool Equals(object obj)
		{
			if (obj != null && obj is ExpressionBinding)
			{
				ExpressionBinding expressionBinding = (ExpressionBinding)obj;
				return StringUtil.EqualsIgnoreCase(this._propertyName, expressionBinding.PropertyName);
			}
			return false;
		}

		// Token: 0x04001998 RID: 6552
		private string _propertyName;

		// Token: 0x04001999 RID: 6553
		private Type _propertyType;

		// Token: 0x0400199A RID: 6554
		private string _expression;

		// Token: 0x0400199B RID: 6555
		private string _expressionPrefix;

		// Token: 0x0400199C RID: 6556
		private bool _generated;

		// Token: 0x0400199D RID: 6557
		private object _parsedExpressionData;
	}
}
