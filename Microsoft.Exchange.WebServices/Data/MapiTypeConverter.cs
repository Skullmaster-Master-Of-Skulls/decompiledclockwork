using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200029E RID: 670
	internal class MapiTypeConverter
	{
		// Token: 0x0600178D RID: 6029 RVA: 0x00040028 File Offset: 0x0003F028
		internal static Array ConvertToValue(MapiPropertyType mapiPropType, IEnumerable<string> strings)
		{
			EwsUtilities.ValidateParam(strings, "strings");
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry = MapiTypeConverter.MapiTypeConverterMap[mapiPropType];
			Array array = Array.CreateInstance(mapiTypeConverterMapEntry.Type, strings.Count<string>());
			int num = 0;
			foreach (string stringValue in strings)
			{
				object value = mapiTypeConverterMapEntry.ConvertToValueOrDefault(stringValue);
				array.SetValue(value, num++);
			}
			return array;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000400B0 File Offset: 0x0003F0B0
		internal static object ConvertToValue(MapiPropertyType mapiPropType, string stringValue)
		{
			return MapiTypeConverter.MapiTypeConverterMap[mapiPropType].ConvertToValue(stringValue);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000400C3 File Offset: 0x0003F0C3
		internal static string ConvertToString(MapiPropertyType mapiPropType, object value)
		{
			if (value != null)
			{
				return MapiTypeConverter.MapiTypeConverterMap[mapiPropType].ConvertToString.Invoke(value);
			}
			return string.Empty;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000400E4 File Offset: 0x0003F0E4
		internal static object ChangeType(MapiPropertyType mapiType, object value)
		{
			EwsUtilities.ValidateParam(value, "value");
			return MapiTypeConverter.MapiTypeConverterMap[mapiType].ChangeType(value);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00040104 File Offset: 0x0003F104
		internal static object ParseMapiIntegerValue(string s)
		{
			int num;
			if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return num;
			}
			return s;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00040129 File Offset: 0x0003F129
		internal static bool IsArrayType(MapiPropertyType mapiType)
		{
			return MapiTypeConverter.MapiTypeConverterMap[mapiType].IsArray;
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x0004013B File Offset: 0x0003F13B
		internal static Dictionary<MapiPropertyType, MapiTypeConverterMapEntry> MapiTypeConverterMap
		{
			get
			{
				return MapiTypeConverter.mapiTypeConverterMap.Member;
			}
		}

		// Token: 0x0400135D RID: 4957
		private const DateTimeStyles UtcDataTimeStyles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal;

		// Token: 0x0400135E RID: 4958
		private static LazyMember<Dictionary<MapiPropertyType, MapiTypeConverterMapEntry>> mapiTypeConverterMap = new LazyMember<Dictionary<MapiPropertyType, MapiTypeConverterMapEntry>>(delegate()
		{
			Dictionary<MapiPropertyType, MapiTypeConverterMapEntry> dictionary = new Dictionary<MapiPropertyType, MapiTypeConverterMapEntry>();
			dictionary.Add(MapiPropertyType.ApplicationTime, new MapiTypeConverterMapEntry(typeof(double)));
			dictionary.Add(MapiPropertyType.ApplicationTimeArray, new MapiTypeConverterMapEntry(typeof(double))
			{
				IsArray = true
			});
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry = new MapiTypeConverterMapEntry(typeof(byte[]));
			mapiTypeConverterMapEntry.Parse = delegate(string s)
			{
				if (!string.IsNullOrEmpty(s))
				{
					return Convert.FromBase64String(s);
				}
				return null;
			};
			mapiTypeConverterMapEntry.ConvertToString = ((object o) => Convert.ToBase64String((byte[])o));
			MapiTypeConverterMapEntry value = mapiTypeConverterMapEntry;
			dictionary.Add(MapiPropertyType.Binary, value);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry2 = new MapiTypeConverterMapEntry(typeof(byte[]));
			mapiTypeConverterMapEntry2.Parse = delegate(string s)
			{
				if (!string.IsNullOrEmpty(s))
				{
					return Convert.FromBase64String(s);
				}
				return null;
			};
			mapiTypeConverterMapEntry2.ConvertToString = ((object o) => Convert.ToBase64String((byte[])o));
			mapiTypeConverterMapEntry2.IsArray = true;
			MapiTypeConverterMapEntry value2 = mapiTypeConverterMapEntry2;
			dictionary.Add(MapiPropertyType.BinaryArray, value2);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry3 = new MapiTypeConverterMapEntry(typeof(bool));
			mapiTypeConverterMapEntry3.Parse = ((string s) => Convert.ChangeType(s, typeof(bool), CultureInfo.InvariantCulture));
			mapiTypeConverterMapEntry3.ConvertToString = ((object o) => ((bool)o).ToString(CultureInfo.InvariantCulture).ToLower());
			MapiTypeConverterMapEntry value3 = mapiTypeConverterMapEntry3;
			dictionary.Add(MapiPropertyType.Boolean, value3);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry4 = new MapiTypeConverterMapEntry(typeof(Guid));
			mapiTypeConverterMapEntry4.Parse = ((string s) => new Guid(s));
			mapiTypeConverterMapEntry4.ConvertToString = ((object o) => ((Guid)o).ToString());
			MapiTypeConverterMapEntry value4 = mapiTypeConverterMapEntry4;
			dictionary.Add(MapiPropertyType.CLSID, value4);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry5 = new MapiTypeConverterMapEntry(typeof(Guid));
			mapiTypeConverterMapEntry5.Parse = ((string s) => new Guid(s));
			mapiTypeConverterMapEntry5.ConvertToString = ((object o) => ((Guid)o).ToString());
			mapiTypeConverterMapEntry5.IsArray = true;
			MapiTypeConverterMapEntry value5 = mapiTypeConverterMapEntry5;
			dictionary.Add(MapiPropertyType.CLSIDArray, value5);
			dictionary.Add(MapiPropertyType.Currency, new MapiTypeConverterMapEntry(typeof(long)));
			dictionary.Add(MapiPropertyType.CurrencyArray, new MapiTypeConverterMapEntry(typeof(long))
			{
				IsArray = true
			});
			dictionary.Add(MapiPropertyType.Double, new MapiTypeConverterMapEntry(typeof(double)));
			dictionary.Add(MapiPropertyType.DoubleArray, new MapiTypeConverterMapEntry(typeof(double))
			{
				IsArray = true
			});
			dictionary.Add(MapiPropertyType.Error, new MapiTypeConverterMapEntry(typeof(int)));
			dictionary.Add(MapiPropertyType.Float, new MapiTypeConverterMapEntry(typeof(float)));
			dictionary.Add(MapiPropertyType.FloatArray, new MapiTypeConverterMapEntry(typeof(float))
			{
				IsArray = true
			});
			Dictionary<MapiPropertyType, MapiTypeConverterMapEntry> dictionary2 = dictionary;
			MapiPropertyType key = MapiPropertyType.Integer;
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry6 = new MapiTypeConverterMapEntry(typeof(int));
			mapiTypeConverterMapEntry6.Parse = ((string s) => MapiTypeConverter.ParseMapiIntegerValue(s));
			dictionary2.Add(key, mapiTypeConverterMapEntry6);
			dictionary.Add(MapiPropertyType.IntegerArray, new MapiTypeConverterMapEntry(typeof(int))
			{
				IsArray = true
			});
			dictionary.Add(MapiPropertyType.Long, new MapiTypeConverterMapEntry(typeof(long)));
			dictionary.Add(MapiPropertyType.LongArray, new MapiTypeConverterMapEntry(typeof(long))
			{
				IsArray = true
			});
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry7 = new MapiTypeConverterMapEntry(typeof(string));
			mapiTypeConverterMapEntry7.Parse = ((string s) => s);
			MapiTypeConverterMapEntry value6 = mapiTypeConverterMapEntry7;
			dictionary.Add(MapiPropertyType.Object, value6);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry8 = new MapiTypeConverterMapEntry(typeof(string));
			mapiTypeConverterMapEntry8.Parse = ((string s) => s);
			mapiTypeConverterMapEntry8.IsArray = true;
			MapiTypeConverterMapEntry value7 = mapiTypeConverterMapEntry8;
			dictionary.Add(MapiPropertyType.ObjectArray, value7);
			dictionary.Add(MapiPropertyType.Short, new MapiTypeConverterMapEntry(typeof(short)));
			dictionary.Add(MapiPropertyType.ShortArray, new MapiTypeConverterMapEntry(typeof(short))
			{
				IsArray = true
			});
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry9 = new MapiTypeConverterMapEntry(typeof(string));
			mapiTypeConverterMapEntry9.Parse = ((string s) => s);
			MapiTypeConverterMapEntry value8 = mapiTypeConverterMapEntry9;
			dictionary.Add(MapiPropertyType.String, value8);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry10 = new MapiTypeConverterMapEntry(typeof(string));
			mapiTypeConverterMapEntry10.Parse = ((string s) => s);
			mapiTypeConverterMapEntry10.IsArray = true;
			MapiTypeConverterMapEntry value9 = mapiTypeConverterMapEntry10;
			dictionary.Add(MapiPropertyType.StringArray, value9);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry11 = new MapiTypeConverterMapEntry(typeof(DateTime));
			mapiTypeConverterMapEntry11.Parse = ((string s) => DateTime.Parse(s, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal));
			mapiTypeConverterMapEntry11.ConvertToString = ((object o) => EwsUtilities.DateTimeToXSDateTime((DateTime)o));
			MapiTypeConverterMapEntry value10 = mapiTypeConverterMapEntry11;
			dictionary.Add(MapiPropertyType.SystemTime, value10);
			MapiTypeConverterMapEntry mapiTypeConverterMapEntry12 = new MapiTypeConverterMapEntry(typeof(DateTime));
			mapiTypeConverterMapEntry12.IsArray = true;
			mapiTypeConverterMapEntry12.Parse = ((string s) => DateTime.Parse(s, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal));
			mapiTypeConverterMapEntry12.ConvertToString = ((object o) => EwsUtilities.DateTimeToXSDateTime((DateTime)o));
			MapiTypeConverterMapEntry value11 = mapiTypeConverterMapEntry12;
			dictionary.Add(MapiPropertyType.SystemTimeArray, value11);
			return dictionary;
		});
	}
}
