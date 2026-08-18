using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004FE RID: 1278
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class FieldOffsetAttribute : Attribute
	{
		// Token: 0x0600318F RID: 12687 RVA: 0x000A980C File Offset: 0x000A880C
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			int offset;
			if (field.DeclaringType != null && field.Module.MetadataImport.GetFieldOffset(field.DeclaringType.MetadataToken, field.MetadataToken, out offset))
			{
				return new FieldOffsetAttribute(offset);
			}
			return null;
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000A9851 File Offset: 0x000A8851
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return FieldOffsetAttribute.GetCustomAttribute(field) != null;
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000A985F File Offset: 0x000A885F
		public FieldOffsetAttribute(int offset)
		{
			this._val = offset;
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x000A986E File Offset: 0x000A886E
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040019A0 RID: 6560
		internal int _val;
	}
}
