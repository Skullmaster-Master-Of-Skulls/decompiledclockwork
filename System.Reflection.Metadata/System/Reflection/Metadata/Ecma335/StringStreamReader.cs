using System;
using System.Diagnostics;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000CC RID: 204
	internal struct StringStreamReader
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x0001681C File Offset: 0x00014A1C
		internal StringStreamReader(MemoryBlock block, MetadataKind metadataKind)
		{
			if (StringStreamReader.s_virtualValues == null && metadataKind != MetadataKind.Ecma335)
			{
				StringStreamReader.s_virtualValues = new string[]
				{
					"System.Runtime.WindowsRuntime",
					"System.Runtime",
					"System.ObjectModel",
					"System.Runtime.WindowsRuntime.UI.Xaml",
					"System.Runtime.InteropServices.WindowsRuntime",
					"System.Numerics.Vectors",
					"Dispose",
					"AttributeTargets",
					"AttributeUsageAttribute",
					"Color",
					"CornerRadius",
					"DateTimeOffset",
					"Duration",
					"DurationType",
					"EventHandler`1",
					"EventRegistrationToken",
					"Exception",
					"GeneratorPosition",
					"GridLength",
					"GridUnitType",
					"ICommand",
					"IDictionary`2",
					"IDisposable",
					"IEnumerable",
					"IEnumerable`1",
					"IList",
					"IList`1",
					"INotifyCollectionChanged",
					"INotifyPropertyChanged",
					"IReadOnlyDictionary`2",
					"IReadOnlyList`1",
					"KeyTime",
					"KeyValuePair`2",
					"Matrix",
					"Matrix3D",
					"Matrix3x2",
					"Matrix4x4",
					"NotifyCollectionChangedAction",
					"NotifyCollectionChangedEventArgs",
					"NotifyCollectionChangedEventHandler",
					"Nullable`1",
					"Plane",
					"Point",
					"PropertyChangedEventArgs",
					"PropertyChangedEventHandler",
					"Quaternion",
					"Rect",
					"RepeatBehavior",
					"RepeatBehaviorType",
					"Size",
					"System",
					"System.Collections",
					"System.Collections.Generic",
					"System.Collections.Specialized",
					"System.ComponentModel",
					"System.Numerics",
					"System.Windows.Input",
					"Thickness",
					"TimeSpan",
					"Type",
					"Uri",
					"Vector2",
					"Vector3",
					"Vector4",
					"Windows.Foundation",
					"Windows.UI",
					"Windows.UI.Xaml",
					"Windows.UI.Xaml.Controls.Primitives",
					"Windows.UI.Xaml.Media",
					"Windows.UI.Xaml.Media.Animation",
					"Windows.UI.Xaml.Media.Media3D"
				};
			}
			this.Block = StringStreamReader.TrimEnd(block);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00016AC8 File Offset: 0x00014CC8
		[Conditional("DEBUG")]
		private static void AssertFilled()
		{
			for (int i = 0; i < StringStreamReader.s_virtualValues.Length; i++)
			{
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00016AE8 File Offset: 0x00014CE8
		private static MemoryBlock TrimEnd(MemoryBlock block)
		{
			if (block.Length == 0)
			{
				return block;
			}
			int num = block.Length - 1;
			while (num >= 0 && block.PeekByte(num) == 0)
			{
				num--;
			}
			if (num == block.Length - 1)
			{
				return block;
			}
			return block.GetMemoryBlockAt(0, num + 2);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00016B34 File Offset: 0x00014D34
		internal string GetVirtualValue(StringHandle.VirtualIndex index)
		{
			return StringStreamReader.s_virtualValues[(int)index];
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00016B40 File Offset: 0x00014D40
		internal string GetString(StringHandle handle, MetadataStringDecoder utf8Decoder)
		{
			byte[] prefix;
			if (handle.IsVirtual)
			{
				StringKind stringKind = handle.StringKind;
				if (stringKind == StringKind.Virtual)
				{
					return StringStreamReader.s_virtualValues[(int)handle.GetVirtualIndex()];
				}
				if (stringKind != StringKind.WinRTPrefixed)
				{
					return null;
				}
				prefix = MetadataReader.WinRTPrefix;
			}
			else
			{
				prefix = null;
			}
			char terminator = (handle.StringKind == StringKind.DotTerminated) ? '.' : '\0';
			int num;
			return this.Block.PeekUtf8NullTerminated(handle.GetHeapOffset(), prefix, utf8Decoder, out num, terminator);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00016BB0 File Offset: 0x00014DB0
		internal StringHandle GetNextHandle(StringHandle handle)
		{
			if (handle.IsVirtual)
			{
				return default(StringHandle);
			}
			int num = this.Block.IndexOf(0, handle.GetHeapOffset());
			if (num == -1 || num == this.Block.Length - 1)
			{
				return default(StringHandle);
			}
			return StringHandle.FromOffset(num + 1);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00016C10 File Offset: 0x00014E10
		internal bool Equals(StringHandle handle, string value, MetadataStringDecoder utf8Decoder, bool ignoreCase)
		{
			if (handle.IsVirtual)
			{
				return string.Equals(this.GetString(handle, utf8Decoder), value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			if (handle.IsNil)
			{
				return value.Length == 0;
			}
			char terminator = (handle.StringKind == StringKind.DotTerminated) ? '.' : '\0';
			return this.Block.Utf8NullTerminatedEquals(handle.GetHeapOffset(), value, utf8Decoder, terminator, ignoreCase);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00016C7C File Offset: 0x00014E7C
		internal bool StartsWith(StringHandle handle, string value, MetadataStringDecoder utf8Decoder, bool ignoreCase)
		{
			if (handle.IsVirtual)
			{
				return this.GetString(handle, utf8Decoder).StartsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			if (handle.IsNil)
			{
				return value.Length == 0;
			}
			char terminator = (handle.StringKind == StringKind.DotTerminated) ? '.' : '\0';
			return this.Block.Utf8NullTerminatedStartsWith(handle.GetHeapOffset(), value, utf8Decoder, terminator, ignoreCase);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00016CE8 File Offset: 0x00014EE8
		internal bool EqualsRaw(StringHandle rawHandle, string asciiString)
		{
			return this.Block.CompareUtf8NullTerminatedStringWithAsciiString(rawHandle.GetHeapOffset(), asciiString) == 0;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00016D10 File Offset: 0x00014F10
		internal int IndexOfRaw(int startIndex, char asciiChar)
		{
			return this.Block.Utf8NullTerminatedOffsetOfAsciiChar(startIndex, asciiChar);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00016D30 File Offset: 0x00014F30
		internal bool StartsWithRaw(StringHandle rawHandle, string asciiPrefix)
		{
			return this.Block.Utf8NullTerminatedStringStartsWithAsciiPrefix(rawHandle.GetHeapOffset(), asciiPrefix);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00016D54 File Offset: 0x00014F54
		internal int BinarySearchRaw(string[] asciiKeys, StringHandle rawHandle)
		{
			return this.Block.BinarySearch(asciiKeys, rawHandle.GetHeapOffset());
		}

		// Token: 0x040005AD RID: 1453
		private static string[] s_virtualValues;

		// Token: 0x040005AE RID: 1454
		internal readonly MemoryBlock Block;
	}
}
