using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Web.DataAccess;
using System.Web.Hosting;

namespace System.Web.Util
{
	// Token: 0x0200021E RID: 542
	internal static class SecUtility
	{
		// Token: 0x06001A0A RID: 6666 RVA: 0x00051634 File Offset: 0x0004F834
		internal static string GetDefaultAppName()
		{
			string result;
			try
			{
				string text = HostingEnvironment.ApplicationVirtualPath;
				if (string.IsNullOrEmpty(text))
				{
					text = Process.GetCurrentProcess().MainModule.ModuleName;
					int num = text.IndexOf('.');
					if (num != -1)
					{
						text = text.Remove(num);
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					result = "/";
				}
				else
				{
					result = text;
				}
			}
			catch
			{
				result = "/";
			}
			return result;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000516A4 File Offset: 0x0004F8A4
		internal static string GetConnectionString(NameValueCollection config)
		{
			string text = config["connectionString"];
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			string text2 = config["connectionStringName"];
			if (string.IsNullOrEmpty(text2))
			{
				throw new ProviderException(SR.GetString("Connection_name_not_specified"));
			}
			text = SqlConnectionHelper.GetConnectionString(text2, true, true);
			if (string.IsNullOrEmpty(text))
			{
				throw new ProviderException(SR.GetString("Connection_string_not_found", new object[]
				{
					text2
				}));
			}
			return text;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00051717 File Offset: 0x0004F917
		internal static bool ValidatePasswordParameter(ref string param, int maxSize)
		{
			return param != null && param.Length >= 1 && (maxSize <= 0 || param.Length <= maxSize);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0005173C File Offset: 0x0004F93C
		internal static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize)
		{
			if (param == null)
			{
				return !checkForNull;
			}
			param = param.Trim();
			return (!checkIfEmpty || param.Length >= 1) && (maxSize <= 0 || param.Length <= maxSize) && (!checkForCommas || !param.Contains(","));
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0005178C File Offset: 0x0004F98C
		internal static void CheckPasswordParameter(ref string param, int maxSize, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
			}
			if (param.Length < 1)
			{
				throw new ArgumentException(SR.GetString("Parameter_can_not_be_empty", new object[]
				{
					paramName
				}), paramName);
			}
			if (maxSize > 0 && param.Length > maxSize)
			{
				throw new ArgumentException(SR.GetString("Parameter_too_long", new object[]
				{
					paramName,
					maxSize.ToString(CultureInfo.InvariantCulture)
				}), paramName);
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00051804 File Offset: 0x0004FA04
		internal static void CheckParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
		{
			if (param == null)
			{
				if (checkForNull)
				{
					throw new ArgumentNullException(paramName);
				}
				return;
			}
			else
			{
				param = param.Trim();
				if (checkIfEmpty && param.Length < 1)
				{
					throw new ArgumentException(SR.GetString("Parameter_can_not_be_empty", new object[]
					{
						paramName
					}), paramName);
				}
				if (maxSize > 0 && param.Length > maxSize)
				{
					throw new ArgumentException(SR.GetString("Parameter_too_long", new object[]
					{
						paramName,
						maxSize.ToString(CultureInfo.InvariantCulture)
					}), paramName);
				}
				if (checkForCommas && param.Contains(","))
				{
					throw new ArgumentException(SR.GetString("Parameter_can_not_contain_comma", new object[]
					{
						paramName
					}), paramName);
				}
				return;
			}
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000518C0 File Offset: 0x0004FAC0
		internal static void CheckArrayParameter(ref string[] param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
		{
			if (param == null)
			{
				throw new ArgumentNullException(paramName);
			}
			if (param.Length < 1)
			{
				throw new ArgumentException(SR.GetString("Parameter_array_empty", new object[]
				{
					paramName
				}), paramName);
			}
			Hashtable hashtable = new Hashtable(param.Length);
			for (int i = param.Length - 1; i >= 0; i--)
			{
				SecUtility.CheckParameter(ref param[i], checkForNull, checkIfEmpty, checkForCommas, maxSize, paramName + "[ " + i.ToString(CultureInfo.InvariantCulture) + " ]");
				if (hashtable.Contains(param[i]))
				{
					throw new ArgumentException(SR.GetString("Parameter_duplicate_array_element", new object[]
					{
						paramName
					}), paramName);
				}
				hashtable.Add(param[i], param[i]);
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00051980 File Offset: 0x0004FB80
		internal static bool GetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
		{
			string text = config[valueName];
			if (text == null)
			{
				return defaultValue;
			}
			bool result;
			if (bool.TryParse(text, out result))
			{
				return result;
			}
			throw new ProviderException(SR.GetString("Value_must_be_boolean", new object[]
			{
				valueName
			}));
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000519C0 File Offset: 0x0004FBC0
		internal static int GetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
		{
			string text = config[valueName];
			if (text == null)
			{
				return defaultValue;
			}
			int num;
			if (!int.TryParse(text, out num))
			{
				if (zeroAllowed)
				{
					throw new ProviderException(SR.GetString("Value_must_be_non_negative_integer", new object[]
					{
						valueName
					}));
				}
				throw new ProviderException(SR.GetString("Value_must_be_positive_integer", new object[]
				{
					valueName
				}));
			}
			else
			{
				if (zeroAllowed && num < 0)
				{
					throw new ProviderException(SR.GetString("Value_must_be_non_negative_integer", new object[]
					{
						valueName
					}));
				}
				if (!zeroAllowed && num <= 0)
				{
					throw new ProviderException(SR.GetString("Value_must_be_positive_integer", new object[]
					{
						valueName
					}));
				}
				if (maxValueAllowed > 0 && num > maxValueAllowed)
				{
					throw new ProviderException(SR.GetString("Value_too_big", new object[]
					{
						valueName,
						maxValueAllowed.ToString(CultureInfo.InvariantCulture)
					}));
				}
				return num;
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00051A94 File Offset: 0x0004FC94
		internal static TimeUnit GetTimeoutUnit(NameValueCollection config, string valueName, TimeUnit defaultValue)
		{
			string text = config[valueName];
			TimeUnit result;
			if (text == null || !Enum.TryParse<TimeUnit>(text, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00051ABC File Offset: 0x0004FCBC
		internal static int? GetNullableIntValue(NameValueCollection config, string valueName)
		{
			string text = config[valueName];
			int value;
			if (text == null || !int.TryParse(text, out value))
			{
				return null;
			}
			return new int?(value);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00051AF0 File Offset: 0x0004FCF0
		internal static void CheckSchemaVersion(ProviderBase provider, SqlConnection connection, string[] features, string version, ref int schemaVersionCheck)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (features == null)
			{
				throw new ArgumentNullException("features");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			if (schemaVersionCheck == -1)
			{
				throw new ProviderException(SR.GetString("Provider_Schema_Version_Not_Match", new object[]
				{
					provider.ToString(),
					version
				}));
			}
			if (schemaVersionCheck == 0)
			{
				lock (provider)
				{
					if (schemaVersionCheck == -1)
					{
						throw new ProviderException(SR.GetString("Provider_Schema_Version_Not_Match", new object[]
						{
							provider.ToString(),
							version
						}));
					}
					if (schemaVersionCheck == 0)
					{
						foreach (string value in features)
						{
							SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_CheckSchemaVersion", connection);
							sqlCommand.CommandType = CommandType.StoredProcedure;
							SqlParameter sqlParameter = new SqlParameter("@Feature", value);
							sqlCommand.Parameters.Add(sqlParameter);
							sqlParameter = new SqlParameter("@CompatibleSchemaVersion", version);
							sqlCommand.Parameters.Add(sqlParameter);
							sqlParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
							sqlParameter.Direction = ParameterDirection.ReturnValue;
							sqlCommand.Parameters.Add(sqlParameter);
							sqlCommand.ExecuteNonQuery();
							int num = (sqlParameter.Value != null) ? ((int)sqlParameter.Value) : -1;
							if (num != 0)
							{
								schemaVersionCheck = -1;
								throw new ProviderException(SR.GetString("Provider_Schema_Version_Not_Match", new object[]
								{
									provider.ToString(),
									version
								}));
							}
						}
						schemaVersionCheck = 1;
					}
				}
			}
		}

		// Token: 0x0200094B RID: 2379
		internal class RandomByteBuffer : IDisposable
		{
			// Token: 0x06006997 RID: 27031 RVA: 0x001778CC File Offset: 0x00175ACC
			public RandomByteBuffer(int size)
			{
				this._rng = new RNGCryptoServiceProvider();
				this._size = size;
				this._buf = new byte[this._size];
				this._idx = 0;
				this._rng.GetBytes(this._buf);
			}

			// Token: 0x06006998 RID: 27032 RVA: 0x0017791C File Offset: 0x00175B1C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public byte GetByte(byte maxVal = 255)
			{
				uint num = (uint)maxVal | (uint)maxVal >> 1;
				num |= num >> 2;
				num |= num >> 4;
				byte b;
				do
				{
					if (this._idx == this._size)
					{
						this._idx = 0;
						this._rng.GetBytes(this._buf);
					}
					byte[] buf = this._buf;
					int idx = this._idx;
					this._idx = idx + 1;
					b = (byte)(buf[idx] & num);
				}
				while (b > maxVal);
				return b;
			}

			// Token: 0x06006999 RID: 27033 RVA: 0x00177984 File Offset: 0x00175B84
			public void Dispose()
			{
				this._rng.Dispose();
			}

			// Token: 0x040037CF RID: 14287
			private RandomNumberGenerator _rng;

			// Token: 0x040037D0 RID: 14288
			private byte[] _buf;

			// Token: 0x040037D1 RID: 14289
			private int _size;

			// Token: 0x040037D2 RID: 14290
			private int _idx;
		}
	}
}
