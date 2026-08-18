using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Design;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000EF RID: 239
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ObjectDataSourceDesigner : DataSourceDesigner
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		internal Type SelectMethodReturnType
		{
			get
			{
				if (this._selectMethodReturnType == null)
				{
					string text = base.DesignerState["DataSourceSchemaSelectMethodReturnTypeName"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						this._selectMethodReturnType = ObjectDataSourceDesigner.GetType(base.Component.Site, text, true);
					}
				}
				return this._selectMethodReturnType;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0002E5FB File Offset: 0x0002C7FB
		public override bool CanConfigure
		{
			get
			{
				return this.TypeServiceAvailable;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0002E603 File Offset: 0x0002C803
		public override bool CanRefreshSchema
		{
			get
			{
				return !string.IsNullOrEmpty(this.TypeName) && !string.IsNullOrEmpty(this.SelectMethod) && this.TypeServiceAvailable;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0002E627 File Offset: 0x0002C827
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0002E639 File Offset: 0x0002C839
		internal object ShowOnlyDataComponentsState
		{
			get
			{
				return base.DesignerState["ShowOnlyDataComponentsState"];
			}
			set
			{
				base.DesignerState["ShowOnlyDataComponentsState"] = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0002E64C File Offset: 0x0002C84C
		private bool TypeServiceAvailable
		{
			get
			{
				IServiceProvider site = base.Component.Site;
				if (site == null)
				{
					return false;
				}
				ITypeResolutionService typeResolutionService = (ITypeResolutionService)site.GetService(typeof(ITypeResolutionService));
				ITypeDiscoveryService typeDiscoveryService = (ITypeDiscoveryService)site.GetService(typeof(ITypeDiscoveryService));
				return typeResolutionService != null || typeDiscoveryService != null;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0002E69F File Offset: 0x0002C89F
		internal ObjectDataSource ObjectDataSource
		{
			get
			{
				return (ObjectDataSource)base.Component;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0002E6AC File Offset: 0x0002C8AC
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x0002E6BC File Offset: 0x0002C8BC
		public string SelectMethod
		{
			get
			{
				return this.ObjectDataSource.SelectMethod;
			}
			set
			{
				if (value != this.SelectMethod)
				{
					this.ObjectDataSource.SelectMethod = value;
					this.UpdateDesignTimeHtml();
					if (this.CanRefreshSchema && !this._inWizard)
					{
						this.RefreshSchema(true);
						return;
					}
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0002E70C File Offset: 0x0002C90C
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0002E719 File Offset: 0x0002C919
		public string TypeName
		{
			get
			{
				return this.ObjectDataSource.TypeName;
			}
			set
			{
				if (value != this.TypeName)
				{
					this.ObjectDataSource.TypeName = value;
					this.UpdateDesignTimeHtml();
					if (this.CanRefreshSchema)
					{
						this.RefreshSchema(true);
						return;
					}
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002E756 File Offset: 0x0002C956
		public override void Configure()
		{
			this._inWizard = true;
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConfigureDataSourceChangeCallback), null, SR.GetString("DataSource_ConfigureTransactionDescription"));
			this._inWizard = false;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002E788 File Offset: 0x0002C988
		private bool ConfigureDataSourceChangeCallback(object context)
		{
			bool result;
			try
			{
				this.SuppressDataSourceEvents();
				IServiceProvider site = base.Component.Site;
				ObjectDataSourceWizardForm form = new ObjectDataSourceWizardForm(site, this);
				DialogResult dialogResult = UIServiceHelper.ShowDialog(site, form);
				if (dialogResult == DialogResult.OK)
				{
					this.OnDataSourceChanged(EventArgs.Empty);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				this.ResumeDataSourceEvents();
			}
			return result;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002E7E8 File Offset: 0x0002C9E8
		private static DataTable[] ConvertSchemaToDataTables(TypeSchema schema)
		{
			if (schema == null)
			{
				return null;
			}
			IDataSourceViewSchema[] views = schema.GetViews();
			if (views == null)
			{
				return null;
			}
			DataTable[] array = new DataTable[views.Length];
			for (int i = 0; i < views.Length; i++)
			{
				IDataSourceViewSchema dataSourceViewSchema = views[i];
				array[i] = new DataTable(dataSourceViewSchema.Name);
				IDataSourceFieldSchema[] fields = dataSourceViewSchema.GetFields();
				if (fields != null)
				{
					List<DataColumn> list = new List<DataColumn>();
					foreach (IDataSourceFieldSchema dataSourceFieldSchema in fields)
					{
						DataColumn dataColumn = new DataColumn();
						dataColumn.AllowDBNull = dataSourceFieldSchema.Nullable;
						dataColumn.AutoIncrement = dataSourceFieldSchema.Identity;
						dataColumn.ColumnName = dataSourceFieldSchema.Name;
						dataColumn.DataType = dataSourceFieldSchema.DataType;
						if (dataColumn.DataType == typeof(string))
						{
							dataColumn.MaxLength = dataSourceFieldSchema.Length;
						}
						dataColumn.ReadOnly = dataSourceFieldSchema.IsReadOnly;
						dataColumn.Unique = dataSourceFieldSchema.IsUnique;
						array[i].Columns.Add(dataColumn);
						if (dataSourceFieldSchema.PrimaryKey)
						{
							list.Add(dataColumn);
						}
					}
					if (list.Count > 0)
					{
						array[i].PrimaryKey = list.ToArray();
					}
				}
			}
			return array;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002E928 File Offset: 0x0002CB28
		private static Parameter CreateMergedParameter(ParameterInfo methodParameter, Parameter[] parameters)
		{
			foreach (Parameter parameter in parameters)
			{
				if (ObjectDataSourceDesigner.ParametersMatch(methodParameter, parameter))
				{
					return parameter;
				}
			}
			Parameter parameter2 = new Parameter(methodParameter.Name);
			if (methodParameter.IsOut)
			{
				parameter2.Direction = ParameterDirection.Output;
			}
			else if (methodParameter.ParameterType.IsByRef)
			{
				parameter2.Direction = ParameterDirection.InputOutput;
			}
			else
			{
				parameter2.Direction = ParameterDirection.Input;
			}
			ObjectDataSourceDesigner.SetParameterType(parameter2, methodParameter.ParameterType);
			return parameter2;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002E99C File Offset: 0x0002CB9C
		internal static Type GetType(IServiceProvider serviceProvider, string typeName, bool silent)
		{
			ITypeResolutionService typeResolutionService = null;
			if (serviceProvider != null)
			{
				typeResolutionService = (ITypeResolutionService)serviceProvider.GetService(typeof(ITypeResolutionService));
			}
			if (typeResolutionService == null)
			{
				return null;
			}
			Type result;
			try
			{
				result = typeResolutionService.GetType(typeName, true, true);
			}
			catch (Exception ex)
			{
				if (!silent)
				{
					UIServiceHelper.ShowError(serviceProvider, ex, SR.GetString("ObjectDataSourceDesigner_CannotGetType", new object[]
					{
						typeName
					}));
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002EA0C File Offset: 0x0002CC0C
		private static Type RemoveNullableFromType(Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				type = type.GetGenericArguments()[0];
			}
			else if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			return type;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002EA4C File Offset: 0x0002CC4C
		private static DbType GetDbTypeForType(Type type)
		{
			type = ObjectDataSourceDesigner.RemoveNullableFromType(type);
			if (typeof(DateTimeOffset).IsAssignableFrom(type))
			{
				return DbType.DateTimeOffset;
			}
			if (typeof(TimeSpan).IsAssignableFrom(type))
			{
				return DbType.Time;
			}
			if (typeof(Guid).IsAssignableFrom(type))
			{
				return DbType.Guid;
			}
			return DbType.Object;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002EAA4 File Offset: 0x0002CCA4
		private static TypeCode GetTypeCodeForType(Type type)
		{
			type = ObjectDataSourceDesigner.RemoveNullableFromType(type);
			if (typeof(bool).IsAssignableFrom(type))
			{
				return TypeCode.Boolean;
			}
			if (typeof(byte).IsAssignableFrom(type))
			{
				return TypeCode.Byte;
			}
			if (typeof(char).IsAssignableFrom(type))
			{
				return TypeCode.Char;
			}
			if (typeof(DateTime).IsAssignableFrom(type))
			{
				return TypeCode.DateTime;
			}
			if (typeof(DBNull).IsAssignableFrom(type))
			{
				return TypeCode.DBNull;
			}
			if (typeof(decimal).IsAssignableFrom(type))
			{
				return TypeCode.Decimal;
			}
			if (typeof(double).IsAssignableFrom(type))
			{
				return TypeCode.Double;
			}
			if (typeof(short).IsAssignableFrom(type))
			{
				return TypeCode.Int16;
			}
			if (typeof(int).IsAssignableFrom(type))
			{
				return TypeCode.Int32;
			}
			if (typeof(long).IsAssignableFrom(type))
			{
				return TypeCode.Int64;
			}
			if (typeof(sbyte).IsAssignableFrom(type))
			{
				return TypeCode.SByte;
			}
			if (typeof(float).IsAssignableFrom(type))
			{
				return TypeCode.Single;
			}
			if (typeof(string).IsAssignableFrom(type))
			{
				return TypeCode.String;
			}
			if (typeof(ushort).IsAssignableFrom(type))
			{
				return TypeCode.UInt16;
			}
			if (typeof(uint).IsAssignableFrom(type))
			{
				return TypeCode.UInt32;
			}
			if (typeof(ulong).IsAssignableFrom(type))
			{
				return TypeCode.UInt64;
			}
			return TypeCode.Object;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002EC04 File Offset: 0x0002CE04
		public override DesignerDataSourceView GetView(string viewName)
		{
			string[] viewNames = this.GetViewNames();
			if (viewNames != null && viewNames.Length != 0)
			{
				if (string.IsNullOrEmpty(viewName))
				{
					viewName = viewNames[0];
				}
				foreach (string b in viewNames)
				{
					if (string.Equals(viewName, b, StringComparison.OrdinalIgnoreCase))
					{
						return new ObjectDesignerDataSourceView(this, viewName);
					}
				}
				return null;
			}
			return new ObjectDesignerDataSourceView(this, string.Empty);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002EC64 File Offset: 0x0002CE64
		public override string[] GetViewNames()
		{
			List<string> list = new List<string>();
			DataTable[] array = this.LoadSchema();
			if (array != null && array.Length != 0)
			{
				foreach (DataTable dataTable in array)
				{
					list.Add(dataTable.TableName);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002ECB0 File Offset: 0x0002CEB0
		internal static bool IsMatchingMethod(MethodInfo method, string methodName, ParameterCollection parameters, Type dataObjectType)
		{
			if (!string.Equals(methodName, method.Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			ParameterInfo[] parameters2 = method.GetParameters();
			if (dataObjectType != null && ((parameters2.Length == 1 && parameters2[0].ParameterType == dataObjectType) || (parameters2.Length == 2 && parameters2[0].ParameterType == dataObjectType && parameters2[1].ParameterType == dataObjectType)))
			{
				return true;
			}
			if (parameters2.Length != parameters.Count)
			{
				return false;
			}
			Hashtable hashtable = new Hashtable(StringComparer.Create(CultureInfo.InvariantCulture, true));
			foreach (object obj in parameters)
			{
				Parameter parameter = (Parameter)obj;
				if (!hashtable.Contains(parameter.Name))
				{
					hashtable.Add(parameter.Name, null);
				}
			}
			foreach (ParameterInfo parameterInfo in parameters2)
			{
				if (!hashtable.Contains(parameterInfo.Name))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002EDC8 File Offset: 0x0002CFC8
		internal DataTable[] LoadSchema()
		{
			if (!this._forceSchemaRetrieval)
			{
				string a = base.DesignerState["DataSourceSchemaTypeName"] as string;
				string a2 = base.DesignerState["DataSourceSchemaSelectMethod"] as string;
				if (!string.Equals(a, this.TypeName, StringComparison.OrdinalIgnoreCase) || !string.Equals(a2, this.SelectMethod, StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
			}
			DataTable[] array = null;
			Pair pair = base.DesignerState["DataSourceSchema"] as Pair;
			if (pair != null)
			{
				string[] array2 = pair.First as string[];
				DataTable[] array3 = pair.Second as DataTable[];
				if (array2 != null && array3 != null)
				{
					int num = array2.Length;
					array = new DataTable[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = array3[i].Clone();
						array[i].TableName = array2[i];
					}
				}
			}
			return array;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002EEA4 File Offset: 0x0002D0A4
		internal static Parameter[] MergeParameters(Parameter[] parameters, MethodInfo methodInfo)
		{
			ParameterInfo[] parameters2 = methodInfo.GetParameters();
			Parameter[] array = new Parameter[parameters2.Length];
			for (int i = 0; i < parameters2.Length; i++)
			{
				ParameterInfo methodParameter = parameters2[i];
				array[i] = ObjectDataSourceDesigner.CreateMergedParameter(methodParameter, parameters);
			}
			return array;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
		internal static void MergeParameters(ParameterCollection parameters, MethodInfo methodInfo, Type dataObjectType)
		{
			Parameter[] array = new Parameter[parameters.Count];
			parameters.CopyTo(array, 0);
			parameters.Clear();
			if (methodInfo == null)
			{
				return;
			}
			if (dataObjectType == null)
			{
				ParameterInfo[] parameters2 = methodInfo.GetParameters();
				foreach (ParameterInfo methodParameter in parameters2)
				{
					Parameter parameter = ObjectDataSourceDesigner.CreateMergedParameter(methodParameter, array);
					if (parameters[parameter.Name] == null)
					{
						parameters.Add(parameter);
					}
				}
			}
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002EF58 File Offset: 0x0002D158
		private static bool ParametersMatch(ParameterInfo methodParameter, Parameter parameter)
		{
			if (!string.Equals(methodParameter.Name, parameter.Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			switch (parameter.Direction)
			{
			case ParameterDirection.Input:
				if (methodParameter.IsOut || methodParameter.ParameterType.IsByRef)
				{
					return false;
				}
				break;
			case ParameterDirection.Output:
				if (!methodParameter.IsOut)
				{
					return false;
				}
				break;
			case ParameterDirection.InputOutput:
				if (!methodParameter.ParameterType.IsByRef)
				{
					return false;
				}
				break;
			case ParameterDirection.ReturnValue:
				return false;
			}
			DbType dbTypeForType = ObjectDataSourceDesigner.GetDbTypeForType(methodParameter.ParameterType);
			if (dbTypeForType != DbType.Object)
			{
				return dbTypeForType == parameter.DbType;
			}
			TypeCode typeCodeForType = ObjectDataSourceDesigner.GetTypeCodeForType(methodParameter.ParameterType);
			return ((typeCodeForType == TypeCode.Object || typeCodeForType == TypeCode.Empty) && (parameter.Type == TypeCode.Object || parameter.Type == TypeCode.Empty)) || typeCodeForType == parameter.Type;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002F020 File Offset: 0x0002D220
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor oldPropertyDescriptor = (PropertyDescriptor)properties["TypeName"];
			properties["TypeName"] = TypeDescriptor.CreateProperty(base.GetType(), oldPropertyDescriptor, new Attribute[0]);
			oldPropertyDescriptor = (PropertyDescriptor)properties["SelectMethod"];
			properties["SelectMethod"] = TypeDescriptor.CreateProperty(base.GetType(), oldPropertyDescriptor, new Attribute[0]);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002F090 File Offset: 0x0002D290
		public override void RefreshSchema(bool preferSilent)
		{
			try
			{
				this.SuppressDataSourceEvents();
				Cursor value = Cursor.Current;
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					Type type = ObjectDataSourceDesigner.GetType(base.Component.Site, this.TypeName, preferSilent);
					if (!(type == null))
					{
						MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
						MethodInfo methodInfo = null;
						MethodInfo methodInfo2 = null;
						bool flag = false;
						Type dataObjectType = null;
						if (!string.IsNullOrEmpty(this.ObjectDataSource.DataObjectTypeName))
						{
							dataObjectType = ObjectDataSourceDesigner.GetType(base.Component.Site, this.ObjectDataSource.DataObjectTypeName, preferSilent);
						}
						foreach (MethodInfo methodInfo3 in methods)
						{
							if (string.Equals(methodInfo3.Name, this.SelectMethod, StringComparison.OrdinalIgnoreCase))
							{
								if (methodInfo2 != null && methodInfo2.ReturnType != methodInfo3.ReturnType)
								{
									flag = true;
								}
								else
								{
									methodInfo2 = methodInfo3;
								}
								if (ObjectDataSourceDesigner.IsMatchingMethod(methodInfo3, this.SelectMethod, this.ObjectDataSource.SelectParameters, dataObjectType))
								{
									methodInfo = methodInfo3;
									break;
								}
							}
						}
						if (methodInfo == null && methodInfo2 != null && !flag)
						{
							methodInfo = methodInfo2;
						}
						if (methodInfo != null)
						{
							this.RefreshSchema(methodInfo.ReflectedType, methodInfo.Name, methodInfo.ReturnType, preferSilent);
						}
					}
				}
				finally
				{
					Cursor.Current = value;
				}
			}
			finally
			{
				this.ResumeDataSourceEvents();
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002F218 File Offset: 0x0002D418
		internal void RefreshSchema(Type objectType, string methodName, Type schemaType, bool preferSilent)
		{
			if (objectType != null && !string.IsNullOrEmpty(methodName) && schemaType != null)
			{
				try
				{
					TypeSchema schema = new TypeSchema(schemaType);
					this._forceSchemaRetrieval = true;
					DataTable[] array = this.LoadSchema();
					this._forceSchemaRetrieval = false;
					IDataSourceSchema schema2 = (array == null) ? null : new ObjectDataSourceDesigner.DataTableArraySchema(array);
					this.SaveSchema(objectType, methodName, ObjectDataSourceDesigner.ConvertSchemaToDataTables(schema), schemaType);
					DataTable[] array2 = this.LoadSchema();
					IDataSourceSchema schema3 = (array2 == null) ? null : new ObjectDataSourceDesigner.DataTableArraySchema(array2);
					if (!DataSourceDesigner.SchemasEquivalent(schema2, schema3))
					{
						this.OnSchemaRefreshed(EventArgs.Empty);
					}
				}
				catch (Exception ex)
				{
					if (!preferSilent)
					{
						UIServiceHelper.ShowError(base.Component.Site, ex, SR.GetString("ObjectDataSourceDesigner_CannotGetSchema", new object[]
						{
							schemaType.FullName
						}));
					}
				}
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
		private void SaveSchema(Type objectType, string methodName, DataTable[] schemaTables, Type schemaType)
		{
			Pair value = null;
			if (schemaTables != null)
			{
				int num = schemaTables.Length;
				string[] array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = schemaTables[i].TableName;
					schemaTables[i].TableName = "Table" + i.ToString(CultureInfo.InvariantCulture);
				}
				value = new Pair(array, schemaTables);
			}
			base.DesignerState["DataSourceSchema"] = value;
			base.DesignerState["DataSourceSchemaTypeName"] = ((objectType == null) ? string.Empty : objectType.FullName);
			base.DesignerState["DataSourceSchemaSelectMethod"] = methodName;
			string a = base.DesignerState["DataSourceSchemaSelectMethodReturnTypeName"] as string;
			if (!string.Equals(a, schemaType.FullName, StringComparison.OrdinalIgnoreCase))
			{
				base.DesignerState["DataSourceSchemaSelectMethodReturnTypeName"] = schemaType.FullName;
				this._selectMethodReturnType = schemaType;
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002F3DE File Offset: 0x0002D5DE
		internal static void SetParameterType(Parameter parameter, Type type)
		{
			parameter.DbType = ObjectDataSourceDesigner.GetDbTypeForType(type);
			if (parameter.DbType == DbType.Object)
			{
				parameter.Type = ObjectDataSourceDesigner.GetTypeCodeForType(type);
				return;
			}
			parameter.Type = TypeCode.Empty;
		}

		// Token: 0x040004DE RID: 1246
		internal const BindingFlags MethodFilter = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;

		// Token: 0x040004DF RID: 1247
		private const string DesignerStateDataSourceSchemaKey = "DataSourceSchema";

		// Token: 0x040004E0 RID: 1248
		private const string DesignerStateDataSourceSchemaTypeNameKey = "DataSourceSchemaTypeName";

		// Token: 0x040004E1 RID: 1249
		private const string DesignerStateDataSourceSchemaSelectMethodKey = "DataSourceSchemaSelectMethod";

		// Token: 0x040004E2 RID: 1250
		private const string DesignerStateDataSourceSchemaSelectMethodReturnTypeNameKey = "DataSourceSchemaSelectMethodReturnTypeName";

		// Token: 0x040004E3 RID: 1251
		private const string DesignerStateShowOnlyDataComponentsStateKey = "ShowOnlyDataComponentsState";

		// Token: 0x040004E4 RID: 1252
		private bool _inWizard;

		// Token: 0x040004E5 RID: 1253
		private Type _selectMethodReturnType;

		// Token: 0x040004E6 RID: 1254
		private bool _forceSchemaRetrieval;

		// Token: 0x02000415 RID: 1045
		private sealed class DataTableArraySchema : IDataSourceSchema
		{
			// Token: 0x06002814 RID: 10260 RVA: 0x000F4F9D File Offset: 0x000F319D
			public DataTableArraySchema(DataTable[] tables)
			{
				this._tables = tables;
			}

			// Token: 0x06002815 RID: 10261 RVA: 0x000F4FAC File Offset: 0x000F31AC
			public IDataSourceViewSchema[] GetViews()
			{
				DataSetViewSchema[] array = new DataSetViewSchema[this._tables.Length];
				for (int i = 0; i < this._tables.Length; i++)
				{
					array[i] = new DataSetViewSchema(this._tables[i]);
				}
				return array;
			}

			// Token: 0x04001C8E RID: 7310
			private DataTable[] _tables;
		}
	}
}
