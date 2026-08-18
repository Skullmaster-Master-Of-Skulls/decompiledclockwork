using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Reflection;

namespace System.Data
{
	// Token: 0x02000132 RID: 306
	internal sealed class TypeLimiter
	{
		// Token: 0x06001201 RID: 4609 RVA: 0x00089E80 File Offset: 0x00089280
		private TypeLimiter(TypeLimiter.Scope scope)
		{
			this.m_instanceScope = scope;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00089E9C File Offset: 0x0008929C
		private static bool IsTypeLimitingEnabled
		{
			get
			{
				bool flag = false;
				return !AppContext.TryGetSwitch("Switch.System.Data.AllowArbitraryDataSetTypeInstantiation", out flag) || !flag;
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00089EC0 File Offset: 0x000892C0
		public static TypeLimiter Capture()
		{
			TypeLimiter.Scope scope = TypeLimiter.s_activeScope;
			if (scope == null)
			{
				return null;
			}
			return new TypeLimiter(scope);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00089EE0 File Offset: 0x000892E0
		public static void EnsureTypeIsAllowed(Type type, TypeLimiter capturedLimiter = null)
		{
			if (type == null)
			{
				return;
			}
			TypeLimiter.Scope scope = (capturedLimiter == null) ? null : capturedLimiter.m_instanceScope;
			scope = ((scope == null) ? TypeLimiter.s_activeScope : scope);
			if (scope == null)
			{
				return;
			}
			if (scope.IsAllowedType(type))
			{
				return;
			}
			DataSetTraceSource.TraceTypeNotAllowed(type);
			if (!SerializationConfig.IsAuditMode())
			{
				throw ExceptionBuilder.TypeNotAllowed(type);
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00089F34 File Offset: 0x00089334
		public static IDisposable EnterRestrictedScope(DataSet dataSet)
		{
			if (!TypeLimiter.IsTypeLimitingEnabled)
			{
				return null;
			}
			TypeLimiter.Scope result = new TypeLimiter.Scope(TypeLimiter.s_activeScope, TypeLimiter.GetPreviouslyDeclaredDataTypes(dataSet));
			TypeLimiter.s_activeScope = result;
			return result;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00089F64 File Offset: 0x00089364
		public static IDisposable EnterRestrictedScope(DataTable dataTable)
		{
			if (!TypeLimiter.IsTypeLimitingEnabled)
			{
				return null;
			}
			TypeLimiter.Scope result = new TypeLimiter.Scope(TypeLimiter.s_activeScope, TypeLimiter.GetPreviouslyDeclaredDataTypes(dataTable));
			TypeLimiter.s_activeScope = result;
			return result;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00089F94 File Offset: 0x00089394
		private static IEnumerable<Type> GetPreviouslyDeclaredDataTypes(DataTable dataTable)
		{
			if (dataTable == null)
			{
				return Enumerable.Empty<Type>();
			}
			return from DataColumn column in dataTable.Columns
			select column.DataType;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00089FDC File Offset: 0x000893DC
		private static IEnumerable<Type> GetPreviouslyDeclaredDataTypes(DataSet dataSet)
		{
			if (dataSet == null)
			{
				return Enumerable.Empty<Type>();
			}
			return dataSet.Tables.Cast<DataTable>().SelectMany((DataTable table) => TypeLimiter.GetPreviouslyDeclaredDataTypes(table));
		}

		// Token: 0x04000645 RID: 1605
		[ThreadStatic]
		private static TypeLimiter.Scope s_activeScope;

		// Token: 0x04000646 RID: 1606
		private TypeLimiter.Scope m_instanceScope;

		// Token: 0x04000647 RID: 1607
		private const string AppDomainDataSetDefaultAllowedTypesKey = "System.Data.DataSetDefaultAllowedTypes";

		// Token: 0x04000648 RID: 1608
		private const string AppContextOptOutSwitchName = "Switch.System.Data.AllowArbitraryDataSetTypeInstantiation";

		// Token: 0x02000362 RID: 866
		private sealed class Scope : IDisposable
		{
			// Token: 0x0600343E RID: 13374 RVA: 0x0014049C File Offset: 0x0013F89C
			static Scope()
			{
				Assembly assembly = null;
				try
				{
					assembly = Assembly.Load("System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				}
				catch
				{
				}
				if (assembly != null)
				{
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.Color", true));
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.Point", true));
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.PointF", true));
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.Rectangle", true));
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.RectangleF", true));
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.Size", true));
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.SizeF", true));
				}
			}

			// Token: 0x0600343F RID: 13375 RVA: 0x00140824 File Offset: 0x0013FC24
			internal Scope(TypeLimiter.Scope previousScope, IEnumerable<Type> allowedTypes)
			{
				this.m_previousScope = previousScope;
				this.m_allowedTypes = new HashSet<Type>(from type in allowedTypes
				where type != null
				select type);
			}

			// Token: 0x06003440 RID: 13376 RVA: 0x00140870 File Offset: 0x0013FC70
			public void Dispose()
			{
				if (this != TypeLimiter.s_activeScope)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				TypeLimiter.s_activeScope = this.m_previousScope;
			}

			// Token: 0x06003441 RID: 13377 RVA: 0x001408A4 File Offset: 0x0013FCA4
			public bool IsAllowedType(Type type)
			{
				if (TypeLimiter.Scope.IsTypeUnconditionallyAllowed(type))
				{
					return true;
				}
				for (TypeLimiter.Scope scope = this; scope != null; scope = scope.m_previousScope)
				{
					if (scope.m_allowedTypes.Contains(type))
					{
						return true;
					}
				}
				Type[] array = (Type[])AppDomain.CurrentDomain.GetData("System.Data.DataSetDefaultAllowedTypes");
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (type == array[i])
						{
							return true;
						}
					}
				}
				return SerializationConfig.IsTypeAllowed(type);
			}

			// Token: 0x06003442 RID: 13378 RVA: 0x00140918 File Offset: 0x0013FD18
			private static bool IsTypeUnconditionallyAllowed(Type type)
			{
				while (!TypeLimiter.Scope.s_allowedTypes.Contains(type))
				{
					Type baseType = type.BaseType;
					while (baseType != null)
					{
						if (TypeLimiter.Scope.s_allowedSuperTypes.Contains(baseType))
						{
							return true;
						}
						baseType = baseType.BaseType;
					}
					if (type.IsArray && type.GetArrayRank() == 1)
					{
						type = type.GetElementType();
					}
					else
					{
						if (!type.IsGenericType || type.IsGenericTypeDefinition || !(type.GetGenericTypeDefinition() == typeof(List<>)))
						{
							return false;
						}
						type = type.GetGenericArguments()[0];
					}
				}
				return true;
			}

			// Token: 0x04001F07 RID: 7943
			private static readonly HashSet<Type> s_allowedTypes = new HashSet<Type>
			{
				typeof(bool),
				typeof(char),
				typeof(sbyte),
				typeof(byte),
				typeof(short),
				typeof(ushort),
				typeof(int),
				typeof(uint),
				typeof(long),
				typeof(ulong),
				typeof(float),
				typeof(double),
				typeof(decimal),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(string),
				typeof(Guid),
				typeof(SqlBinary),
				typeof(SqlBoolean),
				typeof(SqlByte),
				typeof(SqlBytes),
				typeof(SqlChars),
				typeof(SqlDateTime),
				typeof(SqlDecimal),
				typeof(SqlDouble),
				typeof(SqlGuid),
				typeof(SqlInt16),
				typeof(SqlInt32),
				typeof(SqlInt64),
				typeof(SqlMoney),
				typeof(SqlSingle),
				typeof(SqlString),
				typeof(object),
				typeof(Type),
				typeof(BigInteger),
				typeof(Uri)
			};

			// Token: 0x04001F08 RID: 7944
			private static readonly HashSet<Type> s_allowedSuperTypes = new HashSet<Type>
			{
				typeof(Enum)
			};

			// Token: 0x04001F09 RID: 7945
			private HashSet<Type> m_allowedTypes;

			// Token: 0x04001F0A RID: 7946
			private readonly TypeLimiter.Scope m_previousScope;
		}
	}
}
