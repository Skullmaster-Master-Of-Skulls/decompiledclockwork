using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x0200024E RID: 590
	public class BoundPropertyEntry : PropertyEntry
	{
		// Token: 0x06001B1E RID: 6942 RVA: 0x000552AB File Offset: 0x000534AB
		internal BoundPropertyEntry()
		{
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x000552B3 File Offset: 0x000534B3
		// (set) Token: 0x06001B20 RID: 6944 RVA: 0x000552BB File Offset: 0x000534BB
		public string ControlID
		{
			get
			{
				return this._controlID;
			}
			set
			{
				this._controlID = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x000552C4 File Offset: 0x000534C4
		// (set) Token: 0x06001B22 RID: 6946 RVA: 0x000552CC File Offset: 0x000534CC
		public Type ControlType
		{
			get
			{
				return this._controlType;
			}
			set
			{
				this._controlType = value;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x000552D5 File Offset: 0x000534D5
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x000552DD File Offset: 0x000534DD
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

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x000552E6 File Offset: 0x000534E6
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x000552EE File Offset: 0x000534EE
		public ExpressionBuilder ExpressionBuilder
		{
			get
			{
				return this._expressionBuilder;
			}
			set
			{
				this._expressionBuilder = value;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x000552F7 File Offset: 0x000534F7
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x000552FF File Offset: 0x000534FF
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

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x00055308 File Offset: 0x00053508
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x00055310 File Offset: 0x00053510
		public string FieldName
		{
			get
			{
				return this._fieldName;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x00055319 File Offset: 0x00053519
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x00055321 File Offset: 0x00053521
		public string FormatString
		{
			get
			{
				return this._formatString;
			}
			set
			{
				this._formatString = value;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0005532A File Offset: 0x0005352A
		internal bool IsDataBindingEntry
		{
			get
			{
				return string.IsNullOrEmpty(this.ExpressionPrefix);
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x00055337 File Offset: 0x00053537
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0005533F File Offset: 0x0005353F
		internal int Column { get; set; }

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x00055348 File Offset: 0x00053548
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x00055350 File Offset: 0x00053550
		internal int Line { get; set; }

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x00055359 File Offset: 0x00053559
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x00055361 File Offset: 0x00053561
		public bool Generated
		{
			get
			{
				return this._generated;
			}
			set
			{
				this._generated = value;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0005536A File Offset: 0x0005356A
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x00055372 File Offset: 0x00053572
		public object ParsedExpressionData
		{
			get
			{
				return this._parsedExpressionData;
			}
			set
			{
				this._parsedExpressionData = value;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0005537B File Offset: 0x0005357B
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x00055383 File Offset: 0x00053583
		public bool ReadOnlyProperty
		{
			get
			{
				return this._readOnlyProperty;
			}
			set
			{
				this._readOnlyProperty = value;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0005538C File Offset: 0x0005358C
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x00055394 File Offset: 0x00053594
		public bool TwoWayBound
		{
			get
			{
				return this._twoWayBound;
			}
			set
			{
				this._twoWayBound = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0005539D File Offset: 0x0005359D
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x000553A5 File Offset: 0x000535A5
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

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x000553AE File Offset: 0x000535AE
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x000553B6 File Offset: 0x000535B6
		public bool IsEncoded { get; set; }

		// Token: 0x06001B3E RID: 6974 RVA: 0x000553BF File Offset: 0x000535BF
		internal void ParseExpression(ExpressionBuilderContext context)
		{
			if (this.Expression == null || this.ExpressionPrefix == null || this.ExpressionBuilder == null)
			{
				return;
			}
			this._parsedExpressionData = this.ExpressionBuilder.ParseExpression(this.Expression, base.Type, context);
		}

		// Token: 0x04001883 RID: 6275
		private string _expression;

		// Token: 0x04001884 RID: 6276
		private ExpressionBuilder _expressionBuilder;

		// Token: 0x04001885 RID: 6277
		private string _expressionPrefix;

		// Token: 0x04001886 RID: 6278
		private bool _useSetAttribute;

		// Token: 0x04001887 RID: 6279
		private object _parsedExpressionData;

		// Token: 0x04001888 RID: 6280
		private bool _generated;

		// Token: 0x04001889 RID: 6281
		private string _fieldName;

		// Token: 0x0400188A RID: 6282
		private string _formatString;

		// Token: 0x0400188B RID: 6283
		private string _controlID;

		// Token: 0x0400188C RID: 6284
		private Type _controlType;

		// Token: 0x0400188D RID: 6285
		private bool _readOnlyProperty;

		// Token: 0x0400188E RID: 6286
		private bool _twoWayBound;
	}
}
