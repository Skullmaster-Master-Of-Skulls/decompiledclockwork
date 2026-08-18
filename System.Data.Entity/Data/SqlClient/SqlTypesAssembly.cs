using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Spatial;
using System.Data.SqlClient.Internal;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x02000028 RID: 40
	internal sealed class SqlTypesAssembly
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000D060 File Offset: 0x0000B260
		private static SqlTypesAssembly BindToLatest()
		{
			Assembly assembly = null;
			foreach (string assemblyName in SqlTypesAssembly.preferredSqlTypesAssemblies)
			{
				AssemblyName assemblyRef = new AssemblyName(assemblyName);
				try
				{
					assembly = Assembly.Load(assemblyRef);
					break;
				}
				catch (FileNotFoundException)
				{
				}
				catch (FileLoadException)
				{
				}
			}
			if (assembly != null)
			{
				return new SqlTypesAssembly(assembly);
			}
			return null;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		internal static bool TryGetSqlTypesAssembly(Assembly assembly, out SqlTypesAssembly sqlAssembly)
		{
			if (SqlTypesAssembly.IsKnownAssembly(assembly))
			{
				sqlAssembly = new SqlTypesAssembly(assembly);
				return true;
			}
			sqlAssembly = null;
			return false;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000D100 File Offset: 0x0000B300
		private static bool IsKnownAssembly(Assembly assembly)
		{
			foreach (string assemblyName in SqlTypesAssembly.preferredSqlTypesAssemblies)
			{
				if (EntityUtil.AssemblyNamesMatch(assembly.FullName, new AssemblyName(assemblyName)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000D160 File Offset: 0x0000B360
		internal static SqlTypesAssembly Latest
		{
			get
			{
				return SqlTypesAssembly.latestVersion.Value;
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000D16C File Offset: 0x0000B36C
		private SqlTypesAssembly(Assembly sqlSpatialAssembly)
		{
			Type type = sqlSpatialAssembly.GetType("Microsoft.SqlServer.Types.SqlGeography", true);
			Type type2 = sqlSpatialAssembly.GetType("Microsoft.SqlServer.Types.SqlGeometry", true);
			this.SqlGeographyType = type;
			this.sqlGeographyFromWKTString = SqlTypesAssembly.CreateStaticConstructorDelegate<string>(type, "STGeomFromText");
			this.sqlGeographyFromWKBByteArray = SqlTypesAssembly.CreateStaticConstructorDelegate<byte[]>(type, "STGeomFromWKB");
			this.sqlGeographyFromGMLReader = SqlTypesAssembly.CreateStaticConstructorDelegate<XmlReader>(type, "GeomFromGml");
			this.SqlGeometryType = type2;
			this.sqlGeometryFromWKTString = SqlTypesAssembly.CreateStaticConstructorDelegate<string>(type2, "STGeomFromText");
			this.sqlGeometryFromWKBByteArray = SqlTypesAssembly.CreateStaticConstructorDelegate<byte[]>(type2, "STGeomFromWKB");
			this.sqlGeometryFromGMLReader = SqlTypesAssembly.CreateStaticConstructorDelegate<XmlReader>(type2, "GeomFromGml");
			MethodInfo method = this.SqlGeometryType.GetMethod("STAsText", BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null);
			this.SqlCharsType = method.ReturnType;
			this.SqlStringType = this.SqlCharsType.Assembly.GetType("System.Data.SqlTypes.SqlString", true);
			this.SqlBooleanType = this.SqlCharsType.Assembly.GetType("System.Data.SqlTypes.SqlBoolean", true);
			this.SqlBytesType = this.SqlCharsType.Assembly.GetType("System.Data.SqlTypes.SqlBytes", true);
			this.SqlDoubleType = this.SqlCharsType.Assembly.GetType("System.Data.SqlTypes.SqlDouble", true);
			this.SqlInt32Type = this.SqlCharsType.Assembly.GetType("System.Data.SqlTypes.SqlInt32", true);
			this.SqlXmlType = this.SqlCharsType.Assembly.GetType("System.Data.SqlTypes.SqlXml", true);
			this.sqlBytesFromByteArray = Expressions.Lambda<byte[], object>("binaryValue", (ParameterExpression bytesVal) => SqlTypesAssembly.BuildConvertToSqlBytes(bytesVal, this.SqlBytesType)).Compile();
			this.sqlStringFromString = Expressions.Lambda<string, object>("stringValue", (ParameterExpression stringVal) => SqlTypesAssembly.BuildConvertToSqlString(stringVal, this.SqlStringType)).Compile();
			this.sqlCharsFromString = Expressions.Lambda<string, object>("stringValue", (ParameterExpression stringVal) => SqlTypesAssembly.BuildConvertToSqlChars(stringVal, this.SqlCharsType)).Compile();
			this.sqlXmlFromXmlReader = Expressions.Lambda<XmlReader, object>("readerVaue", (ParameterExpression readerVal) => SqlTypesAssembly.BuildConvertToSqlXml(readerVal, this.SqlXmlType)).Compile();
			this.sqlBooleanToBoolean = Expressions.Lambda<object, bool>("sqlBooleanValue", (ParameterExpression sqlBoolVal) => sqlBoolVal.ConvertTo(this.SqlBooleanType).ConvertTo<bool>()).Compile();
			this.sqlBooleanToNullableBoolean = Expressions.Lambda<object, bool?>("sqlBooleanValue", (ParameterExpression sqlBoolVal) => sqlBoolVal.ConvertTo(this.SqlBooleanType).Property("IsNull").IfTrueThen(Expressions.Null<bool?>()).Else(sqlBoolVal.ConvertTo(this.SqlBooleanType).ConvertTo<bool>().ConvertTo<bool?>())).Compile();
			this.sqlBytesToByteArray = Expressions.Lambda<object, byte[]>("sqlBytesValue", (ParameterExpression sqlBytesVal) => sqlBytesVal.ConvertTo(this.SqlBytesType).Property("Value")).Compile();
			this.sqlCharsToString = Expressions.Lambda<object, string>("sqlCharsValue", (ParameterExpression sqlCharsVal) => sqlCharsVal.ConvertTo(this.SqlCharsType).Call("ToSqlString").Property("Value")).Compile();
			this.sqlStringToString = Expressions.Lambda<object, string>("sqlStringValue", (ParameterExpression sqlStringVal) => sqlStringVal.ConvertTo(this.SqlStringType).Property("Value")).Compile();
			this.sqlDoubleToDouble = Expressions.Lambda<object, double>("sqlDoubleValue", (ParameterExpression sqlDoubleVal) => sqlDoubleVal.ConvertTo(this.SqlDoubleType).ConvertTo<double>()).Compile();
			this.sqlDoubleToNullableDouble = Expressions.Lambda<object, double?>("sqlDoubleValue", (ParameterExpression sqlDoubleVal) => sqlDoubleVal.ConvertTo(this.SqlDoubleType).Property("IsNull").IfTrueThen(Expressions.Null<double?>()).Else(sqlDoubleVal.ConvertTo(this.SqlDoubleType).ConvertTo<double>().ConvertTo<double?>())).Compile();
			this.sqlInt32ToInt = Expressions.Lambda<object, int>("sqlInt32Value", (ParameterExpression sqlInt32Val) => sqlInt32Val.ConvertTo(this.SqlInt32Type).ConvertTo<int>()).Compile();
			this.sqlInt32ToNullableInt = Expressions.Lambda<object, int?>("sqlInt32Value", (ParameterExpression sqlInt32Val) => sqlInt32Val.ConvertTo(this.SqlInt32Type).Property("IsNull").IfTrueThen(Expressions.Null<int?>()).Else(sqlInt32Val.ConvertTo(this.SqlInt32Type).ConvertTo<int>().ConvertTo<int?>())).Compile();
			this.sqlXmlToString = Expressions.Lambda<object, string>("sqlXmlValue", (ParameterExpression sqlXmlVal) => sqlXmlVal.ConvertTo(this.SqlXmlType).Property("Value")).Compile();
			this.isSqlGeographyNull = Expressions.Lambda<object, bool>("sqlGeographyValue", (ParameterExpression sqlGeographyValue) => sqlGeographyValue.ConvertTo(this.SqlGeographyType).Property("IsNull")).Compile();
			this.isSqlGeometryNull = Expressions.Lambda<object, bool>("sqlGeometryValue", (ParameterExpression sqlGeometryValue) => sqlGeometryValue.ConvertTo(this.SqlGeometryType).Property("IsNull")).Compile();
			this.geographyAsTextZMAsSqlChars = Expressions.Lambda<object, object>("sqlGeographyValue", (ParameterExpression sqlGeographyValue) => sqlGeographyValue.ConvertTo(this.SqlGeographyType).Call("AsTextZM")).Compile();
			this.geometryAsTextZMAsSqlChars = Expressions.Lambda<object, object>("sqlGeometryValue", (ParameterExpression sqlGeometryValue) => sqlGeometryValue.ConvertTo(this.SqlGeometryType).Call("AsTextZM")).Compile();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000D52D File Offset: 0x0000B72D
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000D535 File Offset: 0x0000B735
		internal Type SqlBooleanType { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000D53E File Offset: 0x0000B73E
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000D546 File Offset: 0x0000B746
		internal Type SqlBytesType { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000D54F File Offset: 0x0000B74F
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000D557 File Offset: 0x0000B757
		internal Type SqlCharsType { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000D560 File Offset: 0x0000B760
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000D568 File Offset: 0x0000B768
		internal Type SqlStringType { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000D571 File Offset: 0x0000B771
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000D579 File Offset: 0x0000B779
		internal Type SqlDoubleType { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000D582 File Offset: 0x0000B782
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000D58A File Offset: 0x0000B78A
		internal Type SqlInt32Type { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000D593 File Offset: 0x0000B793
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000D59B File Offset: 0x0000B79B
		internal Type SqlXmlType { get; private set; }

		// Token: 0x0600037D RID: 893 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		internal bool SqlBooleanToBoolean(object sqlBooleanValue)
		{
			return this.sqlBooleanToBoolean(sqlBooleanValue);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		internal bool? SqlBooleanToNullableBoolean(object sqlBooleanValue)
		{
			if (this.sqlBooleanToBoolean == null)
			{
				return null;
			}
			return this.sqlBooleanToNullableBoolean(sqlBooleanValue);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000D5DF File Offset: 0x0000B7DF
		internal object SqlBytesFromByteArray(byte[] binaryValue)
		{
			return this.sqlBytesFromByteArray(binaryValue);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000D5ED File Offset: 0x0000B7ED
		internal byte[] SqlBytesToByteArray(object sqlBytesValue)
		{
			if (sqlBytesValue == null)
			{
				return null;
			}
			return this.sqlBytesToByteArray(sqlBytesValue);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000D600 File Offset: 0x0000B800
		internal object SqlStringFromString(string stringValue)
		{
			return this.sqlStringFromString(stringValue);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000D60E File Offset: 0x0000B80E
		internal object SqlCharsFromString(string stringValue)
		{
			return this.sqlCharsFromString(stringValue);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000D61C File Offset: 0x0000B81C
		internal string SqlCharsToString(object sqlCharsValue)
		{
			if (sqlCharsValue == null)
			{
				return null;
			}
			return this.sqlCharsToString(sqlCharsValue);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000D62F File Offset: 0x0000B82F
		internal string SqlStringToString(object sqlStringValue)
		{
			if (sqlStringValue == null)
			{
				return null;
			}
			return this.sqlStringToString(sqlStringValue);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000D642 File Offset: 0x0000B842
		internal double SqlDoubleToDouble(object sqlDoubleValue)
		{
			return this.sqlDoubleToDouble(sqlDoubleValue);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000D650 File Offset: 0x0000B850
		internal double? SqlDoubleToNullableDouble(object sqlDoubleValue)
		{
			if (sqlDoubleValue == null)
			{
				return null;
			}
			return this.sqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000D676 File Offset: 0x0000B876
		internal int SqlInt32ToInt(object sqlInt32Value)
		{
			return this.sqlInt32ToInt(sqlInt32Value);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000D684 File Offset: 0x0000B884
		internal int? SqlInt32ToNullableInt(object sqlInt32Value)
		{
			if (sqlInt32Value == null)
			{
				return null;
			}
			return this.sqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		internal object SqlXmlFromString(string stringValue)
		{
			XmlReader arg = SqlTypesAssembly.XmlReaderFromString(stringValue);
			return this.sqlXmlFromXmlReader(arg);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		internal string SqlXmlToString(object sqlXmlValue)
		{
			if (sqlXmlValue == null)
			{
				return null;
			}
			return this.sqlXmlToString(sqlXmlValue);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D6DF File Offset: 0x0000B8DF
		internal bool IsSqlGeographyNull(object sqlGeographyValue)
		{
			return sqlGeographyValue == null || this.isSqlGeographyNull(sqlGeographyValue);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D6F2 File Offset: 0x0000B8F2
		internal bool IsSqlGeometryNull(object sqlGeometryValue)
		{
			return sqlGeometryValue == null || this.isSqlGeometryNull(sqlGeometryValue);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D708 File Offset: 0x0000B908
		internal string GeographyAsTextZM(DbGeography geographyValue)
		{
			if (geographyValue == null)
			{
				return null;
			}
			object arg = this.ConvertToSqlTypesGeography(geographyValue);
			object sqlCharsValue = this.geographyAsTextZMAsSqlChars(arg);
			return this.SqlCharsToString(sqlCharsValue);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D738 File Offset: 0x0000B938
		internal string GeometryAsTextZM(DbGeometry geometryValue)
		{
			if (geometryValue == null)
			{
				return null;
			}
			object arg = this.ConvertToSqlTypesGeometry(geometryValue);
			object sqlCharsValue = this.geometryAsTextZMAsSqlChars(arg);
			return this.SqlCharsToString(sqlCharsValue);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000D766 File Offset: 0x0000B966
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000D76E File Offset: 0x0000B96E
		internal Type SqlGeographyType { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000D777 File Offset: 0x0000B977
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000D77F File Offset: 0x0000B97F
		internal Type SqlGeometryType { get; private set; }

		// Token: 0x06000393 RID: 915 RVA: 0x0000D788 File Offset: 0x0000B988
		internal object ConvertToSqlTypesGeography(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			return this.GetSqlTypesSpatialValue(geographyValue.AsSpatialValue(), this.SqlGeographyType);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		internal object SqlTypesGeographyFromBinary(byte[] wellKnownBinary, int srid)
		{
			return this.sqlGeographyFromWKBByteArray(wellKnownBinary, srid);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D7C3 File Offset: 0x0000B9C3
		internal object SqlTypesGeographyFromText(string wellKnownText, int srid)
		{
			return this.sqlGeographyFromWKTString(wellKnownText, srid);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		internal object ConvertToSqlTypesGeometry(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			return this.GetSqlTypesSpatialValue(geometryValue.AsSpatialValue(), this.SqlGeometryType);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000D800 File Offset: 0x0000BA00
		internal object SqlTypesGeometryFromBinary(byte[] wellKnownBinary, int srid)
		{
			return this.sqlGeometryFromWKBByteArray(wellKnownBinary, srid);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000D80F File Offset: 0x0000BA0F
		internal object SqlTypesGeometryFromText(string wellKnownText, int srid)
		{
			return this.sqlGeometryFromWKTString(wellKnownText, srid);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000D820 File Offset: 0x0000BA20
		private object GetSqlTypesSpatialValue(IDbSpatialValue spatialValue, Type requiredProviderValueType)
		{
			object providerValue = spatialValue.ProviderValue;
			if (providerValue != null && providerValue.GetType() == requiredProviderValueType)
			{
				return providerValue;
			}
			int? coordinateSystemId = spatialValue.CoordinateSystemId;
			if (coordinateSystemId != null)
			{
				byte[] wellKnownBinary = spatialValue.WellKnownBinary;
				if (wellKnownBinary != null)
				{
					if (!spatialValue.IsGeography)
					{
						return this.sqlGeometryFromWKBByteArray(wellKnownBinary, coordinateSystemId.Value);
					}
					return this.sqlGeographyFromWKBByteArray(wellKnownBinary, coordinateSystemId.Value);
				}
				else
				{
					string wellKnownText = spatialValue.WellKnownText;
					if (wellKnownText != null)
					{
						if (!spatialValue.IsGeography)
						{
							return this.sqlGeometryFromWKTString(wellKnownText, coordinateSystemId.Value);
						}
						return this.sqlGeographyFromWKTString(wellKnownText, coordinateSystemId.Value);
					}
					else
					{
						string gmlString = spatialValue.GmlString;
						if (gmlString != null)
						{
							XmlReader arg = SqlTypesAssembly.XmlReaderFromString(gmlString);
							if (!spatialValue.IsGeography)
							{
								return this.sqlGeometryFromGMLReader(arg, coordinateSystemId.Value);
							}
							return this.sqlGeographyFromGMLReader(arg, coordinateSystemId.Value);
						}
					}
				}
			}
			throw spatialValue.NotSqlCompatible();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000D91B File Offset: 0x0000BB1B
		private static XmlReader XmlReaderFromString(string stringValue)
		{
			return XmlReader.Create(new StringReader(stringValue));
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000D928 File Offset: 0x0000BB28
		private static Func<TArg, int, object> CreateStaticConstructorDelegate<TArg>(Type spatialType, string methodName)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(TArg));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(int));
			MethodInfo method = spatialType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
			Expression arg = SqlTypesAssembly.BuildConvertToSqlType(parameterExpression, method.GetParameters()[0].ParameterType);
			Expression<Func<TArg, int, object>> expression = Expression.Lambda<Func<TArg, int, object>>(Expression.Call(null, method, arg, parameterExpression2), new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			});
			return expression.Compile();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D99C File Offset: 0x0000BB9C
		private static Expression BuildConvertToSqlType(Expression toConvert, Type convertTo)
		{
			if (toConvert.Type == typeof(byte[]))
			{
				return SqlTypesAssembly.BuildConvertToSqlBytes(toConvert, convertTo);
			}
			if (toConvert.Type == typeof(string))
			{
				if (convertTo.Name == "SqlString")
				{
					return SqlTypesAssembly.BuildConvertToSqlString(toConvert, convertTo);
				}
				return SqlTypesAssembly.BuildConvertToSqlChars(toConvert, convertTo);
			}
			else
			{
				if (toConvert.Type == typeof(XmlReader))
				{
					return SqlTypesAssembly.BuildConvertToSqlXml(toConvert, convertTo);
				}
				return toConvert;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000DA24 File Offset: 0x0000BC24
		private static Expression BuildConvertToSqlBytes(Expression toConvert, Type sqlBytesType)
		{
			ConstructorInfo constructor = sqlBytesType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				toConvert.Type
			}, null);
			return Expression.New(constructor, new Expression[]
			{
				toConvert
			});
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000DA60 File Offset: 0x0000BC60
		private static Expression BuildConvertToSqlChars(Expression toConvert, Type sqlCharsType)
		{
			Type type = sqlCharsType.Assembly.GetType("System.Data.SqlTypes.SqlString", true);
			ConstructorInfo constructor = sqlCharsType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				type
			}, null);
			ConstructorInfo constructor2 = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(string)
			}, null);
			return Expression.New(constructor, new Expression[]
			{
				Expression.New(constructor2, new Expression[]
				{
					toConvert
				})
			});
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		private static Expression BuildConvertToSqlString(Expression toConvert, Type sqlStringType)
		{
			ConstructorInfo constructor = sqlStringType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(string)
			}, null);
			return Expression.Convert(Expression.New(constructor, new Expression[]
			{
				toConvert
			}), typeof(object));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000DB20 File Offset: 0x0000BD20
		private static Expression BuildConvertToSqlXml(Expression toConvert, Type sqlXmlType)
		{
			ConstructorInfo constructor = sqlXmlType.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				toConvert.Type
			}, null);
			return Expression.New(constructor, new Expression[]
			{
				toConvert
			});
		}

		// Token: 0x040006CB RID: 1739
		private static readonly ReadOnlyCollection<string> preferredSqlTypesAssemblies = new List<string>
		{
			"Microsoft.SqlServer.Types, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91",
			"Microsoft.SqlServer.Types, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
		}.AsReadOnly();

		// Token: 0x040006CC RID: 1740
		private static Singleton<SqlTypesAssembly> latestVersion = new Singleton<SqlTypesAssembly>(new Func<SqlTypesAssembly>(SqlTypesAssembly.BindToLatest));

		// Token: 0x040006D4 RID: 1748
		private readonly Func<object, bool> sqlBooleanToBoolean;

		// Token: 0x040006D5 RID: 1749
		private readonly Func<object, bool?> sqlBooleanToNullableBoolean;

		// Token: 0x040006D6 RID: 1750
		private readonly Func<byte[], object> sqlBytesFromByteArray;

		// Token: 0x040006D7 RID: 1751
		private readonly Func<object, byte[]> sqlBytesToByteArray;

		// Token: 0x040006D8 RID: 1752
		private readonly Func<string, object> sqlStringFromString;

		// Token: 0x040006D9 RID: 1753
		private readonly Func<string, object> sqlCharsFromString;

		// Token: 0x040006DA RID: 1754
		private readonly Func<object, string> sqlCharsToString;

		// Token: 0x040006DB RID: 1755
		private readonly Func<object, string> sqlStringToString;

		// Token: 0x040006DC RID: 1756
		private readonly Func<object, double> sqlDoubleToDouble;

		// Token: 0x040006DD RID: 1757
		private readonly Func<object, double?> sqlDoubleToNullableDouble;

		// Token: 0x040006DE RID: 1758
		private readonly Func<object, int> sqlInt32ToInt;

		// Token: 0x040006DF RID: 1759
		private readonly Func<object, int?> sqlInt32ToNullableInt;

		// Token: 0x040006E0 RID: 1760
		private readonly Func<XmlReader, object> sqlXmlFromXmlReader;

		// Token: 0x040006E1 RID: 1761
		private readonly Func<object, string> sqlXmlToString;

		// Token: 0x040006E2 RID: 1762
		private readonly Func<object, bool> isSqlGeographyNull;

		// Token: 0x040006E3 RID: 1763
		private readonly Func<object, bool> isSqlGeometryNull;

		// Token: 0x040006E4 RID: 1764
		private readonly Func<object, object> geographyAsTextZMAsSqlChars;

		// Token: 0x040006E5 RID: 1765
		private readonly Func<object, object> geometryAsTextZMAsSqlChars;

		// Token: 0x040006E8 RID: 1768
		private readonly Func<string, int, object> sqlGeographyFromWKTString;

		// Token: 0x040006E9 RID: 1769
		private readonly Func<byte[], int, object> sqlGeographyFromWKBByteArray;

		// Token: 0x040006EA RID: 1770
		private readonly Func<XmlReader, int, object> sqlGeographyFromGMLReader;

		// Token: 0x040006EB RID: 1771
		private readonly Func<string, int, object> sqlGeometryFromWKTString;

		// Token: 0x040006EC RID: 1772
		private readonly Func<byte[], int, object> sqlGeometryFromWKBByteArray;

		// Token: 0x040006ED RID: 1773
		private readonly Func<XmlReader, int, object> sqlGeometryFromGMLReader;
	}
}
