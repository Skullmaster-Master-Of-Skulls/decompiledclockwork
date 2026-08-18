using System;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x02000072 RID: 114
	public struct StringHandle : IEquatable<StringHandle>
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x0000A7E6 File Offset: 0x000089E6
		private StringHandle(uint value)
		{
			this._value = value;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000A7EF File Offset: 0x000089EF
		internal static StringHandle FromOffset(int heapOffset)
		{
			return new StringHandle((uint)(0 | heapOffset));
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000A7F9 File Offset: 0x000089F9
		internal static StringHandle FromVirtualIndex(StringHandle.VirtualIndex virtualIndex)
		{
			return new StringHandle((uint)((StringHandle.VirtualIndex)(-2147483648) | virtualIndex));
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000A807 File Offset: 0x00008A07
		internal StringHandle WithWinRTPrefix()
		{
			return new StringHandle(2684354560U | this._value);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000A81A File Offset: 0x00008A1A
		internal StringHandle WithDotTermination()
		{
			return new StringHandle(536870912U | this._value);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000A82D File Offset: 0x00008A2D
		internal StringHandle SuffixRaw(int prefixByteLength)
		{
			return new StringHandle(0U | this._value + (uint)prefixByteLength);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000A83E File Offset: 0x00008A3E
		public static implicit operator Handle(StringHandle handle)
		{
			return new Handle((byte)((handle._value & 2147483648U) >> 24 | 120U | (handle._value & 1610612736U) >> 26), (int)(handle._value & 536870911U));
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000A874 File Offset: 0x00008A74
		public static explicit operator StringHandle(Handle handle)
		{
			if (((int)handle.VType & -132) != 120)
			{
				Throw.InvalidCast();
			}
			return new StringHandle((uint)((int)(handle.VType & 128) << 24 | (int)(handle.VType & 3) << 29 | handle.Offset));
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000A8C1 File Offset: 0x00008AC1
		internal bool IsVirtual
		{
			get
			{
				return (this._value & 2147483648U) > 0U;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000A8D2 File Offset: 0x00008AD2
		public bool IsNil
		{
			get
			{
				return (this._value & 2684354559U) == 0U;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000A8E3 File Offset: 0x00008AE3
		internal int GetHeapOffset()
		{
			return (int)(this._value & 536870911U);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000A8E3 File Offset: 0x00008AE3
		internal StringHandle.VirtualIndex GetVirtualIndex()
		{
			return (StringHandle.VirtualIndex)(this._value & 536870911U);
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000A8F1 File Offset: 0x00008AF1
		internal StringKind StringKind
		{
			get
			{
				return (StringKind)(this._value >> 29);
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000A8FD File Offset: 0x00008AFD
		public override bool Equals(object obj)
		{
			return obj is StringHandle && this.Equals((StringHandle)obj);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000A915 File Offset: 0x00008B15
		public bool Equals(StringHandle other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000A925 File Offset: 0x00008B25
		public override int GetHashCode()
		{
			return (int)this._value;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000A92D File Offset: 0x00008B2D
		public static bool operator ==(StringHandle left, StringHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000A937 File Offset: 0x00008B37
		public static bool operator !=(StringHandle left, StringHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000341 RID: 833
		private readonly uint _value;

		// Token: 0x02000185 RID: 389
		internal enum VirtualIndex
		{
			// Token: 0x040009A8 RID: 2472
			System_Runtime_WindowsRuntime,
			// Token: 0x040009A9 RID: 2473
			System_Runtime,
			// Token: 0x040009AA RID: 2474
			System_ObjectModel,
			// Token: 0x040009AB RID: 2475
			System_Runtime_WindowsRuntime_UI_Xaml,
			// Token: 0x040009AC RID: 2476
			System_Runtime_InteropServices_WindowsRuntime,
			// Token: 0x040009AD RID: 2477
			System_Numerics_Vectors,
			// Token: 0x040009AE RID: 2478
			Dispose,
			// Token: 0x040009AF RID: 2479
			AttributeTargets,
			// Token: 0x040009B0 RID: 2480
			AttributeUsageAttribute,
			// Token: 0x040009B1 RID: 2481
			Color,
			// Token: 0x040009B2 RID: 2482
			CornerRadius,
			// Token: 0x040009B3 RID: 2483
			DateTimeOffset,
			// Token: 0x040009B4 RID: 2484
			Duration,
			// Token: 0x040009B5 RID: 2485
			DurationType,
			// Token: 0x040009B6 RID: 2486
			EventHandler1,
			// Token: 0x040009B7 RID: 2487
			EventRegistrationToken,
			// Token: 0x040009B8 RID: 2488
			Exception,
			// Token: 0x040009B9 RID: 2489
			GeneratorPosition,
			// Token: 0x040009BA RID: 2490
			GridLength,
			// Token: 0x040009BB RID: 2491
			GridUnitType,
			// Token: 0x040009BC RID: 2492
			ICommand,
			// Token: 0x040009BD RID: 2493
			IDictionary2,
			// Token: 0x040009BE RID: 2494
			IDisposable,
			// Token: 0x040009BF RID: 2495
			IEnumerable,
			// Token: 0x040009C0 RID: 2496
			IEnumerable1,
			// Token: 0x040009C1 RID: 2497
			IList,
			// Token: 0x040009C2 RID: 2498
			IList1,
			// Token: 0x040009C3 RID: 2499
			INotifyCollectionChanged,
			// Token: 0x040009C4 RID: 2500
			INotifyPropertyChanged,
			// Token: 0x040009C5 RID: 2501
			IReadOnlyDictionary2,
			// Token: 0x040009C6 RID: 2502
			IReadOnlyList1,
			// Token: 0x040009C7 RID: 2503
			KeyTime,
			// Token: 0x040009C8 RID: 2504
			KeyValuePair2,
			// Token: 0x040009C9 RID: 2505
			Matrix,
			// Token: 0x040009CA RID: 2506
			Matrix3D,
			// Token: 0x040009CB RID: 2507
			Matrix3x2,
			// Token: 0x040009CC RID: 2508
			Matrix4x4,
			// Token: 0x040009CD RID: 2509
			NotifyCollectionChangedAction,
			// Token: 0x040009CE RID: 2510
			NotifyCollectionChangedEventArgs,
			// Token: 0x040009CF RID: 2511
			NotifyCollectionChangedEventHandler,
			// Token: 0x040009D0 RID: 2512
			Nullable1,
			// Token: 0x040009D1 RID: 2513
			Plane,
			// Token: 0x040009D2 RID: 2514
			Point,
			// Token: 0x040009D3 RID: 2515
			PropertyChangedEventArgs,
			// Token: 0x040009D4 RID: 2516
			PropertyChangedEventHandler,
			// Token: 0x040009D5 RID: 2517
			Quaternion,
			// Token: 0x040009D6 RID: 2518
			Rect,
			// Token: 0x040009D7 RID: 2519
			RepeatBehavior,
			// Token: 0x040009D8 RID: 2520
			RepeatBehaviorType,
			// Token: 0x040009D9 RID: 2521
			Size,
			// Token: 0x040009DA RID: 2522
			System,
			// Token: 0x040009DB RID: 2523
			System_Collections,
			// Token: 0x040009DC RID: 2524
			System_Collections_Generic,
			// Token: 0x040009DD RID: 2525
			System_Collections_Specialized,
			// Token: 0x040009DE RID: 2526
			System_ComponentModel,
			// Token: 0x040009DF RID: 2527
			System_Numerics,
			// Token: 0x040009E0 RID: 2528
			System_Windows_Input,
			// Token: 0x040009E1 RID: 2529
			Thickness,
			// Token: 0x040009E2 RID: 2530
			TimeSpan,
			// Token: 0x040009E3 RID: 2531
			Type,
			// Token: 0x040009E4 RID: 2532
			Uri,
			// Token: 0x040009E5 RID: 2533
			Vector2,
			// Token: 0x040009E6 RID: 2534
			Vector3,
			// Token: 0x040009E7 RID: 2535
			Vector4,
			// Token: 0x040009E8 RID: 2536
			Windows_Foundation,
			// Token: 0x040009E9 RID: 2537
			Windows_UI,
			// Token: 0x040009EA RID: 2538
			Windows_UI_Xaml,
			// Token: 0x040009EB RID: 2539
			Windows_UI_Xaml_Controls_Primitives,
			// Token: 0x040009EC RID: 2540
			Windows_UI_Xaml_Media,
			// Token: 0x040009ED RID: 2541
			Windows_UI_Xaml_Media_Animation,
			// Token: 0x040009EE RID: 2542
			Windows_UI_Xaml_Media_Media3D,
			// Token: 0x040009EF RID: 2543
			Count
		}
	}
}
