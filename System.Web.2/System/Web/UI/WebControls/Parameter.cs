using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200049C RID: 1180
	[DefaultProperty("DefaultValue")]
	public class Parameter : ICloneable, IStateManager
	{
		// Token: 0x06003AA6 RID: 15014 RVA: 0x000030B5 File Offset: 0x000012B5
		public Parameter()
		{
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000BE668 File Offset: 0x000BC868
		public Parameter(string name)
		{
			this.Name = name;
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000BE677 File Offset: 0x000BC877
		public Parameter(string name, DbType dbType)
		{
			this.Name = name;
			this.DbType = dbType;
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000BE68D File Offset: 0x000BC88D
		public Parameter(string name, DbType dbType, string defaultValue)
		{
			this.Name = name;
			this.DbType = dbType;
			this.DefaultValue = defaultValue;
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000BE6AA File Offset: 0x000BC8AA
		public Parameter(string name, TypeCode type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000BE6C0 File Offset: 0x000BC8C0
		public Parameter(string name, TypeCode type, string defaultValue)
		{
			this.Name = name;
			this.Type = type;
			this.DefaultValue = defaultValue;
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000BE6E0 File Offset: 0x000BC8E0
		protected Parameter(Parameter original)
		{
			this.DefaultValue = original.DefaultValue;
			this.Direction = original.Direction;
			this.Name = original.Name;
			this.ConvertEmptyStringToNull = original.ConvertEmptyStringToNull;
			this.Size = original.Size;
			this.Type = original.Type;
			this.DbType = original.DbType;
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x000BE747 File Offset: 0x000BC947
		protected bool IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x000BE750 File Offset: 0x000BC950
		// (set) Token: 0x06003AAF RID: 15023 RVA: 0x000BE77A File Offset: 0x000BC97A
		[DefaultValue(DbType.Object)]
		[WebCategory("Parameter")]
		[WebSysDescription("Parameter_DbType")]
		public DbType DbType
		{
			get
			{
				object obj = this.ViewState["DbType"];
				if (obj == null)
				{
					return DbType.Object;
				}
				return (DbType)obj;
			}
			set
			{
				if (value < DbType.AnsiString || value > DbType.DateTimeOffset)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.DbType != value)
				{
					this.ViewState["DbType"] = value;
					this.OnParameterChanged();
				}
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x000BE7B8 File Offset: 0x000BC9B8
		// (set) Token: 0x06003AB1 RID: 15025 RVA: 0x000BE7DC File Offset: 0x000BC9DC
		[DefaultValue(null)]
		[WebCategory("Parameter")]
		[WebSysDescription("Parameter_DefaultValue")]
		public string DefaultValue
		{
			get
			{
				object obj = this.ViewState["DefaultValue"];
				return obj as string;
			}
			set
			{
				if (this.DefaultValue != value)
				{
					this.ViewState["DefaultValue"] = value;
					this.OnParameterChanged();
				}
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x000BE804 File Offset: 0x000BCA04
		// (set) Token: 0x06003AB3 RID: 15027 RVA: 0x000BE82D File Offset: 0x000BCA2D
		[DefaultValue(ParameterDirection.Input)]
		[WebCategory("Parameter")]
		[WebSysDescription("Parameter_Direction")]
		public ParameterDirection Direction
		{
			get
			{
				object obj = this.ViewState["Direction"];
				if (obj == null)
				{
					return ParameterDirection.Input;
				}
				return (ParameterDirection)obj;
			}
			set
			{
				if (this.Direction != value)
				{
					this.ViewState["Direction"] = value;
					this.OnParameterChanged();
				}
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x000BE854 File Offset: 0x000BCA54
		// (set) Token: 0x06003AB5 RID: 15029 RVA: 0x000BE881 File Offset: 0x000BCA81
		[DefaultValue("")]
		[WebCategory("Parameter")]
		[WebSysDescription("Parameter_Name")]
		public string Name
		{
			get
			{
				object obj = this.ViewState["Name"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				if (this.Name != value)
				{
					this.ViewState["Name"] = value;
					this.OnParameterChanged();
				}
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x000BE8A8 File Offset: 0x000BCAA8
		[Browsable(false)]
		internal object ParameterValue
		{
			get
			{
				return this.GetValue(this.ViewState["ParameterValue"], false);
			}
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000BE8C4 File Offset: 0x000BCAC4
		public DbType GetDatabaseType()
		{
			DbType dbType = this.DbType;
			if (dbType == DbType.Object)
			{
				return Parameter.ConvertTypeCodeToDbType(this.Type);
			}
			if (this.Type != TypeCode.Empty)
			{
				throw new InvalidOperationException(SR.GetString("Parameter_TypeNotSupported", new object[]
				{
					this.Name
				}));
			}
			return dbType;
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000BE914 File Offset: 0x000BCB14
		internal object GetValue(object value, bool ignoreNullableTypeChanges)
		{
			DbType dbType = this.DbType;
			if (dbType == DbType.Object)
			{
				return Parameter.GetValue(value, this.DefaultValue, this.Type, this.ConvertEmptyStringToNull, ignoreNullableTypeChanges);
			}
			if (this.Type != TypeCode.Empty)
			{
				throw new InvalidOperationException(SR.GetString("Parameter_TypeNotSupported", new object[]
				{
					this.Name
				}));
			}
			return Parameter.GetValue(value, this.DefaultValue, dbType, this.ConvertEmptyStringToNull, ignoreNullableTypeChanges);
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000BE984 File Offset: 0x000BCB84
		internal static object GetValue(object value, string defaultValue, DbType dbType, bool convertEmptyStringToNull, bool ignoreNullableTypeChanges)
		{
			if (dbType != DbType.DateTimeOffset && dbType != DbType.Time && dbType != DbType.Guid)
			{
				TypeCode type = Parameter.ConvertDbTypeToTypeCode(dbType);
				return Parameter.GetValue(value, defaultValue, type, convertEmptyStringToNull, ignoreNullableTypeChanges);
			}
			value = Parameter.HandleNullValue(value, defaultValue, convertEmptyStringToNull);
			if (value == null)
			{
				return null;
			}
			if (ignoreNullableTypeChanges && Parameter.IsNullableType(value.GetType()))
			{
				return value;
			}
			if (dbType == DbType.DateTimeOffset)
			{
				if (value is DateTimeOffset)
				{
					return value;
				}
				return DateTimeOffset.Parse(value.ToString(), CultureInfo.CurrentCulture);
			}
			else if (dbType == DbType.Time)
			{
				if (value is TimeSpan)
				{
					return value;
				}
				return TimeSpan.Parse(value.ToString(), CultureInfo.CurrentCulture);
			}
			else
			{
				if (dbType != DbType.Guid)
				{
					return null;
				}
				if (value is Guid)
				{
					return value;
				}
				return new Guid(value.ToString());
			}
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000BEA40 File Offset: 0x000BCC40
		internal static object GetValue(object value, string defaultValue, TypeCode type, bool convertEmptyStringToNull, bool ignoreNullableTypeChanges)
		{
			if (type == TypeCode.DBNull)
			{
				return DBNull.Value;
			}
			value = Parameter.HandleNullValue(value, defaultValue, convertEmptyStringToNull);
			if (value == null)
			{
				return null;
			}
			if (type == TypeCode.Object || type == TypeCode.Empty)
			{
				return value;
			}
			if (ignoreNullableTypeChanges && Parameter.IsNullableType(value.GetType()))
			{
				return value;
			}
			return value = Convert.ChangeType(value, type, CultureInfo.CurrentCulture);
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000BEA94 File Offset: 0x000BCC94
		private static object HandleNullValue(object value, string defaultValue, bool convertEmptyStringToNull)
		{
			if (convertEmptyStringToNull)
			{
				string text = value as string;
				if (text != null && text.Length == 0)
				{
					value = null;
				}
			}
			if (value == null)
			{
				if (convertEmptyStringToNull && string.IsNullOrEmpty(defaultValue))
				{
					defaultValue = null;
				}
				if (defaultValue == null)
				{
					return null;
				}
				value = defaultValue;
			}
			return value;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000BEAD3 File Offset: 0x000BCCD3
		private static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x000BEAF4 File Offset: 0x000BCCF4
		// (set) Token: 0x06003ABE RID: 15038 RVA: 0x000BEB1D File Offset: 0x000BCD1D
		[DefaultValue(0)]
		[WebCategory("Parameter")]
		[WebSysDescription("Parameter_Size")]
		public int Size
		{
			get
			{
				object obj = this.ViewState["Size"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				if (this.Size != value)
				{
					this.ViewState["Size"] = value;
					this.OnParameterChanged();
				}
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000BEB44 File Offset: 0x000BCD44
		// (set) Token: 0x06003AC0 RID: 15040 RVA: 0x000BEB6D File Offset: 0x000BCD6D
		[DefaultValue(TypeCode.Empty)]
		[WebCategory("Parameter")]
		[WebSysDescription("Parameter_Type")]
		public TypeCode Type
		{
			get
			{
				object obj = this.ViewState["Type"];
				if (obj == null)
				{
					return TypeCode.Empty;
				}
				return (TypeCode)obj;
			}
			set
			{
				if (value < TypeCode.Empty || value > TypeCode.String)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.Type != value)
				{
					this.ViewState["Type"] = value;
					this.OnParameterChanged();
				}
			}
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x000BEBA8 File Offset: 0x000BCDA8
		// (set) Token: 0x06003AC2 RID: 15042 RVA: 0x000BEBD1 File Offset: 0x000BCDD1
		[DefaultValue(true)]
		[WebCategory("Parameter")]
		[WebSysDescription("Parameter_ConvertEmptyStringToNull")]
		public bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = this.ViewState["ConvertEmptyStringToNull"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (this.ConvertEmptyStringToNull != value)
				{
					this.ViewState["ConvertEmptyStringToNull"] = value;
					this.OnParameterChanged();
				}
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x000BEBF8 File Offset: 0x000BCDF8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag();
					if (this._tracking)
					{
						this._viewState.TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000BEC26 File Offset: 0x000BCE26
		protected virtual Parameter Clone()
		{
			return new Parameter(this);
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000BEC30 File Offset: 0x000BCE30
		public static TypeCode ConvertDbTypeToTypeCode(DbType dbType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
			case DbType.String:
			case DbType.AnsiStringFixedLength:
			case DbType.StringFixedLength:
				return TypeCode.String;
			case DbType.Byte:
				return TypeCode.Byte;
			case DbType.Boolean:
				return TypeCode.Boolean;
			case DbType.Currency:
			case DbType.Decimal:
			case DbType.VarNumeric:
				return TypeCode.Decimal;
			case DbType.Date:
			case DbType.DateTime:
			case DbType.Time:
			case DbType.DateTime2:
				return TypeCode.DateTime;
			case DbType.Double:
				return TypeCode.Double;
			case DbType.Int16:
				return TypeCode.Int16;
			case DbType.Int32:
				return TypeCode.Int32;
			case DbType.Int64:
				return TypeCode.Int64;
			case DbType.SByte:
				return TypeCode.SByte;
			case DbType.Single:
				return TypeCode.Single;
			case DbType.UInt16:
				return TypeCode.UInt16;
			case DbType.UInt32:
				return TypeCode.UInt32;
			case DbType.UInt64:
				return TypeCode.UInt64;
			}
			return TypeCode.Object;
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000BECDC File Offset: 0x000BCEDC
		public static DbType ConvertTypeCodeToDbType(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Boolean:
				return DbType.Boolean;
			case TypeCode.Char:
				return DbType.StringFixedLength;
			case TypeCode.SByte:
				return DbType.SByte;
			case TypeCode.Byte:
				return DbType.Byte;
			case TypeCode.Int16:
				return DbType.Int16;
			case TypeCode.UInt16:
				return DbType.UInt16;
			case TypeCode.Int32:
				return DbType.Int32;
			case TypeCode.UInt32:
				return DbType.UInt32;
			case TypeCode.Int64:
				return DbType.Int64;
			case TypeCode.UInt64:
				return DbType.UInt64;
			case TypeCode.Single:
				return DbType.Single;
			case TypeCode.Double:
				return DbType.Double;
			case TypeCode.Decimal:
				return DbType.Decimal;
			case TypeCode.DateTime:
				return DbType.DateTime;
			case TypeCode.String:
				return DbType.String;
			}
			return DbType.Object;
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x0000298D File Offset: 0x00000B8D
		protected internal virtual object Evaluate(HttpContext context, Control control)
		{
			return null;
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000BED67 File Offset: 0x000BCF67
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.ViewState.LoadViewState(savedState);
			}
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000BED78 File Offset: 0x000BCF78
		protected void OnParameterChanged()
		{
			if (this._owner != null)
			{
				this._owner.CallOnParametersChanged();
			}
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000BED8D File Offset: 0x000BCF8D
		protected virtual object SaveViewState()
		{
			if (this._viewState == null)
			{
				return null;
			}
			return this._viewState.SaveViewState();
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x000BEDA4 File Offset: 0x000BCFA4
		protected internal virtual void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000BEDB2 File Offset: 0x000BCFB2
		internal void SetOwner(ParameterCollection owner)
		{
			this._owner = owner;
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000BEDBB File Offset: 0x000BCFBB
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x000BEDC3 File Offset: 0x000BCFC3
		protected virtual void TrackViewState()
		{
			this._tracking = true;
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x000BEDE0 File Offset: 0x000BCFE0
		internal void UpdateValue(HttpContext context, Control control)
		{
			object obj = this.ViewState["ParameterValue"];
			object obj2 = this.Evaluate(context, control);
			this.ViewState["ParameterValue"] = obj2;
			if ((obj2 == null && obj != null) || (obj2 != null && !obj2.Equals(obj)))
			{
				this.OnParameterChanged();
			}
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000BEE30 File Offset: 0x000BD030
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000BEE38 File Offset: 0x000BD038
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000BEE40 File Offset: 0x000BD040
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000BEE49 File Offset: 0x000BD049
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000BEE51 File Offset: 0x000BD051
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x0400230E RID: 8974
		private ParameterCollection _owner;

		// Token: 0x0400230F RID: 8975
		private bool _tracking;

		// Token: 0x04002310 RID: 8976
		private StateBag _viewState;
	}
}
