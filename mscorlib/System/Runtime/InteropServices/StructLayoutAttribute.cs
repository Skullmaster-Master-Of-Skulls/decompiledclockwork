using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004FD RID: 1277
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public sealed class StructLayoutAttribute : Attribute
	{
		// Token: 0x06003189 RID: 12681 RVA: 0x000A9700 File Offset: 0x000A8700
		internal static Attribute GetCustomAttribute(Type type)
		{
			if (!StructLayoutAttribute.IsDefined(type))
			{
				return null;
			}
			int num = 0;
			int size = 0;
			LayoutKind layoutKind = LayoutKind.Auto;
			TypeAttributes typeAttributes = type.Attributes & TypeAttributes.LayoutMask;
			if (typeAttributes != TypeAttributes.NotPublic)
			{
				if (typeAttributes != TypeAttributes.SequentialLayout)
				{
					if (typeAttributes == TypeAttributes.ExplicitLayout)
					{
						layoutKind = LayoutKind.Explicit;
					}
				}
				else
				{
					layoutKind = LayoutKind.Sequential;
				}
			}
			else
			{
				layoutKind = LayoutKind.Auto;
			}
			CharSet charSet = CharSet.None;
			TypeAttributes typeAttributes2 = type.Attributes & TypeAttributes.StringFormatMask;
			if (typeAttributes2 != TypeAttributes.NotPublic)
			{
				if (typeAttributes2 != TypeAttributes.UnicodeClass)
				{
					if (typeAttributes2 == TypeAttributes.AutoClass)
					{
						charSet = CharSet.Auto;
					}
				}
				else
				{
					charSet = CharSet.Unicode;
				}
			}
			else
			{
				charSet = CharSet.Ansi;
			}
			type.Module.MetadataImport.GetClassLayout(type.MetadataToken, out num, out size);
			if (num == 0)
			{
				num = 8;
			}
			return new StructLayoutAttribute(layoutKind, num, size, charSet);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000A97A1 File Offset: 0x000A87A1
		internal static bool IsDefined(Type type)
		{
			return !type.IsInterface && !type.HasElementType && !type.IsGenericParameter;
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000A97BE File Offset: 0x000A87BE
		internal StructLayoutAttribute(LayoutKind layoutKind, int pack, int size, CharSet charSet)
		{
			this._val = layoutKind;
			this.Pack = pack;
			this.Size = size;
			this.CharSet = charSet;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000A97E3 File Offset: 0x000A87E3
		public StructLayoutAttribute(LayoutKind layoutKind)
		{
			this._val = layoutKind;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000A97F2 File Offset: 0x000A87F2
		public StructLayoutAttribute(short layoutKind)
		{
			this._val = (LayoutKind)layoutKind;
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x000A9801 File Offset: 0x000A8801
		public LayoutKind Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x0400199B RID: 6555
		private const int DEFAULT_PACKING_SIZE = 8;

		// Token: 0x0400199C RID: 6556
		internal LayoutKind _val;

		// Token: 0x0400199D RID: 6557
		public int Pack;

		// Token: 0x0400199E RID: 6558
		public int Size;

		// Token: 0x0400199F RID: 6559
		public CharSet CharSet;
	}
}
