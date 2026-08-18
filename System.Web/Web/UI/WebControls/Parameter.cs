using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000509 RID: 1289
	[DefaultProperty("DefaultValue")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Parameter : ICloneable, IStateManager
	{
		// Token: 0x06003EC1 RID: 16065 RVA: 0x001053D0 File Offset: 0x001043D0
		public Parameter()
		{
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x001053D8 File Offset: 0x001043D8
		public Parameter(string name)
		{
			this.Name = name;
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x001053E7 File Offset: 0x001043E7
		public Parameter(string name, DbType dbType)
		{
			this.Name = name;
			this.DbType = dbType;
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x001053FD File Offset: 0x001043FD
		public Parameter(string name, DbType dbType, string defaultValue)
		{
			this.Name = name;
			this.DbType = dbType;
			this.DefaultValue = defaultValue;
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x0010541A File Offset: 0x0010441A
		public Parameter(string name, TypeCode type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x00105430 File Offset: 0x00104430
		public Parameter(string name, TypeCode type, string defaultValue)
		{
			this.Name = name;
			this.Type = type;
			this.DefaultValue = defaultValue;
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00105450 File Offset: 0x00104450
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

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x001054B7 File Offset: 0x001044B7
		protected bool IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x001054C0 File Offset: 0x001044C0
		// (set) Token: 0x06003ECA RID: 16074 RVA: 0x001054EA File Offset: 0x001044EA
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

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x00105528 File Offset: 0x00104528
		// (set) Token: 0x06003ECC RID: 16076 RVA: 0x0010554C File Offset: 0x0010454C
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

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x00105574 File Offset: 0x00104574
		// (set) Token: 0x06003ECE RID: 16078 RVA: 0x0010559D File Offset: 0x0010459D
		[WebCategory("Parameter")]
		[DefaultValue(ParameterDirection.Input)]
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

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x001055C4 File Offset: 0x001045C4
		// (set) Token: 0x06003ED0 RID: 16080 RVA: 0x001055F1 File Offset: 0x001045F1
		[WebSysDescription("Parameter_Name")]
		[WebCategory("Parameter")]
		[DefaultValue("")]
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

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x00105618 File Offset: 0x00104618
		[Browsable(false)]
		internal object ParameterValue
		{
			get
			{
				return this.GetValue(this.ViewState["ParameterValue"], false);
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x00105634 File Offset: 0x00104634
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

		// Token: 0x06003ED3 RID: 16083 RVA: 0x00105684 File Offset: 0x00104684
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

		// Token: 0x06003ED4 RID: 16084 RVA: 0x001056F4 File Offset: 0x001046F4
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
				return TimeSpan.Parse(value.ToString());
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

		// Token: 0x06003ED5 RID: 16085 RVA: 0x001057AC File Offset: 0x001047AC
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

		// Token: 0x06003ED6 RID: 16086 RVA: 0x00105800 File Offset: 0x00104800
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

		// Token: 0x06003ED7 RID: 16087 RVA: 0x0010583F File Offset: 0x0010483F
		private static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003ED8 RID: 16088 RVA: 0x00105860 File Offset: 0x00104860
		// (set) Token: 0x06003ED9 RID: 16089 RVA: 0x00105889 File Offset: 0x00104889
		[WebSysDescription("Parameter_Size")]
		[DefaultValue(0)]
		[WebCategory("Parameter")]
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

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06003EDA RID: 16090 RVA: 0x001058B0 File Offset: 0x001048B0
		// (set) Token: 0x06003EDB RID: 16091 RVA: 0x001058D9 File Offset: 0x001048D9
		[WebCategory("Parameter")]
		[WebSysDescription("Parameter_Type")]
		[DefaultValue(TypeCode.Empty)]
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

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x00105914 File Offset: 0x00104914
		// (set) Token: 0x06003EDD RID: 16093 RVA: 0x0010593D File Offset: 0x0010493D
		[DefaultValue(true)]
		[WebSysDescription("Parameter_ConvertEmptyStringToNull")]
		[WebCategory("Parameter")]
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

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x00105964 File Offset: 0x00104964
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

		// Token: 0x06003EDF RID: 16095 RVA: 0x00105992 File Offset: 0x00104992
		protected virtual Parameter Clone()
		{
			return new Parameter(this);
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x0010599C File Offset: 0x0010499C
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

		// Token: 0x06003EE1 RID: 16097 RVA: 0x00105A4C File Offset: 0x00104A4C
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

		// Token: 0x06003EE2 RID: 16098 RVA: 0x00105AD9 File Offset: 0x00104AD9
		protected virtual object Evaluate(HttpContext context, Control control)
		{
			return null;
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x00105ADC File Offset: 0x00104ADC
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.ViewState.LoadViewState(savedState);
			}
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x00105AED File Offset: 0x00104AED
		protected void OnParameterChanged()
		{
			if (this._owner != null)
			{
				this._owner.CallOnParametersChanged();
			}
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x00105B02 File Offset: 0x00104B02
		protected virtual object SaveViewState()
		{
			if (this._viewState == null)
			{
				return null;
			}
			return this._viewState.SaveViewState();
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x00105B19 File Offset: 0x00104B19
		protected internal virtual void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x00105B27 File Offset: 0x00104B27
		internal void SetOwner(ParameterCollection owner)
		{
			this._owner = owner;
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x00105B30 File Offset: 0x00104B30
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x00105B38 File Offset: 0x00104B38
		protected virtual void TrackViewState()
		{
			this._tracking = true;
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x00105B54 File Offset: 0x00104B54
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

		// Token: 0x06003EEB RID: 16107 RVA: 0x00105BA4 File Offset: 0x00104BA4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x00105BAC File Offset: 0x00104BAC
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x00105BB4 File Offset: 0x00104BB4
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x00105BBD File Offset: 0x00104BBD
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x00105BC5 File Offset: 0x00104BC5
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x040027A9 RID: 10153
		private ParameterCollection _owner;

		// Token: 0x040027AA RID: 10154
		private bool _tracking;

		// Token: 0x040027AB RID: 10155
		private StateBag _viewState;
	}
}
