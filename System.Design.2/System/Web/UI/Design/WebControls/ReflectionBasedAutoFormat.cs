using System;
using System.ComponentModel;
using System.Data;
using System.Design;
using System.Globalization;
using System.Reflection;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000FB RID: 251
	internal class ReflectionBasedAutoFormat : DesignerAutoFormat
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x00032EC7 File Offset: 0x000310C7
		public ReflectionBasedAutoFormat(string schemeName, string schemes) : base(SR.GetString(schemeName))
		{
			this._schemeName = schemeName;
			this._schemes = schemes;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00032EE4 File Offset: 0x000310E4
		public override void Apply(Control control)
		{
			this.EnsureInitialized();
			foreach (object obj in this._schemeData.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				string columnName = dataColumn.ColumnName;
				if (!string.Equals(columnName, "SchemeName", StringComparison.Ordinal))
				{
					if (columnName.EndsWith("--ClearDefaults", StringComparison.Ordinal))
					{
						if (this._schemeData[columnName].ToString().Equals("true", StringComparison.OrdinalIgnoreCase))
						{
							this.ClearDefaults(control, columnName.Substring(0, columnName.Length - 15));
						}
					}
					else
					{
						this.SetPropertyValue(control, columnName, this._schemeData[columnName].ToString());
					}
				}
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00032FBC File Offset: 0x000311BC
		private void EnsureInitialized()
		{
			if (this._schemeData == null)
			{
				this._schemeData = ControlDesigner.GetSchemeDataRow(this._schemeName, this._schemes);
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00032FE0 File Offset: 0x000311E0
		private void ClearDefaults(Control control, string propertyName)
		{
			ReflectionBasedAutoFormat.InstanceAndPropertyInfo memberInfo = ReflectionBasedAutoFormat.GetMemberInfo(control, propertyName);
			if (memberInfo.PropertyInfo != null && memberInfo.Instance != null)
			{
				object value = memberInfo.PropertyInfo.GetValue(memberInfo.Instance, null);
				Type type = value.GetType();
				type.InvokeMember("ClearDefaults", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, value, new object[0], CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00033044 File Offset: 0x00031244
		private static ReflectionBasedAutoFormat.InstanceAndPropertyInfo GetMemberInfo(Control control, string name)
		{
			Type type = control.GetType();
			PropertyInfo propertyInfo = null;
			object obj = control;
			object obj2 = control;
			string text = name.Replace('-', '.');
			int i = 0;
			while (i < text.Length)
			{
				int num = text.IndexOf('.', i);
				string name2;
				if (num < 0)
				{
					name2 = text.Substring(i);
					i = text.Length;
				}
				else
				{
					name2 = text.Substring(i, num - i);
					i = num + 1;
				}
				BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
				try
				{
					propertyInfo = type.GetProperty(name2, bindingFlags);
				}
				catch (AmbiguousMatchException)
				{
					bindingFlags |= BindingFlags.DeclaredOnly;
					propertyInfo = type.GetProperty(name2, bindingFlags);
				}
				if (propertyInfo != null)
				{
					type = propertyInfo.PropertyType;
					if (obj2 != null)
					{
						obj = obj2;
						obj2 = propertyInfo.GetValue(obj, null);
					}
				}
			}
			return new ReflectionBasedAutoFormat.InstanceAndPropertyInfo(obj, propertyInfo);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00033118 File Offset: 0x00031318
		protected void SetPropertyValue(Control control, string propertyName, string propertyValue)
		{
			object value = null;
			ReflectionBasedAutoFormat.InstanceAndPropertyInfo memberInfo = ReflectionBasedAutoFormat.GetMemberInfo(control, propertyName);
			PropertyInfo propertyInfo = memberInfo.PropertyInfo;
			TypeConverter typeConverter = null;
			TypeConverterAttribute typeConverterAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(TypeConverterAttribute), true) as TypeConverterAttribute;
			if (typeConverterAttribute != null)
			{
				Type type = Type.GetType(typeConverterAttribute.ConverterTypeName, false);
				if (type != null)
				{
					typeConverter = (TypeConverter)Activator.CreateInstance(type);
				}
			}
			if (typeConverter != null && typeConverter.CanConvertFrom(typeof(string)))
			{
				value = typeConverter.ConvertFromInvariantString(propertyValue);
			}
			else
			{
				typeConverter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
				if (typeConverter != null && typeConverter.CanConvertFrom(typeof(string)))
				{
					value = typeConverter.ConvertFromInvariantString(propertyValue);
				}
			}
			propertyInfo.SetValue(memberInfo.Instance, value, null);
		}

		// Token: 0x04000537 RID: 1335
		private const char PERSIST_CHAR = '-';

		// Token: 0x04000538 RID: 1336
		private const char OM_CHAR = '.';

		// Token: 0x04000539 RID: 1337
		private DataRow _schemeData;

		// Token: 0x0400053A RID: 1338
		private readonly string _schemeName;

		// Token: 0x0400053B RID: 1339
		private readonly string _schemes;

		// Token: 0x02000428 RID: 1064
		private struct InstanceAndPropertyInfo
		{
			// Token: 0x06002884 RID: 10372 RVA: 0x000F79AE File Offset: 0x000F5BAE
			public InstanceAndPropertyInfo(object instance, PropertyInfo propertyInfo)
			{
				this.Instance = instance;
				this.PropertyInfo = propertyInfo;
			}

			// Token: 0x04001CCF RID: 7375
			public object Instance;

			// Token: 0x04001CD0 RID: 7376
			public PropertyInfo PropertyInfo;
		}
	}
}
