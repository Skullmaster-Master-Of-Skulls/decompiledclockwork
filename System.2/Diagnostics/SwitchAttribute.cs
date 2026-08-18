using System;
using System.Collections;
using System.Reflection;

namespace System.Diagnostics
{
	// Token: 0x020004A7 RID: 1191
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public sealed class SwitchAttribute : Attribute
	{
		// Token: 0x06002C26 RID: 11302 RVA: 0x000C7684 File Offset: 0x000C5884
		public SwitchAttribute(string switchName, Type switchType)
		{
			this.SwitchName = switchName;
			this.SwitchType = switchType;
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x000C769A File Offset: 0x000C589A
		// (set) Token: 0x06002C28 RID: 11304 RVA: 0x000C76A4 File Offset: 0x000C58A4
		public string SwitchName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[]
					{
						"value"
					}), "value");
				}
				this.name = value;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x000C76F1 File Offset: 0x000C58F1
		// (set) Token: 0x06002C2A RID: 11306 RVA: 0x000C76F9 File Offset: 0x000C58F9
		public Type SwitchType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x000C7716 File Offset: 0x000C5916
		// (set) Token: 0x06002C2C RID: 11308 RVA: 0x000C771E File Offset: 0x000C591E
		public string SwitchDescription
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000C7728 File Offset: 0x000C5928
		public static SwitchAttribute[] GetAll(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			ArrayList arrayList = new ArrayList();
			object[] customAttributes = assembly.GetCustomAttributes(typeof(SwitchAttribute), false);
			arrayList.AddRange(customAttributes);
			Type[] types = assembly.GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				SwitchAttribute.GetAllRecursive(types[i], arrayList);
			}
			SwitchAttribute[] array = new SwitchAttribute[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000C77A0 File Offset: 0x000C59A0
		private static void GetAllRecursive(Type type, ArrayList switchAttribs)
		{
			SwitchAttribute.GetAllRecursive(type, switchAttribs);
			MemberInfo[] members = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < members.Length; i++)
			{
				if (!(members[i] is Type))
				{
					SwitchAttribute.GetAllRecursive(members[i], switchAttribs);
				}
			}
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000C77E0 File Offset: 0x000C59E0
		private static void GetAllRecursive(MemberInfo member, ArrayList switchAttribs)
		{
			object[] customAttributes = member.GetCustomAttributes(typeof(SwitchAttribute), false);
			switchAttribs.AddRange(customAttributes);
		}

		// Token: 0x040026BF RID: 9919
		private Type type;

		// Token: 0x040026C0 RID: 9920
		private string name;

		// Token: 0x040026C1 RID: 9921
		private string description;
	}
}
