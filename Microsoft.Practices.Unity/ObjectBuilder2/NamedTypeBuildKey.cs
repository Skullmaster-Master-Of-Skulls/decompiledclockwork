using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000074 RID: 116
	public class NamedTypeBuildKey
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x000070CC File Offset: 0x000052CC
		public NamedTypeBuildKey(Type type, string name)
		{
			this.type = type;
			this.name = ((!string.IsNullOrEmpty(name)) ? name : null);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000070ED File Offset: 0x000052ED
		public NamedTypeBuildKey(Type type) : this(type, null)
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000070F7 File Offset: 0x000052F7
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static NamedTypeBuildKey Make<T>()
		{
			return new NamedTypeBuildKey(typeof(T));
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007108 File Offset: 0x00005308
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "As designed")]
		public static NamedTypeBuildKey Make<T>(string name)
		{
			return new NamedTypeBuildKey(typeof(T), name);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000711A File Offset: 0x0000531A
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This is the type part of the key.")]
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007122 File Offset: 0x00005322
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000712C File Offset: 0x0000532C
		public override bool Equals(object obj)
		{
			NamedTypeBuildKey namedTypeBuildKey = obj as NamedTypeBuildKey;
			return !(namedTypeBuildKey == null) && this == namedTypeBuildKey;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007154 File Offset: 0x00005354
		public override int GetHashCode()
		{
			int num = (this.type == null) ? 0 : this.type.GetHashCode();
			int num2 = (this.name == null) ? 0 : this.name.GetHashCode();
			return num + 37 ^ num2 + 17;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007198 File Offset: 0x00005398
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Null is accounted for")]
		public static bool operator ==(NamedTypeBuildKey left, NamedTypeBuildKey right)
		{
			bool flag = object.ReferenceEquals(left, null);
			bool flag2 = object.ReferenceEquals(right, null);
			return (flag && flag2) || (!flag && !flag2 && left.type == right.type && string.Compare(left.name, right.name, StringComparison.Ordinal) == 0);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000071EA File Offset: 0x000053EA
		public static bool operator !=(NamedTypeBuildKey left, NamedTypeBuildKey right)
		{
			return !(left == right);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000071F8 File Offset: 0x000053F8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Build Key[{0}, {1}]", new object[]
			{
				this.type,
				this.name ?? "null"
			});
		}

		// Token: 0x04000071 RID: 113
		private readonly Type type;

		// Token: 0x04000072 RID: 114
		private readonly string name;
	}
}
