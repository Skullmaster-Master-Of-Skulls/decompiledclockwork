using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Data
{
	// Token: 0x020000E8 RID: 232
	internal sealed class TypeLimiter
	{
		// Token: 0x06000D8E RID: 3470 RVA: 0x00216C18 File Offset: 0x00216018
		private TypeLimiter(TypeLimiter.Scope scope)
		{
			this.m_instanceScope = scope;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00216C38 File Offset: 0x00216038
		private static bool IsTypeLimitingEnabled
		{
			get
			{
				if (!TypeLimiter.s_isOptedOutValueInitialized)
				{
					TypeLimiter.s_isOptedOut = TypeLimiter.ReadTypeLimitingRegistrySetting();
					TypeLimiter.s_isOptedOutValueInitialized = true;
				}
				return !TypeLimiter.s_isOptedOut;
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00216C68 File Offset: 0x00216068
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool ReadTypeLimitingRegistrySetting()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\AppContext", false))
				{
					if (registryKey != null && registryKey.GetValueKind("Switch.System.Data.AllowArbitraryDataSetTypeInstantiation") == RegistryValueKind.String && "true".Equals((string)registryKey.GetValue("Switch.System.Data.AllowArbitraryDataSetTypeInstantiation"), StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00216D08 File Offset: 0x00216108
		public static TypeLimiter Capture()
		{
			TypeLimiter.Scope scope = TypeLimiter.s_activeScope;
			if (scope == null)
			{
				return null;
			}
			return new TypeLimiter(scope);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00216D28 File Offset: 0x00216128
		public static void EnsureTypeIsAllowed(Type type)
		{
			TypeLimiter.EnsureTypeIsAllowed(type, null);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00216D48 File Offset: 0x00216148
		public static void EnsureTypeIsAllowed(Type type, TypeLimiter capturedLimiter)
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

		// Token: 0x06000D94 RID: 3476 RVA: 0x00216D98 File Offset: 0x00216198
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

		// Token: 0x06000D95 RID: 3477 RVA: 0x00216DC8 File Offset: 0x002161C8
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

		// Token: 0x06000D96 RID: 3478 RVA: 0x00216DF8 File Offset: 0x002161F8
		private static IEnumerable<Type> GetPreviouslyDeclaredDataTypes(DataTable dataTable)
		{
			List<Type> list = new List<Type>();
			if (dataTable != null)
			{
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					list.Add(dataColumn.DataType);
				}
			}
			return list;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00216E78 File Offset: 0x00216278
		private static IEnumerable<Type> GetPreviouslyDeclaredDataTypes(DataSet dataSet)
		{
			List<Type> list = new List<Type>();
			if (dataSet != null)
			{
				foreach (object obj in dataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					list.AddRange(TypeLimiter.GetPreviouslyDeclaredDataTypes(dataTable));
				}
			}
			return list;
		}

		// Token: 0x04000962 RID: 2402
		private const string AppDomainDataSetDefaultAllowedTypesKey = "System.Data.DataSetDefaultAllowedTypes";

		// Token: 0x04000963 RID: 2403
		private const string AppContextOptOutSwitchName = "Switch.System.Data.AllowArbitraryDataSetTypeInstantiation";

		// Token: 0x04000964 RID: 2404
		private const string AppContextOptOutRegValuePath = "SOFTWARE\\Microsoft\\.NETFramework\\AppContext";

		// Token: 0x04000965 RID: 2405
		[ThreadStatic]
		private static TypeLimiter.Scope s_activeScope;

		// Token: 0x04000966 RID: 2406
		private TypeLimiter.Scope m_instanceScope;

		// Token: 0x04000967 RID: 2407
		private static bool s_isOptedOut;

		// Token: 0x04000968 RID: 2408
		private static volatile bool s_isOptedOutValueInitialized;

		// Token: 0x020000E9 RID: 233
		private sealed class Scope : IDisposable
		{
			// Token: 0x06000D98 RID: 3480 RVA: 0x00216EF8 File Offset: 0x002162F8
			static Scope()
			{
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(bool), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(char), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(sbyte), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(byte), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(short), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(ushort), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(int), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(uint), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(long), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(ulong), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(float), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(double), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(decimal), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(DateTime), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(DateTimeOffset), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(TimeSpan), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(string), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(Guid), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlBinary), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlBoolean), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlByte), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlBytes), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlChars), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlDateTime), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlDecimal), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlDouble), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlGuid), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlInt16), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlInt32), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlInt64), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlMoney), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlSingle), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(SqlString), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(object), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(Type), null);
				TypeLimiter.Scope.s_allowedTypes.Add(typeof(Uri), null);
				Assembly assembly = null;
				try
				{
					assembly = Assembly.Load("System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				}
				catch
				{
				}
				if (assembly != null)
				{
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.Color", true), null);
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.Point", true), null);
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.PointF", true), null);
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.Rectangle", true), null);
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.RectangleF", true), null);
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.Size", true), null);
					TypeLimiter.Scope.s_allowedTypes.Add(assembly.GetType("System.Drawing.SizeF", true), null);
				}
				TypeLimiter.Scope.s_allowedSuperTypes.Add(typeof(Enum), null);
			}

			// Token: 0x06000D99 RID: 3481 RVA: 0x002172F8 File Offset: 0x002166F8
			internal Scope(TypeLimiter.Scope previousScope, IEnumerable<Type> allowedTypes)
			{
				this.m_previousScope = previousScope;
				this.m_allowedTypes = new Dictionary<Type, object>();
				foreach (Type key in allowedTypes)
				{
					this.m_allowedTypes[key] = null;
				}
			}

			// Token: 0x06000D9A RID: 3482 RVA: 0x00217378 File Offset: 0x00216778
			public void Dispose()
			{
				if (this != TypeLimiter.s_activeScope)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				TypeLimiter.s_activeScope = this.m_previousScope;
			}

			// Token: 0x06000D9B RID: 3483 RVA: 0x002173B8 File Offset: 0x002167B8
			public bool IsAllowedType(Type type)
			{
				if (TypeLimiter.Scope.IsTypeUnconditionallyAllowed(type))
				{
					return true;
				}
				for (TypeLimiter.Scope scope = this; scope != null; scope = scope.m_previousScope)
				{
					if (scope.m_allowedTypes.ContainsKey(type))
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

			// Token: 0x06000D9C RID: 3484 RVA: 0x00217428 File Offset: 0x00216828
			private static bool IsTypeUnconditionallyAllowed(Type type)
			{
				while (!TypeLimiter.Scope.s_allowedTypes.ContainsKey(type))
				{
					for (Type baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
					{
						if (TypeLimiter.Scope.s_allowedSuperTypes.ContainsKey(baseType))
						{
							return true;
						}
					}
					if (type.IsArray && type.GetArrayRank() == 1)
					{
						type = type.GetElementType();
					}
					else
					{
						if (!type.IsGenericType || type.IsGenericTypeDefinition || type.GetGenericTypeDefinition() != typeof(List<>))
						{
							return false;
						}
						type = type.GetGenericArguments()[0];
					}
				}
				return true;
			}

			// Token: 0x04000969 RID: 2409
			private static readonly Dictionary<Type, object> s_allowedTypes = new Dictionary<Type, object>();

			// Token: 0x0400096A RID: 2410
			private static readonly Dictionary<Type, object> s_allowedSuperTypes = new Dictionary<Type, object>();

			// Token: 0x0400096B RID: 2411
			private Dictionary<Type, object> m_allowedTypes;

			// Token: 0x0400096C RID: 2412
			private readonly TypeLimiter.Scope m_previousScope;
		}
	}
}
