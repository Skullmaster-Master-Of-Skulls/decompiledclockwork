using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200007A RID: 122
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class TypeSchema : IDataSourceSchema
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x00012584 File Offset: 0x00010784
		public TypeSchema(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._type = type;
			if (typeof(DataTable).IsAssignableFrom(this._type))
			{
				this._schema = TypeSchema.GetDataTableSchema(this._type);
				return;
			}
			if (typeof(DataSet).IsAssignableFrom(this._type))
			{
				this._schema = TypeSchema.GetDataSetSchema(this._type);
				return;
			}
			if (TypeSchema.IsBoundGenericEnumerable(this._type))
			{
				this._schema = TypeSchema.GetGenericEnumerableSchema(this._type);
				return;
			}
			if (typeof(IEnumerable).IsAssignableFrom(this._type))
			{
				this._schema = TypeSchema.GetEnumerableSchema(this._type);
				return;
			}
			this._schema = TypeSchema.GetTypeSchema(this._type);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001265D File Offset: 0x0001085D
		public IDataSourceViewSchema[] GetViews()
		{
			return this._schema;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00012668 File Offset: 0x00010868
		private static IDataSourceViewSchema[] GetDataSetSchema(Type t)
		{
			IDataSourceViewSchema[] result;
			try
			{
				DataSet dataSet = Activator.CreateInstance(t) as DataSet;
				List<IDataSourceViewSchema> list = new List<IDataSourceViewSchema>();
				foreach (object obj in dataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					list.Add(new DataSetViewSchema(dataTable));
				}
				result = list.ToArray();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000126FC File Offset: 0x000108FC
		private static IDataSourceViewSchema[] GetDataTableSchema(Type t)
		{
			IDataSourceViewSchema[] result;
			try
			{
				DataTable dataTable = Activator.CreateInstance(t) as DataTable;
				DataSetViewSchema dataSetViewSchema = new DataSetViewSchema(dataTable);
				result = new IDataSourceViewSchema[]
				{
					dataSetViewSchema
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00012740 File Offset: 0x00010940
		private static IDataSourceViewSchema[] GetEnumerableSchema(Type t)
		{
			TypeEnumerableViewSchema typeEnumerableViewSchema = new TypeEnumerableViewSchema(string.Empty, t);
			return new IDataSourceViewSchema[]
			{
				typeEnumerableViewSchema
			};
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00012764 File Offset: 0x00010964
		private static IDataSourceViewSchema[] GetGenericEnumerableSchema(Type t)
		{
			TypeGenericEnumerableViewSchema typeGenericEnumerableViewSchema = new TypeGenericEnumerableViewSchema(string.Empty, t);
			return new IDataSourceViewSchema[]
			{
				typeGenericEnumerableViewSchema
			};
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012788 File Offset: 0x00010988
		private static IDataSourceViewSchema[] GetTypeSchema(Type t)
		{
			TypeViewSchema typeViewSchema = new TypeViewSchema(string.Empty, t);
			return new IDataSourceViewSchema[]
			{
				typeViewSchema
			};
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000127AC File Offset: 0x000109AC
		internal static bool IsBoundGenericEnumerable(Type t)
		{
			Type[] array;
			if (t.IsInterface && t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				array = new Type[]
				{
					t
				};
			}
			else
			{
				array = t.GetInterfaces();
			}
			foreach (Type type in array)
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					Type[] genericArguments = type.GetGenericArguments();
					return !genericArguments[0].IsGenericParameter;
				}
			}
			return false;
		}

		// Token: 0x0400019B RID: 411
		private Type _type;

		// Token: 0x0400019C RID: 412
		private IDataSourceViewSchema[] _schema;
	}
}
